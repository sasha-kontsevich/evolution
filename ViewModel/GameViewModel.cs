using evolution.Custom;
using evolution.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace evolution.ViewModel
{
    public class GameViewModel : BaseViewModel
    {
        public int Phase = 0;
        public static int GameSize = 0;
        public static double cvsH = 0;
        public static double cvsW = 0;
        public static int FirstPlayerToken = 0;
        public static bool isAttacked = false;
        Map map;
        public List<Card> Deck = new List<Card>();
        private static Card selectedCard;
        public static int? SelectedCardIndex = null;
        private static Species selectedSpecies;
        private static Species attackedSpecies;
        private Player currentPlayer;
        private bool currentPlayerTurnEnded = false;
        List<Player> players = new List<Player>();
        List<Card> cards = new List<Card>();
        public List<Player> Players { get => players; set => players = value; }
        public List<Card> Cards { get => cards; set => cards = value; }
        int x = 0;
        int y = 0;
        public int NextPhaseCount = 0;
        public static Card SelectedCard
        {
            get { return selectedCard; }
            set
            {
                if(selectedCard!=null)
                SelectedCard.button.Opacity = 0;
                if (selectedCard == value)
                {
                    return;
                }
                selectedCard = value;
            }
        }
        public static Species SelectedSpecies
        {
            get { return selectedSpecies; }
            set
            {
                    if (selectedSpecies != null)
                        SelectedSpecies.selectionBorder.Visibility = Visibility.Hidden;
                    if (selectedSpecies == value)
                    {
                        return;
                    }
                    selectedSpecies = value;
            }
        }
        public static Species AttackedSpecies
        {
            get { return attackedSpecies; }
            set
            {
                    if (attackedSpecies != null)
                    attackedSpecies.selectionBorder.Visibility = Visibility.Hidden;
                    if (attackedSpecies == value)
                    {
                        return;
                    }
                attackedSpecies = value;
            }
        }
        public Player CurrentPlayer
        {
            get { return currentPlayer; }
            set
            {
                if (currentPlayer == value)
                    return;
                UpdateCardsInArm();
                currentPlayer = value;
                Map.currentPlayerNumber = Players.IndexOf(currentPlayer);
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
        public void GameCycle() 
        {
            DealCards();//раздача карт
            CurrentPlayer = Players.ToArray()[FirstPlayerToken];
            map.SelectSpecies(Players.IndexOf(currentPlayer));
            UpdateCardsInArm();
            UptadePlayersTable();
            //Определение кормовой базы
            Phase = 1;
            SelectFoodChangeButtonsVisibility();
        }
        //разыгрывание карт
        public void PlayCards()
        {
            Phase = 2;
            NextPhaseCount = 0;
            PlayCardsChangeButtonsVisibility();
        }
        //кормление
        public void Feeding()
        {
            Phase = 3;
            FeedingChangeButtonsVisibility();
        }

        public void DealCards()
        {
            foreach (Player player in Players)
            {
                for (int i = 0; i < 3+map.GetPlayersSpeciesCount(Players.IndexOf(player)); i++)
                    if (Deck.Count != 0)
                    {
                        player.Cards.Add(Deck.Last());
                        Deck.Remove(Deck.Last());
                    }
            }
        }
        public void DisplayCards()
        {
            foreach (Card card in Deck)
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

        public RelayCommand PutOnTheWaterHole
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    if (SelectedCard != null && !currentPlayerTurnEnded)
                    {
                        map.TotalFood += SelectedCard.FoodCount;
                        Players.ToArray()[Players.IndexOf(CurrentPlayer)].Cards.Remove(SelectedCard);
                        SelectedCard = null;
                        UpdateCardsInArm();
                        
                        currentPlayerTurnEnded = true;
                        NextPhaseCount++;
                    }
                });
            }
        }
        public RelayCommand NewSpeciesR
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    if (SelectedCard != null && !currentPlayerTurnEnded)
                    {
                        Players.ToArray()[Players.IndexOf(CurrentPlayer)].Cards.Remove(SelectedCard);
                        SelectedCard = null;
                        UpdateCardsInArm();
                        map.AddSpecies(Players.IndexOf(currentPlayer));
                        currentPlayerTurnEnded = true;
                        NextPhaseCount = 0;
                    }
                });
            }
        }
        public RelayCommand IncreasePopulation
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    if (SelectedCard != null && !currentPlayerTurnEnded && SelectedSpecies != null && SelectedSpecies.Population < 6)
                    {
                        Players.ToArray()[Players.IndexOf(CurrentPlayer)].Cards.Remove(SelectedCard);
                        SelectedCard = null;
                        UpdateCardsInArm();
                        SelectedSpecies.Population += 1;
                        currentPlayerTurnEnded = true;
                        EndPlayersTurn();
                        NextPhaseCount = 0;
                    }
                });
            }
        }
        public RelayCommand IncreaseBodySize
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    if (SelectedCard != null && !currentPlayerTurnEnded && SelectedSpecies != null && SelectedSpecies.BodySize < 6)
                    {
                        Players.ToArray()[Players.IndexOf(CurrentPlayer)].Cards.Remove(SelectedCard);
                        SelectedCard = null;
                        UpdateCardsInArm();
                        SelectedSpecies.BodySize += 1;
                        currentPlayerTurnEnded = true;
                        EndPlayersTurn();
                        NextPhaseCount = 0;
                    }
                });
            }
        }
        public RelayCommand AddTrait
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    if (SelectedCard != null && !currentPlayerTurnEnded && SelectedSpecies != null && SelectedSpecies.AddTrait(SelectedCard))
                    {
                        Players.ToArray()[Players.IndexOf(CurrentPlayer)].Cards.Remove(SelectedCard);

                        SelectedCard = null;
                        UpdateCardsInArm();
                        currentPlayerTurnEnded = true;
                        EndPlayersTurn();
                        NextPhaseCount = 0;
                    }
                });
            }
        }
        public RelayCommand NextPlayer
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    EndPlayersTurn();
                });
            }
        }
        public RelayCommand EatPlant
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    if (SelectedSpecies != null)
                    {
                        if(!SelectedSpecies.Contains("Carnivore") && SelectedSpecies.Population != 0)
                        {
                            if (SelectedSpecies.FoodCount < SelectedSpecies.Population && map.RemoveFoodToken())
                            {
                                SelectedSpecies.FoodCount += 1;
                                EndPlayersTurn();
                            }
                        }
                    }
                });
            }
        }
        public RelayCommand Attack
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    if (SelectedSpecies != null)
                    {
                        if(SelectedSpecies.Contains("Carnivore"))
                        {
                            if(AttackedSpecies!=null&&isAttacked)
                            {
                                if (SelectedSpecies.FoodCount < SelectedSpecies.Population&&SelectedSpecies.BodySize>AttackedSpecies.BodySize)
                                {
                                    AttackedSpecies.Population--;
                                    map.Extinction();
                                    SelectedSpecies.FoodCount += AttackedSpecies.BodySize;
                                    map.RemoveSpecies(Players.IndexOf(currentPlayer),AttackedSpecies);
                                    isAttacked = false;
                                }
                            }
                        }
                    }
                });
            }
        }

        public RelayCommand SelectAim
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    if (SelectedSpecies != null)
                    {
                        if (SelectedSpecies.Contains("Carnivore") && SelectedSpecies.Population != 0)
                        {
                            isAttacked = true;
                        }
                    }
                });
            }
        }
        public RelayCommand NextPhase
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    foreach(StackPanel stackPanel in map.PlayersSpecies)
                    {
                        foreach(Species species in stackPanel.Children)
                        {
                            if(species.FoodCount<species.Population)
                            {
                                species.Population = species.FoodCount;
                            }
                        }
                    }
                    map.Extinction();
                    GameCycle();
                    NextPhaseCount = 0;
                });
            }
        }

        public void EndPlayersTurn()
        {
            if (CurrentPlayer != Players.Last())
            {
                int n = Players.IndexOf(CurrentPlayer);
                CurrentPlayer = Players.ToArray()[n + 1];
            }
            else
            {
                CurrentPlayer = Players.First();
            }
            map.SelectSpecies(Players.IndexOf(currentPlayer));
            SelectedCard = null;
            SelectedSpecies = null;
            UpdateCardsInArm();
            currentPlayerTurnEnded = false;
            UptadePlayersTable();

            if (Phase == 1)
            {
                if (NextPhaseCount == Players.Count)
                {
                    map.PutFood();
                    PlayCards();
                }
            }
            else if (Phase == 2)
            {
                NextPhaseCount++;
                if (NextPhaseCount == Players.Count)
                {
                    Feeding();
                }
            }
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
            if (CurrentPlayer != null)
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
                        select new { Current = p == CurrentPlayer, Number = p.Number + 1, Name = p.User.NickName, CardsCount = p.Cards.Count.ToString() };
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
                return new RelayCommand(obj =>
                {
                    mainWindowViewModel.ChangePage(mainWindowViewModel.MainMenuPage);
                    MenuOpacity = 0; MenuVisibility = Visibility.Hidden;
                    ClearMatch();
                });
            }
        }
        public RelayCommand ShowMenu
        {
            get
            {
                return new RelayCommand(obj =>
                {
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

        public void SelectFoodChangeButtonsVisibility()
        {
            VNewSpeciesR = Visibility.Collapsed;
            VNewSpeciesL = Visibility.Collapsed;
            VIncreaseBodySize = Visibility.Collapsed;
            VIncreasePopulation = Visibility.Collapsed;
            VAddTrait = Visibility.Collapsed;
            VPutOnTheWaterHole = Visibility.Visible;
            VAttack = Visibility.Collapsed;
            VSelectAim = Visibility.Collapsed;
            VEatPlant = Visibility.Collapsed;
            VNextPlayer = Visibility.Visible;
            VNextPhase = Visibility.Collapsed;
        }
        public void PlayCardsChangeButtonsVisibility()
        {
            VNewSpeciesR = Visibility.Visible;
            VNewSpeciesL = Visibility.Visible;
            VIncreaseBodySize = Visibility.Visible;
            VIncreasePopulation = Visibility.Visible;
            VAddTrait = Visibility.Visible;
            VPutOnTheWaterHole = Visibility.Collapsed;
            VAttack = Visibility.Collapsed;
            VSelectAim = Visibility.Collapsed;
            VEatPlant = Visibility.Collapsed;
            VNextPlayer = Visibility.Visible;
            VNextPhase = Visibility.Collapsed;
        }
        public void FeedingChangeButtonsVisibility()
        {
            VNewSpeciesR = Visibility.Collapsed;
            VNewSpeciesL = Visibility.Collapsed;
            VIncreaseBodySize = Visibility.Collapsed;
            VIncreasePopulation = Visibility.Collapsed;
            VAddTrait = Visibility.Collapsed;
            VPutOnTheWaterHole = Visibility.Collapsed;
            VAttack = Visibility.Visible;
            VSelectAim = Visibility.Visible;
            VEatPlant = Visibility.Visible;
            VNextPlayer = Visibility.Visible;
            VNextPhase = Visibility.Visible;
        }

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
        private Visibility vNewSpeciesL;
        public Visibility VNewSpeciesL
        {
            get { return vNewSpeciesL; }
            set
            {
                if (vNewSpeciesL == value)
                    return;
                vNewSpeciesL = value;
                RaisePropertyChanged("VNewSpeciesL");
            }
        }
        private Visibility vNewSpeciesR;
        public Visibility VNewSpeciesR
        {
            get { return vNewSpeciesR; }
            set
            {
                if (vNewSpeciesR == value)
                    return;
                vNewSpeciesR = value;
                RaisePropertyChanged("VNewSpeciesR");
            }
        }
        private Visibility vIncreasePopulation;
        public Visibility VIncreasePopulation
        {
            get { return vIncreasePopulation; }
            set
            {
                if (vIncreasePopulation == value)
                    return;
                vIncreasePopulation = value;
                RaisePropertyChanged("VIncreasePopulation");
            }
        }
        private Visibility vIncreaseBodySize;
        public Visibility VIncreaseBodySize
        {
            get { return vIncreaseBodySize; }
            set
            {
                if (vIncreaseBodySize == value)
                    return;
                vIncreaseBodySize = value;
                RaisePropertyChanged("VIncreaseBodySize");
            }
        }
        private Visibility vAddTrait;
        public Visibility VAddTrait
        {
            get { return vAddTrait; }
            set
            {
                if (vAddTrait == value)
                    return;
                vAddTrait = value;
                RaisePropertyChanged("VAddTrait");
            }
        }
        private Visibility vPutOnTheWaterHole;
        public Visibility VPutOnTheWaterHole
        {
            get { return vPutOnTheWaterHole; }
            set
            {
                if (vPutOnTheWaterHole == value)
                    return;
                vPutOnTheWaterHole = value;
                RaisePropertyChanged("VPutOnTheWaterHole");
            }
        }
        private Visibility vSelectAim;
        public Visibility VSelectAim
        {
            get { return vSelectAim; }
            set
            {
                if (vSelectAim == value)
                    return;
                vSelectAim = value;
                RaisePropertyChanged("VSelectAim");
            }
        }
        private Visibility vAttack;
        public Visibility VAttack
        {
            get { return vAttack; }
            set
            {
                if (vAttack == value)
                    return;
                vAttack = value;
                RaisePropertyChanged("VAttack");
            }
        }
        private Visibility vEatPlant;
        public Visibility VEatPlant
        {
            get { return vEatPlant; }
            set
            {
                if (vEatPlant == value)
                    return;
                vEatPlant = value;
                RaisePropertyChanged("VEatPlant");
            }
        }
        private Visibility vNextPhase;
        public Visibility VNextPhase
        {
            get { return vNextPhase; }
            set
            {
                if (vNextPhase == value)
                    return;
                vNextPhase = value;
                RaisePropertyChanged("VNextPhase");
            }
        }
        private Visibility vNextPlayer;
        public Visibility VNextPlayer
        {
            get { return vNextPlayer; }
            set
            {
                if (vNextPlayer == value)
                    return;
                vNextPlayer = value;
                RaisePropertyChanged("VNextPlayer");
            }
        }

    }
}