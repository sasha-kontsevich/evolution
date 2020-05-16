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
        public static int GameSize = 0;
        public static double cvsH=0;
        public static double cvsW=0;
        public static int FirstPlayerToken=0;
        Map map;
        public List<Card> Deck = new List<Card>();
        public static Card SelectedCard;
        public static int? SelectedCardIndex = null;
        public static Species SelectedSpecies;
        private Player currentPlayer;
        private bool currentPlayerTurnEnded = false;
        List<Player> players = new List<Player>();
        List<Card> cards = new List<Card>();
        public List<Player> Players { get => players; set => players = value; }
        public List<Card> Cards { get => cards; set => cards = value; }
        int x = 0;
        int y = 0;
        public Player CurrentPlayer 
        {
            get { return currentPlayer; }
            set
            {
                if (currentPlayer == value)
                    return;
                UpdateCardsInArm();
                currentPlayer = value;
            }
        }

        Canvas cvs;
        public Canvas Cvs { get => cvs; set => cvs = value; }
        MainWindowViewModel mainWindowViewModel;
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
            map = new Map();
            
            UptadePlayersTable();
            Cvs.Children.Add(map);
            InitializeCards();
            map.MapBegin(Players.Count);
            ReDraw();
            GameCycle();    //главный цикл игры
        }
        public void GameCycle() //главный цикл игры
        {
            DealCards();//раздача карт
            CurrentPlayer = Players.ToArray()[FirstPlayerToken];
            UpdateCardsInArm();
            UptadePlayersTable();
            //Определение кормовой базы

        }

        public void DealCards()
        {
            foreach(Player player in Players)
            {
                for (int i = 0; i < 3; i++) 
                if(Deck.Count!=0)
                {
                    player.Cards.Add(Deck.Last());
                    Deck.Remove(Deck.Last());
                }
            }
        }
        public void DisplayCards()
        {
            foreach(Card card in Deck)
            {
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

        public RelayCommand NewSpeciesR
        {
            get
            {
                return new RelayCommand(obj => {
                    if(SelectedCard!=null&&!currentPlayerTurnEnded)
                    {
                        Players.ToArray()[Players.IndexOf(CurrentPlayer)].Cards.Remove(SelectedCard);
                        SelectedCard = null;
                        UpdateCardsInArm();
                        map.AddSpecies(Players.IndexOf(currentPlayer));
                        currentPlayerTurnEnded = true;
                    }
                });
            }
        }
        public RelayCommand IncreasePopulation
        {
            get
            {
                return new RelayCommand(obj => {
                    if(SelectedCard!=null&&!currentPlayerTurnEnded&& SelectedSpecies!=null&& SelectedSpecies.Population<6)
                    {
                        Players.ToArray()[Players.IndexOf(CurrentPlayer)].Cards.Remove(SelectedCard);
                        SelectedCard = null;
                        UpdateCardsInArm();
                        SelectedSpecies.Population += 1;
                        currentPlayerTurnEnded = true;
                        EndPlayersTurn();
                    }
                });
            }
        }
        public RelayCommand IncreaseBodySize
        {
            get
            {
                return new RelayCommand(obj => {
                    if(SelectedCard!=null&&!currentPlayerTurnEnded&& SelectedSpecies!=null&& SelectedSpecies.BodySize < 6)
                    {
                        Players.ToArray()[Players.IndexOf(CurrentPlayer)].Cards.Remove(SelectedCard);
                        SelectedCard = null;
                        UpdateCardsInArm();
                        SelectedSpecies.BodySize += 1;
                        currentPlayerTurnEnded = true;
                        EndPlayersTurn();
                    }
                });
            }
        }
        public RelayCommand AddTrait
        {
            get
            {
                return new RelayCommand(obj => {
                    if(SelectedCard!=null&&!currentPlayerTurnEnded&& SelectedSpecies!=null&& SelectedSpecies.AddTrait(SelectedCard))
                    {
                        Players.ToArray()[Players.IndexOf(CurrentPlayer)].Cards.Remove(SelectedCard);
                        
                        SelectedCard = null;
                        UpdateCardsInArm();
                        currentPlayerTurnEnded = true;
                        EndPlayersTurn();
                    }
                });
            }
        }
        public RelayCommand NextPlayer
        {
            get
            {
                return new RelayCommand(obj => {
                    EndPlayersTurn();
                });
            }
        }
        public RelayCommand EatPlant
        {
            get
            {
                return new RelayCommand(obj => {
                    if(SelectedSpecies!=null&&SelectedSpecies.FoodCount<SelectedSpecies.Population && map.RemoveFoodToken())
                    {
                        SelectedSpecies.FoodCount += 1;
                        EndPlayersTurn();
                    }
                });
            }
        }


        public void EndPlayersTurn()
        {
            if(CurrentPlayer!=Players.Last())
            {
                int n = Players.IndexOf(CurrentPlayer);
                CurrentPlayer = Players.ToArray()[n+1];
            }
            else
            {
                CurrentPlayer = Players.First();
            }
            SelectedCard = null;
            SelectedSpecies = null;
            UpdateCardsInArm();
            currentPlayerTurnEnded = false;
            UptadePlayersTable();
        }

        public void ClearMatch()
        {
            Cvs.Children.Remove(map);
            Deck.Clear();
            Players.Clear();
        }

        private IEnumerable<object> playersTable;
        public IEnumerable<object> PlayersTable
        {
            get { return playersTable; }
            set
            {
                if (playersTable == value)
                    return;
                playersTable = value;
                RaisePropertyChanged("PlayersTable");
            }
        }
        public void UpdateCardsInArm()
        {
            if(CurrentPlayer!=null)
            {
                mainWindowViewModel.GamePage.Arm.Children.Clear();
                foreach (Card card in CurrentPlayer.Cards)
                {
                    mainWindowViewModel.GamePage.Arm.Children.Add(card);
                }
                UptadePlayersTable();
            }
        }
        public void UptadePlayersTable()
        {
            
            var query = from p in Players
                        select new {Current = p==CurrentPlayer, Number = p.Number+1, Name = p.User.NickName, CardsCount = p.Cards.Count.ToString() };
            PlayersTable = query.ToList();

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
                    ClearMatch();
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
        public void InitializeCards()
        {
            Deck.Add(new Card(2, "Burrowing"));
            Deck.Add(new Card(3, "Burrowing"));
            Deck.Add(new Card(4, "Burrowing"));

            Deck.Add(new Card(-2, "Carnivore"));
            Deck.Add(new Card(-3, "Carnivore"));
            Deck.Add(new Card(2, "Carnivore"));
            Deck.Add(new Card(4, "Carnivore"));
            Deck.Add(new Card(6, "Carnivore"));
            Deck.Add(new Card(1, "Carnivore"));
            Deck.Add(new Card(5, "Carnivore"));
            Deck.Add(new Card(7, "Carnivore"));

            Deck.Add(new Card(2, "Ambush"));
            Deck.Add(new Card(3, "Ambush"));
            Deck.Add(new Card(-3, "Ambush"));

            Deck.Add(new Card(4, "Cooperation"));
            Deck.Add(new Card(2, "Cooperation"));
            Deck.Add(new Card(3, "Cooperation"));

            //Deck.Add(new Card(4, "GoodEyesight"));
            //Deck.Add(new Card(-1, "GoodEyesight"));
            //Deck.Add(new Card(5, "GoodEyesight"));

            Deck.Add(new Card(4, "Horns"));
            Deck.Add(new Card(5, "Horns"));
            Deck.Add(new Card(1, "Horns"));

            Deck.Add(new Card(3, "LongNeck"));
            Deck.Add(new Card(1, "LongNeck"));
            Deck.Add(new Card(6, "LongNeck"));

            Deck.Add(new Card(1, "Scavenger"));
            Deck.Add(new Card(-1, "Scavenger"));
            Deck.Add(new Card(2, "Scavenger"));

            Deck.Add(new Card(4, "Climbing"));
            Deck.Add(new Card(3, "Climbing"));
            Deck.Add(new Card(4, "Climbing"));

            Deck.Add(new Card(5, "Fertile"));
            Deck.Add(new Card(3, "Fertile"));
            Deck.Add(new Card(7, "Fertile"));

            Deck.Add(new Card(5, "Foraging"));
            Deck.Add(new Card(4, "Foraging"));
            Deck.Add(new Card(7, "Foraging"));

            Deck.Add(new Card(3, "PackHunting"));
            Deck.Add(new Card(4, "PackHunting"));
            Deck.Add(new Card(5, "PackHunting"));

            Deck.Add(new Card(5, "Symbiosis"));
            Deck.Add(new Card(3, "Symbiosis"));
            Deck.Add(new Card(6, "Symbiosis"));

            Deck.Add(new Card(1, "WarningCall"));
            Deck.Add(new Card(0, "WarningCall"));
            Deck.Add(new Card(5, "WarningCall"));

            Deck.Add(new Card(7, "HardShell"));
            Deck.Add(new Card(5, "HardShell"));
            Deck.Add(new Card(6, "HardShell"));

            Deck.Add(new Card(6, "DefensiveHerding"));
            Deck.Add(new Card(5, "DefensiveHerding"));
            Deck.Add(new Card(4, "DefensiveHerding"));

            //Deck.Add(new Card(7, "FatTissue"));
            //Deck.Add(new Card(5, "FatTissue"));
            //Deck.Add(new Card(-3, "FatTissue"));

            //Deck.Add(new Card(4, "Intelligence"));
            //Deck.Add(new Card(1, "Intelligence"));
            //Deck.Add(new Card(2, "Intelligence"));

            UserFunctions.Shuffle(Deck);
        }

    }
}
