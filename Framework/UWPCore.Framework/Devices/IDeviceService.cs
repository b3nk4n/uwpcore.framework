using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPCore.Framework.Devices
{
    /// <summary>
    /// Interface for device dependent services.
    /// </summary>
    public interface IDeviceService
    {

        /// <summary>
        /// Gets whether the service is supported on the running device.
        /// </summary>
        bool IsSupported { get; }
    }
}
