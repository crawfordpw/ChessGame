namespace ChessModel
{
    /*
     * An abstract class for a Player. A player can either be Human or AI (not yet implemented).
     * Since a Human would move differently than an AI (or rather an AI would require addional
     * methods/classes, each would implement the Move method differently.
     */
    public abstract class Player
    {
        public ChessColor Color { get; set; }
        public PlayerClock Clock { get; set; }
        public int MoveCount { get; set; }

        public Player() : this(ChessColor.White, new PlayerClock())
        {
        }

        public Player(ChessColor color) : this(color, new PlayerClock())
        {
        }

        public Player(ChessColor color, PlayerClock clock)
        {
            this.Color = color;
            this.Clock = clock;
            this.MoveCount = 0;
        }

        public abstract bool Move(MoveLogic ml, Square fromSquare, Square toSquare);
    }
}
