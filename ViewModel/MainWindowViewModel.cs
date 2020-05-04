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
        private static MainWindowViewModel instance;


        public static MainWindowViewModel getInstance(MainWindow window)
        {
            if (instance == null)
                instance = new MainWindowViewModel(window);
            return instance;
        }

        private MainWindow window;
        private Page mainMenuPage;
        private Page singlePlayerPage;
        private Page settingsPage;

        private SinglePlayerViewModel singlePlayerDataContext;
        private MainMenuViewModel mainMenuDataContext;
        private SettingsViewModel settingsDataContex;

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
        

        public MainWindowViewModel(MainWindow _window)
        {
            MainMenuPage = new MainMenu();                                   //Начальная инициализация страниц
            SinglePlayerPage = new SinglePlayer();
            SettingsPage = new Settings();

            mainMenuDataContext = new MainMenuViewModel(this);              //Инициализация контекста для страниц
            singlePlayerDataContext = new SinglePlayerViewModel(this);
            settingsDataContex = new SettingsViewModel(this);

            MainMenuPage.DataContext = mainMenuDataContext;                 //Задание контекста страниц
            SinglePlayerPage.DataContext = singlePlayerDataContext;
            SettingsPage.DataContext = settingsDataContex;

            CurrentPage = MainMenuPage;                             //Страница при загрузке
            Window = _window;
            
        }


        // команды изменения страниц
        public async void SlowOpacity()
        {
            await Task.Factory.StartNew(() =>
            {
                for (double i = 1.0; i > 0.0; i -= 0.05)
                {
                    FrameOpacity = i;
                    Thread.Sleep(50);
                }
                FrameOpacity = 0;
                //window.MainMenu.Visibility=Visibility.Hidden;
            });
        }

        public async void SlowOpacity(Page page)
        {
            await Task.Factory.StartNew(() =>
            {
                for (double i = 1.0; i > 0.0; i -= 0.1)
                {
                    FrameOpacity = i;
                    Thread.Sleep(50);
                }
                CurrentPage = page;
                //for (double i = 0.0; i < 1.1; i += 0.1)
                //{
                //    FrameOpacity = i;
                //    Thread.Sleep(50);
                //}
            });
        }
    }
}
