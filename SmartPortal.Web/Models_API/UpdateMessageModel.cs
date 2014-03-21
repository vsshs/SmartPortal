namespace SmartPortal.Web.Models_API
{
    public class UpdateMessageModel:ApiAuth
    {
        public string PatientId { get; set; }
        public string Message { get; set; }
    }
}
