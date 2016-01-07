namespace ION.Droid {

  using System;

  using Android.OS;

  using Java.Interop;

  /// <summary>
  /// A highly generic class that imlements all of the interfaces for a parcelable. All
  /// you as the implementor have to do is define a constructor that creates the parcelable
  /// from a Parcel and override WriteToParcel to place your data.
  /// </summary>
  public class GenericParcelable : Java.Lang.Object, IParcelable {
    /// <summary>
    /// The creator that will create the parcelable from a parcel source.
    /// </summary>
    private static GenericParcelableCreator<GenericParcelable> CREATOR = new GenericParcelableCreator<GenericParcelable>();
    [ExportField("CREATOR")]
    public static GenericParcelableCreator<GenericParcelable> GetCreator() {
      return CREATOR;
    }

    public GenericParcelable() {
    }

    /// <summary>
    /// Called when the parcelable is being created from the parcel source.
    /// </summary>
    /// <param name="source">Source.</param>
    protected GenericParcelable(Parcel source) {
      // Do creation stuff
    }

    // Overridden from IParcelable
    public virtual int DescribeContents() {
      return 0;
    }

    // Overridden from IParcelable
    public virtual void WriteToParcel(Parcel dest, ParcelableWriteFlags flags) {
    }
  }


  /// <summary>
  /// A highly generic class that imlements all of the interfaces for a parcelable. All
  /// you as the implementor have to do is define a constructor that creates the parcelable
  /// from a Parcel and override WriteToParcel to place your data.
  /// </summary>
  ///
  /*
  public class GenericParcelable<T> : GenericParcelable {

    /// <summary>
    /// The creator that will create the parcelable from a parcel source.
    /// </summary>
    private static GenericParcelableCreator<GenericParcelable<T>> CREATOR = new GenericParcelableCreator<GenericParcelable<T>>();
    [ExportField("CREATOR")]
    public static GenericParcelableCreator<GenericParcelable<T>> GetCreator() {
      return CREATOR;
    }
  }
  */

  /// <summary>
  /// The generic creator that will instantiate parcelables.
  /// </summary>
  public class GenericParcelableCreator <T> : Java.Lang.Object, IParcelableCreator where T : GenericParcelable, new() {
    // Overridden from IParcelableCreator
    public Java.Lang.Object CreateFromParcel(Parcel source) {
      return (T)Activator.CreateInstance(typeof(T), source);
    }

    // Overridden from IParcelableCreator
    public Java.Lang.Object[] NewArray(int size) {
      return new T[size];
    }
  }
}

