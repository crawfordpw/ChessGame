using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessModel;

namespace UI
{
    public class SquareViewModel
    {
        public Square Square { get; set; }
        public IPiece Piece { get; set; }
        public string Cord { get; set; }
        public ChessColor SquareColor { get; set; }
        public ChessPiece SquarePiece { get; set; }
        public ChessColor PieceColor { get; set; }

        public SquareViewModel() : this(new Square())
        {

        }

        public SquareViewModel(Square sq)
        {
            this.Square = sq;
            this.Piece = sq.Piece;
            this.Cord = sq.Cord;
            this.SquareColor = sq.Color;
            this.SquarePiece = sq.Piece == null ? ChessPiece.None : sq.Piece.Type;
            this.PieceColor = sq.Piece == null ? ChessColor.Black : sq.Piece.Color;
        }
    }
}
