using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartPortal.Web.ViewModels
{
    public class AddMessageViewModel
    {
        public string NurseId { get; set; }
        public string PatientId { get; set; }
        public string Message { get; set; }
    }
}