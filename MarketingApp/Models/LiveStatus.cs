using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarketingApp.Models
{
    public class LiveStatus
    {
        [JsonProperty("amountSpent")]
        public float AmountSpent { get; set; }
    }
}
