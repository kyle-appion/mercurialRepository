namespace ION.IOS.ViewController.Dialog {

  using System;

  using Foundation;
  using UIKit;

  using ION.Core.Measure;

  using ION.IOS.Util;

  /// <summary>
  /// A static factory class that creates useful UI elements.
  /// </summary>
  public class CommonDialogs {
    /// <summary>
    /// Creates a diaolg who presents the user with a series of unit for selection.
    /// </summary>
    /// <returns>The unit picker.</returns>
    /// <param name="name">Name.</param>
    /// <param name="units">Units.</param>
    /// <param name="unitSelected">Unit selected.</param>
    public static UIAlertController CreateUnitPicker(string name, Unit[] units, EventHandler<Unit> unitSelected) {
      var ret = UIAlertController.Create(name, "", UIAlertControllerStyle.Alert);

      foreach (var unit in units) {
        ret.AddAction(UIAlertAction.Create(unit.ToString(), UIAlertActionStyle.Default, (action) => {
          unitSelected(ret, unit);
        }));
      }

      ret.AddAction(UIAlertAction.Create(Strings.CANCEL, UIAlertActionStyle.Cancel, null));

      return ret;
    }
  }
}

