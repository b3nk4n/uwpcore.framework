using Ninject.Modules;
using UWPCore.Framework.IoC;
using Windows.ApplicationModel.Background;

namespace UWPCore.Framework.Tasks
{
    public abstract class BackgroundTaskBase : IBackgroundTask
    {
        /// <summary>
        /// Gets the inversion of control container.
        /// </summary>
        public static IInjector Injector { get; private set; }

        public BackgroundTaskBase(params NinjectModule[] modules)
        {
            Injector = new Injector(modules);
        }

        public abstract void Run(IBackgroundTaskInstance taskInstance);
    }
}
