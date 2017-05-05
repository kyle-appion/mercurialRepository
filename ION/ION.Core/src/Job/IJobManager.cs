namespace ION.Core.Job {

  using System;
  using System.Threading.Tasks;

  using ION.Core.App;

  /// <summary>
  /// A contract for the job manager.
  /// </summary>
  public interface IJobManager : IManager {
    /// <summary>
    /// The active job 
    /// </summary>
    /// <value>The active job.</value>
    Job activeJob { get; set; }

    /// <summary>
    /// Creates a new job. If make active is true, then the job will be made the active job.
    /// </summary>
    /// <returns>The new job.</returns>
    /// <param name="makeActive">If set to <c>true</c> make active.</param>
    Task<Job> CreateNewJob(bool makeActive);
  }
}

