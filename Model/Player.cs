using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace evolution.Model
{
    public class Player
    {
        Image image;
        string login;
        string password;
        string nickname;
        int rating;
        int score;
        List<Reward> rewards;

        public Image Image { get => image; set => image = value; }
        public string Login { get => login; set => login = value; }
        public string Nickname { get => nickname; set => nickname = value; }
        public string Password { get => password; set => password = value; }
        public int Rating { get => rating; set => rating = value; }
        public int Score { get => score; set => score = value; }
        public List<Reward> Rewards { get => rewards; set => rewards = value; }
    }
}
