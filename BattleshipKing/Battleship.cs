using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipKing
{
    class Battleship
    {
        public Battleship()
        {
            ShipDirection = GenerateRandomDirection();
            ShipLength = 5;
            ShipSternX = GenerateRandomNumber(_ = ShipDirection == "EastWest" ? 6 : 11);
            ShipSternY = GenerateRandomNumber(_ = ShipDirection == "NorthSouth" ? 6 : 11);
            ShipCoordinates = new int[5, 2];
        }

        public string ShipDirection { get; set; }
        public int ShipLength { get; set; }
        public int ShipSternX { get; set; }
        public int ShipSternY { get; set; }
        public int[,] ShipCoordinates { get; set; }

        public string GenerateRandomDirection()
        {
            if (GenerateRandomNumber(11) > 5)
            {
                return "EastWest";
            } else
            {
                return "NorthSouth";
            }
        }

        public int GenerateRandomNumber(int maxValue)
        {
            var random = new Random();
            int num = maxValue > 0 && maxValue < 11
                ? random.Next(1, maxValue)
                : 1;
            return num;
        }

        public void GenerateShipCoordinates(bool isRevealed)
        {
            for (int i = 0; i <= ShipLength - 1 ; i++)
            {
                ShipCoordinates[i, 0] = ShipDirection == "EastWest" ? ShipSternX : ShipSternX + i;
                ShipCoordinates[i, 1] = ShipDirection == "NorthSouth" ? ShipSternY : ShipSternY + i;
                if (isRevealed) Console.WriteLine($"{ShipCoordinates[i, 0]}, {ShipCoordinates[i, 1]}");                
            }
        }

    }
}
