namespace ION.Core.Report.DataLogs {

  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;

  using ION.Core.App;
  using ION.Core.Database;


  /// <summary>
  /// A simple structure that will maintain a lookup for dates to indices.
  /// </summary>
  public class DateIndexLookup {

    public int count { get { return _lookup.Count; } }

    public int this[DateTime key] {
      get {
        if (_lookup.ContainsKey(key)) {
          return _lookup[key];
        } else {
          return -1;
        }
      }
    }

    private Dictionary<DateTime, int> _lookup;

    public DateIndexLookup(Dictionary<DateTime, int> lookup) {
      _lookup = lookup;
    }

    public DateTime GetDateTimeFromIndex(int index) {
      foreach (var dt in _lookup.Keys) {
        if (_lookup[dt] == index) {
          return dt;
        }
      }

      return DateTime.FromFileTimeUtc(0);
    }

    /// <summary>
    /// Creates a new DateIndexLookup built around the given sessionIds.
    /// </summary>
    /// <returns>The from sessions async.</returns>
    /// <param name="sessionIds">Session identifiers.</param>
    public static Task<DateIndexLookup> CreateFromSessionsAsync(IION ion, IEnumerable<int> sessionIds) {
      return Task.Factory.StartNew(() => {
        // Gather all of the dates.
        var dates = new HashSet<DateTime>();
        foreach (var id in sessionIds) {
          var smrQuery = ion.database.Table<SensorMeasurementRow>().Where(x => x.frn_SID == id)
             .Select(x => new { x.recordedDate });
          foreach (var smr in smrQuery) {
            dates.Add(smr.recordedDate);
          }
        }

        // Sort the dates.
        var sorted = new List<DateTime>(dates);
        sorted.Sort();

        var ret = new Dictionary<DateTime, int>();

        var i = 0;
        foreach (var dt in sorted) {
          ret[dt] = i++;
        }

        return new DateIndexLookup(ret);
      });
    }
  }
}
