﻿namespace ChessModel.Pieces
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

            return false;
        }
    }
}
