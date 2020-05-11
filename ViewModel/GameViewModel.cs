using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace evolution.ViewModel
{
    public class GameViewModel:BaseViewModel
    {
        MainWindowViewModel mainWindowViewModel;

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
                return new RelayCommand(obj => { MenuOpacity = 1; MenuVisibility = Visibility.Visible; });
            }
        }
        #endregion

    }
}
