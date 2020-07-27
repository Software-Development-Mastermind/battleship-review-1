using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace BattleshipKing
{
    class BattleField
    {
        public BattleField()
        {
            _resultCounter = 0;
            _hitCounter = 0;
            _missCounter = 0;
            _isShipRevealed = false;
            _isGameOver = false;
            _numberOfRounds = 8;
            _resultsToDisplay = new string[_numberOfRounds];
            _shotsFiredByUser = new int[_numberOfRounds, 2];

            StartGame();
        }

        private int _latitude;
        private int _longitude;
        private int _resultCounter;
        private int _hitCounter;
        private int _missCounter;
        private bool _isShipRevealed;
        private bool _isGameOver;
        private readonly int _numberOfRounds;
        private readonly string[] _resultsToDisplay;
        private readonly int[,] _shotsFiredByUser;

        public BattleShip BattleShip { get; } = new BattleShip();
        public int Latitude { get; set; }
        public int Longitude { get; set; }
        public int ResultCounter { get; set; }
        public int HitCounter { get; set; }
        public int MissCounter { get; set; }
        public bool IsShipRevealed { get; set; }
        public bool IsGameOver { get; set; }
        public int NumberOfRounds { get; }
        public string[] ResultsToDisplay { get; }
        public int[] ShotsFiredByUser { get; }

        private bool SetShipVisibility()
        {
            Console.WriteLine("Reveal Battlefield?");
            return Console.ReadLine() == "y";
        }

        private void StartGame()
        {
            _isShipRevealed = SetShipVisibility();
            Console.Clear();
            BattleShip.GenerateShipCoordinates(_isShipRevealed);

            for (int attackRound = 0; attackRound < _numberOfRounds; attackRound++)
            {
                if (_isShipRevealed) ShowBattlefield();

                GetHeader(attackRound);
                GetAttackResultHistory();
                GetAttackCoordinates();

                Console.WriteLine("****************************************************");
                LaunchAttack();
                Console.WriteLine("****************************************************");
                Console.WriteLine();

                GetNextRound();
            }
        }

        private void GetNextRound()
        {
            Console.WriteLine("Press Any Key\nTo Continue To The Next Attack Round...");
            Console.WriteLine();
            Console.ReadKey();
            Console.Clear();
        }

        private void GetHeader(int i)
        {
            Console.WriteLine("****************************************************");
            Console.WriteLine("\t -Welcome to Mean Battleship King-");
            Console.WriteLine("   -Where the computer will hurt your feelings-");
            Console.WriteLine("****************************************************");
            Console.WriteLine();
            Console.WriteLine($"\t\tRound: {i + 1} of 8");
            Console.WriteLine();
        }

        private void GetAttackCoordinates()
        {
            do
            {
                GetLatitudeFromUser();
                GetLongitudeFromUser();

            } while (IsDuplicateAttack());
        }

        private bool IsDuplicateAttack()
        {
            bool duplicate = false;
            for (int i = 0; i < _shotsFiredByUser.GetLength(0); i++)
            {
                if (_latitude == _shotsFiredByUser[i, 0] && _longitude == _shotsFiredByUser[i, 1])
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

        private void GetLatitudeFromUser()
        {
            bool valid = false;
            while (!valid)
            {
                Console.WriteLine("Enter Value for X (1-10):");
                _latitude = ConvertToInteger(Console.ReadLine());
                valid = _latitude > 0;
                if (!valid) ClearLines(2);
            }
        }

        private void GetLongitudeFromUser()
        {
            bool valid = false;
            while (!valid)
            {
                Console.WriteLine("Enter Value for Y (1-10):");
                _longitude = ConvertToInteger(Console.ReadLine());
                valid = _longitude > 0;
                if (!valid) ClearLines(2);
            }
        }

        private void EndGame()
        {
            GetAttackResultHistory();
            ShowBattlefield();
            Console.WriteLine("\t\tGame Over");
            Console.WriteLine("\tPress Any Key to Exit");
            Console.ReadKey();
            Environment.Exit(0);
        }

        private void ShowBattlefield()
        {
            for (int i = 1; i <= 10; i++)
            {
                for (int k = 1; k <= 10; k++)
                {
                    bool isShipCoordinate = false;
                    try
                    {
                        for (int latitude = 0; latitude <= BattleShip.ShipCoordinates.GetLength(0); latitude++)
                        {
                            if (BattleShip.ShipCoordinates[latitude, 0] == i)
                            {
                                if (BattleShip.ShipCoordinates[latitude, 1] == k)
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

        private void LaunchAttack()
        {
            string attackResult = "";
            bool isDirectHit = false;

            for (int i = 0; i < BattleShip.ShipCoordinates.GetLength(0); i++)
            {
                if (_latitude == BattleShip.ShipCoordinates[i, 0] && _longitude == BattleShip.ShipCoordinates[i, 1])
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

            GetAttackResults(isDirectHit, attackResult);
        }

        private void GetAttackResults(bool isDirectHit, string attackResult)
        {
            Console.Clear();

            string resultText = $"{_resultCounter}:  {attackResult}\t(Latitude: {_latitude},\tLongitude: {_longitude})";
            _resultsToDisplay[_resultCounter - 1] = resultText;

            _shotsFiredByUser[_resultCounter - 1, 0] = _latitude;
            _shotsFiredByUser[_resultCounter - 1, 1] = _longitude;

            var reactions = new RandomReaction();

            switch (attackResult)
            {
                case "Miss!":
                    Write(reactions.ShotReactions[BattleShip.GenerateRandomNumber(11) - 1]);
                    break;
                case "HIT!":
                    Write("You've hit my Battleship!!!");
                    break;
                case "SUNK!":
                    Write("You've SUNK MY BATTLESHIP!!!");
                    _isGameOver = true;
                    break;
            }

            if (isDirectHit) Write("Nice Shot!");

            string shotOrShots = _resultCounter > 1 ? "shots" : "shot";
            string hitOrHits = _hitCounter > 1 ? "hits" : "hit";
            string missOrMisses = _missCounter > 1 ? "misses" : "miss";

            Write($"{_resultCounter} {shotOrShots} fired");
            Write("-----------");
            Write($"\t-{_hitCounter} {hitOrHits}");
            Write($"\t-{_missCounter} {missOrMisses}");

            if (_resultCounter == 8 || _isGameOver) EndGame();
        }
        private void GetAttackResultHistory()
        {
            foreach (var attackResult in _resultsToDisplay)
            {
                if (attackResult != null)
                {
                    if (attackResult == _resultsToDisplay[0])
                    {
                        Console.WriteLine("****************************************************");
                        Console.WriteLine("Attack Results:");
                        Console.WriteLine();
                    }
                    Console.WriteLine(attackResult);
                }
            }
            if (_resultsToDisplay[0] != null) Console.WriteLine();
            Console.WriteLine("****************************************************");
        }
        private int ConvertToInteger(string sourceValue)
        {
            try
            {
                return int.Parse(sourceValue);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        private static void ClearLines(int linesToDelete)
        {
            for (int i = 1; i <= linesToDelete; i++)
            {
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(new string(' ', Console.BufferWidth));
                Console.SetCursorPosition(0, Console.CursorTop - (Console.WindowWidth >= Console.BufferWidth ? 1 : 0));
            }
        }

        private static void Write(string textToDisplay)
        {
            Console.WriteLine();
            Console.WriteLine(textToDisplay);
            Console.WriteLine();
        }
    }
}