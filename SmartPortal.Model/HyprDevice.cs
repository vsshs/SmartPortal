using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NooSphere.Model.Device;

namespace SmartPortal.Model
{
    class HyprDevice : Device
    {
        public Patient Patient { get; set; }
        public HyprTablet HyprTablet { get; set; }


        public HyprDevice()
        {
            DeviceType = DeviceType.Custom;
            DeviceRole = DeviceRole.Slave;
            DevicePortability = DevicePortability.Mobile;
        }
    }
}
