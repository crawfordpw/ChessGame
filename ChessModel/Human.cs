namespace ChessModel
{
    public class Human : Player
    {
        public Human() : base()
        {

        }

        public Human(ChessColor color, PlayerClock clock) : base(color, clock)
        {
        }

        public override void Move(GameLogic gameLogic, Square fromSquare, Square toSquare)
        {
            gameLogic.MovePiece(fromSquare, toSquare);
        }
    }
}
