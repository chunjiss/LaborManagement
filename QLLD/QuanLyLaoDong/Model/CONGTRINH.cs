//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyLaoDong.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class CONGTRINH
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CONGTRINH()
        {
            this.PHANCONGs = new HashSet<PHANCONG>();
        }
    
        public string MaCT { get; set; }
        public string TenCT { get; set; }
        public string DiaDiem { get; set; }
        public Nullable<System.DateTime> NgayCP { get; set; }
        public Nullable<System.DateTime> NgayKC { get; set; }
        public Nullable<System.DateTime> NgayHTDK { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHANCONG> PHANCONGs { get; set; }
    }
}
