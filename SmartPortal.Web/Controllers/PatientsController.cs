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
            var id = Session["NurseId"] as string;

            ViewBag.NurseId = id;
            ViewBag.TabletId = Session["NurseTablet"] as string;
            return View(patients);
        }

        

    }
}
