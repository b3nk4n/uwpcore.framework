using System.Collections.Generic;
using System.Threading.Tasks;
using UWPCore.Framework.Mvvm;
using Windows.ApplicationModel;
using Windows.UI.Xaml.Navigation;

namespace UWPCore.Framework.Navigation
{
    /// <summary>
    /// The navigable interface that is based on <see cref="IBindable"/>.
    /// </summary>
    public interface INavigable : IBindable
    {
        /// <summary>
        /// Gets or sets the navigation service.
        /// </summary>
        NavigationService NavigationService { get; set; }

        /// <summary>
        /// Hook method that is called when a page using this view model was navigated to.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="mode">The navigation mode.</param>
        /// <param name="state">The state.</param>
        void OnNavigatedTo(object parameter, NavigationMode mode, IDictionary<string, object> state);

        /// <summary>
        /// Hook method that is called when a page using this view model was navigated from.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <param name="suspending">The suspending indicator.</param>
        Task OnNavigatedFromAsync(IDictionary<string, object> state, bool suspending);

        /// <summary>
        /// Hook method that is called when a page using this view model is navigating from.
        /// </summary>
        /// <remarks>
        /// REMEMBER: This method is not called when we suspend the app using the BACK button.
        /// </remarks>
        /// <param name="args">The navigating event args.</param>
        void OnNavigatingFrom(NavigatingEventArgs args);
    }
}
