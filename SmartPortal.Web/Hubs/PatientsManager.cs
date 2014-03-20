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
            try
            {
                Clients.All.updatePatientsLocation(patientId, location);
            }
            catch (Exception)
            {
                
                
            }
            
        }

        public void BroadcastRecordLoactionChange(string patientId, string location)
        {
            try
            {
                Clients.All.updatePatientsRecordLocation(patientId, location);
            }
            catch (Exception)
            {
                
            }
            
        }

        public void BroadcastUserAdded(PatientViewModel patient)
        {
            try
            {
                Clients.All.addPatient(patient);
            }
            catch (Exception)
            {
                
            }
            
        }

        public void BroadcastUserUpdated(PatientViewModel patient)
        {
            try
            {
                Clients.All.updatePatient(patient);
            }
            catch (Exception)
            {
                
            }
            
        }

        public void BroadcastShowPatient(string patientId, string tabletId)
        {
            try
            {
                Clients.All.showPatient(patientId, tabletId);
            }
            catch (Exception)
            {
                
            }
            
        }
    }
}