using System;
using System.Threading;
using System.Threading.Tasks;

namespace GoogleAnalyticsTracker.Core
{
    public static class AsyncHelper
    {
        private static readonly TaskFactory InnerTaskFactory = new TaskFactory(CancellationToken.None, TaskCreationOptions.None, TaskContinuationOptions.None, TaskScheduler.Default);

        public static void RunSync(Func<Task> func)
        {
            InnerTaskFactory.StartNew(func).Unwrap().GetAwaiter().GetResult();
        }

        public static TResult RunSync<TResult>(Func<Task<TResult>> func)
        {
            return InnerTaskFactory.StartNew(func).Unwrap().GetAwaiter().GetResult();
        }
    }
}