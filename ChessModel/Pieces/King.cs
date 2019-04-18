using System;
using System.Collections.Generic;
using System.Text;

namespace ChessModel.Pieces
{
    public class King : IPiece
    {
        public ChessPiece Type { get; set; }
        public ChessColor Color { get; set; }
        public int PosCol { get; set; }
        public int PosRow { get; set; }
        public bool Alive { get; set; }

        public King()
        {

        }

        public King(ChessColor color)
        {
            Type = ChessPiece.King;
            Color = (color == ChessColor.Black) ? ChessColor.Black : ChessColor.White;
        }
    }
}
