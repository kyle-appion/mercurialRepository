using System;
using System.Collections.Generic;

using CoreGraphics;
using Foundation;
using UIKit;

using ION.Core.Fluids;
using ION.Core.Util;

using ION.IOS.Util;

namespace ION.IOS.ViewController.FluidManager {
  public class FluidSource : UITableViewSource {

    private const string CELL_FLUID = "cellFluid";

    /// <summary>
    /// The delegate that will be called when a fluid is selected.
    /// </summary>
    public delegate void OnFluidSelected(string fluidName);

    public event OnFluidSelected onFluidSelected;

    public string selectedFluid {
      get {
        return __selectedFluid;
      }
      set {
        __selectedFluid = value;
        table.ReloadData();
        if (onFluidSelected != null) {
          onFluidSelected(__selectedFluid);
        }
      }
    } string __selectedFluid;

    /// <summary>
    /// The table that this source is providing to.
    /// </summary>
    /// <value>The table.</value>
    private UITableView table { get; set; }
    /// <summary>
    /// The fluid manager that will be used to adjust fluid states.
    /// </summary>
    /// <value>The fluid manager.</value>
    private IFluidManager fluidManager { get; set; }
    /// <summary>
    /// The list of fluid names that will be displayed by the fluid source.
    /// </summary>
    /// <value>The fluid names.</value>
    private List<string> fluidNames { get; set; }

    /// <summary>
    /// The source that will provide cells for the given table.
    /// </summary>
    /// <param name="table">Table.</param>
    /// <param name="fluidManager">Fluid manager.</param>
    public FluidSource(UITableView table, IFluidManager fluidManager, List<string> fluidNames) {
      this.table = table;
      this.fluidManager = fluidManager;
      this.fluidNames = fluidNames;
    }

    // Overridden from UITableViewSource
    public override void RowSelected(UITableView tableView, NSIndexPath indexPath) {
      selectedFluid = fluidNames[indexPath.Row];
      tableView.ReloadData();
    }

    // Overridden from UITableViewSource
    public override nint NumberOfSections(UITableView tableView) {
      return 1;
    }

    // Overridden from UITableViewSource
    public override nint RowsInSection(UITableView tableview, nint section) {
      return fluidNames.Count;
    }

    // Overridden from UITableViewSource
    public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath) {
      return 48;
    }

    // Overridden from UITableViewSource
    public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath) {
      var fluidName = fluidNames[(int)indexPath.Row];

      var cell = tableView.DequeueReusableCell(CELL_FLUID) as FluidTableCell;
      var color = fluidManager.GetFluidColor(fluidName);
      var preferred = fluidManager.IsFluidPreferred(fluidName);

      cell.UpdateTo(fluidName, color, fluidName.Equals(selectedFluid), preferred, (object obj, string fn) => {
        fluidManager.MarkFluidAsPreferred(fn, !preferred);
        tableView.ReloadData();
      });

      return cell;
    }
  }
}

