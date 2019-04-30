namespace ChessModel.Pieces
{
    public class King : IPiece
    {
        public ChessPiece Type { get; set; }
        public ChessColor Color { get; set; }
        public int ColID { get; set; }
        public int RowID { get; set; }
        public int MoveCount { get; set; }

        public King() : this(ChessColor.White)
        {

        }

        public King(ChessColor color)
        {
            Type = ChessPiece.King;
            Color = (color == ChessColor.Black) ? ChessColor.Black : ChessColor.White;
            MoveCount = 0;
        }

        public void AddAtLocation(int row, int col)
        {
            RowID = row;
            ColID = col;
        }

        /*
         * The King can move in any direction by 1 space, unless it is Castling. 
         * The King asks the Move Validator if it is allowed to Castle. 
         * It is only a valid move if it can Castle, or if the square it's moving from has a piece and it is not
         * moving to itself. Also need to check if the space it's moving to is not occupied by it's own colored pieces.
         */
        public bool IsValidMove(GameBoard gb, Square fromSquare, Square toSquare)
        {
            int fromRow = fromSquare.RowID;
            int fromCol = fromSquare.ColID;
            int toRow = toSquare.RowID;
            int toCol = toSquare.ColID;
            bool isOccupied = MoveValidator.IsOccupied(toSquare);
            bool isEnemy = MoveValidator.IsEnemy(fromSquare, toSquare);           

            if (fromSquare.Piece == null || fromSquare.Piece == toSquare.Piece)
                return false;

            bool isCastle = MoveValidator.IsCastle(gb, fromSquare, toSquare);
            if (isCastle)
                return true;

            if (((toRow == fromRow + 1 && toCol == fromCol) || (toCol == fromCol + 1 && toRow == fromRow)
                || (toRow == fromRow - 1 && toCol == fromCol) || (toCol == fromCol - 1 && toRow == fromRow)
                || (toRow == fromRow + 1 && toCol == fromCol + 1) || (toRow == fromRow - 1 && toCol == fromCol - 1)
                || (toRow == fromRow + 1 && toCol == fromCol - 1) || (toRow == fromRow - 1 && toCol == fromCol + 1))
                && (!isOccupied || isEnemy))
                return true;

            return false;
        }
    }
}
