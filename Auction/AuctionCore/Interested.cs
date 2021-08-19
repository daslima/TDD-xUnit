namespace AuctionCore
{
    public class Interested
    {
        public string Name { get; set; }
        public Auction Auction { get; set; }

        public Interested(string name, Auction auction)
        {
            Name = name;
            Auction = auction;
        }
    }
}
