﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EFMDataAccessModel
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class OVODEntities : DbContext
    {
        public OVODEntities()
            : base("name=OVODEntities")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<TTMAST> TTMASTs { get; set; }
        public virtual DbSet<HD_HRPAYADJ> HD_HRPAYADJ { get; set; }
        public virtual DbSet<ANG_EMPLOYEE> ANG_EMPLOYEE { get; set; }
        public virtual DbSet<DT_HRPAYADJ> DT_HRPAYADJ { get; set; }
        public virtual DbSet<PaymentDetail> PaymentDetails { get; set; }
        public virtual DbSet<Participant> Participants { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
    }
}
