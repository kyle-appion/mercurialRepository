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

  [Activity(Label = "SuperheatSubcoolActivity", Icon = "@drawable/ic_nav_supersub", Theme = "@style/TerminalActivityTheme")]      
  public class SuperheatSubcoolActivity : IONActivity {
    /// <summary>
    /// Raises the create event.
    /// </summary>
    /// <param name="savedInstanceState">Saved instance state.</param>
    protected override void OnCreate(Bundle savedInstanceState) {
      base.OnCreate(savedInstanceState);

      SetContentView(Resource.Layout.activity_superheat_subcool);
    }
  }
}

