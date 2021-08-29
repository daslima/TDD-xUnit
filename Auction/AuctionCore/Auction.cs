using AuctionCore.Modality;
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
        public IModality Modality { get; set; }

        public Auction(string item, IModality modality)
        {
            Item = item;
            _Bids = new List<Bid>();
            Status = StatusEnum.Create;
            Modality = modality;
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
            if (!Status.Equals(StatusEnum.Inprogress))
                throw new System.InvalidOperationException("It is necessary to start the auction before finishing");

            Winner = Modality.Evaluate(this); 

            Status = StatusEnum.Finalized;
        }

        private bool BidIsValid(Interested client) => Status == StatusEnum.Inprogress && client != _LastCustomer;


    }
}
