namespace SmartPortal.Web.Models_API
{
    public class UpdateColorModel : ApiAuth
    {
        public string PatientId { get; set; }
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }


    }
}
