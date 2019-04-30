using System.Collections.Generic;
using System.Linq;


namespace ChessModel
{
    /*
     * This class is used to determine the state of the game. It determines if there is a
     * checkmate, stalemate, or if a King is in Check 
     */
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

        /*
         * Returns true if the game is not in checkmate or a stalemate 
         */
        public bool InPlay(GameBoard gb, ChessColor color)
        {
            bool checkmate = CheckMate(gb, color);
            bool stalemate = StaleMate(gb, color);

            if(checkmate || stalemate)
                return false;

            State = State.InPlay;
            return true;
        }

        /*
         * Used to determine if a given player (color) is in Check. isCastle is a flag for checking Castle
         * logic. It is illegal for a King to Castle to being in Check, or through a Check. The both
         * variable is a debug flag for checking both players.
         */
       public bool Check(GameBoard gb, ChessColor color, bool isCastle = false, bool both = false)
        {
            // creates 4 lists for all the pieces. 
            // this can be optimized later. instead of iterating through all 64 squares 4 times at most and creating 4 lists
            // everytime Check is called, update correct list everytime a move happens.
            whiteKing =
                from Square square in gb.squares
                where square.Piece != null && square.Piece.Type == ChessPiece.King && square.Piece.Color == ChessColor.White
                select square;
            blackKing =
                from Square square in gb.squares
                where square.Piece != null && square.Piece.Type == ChessPiece.King && square.Piece.Color == ChessColor.Black
                select square;
            whitePieces =
                from Square square in gb.squares
                where square.Piece != null  && square.Piece.Color == ChessColor.White
                select square;
            blackPieces =
                from Square square in gb.squares
                where square.Piece != null  && square.Piece.Color == ChessColor.Black
                select square;

            // if player color is white, check all moves from back pieces to white king. Vice versa for player color black
            if (color == ChessColor.White || both)
            {
                if (CheckHelper(gb, whiteKing, blackPieces, isCastle))
                    return true;
            }
            if (color == ChessColor.Black || both)
            {
                if (CheckHelper(gb, blackKing, whitePieces, isCastle))
                    return true;
            }

            return false;
        }

        /*
         * A helper function for Check. Goes through a list of all pieces and check if it's a valid move to the King.
         * If it is, return true the King is in check
         */
        private bool CheckHelper(GameBoard gb, IEnumerable<Square> king, IEnumerable<Square> pieces, bool isCastle)
        {
            List<Square> toSquare = king.ToList();

            foreach (var item in pieces)
            {
                if (item.Piece.IsValidMove(gb, item, toSquare[0]))
                {
                    return true;
                }

                // Castle Check logic
                else if (isCastle)
                {
                    // if Castle to the left
                    if (toSquare[0].ColID == 2 && toSquare[0].RowID == MoveLogic.lastMove[3].RowID)
                    {
                        if (item.Piece.IsValidMove(gb, item, gb.squares[toSquare[0].RowID, 3]))
                        {
                            return true;
                        }
                    }

                    // if Castle to the right
                    else if (toSquare[0].ColID == 6 && toSquare[0].RowID == MoveLogic.lastMove[3].RowID)
                    {
                        if (item.Piece.IsValidMove(gb, item, gb.squares[toSquare[0].RowID, 5]))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /*
         * Checkmate is determined if a King is in check, and moving all of it's own pieces still results in check
         */
        public bool CheckMate(GameBoard gb, ChessColor color)
        {
            // store the last move since we will actually be moving the pieces, and the last move will changed/lost
            StoreLastMove(gb);

            if(Check(gb, color))
            {
                var findColor = whiteKing.ToList();
                IEnumerable<Square> pieces = findColor[0].Piece.Color == color ? whitePieces : blackPieces;

                // Gets all the possible valid moves for a piece
                foreach (var item in pieces)
                {
                    if (GetAllPossibleMoves(gb, item, color))
                    {
                        SetLastMove();
                        return false;
                    }
                }

                // restore the last move
                SetLastMove();
                State = State.CheckMate;
                return true;
            }
            SetLastMove();
            return false;
        }

        /*
         * Stalemate is determined if a King is not in check, but moving any of it's own pieces results in check
         */
        public bool StaleMate(GameBoard gb, ChessColor color)
        {
            // store the last move since we will actually be moving the pieces, and the last move will changed/lost
            StoreLastMove(gb);

            if (!Check(gb, color))
            {
                var findColor = whiteKing.ToList();
                IEnumerable<Square> pieces = findColor[0].Piece.Color == color ? whitePieces : blackPieces;

                foreach (var item in pieces)
                {
                    if (GetAllPossibleMoves(gb, item, color))
                    {
                        SetLastMove();
                        return false;
                    }
                }

                // restore the last move
                SetLastMove();
                State = State.StaleMate;
                return true;
            }
            SetLastMove();
            return false;
        }

        /*
         * Helper method for checkmate and stalemate. Iterates throught the entire board and tries to move
         * a given piece to each square. If it is able to move, see if the king is not in Check. If it's not,
         * then there is a move that a player can make to not be in checkmate or stalemate. Need to Undo the
         * move since the player hasn't actually made it
         */
        private bool GetAllPossibleMoves(GameBoard gb, Square fromSquare, ChessColor color)
        {
            for (int row = 0; row < GameBoard.XDim; row++)
            {
                for (int col = 0; col < GameBoard.YDim; col++)
                {
                    if (ml.MovePiece(fromSquare, gb.squares[row, col], true))
                    {                      
                        if (!Check(gb, color))
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

        /*
         * Stores variables needed to return to a state before the program starts making possible moves that a
         * player hasn't made
         */
        private void StoreLastMove(GameBoard gb)
        {

            lastFromMoveIE = from Square square in gb.squares
                             where square.ColID == MoveLogic.lastMove[0].ColID && square.RowID == MoveLogic.lastMove[0].RowID
                             select square;
            lastToMoveIE = from Square square in gb.squares
                           where square.ColID == MoveLogic.lastMove[1].ColID && square.RowID == MoveLogic.lastMove[1].RowID
                           select square;
            lastFromMove = lastFromMoveIE.ToList()[0];
            lastToMove = lastToMoveIE.ToList()[0];
            _castle = ml.isCastle;
            _enPassant = ml.isEnPassant;
            _capture = ml.isCapture;
        }

        /*
         * Returns variables to the state after the program making possible moves that a player hasn't made
         */
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
