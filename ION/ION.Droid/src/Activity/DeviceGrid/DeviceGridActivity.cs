namespace ION.Droid.Activity.Grid {

  using System;

  using Android.App;
  using Android.Content;
  using Android.Content.PM;
  using Android.OS;
  using Android.Support.V7.Widget;
  using Android.Views;
  using Android.Widget;

  [Activity(Label="US Grid", Theme="@style/AppTheme", LaunchMode=LaunchMode.SingleTask, ScreenOrientation=ScreenOrientation.Portrait)]
	public class DeviceGridActivity : IONActivity {

    private RecyclerView list;
    private DeviceGridAdapter adapter;

    protected override void OnCreate(Bundle state) {
      base.OnCreate(state);
      SetContentView(Resource.Layout.activity_device_grid);

      list = FindViewById<RecyclerView>(Resource.Id.list);
      list.SetLayoutManager(new GridLayoutManager(this, 4));
      adapter = new DeviceGridAdapter(ion);
      list.SetAdapter(adapter);
    }
  }
}
