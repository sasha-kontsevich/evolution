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
        public RelayCommand ProfileMenuItem_Click                        //Комманда пункта меню "Рейтинг"
        {
            get
            {
                return new RelayCommand(obj => { mainWindowViewModel.ChangePage(mainWindowViewModel.ProfilePage); });
            }
        }
        public RelayCommand LeaderBoardMenuItem_Click                        //Комманда пункта меню "Рейтинг"
        {
            get
            {
                return new RelayCommand(obj => { mainWindowViewModel.ChangePage(mainWindowViewModel.LeaderBoardPage); });
            }
        }
        public RelayCommand RulesMenuItem_Click                            //Комманда пункта меню "Правила"
        {
            get
            {
                return new RelayCommand(obj => { mainWindowViewModel.ChangePage(mainWindowViewModel.RulesPage); });
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
                return new RelayCommand(obj => { mainWindowViewModel.AppClose(); });
            }
        }
        

    }
}
