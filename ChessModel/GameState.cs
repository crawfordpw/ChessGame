using ChessModel.Pieces;
using System.Collections.Generic;

namespace ChessModel
{
    public class GameState
    {
        readonly GameBoard gameBoard;

        public GameState()
        {

        }

        public GameState(GameBoard gameBoard)
        {
            this.gameBoard = gameBoard;
        }

        public bool InPlay()
        {
            bool checkmate = CheckMate();
            bool stalemate = StaleMate();

            if(checkmate || stalemate)
                return false;

            return true;
        }

        public bool Check()
        {
            // for all opposite color pieces
            // check if valid move to king
            return false;
        }

        public bool CheckMate()
        {
            // king is in check
            // moving places in check

            return false;
        }

        public bool StaleMate()
        {
            //King is NOT in check!
            //No other legal moves to make! (own king cant be left in check)
            //It is a draw!
            return false;
        }
    }
}
