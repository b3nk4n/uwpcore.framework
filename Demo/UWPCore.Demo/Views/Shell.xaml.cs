using System;
using System.Collections.Generic;
using UWPCore.Framework.Common;
using UWPCore.Framework.Mvvm;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace UWPCore.Demo.Views
{
    [Deprecated("Use AppShell instead! Temporary kept for reference of navigation internals.", DeprecationType.Deprecate, 1)]
    public sealed partial class Shell : Page
    {
        public Shell(Frame frame)
        {
            InitializeComponent();
            ShellSplitView.Content = frame;
            var update = new Action(() =>
            {
                // update radiobuttons after frame navigates
                var type = frame.CurrentSourcePageType;
                foreach (var radioButton in AllRadioButtons(this))
                {
                    var target = radioButton.CommandParameter as NavType;
                    if (target == null)
                        continue;
                    radioButton.IsChecked = target.Type.Equals(type);
                }
                ShellSplitView.IsPaneOpen = false;
                BackCommand.RaiseCanExecuteChanged();
            });
            frame.Navigated += (s, e) => update();
            Loaded += (s, e) => update();
            DataContext = this;
        }

        // back
        DelegateCommand _backCommand;
        public DelegateCommand BackCommand { get { return _backCommand ?? (_backCommand = new DelegateCommand(ExecuteBack, CanBack)); } }
        private bool CanBack()
        {
            var nav = (Application.Current as UniversalApp).NavigationService;
            return nav.CanGoBack;
        }
        private void ExecuteBack()
        {
            var nav = (Application.Current as UniversalApp).NavigationService;
            nav.GoBack();
        }

        // menu
        DelegateCommand _menuCommand;
        public DelegateCommand MenuCommand { get { return _menuCommand ?? (_menuCommand = new DelegateCommand(ExecuteMenu)); } }
        private void ExecuteMenu()
        {
            this.ShellSplitView.IsPaneOpen = !this.ShellSplitView.IsPaneOpen;
        }

        // nav
        DelegateCommand<NavType> _navCommand;
        public DelegateCommand<NavType> NavCommand { get { return _navCommand ?? (_navCommand = new DelegateCommand<NavType>(ExecuteNav)); } }
        private void ExecuteNav(NavType navType)
        {
            var type = navType.Type;
            var nav = (Application.Current as UniversalApp).NavigationService;

            // when we nav home, clear history
            if (type.Equals((Application.Current as UniversalApp).DefaultPage))
                nav.ClearHistory();

            // navigate only to new pages
            if (nav.CurrentPageType != null && nav.CurrentPageType != type)
                nav.Navigate(type, navType.Parameter);
        }

        // utility
        public List<RadioButton> AllRadioButtons(DependencyObject parent)
        {
            var list = new List<RadioButton>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is RadioButton)
                {
                    list.Add(child as RadioButton);
                    continue;
                }
                list.AddRange(AllRadioButtons(child));
            }
            return list;
        }

        // prevent check
        private void DontCheck(object s, RoutedEventArgs e)
        {
            // don't let the radiobutton check
            (s as RadioButton).IsChecked = false;
        }
    }

    public class NavType
    {
        public Type Type { get; set; }
        public string Parameter { get; set; }
    }
}
