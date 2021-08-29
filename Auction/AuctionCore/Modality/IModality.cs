namespace AuctionCore.Modality
{
    public interface IModality
    {
        Bid Evaluate(Auction auction);
    }
}
