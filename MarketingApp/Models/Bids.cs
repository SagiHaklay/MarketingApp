using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarketingApp.Models
{
    public class Bids
    {
        [JsonProperty("bySection")]
        public IEnumerable<BidBySection> BySection { get; set; }
    }
}
