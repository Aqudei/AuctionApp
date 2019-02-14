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
using System.Windows.Shapes;
using Auction.ViewModels;

namespace Auction.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView
    {
        public LoginView()
        {
            InitializeComponent();
            Loaded += (sender, args) =>
            {
                PasswordInput.PasswordChanged += PasswordInput_PasswordChanged;
            };
        }

        private void PasswordInput_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel vm) vm.Password = PasswordInput.Password; ;
        }


    }
}
