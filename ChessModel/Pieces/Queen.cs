namespace ChessModel.Pieces
{
    public class Queen : IPiece
    {
        public ChessPiece Type { get; set; }
        public ChessColor Color { get; set; }
        public int ColID { get; set; }
        public int RowID { get; set; }
        public int MoveCount { get; set; }

        public Queen() : this(ChessColor.White)
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

        /*
         * Since a Queen can move like either a Rook or a Bishop, reuses their ValidMove
         * code in the MoveValidator
         */
        public bool IsValidMove(GameBoard gb, Square fromSquare, Square toSquare)
        {
            bool rookValid = MoveValidator.RookMove(gb, fromSquare, toSquare);
            bool bishopValid = MoveValidator.BishopMove(gb, fromSquare, toSquare);

            if (rookValid || bishopValid)
                return true;

            return false;
        }
    }
}
