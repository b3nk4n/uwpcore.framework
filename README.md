<div align="center">
  <img src="https://github.com/bsautermeister/uwpcore.framework/blob/master/Assets/uwpcore.png" alt="UWPCore Framework"><br>
</div>
-----------------


# UWPCore Framework for Windows 10 (Mobile)

The UWPCore Framework is a development acceleration library for the Universal Windows Platform. It is a collection of best practices and reusable services to simplify the development for Windows 10 apps.

### Is the framework inspired by another one?

The framworks navigation system and application shell is maily inspired by [Template 10](https://github.com/Windows-XAML/Template10) by Jerry Nixon. The application shell has been further improved with swipe gestured inspired by [Justin Xin Liu](https://github.com/JustinXinLiu/SwipeableSplitView). From time to time, the UI of the shell has been updated to be more similar to Microsofts default apps, such as News, Weather, Sports or Groove-Music.

### How to use this framework?

1. Check out the repository
2. Launch the `UWPCore.Framework.sln` solution in Visual Studio 
3. Optional: Export a project template based on the `UWPCore.Template` project to get started even faster
  1. Select *File > Export template...*
  2. Select *Project template*, as well as the *Template\UWPCore.Template* project
  3. Give it a handy name, and use *auto-import to Visual Studio*
4. Create a new Universal Windows project in Visual Studio, and use the previously exported template
5. Add the framework project as an *existing project*, to be able to debug within the code of the framework. Otherwise it is sufficient to reference the DLLs of the framework
6. Update the project reference to `UWPCore.Framework`
7. **Important:** Clean and recompile the whole solution
8. You are now ready to get started!
  1. Don't forget to modify the manifeest, as well as the String-resources accoding to your personal needs

The generated app includes a home page, a settings page including the functionality to switch the app theme, as well as an about page. Furthermore, it uses app localization (English and Germany), and the MVVM pattern.

### Is there anything I should know before I can hack down my app?

It might be helpful to read the folowing sections first, before you start with coding...

#### App

The App class is the root of your app. It inherits from `UWPCore.Framework.Common.UniversalApp`, which extends and simplifies the Application base class of a standard UWP project. It provides for example dirct access to the `NavigationService` to navigate to a page. Its constructor requires some important information that has to be defined:

```csharp
public App()
  : base(typeof(MainPage), AppBackButtonBehaviour.KeepAlive, true, new DefaultModule())
{
  InitializeComponent();
}
```

The parameters of the UniversalApp constructor define the starting page, the behavior of the BACK button on the root page of the stack, whether to use the AppShell (hamburger-menu) or not, as well as multiple module definition instances for used DI-framework [Ninject](http://www.ninject.org/). 

Within the `OnInitialize(IActivatedEventArgs)` method, we can initialize the app, such as defining the theme colors:

```csharp
public async override Task OnInitializeAsync(IActivatedEventArgs args)
{
  await base.OnInitializeAsync(args);     
  // setup theme colors (mainly for title bar)
  ColorPropertiesDark = new AppColorProperties(AppConstants.COLOR_ACCENT, Colors.White, Colors.Black, Colors.White, Color.FromArgb(255, 31, 31, 31), null, null);
  ColorPropertiesLight = new AppColorProperties(AppConstants.COLOR_ACCENT, Colors.Black, Colors.White, Colors.Black, Color.FromArgb(255, 230, 230, 230), null, null);
}
```

Within the `OnStartAsync(StartKind, IActivatedEventArgs)` method, the app gets started/activated, and we can select the page we would like to navigate to. This method is lauched even when the app is launched using Cortana, Live Tiles or Toast notifications. Check the start kind and the event arguments to handle the app's startup properly:

```csharp
public override Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
{
  var pageType = DefaultPage;
  object parameter = null;

  // start the user experience
  NavigationService.Navigate(pageType, parameter);

  return Task.FromResult<object>(null);
}
```

In case we are using the AppShell (by using `true` for the thrid parameter in the UniversalApp constructor), it is required to override the `CreateNavigationMenuItems()` and `CreateBottomDockedNavigationMenuItems()` methods, which populate the navigation menu.

In the `App.xaml` file, make sure that you specify the proper theme colors here as well. This is required that even the default controls of the Windows Universal Platform use these theme colors, such as selected items of a ListView control:

```xml
<ResourceDictionary.ThemeDictionaries>
    <ResourceDictionary x:Key="Dark">
        <Color x:Key="SystemAccentColor">#0063B1</Color>
    </ResourceDictionary>
    <ResourceDictionary x:Key="Light">
        <Color x:Key="SystemAccentColor">#0063B1</Color>
    </ResourceDictionary>
</ResourceDictionary.ThemeDictionaries>
```

#### Pages

Every page is placed within the *Views* folder. Since it is recommended to use the MVVM pattern, the code-behind of each page only assignes the proper view model to the data context. All the magic of a page lives in the view model implementation, located in the *ViewModels* folder.

#### ViewModels

Each view model inherits from `ÙWPCore.Framework.Mvvm.ViewModelBase` and offers public properties where the view is able to bind to. For the main logic, we recommend to encapsulate this functionality in *Services*, to be able to share these across multiple view models. These service implementations can then be injected using *Ninject*.

```csharp
private IDialogService _dialogService;

public MainViewModel()
{
  _dialogService = Injector.Get<IDialogService>();
}
```

Feel free in case you prefer to inject the view model implementations as well, to improve the testability of the view model. In this case, you have to create your own DI-module definition and add this to the last constructor parameter of the `UniversalApp` base class in `App.xaml.cs`.

#### Services

In our opinion, the MVVM pattern is lacking the recommendation of using services to share functionality accross view models and pages. Some people prefer the terminology MVVMS pattern. Each service is just a simple class that offers some functionality. A service can be composed of other services, which can be simply injected using the `[Inject]` attribute of Ninject:

```csharp
public class TilePinService : ITilePinService
{
  private ITileService _tileService;
  private IDeviceInfoService _deviceInfoService;

  [Inject]
  public TilePinService(ITileService tileService, IDeviceInfoService deviceInfoService)
  {
  _tileService = tileService;
  _deviceInfoService = deviceInfoService;
  }
  
  // ...
}
```

To be able to inject our new service, we have to include it in our custom module definition class:

```csharp
public class AppModule : NinjectModule
{
  public override void Load()
  {
    // services
    Bind<ITilePinService>().To<TilePinService>().InSingletonScope();
  }
}
```

Make sure you add this custom module definition to the applications constructor:

```csharp
public App()
  : base(typeof(MainPage), AppBackButtonBehaviour.KeepAlive, true, new DefaultModule(), new AppModule())
{
  // ...
}
```
