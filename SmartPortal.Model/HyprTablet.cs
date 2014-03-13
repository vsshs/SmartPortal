using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NooSphere.Model.Device;

namespace SmartPortal.Model
{
    class HyprTablet: Device
    {
        public HyprDevice Device { get; set; }
        public Nurse Nurse { get; set; }

        public HyprTablet()
        {
            DeviceType = DeviceType.Tablet;
            DeviceRole = DeviceRole.Slave;
            DevicePortability = DevicePortability.Mobile;
        }
    }
}
