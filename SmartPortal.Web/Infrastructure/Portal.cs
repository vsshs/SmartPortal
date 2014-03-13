using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using NooSphere.Infrastructure.ActivityBase;
using NooSphere.Model.Users;
using SmartPortal.Model;

namespace SmartPortal.Web.Infrastructure
{
    public class Portal
    {
        private ActivitySystem _activitySystem;
        private ActivityService _activityService;
        private ActivityClient _client;


        public ObservableCollection<Patient> Patients { get; set; }

        private static Portal _instance;

        private Portal()
        {
            Patients = new ObservableCollection<Patient>();
        }

        public static Portal Instance()
        {
            if (_instance == null)
                _instance = new Portal();

            return _instance;
        }

        public ActivitySystem ActivitySystem
        {
            get
            {
                if (_activitySystem == null) 
                    throw new Exception("ActivitySystem has not been created" );
                return _activitySystem;
            }
            set
            {
                _activitySystem = value;
                var users = _activitySystem.GetUsers();
                foreach (var user in users.Where(u => u.GetType() == typeof(Patient)))
                {
                    Patients.Add(user as Patient);
                }
            }
        }

        public ActivityService ActivityService
        {
            get { return _activityService; }
            set { _activityService = value; }
        }

        public ActivityClient ActivityClient
        {
            get { return _client; }
            set { _client = value; }
        }


        public Patient AddPatient(Patient patient)
        {
            _activitySystem.AddUser(patient);
            Patients.Add(patient);

            
            return patient;
        }

        public Patient UpdatePatient(Patient patient)
        {
            patient.LastUpdated = DateTime.UtcNow;
            _activitySystem.UpdateUser(patient);
            return patient;
        }

        public Patient FindPatientById(string id)
        {
            return Patients.FirstOrDefault(p => p.Id.CompareTo(id) == 0);
        }

        public Nurse VerifyPin(string pin)
        {
            return new Nurse
            {
                Name = "DEMO"
            };

            // IMPLEMENT AUTH HERE
        }

        public string Get()
        {
            var users = _activitySystem.GetUsers();

            var res = "";
            foreach (var user in Patients)
            {
                res += "<br> " + user.Id;
            }
            return res;
        }


        
    }
}