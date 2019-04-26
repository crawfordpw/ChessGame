namespace ChessModel
{
    public class Game
    {
        public GameBoard gb;
        public GameLogic gl;

        public void NewGame()
        {
            gb = new GameBoard();
            gl = new GameLogic(gb);
        }

    }
}
