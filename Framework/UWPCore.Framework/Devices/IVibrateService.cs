using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPCore.Framework.Devices
{
    /// <summary>
    /// The service for the vibration controller.
    /// </summary>
    public interface IVibrateService
    {
        /// <summary>
        /// Invokes the vibration controller for the given time.
        /// </summary>
        /// <param name="seconds">The vibration time in seconds.</param>
        void Vibrate(double seconds);

        /// <summary>
        /// Stops the current vibration.
        /// </summary>
        void Stop();
    }

}
