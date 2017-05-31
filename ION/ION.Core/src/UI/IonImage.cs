namespace ION.Core.UI {

  using System;

  /// <summary>
  /// A simple class that wrap raw image data for use throughout the app.
  /// </summary>
	public class IonImage {
    /// <summary>
    /// The type of image data that is contained.
    /// </summary>
    /// <value>The type.</value>
    public EType type { get; private set; }
    /// <summary>
    /// The width of the image in pixels.
    /// </summary>
    /// <value>The width.</value>
    public int width { get; private set; }
    /// <summary>
    /// The height of the image in pixels.
    /// </summary>
    /// <value>The height.</value>
    public int height { get; private set; }
    /// <summary>
    /// The aspect ratio of the image.
    /// </summary>
    /// <value>The aspect ratio.</value>
    public float aspectRatio { get { return width / height; } }

    /// <summary>
    /// The data that makes up the image.
    /// </summary>
    /// <value>The data.</value>
    public byte[] data { get; private set; }

    public IonImage(EType type, int width, int height, byte[] data) {
      this.width = width;
      this.height = height;
      this.data = data;
    }

    /// <summary>
    /// The enumeration of image types. This is used to identify the type of data within the image.
    /// </summary>
    public enum EType {
      Png,
    }
  }
}
