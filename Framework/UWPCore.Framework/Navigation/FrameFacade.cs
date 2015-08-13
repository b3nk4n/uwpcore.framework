using System;
using System.Collections.Generic;
using System.Linq;
using UWPCore.Framework.Common;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace UWPCore.Framework.Navigation
{
    /// <summary>
    /// A facade pattern implementatoin of a <see cref="Frame"/> to simplify its use.
    /// </summary>
    public class FrameFacade
    {
        /// <summary>
        /// Creates a FrameFacade instance.
        /// </summary>
        /// <param name="frame">The frame to wrap.</param>
        public FrameFacade(Frame frame)
        {
            _frame = frame;
            _navigatedEventHandlers = new List<EventHandler<NavigatedEventArgs>>();

            // setup animations
            var c = new TransitionCollection { };
            var t = new NavigationThemeTransition { };
            var i = new EntranceNavigationTransitionInfo();
            t.DefaultNavigationTransitionInfo = i;
            c.Add(t);
            _frame.ContentTransitions = c;
        }

        /// <summary>
        /// The BACK requested event.
        /// </summary>
        public event EventHandler<HandledEventArgs> BackRequested;

        /// <summary>
        /// Raises the BACK requested event.
        /// </summary>
        /// <param name="args">The handled event args.</param>
        public void RaiseBackRequested(HandledEventArgs args)
        {
            BackRequested?.Invoke(this, args);
        }

        /// <summary>
        /// The FORWARD requested event.
        /// </summary>
        public event EventHandler<HandledEventArgs> ForwardRequested;

        /// <summary>
        /// Raises the FORWARD requested event.
        /// </summary>
        /// <param name="args">The handled event args.</param>
        public void RaiseForwardRequested(HandledEventArgs args)
        {
            ForwardRequested?.Invoke(this, args);
        }

        #region State

        /// <summary>
        /// Gets the frame state key.
        /// </summary>
        /// <returns>The frame state key.</returns>
        private string GetFrameStateKey()
        {
            return string.Format("{0}-PageState", FrameId);
        }

        /// <summary>
        /// The frame state container.
        /// </summary>
        private Windows.Storage.ApplicationDataContainer frameStateContainer;

        /// <summary>
        /// Get or creates the frame state container.
        /// </summary>
        /// <returns></returns>
        private Windows.Storage.ApplicationDataContainer GetFrameStateContainer()
        {
            if (frameStateContainer != null)
                return frameStateContainer;
            var data = Windows.Storage.ApplicationData.Current;
            var key = GetFrameStateKey();
            var container = data.LocalSettings.CreateContainer(key, Windows.Storage.ApplicationDataCreateDisposition.Always);
            return container; // FIXME: assign to frameStateContainer for reuse?
        }

        /// <summary>
        /// Sets the frame state.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void SetFrameState(string key, string value)
        {
            GetFrameStateContainer().Values[key] = value ?? string.Empty;
        }

        /// <summary>
        /// Gets the frame state.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="fallbackValue">The fallback value.</param>
        /// <returns>The frame state value.</returns>
        public string GetFrameState(string key, string fallbackValue)
        {
            if (!GetFrameStateContainer().Values.ContainsKey(key))
                return fallbackValue;
            try { return GetFrameStateContainer().Values[key].ToString(); }
            catch { return fallbackValue; }
        }

        /// <summary>
        /// Clears the frame state.
        /// </summary>
        public void ClearFrameState()
        {
            GetFrameStateContainer().Values.Clear();
            foreach (var container in GetFrameStateContainer().Containers)
            {
                GetFrameStateContainer().DeleteContainer(container.Key);
            }
            pageStateContainers.Clear();
        }

        /// <summary>
        /// Gets the page state key.
        /// </summary>
        /// <param name="type">The page type.</param>
        /// <returns>Returns the page state key.</returns>
        private string GetPageStateKey(Type type)
        {
            return string.Format("{0}", type);
        }

        /// <summary>
        /// The page state containers data structure.
        /// </summary>
        readonly Dictionary<Type, IPropertySet> pageStateContainers = new Dictionary<Type, IPropertySet>();

        /// <summary>
        /// Gets the page state container.
        /// </summary>
        /// <param name="type">The page type.</param>
        /// <returns>Returns the page state container.</returns>
        public IPropertySet GetPageStateContainer(Type type)
        {
            if (pageStateContainers.ContainsKey(type))
                return pageStateContainers[type];
            var key = GetPageStateKey(type);
            var container = GetFrameStateContainer().CreateContainer(key, Windows.Storage.ApplicationDataCreateDisposition.Always);
            pageStateContainers.Add(type, container.Values);
            return container.Values;
        }

        /// <summary>
        /// Clears the page state of a given page type.
        /// </summary>
        /// <param name="type">The page type.</param>
        public void ClearPageState(Type type)
        {
            var key = GetPageStateKey(type);
            if (GetFrameStateContainer().Containers.ContainsKey(key))
                GetFrameStateContainer().DeleteContainer(key);
        }

        #endregion

        #region Frame Facade

        /// <summary>
        /// The frame.
        /// </summary>
        private readonly Frame _frame;

        /// <summary>
        /// Gets or sets the frame ID.
        /// </summary>
        public string FrameId { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool Navigate(Type page, string parameter)
        {
            return _frame.Navigate(page, parameter);
        }

        /// <summary>
        /// Sets the navigatoin state.
        /// </summary>
        /// <param name="state">The navigation state.</param>
        public void SetNavigationState(string state)
        {
            _frame.SetNavigationState(state);
        }

        /// <summary>
        /// Gets the navigation state.
        /// </summary>
        /// <returns>Returns the navigation state.</returns>
        public string GetNavigationState()
        {
            return _frame.GetNavigationState();
        }

        /// <summary>
        /// Gets the BACK stack depth.
        /// </summary>
        public int BackStackDepth
        {
            get
            {
                return _frame.BackStackDepth;
            }
        }

        /// <summary>
        /// Gets whether we can go back in the back history.
        /// </summary>
        public bool CanGoBack
        {
            get
            {
                return _frame.CanGoBack;
            }
        }

        /// <summary>
        /// Goes back in the back stack.
        /// </summary>
        /// <remarks>
        /// Check <see cref="CanGoBack"/> whether a call to this method is valid.
        /// </remarks>
        public void GoBack()
        {
            _frame.GoBack();
        }

        /// <summary>
        /// Refreshes the current loaded page by popping it from the stack 
        /// and renavigate to it again.
        /// </summary>
        public void Refresh()
        {
            var page = CurrentPageType;
            var param = CurrentPageParam;
            _frame.BackStack.Remove(_frame.BackStack.Last());
            Navigate(page, param);
        }

        /// <summary>
        /// Gets whether we can go forward in the forward history.
        /// </summary>
        public bool CanGoForward
        {
            get
            {
                return _frame.CanGoForward;
            }
        }

        /// <summary>
        /// Goes forward in the forward stack.
        /// </summary>
        /// <remarks>
        /// Check <see cref="CanGoForward"/> whether a call to this method is valid.
        /// </remarks>
        public void GoForward()
        {
            _frame.GoForward();
        }

        /// <summary>
        /// Gets the content of the of the frame.
        /// </summary>
        public object Content
        {
            get
            {
                return _frame.Content;
            }
        }

        /// <summary>
        /// Gets or sets the current page type.
        /// </summary>
        public Type CurrentPageType { get; internal set; }

        /// <summary>
        /// Gets or sets the current page parameter.
        /// </summary>
        public string CurrentPageParam { get; internal set; }

        /// <summary>
        /// Gets the dependency property value of the frame.
        /// </summary>
        /// <param name="dp">The dependency property.</param>
        /// <returns>The value.</returns>
        public object GetValue(DependencyProperty dp)
        {
            return _frame.GetValue(dp);
        }

        /// <summary>
        /// Sets the dependency property value of the frame.
        /// </summary>
        /// <param name="dp">The dependency property.</param>
        /// <param name="value">The value.</param>
        public void SetValue(DependencyProperty dp, object value)
        {
            _frame.SetValue(dp, value);
        }

        /// <summary>
        /// Clears the dependency property value of the frame.
        /// </summary>
        /// <param name="dp">The dependency property.</param>
        public void ClearValue(DependencyProperty dp)
        {
            _frame.ClearValue(dp);
        }

        #endregion

        /// <summary>
        /// The collection of navigated event handlers.
        /// </summary>
        readonly List<EventHandler<NavigatedEventArgs>> _navigatedEventHandlers;

        /// <summary>
        /// The navigated event.
        /// </summary>
        public event EventHandler<NavigatedEventArgs> Navigated
        {
            add
            {
                if (_navigatedEventHandlers.Contains(value))
                    return;
                _navigatedEventHandlers.Add(value);
                if (_navigatedEventHandlers.Count == 1)
                    _frame.Navigated += FacadeNavigatedEventHandler;
            }

            remove
            {
                if (!_navigatedEventHandlers.Contains(value))
                    return;
                _navigatedEventHandlers.Remove(value);
                if (_navigatedEventHandlers.Count == 0)
                    _frame.Navigated -= FacadeNavigatedEventHandler;
            }
        }

        /// <summary>
        /// The facade navigated event handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        void FacadeNavigatedEventHandler(object sender, NavigationEventArgs e)
        {
            var args = new NavigatedEventArgs(e);
            foreach (var handler in _navigatedEventHandlers)
            {
                handler(this, args);
            }
            CurrentPageType = e.SourcePageType;
            CurrentPageParam = e.Parameter as String;
        }

        /// <summary>
        /// The collection of navigating event handlers.
        /// </summary>
        readonly List<EventHandler<NavigatingEventArgs>> _navigatingEventHandlers = new List<EventHandler<NavigatingEventArgs>>();

        /// <summary>
        /// the navigating event.
        /// </summary>
        public event EventHandler<NavigatingEventArgs> Navigating
        {
            add
            {
                if (_navigatingEventHandlers.Contains(value))
                    return;
                _navigatingEventHandlers.Add(value);
                if (_navigatingEventHandlers.Count == 1)
                    _frame.Navigating += FacadeNavigatingCancelEventHandler;
            }
            remove
            {
                if (!_navigatingEventHandlers.Contains(value))
                    return;
                _navigatingEventHandlers.Remove(value);
                if (_navigatingEventHandlers.Count == 0)
                    _frame.Navigating -= FacadeNavigatingCancelEventHandler;
            }
        }

        /// <summary>
        /// The facade navigating cancel event handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void FacadeNavigatingCancelEventHandler(object sender, NavigatingCancelEventArgs e)
        {
            var args = new NavigatingEventArgs(e);
            foreach (var handler in _navigatingEventHandlers)
            {
                handler(this, args);
            }
            e.Cancel = args.Cancel;
        }
    }
}
