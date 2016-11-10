namespace ION.Droid.Fragments {

  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;

  using Android.App;
  using Android.Content;
  using Android.Graphics;
  using Android.OS;
  using AUtil = Android.Util;
  using Android.Views;
  using Android.Widget;

  using ION.Core.App;
  using ION.Core.Util;

  using ION.Droid.Views;

  public class FluidFragment : Fragment {

    /// <summary>
    /// The delegate that is called when a fluid is selected.
    /// </summary>
    public delegate void OnFluidSelected(FluidFragment fragment, string fluidName);

    /// <summary>
    /// The event that is notified when the fragment's selected fluid changes.
    /// </summary>
    public event OnFluidSelected onFluidSelected;

    /// <summary>
    /// The text that will be displayed when the adapter has no items.
    /// </summary>
    /// <value>The empty text.</value>
    public string emptyText { get; set; }
    public string selectedFluid {
      get {
        return __selectedFluid;
      }
      set {
        __selectedFluid = value;
        adapter.selectedFluid = __selectedFluid;
        adapter.NotifyDataSetChanged();
      }
    } string __selectedFluid;
    /// <summary>
    /// The list of fluid names that are in th app.
    /// </summary>
    /// <value>The fluid list.</value>
    public List<string> fluidList {
      get {
        return adapter.fluids;
      }
      set {
        var list = new List<string>();

        if (value != null) {
          list.AddRange(value);
        }

        adapter.fluids = list;
        adapter.NotifyDataSetChanged();
      }
    }

    public string title { get; set; }

    /// <summary>
    /// The ion context for the fragment.
    /// </summary>
    /// <value>The ion.</value>
    internal IION ion { get; set; }
    /// <summary>
    /// The list view that will display information about the fluid.
    /// </summary>
    /// <value>The list view.</value>
    private ListView listView { get; set; }
    /// <summary>
    /// The text view that will display the empty text.
    /// </summary>
    /// <value>The empty text.</value>
    private TextView textView { get; set; }
    /// <summary>
    /// The fluid adapter that will provide views to the list view.
    /// </summary>
    /// <value>The adapter.</value>
    private FluidAdapter adapter { get; set; }

    public FluidFragment() : this(AppState.context) {
    }

    public FluidFragment(IION ion) {
      this.ion = ion;

      adapter = new FluidAdapter(this);
    }

    // Overridden from Fragment
    public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
      var ret = inflater.Inflate(Resource.Layout.fragment_fluid_list, container, false);
  
      listView = ret.FindViewById<ListView>(Android.Resource.Id.List);
      textView = ret.FindViewById<TextView>(Android.Resource.Id.Empty);

      listView.EmptyView = textView;
      listView.OnItemClickListener = new AdapterViewItemClickedAction((adapterView, view, position, id) => {
        if (onFluidSelected != null) {
          onFluidSelected(this, adapter[position]);
        }
      });

      listView.Adapter = adapter;

      textView.Text = emptyText;

      return ret;
    }

    // Overridden from Fragment
    public override void OnActivityCreated(Bundle savedInstanceState) {
      base.OnActivityCreated(savedInstanceState);
      adapter.NotifyDataSetChanged();
    }
  }

  /// <summary>
  /// The adapter that will provide fluid rows for the fluid manager dialog.
  /// </summary>
  internal class FluidAdapter : BaseAdapter {
    /// <summary>
    /// The name of the selected fluid. The selected fluid will display a checkmark
    /// on its list row.
    /// </summary>
    /// <value>The selected fluid.</value>
    public string selectedFluid { get; set; }
    public List<string> fluids { get; set; }

    public string this[int position] {
      get {
        return fluids[position];
      }
    }

    // Overridden from BaseAdapter
    public override int Count {
      get {
        return fluids.Count;
      }
    }

    private FluidFragment fragment { get; set; } 

    public FluidAdapter(FluidFragment fragment) {
      this.fragment = fragment;
      this.fluids = new List<string>();
    }

    // Overridden from BaseAdapter
    public override Java.Lang.Object GetItem(int position) {
      return this[position];
    }

    public override long GetItemId(int position) {
      return position;
    }

    // Overridden from BaseAdapter
    public override View GetView(int position, View convert, ViewGroup parent) {
      var fm = fragment.ion.fluidManager;
      var c = parent.Context;
      ViewHolder vh = convert?.Tag as ViewHolder;

      if (vh == null) {
        convert = LayoutInflater.From(c).Inflate(Resource.Layout.fluid, parent, false);
        convert.Tag = vh = new ViewHolder();

        vh.color = convert.FindViewById(Resource.Id.color);
        vh.name = convert.FindViewById<TextView>(Resource.Id.name);
        vh.perferred = convert.FindViewById<ImageView>(Resource.Id.preferred);
      }

      string fluidName = fluids[position];

      vh.color.SetBackgroundColor(new Color(fm.GetFluidColor(fluidName))); 
      vh.name.Text = fluidName;

      if (fluidName.Equals(selectedFluid)) {
				convert.SetBackgroundColor(c.Resources.GetColor(Resource.Color.light_gray));
      } else {
				convert.SetBackgroundColor(c.Resources.GetColor(Resource.Color.white));
      }

      if (fm.IsFluidPreferred(fluidName)) {
        vh.perferred.SetColorFilter(new Color(c.Resources.GetColor(Resource.Color.gold)));
      } else {
        vh.perferred.SetColorFilter(new Color(c.Resources.GetColor(Resource.Color.black)));
      }

      vh.perferred.SetOnClickListener(new ViewClickAction((view) => {
        fm.MarkFluidAsPreferred(fluidName, !fm.IsFluidPreferred(fluidName));
      }));

      return convert;
    }

    private class ViewHolder : Java.Lang.Object {
      public View color;
      public TextView name;
      public ImageView perferred;
    }
  }
}

