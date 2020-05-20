namespace DN.Puzzle.Color
{
    public partial class Player
    {
        public enum State
        { 
            Idle = 0,
            Navigating = 1,
            Moving = 2,
            Waiting = 3,
            Stuck = 4,
            Done = 5,
        }
    }
}