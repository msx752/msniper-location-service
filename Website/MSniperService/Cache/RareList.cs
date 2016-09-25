using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RMSniper1.Cache
{
    public class RareList
    {
        public List<string> PokemonNames = new List<string>();

        public string UniqueKey()
        {
            return "RareList";
        }
    }
}