using AuctionCore;
using AuctionCore.Modality;
using System;

namespace AuctionAppConsole
{
    class Program
    {
        static void Main()
        {
            AuctionWithBids();
            AuctionWithOnebid();
        }

        public static void AuctionWithOnebid()
        {
            //Arranje
            IModality modality = new HighestValue();
            var auction = new Auction("PICTURE", modality);
            var joe = new Interested("Joe Delaney", auction);

            auction.ReceiveBid(joe, 500);

            //Act
            auction.End();

            //Assert
            var valueexpected = 600;
            var valueobtained = auction.Winner.Value;

            Check(valueexpected, valueobtained);
        }

        public static void AuctionWithBids()
        {
            //Arranje
            IModality modality = new HighestValue();
            var auction = new Auction("PICTURE", modality);
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

            Check(valueexpected, valueobtained);
         
        }

        public static void Check(double valueexpected, double valueobtained)
        {
            if (valueexpected == valueobtained)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("TEST SUCCESS");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("TEST FAILED");
            }

            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
