namespace RMSniperFeeder
{
    public class EncounterInfo
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
    }
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