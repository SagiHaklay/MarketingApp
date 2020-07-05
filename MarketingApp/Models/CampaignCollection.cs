using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarketingApp.Models
{
    public class CampaignCollection
    {
        [JsonProperty("campaigns")]
        public IEnumerable<Campaign> Campaigns { get; set; }
        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }
        [JsonProperty("count")]
        public int Count { get; set; }
    }
}
