using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipKing
{
    class Battleship
    {
        public Battleship()
        {

        }

        public string BattleshipDirection { get; set; }
        public int BattleshipLength { get; set; }
        public Array FirstSegmentPosition { get; set; }
        public int NumberOfHits { get; set; }

    }
}
