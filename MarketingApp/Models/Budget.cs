using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarketingApp.Models
{
    public class Budget
    {
        [JsonProperty("amount")]
        public float Amount { get; set; }
    }
}
