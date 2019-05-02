using System;
using System.Collections.Generic;

namespace ChessModel
{
    /*
     * This class is used to get all the possible valid moves for a piece. The reason it's in
     * it's own class is for optimization purposes. Instead of going through every single
     * square for a piece on the board, it would only go through the squares where it could move.
     * e.g For a King's valid moves, it would only check 1 space around the King and not the whole board.
     * Each piece has it's own method.
     * 
     * Iterates through all possible moves and tries to move a given piece to each square. If it is able to move, 
     * see if the king is not in Check. If it's not, then there is a move that a player can make. FirstValid is used 
     * for checking checkmate or stalemate. In which case it would stop looking for moves at the first one. Otherwise, 
     * it will push the move into a a list of all valid moves. Need to Undo the move since the player hasn't actually made it
     */
    public static class GetAllValidMoves
    {
        public static List<string> AllValidMoves = new List<string>();

        /*
         * The method that will be called for checking all valid moves. Executes a special method depending
         * on the piece that it is checking the moves for. If it's not checking for checkmate or stalemate (FirstValid),
         * store the last move and set the last move here. Clear the list.
         */
        public static bool GetMoves(MoveLogic ml, Square fromSquare, ChessColor color, bool FirstValid)
        {
            if (!FirstValid)
            {
                ml.gs.StoreLastMove(ml.gb);
                AllValidMoves.Clear();
            }
            int row = fromSquare.RowID;
            int col = fromSquare.ColID;

            switch (fromSquare.Piece.Type)
            {
                case (ChessPiece.Pawn):
                    if (ValidPawn(ml, fromSquare, color, FirstValid, row, col))
                    {
                        return true;
                    }
                    break;
                case (ChessPiece.Rook):
                    if (ValidRook(ml, fromSquare, color, FirstValid, row, col))
                    {
                        return true;
                    }
                    break;
                case (ChessPiece.Bishop):
                    if (ValidBishop(ml, fromSquare, color, FirstValid, row, col))
                    {
                        return true;
                    }
                    break;
                case (ChessPiece.Knight):
                    if (ValidKnight(ml, fromSquare, color, FirstValid, row, col))
                    {
                        return true;
                    }
                    break;
                case (ChessPiece.Queen):
                    if (ValidQueen(ml, fromSquare, color, FirstValid, row, col))
                    {
                        return true;
                    }
                    break;
                case (ChessPiece.King):
                    if (ValidKing(ml, fromSquare, color, FirstValid, row, col))
                    {
                        return true;
                    }
                    break;
            }

            if (!FirstValid)
            {
                ml.gs.SetLastMove();
            }
            return false;
        }

        /*
         * Check two spaces in front and one space to the left and right of the pawn. Since a pawn can only move forward, 
         * the sign variable captures that. 
         */
        private static bool ValidPawn(MoveLogic ml, Square fromSquare, ChessColor color, bool FirstValid, int row, int col)
        {
            int sign = fromSquare.Piece.Color == ChessColor.White ? 1 : -1;

            for (int i = 1; i <= 2; i++)
            {
                for (int j = - 1; j <= 1; j++)
                {
                    if (row + (i * sign) < GameBoard.XDim && row + (i * sign) >= 0 && col + j < GameBoard.YDim && col + j >= 0)
                    {
                        if (ml.MovePiece(fromSquare, ml.gb.squares[row + (i * sign), col + j], true))
                        {
                            if (IsValidMove(ml, color, FirstValid, row + (i * sign), col + j))
                            {
                                if (FirstValid)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        /*
         * Check all squares in the rooks row, then all squares in it's column
         */
        private static bool ValidRook(MoveLogic ml, Square fromSquare, ChessColor color, bool FirstValid, int row, int col)
        {
            // check horizontally
            for (int i = 0; i < 8; i++)
            {
                if (ml.MovePiece(fromSquare, ml.gb.squares[i, col], true))
                {
                    if (IsValidMove(ml, color, FirstValid, i, col))
                    {
                        if (FirstValid)
                        {
                            return true;
                        }
                    }
                }
            }

            // check vertically
            for (int i = 0; i < 8; i++)
            {
                if (ml.MovePiece(fromSquare, ml.gb.squares[row, i], true))
                {
                    if (IsValidMove(ml, color, FirstValid, row, i))
                    {
                        if (FirstValid)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }


        /*
         * Check all squares two spaces around the knight
         */
        private static bool ValidKnight(MoveLogic ml, Square fromSquare, ChessColor color, bool FirstValid, int row, int col)
        {
            for (int i = - 2; i <= 2; i++)
            {
                for (int j = -2; j <= 2; j++)
                {
                    if (row + i < GameBoard.XDim && row + i >= 0 && col + j < GameBoard.YDim && col + j >= 0)
                    {
                        if (ml.MovePiece(fromSquare, ml.gb.squares[row + i, col + j], true))
                        {
                            if (IsValidMove(ml, color, FirstValid, row + i, col + j))
                            {
                                if (FirstValid)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        /*
         * Calculates the given equation |x2 - x1| = |y2 - y1| where x2 is the piece row, x1 is a row 0-8
         * y2 is the piece column, y1 a column 0-8. This gives the diagonals of the bishop. If it is true, 
         * then start checking for valid moves
         */
        private static bool ValidBishop(MoveLogic ml, Square fromSquare, ChessColor color, bool FirstValid, int row, int col)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Math.Abs(row - i) == Math.Abs(col - j))
                    {
                        if (ml.MovePiece(fromSquare, ml.gb.squares[i, j], true))
                        {
                            if (IsValidMove(ml, color, FirstValid, i, j))
                            {
                                if (FirstValid)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        /*
         * A queen can either be a rook or a bishop move
         */
        private static bool ValidQueen(MoveLogic ml, Square fromSquare, ChessColor color, bool FirstValid, int row, int col)
        {
            bool rook = ValidRook(ml, fromSquare, color, FirstValid, row, col);
            bool bishop = ValidBishop(ml, fromSquare, color, FirstValid, row, col);

            if(rook || bishop)
            {
                return true;
            }

            return false;
        }

        /*
         * Checks all squares one space around the kind, and 2 spaces to it;s left and right (for castling)
         */
        private static bool ValidKing(MoveLogic ml, Square fromSquare, ChessColor color, bool FirstValid, int row, int col)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = - 2; j <= 2; j++)
                {
                    if (row + i < GameBoard.XDim && row + i >= 0 && col + j < GameBoard.YDim && col + j >= 0)
                    {
                        if (ml.MovePiece(fromSquare, ml.gb.squares[row + i, col + j], true))
                        {
                            if (IsValidMove(ml, color, FirstValid, row + i, col + j))
                            {
                                if (FirstValid)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        /*
         * If the move that is made does not left the king in check, it is a valid move. If it's not checkmate
         * or a stalemate, add that move to that valid moves list. Need to undo that move made
         */
        private static bool IsValidMove(MoveLogic ml, ChessColor color, bool FirstValid, int row, int col)
        {
            if (!ml.gs.Check(ml.gb, color))
            {
                if (!FirstValid)
                {
                    AllValidMoves.Add(ml.gb.squares[row, col].Coord);
                    ml.Undo();
                    ml.gs.SetLastMove();
                }
                else
                {
                    ml.Undo();
                    return true;
                }
            }
            else
            {
                ml.Undo();
            }
            return false;
        }
    }
}
