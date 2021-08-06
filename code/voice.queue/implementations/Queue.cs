using System;
using System.Threading.Tasks;

namespace voice.queue.implementations
{
    public class Queue : IQueue
    {
        #region Object Declaration
        readonly object _locker = new object();
        WeakReference<Task> _lastTask;
        #endregion

        /// <summary>
        /// This method is used to queue the task.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Task EnqueueAsync(Action action)
        {
            return EnqueueAsync<object>(() => { action(); return null; });
        }

        /// <summary>
        /// This method is used to queue the task.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public void Enqueue(Action action)
        {
            Enqueue<object>(() => { action(); return null; });
        }

        /// <summary>
        /// This method is used to queue the task.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Task<T> EnqueueAsync<T>(Func<T> function)
        {
            lock (_locker)
            {
                Task lastTask = null;
                Task<T> resultTask = null;
                if (_lastTask != null && _lastTask.TryGetTarget(out lastTask))
                    resultTask = lastTask.ContinueWith(_ => function());
                else
                    resultTask = Task.Run(function);
                _lastTask = new WeakReference<Task>(resultTask);
                return resultTask;
            }
        }

        /// <summary>
        /// This method is used to queue the task.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public void Enqueue<T>(Func<T> function)
        {
            lock (_locker)
            {
                Task lastTask = null;
                Task<T> resultTask = null;
                if (_lastTask != null && _lastTask.TryGetTarget(out lastTask))
                    resultTask = lastTask.ContinueWith(_ => function());
                else
                    resultTask = Task.Run(function);
                _lastTask = new WeakReference<Task>(resultTask);
                //return resultTask;
            }
        }

        #region House Keeping

        public void Dispose()
        {
            Disponse(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Disponse(bool disposing)
        {
            if (disposing) { }
        }
              
        #endregion
    }
}
