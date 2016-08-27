using System;
using System.Threading.Tasks;
using Windows.UI.Core;

namespace UWPCore.Framework.Common
{
    /// <summary>
    /// The dispather wrapper class for thread-safity.
    /// </summary>
    public class DispatcherWrapper
    {
        /// <summary>
        /// The core dispatcher of Windows Runtime to wrap.
        /// </summary>
        private readonly CoreDispatcher dispatcher;

        /// <summary>
        /// Creates a dispatcher wrapper instance.
        /// </summary>
        /// <param name="dispatcher">The core dispatcher of Windows Runtime to wrap.</param>
        public DispatcherWrapper(CoreDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        /// <summary>
        /// Dispatches the action asynchronously.
        /// </summary>
        /// <param name="action">The action to dispatch.</param>
        public async Task DispatchAsync(Action action)
        {
            if (dispatcher.HasThreadAccess) { action(); }
            else
            {
                var tcs = new TaskCompletionSource<object>();
                await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    try { action(); tcs.TrySetResult(null); }
                    catch (Exception ex) { tcs.TrySetException(ex); }
                });
                await tcs.Task;
            }
        }

        /// <summary>
        /// Dispatches the function asynchronously.
        /// </summary>
        /// <param name="func">The function to dispatch.</param>
        public async Task DispatchAsync(Func<Task> func)
        {
            if (dispatcher.HasThreadAccess) { await func?.Invoke(); }
            else
            {
                var tcs = new TaskCompletionSource<object>();
                await dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                {
                    try { await func(); tcs.TrySetResult(null); }
                    catch (Exception ex) { tcs.TrySetException(ex); }
                });
                await tcs.Task;
            }
        }

        /// <summary>
        /// Dispatches the function asynchronously.
        /// </summary>
        /// <param name="func">The function to dispatch.</param>
        public async Task<T> DispatchAsync<T>(Func<T> func)
        {
            if (dispatcher.HasThreadAccess) { return func(); }
            else
            {
                var tcs = new TaskCompletionSource<T>();
                await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    try { tcs.TrySetResult(func()); }
                    catch (Exception ex) { tcs.TrySetException(ex); }
                });
                await tcs.Task;
                return tcs.Task.Result;
            }
        }

        /// <summary>
        /// Gets the core dispatcher.
        /// </summary>
        public CoreDispatcher CoreDispatcher
        {
            get { return dispatcher; }
        }
    }
}
