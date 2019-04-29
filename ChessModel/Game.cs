

namespace ChessModel
{
    public class Game
    {
        public GameBoard gb;
        public MoveLogic ml;
        public GameLogic gl;
        public Player CurrentPlayer { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public State State { get { return ml.gs.State; } }

        public void NewGame()
        {
            gb = new GameBoard();
            ml = new MoveLogic(gb);
            gl = new GameLogic(this);
            Player1 = new Human(ChessColor.White);
            Player2 = new Human(ChessColor.Black);
            CurrentPlayer = Player1;
        }

        public bool InPlay(ChessColor color)
        {
            color = color == ChessColor.White ? ChessColor.Black : ChessColor.White;
            return ml.gs.InPlay(gb, color);
        }

        public Player NextPlayer()
        {
            return CurrentPlayer = CurrentPlayer.Color == ChessColor.White ? Player2 : Player1;
        }

        public void EndGame()
        {
            System.Environment.Exit(1);
        }
    }
}
