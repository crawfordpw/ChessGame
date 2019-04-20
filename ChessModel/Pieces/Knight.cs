﻿namespace ChessModel.Pieces
{
    public class Knight : IPiece
    {
        public ChessPiece Type { get; set; }
        public ChessColor Color { get; set; }
        public int ColID { get; set; }
        public int RowID { get; set; }

        public Knight()
        {

        }

        public Knight(ChessColor color)
        {
            Type = ChessPiece.Knight;
            Color = (color == ChessColor.Black) ? ChessColor.Black : ChessColor.White;
        }

        public void AddAtLocation(int row, int col)
        {
            RowID = row;
            ColID = col;
        }

        public bool IsValidMove(GameBoard gameboard)
        {
            return false;
        }
    }
}
