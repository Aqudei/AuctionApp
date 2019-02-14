using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Auction.Models
{
    enum AccountType
    {
        Admin,
        Normal
    }

    class Account : EntityBase
    {
        public AccountType AccountType { get; set; }

        private string _password;
        public string UserName { get; set; }

        public string Password => _password;

        public void SetPassword(string password)
        {
            using (var hasher = new SHA1CryptoServiceProvider())
            {
                var bHash = hasher.ComputeHash(Encoding.ASCII.GetBytes(password));
                _password = Encoding.ASCII.GetString(bHash);
            }
        }


        public bool VerifyPassword(string password)
        {
            using (var hasher = new SHA1CryptoServiceProvider())
            {
                var bHash = hasher.ComputeHash(Encoding.ASCII.GetBytes(password));
                return _password == Encoding.ASCII.GetString(bHash);
            }
        }
    }
}
