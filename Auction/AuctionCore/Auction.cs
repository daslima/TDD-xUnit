using System.Collections.Generic;
using System.Linq;

namespace AuctionCore
{
    public class Auction
    {
        private Interested _LastCustomer;
        private IList<Bid> _Bids;
        public IEnumerable<Bid> Bids => _Bids;
        public string Item { get; }
        public Bid Winner { get; private set; }
        public StatusEnum Status { get; private set; }

        public Auction(string item)
        {
            Item = item;
            _Bids = new List<Bid>();
            Status = StatusEnum.Create;
        }
        
        public void ReceiveBid(Interested client, double value)
        {
            if (BidIsValid(client))
            {
                _Bids.Add(new Bid(client, value));
                _LastCustomer = client;
            }
        }

        public void Start() => Status = StatusEnum.Inprogress;
      
        public void End()
        {
            Status = StatusEnum.Finalized;

            Winner = Bids.DefaultIfEmpty(new Bid(null,0))
                         .OrderBy(b => b.Value)
                         .LastOrDefault();
        }

        private bool BidIsValid(Interested client) => Status == StatusEnum.Inprogress && client != _LastCustomer;


    }
}
