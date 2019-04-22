using ChessModel.Pieces;

namespace ChessModel
{
    public class GameLogic
    {
        public GameBoard gameBoard;

        public GameLogic()
        {

        }

        public GameLogic(GameBoard gameBoard)
        {
            this.gameBoard = gameBoard;
        }

        public bool HasGameEnded(State State)
        {
            if(State ==  State.CheckMate || State == State.StaleMate)
                return true;
            return false;
        }

        public bool MovePiece(Square fromSquare, Square toSquare)
        {
            if (fromSquare.piece.IsValidMove(gameBoard, fromSquare, toSquare))
            {
                if (!MoveValidator.IsEnemy(fromSquare, toSquare))
                {
                    gameBoard.MovePiece(fromSquare, toSquare);
                }
                else
                {
                    Capture(fromSquare, toSquare);
                }
                return true;
            }
            return false;
        }

        public void Capture(Square fromSquare, Square toSquare)
        {
            gameBoard.RemovePiece(toSquare);
            gameBoard.MovePiece(fromSquare, toSquare);
        }
    }
}