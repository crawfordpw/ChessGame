namespace ChessModel
{
    public abstract class Player
    {
        public ChessColor Color { get; set; }
        public int Timer { get; set; }
        public int MoveCount { get; set; }
        public bool Turn { get; set; }

        public Player() : this(ChessColor.White, 30)
        {
        }

        public Player(ChessColor color, int timer)
        {
            this.Color = color;
            this.Timer = timer;
        }

        public abstract void Move(GameLogic gameLogic, Square fromSquare, Square toSquare);
    }
}
