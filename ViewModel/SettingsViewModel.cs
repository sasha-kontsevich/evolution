﻿using evolution.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace evolution.ViewModel
{
    public class SettingsViewModel: BaseViewModel 
    {
        MainWindowViewModel mainWindowViewModel;

        public SettingsViewModel(MainWindowViewModel _mainWindowViewModel)
        {
            mainWindowViewModel = _mainWindowViewModel;
            Languages.Add("English");
            Languages.Add("Русский");
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
        private string selectedLanguage = "English";
        public string SelectedLanguage
        {
            get { return selectedLanguage; }
            set
            {
                if (selectedLanguage == value)
                    return;
                selectedLanguage = value;
                RaisePropertyChanged("SelectedLanguage");
            }
        }


        public RelayCommand SettingsOK                        //Комманда пункта настроек "ОК"
        {
            get
            {
                return new RelayCommand(obj => { ApplyChanges(); mainWindowViewModel.ChangePage(mainWindowViewModel.MainMenuPage); });
            }
        }
        public RelayCommand SettingsAccept                            //Комманда пункта настроек "Принять"
        {
            get
            {
                return new RelayCommand(obj => { ApplyChanges(); });
            }
        }
        public RelayCommand SettingsCancel                                    //Комманда пункта настроек "Отмена"
        {
            get
            {
                return new RelayCommand(obj => { mainWindowViewModel.ChangePage(mainWindowViewModel.MainMenuPage); });
            }
        }
        public void ApplyChanges()
        {
            ChangeLanguage();
        }
        private void ChangeLanguage()
        {
            CultureInfo lang;
            if (selectedLanguage == "Русский")
            {
                lang = new CultureInfo("ru-RU");
            }
            else
            {
                lang = new CultureInfo("en-US");
            }
            if (lang != null)
            {
                App.Language = lang;
            }
        }
    }
}

