using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evolution.ViewModel
{
    public class SinglePlayerViewModel : BaseViewModel
    {
        MainWindowViewModel mainWindowViewModel;
        public SinglePlayerViewModel(MainWindowViewModel _mainWindowViewModel)
        {
            mainWindowViewModel = _mainWindowViewModel;
            //_mainWindowViewModel.window.Close();
            
        }
    }
}
