using MemoryManaging;
using MSniperService.Statics;
using System;

namespace MSniperService.Models
{
    public class EncounterInfo : IStoreValue
    {
        public string EncounterId { get; set; }
        public double Iv { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Move1 { get; set; }
        public string Move2 { get; set; }
        public string PokemonName { get; set; }
        public string SpawnPointId { get; set; }
        public long Expiration { get; set; }

        public string UniqueKey()
        {
            return $"{PokemonName}-{EncounterId}";
        }

        public DateTime GetExpiration()
        {
            return DateConverter.JavaTimeStampToDateTime(Expiration);
        }

        public void Dispose()
        {
            EncounterId = null;
            Iv = 0;
            Latitude = null;
            Longitude = null;
            Expiration = 0;
            SpawnPointId = null;
            Move1 = null;
            Move2 = null;
            GC.SuppressFinalize(this);
        }
    }
}