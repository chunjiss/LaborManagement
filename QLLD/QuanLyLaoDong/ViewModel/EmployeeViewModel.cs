using QuanLyLaoDong.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace QuanLyLaoDong.ViewModel
{
    public class EmployeeViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private DisplayNV _SelectedItem;
        public DisplayNV SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();

                if (SelectedItem != null)
                {
                    NameEmployee = SelectedItem.HoTen;
                    IdDepartment = SelectedItem.Dept.MaPB;
                    IdPosition = SelectedItem.Pos.MaCV;
                    DateOfBirth = SelectedItem.NgaySinh;
                    Gender = SelectedItem.GioiTinh;
                    Resident = SelectedItem.DiaChiThuongTru;
                    Staying = SelectedItem.DiaChiTamTru;
                }
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DelCommand { get; set; }

        private string _IdEmployee;
        public string IdEmployee { get => _IdEmployee; set { _IdEmployee = value; OnPropertyChanged(); } }

        private string _IdDepartment;
        public string IdDepartment { get => _IdDepartment; set { _IdDepartment = value; OnPropertyChanged(); } }

        private string _IdPosition;
        public string IdPosition { get => _IdPosition; set { _IdPosition = value; OnPropertyChanged(); } }

        private string _NameEmployee;
        public string NameEmployee { get => _NameEmployee; set { _NameEmployee = value; OnPropertyChanged(); } }

        private DateTime? _DateOfBirth;
        public DateTime? DateOfBirth { get => _DateOfBirth; set { _DateOfBirth = value; OnPropertyChanged(); } }

        private string _Gender;
        public string Gender { get => _Gender; set { _Gender = value; OnPropertyChanged(); } }

        private string _Resident;
        public string Resident { get => _Resident; set { _Resident = value; OnPropertyChanged(); } }

        private string _Staying;
        public string Staying { get => _Staying; set { _Staying = value; OnPropertyChanged(); } }

        private ObservableCollection<DisplayNV> _displayNV;
        public ObservableCollection<DisplayNV> displayNV
        {
            get => _displayNV; set
            {
                _displayNV = value; OnPropertyChanged();
            }
        }

        public EmployeeViewModel()
        {
            displayNV = new ObservableCollection<DisplayNV>();

            foreach (var item in DataProvider.Ins.DB.NHANVIENs)
            {
                displayNV.Add(new DisplayNV()
                {
                    MaNv = item.MaNV,
                    HoTen = item.HoTen,
                    Pos = DataProvider.Ins.DB.CHUCVUs.Where(x => x.MaCV == item.MaCV).FirstOrDefault(),
                    Dept = DataProvider.Ins.DB.PHONGBANs.Where(x => x.MaPB == item.MaPB).FirstOrDefault(),
                    GioiTinh = item.GioiTinh,
                    NgaySinh = (DateTime)item.NgaySinh,
                    DiaChiThuongTru = item.DiaChiThuongTru,
                    DiaChiTamTru = item.DiaChiTamTru
                });
            }

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(IdEmployee) || string.IsNullOrEmpty(NameEmployee))
                {
                    return false;
                }

                var dp = DataProvider.Ins.DB.NHANVIENs.Where(x => x.MaNV == IdEmployee);
                var pb = DataProvider.Ins.DB.PHONGBANs.Where(x => x.MaPB == IdDepartment);
                var pos = DataProvider.Ins.DB.CHUCVUs.Where(x => x.MaCV == IdPosition);

                if (dp == null || dp.Count() != 0 || pb == null || pos == null)
                {
                    return false;
                }

                return true;
            }, (p) =>
            {
                var nv = new NHANVIEN()
                {
                    MaNV = IdEmployee,
                    HoTen = NameEmployee,
                    MaCV = IdPosition,
                    MaPB = IdDepartment,
                    NgaySinh = DateOfBirth,
                    GioiTinh = Gender,
                    DiaChiThuongTru = Resident,
                    DiaChiTamTru = Staying
                };
                DataProvider.Ins.DB.NHANVIENs.Add(nv);
                DataProvider.Ins.DB.SaveChanges();

                displayNV.Add(new DisplayNV()
                {
                    MaNv = nv.MaNV,
                    HoTen = nv.HoTen,
                    Pos = DataProvider.Ins.DB.CHUCVUs.Where(x => x.MaCV == nv.MaCV).FirstOrDefault(),
                    Dept = DataProvider.Ins.DB.PHONGBANs.Where(x => x.MaPB == nv.MaPB).FirstOrDefault(),
                    GioiTinh = nv.GioiTinh,
                    NgaySinh = (DateTime)nv.NgaySinh,
                    DiaChiThuongTru = nv.DiaChiThuongTru,
                    DiaChiTamTru = nv.DiaChiTamTru
                });

                ICollectionView view = CollectionViewSource.GetDefaultView(displayNV);
                view.Refresh();
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(NameEmployee) || SelectedItem == null)
                {
                    return false;
                }

                var dp = DataProvider.Ins.DB.NHANVIENs.Where(x => x.MaNV == IdEmployee);

                if (dp == null || dp.Count() != 0)
                {
                    return false;
                }

                return true;
            }, (p) =>
            {
                var emp = DataProvider.Ins.DB.NHANVIENs.Where(x => x.MaNV == SelectedItem.MaNv).SingleOrDefault();
                emp.HoTen = NameEmployee;
                emp.MaPB = IdDepartment;
                emp.MaCV = IdPosition;
                emp.NgaySinh = DateOfBirth;
                emp.GioiTinh = Gender;
                emp.DiaChiThuongTru = Resident;
                emp.DiaChiTamTru = Staying;

                DataProvider.Ins.DB.SaveChanges();

                for (int i = 0; i < displayNV.Count; i++)
                {
                    if (displayNV[i].MaNv == SelectedItem.MaNv)
                    {
                        displayNV.RemoveAt(i);
                        displayNV.Insert(i, new DisplayNV()
                        {
                            MaNv = emp.MaNV,
                            HoTen = emp.HoTen,
                            Pos = DataProvider.Ins.DB.CHUCVUs.Where(x => x.MaCV == emp.MaCV).FirstOrDefault(),
                            Dept = DataProvider.Ins.DB.PHONGBANs.Where(x => x.MaPB == emp.MaPB).FirstOrDefault(),
                            GioiTinh = emp.GioiTinh,
                            NgaySinh = (DateTime)emp.NgaySinh,
                            DiaChiThuongTru = emp.DiaChiThuongTru,
                            DiaChiTamTru = emp.DiaChiTamTru
                        }) ;

                        break;
                    }
                }

                ICollectionView view = CollectionViewSource.GetDefaultView(displayNV);
                view.Refresh();
            });

            DelCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                {
                    return false;
                }

                var dp = DataProvider.Ins.DB.NHANVIENs.Where(x => x.MaNV == IdEmployee);

                if (dp == null)
                {
                    return false;
                }

                return true;
            }, (p) =>
            {
                var ac = DataProvider.Ins.DB.NHANVIENs.Where(x => x.MaNV == SelectedItem.MaNv).SingleOrDefault();

                using (var context = new QuanLyLaoDongEntities())
                {
                    var str = "delete p from PHANCONG p " +
                    "inner join NHANVIEN nv on(nv.MaNV = p.MaNV) " +
                    "where nv.MaNV = '" + SelectedItem.MaNv + "' \n" +
                    "delete from NHANVIEN " +
                    "where MaNV='" + SelectedItem.MaNv + "'";
                    int noOfRowInserted = context.Database.ExecuteSqlCommand(str);
                    context.SaveChanges();
                }

                for (int i = 0; i < displayNV.Count; i++)
                {
                    if (displayNV[i].MaNv == SelectedItem.MaNv)
                    {
                        displayNV.RemoveAt(i);
                        break;
                    }
                }

                ICollectionView view = CollectionViewSource.GetDefaultView(displayNV);
                view.Refresh();
            });
        }
    }
}
