using System.Linq;
using System.Threading.Tasks;
using Auction.Events;
using Auction.Persistence;
using Caliburn.Micro;

namespace Auction.ViewModels.AuctionDetail
{
    class AuctionDetailViewModel : Screen, IHandleWithTask<ProductSelectionChanged>
    {
        private readonly BindableCollection<BidItemViewModel> _bidHistoryItemVms = new BindableCollection<BidItemViewModel>();

        public AuctionDetailViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.Subscribe(this);
        }

        public Task Handle(ProductSelectionChanged message)
        {
            _bidHistoryItemVms.Clear();

            return Task.Run(() =>
            {
                if (message.Product == null)
                    return;

                using (var db = new AuctionContext())
                {
                    var bids = db.Bids.Where(bid => bid.ProductId == message.Product.Id);
                    foreach (var bid in bids)
                    {
                        _bidHistoryItemVms.Add(new BidItemViewModel
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
