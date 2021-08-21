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
        public StatusEnum Status { get; private set; }

        public Auction(string item)
        {
            Item = item;
            _Bids = new List<Bid>();
            Status = StatusEnum.Inprogress;
        }

        public void ReceiveBid(Interested client, double value)
        {
            if(Status.Equals(StatusEnum.Inprogress))
                _Bids.Add(new Bid(client, value));
        }

        public void Start()
        {

        }

        public void End()
        {
            Status = StatusEnum.Finalized;

            Winner = Bids.DefaultIfEmpty(new Bid(null,0))
                         .OrderBy(b => b.Value)
                         .LastOrDefault();
        }
       
    }
}
