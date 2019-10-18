using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoProject
{
    // Class Ticket is responsible to deliver new Tickets on-demand. The number of tickets to
    // be credited is determined by the player by calling the appropriate methods each time.
    class Ticket
    {
        // Fields
        private static int TicketID = 100; // Ticket ID starts from 100, can be changed accordingly.
        // Constants
        public const int MinNumberForTicket = 1; // The maximum number that a KINO ticket can have.
        public const int MaxNumberForTicket = 80; // The maximum number that a KINO ticket can have.
        // Properties
        public int ID { get; private set; } // The ID that uniquely identifies a KINO ticket (auto-increment).
        public List<int> ListOfNumbersPerTicket { get; set; }
        private DateTime DatePlayed { get; set; } // The date of which a KINO ticket is played.
        private bool isKinoBonus { get; set; } // A flag that marks a ticket with the optional KINO Bonus option.

        // Default Constructor: Tickets cannot be created directly from the Main Program.
        private Ticket()
        {
            // When creating a new Ticket, it initiates with the following values:
            ID = ++TicketID; // Acquires a unique ID.
            ListOfNumbersPerTicket = new List<int>(MaxNumberForTicket); // Acquires a new list to store the numbers.
            DatePlayed = DateTime.Now; // Acquires a date and time when that particular ticket was created.
        }

        // Initializes a list with the available range [1..80] of numbers for a KINO ticket.
        private static void InitializeList(List<int> listOfNumbers)
        {
            for (int i = 1; i <= listOfNumbers.Capacity; i++)
            {
                listOfNumbers.Add(i);
            }
        }

        // Check Player's choice for KINO Bonus option + validation of the input of data.
        private static bool CheckKinoBonusInput()
        {
            // Flags to handle the control of the following loop.
            bool isKinoBonusActive = true; // Determined by the Player's selection for KINO Bonus.
            bool inputDataIsValid = false; // Determined by the validity of input of data.
            
            do
            {
                Console.Write($"Play KINO Bonus for Ticket with ID #{TicketID}: ");
                string playerChoiceForKINOBonus = Console.ReadLine();

                switch (playerChoiceForKINOBonus)
                {
                    case "Y":
                    case "y":
                    case "YES":
                    case "Yes":
                    case "yes":
                        isKinoBonusActive = true;
                        inputDataIsValid = true;
                        break;
                    case "N":
                    case "n":
                    case "NO":
                    case "No":
                    case "no":
                        isKinoBonusActive = false;
                        inputDataIsValid = true;
                        break;
                    default:
                        Console.WriteLine("\nInvalid input of data. Please try again...");
                        inputDataIsValid = false; // Loop will continue the execution.
                        break;
                }
            } while (!inputDataIsValid);

            return isKinoBonusActive; // Returns the Player's choice for the KINO Bonus option.
        }

        // Generates Tickets on-demand, classified based on their ID.
        public static List<Ticket> GenerateTickets(int counter)
        {
            Ticket newTicket = null;
            List<Ticket> newListOfTickets = new List<Ticket>(); // A list to store the new tickets.
            uint numberOfTickets = 0; // Holds the input number of Tickets.
            bool inputDataIsValid = true; // Determined by the validity of input of data.

            do
            {
                inputDataIsValid = true;
                try
                {
                    Console.Write($"\nNumber of Tickets for Player #{counter}: ");
                    numberOfTickets = uint.Parse(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    // Input number is neither an integer nor is positive.
                    if (ex is FormatException || numberOfTickets <= 0)
                    {
                        Console.Write($"\n{ex.Message}. ");
                        CustomException.CheckExceptions(ex);
                        inputDataIsValid = false;
                    }
                }
            } while (!inputDataIsValid || numberOfTickets <= 0); // Loop will continue the execution.

            for (int i = 0; i < numberOfTickets; i++)
            {                
                newTicket = new Ticket()
                {
                    // Call method to check Player's choice for the KINO Bonus.
                    isKinoBonus = CheckKinoBonusInput()
                };

                // Call method to initialize a list with numbers [1..80].
                InitializeList(newTicket.ListOfNumbersPerTicket);

                // Add recently created Ticket in the list with Tickets.
                newListOfTickets.Add(newTicket);
            }
            return newListOfTickets; // Returns a list with the recently created tickets.
        }

        // Prints Tickets per Player with their current values.
        public override string ToString()
        {
            string str = (isKinoBonus == true) ? str = "Yes" : str = "No"; // Mask KINO Bonus property value.
            return $"\nTicket ID: {ID}, Date Played: {DatePlayed}, KINO Bonus: {str}\nSelection Numbers:";
        }


//------------------------------------------------------------------------------------------------------------------------
        //public static void PrintTickets()
        //{
        //    var tickets = GenerateTickets();

        //    foreach (Ticket t in tickets)
        //    {
        //        Console.WriteLine($"Ticket ID: {t.ID}");
        //        for (int i = 0; i < t.NumbersSelectionList.Capacity; i++)
        //        {
        //            Console.WriteLine($"Ticket Number: {t.NumbersSelectionList[i]}");
        //        }
        //    }
        //}


    }
}
