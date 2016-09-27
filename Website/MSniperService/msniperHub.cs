using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Caching;
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

        public static DateTime PokemonLastCheck = DateTime.Now;
        public static DateTime RateLastCheck = DateTime.Now;
        private static readonly List<string> Feeders = new List<string>();
        private static readonly List<string> Listeners = new List<string>();

        public static RareList defaultRareList = new RareList()
        {
            PokemonNames = new List<string>(){
                                                "dragonite", "snorlax", "pikachu", "charmeleon",
                                                "vaporeon", "lapras", "gyarados","dragonair", "charizard",
                                                "blastoise", "magikarp", "dratini", "growlithe","pidgey"
                                             }
        };

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
                Clients.Client(this.Context.ConnectionId).ServerInfo(Feeders.Count, Listeners.Count);

                var rlist = CacheManager<RareList>.Instance.GetCache("RareList");
                if (rlist == null)
                    rlist = defaultRareList;

                Clients.Client(this.Context.ConnectionId)
                    .RareList(rlist.PokemonNames);
                return "connection established";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public void LPing()
        {
            if ((DateTime.Now - PokemonLastCheck) >= new TimeSpan(0, 0, 15))//default 15sec ;get fresh pokemons from feeders
            {
                PokemonLastCheck = DateTime.Now;
                Clients.Group(HubType.Feeder.ToString()).sendPokemon();

                Clients.Group(HubType.Listener.ToString()).ServerInfo(Feeders.Count, Listeners.Count); // server information feeder/hunter
            }

            if ((DateTime.Now - RateLastCheck) >= new TimeSpan(0, 1, 0))//top 5 snipped pokemons
            {
                RateLastCheck = DateTime.Now;
                var pco = CacheManager<PokemonCounter>.Instance.GetCache("PokemonCounter");
                if (pco == null)
                    pco = new PokemonCounter();
                var pokeInfos = pco.PCounter.OrderByDescending(p => p.Count).Take(5).ToList();
                Clients.Group(HubType.Listener.ToString()).rate(pokeInfos);
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
                    if (Feeders.IndexOf(this.Context.ConnectionId) == -1)
                        Feeders.Add(this.Context.ConnectionId);
                    break;

                case HubType.Listener:
                    Groups.Add(this.Context.ConnectionId, HubType.Listener.ToString());
                    if (Listeners.IndexOf(this.Context.ConnectionId) == -1)
                        Listeners.Add(this.Context.ConnectionId);
                    break;
            }
        }

        public void Leave()
        {
            var index = Feeders.IndexOf(this.Context.ConnectionId);
            if (index != -1)
            {
                Feeders.Remove(this.Context.ConnectionId);
            }
            else
            {
                index = Listeners.IndexOf(this.Context.ConnectionId);
                if (index != -1)
                {
                    Listeners.Remove(this.Context.ConnectionId);
                }
            }
        }
    }
}