﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class genifacedbEntities : DbContext
    {
        public genifacedbEntities()
            : base("name=genifacedbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Locations> Locations { get; set; }
        public virtual DbSet<Sites> Sites { get; set; }
        public virtual DbSet<AttractionTypes> AttractionTypes { get; set; }
        public virtual DbSet<Attractions> Attractions { get; set; }
    }
}
