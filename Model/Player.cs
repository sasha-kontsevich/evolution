using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace evolution.Model
{
    public class Player
    {
        private int number;
        private User user;
        Color color;

        public Player(User _user, int _number)
        {
            User = _user;
            Number = _number;
        }
        public Player()
        {

        }

        public User User { get => user; set => user = value; }
        public Color Color { get => color; set => color = value; }
        public int Number { get => number; set => number = value; }
    }
}
