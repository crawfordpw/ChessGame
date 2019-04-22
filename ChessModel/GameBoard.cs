using System.Collections.Generic;
using ChessModel.Pieces;

namespace ChessModel
{
    public class GameBoard
    {
        public static int XDim { get; set; }
        public static int YDim{ get; set; }
        public Square[,] squares;
        public List<IPiece> pieces;

        public GameBoard()
        {

        }

        public GameBoard(int x, int y, int clear = 0)
        {
            XDim = x;
            YDim = y;
            squares = new Square[XDim, YDim];
            pieces = new List<IPiece>();
            InitializeBoard(clear);
        }

        // if clear is != 0, initilize board without any pieces
        public void InitializeBoard(int clear = 0)
        {
            ChessColor color;
            for(int row = 0; row < XDim; row++)
            {
                for(int col = 0; col < YDim; col++)
                {
                    squares[row, col] = new Square(row, col);
                    if (clear == 0)
                    {
                        if (row == 0 || row == 1)
                        {
                            color = ChessColor.White;
                            squares[row, col].piece = AddStartingPiece(row, col, color);
                        }
                        else if (row == XDim - 1 || row == XDim - 2)
                        {
                            color = ChessColor.Black;
                            squares[row, col].piece = AddStartingPiece(row, col, color);
                        }
                    }
                }
            }
        }

        private IPiece AddStartingPiece(int row, int col, ChessColor color)
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
            piece.ColID = col;
            piece.RowID = row;

            pieces.Add(piece);
            return piece;
        }

        public void PlacePiece(IPiece piece, Square square)
        {
            square.piece = piece;
            square.MakeSameCord();
            square.HasPiece = true;
            pieces.Add(piece);
        }

        public void RemovePiece(Square square)
        {
            square.HasPiece = false;
            pieces.Remove(square.piece);
            square.piece = null;
        }

        public void MovePiece(Square fromSquare, Square toSquare)
        {
            toSquare.piece = fromSquare.piece;
            toSquare.MakeSameCord();
            toSquare.HasPiece = true;
            toSquare.piece.MoveCount += 1;
            fromSquare.piece = null;
            fromSquare.HasPiece = false;
        }
        
        public void ClearBoard()
        {
            for (int row = 0; row < XDim; row++)
            {
                for (int col = 0; col < YDim; col++)
                {
                    squares[row, col].piece = null;
                }
            }
            pieces.Clear();
        }
    }
}
