using System;
using System.Runtime.InteropServices.ComTypes;

namespace BattleshipKing
{
    class Program
    {        
        private int _xPosition;
        private int _yPosition;
        private int _resultCounter = 0;
        private int _hitCounter = 0;
        private int _missCounter = 0;
        private bool _isShipRevealed = false;
        private readonly int _numberOfRounds = 8;
        private readonly string[] _resultsToDisplay = new string[8];
        private readonly int[,] _shotsFiredByUser = new int[8, 2];

        public static void Main()
        {
            var Program = new Program();
            Program.StartGame();
        }

        public bool SetShipVisibility()
        {
            Console.WriteLine("Reveal Battlefield?");
            return Console.ReadLine() == "y";
        }

        public void StartGame()
        {
            var battleShip = new Battleship();
            _isShipRevealed = SetShipVisibility();
            battleShip.GenerateShipCoordinates(_isShipRevealed);

            for (int attackRound = 0; attackRound < _numberOfRounds ; attackRound++)
            {
                if (_isShipRevealed) ShowBattlefield(battleShip);

                DisplayHeader(attackRound);
                ShowAttackHistory();
                GetAttackCoordinates();

                Console.WriteLine("****************************************************");
                LaunchAttack(battleShip);
                Console.WriteLine("****************************************************");
                Console.WriteLine();

                PauseGame();
            }
        }

        public void PauseGame()
        {
            Console.WriteLine("Press Any Key\nTo Continue To The Next Attack Round...");
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

        public void GetAttackCoordinates()
        {
            do
            {
                GetUserX();
                GetUserY();

            } while (IsDuplicate());
        }

        public bool IsDuplicate()
        {
            bool duplicate = false;
            for (int i = 0; i < _shotsFiredByUser.GetLength(0); i++)
            {
                if (_xPosition == _shotsFiredByUser[i, 0] && _yPosition == _shotsFiredByUser[i, 1])
                {
                    duplicate = true;
                    Console.WriteLine("Cannot fire at the same location twice");
                    Console.WriteLine("Press Any Key To Continue...");
                    Console.ReadKey();
                    ClearLines(6);
                    break;
                }
            }
            return duplicate;
        }

        public void GetUserX()
        {
            bool valid = false;
            while (!valid)
            {
                Console.WriteLine("Enter Value for X (1-10):");
                _xPosition = ConvertToInteger(Console.ReadLine());
                valid = _xPosition > 0;
                if (!valid) ClearLines(2);
            }
        }

        public void GetUserY()
        {
            bool valid = false;
            while (!valid)
            {
                Console.WriteLine("Enter Value for Y (1-10):");
                _yPosition = ConvertToInteger(Console.ReadLine());
                valid = _yPosition > 0;
                if (!valid) ClearLines(2);
            }
        }

        public void LaunchAttack(Battleship battleShip)
        {
            string attackResult = "";
            bool isDirectHit = false;

            for (int i = 0; i < battleShip.ShipCoordinates.GetLength(0); i++)
            {
                if (_xPosition == battleShip.ShipCoordinates[i, 0] && _yPosition == battleShip.ShipCoordinates[i, 1])
                {
                    _hitCounter++;
                    isDirectHit = true;
                    if (_hitCounter == 5)
                    {
                        attackResult = "SUNK!";
                        break;
                    }
                    else
                    {
                        attackResult = "HIT!";
                        break;
                    }
                }
            }

            if (!isDirectHit)
            {
                _missCounter++;
                attackResult = "Miss!";
            }

            _resultCounter++;

            ShowAttackResults(isDirectHit, attackResult, battleShip);
        }

        public void ShowAttackResults(bool directHit, string result, Battleship battleShip)
        {
            Console.Clear();

            string resultText = $"{_resultCounter}:  {result}\t(X= {_xPosition},\tY= {_yPosition})";
            _resultsToDisplay[_resultCounter - 1] = resultText;

            _shotsFiredByUser[_resultCounter - 1, 0] = _xPosition;
            _shotsFiredByUser[_resultCounter - 1, 1] = _yPosition;

            var reactions = new RandomReaction();

            switch (result)
            {
                case "Miss!":
                    Console.WriteLine();
                    Console.WriteLine(reactions.ShotReactions[battleShip.GenerateRandomNumber(11) - 1]);
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

            if (_resultCounter == 8) EndGame(battleShip);
        }

        public void EndGame(Battleship battleShip)
        {
            ShowAttackHistory();
            Console.WriteLine("Game Over");
            Console.WriteLine("Press Any Key to Exit");
            ShowBattlefield(battleShip);
            Console.ReadKey();
            Environment.Exit(0);
        }

        public void ShowBattlefield(Battleship battleShip)
        {
            for (int i = 1; i <= 10; i++)
            {
                for (int k = 1; k <= 10; k++)
                {
                    bool isShipCoordinate = false;
                    try
                    {
                        for (int xValue = 0; xValue <= battleShip.ShipCoordinates.GetLength(0); xValue++)
                        {
                            if (battleShip.ShipCoordinates[xValue, 0] == i)
                            {
                                if (battleShip.ShipCoordinates[xValue, 1] == k)
                                {
                                    isShipCoordinate = true;
                                    Console.Write("O ");
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                        if (!isShipCoordinate) Console.Write("- ");
                    }
                }
                Console.WriteLine();
            }
        }

        public void ShowAttackHistory()
        {
            foreach (var result in _resultsToDisplay)
            {
                if (result != null)
                {
                    if (result == _resultsToDisplay[0])
                    {
                        Console.WriteLine("****************************************************");
                        Console.WriteLine("Attack Results:");
                        Console.WriteLine();
                    }
                    Console.WriteLine(result);                }
            }
            if (_resultsToDisplay[0] != null) Console.WriteLine();
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
