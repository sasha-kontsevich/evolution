using System;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evolution.ViewModel
{
    class MainWindowViewModel : BaseViewModel
    {
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

        public MainWindowViewModel()
        {
            mainMenuPage = new MainMenu();
            singlePlayerPage = new View.SinglePlayer();
            CurrentPage = mainMenuPage;
            CurrentPage = singlePlayerPage;
        }
    }
}
