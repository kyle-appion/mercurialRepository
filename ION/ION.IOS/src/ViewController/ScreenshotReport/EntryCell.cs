namespace ION.IOS.ViewController.ScreenshotReport {

  using System;

  using Foundation;
  using UIKit;

	public partial class EntryCell : UITableViewCell {

    public string entryText { get { return entry.Text; } }

		public EntryCell (IntPtr handle) : base (handle) {
		}

    public override void AwakeFromNib() {
      base.AwakeFromNib();

      AddGestureRecognizer(new UITapGestureRecognizer(() => {
        entry.ResignFirstResponder();
      }));
    }

    public void UpdateFrom(IItem item) {
      labelHeader.Text = item.header;
      entry.Text = item.value;
      entry.Delegate = new EntryDelegate(this, item);
      entry.AddTarget((object obj, EventArgs args) => {
      }, UIControlEvent.AllEvents);
    }
	}

  internal class EntryDelegate : UITextFieldDelegate {
    private EntryCell cell { get; set; }
    private IItem item { get; set; }

    public EntryDelegate(EntryCell cell, IItem item) {
      this.cell = cell;
      this.item = item;
    }

    public override void EditingEnded(UITextField textField) {
      item.value = textField.Text;
      cell.entry.ResignFirstResponder();
    }
  }
}
