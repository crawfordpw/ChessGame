using System.Windows;
using ChessModel;

namespace UI
{
    /// <summary>
    /// Interaction logic for EndGameWindow.xaml
    /// </summary>
    public partial class EndGameWindow : Window
    {
        public string Selection { get; set; }
        public string Message { get; set; }
        public Player Player { get; set; }

        public EndGameWindow() : this("Checkmate\nWhite Wins!")
        {

        }


        public EndGameWindow(string message)
        {
            DataContext = this;

            InitializeComponent();
            message = $"{message}";
            this.Message = message;
        }

        public void NewGameClick(object sender, RoutedEventArgs e)
        {
            Selection = "NewGame";
            this.Close();
        }

        public void CloseGameClick(object sender, RoutedEventArgs e)
        {
            Selection = "EndGame";
            this.Close();
        }

        public void CloseWindowClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
