using MemoryManaging;
using System;

namespace MSniperService.Models
{
    public class PokeInfo : IStoreValue
    {
        public string PokemonName { get; set; }
        public int Count { get; set; }

        public PokeInfo(string PName, string pKey = null)
        {
            PokemonName = PName;
            Count = 1;
            PrimaryKey = pKey ?? PokemonName;
        }

        public void Dispose()
        {
            PrimaryKey = null;
            PokemonName = null;
            GC.SuppressFinalize(this);
        }

        ~PokeInfo()
        {
            Dispose();
        }

        public string PrimaryKey { get; set; }
    }
}