using System;
using System.IO;
using UIKit;
using Foundation;
using CoreGraphics;

using ION.Core.App;

namespace ION.IOS.ViewController.JobManager  {
  
  public class EditJobView {
    public UIView editView;
    public UILabel confirmLabel;
    public UITextField jobName;
    public UITextField customerNumber;
    public UITextField dispatchNumber;
    public UITextField prodOrderNumber;
    public UIButton moreInfoButton;
    IION ion;

    public EditJobView(UIView parentView,int frnJID) {
      ion = AppState.context;

      editView = new UIView(new CGRect(0,0,parentView.Bounds.Width,parentView.Bounds.Height));
      editView.AddGestureRecognizer(new UITapGestureRecognizer(() => {
        editView.EndEditing(true);
      }));

      confirmLabel = new UILabel(new CGRect(.25 * editView.Bounds.Width, 0,.5 * editView.Bounds.Width,.07 * (editView.Bounds.Height - 60)));
      confirmLabel.AdjustsFontSizeToFitWidth = true;
      confirmLabel.TextColor = UIColor.FromRGB(49, 111, 18);
      confirmLabel.TextAlignment = UITextAlignment.Center;
      confirmLabel.Font = UIFont.BoldSystemFontOfSize(20);
      confirmLabel.Text = "Saved Successfully";
      confirmLabel.Hidden = true;     

      var holderName = "";
      var holderCustomer = "";
      var holderDispatch = "";      
      var holderPO = "";

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
        Placeholder = "Product Order Number",
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
      
      editView.AddSubview(confirmLabel);
      editView.AddSubview(jobName);
      editView.AddSubview(customerNumber);
      editView.AddSubview(dispatchNumber);
      editView.AddSubview(prodOrderNumber);
    }

  }
}

