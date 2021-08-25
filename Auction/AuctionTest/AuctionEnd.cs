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
        public void ReturnsHighestValueGivenAnAuctionWithAtLeastOneValue(double valueexpected, double[] offers)
        {
            //Arrange
            var auction = new AuctionCore.Auction("PICTURE");
            var joe = new Interested("Joe Delaney", auction);
            var luise = new Interested("Luise Farmo", auction);

            auction.Start();

            for (int i = 0; i < offers.Length; i++)
            {
                var value = offers[i];
                if (i % 2 == 0)
                    auction.ReceiveBid(joe, value);
                else
                    auction.ReceiveBid(luise, value);
            }

            foreach (var value in offers)
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

            auction.Start();

            //Act 
            auction.End();

            //Assert
            var valueexpected = 0;
            var valueobtained = auction.Winner.Value;

            Assert.Equal(valueexpected, valueobtained);
        }

        [Fact]
        public void ThrowInvalidOperationExceptionAuctionNotStarted()
        {
            //Arranje 
            var auction = new AuctionCore.Auction("Mouse");
            
            //Assert
            var obtainedException =  Assert.Throws<System.InvalidOperationException>(
                //Act 
                () => auction.End()
            );

            var message = "It is necessary to start the auction before finishing";

            Assert.Equal(message, obtainedException.Message);
        }

    }
}
