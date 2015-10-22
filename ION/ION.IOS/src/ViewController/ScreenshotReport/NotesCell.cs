namespace ION.IOS.ViewController.ScreenshotReport {
  
  using System;

  using Foundation;
  using UIKit;

	public partial class NotesCell : UITableViewCell {

		public NotesCell (IntPtr handle) : base (handle) {
		}

    public override void AwakeFromNib() {
      base.AwakeFromNib();

      AddGestureRecognizer(new UITapGestureRecognizer(() => {
        text.ResignFirstResponder();
      }));
    }

    public void UpdateFrom(IItem item) {
      labelHeader.Text = item.header;
      text.Text = item.value;
      text.Delegate = new TextDelegate(item);
      text.AddTarget((object obj, EventArgs args) => {
      }, UIControlEvent.AllEvents);
    }
	}

  internal class TextDelegate : UITextFieldDelegate {
    private IItem item { get; set; }

    public TextDelegate(IItem item) {
      this.item = item;
    }

    // Overridden from UITextViewDelegate
    public override void EditingEnded(UITextField textView) {
      item.value = textView.Text;
      textView.ResignFirstResponder();
    }
  }
}
