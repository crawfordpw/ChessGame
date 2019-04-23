using ChessModel.Pieces;

namespace ChessModel
{
    public class GameLogic
    {
        public static Square[] lastMove = new Square[4];
        public GameBoard gameBoard;
        public GameState gs;

        private bool isEnPassant;
        private bool isCastle;
        private bool isCapture;

        public GameLogic()
        {

        }

        public GameLogic(GameBoard gameBoard)
        {
            gs = new GameState();
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

        public bool MovePiece(Square fromSquare, Square toSquare, bool Debug = false)
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

                if (gs.Check(gameBoard, isCastle))
                {
                    Undo();
                    return false;
                }

                if (!Debug)
                {
                    isEnPassant = false;
                    isCastle = false;
                    isCapture = false;
                }
                return true;
            }
            return false;
        }

        public void Capture(Square fromSquare, Square toSquare)
        {
            isCapture = true;
            gameBoard.RemovePieceTemp(toSquare);
            gameBoard.MovePiece(fromSquare, toSquare);
        }

        private void EnPassant(Square fromSquare, Square toSquare)
        {
            isEnPassant = true;
            lastMove[2] = lastMove[1];
            gameBoard.RemovePieceTemp(lastMove[1]);
            gameBoard.MovePiece(fromSquare, toSquare);
        }

        private void Castle(Square fromSquare, Square toSquare)
        {
            isCastle = true;
            int fromRow = fromSquare.RowID;
            if(toSquare.ColID == 2)
            {
                lastMove[3] = gameBoard.squares[fromRow, 3];
                gameBoard.MovePiece(fromSquare, toSquare);
                gameBoard.MovePiece(gameBoard.squares[fromRow, 0], gameBoard.squares[fromRow, 3]);
            }
            else
            {
                lastMove[3] = gameBoard.squares[fromRow, 5];
                gameBoard.MovePiece(fromSquare, toSquare);
                gameBoard.MovePiece(gameBoard.squares[fromRow, 7], gameBoard.squares[fromRow, 5]);
            }
        }

        public void Undo()
        {
            Square fromMove = gameBoard.squares[lastMove[1].RowID, lastMove[1].ColID];
            Square toMove = gameBoard.squares[lastMove[0].RowID, lastMove[0].ColID];
            gameBoard.MovePiece(fromMove, toMove);
            toMove.piece.MoveCount -= 2;

            if (isEnPassant)
            {
                var capturedPiece = gameBoard.pieces.Find(e => e.ColID == lastMove[2].ColID && e.RowID == lastMove[2].RowID);
                gameBoard.squares[capturedPiece.RowID, capturedPiece.ColID].piece = capturedPiece;
                isEnPassant = false;
            }
            else if (isCastle)
            {
                fromMove = gameBoard.squares[lastMove[3].RowID, lastMove[3].ColID];
                if (lastMove[3].ColID == 3){                   
                    toMove = gameBoard.squares[lastMove[3].RowID, 0];
                }
                else
                {
                    toMove = gameBoard.squares[lastMove[3].RowID, 7];
                }
                gameBoard.MovePiece(fromMove, toMove);
                toMove.piece.MoveCount -= 2;
                isCastle = false;
            }
            else if(isCapture)
            {               
                var capturedPiece = gameBoard.pieces.Find(e => e.ColID == fromMove.ColID && e.RowID == fromMove.RowID && e != lastMove[1].piece);
                gameBoard.squares[capturedPiece.RowID, capturedPiece.ColID].piece = capturedPiece;
                isCapture = false;
            }
        }
    }
}