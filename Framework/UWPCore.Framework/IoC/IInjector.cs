using Ninject.Modules;

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

        void Init(params NinjectModule[] modules);
    }
}
