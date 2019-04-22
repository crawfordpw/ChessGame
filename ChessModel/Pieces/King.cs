namespace ChessModel.Pieces
{
    public class King : IPiece
    {
        public ChessPiece Type { get; set; }
        public ChessColor Color { get; set; }
        public int ColID { get; set; }
        public int RowID { get; set; }
        public int MoveCount { get; set; }

        public King()
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

        public bool IsValidMove(GameBoard gameboard, Square fromSquare, Square toSquare)
        {
            int fromRow = fromSquare.RowID;
            int fromCol = fromSquare.ColID;
            int toRow = toSquare.RowID;
            int toCol = toSquare.ColID;
            bool isOccupied = MoveValidator.IsOccupied(toSquare);
            bool isEnemy = MoveValidator.IsEnemy(fromSquare, toSquare);

            if (fromSquare.piece == null || fromSquare.piece == toSquare.piece)
                return false;

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
