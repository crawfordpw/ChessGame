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
        public ObservableCollection<Square> ChessBoard { get; set; }

        public MainWindow()
        {
            this.DataContext = this;
            InitializeComponent();

            Game game = new Game();
            game.NewGame();

            this.ChessBoard = new ObservableCollection<Square>();
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
    }
}
