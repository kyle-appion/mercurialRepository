namespace ION.Core.Report.DataLogs.Exporter {

	using System;
  using System.IO;
  using System.Threading.Tasks;

  using ION.Core.App;
	using ION.Core.Report.DataLogs;

	public interface IDataLogExporter {
    Task<bool> Export(Stream stream, DataLogReport report);
  }
}
