using System.Linq;

namespace AuctionCore.Modality
{
    public class ClosestTopOffer : IModality
    {
        public double DestinationValue { get; set; }

        public ClosestTopOffer(double destinationValue) => DestinationValue = destinationValue;
      
        public Bid Evaluate(Auction auction)
        {
            return auction.Bids.DefaultIfEmpty(new Bid(null, 0))
                             .Where(b => b.Value > DestinationValue)
                             .OrderBy(b => b.Value)
                             .FirstOrDefault();
        }
    }
}
