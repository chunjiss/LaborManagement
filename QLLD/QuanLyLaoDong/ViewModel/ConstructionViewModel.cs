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
    public class ConstructionViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private ObservableCollection<CONGTRINH> _List;
        public ObservableCollection<CONGTRINH> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        //private string _filterText;
        //public string FilterText { get => _filterText; set { _filterText = value; OnPropertyChanged(); } }

        private CONGTRINH _SelectedItem;
        public CONGTRINH SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    NameConstruction = SelectedItem.TenCT;
                    Locations = SelectedItem.DiaDiem;
                    LicenseDate = SelectedItem.NgayCP;
                    StartDay = SelectedItem.NgayKC;
                    FinishDay = SelectedItem.NgayHTDK;
                }
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DelCommand { get; set; }

        private string _IdConstruction;
        public string IdConstruction { get => _IdConstruction; set { _IdConstruction = value; OnPropertyChanged(); } }

        private string _NameConstruction;
        public string NameConstruction { get => _NameConstruction; set { _NameConstruction = value; OnPropertyChanged(); } }

        private string _Locations;
        public string Locations { get => _Locations; set { _Locations = value; OnPropertyChanged(); } }

        private DateTime? _LicenseDate;
        public DateTime? LicenseDate { get => _LicenseDate; set { _LicenseDate = value; OnPropertyChanged(); } }

        private DateTime? _StartDay;
        public DateTime? StartDay { get => _StartDay; set { _StartDay = value; OnPropertyChanged(); } }

        private DateTime? _FinishDay;
        public DateTime? FinishDay { get => _FinishDay; set { _FinishDay = value; OnPropertyChanged(); } }

        public ConstructionViewModel()
        {
            List = new ObservableCollection<CONGTRINH>(DataProvider.Ins.DB.CONGTRINHs);

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(IdConstruction) || string.IsNullOrEmpty(NameConstruction))
                {
                    return false;
                }

                var dp = DataProvider.Ins.DB.CONGTRINHs.Where(x => x.MaCT == IdConstruction);

                if (dp == null || dp.Count() != 0)
                {
                    return false;
                }

                return true;
            }, (p) =>
            {
                var cv = new CONGTRINH() { MaCT = IdConstruction, TenCT = NameConstruction,
                DiaDiem = Locations, NgayCP = LicenseDate, NgayKC = StartDay, NgayHTDK = FinishDay};
                DataProvider.Ins.DB.CONGTRINHs.Add(cv);
                DataProvider.Ins.DB.SaveChanges();

                List.Add(cv);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(NameConstruction) || SelectedItem == null)
                {
                    return false;
                }

                var dp = DataProvider.Ins.DB.CONGTRINHs.Where(x => x.MaCT == IdConstruction);

                if (dp == null )
                {
                    return false;
                }

                return true;
            }, (p) =>
            {
                var ct = DataProvider.Ins.DB.CONGTRINHs.Where(x => x.MaCT == SelectedItem.MaCT).SingleOrDefault();
                ct.TenCT = NameConstruction;
                ct.DiaDiem = Locations;
                ct.NgayCP = LicenseDate;
                ct.NgayKC = StartDay;
                ct.NgayHTDK = FinishDay;

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

                var dp = DataProvider.Ins.DB.CONGTRINHs.Where(x => x.MaCT == IdConstruction);

                if (dp == null)
                {
                    return false;
                }

                return true;
            }, (p) =>
            {
                var ac = DataProvider.Ins.DB.CONGTRINHs.Where(x => x.MaCT == SelectedItem.MaCT).SingleOrDefault();

                using (var context = new QuanLyLaoDongEntities())
                {
                    var str = "delete p from PHANCONG p " +
                    "inner join CONGTRINH c on(c.MaCT = p.MaCT) " +
                    "where c.MaCT = '" + SelectedItem.MaCT + "' \n" +
                    "delete from CONGTRINH " +
                    "where MaCT='" + SelectedItem.MaCT + "'";
                    int noOfRowInserted = context.Database.ExecuteSqlCommand(str);
                    context.SaveChanges();
                }

                for (int i = 0; i < List.Count; i++)
                {
                    if (List[i].MaCT == SelectedItem.MaCT)
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
