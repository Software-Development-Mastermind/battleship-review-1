using System;

namespace BattleshipKing
{
    class Program
    {        
        int xPos;
        int yPos;
        int resultCounter = 0;
        int hitCounter = 0;
        int missCounter = 0;
        public string[] results = new string[8];

        public static void Main(string[] args)
        {
            var Program = new Program();

            var battleship = new Battleship();
            for (int i = 0; i < 8; i++)
            {
                Console.Clear();
                Console.WriteLine("****************************************************");
                Console.WriteLine($"Welcome to Battleship");
                Console.WriteLine($"Round: {i + 1}");
                Console.WriteLine("****************************************************");
                Program.DisplayAllResults();

                Console.WriteLine(battleship.ShipSternX);
                Console.WriteLine(battleship.ShipSternY);
                
                Program.PromptUserForX();
                Program.PromptUserForY();
                if (Program.xPos == 0 || Program.yPos == 0)
                {
                    Console.WriteLine("Invalid entry.  Run program again to try again.");
                    return;
                }

                Console.WriteLine("****************************************************");
                Program.DetectHit(battleship, Program.xPos, Program.yPos);
                Console.WriteLine("****************************************************");
                Console.WriteLine();                
            }
            
        }

        public void PromptUserForX()
        {
            Console.WriteLine("Select Firing Position for -X-:  ");
            xPos = ConvertToInteger(Console.ReadLine());            
        }

        public void PromptUserForY()
        {
            Console.WriteLine("Select Firing Position for -Y-:  ");
            yPos = ConvertToInteger(Console.ReadLine());
        }





        public void DetectHit(Battleship battleship, int xValue, int yValue)
        {
            string result = "";

            if (xValue == battleship.ShipSternX && yValue == battleship.ShipSternY)
            {
                hitCounter++;
                if (hitCounter == 5)
                {
                    Console.WriteLine("You've Sank My BattleShip!!!");
                    result = "HIT, and SUNK!!!";
                }
                else
                {
                    Console.WriteLine("You hit my battleship");
                    result = "HIT!!!";
                }                
            }
            else
            {
                missCounter++;
                Console.WriteLine("You missed!");
                result = "Miss :-(";
            }

            string resultText = $"{resultCounter + 1}:  {xValue}, {yValue}:  {result}";
            results[resultCounter] = resultText;

            resultCounter++;
            Console.WriteLine($"{resultCounter} shots fired");
            Console.WriteLine($"Hits:  {hitCounter}");
            Console.WriteLine($"Misses:  {missCounter}");
        }

        public void DisplayAllResults()
        {
            Console.WriteLine("****************************************************");
            Console.WriteLine("Previous Results:");
            foreach (var result in results)
            {
                if (result != null)
                {
                    Console.WriteLine(result);
                }                
            }           
        }


        public int ConvertToInteger(string x)
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
