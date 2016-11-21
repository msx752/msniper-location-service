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
        private static Random rn = new Random();

        private static long unique = 0;

        public static List<EncounterInfo> CreateData()
        {
            var PkmnLocations = new List<EncounterInfo>();

            int c = 1;
            for (int i = 1; i < c + 1; i++)
            {
                PkmnLocations.Add(new EncounterInfo()
                {
                    EncounterId = (2157859740816806781 + unique).ToString(),
                    Expiration = DateTime.Now.ToUnixTimestamp() + (int)new TimeSpan(0, 0, rn.Next(3110, 11190)).TotalMilliseconds,
                    PokemonName = ((PokemonId)rn.Next(1, 152)).ToString(),
                    Iv = rn.Next(0, 101),
                    Latitude = (-33.8646353402715 + rn.Next(1, 99999)).ToString("G17", CultureInfo.InvariantCulture),
                    Longitude = (151.20600957337419 + rn.Next(1, 99999)).ToString("G17", CultureInfo.InvariantCulture),
                    Move1 = ((PokemonMove)rn.Next(1, 241)).ToString(),
                    Move2 = ((PokemonMove)rn.Next(1, 241)).ToString(),
                    SpawnPointId = "6b12ae46d31"
                });
                unique++;
            }
            return PkmnLocations;
        }

        private static void Main(string[] args)
        {
            try
            {
                for (int i = 0; i < 256; i++)
                {
                    CloneConnection clone = new CloneConnection(i + 1);
                    clone.Run();
                }
                do
                {
                    Thread.Sleep(99999);
                } while (true);
            }
            catch (Exception)
            {
            }
        }
    }
}