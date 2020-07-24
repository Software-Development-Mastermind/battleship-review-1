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
            ShipSternX = GenerateRandomNumber();
            ShipSternY = GenerateRandomNumber();
            ShipSpan = new int[5, 2];
        }

        public string ShipDirection { get; set; }
        public int ShipLength { get; set; }
        public int ShipSternX { get; set; }
        public int ShipSternY { get; set; }
        public int[,] ShipSpan { get; set; }

        public string GenerateRandomDirection()
        {
            if (GenerateRandomNumber() > 5)
            {
                return "EastWest";
            } else
            {
                return "NorthSouth";
            }
        }

        public int GenerateRandomNumber()
        {
            var random = new System.Random();
            int num = random.Next(1, 11);
            return num;
        }

        public void FillPositionArray()
        {
            //ShipSpan = new int[5, 2];

            if (ShipSternX > 5) ShipSternX = 3;
            if (ShipSternY > 5) ShipSternY = 3;

            for (int i = 0; i <= 4 ; i++)
            {
                ShipSpan[i, 0] = ShipDirection == "EastWest" ? ShipSternX : ShipSternX + i;
                ShipSpan[i, 1] = ShipDirection == "NorthSouth" ? ShipSternY : ShipSternY + i;
                Console.WriteLine($"{ShipSpan[i, 0]}, {ShipSpan[i, 1]}");
            }
        }

    }
}
