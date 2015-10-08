using Ninject;
using Ninject.Modules;
using UWPCore.Framework.Accounts;
using UWPCore.Framework.Audio;
using UWPCore.Framework.Data;
using UWPCore.Framework.Devices;
using UWPCore.Framework.Graphics;
using UWPCore.Framework.Networking;
using UWPCore.Framework.Notifications;
using UWPCore.Framework.Share;
using UWPCore.Framework.Speech;
using UWPCore.Framework.Storage;
using UWPCore.Framework.Store;
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

            // Audio
            Bind<IAudioService>().To<AudioService>().InSingletonScope();

            // Data
            Bind<ICompressionService>().To<ZipArchiveService>().InSingletonScope();
            Bind<ISerializationService>().To<DataContractSerializationService>().InSingletonScope();

            // Devices
            Bind<IDeviceInfoService>().To<DeviceInfoService>().InSingletonScope();
            Bind<IDisplayService>().To<DisplayService>().InSingletonScope();
            Bind<IPersonalizationService>().To<PersonalizationService>().InSingletonScope();
            Bind<IStatusBarService>().To<StatusBarService>().InTransientScope();
            Bind<IVibrateService>().To<VibrateService>().InSingletonScope();

            // Graphics
            Bind<IGraphicsService>().To<GraphicsService>().InSingletonScope();

            // Navigation
            // TODO: introduce INavigationService interface?

            // Networking
            Bind<IHttpService>().To<HttpService>();
            Bind<INetworkInfoService>().To<NetworkInfoService>().InSingletonScope();
            Bind<IWebDownloadService>().To<WebDownloadService>();

            // Notifications
            Bind<IAdaptiveTileFactory>().To<AdaptiveTileFactory>().InSingletonScope();
            Bind<ITileFactory>().To<TileFactory>().InSingletonScope();
            Bind<ITileService>().To<TileService>().InSingletonScope();

            Bind<IAdaptiveToastFactory>().To<AdaptiveToastFactory>().InSingletonScope();
            Bind<IToastFactory>().To<ToastFactory>().InSingletonScope();
            Bind<IToastService>().To<ToastService>().InSingletonScope();
            

            // Security
            // TODO: implementation finished?

            // Share
            Bind<IEmailService>().To<EmailService>().InSingletonScope();
            Bind<IShareContentService>().To<ShareContentService>();

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

            // Tasks
            Bind<IBackgroundTaskService>().To<BackgroundTaskService>().InSingletonScope();

            // UI
            Bind<IDialogService>().To<DialogService>().InSingletonScope();
        }
    }
}
