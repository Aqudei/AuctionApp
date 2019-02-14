using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auction.Models;

namespace Auction.Events
{
    class ProductSelectionChanged
    {
        public Product Product { get; set; }
    }
}
