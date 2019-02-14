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
    sealed class RegistrationViewModel : CrudViewModel<Account>
    {
        private string _userName;
        private string _password;
        private string _passwordCopy;
        private AccountType _accountType;

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

        public BindableCollection<AccountType> AccountTypes { get; set; } = new BindableCollection<AccountType>();
        public AccountType AccountType { get => _accountType; set => Set(ref _accountType, value); }

        public string PasswordCopy
        {
            get => _passwordCopy;
            set => Set(ref _passwordCopy, value);
        }

        public RegistrationViewModel(IMapper mapper, IEventAggregator eventAggregator) : base(mapper, eventAggregator)
        {
            DisplayName = "Registration";

            AccountTypes.AddRange(Enum.GetValues(typeof(AccountType)).OfType<AccountType>());
        }

        protected override void PreSaveAction(Account item)
        {
            item.SetPassword(Password);
        }

        protected override bool PreSaveCheck()
        {
            return Password == PasswordCopy;
        }

        protected override void ClearFields()
        {
            PasswordCopy = UserName = Password = "";
        }
    }
}
