namespace ION.Core.Report.DataLogs {

  using System;
  using System.Collections.Generic;

  using ION.Core.Database;

  public class DataLogReport {

    /// <summary>
    /// The session results that are making up the current report.
    /// </summary>
    /// <value>The results.</value>
    public List<SessionResults> results { get; set; }

    public DataLogReport() {
    }
  }
}

