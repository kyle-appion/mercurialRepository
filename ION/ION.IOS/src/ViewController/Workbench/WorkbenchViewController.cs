using System;
using System.Collections.Generic;

using Foundation;
using UIKit;

using ION.Core.App;
using ION.Core.Content;
using ION.Core.Content.Parsers;
using ION.Core.Devices;
using ION.Core.Measure;
using ION.Core.Sensors;
using ION.Core.Util;

using ION.IOS.Devices;
using ION.IOS.Sensors;
using ION.IOS.UI;
using ION.IOS.Util;
using ION.IOS.ViewController.DeviceManager;

namespace ION.IOS.ViewController.Workbench {
	public partial class WorkbenchViewController : BaseIONViewController {
    /// <summary>
    /// The current ion context.
    /// </summary>
    /// <value>The ion.</value>
    public IION ion { get; private set; }
    /// <summary>
    /// The workbench that we are working with.
    /// </summary>
    /// <value>The workbench.</value>
    private ION.Core.Content.Workbench workbench { get; set; }
    /// <summary>
    /// The source that will provide Viewer views to the table view.
    /// </summary>
    /// <value>The source.</value>
    private WorkbenchSource source { get; set; }

		public WorkbenchViewController (IntPtr handle) : base (handle) {
      // Nope
		}

    // Overridden from UIViewController
    public override void ViewDidLoad() {
      base.ViewDidLoad();

      InitNavigationBar("ic_nav_workbench", false);
      backAction = () => {
        root.navigation.ToggleMenu();
      };

      Title = Strings.Workbench.SELF.FromResources();

      ion = AppState.context;
      workbench = ion.currentWorkbench;

      tableContent.AllowsSelection = true;

      source = new WorkbenchSource(this, workbench);
      source.onRequestViewerDelegate = OnRequestViewer;

      tableContent.Source = source;

      ion.currentWorkbench.onManifoldAdded += OnManifoldAdded;
      ion.currentWorkbench.onManifoldRemoved += OnManifoldRemoved;
    }

    // Overridden from UIViewController
    public override void ViewDidUnload() {
      ion.currentWorkbench.onManifoldAdded -= OnManifoldAdded;
      ion.currentWorkbench.onManifoldRemoved -= OnManifoldRemoved;
    }

    /// <summary>
    /// Called when the viewer source wishes to request a new viewer.
    /// </summary>
    private void OnRequestViewer() {
      var sb = InflateViewController<DeviceManagerViewController>(VC_DEVICE_MANAGER);
      sb.onSensorReturnDelegate = (GaugeDeviceSensor sensor) => {
        workbench.AddSensor(sensor);
      };
      // TODO ahodder@appioninc.com: Set initialial arguments.
      NavigationController.PushViewController(sb, true);
      //          Toast.New(tableView, "Clicked 'Add New Viewer'");
    }

    /// <summary>
    /// Called when the workbench adds a new manifold.
    /// </summary>
    /// <param name="workbench">Workbench.</param>
    /// <param name="manifold">Manifold.</param>
    private async void OnManifoldAdded(ION.Core.Content.Workbench workbench, Manifold manifold) {
      tableContent.ReloadData();
      await ion.SaveWorkbenchAsync();
    }

    /// <summary>
    /// Called when the workbench removes a manifold.
    /// </summary>
    /// <param name="workbench">Workbench.</param>
    /// <param name="manifold">Manifold.</param>
    private async void OnManifoldRemoved(ION.Core.Content.Workbench workbench, Manifold manifold) {
      tableContent.ReloadData();
      await ion.SaveWorkbenchAsync();
    }
	}
}
