using ChessModel.Pieces;
using System.Collections.Generic;
using System.Linq;


namespace ChessModel
{
    public class GameState
    {
        public bool InPlay()
        {
            //bool check = Check();
            bool checkmate = CheckMate();
            bool stalemate = StaleMate();

            if(checkmate || stalemate)
                return false;

            return true;
        }

       public bool Check(GameBoard gameBoard, bool isCastle = false)
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

            if (check = CheckHelper(gameBoard, whiteKing, blackPieces, isCastle))
                return true;
            else if (check = CheckHelper(gameBoard, blackKing, whitePieces, isCastle))
                return true;

            return false;
        }

        private bool CheckHelper(GameBoard gameBoard, IEnumerable<Square> king, IEnumerable<Square> pieces, bool isCastle)
        {
            List<Square> toSquare = king.ToList();

            foreach (var item in pieces)
            {
                if (item.piece.IsValidMove(gameBoard, item, toSquare[0]))
                {
                    return true;
                }
                else if (isCastle)
                {
                    if (toSquare[0].ColID == 2 && toSquare[0].RowID == GameLogic.lastMove[3].RowID)
                    {
                        if (item.piece.IsValidMove(gameBoard, item, gameBoard.squares[toSquare[0].RowID, 3]))
                        {
                            return true;
                        }
                    }
                    else if (toSquare[0].ColID == 6 && toSquare[0].RowID == GameLogic.lastMove[3].RowID)
                    {
                        if (item.piece.IsValidMove(gameBoard, item, gameBoard.squares[toSquare[0].RowID, 5]))
                        {
                            return true;
                        }
                    }
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
