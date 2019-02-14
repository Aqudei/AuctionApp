using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auction.Models;
using Auction.Persistence;
using AutoMapper;
using Caliburn.Micro;

namespace Auction.ViewModels
{
    class RegistrationViewModel : CrudViewModel<Account>
    {
        private string _userName;
        private string _password;
        private string _passwordCopy;

        public string UserName
        {
            get => _userName;
            set => Set(ref _userName, value);
        }

        public string Password
        {
            get => _password;
            set => Set(ref _password, value);
        }

        public string PasswordCopy
        {
            get => _passwordCopy;
            set => Set(ref _passwordCopy, value);
        }

        public RegistrationViewModel(IMapper mapper, IEventAggregator eventAggregator) : base(mapper, eventAggregator)
        {
        }

        protected override void ClearFields()
        {
            PasswordCopy = UserName = Password = "";
        }
    }
}
