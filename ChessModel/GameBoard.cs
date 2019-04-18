using System;
using System.Collections.Generic;
using System.Text;

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
            for(int row = 0; row < XDim; row++)
            {
                for(int col = 0; col < YDim; col++)
                {
                    squares[row, col] = new Square(row, col);
                    AddPiece(row, col);
                }
            }
        }

        public void AddPiece(int row, int col)
        {

        }
    }
}
