﻿using System.ComponentModel;
using System.Windows;
using ChessModel;

namespace UI
{
    public class SquareViewModel : INotifyPropertyChanged
    {
        public Square Square { get; set; }
        public IPiece Piece { get; set; }
        public string Coord { get; set; }
        public ChessColor SquareColor { get; set; }

        private Visibility _validMove;
        private ChessPiece _squarePiece;
        private ChessColor _pieceColor;
        private bool _isChecked;

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

        public Visibility ValidMove {
            get { return _validMove; }
            set {
                _validMove = value;
                OnPropertyChanged("ValidMove");
            }
        }

        public bool IsChecked {
            get { return _isChecked; }
            set {
                _isChecked = value;
                OnPropertyChanged("IsChecked");
            }
        }

        public SquareViewModel() : this(new Square())
        {

        }

        public SquareViewModel(Square sq)
        {
            this.Square = sq;
            this.Piece = sq.Piece;
            this.Coord = sq.Coord;
            this.SquareColor = sq.Color;
            this.SquarePiece = sq.Piece == null ? ChessPiece.None : sq.Piece.Type;
            this.PieceColor = sq.Piece == null ? ChessColor.Black : sq.Piece.Color;
            this.ValidMove = Visibility.Hidden;
        }

        public void Update(Square sq, Visibility valid)
        {
            PieceColor = sq.Piece == null ? ChessColor.Black : Square.Piece.Color;
            SquarePiece = sq.Piece == null ? ChessPiece.None : Square.Piece.Type;
            ValidMove = valid;
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
