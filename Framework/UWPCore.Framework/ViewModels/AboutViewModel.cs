using Ninject;
using System;
using System.Collections.ObjectModel;
using UWPCore.Framework.Common;
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
            _shareContentService = Injector.Get<IShareContentService>();

            Contributors.CollectionChanged += (s, e) =>
            {
                // updates the contributors visibility
                HasContributors = Contributors.Count > 0;
            };

            ThirdParties.CollectionChanged += (s, e) =>
            {
                // updates the 3rd parties visibility
                HasThirdParties = ThirdParties.Count > 0;
            };
        }

        /// <summary>
        /// Gets or sets the app title.
        /// </summary>
        public string AppTitle { get { return AppInfo.AppTitle; } }

        /// <summary>
        /// Gets or sets the app developer name.
        /// </summary>
        public string AppDeveloper { get { return _appDeveloper; } set { Set(ref _appDeveloper, value); } }
        private string _appDeveloper;

        /// <summary>
        /// Gets or sets the app version.
        /// </summary>
        public string AppVersion { get { return VersionPrefixFormat + AppInfo.Version; } }

        /// <summary>
        /// Gets or sets the app version prefix
        /// </summary>
        public string VersionPrefixFormat { get; set; } = "Version ";

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
        public string ShareAppTextFormat { get; set; } = "Check out {0} for Windows 10";

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
        /// Gets or sets the contributors title.
        /// </summary>
        public string ContributorsTitle { get { return _contributorsTitle; } set { Set(ref _contributorsTitle, value); } }
        private string _contributorsTitle;

        /// <summary>
        /// Gets or sets the 3rd party items.
        /// </summary>
        public ThirdPartyModelList ThirdParties { get; private set; } = new ThirdPartyModelList();

        /// <summary>
        /// Gets whether there are 3rd parties to display.
        /// </summary>
        public bool HasThirdParties { get { return _hasThirdParties; } set { Set(ref _hasThirdParties, value); } }
        private bool _hasThirdParties;

        /// <summary>
        /// Gets or sets the third party title.
        /// </summary>
        public string ThirdPartiesTitle { get { return _thirdPartiesTitle; } set { Set(ref _thirdPartiesTitle, value); } }
        private string _thirdPartiesTitle;

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
            await StoreLauncher.LaunchReviewAsync(AppInfo.ProductId);
        }

        /// <summary>
        /// Gets the command to show more apps of the publisher.
        /// </summary>
        public DelegateCommand MoreAppsCommand { get { return _moreAppsCommand ?? (_moreAppsCommand = new DelegateCommand(ExecuteMoreApps)); } }
        DelegateCommand _moreAppsCommand = default(DelegateCommand);
        private async void ExecuteMoreApps()
        {
            await StoreLauncher.LaunchSearchAppsByPublisherAsync(AppInfo.PublisherName);
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
            _shareContentService.ShareWebLink(shareTitle, AppInfo.StoreLink);
        }
    }

    /// <summary>
    /// Implemented an own class because XAML can not handle generics.
    /// </summary>
    public sealed class ContributorModelList : ObservableCollection<ContributorModel> { }

    /// <summary>
    /// Implemented an own class because XAML can not handle generics.
    /// </summary>
    public sealed class ThirdPartyModelList : ObservableCollection<ThirdPartyModel> { }
}
