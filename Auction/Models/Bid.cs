using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.Models
{
    class Bid : EntityBase
    {
        public Guid ProductId { get; set; }
        public double BidAmount { get; set; }
        public Guid AccountId { get; set; }
        public DateTime BidDate { get; set; } = DateTime.Now;
    }
}
