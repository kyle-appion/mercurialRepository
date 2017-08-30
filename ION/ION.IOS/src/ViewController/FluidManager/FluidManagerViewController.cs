// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Collections.Generic;

using Appion.Commons.Util;
using Foundation;
using ION.Core.App;
using ION.Core.Fluids;

using ION.IOS.Util;
using ION.IOS.ViewController;
using UIKit;

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
			AutomaticallyAdjustsScrollViewInsets = false;
			
      ion = AppState.context;
      fluidManager = ion.fluidManager;
      selectedFluid = fluidManager.lastUsedFluid.name;
      
      safetyHelpButton.Layer.BorderWidth = 2f;
			safetyHelpButton.TouchUpInside += safetyPopup;
			
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
        Log.D(this, "Called fluid selected delegate");
        if (onFluidSelectedDelegate != null) {
          Log.D(this, "Returning fluid " + selectedFluid);
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
          source = new FluidSource(tableContent, fluidManager, new List<string>(fluidManager.preferredFluids));
          break;
        case SECTION_LIBRARY:
          source = new FluidSource(tableContent, fluidManager, new List<string>(fluidManager.GetAvailableFluidNames()));
          break;
      }

      source.selectedFluid = selectedFluid;
      source.onFluidSelected += (string fluidName) => {
        selectedFluid = fluidName;
      };

      tableContent.Source = source;
      tableContent.ReloadData();
    }  
    
    public void safetyPopup(object sender, EventArgs e){
			UIAlertController safetyInformation;		
			
			NSError error = null;
      var htmlString = new NSAttributedString (
				NSData.FromString("ASHRAE Standard 34 provides refrigerant safety classification according to <u><b>toxicity</b></u> and <u><b>flammability</b></u>.</br></br>" +
													"<u><b>Toxicity</b></u>:</br>" +
													"A: Lower Toxicity</br>" +
													"B: Higher Toxicity</br></br>" +

													"<u><b>Flammability</b></u>:</br>" +
													"1: Non-flammable</br>" +
													"2L: Mildly flammable, lower limits</br>" +
													"2: Mildly flammable</br>" +
													"3: Flammable</br></br>" +

													"<u><b>Example</b></u>:</br>" +
													"<b>R410A</b> is classified <b>A1</b> (lower toxicity, non-flammable)</br>" +
													"<b>R32</b> is classified <b>A2L</b> (lower toxicity, mildly flammable, lower limits)"), 						
				new NSAttributedStringDocumentAttributes{ DocumentType = NSDocumentType.HTML, StringEncoding = NSStringEncoding.UTF8},
				ref error
			);

			UILabel safetyLabel = new UILabel();
			safetyLabel.AttributedText = htmlString;
			
      safetyInformation = UIAlertController.Create (title: "Safety", message: "", preferredStyle: UIAlertControllerStyle.Alert);
      safetyInformation.AddAction (UIAlertAction.Create (Util.Strings.CLOSE, UIAlertActionStyle.Cancel, (action) => {}));
      
      safetyInformation.SetValueForKey(htmlString,new NSString("attributedMessage"));
      
      this.PresentViewController (safetyInformation, true, null);
		}
	}
}
