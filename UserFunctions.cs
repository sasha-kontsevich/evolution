using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evolution
{
    public static class UserFunctions
    {
        public static void Shuffle<T>(List<T> list)
        {
            Random rand = new Random();

            for (int i = list.Count - 1; i >= 1; i--)
            {
                int j = rand.Next(i + 1);

                T tmp = list[j];
                list[j] = list[i];
                list[i] = tmp;
            }
        }
        public static string RuEngLang(string sE, string sR)
        {
            if (App.Language.Name == "en-US")
            {
                return sE;
            }
            else
            {
                return sR;
            }
        }
    }
}
