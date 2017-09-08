using System;
using UIKit;
using Foundation;
using CoreGraphics;
using ION.Core.Content;
using ION.Core.Devices;
using ION.Core.Fluids;
using ION.Core.App;

namespace ION.IOS.ViewController.Workbench {


  public class TargetSHSCView {
		public UIView tView;
		public UILabel headerLabel;
		public UILabel nameLabel;
		public UILabel directionsLabel;
		public UILabel unitLabel;
		public UILabel slideLabel;
		public UILabel toggleLabel;
    public UITextField targetInput;
		public UIButton closeButton;
		public UIButton setButton;
    public Manifold targetManifold;
		public UITapGestureRecognizer toggleTap;
		public UITapGestureRecognizer keyboardTap;
    public IION ion;

    public TargetSHSCView(UIView containerView) {
      ion = AppState.context;

      tView = new UIView(new CGRect(.1 * containerView.Bounds.Width, .2 * containerView.Bounds.Height, .8 * containerView.Bounds.Width, .45 * containerView.Bounds.Height));
      tView.BackgroundColor = UIColor.White;
      tView.Hidden = true;

      headerLabel = new UILabel(new CGRect(0, 0, tView.Bounds.Width, .1 * tView.Bounds.Height));
      headerLabel.BackgroundColor = UIColor.FromRGB(0, 174, 239);
      headerLabel.Text = "  Set Target SH/SC";
      headerLabel.Font = UIFont.BoldSystemFontOfSize(20f);

      closeButton = new UIButton(new CGRect(.9 * tView.Bounds.Width, .05 * headerLabel.Bounds.Height, .9 * headerLabel.Bounds.Height, .9 * headerLabel.Bounds.Height));
      closeButton.SetImage(UIImage.FromBundle("img_button_blackclosex"), UIControlState.Normal);
      closeButton.TouchUpInside += (sender, e) => {
        Console.WriteLine("Closing the target window");
        tView.Hidden = true;
      };

      nameLabel = new UILabel(new CGRect(0,.1 * tView.Bounds.Height,tView.Bounds.Width,.1 * tView.Bounds.Height));
      nameLabel.TextAlignment = UITextAlignment.Center;
      nameLabel.Text = "PT500: S516H123";
      nameLabel.Font = UIFont.BoldSystemFontOfSize(20f);

			directionsLabel = new UILabel(new CGRect(.05 * tView.Bounds.Width,.2 * tView.Bounds.Height,.9 * tView.Bounds.Width, .3 * tView.Bounds.Height));
      directionsLabel.Font = UIFont.ItalicSystemFontOfSize(20f);
      directionsLabel.Lines = 0;
      directionsLabel.Text = "Enter the target SH/SC value as provided by the system manufacturer";

			targetInput = new UITextField(new CGRect(.05 * tView.Bounds.Width, .5 * tView.Bounds.Height, .3 * tView.Bounds.Width, .15 * tView.Bounds.Height));
      targetInput.Text = "0.0";
			targetInput.Layer.BorderWidth = 1f;

			unitLabel = new UILabel(new CGRect(.35 * tView.Bounds.Width, .5 * tView.Bounds.Height,.1 * tView.Bounds.Width,.15 * tView.Bounds.Height));
      unitLabel.Text = "°F";

      slideLabel = new UILabel(new CGRect(.6 * tView.Bounds.Width,.55 * tView.Bounds.Height, .3 * tView.Bounds.Width, .05 * tView.Bounds.Height));
      slideLabel.BackgroundColor = UIColor.LightGray;
			slideLabel.Layer.BorderWidth = 1f;

      toggleLabel = new UILabel(new CGRect(.55 * tView.Bounds.Width, .5 * tView.Bounds.Height, .2 * tView.Bounds.Width, .15 * tView.Bounds.Height));
      toggleLabel.BackgroundColor = UIColor.Blue;
      toggleLabel.Text = "S/H";
      toggleLabel.Font = UIFont.BoldSystemFontOfSize(30f);
      toggleLabel.TextColor = UIColor.White;
      toggleLabel.TextAlignment = UITextAlignment.Center;
      toggleLabel.UserInteractionEnabled = true;

      toggleTap = new UITapGestureRecognizer((obj) => {
        Console.WriteLine("Toggling the slider");
        if(targetManifold.ptChart.state == Core.Fluids.Fluid.EState.Dew){
  			  toggleLabel.Frame = new CGRect(.75 * tView.Bounds.Width, .5 * tView.Bounds.Height, .2 * tView.Bounds.Width, .15 * tView.Bounds.Height);
          toggleLabel.BackgroundColor = UIColor.Red;
  				toggleLabel.Text = "S/C";
          targetManifold.ptChart = PTChart.New(ion, Fluid.EState.Bubble);
        } else {
  			  toggleLabel.Frame = new CGRect(.55 * tView.Bounds.Width, .5 * tView.Bounds.Height, .2 * tView.Bounds.Width, .15 * tView.Bounds.Height);
  			  toggleLabel.BackgroundColor = UIColor.Blue;
  			  toggleLabel.Text = "S/H";
          targetManifold.ptChart = PTChart.New(ion, Fluid.EState.Dew);
		    } 
      });

      keyboardTap = new UITapGestureRecognizer((obj) => {
        targetInput.ResignFirstResponder();
      });

      toggleLabel.AddGestureRecognizer(toggleTap);
      tView.AddGestureRecognizer(keyboardTap);

      setButton = new UIButton(new CGRect(.45 * tView.Bounds.Width, .8 * tView.Bounds.Height, .1 * tView.Bounds.Width, .1 * tView.Bounds.Width));
      setButton.Layer.CornerRadius = 5f;
      setButton.Layer.BorderWidth = 1f;
      setButton.BackgroundColor = UIColor.FromRGB(255, 200, 46);
      setButton.SetTitle("OK", UIControlState.Normal);

      setButton.TouchUpInside += (sender, e) => {
        Console.WriteLine("Setting manifold target SHSC");
        if(targetManifold != null){
          targetManifold.targetSHSC = double.Parse(targetInput.Text);
        }
		    targetInput.ResignFirstResponder();

		    tView.Hidden = true;
      };

			tView.AddSubview(headerLabel);
			tView.AddSubview(closeButton);
			tView.AddSubview(nameLabel);
			tView.AddSubview(directionsLabel);
			tView.AddSubview(targetInput);
			tView.AddSubview(unitLabel);
			tView.AddSubview(slideLabel);
			tView.AddSubview(toggleLabel);
			tView.AddSubview(setButton);
      containerView.AddSubview(tView);
    }

    public void setManifold(Manifold manifold){
      targetManifold = manifold;
      var holderSensor = manifold.primarySensor as GaugeDeviceSensor;
      nameLabel.Text = holderSensor.device.type.ToString()+": "+holderSensor.device.serialNumber.rawSerial;
      targetInput.Text = manifold.targetSHSC.ToString();

			if (targetManifold.ptChart.state == Core.Fluids.Fluid.EState.Dew) {
				toggleLabel.Frame = new CGRect(.55 * tView.Bounds.Width, .5 * tView.Bounds.Height, .2 * tView.Bounds.Width, .15 * tView.Bounds.Height);
				toggleLabel.BackgroundColor = UIColor.Blue;
				toggleLabel.Text = "S/H";
			} else {
				toggleLabel.Frame = new CGRect(.75 * tView.Bounds.Width, .5 * tView.Bounds.Height, .2 * tView.Bounds.Width, .15 * tView.Bounds.Height);
				toggleLabel.BackgroundColor = UIColor.Red;
				toggleLabel.Text = "S/C";
			}
			
      tView.Hidden = false;
    }
  }
}
