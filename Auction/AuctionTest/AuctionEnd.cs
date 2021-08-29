using AuctionCore;
using AuctionCore.Modality;
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
            IModality modality = new HighestValue();
            var auction = new AuctionCore.Auction("PICTURE", modality);
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
            IModality modality = new HighestValue();
            var auction = new AuctionCore.Auction("PICTURE", modality);

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
            IModality modality = new HighestValue();
            var auction = new AuctionCore.Auction("Mouse",modality);
            
            //Assert
            var obtainedException =  Assert.Throws<System.InvalidOperationException>(
                //Act 
                () => auction.End()
            );

            var message = "It is necessary to start the auction before finishing";

            Assert.Equal(message, obtainedException.Message);
        }

        [Theory]
        [InlineData(1200,1250,new double[] {800,1150,1400,1250})]
        public void ClosestSuperiorOffer(double destinationValue, double expectedValue, double[] offers)
        {
            //Arrange
            IModality modality = new ClosestTopOffer(destinationValue);
            var auction = new AuctionCore.Auction("PICTURE", modality);
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

            //Act 
            auction.End();

            //Assert
            var valueobtained = auction.Winner.Value;

            Assert.Equal(expectedValue, valueobtained);
        }

    }
}
