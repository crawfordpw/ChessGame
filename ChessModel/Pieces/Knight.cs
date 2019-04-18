using System;
using System.Collections.Generic;
using System.Text;

namespace ChessModel.Pieces
{
    public class Knight : IPiece
    {
        public ChessPiece Type { get; set; }
        public ChessColor Color { get; set; }
        public int PosCol { get; set; }
        public int PosRow { get; set; }
        public bool Alive { get; set; }

        public Knight()
        {

        }

        public Knight(ChessColor color)
        {
            Type = ChessPiece.Knight;
            Color = (color == ChessColor.Black) ? ChessColor.Black : ChessColor.White;
        }
    }
}
