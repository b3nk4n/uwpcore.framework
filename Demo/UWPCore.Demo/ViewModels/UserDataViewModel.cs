using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using UWPCore.Framework.Mvvm;
using Windows.ApplicationModel.Contacts;
using Windows.UI.Xaml.Navigation;

namespace UWPCore.Demo.ViewModels
{
    /// <summary>
    /// View model of page user data.
    /// </summary>
    public class UserDataViewModel : ViewModelBase
    {
        #region Fields

        private ContactStore store = null;
        public event EventHandler LostConnectionToStore;
        private readonly ObservableCollection<ContactViewModel> contacts;

        #endregion

        #region Properties

        /// <summary>
        /// Get contacts.
        /// </summary>
        public ObservableCollection<ContactViewModel> Contacts
        {
            get { return contacts; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize object.
        /// </summary>
        public UserDataViewModel()
        {
            contacts = new ObservableCollection<ContactViewModel>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Load data if navigated to page.
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="mode"></param>
        /// <param name="state"></param>
        public async override void OnNavigatedTo(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            base.OnNavigatedTo(parameter, mode, state);

            await LoadContactsAsync();
        }

        #region Contacts

        /// <summary>
        /// Load contacts from contact manager.
        /// Add <uap:Capability Name="contacts" /> to manifest
        /// </summary>
        /// <returns></returns>
        public async Task LoadContactsAsync()
        {
            store = await ContactManager.RequestStoreAsync(ContactStoreAccessType.AllContactsReadOnly);
            if (store == null) // check every time because user can deactivate access at any time
            {
                LostConnectionToStore(this, new EventArgs());
                return;
            }

            Debug.WriteLine("Contact store opend for writing successfully.", "information");

            // Load contacts into ListView on the page
            ContactReader reader = store.GetContactReader();
            await DisplayContactsFromReaderAsync(reader);

            // Start tracking changes to the store once the list is loaded into memory
            store.ChangeTracker.Enable();
            store.ContactChanged += Store_ContactChanged;
        }

        /// <summary>
        /// Handle if contact changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void Store_ContactChanged(ContactStore sender, ContactChangedEventArgs args)
        {
            // throw new NotImplementedException();
        }

        /// <summary>
        /// Update displayed contacts.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private async Task DisplayContactsFromReaderAsync(ContactReader reader)
        {
            Contacts.Clear();
            ContactBatch contactBatch = await reader.ReadBatchAsync();
            while (contactBatch.Contacts.Count != 0 &&
                contactBatch.Status == ContactBatchStatus.Success)
            {
                foreach (Contact c in contactBatch.Contacts)
                {
                    Contacts.Add(new ContactViewModel(c));
                }
                contactBatch = await reader.ReadBatchAsync();
            }
        }

        /// <summary>
        /// Search for contacts
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        internal async Task SearchForTextAsync(string text)
        {
            if (!String.IsNullOrWhiteSpace(text))
            {
                ContactQueryOptions option = new ContactQueryOptions(text, ContactQuerySearchFields.All);
                ContactReader reader = store.GetContactReader(option);
                await DisplayContactsFromReaderAsync(reader);
            }
            // A null query string is beeing treated as query for "*"
            else
            {
                ContactReader reader = store.GetContactReader();
                await DisplayContactsFromReaderAsync(reader);
            }
        }

        #endregion

        #endregion
    }
}
