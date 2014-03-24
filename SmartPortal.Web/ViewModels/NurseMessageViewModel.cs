using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartPortal.Web.ViewModels
{
    public class NurseMessageViewModel
    {
        public string Message { get; set; }
        public string NurseName { get; set; }
        public DateTime Created { get; set; }
    }
}