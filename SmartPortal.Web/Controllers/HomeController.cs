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
using System.Collections.ObjectModel;

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

        public string Seed()
        {
            var patients = Portal.Instance().GetPatients();

            foreach (var patient in patients)
            {
                Portal.Instance().RemovePatient(patient);
            }


            

            //"cf888dce-5295-4082-a132-1dc6d8eeb1ef"
            //"3cd3fe8f-2e7e-4478-9e70-18ff0f0fa96f"

            var nurse = "fc7cf126-5f63-42cb-b12c-15e847720373";//nurses.ToArray()[0].Id;
            var doctor = "22a01251-5799-4d33-903d-98083d76ed24";//nurses.ToArray()[1].Id;

            if (Portal.Instance().FindNurseById(nurse) == null)
                Portal.Instance().AddNurse(new Nurse
                {
                    Id = nurse,
                    Name = "FIX ME 1",
                    Pin = "1"
                });

            if (Portal.Instance().FindNurseById(doctor) == null)
                Portal.Instance().AddNurse(new Nurse
                {
                    Id = doctor,
                    Name = "Fix Me 2",
                    Pin = "2"

                });

            Portal.Instance().AddPatient(new Patient
            {
                Name = "Celina C. Olesen",
                Cpr = "300867-4042",
                DeviceId = 1113,
                SonitorTag = "1606",
                Color = new Rgb(0, 255, 0),
                Location = "B4",
                Procedure = "Blood test",
                NurseMessages = new ObservableCollection<NurseMessage>(
                     new List<NurseMessage>
                    {
                        new NurseMessage
                        {
                            CreatedAt = new DateTime(2014,04,01,12,15,0, DateTimeKind.Local).ToBinary(),
                            Message = "All vital signs are OK and patient is sleeping",
                            NurseId = nurse
                        },
                        new NurseMessage
                        {
                            CreatedAt = new DateTime(2014,04,01,03,20,0, DateTimeKind.Local).ToBinary(),
                            Message = "Stable and sleeping",
                            NurseId = nurse
                        },
                        new NurseMessage
                        {
                            CreatedAt = new DateTime(2014,04,01,05,55,0, DateTimeKind.Local).ToBinary(),
                            Message = "Patient not feeling well",
                            NurseId = nurse
                        }
                       
                    })

            });

            Portal.Instance().AddPatient(new Patient
            {
                Name = "Paolo Tell",
                Cpr = "010983-4069",
                Location = "B4",
                DeviceId = 1111,
                Procedure = "Observation",
                SonitorTag = "1607",
                Color = new Rgb(255, 0, 0),
                NurseMessages = new ObservableCollection<NurseMessage>(
                    new List<NurseMessage>
                    {
                        new NurseMessage
                        {
                            CreatedAt = new DateTime(2014,04,01,01,10,0, DateTimeKind.Local).ToBinary(),
                            Message = "Patient complains about feel nauseous-- I gave him extra pain killers",
                            NurseId = nurse
                        },
                        new NurseMessage
                        {
                            CreatedAt = new DateTime(2014,04,01,02,15,0, DateTimeKind.Local).ToBinary(),
                            Message = "Patient pols and temperature is up and he is vomiting regularly. I called in the night doctor",
                            NurseId = nurse
                        },
                        new NurseMessage
                        {
                            CreatedAt = new DateTime(2014,04,01,03,25,0, DateTimeKind.Local).ToBinary(),
                            Message = "Assessed state of patient and gave him medication for vomiting",
                            NurseId = doctor
                        },
                        new NurseMessage
                        {
                            CreatedAt = new DateTime(2014,04,01,04,40,0, DateTimeKind.Local).ToBinary(),
                            Message = "Temperature down a bit but still high pulse.",
                            NurseId = nurse
                        },
                        new NurseMessage
                        {
                            CreatedAt = new DateTime(2014,04,01,05,50,0, DateTimeKind.Local).ToBinary(),
                            Message = "Patient still has a high pulse, fever and breeding problems",
                            NurseId = nurse
                        }
                        
                    })

            });

            Portal.Instance().AddPatient(new Patient
            {
                Name = "Sebastian B. Winther",
                Cpr = "300867-4043",
                DeviceId = 1112,
                Procedure = "None",
                Color = new Rgb(0, 0, 255),
                Location = "B3",
                SonitorTag = "1582",
                NurseMessages = new ObservableCollection<NurseMessage>(
                   new List<NurseMessage>
                    {
                        new NurseMessage
                        {
                            CreatedAt = new DateTime(2014, 04, 01, 02, 30, 0, DateTimeKind.Local).ToBinary(),
                            Message = "Patient has low saturation and blood pressure. Mild breading problems. Still not concious.",
                            NurseId = nurse
                        },
                        new NurseMessage
                        {
                            CreatedAt = new DateTime(2014, 04, 01, 03, 15, 0, DateTimeKind.Local).ToBinary(),
                            Message = "Saturation and blood pressure still low, mild fever . Mild breading problems. Still not concious.",
                            NurseId = nurse
                        },
                        new NurseMessage
                        {
                            CreatedAt = new DateTime(2014, 04, 01, 05, 15, 0, DateTimeKind.Local).ToBinary(),
                            Message = "Patient is stable, but should be closely monitored",
                            NurseId = doctor
                        },
                        new NurseMessage
                        {
                            CreatedAt = new DateTime(2014, 04, 01, 06, 05, 0, DateTimeKind.Local).ToBinary(),
                            Message = "Saturation and blood pressure has normalized. Still not concious.",
                            NurseId = nurse
                        }
                        
                    })
            });

            Portal.Instance().AddPatient(new Patient
            {
                Name = "Mille M. Andreasen",
                Cpr = "300867-4045",
                Location = "B5",
                Procedure = "Electroencephalography",
                Color = new Rgb(255, 255, 0),
                NurseMessages = new ObservableCollection<NurseMessage>(
                   new List<NurseMessage>
                    {
                        new NurseMessage
                        {
                            CreatedAt = new DateTime(2014, 04, 01, 02, 30, 0, DateTimeKind.Local).ToBinary(),
                            Message = "Patient not concious.",
                            NurseId = nurse
                        }
                        
                    })
            });

            Portal.Instance().AddPatient(new Patient
            {
                Name = "Benjamin M. Ravn",
                Cpr = "120477-2769",
                Location = "B5",
                Color = new Rgb(0, 255, 255),
                Procedure = "Colonoscopy",
                NurseMessages = new ObservableCollection<NurseMessage>(
                   new List<NurseMessage>
                    {
                        new NurseMessage
                        {
                            CreatedAt = new DateTime(2014, 04, 01, 02, 30, 0, DateTimeKind.Local).ToBinary(),
                            Message = "Severe headaches",
                            NurseId = nurse
                        }
                        
                    })
            });

            Portal.Instance().AddPatient(new Patient
            {
                Name = "Malthe M. Lind",
                Cpr = "180139-3361",
                Location = "B6", 
                Color = new Rgb(255, 0, 255),
                Procedure = "Gastroscopy",
                NurseMessages = new ObservableCollection<NurseMessage>(
                   new List<NurseMessage>
                    {
                        new NurseMessage
                        {
                            CreatedAt = new DateTime(2014, 04, 01, 02, 30, 0, DateTimeKind.Local).ToBinary(),
                            Message = "Bleeding from nose",
                            NurseId = nurse
                        }
                        
                    })

            });

            Portal.Instance().AddPatient(new Patient
            {
                Name = "Kasper C. Pedersen",
                Cpr = "261231-3167",
                Location = "B6",
                Color = new Rgb(255, 255, 0),
                Procedure = "Sigmoidoscopy",
                NurseMessages = new ObservableCollection<NurseMessage>(
                   new List<NurseMessage>
                    {
                        new NurseMessage
                        {
                            CreatedAt = new DateTime(2014, 04, 01, 02, 30, 0, DateTimeKind.Local).ToBinary(),
                            Message = "Left ear bleeding. Gave some blood thickeners",
                            NurseId = nurse
                        }
                        
                    })

            });

            Portal.Instance().AddPatient(new Patient
            {
                Name = "Jonathan Gregersen",
                Cpr = "210450-1381",
                Location = "B7",
                Color = new Rgb(0, 0, 255),
                Procedure = "Chest photofluorography",
                NurseMessages = new ObservableCollection<NurseMessage>(
                   new List<NurseMessage>
                    {
                        new NurseMessage
                        {
                            CreatedAt = new DateTime(2014, 04, 01, 02, 30, 0, DateTimeKind.Local).ToBinary(),
                            Message = "Mild pain in left leg",
                            NurseId = nurse
                        }
                        
                    })

            });

            Portal.Instance().AddPatient(new Patient
            {
                Name = "Line J. Johansen",
                Cpr = "220188-1490",
                Location = "B7",
                Color = new Rgb(0, 255, 0),
                Procedure = "Respiratory therapy",
                NurseMessages = new ObservableCollection<NurseMessage>(
                   new List<NurseMessage>
                    {
                        new NurseMessage
                        {
                            CreatedAt = new DateTime(2014, 04, 01, 02, 30, 0, DateTimeKind.Local).ToBinary(),
                            Message = "Mild headache. Administered some medicine",
                            NurseId = nurse
                        }
                        
                    })
                
            });

            Portal.Instance().AddPatient(new Patient
            {
                Name = "Amanda Kjær",
                Cpr = "230857-2768",
                Location = "B8",
                Color = new Rgb(255, 0, 255),
                Procedure = "Transcutaneous electrical nerve stimulation"

            });

            Portal.Instance().AddPatient(new Patient
            {
                Name = "Tobias A. Poulsen",
                Cpr = "210232-0789",
                Location = "B8",
                Color = new Rgb(0, 255, 255),
                Procedure = "Craniosacral therapy",
                NurseMessages = new ObservableCollection<NurseMessage>(
                   new List<NurseMessage>
                    {
                        new NurseMessage
                        {
                            CreatedAt = new DateTime(2014, 04, 01, 02, 30, 0, DateTimeKind.Local).ToBinary(),
                            Message = "Headaches.",
                            NurseId = nurse
                        }
                        
                    })
            });

            Portal.Instance().AddPatient(new Patient
            {
                Name = "Silas M. Andresen",
                Cpr = "040167-0505",
                Location = "B9",
                Color = new Rgb(255, 255, 0),
                Procedure = "Cryosurgery",
                NurseMessages = new ObservableCollection<NurseMessage>(
                   new List<NurseMessage>
                    {
                        new NurseMessage
                        {
                            CreatedAt = new DateTime(2014, 04, 01, 02, 30, 0, DateTimeKind.Local).ToBinary(),
                            Message = "Patient is feeling OK",
                            NurseId = nurse
                        }
                        
                    })
            });

            Portal.Instance().AddPatient(new Patient
            {
                Name = "Filippa M. Svendsen",
                Cpr = "211155-3354",
                Location = "B9",
                Color = new Rgb(255, 0, 255),
                Procedure = "Laminectomy",
                NurseMessages = new ObservableCollection<NurseMessage>(
                   new List<NurseMessage>
                    {
                        new NurseMessage
                        {
                            CreatedAt = new DateTime(2014, 04, 01, 02, 30, 0, DateTimeKind.Local).ToBinary(),
                            Message = "The wound was bleeding. Fixed it.",
                            NurseId = nurse
                        }
                        
                    })

            });

            Portal.Instance().AddPatient(new Patient
            {
                Name = "Asger F. Olsen",
                Cpr = "100570-3413",
                Location = "B10",
                Color = new Rgb(0, 0, 255),
                Procedure = "Interventional radiology",
                NurseMessages = new ObservableCollection<NurseMessage>(
                   new List<NurseMessage>
                    {
                        new NurseMessage
                        {
                            CreatedAt = new DateTime(2014, 04, 01, 02, 30, 0, DateTimeKind.Local).ToBinary(),
                            Message = "Need to cut nails",
                            NurseId = nurse
                        }
                        
                    })
            });

            Portal.Instance().AddPatient(new Patient
            {
                Name = "Størm H. Olesen",
                Cpr = "120656-1285",
                Location = "B10",
                Color = new Rgb(255, 255, 0),
                Procedure = "Cancer vaccine",
                NurseMessages = new ObservableCollection<NurseMessage>(
                   new List<NurseMessage>
                    {
                        new NurseMessage
                        {
                            CreatedAt = new DateTime(2014, 04, 01, 02, 30, 0, DateTimeKind.Local).ToBinary(),
                            Message = "Sore throat",
                            NurseId = nurse
                        }
                        
                    })
                

            });

            Portal.Instance().AddPatient(new Patient
            {
                Name = "Melanie Lind",
                Cpr = "160235-2338",
                Location = "B11",
                Color = new Rgb(255, 0, 255),
                Procedure = "None",
                NurseMessages = new ObservableCollection<NurseMessage>(
                   new List<NurseMessage>
                    {
                        new NurseMessage
                        {
                            CreatedAt = new DateTime(2014, 04, 01, 02, 30, 0, DateTimeKind.Local).ToBinary(),
                            Message = "Lost earring. Need help with finding it",
                            NurseId = nurse
                        }
                        
                    })
                
            });

            Portal.Instance().AddPatient(new Patient
            {
                Name = "Mathias M. Vestergård",
                Cpr = "030456-2013",
                Location = "B11",
                Color = new Rgb(0, 255, 0),
                NurseMessages = new ObservableCollection<NurseMessage>(
                   new List<NurseMessage>
                    {
                        new NurseMessage
                        {
                            CreatedAt = new DateTime(2014, 04, 01, 02, 30, 0, DateTimeKind.Local).ToBinary(),
                            Message = "Patient is feeling good now",
                            NurseId = nurse
                        }
                        
                    })
            });

            Portal.Instance().AddPatient(new Patient
            {
                Name = "Sofia J. Nørgaard",
                Cpr = "210875-0246",
                Location = "B12",
                Color = new Rgb(255, 0, 255),
                NurseMessages = new ObservableCollection<NurseMessage>(
                   new List<NurseMessage>
                    {
                        new NurseMessage
                        {
                            CreatedAt = new DateTime(2014, 04, 01, 02, 30, 0, DateTimeKind.Local).ToBinary(),
                            Message = "Patient not concious.",
                            NurseId = nurse
                        }
                        
                    })
            });

            Portal.Instance().AddPatient(new Patient
            {
                Name = "Jasmin Olesen",
                Cpr = "261141-3664",
                Location = "B12",
                Color = new Rgb(0, 255, 0),
                NurseMessages = new ObservableCollection<NurseMessage>(
                   new List<NurseMessage>
                    {
                        new NurseMessage
                        {
                            CreatedAt = new DateTime(2014, 04, 01, 02, 30, 0, DateTimeKind.Local).ToBinary(),
                            Message = "Mild headache. Administered some medicine",
                            NurseId = nurse
                        }
                        
                    })
            });

            Portal.Instance().AddPatient(new Patient
            {
                Name = "Thea Lind",
                Cpr = "030966-2474",
                Location = "B13",
                Color = new Rgb(255, 0, 255),
                Procedure = "Kinesiotherapy",
                NurseMessages = new ObservableCollection<NurseMessage>(
                   new List<NurseMessage>
                    {
                        new NurseMessage
                        {
                            CreatedAt = new DateTime(2014, 04, 01, 02, 30, 0, DateTimeKind.Local).ToBinary(),
                            Message = "Mild headache. Administered some medicine",
                            NurseId = nurse
                        }
                        
                    })
                
            });

            Portal.Instance().AddPatient(new Patient
            {
                Name = "Maria Steffensen",
                Cpr = "120466-1886",
                Location = "B13",
                Color = new Rgb(255, 0, 0),
                NurseMessages = new ObservableCollection<NurseMessage>(
                   new List<NurseMessage>
                    {
                        new NurseMessage
                        {
                            CreatedAt = new DateTime(2014, 04, 01, 02, 30, 0, DateTimeKind.Local).ToBinary(),
                            Message = "Mild headache. Administered some medicine",
                            NurseId = nurse
                        }
                        
                    })
            });

            Portal.Instance().AddPatient(new Patient
            {
                Name = "Anders Hansen",
                Cpr = "110989-4121",
                Location = "B14",
                Color = new Rgb(0, 255, 255)
            });
             
            return "done";

        }
    }
}
