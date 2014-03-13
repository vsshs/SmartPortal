using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace SmartPortal.Web.Models_API
{
    public class ArduinoPatient
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("lastUpdated")]
        public long LastUpdated { get; set; }

        [JsonProperty("r")]
        public byte R { get; set; }

        [JsonProperty("g")]
        public byte G { get; set; }
        
        [JsonProperty("b")]
        public byte B { get; set; }

        [JsonProperty("buzzer")]
        public bool Buzzer { get; set; }

    }
}