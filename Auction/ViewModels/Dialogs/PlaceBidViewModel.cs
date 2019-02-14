using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace Auction.ViewModels.Dialogs
{
    sealed class PlaceBidViewModel : Screen
    {
        private string _title;
        private double _bidAmount;
        public bool IsCancelled { get; set; } = false;

        public double BidAmount
        {
            get => _bidAmount;
            set => Set(ref _bidAmount, value);
        }

        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        public PlaceBidViewModel()
        {
            DisplayName = "Place You Bid";
        }

        public void Close()
        {
            IsCancelled = true;
            TryClose();
        }


        public void PlaceBid()
        {
            IsCancelled = false;
            TryClose();
        }
    }
}
