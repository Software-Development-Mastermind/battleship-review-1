using System;
using System.Runtime.InteropServices.ComTypes;

namespace BattleshipKing
{
    class Program
    {        
        private int _xPos;
        private int _yPos;
        private int _resultCounter = 0;
        private int _hitCounter = 0;
        private int _missCounter = 0;
        private readonly string[] _results = new string[8];
        private readonly int[,] _userShots = new int[8, 2];

        public static void Main()
        {
            var Program = new Program();
            Program.StartGame();
        }

        public void StartGame()
        {
            var battleShip = new Battleship();

            for (int i = 0; i <= 7; i++)
            {
                // Temporary display battleShip location
                // Delete before deployment
                battleShip.FillPositionArray();

                DisplayHeader(i);
                DisplayResults();
                GetUserXY();

                Console.WriteLine("****************************************************");
                DetectHit(battleShip);
                Console.WriteLine("****************************************************");
                Console.WriteLine();

                PauseGame();
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

        public void GetUserXY()
        {
            do
            {
                PromptUserForX();
                PromptUserForY();

            } while (DetectDuplicate());
        }

        public bool DetectDuplicate()
        {
            bool duplicate = false;
            for (int i = 0; i < _userShots.GetLength(0); i++)
            {
                if (_xPos == _userShots[i, 0] && _yPos == _userShots[i, 1])
                {
                    duplicate = true;
                    Console.WriteLine("Cannot use duplicate coordinates");
                    Console.WriteLine("Press Any Key To Continue...");
                    Console.ReadKey();
                    //ClearLastLine();
                    ClearLines(6);
                    break;
                }
            }
            return duplicate;
        }

        public void PromptUserForX()
        {
            bool valid = false;
            while (!valid)
            {
                Console.WriteLine("Enter Value for X (1-10):");
                _xPos = ConvertToInteger(Console.ReadLine());
                valid = _xPos > 0;
                if (!valid) ClearLines(2);
            }            
        }

        public void PromptUserForY()
        {
            bool valid = false;
            while (!valid)
            {
                Console.WriteLine("Enter Value for Y (1-10):");
                _yPos = ConvertToInteger(Console.ReadLine());
                valid = _yPos > 0;
                if (!valid) ClearLines(2);
            }            
        }               

        public void DetectHit(Battleship battleShip)
        {
            string result = "";
            bool directHit = false;

            for (int i = 0; i < battleShip.ShipSpan.GetLength(0); i++)
            {
                if (_xPos == battleShip.ShipSpan[i, 0] && _yPos == battleShip.ShipSpan[i, 1])
                {
                    _hitCounter++;
                    directHit = true;
                    if (_hitCounter == 5)
                    {                        
                        result = "SUNK!";
                        break;
                    }
                    else
                    {
                        result = "HIT!";
                        break;
                    }
                }
            }
            
            if (!directHit)
            {
                _missCounter++;
                result = "Miss!";
            }
            
            _resultCounter++;

            DisplayGameStats(directHit, result, battleShip);
        }
     
        public void DisplayGameStats(bool directHit, string result, Battleship battleShip)
        {
            string resultText = $"{_resultCounter}:  {result}\t(X={_xPos},\tY={_yPos})";
            _results[_resultCounter - 1] = resultText;
            _userShots[_resultCounter - 1, 0] = _xPos;
            _userShots[_resultCounter - 1, 1] = _yPos;

            var reactions = new RandomReaction();            

            Console.Clear();

            switch (result)
            {
                case "Miss!":
                    Console.WriteLine();
                    Console.WriteLine(reactions.ShotReactions[battleShip.GenerateRandomNumber() - 1]);
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

            string shotOrShots = _resultCounter > 1 ? "shots" : "shot";
            string hitOrHits = _hitCounter > 1 ? "hits" : "hit";
            string missOrMisses = _missCounter > 1 ? "misses" : "miss";

            Console.WriteLine($"{_resultCounter} {shotOrShots} fired");
            Console.WriteLine("-----------");
            Console.WriteLine($"\t-{_hitCounter} {hitOrHits}");
            Console.WriteLine($"\t-{_missCounter} {missOrMisses}");
        }

        public void EndGame(Battleship battleShip)
        {
            DisplayResults();
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

        public void DisplayResults()
        {            
            foreach (var result in _results)
            {
                if (result != null)
                {
                    if (result == _results[0])
                    {
                        Console.WriteLine("****************************************************");
                        Console.WriteLine("Your Previous Results:");
                        Console.WriteLine();
                    }

                    Console.WriteLine(result);                   
                }
            }
            if (_results[0] != null) Console.WriteLine();
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

        public static void ClearLastLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }

        public static void ClearLines(int lines)
        {
            for (int i = 1; i <= lines; i++)
            {
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop - (Console.WindowWidth >= Console.BufferWidth ? 1 : 0));
            }
        }
    }
}