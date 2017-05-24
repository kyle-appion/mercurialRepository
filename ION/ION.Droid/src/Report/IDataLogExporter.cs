namespace ION.Droid {

  using System;

  using ION.Core.App;
  using ION.Core.Report.DataLogs;

  public interface IDataLogExporter {
    Task<bool> Export(IION ion, DataLogReport dlr);
  }
}
