using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartPortal.Model;
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
            //var patients = Portal.Instance().Patients;
            var id = Session["NurseId"] as string;

            ViewBag.NurseId = id;
            ViewBag.TabletId = Session["NurseTablet"] as string;
            return View();
        }


        [HttpGet]
        public ActionResult Create()
        {
            var model = new PatientViewModel();
            return View("Create", model);
        }


        [HttpPost]
        public ActionResult Create(PatientViewModel model)
        {
            var patient = new Patient
            {
                Name = model.Name,
                Cpr = model.Cpr,
                RfidTag = model.Rfid,
                Location = model.Location,
                DeviceId = model.DeviceId
            };

            Portal.Instance().AddPatient(patient);
            return RedirectToAction("Create");
        }
    }
}
