namespace ChessModel.Pieces
{
    public enum ChessPiece
    {
        None,
        Pawn,
        Knight,
        Bishop,
        Rook,
        Queen,
        King
    }

    public enum ChessColor
    {
        Black,
        White
    }

    public enum State
    {
        InPlay,
        Check, 
        CheckMate,
        StaleMate
    }

    public enum Promotion
    {
        Queen,
        Knight,
        Rook,
        Bishop
    }
}
