using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evolution.Model
{
    public static class GameSettings
    {
        private static string language;

        public static string Language { get => language; set => language = value; }
    }
}
