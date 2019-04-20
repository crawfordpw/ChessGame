namespace ChessModel.Pieces 
{
    public class Pawn : IPiece
    {
        public ChessPiece Type { get; set; }
        public ChessColor Color { get; set; }
        public int ColID { get; set; }
        public int RowID { get; set; }

        public Pawn()
        {

        }

        public Pawn(ChessColor color)
        {
            Type = ChessPiece.Pawn;
            Color = (color == ChessColor.Black) ? ChessColor.Black : ChessColor.White;
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

            if(fromSquare.piece == null)
                return false;

            if (fromSquare.piece.Color == ChessColor.White)
            {
                if (toRow == fromRow + 1 && fromCol == toCol && !isOccupied)
                    return true;
                else if (toRow == fromRow + 1 && toCol == fromCol + 1 && isEnemy)
                    return true;
                else if (toRow == fromRow + 1 && toCol == fromCol - 1 && isEnemy)
                    return true;
                else
                    return false;
            }
            else
            {
                if (toRow == fromRow - 1 && fromCol == toCol && !isOccupied)
                    return true;
                else if (toRow == fromRow - 1 && toCol == fromCol + 1 && isEnemy)
                    return true;
                else if (toRow == fromRow - 1 && toCol == fromCol - 1 && isEnemy)
                    return true;
                else
                    return false;
            }
        }
    }
}
