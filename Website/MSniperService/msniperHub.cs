using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Caching;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using MSniperService.Cache;
using MSniperService.Enums;
using MSniperService.Models;
using MSniperService.Statics;

namespace MSniperService
{
    public class msniperHub : Hub
    {
        private readonly static Timer pokemonTimer;
        private readonly static Timer pokemonTop5Timer;
        //public static DateTime pokemonLastCheck = DateTime.Now;
        public static DateTime RateLastCheck = DateTime.Now;
        private static readonly List<string> feeders = new List<string>();
        private static readonly List<string> listeners = new List<string>();

        public static RareList defaultRareList = new RareList()
        {
            PokemonNames = new List<string>(){
                                                "dragonite", "snorlax", "pikachu", "charmeleon",
                                                "vaporeon", "lapras", "gyarados","dragonair", "charizard",
                                                "blastoise", "magikarp", "dratini", "growlithe","pidgey"
                                             }
        };
        
        static msniperHub()
        {
            pokemonTimer = new Timer(pokemonTick,null,
                (int)new TimeSpan(0, 0, 15).TotalMilliseconds,
                (int)new TimeSpan(0, 0, 15).TotalMilliseconds);

            pokemonTop5Timer = new Timer(pokemonTop5Tick, null,
                (int)new TimeSpan(0, 1, 0).TotalMilliseconds,
                (int)new TimeSpan(0, 1, 0).TotalMilliseconds);
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

        public static IHubContext Instance
        {
            get
            {
                return GlobalHost.ConnectionManager.GetHubContext<msniperHub>();
            }
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            Leave();
            return base.OnDisconnected(stopCalled);
        }

        #region signalr receivers

        public void RecvPokemons(List<EncounterInfo> data)
        {
            if (data.Count > 0)
            {
                Clients.Group(HubType.Listener.ToString()).NewPokemons(data);
            }
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

        public void Rate(string pokemonName)
        {
            //check snipped pokemons
            var pco = CacheManager<PokemonCounter>.Instance.GetCache("PokemonCounter");
            if (pco == null)
                pco = new PokemonCounter();

            pco.Increase(pokemonName);
            CacheManager<PokemonCounter>.Instance.AddCache(pco);
        }

        public void Identity()
        {
            //feder joined
            Join(HubType.Feeder);
            Clients.Client(this.Context.ConnectionId).sendIdentity(this.Context.ConnectionId);
        }

        #endregion

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
    }
}