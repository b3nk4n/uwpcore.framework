using Ninject.Modules;

namespace UWPCore.Framework.IoC
{
    /// <summary>
    /// Injector pseudo class classes in design time.
    /// </summary>
    /// <remarks>
    /// Remember that NULL values are returned. Make sure not to call any function of injected objects in the constructor.
    /// Alternatively, wrap these calles with <see cref="Mvvm.ViewModelBase.IsDesignMode"/>
    /// </remarks>
    public class DesignTimeInjector : IInjector
    {
        public T Get<T>()
        {
            return default(T);
        }

        public void Init(params NinjectModule[] modules)
        {
        }
    }
}
