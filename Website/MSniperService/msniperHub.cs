using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using MSniperService.Enums;
using MSniperService.Models;

namespace MSniperService
{
    public class msniperHub : Hub
    {
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

        private static void pokemonTick(object state)
        {
            msniperData.Instance.Clients.Group(HubType.Feeder.ToString())
                .sendPokemon();
            msniperData.Instance.Clients.Group(HubType.Listener.ToString())
                .ServerInfo(msniperData.Feeders.Count, msniperData.Listeners.Count);
            // server information feeder/hunter
        }

        private static void pokemonTop5Tick(object state)
        {
            msniperData.Instance.Clients.Group(HubType.Listener.ToString())
                .rate(msniperData.Instance.GetRateList()
                .OrderByDescending(p => p.Count)
                .ToList());
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