using Microsoft.AspNet.SignalR;
using MSniperService.Cache;
using MSniperService.Enums;
using MSniperService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MSniperService
{
    public class msniperHub : Hub
    {
        public static RareList DefaultRareList = new RareList()
        {
            PokemonNames = new List<string>(){
                    "dragonite", "snorlax", "pikachu", "charmeleon",
                    "vaporeon", "lapras", "gyarados","dragonair", "charizard",
                    "blastoise", "magikarp", "dratini", "growlithe","pidgey"
                                             }
        };

        private static List<string> Feeders { get; } = new List<string>();
        private static List<string> Listeners { get; } = new List<string>();
        private static Timer PokemonTimer { get; }
        private static Timer PokemonTop5Timer { get; }

        static msniperHub()
        {
            PokemonTimer = new Timer(pokemonTick, null,
                (int)new TimeSpan(0, 0, 15).TotalMilliseconds,
                (int)new TimeSpan(0, 0, 15).TotalMilliseconds);

            PokemonTop5Timer = new Timer(pokemonTop5Tick, null,
                (int)new TimeSpan(0, 1, 0).TotalMilliseconds,
                (int)new TimeSpan(0, 1, 0).TotalMilliseconds);
        }

        public static IHubContext Instance => GlobalHost.ConnectionManager.GetHubContext<msniperHub>();

        public static bool Join(HubType groupName, string connectionId)
        {
            switch (groupName)
            {
                case HubType.Feeder:
                    Instance.Groups.Add(connectionId, HubType.Feeder.ToString());
                    if (Feeders.IndexOf(connectionId) == -1)
                    {
                        Feeders.Add(connectionId);
                        return true;
                    }
                    break;
                case HubType.Listener:
                    Instance.Groups.Add(connectionId, HubType.Listener.ToString());
                    if (Listeners.IndexOf(connectionId) == -1)
                    {
                        Listeners.Add(connectionId);
                        return true;
                    }
                    break;
            }
            return false;
        }

        public static void Leave(string connectionId)
        {
            var index = Feeders.IndexOf(connectionId);
            if (index != -1)
            {
                Feeders.Remove(connectionId);
            }
            else
            {
                index = Listeners.IndexOf(connectionId);
                if (index != -1)
                {
                    Listeners.Remove(connectionId);
                }
            }
        }

        private static void pokemonTick(object state)
        {
            Instance.Clients.Group(HubType.Feeder.ToString())
                .sendPokemon();
            Instance.Clients.Group(HubType.Listener.ToString())
                .ServerInfo(Feeders.Count, Listeners.Count); // server information feeder/hunter
        }

        private static void pokemonTop5Tick(object state)
        {
            var pco = CacheManager<PokemonCounter>.Instance.GetCache("PokemonCounter") ?? new PokemonCounter();

            var pokeInfos = pco.PCounter.OrderByDescending(p => p.Count).Take(5).ToList();
            Instance.Clients.Group(HubType.Listener.ToString()).rate(pokeInfos);
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            Leave(this.Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }

        #region signalr receivers

        private bool firstPokemons = true;

        public void Identity()
        {
            //feder joined
            if (Join(HubType.Feeder, this.Context.ConnectionId))
            {
                Instance.Clients.Client(this.Context.ConnectionId)
                    .sendIdentity(this.Context.ConnectionId);
            }
        }

        public void Rate(string pokemonName)
        {
            //check snipped pokemons
            var pco = CacheManager<PokemonCounter>.Instance.GetCache("PokemonCounter") ?? new PokemonCounter();

            pco.Increase(pokemonName);
            CacheManager<PokemonCounter>.Instance.AddCache(pco);
        }

        public string RecvIdentity()
        {
            try
            {
                //visitor joined
                Join(HubType.Listener, this.Context.ConnectionId);
                Instance.Clients.Client(this.Context.ConnectionId)
                    .ServerInfo(Feeders.Count, Listeners.Count);
                var rlist = CacheManager<RareList>.Instance.GetCache("RareList");
                if (rlist == null)
                {
                    rlist = DefaultRareList;
                    CacheManager<RareList>.Instance.AddCache(DefaultRareList);
                }
                if (firstPokemons)
                {
                    Instance.Clients.Client(this.Context.ConnectionId)
                   .NewPokemons(CacheManager<EncounterInfo>
                   .Instance.GetAll()
                   .OrderBy(p => p.Iv)
                   .ToList());
                    firstPokemons = false;
                }

                Instance.Clients.Client(this.Context.ConnectionId)
                    .RareList(rlist.PokemonNames);


                return "connection established";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public void RecvPokemons(List<EncounterInfo> data)
        {
            if (data.Count > 0 /* && data.Count < 20temp flood fix */ )
            {
                CacheManager<EncounterInfo>.Instance.AddRangeCache(data);
                Instance.Clients.Group(HubType.Listener.ToString())
                    .NewPokemons(data);
            }
        }

        #endregion signalr receivers
    }
}