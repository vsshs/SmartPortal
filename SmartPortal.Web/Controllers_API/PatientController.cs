using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR.Owin;
using NooSphere.Model.Primitives;
using SmartPortal.Web.Infrastructure;
using SmartPortal.Web.Models;
using SmartPortal.Web.Models_API;

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

        [System.Web.Http.HttpGet]
        public ArduinoPatient CheckPatient(string id, long lastUpdated, string deviceAuth)
        {
            // validate auth

            var patient  = Portal.Instance().FindPatientById(id);

            if (patient == null)
                return null;

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

        }

    }
}
