using System.ComponentModel;
using ChessModel;

namespace UI
{
    public class SquareViewModel : INotifyPropertyChanged
    {
        public Square Square { get; set; }
        public IPiece Piece { get; set; }

        public string Cord { get; set; }
        public ChessColor SquareColor { get; set; }
        private ChessPiece _squarePiece;
        private ChessColor _pieceColor;

        public event PropertyChangedEventHandler PropertyChanged;

        public ChessPiece SquarePiece {
            get { return _squarePiece; }
            set {
                _squarePiece = value;
                OnPropertyChanged("SquarePiece");
            }
        }

        public ChessColor PieceColor {
            get { return _pieceColor; }
            set {
                _pieceColor = value;
                OnPropertyChanged("PieceColor");
            }
        }

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

        public void Update(Square sq)
        {
            PieceColor = sq.Piece == null ? ChessColor.Black : Square.Piece.Color;
            SquarePiece = sq.Piece == null ? ChessPiece.None : Square.Piece.Type;
        }

        // Create the OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
