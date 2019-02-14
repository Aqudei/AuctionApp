using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Auction.Models;
using Auction.Persistence;
using Caliburn.Micro;

namespace Auction.ViewModels
{
    class LoginViewModel : Screen
    {
        private string _message;
        public string UserName { get; set; }
        public string Password { get; set; }
        public Account Account { get; private set; }

        public string Message
        {
            get => _message;
            set => Set(ref _message, value);
        }

        public void Exit()
        {
            Application.Current.Shutdown();
        }

        public void Login()
        {
            Account = null;
            using (var db = new AuctionContext())
            {
                var user = db.Accounts.Single(u => u.UserName == UserName);
                if (user.VerifyPassword(Password))
                {
                    Account = user;
                    TryClose();
                }
            }
        }
    }
}
