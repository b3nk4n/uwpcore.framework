using System.Xml.Linq;

namespace UWPCore.Framework.Notifications.Models
{
    public interface IAdaptiveTile
    {
        XElement GetXElement();
    }
}
