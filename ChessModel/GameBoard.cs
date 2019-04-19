using System;
using System.Collections.Generic;
using System.Text;
using ChessModel.Pieces;

namespace ChessModel
{
    public class GameBoard
    {
        public int XDim { get; set; }
        public int YDim{ get; set; }
        public Square[,] squares;
        public List<IPiece> pieces;

        public GameBoard()
        {

        }

        public GameBoard(int x, int y)
        {
            XDim = x;
            YDim = y;
            squares = new Square[XDim, YDim];
            pieces = new List<IPiece>();
        }

        public void InitializeBoard()
        {
            ChessColor color;
            for(int row = 0; row < XDim; row++)
            {
                for(int col = 0; col < YDim; col++)
                {
                    squares[row, col] = new Square(row, col);
                    if (row == 0 || row == 1)
                    {
                        color = ChessColor.White;
                        squares[row, col].piece = AddPiece(row, col, color);
                    }
                    else if (row == XDim - 1 || row == XDim - 2)
                    {
                        color = ChessColor.Black;
                        squares[row, col].piece = AddPiece(row, col, color);
                    }
                }
            }
        }

        public IPiece AddPiece(int row, int col, ChessColor color)
        {
            IPiece piece;

            squares[row, col].HasPiece = true;

            // Adds piece to a List, 0/7 for non pawns, else it has to be a pawn
            if (row == 0 || row == XDim - 1)
            {
                if(col == 0 || col == YDim -1)
                    piece = new Rook(color);
                else if (col == 1 || col == YDim - 2)
                    piece = new Knight(color);
                else if (col == 2 || col == YDim - 3)
                    piece = new Bishop(color);
                else if (col == 3)
                    piece = new King(color);
                else
                    piece = new Queen(color);
            }
            else
            {
                piece = new Pawn(color);
            }
            piece.Alive = true;
            piece.PosCol = col;
            piece.PosRow = row;

            pieces.Add(piece);
            return piece;
        }
    }
}
