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
    public int maxStep {get;set;}
    public UIView parentView {get;set;}
    public UILabel explanation {get;set;}
    public UIImageView arrowImage {get;set;}
    public UIButton arrowButton {get;set;}
    public UIImageView screenshot {get;set;}
    
    public abstract void GoForward();
    public abstract void GoBackward();
	}
		
	public class WorkBenchWalkthrough : WalkthroughProperties{
		public WorkBenchWalkthrough(UIView pView, UILabel textLabel,UIImageView screenshotImage, UIButton nextButton) {
      currentStep = 1;
      maxStep = 20;
      parentView = pView;
      explanation = textLabel;
      screenshot = screenshotImage;
      arrowButton = new UIButton(new CGRect(.16 * parentView.Bounds.Width,.11 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height));
      arrowImage = new UIImageView(new CGRect(.16 * parentView.Bounds.Width,.13 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height));
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
			
      arrowButton.Frame = new CGRect(.16 * parentView.Bounds.Width,.11 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
      arrowImage.Frame = new CGRect(.16 * parentView.Bounds.Width,.13 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);
      explanation.Text = "This is the app menu button. You can press this to access any other section of the app.";
    }
    private void explainRecordButton(){
    	arrowButton.Frame = new CGRect(.755 * parentView.Bounds.Width,.11 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
      arrowImage.Frame = new CGRect(.755 * parentView.Bounds.Width,.13 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);
      explanation.Text = "This is the record button for starting and ending a logging session of your connected devices' readings.";
    }
    private void explainScreenshotButton(){
      arrowButton.Frame = new CGRect(.79 * parentView.Bounds.Width,.11 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
      arrowImage.Frame = new CGRect(.79 * parentView.Bounds.Width,.13 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);
      explanation.Text = "This is the screenshot button that captures your device's screen to allow for keeping records of important moments.";
    }
    private void explainAddViewerButton(){
    	arrowImage.Hidden = false;
    	arrowButton.Hidden = false;
    	
      arrowButton.Frame = new CGRect(.475 * parentView.Bounds.Width,.16 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
      arrowImage.Frame = new CGRect(.475 * parentView.Bounds.Width,.18 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);
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
			
			arrowButton.Frame = new CGRect(.16 * parentView.Bounds.Width,.11 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
			arrowImage.Frame = new CGRect(.16 * parentView.Bounds.Width,.13 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);
			 explanation.Text = "Pressing this button will take you back to the previous screen.";		
		}
		private void explainDMScanButton(){
			arrowButton.Hidden = false;
			arrowImage.Hidden = false;
		
      arrowButton.Frame = new CGRect(.79 * parentView.Bounds.Width,.11 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
      arrowImage.Frame = new CGRect(.79 * parentView.Bounds.Width,.13 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);
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
			
			arrowButton.Frame = new CGRect(.475 * parentView.Bounds.Width,.16 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
      arrowImage.Frame = new CGRect(.475 * parentView.Bounds.Width,.18 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);      
			explanation.Text = "Press here to see a dropdown displaying more info about the device";
		}
		private void explainDMAddDevice(){
			arrowImage.Hidden = false;
			arrowButton.Hidden = false;
			
			arrowButton.Frame = new CGRect(.785 * parentView.Bounds.Width,.2 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
      arrowImage.Frame = new CGRect(.785 * parentView.Bounds.Width,.22 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height); 
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

			arrowButton.Frame = new CGRect(.475 * parentView.Bounds.Width,.18 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
      arrowImage.Frame = new CGRect(.475 * parentView.Bounds.Width,.2 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);      
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
	
		public class AnalyzerWalkthrough : WalkthroughProperties{
			public AnalyzerWalkthrough(UIView pView, UILabel textLabel,UIImageView screenshotImage, UIButton nextButton) {
	      currentStep = 1;
	      maxStep = 20;
	      parentView = pView;
	      explanation = textLabel;
	      screenshot = screenshotImage;
	      arrowButton = new UIButton(new CGRect(.16 * parentView.Bounds.Width,.11 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height));
	      arrowImage = new UIImageView(new CGRect(.16 * parentView.Bounds.Width,.13 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height));
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
					default:
						currentStep = 11;
						screenshot.Image = UIImage.FromBundle("Analyzer4");
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
					default:
						currentStep = 13;
						screenshot.Image = UIImage.FromBundle("Analyzer7");
						explainDeviceActions();
						break;
			 	}
			}
			private void explainMenuDrawer(){
	    	arrowImage.Hidden = false;
				arrowButton.Hidden = false;
				
	      arrowButton.Frame = new CGRect(.16 * parentView.Bounds.Width,.11 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
	      arrowImage.Frame = new CGRect(.16 * parentView.Bounds.Width,.13 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);
	      explanation.Text = "This is the app menu button. You can press this to access any other section of the app.";
	    }
		  private void explainRecordButton(){
	    	arrowButton.Frame = new CGRect(.795 * parentView.Bounds.Width,.11 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
	      arrowImage.Frame = new CGRect(.795 * parentView.Bounds.Width,.13 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);
	      explanation.Text = "This is the record button for starting and ending a logging session of your connected devices' readings.";
	    }
	    private void explainEmptyTap(){
	    	arrowImage.Hidden = false;
				arrowButton.Hidden = false;
				
	    	arrowButton.Frame = new CGRect(.22 * parentView.Bounds.Width,.21 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
	      arrowImage.Frame = new CGRect(.22 * parentView.Bounds.Width,.23 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);
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

	    	arrowButton.Frame = new CGRect(.368 * parentView.Bounds.Width,.21 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
	      arrowImage.Frame = new CGRect(.368 * parentView.Bounds.Width,.23 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);				
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
				
				arrowButton.Frame = new CGRect(.22 * parentView.Bounds.Width,.5 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
	      arrowImage.Frame = new CGRect(.22 * parentView.Bounds.Width,.5 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);
				explanation.Text = "Pressing the low or high area with a device associated will display a popup with more actions.";
			}
	}
}

