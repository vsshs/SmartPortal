using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NooSphere.Model.Primitives;
using NooSphere.Model.Users;

namespace SmartPortal.Model
{
    public class Patient : User
    {
        private string _cprNumber;
        private string _location;
        private string _procedure;
        private string _recordLoaction;
        private string _lastUpdatedBinary;
        private bool _buzzer;
        private int _rfidTag;
        private bool _blink;

        private int _deviceId;

        public ObservableCollection<NurseMessage> NurseMessages; 

        public Patient()
        {
            Name = "Unnamed";
            Cpr = "010101-6666";
            //BaseType = typeof (Patient).Name;
            Location = "Unknown";
            Color = new Rgb(0, 0, 0);
            LastUpdated = DateTime.UtcNow;


            NurseMessages = new ObservableCollection<NurseMessage>();
            //Type = "Patient";
        }


        public string Cpr
        {
            get { return _cprNumber; }
            set
            {
                _cprNumber = value;
                OnPropertyChanged("Cpr");
            }
        }

        public string Location
        {
            get { return _location; }
            set
            {
                _location = value;
                OnPropertyChanged("location");
            }
        }

        public string Procedure
        {
            get { return _procedure; }
            set
            {
                _procedure = value;
                OnPropertyChanged("procedure");
            }
        }

        public string RecordLoaction
        {
            get { return _recordLoaction; }
            set
            {
                _recordLoaction = value;
                OnPropertyChanged("recordLoaction");
            }
        }

        public DateTime LastUpdated {
            get { return DateTime.FromBinary(long.Parse(_lastUpdatedBinary)); }
            set
            {
                _lastUpdatedBinary = value.ToBinary().ToString();
                OnPropertyChanged("lastUpdated");
            }
        }

        public bool Buzzer
        {
            get { return _buzzer; }
            set
            {
                _buzzer = value;
                //OnPropertyChanged("buzzer");

            }
        }

        public bool Blink
        {
            get { return _blink; }
            set
            {
                _blink = value;
                OnPropertyChanged("blink");

            }
        }

        public int RfidTag
        {
            get { return _rfidTag; }
            set
            {
                _rfidTag = value;
                OnPropertyChanged("rfidtag");
            }
        }

        public int DeviceId
        {
            get { return _deviceId; }
            set
            {
                _deviceId = value;
                OnPropertyChanged("deviceid");

            }
        }
        
    }
}
