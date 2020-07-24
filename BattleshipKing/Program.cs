using System;
using System.Runtime.InteropServices.ComTypes;

namespace BattleshipKing
{
    class Program
    {        
        int xPos;
        int yPos;
        int resultCounter = 0;
        int hitCounter = 0;
        int missCounter = 0;
        string[] results = new string[8];

        // Still need to develop this to stop the ability
        // to use the same coordinates twice.
        // Need to determine how to compare, then block
        int[,] userShots = new int[8, 2];

        public static void Main()
        {
            var Program = new Program();
            var battleShip = new Battleship();

            for (int i = 0; i <= 7; i++)
            {
                battleShip.FillPositionArray();
                Program.DisplayHeader(i);
                Program.DisplayAllResults();

                Program.PromptUserForX();
                Program.PromptUserForY();

                Console.WriteLine("****************************************************");
                Program.DetectHit(battleShip, Program.xPos, Program.yPos);
                Console.WriteLine("****************************************************");
                Console.WriteLine();

                Program.PauseGame();
            }
            
        }

        public void PauseGame()
        {
            Console.WriteLine("Press Any Key\nTo Continue To The Next Round...");
            Console.WriteLine();
            Console.ReadKey();
            Console.Clear();
        }

        public void DisplayHeader(int i)
        {
            Console.WriteLine("****************************************************");
            Console.WriteLine("\t -Welcome to Mean Battleship King-");
            Console.WriteLine("   -Where the computer will hurt your feelings-");
            Console.WriteLine("****************************************************");
            Console.WriteLine();
            Console.WriteLine($"\t\tRound: {i + 1} of 8");
            Console.WriteLine();
        }

        public void PromptUserForX()
        {
            bool valid = false;
            while (!valid)
            {
                Console.WriteLine("Enter Value for X (1-10):");
                xPos = ConvertToInteger(Console.ReadLine());
                valid = xPos > 0;
            }            
        }

        public void PromptUserForY()
        {
            bool valid = false;
            while (!valid)
            {
                Console.WriteLine("Enter Value for Y (1-10):");
                yPos = ConvertToInteger(Console.ReadLine());
                valid = yPos > 0;
            }            
        }

        public void DetectHit(Battleship battleShip, int xValue, int yValue)
        {
            string result = "";
            bool directHit = false;

            for (int i = 0; i < battleShip.ShipSpan.GetLength(0); i++)
            {
                if (xValue == battleShip.ShipSpan[i, 0] && yValue == battleShip.ShipSpan[i, 1])
                {
                    hitCounter++;
                    directHit = true;
                    if (hitCounter == 5)
                    {                        
                        result = "SUNK!";
                        break;
                    }
                    else
                    {
                        result = "HIT!";
                    }
                    break;
                }
            }
            
            if (!directHit)
            {
                missCounter++;
                result = "Miss!";
            }
            
            resultCounter++;

            DisplayGameStats(
                    directHit,
                    xValue,
                    yValue,
                    result,
                    resultCounter,
                    hitCounter,
                    missCounter,
                    battleShip
                );
        }
     
        public void DisplayGameStats(bool directHit, int xValue, int yValue, string result, int resultCounter, int hitCounter, int missCounter, Battleship battleShip)
        {
            string resultText = $"{resultCounter}:  {result}\t(X={xValue},\tY={yValue})";
            results[resultCounter - 1] = resultText;

            var gameSession = new GameSession();

            // Add user shots to user array
            // Use to compare and make sure
            // the user doesn't use the same coordinates
            userShots[resultCounter - 1, 0] = xValue;
            userShots[resultCounter - 1, 1] = yValue;

            Console.Clear();

            switch (result)
            {
                case "Miss!":
                    Console.WriteLine();
                    Console.WriteLine(gameSession.ShotReactions[battleShip.GenerateRandomNumber() - 1]);
                    Console.WriteLine();
                    break;
                case "HIT!":
                    Console.WriteLine();
                    Console.WriteLine("You've hit my Battleship!!!");
                    Console.WriteLine();
                    break;
                case "SUNK!":
                    Console.WriteLine();
                    Console.WriteLine("You've SUNK MY BATTLESHIP!!!");
                    Console.WriteLine();
                    EndGame(battleShip);
                    break;
            }

            if (directHit) Console.WriteLine("Nice Shot!");

            string shotOrShots = resultCounter > 1 ? "shots" : "shot";
            string hitOrHits = hitCounter > 1 ? "hits" : "hit";
            string missOrMisses = missCounter > 1 ? "misses" : "miss";
            Console.WriteLine($"{resultCounter} {shotOrShots} fired");
            Console.WriteLine("-----------");
            Console.WriteLine($"\t-{hitCounter} {hitOrHits}");
            Console.WriteLine($"\t-{missCounter} {missOrMisses}");
        }

        public void EndGame(Battleship battleShip)
        {
            DisplayAllResults();
            Console.WriteLine("Game Over");
            Console.WriteLine("Press Any Key to Exit");
            Grid10x10(battleShip);
            Console.ReadKey();
            Environment.Exit(0);
        }

        public void Grid10x10(Battleship battleShip)
        {
            for (int i = 0; i <= 9; i++)
            {
                for (int k = 0; k <= 9; k++)
                {
                    if (i <= 4)
                    {
                        if (battleShip.ShipSpan[i, 0] == i)
                        {
                            if (battleShip.ShipSpan[i, 1] == k)
                            {
                                Console.Write("X");
                                continue;
                            }
                        }
                    }
                    else 
                    {
                        Console.Write($"-");
                    }
                    


                }
                Console.WriteLine();
            }
        }

        public void DisplayAllResults()
        {            
            foreach (var result in results)
            {
                if (result != null)
                {
                    if (result == results[0])
                    {
                        Console.WriteLine("****************************************************");
                        Console.WriteLine("Your Previous Results:");
                        Console.WriteLine();
                    }

                    Console.WriteLine(result);                   
                }
            }
            if (results[0] != null) Console.WriteLine();
            Console.WriteLine("****************************************************");
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
    }
}
