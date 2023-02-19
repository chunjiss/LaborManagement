using MaterialDesignThemes.Wpf;
using QuanLyLaoDong.Model;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for Assignment.xaml
    /// </summary>
    public partial class Assignment : Page
    {
        public Assignment()
        {
            InitializeComponent();
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvAssignment.ItemsSource);
            view.Filter = AssignmentFilter;
        }

        private bool AssignmentFilter(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
            {
                return true;
            }
            else
            {
                return ((item as DisplayPC).Nv.HoTen.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lvAssignment.ItemsSource).Refresh();
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
    }
}