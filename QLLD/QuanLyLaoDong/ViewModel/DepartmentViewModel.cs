using QuanLyLaoDong.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;

namespace QuanLyLaoDong.ViewModel
{
    public class DepartmentViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private ObservableCollection<PHONGBAN> _List;
        public ObservableCollection<PHONGBAN> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        //private string _filterText;
        //public string FilterText { get => _filterText; set { _filterText = value; OnPropertyChanged(); } }

        private PHONGBAN _SelectedItem;
        public PHONGBAN SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    NameDepartment = SelectedItem.TenPB;
                }
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DelCommand { get; set; }

        private string _IdDepartment;
        public string IdDepartment { get => _IdDepartment; set { _IdDepartment = value; OnPropertyChanged(); } }

        private string _NameDepartment;
        public string NameDepartment { get => _NameDepartment; set { _NameDepartment = value; OnPropertyChanged(); } }

        public DepartmentViewModel()
        {
            List = new ObservableCollection<PHONGBAN>(DataProvider.Ins.DB.PHONGBANs);

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(IdDepartment) || string.IsNullOrEmpty(NameDepartment))
                {
                    return false;
                }

                var dp = DataProvider.Ins.DB.PHONGBANs.Where(x => x.MaPB == IdDepartment);

                if (dp == null || dp.Count() != 0)
                {
                    return false;
                }

                return true;
            }, (p) =>
            {
                var delpartment = new PHONGBAN() { MaPB = IdDepartment, TenPB = NameDepartment };
                DataProvider.Ins.DB.PHONGBANs.Add(delpartment);
                DataProvider.Ins.DB.SaveChanges();

                List.Add(delpartment);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(NameDepartment) || SelectedItem == null)
                {
                    return false;
                }

                var dp = DataProvider.Ins.DB.PHONGBANs.Where(x => x.TenPB == NameDepartment);

                if (dp == null || dp.Count() != 0)
                {
                    return false;
                }

                return true;
            }, (p) =>
            {
                var department = DataProvider.Ins.DB.PHONGBANs.Where(x => x.MaPB == SelectedItem.MaPB).SingleOrDefault();
                department.TenPB = NameDepartment;

                DataProvider.Ins.DB.SaveChanges();
                ICollectionView view = CollectionViewSource.GetDefaultView(List);
                view.Refresh();
            });

            DelCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                {
                    return false;
                }

                var dp = DataProvider.Ins.DB.PHONGBANs.Where(x => x.MaPB == IdDepartment);

                if (dp == null)
                {
                    return false;
                }

                return true;
            }, (p) =>
            {
                var ac = DataProvider.Ins.DB.PHONGBANs.Where(x => x.MaPB == SelectedItem.MaPB).SingleOrDefault();

                using (var context = new QuanLyLaoDongEntities())
                {
                    var str = "delete p from PHANCONG p " +
                    "inner join NHANVIEN e on(e.MaNV = p.MaNV) " +
                    "where e.MaPB = '" + SelectedItem.MaPB + "' \n" +
                    "delete e from NHANVIEN e " +
                    "inner join PHONGBAN pb on(pb.MaPB = e.MaPB) " +
                    "where pb.MaPB = '" + SelectedItem.MaPB + "' \n" +
                    "delete from PHONGBAN " +
                    "where MaPB='" + SelectedItem.MaPB + "'";
                    int noOfRowInserted = context.Database.ExecuteSqlCommand(str);
                    context.SaveChanges();
                }

                for (int i = 0; i < List.Count; i++)
                {
                    if (List[i].MaPB == SelectedItem.MaPB)
                    {
                        List.RemoveAt(i);
                        break;
                    }
                }

                ICollectionView view = CollectionViewSource.GetDefaultView(List);
                view.Refresh();
            });
        }
    }
}