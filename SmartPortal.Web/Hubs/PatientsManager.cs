using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using NooSphere.Infrastructure.Helpers;
using SmartPortal.Model;
using SmartPortal.Web.ViewModels;

namespace SmartPortal.Web.Hubs
{
    public class PatientsManager
    {


        private readonly static Lazy<PatientsManager> _instance =
            new Lazy<PatientsManager>(() => new PatientsManager(GlobalHost
                                                                    .ConnectionManager
                                                                    .GetHubContext<PatientsManagerHub>()
                                                                    .Clients));

        private PatientsManager(IHubConnectionContext clients)
        {
            Clients = clients;
        }

        public static PatientsManager Instance { get { return _instance.Value; } }

        private IHubConnectionContext Clients { get; set; }


        public void BrodcastLocationChange(int patientId, string location)
        {
            Clients.All.updatePatientsLocation(patientId, location);
        }

        public void BroadcastRecordLoactionChange(string patientId, string location)
        {
            Clients.All.updatePatientsRecordLocation(patientId, location);
        }

        public void BroadcastUserAdded(PatientViewModel patient)
        {
            Clients.All.addPatient(patient);
        }

        public void BroadcastUserUpdated(PatientViewModel patient)
        {
            Clients.All.updatePatient(patient);
        }
    }
}