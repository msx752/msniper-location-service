using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSniperService.Models
{
    public class MSniperInfo2
    {
        public short PokemonId { get; set; }
        public ulong EncounterId { get; set; }
        public string SpawnPointId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Iv { get; set; }
    }

}