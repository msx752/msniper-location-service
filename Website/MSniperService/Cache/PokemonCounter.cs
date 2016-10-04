using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSniperService.Models
{
    public class PokeInfo
    {
        public string PokemonName { get; set; }
        public int Count { get; set; }
    }

    public class PokemonCounter
    {
        public List<PokeInfo> PCounter = new List<PokeInfo>();
        public DateTime Firstdata = DateTime.Today;

        public void Increase(PokemonId pokemonName)
        {
            Increase(pokemonName.ToString());
        }

        public void Increase(string pokemonName)
        {
            TimeSpan ts = DateTime.Now - Firstdata;
            if (ts.TotalHours > 24)
            {
                Firstdata = DateTime.Today;
                for (int i = 0; i < PCounter.Count; i++)
                    PCounter[i].Count = 0;
            }
            int index = PCounter.FindIndex(p => p.PokemonName == pokemonName.ToLower());
            if (index == -1)
            {
                PCounter.Add(new PokeInfo() { PokemonName = pokemonName.ToLower(), Count = 1 });
            }
            else
            {
                PCounter[index].Count += 1;
            }
        }

        public PokeInfo this[string pokemonName]
        {
            get
            {
                return PCounter.FirstOrDefault(p => p.PokemonName == pokemonName.ToLower());
            }
        }

        public void Decrease(PokemonId pokemonName)
        {
            Decrease(pokemonName.ToString());

        }
        public void Decrease(string pokemonName)
        {
            if (Firstdata < DateTime.Now.AddHours(24))
            {
                Firstdata = DateTime.Today;
                for (int i = 0; i < PCounter.Count; i++)
                    PCounter[i].Count = 0;
            }
            int index = PCounter.FindIndex(p => p.PokemonName == pokemonName.ToLower());
            if (index == -1)
            {
                PCounter.Add(new PokeInfo() { PokemonName = pokemonName.ToLower(), Count = 0 });
            }
            else
            {
                if (PCounter[index].Count > 0)
                    PCounter[index].Count -= 1;
            }

        }


        public string UniqueKey()
        {
            return "PokemonCounter";
        }
    }
}