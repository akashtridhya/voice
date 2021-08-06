using System;
using System.Threading.Tasks;

namespace voice.queue
{
    public interface IQueue : IDisposable
    {
        /// <summary>
        /// This method is used to queue the task.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        Task EnqueueAsync(Action action);

        /// <summary>
        /// This method is used to queue the task.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        Task<T> EnqueueAsync<T>(Func<T> function);
    }
}