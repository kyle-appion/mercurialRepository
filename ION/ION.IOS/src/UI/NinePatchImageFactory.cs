namespace ION.IOS.UI {

	using System;

	using CoreGraphics;
	using UIKit;

	using Appion.Commons.Util;

  /// <summary>
  /// A Factory used to inflate ninepatch images.
  /// </summary>
  /// <remarks>
  /// This API was heavily stolen from https://github.com/shiami/SWNinePatchImageFactory
  /// under the Apache 2.0 license.
  /// </remarks>
  /// <remarks>
  /// Note: This is not a whole implementation of a ninepatch inflator. Some
  /// key ninepatch features are simply not implemented such as internal
  /// image padding (the right and bottom bars that are used to provided view
  /// padding in Android). Further, Android ninepatch allows for "segmentated"
  /// image scaling. This factory does not. Instead we will take the inclusive
  /// union of all scale "segments". Meaning, the first left pixel on the width
  /// scale bar is set the left scale corner, and the right pixel will be the
  /// right scale corner. All other width pixels are ignored. The same holds
  /// true for height pixels.
  /// </remarks>
  public class NinePatchImageFactory {
    /// <summary>
    /// Loads a ninepatch from resources using the given relative pathname.
    /// If the image is not a compliant ninepatch, we will throw an
    /// ArgumentException.
    /// </summary>
    /// <remarks>
    /// Note: the proper file extension for a compliant ninepatch is .9.png.
    /// If the extension is not correct, we will throw an exception.
    /// </remarks>
    /// <returns>The resizable nine patch image.</returns>
    /// <param name="name">Name.</param>
    public static UIImage CreateResizableNinePatchImage(string name) {
      if (!name.EndsWith(".9.png")) {
        // This assertion is used to enforce the explicit intent to create a
        // ninepatch.
        throw new ArgumentException("Cannot create ninepatch: invalid name. Are you missing .9 token before extension?");
      }

      return CreateResizableImageFromNinePatchImage(new UIImage(name));
    }

    /// <summary>
    /// Parses the given image into a ninepatch image. If the image is not
    /// a compliant ninepatch, we will throw an ArgumentException.
    /// </summary>
    /// <returns>The resizable nine patch image.</returns>
    /// <param name="image">Image.</param>
    public static UIImage CreateResizableNinePatchImage(UIImage image) {
      return CreateResizableImageFromNinePatchImage(image);
    }

    private static void log(string message) {
      Log.D("NinePatchImageFactory", message);
    }

    /// <summary>
    /// Returns the 2d alpha map of the image.
    /// </summary>
    /// <returns>The RGB as from image.</returns>
    /// <param name="image">Image.</param>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    /// <param name="count">Count.</param>
    private static float[,] GetAlphasFromImage(UIImage image, int x, int y, int count) {
      nint bytesPerPixel = 4;
      nint bitsPerComponent = 8;

      CGImage imageRef = image.CGImage;
      nint width = imageRef.Width;
      nint height = imageRef.Height;
      CGColorSpace colorSpace = CGColorSpace.CreateDeviceRGB();
      byte[] rawData = new byte[height * width * bytesPerPixel];
      nint bytesPerRow = bytesPerPixel * width;
      CGContext context = new CGBitmapContext(rawData, width, height, bitsPerComponent, bytesPerRow, colorSpace, CGImageAlphaInfo.PremultipliedLast);
      context.DrawImage(new CGRect(0, 0, width, height), imageRef);
      colorSpace.Dispose();
      context.Dispose();

      float[,] ret = new float[width,height];

      for (int h = 0; h < height; h++) {
        for (int w = 0; w < width; w++) {
          ret[w, h] = rawData[bytesPerRow * h + (w * bytesPerPixel) + 3] / 255.0f;
        }
      }

      return ret;
    }

    /// <summary>
    /// Using the ninepatch core image, create the newly resized image.
    /// </summary>
    /// <returns>The resizable image from nine patch image.</returns>
    /// <param name="image">Image.</param>
    private static UIImage CreateResizableImageFromNinePatchImage(UIImage ninePatch) {
      int width = (int)ninePatch.Size.Width;
      int height = (int)ninePatch.Size.Height;

      float[,] alphaImage = GetAlphasFromImage(ninePatch, 0, 0, width * height);

      // The width scale section of a ninepatch is only valid from 0 - image.width EXCLUDING the first and last pixel (ctnd.)
      float[] npWidthBar = new float[width];
      float[] npHeightBar = new float[height];
      for (int w = 0; w < width; w++) {
        npWidthBar[w] = alphaImage[w,0];
      }
      for (int h = 0; h < height; h++) {
        npHeightBar[h] = alphaImage[0,h];
      }

      float left = -1, top = -1, right = -1, bottom = -1;

      // Start looking for the scale bounds of the image.
      // Note: all of the following loops are inset by a single pixel to account
      // for the fact that a valid ninepatch CANNOT have a scale pixel at the
      // width and height extremes. (ie. a width pixel cannot be at 0,0)

      // Find the left-most ninepatch scale indicator
      for (int i = 1; i < width - 1; i++) {
        if (npWidthBar[i] == 1) {
          left = i;
          break;
        }
      }

      // Find the right-most ninepatch scale indicator
      for (int i = width - 2; i > 0; i--) {
        if (npWidthBar[i] == 1) {
          right = i;
          break;
        }
      }

      // Find the top-most ninepatch scale indicator
      for (int i = 1; i < height - 1; i++) {
        if (npHeightBar[i] == 1) {
          top = i;
          break;
        }
      }

      // Find the bottom-most ninepatch scale indicator
      for (int i = height - 2; i > 0; i--) {
        if (npHeightBar[i] == 1) {
          bottom = i;
          break;
        }
      }

      // Ensure that the image is valid
      if (left == -1 || right == -1 || top == -1 || bottom == -1) {
        Log.E("NinePathImageFactory", "Failed to parse ninepatch. Returning image instead.");
        return ninePatch;
//        throw new ArgumentException("Cannot create ninepatch image: ninepatch scale bars invalid {l:" + left + ", t: " + top + ", r:" + right + " b:" + bottom + "}");
      }

      var cropImage = ninePatch.Crop(new CGRect(1, 1, width - 3, height - 3));
      return cropImage.CreateResizableImage(new UIEdgeInsets(top, left, bottom, right));
    }
  }
}

