using System;
using System.Windows.Controls;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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

        private SinglePlayerViewModel singlePlayerDataContext;
        private MainMenuViewModel mainMenuDataContext;
        private SettingsViewModel settingsDataContex;
        private RulesViewModel rulesDataContex;

        private Page currentPage;

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

        public MainWindowViewModel(MainWindow _window)
        {
            MainMenuPage = new MainMenu();                                   //Начальная инициализация страниц
            SinglePlayerPage = new SinglePlayer();
            SettingsPage = new Settings();
            RulesPage = new Rules();

            mainMenuDataContext = new MainMenuViewModel(this);              //Инициализация контекста для страниц
            singlePlayerDataContext = new SinglePlayerViewModel(this);
            settingsDataContex = new SettingsViewModel(this);
            rulesDataContex = new RulesViewModel(this);

            MainMenuPage.DataContext = mainMenuDataContext;                 //Задание контекста страниц
            SinglePlayerPage.DataContext = singlePlayerDataContext;
            SettingsPage.DataContext = settingsDataContex;
            RulesPage.DataContext = rulesDataContex;

            CurrentPage = MainMenuPage;                             //Страница при загрузке
            Window = _window;
            
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
