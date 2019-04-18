using System;
using System.Collections.Generic;
using System.Text;
using ChessModel.Pieces;

namespace ChessModel
{
    public class Square
    {
        public int ColID { get; set; }
        public int RowID { get; set; }
        public bool HasPiece { get; set; }
        public ChessPiece Type { get; set; }

        public Square()
        {

        }

        public Square(int row, int col)
        {
            Type = ChessPiece.None;
            HasPiece = false;
            RowID = row;
            ColID = col;
        }
    }
}
