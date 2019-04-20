namespace ChessModel
{
    public class Square
    {
        public int ColID { get; set; }
        public int RowID { get; set; }
        public bool HasPiece { get; set; }
        public IPiece piece { get; set; }

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
            piece.ColID = ColID;
            piece.RowID = RowID;
        }
    }
}
