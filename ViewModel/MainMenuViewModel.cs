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

namespace evolution.ViewModel
{
    class MainMenuViewModel : BaseViewModel
    {
        MainWindowViewModel mainWindowViewModel;
        public MainMenuViewModel(MainWindowViewModel _mainWindowViewModel)              //Начальная инициализация
        {
            mainWindowViewModel = _mainWindowViewModel;

        }

        private RelayCommand singlePlayerMenuItem_Click;                        //Комманда пункта меню "Одиночная игра"
        public RelayCommand SinglePlayerMenuItem_Click
        {
            get
            {
                return new RelayCommand(obj => {mainWindowViewModel.CurrentPage = mainWindowViewModel.SinglePlayerPage; });
            }
        }

        private RelayCommand settingsMenuItem_Click;                        //Комманда пункта меню "Найстройки"
        public RelayCommand SettingsMenuItem_Click
        {
            get
            {
                return new RelayCommand(obj => { mainWindowViewModel.CurrentPage = mainWindowViewModel.SettingsPage; });
            }
        }

        private RelayCommand appClose_Click;
        public RelayCommand AppClose_Click
        {
            get
            {
                return new RelayCommand(obj => { App.Current.MainWindow.Close(); });
            }
        }
        //public async void SlowOpacity()
        //{
        //    await Task.Factory.StartNew(() =>
        //    {
        //        for (double i = 1.0; i > 0.0; i -= 0.05)
        //        {
        //            FrameOpacity = i;
        //            Thread.Sleep(50);
        //        }
        //        FrameOpacity = 0;
        //        window.MainMenu.Visibility=Visibility.Hidden;
        //    });
        //}
    }
}
