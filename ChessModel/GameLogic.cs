using System;
using System.Collections.Generic;
using System.Text;

namespace ChessModel
{
    public class GameLogic
    {
        public Square FromSquare { get; set; }
        public Square ToSquare { get; set; }
        public bool UpdateMovement { get; set; }
        public bool Promotion { get; set; }
        public bool CheckMate { get; set; }
        public bool StaleMate { get; set; }
        private Game _game;
        private Player Player { get; set; }

        public GameLogic(Game game)
        {
            this._game = game;
            FromSquare = null;
            ToSquare = null;
            Player = game.CurrentPlayer;
        }

        public void HandleGame(int row, int col)
        {
            UpdateMovement = false;
            Promotion = false;
            CheckMate = false;
            StaleMate = false;
            if (HandleMovement(row, col))
            {
                if (_game.ml.IsPromotion(ToSquare))
                {
                    Promotion = true;
                }               

                if (!_game.InPlay(Player.Color))
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
                Player = _game.NextPlayer();
            }
        }

        private bool HandleMovement(int row, int col)
        {
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
                else
                {
                    FromSquare = null;
                    ToSquare = null;
                }
            }
            return false;
        }
    }
}
