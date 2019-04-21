using System;

namespace ChessModel.Pieces
{
    public class Bishop : IPiece
    {
        public ChessPiece Type { get; set; }
        public ChessColor Color { get; set; }
        public int ColID { get; set; }
        public int RowID { get; set; }

        public Bishop()
        {

        }

        public Bishop(ChessColor color)
        {
            Type = ChessPiece.Bishop;
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
            int rowDiff = toRow - fromRow;
            int colDiff = toCol - fromCol;

            int signRow = rowDiff > 0 ? 1 : -1;
            int signCol = colDiff > 0 ? 1 : -1;

            if (fromSquare.piece == null || Math.Abs(rowDiff) != Math.Abs(colDiff))
                return false;

            for (int i = 1; i < Math.Abs(rowDiff); i++)
            {
                for( int j = 1; j < Math.Abs(colDiff); j++)
                {
                    if (!MoveValidator.IsOccupied(gameboard.squares[fromRow + (i * signRow), fromCol + (i * signCol)]))
                        continue;
                    else
                        return false;
                }
            }

            // target space can either be empty or contain an enemy piece
            if (!isOccupied || isEnemy)
                return true;

            return false;
        }
    }
}
