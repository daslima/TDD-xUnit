namespace AuctionCore
{
    public class Bid
    {
        public Interested Client { get; set; }
        public double Value { get; set; }

        public Bid(Interested client, double value)
        {
            if (value < 0)
                throw new System.ArgumentException("Value must be greater than zero");
            else
            {
                Client = client;
                Value = value;
            }
           
        }
    }
}
