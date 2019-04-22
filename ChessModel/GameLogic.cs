using ChessModel.Pieces;

namespace ChessModel
{
    public class GameLogic
    {
        public static Square[] lastMove = new Square[2];
        public GameBoard gameBoard;

        public GameLogic()
        {

        }

        public GameLogic(GameBoard gameBoard)
        {
            this.gameBoard = gameBoard;
            lastMove[0] = gameBoard.squares[4, 4];
            lastMove[1] = gameBoard.squares[4, 4];
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
                    if (MoveValidator.IsEnPassant(fromSquare, toSquare))
                        EnPassant(fromSquare, toSquare);
                    else if (MoveValidator.IsCastle(gameBoard, fromSquare, toSquare))
                        Castle(fromSquare, toSquare);
                    else
                        gameBoard.MovePiece(fromSquare, toSquare);
                }
                else
                {
                    Capture(fromSquare, toSquare);
                }
                lastMove[0] = fromSquare;
                lastMove[1] = toSquare;

                return true;
            }
            return false;
        }

        public void Capture(Square fromSquare, Square toSquare)
        {
            gameBoard.RemovePiece(toSquare);
            gameBoard.MovePiece(fromSquare, toSquare);
        }

        private void EnPassant(Square fromSquare, Square toSquare)
        {
            gameBoard.RemovePiece(lastMove[1]);
            gameBoard.MovePiece(fromSquare, toSquare);
        }

        private void Castle(Square fromSquare, Square toSquare)
        {
            int fromRow = fromSquare.RowID;
            if(toSquare.ColID == 2)
            {
                gameBoard.MovePiece(fromSquare, toSquare);
                gameBoard.MovePiece(gameBoard.squares[fromRow, 0], gameBoard.squares[fromRow, 3]);
            }
            else
            {
                gameBoard.MovePiece(fromSquare, toSquare);
                gameBoard.MovePiece(gameBoard.squares[fromRow, 7], gameBoard.squares[fromRow, 5]);
            }
        }
    }
}