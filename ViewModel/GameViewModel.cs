using evolution.Custom;
using evolution.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace evolution.ViewModel
{
    public class GameViewModel:BaseViewModel
    {
        public static Card SelectedCard;
        MainWindowViewModel mainWindowViewModel;
        public static double cvsH=0;
        public static double cvsW=0;
        Map map = new Map();

        Canvas cvs;
        List<Player> players = new List<Player>();
        List<Card> cards = new List<Card>();
        public List<Player> Players { get => players; set => players = value; }
        public Canvas Cvs { get => cvs; set => cvs = value; }
        public List<Card> Cards { get => cards; set => cards = value; }
        int x = 0;
        int y = 0;


        public GameViewModel(MainWindowViewModel _mainWindowViewModel, Canvas _canvas)
        {
            mainWindowViewModel = _mainWindowViewModel;
            cvs = _canvas;

        }
        private int gameTurn = 0;

        public int GameTurn
        {
            get { return gameTurn; }
            set
            {
                if (gameTurn == value)
                    return;

                gameTurn = value;
                RaisePropertyChanged("GameTurn");
            }
        }

        public void StartGame()
        {
            Cvs.Children.Add(map);
            InitializeCards();
            DisplayCards();
            ReDraw();
        }
        public void InitializeCards()
        {
            Cards.Add(new Card(2, "Burrowing"));
            Cards.Add(new Card(3, "Burrowing"));
            Cards.Add(new Card(4, "Burrowing"));
            Cards.Add(new Card(-2, "Carnivorous"));
            Cards.Add(new Card(-3, "Carnivorous"));
            Cards.Add(new Card(2, "Carnivorous"));
            Cards.Add(new Card(2, "Ambush"));
            Cards.Add(new Card(3, "Ambush"));
            Cards.Add(new Card(4, "Cooperation"));
            Cards.Add(new Card(2, "Cooperation"));
            Cards.Add(new Card(3, "Cooperation"));
            Cards.Add(new Card(4, "GoodEyesight"));
        }
        public void DisplayCards()
        {
            int x = 0;
            int y = 0;
            foreach(Card card in Cards)
            {
                card.RenderTransform.Value.ScalePrepend(3, 3);
                //Cvs.Children.Add(card);
                //Canvas.SetLeft(card, Cvs.ActualWidth *0.3f + x);
                //Canvas.SetTop(card, Cvs.ActualHeight * 0.7f + y);
                x += 100;
                y += 5;
                mainWindowViewModel.GamePage.Arm.Children.Add(card);
            }
        }
        public void ChangePosition(double dx, double dy)
        {
            map.X += dx;
            map.Y += dy;
            ReDraw();
        }
        public void ReDraw()
        {
            Canvas.SetLeft(map, map.X + x);
            Canvas.SetTop(map, map.Y + y);

        }

        public RelayCommand RemoveCard
        {
            get
            {
                return new RelayCommand(obj => {
                    mainWindowViewModel.GamePage.Arm.Children.Remove(SelectedCard);
                });
            }
        }
        public RelayCommand EatPlant
        {
            get
            {
                return new RelayCommand(obj => {
                    map.RemoveFoodToken();
                });
            }
        }

        #region Menu
        private double menuOpacity = 0;
        public double MenuOpacity
        {
            get { return menuOpacity; }
            set
            {
                if (menuOpacity == value)
                    return;

                menuOpacity = value;
                RaisePropertyChanged("MenuOpacity");
            }
        }
        private Visibility menuVisibility = Visibility.Hidden;
        public Visibility MenuVisibility
        {
            get { return menuVisibility; }
            set
            {
                if (menuVisibility == value)
                    return;

                menuVisibility = value;
                RaisePropertyChanged("MenuVisibility");
            }
        }
        public RelayCommand Continue
        {
            get
            {
                return new RelayCommand(obj => { MenuOpacity = 0; MenuVisibility = Visibility.Hidden; });
            }
        }
        public RelayCommand ReturnToMenu
        {
            get
            {
                return new RelayCommand(obj => { mainWindowViewModel.ChangePage(mainWindowViewModel.MainMenuPage);
                    MenuOpacity = 0; MenuVisibility = Visibility.Hidden;
                });
            }
        }
        public RelayCommand ShowMenu
        {
            get
            {
                return new RelayCommand(obj => {
                    MenuOpacity = 1; 
                    MenuVisibility = Visibility.Visible; 
                });
            }
        }

        private Page gameFrame;
        public Page GameFrame
        {
            get { return gameFrame; }
            set
            {
                if (gameFrame == value)
                    return;
                gameFrame = value;
                RaisePropertyChanged("GameFrame");
            }
        }
        public RelayCommand SettingsMenuItem
        {
            get
            {
                return new RelayCommand(obj => { GameFrame = mainWindowViewModel.SettingsPage; });
            }
        }
        public RelayCommand RulesMenuItem
        {
            get
            {
                return new RelayCommand(obj => { GameFrame = mainWindowViewModel.RulesPage; });
            }
        }
        private List<string> languages = new List<string>();
        public List<string> Languages
        {
            get { return languages; }
            set
            {
                if (languages == value)
                    return;
                languages = value;
                RaisePropertyChanged("Languages");
            }
        }

        #endregion

    }
}
