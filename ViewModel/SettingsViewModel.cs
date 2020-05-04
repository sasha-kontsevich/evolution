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

        private RelayCommand settingsOK;
        private RelayCommand settingsAccept;
        private RelayCommand settingsCancel;

        public SettingsViewModel(MainWindowViewModel _mainWindowViewModel)
        {
            mainWindowViewModel = _mainWindowViewModel;
        }

        public RelayCommand SettingsOK                        //Комманда пункта настроек "ОК"
        {
            get
            {
                return new RelayCommand(obj => { /*TODO: Сохранение настроек*/ mainWindowViewModel.ChangePage(mainWindowViewModel.MainMenuPage); });
            }
        }
        public RelayCommand SettingsAccept                            //Комманда пункта настроек "Принять"
        {
            get
            {
                return new RelayCommand(obj => { /*TODO: Сохранение настроек*/ });
            }
        }
        public RelayCommand SettingsCancel                                    //Комманда пункта настроек "Отмена"
        {
            get
            {
                return new RelayCommand(obj => { mainWindowViewModel.ChangePage(mainWindowViewModel.MainMenuPage); });
            }
        }
    }
}
