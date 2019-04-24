namespace ChessModel
{
    class Human : Player
    {
        public override void Move(GameLogic gameLogic, Square fromSquare, Square toSquare)
        {
            gameLogic.MovePiece(fromSquare, toSquare);
        }
    }
}
