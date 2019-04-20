﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ChessModel.Pieces 
{
    public class Pawn : IPiece
    {
        public ChessPiece Type { get; set; }
        public ChessColor Color { get; set; }
        public int PosCol { get; set; }
        public int PosRow { get; set; }

        public Pawn()
        {

        }

        public Pawn(ChessColor color)
        {
            Type = ChessPiece.Pawn;
            Color = (color == ChessColor.Black) ? ChessColor.Black : ChessColor.White;
        }

        public void AddAtLocation(int row, int col)
        {
            PosRow = row;
            PosCol = col;
        }
    }
}
