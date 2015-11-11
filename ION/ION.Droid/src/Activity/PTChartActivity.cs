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

  using ION.Core.Sensors;

  [Activity(Label = "PTChartActivity", Icon = "@drawable/ic_nav_ptconversion", Theme = "@style/TerminalActivityTheme")]      
  public class PTChartActivity : IONActivity {

    /// <summary>
    /// The view that will hold the fluid color for the ptchart.
    /// </summary>
    /// <value>The color of the fluid.</value>
    private View fluidColor { get; set; }

    private Switch fluidStateToggle { get; set; }

    /// <summary>
    /// The sensor that will hold / provide the pressure measurements for calculation.
    /// </summary>
    /// <value>The pressure sensor.</value>
    private Sensor pressureSensor { get; set; }
    /// <summary>
    /// The sensor that will hold / provide the temperature measurements for calculation.
    /// </summary>
    /// <value>The temperautre sensor.</value>
    private Sensor temperatureSensor { get; set; }

    // Overridden from IONActivity
    protected override void OnCreate(Bundle bundle) {
      base.OnCreate(bundle);

      ActionBar.SetIcon(GetColoredDrawable(Resource.Drawable.ic_nav_ptconversion, Resource.Color.gray));
      ActionBar.SetDisplayHomeAsUpEnabled(true);

      SetContentView(Resource.Layout.activity_ptchart);
    }
  }
}

