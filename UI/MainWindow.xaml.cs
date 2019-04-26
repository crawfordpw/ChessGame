using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChessModel;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Square> ChessBoard { get; set; }
        private Game game { get; set; }
        public Square FromSquare { get; set; }
        public Square ToSquare { get; set; }

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();

            FromSquare = null;
            ToSquare = null;
            game = new Game();
            game.NewGame();

            ChessBoard = new ObservableCollection<Square>();
            ConvertToList(game, ChessBoard);
        }

        public void ConvertToList(Game game, ObservableCollection<Square> ChessBoard)
        {
            for(int row = 7; row > -1; row--)
            {
                for (int col = 0; col < 8; col++)
                {
                    ChessBoard.Add(game.gb.squares[row, col]);
                }
            }
            Board.ItemsSource = ChessBoard;
        }

        public void HandleGame(object sender, RoutedEventArgs e)
        {
            HandleMovement(sender);
        }

        private void HandleMovement(object sender)
        {
            var button = (Button)sender;
            var tag = button.Tag.ToString();
            int row = (int)Char.GetNumericValue(tag[0]);
            int col = (int)Char.GetNumericValue(tag[1]);

            if (FromSquare == null)
            {
                FromSquare = game.gb.squares[row, col];
                if (game.gb.squares[row, col].Piece == null)
                {
                    FromSquare = null;
                }
            }
            else
            {
                ToSquare = game.gb.squares[row, col];
            }

            if (FromSquare != null && ToSquare != null)
            {
                game.gl.MovePiece(FromSquare, ToSquare);
                FromSquare = null;
                ToSquare = null;
                ChessBoard.Clear();
                ConvertToList(game, ChessBoard);
            }           
        }
    }
}
