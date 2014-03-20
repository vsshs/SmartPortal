﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using NooSphere.Infrastructure.ActivityBase;
using NooSphere.Infrastructure.Context.Location;
using NooSphere.Model.Users;
using SmartPortal.Model;
using SmartPortal.Web.Hubs;

namespace SmartPortal.Web.Infrastructure
{
    public class Portal
    {
        private ActivitySystem _activitySystem;
        private ActivityService _activityService;
        private ActivityClient _client;


        //public ObservableCollection<Patient> Patients { get; set; }
        //public ObservableCollection<Nurse> Nurses { get; set; }
        private static Portal _instance;

        private Portal()
        {
            //Patients = new ObservableCollection<Patient>();
            //Nurses = new ObservableCollection<Nurse>();
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
                /*
                try
                {
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
                catch (Exception e)
                {
                    
                    
                }
                */
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
            try
            {
                _activitySystem.AddUser(patient);
            }
            catch (Exception e)
            {
                return null;
            }


            
            return patient;
        }

        public Patient UpdatePatient(Patient patient)
        {
            try
            {
                patient.LastUpdated = DateTime.UtcNow;
                _activitySystem.UpdateUser(patient);
                return patient;
            }
            catch (Exception e)
            {
                return null;
            }
            
        }

        public Patient FindPatientById(string id)
        {
            var user = _activitySystem.GetUsers().FirstOrDefault(p => p.Id.CompareTo(id) == 0);
            return user as Patient;
        }

        public Nurse VerifyPin(string pin)
        {
            try
            {
                if (string.IsNullOrEmpty(pin))
                    return null;
                var users = _activitySystem.GetUsers();
                foreach (var user in users)
                {
                    var nurse = user as Nurse;
                    if (nurse != null && !string.IsNullOrEmpty(nurse.Pin))
                        if (nurse.Pin.CompareTo(pin) == 0)
                            return nurse;
                }

                // no nurse with id found...
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
           
        }

        public Nurse AddNurse(Nurse nurse)
        {
            try
            {
                _activitySystem.AddUser(nurse);
                
                return nurse;
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public Nurse UpdateNurse(Nurse nurse)
        {
            try
            {
                _activitySystem.UpdateUser(nurse);
                return nurse;
            }
            catch (Exception e)
            {
                return null;
            }
            
        }

        public void UpdateTagLocation(Tag tag)
        {
            PatientsManager.Instance.BroadcastRecordLoactionChange("20dbf965-d96c-4a34-8586-2108e4a9eca7", tag.Name);
        }

        public ICollection<Nurse> GetNurses()
        {
            var result = new Collection<Nurse>();
            var users = _activitySystem.GetUsers();
            foreach (var nurse in users.OfType<Nurse>())
            {
                result.Add(nurse);
            }
            return result;
        }

        public ICollection<Patient> GetPatients()
        {
            var result = new Collection<Patient>();
            var users = _activitySystem.GetUsers();
            foreach (var patient in users.OfType<Patient>())
            {
                result.Add(patient);
            }
            return result;
        }
    }
}