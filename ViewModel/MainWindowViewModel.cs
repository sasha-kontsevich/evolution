﻿using System;
using System.Windows.Controls;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace evolution.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private static MainWindowViewModel instance;


        public static MainWindowViewModel getInstance()
        {
            if (instance == null)
                instance = new MainWindowViewModel();
            return instance;
        }


        private Page mainMenuPage;
        private Page singlePlayerPage;
        private Page currentPage;

        public Page CurrentPage
        {
            get { return currentPage; }
            set
            {
                if (currentPage == value)
                    return;

                currentPage = value;
                RaisePropertyChanged("CurrentPage");
            }
        }
        private double frameOpacity = 1;
        public double FrameOpacity
        {
            get { return frameOpacity; }
            set
            {
                if (frameOpacity == value)
                    return;

                frameOpacity = value;
                RaisePropertyChanged("FrameOpacity");
            }
        }
        public MainWindowViewModel()
        {
            mainMenuPage = new MainMenu();                          //Начальная инициализация страниц
            singlePlayerPage = new View.SinglePlayer();
            CurrentPage = mainMenuPage;                             //Страница при загрузке

        }

        public void SelectSinglePayer()
        {
            CurrentPage = singlePlayerPage;
        }
        // команды изменения страниц


        public async void SlowOpacity(Page page)
        {
            await Task.Factory.StartNew(() =>
            {
                //for (double i = 1.0; 1 > 0.0; i -= 0.1)
                //{
                //    FrameOpacity = i;
                //    Thread.Sleep(50);
                //}
                CurrentPage = page;
                for (double i = 0.0; i < 1.1; i += 0.1)
                {
                    FrameOpacity = i;
                    Thread.Sleep(50);
                }
            });
        }
    }
}
