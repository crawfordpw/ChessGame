using System.ComponentModel;

namespace ChessModel
{
    public class Square : INotifyPropertyChanged
    {
        public int ColID { get; set; }
        public int RowID { get; set; }
        public ChessColor Color { get; set; }
        public IPiece Piece { get; set; }
        public string Cord { get; set; }

        public Square() : this(0, 0)
        {

        }

        public Square(int row, int col)
        {
            RowID = row;
            ColID = col;
            Cord = $"{row}{col}";
        }

        public void MakeSameCord()
        {
            Piece.ColID = ColID;
            Piece.RowID = RowID;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
