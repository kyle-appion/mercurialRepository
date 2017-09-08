namespace ION.IOS.ViewController.Help {

  using System;

  using Foundation;
  using UIKit;

	public partial class HelpViewController : BaseIONViewController {

    /// <summary>
    /// The current help page that the view controller is showing.
    /// </summary>
    /// <value>The page.</value>
    public HelpPage page {
      get {
        return __page;
      }
      set {
        __page = value;
        source = new HelpSource(this, __page);
      }
    } HelpPage __page;

    private HelpSource source { get; set; }

		public HelpViewController (IntPtr handle) : base (handle) {
		}

    // Overridden from BaseIONViewController
    public override void ViewDidLoad() {
      base.ViewDidLoad();

      NavigationItem.Title = page.title;

      if (page == null) {
        page = new HelpPageBuilder("").Build();
      }

      table.Source = source;
    }
	}
}
