using System;
using System.IO;
using UIKit;
using Foundation;
using CoreGraphics;
using CoreLocation;

using ION.Core.App;
using System.Threading.Tasks;
using ION.Core.Location;
using ION.IOS.App;

namespace ION.IOS.ViewController.JobManager  {
  
  public class EditJobView {
    public UIView editView;
    public UILabel confirmLabel;
    public UILabel coordinateLabel;
    public UITextField jobName;
    public UITextField customerNumber;
    public UITextField dispatchNumber;
    public UITextField prodOrderNumber;
    public UITextField techName;
    public UITextField systemName;    
    public UITextField jobAddress;
		public UIActivityIndicatorView loadingCoordinates;
    
    public UIButton coordinateButton;
    public UIButton additionalInfo;
    IION ion;
    
    public CLGeocoder geoCoder;
    
    public bool expanded = false;
    public int jobID = 0;

    public EditJobView(UIView parentView,int frnJID) {
      ion = AppState.context;
      jobID = frnJID;
      geoCoder = new CLGeocoder();

      editView = new UIView(new CGRect(0,0,parentView.Bounds.Width,parentView.Bounds.Height));
      editView.AddGestureRecognizer(new UITapGestureRecognizer(() => {
        editView.EndEditing(true);
      }));

      confirmLabel = new UILabel(new CGRect(.25 * editView.Bounds.Width, 0,.5 * editView.Bounds.Width,.07 * (editView.Bounds.Height - 60)));
      confirmLabel.AdjustsFontSizeToFitWidth = true;
      confirmLabel.TextColor = UIColor.FromRGB(49, 111, 18);
      confirmLabel.TextAlignment = UITextAlignment.Center;
      confirmLabel.Font = UIFont.BoldSystemFontOfSize(20);
      confirmLabel.Text = Util.Strings.Job.SAVEDSUCCESS;
      confirmLabel.Hidden = true;     

      var holderName = "";
      var holderCustomer = "";
      var holderDispatch = "";      
      var holderPO = "";
      var holderTech = "";
      var holderSystem = "";
      var holderAddress = "";
      var holderLocation = "";

      if (!frnJID.Equals(0)) {
        var infoQuery = ion.database.Query<ION.Core.Database.JobRow>("SELECT * FROM JobRow WHERE JID = ?", frnJID);
        foreach (var job in infoQuery) {
          holderName = job.jobName;
          if (job.customerNumber != null) {
            holderCustomer = job.customerNumber;
          } 

          if (job.dispatchNumber != null) {
            holderDispatch = job.dispatchNumber;
          } 

          if (job.poNumber != null) {
            holderPO = job.poNumber;
          }
          if(job.techName != null){
						holderTech = job.techName;
					}
					if(job.systemType != null){
						holderSystem = job.systemType;
					}
					if(job.jobAddress != null){
						holderAddress = job.jobAddress;
					}
					if(job.jobLocation != null){
						holderLocation = job.jobLocation;
					}
        }
      }

      jobName = new FloatLabeledTextField(new CGRect(.1 * editView.Bounds.Width,.07 * (editView.Bounds.Height - 60),.8 * editView.Bounds.Width,.14 * (editView.Bounds.Height - 60))){
        Placeholder = Util.Strings.Job.JOBNAME+"("+ Util.Strings.REQUIRED+")",
        FloatingLabelFont = UIFont.BoldSystemFontOfSize(12),
        FloatingLabelTextColor = UIColor.Gray,
        FloatingLabelActiveTextColor = UIColor.Blue,
        TextAlignment = UITextAlignment.Center,
        AutocorrectionType = UITextAutocorrectionType.No,
        AutocapitalizationType = UITextAutocapitalizationType.None,
      };
      jobName.Layer.BorderWidth = 1f;
      jobName.ShouldReturn += (textField) => {
        textField.ResignFirstResponder();
        return true;
      };
      jobName.Text = holderName;

      customerNumber = new FloatLabeledTextField(new CGRect(.1 * editView.Bounds.Width,.21 * (editView.Bounds.Height - 60),.8 * editView.Bounds.Width,.14 * (editView.Bounds.Height - 60))){
        Placeholder = Util.Strings.Job.CUSTOMERNUMBER,
        FloatingLabelFont = UIFont.BoldSystemFontOfSize(12),
        FloatingLabelTextColor = UIColor.Gray,
        FloatingLabelActiveTextColor = UIColor.Blue,
        TextAlignment = UITextAlignment.Center,
        AutocorrectionType = UITextAutocorrectionType.No,
        AutocapitalizationType = UITextAutocapitalizationType.None,
      };
      customerNumber.Layer.BorderWidth = 1f;
      customerNumber.ShouldReturn += (textField) => {
        textField.ResignFirstResponder();
        return true;
      };
      customerNumber.Text = holderCustomer;

      dispatchNumber = new FloatLabeledTextField(new CGRect(.1 * editView.Bounds.Width,.35 * (editView.Bounds.Height - 60),.8 * editView.Bounds.Width,.14 * (editView.Bounds.Height - 60))){
        Placeholder = Util.Strings.Job.DISPATCHNUMBER,
        FloatingLabelFont = UIFont.BoldSystemFontOfSize(12),
        FloatingLabelTextColor = UIColor.Gray,
        FloatingLabelActiveTextColor = UIColor.Blue,
        TextAlignment = UITextAlignment.Center,
        AutocorrectionType = UITextAutocorrectionType.No,
        AutocapitalizationType = UITextAutocapitalizationType.None,
      };
      dispatchNumber.Layer.BorderWidth = 1f;
      //dispatchNumber.Layer.CornerRadius = 8;
      dispatchNumber.ShouldReturn += (textField) => {
        textField.ResignFirstResponder();
        return true;
      };
      dispatchNumber.Text = holderDispatch;

      prodOrderNumber = new FloatLabeledTextField(new CGRect(.1 * editView.Bounds.Width,.49 * (editView.Bounds.Height - 60),.8 * editView.Bounds.Width,.14 * (editView.Bounds.Height - 60))){
        Placeholder = Util.Strings.Job.POFULL,
        FloatingLabelFont = UIFont.BoldSystemFontOfSize(12),
        FloatingLabelTextColor = UIColor.Gray,
        FloatingLabelActiveTextColor = UIColor.Blue,
        TextAlignment = UITextAlignment.Center,
        AutocorrectionType = UITextAutocorrectionType.No,
        AutocapitalizationType = UITextAutocapitalizationType.None,
      };
      prodOrderNumber.Layer.BorderWidth = 1f;
      prodOrderNumber.ShouldReturn += (textField) => {
        textField.ResignFirstResponder();
        return true;
      };
      prodOrderNumber.Text = holderPO;
      
      additionalInfo = new UIButton(new CGRect(.1 * editView.Bounds.Width,.63 * (editView.Bounds.Height - 60),.8 * editView.Bounds.Width,.07 * (editView.Bounds.Height - 60)));
      additionalInfo.SetTitle("Additional Information",UIControlState.Normal);
      additionalInfo.SetTitleColor(UIColor.Blue,UIControlState.Normal);
      additionalInfo.TouchUpInside += showAdditionalInfo;
			additionalInfo.TouchDown += (sender, e) => {additionalInfo.SetTitleColor(UIColor.Black, UIControlState.Normal);};
			additionalInfo.TouchUpOutside += (sender, e) => {additionalInfo.SetTitleColor(UIColor.Blue, UIControlState.Normal);}; 
    
      techName = new FloatLabeledTextField(new CGRect(.1 * editView.Bounds.Width,.7 * (editView.Bounds.Height - 60),.8 * editView.Bounds.Width,.09 * (editView.Bounds.Height - 60))){
        Placeholder = "Tech Name",
        FloatingLabelFont = UIFont.BoldSystemFontOfSize(12),
        FloatingLabelTextColor = UIColor.Gray,
        FloatingLabelActiveTextColor = UIColor.Blue,
        TextAlignment = UITextAlignment.Center,
        AutocorrectionType = UITextAutocorrectionType.No,
        AutocapitalizationType = UITextAutocapitalizationType.None,
        Hidden = true,
			};
			techName.Layer.BorderWidth = 1f;
      techName.ShouldReturn += (textField) => {
        textField.ResignFirstResponder();
        return true;
      };
      techName.Text = holderTech;

      systemName = new FloatLabeledTextField(new CGRect(.1 * editView.Bounds.Width,.79 * (editView.Bounds.Height - 60),.8 * editView.Bounds.Width,.09 * (editView.Bounds.Height - 60))){
        Placeholder = "Type of System",
        FloatingLabelFont = UIFont.BoldSystemFontOfSize(12),
        FloatingLabelTextColor = UIColor.Gray,
        FloatingLabelActiveTextColor = UIColor.Blue,
        TextAlignment = UITextAlignment.Center,
        AutocorrectionType = UITextAutocorrectionType.No,
        AutocapitalizationType = UITextAutocapitalizationType.None,
        Hidden = true,
			};
			systemName.Layer.BorderWidth = 1f;
      systemName.ShouldReturn += (textField) => {
        textField.ResignFirstResponder();
        return true;
      };
      systemName.Text = holderSystem;
           
      jobAddress = new FloatLabeledTextField(new CGRect(.1 * editView.Bounds.Width, .88 * (editView.Bounds.Height - 60),.8 * editView.Bounds.Width,.09 * (editView.Bounds.Height - 60))){
				Placeholder = "Address(Empty Address Uses GPS)",
        FloatingLabelFont = UIFont.BoldSystemFontOfSize(12),
        FloatingLabelTextColor = UIColor.Gray,
        FloatingLabelActiveTextColor = UIColor.Blue,
        TextAlignment = UITextAlignment.Center,
        AutocorrectionType = UITextAutocorrectionType.No,
        AutocapitalizationType = UITextAutocapitalizationType.None,
        AdjustsFontSizeToFitWidth = true,
        Hidden = true,
			};
			jobAddress.Layer.BorderWidth = 1f;
			jobAddress.Text = holderAddress;
			
			coordinateButton = new UIButton(new CGRect(.25 * editView.Bounds.Width, .97 * (editView.Bounds.Height - 60),.5 * editView.Bounds.Width,.07 * (editView.Bounds.Height - 60)));
			coordinateButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);			
			coordinateButton.SetTitle("Get Coordinates",UIControlState.Normal);
			coordinateButton.SetTitleColor(UIColor.Black,UIControlState.Normal);
			coordinateButton.Layer.BorderWidth = 1f;
			coordinateButton.Layer.CornerRadius = 5f;
			coordinateButton.Hidden = true;
			coordinateButton.TouchDown += (sender, e) => {coordinateButton.BackgroundColor = UIColor.Blue;};
			coordinateButton.TouchUpOutside += (sender, e) => {coordinateButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);};
			coordinateButton.TouchUpInside += (sender, e) => {
				updateJobCoordinates(sender, e);
			};

			coordinateButton.TouchDown += (sender, e) => {coordinateButton.SetTitleColor(UIColor.Black, UIControlState.Normal);};
			coordinateButton.TouchUpOutside += (sender, e) => {coordinateButton.SetTitleColor(UIColor.Black, UIControlState.Normal);};
			
			coordinateLabel = new UILabel(new CGRect(.1 * editView.Bounds.Width, 1.05 * (editView.Bounds.Height - 60),.8 * editView.Bounds.Width,.07 * (editView.Bounds.Height - 60)));
			coordinateLabel.AdjustsFontSizeToFitWidth = true;
			coordinateLabel.Hidden = true;
			coordinateLabel.TextAlignment = UITextAlignment.Center;
			coordinateLabel.Text = holderLocation;
			
      editView.AddSubview(confirmLabel);
      editView.AddSubview(jobName);
      editView.AddSubview(customerNumber);
      editView.AddSubview(dispatchNumber);
      editView.AddSubview(prodOrderNumber);
      editView.AddSubview(additionalInfo);
      editView.AddSubview(techName);
      editView.AddSubview(systemName);
      editView.AddSubview(jobAddress);
      editView.AddSubview(coordinateButton);
      editView.AddSubview(coordinateLabel);
    }
		
		public void showAdditionalInfo(object sender, EventArgs e){
			additionalInfo.SetTitleColor(UIColor.Blue, UIControlState.Normal);
			if(expanded){
				techName.Hidden = true;
				systemName.Hidden = true;
				jobAddress.Hidden = true;
				coordinateButton.Hidden = true;
				coordinateLabel.Hidden = true;
				expanded = false;			
			} else {
				techName.Hidden = false;
				systemName.Hidden = false;
				jobAddress.Hidden = false;
				coordinateButton.Hidden = false;
				coordinateLabel.Hidden = false;
				expanded = true;
			}			
		}
		
		public async Task<bool> updateJobCoordinates(object sender, EventArgs e){
			coordinateButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
			await Task.Delay(TimeSpan.FromMilliseconds(2));			
			loadingCoordinates = new UIActivityIndicatorView(new CGRect(.1 * editView.Bounds.Width, 1.05 * (editView.Bounds.Height - 60),.8 * editView.Bounds.Width,.07 * (editView.Bounds.Height - 60)));
			loadingCoordinates.BackgroundColor = UIColor.Black;
			loadingCoordinates.Alpha = .8f;
			editView.AddSubview(loadingCoordinates);
			editView.BringSubviewToFront(loadingCoordinates);
			loadingCoordinates.StartAnimating();
			
			if(string.IsNullOrEmpty(jobAddress.Text)){
        var iosIon = ion as IosION;
        if(iosIon.settings._location.allowsGps){
					var latlong = ion.locationManager.lastKnownLocation.latitude.amount.ToString("0.000000") + "," + ion.locationManager.lastKnownLocation.longitude.amount.ToString("0.000000");
					coordinateButton.Enabled = false;
					coordinateLabel.Text = latlong;			
					
					var placemarks = await geoCoder.ReverseGeocodeLocationAsync(new CLLocation(ion.locationManager.lastKnownLocation.latitude.amount,ion.locationManager.lastKnownLocation.longitude.amount));
					var address = "";
			    foreach (var placemark in placemarks) {
	          address = placemark.Name + " " + placemark.Locality + ", " + placemark.AdministrativeArea + " " + placemark.PostalCode;
			    }
					loadingCoordinates.StopAnimating();
			    
					ion.database.Query<ION.Core.Database.JobRow>("UPDATE JobRow SET jobLocation = ?, jobAddress = ? WHERE JID = ?",latlong,address,jobID);
					jobAddress.Text = address;
								
					coordinateButton.Enabled = true;
					} else {
						loadingCoordinates.StopAnimating();
					
						var window = UIApplication.SharedApplication.KeyWindow;
						var rootVC = window.RootViewController as IONPrimaryScreenController;
						
						var alert = UIAlertController.Create ("GPS Disabled", "You must enable ION HVAC/R through your device settings to access your location if no address is entered.", UIAlertControllerStyle.Alert);
						alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
						rootVC.PresentViewController (alert, animated: true, completionHandler: null);
					}
					return true;
			} else {
				coordinateButton.Enabled = false;
				var placemarks = await geoCoder.GeocodeAddressAsync(jobAddress.Text);
				loadingCoordinates.StopAnimating();
				
				if(placemarks.Length == 0){
					coordinateLabel.Text = "Unable to retrieve coordinates";
				} else {
					var latlong = "";
					foreach(var placemark in placemarks){
						latlong = placemark.Location.Coordinate.Latitude + ","+placemark.Location.Coordinate.Longitude;
					}
					ion.database.Query<ION.Core.Database.JobRow>("UPDATE JobRow SET jobLocation = ?, jobAddress = ? WHERE JID = ?",latlong,jobAddress.Text,jobID);

					coordinateLabel.Text = latlong;
				}

				coordinateButton.Enabled = true;
				return true;
			}
		}	
  }
}

