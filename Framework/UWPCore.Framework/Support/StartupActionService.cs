using System;
using System.Collections.Generic;
using UWPCore.Framework.Storage;
using Windows.UI.Xaml.Navigation;

namespace UWPCore.Framework.Support
{
    /// <summary>
    /// Logs the number of start ups and performs appropriate actions.
    /// </summary>
    public class StartupActionService : IStartupActionService
    {
        #region Members

        /// <summary>
        /// The persistent startup counter.
        /// </summary>
        private StoredObjectBase<int> _count = new LocalObject<int>("_startups_count_", 0);

        /// <summary>
        /// The registered actions, which are fired when the startup count is equal.
        /// </summary>
        private Dictionary<int, List<StartupAction>> _actionsEquals = new Dictionary<int, List<StartupAction>>();

        /// <summary>
        /// The registered actions, which are fired when the startup count is less than the specified value.
        /// </summary>
        private Dictionary<int, List<StartupAction>> _actionsLessThan = new Dictionary<int, List<StartupAction>>();

        /// <summary>
        /// The registered actions, which are fired when the startup count is more than the specified value.
        /// </summary>
        private Dictionary<int, List<StartupAction>> _actionsMoreThan = new Dictionary<int, List<StartupAction>>();

        /// <summary>
        /// Indicates whether the actions have already been fired.
        /// </summary>
        private bool _hasFired = false;

        #endregion

        #region Methods

        public void Register(int startupCount, ActionExecutionRule executionRule, Action action, bool persistent = false)
        {
            var startupAction = new StartupAction(action, persistent);

            switch (executionRule)
            {
                case ActionExecutionRule.LessThan:
                    AddAction(_actionsLessThan, startupCount, startupAction);
                    break;
                case ActionExecutionRule.LessOrEquals:
                    AddAction(_actionsLessThan, startupCount, startupAction);
                    AddAction(_actionsEquals, startupCount, startupAction);
                    break;
                case ActionExecutionRule.Equals:
                    AddAction(_actionsEquals, startupCount, startupAction);
                    break;
                case ActionExecutionRule.MoreOrEquals:
                    AddAction(_actionsEquals, startupCount, startupAction);
                    AddAction(_actionsMoreThan, startupCount, startupAction);
                    break;
                case ActionExecutionRule.MoreThan:
                    AddAction(_actionsMoreThan, startupCount, startupAction);
                    break;
            }
        }

        /// <summary>
        /// Adds an action to the given dictionary-list container.
        /// </summary>
        /// <param name="container">The dictionary container.</param>
        /// <param name="startupCount">The startup count key.</param>
        /// <param name="action">The startup action.</param>
        private void AddAction(Dictionary<int, List<StartupAction>> container, int startupCount, StartupAction action)
        {
            if (!container.ContainsKey(startupCount))
                container[startupCount] = new List<StartupAction>();

            container[startupCount].Add(action);
        }

        public void OnNavigatedTo(NavigationMode navigationMode)
        {
            // verify has not already been fired
            if (_hasFired || navigationMode != NavigationMode.New)
                return;

            _count.Value += 1;
            _hasFired = true;

            // equals
            foreach (var key in _actionsEquals.Keys)
            {
                if (key == Count)
                    FireActions(_actionsEquals[key]);
            }
            // less
            foreach (var key in _actionsLessThan.Keys)
            {
                if (key > Count)
                    FireActions(_actionsLessThan[key]);
            }
            // more
            foreach (var key in _actionsMoreThan.Keys)
            {
                if (key < Count)
                    FireActions(_actionsMoreThan[key]);
            }
        }

        /// <summary>
        /// Fires all active actions.
        /// </summary>
        /// <param name="actions">The actions list</param>
        private void FireActions(List<StartupAction> actions)
        {
            if (actions == null)
                return;

            foreach (var action in actions)
            {
                if (!action.IsActive)
                    continue;

                if (!action.IsPersistent)
                    action.IsActive = false;

                // fire action
                action.Action();
            }
        }

        #endregion

        #region Properties

        public int Count
        {
            get
            {
                return _count.Value;
            }
        }

        public bool HasFired
        {
            get
            {
                return _hasFired;
            }
        }

        #endregion
    }
}
