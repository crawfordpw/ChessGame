namespace ChessModel.Pieces
{
    public class Queen : IPiece
    {
        public ChessPiece Type { get; set; }
        public ChessColor Color { get; set; }
        public int ColID { get; set; }
        public int RowID { get; set; }
        public int MoveCount { get; set; }

        public Queen()
        {

        }

        public Queen(ChessColor color)
        {
            Type = ChessPiece.Queen;
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
            bool rookValid = MoveValidator.RookMove(gameboard, fromSquare, toSquare);
            bool bishopValid = MoveValidator.BishopMove(gameboard, fromSquare, toSquare);

            if (rookValid || bishopValid)
                return true;

            return false;
        }
    }
}
