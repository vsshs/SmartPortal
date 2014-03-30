using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;
using NooSphere.Model.Primitives;
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

        public ActionResult Logout()
        {
            Session["NurseId"] = null;
            Session["TabletId"] = null;
            return RedirectToAction("Index", "Home");
        }

        public bool EnableLocation()
        {
            Portal.RenderLocation = true;
            return Portal.RenderLocation;
        }

        public bool DisableLocation()
        {
            Portal.RenderLocation = false;
            return Portal.RenderLocation;
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

        public void Seed()
        {
            var patients = Portal.Instance().GetPatients();

            foreach (var patient in patients)
            {
                Portal.Instance().RemovePatient(patient);
            }

            Portal.Instance().AddPatient(new Patient
            {
                Name = "Celina C. Olesen",
                Cpr = "300867-4042"
            });

            Portal.Instance().AddPatient(new Patient
            {
                Name = "Sebastian B. Winther",
                Cpr = "300867-4043",
                DeviceId = 1113,
                Color = new Rgb(0, 0, 255)
            });

            Portal.Instance().AddPatient(new Patient
            {
                Name = "Mille M. Andreasen",
                Cpr = "300867-4045"
            });

            Portal.Instance().AddPatient(new Patient
            {
                Name = "Benjamin M. Ravn",
                Cpr = "120477-2769"
            });

            Portal.Instance().AddPatient(new Patient
            {
                Name = "Malthe M. Lind",
                Cpr = "180139-3361"
            });

            Portal.Instance().AddPatient(new Patient
            {
                Name = "Kasper C. Pedersen",
                Cpr = "261231-3167"
            });

            Portal.Instance().AddPatient(new Patient
            {
                Name = "Jonathan Gregersen",
                Cpr = "210450-1381"
            });

            Portal.Instance().AddPatient(new Patient
            {
                Name = "Line J. Johansen",
                Cpr = "220188-1490"
            });

            Portal.Instance().AddPatient(new Patient
            {
                Name = "Amanda Kjær",
                Cpr = "230857-2768",
                DeviceId = 1112,
                Color = new Rgb(0, 255, 0)

            });

            Portal.Instance().AddPatient(new Patient
            {
                Name = "Tobias A. Poulsen",
                Cpr = "210232-0789"
            });

            Portal.Instance().AddPatient(new Patient
            {
                Name = "Silas M. Andresen",
                Cpr = "040167-0505"
            });

            Portal.Instance().AddPatient(new Patient
            {
                Name = "Filippa M. Svendsen",
                Cpr = "211155-3354"
            });

            Portal.Instance().AddPatient(new Patient
            {
                Name = "Asger F. Olsen",
                Cpr = "100570-3413"
            });

            Portal.Instance().AddPatient(new Patient
            {
                Name = "Størm H. Olesen",
                Cpr = "120656-1285",
                DeviceId = 1111,
                Color = new Rgb(255, 0, 0)

            });

            Portal.Instance().AddPatient(new Patient
            {
                Name = "Melanie Lind",
                Cpr = "160235-2338"
            });

            Portal.Instance().AddPatient(new Patient
            {
                Name = "Mathias M. Vestergård",
                Cpr = "030456-2013"
            });

            Portal.Instance().AddPatient(new Patient
            {
                Name = "Sofia J. Nørgaard",
                Cpr = "210875-0246"
            });

            Portal.Instance().AddPatient(new Patient
            {
                Name = "Jasmin Olesen",
                Cpr = "261141-3664"
            });

            Portal.Instance().AddPatient(new Patient
            {
                Name = "Thea Lind",
                Cpr = "030966-2474"
            });

            Portal.Instance().AddPatient(new Patient
            {
                Name = "Maria Steffensen",
                Cpr = "120466-1886"
            });

            Portal.Instance().AddPatient(new Patient
            {
                Name = "Anders Hansen",
                Cpr = "110989-4121"
            });



        }
    }
}
