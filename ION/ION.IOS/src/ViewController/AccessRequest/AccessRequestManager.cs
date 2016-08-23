﻿using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using Foundation;
using CoreGraphics;
using UIKit;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using ION.IOS.App;
using ION.IOS.ViewController.WebServices;

namespace ION.IOS.ViewController.AccessRequest {
	public class AccessRequestManager {
		public UIView accessView;
		public UITextField submitCodeField;  
		public UILabel requestLabel;
		public UILabel submitLabel;
		public UIButton submitButton;
		public UIButton requestButton;
		public UIButton refreshButton;
		public UITableView pendingTable;
		public List<requestData> pendingUsers;
		public UIActivityIndicatorView loadingRequests;
		public SessionPayload webActivities;
		
		public AccessRequestManager(UIView parentView, SessionPayload webServices) {
			webActivities = webServices;
			var viewTap = new UITapGestureRecognizer(() => {
				submitCodeField.ResignFirstResponder();
			});
			
			viewTap.CancelsTouchesInView = false;
			accessView = new UIView(new CGRect(0,50, parentView.Bounds.Width, parentView.Bounds.Height - 50));
			accessView.BackgroundColor = UIColor.White;
			accessView.AddGestureRecognizer(viewTap);
		
			submitLabel = new UILabel(new CGRect(.05 * accessView.Bounds.Width, 0, .9 * accessView.Bounds.Width, .07 * accessView.Bounds.Height));
			submitLabel.Text = "Enter Access Code";
			submitLabel.AdjustsFontSizeToFitWidth = true;
			submitLabel.TextAlignment = UITextAlignment.Center;
			
			submitCodeField = new UITextField(new CGRect(.05 * accessView.Bounds.Width, .07 * accessView.Bounds.Height, .9 * accessView.Bounds.Width, .1 * accessView.Bounds.Height));
			submitCodeField.Layer.CornerRadius = 5f;
			submitCodeField.Layer.BorderWidth = 1f;
			submitCodeField.TextAlignment = UITextAlignment.Center;
			submitCodeField.ShouldReturn += (textField) => {
				textField.ResignFirstResponder();
				return true;
			};
			submitCodeField.AutocorrectionType = UITextAutocorrectionType.No;
			
			submitButton = new UIButton(new CGRect(.05 * accessView.Bounds.Width, .175 * accessView.Bounds.Height, .9 * accessView.Bounds.Width, .1 * accessView.Bounds.Height));
			submitButton.SetTitle("Submit", UIControlState.Normal);
			submitButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
			submitButton.Layer.CornerRadius = 5f;
			submitButton.Layer.BorderWidth = 1f;
			submitButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
			submitButton.TouchDown += (sender, e) => {submitButton.BackgroundColor = UIColor.Blue;};
			submitButton.TouchUpInside += (sender, e) => {submitButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);};
			submitButton.TouchUpOutside += (sender, e) => {submitButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);};
			submitButton.TouchUpInside += submitCode;
			
			requestLabel = new UILabel(new CGRect(.2 * accessView.Bounds.Width, .27 * accessView.Bounds.Height, .6 * accessView.Bounds.Width, .08 * accessView.Bounds.Height));
			requestLabel.Text = "Access Requests";
			requestLabel.TextAlignment = UITextAlignment.Center;
			requestLabel.AdjustsFontSizeToFitWidth = true;
			
			refreshButton = new UIButton(new CGRect(.9 * accessView.Bounds.Width - requestLabel.Bounds.Height,.275 * accessView.Bounds.Height,requestLabel.Bounds.Height, .9 * requestLabel.Bounds.Height));
			refreshButton.SetImage(UIImage.FromBundle("ic_refresh"), UIControlState.Normal);
			refreshButton.TouchUpInside += getAllRequests;
			
			pendingTable = new UITableView(new CGRect(.05 * accessView.Bounds.Width, .35 * accessView.Bounds.Height, .9 * accessView.Bounds.Width, .5 * accessView.Bounds.Height));
			pendingTable.BackgroundColor = UIColor.Clear;
			pendingTable.Layer.CornerRadius = 5f;
			pendingTable.Layer.BorderWidth = 1f;
			pendingTable.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			pendingTable.RegisterClassForCellReuse(typeof(AccessRequestTableCell),"accessCell");
			pendingTable.AllowsSelection = true;
						
			requestButton = new UIButton(new CGRect(.05 * accessView.Bounds.Width, .86 * accessView.Bounds.Height, .9 * accessView.Bounds.Width, .1 * accessView.Bounds.Height));
			requestButton.SetTitle("Generate Permanent Access Code", UIControlState.Normal);
			requestButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
			requestButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
			requestButton.Layer.CornerRadius = 5f;
			requestButton.Layer.BorderWidth = 1f;
			requestButton.TouchUpInside += GenerateAccessCode;

			loadingRequests = new UIActivityIndicatorView(new CGRect(.05 * accessView.Bounds.Width, .35 * accessView.Bounds.Height, .9 * accessView.Bounds.Width, .5 * accessView.Bounds.Height));
			loadingRequests.Alpha = .8f;
			loadingRequests.BackgroundColor = UIColor.Gray;
			loadingRequests.HidesWhenStopped = true;
			
			accessView.AddSubview(submitLabel);
			accessView.AddSubview(submitCodeField);
			accessView.AddSubview(submitButton);
			accessView.AddSubview(requestLabel);
			accessView.AddSubview(refreshButton);
			accessView.AddSubview(pendingTable);
			accessView.AddSubview(loadingRequests);
			accessView.AddSubview(requestButton);
			accessView.BringSubviewToFront(loadingRequests);
			getAllRequests(this,null);
		}
		/// <summary>
		/// Posts the entered code and checks for a match to link users together
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		public async void submitCode(object sender, EventArgs e){
			if(submitCodeField.Text.Length >= 8 && submitCodeField.Text.Length <= 10){
				await webActivities.submitAccessCode(submitCodeField.Text);
				submitCodeField.Text = "";
			} else {
				var window = UIApplication.SharedApplication.KeyWindow;
				var rootVC = window.RootViewController as IONPrimaryScreenController;
				
				var alert = UIAlertController.Create ("Confirm Code", "Code entered does not match the required length", UIAlertControllerStyle.Alert);
				alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
				rootVC.PresentViewController (alert, animated: true, completionHandler: null);
			}
		}
	
    /// <summary>
    /// Post to the server and get the list of open requests for the user
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
		public async void getAllRequests(object sender, EventArgs e){
			refreshButton.Enabled = false;
			loadingRequests.StartAnimating();
			await Task.Delay(TimeSpan.FromMilliseconds(1));
			pendingUsers = new List<requestData>();
			await webActivities.getAllRequests(pendingUsers);
			
			pendingTable.Source = new AccessRequestTableSource(pendingUsers, .1 * accessView.Bounds.Height);
			pendingTable.ReloadData();
			
			loadingRequests.StopAnimating();
			refreshButton.Enabled = true;
		}
		/// <summary>
		/// Generates an access code for the user up to 5.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		public async void GenerateAccessCode(object sender, EventArgs e){
			loadingRequests.StartAnimating();
			await Task.Delay(TimeSpan.FromMilliseconds(1));
			
			if(pendingUsers == null){
				pendingUsers = new List<requestData>();
			}
			
			await webActivities.GenerateAccessCode(pendingUsers);
			
			pendingTable.Source = new AccessRequestTableSource(pendingUsers, .1 * accessView.Bounds.Height);
			pendingTable.ReloadData();			
			loadingRequests.StopAnimating();
		}	
	}
		[WebServices.Preserve(AllMembers = true)]
		public class requestData{
			public requestData(){}
			[JsonProperty("display")]
			public string displayName { get; set; }
			[JsonProperty("id")]
			public int id { get; set; }
			[JsonProperty("accessCode")]
			public string accessCode { get; set; }
		}	
}
