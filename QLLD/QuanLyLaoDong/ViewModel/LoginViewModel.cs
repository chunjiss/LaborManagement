using QuanLyLaoDong.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyLaoDong.ViewModel
{
    class LoginViewModel : BaseViewModel
    {
        public bool IsLogin { get; set; }
        public bool Isloaded = false;
        private string _Username;
        public string Username { get => _Username; set { _Username = value; OnPropertyChanged(); } }
        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }

        public ICommand LoginCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }

        public LoginViewModel()
        {
            IsLogin = false;

            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { Login(p); });

            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { p.Close(); });

            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });
        }

        void Login(Window p)
        {
            if (p == null)
            {
                return;
            }

            var acc = DataProvider.Ins.DB.TAIKHOANs.Where(x => x.TenDangNhap == Username && x.MatKhau == Password).Count();

            if (acc > 0)
            {
                IsLogin = true;
                p.Close();
            }
            else
            {
                IsLogin = false;
                MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
            }
        }
    }
}
