using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using MSniperService.Statics;

namespace MSniperService.Models
{
    public class EncounterInfo
    {
        private bool disposed;

        public string EncounterId { get; set; }
        public double Iv { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Move1 { get; set; }
        public string Move2 { get; set; }
        public string PokemonName { get; set; }
        public string SpawnPointId { get; set; }
        public long Expiration { get; set; }

        public string UniqueKey()//for caching
        {
            return $"{EncounterId}-{SpawnPointId}";
        }

        public DateTime GetDateTime()
        {
            return DateConverter.JavaTimeStampToDateTime(Expiration);
        }
        public string GetLink1()
        {
            return $"msniper://{PokemonName}/{EncounterId}/{SpawnPointId}/{Latitude},{Longitude}/{Iv}";
        }
        public string GetLink2()
        {
            return $"pokesniper2://{PokemonName}/{Latitude},{Longitude}";
        }
    }
}