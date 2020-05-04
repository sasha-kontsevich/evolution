using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evolution.ViewModel
{
    public class GameViewModel
    {
        MainWindowViewModel mainWindowViewModel;

        private RelayCommand appClose_Click;
        private RelayCommand singlePlayerMenuItem_Click;
        public GameViewModel(MainWindowViewModel _mainWindowViewModel)
        {
            mainWindowViewModel = _mainWindowViewModel;
        }
        

    }
}
