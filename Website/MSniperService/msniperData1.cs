using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using MSniperService.Enums;
using MSniperService.Models;
using MSniperService.Statics;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Text.RegularExpressions;

namespace MSniperService
{
    public partial class msniperData
    {
        public static msniperData Instance => _instance.Value;

        private static readonly Lazy<msniperData> _instance = new Lazy<msniperData>(
            () => new msniperData(GlobalHost.ConnectionManager.GetHubContext<msniperHub>()));

        public IHubConnectionContext<dynamic> Clients { get; set; }
        public IGroupManager Groups { get; set; }

        public msniperData(IHubContext hub)
        {
            Clients = hub.Clients;
            Groups = hub.Groups;
        }

        public bool Join(HubType groupName, string connectionId)
        {
            if (Connections[connectionId] == null)
            {
                Groups.Add(connectionId, groupName.ToString());
                Connections.Add(new Connection(connectionId, groupName));
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Leave(string connectionId)
        {
            if (Connections[connectionId] != null)
            {
                Connections.Remove(connectionId);
            }
        }

        public void Rate(string pokemonName)
        {
            var fnd = Pokeinfos[pokemonName];
            if (fnd == null)
                Pokeinfos.Add(new PokeInfo(pokemonName), new TimeSpan(24, 0, 0));
            else
                fnd.Data.Count++;
        }

        public void identifiednecrobot(List<string> identities, string link)
        {
            for (var i = 0; i < identities.Count; i++)
            {
                if (Connections[identities[i]] != null)
                {
                    if (Connections[identities[i]].Data.Type == HubType.Feeder)
                    {
                        var mslnk = MSRGX(link);
                        Clients.Client(identities[i]).msvc(mslnk);
                        continue;
                    }
                }
                identities.RemoveAt(i);
                i--;
            }
        }

        private MSniperInfo2 MSRGX(string link)
        {
            var re0 = "(msniper://)"; //protocol
            var re1 = "((?:\\w+))";//pokemon name
            var re2 = "(\\/)";//separator
            var re3 = "(\\d+)";
            var re4 = "(\\/)";//separator
            var re5 = "((?:[a-zA-Z0-9]*))";
            var re6 = "(\\/)";//separator
            var re7 = "([+-]?\\d*\\.\\d+)(?![-+0-9\\.])";//lat
            var re8 = "(,)";//separator
            var re9 = "([+-]?\\d*\\.\\d+)(?![-+0-9\\.])";//lon
            var re10 = "(\\/)";//separator
            var re11 = "(\\d*(\\.)?\\d+)";

            var r = new Regex(re0 + re1 + re2 + re3 + re4 + re5 + re6 + re7 + re8 + re9 + re10 + re11, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            var m = r.Match(link);
            MSniperInfo2 mslnk = new MSniperInfo2();

            if (m.Success)
            {
                mslnk.EncounterId = ulong.Parse(m.Groups[4].Value);
                mslnk.SpawnPointId = m.Groups[6].Value;
                mslnk.Latitude = double.Parse(m.Groups[8].Value, CultureInfo.InvariantCulture);
                mslnk.Longitude = double.Parse(m.Groups[10].Value, CultureInfo.InvariantCulture);
                mslnk.Iv = double.Parse(m.Groups[12].Value);
                var name = (PokemonId)Enum.Parse(typeof(PokemonId), m.Groups[2].Value);
                mslnk.PokemonId = (short)name;
                return mslnk;
            }
            return mslnk;
        }

        public void RecvPokemons(List<EncounterInfo> data)
        {
            data = data.FindNonContains(Encounters.GetAll);
            if (data.Count <= 0) return;
            try
            {
                for (var i = 0; i < data.Count; i++)
                {
                    var elapsed = (int)(data[i].GetExpiration() - DateTime.Now).TotalMilliseconds;
                    if (elapsed > 0)
                    {
                        var snc = Encounters.Add(data[i], new TimeSpan(0, 0, 0, 0, elapsed), data[i].UniqueKey());
                        if (snc) continue;
                    }
                    data.RemoveAt(i);
                    i--;
                }
                if (data.Count > 0)
                    Clients.Group(HubType.Listener.ToString()).NewPokemons(data);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        #region rare pokemon list (sidebar) for admin

        public void DeleteRarePokemon(string pokemonName)
        {
            rarePokemons.Remove(pokemonName);
        }

        public void AddRarePokemon(string pokemonName)
        {
            rarePokemons.Add(new RarePokemon(pokemonName));
        }

        public List<string> GetRareList()
        {
            return rarePokemons.GetAll.Select(p => p.pokemonName).ToList();
        }

        #endregion rare pokemon list (sidebar) for admin

        public List<PokeInfo> GetRateList()
        {
            return Pokeinfos.GetAll;
        }

        #region login state

        public void Identity(string connectionId)
        {
            //feder joined
            if (Join(HubType.Feeder, connectionId))
                Clients.Client(connectionId).sendIdentity(connectionId);
        }

        public string RecvIdentity(string connectionId)
        {
            try
            {
                //visitor joined
                if (Join(HubType.Listener, connectionId))
                {
                    Clients.Client(connectionId).ServerInfo(Connections.MemberCount, Connections.MemberCount);
                    Clients.Client(connectionId).RareList(GetRareList());
                    var pokeInfos = Pokeinfos.GetAll.OrderByDescending(p => p.Count).Take(6).ToList();
                    Clients.Client(connectionId).rate(pokeInfos);
                    Clients.Client(connectionId).NewPokemons(Encounters.GetAll);
                    return $"connection established #{connectionId}";
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return null;
        }

        #endregion login state
    }
}