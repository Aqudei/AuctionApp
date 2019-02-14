using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Auction.Models;
using Caliburn.Micro;

namespace Auction.ViewModels
{
    class ShellViewModel : Conductor<object>.Collection.OneActive
    {
        private readonly IWindowManager _windowManager;

        public ShellViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;
        }

        protected override void OnViewReady(object view)
        {
            Execute.OnUIThreadAsync(() =>
            {
                var loginVm = IoC.Get<LoginViewModel>();
                while (loginVm.Account == null && !loginVm.Quitting)
                {
                    _windowManager.ShowDialog(loginVm);
                }

                if (loginVm.Quitting)
                {
                    Application.Current.Shutdown();
                    return;
                }

                if (loginVm.Account.AccountType == AccountType.Normal)
                {
                    ActivateItem(IoC.Get<AuctionMainViewModel>()); 
                }
                else
                {
                    Items.Add(IoC.Get<ProductsViewModel>());
                    ActivateItem(IoC.Get<RegistrationViewModel>());
                }
            });
        }
    }
}
