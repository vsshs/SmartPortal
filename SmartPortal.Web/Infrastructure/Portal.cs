using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using NooSphere.Infrastructure.ActivityBase;
using NooSphere.Infrastructure.Context.Location;
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
        public ObservableCollection<Nurse> Nurses { get; set; }
        private static Portal _instance;

        private Portal()
        {
            Patients = new ObservableCollection<Patient>();
            Nurses = new ObservableCollection<Nurse>();
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

                foreach (var user in users.Where(u => u.GetType() == typeof(Nurse)))
                {
                    Nurses.Add(user as Nurse);
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
            var nurse = Nurses.FirstOrDefault(n => n.Pin != null && n.Pin.CompareTo(pin) == 0);
            return nurse;
        }

        public Nurse AddNurse(Nurse nurse)
        {
            _activitySystem.AddUser(nurse);
            Nurses.Add(nurse);

            return nurse;
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


        public Nurse UpdateNurse(Nurse nurse)
        {
            _activitySystem.UpdateUser(nurse);
            return nurse;
        }

        public void UpdateTagLocation(Tag tag)
        {
            
        }
    }
}