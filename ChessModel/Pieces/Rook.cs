using System;

namespace ChessModel.Pieces
{
    public class Rook : IPiece
    {
        public ChessPiece Type { get; set; }
        public ChessColor Color { get; set; }
        public int ColID { get; set; }
        public int RowID { get; set; }

        public Rook()
        {

        }

        public Rook(ChessColor color)
        {
            Type = ChessPiece.Rook;
            Color = (color == ChessColor.Black) ? ChessColor.Black : ChessColor.White;
        }

        public void AddAtLocation(int row, int col)
        {
            RowID = row;
            ColID = col;
        }

        public bool IsValidMove(GameBoard gameboard, Square fromSquare, Square toSquare)
        {
            int fromRow = fromSquare.RowID;
            int fromCol = fromSquare.ColID;
            int toRow = toSquare.RowID;
            int toCol = toSquare.ColID;
            bool isOccupied = MoveValidator.IsOccupied(toSquare);
            bool isEnemy = MoveValidator.IsEnemy(fromSquare, toSquare);
            int rowDiff = toRow - fromRow;
            int colDiff = toCol- fromCol;

            if (fromSquare.piece == null || (rowDiff == 0 && colDiff == 0)) 
                return false;

            // if the move is vertical
            if (colDiff == 0)
            {
                // if move upwards
                if (rowDiff > 0)
                {
                    // loop to square right before target and check if empty
                    for (int i = 1; i < rowDiff; i++)
                    {
                        if (!MoveValidator.IsOccupied(gameboard.squares[fromRow + i, fromCol]))
                            continue;
                        else
                            return false;
                    }
                }
                // otherwise its downwards
                else
                {
                    for (int i = -1; i > rowDiff; i--)
                    {
                        if (!MoveValidator.IsOccupied(gameboard.squares[fromRow + i, fromCol]))
                            continue;
                        else
                            return false;
                    }
                }

                // target space can either be empty or contain an enemy piece
                if (!isOccupied || isEnemy)
                    return true;
            }

            //if the move is horizontal
            else if (rowDiff == 0)
            {
                // if move its towards the right
                if (colDiff > 0)
                {
                    for (int i = 1; i < colDiff; i++)
                    {
                        if (!MoveValidator.IsOccupied(gameboard.squares[fromRow, fromCol + i]))
                            continue;
                        else
                            return false;
                    }
                }
                // otherwise its to the left
                else
                {
                    for (int i = -1; i > colDiff; i--)
                    {
                        if (!MoveValidator.IsOccupied(gameboard.squares[fromRow, fromCol + i]))
                            continue;
                        else
                            return false;
                    }
                }
                if (!isOccupied || isEnemy)
                    return true;
            }
            return false;
        }
    }
}
