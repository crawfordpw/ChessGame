using System;
using System.Collections.Generic;
using System.Text;

namespace ChessModel
{
    public class GameLogic
    {
        public bool GameEnd { get; set; }
        private Player Player { get; set; }
        public Square FromSquare { get; set; }
        public Square ToSquare { get; set; }
        public bool UpdateMovement { get; set; }
        private Game _game;

        public GameLogic(Game game)
        {
            this._game = game;
            FromSquare = null;
            ToSquare = null;
            Player = game.CurrentPlayer;
        }

        // this logic is not used is this application due to the way updating the board works.
        // something similar has been implemented in the board user control.
        // it is here, however, for other UI's
        private bool HandleGame(int row, int col)
        {
            if (HandleMovement(row, col))
            {
                if (_game.ml.IsPromotion(_game.gl.ToSquare))
                {
                    _game.ml.Promote(_game.gl.ToSquare);
                }

                FromSquare = null;
                ToSquare = null;
                if (!_game.InPlay(Player.Color))
                {
                    _game.EndGame();
                }
                Player = _game.NextPlayer();
                return true;
            }
            return false;
        }

        public bool HandleMovement(int row, int col)
        {
            UpdateMovement = false;
            Player = _game.CurrentPlayer;
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
            else
            {
                ToSquare = _game.gb.squares[row, col];
            }

            if (FromSquare != null && ToSquare != null)
            {
                if (Player.Move(_game.ml, FromSquare, ToSquare))
                {
                    UpdateMovement = true;
                    return true;
                }
            }
            return false;
        }
    }
}
