using ChessModel.Pieces;
using System.Collections.Generic;
using System.Linq;


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
            bool check;
            IEnumerable<Square> whiteKing =
                from Square square in gameBoard.squares
                where square.piece != null && square.piece.Type == ChessPiece.King && square.piece.Color == ChessColor.White
                select square;
            IEnumerable<Square> blackKing =
                from Square square in gameBoard.squares
                where square.piece != null && square.piece.Type == ChessPiece.King && square.piece.Color == ChessColor.Black
                select square;
            IEnumerable<Square> whitePieces =
                from Square square in gameBoard.squares
                where square.piece != null  && square.piece.Color == ChessColor.White
                select square;
            IEnumerable<Square> blackPieces =
                from Square square in gameBoard.squares
                where square.piece != null  && square.piece.Color == ChessColor.Black
                select square;

            if (check = CheckHelper(whiteKing, blackPieces))
                return true;
            else if (check = CheckHelper(blackKing, whitePieces))
                return true;

            return false;
        }

        private bool CheckHelper(IEnumerable<Square> king, IEnumerable<Square> pieces)
        {
            List<Square> toSquare = king.ToList();

            foreach (var item in pieces)
            {
                if (item.piece.IsValidMove(gameBoard, item, toSquare[0]))
                {
                    return true;
                }
            }
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
