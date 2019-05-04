using System.ComponentModel;
using System.Windows.Controls.Primitives;
using ChessModel;

namespace UI
{
    public class GameLogicViewModel : INotifyPropertyChanged
    {
        public bool UpdateMovement { get; set; }
        public bool Promotion { get; set; }
        public bool CheckMate { get; set; }
        public bool StaleMate { get; set; }

        public Square FromSquare { get; set; }
        public Square ToSquare { get; set; }
        public ToggleButton LastButton { get; set; }

        private readonly Game _game;
        private string _player1Clock;
        private string _player2Clock;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Player1Clock {
            get { return _player1Clock; }
            set {
                _player1Clock = value;
                OnPropertyChanged("Player1Clock");
            }
        }

        public string Player2Clock {
            get { return _player2Clock; }
            set {
                _player2Clock = value;
                OnPropertyChanged("Player2Clock");
            }
        }

        public GameLogicViewModel(Game game, string format)
        {
            _game = game;
            Player1Clock = game.Player1.Clock.TimeRemaining.ToString(format);
            Player2Clock = game.Player2.Clock.TimeRemaining.ToString(format);
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

        public void UpdateClock(string format)
        {
            Player1Clock = _game.Player1.Clock.TimeRemaining.ToString(format);
            Player2Clock = _game.Player2.Clock.TimeRemaining.ToString(format);
        }

        // Create the OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
