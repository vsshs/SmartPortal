using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NooSphere.Model.Primitives;

namespace SmartPortal.Model
{
    public class NurseMessage : Base
    {

        private long _createdAt;
        private string _message;
        private string _nurseId;

        public NurseMessage()
        {
            CreatedAt = DateTime.UtcNow.ToBinary();
        }
        
        public long CreatedAt
        {
            get { return _createdAt; }
            set
            {
                _createdAt = value;
                OnPropertyChanged("CreatedAt");
            }
        }

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged("Message");
            }
        }

    }
}
