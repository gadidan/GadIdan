//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GadIdan
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sites
    {
        public Sites()
        {
            this.Attractions = new HashSet<Attractions>();
        }
    
        public int SiteID { get; set; }
        public string SiteName { get; set; }
        public string SiteUrl { get; set; }
        public string SiteData { get; set; }
    
        public virtual ICollection<Attractions> Attractions { get; set; }
    }
}
