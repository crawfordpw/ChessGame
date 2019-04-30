namespace ChessModel
{
    /*
     * An interface for creating each chess piece. Add at location is mainly a method used for
     * debugging purposes. Place piece in the Gameboard class is a more robust method. IsValidMove
     * defines how a piece should move.
     */
    public interface IPiece
    {
        ChessPiece Type { get; set; }
        ChessColor Color { get; set; }
        int ColID { get; set; }
        int RowID { get; set; }
        int MoveCount { get; set; }

        void AddAtLocation(int row, int col);
        bool IsValidMove(GameBoard gb, Square fromSquare, Square toSquare);
    }
}
