using System;
using Caliburn.Micro;

namespace Auction.ViewModels.AuctionDetail
{
    class BidItemViewModel : Screen
    {
        public string BidderName { get; set; }
        public double BidAmount { get; set; }
        public DateTime BidDate { get; set; }

    }
}
