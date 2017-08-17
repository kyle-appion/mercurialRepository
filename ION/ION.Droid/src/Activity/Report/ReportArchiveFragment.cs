using System;

using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

using ION.Droid.Fragments;

namespace ION.Droid.Activity.Report {

  public class ReportArchiveFragment : IONFragment {

    public override View OnCreateView(LayoutInflater li, ViewGroup container, Bundle state) {
      var ret = li.Inflate(Resource.Layout.fragment_report_archive, container, false);

      return ret;
    }
  }
}
