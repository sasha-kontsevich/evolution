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

namespace evolution.View
{
    /// <summary>
    /// Логика взаимодействия для Game.xaml
    /// </summary>
    public partial class Game : Page
    {
        MainWindowViewModel mainWindowViewModel;
        public Game(MainWindowViewModel _mainWindowViewModel)
        {

            InitializeComponent();
            mainWindowViewModel = _mainWindowViewModel;
        }
        bool mouseDown = false;

         public Canvas Cvs;

        private void Arm_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Arm.Children.Remove(GameViewModel.SelectedCard);
        }

        private void Arm_MouseEnter(object sender, MouseEventArgs e)
        {
            Arm.Height = 300;
        }

        private void Arm_MouseLeave(object sender, MouseEventArgs e)
        {
            Arm.Height = 100;
        }

        private void cvs_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            mainWindowViewModel.GameContext.ChangePosition(0,0);
            GameViewModel.cvsH = cvs.ActualHeight;
            GameViewModel.cvsW = cvs.ActualWidth;
        }

        Point point1;
        Point point2;
        private void cvs_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mouseDown = true;
            point1 = Mouse.GetPosition(this);
        }

        private void cvs_MouseMove(object sender, MouseEventArgs e)
        {
            if(mouseDown)
            {
                //mainWindowViewModel.GameContext.ChangePosition(point2.X - point1.X, point2.Y - point1.Y);
            }
        }

        private void cvs_MouseUp(object sender, MouseButtonEventArgs e)
        {
            mouseDown = false;
            point2 = Mouse.GetPosition(this);
            mainWindowViewModel.GameContext.ChangePosition(point2.X-point1.X, point2.Y - point1.Y);
        }
    }
}
