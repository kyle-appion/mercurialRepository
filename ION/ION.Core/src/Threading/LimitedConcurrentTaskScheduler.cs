using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ION.Core.Threading {
  /// <summary>
  /// A simple task scheduler that will attempt to bring task down to the
  /// desired number of concurrent queues.
  /// </summary>
  public class LimitedConcurrentTaskScheduler : TaskScheduler {
    /// <summary>
    /// Indicates whether or not the current thread is processing tasks.
    /// </summary>
    private static bool currentThreadIsProcessingTasks { get; set; }

    // Overridden from TaskScheduler
    public sealed override int MaximumConcurrencyLevel {
      get {
        return maxParallelism;
      }
    }

    /// <summary>
    /// The max number of tasks to be executed in parallel.
    /// </summary>
    public int maxParallelism { get; private set; }
    /// <summary>
    /// The number of tasks that are currently being excuted.
    /// </summary>
    public int tasksBeingExecutedCount { get; private set; }
    /// <summary>
    /// The list of tasks that are waiting to be executed.
    /// </summary>
    private LinkedList<Task> tasks = new LinkedList<Task>();

    /// <summary>
    /// Creates a new LimitedConcurrentTaskScheduler that will only allow
    /// maxParallelism tasks to be executed at once. If maxParallelism is <= 0,
    /// we will default to 1.
    /// </summary>
    /// <param name="maxParallelism"></param>
    public LimitedConcurrentTaskScheduler(int maxParallelism) {
      if (maxParallelism <= 0) {
        maxParallelism = 1;
      }
      this.maxParallelism = maxParallelism;
    }

    // Overridden from TaskScheduler
    protected sealed override void QueueTask(Task task) {
      lock (tasks) {
        tasks.AddLast(task);
        if (tasksBeingExecutedCount < maxParallelism) {
          tasksBeingExecutedCount++;
          NotifyThreadPoolOfPendingWork();
        }
      }
    }

    // Overridden from TaskScheduler
    protected sealed override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued) {
      if (!currentThreadIsProcessingTasks) {
        return false;
      }

      if (taskWasPreviouslyQueued) {
        TryDequeue(task);
      }

      return base.TryExecuteTask(task);
    }

    // Overridden from TaskScheduler
    protected sealed override bool TryDequeue(Task task) {
      lock (tasks) {
        return tasks.Remove(task);
      }
    }

    protected sealed override IEnumerable<Task> GetScheduledTasks() {
      bool lockTaken = false;

      try {
        Monitor.TryEnter(tasks, ref lockTaken);
        if (lockTaken) {
          return tasks.ToArray();
        } else {
          throw new NotSupportedException();
        }
      } finally {
        if (lockTaken) {
          Monitor.Exit(tasks);
        }
      }
    }

    /// <summary>
    /// Notifies the thread pool that more work has become available for
    /// processing.
    /// </summary>
    private void NotifyThreadPoolOfPendingWork() {
      ThreadPool.QueueUserWorkItem(_ => {
        currentThreadIsProcessingTasks = true;

        try {
          while (true) {
            Task task;

            lock(tasks) {
              if (tasks.Count == 0) {
                tasksBeingExecutedCount--;
                break;
              }

              task = tasks.First.Value;
              tasks.RemoveFirst();
            }

            base.TryExecuteTask(task);
          }
        } finally {
          currentThreadIsProcessingTasks = false;
        }
      }, null);
    }
  } // End LimitedConcurrentTaskScheduler
}
