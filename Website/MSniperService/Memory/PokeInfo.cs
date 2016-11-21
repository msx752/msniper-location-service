using MemoryManaging;
using System;

namespace MSniperService.Models
{
    public class PokeInfo : IStoreValue
    {
        public string PokemonName { get; set; }
        public int Count { get; set; }

        public PokeInfo(string PName)
        {
            PokemonName = PName;
            Count = 1;
        }

        public void Dispose()
        {
            PokemonName = null;
            GC.SuppressFinalize(this);
        }
    }
}