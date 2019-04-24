﻿namespace ChessModel.Pieces
{
    public class Knight : IPiece
    {
        public ChessPiece Type { get; set; }
        public ChessColor Color { get; set; }
        public int ColID { get; set; }
        public int RowID { get; set; }
        public int MoveCount { get; set; }

        public Knight() : this(ChessColor.White)
        {

        }

        public Knight(ChessColor color)
        {
            Type = ChessPiece.Knight;
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

            if (fromSquare.Piece == null || fromSquare.Piece == toSquare.Piece)
                return false;

            if (toRow == fromRow + 1 && fromCol == toCol + 2 && (!isOccupied || isEnemy))
                return true;
            else if (toRow == fromRow + 1 && fromCol == toCol - 2 && (!isOccupied || isEnemy))
                return true;
            else if (toRow == fromRow - 1 && fromCol == toCol + 2 && (!isOccupied || isEnemy))
                return true;
            else if (toRow == fromRow - 1 && fromCol == toCol - 2 && (!isOccupied || isEnemy))
                return true;
            else if (toRow == fromRow + 2 && fromCol == toCol + 1 && (!isOccupied || isEnemy))
                return true;
            else if (toRow == fromRow - 2 && fromCol == toCol + 1 && (!isOccupied || isEnemy))
                return true;
            else if (toRow == fromRow + 2 && fromCol == toCol - 1 && (!isOccupied || isEnemy))
                return true;
            else if (toRow == fromRow - 2 && fromCol == toCol - 1 && (!isOccupied || isEnemy))
                return true;
            else
                return false;
        }
    }
}
