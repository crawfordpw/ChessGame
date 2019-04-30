using System.Collections.Generic;
using ChessModel.Pieces;

namespace ChessModel
{
    /*
     * A class to create and store useful information about the Game Board. Contains 
     * some useful methods for manipulating each square on the board.
     */
    public class GameBoard
    {
        public static int XDim { get; set; }
        public static int YDim { get; set; }
        public Square[,] squares;
        public List<IPiece> pieces;

        public GameBoard() : this(8, 8)
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

        /*
         * Creates the game board and places all the pieces in their starting
         * positions. If clear is != 0, initilize board without any pieces
         */
        public void InitializeBoard(int clear = 0)
        {
            ChessColor color;
            for(int row = 0; row < XDim; row++)
            {
                for(int col = 0; col < YDim; col++)
                {
                    squares[row, col] = new Square(row, col);

                    // Determines the color of each square
                    if ((row % 2 == 0 && col % 2 == 0) || (row % 2 == 1 && col % 2 == 1))
                    {
                         squares[row, col].Color = ChessColor.Black;
                    }
                    else
                    {
                        squares[row, col].Color = ChessColor.White;
                    }

                    // Adds the appropriate pieces in their respective places
                    if (clear == 0)
                    {
                        if (row == 0 || row == 1)
                        {
                            color = ChessColor.White;
                            squares[row, col].Piece = AddStartingPiece(row, col, color);
                        }
                        else if (row == XDim - 1 || row == XDim - 2)
                        {
                            color = ChessColor.Black;
                            squares[row, col].Piece = AddStartingPiece(row, col, color);
                        }
                    }
                }
            }
        }

        /*
         * A Helper method for placing the starting pieces in the correct location
         */
        private IPiece AddStartingPiece(int row, int col, ChessColor color)
        {
            IPiece piece;

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
                    piece = new Queen(color);
                else
                    piece = new King(color);
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

        /*
         * A Method for manually placing a piece on the board at any location
         */
        public void PlacePiece(IPiece piece, Square square)
        {
            square.Piece = piece;
            square.MakeSameCord();
            pieces.Add(piece);
        }

        /*
         * A method for removing a piece from the game board, but still keeps it in the list. Useful for Undo
         */
        public void RemovePieceTemp(Square square)
        {
            square.Piece = null;
        }

        /*
         * A method for completely removing a piece from the board and list of pieces
         */
        public void RemovePiece(Square square)
        {
            pieces.Remove(square.Piece);
            square.Piece = null;
        }

        /*
         * Move any piece on board without having to go through any of the move logic
         */
        public void MovePiece(Square fromSquare, Square toSquare)
        {
            toSquare.Piece = fromSquare.Piece;
            toSquare.MakeSameCord();
            toSquare.Piece.MoveCount += 1;
            fromSquare.Piece = null;
        }
        
        /*
         * Removes all the pieces from the board and list
         */
        public void ClearBoard()
        {
            for (int row = 0; row < XDim; row++)
            {
                for (int col = 0; col < YDim; col++)
                {
                    squares[row, col].Piece = null;
                }
            }
            pieces.Clear();
        }
    }
}
