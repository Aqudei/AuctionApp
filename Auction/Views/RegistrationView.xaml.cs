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
    /// Interaction logic for RegistrationView.xaml
    /// </summary>
    public partial class RegistrationView
    {
        public RegistrationView()
        {
            InitializeComponent();
            Loaded += (sender, args) =>
            {
                Password.PasswordChanged += PasswordOnPasswordChanged;
                PasswordCopy.PasswordChanged += PasswordCopy_PasswordChanged;
            };
        }

        private void PasswordCopy_PasswordChanged(object sender, RoutedEventArgs e)
        {
          
            if (DataContext is RegistrationViewModel vm) vm.PasswordCopy = PasswordCopy.Password;
        }

        private void PasswordOnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegistrationViewModel vm) vm.Password = Password.Password;
        }
    }
}
