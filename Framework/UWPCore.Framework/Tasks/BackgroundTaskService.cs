using System;
using System.Threading.Tasks;
using UWPCore.Framework.Logging;
using Windows.ApplicationModel.Background;

namespace UWPCore.Framework.Tasks
{
    /// <summary>
    /// The background task service class.
    /// </summary>
    public class BackgroundTaskService : IBackgroundTaskService
    {
        public async Task<bool> RequestAccessAsync()
        {
            BackgroundAccessStatus status = await BackgroundExecutionManager.RequestAccessAsync();

            return (status == BackgroundAccessStatus.AllowedSubjectToSystemPolicy ||
                status == BackgroundAccessStatus.AlwaysAllowed ||
#pragma warning disable CS0618 // Typ oder Element ist veraltet
                status == BackgroundAccessStatus.AllowedMayUseActiveRealTimeConnectivity ||
#pragma warning restore CS0618 // Typ oder Element ist veraltet
#pragma warning disable CS0618 // Typ oder Element ist veraltet
                status == BackgroundAccessStatus.AllowedWithAlwaysOnRealTimeConnectivity);
#pragma warning restore CS0618 // Typ oder Element ist veraltet
        }

        public IBackgroundTaskRegistration Register(string taskName, Type taskEntryPoint, IBackgroundTrigger trigger, params IBackgroundCondition[] conditions)
        {
            return Register(taskName, taskEntryPoint.FullName, trigger, conditions);
        }

        public IBackgroundTaskRegistration Register(string taskName, string taskEntryPoint, IBackgroundTrigger trigger, params IBackgroundCondition[] conditions)
        {
            // check for existing registration
            var registration = GetRegistration(taskName);
            if (registration != null)
                return registration;

            // create new registration
            var taskBuilder = new BackgroundTaskBuilder();
            taskBuilder.Name = taskName;
            taskBuilder.TaskEntryPoint = taskEntryPoint;
            taskBuilder.SetTrigger(trigger);

            if (conditions != null)
            {
                foreach (var condition in conditions)
                {
                    taskBuilder.AddCondition(condition);
                }
            }

            IBackgroundTaskRegistration result = null;
            try
            {
                result = taskBuilder.Register();
            }
            catch (Exception ex)
            {
                Logger.WriteLine(ex, "Could not register Task: " + taskName);
            }

            return result;
        }

        public IBackgroundTaskRegistration GetRegistration(string taskName)
        {
            foreach (var task in BackgroundTaskRegistration.AllTasks)
            {
                if (task.Value.Name == taskName)
                {
                    return task.Value;
                }
            }
            return null;
        }

        public bool RegistrationExists(string taskName)
        {
            return GetRegistration(taskName) != null;
        }

        public void Unregister(string taskName, bool cancelTask = false)
        {
            var registration = GetRegistration(taskName);
            if (registration != null)
                registration.Unregister(cancelTask);
        }

        public void UnregisterAll(bool cancelTasks = false)
        {
            foreach (var task in BackgroundTaskRegistration.AllTasks)
            {
                task.Value.Unregister(cancelTasks);
            }
        }
    }
}
