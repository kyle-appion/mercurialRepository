using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace ION.IOS.ViewController.Walkthrough {
  public interface IWalkthrough{  
		void GoForward();
		void GoBackward();
	}
	
	public abstract class WalkthroughProperties : IWalkthrough{
    public int currentStep {get;set;}
    public UIView parentView {get;set;}
    public UILabel explanation {get;set;}
    public UIImageView arrowImage {get;set;}
    public UIButton arrowButton {get;set;}
    public UIImageView screenshot {get;set;}
    
    public abstract void GoForward();
    public abstract void GoBackward();
	}
/// <summary>
/// Walkthrough methods for Workbench
/// </summary>
	public class WorkBenchWalkthrough : WalkthroughProperties{
		public WorkBenchWalkthrough(UIView pView, UILabel textLabel,UIImageView screenshotImage, UIButton nextButton) {
      currentStep = 1;
      parentView = pView;
      explanation = textLabel;
      screenshot = screenshotImage; 
      arrowButton = new UIButton(new CGRect(.16 * parentView.Bounds.Width,60,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height));
      arrowImage = new UIImageView(new CGRect(.16 * parentView.Bounds.Width,60,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height));
      arrowImage.Image = UIImage.FromBundle("walkthrough_arrow");
      
      arrowButton.TouchDown += (sender, e) => {
				nextButton.SendActionForControlEvents(UIControlEvent.TouchDown);
			};
			arrowButton.TouchUpOutside += (sender, e) => {
				nextButton.SendActionForControlEvents(UIControlEvent.TouchUpOutside);
			};
      arrowButton.TouchUpInside += (sender, e) => {
				nextButton.SendActionForControlEvents(UIControlEvent.TouchUpInside);
			};
			pView.AddSubview(arrowButton);
			pView.BringSubviewToFront(arrowButton);
			pView.AddSubview(arrowImage);
			pView.BringSubviewToFront(arrowImage);
		}
		
		public override void GoForward(){
      currentStep++;

      switch (currentStep) {
        case 1:
          screenshot.Image = UIImage.FromBundle("WorkBench1");
          explainMenuDrawer();
          break;
        case 2:
          explainRecordButton();
          break;
        case 3:
          explainScreenshotButton();
          break;
        case 4:
          explainAddViewerButton();
          break;
        case 5:
        	screenshot.Image = UIImage.FromBundle("WorkBench2");
        	explainOpenDeviceManger();
        	break;
        case 6:
        	explainDMMenu();
        	break;
      	case 7:
        	explainDMScanButton();
        	break;
      	case 8:
        	explainDMCategories();
        	break;
      	case 9:
        	explainDMDeviceDropDown();
        	break;
      	case 10:
      		screenshot.Image = UIImage.FromBundle("WorkBench3");
        	explainDMAddDevice();
        	break;
        case 11:
        	screenshot.Image = UIImage.FromBundle("WorkBench4");
        	explainInitialDevice();
        	break;
        case 12:
					explainWBDeviceTap();
        	break;
        case 13:
        	screenshot.Image = UIImage.FromBundle("WorkBench5");
   				explainWBActionPopup();
        	break;
				case 14:
        	screenshot.Image = UIImage.FromBundle("WorkBench6");
					explainAddingSubview();
        	break;
				case 15:
        	screenshot.Image = UIImage.FromBundle("WorkBench7");
        	explainSubviewLayout();
        	break;      
        default:
          currentStep = 1;
          screenshot.Image = UIImage.FromBundle("WorkBench1");
          explainMenuDrawer();
          break;
      }
		}
		public override void GoBackward(){
        currentStep--;
      switch (currentStep) {
        case 1:
          explainMenuDrawer();
          break;
        case 2:
          explainRecordButton();
          break;
        case 3:
          explainScreenshotButton();
          break;
        case 4:
          screenshot.Image = UIImage.FromBundle("WorkBench1");
          explainAddViewerButton();
          break;
        case 5:					
					explainOpenDeviceManger();
					break;
				case 6:
					explainDMMenu();
					break;
      	case 7:
        	explainDMScanButton();
        	break;
      	case 8:
        	explainDMCategories();
        	break;
      	case 9:
      	  screenshot.Image = UIImage.FromBundle("WorkBench2");
        	explainDMDeviceDropDown();
        	break;
      	case 10:
      	  screenshot.Image = UIImage.FromBundle("WorkBench3");
        	explainDMAddDevice();
        	break;
        case 11:
        	explainInitialDevice();
        	break;
        case 12:
        	screenshot.Image = UIImage.FromBundle("WorkBench4");
					explainWBDeviceTap();
        	break;
        case 13:
        	screenshot.Image = UIImage.FromBundle("WorkBench5");
   				explainWBActionPopup();
        	break;
				case 14:
        	screenshot.Image = UIImage.FromBundle("WorkBench6");
					explainAddingSubview();
        	break;
				case 15:
        	screenshot.Image = UIImage.FromBundle("WorkBench7");
        	explainSubviewLayout();
        	break;
        default:
          currentStep = 15;
          screenshot.Image = UIImage.FromBundle("WorkBench7");
          explainSubviewLayout();
          Console.WriteLine("Step 15 restart backward");
          break;
      }
		}
    private void explainMenuDrawer(){
    	arrowImage.Hidden = false;
			arrowButton.Hidden = false;
			
      arrowButton.Frame = new CGRect(.16 * parentView.Bounds.Width,60,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
      arrowImage.Frame = new CGRect(.16 * parentView.Bounds.Width,60,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);
      explanation.Text = "This is the app menu button. You can press this to access any other section of the app.";
    }
    private void explainRecordButton(){
    	arrowButton.Frame = new CGRect(.755 * parentView.Bounds.Width,.065 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
      arrowImage.Frame = new CGRect(.755 * parentView.Bounds.Width,.085 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);
      explanation.Text = "This is the record button for starting and ending a logging session of your connected devices' readings.";
    }
    private void explainScreenshotButton(){
      arrowButton.Frame = new CGRect(.795 * parentView.Bounds.Width,.065 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
      arrowImage.Frame = new CGRect(.795 * parentView.Bounds.Width,.085 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);
      explanation.Text = "This is the screenshot button that captures your device's screen to allow for keeping records of important moments.";
    }
    private void explainAddViewerButton(){
    	arrowImage.Hidden = false;
    	arrowButton.Hidden = false;
    	
      arrowButton.Frame = new CGRect(.475 * parentView.Bounds.Width,.13 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
      arrowImage.Frame = new CGRect(.475 * parentView.Bounds.Width,.15 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);
      explanation.Text = "This button lets you add a device to the workbench for monitoring. You will recieve the information as soon as the physical device does. Pressing this button opens the device manager.";
    }
    private void explainOpenDeviceManger(){ 
      arrowImage.Hidden = true;
      arrowButton.Hidden = true;
      explanation.Text = "This is the device manager. It lists the devices available for connecting to.";	
		}
		private void explainDMMenu(){
			arrowImage.Hidden = false;
			arrowButton.Hidden = false;
			
			arrowButton.Frame = new CGRect(.16 * parentView.Bounds.Width,.065 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
			arrowImage.Frame = new CGRect(.16 * parentView.Bounds.Width,.085 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);
			 explanation.Text = "Pressing this button will take you back to the previous screen.";		
		}
		private void explainDMScanButton(){
			arrowButton.Hidden = false;
			arrowImage.Hidden = false;
		
      arrowButton.Frame = new CGRect(.79 * parentView.Bounds.Width,.065 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
      arrowImage.Frame = new CGRect(.79 * parentView.Bounds.Width,.085 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);
      explanation.Text = "This button lists the devices you know and haven't seen before.";
		}
		private void explainDMCategories(){
			arrowButton.Hidden = true;
			arrowImage.Hidden = true;
		
			explanation.Text = "Devices are categorized to let you know what you are currently connected to and what else you have available.";
		}	
		private void explainDMDeviceDropDown(){ 
			arrowButton.Hidden = false;
			arrowImage.Hidden = false;
			
			arrowButton.Frame = new CGRect(.475 * parentView.Bounds.Width,.12 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
      arrowImage.Frame = new CGRect(.475 * parentView.Bounds.Width,.14 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);      
			explanation.Text = "Press here to see a dropdown displaying more info about the device";
		}
		private void explainDMAddDevice(){
			arrowImage.Hidden = false;
			arrowButton.Hidden = false;
			
			arrowButton.Frame = new CGRect(.785 * parentView.Bounds.Width,.17 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
      arrowImage.Frame = new CGRect(.785 * parentView.Bounds.Width,.19 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height); 
			explanation.Text = "Press here to add the device to the workbench"; 
		}
		private void explainInitialDevice(){
			arrowImage.Hidden = true;
			arrowButton.Hidden = true;
			explanation.Text = "The chosen device has now been added to the workbench.";
		}
		private void explainWBDeviceTap(){
			arrowImage.Hidden = false;
			arrowButton.Hidden = false;

			arrowButton.Frame = new CGRect(.475 * parentView.Bounds.Width,.16 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
      arrowImage.Frame = new CGRect(.475 * parentView.Bounds.Width,.18 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);      
			explanation.Text = "Press the device to open a list of actions.";			
		}
		private void explainWBActionPopup(){
			arrowImage.Hidden = true;
			arrowButton.Hidden = true;
			explanation.Text = "A popup will appear giving you the options available for that device. You can rename the device, add a subview to monitor information about the device, set an alarm to alert you based on device readings, disconnect from a device, or remove a device from the workbench.";
		}
		private void explainAddingSubview(){
			explanation.Text = "When selecting to add a subview a new popup will list a device's available subviews. Based on the device you can choose to keep track of the rate of change, minimum, maximum, hold a specific value that was recieved, monitor changes with a timer, view the saturated temperature or pressure for a reading, or check the superheat/subcool.";
		}
		private void explainSubviewLayout(){
			arrowImage.Hidden = true;
			arrowButton.Hidden = true;
			
			explanation.Text = "Subviews will be displayed under each device and pressing on them will allow you to interact and setup their configurations.";
		}		
	}
	/// <summary>
	/// Walkthrough methods for Analyzer
	/// </summary>
	public class AnalyzerWalkthrough : WalkthroughProperties{
		public AnalyzerWalkthrough(UIView pView, UILabel textLabel,UIImageView screenshotImage, UIButton nextButton) {
      currentStep = 1;
      parentView = pView;
      explanation = textLabel;
      screenshot = screenshotImage;
      arrowButton = new UIButton(new CGRect(.16 * parentView.Bounds.Width,.07 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height));
      arrowImage = new UIImageView(new CGRect(.16 * parentView.Bounds.Width,.09 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height));
      arrowImage.Image = UIImage.FromBundle("walkthrough_arrow");
      
      arrowButton.TouchDown += (sender, e) => {
				nextButton.SendActionForControlEvents(UIControlEvent.TouchDown);
			};
			arrowButton.TouchUpOutside += (sender, e) => {
				nextButton.SendActionForControlEvents(UIControlEvent.TouchUpOutside);
			};
      arrowButton.TouchUpInside += (sender, e) => {
				nextButton.SendActionForControlEvents(UIControlEvent.TouchUpInside);
			};

			pView.AddSubview(arrowButton);
			pView.BringSubviewToFront(arrowButton);
			pView.AddSubview(arrowImage);
			pView.BringSubviewToFront(arrowImage);	
		}
		
		public override void GoForward(){
    	currentStep++;
    	switch (currentStep) {
    		case 1:
    		  screenshot.Image = UIImage.FromBundle("Analyzer1");		      
     			explainMenuDrawer();
    			break;
    		case 2:
    			explainRecordButton();
    			break;
				case 3:
					explainEmptyTap();
					break;
				case 4:
					screenshot.Image = UIImage.FromBundle("Analyzer2");
					explainAddDevice();
					break;
				case 5:
					screenshot.Image = UIImage.FromBundle("Analyzer3");
					explainAddedDevice();
					break;
				case 6:
					screenshot.Image = UIImage.FromBundle("Analyzer4");
					explainSwapDevice();
					break;
				case 7:
					explainOccupiedTap();
					break;
				case 8:
					screenshot.Image = UIImage.FromBundle("Analyzer5");
					explainDevicePopup();
					break;
				case 9:
					explainActionPress();
					break;
				case 10:
					screenshot.Image = UIImage.FromBundle("Analyzer6");
					explainDeviceActions();
					break;
				case 11:
					screenshot.Image = UIImage.FromBundle("Analyzer4");
					explainLowHighDrag();
					break;
				case 12:
					screenshot.Image = UIImage.FromBundle("Analyzer7");
					explainLowHigh();
					break;
			  case 13:
			  	explainLowHighTap();
			  	break;
			  case 14:
			  	screenshot.Image = UIImage.FromBundle("Analyzer8");
			  	explainLowHighOptions();
			  	break;
			  case 15:
			  	screenshot.Image = UIImage.FromBundle("Analyzer9");
			  	explainLowHighSubviews();
			  	break;
			  case 16:
			  	screenshot.Image = UIImage.FromBundle("Analyzer10");
			  	explainSubviewArrangement();
			  	break;
				default:
					currentStep = 1;
					screenshot.Image = UIImage.FromBundle("Analyzer1");
					explainMenuDrawer();						
					break;
			}
		}
		public override void GoBackward(){
     currentStep--;
     	switch (currentStep) {
     		case 1:
     			screenshot.Image = UIImage.FromBundle("Analyzer1");
     			explainMenuDrawer();
     			break;
     		case 2:
     			explainRecordButton();
     			break;
				case 3:
					screenshot.Image = UIImage.FromBundle("Analyzer1");
					explainEmptyTap();
					break;
				case 4:
					screenshot.Image = UIImage.FromBundle("Analyzer2");
					explainAddDevice();
					break;
				case 5:
					screenshot.Image = UIImage.FromBundle("Analyzer3");
					explainAddedDevice();
					break;
				case 6:						
					explainSwapDevice();
					break;
				case 7:
					screenshot.Image = UIImage.FromBundle("Analyzer4");
					explainOccupiedTap();
					break;
				case 8:						
					explainDevicePopup();
					break;
				case 9:
					screenshot.Image = UIImage.FromBundle("Analyzer5");
					explainActionPress();
					break;
				case 10:
					screenshot.Image = UIImage.FromBundle("Analyzer6");
					explainDeviceActions();
					break;
				case 11:
					screenshot.Image = UIImage.FromBundle("Analyzer4");
					explainLowHighDrag();
					break;
				case 12:						
					explainLowHigh();
					break;
			  case 13:
			  	screenshot.Image = UIImage.FromBundle("Analyzer7");
			  	explainLowHighTap();
			  	break;
			  case 14:
			  	screenshot.Image = UIImage.FromBundle("Analyzer8");
			  	explainLowHighOptions();
			  	break;
			  case 15:
			  	screenshot.Image = UIImage.FromBundle("Analyzer9");
			  	explainLowHighSubviews();
			  	break;
			  case 16:
			  	screenshot.Image = UIImage.FromBundle("Analyzer10");
			  	explainSubviewArrangement();
			  	break;					
				default:
					currentStep = 16;
					screenshot.Image = UIImage.FromBundle("Analyzer10");
					explainSubviewArrangement();
					break;
		 	}
		}
		private void explainMenuDrawer(){
    	arrowImage.Hidden = false;
			arrowButton.Hidden = false;
			
      arrowButton.Frame = new CGRect(.16 * parentView.Bounds.Width,.07 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
      arrowImage.Frame = new CGRect(.16 * parentView.Bounds.Width,.09 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);
      explanation.Text = "This is the app menu button. You can press this to access any other section of the app.";
    }
	  private void explainRecordButton(){
    	arrowButton.Frame = new CGRect(.795 * parentView.Bounds.Width,.07 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
      arrowImage.Frame = new CGRect(.795 * parentView.Bounds.Width,.09 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);
      explanation.Text = "This is the record button for starting and ending a logging session of your connected devices' readings.";
    }
    private void explainEmptyTap(){
    	arrowImage.Hidden = false;
			arrowButton.Hidden = false;
			
    	arrowButton.Frame = new CGRect(.22 * parentView.Bounds.Width,.19 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
      arrowImage.Frame = new CGRect(.22 * parentView.Bounds.Width,.21 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);
      explanation.Text = "Pressing an empty sensor slot will show a popup listing the options for adding a device to the analyzer.";				
		}
		private void explainAddDevice(){
    	arrowImage.Hidden = true;
			arrowButton.Hidden = true;
			
      explanation.Text = "You can choose to add a device through the device manager via bluetooth or create a manual device with your own type, unit of measurement, and value.";
		}
		private void explainAddedDevice(){				
      explanation.Text = "The device you've added or created will be positioned in the area that was pressed.";
		}
		private void explainSwapDevice(){
    	arrowImage.Hidden = true;
			arrowButton.Hidden = true;
			
      explanation.Text = "You can move the device around the analyzer by pressing and dragging it onto the area of another slot whether it's occupied or not.";
		}
		private void explainOccupiedTap(){
    	arrowImage.Hidden = false;
			arrowButton.Hidden = false;

    	arrowButton.Frame = new CGRect(.368 * parentView.Bounds.Width,.19 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
      arrowImage.Frame = new CGRect(.368 * parentView.Bounds.Width,.21 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);				
      explanation.Text = "Pressing an existing device will display a popup with more information.";
		}
		private void explainDevicePopup(){
    	arrowImage.Hidden = true;
			arrowButton.Hidden = true;
			
			explanation.Text = "This popup lets you see the device's information in a larger context and with additional information such as battery level and connection status.";
		}
		private void explainActionPress(){
    	arrowImage.Hidden = false;
			arrowButton.Hidden = false;
			
			arrowButton.Frame = new CGRect(.63 * parentView.Bounds.Width,.36 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
      arrowImage.Frame = new CGRect(.63 * parentView.Bounds.Width,.38 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);	
      explanation.Text = "Press the 'Actions' button to display more options that will be specific to the device.";
		}
		private void explainDeviceActions(){
    	arrowImage.Hidden = true;
			arrowButton.Hidden = true;
			
			explanation.Text = "From here you can setup alarms to alert you based on the device's readings, rename it to something easier to manage, or remove it from the analyzer completely.";
		}
		private void explainLowHighDrag(){
			
			explanation.Text = "You can also drag a device to the 'Low' or 'High' areas at the bottom of the analyzer to have additional information displayed as the device readings change.";				
		}
		private void explainLowHigh(){
    	arrowImage.Hidden = true;
			arrowButton.Hidden = true;
			
			explanation.Text = "The device you add will be color coded to the low or high area so you can keep track of what is associated.";
		}
		private void explainLowHighTap(){
    	arrowImage.Hidden = false;
			arrowButton.Hidden = false;
			
			arrowButton.Frame = new CGRect(.3 * parentView.Bounds.Width,.46 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
      arrowImage.Frame = new CGRect(.3 * parentView.Bounds.Width,.48 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);
			explanation.Text = "Pressing the low or high area with a device associated will display a popup with more actions.";
		}
		private void explainLowHighOptions(){
    	arrowImage.Hidden = true;
			arrowButton.Hidden = true;
	
			explanation.Text = "You can set an alarm to alert you for device readings, add a subview to monitor additional information, or remove the sensor from the low or high area.";
		}
		private void explainLowHighSubviews(){			
			explanation.Text = "You can hold onto a specific measurement form your device, keep track of the maximum and minimum measurements recieved, see the measurement in an alternate unit, monitor the rate of change, see the superheat or subcool, and view the pressure or temperature.";
		}
		private void explainSubviewArrangement(){
    	arrowImage.Hidden = true;
			arrowButton.Hidden = true;
			
			explanation.Text = "Subviews will be displayed under the low or high section and can be scrolled to view all added subviews.";
		}
	}
	/// <summary>
	/// Walkthrough methods for ptchart
	/// </summary>
	public class PTChartWalkthrough : WalkthroughProperties{
	
		public PTChartWalkthrough(UIView pView, UILabel textLabel,UIImageView screenshotImage, UIButton nextButton) {
      currentStep = 1;
      parentView = pView;
      explanation = textLabel;
      screenshot = screenshotImage;
      arrowButton = new UIButton(new CGRect(.16 * parentView.Bounds.Width,.07 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height));
      arrowImage = new UIImageView(new CGRect(.16 * parentView.Bounds.Width,.09 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height));
      arrowImage.Image = UIImage.FromBundle("walkthrough_arrow");
      
      arrowButton.TouchDown += (sender, e) => {
				nextButton.SendActionForControlEvents(UIControlEvent.TouchDown);
			};
			arrowButton.TouchUpOutside += (sender, e) => {
				nextButton.SendActionForControlEvents(UIControlEvent.TouchUpOutside);
			};
      arrowButton.TouchUpInside += (sender, e) => {
				nextButton.SendActionForControlEvents(UIControlEvent.TouchUpInside);
			};

			pView.AddSubview(arrowButton);
			pView.BringSubviewToFront(arrowButton);
			pView.AddSubview(arrowImage);
			pView.BringSubviewToFront(arrowImage);	
		}
		public override void GoForward(){ 
			currentStep++;
			switch(currentStep){
				case 1:
					screenshot.Image = UIImage.FromBundle("PTChart1");
					explainMenuDrawer();
					break;
				case 2:
					explainHelpButton();
					break;
				case 3:
					explainFluidSelection();
					break;
				case 4:
					screenshot.Image = UIImage.FromBundle("PTChart5");
					explainFluidFavorites();
					break;
				case 5:
					explainLibraryButton();
					break;
				case 6:
					screenshot.Image = UIImage.FromBundle("PTChart6");
					explainFluidLibrary();
					break;
				case 7:
					explainReturnToPTChart();
					break;
				case 8:
					screenshot.Image = UIImage.FromBundle("PTChart1");
					explainChangeToBubble();
					break;
				case 9:
					screenshot.Image = UIImage.FromBundle("PTChart2");
					explainBubbleChange();
					break;
				case 10:
					screenshot.Image = UIImage.FromBundle("PTChart1");
					explainAddDeviceButton();
					break;
				case 11:
					screenshot.Image = UIImage.FromBundle("PTChart4");
					explainDeviceImage();
					break;
				case 12:
					explainRemoveDevice();
					break;
				case 13:
					screenshot.Image = UIImage.FromBundle("PTChart1");
					explainChangeUnit();
					break;
				case 14:
					screenshot.Image = UIImage.FromBundle("PTChart7");
					explainUnitPopup();
					break;
				default:
					currentStep = 1;
					screenshot.Image = UIImage.FromBundle("PTChart1");
					explainMenuDrawer();
					break;
			}
		}
		public override void GoBackward(){
			currentStep--;
			switch(currentStep){
				case 1:
					explainMenuDrawer();
					break;
				case 2:
					explainHelpButton();
					break;
				case 3:
					screenshot.Image = UIImage.FromBundle("PTChart1");
					explainFluidSelection();
					break;
				case 4:
					explainFluidFavorites();
					break;
				case 5:
					screenshot.Image = UIImage.FromBundle("PTChart5");
					explainLibraryButton();
					break;
				case 6:
					explainFluidLibrary();
					break;
				case 7:
					screenshot.Image = UIImage.FromBundle("PTChart6");
					explainReturnToPTChart();
					break;
				case 8:
					screenshot.Image = UIImage.FromBundle("PTChart1");
					explainChangeToBubble();
					break;
				case 9:
					screenshot.Image = UIImage.FromBundle("PTChart2");
					explainBubbleChange();
					break;
				case 10:
					screenshot.Image = UIImage.FromBundle("PTChart1");
					explainAddDeviceButton();
					break;
				case 11:
					explainDeviceImage();
					break;
				case 12:
					screenshot.Image = UIImage.FromBundle("PTChart4");
					explainRemoveDevice();
					break;
				case 13:
					screenshot.Image = UIImage.FromBundle("PTChart1");
					explainChangeUnit();
					break;
				case 14:
					screenshot.Image = UIImage.FromBundle("PTChart7");
					explainUnitPopup();
					break;
				default:
					currentStep = 14;
					screenshot.Image = UIImage.FromBundle("PTChart7");
					explainUnitPopup();
					break;
			}
		}
		public void explainMenuDrawer(){
    	arrowImage.Hidden = false;
			arrowButton.Hidden = false;
			 
      arrowButton.Frame = new CGRect(.16 * parentView.Bounds.Width,.07 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
      arrowImage.Frame = new CGRect(.16 * parentView.Bounds.Width,.09 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);
      explanation.Text = "This is the menu button and pressing it will allow you to navigate through the app.";
		}
		private void explainHelpButton(){
    	arrowButton.Frame = new CGRect(.795 * parentView.Bounds.Width,.07 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
      arrowImage.Frame = new CGRect(.795 * parentView.Bounds.Width,.09 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);
      explanation.Text = "Pressing here opens a popup with a quick tip about Dew and Bubble";
		}
		private void explainFluidSelection(){
    	arrowImage.Hidden = false;
			arrowButton.Hidden = false;
			
      arrowButton.Frame = new CGRect(.18 * parentView.Bounds.Width,.1 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
      arrowImage.Frame = new CGRect(.18 * parentView.Bounds.Width,.12 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);
      explanation.Text = "Pressing here allows you to select a fluid to work with by opening the fluid list.";
		}
		private void explainFluidFavorites(){
    	arrowImage.Hidden = true;
			arrowButton.Hidden = true;
			
			explanation.Text = "You are first brought to a list of fluids that you have bookmarked for quick access.";
		}
		private void explainLibraryButton(){
    	arrowImage.Hidden = false;
			arrowButton.Hidden = false;
			
			explanation.Text = "The full list of supported fluids can be accessed by pressing the top tab marked 'Library'.";
		}
		private void explainFluidLibrary(){
    	arrowImage.Hidden = true;
			arrowButton.Hidden = true;
			
			explanation.Text = "From here you can scroll through the fluids and bookmark them for quick reference as well as choose them as your active fluid.";
		}
		private void explainReturnToPTChart(){
    	arrowImage.Hidden = false;
			arrowButton.Hidden = false;
			
			explanation.Text = "Pressing the top left button marked 'P/T Calculator will take you back to the main area that has been updated based on your choices.";
		}
		private void explainChangeToBubble(){
			explanation.Text = "For mixed fluids, you can switch between the dew and bubble values for your measurements.";
		}
		private void explainBubbleChange(){
    	arrowImage.Hidden = true;
			arrowButton.Hidden = true;
			
			explanation.Text = "This will update the pressure and temperature you see to match your selection.";	
		}
		private void explainAddDeviceButton(){
			explanation.Text = "As well as entering values manually, you can press here to find and connect to a device to use it's measurements.";
		}
		private void explainDeviceImage(){
			explanation.Text = "Once a device is added, you will see the add button replaced with an image of the device and can no longer make manual adjustments until it is removed.";
		}
		private void explainRemoveDevice(){
			explanation.Text = "Pressing the device image and holding will remove the association and allow for manual entries once again.";
		}
		private void explainChangeUnit(){
			explanation.Text = "Pressing the unit button for any non device associated value will allow you to change what unit of measurement your values are displayed in.";
		}
		private void explainUnitPopup(){
			explanation.Text = "Based on temperature or pressure, you will see a popup listing the available choices.";
		}
	}
	/// <summary>
	/// Walkthrough methods for Superheat/Subcool
	/// </summary>
	public class SuperheatSubcoolWalkthrough : WalkthroughProperties{
	
		public SuperheatSubcoolWalkthrough(UIView pView, UILabel textLabel,UIImageView screenshotImage, UIButton nextButton) {
      currentStep = 1;
      parentView = pView;
      explanation = textLabel;
      screenshot = screenshotImage;
      arrowButton = new UIButton(new CGRect(.16 * parentView.Bounds.Width,.06 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height));
      arrowImage = new UIImageView(new CGRect(.16 * parentView.Bounds.Width,.08 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height));
      arrowImage.Image = UIImage.FromBundle("walkthrough_arrow");
      
      arrowButton.TouchDown += (sender, e) => {
				nextButton.SendActionForControlEvents(UIControlEvent.TouchDown);
			};
			arrowButton.TouchUpOutside += (sender, e) => {
				nextButton.SendActionForControlEvents(UIControlEvent.TouchUpOutside);
			};
      arrowButton.TouchUpInside += (sender, e) => {
				nextButton.SendActionForControlEvents(UIControlEvent.TouchUpInside);
			};

			pView.AddSubview(arrowButton);
			pView.BringSubviewToFront(arrowButton);
			pView.AddSubview(arrowImage);
			pView.BringSubviewToFront(arrowImage);	
		}
		
		public override void GoForward(){
			currentStep++;
			Console.WriteLine("Moving forward to step " + currentStep);
			switch(currentStep){
				case 1:
					screenshot.Image = UIImage.FromBundle("SHSC1");
					explainMenuDrawer();
					break;
				case 2:
					explainHelpButton();
					break;
				case 3:
					explainFluidSelection();
					break;
				case 4:
					explainFluidFavorites();
					break;
				case 5:
				  explainFluidLibrary();
					break;
				case 6:
					explainReturnToSHSC();
					break;
				case 7:
					explainChangeToBubble();
					break;
				case 8:
					explainBubbleChange();
					break;
				case 9:
					explainAddDeviceButton();
					break;
				case 10:
					explainDeviceImage();
					break;
				case 11:
					explainRemoveDevice();
					break;
				case 12:
					explainChangeUnit();
					break;
				case 13:
					explainUnitPopup();
					break;
				default:
					currentStep = 1;
					screenshot.Image = UIImage.FromBundle("SHSC1");
					explainMenuDrawer();					
					break;
			}
		}
		public override void GoBackward(){
			currentStep--;
			Console.WriteLine("Moving backward to step " + currentStep);
			switch(currentStep){
				case 1:
					screenshot.Image = UIImage.FromBundle("SHSC1");
					explainMenuDrawer();
					break;
				case 2:
					explainHelpButton();
					break;
				case 3:
					explainFluidSelection();
					break;
				case 4:
					explainFluidFavorites();
					break;
				case 5:
				  explainFluidLibrary();
					break;
				case 6:
					explainReturnToSHSC();
					break;
				case 7:
					explainChangeToBubble();
					break;
				case 8:
					explainBubbleChange();
					break;
				case 9:
					explainAddDeviceButton();
					break;
				case 10:
					explainDeviceImage();
					break;
				case 11:
					explainRemoveDevice();
					break;
				case 12:
					explainChangeUnit();
					break;
				case 13:
					explainUnitPopup();
					break;
				default:
					currentStep = 13;
					screenshot.Image = UIImage.FromBundle("SHSC4");
					explainUnitPopup();
					break;
			}
		}
		private void explainMenuDrawer(){
			arrowImage.Hidden = false;
			arrowButton.Hidden = false;

			explanation.Text = "Pressing this button will open the menu to navigate through the app";			
		}
		private void explainHelpButton(){
			explanation.Text = "Pressing here opens a popup with a quick tip about Dew and Bubble";
		}
		private void explainFluidSelection(){
			explanation.Text = "You can change the fluid you're working with by pressing here to open up the fluid library";
		}
		private void explainFluidFavorites(){
			explanation.Text =  "You are first brought to a list of fluids that you have bookmarked for quick access.";
		}
		private void explainFluidLibrary(){
    	arrowImage.Hidden = true;
			arrowButton.Hidden = true;
			
			explanation.Text = "From here you can scroll through the fluids and bookmark them for quick reference as well as choose them as your active fluid.";
		}
		private void explainReturnToSHSC(){
    	arrowImage.Hidden = false;
			arrowButton.Hidden = false;
			
			explanation.Text = "Pressing the top left button marked 'Superheat/Subcool' will take you back to the main area that has been updated based on your fluid choice.";
		}
		private void explainChangeToBubble(){
			explanation.Text = "For mixed fluids, you can switch between the dew and bubble values for your measurements.";
		}
		private void explainBubbleChange(){
    	arrowImage.Hidden = true;
			arrowButton.Hidden = true;
			
			explanation.Text = "This will update the pressure and temperature you see to match your selection.";	
		}
		private void explainAddDeviceButton(){
			explanation.Text = "As well as entering values manually, you can press here to find and connect to a device to use it's measurements.";
		}
		private void explainDeviceImage(){
			explanation.Text = "Once a device is added, you will see the add button replaced with an image of the device and can no longer make manual adjustments until it is removed.";
		}
		private void explainRemoveDevice(){
			explanation.Text = "Pressing the device image and holding will remove the association and allow for manual entries once again.";
		}
		private void explainChangeUnit(){
			explanation.Text = "Pressing the unit button for any non device associated value will allow you to change what unit of measurement your values are displayed in.";
		}
		private void explainUnitPopup(){
			explanation.Text = "Based on temperature or pressure, you will see a popup listing the available choices.";
		}
	}
	/// <summary>
	/// Walkthrough methods for calibration certificates
	/// </summary>
	public class CalibrationCertWalkthrough : WalkthroughProperties{

		public CalibrationCertWalkthrough(UIView pView, UILabel textLabel,UIImageView screenshotImage, UIButton nextButton) {
      currentStep = 1;
      parentView = pView;
      explanation = textLabel;
      screenshot = screenshotImage;
      arrowButton = new UIButton(new CGRect(.16 * parentView.Bounds.Width,.06 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height));
      arrowImage = new UIImageView(new CGRect(.16 * parentView.Bounds.Width,.08 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height));
      arrowImage.Image = UIImage.FromBundle("walkthrough_arrow");
      
      arrowButton.TouchDown += (sender, e) => {
				nextButton.SendActionForControlEvents(UIControlEvent.TouchDown);
			};
			arrowButton.TouchUpOutside += (sender, e) => {
				nextButton.SendActionForControlEvents(UIControlEvent.TouchUpOutside);
			};
      arrowButton.TouchUpInside += (sender, e) => {
				nextButton.SendActionForControlEvents(UIControlEvent.TouchUpInside);
			};

			pView.AddSubview(arrowButton);
			pView.BringSubviewToFront(arrowButton);
			pView.AddSubview(arrowImage);
			pView.BringSubviewToFront(arrowImage);	
		}
		public override void GoForward(){
			currentStep++;
			switch(currentStep){
				case 1:
					explainBackButton();
					break;
				case 2:
					explainDownloadButton();
					break;
				case 3:
					explainDownloadPopup();
					break;
				case 4:
					explainCertificateList();
					break;
				default:
					currentStep = 1;
					screenshot.Image = UIImage.FromBundle("Cert1");
					//explainBackButton();					
					break;
			}
		}
		public override void GoBackward(){
			currentStep--;
			switch(currentStep){
				case 1:
					explainBackButton();
					break;
				case 2:
					explainDownloadButton();
					break;
				case 3:
					explainDownloadPopup();
					break;
				case 4:
					explainCertificateList();
					break;
				default:
					currentStep = 4;
					explainCertificateList();
					break;
			}
		}
		private void explainBackButton(){}
		private void explainDownloadButton(){}
		private void explainDownloadPopup(){}
		private void explainCertificateList(){}
		
	}
	/// <summary>
	/// Walkthrough methods for screenshots
	/// </summary>
	public class ScreenshotArchiveWalkthrough : WalkthroughProperties{

		public ScreenshotArchiveWalkthrough(UIView pView, UILabel textLabel,UIImageView screenshotImage, UIButton nextButton) {
      currentStep = 1;
      parentView = pView;
      explanation = textLabel;
      screenshot = screenshotImage;
      arrowButton = new UIButton(new CGRect(.16 * parentView.Bounds.Width,.06 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height));
      arrowImage = new UIImageView(new CGRect(.16 * parentView.Bounds.Width,.08 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height));
      arrowImage.Image = UIImage.FromBundle("walkthrough_arrow");
      
      arrowButton.TouchDown += (sender, e) => {
				nextButton.SendActionForControlEvents(UIControlEvent.TouchDown);
			};
			arrowButton.TouchUpOutside += (sender, e) => {
				nextButton.SendActionForControlEvents(UIControlEvent.TouchUpOutside);
			};
      arrowButton.TouchUpInside += (sender, e) => {
				nextButton.SendActionForControlEvents(UIControlEvent.TouchUpInside);
			};

			pView.AddSubview(arrowButton);
			pView.BringSubviewToFront(arrowButton);
			pView.AddSubview(arrowImage);
			pView.BringSubviewToFront(arrowImage);
		}
		
		public override void GoForward(){
			currentStep++;
			switch(currentStep){
				case 1:
					explainBackButton();
					break;
				case 2:
					explainScreenshotButton();
					break;
				case 3:
					explainScreenshotLayout();
					break;
				case 4:
					explainScreenshotList();
					break;
				default:
					currentStep = 1;
					screenshot.Image = UIImage.FromBundle("Archives1");
					explainBackButton();					
					break;
			}		

		}
		public override void GoBackward(){
			currentStep--;
			switch(currentStep){
				case 1:
					explainBackButton();
					break;
				case 2:
					explainScreenshotButton();
					break;
				case 3:
					explainScreenshotLayout();
					break;
				case 4:
					explainScreenshotList();
					break;
				default:
					currentStep = 4;
					explainScreenshotList();
					break;
			}
		}
		private void explainBackButton(){}
		private void explainScreenshotButton(){}
		private void explainScreenshotLayout(){}
		private void explainScreenshotList(){}
	}
	/// <summary>
	/// Walkthrough methods for app settings
	/// </summary>
	public class SettingsWalkthrough : WalkthroughProperties{
		public override void GoForward(){
			currentStep++;
			switch(currentStep){
				case 1:
				
					break;
				case 2:
				
					break;
				case 3:
				
					break;
				case 4:
				
					break;
				case 5:
				
					break;
				case 6:
				
					break;
				case 7:
				
					break;
				case 8:
				
					break;
				default:
				
					break;
			}
		}
		public override void GoBackward(){
			currentStep--;
			switch(currentStep){
				case 1:
				
					break;
				case 2:
				
					break;
				case 3:
				
					break;
				case 4:
				
					break;
				case 5:
				
					break;
				case 6:
				
					break;
				case 7:
				
					break;
				case 8:
				
					break;
				default:
				
					break;
			}
		}
	}
}

