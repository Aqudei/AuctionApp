using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Auction.Models;
using Auction.ViewModels.AuctionDetail;
using Caliburn.Micro;

namespace Auction.ViewModels
{
    sealed class AuctionMainViewModel : Screen
    {
        private readonly BindableCollection<Product> _items = new BindableCollection<Product>();
        private Product _selectedItem;
        public ICollectionView Items { get; set; }

        public AuctionDetailViewModel AuctionDetail { get; set; } = IoC.Get<AuctionDetailViewModel>();

        public Product SelectedItem
        {
            get => _selectedItem;
            set => Set(ref _selectedItem, value);
        }

        public AuctionMainViewModel()
        {
            Items = CollectionViewSource.GetDefaultView(_items);
            DisplayName = "Auction Main";
        }
    }
}
