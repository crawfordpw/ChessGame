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
using ChessModel.Pieces;

namespace UI
{
    /// <summary>
    /// Interaction logic for Board.xaml
    /// </summary>
    public partial class UCChessBoard : UserControl
    {
        ObservableCollection<SquareViewModel> ChessBoard { get; set; }
        private Player Player { get; set; }
        private Game game { get; set; }
        private Square FromSquare { get; set; }
        private Square ToSquare { get; set; }

        public UCChessBoard()
        {
            InitializeComponent();
            FromSquare = null;
            ToSquare = null;
            game = new Game();
            game.NewGame();
            game.gb.MovePiece(game.gb.squares[1, 4], game.gb.squares[4, 4]);

            Player = game.CurrentPlayer;

            ChessBoard = new ObservableCollection<SquareViewModel>();
            ConvertToList(game, ChessBoard);
        }

        public void ConvertToList(Game game, ObservableCollection<SquareViewModel> ChessBoard)
        {
            for (int row = 7; row > -1; row--)
            {
                for (int col = 0; col < 8; col++)
                {
                    ChessBoard.Add(new SquareViewModel(game.gb.squares[row, col]));
                }
            }
            Board.ItemsSource = ChessBoard;
        }

        public void HandleGame(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var tag = button.Tag.ToString();
            int row = (int)Char.GetNumericValue(tag[0]);
            int col = (int)Char.GetNumericValue(tag[1]);

            if(HandleMovement(row, col))
            {
                if (!game.InPlay(Player.Color))
                {
                    if (game.State == State.CheckMate)
                    {
                        MessageBox.Show("Checkmate");
                    }
                    else if(game.State == State.StaleMate)
                    {
                        MessageBox.Show("Stalemate");
                    }
                }
                Player = game.NextPlayer();
            }
        }

        private bool HandleMovement(int row, int col)
        {
            if (FromSquare == null)
            {
                FromSquare = game.gb.squares[row, col];
                if (game.gb.squares[row, col].Piece == null)
                {
                    FromSquare = null;
                }
                else if (game.gb.squares[row, col].Piece.Color != Player.Color)
                {
                    FromSquare = null;
                }
            }
            else
            {
                ToSquare = game.gb.squares[row, col];

                if (ToSquare != null)
                {
                    if (FromSquare.Piece.Color == Player.Color)
                    {
                        if (Player.Move(game.gl, FromSquare, ToSquare))
                        {
                            UpdateMovement();
                            return true;
                        }
                        FromSquare = null;
                        ToSquare = null;
                    }
                }
            }
            return false;
        }

        private void UpdateMovement()
        {
            //var from =
            //    from SquareViewModel square in ChessBoard
            //    where square.Cord == FromSquare.Cord
            //    select square;
            //var to =
            //    from SquareViewModel square in ChessBoard
            //    where square.Cord == ToSquare.Cord
            //    select square;

            //from.ToList()[0].Update(FromSquare);
            //to.ToList()[0].Update(ToSquare);
            //if (game.gl.isEnPassant)
            //{
            //    from =
            //       from SquareViewModel square in ChessBoard
            //       where square.Cord == GameLogic.lastMove[2].Cord
            //       select square;
            //    from.ToList()[0].Update(GameLogic.lastMove[2]);
            //}
            //if (game.gl.isCastle)
            //{
            //    from =
            //       from SquareViewModel square in ChessBoard
            //       where square.Cord == GameLogic.lastMove[3].Cord
            //       select square;
            //    to =
            //       from SquareViewModel square in ChessBoard
            //       where square.Cord == GameLogic.lastMove[4].Cord
            //       select square;
            //    from.ToList()[0].Update(GameLogic.lastMove[3]);
            //    to.ToList()[0].Update(GameLogic.lastMove[4]);
            //}
            FromSquare = null;
            ToSquare = null;
            ChessBoard.Clear();
            ConvertToList(game, ChessBoard);
        }
    }
}