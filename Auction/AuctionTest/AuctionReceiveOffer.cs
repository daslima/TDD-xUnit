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

            auction.Start();

            foreach (var offer in offers)
                auction.ReceiveBid(Michel, offer);

            auction.End();

            //Act 
            auction.ReceiveBid(Michel, 1000);

            //Assert
            var valueobtained = auction.Bids.Count();

            Assert.Equal(qtdExpected, valueobtained);
        }

        [Theory]
        [InlineData(new double[] { 300, 500, 700 })]
        public void ReturnsZeroIfTheAuctionHasNotStarted(double[] offers)
        {
            //Arrange
            var auction = new AuctionCore.Auction("Iphone XS");
            var Joe = new Interested("Joe", auction);

            //Act 
            foreach (var offer in offers)
                auction.ReceiveBid(Joe, offer);

            auction.End();

            //Assert
            var valueobtained = auction.Bids.Count();

            Assert.Equal(0, valueobtained);
        }

        [Fact]
        public void NotAcceptedNextBidDealSameCustomerPerformedLastBid()
        {
            //Arrange
            var auction = new AuctionCore.Auction("Iphone XS");
            var Joe = new Interested("Joe", auction);

            auction.Start();
            auction.ReceiveBid(Joe, 500);

            //Act 
            auction.ReceiveBid(Joe, 900);

            auction.End();

            //Assert
            var valueExpected = 1;
            var valueobtained = auction.Bids.Count();

            Assert.Equal(valueExpected, valueobtained);
        }
    }
}
