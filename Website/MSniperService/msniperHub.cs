using Microsoft.AspNet.SignalR;
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
        private static Timer PokemonTimer { get; }
        private static Timer PokemonTop5Timer { get; }

        static msniperHub()
        {
            PokemonTimer = new Timer(PokemonTick, null,
                 (int)new TimeSpan(0, 0, 10).TotalMilliseconds,
                 (int)new TimeSpan(0, 0, 10).TotalMilliseconds);

            PokemonTop5Timer = new Timer(PokemonTop5Tick, null,
                (int)new TimeSpan(0, 1, 0).TotalMilliseconds,
                (int)new TimeSpan(0, 1, 0).TotalMilliseconds);

            var rarePokemons = new List<string> {
                "dragonite", "snorlax", "pikachu", "charmeleon",
                "vaporeon", "lapras", "gyarados", "dragonair",
                "charizard", "blastoise", "magikarp", "dratini",
                "arcanine", "aerodactyl", "onix", "mrmime",
                "electabuzz", "zapdos", "articuno", "ditto",
                "eevee","farfetchd", "porygon"
            };

            rarePokemons.ForEach(p => msniperData.rarePokemons.Add(new RarePokemon(p)));
        }

        private static void PokemonTick(object state)
        {
            msniperData.Instance.Clients.Group(HubType.Feeder.ToString())
                .sendPokemon();
            msniperData.Instance.Clients.Group(HubType.Listener.ToString())
                .ServerInfo(msniperData.Connections.MemberCount, msniperData.Connections.MemberCount);
            // server information feeder/hunter
        }

        private static void PokemonTop5Tick(object state)
        {
            msniperData.Instance.Clients.Group(HubType.Listener.ToString())
                .rate(msniperData.Instance.GetRateList()
                .OrderByDescending(p => p.Count).Take(6).ToList());
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            msniperData.Instance.Leave(this.Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }

        public void Rate(string pokemonName)
        {
            msniperData.Instance.Rate(pokemonName);
        }

        public void identifiednecrobot(List<string> identities, string link)
        {
            msniperData.Instance.identifiednecrobot(identities, link);
        }

        public void Identity()
        {
            msniperData.Instance.Identity(this.Context.ConnectionId);
        }

        public string RecvIdentity()
        {
            return msniperData.Instance.RecvIdentity(this.Context.ConnectionId);
        }

        public void RecvPokemons(List<EncounterInfo> data)
        {
            msniperData.Instance.RecvPokemons(data);
        }
    }
}