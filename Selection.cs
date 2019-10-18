using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoProject
{
    // Class Selection is responsible to handle the selection of numbers a ticket must include.
    // The selected numbers are defined by the player by calling the appropriate methods each time.
    class Selection
    {
        // Constants
        private const int MinNumberForSelection = 1; // The minimum number a Player can select in a KINO ticket.
        private const int MaxNumberForSelection = 12; // The maximum number a Player can select in a KINO ticket.
        // Random Number Generator
        private static int seed = (int)DateTime.Now.Ticks;
        private static Random randomNumberGenerator = new Random(seed);

        // Generates selection numbers for the KINO tickets either
        // randomly or by input data based on the Player's preferences.
        public static void GenerateNumbersBySelection(List<Ticket> listOfTickets)
        {
            // Local variables
            List<int> listOfNumbers; // A list to store the selected numbers [1..12].
            bool useRandomData = true; // Used for accessing the code to generate random data.
            bool inputDataIsValid = true; // Used for loop again the code for input data.

            do
            {
                inputDataIsValid = true;
                Console.Write("Use Random or Input of Data ? ");
                string input = Console.ReadLine();

                // Nested Ternary Operator:
                // Check if input of data is valid. If yes the program will flow accordingly.
                useRandomData =
                    input.Equals("ID") || input.Equals("Id") || input.Equals("id") ? false
                    : input.Equals("RD") || input.Equals("Rd") || input.Equals("rd") ? true
                    : inputDataIsValid = false;

                // Random selection of numbers [1..80] within a ticket.
                // Random number of selections [1..12] in a ticket.
                if (inputDataIsValid && useRandomData)
                {
                    foreach (Ticket ticket in listOfTickets)
                    {
                        listOfNumbers = new List<int>(MaxNumberForSelection); // Initialize the list of numbers.

                        // Elements of a list of numbers per Ticket are removed randomly,
                        // until the capacity remains between [12..1] also randomly.
                        for (int j = Ticket.MaxNumberForTicket; j > randomNumberGenerator.Next(MinNumberForSelection, MaxNumberForSelection); j--)
                        {
                            ticket.ListOfNumbersPerTicket.RemoveAt(randomNumberGenerator.Next(j));
                        }
                    }
                }

                // Selecting numbers by input data from a specific range [1..12].
                else if (inputDataIsValid && !useRandomData)
                {
                    uint numbersOfTicket = 0;
                    foreach (Ticket ticket in listOfTickets)
                    {
                        listOfNumbers = new List<int>(MaxNumberForSelection); // Initialize the list of numbers.
                        do
                        {
                            inputDataIsValid = true;
                            try
                            {
                                Console.Write($"Enter the numbers [1..12] you want to play for ticket with ID #{ticket.ID}: ");
                                numbersOfTicket = uint.Parse(Console.ReadLine());
                            }
                            catch (Exception ex)
                            {
                                // Input number is neither an integer nor is positive.
                                if (ex is FormatException || numbersOfTicket <= 0)
                                {
                                    Console.Write($"\n{ex.Message}. ");
                                    CustomException.CheckExceptions(ex);
                                    inputDataIsValid = false;
                                }
                            }
                            // Loop will continue the execution.
                        } while (!inputDataIsValid || numbersOfTicket <= 0 || numbersOfTicket > MaxNumberForSelection);

                        // Loop to select numbers from a specific range [1..80].
                        for (int i = 1; i <= numbersOfTicket; i++)
                        {
                            uint selectionNumber = 0;
                            do
                            {
                                inputDataIsValid = true;
                                try
                                {
                                    Console.Write($"Enter Selection Number [1..80] #{i}: ");
                                    selectionNumber = uint.Parse(Console.ReadLine());
                                }
                                catch (Exception ex)
                                {
                                    // Input number is neither an integer nor is positive.
                                    if (ex is FormatException || selectionNumber <= 0)
                                    {
                                        Console.Write($"\n{ex.Message}. ");
                                        CustomException.CheckExceptions(ex);
                                        inputDataIsValid = false;
                                    }
                                }
                                // Loop will continue the execution.
                            } while (!inputDataIsValid || selectionNumber <= 0 || selectionNumber > Ticket.MaxNumberForTicket);
                            // Call of method to check for duplicate numbers in the corresponding list with numbers.
                            CheckListForDuplicates(listOfNumbers, selectionNumber, ref i); 
                        }
                        // Store the new list of numbers in the corresponding list of the specific ticket.
                        ticket.ListOfNumbersPerTicket = listOfNumbers;
                    }
                }
            } while (!inputDataIsValid);
        }

        // Check Ticket's list of numbers for dublicate entries.
        // The parameter idx is passed by reference for the corresponding
        // variable i (counter) of a for loop used by the above method.
        private static void CheckListForDuplicates(List<int> listOfNumbers, uint selectionNumber, ref int idx)
        {
            if (listOfNumbers.Contains(Convert.ToInt32(selectionNumber)))
            {
                Console.WriteLine("Number already given. Try again...");
                // If a number is already exist in the list, the i counter will be decreased by one.
                idx = idx - 1;
            }
            else
            {
                // The number is added in the existing list.
                listOfNumbers.Add(Convert.ToInt32(selectionNumber));
            }
        }

    }
}
