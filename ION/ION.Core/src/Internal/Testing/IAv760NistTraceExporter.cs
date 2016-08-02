namespace ION.Core.Internal.Testing {

	using System.Collections.Generic;
	using System.IO;

	using ION.Core.Devices;

	public interface IAv760NistTraceExporter {
		bool Export(Stream stream, TestResults results);
	}
}

