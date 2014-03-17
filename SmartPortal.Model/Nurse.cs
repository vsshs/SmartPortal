using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NooSphere.Model.Users;

namespace SmartPortal.Model
{
    public class Nurse : User
    {
        private string _loginPin;

        private string _tabletId;

        public string TabletId
        {
            get
            {
                return _tabletId;
            }

            set
            {
                _tabletId = value;
                OnPropertyChanged("tabletid");
            }
        }

        public string Pin
        {
            get { return _loginPin; }
            set
            {
                _loginPin = value;
                OnPropertyChanged("pin");
            }

        }


    }
}
