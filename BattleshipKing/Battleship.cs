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
        }

        public string ShipDirection { get; set; }
        public int ShipLength { get; set; }
        public int ShipSternX { get; set; }
        public int ShipSternY { get; set; }

        public string GenerateRandomDirection()
        {
            int dirNum = GenerateRandomNumber();
            if (dirNum > 4)
            {
                return "Horizontal";
            } else
            {
                return "Vertical";
            }
        }

        public int GenerateRandomNumber()
        {
            var random = new System.Random();
            int num = random.Next(1, 11);
            return num;
        }

    }
}
