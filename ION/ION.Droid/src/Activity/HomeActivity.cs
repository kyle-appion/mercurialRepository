namespace ION.Droid.Activity {

  using System;
  using System.Collections.Generic;

  using Android.App;
  using Android.Content;
  using Android.Graphics;
  using Android.Graphics.Drawables;
  using Android.OS;
  using Android.Runtime;
  using Android.Views;
  using Android.Widget;

  using Android.Support.V7.App;
  using Android.Support.V4.Widget;

  using ION.Core.App;
  using ION.Core.Util;

  using ION.Droid.Activity.DataLogging;
  using ION.Droid.Dialog;
  using ION.Droid.Fragments;
  using ION.Droid.Util;
  using ION.Droid.Widgets.Adapters.Navigation;

  [Activity(Label = "ActivityHome", Theme = "@style/AppTheme", LaunchMode=Android.Content.PM.LaunchMode.SingleTask)]      
  public class HomeActivity : IONActivity, AbsListView.IOnItemClickListener {

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
    /// The layout that will manager our navigation drawer.
    /// </summary>
    /// <value>The drawer layout.</value>
    private DrawerLayout drawerLayout { get; set; }
    /// <summary>
    /// The toggle that will expand or collapse the drawer.
    /// </summary>
    /// <value>The drawer toggle.</value>
    private DrawerToggle drawerToggle { get; set; }
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

    /// <summary>
    /// The items that are present in the navigation drawer.
    /// </summary>
    /// <value>The navigation items.</value>
    private List<NavigationItem> navigationItems { get; set; }
    /// <summary>
    /// The adapter that will provide navigation items to the navigation drawer
    /// </summary>
    /// <value>The navigation adapter.</value>
    private NavigationAdapter navigationAdapter { get; set; }
    /// <summary>
    /// The current active fragment.
    /// </summary>
    private Fragment activeFragment;

    // Overridden from Activity
    protected override void OnCreate(Bundle bundle) {
      base.OnCreate(bundle);

      if (AppState.context == null) {
        StartActivity(typeof(MainActivity));
        Finish();
        return;
      }

      SetContentView(Resource.Layout.activity_home);

      ActionBar.SetDisplayHomeAsUpEnabled(true);
      ActionBar.SetHomeButtonEnabled(true);
      drawerLayout = FindViewById<DrawerLayout>(Resource.Id.main);
      drawerToggle = new DrawerToggle(this, drawerLayout);
      drawerLayout.SetDrawerListener(drawerToggle);
      content = FindViewById(Resource.Id.content);
      drawer = FindViewById(Resource.Id.drawer);
      drawerList = FindViewById<ListView>(Resource.Id.drawer_list);
      drawerList.OnItemClickListener = this;
      drawerList.Adapter = navigationAdapter = new NavigationAdapter(BuildNavigationItems(), cache);

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

      if (!ion.version.Equals(ion.preferences.appVersion)/* && !ion.preferences.firstLaunch*/) {
        if (ion.preferences.showWhatsNew) {
          new WhatsNewDialog(ion, this).Show();
        }
        ion.preferences.appVersion = ion.version;
      }
    }

    // Overridden from Activity
    protected override void OnPostCreate(Bundle savedInstanceState) {
      base.OnPostCreate(savedInstanceState);
      drawerToggle.SyncState();
    }

    // Overridden from Activity
    public override void OnConfigurationChanged(Android.Content.Res.Configuration newConfig) {
      base.OnConfigurationChanged(newConfig);
      drawerToggle.OnConfigurationChanged(newConfig);
    }

    // Overridden from Activity
    public override bool OnOptionsItemSelected(IMenuItem item) {
      return drawerToggle.OnOptionsItemSelected(item) || base.OnOptionsItemSelected(item);
    }

    // Overridden from IOnItemClickListener
    public void OnItemClick(AdapterView adapterView, View view, int position, long id) {
      ResolveNavigationItemClick(position);
    }

    /// <summary>
    /// Hides the navigation drawer.
    /// </summary>
    public void HideDrawer() { 
      drawerLayout.CloseDrawer(drawer);
    }

    /// <summary>
    /// Makes the analyzer the primary fragment to be viewed.
    /// </summary>
    public void DisplayAnalyzer() {
      var ab = ActionBar;
      ab.SetIcon(GetColoredDrawable(Resource.Drawable.ic_nav_analyzer, Resource.Color.gray));
      drawerToggle.lastTitle = ab.Title = GetString(Resource.String.analyzer);

      GotoFragment(new AnalyzerFragment());
    }

    /// <summary>
    /// Makes the workbench the primary fragment to be viewed.
    /// </summary>
    public void DisplayWorkbench() {
      var ab = ActionBar;
      ab.SetIcon(GetColoredDrawable(Resource.Drawable.ic_nav_workbench, Resource.Color.gray));
      drawerToggle.lastTitle = ab.Title = GetString(Resource.String.workbench);

      GotoFragment(new WorkbenchFragment());
    }

    /// <summary>
    /// Navigates the to the given fragment.
    /// </summary>
    /// <param name="fragment">Fragment.</param>
    private void GotoFragment(Fragment fragment) {
      var ft = FragmentManager.BeginTransaction();

      ft.SetCustomAnimations(Resource.Animation.enter, Resource.Animation.exit);

      if (activeFragment != null) {
        ft.Remove(activeFragment);
      }

      ft.Add(Resource.Id.content, activeFragment = fragment, null);

      ft.Commit();

      drawerList.Adapter = navigationAdapter = new NavigationAdapter(BuildNavigationItems(), cache);
    }

    /// <summary>
    /// Constructs the list of navigation items that are present within the navigation adapter.
    /// </summary>
    /// <returns>The navitation items.</returns>
    private List<NavigationItem> BuildNavigationItems() {
      var ret = new List<NavigationItem>();

      var main = new NavigationCategory() {
        id = Resource.Id.main,
        title = GetString(Resource.String.main),
        items = new NavigationItem[] {

          new NavigationIconItem() {
            id = Resource.Id.analyzer,
            title = GetString(Resource.String.analyzer),
            icon = Resource.Drawable.ic_nav_analyzer,
            hidden = activeFragment is AnalyzerFragment,
            action = () => {
              DisplayAnalyzer();
              HideDrawer();
            },
          },

          new NavigationIconItem() {
            id = Resource.Id.workbench,
            title = GetString(Resource.String.workbench),
            icon = Resource.Drawable.ic_nav_workbench,
            hidden = activeFragment is WorkbenchFragment,
            action = () => {
              DisplayWorkbench();
              HideDrawer();
            },
          },
        },
      };

      var calculators = new NavigationCategory() {
        id = Resource.Id.calculators,
        title = GetString(Resource.String.calculators),
        items = new NavigationItem[] {
          new NavigationIconItem() {
            id = Resource.Id.ptchart,
            title = GetString(Resource.String.ptchart),
            icon = Resource.Drawable.ic_nav_ptconversion,
            action = () => {
              StartActivity(typeof(PTChartActivity));
            }
          },

          new NavigationIconItem() {
            id = Resource.Id.shsc,
            title = GetString(Resource.String.shsc),
            icon = Resource.Drawable.ic_nav_supersub,
            action = () => {
              StartActivity(typeof(SuperheatSubcoolActivity));
            }
          },
        },
      };

      var reports = new NavigationCategory() {
        id = Resource.Id.reports,
        title = GetString(Resource.String.reports),
        items = new NavigationItem[] {
          new NavigationIconItem() {
            id = Resource.Id.report_certificates,
            title = GetString(Resource.String.report_certificates),
            icon = Resource.Drawable.ic_nav_certificate,
            action = () => {
              StartActivity(typeof(CalibrationCertificateArchiveActivity));
            }
          },
          new NavigationIconItem() {
            id = Resource.Id.report_screenshot_archive,
            title = GetString(Resource.String.report_screenshot_archive),
            icon = Android.Resource.Drawable.IcMenuCamera,
            action = () => {
              StartActivity(typeof(ScreenshotArchiveActivity));
            }
          },

#if DEBUG
          new NavigationIconItem() {
            id = Resource.Id.report_data_logging,
            title = GetString(Resource.String.report_data_logging),
            icon = Android.Resource.Drawable.StatSysDataBluetooth,
            action = () => {
              StartActivity(typeof(DataLoggingReportActivity));
            }
          }
#endif
        },
      };

      var settings = new NavigationCategory() {
        title = GetString(Resource.String.settings),
        items = new NavigationItem[] {
          new NavigationIconItem() {
            id = Resource.Id.settings,
            title = GetString(Resource.String.settings),
            icon = Resource.Drawable.ic_settings,
            action = () => {
              StartActivity(typeof(AppPreferenceActivity));
            }
          },
          new NavigationIconItem() {
            id = Resource.Id.help,
            title = GetString(Resource.String.help),
            icon = Resource.Drawable.ic_help,
            action = () => {
              StartActivity(typeof(HelpPreferenceActivity));
            }
          }
        },
      };

      var exit = new NavigationCategory() {
        title = GetString(Resource.String.exit),
        items = new NavigationItem[] {
          new NavigationIconItem() {
            id = Resource.Id.exit,
            title = GetString(Resource.String.exit),
            icon = Resource.Drawable.ic_nav_power,
            action = () => {
              Shutdown();
            }
          },
        },
      };

      ret.Add(main);
      ret.Add(calculators);
      ret.Add(reports);
      ret.Add(settings);
      ret.Add(exit);

      return ret;
    }

    /// <summary>
    /// Resolves the click of a navigation item.
    /// </summary>
    /// <param name="position">Position.</param>
    private void ResolveNavigationItemClick(int position) {
      Action action = navigationAdapter[position].action;
      if (action != null) {
        action();
      }
      HideDrawer();
    }

    /// <summary>
    /// Terminates the application in a safe way.
    /// </summary>
    private void Shutdown() {
      AppState.context.Dispose();
      Finish();
    }

    /// <summary>
    /// A quick implementation of the drawer toggle that will give us some customized
    /// operations.
    /// </summary>
    private class DrawerToggle : ActionBarDrawerToggle {
      private HomeActivity activity { get; set; }
      public string lastTitle { get; set; }

      public DrawerToggle(HomeActivity activity, DrawerLayout layout) : base(activity, layout, Resource.String.open, Resource.String.close) {
        this.activity = activity;
      }

      // Overridden from ActionBarDrawerToggle
      public override void OnDrawerOpened(View drawerView) {
        base.OnDrawerOpened(drawerView);
        lastTitle = activity.ActionBar.Title;
        activity.ActionBar.Title = activity.GetString(Resource.String.navigation);
      }

      // Overridden from ActionBarDrawerToggle
      public override void OnDrawerClosed(View drawerView) {
        base.OnDrawerClosed(drawerView);
        activity.ActionBar.Title = lastTitle;
      }
    }
  }
}

