using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartPortal.Web.Models_API
{
    public class UpdateProcedureModel:ApiAuth
    {
        public string PatientId { get; set; }
        public string Procedure { get; set; }
    }
}