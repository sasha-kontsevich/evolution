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
    /// Логика взаимодействия для FoodToken.xaml
    /// </summary>
    public partial class FoodToken : UserControl
    {
        Random random = new Random(Convert.ToInt32(DateTime.Now.Millisecond));
        public FoodToken()
        {
            InitializeComponent();
            image.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"../../../Resources/Images/Plant" + random.Next(1,5).ToString() + ".png", UriKind.RelativeOrAbsolute));
        }
    }
}
