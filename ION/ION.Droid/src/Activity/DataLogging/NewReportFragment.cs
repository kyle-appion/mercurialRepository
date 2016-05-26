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
      // Use this to return your custom view for this Fragment
      // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

      return base.OnCreateView(inflater, container, savedInstanceState);
    }

    /// <Docs>If the fragment is being re-created from
    ///  a previous saved state, this is the state.</Docs>
    /// <summary>
    /// Raises the create event.
    /// </summary>
    /// <param name="savedInstanceState">Saved instance state.</param>
    public override void OnCreate(Bundle savedInstanceState) {
      base.OnCreate(savedInstanceState);

      // Create your fragment here
    }
  }
}

