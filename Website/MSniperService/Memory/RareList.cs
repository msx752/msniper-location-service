using MemoryManaging;
using System;

namespace MSniperService.Models
{
    public class RarePokemon : IStoreValue
    {
        public string pokemonName;

        public RarePokemon(string pName)
        {
            pokemonName = pName;
            PrimaryKey = pokemonName;
        }

        public override string ToString()
        {
            return pokemonName;
        }

        public void Dispose()
        {
            PrimaryKey = null;
            pokemonName = null;
            GC.SuppressFinalize(this);
        }

        ~RarePokemon()
        {
            Dispose();
        }

        public string PrimaryKey { get; set; }
    }
}