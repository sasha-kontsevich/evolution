using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evolution.ViewModel
{
    public class SettingsViewModel
    {
        MainWindowViewModel mainWindowViewModel;
        public SettingsViewModel(MainWindowViewModel _mainWindowViewModel)
        {
            mainWindowViewModel = _mainWindowViewModel;
        }
    }
}
