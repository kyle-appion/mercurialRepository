namespace ION.Droid.Views{

  using System;

  using Android.Views;
  using Android.Widget;

  public class AdapterViewItemClickedAction : Java.Lang.Object, AdapterView.IOnItemClickListener {

    public delegate void OnItemClickedAction(AdapterView adapterView, View view, int position, long id);

    private OnItemClickedAction action { get; set; }


    public AdapterViewItemClickedAction(OnItemClickedAction action) {
      this.action = action;
    }

    // Overridden from AdapterView.IOnItemSelectedListener
    public void OnItemClick(AdapterView adapterView, View view, int position, long id) {
      if (action != null) {
        action(adapterView, view, position, id);
      }
    }
  }
}

