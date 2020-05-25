using evolution.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace evolution.ViewModel
{
    public class ResultsViewModel : BaseViewModel
    {
        MainWindowViewModel mainWindowViewModel;
        List<Player> players;
        public ResultsViewModel(MainWindowViewModel _mainWindowViewModel, List<Player> _players)
        {
            mainWindowViewModel = _mainWindowViewModel;
            players = _players;
            players.OrderByDescending(p => p.Score);
            for(int i =0;i<players.Count;i++)
            {
                players.ToArray()[i].Number = i + 1;
            }
            ResultsHeader = UserFunctions.RuEngLang("Player ", "Игрок ")+ players.First().User.NickName + UserFunctions.RuEngLang(" wins ", " победил ");
            var query = from p in players
                        orderby p.Number
                        select new {Place = p.Number, Player = p.User.NickName, Score = p.Score, Result = GetRes(p) };
            Results = query.ToList();

            //Инфа о матче
            var context = new EvolutionDBContext();
            int matchID = (from m in context.Matches
                           select m.MatchID).Max();
            context.Matches.Add(new Match { MatchID = matchID + 1, Date = DateTime.Now, GameDuration = GameViewModel.t2 - GameViewModel.t1, PlayerCount = players.Count });
            context.SaveChanges();
            
            //Результаты игроков

            foreach (Player player in players)
            {
                try
                {
                    int matchResID = (from m in context.UserMatchResults
                                   select m.ID).Max()+1;

                    User user = (from u in context.Users
                                     where u.Login == player.User.Login
                                     select u).First();
                        context.UserMatchResults.Add(new UserMatchResults {ID = matchResID, MatchID = matchID + 1, UserID = user.ID, Place = player.Number, Score = player.Score, Result = GetRes(player) });
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    //ignore
                }
            }

            //Инфа о игроках
            foreach (Player player in players)
            {
                try
                {
                        User user = (from u in context.Users
                                     where u.Login == player.User.Login
                                     select u).First();
                        user.Rating += GetRes(player);
                    context.SaveChanges();
                }
                catch(Exception ex)
                {
                    //ignore
                }
            }


            context.SaveChanges();
        }
        private string resultsHeader;
        public string ResultsHeader
        {
            get { return resultsHeader; }
            set
            {
                if (resultsHeader == value)
                    return;
                resultsHeader = value;
                RaisePropertyChanged("ResultsHeader");
            }
        }
        private IEnumerable<object> results;
        public IEnumerable<object> Results
        {
            get { return results; }
            set
            {
                if (results == value)
                    return;
                results = value;
                RaisePropertyChanged("Results");
            }
        }
        public RelayCommand CompleteTheGame
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    mainWindowViewModel.ChangePage(mainWindowViewModel.MainMenuPage);
                });
            }
        }

        public int GetRes(Player player)
        {
            return   player.Score * 3;
        }
    }
}
