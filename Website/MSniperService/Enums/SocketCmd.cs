using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RMSniper1.Enums
{
    public enum SocketCmd
    {
        None = 0,
        Identity = 1,
        //PokemonCount = 2,
        SendPokemon = 3,
        //SendOneSpecies = 4,
        //Brodcaster = 5,
        //IpLimit = 6,
        //ServerLimit = 7,
        NewIvPercent = 8,
        ServerInfo = 9,
        NewPokemons = 10,
        ImListener = 11,
        LPing = 12,
        Rate = 13
    }
}