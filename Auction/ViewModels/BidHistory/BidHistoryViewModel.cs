using System.Linq;
using System.Threading.Tasks;
using Auction.Events;
using Auction.Persistence;
using AutoMapper.QueryableExtensions;
using Caliburn.Micro;

namespace Auction.ViewModels.BidHistory
{
    class BidHistoryViewModel : Screen, IHandleWithTask<ProductSelectionChanged>
    {
        private readonly BindableCollection<BidHistoryItemViewModel> _bidHistoryItemVms = new BindableCollection<BidHistoryItemViewModel>();

        public BidHistoryViewModel(IEventAggregator eventAggregator)
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
                        _bidHistoryItemVms.Add(new BidHistoryItemViewModel
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
