namespace ChessModel.Pieces
{
    public class Bishop : IPiece
    {
        public ChessPiece Type { get; set; }
        public ChessColor Color { get; set; }
        public int ColID { get; set; }
        public int RowID { get; set; }
        public int MoveCount { get; set; }

        public Bishop() : this(ChessColor.White)
        {

        }

        public Bishop(ChessColor color)
        {
            Type = ChessPiece.Bishop;
            Color = (color == ChessColor.Black) ? ChessColor.Black : ChessColor.White;
            MoveCount = 0;
        }

        public void AddAtLocation(int row, int col)
        {
            RowID = row;
            ColID = col;
        }

        public bool IsValidMove(GameBoard gameboard, Square fromSquare, Square toSquare)
        {
            return MoveValidator.BishopMove(gameboard, fromSquare, toSquare);
        }
    }
}
