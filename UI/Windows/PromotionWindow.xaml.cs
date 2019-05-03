using System.Windows;
using System.Windows.Controls;
using ChessModel;

namespace UI
{
    /// <summary>
    /// Interaction logic for PromotionWindow.xaml
    /// </summary>
    public partial class PromotionWindow : Window
    {
        public ChessColor Player { get; set; }
        public Promotion Selection { get; set; }

        public PromotionWindow() : this(ChessColor.White)
        {
        }

        public PromotionWindow(ChessColor player)
        {
            InitializeComponent();
            DataContext = this;
            this.Player = player;
        }

        public void Select_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var button = sender as RadioButton;
            string selection = button.Name.ToString();

            switch (selection)
            {
                case "Knight":
                    Selection = Promotion.Knight;
                    break;
                case "Rook":
                    Selection = Promotion.Rook;
                    break;
                case "Bishop":
                    Selection = Promotion.Bishop;
                    break;
                default:
                    Selection = Promotion.Queen;
                    break;
            }
        }
    }
}
