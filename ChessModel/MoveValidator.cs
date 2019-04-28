using System;
using ChessModel.Pieces;

namespace ChessModel
{
   public static class MoveValidator
    {
        public static bool IsOccupied(Square toSquare)
        {
            if (toSquare.Piece != null)
                return true;
            else
                return false;
        }

        public static bool IsEnemy(Square fromSquare, Square toSquare)
        {
            if (IsOccupied(toSquare))
            {
                if (fromSquare.Piece.Color == toSquare.Piece.Color)
                    return false;
                else
                    return true;
            }
            else
                return false;
        }

        public static bool RookMove(GameBoard gameboard, Square fromSquare, Square toSquare)
        {
            int fromRow = fromSquare.RowID;
            int fromCol = fromSquare.ColID;
            int toRow = toSquare.RowID;
            int toCol = toSquare.ColID;
            bool isOccupied = IsOccupied(toSquare);
            bool isEnemy = IsEnemy(fromSquare, toSquare);
            int rowDiff = toRow - fromRow;
            int colDiff = toCol - fromCol;

            int signRow = rowDiff > 0 ? 1 : -1;
            int signCol = colDiff > 0 ? 1 : -1;

            if (fromSquare.Piece == null || (rowDiff == 0 && colDiff == 0))
                return false;

            // if the move is vertical
            if (colDiff == 0)
            {
                // loop to square right before target
                for (int i = 1; i < Math.Abs(rowDiff); i++)
                {
                    if (!IsOccupied(gameboard.squares[fromRow + (i * signRow), fromCol]))
                        continue;
                    else
                        return false;
                }
                // target space can either be empty or contain an enemy piece
                if (!isOccupied || isEnemy)
                    return true;
            }

            //if the move is horizontal
            else if (rowDiff == 0)
            {
                for (int i = 1; i < Math.Abs(colDiff); i++)
                {
                    if (!IsOccupied(gameboard.squares[fromRow, fromCol + (i * signCol)]))
                        continue;
                    else
                        return false;
                }
                if (!isOccupied || isEnemy)
                    return true;
            }
            return false;
        }

        public static bool BishopMove(GameBoard gameboard, Square fromSquare, Square toSquare)
        {
            int fromRow = fromSquare.RowID;
            int fromCol = fromSquare.ColID;
            int toRow = toSquare.RowID;
            int toCol = toSquare.ColID;
            bool isOccupied = IsOccupied(toSquare);
            bool isEnemy = IsEnemy(fromSquare, toSquare);
            int rowDiff = toRow - fromRow;
            int colDiff = toCol - fromCol;

            int signRow = rowDiff > 0 ? 1 : -1;
            int signCol = colDiff > 0 ? 1 : -1;

            if (fromSquare.Piece == null || Math.Abs(rowDiff) != Math.Abs(colDiff))
                return false;

            for (int i = 1; i < Math.Abs(rowDiff); i++)
            {
                for (int j = 1; j < Math.Abs(colDiff); j++)
                {
                    if (!IsOccupied(gameboard.squares[fromRow + (i * signRow), fromCol + (i * signCol)]))
                        continue;
                    else
                        return false;
                }
            }

            // target space can either be empty or contain an enemy piece
            if (!isOccupied || isEnemy)
                return true;

            return false;
        }

        public static bool IsEnPassant(GameBoard gb, Square fromSquare, Square toSquare)
        {
            bool enPassant = EnPassantHelper(gb, fromSquare);
            if (enPassant)
            {
                int sign = fromSquare.Piece.Color == ChessColor.White ? 1 : -1;

                if (fromSquare.Piece.Type == ChessPiece.Pawn && toSquare.RowID == GameLogic.lastMove[1].RowID + sign 
                    && toSquare.ColID == GameLogic.lastMove[1].ColID)
                {
                    GameLogic.lastMove[2] = gb.squares[GameLogic.lastMove[1].RowID, GameLogic.lastMove[1].ColID];
                    return true;
                }
            }
            return false;
        }

        private static bool EnPassantHelper(GameBoard gb, Square fromSquare)
        {
            var lastMove = GameLogic.lastMove;
            if (lastMove[1].Piece == null)
                return false;

            if (Math.Abs(lastMove[0].RowID - lastMove[1].RowID) == 2 && lastMove[1].Piece.Type == ChessPiece.Pawn && IsEnemy(lastMove[1], fromSquare))
            {
                return true;
            }

            return false;
        }

        public static bool IsCastle(GameBoard gameBoard, Square fromSquare, Square toSquare)
        {
            int fromRow = fromSquare.RowID;
            int toRow = toSquare.RowID;
            bool isOccupied = IsOccupied(toSquare);

            if (fromRow - toRow != 0 || fromSquare.Piece.MoveCount != 0 || isOccupied)
                return false;


            if(toSquare.ColID == 2)
            {
                if (gameBoard.squares[fromRow, 0].Piece == null || gameBoard.squares[fromRow, 4].Piece == null)
                {
                    return false;
                }
                if (gameBoard.squares[fromRow, 1].Piece == null && gameBoard.squares[fromRow, 3].Piece == null && gameBoard.squares[fromRow, 0].Piece.MoveCount == 0)
                {
                    return true;
                }
            }
            else if (toSquare.ColID == 6)
            {
                if(gameBoard.squares[fromRow, 7].Piece == null || gameBoard.squares[fromRow, 4].Piece == null)
                {
                    return false;
                }
                if (gameBoard.squares[fromRow, 5].Piece == null && gameBoard.squares[fromRow, 7].Piece.MoveCount == 0)
                {
                    return true;
                }                
            }
            return false;
        }
    }
}