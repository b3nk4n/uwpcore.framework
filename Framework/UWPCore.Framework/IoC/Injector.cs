using Ninject;
using Ninject.Modules;

namespace UWPCore.Framework.IoC
{
    /// <summary>
    /// The base class of a platform specific injector using Ninject for inversion of control.
    /// </summary>
    public class Injector : IInjector
    {
        /// <summary>
        /// The Ninjct kernel.
        /// </summary>
        private IKernel _kernel;

        /// <summary>
        /// Creates or initializes a <see cref="Injector"/> instance.
        /// </summary>
        /// <param name="modules">The modules to load.</param>
        public Injector(params NinjectModule[] modules)
        {
            _kernel = new StandardKernel(modules);
        }

        /// <summary>
        /// Gets an injected instance.
        /// </summary>
        /// <typeparam name="T">The type to inject.</typeparam>
        /// <returns>The injected implementation class based on the module descriptions.</returns>
        public T Get<T>()
        {
            return _kernel.Get<T>();
        }

        /// <summary>
        /// Gets the Ninject kernel in case any additional configuration has to be done.
        /// </summary>
        protected IKernel Kernel
        {
            get { return _kernel; }
        }
    }
}
