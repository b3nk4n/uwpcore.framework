using System;
using System.Collections.ObjectModel;
using UWPCore.Framework.Launcher;
using UWPCore.Framework.Models;
using UWPCore.Framework.Mvvm;
using UWPCore.Framework.Share;
using Windows.UI.Xaml.Media;

namespace UWPCore.Framework.ViewModels
{
    /// <summary>
    /// The view model for the default <see cref="Controls.AboutControl"/>.
    /// </summary>
    // TODO: because this is a static view model, it would be sufficient to use standard properties here...
    public sealed class AboutViewModel : ViewModelBase
    {
        /// <summary>
        /// The share content service.
        /// </summary>
        private IShareContentService _shareContentService;

        /// <summary>
        /// Creates a AboutViewModel instance.
        /// </summary>
        public AboutViewModel()
        {
            _shareContentService = new ShareContentService();

            Contributors.CollectionChanged += (s, e) =>
            {
                // updates the contributors visibility
                HasContributors = Contributors.Count > 0;
            };
        }

        /// <summary>
        /// Gets or sets the app title.
        /// </summary>
        public string AppTitle { get { return _appTitle; } set { Set(ref _appTitle, value); } }
        private string _appTitle;

        /// <summary>
        /// Gets or sets the app developer name.
        /// </summary>
        public string AppDeveloper { get { return _appDeveloper; } set { Set(ref _appDeveloper, value); } }
        private string _appDeveloper;

        /// <summary>
        /// Gets or sets the app version.
        /// </summary>
        public string AppVersion { get { return _appVersion; } set { Set(ref _appVersion, value); } }
        private string _appVersion;

        /// <summary>
        /// Gets or sets the app description.
        /// </summary>
        public string AppDescription { get { return _appDescription; } set { Set(ref _appDescription, value); } }
        private string _appDescription;

        /// <summary>
        /// Gets or sets the app icon.
        /// </summary>
        public ImageSource AppIcon { get { return _appIcon; } set { Set(ref _appIcon, value); } }
        private ImageSource _appIcon;

        /// <summary>
        /// Gets or sets the privacy info URI.
        /// </summary>
        public string PrivacyInfoUri { get; set; }

        /// <summary>
        /// Gets or sets the email address where to send the feedback emails.
        /// </summary>
        public string FeedbackToEmail { get; set; }

        /// <summary>
        /// Gets or sets the apps product ID.
        /// </summary>
        public string AppProductId { get; set; }

        /// <summary>
        /// Gets or sets the apps publisher name in Windows Store.
        /// </summary>
        public string PublisherName { get; set; }

        /// <summary>
        /// Gets or sets the share app text.
        /// </summary>
        public string ShareAppUri { get; set; }

        /// <summary>
        /// Gets or sets the privacy info text.
        /// </summary>
        public string ShowPrivacyInfoText { get; set; }

        /// <summary>
        /// Gets or sets the send feedback text.
        /// </summary>
        public string SendFeedbackEmailText { get; set; }

        /// <summary>
        /// Gets or sets the rate and review text.
        /// </summary>
        public string RateAndReviewText { get; set; }

        /// <summary>
        /// Gets or sets the more apps text.
        /// </summary>
        public string MoreAppsText { get; set; }

        /// <summary>
        /// Gets or sets the share app text.
        /// </summary>
        public string ShareAppText { get; set; }

        /// <summary>
        /// Gets or sets the optional share app format-text. It requries 1 placeholder!
        /// </summary>
        public string ShareAppTextFormat { get; set; } = "Check out {0} for Windows 10 Mobile:";

        /// <summary>
        /// Gets or sets the contributors.
        /// </summary>
        public ContributorModelList Contributors { get; private set; } = new ContributorModelList();

        /// <summary>
        /// Gets whether there are contributors to display.
        /// </summary>
        public bool HasContributors { get { return _hasContributors; } set { Set(ref _hasContributors, value); } }
        private bool _hasContributors;

        /// <summary>
        /// Gets the command to show the privacy info.
        /// </summary>
        public DelegateCommand ShowPrivacyInfoCommand { get { return _showPrivacyInfoCommand ?? (_showPrivacyInfoCommand = new DelegateCommand(ExecuteShowPrivacyInfo)); } }
        DelegateCommand _showPrivacyInfoCommand = default(DelegateCommand);
        private async void ExecuteShowPrivacyInfo()
        {
            await SystemLauncher.LaunchUriAsync(new Uri(PrivacyInfoUri));
        }
        
        /// <summary>
        /// Gets the command to send a feedback email.
        /// </summary>
        public DelegateCommand SendFeedbackEmailCommand { get { return _sendFeedbackEmailCommand ?? (_sendFeedbackEmailCommand = new DelegateCommand(ExecuteSendFeedbackEmail)); } }
        DelegateCommand _sendFeedbackEmailCommand = default(DelegateCommand);
        private void ExecuteSendFeedbackEmail()
        {
            IEmailService _emailSerivce = new EmailService();
            _emailSerivce.Show(new[] { FeedbackToEmail },
                string.Format("[{0}] Feedback", AppTitle),
                string.Empty);
        }

        /// <summary>
        /// Gets the command to rate and review the app.
        /// </summary>
        public DelegateCommand RateAndReviewCommand { get { return _rateAndReviewCommand ?? (_rateAndReviewCommand = new DelegateCommand(ExecuteRateAndReview)); } }
        DelegateCommand _rateAndReviewCommand = default(DelegateCommand);
        private async void ExecuteRateAndReview()
        {
            await StoreLauncher.LaunchReviewAsync(AppProductId);
        }

        /// <summary>
        /// Gets the command to show more apps of the publisher.
        /// </summary>
        public DelegateCommand MoreAppsCommand { get { return _moreAppsCommand ?? (_moreAppsCommand = new DelegateCommand(ExecuteMoreApps)); } }
        DelegateCommand _moreAppsCommand = default(DelegateCommand);
        private async void ExecuteMoreApps()
        {
            await StoreLauncher.LaunchSearchAppsByPublisherAsync(PublisherName);
        }

        /// <summary>
        /// Gets the command to share the app.
        /// </summary>
        public DelegateCommand ShareAppCommand { get { return _shareAppCommand ?? (_shareAppCommand = new DelegateCommand(ExecuteShareApp)); } }
        DelegateCommand _shareAppCommand = default(DelegateCommand);
        private void ExecuteShareApp()
        {
            // we do not need a translation here I guess...
            var shareTitle = string.Format(ShareAppTextFormat, AppTitle);
            _shareContentService.ShareWebLink(shareTitle, new Uri(ShareAppUri));
        }
    }

    /// <summary>
    /// Implemented an own class because XAML can not handle generics.
    /// </summary>
    public sealed class ContributorModelList : ObservableCollection<ContributorModel> { }
}
