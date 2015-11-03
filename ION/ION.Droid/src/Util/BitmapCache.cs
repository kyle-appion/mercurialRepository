using System;

using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;

namespace ION.Droid.Util {
  /// <summary>
  /// BitmapCache is an extremely useful util object that will allow us to cache
  /// bitmaps in a friendly fashion. Using this cache dramatically increased the
  /// speed and responsiveness of the Android application as we weren't raping
  /// the disk constantly looking for resources.
  /// </summary>
  public class BitmapCache : Android.Util.LruCache {

    /// <summary>
    /// The singleton representation of the cache. This may not necessarily be
    /// used.
    /// </summary>
    private static BitmapCache SINGLETON;

    /// <summary>
    /// Queries the current bitmap cache instance.
    /// </summary>
    /// <param name="res"></param>
    /// <returns></returns>
    public static BitmapCache Get(Resources res) {
      if (SINGLETON == null) {
        SINGLETON = new BitmapCache(res);
      }

      return SINGLETON;
    }

    private Resources res { get; set; }

    public BitmapCache(Resources res) : base(64) {
      this.res = res;
    }

    /// <summary>
    /// Queries the given drawable from the bitmap cache. If the drawable is not
    /// currently in the cache, it will be infated and added.
    /// </summary>
    /// <param name="drawableRes"></param>
    /// <returns></returns>
    public Bitmap GetBitmap(int drawableRes) {
      Java.Lang.Integer id = new Java.Lang.Integer(drawableRes);
      Bitmap ret = (Bitmap)Get(id);

      if (ret == null) {
        ret = BitmapFactory.DecodeResource(res, drawableRes);
        Put(id, ret);
      }

      return ret;
    }
  }
}