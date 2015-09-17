// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;

using ION.Core.App;
using ION.Core.Fluids;

using ION.IOS.Util;
using ION.IOS.ViewController;

namespace ION.IOS.ViewController.FluidManager {
	public partial class FluidManagerViewController : BaseIONViewController {
    /// <summary>
    /// The delegate that is called when a fluid is selected by the view controller
    /// </summary>
    public delegate void OnFluidSelected(Fluid fluid);

    private const int SECTION_FAVORITE = 0;
    private const int SECTION_LIBRARY = 1;

    public OnFluidSelected onFluidSelectedDelegate;

    /// <summary>
    /// The current fluid that the view controller has selected.
    /// </summary>
    /// <value>The selected fluid.</value>
    public string selectedFluid {
      get {
        return __selectedFluid;
      }
      set {
        __selectedFluid = value;
        if (IsViewLoaded) {
          viewFluidColor.BackgroundColor = CGExtensions.FromARGB8888(fluidManager.GetFluidColor(__selectedFluid));
          labelFluidName.Text = __selectedFluid;
        }
      }
    } string __selectedFluid;

    /// <summary>
    /// The ion instance for the view controller.
    /// </summary>
    /// <value>The ion.</value>
    private IION ion { get; set; }
    /// <summary>
    /// The fluid manager.
    /// </summary>
    /// <value>The fluid manager.</value>
    private IFluidManager fluidManager { get; set; }
    /// <summary>
    /// The source of fluid cells for our table.
    /// </summary>
    /// <value>The source.</value>
    private FluidSource source { get; set; }

    public FluidManagerViewController (IntPtr handle) : base (handle) {
      // Nope
		}

    // Overridden from UIViewController
    public override void ViewDidLoad() {
      base.ViewDidLoad();

      NavigationItem.Title = Strings.Fluid.Manager.SELF;
      /*
      NavigationItem.SetRightBarButtonItem(
        new UIBarButtonItem("Done", UIBarButtonItemStyle.Done, async (obj, args) => {
          if (onFluidSelectedDelegate != null) {
            onFluidSelectedDelegate(await fluidManager.GetFluidAsync(source.selectedFluid));
            NavigationController.PopViewController(true);
          } 
        }), false);
      */

      ion = AppState.context;
      fluidManager = ion.fluidManager;
      selectedFluid = fluidManager.lastUsedFluid.name;

      fluidManager.onFluidPreferenceChanged += (IFluidManager fluidManager, string fluidName) => {
        UpdateTableContent();
      };

      switchFluidSource.ValueChanged += (object sender, EventArgs e) => {
        UpdateTableContent();
      };
      switchFluidSource.SelectedSegment = SECTION_FAVORITE;
      UpdateTableContent();
    }

    // Overridden from UIViewController
    public async override void ViewWillDisappear(bool animated) {
      base.ViewWillDisappear(animated);

      if (IsMovingFromParentViewController) {
        ION.Core.Util.Log.D(this, "Called fluid selected delegate");
        if (onFluidSelectedDelegate != null) {
          ION.Core.Util.Log.D(this, "Returning fluid " + selectedFluid);
          onFluidSelectedDelegate(await fluidManager.GetFluidAsync(selectedFluid));
        }
      }
    }

    /// <summary>
    /// Updates the content of the view's table.
    /// </summary>
    private void UpdateTableContent() {
      switch ((int)switchFluidSource.SelectedSegment) {
        case SECTION_FAVORITE:
          source = new FluidSource(tableContent, fluidManager, fluidManager.preferredFluids);
          break;
        case SECTION_LIBRARY:
          source = new FluidSource(tableContent, fluidManager, fluidManager.GetAvailableFluidNames());
          break;
      }

      source.selectedFluid = selectedFluid;
      source.onFluidSelected += (string fluidName) => {
        selectedFluid = fluidName;
      };

      tableContent.Source = source;
      tableContent.ReloadData();
    }
	}
}
