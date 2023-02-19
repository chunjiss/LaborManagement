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
    class AssignmentViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private string _IdEmployee;
        public string IdEmployee { get => _IdEmployee; set { _IdEmployee = value; OnPropertyChanged(); } }

        private string _IdConstruction;
        public string IdConstruction { get => _IdConstruction; set { _IdConstruction = value; OnPropertyChanged(); } }

        private int _Tgian;
        public int Tgian { get => _Tgian; set { _Tgian = value; OnPropertyChanged(); } }

        private ObservableCollection<DisplayPC> _displayPC;
        public ObservableCollection<DisplayPC> displayPC
        {
            get => _displayPC; set
            {
                _displayPC = value; OnPropertyChanged();
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DelCommand { get; set; }

        private DisplayPC _SelectedItem;
        public DisplayPC SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();

                if (SelectedItem != null)
                {
                    IdEmployee = SelectedItem.Nv.MaNV;
                    IdConstruction = SelectedItem.Ct.MaCT;
                    Tgian = SelectedItem.ThoiGian;
                }
            }
        }

        public AssignmentViewModel()
        {
            displayPC = new ObservableCollection<DisplayPC>();

            foreach (var item in DataProvider.Ins.DB.PHANCONGs)
            {
                displayPC.Add(new DisplayPC()
                {
                    Nv = DataProvider.Ins.DB.NHANVIENs.Where(x => x.MaNV == item.MaNV).FirstOrDefault(),
                    Ct = DataProvider.Ins.DB.CONGTRINHs.Where(x => x.MaCT == item.MaCT).FirstOrDefault(),
                    ThoiGian = item.ThoiGian
                });
            }

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(IdEmployee) || string.IsNullOrEmpty(IdConstruction))
                {
                    return false;
                }

                var dp = DataProvider.Ins.DB.NHANVIENs.Where(x => x.MaNV == IdEmployee);
                var ct = DataProvider.Ins.DB.CONGTRINHs.Where(x => x.MaCT == IdConstruction);

                if (dp == null || ct == null)
                {
                    return false;
                }

                return true;
            }, (p) =>
            {
                var nv = new PHANCONG()
                {
                    MaNV = IdEmployee,
                    MaCT = IdConstruction,
                    ThoiGian = Tgian
                };

                using (var ctx = new QuanLyLaoDongEntities())
                {
                    int noOfRowInserted = ctx.Database.ExecuteSqlCommand("insert into PHANCONG values('" + nv.MaNV + "', '" + nv.MaCT + "', '" + nv.ThoiGian + "')");
                }

                displayPC.Add(new DisplayPC()
                {
                    Nv = DataProvider.Ins.DB.NHANVIENs.Where(x => x.MaNV == nv.MaNV).FirstOrDefault(),
                    Ct = DataProvider.Ins.DB.CONGTRINHs.Where(x => x.MaCT == nv.MaCT).FirstOrDefault(),
                    ThoiGian = nv.ThoiGian
                });

                ICollectionView view = CollectionViewSource.GetDefaultView(displayPC);
                view.Refresh();
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(IdEmployee) || string.IsNullOrEmpty(IdConstruction) || SelectedItem == null)
                {
                    return false;
                }

                var dp = DataProvider.Ins.DB.NHANVIENs.Where(x => x.MaNV == IdEmployee);
                var ct = DataProvider.Ins.DB.CONGTRINHs.Where(x => x.MaCT == IdConstruction);

                if (dp == null || ct == null)
                {
                    return false;
                }

                return true;
            }, (p) =>
            {
                var emp = DataProvider.Ins.DB.PHANCONGs.Where(x => x.MaNV == SelectedItem.Nv.MaNV && x.MaCT == SelectedItem.Ct.MaCT).SingleOrDefault();
                emp.MaNV = IdEmployee;
                emp.MaCT = IdConstruction;
                emp.ThoiGian = Tgian;

                using (var context = new QuanLyLaoDongEntities())
                {
                    int noOfRowInserted = context.Database.ExecuteSqlCommand("update PHANCONG set MaNV = '" + emp.MaNV + "', MaCT = '" +
                         emp.MaCT + "', ThoiGian = '" + emp.ThoiGian + "'" +
                         "where MaNV = '" + SelectedItem.Nv.MaNV + "' and MaCT = '" + SelectedItem.Ct.MaCT + "'");
                    
                }

                for (int i = 0; i < displayPC.Count; i++)
                {
                    if (displayPC[i].Nv.MaNV == SelectedItem.Nv.MaNV && displayPC[i].Ct.MaCT == SelectedItem.Ct.MaCT)
                    {
                        displayPC.RemoveAt(i);
                        displayPC.Insert(i, new DisplayPC()
                        {
                            Nv = DataProvider.Ins.DB.NHANVIENs.Where(x => x.MaNV == IdEmployee).FirstOrDefault(),
                            Ct = DataProvider.Ins.DB.CONGTRINHs.Where(x => x.MaCT == IdConstruction).FirstOrDefault(),
                            ThoiGian = emp.ThoiGian
                        });

                        break;
                    }
                }

                ICollectionView view = CollectionViewSource.GetDefaultView(displayPC);
                view.Refresh();
            });

            DelCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                {
                    return false;
                }

                var dp = DataProvider.Ins.DB.PHANCONGs.Where(x => x.MaNV == IdEmployee);

                if (dp == null)
                {
                    return false;
                }

                return true;
            }, (p) =>
            {
                var ac = DataProvider.Ins.DB.PHANCONGs.Where(x => x.MaNV == SelectedItem.Nv.MaNV).SingleOrDefault();

                using (var context = new QuanLyLaoDongEntities())
                {
                    int noOfRowInserted = context.Database.ExecuteSqlCommand("delete from PHANCONG " +
                        "where MaNV='" + SelectedItem.Nv.MaNV + "' and MaCT='"+SelectedItem.Ct.MaCT+"'");
                    context.SaveChanges();
                }

                for (int i = 0; i < displayPC.Count; i++)
                {
                    if (displayPC[i].Nv.MaNV == SelectedItem.Nv.MaNV && displayPC[i].Ct.MaCT == SelectedItem.Ct.MaCT)
                    {
                        displayPC.RemoveAt(i);
                        break;
                    }
                }

                ICollectionView view = CollectionViewSource.GetDefaultView(displayPC);
                view.Refresh();
            });
        }
    }
}
