using System;
using Windows.ApplicationModel.Background;

namespace UWPCore.Framework.Tasks
{
    /// <summary>
    /// The interface for the background task service.
    /// </summary>
    public interface IBackgroundTaskService
    {
        /// <summary>
        /// Registers a background task.
        /// </summary>
        /// <param name="taskName">The task name.</param>
        /// <param name="taskEntryPoint">The entry point class type.</param>
        /// <param name="trigger">The trigger.</param>
        /// <param name="conditions">The optional conditions.</param>
        /// <returns>Returns the background task registration.</returns>
        IBackgroundTaskRegistration Register(string taskName, Type taskEntryPoint, IBackgroundTrigger trigger, params IBackgroundCondition[] conditions);

        /// <summary>
        /// Registers a background task.
        /// </summary>
        /// <param name="taskName">The task name.</param>
        /// <param name="taskEntryPoint">The full qualified task entry point class name.</param>
        /// <param name="trigger">The trigger.</param>
        /// <param name="conditions">The optional conditions.</param>
        /// <returns>Returns the background task registration.</returns>
        IBackgroundTaskRegistration Register(string taskName, string taskEntryPoint, IBackgroundTrigger trigger, params IBackgroundCondition[] conditions);

        /// <summary>
        /// Gets the background task registration.
        /// </summary>
        /// <param name="taskName">The task name.</param>
        /// <returns>Returns the background task registration or NULL.</returns>
        IBackgroundTaskRegistration GetRegistration(string taskName);

        /// <summary>
        /// Checks if a registration already exists.
        /// </summary>
        /// <param name="taskName">The task name</param>
        /// <returns>True when the registration exists, else false.</returns>
        bool RegistrationExists(string taskName);

        /// <summary>
        /// Unregisters a background task.
        /// </summary>
        /// <param name="taskName">The background task name.</param>
        void Unregister(string taskName);

        /// <summary>
        /// Unregisters all background tasks.
        /// </summary>
        void UnregisterAll();
    }
}
