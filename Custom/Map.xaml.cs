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
        public double X = -1000;
        public double Y = -1000;

        public Map()
        {
            InitializeComponent();
            waterholeImage.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"../../../Resources/Images/Waterhole.png", UriKind.RelativeOrAbsolute));
            for(int i = 0; i< 10;i++)
            {
                TokensPanel.Children.Add(new FoodToken());
            }
            player1Species.Children.Add(new Species());
        }
        public void RemoveFoodToken()
        {
            try
                {
                TokensPanel.Children.RemoveAt(TokensPanel.Children.Count - 1);
                }
            catch
            {

            }

        }
    }
}
