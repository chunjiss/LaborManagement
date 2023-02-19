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
    public class PositionViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private ObservableCollection<CHUCVU> _List;
        public ObservableCollection<CHUCVU> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        //private string _filterText;
        //public string FilterText { get => _filterText; set { _filterText = value; OnPropertyChanged(); } }

        private CHUCVU _SelectedItem;
        public CHUCVU SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    NamePosition = SelectedItem.TenCV;
                }
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DelCommand { get; set; }

        private string _IdPosition;
        public string IdPosition { get => _IdPosition; set { _IdPosition = value; OnPropertyChanged(); } }

        private string _NamePosition;
        public string NamePosition { get => _NamePosition; set { _NamePosition = value; OnPropertyChanged(); } }

        public PositionViewModel()
        {
            List = new ObservableCollection<CHUCVU>(DataProvider.Ins.DB.CHUCVUs);

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(IdPosition) || string.IsNullOrEmpty(NamePosition))
                {
                    return false;
                }

                var dp = DataProvider.Ins.DB.CHUCVUs.Where(x => x.MaCV == IdPosition);

                if (dp == null || dp.Count() != 0)
                {
                    return false;
                }

                return true;
            }, (p) =>
            {
                var cv = new CHUCVU() { MaCV = IdPosition, TenCV = NamePosition };
                DataProvider.Ins.DB.CHUCVUs.Add(cv);
                DataProvider.Ins.DB.SaveChanges();

                List.Add(cv);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(NamePosition) || SelectedItem == null)
                {
                    return false;
                }

                var dp = DataProvider.Ins.DB.CHUCVUs.Where(x => x.TenCV == NamePosition);

                if (dp == null || dp.Count() != 0)
                {
                    return false;
                }

                return true;
            }, (p) =>
            {
                var position = DataProvider.Ins.DB.CHUCVUs.Where(x => x.MaCV == SelectedItem.MaCV).SingleOrDefault();
                position.TenCV = NamePosition;

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

                var dp = DataProvider.Ins.DB.CHUCVUs.Where(x => x.MaCV == IdPosition);

                if (dp == null)
                {
                    return false;
                }

                return true;
            }, (p) =>
            {
                var ac = DataProvider.Ins.DB.CHUCVUs.Where(x => x.MaCV == SelectedItem.MaCV).SingleOrDefault();

                using (var context = new QuanLyLaoDongEntities())
                {
                    var str = "delete p from PHANCONG p " +
                    "inner join NHANVIEN e on(e.MaNV = p.MaNV) " +
                    "where e.MaCV = '" + SelectedItem.MaCV + "' \n" +
                    "delete e from NHANVIEN e " +
                    "inner join CHUCVU cv on(cv.MaCV = e.MaCV) " +
                    "where cv.MaCV = '" + SelectedItem.MaCV + "' \n" +
                    "delete from CHUCVU " +
                    "where MaCV='" + SelectedItem.MaCV + "'";
                    int noOfRowInserted = context.Database.ExecuteSqlCommand(str);
                    context.SaveChanges();
                }

                for (int i = 0; i < List.Count; i++)
                {
                    if (List[i].MaCV == SelectedItem.MaCV)
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
