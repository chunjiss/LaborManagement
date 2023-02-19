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

namespace QuanLyLaoDong
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new System.Uri("Pages/Home.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnManage_Click(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new System.Uri("Pages/Manage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnEmployee_Click(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new System.Uri("/Pages/Employee.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnPosition_Click(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new System.Uri("Pages/Position.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnConstruction_Click(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new System.Uri("Pages/Construction.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnAssignment_Click(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new System.Uri("Pages/Assignment.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new System.Uri("Pages/Account.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new System.Uri("Pages/Home.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
