using AuctionCore;
using Xunit;

namespace Test.Auction
{
    public class AuctionEnd
    {
        [Theory]
        [InlineData(1200, new double[] { 800,900,1000,1200})]
        [InlineData(1000, new double[] { 800,900,1000,990})]
        [InlineData(800, new double[] { 800})]
        public void ReturnsHighestValueGivenAnAuctionWithAtLeastOneValue(double valueexpected, double[] ofertas)
        {
            //Arrange
            var auction = new AuctionCore.Auction("PICTURE");
            var joe = new Interested("Joe Delaney", auction);
            var luise = new Interested("Luise Farmo", auction);

            foreach (var value in ofertas)
            {
                auction.ReceiveBid(joe, value);
            }

         
            //Act 
            auction.End();

            //Assert
            var valueobtained = auction.Winner.Value;

            Assert.Equal(valueexpected, valueobtained);

        }

        [Fact]
        public void ReturnsZeroGivenToNoBidAuction()
        {
            //Arrange
            var auction = new AuctionCore.Auction("PICTURE");

            //Act 
            auction.End();

            //Assert
            var valueexpected = 0;
            var valueobtained = auction.Winner.Value;

            Assert.Equal(valueexpected, valueobtained);
        }

    }
}
