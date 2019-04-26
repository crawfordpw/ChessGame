namespace ChessModel
{
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

        public abstract bool Move(GameLogic gameLogic, Square fromSquare, Square toSquare);
    }
}
