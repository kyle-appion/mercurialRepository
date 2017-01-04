namespace Appion.Commons.Util {
  
  using System;
  using System.Threading;
  using System.Threading.Tasks;

  /// <summary>
  /// The delegate that is called on every timer interval.
  /// </summary>
  public delegate void TimerCallback(Timer timer);

  /// <summary>
  /// This class is a simple implementation of a timer used to address the fact that PCL does not support System.Timers.
  /// Timer starts the timer thread as soon as construction is complete, and will run in a separate until stopped.
  /// </summary>
  public class Timer : IDisposable {

    /// <summary>
    /// The callback that will be notified when the timer event fires.
    /// </summary>
    private TimerCallback callback;
    /// <summary>
    /// The source that will allow us to cancel the timer.
    /// </summary>
    private CancellationTokenSource cancelSource;
    /// <summary>
    /// The task that is started when the timer is created.
    /// </summary>
    private Task task;

    public Timer(TimerCallback callback, TimeSpan period) {
      cancelSource = new CancellationTokenSource();
      Task.Factory.StartNew(async () => {
        while (!cancelSource.IsCancellationRequested) {
          callback(this);

          await Task.Delay(period);
        }
      }, cancelSource.Token);
    }

    /// <summary>
    /// Cancels the timer.
    /// </summary>
    /// <returns><c>true</c> if this instance cancel ; otherwise, <c>false</c>.</returns>
    public void Cancel() {
      Dispose();
    }

    /// <summary>
    /// Releases all resource used by the <see cref="ION.Core.Timer"/> object.
    /// </summary>
    /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="ION.Core.Timer"/>. The
    /// <see cref="Dispose"/> method leaves the <see cref="ION.Core.Timer"/> in an unusable state. After calling
    /// <see cref="Dispose"/>, you must release all references to the <see cref="ION.Core.Timer"/> so the garbage
    /// collector can reclaim the memory that the <see cref="ION.Core.Timer"/> was occupying.</remarks>
    public void Dispose() {
      try {
        cancelSource.Cancel();
        task.Wait();
      } catch (Exception e) {
      }

      cancelSource.Dispose();
    }
  }
}

