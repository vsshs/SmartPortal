using System.Linq;
using Newtonsoft.Json;
using SmartPortal.Model;

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
        [JsonProperty("rfid")]
        public int Rfid;

        [JsonProperty("lastMessage")] 
        public string LastMessage;

        public static PatientViewModel CreateFromPatient(Patient p)
        {
            var lastMessage = "";
            var m = p.NurseMessages.FirstOrDefault();
            if (m != null)
            {
               m = p.NurseMessages.OrderBy(msg => msg.CreatedAt).FirstOrDefault();
                if (m.Message != null)
                    lastMessage = m.Message;
            }
            return new PatientViewModel
            {
                Id = p.Id,
                Color = string.Format("rgb({0}, {1}, {2})", p.Color.Red, p.Color.Green, p.Color.Blue),
                Cpr = p.Cpr,
                Location = p.Location,
                Name = p.Name,
                Procedure = p.Procedure,
                RecordLocation = p.RecordLoaction,
                Rfid =  p.RfidTag,
                LastMessage = lastMessage

            };
        }
    }

    
}
