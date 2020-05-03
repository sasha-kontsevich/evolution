using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace evolution.ViewModel
{
    class MainMenuViewModel : BaseViewModel
    {
        
        public MainMenuViewModel()
        {

        }

        private RelayCommand singlePlayerMenuItem_Click;
        public RelayCommand SinglePlayerMenuItem_Click
        {
            get
            {
                //return singlePlayerMenuItem_Click ??
                //    (singlePlayerMenuItem_Click = new RelayCommand(obj =>
                //    //SlowOpacity(singlePlayerPage)
                //    {
                //        MessageBox.Show("ef");
                //    }
                //    ));
                return new RelayCommand(obj => { MessageBox.Show("ef"); });
            }
        }
    }
}
