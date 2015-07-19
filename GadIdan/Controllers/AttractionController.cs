using GadIdan.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GadIdan.Controllers
{
    public class AttractionController : Controller
    {
        private genifacedbEntities db = new genifacedbEntities();
        // GET: Attraction
        public ActionResult Index(string attractionName, int? SelectedAttraction, int? LocationID, int page = 1)
        {
            int pageSize = 15;
            if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["PageSize"]))
            {
                if (!int.TryParse(ConfigurationManager.AppSettings["PageSize"], out pageSize))
                {
                    pageSize = 15;
                }
            }

            var tempAttractions = db.Attractions.OrderBy(q => q.AttractionName)
                                .Select(s => new { s.AttractionID, s.AttractionName }).Distinct().ToList();
            ViewBag.SelectedAttraction = new SelectList(tempAttractions, "AttractionID", "AttractionName", SelectedAttraction);
            PopulateLocationsDropDownList();

            var attractions = db.Attractions.OrderBy(o => o.AttractionName).Distinct().ToList();

            IQueryable<Attractions> list = db.Attractions
                .Where(c => (string.IsNullOrEmpty(attractionName) || c.AttractionName.Contains(attractionName)) &&
                            (!SelectedAttraction.HasValue || c.AttractionID == SelectedAttraction) &&
                            (!LocationID.HasValue || c.LocationID == LocationID))

                .OrderBy(d => d.AttractionPrice)
                .Include(d => d.Locations)
                .Include(d => d.Sites);

            PagedList<Attractions> model = new PagedList<Attractions>(list.ToList(), page, pageSize);
            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attractions attraction = db.Attractions.Where(w => w.Id == id).FirstOrDefault();
            if (attraction == null)
            {
                return HttpNotFound();
            }
            PopulateLocationsDropDownList(attraction.LocationID);
            PopulateSitesDropDownList(attraction.SiteID);
            PopulateAttractionTypeIDDropDownList(attraction.AttractionTypeID);

            return View(attraction);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var attractionToUpdate = db.Attractions.Find(id);
            if (TryUpdateModel(attractionToUpdate, "",
               new string[] { "AttractionName", "LocationID", "AttractionDate", "AttractionPrice", "Price","AttractionTypeID","AttractionTypeValue", "SiteID", "AttractionSiteUrl", "AttractionData1", "AttractionData2" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(attractionToUpdate);
        }

        // GET: Course/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PopulateLocationsDropDownList(id);
            PopulateSitesDropDownList(id);
            Attractions attraction = db.Attractions.Find(id);
            if (attraction == null)
            {
                return HttpNotFound();
            }
            return View(attraction);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Attractions attraction = db.Attractions.Find(id);
            db.Attractions.Remove(attraction);
            db.SaveChanges();
            return RedirectToAction("Index");
        }        

        public ActionResult Create()
        {
            PopulateLocationsDropDownList();
            PopulateSitesDropDownList();
            PopulateAttractionTypeIDDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AttractionName, LocationID,AttractionDate,AttractionPrice,Price,AttractionTypeID,AttractionTypeValue,SiteID,AttractionSiteUrl,AttractionData1,AttractionData2")]Attractions attraction)                                                   
        {
            try
            {
                int maxAttractionId = db.Attractions.Max(x => x.AttractionID);
                attraction.AttractionID = maxAttractionId + 1; 
                if (ModelState.IsValid)
                {
                    db.Attractions.Add(attraction);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            PopulateLocationsDropDownList(attraction.Id);
            PopulateSitesDropDownList(attraction.Id);
            return View(attraction);
        }

        private void PopulateLocationsDropDownList(object selectedLocation = null)
        {
            var locationsQuery = from d in db.Locations
                                 orderby d.LocationName
                                 select d;
            ViewBag.LocationID = new SelectList(locationsQuery, "LocationID", "LocationName", selectedLocation);
        }

        private void PopulateSitesDropDownList(object selectedSite = null)
        {
            var sitesQuery = from d in db.Sites
                             orderby d.SiteName
                             select d;
            ViewBag.SiteID = new SelectList(sitesQuery, "SiteID", "SiteName", selectedSite);
        }

        private void PopulateAttractionTypeIDDropDownList(object selectedType = null)
        {
            var attractionTypeID = from d in db.AttractionTypes
                                   orderby d.AttractionTypeID
                                   select d;

            ViewBag.AttractionTypeID = new SelectList(attractionTypeID, "AttractionTypeID", "AttractionTypeName", selectedType);
        }
    }
}