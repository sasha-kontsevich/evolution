using evolution.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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

namespace evolution.Custom
{
    /// <summary>
    /// Логика взаимодействия для Card.xaml
    /// </summary>
    public partial class Card : UserControl
    {

        private string cardName;
        private int foodCount;

        public string CardName { get => cardName; set => cardName = value; }
        public int FoodCount { get => foodCount; set => foodCount = value; }

        public Card()
        {
            InitializeComponent();
            cardImage.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"../../../Resources/Images/Burrowing.png", UriKind.RelativeOrAbsolute));
            CardName = "empty";

            button.Opacity = 0;
        }

        public Card(int _foodCount, string _cardName)
        {
            InitializeComponent();
            cardImage.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"../../../Resources/Images/" + _cardName + ".png", UriKind.RelativeOrAbsolute));
            FoodCount = _foodCount;
            CardName = _cardName;
            button.Command = SelectCard;
            
            button.Opacity = 0;
        }


        public RelayCommand SelectCard
        {
            get
            {
                return new RelayCommand(obj => {
                    if(GameViewModel.SelectedCard!=this)
                    {
                        GameViewModel.SelectedCard = this;
                        button.Opacity = 0.5;
                    }
                    else
                    {
                        GameViewModel.SelectedCard = null;
                        button.Opacity = 0;
                    }
                });
            }
        }

    }
}
