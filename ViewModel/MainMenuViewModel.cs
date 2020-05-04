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

        private RelayCommand appClose_Click;
        private RelayCommand singlePlayerMenuItem_Click;
        private RelayCommand settingsMenuItem_Click;

        public MainMenuViewModel(MainWindowViewModel _mainWindowViewModel)      //Начальная инициализация
        {
            mainWindowViewModel = _mainWindowViewModel;

        }

        public RelayCommand SinglePlayerMenuItem_Click                        //Комманда пункта меню "Одиночная игра"
        {
            get
            {
                return new RelayCommand(obj => {mainWindowViewModel.ChangePage(mainWindowViewModel.SinglePlayerPage); });
            }
        }
        public RelayCommand SettingsMenuItem_Click                            //Комманда пункта меню "Найстройки"
        {
            get
            {
                return new RelayCommand(obj => { mainWindowViewModel.ChangePage(mainWindowViewModel.SettingsPage); });
            }
        }
        public RelayCommand AppClose_Click                                    //Закрыть приложение
        {
            get
            {
                return new RelayCommand(obj => { App.Current.MainWindow.Close(); });
            }
        }
        

    }
}
