namespace SmartPortal.Web.Models_API
{
    public class UpdateRoomModel:ApiAuth
    {
        public string PatientId { get; set; }
        public string Room { get; set; }
    }
}
