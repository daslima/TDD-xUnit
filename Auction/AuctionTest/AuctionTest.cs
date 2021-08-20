using AuctionCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Test.Auction
{
    public class AuctionTest
    {
        [Fact]
        public void AuctionWithOnebid()
        {
            //Arranje
            var auction = new AuctionCore.Auction("PICTURE");
            var joe = new Interested("Joe Delaney", auction);

            auction.ReceiveBid(joe, 500);

            //Act
            auction.End();

            //Assert
            var valueexpected = 500;
            var valueobtained = auction.Winner.Value;

            Assert.Equal(valueexpected, valueobtained);

        }

        [Fact]
        public void AuctionWithBids()
        {
            //Arranje
            var auction = new AuctionCore.Auction("PICTURE");
            var joe = new Interested("Joe Delaney", auction);
            var luise = new Interested("Luise Farmo", auction);

            auction.ReceiveBid(joe, 500);
            auction.ReceiveBid(luise, 800);
            auction.ReceiveBid(joe, 900);
            auction.ReceiveBid(luise, 850);

            //Act 
            auction.End();

            //Assert
            var valueexpected = 900;
            var valueobtained = auction.Winner.Value;

            Assert.Equal(valueexpected, valueobtained);

        }

    }
}
