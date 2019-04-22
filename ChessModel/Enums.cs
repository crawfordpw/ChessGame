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

    public enum GameState
    {
        InPlay,
        Check, 
        CheckMate,
        StaleMate
    }
}
