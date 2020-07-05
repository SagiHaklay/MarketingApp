using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarketingApp.Models
{
    public class BidBySection
    {
        [JsonProperty("sectionId")]
        public string SectionId { get; set; }
    }
}
