using MaterialDesignThemes.Wpf;
using QuanLyLaoDong.Model;
using QuanLyLaoDong.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyLaoDong.Pages
{
    /// <summary>
    /// Interaction logic for Manage.xaml
    /// </summary>
    public partial class Manage : Page
    {
        public bool IsSort;

        public Manage()
        {
            IsSort = false;
            InitializeComponent();

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvDepartment.ItemsSource);
            view.Filter = DepartmentFilter;
        }

        private bool DepartmentFilter(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
            {
                return true;
            }
            else
            {
                return ((item as PHONGBAN).TenPB.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lvDepartment.ItemsSource).Refresh();
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
    }
}
