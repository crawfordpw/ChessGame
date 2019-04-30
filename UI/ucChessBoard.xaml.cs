using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for UCChessBoard.xaml
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

        /*
         * Creates a new game and updates the UI view
         */
        private void NewGame()
        {
            Game = new Game();
            Game.NewGame();
            GameLogicViewModel = new GameLogicViewModel(Game);
            ChessBoard = new ObservableCollection<SquareViewModel>();
            ConvertToList(Game, ChessBoard);
        }
        
        /*
         * Adds all the chessboard squares to an Observable Collection for the ItemsSource to use in the xaml
         */
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

        /*
         * When a square in the UI is clicked, this method is called to Handle what is to be done
         */
        public void ChessBoardHandleGame(object sender, RoutedEventArgs e)
        {
            // converts the button coordinates to a row and column integer for the GameLogic ViewModel
            var button = (ToggleButton)sender;
            var tag = button.Tag.ToString();
            int row = (int)Char.GetNumericValue(tag[0]);
            int col = (int)Char.GetNumericValue(tag[1]);

            // Calls the View Model to handle the game and then updates the UI view
            GameLogicViewModel.LastButton.IsChecked = false;
            GameLogicViewModel.HandleGame(button, row, col);
            var ToSquare = Game.gl.ToSquare;
            GameLogicViewModel.Update();

            // When a promotion happens, promote the pawn and need the re-check if there is a
            // checkmate or stalemate. (since now there is a new piece on the board)
            if (GameLogicViewModel.Promotion)
            {
                // switch color since NextPLayer would've already been called
                var color = Game.CurrentPlayer.Color == ChessColor.Black ? ChessColor.White : ChessColor.Black;

                // prompts user for the pawn to promote to
                var PromotionWindow = new PromotionWindow(color)
                {
                    Owner = Window.GetWindow(this)
                };

                PromotionWindow.ShowDialog();
                var selection = PromotionWindow.Selection;
                GameLogicViewModel.Promote(ToSquare, selection);
                GameLogicViewModel.HandleGame(button, row, col, true);
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

            if(GameLogicViewModel.FromSquare == null)
            {
                button.IsChecked = false;
            }
            if (GameLogicViewModel.ToSquare != null)
            {
                button.IsChecked = false;               
            }

        }

        /*
         * Prompts the user what is to be done once the game has ended
         */
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

        /*
         * Updates the UI without having to clear and create a new Observable Collection
         * Instead, just updates the squares needed. May be a way to implement INotifyPropertyChanged
         * in each class that needs to update the ViewModel so this doesn't have to be done.
         * However, I am not sure how at the moment
         */
        private void UpdateMovement()
        {
            var FromSquare = Game.gl.FromSquare;
            var ToSquare = Game.gl.ToSquare;
            var FromIndex = ConvertCordToIndex(FromSquare.Cord);
            var ToIndex = ConvertCordToIndex(ToSquare.Cord);

            ChessBoard[FromIndex].Update(FromSquare);
            ChessBoard[ToIndex].Update(ToSquare);

            if (Game.ml.isEnPassant)
            {
                FromIndex = ConvertCordToIndex(MoveLogic.lastMove[2].Cord);
                ChessBoard[FromIndex].Update(MoveLogic.lastMove[2]);
            }
            else if (Game.ml.isCastle)
            {
                FromIndex = ConvertCordToIndex(MoveLogic.lastMove[3].Cord);
                ToIndex = ConvertCordToIndex(MoveLogic.lastMove[4].Cord);
                ChessBoard[FromIndex].Update(MoveLogic.lastMove[3]);
                ChessBoard[ToIndex].Update(MoveLogic.lastMove[4]);
            }

            Game.gl.FromSquare = null;
            Game.gl.ToSquare = null;
        }

        /*
         * Converts a grid coordinate (row and column) to the corresponting index
         * in a List
         */
        private int ConvertCordToIndex(String cord)
        {
            char row = cord[0];
            int col = (int)Char.GetNumericValue(cord[1]);
            int index= 0;
            switch (row)
            {
                case '6':
                    index = 8;
                    break;
                case '5':
                    index = 16;
                    break;
                case '4':
                    index = 24;
                    break;
                case '3':
                    index = 32;
                    break;
                case '2':
                    index = 40;
                    break;
                case '1':
                    index = 48;
                    break;
                case '0':
                    index = 56;
                    break;
            }
            return index += col;
        }
    }
}