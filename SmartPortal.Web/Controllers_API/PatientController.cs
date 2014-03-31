using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR.Owin;
using NooSphere.Infrastructure.Context.Location;
using NooSphere.Model.Primitives;
using SmartPortal.Model;
using SmartPortal.Web.Hubs;
using SmartPortal.Web.Infrastructure;
using SmartPortal.Web.Models;
using SmartPortal.Web.Models_API;
using SmartPortal.Web.ViewModels;

namespace SmartPortal.Web.Controllers_API
{
    public class PatientController : ApiController
    {


        [System.Web.Http.HttpPost]
        public ServerResponse<int> UpdateProcedure(UpdateProcedureModel model)
        {
            if (model == null)
                throw new Exception("Could not deserialize model!");

            var nurse = Portal.Instance().VerifyPin(model.Pin);


            var patient = Portal.Instance().FindPatientById(model.PatientId);

            if (patient == null)
                return new ServerResponse<int>
                {
                    Success = false
                };

            patient.Procedure = model.Procedure;
            patient.NurseMessages.Add(new NurseMessage
            {
                Message = string.Format("Nurse {0} changed procedure {1}", nurse.Name, model.Procedure)
            });
            Portal.Instance().UpdatePatient(patient);
            return new ServerResponse<int>();

        }

        [System.Web.Http.HttpPost]
        public ServerResponse<int> UpdateRoom(UpdateRoomModel model)
        {
            if (model == null)
                throw new Exception("Could not deserialize model!");

            var nurse = Portal.Instance().VerifyPin(model.Pin);


            var patient = Portal.Instance().FindPatientById(model.PatientId);

            if (patient == null)
                return new ServerResponse<int>
                {
                    Success = false
                };

            patient.Location = model.Room;
            Portal.Instance().UpdatePatient(patient);
            return new ServerResponse<int>();

        }
        [System.Web.Http.HttpPost]
        public object UpdateColor(UpdateColorModel model)
        {
            if (model == null)
                throw new Exception("Could not deserialize model!");

            //var nurse = Portal.Instance().VerifyPin(model.Pin);


            var patient = Portal.Instance().FindPatientById(model.PatientId);

            if (patient == null)
                return new { Success = false };

            patient.Color = new Rgb(model.R, model.G, model.B);

            Portal.Instance().UpdatePatient(patient);
            return new { Success = true };

        }

        [System.Web.Http.HttpPost]
        public ServerResponse<int> UpdateBuzzer(UpdateBuzzerModel model)
        {
            if (model == null)
                throw new Exception("Could not deserialize model!");

            //var nurse = Portal.Instance().VerifyPin(model.Pin);


            var patient = Portal.Instance().FindPatientById(model.PatientId);

            if (patient == null)
                return new ServerResponse<int>
                {
                    Success = false
                };

            patient.Buzzer = true;

            Portal.Instance().UpdatePatient(patient);

            var nurse = Portal.Instance().FindNurseById(model.NurseId);
            var name = nurse != null ? nurse.Name : "unknown";
            Portal.Instance()
                .Log(model.NurseId, "Buzzer was used on patient name = " + patient.Name + " by " + name);
            return new ServerResponse<int>();

        }


        [System.Web.Http.HttpPost]
        public object UpdateBlinking(UpdateBuzzerModel model)
        {
            var patient = Portal.Instance().FindPatientById(model.PatientId);

            patient.Blink = true;

            Portal.Instance().UpdatePatient(patient);
            var nurse = Portal.Instance().FindNurseById(model.NurseId);
            var name = nurse != null ? nurse.Name : "unknown";
            Portal.Instance()
                .Log(model.NurseId, "blinking was used on patient name = " + patient.Name + " by " + name);

            return new { Success = true };
        }
        [System.Web.Http.HttpPost]
        public ServerResponse<int> UpdateMessage(UpdateMessageModel model)
        {
            if (model == null)
                throw new Exception("Could not deserialize model!");

            var nurse = Portal.Instance().VerifyPin(model.Pin);


            var patient = Portal.Instance().FindPatientById(model.PatientId);


            if (nurse == null || patient == null)
            {
                return new ServerResponse<int>
                {
                    Success = false
                };

            }


            patient.NurseMessages.Add(new NurseMessage
            {
                Message = model.Message,
                NurseId = nurse.Id
            });
            patient.Blink = true;
            Portal.Instance().UpdatePatient(patient);

            return new ServerResponse<int>();
        }

        [System.Web.Http.HttpGet]
        public ICollection<PatientViewModel> GetPatients()
        {
            var patients = Portal.Instance().GetPatients().ToList();

            var result = new Collection<PatientViewModel>();

            foreach (var patient in patients)
            {
                result.Add(PatientViewModel.CreateFromPatient(patient));
            }
            return result;
        }



        // for arduino
        [System.Web.Http.HttpGet]
        public string CheckPatient(string tagId = "0", long lastUpdated = 0, int deviceAuth = 0)
        {
            try
            {
                bool changed = false;
                // validate device auth


                Patient patient = null;

                if (deviceAuth != 0)
                {
                    patient = Portal.Instance().FindPatientByDevice(deviceAuth);

                }
                if (patient == null)
                    return null;

                var buzz = patient.Buzzer;

                if (patient.Buzzer)
                {
                    patient.Buzzer = false;

                    changed = true;
                }

                // find tablet by tag id
                int decValue = 0;
                if (tagId != "0")
                {
                    decValue = Convert.ToInt32(tagId, 16);
                    //Portal.Instance().UpdateTagLocation(new Tag
                    //{
                    //    Name = decValue.ToString()
                    //});

                    if (patient.Blink)
                    {
                        patient.Blink = false;
                        changed = true;
                    }



                }

                if (changed)
                    Portal.Instance().UpdatePatient(patient);


                if (tagId != "0")
                    PatientsManager.Instance.BroadcastShowPatient(patient.Id, decValue.ToString());

                return new ArduinoPatient
                {
                    LastUpdated = patient.LastUpdated.ToBinary(),
                    R = patient.Color.Red,
                    G = patient.Color.Green,
                    B = patient.Color.Blue,
                    Buzzer = buzz,
                    Blink = patient.Blink
                }.ToString();
            }
            catch (Exception)
            {
                return "?";
            }

        }

    }
}
