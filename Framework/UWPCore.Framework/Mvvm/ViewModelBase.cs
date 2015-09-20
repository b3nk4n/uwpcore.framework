using System.Collections.Generic;
using System.Threading.Tasks;
using UWPCore.Framework.Navigation;
using Windows.UI.Xaml.Navigation;

namespace UWPCore.Framework.Mvvm
{
    /// <summary>
    /// The base class of view model in the MVVM pattern.
    /// </summary>
    public abstract class ViewModelBase : BindableBase, INavigable
    {
        /// <summary>
        /// Gets or sets the identifier of the view model.
        /// </summary>
        /// <remarks>
        /// This identifier is used by the navigation service for (re)storing the view model.
        /// </remarks>
        public string Identifier { get; set; }

        /// <summary>
        /// Gets or sets the navigation service.
        /// </summary>
        public NavigationService NavigationService { get; set; }

        /// <summary>
        /// Hook method that is called when a page using this view model was navigated to.
        /// NOTE: This method is called BEFORE OnNavigatedTo() of the next page, but after OnNavigatingTo()!
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="mode">The navigation mode.</param>
        /// <param name="state">The state.</param>
        public virtual void OnNavigatedTo(object parameter, NavigationMode mode, IDictionary<string, object> state) { }

        /// <summary>
        /// Hook method that is called when a page using this view model was navigated from.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <param name="suspending">The suspending indicator.</param>
        public virtual Task OnNavigatedFromAsync(IDictionary<string, object> state, bool suspending) { return Task.FromResult<object>(null); }

        /// <summary>
        /// Hooh method that is called when a page using this view model is navigating from.
        /// </summary>
        /// <param name="args">The navigating event args.</param>
        public virtual void OnNavigatingFrom(NavigatingEventArgs args) { }
    }
}
