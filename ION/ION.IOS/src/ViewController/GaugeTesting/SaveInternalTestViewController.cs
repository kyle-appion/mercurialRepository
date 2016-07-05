namespace ION.IOS.ViewController.GaugeTesting {
	
	using System;
	using System.IO;
	using System.Threading.Tasks;

	using CoreGraphics;
	using Foundation;
	using MessageUI;
	using UIKit;

	using ION.Core.App;
	using ION.Core.IO;
	using ION.Core.Internal.Testing;
	using ION.Core.Util;

	using ION.IOS.UI;

	public partial class SaveInternalTestViewController : BaseIONViewController {

		private const string EXPORT_EMAIL = "ahodder@appioninc.com";

		public TestResults testResults;

		private UIView loadingView;

		public SaveInternalTestViewController (IntPtr handle) : base (handle) {
		}

		public override void ViewDidLoad() {
			base.ViewDidLoad();

			NavigationItem.RightBarButtonItem = new UIBarButtonItem("Send", UIBarButtonItemStyle.Plain, delegate {
				ExportResults();
			});

			var button = new UIButton(new CGRect(0, 0, 31, 30));
			button.TouchUpInside += (sender, e) => {
				if (testResults == null) {
					var dialog = UIAlertController.Create("Test Results Null", "Some idiot programmer messed up and not you can't export. Sorry for wasting your time.", UIAlertControllerStyle.Alert);
					dialog.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, null));
					PresentViewController(dialog, true, null);
				} else if (IsValid()) {
					PerformExport();
				} else {
					var dialog = UIAlertController.Create("Form incomplete", "The form is not complete. Please ensure all fields are populated", UIAlertControllerStyle.Alert);
					dialog.AddAction(UIAlertAction.Create("Try again", UIAlertActionStyle.Cancel, null)); 
					PresentViewController(dialog, true, null);
				}
			};

			DisableEditing();

			enterTester.Text = testResults.tester;
			enterInstrumentModel.Text = testResults.instrument.model;
			enterCalibrationDate.Date = testResults.instrument.lastCalibrationDate.DateTimeToNSDate();
			enterInstrumentSerial.Text = testResults.instrument.serialNumber;
			enterInstrumentAccuracy.Text = testResults.instrument.accuracy;

			enterTester.ShouldReturn += (textField) => {
				enterTester.ResignFirstResponder();
				return true;
			};

			enterInstrumentModel.ShouldReturn += (textField) => {
				enterInstrumentModel.ResignFirstResponder();
				return true;
			};

			enterInstrumentSerial.ShouldReturn += (textField) => {
				enterInstrumentSerial.ResignFirstResponder();
				return true;
			};

			enterInstrumentAccuracy.ShouldReturn += (textField) => {
				enterInstrumentAccuracy.ResignFirstResponder();
				return true;
			};

			editInstrumentDetails.TouchUpInside += (sender, e) => {
				ShowVerifyEditDetails();
			};
		}

		private void ExportResults() {
			try {
				testResults.tester = enterTester.Text;
				testResults.instrument.model = enterInstrumentModel.Text;
				testResults.instrument.lastCalibrationDate = enterCalibrationDate.Date.NSDateToDateTime();
				testResults.instrument.serialNumber = enterInstrumentSerial.Text;
				testResults.instrument.accuracy = enterInstrumentAccuracy.Text;

				var ion = AppState.context;
				var fm = ion.fileManager;
				var file = fm.CreateTemporaryFile("Av760 Gauge Test Results", EFileAccessResponse.CreateIfMissing);
				var stream = file.OpenForWriting();
				if (new CSVAv760NistTraceExporter().Export(stream, testResults)) {
					if (MFMailComposeViewController.CanSendMail) {
						var vc = new MFMailComposeViewController();
						vc.MailComposeDelegate = new MailDelegate();
						vc.SetSubject("New ION Gauge Test Results");
						vc.SetMessageBody("Hello Person of Interest\n\nThis is your morning call informing you of the new gauge results. One love,\nThe world", false);
						vc.SetToRecipients(new string[] { "ahodder@appioninc.com" });
						vc.AddAttachmentData(NSData.FromFile(file.fullPath), "application/csv", file.name);
						NavigationController.PresentViewController(vc, true, null);
					} else {
						Toast.New(View, "Failed to send results: no mail support. Sorry.");
					}
				} else {
					throw new Exception("Failed to export");
				}
			} catch (Exception e) {
				Log.E(this, "Failed to export test", e);
				var dialog = UIAlertController.Create("Failed to export", "Failed to export the test. A log was made documenting why.", UIAlertControllerStyle.Alert);
				dialog.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, null));
				PresentViewController(dialog, true, null);
			}
		}

		private void ShowVerifyEditDetails() {
			var dialog = UIAlertController.Create("Are you sure?", "Ignorantly modifying these values can greatly damage the validity of the test. Are you sure you wish to change them", UIAlertControllerStyle.Alert);
			dialog.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, null));
			dialog.AddAction(UIAlertAction.Create("Edit", UIAlertActionStyle.Default, (handler) => {
				EnableEditing();
			}));
			PresentViewController(dialog, true, null);
		}

		private Task PerformExport() {
			if (loadingView != null) {
				loadingView.RemoveFromSuperview();
			}

			loadingView = new LoadingOverlay(View.Frame);
			View.AddSubview(loadingView);

			return Task.Factory.StartNew(() => {
				ExportResults();

				AppState.context.PostToMain(() => {
					if (loadingView != null) {
						loadingView.RemoveFromSuperview();
					}
				});
			});
		}

		private void EnableEditing() {
			enterInstrumentModel.UserInteractionEnabled = true;
			enterInstrumentAccuracy.UserInteractionEnabled = true;
			enterInstrumentSerial.UserInteractionEnabled = true;
			enterCalibrationDate.UserInteractionEnabled = true;
		}

		private void DisableEditing() {
			enterInstrumentModel.UserInteractionEnabled = false;
			enterInstrumentAccuracy.UserInteractionEnabled = false;
			enterInstrumentSerial.UserInteractionEnabled = false;
			enterCalibrationDate.UserInteractionEnabled = false;
		}

		private bool IsValid() {
			return string.IsNullOrEmpty(enterInstrumentModel.Text) &&
				           string.IsNullOrEmpty(enterInstrumentAccuracy.Text) &&
				           string.IsNullOrEmpty(enterInstrumentSerial.Text) &&
				           string.IsNullOrEmpty(enterTester.Text);
		}

		public class LoadingOverlay : UIView {
			// control declarations
			UIActivityIndicatorView activitySpinner;
			UILabel loadingLabel;

			public LoadingOverlay (CGRect frame) : base (frame)
			{
				// configurable bits
				BackgroundColor = UIColor.Black;
				Alpha = 0.75f;
				AutoresizingMask = UIViewAutoresizing.All;

				nfloat labelHeight = 22;
				nfloat labelWidth = Frame.Width - 20;

				// derive the center x and y
				nfloat centerX = Frame.Width / 2;
				nfloat centerY = Frame.Height / 2;

				// create the activity spinner, center it horizontall and put it 5 points above center x
				activitySpinner = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge);
				activitySpinner.Frame = new CGRect (
					centerX - (activitySpinner.Frame.Width / 2) ,
					centerY - activitySpinner.Frame.Height - 20 ,
					activitySpinner.Frame.Width,
					activitySpinner.Frame.Height);
				activitySpinner.AutoresizingMask = UIViewAutoresizing.All;
				AddSubview (activitySpinner);
				activitySpinner.StartAnimating ();

				// create and configure the "Loading Data" label
				loadingLabel = new UILabel(new CGRect (
					centerX - (labelWidth / 2),
					centerY + 20 ,
					labelWidth ,
					labelHeight
				));
				loadingLabel.BackgroundColor = UIColor.Clear;
				loadingLabel.TextColor = UIColor.White;
				loadingLabel.Text = "Loading Data...";
				loadingLabel.TextAlignment = UITextAlignment.Center;
				loadingLabel.AutoresizingMask = UIViewAutoresizing.All;
				AddSubview (loadingLabel);

			}

			/// <summary>
			/// Fades out the control and then removes it from the super view
			/// </summary>
			public void Hide ()
			{
				UIView.Animate (
					0.5, // duration
					() => { Alpha = 0; },
					() => { RemoveFromSuperview(); }
				);
			}
		}
	}
}
