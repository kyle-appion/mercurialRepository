using System;
using UIKit;
using CoreGraphics;
using Foundation;
using ION.Core.Sensors;
using Appion.Commons.Measure;
using ION.Core.App;
using ION.Core.Content;
using ION.Core.Fluids;
using ION.IOS.Util;

namespace ION.IOS.ViewController.Analyzer {
  public class ManualView {
  
    public UIView mView;
    public UIButton mcloseButton;
    public UIButton mdoneButton;
    public UIButton mmeasurementType;
    public UIButton dtypeButton;
    public UITextField mtextValue;
    public UITextField mbuttonText;
    public UILabel mdeviceType;
    public UILabel popupText;
    public UILabel textValidation;
    public UIView mbuttonBorder;
    public UIView mbuttonBorder2;

    public bool isManual = false;
    public manualEntry start;
    public IION ion;
    public sensorGroup analyzerSensors;
    public LowHighArea lowHighSensors;

    public ManualView(UIView mainView) {
      ion = AppState.context;

      mView = new UIView(new CGRect(.062 * mainView.Bounds.Width, .176 * mainView.Bounds.Height, .875 * mainView.Bounds.Width, .343 * mainView.Bounds.Height));
      mView.ClipsToBounds = true;
			mView.BackgroundColor = UIColor.White;
			mView.Hidden = true;
			mView.Layer.CornerRadius = 5;
			mView.Layer.BorderWidth = 1f;
			mView.Layer.BorderColor = UIColor.LightGray.CGColor;

      mcloseButton = new UIButton(new CGRect(0,.753 * mView.Bounds.Height, .503 * mView.Bounds.Width, .246 * mView.Bounds.Height));
      mcloseButton.ReverseTitleShadowWhenHighlighted = true;
			mcloseButton.SetTitleColor(UIColor.Gray, UIControlState.Normal);
			mcloseButton.SetTitle(Util.Strings.CLOSE, UIControlState.Normal);
			mcloseButton.ClipsToBounds = true;

      mdoneButton = new UIButton(new CGRect(.5 * mView.Bounds.Width, .753 * mView.Bounds.Height, .5 * mView.Bounds.Width, .246 * mView.Bounds.Height));
      mdoneButton.ReverseTitleShadowWhenHighlighted = true;
			mdoneButton.SetTitleColor(UIColor.Gray, UIControlState.Normal);
			mdoneButton.SetTitle(Util.Strings.OKDONE, UIControlState.Normal);
			mdoneButton.ClipsToBounds = true;

      mmeasurementType = new UIButton (new CGRect(.535 * mView.Bounds.Width, .384 * mView.Bounds.Height, .435 * mView.Bounds.Width, .153 * mView.Bounds.Height));
      mmeasurementType.BackgroundColor = UIColor.FromRGB(255, 215, 101);
      mmeasurementType.Layer.BorderColor = UIColor.Black.CGColor;
      mmeasurementType.Layer.BorderWidth = 2f;      

      dtypeButton = new UIButton (new CGRect(.535 * mView.Bounds.Width, .179 * mView.Bounds.Height, .435 * mView.Bounds.Width, .153 * mView.Bounds.Height));
      dtypeButton.AdjustsImageWhenHighlighted = true;
      dtypeButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
      dtypeButton.Layer.BorderColor = UIColor.Black.CGColor;
      dtypeButton.Layer.BorderWidth = 2f;
			dtypeButton.SetTitle(Util.Strings.Sensor.Type.PRESSURE, UIControlState.Normal);
			dtypeButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
			dtypeButton.AccessibilityIdentifier = "Pressure";
			dtypeButton.Font = UIFont.FromName("Helvetica-Bold", 20f);

      mtextValue = new UITextField (new CGRect(.028 * mView.Bounds.Width, .384 * mView.Bounds.Height, .5 * mView.Bounds.Width, .153 * mView.Bounds.Height));
      mtextValue.AttributedPlaceholder = new NSAttributedString (
        Util.Strings.Analyzer.ENTERMEASUREMENT
      );
      mtextValue.KeyboardType = UIKeyboardType.NumbersAndPunctuation;
      mtextValue.AdjustsFontSizeToFitWidth = true;
			mtextValue.Layer.BorderColor = UIColor.LightGray.CGColor;
			mtextValue.Layer.BorderWidth = 1f;
			mtextValue.Layer.CornerRadius = 5;

      mtextValue.ShouldChangeCharacters = (textField, range, replacementString) => {
        if(replacementString.Contains("0") || replacementString.Contains("1") || replacementString.Contains("2") || replacementString.Contains("3") ||
           replacementString.Contains("4") || replacementString.Contains("5") || replacementString.Contains("6") || replacementString.Contains("7") ||
           replacementString.Contains("8") || replacementString.Contains("9") || replacementString.Contains(".") || replacementString.Contains("-") || 
           replacementString.Length.Equals(0)){

          if(replacementString.Contains(".") && textField.Text.Contains(".")){
            return false;
          }
          if((replacementString.Contains("-") && textField.Text.Contains("-")) || replacementString.Contains("-") && textField.Text.Length > 0){
            return false;
          }
          if(replacementString.Contains(".") && textField.Text.Length.Equals(0)){
            textField.Text = "0";
            return true;
          }
          return true;
        }
        else {
          return false;
        }
      };
      mbuttonText = new UITextField (new CGRect(.535 * mView.Bounds.Width, .384 * mView.Bounds.Height, .435 * mView.Bounds.Width, .153 * mView.Bounds.Height));
      mbuttonText.Layer.BorderColor = UIColor.Black.CGColor;
      mbuttonText.Layer.BorderWidth = 2f;
			mbuttonText.UserInteractionEnabled = false;
			mbuttonText.Text = "psig";
			mbuttonText.Font = UIFont.FromName("Helvetica-Bold", 20f);
			mbuttonText.TextColor = UIColor.Black;
			mbuttonText.TextAlignment = UITextAlignment.Center;

      mdeviceType = new UILabel (new CGRect(.028 * mView.Bounds.Width, .169 * mView.Bounds.Height, .5 * mView.Bounds.Width, .174 * mView.Bounds.Height));
      mdeviceType.TextAlignment = UITextAlignment.Right;
      mdeviceType.Font = UIFont.FromName("Helvetica-Bold", 20f);
			mdeviceType.Text = Util.Strings.Device.TYPE + ":";
			mdeviceType.AdjustsFontSizeToFitWidth = true;

      popupText = new UILabel (new CGRect(0,0,mView.Bounds.Width,.158 * mView.Bounds.Height));
      popupText.ClipsToBounds = true;
			popupText.Text = Util.Strings.Analyzer.CREATEMANUAL;
			popupText.TextAlignment = UITextAlignment.Center;
			popupText.AdjustsFontSizeToFitWidth = true;
			popupText.BackgroundColor = UIColor.FromRGB(9, 221, 255);
			popupText.Layer.CornerRadius = 5;
			popupText.Font = UIFont.FromName("Helvetica-Bold", 27f);

      mbuttonBorder = new UIView (new CGRect(0,.753 * mView.Bounds.Height,mView.Bounds.Width,1));
      mbuttonBorder.BackgroundColor = UIColor.LightGray;
      mbuttonBorder2 = new UIView(new CGRect(.5 * mView.Bounds.Width,.753 * mView.Bounds.Height,1,.246 * mView.Bounds.Height));
      mbuttonBorder2.BackgroundColor = UIColor.LightGray;
      textValidation = new UILabel(new CGRect(.028 * mView.Bounds.Width,.553 * mView.Bounds.Height,.942 *mView.Bounds.Width,.153 * mView.Bounds.Height));
      textValidation.TextAlignment = UITextAlignment.Center;
      textValidation.TextColor = UIColor.Red;
      textValidation.Hidden = true;
      textValidation.LineBreakMode = UILineBreakMode.WordWrap;
      textValidation.AdjustsFontSizeToFitWidth = true;

      mView.AddSubview(mcloseButton);
      mView.AddSubview(mdoneButton);
      mView.AddSubview(mmeasurementType);
      mView.AddSubview(dtypeButton);
      mView.AddSubview(mtextValue);
      mView.AddSubview(mbuttonText);
      mView.AddSubview(mdeviceType);
      mView.AddSubview(popupText);
      mView.AddSubview(mbuttonBorder);
      mView.AddSubview(mbuttonBorder2);
      mView.AddSubview(textValidation);

			mmeasurementType.TouchUpInside += showManualPicker;
			dtypeButton.TouchUpInside += showDeviceTypePicker;
			


			mtextValue.ShouldReturn += (textField) => {
				textField.ResignFirstResponder();
				return true;
			};

			mmeasurementType.TouchDown += delegate {
        mbuttonText.BackgroundColor = UIColor.Blue;
      };
      mmeasurementType.TouchUpOutside += delegate {
        mbuttonText.BackgroundColor = UIColor.FromRGB(255, 215, 101);
      };

      dtypeButton.TouchDown += delegate {
        dtypeButton.BackgroundColor = UIColor.Blue;
      };
      dtypeButton.TouchUpOutside += delegate {
        dtypeButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
      };

      mcloseButton.TouchDown += delegate {
        mcloseButton.BackgroundColor = UIColor.Blue;
      };

      mcloseButton.TouchUpInside += delegate {
        mcloseButton.BackgroundColor = UIColor.Clear;
  		  mtextValue.Text = "";
  		  mView.Hidden = true;
  		  dtypeButton.SetTitle(Util.Strings.Sensor.Type.PRESSURE, UIControlState.Normal);
  		  dtypeButton.AccessibilityIdentifier = "Pressure";
  		  mbuttonText.Text = start.pressures[0];
  		  textValidation.Hidden = true;
  		  mtextValue.ResignFirstResponder();
        if (start.pressedSensor != null) {
          start.pressedSensor.isManual = false;
        }
      };

      mcloseButton.TouchUpOutside += delegate {
        mcloseButton.BackgroundColor = UIColor.Clear;
      };

      mdoneButton.TouchDown += delegate {
        mdoneButton.BackgroundColor = UIColor.Blue;
      };
      mdoneButton.TouchUpInside += delegate {
        mdoneButton.BackgroundColor = UIColor.Clear;
      };
      mdoneButton.TouchUpOutside += delegate {
        mdoneButton.BackgroundColor = UIColor.Clear;
      };
    }

    public void setDoneAction(bool single = false){
      if(single){
				mdoneButton.TouchUpInside -= handleManualLHPopup;
				mdoneButton.TouchUpInside -= handleManualPopup;
				mdoneButton.TouchUpInside += handleManualPopup;
			} else {
				mdoneButton.TouchUpInside -= handleManualLHPopup;
				mdoneButton.TouchUpInside -= handleManualPopup;
				mdoneButton.TouchUpInside += handleManualLHPopup;
			}
    }

		/// <summary>
		/// SHOWS THE PICKER FOR THE MEASUREMENT TYPES
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		void showManualPicker(object sender, EventArgs e) {
      mbuttonText.BackgroundColor = UIColor.FromRGB(255, 215, 101);

			var window = UIApplication.SharedApplication.KeyWindow;
			var vc = window.RootViewController;
			while (vc.PresentedViewController != null) {
				vc = vc.PresentedViewController;
			}

			UIAlertController mtypeAlert = UIAlertController.Create(Util.Strings.Analyzer.UNITPICKER, "", UIAlertControllerStyle.Alert);

			if (dtypeButton.AccessibilityIdentifier == "Pressure") {
				foreach (string unit in start.pressures) {
					mtypeAlert.AddAction(UIAlertAction.Create(unit, UIAlertActionStyle.Default, (action) => {
						mbuttonText.Text = unit;
					}));
				}
			} else if (dtypeButton.AccessibilityIdentifier == "Temperature") {
				foreach (string unit in start.temperatures) {
					mtypeAlert.AddAction(UIAlertAction.Create(unit, UIAlertActionStyle.Default, (action) => {
						mbuttonText.Text = unit;
					}));
				}
			} else if (dtypeButton.AccessibilityIdentifier == "Vacuum") {
				foreach (string unit in start.vacuum) {
					mtypeAlert.AddAction(UIAlertAction.Create(unit, UIAlertActionStyle.Default, (action) => {
						mbuttonText.Text = unit;
					}));
				}
			}

			mtypeAlert.AddAction(UIAlertAction.Create(Util.Strings.CANCEL, UIAlertActionStyle.Cancel, (action) => { }));
			vc.PresentViewController(mtypeAlert, true, null);
			mtextValue.ResignFirstResponder();
		}

		/// <summary>
		/// SHOWS THE ALERT FOR WHAT TYPE OF DEVICE TO USE
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		void showDeviceTypePicker(object sender, EventArgs e) {
			dtypeButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);

			var window = UIApplication.SharedApplication.KeyWindow;
			var vc = window.RootViewController;
			while (vc.PresentedViewController != null) {
				vc = vc.PresentedViewController;
			}

			dtypeButton.AccessibilityIdentifier = "Pressure";
			UIAlertController dtypeAlert = UIAlertController.Create(Util.Strings.Analyzer.CHOOSEDEVICE, "", UIAlertControllerStyle.Alert);

			dtypeAlert.AddAction(UIAlertAction.Create(Util.Strings.Sensor.Type.PRESSURE, UIAlertActionStyle.Default, (action) => {
				dtypeButton.SetTitle(Util.Strings.Sensor.Type.PRESSURE, UIControlState.Normal);
				dtypeButton.AccessibilityIdentifier = "Pressure";
				mbuttonText.Text = start.pressures[0];
			}));
			if (!isManual) {
				dtypeAlert.AddAction(UIAlertAction.Create(Util.Strings.Sensor.Type.TEMPERATURE, UIAlertActionStyle.Default, (action) => {
					dtypeButton.SetTitle(Util.Strings.Sensor.Type.TEMPERATURE, UIControlState.Normal);
					dtypeButton.AccessibilityIdentifier = "Temperature";
					mbuttonText.Text = start.temperatures[0];
				}));
			}
			dtypeAlert.AddAction(UIAlertAction.Create(Util.Strings.Sensor.Type.VACUUM, UIAlertActionStyle.Default, (action) => {
				dtypeButton.SetTitle(Util.Strings.Sensor.Type.VACUUM, UIControlState.Normal);
				dtypeButton.AccessibilityIdentifier = "Vacuum";
				mbuttonText.Text = start.vacuum[0];
			}));
			dtypeAlert.AddAction(UIAlertAction.Create(Util.Strings.CANCEL, UIAlertActionStyle.Cancel, (action) => { }));
			vc.PresentViewController(dtypeAlert, true, null);
		}

		/// <summary>
		/// EVENT FUNCTION FOR MANUAL ENTRY POPUP
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		void handleManualPopup(object sender, EventArgs e) {
			if (mtextValue.Text.Contains(".")) {
				var p1 = mtextValue.Text.Split('.');
				var check = p1[1];
				if (check.Length.Equals(0)) {
					textValidation.Text = Util.Strings.Analyzer.VALIDMEASUREMENT;
					textValidation.Hidden = false;
					return;
				}
			}
			if (mtextValue.Text.Length <= 0) {
				textValidation.Text = Util.Strings.Analyzer.MISSINGVALUE;
				textValidation.Hidden = false;
				return;
			}
			if (mtextValue.Text.Contains("-") && mtextValue.Text.Length.Equals(1)) {
				textValidation.Text = Util.Strings.Analyzer.VALIDMEASUREMENT;
				textValidation.Hidden = false;
				return;
			}
			if (start.addPan == null) {
				Console.WriteLine("Pan gesture is null");
			}
			start.pressedSensor.isManual = true;
			start.pressedView.AddGestureRecognizer(start.addPan);
			start.availableView.Hidden = true;
			start.pressedView.BackgroundColor = UIColor.White;
			start.topLabel.Hidden = false;
			start.middleLabel.Hidden = false;
			start.bottomLabel.Hidden = false;
			start.topLabel.Text = " " + dtypeButton.AccessibilityIdentifier;
			var amount = Convert.ToDecimal(mtextValue.Text);
			if (dtypeButton.AccessibilityIdentifier.Equals("Vaccum")) {
				start.middleLabel.Text = ((int)amount).ToString();
			} else {
				start.middleLabel.Text = amount.ToString("N");
			}
			start.bottomLabel.Text = mbuttonText.Text;
			start.pressedSensor.isManual = true;
			start.addIcon.Hidden = true;

			///CREATE MANUAL MANIFOLDS
			if (dtypeButton.AccessibilityIdentifier.Equals("Pressure")) {
				start.pressedSensor.manualSensor = new ManualSensor(ESensorType.Pressure, ion.preferences.units.DefaultUnitFor(ESensorType.Pressure).OfScalar(0.0));
			} else if (dtypeButton.AccessibilityIdentifier.Equals("Temperature")) {
				start.pressedSensor.manualSensor = new ManualSensor(ESensorType.Temperature, ion.preferences.units.DefaultUnitFor(ESensorType.Temperature).OfScalar(0.0));
			} else {
				start.pressedSensor.manualSensor = new ManualSensor(ESensorType.Vacuum, ion.preferences.units.DefaultUnitFor(ESensorType.Vacuum).OfScalar(0.0));
			}

			///SET MANUALSENSOR MEASUREMENT AND UNIT TYPE
			start.pressedSensor.manualSensor.unit = AnalyserUtilities.getManualUnit(start.pressedSensor.manualSensor.type, mbuttonText.Text.ToLower());

			start.pressedSensor.manualSensor.measurement = new Scalar(start.pressedSensor.manualSensor.unit, Convert.ToDouble(mtextValue.Text));

			textValidation.Hidden = true;
			mdoneButton.TouchUpInside -= handleManualPopup;
			dtypeButton.SetTitle(Util.Strings.Sensor.Type.PRESSURE, UIControlState.Normal);
			dtypeButton.AccessibilityIdentifier = "Pressure";
			mbuttonText.Text = start.pressures[0];
			mtextValue.Text = "";
			mView.Hidden = true;
			mtextValue.ResignFirstResponder();
		}

		/// <summary>
		/// EVENT FUNCTION FOR LOW HIGH MANUAL POPUP. ASSOCIATES A SINGLE SENSOR TO THE LOW/HIGH AREA WHEN MAKING A MANUAL LOW/HIGH ENTRY
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		void handleManualLHPopup(object sender, EventArgs e) {

			if (mtextValue.Text.Contains(".")) {
				var p1 = mtextValue.Text.Split('.');
				var check = p1[1];
				if (check.Length.Equals(0)) {
					mtextValue.Text += "0";
					return;
				}
			}
			if (mtextValue.Text.Length <= 0) {
				textValidation.Text = Util.Strings.Analyzer.MISSINGVALUE;
				textValidation.Hidden = false;
				return;
			}

			var amount = Convert.ToDecimal(mtextValue.Text);
			var begin = 0;
			var end = 4;
			var color = UIColor.Blue;
			if (start.pressedView.AccessibilityIdentifier == "high") {
				begin = 4;
				end = 8;
				color = UIColor.Red;
			}

			for (int i = begin; i < end; i++) {
				if (!analyzerSensors.viewList[i].availableView.Hidden) {
					analyzerSensors.viewList[i].addIcon.Hidden = true;
					analyzerSensors.viewList[i].availableView.Hidden = true;
					analyzerSensors.viewList[i].snapArea.BackgroundColor = UIColor.White;
					analyzerSensors.viewList[i].snapArea.AddGestureRecognizer(analyzerSensors.viewList[i].panGesture);
					analyzerSensors.viewList[i].topLabel.Text = " " + dtypeButton.AccessibilityIdentifier;
					analyzerSensors.viewList[i].topLabel.Hidden = false;
					analyzerSensors.viewList[i].topLabel.BackgroundColor = color;
					analyzerSensors.viewList[i].topLabel.TextColor = UIColor.White;

					if (mbuttonText.Text == "micron") {
						analyzerSensors.viewList[i].middleLabel.Text = ((int)amount).ToString();
					} else {
						analyzerSensors.viewList[i].middleLabel.Text = amount.ToString("N");
					}
					analyzerSensors.viewList[i].middleLabel.Hidden = false;
					analyzerSensors.viewList[i].bottomLabel.Text = mbuttonText.Text;
					analyzerSensors.viewList[i].bottomLabel.Hidden = false;
					analyzerSensors.viewList[i].isManual = true;

					if (dtypeButton.AccessibilityIdentifier.Equals("Pressure")) {
						analyzerSensors.viewList[i].manualSensor = new ManualSensor(ESensorType.Pressure, ion.preferences.units.DefaultUnitFor(ESensorType.Pressure).OfScalar(0.0));
					} else if (dtypeButton.AccessibilityIdentifier.Equals("Temperature")) {
						analyzerSensors.viewList[i].manualSensor = new ManualSensor(ESensorType.Temperature, ion.preferences.units.DefaultUnitFor(ESensorType.Temperature).OfScalar(0.0));
					} else {
						analyzerSensors.viewList[i].manualSensor = new ManualSensor(ESensorType.Vacuum, ion.preferences.units.DefaultUnitFor(ESensorType.Vacuum).OfScalar(0.0));
					}
					analyzerSensors.viewList[i].manualSensor.name = analyzerSensors.viewList[i].topLabel.Text;
					analyzerSensors.viewList[i].manualSensor.unit = AnalyserUtilities.getManualUnit(analyzerSensors.viewList[i].manualSensor.type, mbuttonText.Text.ToLower());
					analyzerSensors.viewList[i].manualSensor.measurement = new Scalar(analyzerSensors.viewList[i].manualSensor.unit, Convert.ToDouble(mtextValue.Text));

					Console.WriteLine("Handlemanuallhpopup about to low high associate");
					if (begin == 4) {
						/////ASSOCIATE HIGH AREA TO MANUALLY CREATED SENSOR 
						lowHighSensors.highArea.manualSensor = analyzerSensors.viewList[i].manualSensor;
						lowHighSensors.highArea.manualGType = analyzerSensors.viewList[i].manualSensor.type.ToString();
						lowHighSensors.highArea.isManual = true;
            ion.currentAnalyzer.SetManifold(Core.Content.Analyzer.ESide.High,lowHighSensors.highArea.manualSensor);
						ion.currentAnalyzer.highSideManifold.ptChart = PTChart.New(ion, Fluid.EState.Bubble);
						lowHighSensors.highArea.manifold = ion.currentAnalyzer.highSideManifold;

						///////////////////// change the high manifold to be based on moved location instead of created position
						Console.WriteLine("addLHDeviceSensor already on analyzer Should change highAccessibility to " + i + " instead of " + analyzerSensors.viewList[i].snapArea.AccessibilityIdentifier);
						lowHighSensors.highArea.snapArea.AccessibilityIdentifier = i.ToString();
						ion.currentAnalyzer.highAccessibility = i.ToString();
						/////////////////////                          
						//analyzer.highSubviews = analyzerSensors.viewList[i].highArea.tableSubviews; 
						lowHighSensors.highArea.setLHUI();

					} else {
						/////ASSOCIATE LOW AREA TO MANUALLY CREATED SENSOR 
						lowHighSensors.lowArea.manualSensor = analyzerSensors.viewList[i].manualSensor;
						lowHighSensors.lowArea.manualGType = analyzerSensors.viewList[i].manualSensor.type.ToString();
						lowHighSensors.lowArea.isManual = true;
						ion.currentAnalyzer.SetManifold(Core.Content.Analyzer.ESide.Low, lowHighSensors.highArea.manualSensor);
						ion.currentAnalyzer.lowSideManifold.ptChart = PTChart.New(ion, Fluid.EState.Dew);
						lowHighSensors.lowArea.manifold = ion.currentAnalyzer.lowSideManifold;

						///////////////////// change the low manifold to be based on moved location instead of created position
						Console.WriteLine("handleManualLHPopup Should change lowAccessibility to " + i + " instead of " + analyzerSensors.viewList[i].snapArea.AccessibilityIdentifier);
						lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = i.ToString();
						ion.currentAnalyzer.lowAccessibility = i.ToString();
						/////////////////////
						lowHighSensors.lowArea.setLHUI();

					}

					break;
				}
			}

			mdoneButton.TouchUpInside -= handleManualLHPopup;
			dtypeButton.SetTitle(Util.Strings.Sensor.Type.PRESSURE, UIControlState.Normal);
			dtypeButton.AccessibilityIdentifier = "Pressure";
			mtextValue.Text = "";
			mbuttonText.Text = start.pressures[0];
			mView.Hidden = true;
			mtextValue.ResignFirstResponder();
		}

  }
}

