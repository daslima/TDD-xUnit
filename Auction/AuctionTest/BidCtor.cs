using AuctionCore;
using System;
using Xunit;

namespace Test.Auction
{
    public class BidCtor
    {
        [Theory]
        [InlineData(-100)]
        [InlineData(-2)]
        public void BidArgumentExceptionValueLessThanZero(double value)
        {
            //Arrange - cenário
            var negativeValue = value;

            //Assert
            var obtainedException = Assert.Throws<ArgumentException>(
                //Act
                () => new Bid(null, negativeValue)
            );

            var message = "Value must be greater than zero";

            Assert.Equal(message, obtainedException.Message);
        }

    }
}
