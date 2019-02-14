using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Threading;
using Auction.Events;
using Auction.Models;
using Auction.Persistence;
using Auction.ViewModels.AuctionDetail;
using Auction.ViewModels.Dialogs;
using Auction.Views.Dialogs;
using Caliburn.Micro;

namespace Auction.ViewModels
{
    sealed class AuctionMainViewModel : Screen
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowManager _windowManager;
        private readonly LoginViewModel _loginViewModel;
        private readonly BindableCollection<Product> _items = new BindableCollection<Product>();
        private Product _selectedItem;
        public ICollectionView Items { get; set; }

        public AuctionDetailViewModel AuctionDetail { get; set; } = IoC.Get<AuctionDetailViewModel>();

        private readonly BackgroundWorker _backgroundWorker = new BackgroundWorker();


        public void SubmitBid(Product product)
        {
            var vm = new PlaceBidViewModel
            {
                BidAmount = product.LastBidAmount,
                Title = product.Title
            };
            _windowManager.ShowDialog(vm);

            if (vm.IsCancelled)
                return;

            using (var db = new AuctionContext())
            {
                product.LastBidAmount = vm.BidAmount;
                db.Entry(product).State = EntityState.Modified;
                db.Bids.Add(new Bid
                {
                    AccountId = _loginViewModel.Account.Id,
                    BidAmount = vm.BidAmount,
                    ProductId = product.Id
                });
                db.SaveChanges();
            }
        }

        public void CloseBidding(Product product)
        {

        }

        public Product SelectedItem
        {
            get => _selectedItem;
            set => Set(ref _selectedItem, value);
        }

        public AuctionMainViewModel(IEventAggregator eventAggregator,
            IWindowManager windowManager, LoginViewModel loginViewModel)
        {
            _eventAggregator = eventAggregator;
            _windowManager = windowManager;
            _loginViewModel = loginViewModel;
            Items = CollectionViewSource.GetDefaultView(_items);
            DisplayName = "Auction Main";

            _backgroundWorker.DoWork += _backgroundWorker_DoWork;
            _backgroundWorker.RunWorkerAsync();

            PropertyChanged += AuctionMainViewModel_PropertyChanged;
        }

        private async void AuctionMainViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (nameof(SelectedItem) == e.PropertyName)
            {
                await _eventAggregator.PublishOnUIThreadAsync(new ProductSelectionChanged
                {
                    Product = SelectedItem
                });
            }
        }

        private void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                Thread.Sleep(1024);
                var selectedId = SelectedItem?.Id;

                _items.Clear();
                using (var db = new AuctionContext())
                {
                    var products = db.Products.ToList();
                    _items.AddRange(products);
                    var selected = _items.FirstOrDefault(i => i.Id == selectedId);
                    if (selected != null)
                    {
                        SelectedItem = selected;
                    }
                }
            }
        }
    }
}
