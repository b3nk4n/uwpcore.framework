using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace UWPCore.Framework.Tasks
{
    /// <summary>
    /// The interface for the background task service.
    /// </summary>
    public interface IBackgroundTaskService
    {
        /// <summary>
        /// Requests user access for registering background tasks.
        /// </summary>
        /// <returns>Returns True for success, else False.</returns>
        Task<bool> RequestAccessAsync();

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
        /// <param name="cancelTask">True if currently running instances of this background task should be canceled.
        /// If this parameter is false, currently running instances are allowed to finish.
        /// Canceled instances receive a Canceled event with a cancellation reason of Abort.</param>
        void Unregister(string taskName, bool cancelTask = false);

        /// <summary>
        /// Unregisters all background tasks.
        /// </summary>
        /// <param name="cancelTasks">True if currently running instances of this background task should be canceled.
        /// If this parameter is false, currently running instances are allowed to finish.
        /// Canceled instances receive a Canceled event with a cancellation reason of Abort.</param>
        void UnregisterAll(bool cancelTasks = false);
    }
}
