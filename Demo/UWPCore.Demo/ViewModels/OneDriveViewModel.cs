using Microsoft.OneDrive.Sdk;
using Microsoft.OneDrive.Sdk.WinStore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using UWPCore.Framework.Mvvm;
using UWPCore.Framework.Navigation;
using UWPCore.Framework.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace UWPCore.Demo.ViewModels
{
    class OneDriveViewModel : ViewModelBase
    {
        //https://dev.onedrive.com/misc/path-encoding.htm
        private string[] oneDriveReservedCharacters = new string[] { "/", @"\", "<", ">", "?", ":", "|" };

        private Visibility signUpPaneVisibility;
        private string signInStatusText;
        private bool signInIsIndeterminate;

        private string uploadName;

        public string UploadName
        {
            get { return uploadName; }
            set
            {
                if (Set(ref uploadName, value))
                {
                    UploadCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private string uploadPath;

        public string UploadPath
        {
            get { return uploadPath; }
            set { Set(ref uploadPath, value); }
        }



        public Visibility SignUpPaneVisibility
        {
            get { return signUpPaneVisibility; }
            set { Set(ref signUpPaneVisibility, value); }
        }

        public string SignInStatusText
        {
            get { return signInStatusText; }
            set { Set(ref signInStatusText, value); }
        }

        public bool SignInIsIndeterminate
        {
            get { return signInIsIndeterminate; }
            set { Set(ref signInIsIndeterminate, value); }
        }

        public DelegateCommand SignUpRetryCommand { get; private set; }
        public DelegateCommand UploadCommand { get; private set; }



        #region Fields

        OneDriveStorageService service;

        private bool isAuthenticated;

        #endregion

        #region Properties

        public bool IsAuthenticated
        {
            get { return isAuthenticated; }
            set { Set(ref isAuthenticated, value); }
        }

        private string uploadValidationText;

        public string UploadValidationText
        {
            get { return uploadValidationText; }
            set { Set(ref uploadValidationText, value); }
        }

        #endregion

        #region Constructors

        public OneDriveViewModel()
        {
            service = new OneDriveStorageService();

            uploadName = string.Empty;

            SignUpRetryCommand = new DelegateCommand(async () =>
            {
                await AuthenticateAsync();
            },
            () =>
            {
                if (SignInIsIndeterminate)
                    return false;

                if (IsAuthenticated)
                    return false;
                return true;
            });

            UploadCommand = new DelegateCommand(async () =>
            {
                await CreateAndUploadBackup();
            },
            () =>
            {
                // Maybe add length between 3 and 20 
                if (UploadName.Length == 0)
                {
                    UploadValidationText = "Name backup.";
                    return false;
                }

                if (oneDriveReservedCharacters.Count(a => UploadName.Contains(a)) > 0)
                {
                    UploadValidationText = @"Contains reserved char /\<>:? or |.";
                    return false;
                }

                UploadValidationText = string.Empty;
                return true;
            });

            IsAuthenticated = false;
            SignUpPaneVisibility = Visibility.Visible;

            SignUpRetryCommand.RaiseCanExecuteChanged();
            UploadCommand.RaiseCanExecuteChanged();
        }

        private Task CreateAndUploadBackup()
        {
            throw new NotImplementedException();
        }

        #endregion

        public override void OnNavigatedTo(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            base.OnNavigatedTo(parameter, mode, state);

            Action task = async () =>
            {
                await AuthenticateAsync();
            };
            task();
        }

        private async Task AuthenticateAsync()
        {
            SignInIsIndeterminate = true;
            SignInStatusText = "Connecting to OneDrive ...";
            SignUpRetryCommand.RaiseCanExecuteChanged();

            await Task.Delay(1000);

            IsAuthenticated = await service.AuthenticateAsync();
            SignInIsIndeterminate = false;

            if (IsAuthenticated)
            {
                SignUpPaneVisibility = Visibility.Collapsed;
            }
            else
            {
                SignInStatusText = "Could not connect to OneDrive.";
                SignUpRetryCommand.RaiseCanExecuteChanged();
            }
        }

        internal async Task Test()
        {
            MemoryStream ms = new MemoryStream();
            StreamWriter sw = new StreamWriter(ms);
            sw.Write("Helloasdasd kasjd kjaldsk jasdl k");
            sw.Write("|||Helloasdasd kasjd kjaldsk jasdl k");
            sw.Flush();

            var item = await service.UploadFileByPathAsync("Game/Test23/aaaa/test2.txt", ms);

            var stream = await service.DownloadFileByPathAsync("Game/Test23/aaaa/test2.txt");
            StreamReader sr = new StreamReader(stream);
            var str = sr.ReadToEnd();
        }
    }
}
