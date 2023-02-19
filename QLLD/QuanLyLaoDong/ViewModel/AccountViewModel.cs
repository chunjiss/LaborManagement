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
    class AccountViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private ObservableCollection<TAIKHOAN> _List;
        public ObservableCollection<TAIKHOAN> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private TAIKHOAN _SelectedItem;
        public TAIKHOAN SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    NameDisplay = SelectedItem.TenHienThi;
                    Password = SelectedItem.MatKhau;
                }
            }
        }


        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DelCommand { get; set; }

        private string _IdAcc;
        public string IdAcc { get => _IdAcc; set { _IdAcc = value; OnPropertyChanged(); } }

        private string _NameDisplay;
        public string NameDisplay { get => _NameDisplay; set { _NameDisplay = value; OnPropertyChanged(); } }

        private string _NameAcc;
        public string NameAcc { get => _NameAcc; set { _NameAcc = value; OnPropertyChanged(); } }

        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }

        public AccountViewModel()
        {
            List = new ObservableCollection<TAIKHOAN>(DataProvider.Ins.DB.TAIKHOANs);

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(IdAcc) || string.IsNullOrEmpty(NameAcc) || string.IsNullOrEmpty(Password))
                {
                    return false;
                }

                var dp = DataProvider.Ins.DB.TAIKHOANs.Where(x => x.MaTK == IdAcc);

                if (dp == null || dp.Count() != 0)
                {
                    return false;
                }

                return true;
            }, (p) =>
            {
                var acc = new TAIKHOAN()
                {
                    MaTK = IdAcc,
                    TenHienThi = NameDisplay,
                    TenDangNhap = NameAcc,
                    MatKhau = Password
                };
                DataProvider.Ins.DB.TAIKHOANs.Add(acc);
                DataProvider.Ins.DB.SaveChanges();

                List.Add(acc);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(NameDisplay) || string.IsNullOrEmpty(Password) || SelectedItem == null)
                {
                    return false;
                }

                var dp = DataProvider.Ins.DB.TAIKHOANs.Where(x => x.MaTK == IdAcc);

                if (dp == null)
                {
                    return false;
                }

                return true;
            }, (p) =>
            {
                var ac = DataProvider.Ins.DB.TAIKHOANs.Where(x => x.MaTK == SelectedItem.MaTK).SingleOrDefault();
                ac.TenHienThi = NameDisplay;
                ac.MatKhau = Password.Trim();

                DataProvider.Ins.DB.SaveChanges();
                ICollectionView view = CollectionViewSource.GetDefaultView(List);
                view.Refresh();
            });

            DelCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(NameDisplay) || string.IsNullOrEmpty(Password) || SelectedItem == null)
                {
                    return false;
                }

                var dp = DataProvider.Ins.DB.TAIKHOANs.Where(x => x.MaTK == IdAcc);

                if (dp == null)
                {
                    return false;
                }

                return true;
            }, (p) =>
            {
                var ac = DataProvider.Ins.DB.TAIKHOANs.Where(x => x.MaTK == SelectedItem.MaTK).SingleOrDefault();

                using (var context = new QuanLyLaoDongEntities())
                {
                    int noOfRowInserted = context.Database.ExecuteSqlCommand("delete from TAIKHOAN " +
                        "where MaTK='" + SelectedItem.MaTK + "'");
                    context.SaveChanges();
                }

                for (int i = 0; i < List.Count; i++)
                {
                    if (List[i].MaTK == SelectedItem.MaTK)
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
