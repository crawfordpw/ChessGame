using System.Windows.Controls.Primitives;
using ChessModel;

namespace UI
{
    public class GameLogicViewModel
    {
        private readonly Game _game;

        public bool UpdateMovement { get; set; }
        public bool Promotion { get; set; }
        public bool CheckMate { get; set; }
        public bool StaleMate { get; set; }

        public Square FromSquare { get; set; }
        public Square ToSquare { get; set; }

        public ToggleButton LastButton { get; set; }

        public GameLogicViewModel(Game game)
        {
            _game = game;
            LastButton = new ToggleButton();
        }

        public void HandleGame(ToggleButton button, int row, int col, bool inPlay = false)
        {
            LastButton = button;
            _game.gl.HandleGame(row, col, inPlay);
        }

        public void Promote(Square square, Promotion selection)
        {
            _game.ml.Promote(square, selection);
        }

        public void Update()
        {
            UpdateMovement = _game.gl.UpdateMovement;
            Promotion = _game.gl.Promotion;
            CheckMate = _game.gl.CheckMate;
            StaleMate = _game.gl.StaleMate;
            FromSquare = _game.gl.FromSquare;
            ToSquare = _game.gl.ToSquare;
        }
    }
}
