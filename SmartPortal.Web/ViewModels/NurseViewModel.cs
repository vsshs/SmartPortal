using System.ComponentModel.DataAnnotations;

namespace SmartPortal.Web.ViewModels
{
    public class NurseViewModel
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Pin { get; set; }
    }
}