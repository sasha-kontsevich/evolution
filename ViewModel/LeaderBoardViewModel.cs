using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evolution.ViewModel
{
    public class LeaderBoardViewModel : BaseViewModel
    {
        MainWindowViewModel mainWindowViewModel;
        public LeaderBoardViewModel(MainWindowViewModel _mainWindowViewModel)
        {
            mainWindowViewModel = _mainWindowViewModel;
            Update();
        }
        public RelayCommand BackToMenu
        {
            get
            {
                return new RelayCommand(obj => { mainWindowViewModel.ChangePage(mainWindowViewModel.MainMenuPage); });
            }
        }
        public RelayCommand ToProfile
        {
            get
            {
                return new RelayCommand(obj => { mainWindowViewModel.ChangePage(mainWindowViewModel.ProfilePage); });
            }
        }
        private IEnumerable<object> leaderBoardList;
        public IEnumerable<object> LeaderBoardList
        {
            get { return leaderBoardList; }
            set
            {
                if (leaderBoardList == value)
                    return;
                leaderBoardList = value;
                RaisePropertyChanged("LeaderBoardList");
            }
        }
        public void Update()
        {
            var context = new EvolutionDBContext();
            var query = from u in context.Users
                        orderby u.Rating descending
                                     select new {ID = u.ID, u.NickName, u.Rating };
            LeaderBoardList = query.ToList();
        }
    }
}
