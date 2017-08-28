using System;
using System.Collections.Generic;
using System.IO;

using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V7.Widget;
using Android.Views;

using ION.Core.App;
using ION.Core.Database;
using ION.Core.Devices;
using ION.Core.Report.DataLogs;
using ION.Core.Sensors;
using ION.Core.UI;

using ION.Droid.Widgets.RecyclerViews;

namespace ION.Droid.Activity.Report {
  public class ReportGraphAdapter : RecordAdapter {
		/// <summary>
		/// The event that is used to notify listeners that the overlay has been changed.
		/// </summary>
		public event Action onOverlayDragEvent;

    /// <summary>
    /// Whether or not the adapter records are showing a detailed summary or a graph.
    /// </summary>
    /// <value><c>true</c> if show details; otherwise, <c>false</c>.</value>
    public bool showDetails {
      get {
        return _showDetails;
      }
      set {
        _showDetails = value;
        foreach (var record in records) {
          var r = record as ReportGraphRecord;
          if (r != null) {
            r.detailView = _showDetails;
          }
        }

        NotifyDataSetChanged();
      }
    } bool _showDetails;

    /// <summary>
    /// The ion context.
    /// </summary>
    /// <value>The ion.</value>
    public IION ion { get; private set; }
    /// <summary>
    /// Gets the dil.
    /// </summary>
    /// <value>The dil.</value>
    public DateIndexLookup dil { get; private set; }
		/// <summary>
		/// The percent from the left of the view that the left handle has been dragged.
		/// </summary>
		/// <value>The left percent.</value>
		public float leftPercent { get; private set; }
		/// <summary>
		/// The percent from the left of the view that the right handle has been dragged.
		/// </summary>
		/// <value>The left percent.</value>
		public float rightPercent { get; private set; }

		/// <summary>
		/// The view that contains the left and right handles. This is used to measure the size of over that will be drawn
		/// to this list. (ie. the check box that appears in the list items for the ReportGraphActivity are not subject to
		/// the overlay).
		/// </summary>
		/// <remarks>
		/// This is done in a round about way as at the time of writing, a clear option to have the overlay render to the
		/// list was not available.
		/// </remarks>
		private View _parent;
		/// <summary>
		/// The view that is used to drag the left section of the overlay.
		/// </summary>
		private View _leftHandle;
		/// <summary>
		/// The view that is used to drag the right section of the overlay.
		/// </summary>
		private View _rightHandle;
		/// <summary>
		/// The last touch x for the left handle.
		/// </summary>
		private float _lastLeftX;
		/// <summary>
		/// The last touch x for the right handle.
		/// </summary>
		private float _lastRightX;


    public ReportGraphAdapter(IION ion, DateIndexLookup dil, IEnumerable<SensorDataLogResults> sensorResults, View parent, View leftHandle, View rightHandle) {
      this.ion = ion;
      this.dil = dil;
			_parent = parent;
			_leftHandle = leftHandle;
			_rightHandle = rightHandle;


      foreach (var result in sensorResults) {
        records.Add(new ReportGraphRecord(dil, result));
      }

			_leftHandle.Touch += (sender, e) => {
				switch (e.Event.Action) {
					case MotionEventActions.Down:
						_lastLeftX = e.Event.GetX();
						break;

					case MotionEventActions.Move:
						var xcoord = _leftHandle.GetX() + (e.Event.GetX() - _lastLeftX);
						xcoord = xcoord.Clamp(0, _rightHandle.GetX() - _leftHandle.Width);
						_leftHandle.SetX(xcoord);
						_leftHandle.Invalidate();
            NotifyDataSetChanged();
						leftPercent = (_leftHandle.GetX()) / (float)_parent.Width;
						NotifyDrag();
						break;
				}
			};

			_rightHandle.Touch += (sender, e) => {
				switch (e.Event.Action) {
					case MotionEventActions.Down:
						_lastRightX = e.Event.GetX();
						break;

					case MotionEventActions.Move:
						var xcoord = _rightHandle.GetX() + (e.Event.GetX() - _lastRightX);
						xcoord = xcoord.Clamp(_leftHandle.GetX() + _leftHandle.Width, _parent.Width - _rightHandle.Width);
						_rightHandle.SetX(xcoord);
						_rightHandle.Invalidate();
						NotifyDataSetChanged();
						rightPercent = (_rightHandle.GetX() + _leftHandle.Width) / (float)_parent.Width;
						NotifyDrag();
						break;
				}
			};
    }

    public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
      return new ReportGraphViewHolder(this, parent);
    }

    /// <summary>
    /// Sets the displayed start and end time for all of the records. This will cause a list invalidation.
    /// </summary>
    /// <param name="start">Start.</param>
    /// <param name="end">End.</param>
    public void SetStartEndDates(DateTime start, DateTime end) {
      foreach (var record in records) {
        var r = record as ReportGraphRecord;
        if (r != null) {
          r.ApplyDateRange(start, end);
        }
      }

      NotifyDataSetChanged();
    }

    /// <summary>
    /// Resets the records dates to the dates contained in the dil.
    /// </summary>
    public void Reset() {
      leftPercent = 0;
      rightPercent = 1;  
      _leftHandle.SetX(0);
      _rightHandle.SetX(_parent.Width - _rightHandle.Width);
      var start = dil.GetDateTimeFromIndex(0);
      var end = dil.GetDateTimeFromIndex(dil.count - 1);
			foreach (var record in records) {
				var r = record as ReportGraphRecord;
				if (r != null) {
					r.ApplyDateRange(start, end);
				}
			}
      
      NotifyDrag();
			NotifyDataSetChanged();
    }
    
    /// <summary>
    /// Builds the dictionary of results that have been selected from the adapter.
    /// </summary>
    /// <returns>The selected results.</returns>
    public Dictionary<GaugeDeviceSensor, SensorDataLogResults> GetCheckedResults() {
      var ret = new Dictionary<GaugeDeviceSensor, SensorDataLogResults>();
      
      var startDate = dil.GetDateTimeFromIndex((int)(dil.count * leftPercent));
      var endDate = dil.GetDateTimeFromIndex((int)((dil.count - 1) * rightPercent));
      
      foreach (var record in records) {
        var rgr = record as ReportGraphRecord;
        if (rgr == null || !rgr.isChecked) {
          continue;
        }
        
        ret[rgr.data.sensor] = rgr.data.SubsetByDates(startDate, endDate);
      }
      
      return ret;
    }
    
		/// <summary>
		/// Safely performs an onOverlayDragEvent notification.
		/// </summary>
		private void NotifyDrag() {
			if (onOverlayDragEvent != null) {
				onOverlayDragEvent();
			}
		}
  }
}
