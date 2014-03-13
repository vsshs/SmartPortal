using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            //ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return RedirectToAction("Index", "Patients"); //View();
        }


        public string Broadcast()
        {
            var rnd = new Random();
            PatientsManager.Instance.BrodcastLocationChange(1, "A."+rnd.Next(1, int.MaxValue));
            return "OK";
        }

        public string Add()
        {
            Portal.Instance().AddPatient(new Patient
            {
                Cpr = "1234" + DateTime.UtcNow.ToBinary(),
                Name = "P"+DateTime.UtcNow.ToBinary()
            });
            return "OK";
        }

        public string Get()
        {
            return Portal.Instance().Get();
        }

        public string LastUpdated()
        {
            var p = Portal.Instance().Patients.First();
            return p.LastUpdated.ToBinary().ToString();
        }
        public string Update()
        {
            var p = Portal.Instance().Patients.First();

            p.Name = "UPDATED" + DateTime.Now.ToBinary();

            Portal.Instance().UpdatePatient(p);

            return "OK";
        }
        
    }
}
