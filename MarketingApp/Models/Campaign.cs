using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarketingApp.Models
{
    public class Campaign
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("creationTime")]
        public string CreationTime { get; set; }
        [JsonProperty("budget")]
        public Budget Budget { get; set; }
        [JsonProperty("liveStatus")]
        public LiveStatus LiveStatus { get; set; }
        [JsonProperty("bids")]
        public Bids Bids { get; set; }
    }
}
