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


        private Page mainMenuPage;
        private MainMenuViewModel mainMenuDataContext;
        private Page singlePlayerPage;
        private SinglePlayerViewModel singlePlayerDataContext;
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

        public Page SinglePlayerPage { get => singlePlayerPage; set => singlePlayerPage = value; }

        public MainWindow window;
        public MainWindowViewModel(MainWindow _window)
        {
            mainMenuPage = new MainMenu();                          //Начальная инициализация страниц
            SinglePlayerPage = new SinglePlayer();

            mainMenuDataContext = new MainMenuViewModel(this);
            singlePlayerDataContext = new SinglePlayerViewModel(this);

            mainMenuPage.DataContext = mainMenuDataContext;
            SinglePlayerPage.DataContext = singlePlayerDataContext;

            CurrentPage = mainMenuPage;                             //Страница при загрузке
            window = _window;
            
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
