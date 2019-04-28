using ChessModel.Pieces;

namespace ChessModel
{
    public class GameLogic
    {
        public static Square[] lastMove = new Square[6];
        public GameBoard gameBoard;
        public GameState gs;

        public bool isEnPassant;
        public bool isCastle;
        public bool isCapture;

        public GameLogic(GameBoard gameBoard)
        {
            gs = new GameState(this);
            this.gameBoard = gameBoard;
            lastMove[0] = gameBoard.squares[4, 4];
            lastMove[1] = gameBoard.squares[4, 4];
            lastMove[2] = gameBoard.squares[4, 4];
            lastMove[3] = gameBoard.squares[4, 4];
            lastMove[4] = gameBoard.squares[4, 4];
        }

        public bool MovePiece(Square fromSquare, Square toSquare)
        {
            if (fromSquare.Piece.IsValidMove(gameBoard, fromSquare, toSquare))
            {
                isEnPassant = false;
                isCastle = false;
                isCapture = false;

                if (!MoveValidator.IsEnemy(fromSquare, toSquare))
                {
                    if (MoveValidator.IsEnPassant(gameBoard, fromSquare, toSquare))
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

                if (gs.Check(gameBoard, toSquare.Piece.Color, isCastle))
                {
                    Undo();
                    return false;
                }

                if (isCapture)
                {
                    gameBoard.pieces.Remove(toSquare.Piece);
                }
                if (isEnPassant)
                {
                    gameBoard.pieces.Remove(lastMove[1].Piece);
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
            //lastMove[2] = lastMove[1];
            gameBoard.RemovePieceTemp(lastMove[2]);
            gameBoard.MovePiece(fromSquare, toSquare);
        }

        private void Castle(Square fromSquare, Square toSquare)
        {
            isCastle = true;
            int fromRow = fromSquare.RowID;
            if(toSquare.ColID == 2)
            {
                lastMove[3] = gameBoard.squares[fromRow, 3];
                lastMove[4] = gameBoard.squares[fromRow, 0];
                gameBoard.MovePiece(fromSquare, toSquare);
                gameBoard.MovePiece(gameBoard.squares[fromRow, 0], lastMove[3]);
            }
            else
            {
                lastMove[3] = gameBoard.squares[fromRow, 5];
                lastMove[4] = gameBoard.squares[fromRow, 7];
                gameBoard.MovePiece(fromSquare, toSquare);
                gameBoard.MovePiece(gameBoard.squares[fromRow, 7], lastMove[3]);
            }
        }

        public void Undo()
        {
            Square fromMove = gameBoard.squares[lastMove[1].RowID, lastMove[1].ColID];
            Square toMove = gameBoard.squares[lastMove[0].RowID, lastMove[0].ColID];
            gameBoard.MovePiece(fromMove, toMove);
            toMove.Piece.MoveCount -= 2;

            if (isEnPassant)
            {
                var capturedPiece = gameBoard.pieces.Find(e => e.ColID == lastMove[2].ColID && e.RowID == lastMove[2].RowID);
                if(capturedPiece.Color == ChessColor.White)
                    gameBoard.squares[capturedPiece.RowID, capturedPiece.ColID].Piece = capturedPiece;
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
                toMove.Piece.MoveCount -= 2;
                isCastle = false;
            }
            else if(isCapture)
            {               
                var capturedPiece = gameBoard.pieces.Find(e => e.ColID == fromMove.ColID && e.RowID == fromMove.RowID && e != lastMove[1].Piece);
                gameBoard.squares[capturedPiece.RowID, capturedPiece.ColID].Piece = capturedPiece;
                isCapture = false;
            }
        }

        public bool IsPromotion(Square square)
        {
            if (square.Piece.Type != ChessPiece.Pawn)
            {
                return false;
            }
            else if (square.Piece.Color == ChessColor.White && square.RowID == 7)
            {
                return true;
            }
            else if (square.Piece.Color == ChessColor.Black && square.RowID == 0)
            {
                return true;
            }
            return false;
        }

        public void Promote(Square square, Promotion selection)
        {
            IPiece piece;
            ChessColor color = square.Piece.Color;

            if (selection == Promotion.Queen)
            {
                piece = new Queen(color);                
            }
            else if (selection == Promotion.Knight)
            {
                piece = new Knight(color);
            }
            else if (selection == Promotion.Rook)
            {
                piece = new Rook(color);
            }
            else
            {
                piece = new Bishop(color);
            }

            gameBoard.RemovePiece(square);
            gameBoard.PlacePiece(piece, square);
        }
    }
}