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
    IION ion;

    public EditJobView(UIView parentView,int frnJID) {
      ion = AppState.context;

      editView = new UIView(new CGRect(0,0,parentView.Bounds.Width,parentView.Bounds.Height - 70));
      editView.AddGestureRecognizer(new UITapGestureRecognizer(() => {
        editView.EndEditing(true);
      }));

      confirmLabel = new UILabel(new CGRect(.25 * editView.Bounds.Width, .08 * editView.Bounds.Height,.5 * editView.Bounds.Width,.07 * editView.Bounds.Height));
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

      jobName = new FloatLabeledTextField(new CGRect(.1 * editView.Bounds.Width,.18 * editView.Bounds.Height,.8 * editView.Bounds.Width,.14 * editView.Bounds.Height)){
        Placeholder = "Job Name(Required)",
        FloatingLabelFont = UIFont.BoldSystemFontOfSize(12),
        FloatingLabelTextColor = UIColor.Gray,
        FloatingLabelActiveTextColor = UIColor.Blue,
        TextAlignment = UITextAlignment.Center,
      };
      jobName.Layer.BorderWidth = 1f;
      jobName.ShouldReturn += (textField) => {
        textField.ResignFirstResponder();
        return true;
      };
      jobName.Text = holderName;

      customerNumber = new FloatLabeledTextField(new CGRect(.1 * editView.Bounds.Width,.32 * editView.Bounds.Height,.8 * editView.Bounds.Width,.14 * editView.Bounds.Height)){
        Placeholder = "Customer Number",
        FloatingLabelFont = UIFont.BoldSystemFontOfSize(12),
        FloatingLabelTextColor = UIColor.Gray,
        FloatingLabelActiveTextColor = UIColor.Blue,
        TextAlignment = UITextAlignment.Center,
      };
      customerNumber.Layer.BorderWidth = 1f;
      customerNumber.ShouldReturn += (textField) => {
        textField.ResignFirstResponder();
        return true;
      };
      customerNumber.Text = holderCustomer;

      dispatchNumber = new FloatLabeledTextField(new CGRect(.1 * editView.Bounds.Width,.46 * editView.Bounds.Height,.8 * editView.Bounds.Width,.14 * editView.Bounds.Height)){
        Placeholder = "Dispatch Number",
        FloatingLabelFont = UIFont.BoldSystemFontOfSize(12),
        FloatingLabelTextColor = UIColor.Gray,
        FloatingLabelActiveTextColor = UIColor.Blue,
        TextAlignment = UITextAlignment.Center,
      };
      dispatchNumber.Layer.BorderWidth = 1f;
      //dispatchNumber.Layer.CornerRadius = 8;
      dispatchNumber.ShouldReturn += (textField) => {
        textField.ResignFirstResponder();
        return true;
      };
      dispatchNumber.Text = holderDispatch;

      prodOrderNumber = new FloatLabeledTextField(new CGRect(.1 * editView.Bounds.Width,.6 * editView.Bounds.Height,.8 * editView.Bounds.Width,.14 * editView.Bounds.Height)){
        Placeholder = "Product Order Number",
        FloatingLabelFont = UIFont.BoldSystemFontOfSize(12),
        FloatingLabelTextColor = UIColor.Gray,
        FloatingLabelActiveTextColor = UIColor.Blue,
        TextAlignment = UITextAlignment.Center,
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

