//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class DT_HRPAYADJ
    {
        public int PAD_ID { get; set; }
        public int PA_ID { get; set; }
        public int EM_ID { get; set; }
        public int TT_ID { get; set; }
        public Nullable<decimal> PAD_QTY { get; set; }
        public Nullable<decimal> PAD_RATE { get; set; }
        public Nullable<decimal> PAD_AMT { get; set; }
        public string PAD_REMARKS { get; set; }
    
        public virtual HD_HRPAYADJ HD_HRPAYADJ { get; set; }
        public virtual TTMAST TTMAST { get; set; }
    }
}
