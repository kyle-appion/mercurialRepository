namespace TestBench.Droid {

	using System.Collections.Generic;

	using DSoft.Datatypes.Grid.Data;

	using Appion.Commons.Util;

	public class TestDataSource : DSDataTable {
		public ITest test { get; private set; }

		private List<IConnection> connections;

		public TestDataSource(ITest test) : base() {
			this.test = test;

			Columns.Add(new DSDataColumn("Serial Number") {
				Caption = "Serial Number",
				DataType = typeof(string),
				AllowSort = false,
				ReadOnly = true,
				Width = 110,
			});

			foreach (var tp in test.parameters.targetPoints) {
				Log.D(this, tp.ToRowString());
				Columns.Add(new DSDataColumn(tp.ToRowString()) {
					Caption = tp.ToRowString(),
					DataType = typeof(string),
					AllowSort = false,
					ReadOnly = true,
					Width = 125,
				});
			}

			connections = new List<IConnection>();
			connections.AddRange(test.connections.Values);
		}

		public override int GetRowCount() {
			return connections.Count;
		}

		public override DSDataRow GetRow(int index) {
			var connection = connections[index];

			DSDataRow ret = null;
			if (index < Rows.Count) {
				ret = Rows[index];
			}

			if (ret == null) {
				ret = new DSDataRow();
				ret["Serial Number"] = connection.serialNumber.ToString();
				Rows.Add(ret);
			}

			foreach (var tp in test.parameters.targetPoints) {
				var resultTable = test.testResults.resultsTable[connection.serialNumber];
				TestResults.Result result;
				if (resultTable.TryGetValue(tp, out result) && result != null) {
					ret[tp.ToRowString()] = resultTable[tp].result.ToString(); 
				} else {
					ret[tp.ToRowString()] = "N/A";
				}
			}

			return ret;
		}
	}
}
