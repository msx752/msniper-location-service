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
        public static RareList defaultRareList = new RareList()
        {
            PokemonNames = new List<string>(){
                                                "dragonite", "snorlax", "pikachu", "charmeleon",
                                                "vaporeon", "lapras", "gyarados","dragonair", "charizard",
                                                "blastoise", "magikarp", "dratini", "growlithe","pidgey"
                                             }
        };

        private static readonly List<string> feeders = new List<string>();
        private static readonly List<string> listeners = new List<string>();
        private static readonly Timer pokemonTimer;
        private static readonly Timer pokemonTop5Timer;

        static msniperHub()
        {
            pokemonTimer = new Timer(pokemonTick, null,
                (int)new TimeSpan(0, 0, 15).TotalMilliseconds,
                (int)new TimeSpan(0, 0, 15).TotalMilliseconds);

            pokemonTop5Timer = new Timer(pokemonTop5Tick, null,
                (int)new TimeSpan(0, 1, 0).TotalMilliseconds,
                (int)new TimeSpan(0, 1, 0).TotalMilliseconds);
        }

        public static IHubContext Instance
        {
            get
            {
                return GlobalHost.ConnectionManager.GetHubContext<msniperHub>();
            }
        }

        public void Join(HubType groupName)
        {
            switch (groupName)
            {
                case HubType.Feeder:
                    Groups.Add(this.Context.ConnectionId, HubType.Feeder.ToString());
                    if (feeders.IndexOf(this.Context.ConnectionId) == -1)
                        feeders.Add(this.Context.ConnectionId);
                    break;

                case HubType.Listener:
                    Groups.Add(this.Context.ConnectionId, HubType.Listener.ToString());
                    if (listeners.IndexOf(this.Context.ConnectionId) == -1)
                        listeners.Add(this.Context.ConnectionId);
                    break;
            }
        }

        public void Leave()
        {
            var index = feeders.IndexOf(this.Context.ConnectionId);
            if (index != -1)
            {
                feeders.Remove(this.Context.ConnectionId);
            }
            else
            {
                index = listeners.IndexOf(this.Context.ConnectionId);
                if (index != -1)
                {
                    listeners.Remove(this.Context.ConnectionId);
                }
            }
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            Leave();
            return base.OnDisconnected(stopCalled);
        }

        private static void pokemonTick(object state)
        {
            Instance.Clients.Group(HubType.Feeder.ToString()).sendPokemon();
            Instance.Clients.Group(HubType.Listener.ToString()).ServerInfo(feeders.Count, listeners.Count); // server information feeder/hunter
        }

        private static void pokemonTop5Tick(object state)
        {
            var pco = CacheManager<PokemonCounter>.Instance.GetCache("PokemonCounter");
            if (pco == null)
                pco = new PokemonCounter();

            var pokeInfos = pco.PCounter.OrderByDescending(p => p.Count).Take(5).ToList();
            Instance.Clients.Group(HubType.Listener.ToString()).rate(pokeInfos);
        }

        #region signalr receivers

        public void Identity()
        {
            //feder joined
            Join(HubType.Feeder);
            Clients.Client(this.Context.ConnectionId).sendIdentity(this.Context.ConnectionId);
        }

        public void Rate(string pokemonName)
        {
            //check snipped pokemons
            var pco = CacheManager<PokemonCounter>.Instance.GetCache("PokemonCounter");
            if (pco == null)
                pco = new PokemonCounter();

            pco.Increase(pokemonName);
            CacheManager<PokemonCounter>.Instance.AddCache(pco);
        }

        public string RecvIdentity()
        {
            try
            {
                //visitor joined
                Join(HubType.Listener);
                Clients.Client(this.Context.ConnectionId).ServerInfo(feeders.Count, listeners.Count);

                var rlist = CacheManager<RareList>.Instance.GetCache("RareList");
                if (rlist == null)
                {
                    rlist = defaultRareList;
                    CacheManager<RareList>.Instance.AddCache(defaultRareList);
                }

                Clients.Client(this.Context.ConnectionId)
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
            if (data.Count > 0)
            {
                Clients.Group(HubType.Listener.ToString()).NewPokemons(data);
            }
        }

        #endregion signalr receivers
    }
}