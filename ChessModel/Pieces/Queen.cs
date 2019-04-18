using System;
using System.Collections.Generic;
using System.Text;

namespace ChessModel.Pieces
{
    public class Queen : IPiece
    {
        public ChessPiece Type { get; set; }
        public ChessColor Color { get; set; }
        public int PosCol { get; set; }
        public int PosRow { get; set; }
        public bool Alive { get; set; }

        public Queen()
        {

        }

        public Queen(ChessColor color)
        {
            Type = ChessPiece.Queen;
            Color = (color == ChessColor.Black) ? ChessColor.Black : ChessColor.White;
        }


        public void ValidMove()
        {
            throw new NotImplementedException();
        }
    }
}
