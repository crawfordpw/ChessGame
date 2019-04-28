using ChessModel.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ChessModel
{
    public class GameState
    {
        public State State { get; set; }
        private readonly MoveLogic ml;
        private IEnumerable<Square> whiteKing;
        private IEnumerable<Square> blackKing;
        private IEnumerable<Square> whitePieces;
        private IEnumerable<Square> blackPieces;
        private IEnumerable<Square> lastFromMoveIE;
        private IEnumerable<Square> lastToMoveIE;
        private Square lastFromMove;
        private Square lastToMove;
        private bool _castle;
        private bool _enPassant;
        private bool _capture;

        public GameState(MoveLogic ml)
        {
            this.ml = ml;
        }

        public bool InPlay(GameBoard gameboard, ChessColor color)
        {
            bool checkmate = CheckMate(gameboard, color);
            bool stalemate = StaleMate(gameboard, color);

            if(checkmate || stalemate)
                return false;

            State = State.InPlay;
            return true;
        }

       public bool Check(GameBoard gameBoard, ChessColor color, bool isCastle = false, bool both = false)
        {
            bool check;
            whiteKing =
                from Square square in gameBoard.squares
                where square.Piece != null && square.Piece.Type == ChessPiece.King && square.Piece.Color == ChessColor.White
                select square;
            blackKing =
                from Square square in gameBoard.squares
                where square.Piece != null && square.Piece.Type == ChessPiece.King && square.Piece.Color == ChessColor.Black
                select square;
            whitePieces =
                from Square square in gameBoard.squares
                where square.Piece != null  && square.Piece.Color == ChessColor.White
                select square;
            blackPieces =
                from Square square in gameBoard.squares
                where square.Piece != null  && square.Piece.Color == ChessColor.Black
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
                if (item.Piece.IsValidMove(gameBoard, item, toSquare[0]))
                {
                    return true;
                }
                else if (isCastle)
                {
                    if (toSquare[0].ColID == 2 && toSquare[0].RowID == MoveLogic.lastMove[3].RowID)
                    {
                        if (item.Piece.IsValidMove(gameBoard, item, gameBoard.squares[toSquare[0].RowID, 3]))
                        {
                            return true;
                        }
                    }
                    else if (toSquare[0].ColID == 6 && toSquare[0].RowID == MoveLogic.lastMove[3].RowID)
                    {
                        if (item.Piece.IsValidMove(gameBoard, item, gameBoard.squares[toSquare[0].RowID, 5]))
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
            StoreLastMove(gameboard);

            if(Check(gameboard, color))
            {
                var findColor = whiteKing.ToList();
                IEnumerable<Square> pieces = findColor[0].Piece.Color == color ? whitePieces : blackPieces;

                foreach (var item in pieces)
                {
                    if (GetAllPossibleMoves(gameboard, item, color))
                    {
                        SetLastMove();
                        return false;
                    }
                }
                SetLastMove();
                State = State.CheckMate;
                return true;
            }
            SetLastMove();
            return false;
        }

        public bool StaleMate(GameBoard gameboard, ChessColor color)
        {
            // king is NOT in check!
            // no other legal moves to make! (own king cant be left in check)
            StoreLastMove(gameboard);

            if (!Check(gameboard, color))
            {
                var findColor = whiteKing.ToList();
                IEnumerable<Square> pieces = findColor[0].Piece.Color == color ? whitePieces : blackPieces;

                foreach (var item in pieces)
                {
                    if (GetAllPossibleMoves(gameboard, item, color))
                    {
                        SetLastMove();
                        return false;
                    }
                }
                State = State.StaleMate;
                SetLastMove();
                return true;
            }
            SetLastMove();
            return false;
        }

        // There is a valid move to make where own King is not in Check
        private bool GetAllPossibleMoves(GameBoard gameboard, Square fromSquare, ChessColor color)
        {
            for (int row = 0; row < GameBoard.XDim; row++)
            {
                for (int col = 0; col < GameBoard.YDim; col++)
                {
                    if (ml.MovePiece(fromSquare, gameboard.squares[row, col], true))
                    {                      
                        if (!Check(gameboard, color))
                        {
                            ml.Undo();
                            return true;
                        }
                        else
                        {
                            ml.Undo();
                        }
                    }
                }
            }
            return false;
        }

        private void StoreLastMove(GameBoard gameboard)
        {

            lastFromMoveIE = from Square square in gameboard.squares
                             where square.ColID == MoveLogic.lastMove[0].ColID && square.RowID == MoveLogic.lastMove[0].RowID
                             select square;
            lastToMoveIE = from Square square in gameboard.squares
                           where square.ColID == MoveLogic.lastMove[1].ColID && square.RowID == MoveLogic.lastMove[1].RowID
                           select square;
            lastFromMove = lastFromMoveIE.ToList()[0];
            lastToMove = lastToMoveIE.ToList()[0];
            _castle = ml.isCastle;
            _enPassant = ml.isEnPassant;
            _capture = ml.isCapture;
        }

        private void SetLastMove()
        {
            MoveLogic.lastMove[0] = lastFromMove;
            MoveLogic.lastMove[1] = lastToMove;
            ml.isCastle = _castle;
            ml.isEnPassant = _enPassant;
            ml.isCapture = _capture;
        }
    }
}
