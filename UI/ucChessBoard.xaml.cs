using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;
using ChessModel;

namespace UI
{
    /// <summary>
    /// Interaction logic for UCChessBoard.xaml
    /// </summary>
    public partial class UCChessBoard : UserControl
    {
        ObservableCollection<SquareViewModel> ChessBoard { get; set; }
        private Game Game { get; set; }
        public GameLogicViewModel GameLogicViewModel { get; set; }
        private List<int> ValidMoves { get; set; }
        private int LastFrom { get; set; }
        private int LastTo { get; set; }

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

            LastFrom = -1;
            ValidMoves = new List<int>();
            GameLogicViewModel = new GameLogicViewModel(Game);
            ChessBoard = new ObservableCollection<SquareViewModel>();
            ConvertToList(Game, ChessBoard);

            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(1000)
            };
            timer.Tick += TimerTick;
            timer.Start();
            
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

            if (GameLogicViewModel.FromSquare == null || GameLogicViewModel.ToSquare != null)
            {
                HideLastValidMoves();
            }

            if (GameLogicViewModel.FromSquare != null && GameLogicViewModel.ToSquare == null)
            {
                var Index = ConvertCordToIndex(tag);
                ChessBoard[Index].IsChecked = true;
                HideLastValidMoves();
                ShowValidMoves(Game.gl.FromSquare);
            }

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
                EndGameWindow($"Checkmate\n{color} Wins!");
            }
            else if (GameLogicViewModel.StaleMate)
            {
                EndGameWindow("Stalemate!");
            }

            if(GameLogicViewModel.FromSquare == null)
            {
                var Index = ConvertCordToIndex(tag);
                ChessBoard[Index].IsChecked = false;
            }
            //if (GameLogicViewModel.ToSquare != null)
            //{
            //    var Index = ConvertCordToIndex(tag);
            //    ChessBoard[Index].IsChecked = false;
            //}

            if (LastFrom != -1)
            {
                ChessBoard[LastFrom].IsChecked = true;
                ChessBoard[LastTo].IsChecked = true;
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
            if (LastFrom != -1)
            {
                ChessBoard[LastFrom].IsChecked = false;
                ChessBoard[LastTo].IsChecked = false;
            }

            var FromSquare = Game.gl.FromSquare;
            var ToSquare = Game.gl.ToSquare;
            var FromIndex = ConvertCordToIndex(FromSquare.Coord);
            var ToIndex = ConvertCordToIndex(ToSquare.Coord);
            LastFrom = FromIndex;
            LastTo = ToIndex;

            ChessBoard[FromIndex].Update(FromSquare, Visibility.Hidden);
            ChessBoard[ToIndex].Update(ToSquare, Visibility.Hidden);

            if (Game.ml.isEnPassant)
            {
                FromIndex = ConvertCordToIndex(MoveLogic.lastMove[2].Coord);
                ChessBoard[FromIndex].Update(MoveLogic.lastMove[2], Visibility.Hidden);
            }
            else if (Game.ml.isCastle)
            {
                FromIndex = ConvertCordToIndex(MoveLogic.lastMove[3].Coord);
                ToIndex = ConvertCordToIndex(MoveLogic.lastMove[4].Coord);
                ChessBoard[FromIndex].Update(MoveLogic.lastMove[3], Visibility.Hidden);
                ChessBoard[ToIndex].Update(MoveLogic.lastMove[4], Visibility.Hidden);
            }

            Game.gl.FromSquare = null;
            Game.gl.ToSquare = null;
        }

        /*
         * Gets all valid move for the selected piece and updates the ViewModel
         */
        private void ShowValidMoves(Square FromSquare)
        {
            //var color = Game.CurrentPlayer.Color == ChessColor.Black ? ChessColor.White : ChessColor.Black;
            var color = Game.CurrentPlayer.Color;
            ValidMoves.Clear();
            GetAllValidMoves.AllValidMoves.Clear();
            GetAllValidMoves.GetMoves(Game.ml, FromSquare, color, false);

            for(int i = 0; i < GetAllValidMoves.AllValidMoves.Count; i++)
            {
                ValidMoves.Add(ConvertCordToIndex(GetAllValidMoves.AllValidMoves[i]));
                ChessBoard[ValidMoves[i]].ValidMove = Visibility.Visible;
            }
        }

        /*
         * Hides all valid moves from last selected piece and updates the ViewModel
         */
        private void HideLastValidMoves()
        {
            for (int i = 0; i < ValidMoves.Count; i++)
            {
                ChessBoard[ValidMoves[i]].ValidMove = Visibility.Hidden;
            }
        }

        /*
         * Converts a grid coordinate (row and column) to the corresponting index
         * in a List
         */
        private int ConvertCordToIndex(String coord)
        {
            char row = coord[0];
            int col = (int)Char.GetNumericValue(coord[1]);
            int index = 0;
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

        private void TimerTick(object sender, EventArgs e)
        {
            GameLogicViewModel.UpdateClock();
            //Player1Clock = GameLogicViewModel.Player1Clock;
            //Player2Clock = GameLogicViewModel.Player2Clock;
        }
    }
}