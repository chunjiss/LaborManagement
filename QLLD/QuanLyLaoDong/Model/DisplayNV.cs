using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyLaoDong.Model
{
    public class DisplayNV
    {
        public string MaNv { get; set; }
        public string HoTen { get; set; }
        public PHONGBAN Dept { get; set; }
        public CHUCVU Pos { get; set; }
        public string GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public string DiaChiThuongTru { get; set; }
        public string DiaChiTamTru { get; set; }
    }
}
