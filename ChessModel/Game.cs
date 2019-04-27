namespace ChessModel
{
    public class Game
    {
        public GameBoard gb;
        public GameLogic gl;
        public Player CurrentPlayer { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public State State { get { return gl.gs.State; } }

        public void NewGame()
        {
            gb = new GameBoard();
            gl = new GameLogic(gb);
            Player1 = new Human(ChessColor.White);
            Player2 = new Human(ChessColor.Black);
            CurrentPlayer = Player1;
        }

        public bool InPlay(ChessColor color)
        {
            color = color == ChessColor.White ? ChessColor.Black : ChessColor.White;
            return gl.gs.InPlay(gb, color);
        }

        public Player NextPlayer()
        {
            return CurrentPlayer = CurrentPlayer.Color == ChessColor.White ? Player2 : Player1;
        }
    }
}
