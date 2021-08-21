using AuctionCore;
using System.Linq;
using Xunit;

namespace Test.Auction
{
    public class AuctionReceiveOffer
    {
        [Theory]
        [InlineData(4, new double[] { 1000, 2000, 4000, 5000 })]
        [InlineData(2,new double[]{800,1000})]
        public void DoesNotAllowNewBidsAuctionCompleted(int qtdExpected, double[] offers)
        {
            //Arrange
            var auction = new AuctionCore.Auction("PICTURE");
            var Michel = new Interested("Michel", auction);

            foreach (var offer in offers)
                auction.ReceiveBid(Michel, offer);

            auction.End();

            //Act 
            auction.ReceiveBid(Michel, 1000);

            //Assert
            var valueobtained = auction.Bids.Count();

            Assert.Equal(qtdExpected, valueobtained);
        }
    }
}
