using MemoryManaging;
using MSniperService.Cache;
using MSniperService.Models;
using System.Collections.Generic;

namespace MSniperService
{
    public partial class msniperData
    {
        static msniperData()
        {
        }

        public static readonly RareList DefaultRareList = new RareList()
        {
            PokemonNames = new List<string>(){
                    "dragonite", "snorlax", "pikachu", "charmeleon",
                    "vaporeon", "lapras", "gyarados","dragonair", "charizard",
                    "blastoise", "magikarp", "dratini", "arcanine","aerodactyl",
                    "onix","mrmime","electabuzz","zapdos","articuno","ditto","eevee",
                "farfetchd","porygon"
            }
        };

        private static MemoryStore<EncounterInfo> encounters = new MemoryStore<EncounterInfo>(50000);

        private static MemoryStore<PokeInfo> pokeinfos = new MemoryStore<PokeInfo>(152, PokeinfosEvent);

        internal static void PokeinfosEvent(MemoryStore<PokeInfo>.MemoryStoreEventArgs e)
        {
            e.Sender.Count = 1;
        }

        public static MemoryStore<Connection> feeders = new MemoryStore<Connection>(50000, ConnectionsEvent);

        public static MemoryStore<Connection> listeners = new MemoryStore<Connection>(10000, ConnectionsEvent);

        internal static void ConnectionsEvent(MemoryStore<Connection>.MemoryStoreEventArgs e)
        {
        }
    }
}