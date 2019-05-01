namespace ChessModel
{
    /*
     * The GameLogic class is responsible for filtering through a given input of 
     * a row and column(a square on the chessboard) and determining whether it is a
     * square where a piece is moving from or to. It also calls methods in other classes
     * for moving a piece, checking if a game as ended and then flagging it, promoting a
     * pawn, and getting the next player's turn. 
     */
    public class GameLogic
    {
        public Square FromSquare { get; set; }
        public Square ToSquare { get; set; }
        public bool UpdateMovement { get; set; } // Flag for updating movement for UI
        public bool Promotion { get; set; }
        public bool CheckMate { get; set; }
        public bool StaleMate { get; set; }
        public Player Player { get; set; }

        private readonly Game _game;

        public GameLogic(Game game)
        {
            this._game = game;
            FromSquare = null;
            ToSquare = null;
            Player = game.CurrentPlayer;
        }

        /*
         * The main function of the class. Flags the game states and calls methods for movement.
         * This would be called in the main loop of an application. inPlay variable is used
         * to check if the game is in checkmate or stalement without having to go through
         * all the movement logic. 
         */
        public void HandleGame(int row, int col, bool inPlay = false)
        {
            UpdateMovement = false;
            Promotion = false;
            CheckMate = false;
            StaleMate = false;

            // When HandleMovement is false, it is either waiting for a To square selection,
            // or an invalid move has been made
            if (!inPlay && HandleMovement(row, col))
            {
                // flags that a promotion needs to be done
                if (_game.ml.IsPromotion(ToSquare))
                {
                    Promotion = true;
                }

                // Check the state of the game
                CheckInPlay(Player.Color);

                // Player turn has ended and a valid move has been made
                Player = _game.NextPlayer();
            }
            if (inPlay)
            {
                UpdateMovement = true;
                var color = Player.Color == ChessColor.Black ? ChessColor.White : ChessColor.Black;
                CheckInPlay(color);
            }
        }

        /*
         * Determines whether a given row or column is a From square or a To square
         * Returns true if a player has moved, false otherwise
         */
        private bool HandleMovement(int row, int col)
        {
            Player = _game.CurrentPlayer;

            // Need to determine the From square. Not set if selection is not a piece or not the right color
            if (FromSquare == null)
            {
                FromSquare = _game.gb.squares[row, col];
                if (_game.gb.squares[row, col].Piece == null)
                {
                    FromSquare = null;
                }
                else if (_game.gb.squares[row, col].Piece.Color != Player.Color)
                {
                    FromSquare = null;
                }
            }

            // Need to determine the To square. Only done after a From square has been set
            else
            {
                // if the selected To Square is the same color, set it to From square instead.
                // not totally necessary, but nice for the user experience so they don't have to
                // go through additional inputs to select another piece
                if (_game.gb.squares[row, col].Piece != null)
                {
                    if (_game.gb.squares[row, col].Piece.Color == Player.Color)
                    {
                        FromSquare = _game.gb.squares[row, col];
                        ToSquare = null;
                    }
                    else
                    {
                        ToSquare = _game.gb.squares[row, col];
                    }
                }
                else
                {
                    ToSquare = _game.gb.squares[row, col];
                }
            }

            // only attempt to move once a From square and To square has been selected
            if (FromSquare != null && ToSquare != null)
            {
                // If the move can be made, update the view. Otherwise, reset selection process. It wasn't valid
                if (Player.Move(_game.ml, FromSquare, ToSquare))
                {
                    UpdateMovement = true;
                    return true;
                }
                else
                {
                    FromSquare = null;
                    ToSquare = null;
                }
            }
            return false;
        }

        /*
         * Sets the flags for whether the state of the game is in checkmate or stalemate
         */
        public void CheckInPlay(ChessColor color)
        {
            if (!_game.InPlay(color))
            {
                if (_game.State == State.CheckMate)
                {
                    CheckMate = true;
                }
                else
                {
                    StaleMate = true;
                }
            }
        }
    }
}
