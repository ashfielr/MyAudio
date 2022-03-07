namespace MyAudio.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Class for creating tasks which are async and lazy.
    /// </summary>
    /// <typeparam name="T">Generic type of task.</typeparam>
    internal class AsyncLazy<T>
    {
        private readonly Lazy<Task<T>> instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncLazy{T}"/> class.
        /// Runs an async lazy factory.
        /// </summary>
        /// <param name="factory">Factory to be run.</param>
        public AsyncLazy(Func<T> factory)
        {
            this.instance = new Lazy<Task<T>>(() => Task.Run(factory));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncLazy{T}"/> class.
        /// Runs an async lazy factory.
        /// </summary>
        /// <param name="factory">Factory to be run.</param>
        public AsyncLazy(Func<Task<T>> factory)
        {
            this.instance = new Lazy<Task<T>>(() => Task.Run(factory));
        }

        /// <summary>
        /// Gets await for task.
        /// </summary>
        /// <returns>Awaiter for the task.</returns>
        public TaskAwaiter<T> GetAwaiter()
        {
            return this.instance.Value.GetAwaiter();
        }
    }
}
