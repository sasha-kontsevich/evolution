using evolution.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace evolution.Custom
{
    /// <summary>
    /// Логика взаимодействия для Species.xaml
    /// </summary>
    public partial class Species : UserControl
    {
        public Species()
        {
            InitializeComponent();
            FoodCountText.Text = "0";
            PopulationText.Text = "1";
            BodySizeText.Text = "1";
        }
        private int foodCount = 0;
        private int population = 1;
        private int bodySize = 1;

        public int FoodCount
        {
            get { return foodCount; }
            set
            {
                if (foodCount == value)
                    return;

                foodCount = value;
                FoodCountText.Text = value.ToString();
            }
        }

        public int Population
        {
            get { return population; }
            set
            {
                if (population == value)
                    return;

                population = value;
                PopulationText.Text = value.ToString();
            }
        }

        public int BodySize
        {
            get { return bodySize; }
            set
            {
                if (bodySize == value)
                    return;

                bodySize = value;
                BodySizeText.Text = value.ToString();
            }
        }


        private void species_MouseUp(object sender, MouseButtonEventArgs e)
        {

            if(GameViewModel.isAttacked)
            {
                if (GameViewModel.AttackedSpecies != this)
                {
                    GameViewModel.AttackedSpecies = this;
                    selectionBorder.Visibility = Visibility.Visible;
                }
                else
                {
                    GameViewModel.AttackedSpecies = null;
                    selectionBorder.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                if (GameViewModel.SelectedSpecies != this)
                {
                    GameViewModel.SelectedSpecies = this;
                    selectionBorder.Visibility = Visibility.Visible;
                }
                else
                {
                    GameViewModel.SelectedSpecies = null;
                    selectionBorder.Visibility = Visibility.Hidden;
                }
            }
        }
        public bool AddTrait(Card card)
        {
            if(!Contains(card.CardName))
            {
                if (Trait1.CardName == "empty")
                {
                    Trait1.Content = card;
                    Trait1.CardName = card.CardName;
                    return true;
                }
                else
    if (Trait2.CardName == "empty")
                {
                    Trait2.Content = card;
                    Trait2.CardName = card.CardName;
                    return true;
                }
                else
    if (Trait3.CardName == "empty")
                {
                    Trait3.Content = card;
                    Trait3.CardName = card.CardName;
                    return true;
                }
            }
            return false;
            //MessageBox.Show(Trait1.CardName);
        }
        public bool Contains(string cardName)
        {
            if (Trait1.CardName == cardName)
            {
                return true;
            }
            else
              if (Trait2.CardName == cardName)
            {
                return true;
            }
            else
           if (Trait3.CardName == cardName)
            {
                return true;
            }
            return false;
        }
        public bool ReplaceTrait()
        {
            return false;
        }
    }
}
