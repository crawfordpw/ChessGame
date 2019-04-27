using ChessModel.Pieces;

namespace ChessModel
{
    public class MoveLogic
    {
        public static Square[] lastMove = new Square[5];
        public GameBoard gameboard;
        public GameState gs;

        public bool isEnPassant;
        public bool isCastle;
        private bool isCapture;

        public MoveLogic(GameBoard gameboard)
        {
            gs = new GameState(this);
            this.gameboard = gameboard;
            lastMove[0] = gameboard.squares[4, 4];
            lastMove[1] = gameboard.squares[4, 4];
        }

        public bool MovePiece(Square fromSquare, Square toSquare)
        {
            if (fromSquare.Piece.IsValidMove(gameboard, fromSquare, toSquare))
            {
                isEnPassant = false;
                isCastle = false;
                isCapture = false;

                if (!MoveValidator.IsEnemy(fromSquare, toSquare))
                {
                    if (MoveValidator.IsEnPassant(fromSquare, toSquare))
                        EnPassant(fromSquare, toSquare);
                    else if (MoveValidator.IsCastle(gameboard, fromSquare, toSquare))
                        Castle(fromSquare, toSquare);
                    else
                        gameboard.MovePiece(fromSquare, toSquare);
                }
                else
                {
                    Capture(fromSquare, toSquare);
                }
                lastMove[0] = fromSquare;
                lastMove[1] = toSquare;

                if (gs.Check(gameboard, toSquare.Piece.Color, isCastle))
                {
                    Undo();
                    return false;
                }

                if (isCapture)
                {
                    gameboard.pieces.Remove(toSquare.Piece);
                }

                return true;
            }
            return false;
        }

        public void Capture(Square fromSquare, Square toSquare)
        {
            isCapture = true;
            gameboard.RemovePieceTemp(toSquare);
            gameboard.MovePiece(fromSquare, toSquare);
        }

        private void EnPassant(Square fromSquare, Square toSquare)
        {
            isEnPassant = true;
            lastMove[2] = lastMove[1];
            gameboard.RemovePieceTemp(lastMove[1]);
            gameboard.MovePiece(fromSquare, toSquare);
        }

        private void Castle(Square fromSquare, Square toSquare)
        {
            isCastle = true;
            int fromRow = fromSquare.RowID;
            if (toSquare.ColID == 2)
            {
                lastMove[3] = gameboard.squares[fromRow, 3];
                lastMove[4] = gameboard.squares[fromRow, 0];
                gameboard.MovePiece(fromSquare, toSquare);
                gameboard.MovePiece(gameboard.squares[fromRow, 0], lastMove[3]);
            }
            else
            {
                lastMove[3] = gameboard.squares[fromRow, 5];
                lastMove[4] = gameboard.squares[fromRow, 7];
                gameboard.MovePiece(fromSquare, toSquare);
                gameboard.MovePiece(gameboard.squares[fromRow, 7], lastMove[3]);
            }
        }

        public void Undo()
        {
            Square fromMove = gameboard.squares[lastMove[1].RowID, lastMove[1].ColID];
            Square toMove = gameboard.squares[lastMove[0].RowID, lastMove[0].ColID];
            gameboard.MovePiece(fromMove, toMove);
            toMove.Piece.MoveCount -= 2;

            if (isEnPassant)
            {
                var capturedPiece = gameboard.pieces.Find(e => e.ColID == lastMove[2].ColID && e.RowID == lastMove[2].RowID);
                gameboard.squares[capturedPiece.RowID, capturedPiece.ColID].Piece = capturedPiece;
                isEnPassant = false;
            }
            else if (isCastle)
            {
                fromMove = gameboard.squares[lastMove[3].RowID, lastMove[3].ColID];
                if (lastMove[3].ColID == 3)
                {
                    toMove = gameboard.squares[lastMove[3].RowID, 0];
                }
                else
                {
                    toMove = gameboard.squares[lastMove[3].RowID, 7];
                }
                gameboard.MovePiece(fromMove, toMove);
                toMove.Piece.MoveCount -= 2;
                isCastle = false;
            }
            else if (isCapture)
            {
                var capturedPiece = gameboard.pieces.Find(e => e.ColID == fromMove.ColID && e.RowID == fromMove.RowID && e != lastMove[1].Piece);
                gameboard.squares[capturedPiece.RowID, capturedPiece.ColID].Piece = capturedPiece;
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

            gameboard.RemovePiece(square);
            gameboard.PlacePiece(piece, square);
        }
    }
}