using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using SmartPortal.Web.ViewModels;

namespace SmartPortal.Web.Hubs
{
    public class PatientsManagerHub : Hub
    {
        private readonly PatientsManager _patientsManager;

        public PatientsManagerHub() : this(PatientsManager.Instance) { }

        public PatientsManagerHub(PatientsManager patientsManager)
        {
            _patientsManager = patientsManager;
        }

        public void BrodcastLocationChange(int patientId, string location)
        {
            _patientsManager.BrodcastLocationChange(patientId, location);
        }

        public void BroadcastRecordLoactionChange(string patientId, string location)
        {
            _patientsManager.BroadcastRecordLoactionChange(patientId, location);
        }

        public void BroadcastUserAdded(PatientViewModel patient)
        {
            _patientsManager.BroadcastUserAdded(patient);
        }
    }
}