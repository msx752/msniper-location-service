using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using MSniperService.Cache;
using MSniperService.Enums;
using MSniperService.Models;

namespace MSniperService
{
    public class msniperData
    {
        public static readonly RareList DefaultRareList = new RareList()
        {
            PokemonNames = new List<string>(){
                    "dragonite", "snorlax", "pikachu", "charmeleon",
                    "vaporeon", "lapras", "gyarados","dragonair", "charizard",
                    "blastoise", "magikarp", "dratini", "arcanine","aerodactyl","onix" }
        };
        public static readonly List<string> Feeders = new List<string>();
        public static readonly List<string> Listeners = new List<string>();
        public static readonly PokemonCounter RateList = new PokemonCounter();

        public static msniperData Instance => _instance.Value;
        private static readonly Lazy<msniperData> _instance = new Lazy<msniperData>(
            () => new msniperData(GlobalHost.ConnectionManager.GetHubContext<msniperHub>()));

        private readonly object _loginstate = new object();
        private readonly object _rareList = new object();
        private readonly object _rateList = new object();

        public IHubConnectionContext<dynamic> Clients { get; set; }
        public IGroupManager Groups { get; set; }

        public msniperData(IHubContext hub)
        {
            Clients = hub.Clients;
            Groups = hub.Groups;
        }

        public bool Join(HubType groupName, string connectionId)
        {
            lock (_loginstate)
            {
                switch (groupName)
                {
                    case HubType.Feeder:
                        Groups.Add(connectionId, HubType.Feeder.ToString());
                        if (Feeders.IndexOf(connectionId) == -1)
                        {
                            Feeders.Add(connectionId);
                            return true;
                        }
                        break;
                    case HubType.Listener:
                        Groups.Add(connectionId, HubType.Listener.ToString());
                        if (Listeners.IndexOf(connectionId) == -1)
                        {
                            Listeners.Add(connectionId);
                            return true;
                        }
                        break;
                }
                return false;
            }
        }

        public void Leave(string connectionId)
        {
            lock (_loginstate)
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
        }

        public void Rate(string pokemonName)
        {
            lock (_rateList)
            {
                RateList.Increase(pokemonName);
            }
        }

        public void RecvPokemons(List<EncounterInfo> data)
        {
            if (data.Count > 0 /* && data.Count < 20 temp flood fix */ )
            {
               Clients.Group(HubType.Listener.ToString())
                    .NewPokemons(CacheManager<EncounterInfo>.Instance.NonContainsCache(data));
            }
        }

        #region rare pokemon list (sidebar) for admin
        public void DeleteRarePokemon(string pokemonName)
        {
            lock (_rareList)
            {
                int index = DefaultRareList.PokemonNames.IndexOf(pokemonName.ToLower());
                if (index != -1)
                {
                    DefaultRareList.PokemonNames.RemoveAt(index);
                }
            }
        }

        public void AddRarePokemon(string pokemonName)
        {
            lock (_rareList)
            {
                int index = DefaultRareList.PokemonNames.IndexOf(pokemonName.ToLower());
                if (index == -1)
                {
                    DefaultRareList.PokemonNames.Add(pokemonName);
                }
            }
        }
        public List<string> GetRareList()
        {
            lock (_rareList)
            {
                return DefaultRareList.PokemonNames;
            }
        }
        #endregion
        
        public List<PokeInfo> GetRateList()
        {
            lock (_rateList)
            {
                return RateList.PCounter;
            }
        }
        #region login state
        public void Identity(string connectionId)
        {
            //feder joined
            if (Join(HubType.Feeder, connectionId))
            {
                Clients.Client(connectionId)
                    .sendIdentity(connectionId);
            }
        }

        public string RecvIdentity(string connectionId)
        {
            try
            {
                //visitor joined
                if (Join(HubType.Listener, connectionId))
                {
                    Clients.Client(connectionId)
                    .ServerInfo(Feeders.Count, Listeners.Count);
                    //
                    Clients.Client(connectionId)
                        .RareList(GetRareList());
                    //
                    var pokeInfos = RateList.PCounter.OrderByDescending(p => p.Count).Take(6).ToList();
                    Clients.Client(connectionId).rate(pokeInfos);
                    //
                    Clients.Client(connectionId)
                        .NewPokemons(CacheManager<EncounterInfo>
                            .Instance.GetAll()
                            .OrderBy(p => p.Iv)
                            .ToList());
                    return "connection established - " + connectionId;
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return null;
        }
        #endregion

    }
}