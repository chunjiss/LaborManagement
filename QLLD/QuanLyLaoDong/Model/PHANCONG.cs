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
    
    public partial class PHANCONG
    {
        public string MaNV { get; set; }
        public string MaCT { get; set; }
        public int ThoiGian { get; set; }
    
        public virtual CONGTRINH CONGTRINH { get; set; }
        public virtual NHANVIEN NHANVIEN { get; set; }
    }
}
