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
        private Game Game { get; set; }
        private GameLogicViewModel GameLogicViewModel { get; set; }

        public UCChessBoard()
        {
            InitializeComponent();
            NewGame();                         
        }

        private void NewGame()
        {
            Game = new Game();
            Game.NewGame();
            GameLogicViewModel = new GameLogicViewModel(Game);
            ChessBoard = new ObservableCollection<SquareViewModel>();
            ConvertToList(Game, ChessBoard);
        }
        
        private void ConvertToList(Game game, ObservableCollection<SquareViewModel> ChessBoard)
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

        public void ChessBoardHandleGame(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var tag = button.Tag.ToString();
            int row = (int)Char.GetNumericValue(tag[0]);
            int col = (int)Char.GetNumericValue(tag[1]);

            GameLogicViewModel.HandleGame(row, col);
            var ToSquare = Game.gl.ToSquare;
            GameLogicViewModel.Update();

            if (GameLogicViewModel.Promotion)
            {
                // switch color since NextPLayer would've already been called
                var color = Game.CurrentPlayer.Color == ChessColor.Black ? ChessColor.White : ChessColor.Black;
                var PromotionWindow = new PromotionWindow(color)
                {
                    Owner = Window.GetWindow(this)
                };

                PromotionWindow.ShowDialog();
                var selection = PromotionWindow.Selection;
                GameLogicViewModel.Promote(ToSquare, selection);
                GameLogicViewModel.HandleGame(row, col, true);
                GameLogicViewModel.Update();
            }
            if (GameLogicViewModel.UpdateMovement)
            {
                UpdateMovement();
            }
            if (GameLogicViewModel.CheckMate)
            {
                string color = Game.CurrentPlayer.Color == ChessColor.Black ? "White" : "Black";
                EndGameWindow($"Checkmate! {color} Wins!");
            }
            else if (GameLogicViewModel.StaleMate)
            {
                EndGameWindow("Stalemate!");
            }

        }
        private void UpdateMovement()
        {
            var FromSquare = Game.gl.FromSquare;
            var ToSquare = Game.gl.ToSquare;

            var from =
                from SquareViewModel square in ChessBoard
                where square.Cord == FromSquare.Cord
                select square;
            var to =
                from SquareViewModel square in ChessBoard
                where square.Cord == ToSquare.Cord
                select square;

            from.ToList()[0].Update(FromSquare);
            to.ToList()[0].Update(ToSquare);
            if (Game.ml.isEnPassant)
            {
                from =
                   from SquareViewModel square in ChessBoard
                   where square.Cord == MoveLogic.lastMove[2].Cord
                   select square;
                from.ToList()[0].Update(MoveLogic.lastMove[2]);
            }
            else if (Game.ml.isCastle)
            {
                from =
                   from SquareViewModel square in ChessBoard
                   where square.Cord == MoveLogic.lastMove[3].Cord
                   select square;
                to =
                   from SquareViewModel square in ChessBoard
                   where square.Cord == MoveLogic.lastMove[4].Cord
                   select square;
                from.ToList()[0].Update(MoveLogic.lastMove[3]);
                to.ToList()[0].Update(MoveLogic.lastMove[4]);
            }
            //ChessBoard.Clear();
            //ConvertToList(Game, ChessBoard);
            Game.gl.FromSquare = null;
            Game.gl.ToSquare = null;
        }

        private void EndGameWindow(string message)
        {
            var EndGameWindow = new EndGameWindow(message)
            {
                Owner = Window.GetWindow(this)
            };
            EndGameWindow.ShowDialog();
            var selection = EndGameWindow.Selection;

            switch (selection)
            {
                case "NewGame":
                    NewGame();
                    break;
                case "EndGame":
                    Window.GetWindow(this).Close();
                    break;
            }
        }
    }
}