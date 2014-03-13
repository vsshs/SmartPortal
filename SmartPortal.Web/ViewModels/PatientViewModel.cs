using Newtonsoft.Json;

namespace SmartPortal.Web.ViewModels
{
    
    public class PatientViewModel
    {
        [JsonProperty("id")]
        public string Id;
        [JsonProperty("name")]
        public string Name;
        [JsonProperty("cpr")]
        public string Cpr;
        [JsonProperty("location")]
        public string Location;
        [JsonProperty("recordLoacation")]
        public string RecordLocation;
        [JsonProperty("procedure")]
        public string Procedure;
        [JsonProperty("color")]
        public string Color;
    }
}
