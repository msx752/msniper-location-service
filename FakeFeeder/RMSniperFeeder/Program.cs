using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading;

namespace RMSniperFeeder
{
    internal class Program
    {
        private static List<EncounterInfo> CreateData()
        {
            List<EncounterInfo> PkmnLocations = new List<EncounterInfo>();
            Random rn = new Random();
            int c = rn.Next(1, 10);
            for (int i = 1; i < c; i++)
            {
                PkmnLocations.Add(new EncounterInfo()
                {
                    EncounterId = (2157859740816806781 + rn.Next(1, 999)).ToString(),
                    Expiration = DateTime.Now.ToUnixTimestamp() + (int)new TimeSpan(0, 0, rn.Next(30, 90)).TotalMilliseconds,
                    PokemonName = ((PokemonId)rn.Next(1, 152)).ToString(),
                    Iv = 100,
                    Latitude = (-33.8646353402715 + rn.Next(1, 99999)).ToString("G17", CultureInfo.InvariantCulture),
                    Longitude = (151.20600957337419 + rn.Next(1, 99999)).ToString("G17", CultureInfo.InvariantCulture),
                    Move1 = ((PokemonMove)rn.Next(1, 241)).ToString(),
                    Move2 = ((PokemonMove)rn.Next(1, 241)).ToString(),
                    SpawnPointId = "6b12ae46d31"
                });
            }
            return PkmnLocations;
        }

        private static void Main(string[] args)
        {
            try
            {
                Run();
                do
                {
                    Thread.Sleep(99999);
                } while (true);
            }
            catch (Exception)
            {
            }
        }

        private static HubConnection connection;
        private static IHubProxy msniperHub;

        private static void Run()
        {
            connection = new HubConnection("http://localhost:55774/signalr", useDefaultUrl: false);
            msniperHub = connection.CreateHubProxy("msniperHub");
            connection.Received += Connection_Received;
            connection.Reconnecting += Connection_Reconnecting;
            connection.Reconnected += Connection_Reconnected;
            connection.Closed += Connection_Closed;
            connection.Start().Wait();

            msniperHub.Invoke("Send", "Identity", null);
        }

        private static void Connection_Closed()
        {
            Console.WriteLine("closed");
        }

        private static void Connection_Reconnected()
        {
            Console.WriteLine("reconnect");
            //msniperHub.Invoke("Send", "Identity", null);
        }

        private static void Connection_Reconnecting()
        {
            Console.WriteLine("reconnecting");
            Process.GetCurrentProcess().Kill();
        }

        private static void Connection_Received(string obj)
        {
            try
            {
                HubData xx = connection.JsonDeserializeObject<HubData>(obj);
                SocketCmd cmd = (SocketCmd)Enum.Parse(typeof(SocketCmd), xx.List[0]);
                switch (xx.Method)
                {
                    case "sendIdentity":
                        Console.WriteLine($"(Identity) [ {xx.List[1]} ] connection establisted");
                        Console.WriteLine("now waiting pokemon request (15sec)");
                        break;

                    case "sendPokemon":
                        var data = CreateData();
                        Console.WriteLine($"sending.. {data.Count} count");
                        msniperHub.Invoke("RecvPokemons", data);
                        break;
                }
            }
            catch (Exception e)
            {
            }
        }
    }
}