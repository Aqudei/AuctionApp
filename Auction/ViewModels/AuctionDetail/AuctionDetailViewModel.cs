using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using Auction.Events;
using Auction.Persistence;
using Caliburn.Micro;

namespace Auction.ViewModels.AuctionDetail
{
    class AuctionDetailViewModel : Screen, IHandleWithTask<ProductSelectionChanged>
    {
        private readonly BindableCollection<BidItemViewModel> _bidItemsViewModels = new BindableCollection<BidItemViewModel>();

        public ICollectionView BidItems { get; set; }

        public AuctionDetailViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.Subscribe(this);
            BidItems = CollectionViewSource.GetDefaultView(_bidItemsViewModels);
        }

        public Task Handle(ProductSelectionChanged message)
        {
            _bidItemsViewModels.Clear();

            return Task.Run(() =>
            {
                if (message.Product == null)
                    return;

                using (var db = new AuctionContext())
                {
                    var bids = db.Bids.Where(bid => bid.ProductId == message.Product.Id).OrderByDescending(bid => bid.BidDate).ToList();

                    foreach (var bid in bids)
                    {
                        _bidItemsViewModels.Add(new BidItemViewModel
                        {
                            BidAmount = bid.BidAmount,
                            BidDate = bid.BidDate,
                            BidderName = db.Accounts.First(account => account.Id == bid.AccountId).UserName,
                        });
                    }
                }
            });
        }
    }
}
