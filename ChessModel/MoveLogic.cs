using ChessModel.Pieces;

namespace ChessModel
{
    /*
     * The Move Logic class is responsible for making legal moves, promoting a pawn, 
     * castling, en passant, capturing, and for Undo-ing a move.
     */
    public class MoveLogic
    {
        /*
         * Stores the last move made. Index 0 is From square, Index 1 is To square
         * Index 2 stores the location of the en passant captured square
         * Index 3 is the castled Rook From square
         * Index 4 is the castled Rook To square
        */
        public static Square[] lastMove = new Square[6];
        public GameBoard gb;
        public GameState gs;

        // These 3 variable are for determining if one of these moves happened. Useful for Undo
        public bool isEnPassant;
        public bool isCastle;
        public bool isCapture;

        public MoveLogic(GameBoard gb)
        {
            gs = new GameState(this);
            this.gb = gb;
            for(int i = 0; i < lastMove.Length; i++)
            {
                lastMove[i] = gb.squares[4, 4];
            }
        }

        /*
         * The main logic for determing whether a legal move has been requested. If so, 
         * return true and moves that piece along with any another pieces that need moving or
         * removing. If not, return false and does nothing.
         */
        public bool MovePiece(Square fromSquare, Square toSquare, bool debug = false)
        {
            // Checks if the piece is a Valid Move
            if (fromSquare.Piece.IsValidMove(gb, fromSquare, toSquare))
            {
                isEnPassant = false;
                isCastle = false;
                isCapture = false;

                // Uses the Move Validator in helping to determine En Passant or Castle and does so. 
                // If not, it must be either a move to an empty space or a capturing move.
                if (!MoveValidator.IsEnemy(fromSquare, toSquare))
                {
                    if (MoveValidator.IsEnPassant(gb, fromSquare, toSquare))
                        EnPassant(fromSquare, toSquare);
                    else if (MoveValidator.IsCastle(gb, fromSquare, toSquare))
                        Castle(fromSquare, toSquare);
                    else
                        gb.MovePiece(fromSquare, toSquare);
                }
                else
                {
                    Capture(fromSquare, toSquare);
                }               
                lastMove[0] = fromSquare;
                lastMove[1] = toSquare;

                // If the Move that was made left own King in Check, it is illegal. Undo move and return false
                if (gs.Check(gb, toSquare.Piece.Color, isCastle))
                {
                    Undo();
                    return false;
                }

                // Due to a possibility of an Undo, a capture or en passant doesn't completely remove a piece beforehand. Here it does remove it
                if (isCapture && !debug)
                {
                    var color = toSquare.Piece.Color;
                    IPiece findPiece = gb.pieces.Find(e => e.ColID == toSquare.Piece.ColID && e.RowID == toSquare.Piece.RowID && e.Color != color);
                    gb.pieces.Remove(findPiece);
                }
                if (isEnPassant && !debug)
                {
                    gb.pieces.Remove(lastMove[1].Piece);
                }

                return true;
            }
            return false;
        }

        /*
         * Logic for capturing a piece. Doesn't completely remove a piece from pieces array in the Gameboard class due to
         * a possibility of an Undo
         */
        public void Capture(Square fromSquare, Square toSquare)
        {
            isCapture = true;
            gb.RemovePieceTemp(toSquare);
            gb.MovePiece(fromSquare, toSquare);
        }

        /*
         * Logic for En Passant movement. Doesn't completely remove a piece from pieces array in the Gameboard class due to
         * a possibility of an Undo
         */
        private void EnPassant(Square fromSquare, Square toSquare)
        {
            isEnPassant = true;
            gb.RemovePieceTemp(lastMove[2]);
            gb.MovePiece(fromSquare, toSquare);
        }

        /*
         * Logic for Castling movement. 
         */
        private void Castle(Square fromSquare, Square toSquare)
        {
            isCastle = true;
            int fromRow = fromSquare.RowID;

            // Castle to the left
            if(toSquare.ColID == 2)
            {
                lastMove[4] = gb.squares[fromRow, 0];
                lastMove[3] = gb.squares[fromRow, 3];
                gb.MovePiece(fromSquare, toSquare);
                gb.MovePiece(gb.squares[fromRow, 0], lastMove[3]);
            }
            // Castle to the right
            else
            {
                lastMove[3] = gb.squares[fromRow, 5];
                lastMove[4] = gb.squares[fromRow, 7];
                gb.MovePiece(fromSquare, toSquare);
                gb.MovePiece(gb.squares[fromRow, 7], lastMove[3]);
            }
        }

        /*
         * Undo the last move made. This is entirely dependent on whether that move was a 
         * capture, en passant, castle, or move to empty space. 
         */
        public void Undo()
        {
            // Move back the piece that moved and resets the Movecount 
            Square fromMove = gb.squares[lastMove[1].RowID, lastMove[1].ColID];
            Square toMove = gb.squares[lastMove[0].RowID, lastMove[0].ColID];
            gb.MovePiece(fromMove, toMove);
            toMove.Piece.MoveCount -= 2;

            // Need to put the captured piece back on the board
            if (isEnPassant)
            {
                var capturedPiece = gb.pieces.Find(e => e.ColID == lastMove[2].ColID && e.RowID == lastMove[2].RowID);
                gb.squares[capturedPiece.RowID, capturedPiece.ColID].Piece = capturedPiece;
                isEnPassant = false;
            }

            // Need to move the Rook back to it's position
            else if (isCastle)
            {
                fromMove = gb.squares[lastMove[3].RowID, lastMove[3].ColID];

                // last move was a castle to the left
                if (lastMove[3].ColID == 3){                   
                    toMove = gb.squares[lastMove[3].RowID, 0];
                }
                // otherwise it was a castle to the right
                else
                {
                    toMove = gb.squares[lastMove[3].RowID, 7];
                }
                gb.MovePiece(fromMove, toMove);
                toMove.Piece.MoveCount -= 2;
                isCastle = false;
            }

            // Need to put the captured piece back on the board
            else if (isCapture)
            {               
                var capturedPiece = gb.pieces.Find(e => e.ColID == fromMove.ColID && e.RowID == fromMove.RowID);
                gb.squares[capturedPiece.RowID, capturedPiece.ColID].Piece = capturedPiece;
                isCapture = false;
            }
        }

        /*
         * Checks if a Pawn makes it to the other side of the board.
         */
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

        /*
         * A Pawn can be promoted to a Queen, Knight, Rook, or Bishop
         */
        public void Promote(Square square, Promotion selection = Promotion.Queen)
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

            gb.RemovePiece(square);
            gb.PlacePiece(piece, square);
        }
    }
}