namespace ION.Droid.Fragments {

  using System;

  using Android.Views;
  using Android.OS;

  using ION.Droid.Widgets;

  public class AnalyzerFragment : IONFragment {

    /// <summary>
    /// The view that will display the application's analyzer.
    /// </summary>
    private AnalyzerView analyzerView;

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
      analyzerView = new AnalyzerView(inflater.Context);


      return analyzerView;
    }

    /// <Docs>If the fragment is being re-created from
    ///  a previous saved state, this is the state.</Docs>
    /// <summary>
    /// Raises the activity created event.
    /// </summary>
    /// <param name="savedInstanceState">Saved instance state.</param>
    public override void OnActivityCreated(Bundle savedInstanceState) {
      base.OnActivityCreated(savedInstanceState);
    }
  }
}

