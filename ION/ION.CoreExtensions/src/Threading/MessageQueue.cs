namespace ION.Core.Thread {

	using System;
	using System.Collections.Generic;
	using System.Threading;

	using ION.Core.Util;

	/// <summary>
	/// Message queue is a cross platform message handler that synchronizes and executes messages in a secondary thread.
	/// </summary>
	// TODO ahodder@appioninc.com: Optimize internal list
	// The pendingActions list really needs to be a sorted queue, sorted by execution time. However, in need to a timely
	// message queue implementation, I just used a list and removed the first time. This is pretty awful and should be
	// fixed.
	public class MessageQueue {
		/// <summary>
		/// The list of actions that are to be executed by the
		/// </summary>
		private List<PendingAction> pendingActions = new List<PendingAction>();
		/// <summary>
		/// The thread that the message queue is running on.
		/// </summary>
		private Thread thread;
		/// <summary>
		/// Whether or not the message queue has been started.
		/// </summary>
		private bool isStarted;
		/// <summary>
		/// Whether or not the message queue has been stopped.
		/// </summary>
		private bool isStopped;

		public MessageQueue() {
		}

		/// <summary>
		/// Starts the message queue.
		/// </summary>
		public void Start(ThreadPriority priority) {
			lock (pendingActions) {
				if (isStarted) {
					Log.D(this, "The message queue has already been started!");
					return;
				}
				thread = new Thread(ThreadLoop);
				thread.Priority = priority;
				isStarted = true;
				isStopped = false;
				thread.Start();
			}
		}

		/// <summary>
		/// Stops the message queue gracefully. The message queue will be stopped at the completion of its current task.
		/// </summary>
		public void Stop() {
			lock (pendingActions) {
				isStopped = true;
				isStarted = false;
				// Notify the ThreadLoop that it is time to stop.
				Monitor.PulseAll(pendingActions);
			}
		}

		/// <summary>
		/// Posts a new PendingAction.
		/// </summary>
		/// <param name="action">Action.</param>
		public void Post(PendingAction action) {
			lock (pendingActions) {
				pendingActions.Add(action);
				pendingActions.Sort((x, y) => x.desiredExecutionTime.CompareTo(y.desiredExecutionTime));
				Monitor.PulseAll(pendingActions);
			}
		}

		/// <summary>
		/// Posts a new action to the message queue. The action will execute at the given execution time. If the execution
		/// time is past the current time, the action will simply be sorted to the head of the queue.
		/// </summary>
		/// <param name="">.</param>
		/// <param name="desiredExecutionTime">Desired execution time.</param>
		public void Post(Action action, DateTime desiredExecutionTime, int categoryId=PendingAction.DEFAULT_CATEGORY_ID) {
			Post(new PendingAction(action, desiredExecutionTime, categoryId));
		}

		/// <summary>
		/// Posts and Action into the MessageQueue. 
		/// </summary>
		/// <param name="action">Action.</param>
		public void Post(Action action, int categoryId=PendingAction.DEFAULT_CATEGORY_ID) {
			Post(action, DateTime.Now, categoryId);
		}

		/// <summary>
		/// Posts a delayed action that will execute in the future as dictatated by the time span.
		/// </summary>
		/// <param name="action">Action.</param>
		/// <param name="timeSpan">Time span.</param>
		public void PostDelayed(Action action, TimeSpan timeSpan, int categoryId=PendingAction.DEFAULT_CATEGORY_ID) {
			Post(action, DateTime.Now + timeSpan, categoryId);
		}

		/// <summary>
		/// Posts a delayed action that will execute in the given number of milliseconds.
		/// </summary>
		/// <param name="action">Action.</param>
		/// <param name="milliseconds">Milliseconds.</param>
		public void PostDelayed(Action action, double milliseconds, int categoryId=PendingAction.DEFAULT_CATEGORY_ID) {
			PostDelayed(action, TimeSpan.FromMilliseconds(milliseconds), categoryId);
		}

		/// <summary>
		/// Removes all of the pending actions from the message queue.
		/// </summary>
		public void RemoveAllPendingActions() {
			lock (pendingActions) {
				pendingActions.Clear();
				Monitor.PulseAll(pendingActions);
			}
		}

		/// <summary>
		/// Removes all of the pending actions with the given category from the message queue.
		/// </summary>
		/// <param name="categoryId">Category identifier.</param>
		public int RemovePendingActionsWithCategoryId(int categoryId) {
			lock (pendingActions) {
				var ret = 0;

				for (int i = pendingActions.Count - 1; i >= 0; i--) {
					if (pendingActions[i].categoryId == categoryId) {
						pendingActions.RemoveAt(i);
						ret++;
					}
				}

				Monitor.PulseAll(pendingActions);

				return ret;
			}
		}


		/// <summary>
		/// The method that is the main loop for the message queue.
		/// </summary>
		private void ThreadLoop() {
			while (!isStopped) {
				lock (pendingActions) {
					Log.D(this, "We have: " + pendingActions.Count + " pending actions");
					if (pendingActions.Count <= 0) {
						Monitor.Wait(pendingActions);
					}
					Log.D(this, "The wait is over");

					// Check whether or not we have been stopped since the wait.
					if (isStopped) {
						continue;
					}

					// We still don't have anything in the queue.
					if (pendingActions.Count <= 0) {
						continue;
					}

					try {
						var action = pendingActions[0];

						if (action.desiredExecutionTime < DateTime.Now) {
							Log.D(this, "Executing action");
							action.action();
							pendingActions.RemoveAt(0);
						} else {
							// Inform the monitor that we want to wait untile the execution time OR we get a pulse. The pulse can come
							// a newly posted action or a removal of actions.
							Log.D(this, "We have: " + pendingActions.Count + " pending actions");
							Log.D(this, "We will wait: " + (action.desiredExecutionTime - DateTime.Now) + " until we execute the action");
							Monitor.Wait(pendingActions, action.desiredExecutionTime - DateTime.Now);
						}
					} catch (Exception e) {
						Log.E(this, "Action failed execution", e);
					}
				}
			}
		}
	}

	/// <summary>
	/// The action that will be executed by the message queue.
	/// </summary>
	public class PendingAction {
		public const int DEFAULT_CATEGORY_ID = 0;

		public Action action { get; private set; }
		public DateTime desiredExecutionTime { get; private set; }
		public int categoryId { get; private set; }

		/// <summary>
		/// Creates a new pending action that will execute as soon as possible with an id of DEFAULT_CATEGORY_ID.
		/// </summary>
		/// <param name="action">Action.</param>
		public PendingAction(Action action) : this(action, DateTime.Now, DEFAULT_CATEGORY_ID) {
		}

		public PendingAction(Action action, DateTime desiredExecutionTime) : this(action, desiredExecutionTime, DEFAULT_CATEGORY_ID) {
		}

		public PendingAction(Action action, DateTime desiredExecutionTime, int categoryId) {
			this.action = action;
			this.desiredExecutionTime = desiredExecutionTime;
			this.categoryId = categoryId;
		}
	}
}
