namespace ION.Core.Job {

  using System;
  using System.Threading.Tasks;

  using ION.Core.Database;

  public class Job {

    /// <summary>
    /// The database object that we will use to modify this job in the database.
    /// </summary>
    private JobRow internalJob;

    public Job() {
    }

    /// <summary>
    /// Creates a new job. The job will be added to the database.
    /// </summary>
    /// <returns>The new job.</returns>
    public static Task<Job> CreateNewJob() {
      return null;
    }
  }
}

