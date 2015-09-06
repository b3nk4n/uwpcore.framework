using UWPCore.Framework.Logging;
using Windows.System.Display;

namespace UWPCore.Framework.Devices
{
    /// <summary>
    /// Class for services that access the display functionality.
    /// </summary>
    public class DisplayService : IDisplayService
    {
        /// <summary>
        /// The display request instance.
        /// </summary>
        private DisplayRequest _displayRequest;

        /// <summary>
        /// The request reference counter.
        /// </summary>
        private long _displayRequestRefCount = 0;

        public bool RequestActive()
        {
            if (_displayRequest == null)
            {
                // lazily creates an instance of the displayRequest object 
                _displayRequest = new DisplayRequest();
            }

            if (_displayRequestRefCount == int.MaxValue)
            {
                // error
                Logger.WriteLine("Error: Exceeded maximum display request active instant count ({0})", _displayRequestRefCount);
                return false;
            }

            // this call activates a display-required request. If successful, the screen
            // is guaranteed not to turn off automatically due to user inactivity.
            _displayRequest.RequestActive();
            _displayRequestRefCount++;
            Logger.WriteLine("Display request activated ({0})", _displayRequestRefCount);
            return true;
        }

        public bool RequestRelease()
        {
            if (_displayRequest == null || _displayRequestRefCount == 0)
            {
                // error
                Logger.WriteLine("No existing active display request instance to be released");
                return false;
            }

            // this call de-activates the display-required request. If successful, the screen 
            // might be turned off automatically due to a user inactivity, depending on the 
            // power policy settings of the system. The requestRelease method throws an exception  
            // if it is called before a successful requestActive call on this object 
            _displayRequest.RequestRelease();
            _displayRequestRefCount--;
            Logger.WriteLine("Display request released ({0})", _displayRequestRefCount);
            return true;
        }

        public bool RequestReleaseAll()
        {
            while (_displayRequestRefCount > 0)
            {
                // stop when an error was detected
                if (!RequestRelease())
                    return false;
            }

            return true;
        }
    }
}
