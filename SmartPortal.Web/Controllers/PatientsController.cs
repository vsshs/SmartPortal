using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartPortal.Web.Infrastructure;
using SmartPortal.Web.ViewModels;

namespace SmartPortal.Web.Controllers
{
    public class PatientsController : Controller
    {
        //
        // GET: /Patients/

        public ActionResult Index()
        {
            var patients = Portal.Instance().Patients;
            return View(patients);
        }

        

    }
}
