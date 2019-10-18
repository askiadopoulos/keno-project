using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoProject
{
    // Class Player is responsible to deliver new Players on-demand. The number of players to
    // be created is determined by the player by calling the appropriate methods each time.
    class Player
    {
        // Fields
        private static int counter = 0; // Records the number of Players that have been created.
        private static List<int> listOfCounts = new List<int>(); // Holds the number of Players (NOT THE PLAYERS).
        private static int PlayerID = 10; // Player ID starts from 10, can be changed accordingly.
        // Properties
        private int ID { get; set; } // The ID that uniquely identifies a Player (auto-increment).
        // Implement One-To-Many Relationship: A player can play KINO with more than one tickets.
        private List<Ticket> Tickets { get; set; }
        // Accessor to get the Player's counter current value.
        private static int Counter { get { return counter; } }
        // Accessor to get the Player counter list.
        private static List<int> ListOfCounts { get { return listOfCounts; } }
        
        // Default Constructor: Players cannot be created directly from the Main Program.
        private Player()
        {
            // When creating a new Player, it initiates with the following values:
            ListOfCounts.Add(++counter); // Player counter value is added to the corresponding list.
            ID = ++PlayerID; // Acquires a unique ID.
            Tickets = new List<Ticket>(); // Acquires a new list to store the tickets.
        }

        // Generates Players on-demand, classified based on their ID and
        // store them in a list together with their corresponding tickets.
        private static List<Player> GeneratePlayers()
        {
            Player newPlayer = null;
            List<Player> newListOfPlayers = new List<Player>(); // A list to store the new players.
            uint numberOfPlayers = 0; // Holds the input number of Players.
            bool inputDataIsValid = true; // Determined by the validity of input of data.

            do
            {
                inputDataIsValid = true;
                try
                {
                    Console.Write("\nNumber of Players to join KINO: ");
                    numberOfPlayers = uint.Parse(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    // Input number is neither an integer nor is positive.
                    if (ex is FormatException || numberOfPlayers <= 0)
                    {
                        Console.Write($"\n{ex.Message}. ");
                        CustomException.CheckExceptions(ex);
                        inputDataIsValid = false;
                    }
                }
            } while (!inputDataIsValid || numberOfPlayers <= 0); // Loop will continue the execution.

            for (int i = 0; i < numberOfPlayers; i++)
            {
                newPlayer = new Player();

                // Call method to create Tickets on-demand.
                var listOfTickets = Ticket.GenerateTickets(Counter);

                // Call method to generate numbers randomly or by input data.
                Selection.GenerateNumbersBySelection(listOfTickets);

                // Add recently created Ticket in the list with Tickets.
                newPlayer.Tickets = listOfTickets;

                // Add recently created Player in the list with Players.
                newListOfPlayers.Add(newPlayer);
            }
            return newListOfPlayers; // Returns a list with the recently created players.
        }

        // Starts the KINO game by calling the method below.
        public static void StartKino()
        {
            List<Player> listOfPlayers; // A list to store the Players.
            int counter = 0; // Records the number of program executions.
            bool programIsRunning; // Determines whether the program will run repeatedly.
            bool inputDataIsValid = true; // If the input data is invalid the loop will be executed again.

            // Create an object of type CultureInfo to hold culture-specific
            // information, such as the associated country/region.
            CultureInfo culture = new CultureInfo("en-US");
            var now = DateTime.Now; // Get the current date and time.
            // Loop to handle the execution of the block of code.
            do
            {
                programIsRunning = true;
                counter++;
                Console.Clear();
                Console.WriteLine(now.ToString("[dddd MM yyyy HH:mm]", culture)); // print DateTime in this format.
                Console.Write($"KINO GAME LOTTERY NUMBER #{counter}\n");

                // Call of method to create new Players.
                listOfPlayers = GeneratePlayers();
                // Call of method to print the data of all the Players.
                PrintPlayers(listOfPlayers);
                // Loop to handle the validity of input of data.
                do
                {
                    inputDataIsValid = true;
                    Console.Write("\nDo you want to play again in a new lottery ? ");
                    string input = Console.ReadLine();

                    // Nested Ternary Operator:
                    // Check if input of data is valid. If yes the program will flow accordingly.
                    programIsRunning =
                        input.Equals("Y") || input.Equals("y") || input.Equals("YES") || input.Equals("Yes") ||
                        input.Equals("yes") ? true
                        : input.Equals("N") || input.Equals("n") || input.Equals("NO") || input.Equals("No") ||
                        input.Equals("no") ? false : inputDataIsValid = false;

                    if (!inputDataIsValid) { Console.WriteLine("\nInvalid input of data. Please try again..."); }

                } while (!inputDataIsValid);
            } while (programIsRunning);

            Console.Write("\nThank you for joining KINO.");
        }

        //------------------------------------------------------------------------------------------------------------------------
        // Prints all the Players according with Player ID, Ticket ID and selection of Ticket numbers.
        private static void PrintPlayers(List<Player> players)
        {
            int i = 0;
            Console.Clear();

            // Iterates the list of Players.
            foreach (Player p in players)
            {
                // Iterates the list that holds the number of players.
                for (; i < ListOfCounts.Count;)
                {
                    Console.WriteLine($"\nPlayer #{ListOfCounts[i]}{p.ToString()}");
                    // Loop to print the Ticket ID.
                    foreach (Ticket t in p.Tickets)
                    {
                        Console.Write($"{t.ToString()}");
                        // Loop to print the numbers per Ticket.
                        for (int j = 0; j < t.ListOfNumbersPerTicket.Count(); j++)
                        {
                            Console.Write($"|{t.ListOfNumbersPerTicket[j]}");
                        }
                        Console.WriteLine("|");
                    }
                    i++;
                    break;
                }
            }
            Console.WriteLine();
        }
        public override string ToString()
        {
            return $" with ID: {ID}";
        }


        // Alternatively way with Lambda Expression + ForEach method
        //tickets.ForEach(x => Console.WriteLine("Ticket ID: " + x.ID));
        //tickets.ForEach(y => Console.WriteLine("Ticket Number: " + y.numbersSelectionList[y.ID]));

        // Alternatively way with LINQ query - does not print the element of the list
        //var querySyntax = from t in tickets
        //                  select new { t.ID, t.numbersSelectionList };

        //foreach (var t in querySyntax)
        //{
        //    Console.WriteLine(t);
        //}

    }
}
