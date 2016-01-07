namespace ION.Droid.Fragment {

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

  using ION.Core.App;

  public class IONFragment : Fragment {

    /// <summary>
    /// The ion context for the fragment.
    /// </summary>
    /// <value>The ion.</value>
    public IION ion { get { return AppState.context; } }

    // Overridden from Fragment
    public override void OnCreate(Bundle savedInstanceState) {
      base.OnCreate(savedInstanceState);

      // Create your fragment here
    }

    // Overridden from OnCreateView
    public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
      // Use this to return your custom view for this Fragment
      // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

      return base.OnCreateView(inflater, container, savedInstanceState);
    }
  }
}

