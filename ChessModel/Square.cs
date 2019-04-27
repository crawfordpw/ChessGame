﻿namespace ChessModel
{
    public class Square
    {
        public int ColID { get; set; }
        public int RowID { get; set; }
        public ChessColor Color { get; set; }
        public IPiece Piece { get; set; }
        public string Cord { get; set; }

        public Square() : this(0, 0)
        {

        }

        public Square(int row, int col)
        {
            RowID = row;
            ColID = col;
            Cord = $"{row}{col}";
        }

        public Square(int row, int col, IPiece piece)
        {
            RowID = row;
            ColID = col;
            Cord = $"{row}{col}";
            Piece = piece;
        }

        public void MakeSameCord()
        {
            Piece.ColID = ColID;
            Piece.RowID = RowID;
        }
    }
}
