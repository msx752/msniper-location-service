using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using RMSniper1.Cache;
using RMSniper1.Enums;
using RMSniper1.Models;
using RMSniper1.Statics;

namespace RMSniper1
{
    public class msniperHub : Hub
    {
        public static RareList defaultRareList = new RareList()
        {
            PokemonNames = new List<string>()
                             {
                                "dragonite", "snorlax", "pikachu", "charmeleon",
                                "vaporeon", "lapras", "gyarados","dragonair", "charizard",
                                "blastoise", "magikarp", "dratini", "growlithe","pidgey"
                            }
        };

        public static DateTime PokemonLastCheck = DateTime.Now;
        public static DateTime RateLastCheck = DateTime.Now;
        private static readonly List<string> Feeders = new List<string>();
        private static readonly List<string> Listeners = new List<string>();

        //public static List<EncounterInfo> pokemons = new List<EncounterInfo>();

        public override Task OnDisconnected(bool stopCalled)
        {
            Leave();
            return base.OnDisconnected(stopCalled);
        }
        public void Send(string command, string data)
        {
            SocketCmd cmd = (SocketCmd)Enum.Parse(typeof(SocketCmd), command);
            PokemonCounter pco;
            switch (cmd)
            {
                case SocketCmd.Identity:
                    //feder joined
                    Join(HubType.Feeder);
                    Clients.Client(this.Context.ConnectionId).sendIdentity(cmd.ToString(), this.Context.ConnectionId);
                    break;

                case SocketCmd.ImListener:
                    //visitor joined
                    Join(HubType.Listener);
                    Clients.Client(this.Context.ConnectionId).ImListener("connection established");
                    //Clients.Client(this.Context.ConnectionId).NewPokemons(JsonConvert.SerializeObject(CacheManager.Instance.GetAll())); //get all data instead of mvc render
                    Clients.Client(this.Context.ConnectionId).ServerInfo(Feeders.Count, Listeners.Count);


                    var rlist = CacheManager<RareList>.Instance.GetCache("RareList");
                    if (rlist == null)
                    {
                        rlist = defaultRareList;
                    }
                    Clients.Client(this.Context.ConnectionId)
                        .SendRareList(rlist.PokemonNames);
                    break;


                case SocketCmd.SendPokemon:
                    //fresh pokemons came
                    var notFound = CacheManager<EncounterInfo>.Instance.NonContainsCache(JsonConvert.DeserializeObject<List<EncounterInfo>>(data));
                    if (notFound.Count > 0)
                    {
                        Dictionary<string, List<EncounterInfo>> tolistener =
                            new Dictionary<string, List<EncounterInfo>>();
                        tolistener.Add("data", notFound);
                        Clients.Group(HubType.Listener.ToString()).NewPokemons(JsonConvert.SerializeObject(tolistener));
                    }
                    break;

                case SocketCmd.LPing:
                    if ((DateTime.Now - PokemonLastCheck) >= new TimeSpan(0, 0, 15))//get fresh pokemons from feeders
                    {
                        PokemonLastCheck = DateTime.Now;
                        Clients.Group(HubType.Feeder.ToString()).sendPokemon(SocketCmd.SendPokemon.ToString(), "");

                        Clients.Group(HubType.Listener.ToString()).ServerInfo(Feeders.Count, Listeners.Count); // server information feeder/hunter
                    }

                    if ((DateTime.Now - RateLastCheck) >= new TimeSpan(0, 1, 0))//top 5 snipped pokemons
                    {
                        RateLastCheck = DateTime.Now;
                        pco = CacheManager<PokemonCounter>.Instance.GetCache("PokemonCounter");
                        if (pco == null)
                            pco = new PokemonCounter();
                        var pokeInfos = pco.PCounter.OrderByDescending(p => p.Count).Take(5).ToList();
                        Clients.Group(HubType.Listener.ToString()).SendRate(pokeInfos);
                    }
                    break;

                case SocketCmd.Rate:
                    //check snipped pokemons
                    pco = CacheManager<PokemonCounter>.Instance.GetCache("PokemonCounter");
                    if (pco == null)
                        pco = new PokemonCounter();

                    pco.Increase(data);
                    CacheManager<PokemonCounter>.Instance.AddCache(pco);
                    break;

            }
        }

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