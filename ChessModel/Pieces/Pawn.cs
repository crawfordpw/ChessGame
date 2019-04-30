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

        public void AddAtLocation(int row, int col)
        {
            RowID = row;
            ColID = col;
        }

        /*
         * The Pawn has 4 different moves it can make. A Pawn can only move forward, never backward.
         * It may move 1 space forward as long as that space is not occupied by any other piece. It can
         * move 2 spaces forward as long as it has never moved before. It can move diagonally forward 1
         * space as long as that space is an enemy. It asks the MoveValidator if it can En Passant.
         * Under these conditions it is a Valid Move and if the square it's moving from has a piece and it is not
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

            if(fromSquare.Piece == null || fromSquare.Piece == toSquare.Piece)
                return false;


            bool enPassant = MoveValidator.IsEnPassant(gb, fromSquare, toSquare);
            int sign = fromSquare.Piece.Color == ChessColor.White ? 1 : -1;

            if (MoveCount == 0)
            {
                if ((toRow == fromRow + (2 * sign) && fromCol == toCol && !isOccupied)
                    && (!MoveValidator.IsOccupied(gb.squares[fromRow + (1 * sign), fromCol])))
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
