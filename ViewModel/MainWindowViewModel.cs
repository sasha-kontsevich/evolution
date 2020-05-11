using System;
using System.Windows.Controls;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using evolution.View;

namespace evolution.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {

        private MainWindow window;

        private Page mainMenuPage;
        private Page singlePlayerPage;
        private Page settingsPage;
        private Page rulesPage;
        private Page leaderBoardPage;
        private Page profilePage;
        private Page gamePage;

        private SinglePlayerViewModel singlePlayerDataContext;
        private MainMenuViewModel mainMenuDataContext;
        private SettingsViewModel settingsDataContex;
        private RulesViewModel rulesDataContex;
        private LeaderBoardViewModel leaderBoardContext;
        private ProfileViewModel profileContext;
        private GameViewModel gameContext;

        private Page currentPage;

        private User currentUser;
        public Page CurrentPage
        {
            get { return currentPage; }
            set
            {
                if (currentPage == value)
                    return;

                currentPage = value;
                RaisePropertyChanged("CurrentPage");
            }
        }
        private double frameOpacity = 1;
        public double FrameOpacity
        {
            get { return frameOpacity; }
            set
            {
                if (frameOpacity == value)
                    return;

                frameOpacity = value;
                RaisePropertyChanged("FrameOpacity");
            }
        }

        public MainWindow Window { get => window; set => window = value; }
        public Page SinglePlayerPage { get => singlePlayerPage; set => singlePlayerPage = value; }
        public Page SettingsPage { get => settingsPage; set => settingsPage = value; }
        public Page MainMenuPage { get => mainMenuPage; set => mainMenuPage = value; }
        public Page RulesPage { get => rulesPage; set => rulesPage = value; }
        public Page LeaderBoardPage { get => leaderBoardPage; set => leaderBoardPage = value; }
        public Page ProfilePage { get => profilePage; set => profilePage = value; }
        public Page GamePage { get => gamePage; set => gamePage = value; }
        public LeaderBoardViewModel LeaderBoardContext { get => leaderBoardContext; set => leaderBoardContext = value; }

        public MainWindowViewModel(MainWindow _window)
        {
            Window = _window;

            MainMenuPage = new MainMenu();                                   //Начальная инициализация страниц
            SinglePlayerPage = new SinglePlayer();
            SettingsPage = new Settings();
            RulesPage = new Rules();
            LeaderBoardPage = new LeaderBoard();
            ProfilePage = new Profile();
            GamePage = new Game();

            mainMenuDataContext = new MainMenuViewModel(this);              //Инициализация контекста для страниц
            singlePlayerDataContext = new SinglePlayerViewModel(this);
            settingsDataContex = new SettingsViewModel(this);
            rulesDataContex = new RulesViewModel(this);
            LeaderBoardContext = new LeaderBoardViewModel(this);
            profileContext = new ProfileViewModel(this);
            gameContext = new GameViewModel(this);

            MainMenuPage.DataContext = mainMenuDataContext;                 //Задание контекста страниц
            SinglePlayerPage.DataContext = singlePlayerDataContext;
            SettingsPage.DataContext = settingsDataContex;
            RulesPage.DataContext = rulesDataContex;
            LeaderBoardPage.DataContext = LeaderBoardContext;
            ProfilePage.DataContext = profileContext;
            GamePage.DataContext = gameContext;

            CurrentPage = MainMenuPage;                             //Страница при загрузке
        }

        public void ChangePage(Page page)
        {
            SlowOpacity(page);
        }


        public async void SlowOpacity(Page page)
        {
            await Task.Factory.StartNew(() =>
            {
                for (double i = 1.0; i > 0.0; i -= 0.06)
                {
                    FrameOpacity = i;
                    Thread.Sleep(10);
                }
                CurrentPage = page;
                for (double i = 0.0; i < 1.1; i += 0.07)
                {
                    FrameOpacity = i;
                    Thread.Sleep(10);
                }
            });
        }
        public void AppClose()
        {
            App.Current.MainWindow.Close();
        }

    }
}
