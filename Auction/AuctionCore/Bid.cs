namespace AuctionCore
{
    public class Bid
    {
        public Interested Client { get; set; }
        public double Value { get; set; }

        public Bid(Interested client, double value)
        {
            Client = client;
            Value = value;
        }
    }
}
