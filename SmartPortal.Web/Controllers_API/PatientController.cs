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
        public ServerResponse<int> UpdateColor(UpdateColorModel model)
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

            patient.Color = new Rgb(model.R, model.G, model.B);

            Portal.Instance().UpdatePatient(patient);
            return new ServerResponse<int>();

        }

        [System.Web.Http.HttpPost]
        public ServerResponse<int> UpdateBuzzer(UpdateBuzzerModel model)
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

            patient.Buzzer = true;

            Portal.Instance().UpdatePatient(patient);
            return new ServerResponse<int>();

        }


        [System.Web.Http.HttpGet]
        public ICollection<PatientViewModel> GetPatients()
        {
            var patients = Portal.Instance().Patients.ToList();

            var result = new Collection<PatientViewModel>();

            foreach (var patient in patients)
            {
                result.Add(PatientViewModel.CreateFromPatient(patient));
            }
            return result;
        }
            
            
        [System.Web.Http.HttpGet]
        public string CheckPatient(string tagId="0", long lastUpdated= 0, string deviceAuth="0")
        {
            // validate device auth

            // find tablet by tag id

            if (tagId != "0")
            {
                int decValue = Convert.ToInt32(tagId, 16);
                Portal.Instance().UpdateTagLocation(new Tag
                {
                    Name = decValue.ToString()
                });
            }
            var patient  = Portal.Instance().FindPatientById(Portal.Instance().Patients.First().Id);

            if (patient == null)
                return null;

            var buzz = patient.Buzzer;

            if (patient.Buzzer)
            {
                patient.Buzzer = false;

                Portal.Instance().UpdatePatient(patient);
            }
            return new ArduinoPatient
            {
                LastUpdated = patient.LastUpdated.ToBinary(),
                R = patient.Color.Red,
                G = patient.Color.Green,
                B = patient.Color.Blue,
                Buzzer = buzz
            }.ToString();

            /*

            if (patient.LastUpdated > DateTime.FromBinary(lastUpdated))
                return new ArduinoPatient
                {
                    LastUpdated = patient.LastUpdated.ToBinary(),
                    R = patient.Color.Red,
                    G = patient.Color.Green,
                    B = patient.Color.Blue,
                    Buzzer = patient.Buzzer
                };
            else
            {
                return null;
            }
            */
        }

    }
}
