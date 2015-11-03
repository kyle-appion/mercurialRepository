
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

namespace ION.Droid.Widgets.Adapters.Viewer {
  /// <summary>
  /// An android pattern "ViewHolder" that can be used for multiple Viewer views.
  /// </summary>
  internal class ManifoldViewHolder : Java.Lang.Object {
    public TextView title, measurement, status, unit;
    public ImageView battery, connection, icon, alarm;
  }
}

