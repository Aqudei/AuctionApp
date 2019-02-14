using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auction.Models;

namespace Auction.Persistence
{
    class AuctionContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Bid> Bids { get; set; }
    }
}
