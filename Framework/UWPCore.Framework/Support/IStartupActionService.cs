using System;
using Windows.UI.Xaml.Navigation;

namespace UWPCore.Framework.Support
{
    /// <summary>
    /// The execution rule for the action.
    /// </summary>
    public enum ActionExecutionRule
    {
        LessThan,
        LessOrEquals,
        Equals,
        MoreOrEquals,
        MoreThan
    }

    /// <summary>
    /// Logs the number of start ups and performs appropriate actions.
    /// </summary>
    public interface IStartupActionService
    {
        /// <summary>
        /// Registers a new action to a specified number of application startups.
        /// </summary>
        /// <param name="startupCount">The number of startups.</param>
        /// <param name="executionRule">The execution rule.</param>
        /// <param name="action">The action to fire.</param>
        /// <param name="persistent">
        /// Specifies whether the action is persistent or a one time shot. This is needed for actions which are probably
        /// not fired when the user doesn't reach the state where the actions are fired.
        /// </param>
        void Register(int startupCount, ActionExecutionRule executionRule, Action action, bool persistent = false);

        /// <summary>
        /// Checks and fires the appropriate action if there is any
        /// matching action for the current startup count.
        /// <remarks>
        /// Ensures only one action firing per app lifetime, because each fire is equivalent to a startup.
        /// </remarks>
        /// <param name="navigationMode">The navigation mode.</param>
        /// </summary>
        void OnNavigatedTo(NavigationMode navigationMode);

        /// <summary>
        /// Gets the number of startups.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Gets whether the startup actions have already been fired or not.
        /// </summary>
        bool HasFired { get; }
    }
}
