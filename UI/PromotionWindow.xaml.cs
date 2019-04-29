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
using ChessModel;

namespace UI
{
    /// <summary>
    /// Interaction logic for Promotion.xaml
    /// </summary>
    public partial class PromotionWindow : Window
    {
        public ChessColor Player { get; set; }
        public PromotionWindow()
        {
            InitializeComponent();
            DataContext = this;
            Game Game = new Game();
            Game.NewGame();
            Player = Game.CurrentPlayer.Color;
        }

        public void Select_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
