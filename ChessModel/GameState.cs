using ChessModel.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ChessModel
{
    public class GameState
    {
        GameLogic gameLogic;
        private IEnumerable<Square> whiteKing;
        private IEnumerable<Square> blackKing;
        private IEnumerable<Square> whitePieces;
        private IEnumerable<Square> blackPieces;

        public GameState(GameLogic gameLogic)
        {
            this.gameLogic = gameLogic;
        }

        public bool InPlay(GameBoard gameboard)
        {
            bool checkmate = CheckMate(gameboard, ChessColor.Black);
            bool stalemate = StaleMate();

            if(checkmate || stalemate)
                return false;

            return true;
        }

       public bool Check(GameBoard gameBoard, ChessColor color, bool isCastle = false, bool both = false)
        {
            bool check;
            whiteKing =
                from Square square in gameBoard.squares
                where square.piece != null && square.piece.Type == ChessPiece.King && square.piece.Color == ChessColor.White
                select square;
            blackKing =
                from Square square in gameBoard.squares
                where square.piece != null && square.piece.Type == ChessPiece.King && square.piece.Color == ChessColor.Black
                select square;
            whitePieces =
                from Square square in gameBoard.squares
                where square.piece != null  && square.piece.Color == ChessColor.White
                select square;
            blackPieces =
                from Square square in gameBoard.squares
                where square.piece != null  && square.piece.Color == ChessColor.Black
                select square;

            if (color == ChessColor.White || both)
            {
                if (check = CheckHelper(gameBoard, whiteKing, blackPieces, isCastle))
                    return true;
            }
            if (color == ChessColor.Black || both)
            {
                if (check = CheckHelper(gameBoard, blackKing, whitePieces, isCastle))
                    return true;
            }

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

        public bool CheckMate(GameBoard gameboard, ChessColor color)
        {
            // king is in check
            // moving own pieces still in check

            if(Check(gameboard, color))
            {
                var findColor = whiteKing.ToList();
                IEnumerable<Square> pieces = findColor[0].piece.Color == color ? whitePieces : blackPieces;

                foreach (var item in pieces)
                {
                    if (GetAllPossibleMoves(gameboard, item, color))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        private bool GetAllPossibleMoves(GameBoard gameboard, Square fromSquare, ChessColor color)
        {
            for (int row = 0; row < GameBoard.XDim; row++)
            {
                for (int col = 0; col < GameBoard.YDim; col++)
                {
                    if (gameLogic.MovePiece(fromSquare, gameboard.squares[row, col]))
                    {
                        if(!Check(gameboard, color))
                        {
                            gameLogic.Undo();
                            return true;
                        }
                        else
                        {
                            gameLogic.Undo();
                        }
                    }
                }
            }
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
