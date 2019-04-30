using System;

namespace ChessModel
{
    /*
     * A helper static class for assiting the MoveLogic class in determing if a piece
     * is allowed to move to a square or not. Also aids the piece classes in determining
     * how a piece can move.
     */
    public static class MoveValidator
    {
        /*
         * Checks if a square is occupied by a piece
         */
        public static bool IsOccupied(Square toSquare)
        {
            if (toSquare.Piece != null)
                return true;
            else
                return false;
        }

        /*
         * Checks if a square contains a piece of opposite color(enemy)
         */
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

        /*
         * The Rook can move horizontally or vertically any number of places, provided that
         * another piece does not obstruct it. The only exception is if a King is Castling, 
         * but since it is considered to be a King move, it is handled as such. It is only a 
         * valid move if the square it's moving from has a piece and it is not
         * moving to itself. Also need to check if the space it's moving to is not occupied by it's own colored pieces.
         */
        public static bool RookMove(GameBoard gb, Square fromSquare, Square toSquare)
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
                // loop to square right before target can check if each square is empty
                for (int i = 1; i < Math.Abs(rowDiff); i++)
                {
                    if (!IsOccupied(gb.squares[fromRow + (i * signRow), fromCol]))
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
                    if (!IsOccupied(gb.squares[fromRow, fromCol + (i * signCol)]))
                        continue;
                    else
                        return false;
                }
                if (!isOccupied || isEnemy)
                    return true;
            }
            return false;
        }

        /*
         * The Bishop can move diagonally any number of spaces provided that another piece
         * does not obstruct it. It is only a valid move if the square it's moving from has a piece and it is not
         * moving to itself. Also need to check if the space it's moving to is not occupied by it's own colored pieces.
         */
        public static bool BishopMove(GameBoard gb, Square fromSquare, Square toSquare)
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

            // loop to square right before target can check if each square is empty
            for (int i = 1; i < Math.Abs(rowDiff); i++)
            {
                for (int j = 1; j < Math.Abs(colDiff); j++)
                {
                    if (!IsOccupied(gb.squares[fromRow + (i * signRow), fromCol + (i * signCol)]))
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

        /*
         * En Passant is a special Pawn move. If a Pawn moves 2 spaces, an enemy Pawn can capture
         * it by moving to a square as if it only moved 1 space. This can only in the move immediately
         * after a Pawn moves 2 spaces.
         */
        public static bool IsEnPassant(GameBoard gb, Square fromSquare, Square toSquare)
        {
            bool enPassant = EnPassantHelper(gb, fromSquare);
            if (enPassant)
            {
                int sign = fromSquare.Piece.Color == ChessColor.White ? 1 : -1;

                if (fromSquare.Piece.Type == ChessPiece.Pawn && toSquare.RowID == MoveLogic.lastMove[1].RowID + sign 
                    && toSquare.ColID == MoveLogic.lastMove[1].ColID)
                {
                    MoveLogic.lastMove[2] = gb.squares[MoveLogic.lastMove[1].RowID, MoveLogic.lastMove[1].ColID];
                    return true;
                }
            }
            return false;
        }

        /*
         * A helper function for En Passant. Checks if the last move that was made was a Pawn, and it moved 2 spaces.
         * The Move Logic stores the last move, where lastMove[0] is the From square and lastMove[1] is the To square.
         */
        private static bool EnPassantHelper(GameBoard gb, Square fromSquare)
        {
            var lastMove = MoveLogic.lastMove;
            if (lastMove[1].Piece == null)
                return false;

            if (Math.Abs(lastMove[0].RowID - lastMove[1].RowID) == 2 && lastMove[1].Piece.Type == ChessPiece.Pawn && IsEnemy(lastMove[1], fromSquare))
            {
                return true;
            }

            return false;
        }

        /*
         * A Castle is where the King moves 2 spaces horizontally and the Rook of the same color moved to
         * the other side of it. This can only be done if both pieces have never moved, and there are no
         * pieces between them.
         */
        public static bool IsCastle(GameBoard gb, Square fromSquare, Square toSquare)
        {
            int fromRow = fromSquare.RowID;
            int toRow = toSquare.RowID;
            bool isOccupied = IsOccupied(toSquare);

            if (fromRow - toRow != 0 || fromSquare.Piece.MoveCount != 0 || isOccupied)
                return false;

            // Castle to the Left
            if(toSquare.ColID == 2)
            {
                if (gb.squares[fromRow, 0].Piece == null || gb.squares[fromRow, 4].Piece == null)
                {
                    return false;
                }
                if (gb.squares[fromRow, 1].Piece == null && gb.squares[fromRow, 3].Piece == null && gb.squares[fromRow, 0].Piece.MoveCount == 0)
                {
                    return true;
                }
            }

            // Castle to the Right
            else if (toSquare.ColID == 6)
            {
                if(gb.squares[fromRow, 7].Piece == null || gb.squares[fromRow, 4].Piece == null)
                {
                    return false;
                }
                if (gb.squares[fromRow, 5].Piece == null && gb.squares[fromRow, 7].Piece.MoveCount == 0)
                {
                    return true;
                }                
            }
            return false;
        }
    }
}