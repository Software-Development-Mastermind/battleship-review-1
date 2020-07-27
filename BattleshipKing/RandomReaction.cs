namespace BattleshipKing
{
    class RandomReaction
    {
        public RandomReaction()
        {
            FillReactions();
        }
        public string[] ShotReactions = new string[10];


        public void FillReactions()
        {
            ShotReactions[0] = "You missed, try again.";
            ShotReactions[1] = "Missed... again.  You're not very good at this.";
            ShotReactions[2] = "Are you intentionally trying to miss?";
            ShotReactions[3] = "My two year old nephew gets it right more often than you.";
            ShotReactions[4] = "You miss so often, are you sure you want to keep playing?";
            ShotReactions[5] = "Missed, sheesh.";
            ShotReactions[6] = "You should probably practice a little before playing again.";
            ShotReactions[7] = "Why do you keep missing?";
            ShotReactions[8] = "Do you even know how to play this game?";
            ShotReactions[9] = "Miss.";

        }
    }

 
}
