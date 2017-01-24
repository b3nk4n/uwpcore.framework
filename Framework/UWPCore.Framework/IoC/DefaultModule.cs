using Ninject;
using Ninject.Modules;
using UWPCore.Framework.Accounts;
using UWPCore.Framework.Audio;
using UWPCore.Framework.Data;
using UWPCore.Framework.Devices;
using UWPCore.Framework.Graphics;
using UWPCore.Framework.Input;
using UWPCore.Framework.Networking;
using UWPCore.Framework.Notifications;
using UWPCore.Framework.Security.Cryptography;
using UWPCore.Framework.Share;
using UWPCore.Framework.Speech;
using UWPCore.Framework.Storage;
using UWPCore.Framework.Store;
using UWPCore.Framework.Support;
using UWPCore.Framework.Tasks;
using UWPCore.Framework.UI;

namespace UWPCore.Framework.IoC
{
    /// <summary>
    /// The default inversion of control module.
    /// </summary>
    public class DefaultModule : NinjectModule
    {
        public override void Load()
        {
            // Accounts
            Bind<IUserInfoService>().To<UserInfoService>().InSingletonScope();
            Bind<IOnlineIdService>().To<OnlineIdService>().InSingletonScope();

            // Audio
            Bind<IAudioService>().To<AudioService>().InSingletonScope();

            // Data
            Bind<ICompressionService>().To<ZipArchiveService>().InSingletonScope();
            Bind<ISerializationService>().To<DataContractSerializationService>().InSingletonScope();

            // Devices
            Bind<IDeviceInfoService>().To<DeviceInfoService>().InSingletonScope();
            Bind<IDisplayService>().To<DisplayService>().InSingletonScope();
            Bind<IPersonalizationService>().To<PersonalizationService>().InSingletonScope();
            Bind<IStatusBarService>().To<StatusBarService>().InSingletonScope();
            Bind<IVibrateService>().To<VibrateService>().InSingletonScope();

            // Graphics
            Bind<IGraphicsService>().To<GraphicsService>().InSingletonScope();

            // Input
            Bind<IKeyboardService>().To<KeyboardService>().InSingletonScope();

            // Networking
            Bind<IHttpService>().To<HttpService>(); // no singleton, because each has its own HTTP headers and session
            Bind<INetworkInfoService>().To<NetworkInfoService>().InSingletonScope();
            Bind<IWebDownloadService>().To<WebDownloadService>(); // no singleton, because each has its own HTTP headers and session

            // Notifications
            Bind<IAdaptiveTileFactory>().To<AdaptiveTileFactory>().InSingletonScope();
            Bind<ITileFactory>().To<TileFactory>().InSingletonScope();
            Bind<ITileService>().To<TileService>().InSingletonScope();

            Bind<IAdaptiveToastFactory>().To<AdaptiveToastFactory>().InSingletonScope();
            Bind<IToastFactory>().To<ToastFactory>().InSingletonScope();
            Bind<IToastService>().To<ToastService>().InSingletonScope();

            Bind<IBadgeFactory>().To<BadgeFactory>().InSingletonScope();
            Bind<IBadgeService>().To<BadgeService>().InSingletonScope();


            // Security
            Bind<IDataProtectionProviderService>().To<DataProtectionProviderService>().InSingletonScope();
            Bind<ISymmetricAlgorithmService>().To<Aes256Service>().InSingletonScope();

            // Share
            Bind<IEmailService>().To<EmailService>().InSingletonScope();
            Bind<IShareContentService>().To<ShareContentService>(); // no singleton, because temporary fields are used 

            // Speech
            Bind<ISpeechService>().To<SpeechService>().InSingletonScope();

            // Storage
            Bind<LocalStorageService>().ToSelf().InSingletonScope();
            Bind<IStorageService>().ToMethod(c => c.Kernel.Get<LocalStorageService>());
            Bind<ILocalStorageService>().ToMethod(c => c.Kernel.Get<LocalStorageService>());
            Bind<IRoamingStorageService>().To<RoamingStorageService>().InSingletonScope();
            Bind<ISharedLocalStorageService>().To<SharedLocalStorageService>().InSingletonScope();
            Bind<ITemporaryStorageService>().To<TemporaryStorageService>().InSingletonScope();

            // Store
            Bind<ILicenseService>().To<LicenseService>().InSingletonScope();

            // Support
            Bind<IStartupActionService>().To<StartupActionService>().InSingletonScope();

            // Tasks
            Bind<IBackgroundTaskService>().To<BackgroundTaskService>().InSingletonScope();

            // UI
            Bind<IDialogService>().To<DialogService>().InSingletonScope();
        }
    }
}
