using ChessModel.Pieces;

namespace ChessModel
{
    public interface IPiece
    {
        ChessPiece Type { get; set; }
        ChessColor Color { get; set; }
        int ColID { get; set; }
        int RowID { get; set; }

        void AddAtLocation(int row, int col);
        bool IsValidMove(GameBoard gameboard);
    }
}
