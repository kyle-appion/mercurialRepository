namespace ION.Droid.Activity {

  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;

  using Android.App;
  using Android.Content;
  using Android.OS;
  using Android.Runtime;
  using Android.Views;
  using Android.Widget;

  using ION.Core.App;
  using ION.Core.Util;

  [Activity(Label = "ActivityHome")]      
  public class HomeActivity : Activity {

    /// <summary>
    /// The extra that indicates which fragment the home activity should launch when
    /// created.
    /// </summary>
    public const string EXTRA_SHOW_FRAGMENT = "ION.Droid.extra.SHOW_FRAGMENT";
    /// <summary>
    /// The constant that is used with EXTRA_SHOW_FRAGMENT to make the activity
    /// show the workbench fragment on create. This is the default value when the
    /// extra is null.
    /// </summary>
    public const string SHOW_WORKBENCH = "showWorkbench";

    /// <summary>
    /// The view that holds the content views.
    /// </summary>
    /// <value>The content.</value>
    private View content { get; set; }

    /// <summary>
    /// The view that contains the drawer views.
    /// </summary>
    /// <value>The drawer.</value>
    private View drawer { get; set; }
    /// <summary>
    /// The list that will hold the navigation items.
    /// </summary>
    /// <value>The drawer lsit.</value>
    private ListView drawerList { get; set; }

    protected override void OnCreate(Bundle bundle) {
      base.OnCreate(bundle);

      if (AppState.context == null) {
        StartActivity(typeof(MainActivity));
        Finish();
        return;
      }

      SetContentView(Resource.Layout.activity_home);

      content = FindViewById(Resource.Id.content);
      drawer = FindViewById(Resource.Id.drawer);
      drawerList = FindViewById<ListView>(Resource.Id.drawer_list);

      var emptyText = new TextView(this);
      emptyText.Text = "Not items";
      drawerList.EmptyView = emptyText;

      var i = Intent;

      var show = i.GetStringExtra(EXTRA_SHOW_FRAGMENT);
      if (show == null) {
        show = SHOW_WORKBENCH;
      }

      switch (show) {
        case SHOW_WORKBENCH:
          DisplayWorkbench();
          break;
        default:
          Log.D(this, "SHOW_FRAGMENT extra value: " + show + " invalid. Defaulting to workbench");
          DisplayWorkbench();
          break;
      }
    }

    /// <summary>
    /// Makes the workbench the primary fragment to be viewed.
    /// </summary>
    public void DisplayWorkbench() {
    }
  }
}

