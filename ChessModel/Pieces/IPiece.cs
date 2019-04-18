using ChessModel.Pieces;

namespace ChessModel
{
    public interface IPiece
    {
        ChessPiece Type { get; set; }
        ChessColor Color { get; set; }
        int PosCol { get; set; }
        int PosRow { get; set; }
        bool Alive { get; set; }
    }
}
