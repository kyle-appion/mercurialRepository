using CoreGraphics;
using Foundation;
using UIKit;

namespace ION.IOS.UI {
  public static class UIImageExtensions {
    /// <summary>
    /// Converts the given image into a ninepatch image.
    /// </summary>
    /// <returns>The nine patch.</returns>
    /// <param name="image">Image.</param>
    public static UIImage AsNinePatch(this UIImage image) {
      return NinePatchImageFactory.CreateResizableNinePatchImage(image);
    }

    /// <summary>
    /// Crops the UI image.
    /// </summary>
    /// <param name="image">Image.</param>
    public static UIImage Crop(this UIImage image, CGRect rect) {
      var scale = image.CurrentScale;
      rect = new CGRect(rect.X * scale, rect.Y * scale, rect.Right * scale, rect.Bottom * scale);

      var reference = image.CGImage.WithImageInRect(rect);
      UIImage ret = new UIImage(reference, scale, image.Orientation);
      reference.Dispose();
      return ret;
    }

    /// <summary>
    /// Exports the image as a PDF with the given file name.
    /// </summary>
    /// <param name="filename">Filename.</param>
    public static void ExportAsPDF(this UIImage image, string filename, CGPDFInfo info, double horizRes = 96, double vertRes = 96) {
      var width = (double)image.Size.Width * (double)image.CurrentScale * 72 * horizRes;
      var height = (double)image.Size.Height * (double)image.CurrentScale * 72 * vertRes;

      var pdfFile = new NSMutableData();
      pdfFile.Init();

      var pdfConsumer = new CGDataConsumer(pdfFile);
      var mediaBox = new CGRect(0, 0, width, height);

      using (var context = new CGContextPDF(pdfConsumer, mediaBox, info)) {
        context.BeginPage(new CGPDFPageInfo());
        context.DrawImage(mediaBox, image.CGImage);
        context.EndPage();
      }

      var attrs = new NSFileAttributes();
      NSFileManager.DefaultManager.CreateFile(filename, pdfFile, attrs);
    }
  }
}

