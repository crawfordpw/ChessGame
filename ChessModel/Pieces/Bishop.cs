using System;
using System.Collections.Generic;
using System.Text;

namespace ChessModel.Pieces
{
    public class Bishop : IPiece
    {
        public ChessPiece Type { get; set; }
        public ChessColor Color { get; set; }
        public int PosCol { get; set; }
        public int PosRow { get; set; }
        public bool Alive { get; set; }

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
            PosRow = row;
            PosCol = col;
        }
    }
}
