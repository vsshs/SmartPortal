﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;
using SmartPortal.Model;
using SmartPortal.Web.Hubs;
using SmartPortal.Web.Infrastructure;

namespace SmartPortal.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                return null;
            }
            //ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";



            //return RedirectToAction("Index", "Patients"); //View();
        }


        public class PinLoginModel
        {
            [Required]
            public string Pin { get; set; }

            [Required]
            public string TabletId { get; set; }
        }

        [HttpPost]
        public ActionResult Login(PinLoginModel model)
        {
            try
            {
                if (model != null && model.Pin != null)
                {
                    // verifyPin
                    var nurse = Portal.Instance().VerifyPin(model.Pin);

                    if (nurse == null)
                        return RedirectToAction("Index", "Home");

                    nurse.TabletId = model.TabletId;

                    nurse = Portal.Instance().UpdateNurse(nurse);

                    Session["NurseID"] = nurse.Id;
                    Session["NurseTablet"] = model.TabletId;

                    return RedirectToAction("Index", "Patients"); //View(); 
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                return null;
            }
        }



        public string Broadcast()
        {
            var rnd = new Random();
            PatientsManager.Instance.BrodcastLocationChange(1, "A." + rnd.Next(1, int.MaxValue));
            return "OK";
        }

        public string Add()
        {
            Portal.Instance().AddPatient(new Patient
            {
                Cpr = "1234" + DateTime.UtcNow.ToBinary(),
                Name = "P" + DateTime.UtcNow.ToBinary()
            });
            return "OK";
        }

        public string AddNurse()
        {
            Portal.Instance().AddNurse(new Nurse
            {
                Name = "DemoNurse",
                Pin = "123"
            });
            return "OK";
        }
    }
}
