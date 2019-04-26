namespace ChessModel
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
        None,
        Black,
        White, 
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
