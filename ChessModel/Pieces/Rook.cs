using System;
using System.Collections.Generic;
using System.Text;

namespace ChessModel.Pieces
{
    public class Rook : IPiece
    {
        public ChessPiece Type { get; set; }
        public ChessColor Color { get; set; }
        public int PosCol { get; set; }
        public int PosRow { get; set; }
        public bool Alive { get; set; }

        public Rook()
        {

        }

        public Rook(ChessColor color)
        {
            Type = ChessPiece.Rook;
            Color = (color == ChessColor.Black) ? ChessColor.Black : ChessColor.White;
        }


        public void ValidMove()
        {
            throw new NotImplementedException();
        }
    }
}
