namespace ION.Droid.Dialog {

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

	using Appion.Commons.Measure;

  using ION.Core.Sensors;

  public class UnitDialog {
    /// <summary>
    /// Creates a new UnitDialog that will allow the user to select a unit.
    /// </summary>
    /// <param name="context">Context.</param>
    /// <param name="units">Units.</param>
    public static AlertDialog Create(Context context, IEnumerable<Unit> units, EventHandler<Unit> unitSelectionEvent) {
      var adb = new IONAlertDialog(context);

      adb.SetTitle(Resource.String.pick_unit);
      adb.SetItems(UnitsToString(units), (obj, args) =>{
        unitSelectionEvent(adb, units.ElementAt(args.Which));
      });
      adb.SetCancelable(true);

      return adb.Create();
    }

    /// <summary>
    /// Converts an enumeration of units to a string list.
    /// </summary>
    /// <returns>The to string.</returns>
    /// <param name="units">Units.</param>
    private static string[] UnitsToString(IEnumerable<Unit> units) {
      var ret = new string[units.Count()];

      for (int i = 0; i < units.Count(); i++) {
        ret[i] = units.ElementAt(i).ToString();
      }

      return ret;
    }
  }
}

