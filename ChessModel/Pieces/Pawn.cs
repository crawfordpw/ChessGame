namespace ChessModel.Pieces 
{
    public class Pawn : IPiece
    {
        public ChessPiece Type { get; set; }
        public ChessColor Color { get; set; }
        public int ColID { get; set; }
        public int RowID { get; set; }
        public int MoveCount { get; set; }

        public Pawn() : this(ChessColor.White)
        {

        }

        public Pawn(ChessColor color)
        {
            Type = ChessPiece.Pawn;
            Color = (color == ChessColor.Black) ? ChessColor.Black : ChessColor.White;
            MoveCount = 0;
        }

        public Pawn(int row, int col)
        {
            Type = ChessPiece.Pawn;
            Color = ChessColor.White;
            MoveCount = 0;
            ColID = col;
            RowID = row;
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

            if(fromSquare.Piece == null || fromSquare.Piece == toSquare.Piece)
                return false;


            bool enPassant = MoveValidator.IsEnPassant(fromSquare, toSquare);
            int sign = fromSquare.Piece.Color == ChessColor.White ? 1 : -1;

            if (MoveCount == 0)
            {
                if ((toRow == fromRow + (2 * sign) && fromCol == toCol && !isOccupied)
                    && (!MoveValidator.IsOccupied(gameboard.squares[fromRow + (1 * sign), fromCol])))
                {
                    return true;
                }
            }

            if (toRow == fromRow + (1 * sign) && fromCol == toCol && !isOccupied)
                return true;
            else if (toRow == fromRow + (1 * sign) && toCol == fromCol + 1 && (isEnemy || enPassant))
                return true;
            else if (toRow == fromRow + (1 * sign) && toCol == fromCol - 1 && (isEnemy || enPassant))
                return true;
            else
                return false;
        }
    }
}
