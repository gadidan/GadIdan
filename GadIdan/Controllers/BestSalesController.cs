using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace GadIdan.Controllers
{
    public class BestSalesController : Controller
    {
        private genifacedbEntities db = new genifacedbEntities();
        // GET: BestSales
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

            //IQueryable<Attractions> list = db.Attractions
            //    .Where(c => (string.IsNullOrEmpty(attractionName) || c.AttractionName.Contains(attractionName)) &&
            //                (!SelectedAttraction.HasValue || c.AttractionID == SelectedAttraction) &&
            //                (!LocationID.HasValue || c.LocationID == LocationID))

            //    .OrderBy(d => d.AttractionPrice)
            //    .Include(d => d.Locations)
            //    .Include(d => d.Sites);


            IQueryable<Attractions> list = db.Attractions.GroupBy(p => p.AttractionID)
                  .Select(g => g.OrderBy(p => p.Price)
                                .FirstOrDefault()
                   );

            PagedList<Attractions> model = new PagedList<Attractions>(list.ToList(), page, pageSize);
            return View(model);
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