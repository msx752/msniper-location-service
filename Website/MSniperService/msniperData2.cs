using MemoryManaging;
using MSniperService.Models;
using System.Collections.Generic;

namespace MSniperService
{
    public partial class msniperData
    {
        public static readonly MemoryStore<RarePokemon> rarelist = new MemoryStore<RarePokemon>();

        public static readonly MemoryStore<EncounterInfo> Encounters = new MemoryStore<EncounterInfo>();

        public static readonly MemoryStore<PokeInfo> Pokeinfos = new MemoryStore<PokeInfo>(PokeinfosEvent, 152);

        protected static void PokeinfosEvent(MemoryStore<PokeInfo>.MemoryStoreEventArgs e)
        {
            e.Sender.Count = 1;
        }

        public static readonly MemoryStore<Connection> Connections = new MemoryStore<Connection>(ConnectionsEvent);

        protected static void ConnectionsEvent(MemoryStore<Connection>.MemoryStoreEventArgs e)
        {
        }
    }
}