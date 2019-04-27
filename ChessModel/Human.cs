namespace ChessModel
{
    public class Human : Player
    {
        public Human() : base()
        {

        }
        public Human(ChessColor color) : base(color)
        {
        }

        public Human(ChessColor color, PlayerClock clock) : base(color, clock)
        {
        }

        public override bool Move(MoveLogic ml, Square fromSquare, Square toSquare)
        {
            return ml.MovePiece(fromSquare, toSquare);
        }
    }
}
