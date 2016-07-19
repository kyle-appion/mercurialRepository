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
	public class IntroductoryWalkthrough : WalkthroughProperties {
		public IntroductoryWalkthrough(UIView pView, UILabel textLabel,UIImageView screenshotImage, UIButton nextButton) {
	      currentStep = 1;
	      parentView = pView;	
	      explanation = textLabel;
	      screenshot = screenshotImage;
      
      		if(parentView.Bounds.Height <= 568){
				arrowButton = new UIButton(new CGRect(.293 * parentView.Bounds.Width,60,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height));
      			arrowImage = new UIImageView(new CGRect(.293 * parentView.Bounds.Width,65,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height));
			}
			else if (parentView.Bounds.Height <= 700){
				arrowButton = new UIButton(new CGRect(.293 * parentView.Bounds.Width,60,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height));
      			arrowImage = new UIImageView(new CGRect(.293 * parentView.Bounds.Width,65,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height));				
			}
			else {
				arrowButton = new UIButton(new CGRect(.293 * parentView.Bounds.Width,65,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height));
      			arrowImage = new UIImageView(new CGRect(.293 * parentView.Bounds.Width,70,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height));				
			}
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
	          screenshot.Image = UIImage.FromBundle("Intro1");
			  explainMenu();
	          break;
	        case 2:
			  explainRecordButton();
	          break;
	        case 3:
			  explainScreenshotButton();
	          break;
	        case 4:
	          explainWorkbenchAdd();  				
	          break;
	        case 5:		
				screenshot.Image = UIImage.FromBundle("Intro2");			
				explainDMSections();
				break;
			case 6:
				explainBluetoothButtons();
				break;
	      	case 7:      		
	 			explainAddDeviceButton();
	        	break;
	      	case 8:
	      		screenshot.Image = UIImage.FromBundle("Intro3");
	  			explainWorkbenchDeviceOptions();
	        	break;
	      	case 9:
				screenshot.Image = UIImage.FromBundle("Intro4");
	        	break;
	        default:
	          currentStep = 1;
	          screenshot.Image = UIImage.FromBundle("Intro1"); 
			  explainMenu();        
	          break;
	      }
		} 
		public override void GoBackward(){
        	currentStep--;
	      	switch (currentStep) { 
	        	case 1:
				explainMenu();
	          	break;
	        case 2:
				explainRecordButton();
	          	break;
	        case 3:
				explainScreenshotButton();
	          	break;
	        case 4:
	        	screenshot.Image = UIImage.FromBundle("Intro1");
	          	explainWorkbenchAdd();  				
	          	break;
	        case 5:					
				explainDMSections();
				break;
			case 6:
				explainBluetoothButtons();
				break;
	      	case 7:
	      		screenshot.Image = UIImage.FromBundle("Intro2");
	 			explainAddDeviceButton();
	        	break;
	      	case 8:
	      		screenshot.Image = UIImage.FromBundle("Intro3");
	  			explainWorkbenchDeviceOptions();
	        	break;
	      	case 9:
	      		arrowImage.Hidden = true;
	      		arrowButton.Hidden = true;
				screenshot.Image = UIImage.FromBundle("Intro4");
	        	break;
	        default:
	          arrowImage.Hidden = true;
	      	  arrowButton.Hidden = true;
	          currentStep = 9;
	          screenshot.Image = UIImage.FromBundle("Intro4");          
	          break;
	      }
		}

		private void explainMenu(){
			arrowImage.Hidden = false;
			arrowButton.Hidden = false;
			
			explanation.Text = "The menu button will allow for navigation through the app.";
      		if(parentView.Bounds.Height <= 568){				 
				arrowButton.Frame = new CGRect(.293 * parentView.Bounds.Width,60,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
      			arrowImage.Frame = new CGRect(.293 * parentView.Bounds.Width,65,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);
			}
			else if (parentView.Bounds.Height <= 700){ 
				arrowButton.Frame = new CGRect(.293 * parentView.Bounds.Width,60,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
      			arrowImage.Frame = new CGRect(.293 * parentView.Bounds.Width,65,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);				
			}
			else { 
				arrowButton.Frame = new CGRect(.293 * parentView.Bounds.Width,65,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
      			arrowImage.Frame = new CGRect(.293 * parentView.Bounds.Width,70,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);				
			}		
		}
		
		private void explainRecordButton(){
			explanation.Text = "Pressing this button will start or stop a recording session(data logging)";
      		if(parentView.Bounds.Height <= 568){				 
				arrowButton.Frame = new CGRect(.755 * parentView.Bounds.Width,60,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
      			arrowImage.Frame = new CGRect(.755 * parentView.Bounds.Width,65,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);
			}
			else if (parentView.Bounds.Height <= 700){ 
				arrowButton.Frame = new CGRect(.755 * parentView.Bounds.Width,60,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
      			arrowImage.Frame = new CGRect(.755 * parentView.Bounds.Width,65,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);				
			}
			else { 
				arrowButton.Frame = new CGRect(.755 * parentView.Bounds.Width,65,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
      			arrowImage.Frame = new CGRect(.755 * parentView.Bounds.Width,70,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);				
			}		
		}
		
		private void explainScreenshotButton(){
			explanation.Text = "You can take a screenshot of the workbench by pressing this button.";
      		if(parentView.Bounds.Height <= 568){				 
				arrowButton.Frame = new CGRect(.793 * parentView.Bounds.Width,60,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
      			arrowImage.Frame = new CGRect(.793 * parentView.Bounds.Width,65,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);
			}
			else if (parentView.Bounds.Height <= 700){ 
				arrowButton.Frame = new CGRect(.793 * parentView.Bounds.Width,60,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
      			arrowImage.Frame = new CGRect(.793 * parentView.Bounds.Width,65,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);				
			}
			else { 
				arrowButton.Frame = new CGRect(.793 * parentView.Bounds.Width,65,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
      			arrowImage.Frame = new CGRect(.793 * parentView.Bounds.Width,70,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);				
			}	
		}
		
		private void explainWorkbenchAdd(){
			arrowImage.Hidden = false;
			arrowButton.Hidden = false;
			
			explanation.Text = "Pressing here will open the device manager where you can choose an available device to add to the workbench.";
      		if(parentView.Bounds.Height <= 568){				 
				arrowButton.Frame = new CGRect(.5 * parentView.Bounds.Width,.335 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
      			arrowImage.Frame = new CGRect(.5 * parentView.Bounds.Width,.355 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);
			}
			else if (parentView.Bounds.Height <= 700){ 
				arrowButton.Frame = new CGRect(.5 * parentView.Bounds.Width,.33 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
      			arrowImage.Frame = new CGRect(.5 * parentView.Bounds.Width,.35 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);				
			}
			else { 
				arrowButton.Frame = new CGRect(.5 * parentView.Bounds.Width,.30 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
      			arrowImage.Frame = new CGRect(.5 * parentView.Bounds.Width,.32 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);				
			}		
		}		
		
		private void explainDMSections(){ 
			arrowImage.Hidden = true;
			arrowButton.Hidden = true;
			
			explanation.Text = "The available devices will be shown and separated based on if they are connected or not.";
		}
		
		private void explainBluetoothButtons(){
			arrowImage.Hidden = false;
			arrowButton.Hidden = false;
			
      		if(parentView.Bounds.Height <= 568){				 
				arrowButton.Frame = new CGRect(.785 * parentView.Bounds.Width,.145 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
      			arrowImage.Frame = new CGRect(.785 * parentView.Bounds.Width,.165 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);
			}
			else if (parentView.Bounds.Height <= 700){ 
				arrowButton.Frame = new CGRect(.785 * parentView.Bounds.Width,.135 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
      			arrowImage.Frame = new CGRect(.785 * parentView.Bounds.Width,.155 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);				
			}
			else { 
				arrowButton.Frame = new CGRect(.785 * parentView.Bounds.Width,.11 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
      			arrowImage.Frame = new CGRect(.785 * parentView.Bounds.Width,.13 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);				
			}			
			explanation.Text = "These buttons let you connect and disconnect from a device.";
		}
		
		private void explainAddDeviceButton(){
			arrowImage.Hidden = false;
			arrowButton.Hidden = false;
			
      		if(parentView.Bounds.Height <= 568){				 
				arrowButton.Frame = new CGRect(.785 * parentView.Bounds.Width,.18 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
      			arrowImage.Frame = new CGRect(.785 * parentView.Bounds.Width,.2 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);
			}
			else if (parentView.Bounds.Height <= 700){ 
				arrowButton.Frame = new CGRect(.785 * parentView.Bounds.Width,.16 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
      			arrowImage.Frame = new CGRect(.785 * parentView.Bounds.Width,.18 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);				
			}
			else { 
				arrowButton.Frame = new CGRect(.785 * parentView.Bounds.Width,.155 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.07 * parentView.Bounds.Height);
      			arrowImage.Frame = new CGRect(.785 * parentView.Bounds.Width,.155 * parentView.Bounds.Height,.05 * parentView.Bounds.Width,.05 * parentView.Bounds.Height);				
			}			
			explanation.Text = "You add the device to the workbench, analyzer, or other section by pressing this button";
		}
		
		private void explainWorkbenchDeviceOptions(){
			arrowImage.Hidden = true;
			arrowButton.Hidden = true;
			
			explanation.Text = "Pressing on an added device will display a popup that allows you to connect/disconnect, add a subview, set an alarm, or other options.";
		}
	}
}

