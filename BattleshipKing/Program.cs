using System;

namespace BattleshipKing
{
    class Program
    {        
        static int xPos;
        static int yPos;
        static int resultCounter = 0;
        public static string[] results = new string[8];

        public static void Main(string[] args)
        {
            var battleship = new Battleship();
            Console.WriteLine(battleship.ShipSternX);
            Console.WriteLine(battleship.ShipSternY);
            DisplayAllResults();
            PromptUserForX();
            PromptUserForY();
            DetectHit(battleship, xPos, yPos);
            
            Console.WriteLine($"x = {xPos}, y = {yPos}");  
            Console.WriteLine($"Battleship position = {battleship.ShipSternX}, {battleship.ShipSternY}");
        }

        public static void PromptUserForX()
        {
            Console.WriteLine("Select X Firing Position");
            Console.WriteLine("X:  ");
            xPos = ConvertToInteger(Console.ReadLine());            
        }

        public static void PromptUserForY()
        {
            Console.WriteLine("Select Y Firing Position");
            Console.WriteLine("Y:  ");
            yPos = ConvertToInteger(Console.ReadLine());
        }

        public static void DetectHit(Battleship battleship, int xValue, int yValue)
        {
            if (xValue == battleship.ShipSternX && yValue == battleship.ShipSternY)
            {
                Console.WriteLine("You hit my battleship");
            }
            else
            {
                Console.WriteLine("You missed!");
            }
        }

        public static void DisplayAllResults()
        {

            results[0] = "Miss";
            Console.WriteLine("Previous Results");

            foreach (var result in results)
            {
                Console.WriteLine(result);
            }           
        }


        public static int ConvertToInteger(string x)
        {
            try
            {
                return int.Parse(x);
            }
            catch (Exception)
            {

                return 0;
            }

        }
        


        // Pseudo Code
        // 10x10 Game Grid
        // Randomly assign battleship

        // Create Battleship Class
        //   - Random beginning grid position
        //   - 5 hits to sink, so ship will be 5 segments long.
        //   - After 8 guesses, game is over


        // Create "GameSession" class
        //   - Gets instantiated when the Battleship class is instantiated
        //   - Contains vital game data:
        //      - How many direct hits
        //      - How many turns have been taken
        //      - 

        // 8 user guesses

        // Notify user of hit/miss after each fire
        //   - Keep track of how many hits (Use array to hold each result)
        //   - Battleship sunk after 5 hits
        // If incorrect value entered, notify user to re-enter
        //
        // Prompt User for Column (1-10)
        // Prompt User for Row (1-10)
        // Assign input values to variable, maybe an array.


    }
}
