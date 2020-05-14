using evolution.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace evolution.ViewModel
{
    public class GameViewModel:BaseViewModel
    {
        MainWindowViewModel mainWindowViewModel;
        List<Player> players = new List<Player>();
        public List<Player> Players { get => players; set => players = value; }

        public GameViewModel(MainWindowViewModel _mainWindowViewModel)
        {
            mainWindowViewModel = _mainWindowViewModel;
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
