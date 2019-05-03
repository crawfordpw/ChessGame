namespace ChessModel
{
    /*
     * A Human Player only needs to provide a square to move from and another square
     * to move to. The neccessary logic for moving is handled in the MoveLogic class
     */
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
            MoveCount += 1;
            return ml.MovePiece(fromSquare, toSquare);
        }
    }
}
