using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auction.Models;
using Caliburn.Micro;

namespace Auction.ViewModels
{
    class ShellViewModel : Conductor<object>
    {
        private readonly IWindowManager _windowManager;

        public ShellViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;
        }
        protected override void OnViewReady(object view)
        {
            var loginVm = IoC.Get<LoginViewModel>();
            while (loginVm.Account == null)
            {
                _windowManager.ShowDialog(loginVm);
            }

            if (loginVm.Account.AccountType == AccountType.Normal)
            {

            }
            else
            {
                ActivateItem(IoC.Get<RegistrationViewModel>());
                
            }
        }
    }
}
