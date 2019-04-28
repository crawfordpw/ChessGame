using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessModel;

namespace UI
{
    public class GameLogicViewModel
    {
        private Game _game;

        public bool UpdateMovement { get; set; }
        public bool Promotion { get; set; }
        public bool CheckMate { get; set; }
        public bool StaleMate { get; set; }

        public GameLogicViewModel(Game game)
        {
            _game = game;
        }

        public void HandleGame(Game game, int row, int col)
        {
            game.gl.HandleGame(row, col);
        }

        public void Promote(Square square)
        {
            _game.ml.Promote(square);
        }

        public void Update()
        {
            UpdateMovement = _game.gl.UpdateMovement;
            Promotion = _game.gl.Promotion;
            CheckMate = _game.gl.CheckMate;
            StaleMate = _game.gl.StaleMate;
        }
    }
}
