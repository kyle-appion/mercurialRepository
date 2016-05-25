namespace ION.Droid.Activity.DataLogging {

  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;

  using Android.App;
  using Android.Content;
  using Android.OS;
  using Android.Runtime;
  using Android.Util;
  using Android.Views;
  using Android.Widget;

  using ION.Droid.Activity;
  using ION.Droid.Fragments;

  /// <summary>
  /// The fragment that will move the user onward to selecting session to create a report.
  /// </summary>
  public class NewReportFragment : IONFragment {

    /// <summary>
    /// The button that will set the list content to that of the job selector.
    /// </summary>
    private Button buttonByJob;
    /// <summary>
    /// The button that will set the list content to that of the date selector.
    /// </summary>
    private Button buttonByDate;
    /// <summary>
    /// The list view that will display either the job content or the date content.
    /// </summary>
    private ListView listView;
    /// <summary>
    /// The button that will, in by job mode, allow the user to create a job if on does not exist.
    /// </summary>
    private Button buttonCreateJob;

    private ReportSessionListAdapter sessionAdapter;


    /// <Docs>The LayoutInflater object that can be used to inflate
    ///  any views in the fragment,</Docs>
    /// <param name="savedInstanceState">If non-null, this fragment is being re-constructed
    ///  from a previous saved state as given here.</param>
    /// <returns>To be added.</returns>
    /// <summary>
    /// Raises the create view event.
    /// </summary>
    /// <param name="inflater">Inflater.</param>
    /// <param name="container">Container.</param>
    public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
      var ret = inflater.Inflate(Resource.Layout.fragment_new_report, container, false);

      buttonByJob = ret.FindViewById<Button>(Resource.Id.report_by_job);
      buttonByDate = ret.FindViewById<Button>(Resource.Id.report_by_date);
      listView = ret.FindViewById<ListView>(Resource.Id.list);
      buttonCreateJob = ret.FindViewById<Button>(Resource.Id.report_create_job);

      return ret;
    }

    /// <Docs>If the fragment is being re-created from
    ///  a previous saved state, this is the state.</Docs>
    /// <summary>
    /// Raises the activity created event.
    /// </summary>
    /// <param name="savedInstanceState">Saved instance state.</param>
    public override void OnActivityCreated(Bundle savedInstanceState) {
      base.OnActivityCreated(savedInstanceState);

      buttonByJob.Click += (sender, e) => {
      };

      buttonByDate.Click += (sender, e) => {
      };

      sessionAdapter = new ReportSessionListAdapter();
    }
  }
}

