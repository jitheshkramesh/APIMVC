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
    
    public partial class TTMAST
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TTMAST()
        {
            this.DT_HRPAYADJ = new HashSet<DT_HRPAYADJ>();
        }
    
        public int TT_ID { get; set; }
        public string TT_CODE { get; set; }
        public string TT_DESC { get; set; }
        public string TT_GROUP { get; set; }
        public Nullable<bool> TT_LVERESUME { get; set; }
        public Nullable<bool> TT_LVEACCR { get; set; }
        public Nullable<bool> TT_GRATACCR { get; set; }
        public Nullable<bool> TT_SUSLVE { get; set; }
        public Nullable<bool> TT_SUSGRAT { get; set; }
        public Nullable<bool> TT_SUSAIR { get; set; }
        public string TT_TYPE { get; set; }
        public string TT_ED { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DT_HRPAYADJ> DT_HRPAYADJ { get; set; }
    }
}
