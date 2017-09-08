using System;
using QuickLook;
using Foundation;
using System.IO;

namespace ION.IOS.ViewController.Logging
{
  public class QLPreviewItemFileSystem : QLPreviewItem
  {
    string _fileName, _filePath;

    public QLPreviewItemFileSystem(string fileName, string filePath)
    {
      _fileName = fileName;
      _filePath = filePath;
    }

    public override string ItemTitle {
      get {
        return _fileName;
      }
    }
    public override NSUrl ItemUrl {
      get {
        return NSUrl.FromFilename (_filePath);
      }
    }
  }

  public class QLPreviewItemBundle : QLPreviewItem
  {
    string _fileName, _filePath;
    public QLPreviewItemBundle(string fileName, string filePath)
    {
      _fileName = fileName;
      _filePath = filePath;
    }

    public override string ItemTitle {
      get {
        return _fileName;
      }
    }
    public override NSUrl ItemUrl {
      get {
        var documents = NSBundle.MainBundle.BundlePath;
        var lib = Path.Combine (documents, _filePath);
        var url = NSUrl.FromFilename (lib);
        return url;
      }
    }
  }

  public class PreviewControllerDS : QLPreviewControllerDataSource
  {
    private QLPreviewItem _item;

    public PreviewControllerDS(QLPreviewItem item)
    {
      _item = item;
    }
    public override nint PreviewItemCount(QLPreviewController controller) {
      return 1;
    }

    public override IQLPreviewItem GetPreviewItem(QLPreviewController controller, nint index) {
      return _item;
    }

  }
}