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
            Console.WriteLine($"Ship Direction: {ShipDirection}");
            ShipLength = 5;
            ShipSternX = GenerateRandomNumber();
            ShipSternY = GenerateRandomNumber();
            FillPositionArray();
        }

        public string ShipDirection { get; set; }
        public int ShipLength { get; set; }
        public int ShipSternX { get; set; }
        public int ShipSternY { get; set; }
        public int[,] ShipSpan { get; set; }

        public string GenerateRandomDirection()
        {
            int dirNum = GenerateRandomNumber();
            // comparing less than, and greater than 5
            // to be able to reuse the GenerateRandonNumber() method
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

        public void FillPositionArray()
        {
            ShipSpan = new int[2, 5];
            switch(ShipDirection)
            {
                case "Horizontal":
                    // Spread array by incrementing xPos value
                    // if ShipSternX is greater than 5, change direction (or subtract)
                    // Because the ship will be 5 in length and cannot extend past
                    // the grid boundaries.
                    if (ShipSternX <= 5)
                    {
                        // Fill array
                        for (int i = 0; i <= 1; i++)
                        {
                            ShipSpan[i, 0] = ShipSternX;

                            for (int k = 0; k <= 3; k++)
                            {
                                ShipSpan[i, k + 1] = ShipSternY + k;
                            }
                        }
                    }
                    else
                    {
                        // Change directions, or just manually assign 5
                        Console.WriteLine("Horizontal, ShipsternX > 5");
                    }

                    break;

                case "Vertical":
                    // Spread array by incrementing yPos value
                    // If ShipSternY is greater than 5, change direction (or subtract)
                    // Because the ship will be 5 in length and cannot extend past
                    // the grid boundaries.
                    if (ShipSternY <= 5)
                    {
                        // Fill array
                        Console.WriteLine("Vertical, ShipsternY <= 5");
                        //for (int i = 0; i < 5; i++)
                        //{
                        //    for (int k = 0; k < 3; k++)
                        //    {
                        //        ShipSpan[i, k] = ShipSternX + k;
                        //        ShipSpan[i, k + 1] = ShipSternY;
                        //        Console.WriteLine(ShipSpan);
                        //    }
                        //}
                    }
                    else
                    {
                        // Change directions, or just manually assign 5
                        Console.WriteLine("Vertical, ShipsternY > 5");
                    }

                    break;

            }

            

        }

    }
}
