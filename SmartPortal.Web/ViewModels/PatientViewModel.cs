using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json;
using SmartPortal.Model;

namespace SmartPortal.Web.ViewModels
{

    public class PatientViewModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("cpr")]
        public string Cpr { get; set; }
        [JsonProperty("location")]
        public string Location { get; set; }
        [JsonProperty("recordLoacation")]
        public string RecordLocation { get; set; }
        [JsonProperty("procedure")]
        public string Procedure { get; set; }
        [JsonProperty("color")]
        public string Color { get; set; }
        [JsonProperty("rfid")]
        public int Rfid { get; set; }

        [JsonProperty("deviceid")]
        public int DeviceId { get; set; }

        [JsonProperty("lastMessage")]
        public string LastMessage { get; set; }

        [JsonProperty("sonitorTag")]
        public string SonitorTag { get; set; }

        public ICollection<NurseMessageViewModel> NurseMessages { get; set; }

        public static PatientViewModel CreateFromPatient(Patient p)
        {
            var lastMessage = "";

            if (p.NurseMessages.Any())
            {
                var m = p.NurseMessages.OrderByDescending(msg => msg.CreatedAt).FirstOrDefault();
                if (m.Message != null)
                    lastMessage = m.Message;
            }
            return new PatientViewModel
            {
                Id = p.Id,
                Color = string.Format("rgb({0}, {1}, {2})", p.Color.Red, p.Color.Green, p.Color.Blue),
                Cpr = p.Cpr,
                Location = string.IsNullOrEmpty(p.Location)? "- - -":p.Location,
                Name = p.Name,
                Procedure = string.IsNullOrEmpty(p.Procedure) ? "- - -": p.Procedure,
                RecordLocation = string.IsNullOrEmpty(p.RecordLoaction) ? "- - -" : p.RecordLoaction,
                Rfid = p.RfidTag,
                LastMessage = string.IsNullOrEmpty(lastMessage)? "- - -":lastMessage,
                NurseMessages = new Collection<NurseMessageViewModel>()

            };
        }
    }


}
