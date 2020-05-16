using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для Map.xaml
    /// </summary>
    public partial class Map : UserControl
    {
        public double X = -1450;
        public double Y = -1200;
        List<StackPanel> playersSpecies = new List<StackPanel>();
        public Map()
        {
            InitializeComponent();
            waterholeImage.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"../../../Resources/Images/Waterhole.png", UriKind.RelativeOrAbsolute));
            for(int i = 0; i< 10;i++)
            {
                TokensPanel.Children.Add(new FoodToken());
            }

        }

        public List<StackPanel> PlayersSpecies { get => playersSpecies; set => playersSpecies = value; }

        public bool RemoveFoodToken()
        {
            try
                {
                TokensPanel.Children.RemoveAt(TokensPanel.Children.Count - 1);
                }
            catch
            {
                return false;
            }
            return true;
        }
        public void InitializeStackPanels(int n)
        {
            PlayersSpecies.Add(player1Species);
            PlayersSpecies.Add(player2Species);
            if (n == 2) return;
            PlayersSpecies.Add(player3Species);
            if (n == 3) return;
            PlayersSpecies.Add(player4Species);
            if (n == 4) return;
            PlayersSpecies.Add(player5Species);
            if (n == 5) return;
            PlayersSpecies.Add(player6Species);
        }
        public void MapBegin(int n)
        {
            InitializeStackPanels(n);
            foreach (StackPanel stackPanel in PlayersSpecies)
            {
                stackPanel.Children.Add(new Species());
            }
        }
        public void AddSpecies(int i)
        {
            PlayersSpecies.ToArray()[i].Children.Add(new Species());
        }
        public void IncreasePopulation(int i, Species species)
        {
            species.Population += 1;
            int n = PlayersSpecies.ToArray()[i].Children.IndexOf(species);
            try
            {
                PlayersSpecies.ToArray()[i].Children.RemoveAt(n);
                PlayersSpecies.ToArray()[i].Children.Insert(n, species);
            }
            catch
            {

            }
        }
    }
}
