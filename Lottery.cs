using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoProject
{
    // Class Lottery involves the drawing of numbers at random for a prize.
    // The numbers that can be drawn from a pre-fixed range [1..80] are limited to 20.
    class Lottery
    {
        // Fields
        private static int LotteryID = 1000; // Lottery ID starts from 1000, can be changed accordingly.
        // Constants
        private static double amount = 0; // The initial amount of a (OR EACH) draw.
        private static double jackpot = 0; // the funds that are left over from previous draws.
        private const int NumbersForDraw = 20; // Used to determine the range of numbers that can be drawn in a lottery.
        // Random Number Generator
        private static int seed = (int)DateTime.Now.Ticks;
        private static Random randomNumberGenerator = new Random(seed);
        // Properties
        public int ID { get; private set; } // The ID that uniquely identifies a KINO draw (auto-increment).
        public List<int> ListOfDraws { get; set; }
        public DateTime DateOfDraw { get; set; } // The date of which a KINO draw was made.

        // The isKinoBonus and listOfNumbersPerKinoTicket will be retrieved from class Player.
        

        // Default Constructor
        public Lottery()
        {
            ID = ++LotteryID;
            ListOfDraws = new List<int>(NumbersForDraw);
        }

        // Draws the (unique) lucky numbers [20] and stores them in a list.
        private static List<int> DrawNumbers()
        {
            List<int> newListWithDrawNumbers = new List<int>(NumbersForDraw); // Holds the lucky numbers.

            // Loop to initialize the list with the lucky numbers.
            for (int i = 0; i < newListWithDrawNumbers.Capacity; i++)
            {
                // Add the randomly generated number from a specific range [1..80].
                newListWithDrawNumbers.Add(randomNumberGenerator.Next(Ticket.MinNumberForTicket, Ticket.MaxNumberForTicket));

                // Loop to check for duplicates.
                for (int j = 0; j <= i - 1; j++)
                {
                    // If array index is in the 2nd position and previous element is equal with last.
                    if (i > 1 && newListWithDrawNumbers[j] == newListWithDrawNumbers[i])
                    {
                        newListWithDrawNumbers.RemoveAt(i);
                        i--; // In order to maintain the index position decrease counter i by 1.
                        break;
                    }
                }
            }
            return newListWithDrawNumbers;
        }

        public static void PrintDraw()
        {
            var listOfLuckyNumbers = DrawNumbers();

            Console.Write("\nThe Draw of the lucky numbers will begin by pressing a key...");
            Console.ReadKey();
            Console.WriteLine("\n");
            foreach (var number in listOfLuckyNumbers)
            {
                Console.Write($"|{number}");
                
            }
            Console.Write("|");

            var kinoBonus = listOfLuckyNumbers.Last();
            Console.WriteLine($"\nThe Kino Bonus is: {kinoBonus}\n");
        }




    }
}
