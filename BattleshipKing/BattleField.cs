using System;

namespace BattleshipKing
{
    class BattleField
    {
        public BattleField()
        {
            GetNumberOfAttacksFromUser();

            _attackCounter = 0;
            _hitCounter = 0;
            _missCounter = 0;
            _isBattleFieldRevealed = false;
            _isBattleOver = false;

            _attackResults = new string[_numberOfAttacks];
            _attacksLaunchedByUser = new int[_numberOfAttacks, 2];

            StartBattle();
        }

        private int _latitude;
        private int _longitude;
        private int _attackCounter;
        private int _hitCounter;
        private int _missCounter;
        private bool _isBattleFieldRevealed;
        private bool _isBattleOver;
        private int _numberOfAttacks;
        private readonly string[] _attackResults;
        private readonly int[,] _attacksLaunchedByUser;
        private readonly BattleShip _battleShip = new BattleShip();

        private void GetNumberOfAttacksFromUser()
        {
            bool valid = false;
            while (!valid)
            {
                Console.WriteLine("Enter Number of Attacks (1-20):");
                _numberOfAttacks = ConvertToInteger(Console.ReadLine());

                if (_numberOfAttacks > 0 && _numberOfAttacks <= 20)
                {
                    valid = true;
                }
                if (!valid) ClearLines(2);
            }
        }

        private void StartBattle()
        {
            _isBattleFieldRevealed = SetBattleFieldVisibility();
            Console.Clear();
            _battleShip.GenerateShipCoordinates(_isBattleFieldRevealed);

            for (int attack = 0; attack < _numberOfAttacks; attack++)
            {
                if (_isBattleFieldRevealed) ShowBattleField();

                GetBattleHeader(attack);
                GetAttackResultHistory();
                GetAttackCoordinates();

                WriteBorder();

                GetAttackResults(LaunchAttack());

                if (_attackCounter == _numberOfAttacks || _isBattleOver) EndBattle();

                WriteBorder();
                Write("");

                GetNextAttack();
            }
        }

        private bool SetBattleFieldVisibility()
        {
            Console.WriteLine("Reveal Battlefield?");
            return Console.ReadLine() == "y";
        }

        private void ShowBattleField()
        {
            for (int i = 1; i <= 10; i++)
            {
                for (int k = 1; k <= 10; k++)
                {
                    bool isShipCoordinate = false;
                    try
                    {
                        for (int latitude = 0; latitude <= _battleShip.ShipCoordinates.GetLength(0); latitude++)
                        {
                            if (_battleShip.ShipCoordinates[latitude, 0] == i)
                            {
                                if (_battleShip.ShipCoordinates[latitude, 1] == k)
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

        private void GetBattleHeader(int i)
        {
            WriteBorder();
            Write("\t -Welcome to 'Mean Battleship'-");
            Write("   -Where the computer will hurt your feelings-");
            WriteBorder();
            Write($"\t\tRound: {i + 1} of {_numberOfAttacks}");
        }

        private void GetAttackCoordinates()
        {
            do
            {
                GetLatitudeFromUser();
                GetLongitudeFromUser();

            } while (IsDuplicateAttack());
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

        private string LaunchAttack()
        {
            string attackResult = "";
            bool isDirectHit = false;

            for (int i = 0; i < _battleShip.ShipCoordinates.GetLength(0); i++)
            {
                if (_latitude == _battleShip.ShipCoordinates[i, 0] && _longitude == _battleShip.ShipCoordinates[i, 1])
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

            _attackCounter++;

            return attackResult;
        }

        private void GetAttackResults(string attackResult)
        {
            Console.Clear();

            _attackResults[_attackCounter - 1] = $"{_attackCounter}:  {attackResult}\t(Latitude: {_latitude},\tLongitude: {_longitude})";

            _attacksLaunchedByUser[_attackCounter - 1, 0] = _latitude;
            _attacksLaunchedByUser[_attackCounter - 1, 1] = _longitude;

            var reactions = new RandomReaction();

            switch (attackResult)
            {
                case "Miss!":
                    Write(reactions.ShotReactions[_battleShip.GenerateRandomNumber(11) - 1]);
                    break;
                case "HIT!":
                    Write("You've hit my Battleship!!!");
                    break;
                case "SUNK!":
                    Write("You've SUNK MY BATTLESHIP!!!");
                    _isBattleOver = true;
                    break;
            }

            string shotOrShots = _attackCounter > 1 ? "shots" : "shot";
            string hitOrHits = _hitCounter > 1 ? "hits" : "hit";
            string missOrMisses = _missCounter > 1 ? "misses" : "miss";

            Write($"{_attackCounter} {shotOrShots} fired");
            WriteBorder();
            Write($"\t-{_hitCounter} {hitOrHits}");
            Write($"\t-{_missCounter} {missOrMisses}");
        }

        private bool IsDuplicateAttack()
        {
            bool duplicateAttack = false;
            for (int i = 0; i < _attacksLaunchedByUser.GetLength(0); i++)
            {
                if (_latitude == _attacksLaunchedByUser[i, 0] && _longitude == _attacksLaunchedByUser[i, 1])
                {
                    duplicateAttack = true;
                    Write("Cannot fire at the same location twice\nPress Any Key To Continue...");
                    Console.ReadKey();
                    ClearLines(6);
                    break;
                }
            }
            return duplicateAttack;
        }

        private void GetAttackResultHistory()
        {
            foreach (var attackResult in _attackResults)
            {
                if (attackResult != null)
                {
                    if (attackResult == _attackResults[0])
                    {
                        WriteBorder();
                        Write("Attack Results:");
                    }
                    Console.WriteLine(attackResult);
                }
            }
            if (_attackResults[0] != null) Console.WriteLine();
            WriteBorder();
        }

        private void GetNextAttack()
        {
            Write("Press Any Key\nTo Continue To The Next Attack...");
            Console.ReadKey();
            Console.Clear();
        }

        private void EndBattle()
        {
            GetAttackResultHistory();
            ShowBattleField();
            Write("Game Over\nPress Any Key to Exit");
            Console.ReadKey();
            Environment.Exit(0);
        }
        
        private static int ConvertToInteger(string sourceValue)
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

        private static void WriteBorder()
        {
            Console.WriteLine("****************************************************");
        }
    }
}