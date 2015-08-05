using System;
using Windows.ApplicationModel.Background;

namespace UWPCore.Framework.Tasks
{
    /// <summary>
    /// The background task service class.
    /// </summary>
    public class BackgroundTaskService : IBackgroundTaskService
    {
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

            return taskBuilder.Register();
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

        public void Unregister(string taskName)
        {
            var registration = GetRegistration(taskName);
            if (registration != null)
                registration.Unregister(true);
        }

        public void UnregisterAll()
        {
            foreach (var task in BackgroundTaskRegistration.AllTasks)
            {
                task.Value.Unregister(true);
            }
        }
    }
}
