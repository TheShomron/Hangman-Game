using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman_C
{
    internal class Words
    {

        private string[] Hard_Words = { "waterfall", "slayer", "syndrome", "lengths", "whiskey", "bookworm", "bandwagon", "beekeeper", "blizzard", "jiujitsu" };
        private string[] Easy_Words = { "shop", "mall", "flag", "mom", "dad", "car", "blue", "red", "pie", "hat" };

        Random r = new Random();





        public string Get_Easy_Word()
        {

            return Easy_Words[r.Next(Easy_Words.Length)];

        }
        public string Get_Hard_Word()
        {

            return Hard_Words[r.Next(Hard_Words.Length)];

        }
    }
}
