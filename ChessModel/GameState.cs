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
        //private IEnumerable<Square> whiteKing;
        //private IEnumerable<Square> blackKing;
        //private IEnumerable<Square> whitePieces;
        //private IEnumerable<Square> blackPieces;
        private IPiece _whiteKing;
        private IPiece _blackKing;

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
         * logic. It is illegal for a King to Castle to being in Check, or through a Check. 
         */
       public bool Check(GameBoard gb, ChessColor color, bool isCastle = false)
        {           
            var piecesColor = color == ChessColor.White ? ChessColor.Black : ChessColor.White;
            GetKings();

            // if player color is white, check all moves from black pieces to white king. Vice versa for player color black
            if (color == ChessColor.White)
            {
                if (CheckHelper(gb, _whiteKing, piecesColor, isCastle))
                    return true;
            }
            if (color == ChessColor.Black)
            {                
                if (CheckHelper(gb, _blackKing, piecesColor, isCastle))
                    return true;
            }

            return false;
        }

        /*
         * A helper function for Check. Goes through a list of all pieces and check if it's a valid move to the King.
         * If it is, return true the King is in check
         */
        private bool CheckHelper(GameBoard gb, IPiece king, ChessColor color, bool isCastle)
        {

            foreach (var item in gb.pieces)
            {
                if (item.Color == color && gb.squares[item.RowID, item.ColID].Piece != null)
                {
                    if (item.IsValidMove(gb, gb.squares[item.RowID, item.ColID], gb.squares[king.RowID, king.ColID]))
                    {
                        return true;
                    }

                    // Castle Check logic
                    else if (isCastle)
                    {
                        // if Castle to the left
                        if (king.ColID == 2 && king.RowID == MoveLogic.lastMove[3].RowID)
                        {
                            if (item.IsValidMove(gb, gb.squares[item.RowID, item.ColID], gb.squares[king.RowID, 3]))
                            {
                                return true;
                            }
                        }

                        // if Castle to the right
                        else if (king.ColID == 6 && king.RowID == MoveLogic.lastMove[3].RowID)
                        {
                            if (item.IsValidMove(gb, gb.squares[item.RowID, item.ColID], gb.squares[king.RowID, 5]))
                            {
                                return true;
                            }
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
            GetKings();

            if(Check(gb, color))
            {
                // Gets all the possible valid moves for a piece
                foreach (var item in gb.pieces)
                {
                    if (item.Color == color)
                    {
                        if (GetAllPossibleMoves(gb, gb.squares[item.RowID, item.ColID], color))
                        {
                            SetLastMove();
                            return false;
                        }
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
            GetKings();

            if (!Check(gb, color))
            {
                foreach (var item in gb.pieces)
                {
                    if (item.Color == color)
                    {
                        if (GetAllPossibleMoves(gb, gb.squares[item.RowID, item.ColID], color))
                        {
                            SetLastMove();
                            return false;
                        }
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

            lastFromMove = (from Square square in gb.squares
                             where square.ColID == MoveLogic.lastMove[0].ColID && square.RowID == MoveLogic.lastMove[0].RowID
                             select square).First();
            lastToMove = (from Square square in gb.squares
                           where square.ColID == MoveLogic.lastMove[1].ColID && square.RowID == MoveLogic.lastMove[1].RowID
                            select square).First();
  
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

        /*
         * Updates the Kings variable by looping through the list of pieces. Normally this would not be necessary,
         * and it would only need to be assigned once. However, in the event that the board is cleared, or a king is
         * somehow removed or replaced from the list of pieces, this added redundancy protects from pointing to 
         * the "wrong" king.
         */
        private void GetKings()
        {
            for (int i = 0; i < this.ml.gb.pieces.Count; i++)
            {
                if (ml.gb.pieces[i].Type == ChessPiece.King && ml.gb.pieces[i].Color == ChessColor.White)
                {
                    _whiteKing = ml.gb.pieces[i];
                }
                if (ml.gb.pieces[i].Type == ChessPiece.King && ml.gb.pieces[i].Color == ChessColor.Black)
                {
                    _blackKing = ml.gb.pieces[i];
                }
            }
        }
    }
}