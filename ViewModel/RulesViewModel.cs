using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evolution.ViewModel
{
    public class RulesViewModel
    {
        MainWindowViewModel mainWindowViewModel;
        public RulesViewModel(MainWindowViewModel _mainWindowViewModel)
        {
            mainWindowViewModel = _mainWindowViewModel;
        }
        public RelayCommand BackToMenu                               //Вернуться в главное меню
        {
            get
            {
                return new RelayCommand(obj => { Return(); });
            }
        }
        public RelayCommand ToSinglePlayer                               //Вернуться в главное меню
        {
            get
            {
                return new RelayCommand(obj => { Return(); });
            }
        }
        public void Return()
        {
            if (mainWindowViewModel.CurrentPage == mainWindowViewModel.RulesPage)
            {
                mainWindowViewModel.ChangePage(mainWindowViewModel.MainMenuPage);
            }
            else
            {
                mainWindowViewModel.GameContext.GameFrame = null;
            }
        }

    }
}
