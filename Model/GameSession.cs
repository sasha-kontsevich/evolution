using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evolution.Model
{
    public class GameSession
    {
        public Dictionary<int, Card> Cards;
        public Dictionary<int, Player> Players;
        public Dictionary<int, Species> Species;
    }
}
