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
using System.Xml;

namespace ION.IOS.ViewController.JobManager  {
  
  public class EditJobView {
    public UIView editView;
		public UITextField jobName;
		public UILabel jobNameLabel;
		public UITextField customerNumber;
		public UILabel customerNumberLabel;
		public UITextField dispatchNumber;
		public UILabel dispatchNumberLabel;
		public UITextField prodOrderNumber;
		public UILabel prodOrderNumberLabel;
		public UITextField notes;
		public UILabel notesLabel;
		public UITextField techName;
		public UILabel techNameLabel;
		public UITextField systemName;
		public UILabel systemNameLabel;
		public UITextField jobAddress;
		public UILabel jobAddressLabel;
		public UIActivityIndicatorView loadingCoordinates;
    IION ion;
		public string fileDir;

		//public CLGeocoder geoCoder;
    
    public bool expanded = false;
    public int jobID = 0;

    public EditJobView(UIView parentView,int frnJID) {
      ion = AppState.context;
      jobID = frnJID;
      //geoCoder = new CLGeocoder();

      editView = new UIView(new CGRect(0,0,parentView.Bounds.Width,parentView.Bounds.Height));
      editView.AddGestureRecognizer(new UITapGestureRecognizer(() => {
        editView.EndEditing(true);
      }));
      editView.BackgroundColor = UIColor.White;

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

      jobNameLabel = new UILabel(new CGRect(.05 * editView.Bounds.Width, .03 * (editView.Bounds.Height - 60), .9 * editView.Bounds.Width, .05 * (editView.Bounds.Height - 60)));
      jobNameLabel.Text = "Job Name:("+ Util.Strings.REQUIRED+")";
      jobNameLabel.Font = UIFont.BoldSystemFontOfSize(15f);

      jobName = new UITextField(new CGRect(.05 * editView.Bounds.Width,.08 * (editView.Bounds.Height - 60),.9 * editView.Bounds.Width,.07 * (editView.Bounds.Height - 60))){
        Placeholder = "TEXT IS ENTERED HERE",
        //FloatingLabelFont = UIFont.BoldSystemFontOfSize(12),
        //FloatingLabelTextColor = UIColor.Gray,
        //FloatingLabelActiveTextColor = UIColor.Blue,
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

      customerNumberLabel = new UILabel(new CGRect(.05 * editView.Bounds.Width, .15 * (editView.Bounds.Height - 60), .9 * editView.Bounds.Width, .05 * (editView.Bounds.Height - 60)));
			customerNumberLabel.Text = "Customer #:";
			customerNumberLabel.Font = UIFont.BoldSystemFontOfSize(15f);
      customerNumber = new UITextField(new CGRect(.05 * editView.Bounds.Width,.2 * (editView.Bounds.Height - 60),.9 * editView.Bounds.Width,.07 * (editView.Bounds.Height - 60))){
        Placeholder = "TEXT IS ENTERED HERE",
        //FloatingLabelFont = UIFont.BoldSystemFontOfSize(12),
        //FloatingLabelTextColor = UIColor.Gray,
        //FloatingLabelActiveTextColor = UIColor.Blue,
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

      dispatchNumberLabel = new UILabel(new CGRect(.05 * editView.Bounds.Width, .27 * (editView.Bounds.Height - 60), .9 * editView.Bounds.Width, .05 * (editView.Bounds.Height - 60)));
			dispatchNumberLabel.Text = "Dispatch #:";
			dispatchNumberLabel.Font = UIFont.BoldSystemFontOfSize(15f);
      dispatchNumber = new UITextField(new CGRect(.05 * editView.Bounds.Width,.32 * (editView.Bounds.Height - 60),.9 * editView.Bounds.Width,.07 * (editView.Bounds.Height - 60))){
        Placeholder = "TEXT IS ENTERED HERE",
        //FloatingLabelFont = UIFont.BoldSystemFontOfSize(12),
        //FloatingLabelTextColor = UIColor.Gray,
        //FloatingLabelActiveTextColor = UIColor.Blue,
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

      prodOrderNumberLabel = new UILabel(new CGRect(.05 * editView.Bounds.Width, .39 * (editView.Bounds.Height - 60), .9 * editView.Bounds.Width, .05 * (editView.Bounds.Height - 60)));
			prodOrderNumberLabel.Text = "Purchase Order #:";
			prodOrderNumberLabel.Font = UIFont.BoldSystemFontOfSize(15f);
      prodOrderNumber = new UITextField(new CGRect(.05 * editView.Bounds.Width,.44 * (editView.Bounds.Height - 60),.9 * editView.Bounds.Width,.07 * (editView.Bounds.Height - 60))){
        Placeholder = "TEXT IS ENTERED HERE",
        //FloatingLabelFont = UIFont.BoldSystemFontOfSize(12),
        //FloatingLabelTextColor = UIColor.Gray,
        //FloatingLabelActiveTextColor = UIColor.Blue,
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

			notesLabel = new UILabel(new CGRect(.05 * editView.Bounds.Width, .51 * (editView.Bounds.Height - 60), .9 * editView.Bounds.Width, .05 * (editView.Bounds.Height - 60)));
			notesLabel.Text = "Notes:";
			notesLabel.Font = UIFont.BoldSystemFontOfSize(15f);
			notes = new UITextField(new CGRect(.05 * editView.Bounds.Width, .56 * (editView.Bounds.Height - 60), .9 * editView.Bounds.Width, .07 * (editView.Bounds.Height - 60))) {
				Placeholder = "TEXT IS ENTERED HERE",
				//FloatingLabelFont = UIFont.BoldSystemFontOfSize(12),
				//FloatingLabelTextColor = UIColor.Gray,
				//FloatingLabelActiveTextColor = UIColor.Blue,
				TextAlignment = UITextAlignment.Center,
				AutocorrectionType = UITextAutocorrectionType.No,
				AutocapitalizationType = UITextAutocapitalizationType.None,
			};
			notes.Layer.BorderWidth = 1f;
			notes.ShouldReturn += (textField) => {
				textField.ResignFirstResponder();
				return true;
			};
			var notesQuery = ion.database.Query<ION.Core.Database.JobRow>("SELECT jobName FROM JobRow WHERE JID = ?", jobID);
			if (notesQuery.Count > 0) {
				fileDir = System.IO.Path.Combine(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal)), notesQuery[0].jobName + ".xml");
			}

      loadNotes(jobID);

      techNameLabel = new UILabel(new CGRect(.05 * editView.Bounds.Width, .63 * (editView.Bounds.Height - 60), .9 * editView.Bounds.Width, .05 * (editView.Bounds.Height - 60)));
			techNameLabel.Text = "Technician Name:";
			techNameLabel.Font = UIFont.BoldSystemFontOfSize(15f);
      techName = new UITextField(new CGRect(.05 * editView.Bounds.Width,.68 * (editView.Bounds.Height - 60),.9 * editView.Bounds.Width,.07 * (editView.Bounds.Height - 60))){
        Placeholder = "TEXT IS ENTERED HERE",
        //FloatingLabelFont = UIFont.BoldSystemFontOfSize(12),
        //FloatingLabelTextColor = UIColor.Gray,
        //FloatingLabelActiveTextColor = UIColor.Blue,
        TextAlignment = UITextAlignment.Center,
        AutocorrectionType = UITextAutocorrectionType.No,
        AutocapitalizationType = UITextAutocapitalizationType.None,
			};
			techName.Layer.BorderWidth = 1f;
      techName.ShouldReturn += (textField) => {
        textField.ResignFirstResponder();
        return true;
      };
      techName.Text = holderTech;

      systemNameLabel = new UILabel(new CGRect(.05 * editView.Bounds.Width, .75 * (editView.Bounds.Height - 60), .9 * editView.Bounds.Width, .05 * (editView.Bounds.Height - 60)));
			systemNameLabel.Text = "System Info:";
			systemNameLabel.Font = UIFont.BoldSystemFontOfSize(15f);
      systemName = new UITextField(new CGRect(.05 * editView.Bounds.Width,.8 * (editView.Bounds.Height - 60),.9 * editView.Bounds.Width,.07 * (editView.Bounds.Height - 60))){
        Placeholder = "TEXT IS ENTERED HERE",
        //FloatingLabelFont = UIFont.BoldSystemFontOfSize(12),
        //FloatingLabelTextColor = UIColor.Gray,
        //FloatingLabelActiveTextColor = UIColor.Blue,
        TextAlignment = UITextAlignment.Center,
        AutocorrectionType = UITextAutocorrectionType.No,
        AutocapitalizationType = UITextAutocapitalizationType.None,
			};
			systemName.Layer.BorderWidth = 1f;
      systemName.ShouldReturn += (textField) => {
        textField.ResignFirstResponder();
        return true;
      };
      systemName.Text = holderSystem;

			jobAddressLabel = new UILabel(new CGRect(.05 * editView.Bounds.Width, .87 * (editView.Bounds.Height - 60), .9 * editView.Bounds.Width, .05 * (editView.Bounds.Height - 60)));
			jobAddressLabel.Text = "Address:";
			jobAddressLabel.Font = UIFont.BoldSystemFontOfSize(15f);
      jobAddress = new UITextField(new CGRect(.05 * editView.Bounds.Width, .93 * (editView.Bounds.Height - 60),.9 * editView.Bounds.Width,.07 * (editView.Bounds.Height - 60))){
				Placeholder = "TEXT IS ENTERED HERE",
        //FloatingLabelFont = UIFont.BoldSystemFontOfSize(12),
        //FloatingLabelTextColor = UIColor.Gray,
        //FloatingLabelActiveTextColor = UIColor.Blue,
        TextAlignment = UITextAlignment.Center,
        AutocorrectionType = UITextAutocorrectionType.No,
        AutocapitalizationType = UITextAutocapitalizationType.None,
        AdjustsFontSizeToFitWidth = true,
			};
			jobAddress.Layer.BorderWidth = 1f;
			jobAddress.Text = holderAddress;
			
			editView.AddSubview(jobName);
			editView.AddSubview(jobNameLabel);
			editView.AddSubview(customerNumber);
			editView.AddSubview(customerNumberLabel);
			editView.AddSubview(dispatchNumber);
			editView.AddSubview(dispatchNumberLabel);
			editView.AddSubview(prodOrderNumber);
			editView.AddSubview(prodOrderNumberLabel);
			editView.AddSubview(notes);
			editView.AddSubview(notesLabel);
			editView.AddSubview(techName);
			editView.AddSubview(techNameLabel);
			editView.AddSubview(systemName);
			editView.AddSubview(systemNameLabel);
			editView.AddSubview(jobAddress);
			editView.AddSubview(jobAddressLabel);
    }

		public void loadNotes(int frnJID) {
			if (!frnJID.Equals(0)) {
				if (File.Exists(fileDir)) {
					// Create an XML reader for this file.
					using (XmlReader reader = XmlReader.Create(fileDir)) {
						while (reader.Read()) {
							// Only detect start elements.
							if (reader.IsStartElement()) {
								// Get element name and switch on it.
								switch (reader.Name) {
									case "Info":
									// Search for the attribute name on this current node.
									string attribute = reader["Info"];
									if (attribute != null) {
										Console.WriteLine(" Has attribute name: " + attribute);
									}
									// Next read will contain text.
									if (reader.Read()) {
										notes.Text = reader.Value.Trim();
									}
									break;
								}
							}
						}
					}
				}
			}
		}

		public void updateNotes(int frnJID) {
			if (!frnJID.Equals(0)) {
				if (File.Exists(fileDir)) {
					File.Delete(fileDir);
					using (XmlWriter writer = XmlWriter.Create(fileDir)) {
						writer.WriteStartDocument();
						writer.WriteStartElement("Job");

						writer.WriteStartElement("Notes");

						writer.WriteElementString("Info", notes.Text);   // <-- These are new

						writer.WriteEndElement();

						writer.WriteEndElement();
						writer.WriteEndDocument();
					}
				} else {
					using (XmlWriter writer = XmlWriter.Create(fileDir)) {
						writer.WriteStartDocument();
						writer.WriteStartElement("Job");

						writer.WriteStartElement("Notes");

						writer.WriteElementString("Info", notes.Text);   // <-- These are new

						writer.WriteEndElement();

						writer.WriteEndElement();
						writer.WriteEndDocument();
					}
				}
			}
		}
		//public async Task<bool> updateJobCoordinates(object sender, EventArgs e){
		//	coordinateButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
		//	await Task.Delay(TimeSpan.FromMilliseconds(2));			
		//	loadingCoordinates = new UIActivityIndicatorView(new CGRect(.1 * editView.Bounds.Width, 1.05 * (editView.Bounds.Height - 60),.8 * editView.Bounds.Width,.07 * (editView.Bounds.Height - 60)));
		//	loadingCoordinates.BackgroundColor = UIColor.Black;
		//	loadingCoordinates.Alpha = .8f;
		//	editView.AddSubview(loadingCoordinates);
		//	editView.BringSubviewToFront(loadingCoordinates);
		//	loadingCoordinates.StartAnimating();
			
		//	if(string.IsNullOrEmpty(jobAddress.Text)){
  //      var iosIon = ion as IosION;
  //      if(iosIon.settings._location.allowsGps){
		//			var latlong = ion.locationManager.lastKnownLocation.latitude.ToString("0.000000") + "," + ion.locationManager.lastKnownLocation.longitude.ToString("0.000000");
		//			coordinateButton.Enabled = false;
		//			coordinateLabel.Text = latlong;			
					
		//			var placemarks = await geoCoder.ReverseGeocodeLocationAsync(new CLLocation(ion.locationManager.lastKnownLocation.latitude,ion.locationManager.lastKnownLocation.longitude));
		//			var address = "";
		//	    foreach (var placemark in placemarks) {
	 //         address = placemark.Name + " " + placemark.Locality + ", " + placemark.AdministrativeArea + " " + placemark.PostalCode;
		//	    }
		//			loadingCoordinates.StopAnimating();
			    
		//			ion.database.Query<ION.Core.Database.JobRow>("UPDATE JobRow SET jobLocation = ?, jobAddress = ? WHERE JID = ?",latlong,address,jobID);
		//			jobAddress.Text = address;
								
		//			coordinateButton.Enabled = true;
		//			} else {
		//				loadingCoordinates.StopAnimating();
					
		//				var window = UIApplication.SharedApplication.KeyWindow;
		//				var rootVC = window.RootViewController as IONPrimaryScreenController;
						
		//				var alert = UIAlertController.Create ("GPS Disabled", "You must enable ION HVAC/R through your device settings to access your location if no address is entered.", UIAlertControllerStyle.Alert);
		//				alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
		//				rootVC.PresentViewController (alert, animated: true, completionHandler: null);
		//			}
		//			return true;
		//	} else {
		//		coordinateButton.Enabled = false;
		//		var placemarks = await geoCoder.GeocodeAddressAsync(jobAddress.Text);
		//		loadingCoordinates.StopAnimating();
				
		//		if(placemarks.Length == 0){
		//			coordinateLabel.Text = "Unable to retrieve coordinates";
		//		} else {
		//			var latlong = "";
		//			foreach(var placemark in placemarks){
		//				latlong = placemark.Location.Coordinate.Latitude + ","+placemark.Location.Coordinate.Longitude;
		//			}
		//			ion.database.Query<ION.Core.Database.JobRow>("UPDATE JobRow SET jobLocation = ?, jobAddress = ? WHERE JID = ?",latlong,jobAddress.Text,jobID);

		//			coordinateLabel.Text = latlong;
		//		}

		//		coordinateButton.Enabled = true;
		//		return true;
		//	}
		//}	
  }
}

