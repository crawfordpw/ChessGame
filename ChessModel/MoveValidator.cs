using System;
using ChessModel.Pieces;

namespace ChessModel
{
   public static class MoveValidator
    {
        public static bool IsOccupied(Square toSquare)
        {
            if (toSquare.piece != null)
                return true;
            else
                return false;
        }

        public static bool IsEnemy(Square fromSquare, Square toSquare)
        {
            if (IsOccupied(toSquare))
            {
                if (fromSquare.piece.Color == toSquare.piece.Color)
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
            bool isOccupied = MoveValidator.IsOccupied(toSquare);
            bool isEnemy = MoveValidator.IsEnemy(fromSquare, toSquare);
            int rowDiff = toRow - fromRow;
            int colDiff = toCol - fromCol;

            int signRow = rowDiff > 0 ? 1 : -1;
            int signCol = colDiff > 0 ? 1 : -1;

            if (fromSquare.piece == null || (rowDiff == 0 && colDiff == 0))
                return false;

            // if the move is vertical
            if (colDiff == 0)
            {
                // loop to square right before target
                for (int i = 1; i < Math.Abs(rowDiff); i++)
                {
                    if (!MoveValidator.IsOccupied(gameboard.squares[fromRow + (i * signRow), fromCol]))
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
                    if (!MoveValidator.IsOccupied(gameboard.squares[fromRow, fromCol + (i * signCol)]))
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
            bool isOccupied = MoveValidator.IsOccupied(toSquare);
            bool isEnemy = MoveValidator.IsEnemy(fromSquare, toSquare);
            int rowDiff = toRow - fromRow;
            int colDiff = toCol - fromCol;

            int signRow = rowDiff > 0 ? 1 : -1;
            int signCol = colDiff > 0 ? 1 : -1;

            if (fromSquare.piece == null || Math.Abs(rowDiff) != Math.Abs(colDiff))
                return false;

            for (int i = 1; i < Math.Abs(rowDiff); i++)
            {
                for (int j = 1; j < Math.Abs(colDiff); j++)
                {
                    if (!MoveValidator.IsOccupied(gameboard.squares[fromRow + (i * signRow), fromCol + (i * signCol)]))
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

        public static bool IsEnPassant(Square fromSquare, Square toSquare)
        {
            bool enPassant = EnPassantHelper(fromSquare, toSquare);
            if (enPassant)
            {
                int sign = fromSquare.piece.Color == ChessColor.White ? 1 : -1;

                if (fromSquare.piece.Type == ChessPiece.Pawn && toSquare.RowID == GameLogic.lastMove[1].RowID + sign 
                    && toSquare.ColID == GameLogic.lastMove[1].ColID)
                {
                    return true;
                }
            }
            return false;
        }

        private static bool EnPassantHelper(Square fromSquare, Square toSquare)
        {
            var lastMove = GameLogic.lastMove;

            if (Math.Abs(lastMove[0].RowID - lastMove[1].RowID) == 2 && lastMove[1].piece.Type == ChessPiece.Pawn && IsEnemy(lastMove[1], fromSquare))
                return true;

            return false;
        }
    }
}
