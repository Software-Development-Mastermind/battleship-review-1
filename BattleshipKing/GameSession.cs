using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipKing
{
    class GameSession
    {
        public GameSession()
        {
            DirectHits = 0;
            ShotsFired = 0;
        }
        public int DirectHits { get; set; }
        public int ShotsFired { get; set; }
    }

 
}
