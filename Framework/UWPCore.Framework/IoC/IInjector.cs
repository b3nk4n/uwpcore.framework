using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPCore.Framework.IoC
{
    public interface IInjector
    {
        /// <summary>
        /// Gets an injected instance.
        /// </summary>
        /// <typeparam name="T">The type to inject.</typeparam>
        /// <returns>The injected implementation class based on the module descriptions.</returns>
        T Get<T>();
    }
}
