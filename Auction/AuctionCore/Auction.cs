using System.Collections.Generic;
using System.Linq;

namespace AuctionCore
{
    public class Auction
    {
        private IList<Bid> _Bids;
        public IEnumerable<Bid> Bids => _Bids;
        public string Item { get; }
        public Bid Winner { get; private set; }


        public Auction(string item)
        {
            Item = item;
            _Bids = new List<Bid>();
        }

        public void ReceiveBid(Interested client, double value) => _Bids.Add(new Bid(client, value));

        public void Start()
        {

        }

        public void End()
        {
            Winner = Bids.OrderBy(b => b.Value)
                            .Last();
        }
       
    }
}
