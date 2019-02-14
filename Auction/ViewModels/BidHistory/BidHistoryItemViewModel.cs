using System;
using Auction.Models;
using Caliburn.Micro;

namespace Auction.ViewModels.BidHistory
{
    class BidHistoryItemViewModel : Screen
    {
        public string BidderName { get; set; }
        public double BidAmount { get; set; }
        public DateTime BidDate { get; set; }
    }
}
