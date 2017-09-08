using System;
using System.Collections.Generic;

using OxyPlot;
using OxyPlot.Series;

using ION.Core.Report.DataLogs;

namespace ION.Droid.Activity.Report {
  
  public static class SensorDataLogResultsExtensions {
    
    public static List<LineSeries> AsLineSeries(this SensorDataLogResults results, DateIndexLookup dil) {
      var ret = new List<LineSeries>();

      foreach (var id in results.sessionIds) {
        var ls = new LineSeries() {
          StrokeThickness = 1,
          MarkerType = MarkerType.Circle,
          MarkerSize = 0,
          MarkerStroke = OxyColors.Transparent,
          MarkerStrokeThickness = 1,
        };

        foreach (var dlm in results[id]) {
          ls.Points.Add(new DataPoint(dil[dlm.recordedDate], dlm.measurement.ConvertTo(dlm.measurement.unit.standardUnit).amount));
        }

        ret.Add(ls);
      }

      return ret;
    }
  }
}
