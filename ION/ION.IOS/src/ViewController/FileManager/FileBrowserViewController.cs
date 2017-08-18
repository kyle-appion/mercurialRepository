namespace ION.IOS.ViewController.FileManager {


  using System;

  using Foundation;
  using QuickLook;
  using UIKit;

  using ION.Core.App;
  using ION.Core.IO;

	public partial class FileBrowserViewController : BaseIONViewController {
    /// <summary>
    /// The root folder whose files are being displayed.
    /// </summary>
    /// <value>The root folder.</value>
    public IFolder rootFolder {
      get {
        return __rootFolder;
      }
      set {
        __rootFolder = value;
        source = new FileManagerSource(__rootFolder.GetFileList());
        source.onFileRowClicked = OnFileRowClicked;
      }
    } IFolder __rootFolder;

    public string title { 
      get { 
        return __title;
      }
      set {
        __title = value;
        if (NavigationItem != null) {
          NavigationItem.Title = __title;
        }
      }
    } string __title;


    private IION ion { get; set; }

    private FileManagerSource source { get; set; }


		public FileBrowserViewController (IntPtr handle) : base (handle) {
		}

    // Overridden from ViewDidLoad
    public override void ViewDidLoad() {
      base.ViewDidLoad();

      NavigationItem.Title = title;

      ion = AppState.context;

      if (rootFolder == null) {
        rootFolder = ion.fileManager.GetApplicationInternalDirectory();
      }

      table.Source = source;

    }

    // TODO ahodder@appioninc.com: MAKE MORE GENERIC CAUSE WE ASSUME PDF
    /// <summary>
    /// Called when a file row is clicked within the source.
    /// </summary>
    /// <param name="file">File.</param>
    /// <param name="indexPath">Index path.</param>
    private void OnFileRowClicked(IFile file, NSIndexPath indexPath) {
      // Opens the file in the user's perferred (as in user clicked) app
      var url = new NSUrl(file.fullPath, false);
      /*
      var externalViewer = UIDocumentInteractionController.FromUrl(new NSUrl(file.fullPath, false));
      externalViewer.PresentOptionsMenu(View.Frame, View, true);
      */
      /*
      var externalViewer = UIDocumentInteractionController.FromUrl (new NSUrl(file.fullPath, false));
      externalViewer.ViewControllerForPreview = c => this;
      externalViewer.ViewForPreview = c => this.View;
      externalViewer.RectangleForPreview = c => this.View.Bounds;
      externalViewer.PresentOpenInMenu(View.Frame, View, true);
      */

      QLPreviewController previewController = new QLPreviewController();
      previewController.DataSource = new DataSource(url);
      PresentViewController(previewController, true, null);
    }

    public void Refresh() {
      source = new FileManagerSource(__rootFolder.GetFileList());
      source.onFileRowClicked = OnFileRowClicked;
      table.Source = source;

      table.ReloadData();
    }
	}

  internal class DataSource : QLPreviewControllerDataSource {
    public NSUrl url { get; set; }

    public DataSource(NSUrl url) {
      this.url = url;
    }

    public override nint PreviewItemCount(QLPreviewController conteoller) {
      return 1;
    }

    // Overridden form QLPreviewControllerDataSource
    public override IQLPreviewItem GetPreviewItem(QLPreviewController controller, nint index) {
      return new QLItem("", url);
    }
  }

  internal class QLItem : QLPreviewItem {
    public string title { get; set; }
    public NSUrl url { get; set; }

    public override string ItemTitle {
      get {
        return title;
      }
    }

    public override NSUrl ItemUrl {
      get {
        return url;
      }
    }

    public QLItem(string title, NSUrl url) {
      this.title = title;
      this.url = url;
    }
  }
}
