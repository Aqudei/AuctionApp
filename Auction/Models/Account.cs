using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        private string _password;

        public AccountType AccountType { get; set; }

        [StringLength(32)]
        public string UserName { get; set; }

        public string Password { get => _password; set => _password = value; }

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
