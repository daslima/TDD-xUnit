using System.Linq;

namespace AuctionCore.Modality
{
    public class HighestValue : IModality
    {
        public Bid Evaluate(Auction auction)
        {
            return auction.Bids.DefaultIfEmpty(new Bid(null, 0))
                         .OrderBy(b => b.Value)
                         .LastOrDefault();
        }
    }
}
