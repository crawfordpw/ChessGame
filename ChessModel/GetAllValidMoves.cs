using System.Collections.Generic;

namespace ChessModel
{
    /*
     * This class is used to get all the possible valid moves for a piece. The reason it's in
     * it's own class is that it could be optimized later. Instead of going through every single
     * square for a piece on the board, it would only go through the squares where it could move.
     * e.g For a King's valid moves, it would only check 1 space around the King and not the whole board.
     * Each piece would have it's own method.
     * 
     * Iterates through the entire board and tries to move
     * a given piece to each square. If it is able to move, see if the king is not in Check. If it's not,
     * then there is a move that a player can make. FirstValid is used for checking checkmate or stalemate. 
     * In which case it would stop looking for moves at the first one. Otherwise, it will push the move into a 
     * a list of all valid moves. Need to Undo the move since the player hasn't actually made it
     */
    public static class GetAllValidMoves
    {
        public static List<string> AllValidMoves = new List<string>();

        public static bool GetMoves(MoveLogic ml, Square fromSquare, ChessColor color, bool FirstValid)
        {
            if (!FirstValid)
            {
                ml.gs.StoreLastMove(ml.gb);
                AllValidMoves.Clear();
            }
            for (int row = 0; row < GameBoard.XDim; row++)
            {
                for (int col = 0; col < GameBoard.YDim; col++)
                {
                    if (ml.MovePiece(fromSquare, ml.gb.squares[row, col], true))
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
                    }
                }
            }
            if (!FirstValid)
            {
                ml.gs.SetLastMove();
            }
            return false;
        }
    }
}
