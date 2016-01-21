namespace ION.Droid {

  using System;

  using Android.OS;

  using Java.Interop;

  public class GenericParcelable : Java.Lang.Object, IParcelable {
    public GenericParcelable() {
    }

    public GenericParcelable(Parcel source) {
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
  /// The generic creator that will instantiate parcelables.
  /// </summary>
  public sealed class GenericParcelableCreator<T> : Java.Lang.Object, IParcelableCreator where T : GenericParcelable {
    public GenericParcelableCreator() {
    }

    // Overridden from IParcelableCreator
    public Java.Lang.Object CreateFromParcel(Parcel source) {
      return (T)Activator.CreateInstance(typeof(T), source);
    }

    // Overridden from IParcelableCreator
    public Java.Lang.Object[] NewArray(int size) {
      return (Java.Lang.Object[])new T[size];
    }
  }
}

