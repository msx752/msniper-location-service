using MemoryManaging;
using MemoryManaging.Events;
using MSniperService.Models;
using System.Collections.Generic;

namespace MSniperService
{
    public partial class msniperData
    {
        public static readonly MemoryStore<RarePokemon> rarePokemons = new MemoryStore<RarePokemon>(true);

        public static readonly MemoryStore<EncounterInfo> Encounters = new MemoryStore<EncounterInfo>();

        public static readonly MemoryStore<PokeInfo> Pokeinfos = new MemoryStore<PokeInfo>(PokeinfosEvent, 152);

        protected static void PokeinfosEvent(MemoryStoreEventArgs<PokeInfo> e)
        {
            e.Store.Data.Count = 0;
        }

        public static readonly List<string> Feeders = new List<string>();
        public static readonly List<string> Listeners = new List<string>();

        private static readonly object Loginstate = new object();
    }
}