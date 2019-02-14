using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.Models
{
    class Product : EntityBase
    {
        public string Title { get; set; }
        public double InitialPrice { get; set; }
        public double LastBidAmount { get; set; }
    }
}
