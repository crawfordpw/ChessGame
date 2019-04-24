namespace ChessModel
{
    public class Square
    {
        public int ColID { get; set; }
        public int RowID { get; set; }
        public bool HasPiece { get; set; }
        public IPiece Piece { get; set; }

        public Square()
        {

        }

        public Square(int row, int col)
        {
            RowID = row;
            ColID = col;
        }

        public void MakeSameCord()
        {
            Piece.ColID = ColID;
            Piece.RowID = RowID;
        }
    }
}
