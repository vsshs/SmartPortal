using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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


        public ActionResult Index2()
        {
            var patients = Portal.Instance().GetPatients();
            var viewModels = new Collection<PatientViewModel>();
            foreach (var patient in patients)
                viewModels.Add(PatientViewModel.CreateFromPatient(patient));

            return View(viewModels);
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
                DeviceId = model.DeviceId,
                SonitorTag = model.SonitorTag
            };

            Portal.Instance().AddPatient(patient);
            return RedirectToAction("Create");
        }

        [HttpGet]
        public ActionResult Edit(string patientId)
        {
            var patient = Portal.Instance().FindPatientById(patientId);
            if (patient == null)
                return HttpNotFound("Patient with id = " + patientId + " was not found");

            var model = PatientViewModel.CreateFromPatient(patient);

            if (model.NurseMessages == null)
                model.NurseMessages = new Collection<NurseMessageViewModel>();
            foreach (var nurseMessage in patient.NurseMessages.OrderByDescending(m => m.CreatedAt))
            {
                model.NurseMessages.Add(new NurseMessageViewModel
                {
                    NurseName = Portal.Instance().FindNurseById(nurseMessage.NurseId).Name,
                    Message = nurseMessage.Message
                });
            }
            return PartialView("_Edit", model);
        }


        [HttpGet]
        public ActionResult OtherPatientData(string patientId)
        {
            var patient = Portal.Instance().FindPatientById(patientId);
            if (patient == null)
                return HttpNotFound("Patient with id = " + patientId + " was not found");

            var model = PatientViewModel.CreateFromPatient(patient);
            
            return PartialView("_OtherPatientData", model);
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PatientViewModel model)
        {
            if (ModelState.IsValid)
            {
                var patient = Portal.Instance().FindPatientById(model.Id);
                if (patient == null)
                    return HttpNotFound("Patient with id = " + model.Id + " was not found");

                patient.Name = model.Name;
                patient.Cpr = model.Cpr;
                patient.Location = model.Location;
                patient.Procedure = model.Procedure;
                patient.DeviceId = model.DeviceId;
                patient.SonitorTag = model.SonitorTag;
                patient.RecordLoaction = model.RecordLocation;
                patient.Ews = model.Ews;

                Portal.Instance().UpdatePatient(patient);
            }

            return OtherPatientData(model.Id);
        }


        [HttpGet]
        public ActionResult AddMessage(string patientId)
        {
            var patient = Portal.Instance().FindPatientById(patientId);
            if (patient == null)
                return HttpNotFound("Patient with id = " + patientId + " was not found");
            var model = PatientViewModel.CreateFromPatient(patient);

            
            foreach (var nurseMessage in patient.NurseMessages.OrderByDescending(m => m.CreatedAt))
            {
                var nurse = Portal.Instance().FindNurseById(nurseMessage.NurseId);
                if (nurse == null)
                    throw new Exception("wrong ID in nurse message");

                    model.NurseMessages.Add(new NurseMessageViewModel
                    {
                        NurseName = nurse == null ? "_NOT FOUND_": nurse.Name,
                        Message = nurseMessage.Message,
                        Created = DateTime.FromBinary(nurseMessage.CreatedAt)
                    });
            }

            ViewBag.PatientId = patientId;
            return PartialView("_NurseMessages", model.NurseMessages);

        }

        [HttpPost]
        public ActionResult AddMessage(AddMessageViewModel model)
        {
            if (ModelState.IsValid)
            {
                var patient = Portal.Instance().FindPatientById(model.PatientId);
                if (patient == null)
                    return HttpNotFound("Patient with id = " + model.PatientId + " was not found");

                var nurse = Portal.Instance().FindNurseById(model.NurseId);
                if (nurse == null)
                    return HttpNotFound("Nurse with id = " + model.NurseId + " was not found");

                patient.NurseMessages.Add(new NurseMessage
                {
                    Message = model.Message,
                    NurseId = nurse.Id
                });

                patient.Blink = true;

                Portal.Instance().UpdatePatient(patient);

                var m = PatientViewModel.CreateFromPatient(patient);

                foreach (var nurseMessage in patient.NurseMessages.OrderByDescending(msg => msg.CreatedAt))
                {
                    var n = Portal.Instance().FindNurseById(nurseMessage.NurseId);
                    if (n == null)
                        throw new Exception("wrong ID in nurse message");
                    m.NurseMessages.Add(new NurseMessageViewModel
                    {
                        NurseName = n == null ? "_NOT FOUND_": nurse.Name,
                        Message = nurseMessage.Message,
                        Created = DateTime.FromBinary(nurseMessage.CreatedAt)
                    });
                }
                ViewBag.PatientId = model.PatientId;
                return PartialView("_NurseMessages", m.NurseMessages);

            }
            return null;
        }
    }
}
