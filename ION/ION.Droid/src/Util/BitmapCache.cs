namespace ION.Droid.Util {

	using System;

	using Android.Content;
	using Android.Content.Res;
	using Android.Graphics;
	using Android.OS;

  using Appion.Commons.Util;

  /// <summary>
  /// BitmapCache is an extremely useful util object that will allow us to cache
  /// bitmaps in a friendly fashion. Using this cache dramatically increased the
  /// speed and responsiveness of the Android application as we weren't raping
  /// the disk constantly looking for resources.
  /// </summary>
  public class BitmapCache : Android.Util.LruCache {
    private Resources res { get; set; }

    public BitmapCache(Resources res, int size=4) : base(size) {
      this.res = res;
    }

    protected override void EntryRemoved(bool evicted, Java.Lang.Object key, Java.Lang.Object oldValue, Java.Lang.Object newValue) {
      base.EntryRemoved(evicted, key, oldValue, newValue);

      var bitmap = oldValue as Bitmap;
      if (bitmap != null) {
        bitmap.Recycle();
      }
    }

    /// <summary>
    /// Queries the given drawable from the bitmap cache. If the drawable is not
    /// currently in the cache, it will be infated and added.
    /// </summary>
    /// <param name="drawableRes"></param>
    /// <returns></returns>
    public Bitmap GetBitmap(int drawableRes, bool tryAgain=false) {
      try {
        Java.Lang.Integer id = new Java.Lang.Integer(drawableRes);
        Bitmap ret = (Bitmap)Get(id);

        if (ret == null) {
          ret = BitmapFactory.DecodeResource(res, drawableRes);
          Put(id, ret);
        }

        return ret;
      } catch (OutOfMemoryException e) {
        Log.E(this, "Cache caught an OutOfMemoryException while attempting to acquire bitmap.", e);
        Clear();
        if (tryAgain) {
          return GetBitmap(drawableRes, true);
        } else {
          return BitmapFactory.DecodeResource(res, drawableRes);
        }
      } catch (Exception e) {
        Log.E(this, "Failed to get bitmap for an unknown reason", e);
        Clear();
        return BitmapFactory.DecodeResource(res, drawableRes);
      }
    }

    /// <summary>
    /// Clears the cache allowing for the memory to be garbage collected.
    /// </summary>
    public void Clear() {
      EvictAll();
    }
  }
}