using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace UI
{
    /// <summary>
    /// Interaction logic for EndGameWindow.xaml
    /// </summary>
    public partial class EndGameWindow : Window
    {
        public string Selection { get; set; }
        public string Message { get; set; }

        public EndGameWindow() : this("Checkmate! White Wins!")
        {

        }


        public EndGameWindow(string message)
        {
            DataContext = this;
            InitializeComponent();
            message = $"{message}\n\n\nPress \"New Game\" to start a new game. Press \"End Game\" to close the Application";
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
    }
}
