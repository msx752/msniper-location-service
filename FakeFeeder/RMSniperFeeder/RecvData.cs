using Newtonsoft.Json;
using System.Collections.Generic;

namespace RMSniperFeeder
{
    public class HubData
    {
        [JsonProperty("H")]
        public string HubName { get; set; }

        [JsonProperty("M")]
        public string Method { get; set; }

        [JsonProperty("A")]
        public List<string> List { get; set; }
    }
}