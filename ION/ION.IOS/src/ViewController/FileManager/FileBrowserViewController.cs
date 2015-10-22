namespace ION.IOS.ViewController.FileManager {


  using System;

  using Foundation;
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

    /// <summary>
    /// Called when a file row is clicked within the source.
    /// </summary>
    /// <param name="file">File.</param>
    /// <param name="indexPath">Index path.</param>
    private void OnFileRowClicked(IFile file, NSIndexPath indexPath) {
      // Opens the file in the user's perferred (as in user clicked) app
      var externalViewer = UIDocumentInteractionController.FromUrl(new NSUrl(file.fullPath, false));
      externalViewer.PresentOpenInMenu(View.Frame, View, true);
    }
	}
}
