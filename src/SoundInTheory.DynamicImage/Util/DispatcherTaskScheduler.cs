using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace SoundInTheory.DynamicImage.Util
{
    /// <summary>
    /// This class was authored by Drew Marsh (https://github.com/drub0y).
    /// It is from the gist at https://gist.github.com/drub0y/5681496/raw/b3d3ffd2d8a5bb23c055c05c808a9f90f805ef5c/DispatcherTaskScheduler.cs.
    /// </summary>
    internal sealed class DispatcherTaskScheduler : TaskScheduler, IDisposable
    {
        #region Fields

        private static readonly Lazy<DispatcherTaskScheduler> DefaultSchedulerInstance = new Lazy<DispatcherTaskScheduler>(() => new DispatcherTaskScheduler());

        private readonly DispatcherWorkerThreadDetails[] dispatcherWorkerThreadDetails;
        private readonly Queue<Task> taskQueue = new Queue<Task>();
        private readonly int maxDegreeOfParallelism;
        private readonly Action tryExecuteAction;
        private int nextRoundRobinSelectedWorkerThreadIndex;

        #endregion

        #region Constructors

        public DispatcherTaskScheduler()
            : this(Environment.ProcessorCount)
        {

        }

        public DispatcherTaskScheduler(int maxDegreeOfParallelism)
        {
            if (maxDegreeOfParallelism < 1)
            {
                throw new ArgumentOutOfRangeException("maxDegreeOfParallelism", "The value for maxDegreeOfParallelism cannot be less than 1.");
            }

            this.maxDegreeOfParallelism = maxDegreeOfParallelism;
            this.tryExecuteAction = new Action(this.TryExecuteTaskFromQueue);

            this.dispatcherWorkerThreadDetails = new DispatcherWorkerThreadDetails[maxDegreeOfParallelism];

            for (int threadIndex = 0; threadIndex < maxDegreeOfParallelism; threadIndex++)
            {
                this.dispatcherWorkerThreadDetails[threadIndex] = this.StartNewDispatcherWorkerThread();
            }
        }

        #endregion

        #region Type specific properties

        public static new DispatcherTaskScheduler Default
        {
            get { return DispatcherTaskScheduler.DefaultSchedulerInstance.Value; }
        }

        #endregion

        #region Base class overrides

        public override int MaximumConcurrencyLevel
        {
            get { return this.maxDegreeOfParallelism; }
        }

        protected override IEnumerable<Task> GetScheduledTasks()
        {
            lock (this.taskQueue)
            {
                return this.taskQueue.ToArray();
            }
        }

        protected override void QueueTask(Task task)
        {
            lock (this.taskQueue)
            {
                this.taskQueue.Enqueue(task);
            }

            Thread targetThread;

            // Look for an idle thread to dispatch the work to first
            if (!this.TryFindIdleWorkerThread(out targetThread))
            {
                // No idle threads, use round robin to fairly distribute
                targetThread = this.GetNextWorkerThreadViaRoundRobin();
            }

            Dispatcher.FromThread(targetThread).BeginInvoke(this.tryExecuteAction, DispatcherPriority.Normal);
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            bool result;

            if (!taskWasPreviouslyQueued
                    &&
                Dispatcher.FromThread(Thread.CurrentThread) != null)
            {
                result = this.TryExecuteTask(task);
            }
            else
            {
                result = false;
            }

            return result;
        }

        #endregion

        #region IDisposable implementation

        public void Dispose()
        {
            foreach (DispatcherWorkerThreadDetails dispatcherThreadDetails in this.dispatcherWorkerThreadDetails)
            {
                // Tell the dispatcher to shut down
                Dispatcher.FromThread(dispatcherThreadDetails.Thread).BeginInvokeShutdown(DispatcherPriority.Send);

                // Wait for the Dispatcher message pump to exit
                dispatcherThreadDetails.Thread.Join();
            }
        }

        #endregion

        #region Helper methods

        private DispatcherWorkerThreadDetails StartNewDispatcherWorkerThread()
        {
            Thread dispatcherThread = new Thread(() =>
            {
                // Start the dispatcher message loop so that it begins processing messages on this thread
                Dispatcher.Run();
            });

            dispatcherThread.Name = "DispatcherTaskScheduler Worker Thread";
            dispatcherThread.SetApartmentState(ApartmentState.STA);
            dispatcherThread.IsBackground = true;
            dispatcherThread.Start();

            return new DispatcherWorkerThreadDetails(dispatcherThread);
        }

        private void TryExecuteTaskFromQueue()
        {
            Task task;

            Debug.WriteLine("{0} #{1} attempting to grab work off the task queue...", Thread.CurrentThread.Name, Thread.CurrentThread.ManagedThreadId);

            // Try to aquire the task queue lock lock
            lock (this.taskQueue)
            {
                // Attempt to get a new task from the queue to process
                if (this.taskQueue.Count > 0)
                {
                    task = this.taskQueue.Dequeue();
                }
                else
                {
                    task = null;
                }
            }

            if (task != null)
            {
                // Attempt to execute the task
                this.TryExecuteTask(task);
            }
            else
            {
                Debug.WriteLine("{0} #{1} didn't find a task to work on which would indicate another thread must have beat it to the dequeue and there was no work left.", Thread.CurrentThread.Name, Thread.CurrentThread.ManagedThreadId);
            }
        }

        private bool TryFindIdleWorkerThread(out Thread targetThread)
        {
            targetThread = null;

            for (int threadIndex = 0; threadIndex < this.dispatcherWorkerThreadDetails.Length; threadIndex++)
            {
                DispatcherWorkerThreadDetails details = this.dispatcherWorkerThreadDetails[threadIndex];

                if (!details.HasWork)
                {
                    targetThread = details.Thread;

                    break;
                }
            }

            return targetThread != null;
        }

        private Thread GetNextWorkerThreadViaRoundRobin()
        {
            int lastLocal;
            int nextLocal;

            do
            {
                lastLocal = this.nextRoundRobinSelectedWorkerThreadIndex;
                nextLocal = lastLocal + 1;

                if (nextLocal == this.dispatcherWorkerThreadDetails.Length)
                {
                    nextLocal = 0;
                }
            }
            while (Interlocked.CompareExchange(ref this.nextRoundRobinSelectedWorkerThreadIndex, nextLocal, lastLocal) != lastLocal);

            return this.dispatcherWorkerThreadDetails[lastLocal].Thread;
        }

        #endregion

        #region Nested types

        private sealed class DispatcherWorkerThreadDetails
        {
            public Thread Thread;
            public volatile bool HasWork;

            public DispatcherWorkerThreadDetails(Thread thread)
            {
                this.Thread = thread;

                Dispatcher dispatcher;

                // Get the Dispatcher instance for the thread
                do
                {
                    dispatcher = Dispatcher.FromThread(thread);

                    // The dispatcher might not have been spun up yet so, if not, sleep and try again until it is
                    if (dispatcher == null)
                    {
                        Thread.Sleep(100);
                    }
                } while (dispatcher == null);

                // Hook the events that tell us when the Dispatcher is actually doing something
                DispatcherHooks dispatcherHooks = dispatcher.Hooks;
                dispatcherHooks.DispatcherInactive += (s, a) => this.HasWork = false;
                dispatcherHooks.OperationPosted += (s, a) => this.HasWork = true;
            }
        }

        #endregion
    }
}