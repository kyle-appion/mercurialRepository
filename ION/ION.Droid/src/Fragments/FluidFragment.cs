﻿namespace ION.Droid.Fragments {

  using System.Collections.Generic;

  using Android.App;
  using Android.Graphics;
	using Android.Graphics.Drawables;
  using Android.OS;
  using Android.Views;
  using Android.Widget;

  using ION.Core.App;
	using ION.Core.Fluids;

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
    public override View GetView(int position, View convertView, ViewGroup parent) {
      var fm = fragment.ion.fluidManager;
      var c = parent.Context;
      ViewHolder vh = convertView?.Tag as ViewHolder;

      if (vh == null) {
        convertView = LayoutInflater.From(c).Inflate(Resource.Layout.fluid, parent, false);
        convertView.Tag = vh = new ViewHolder();

        vh.color = convertView.FindViewById(Resource.Id.color);
				vh.safetyColor = convertView.FindViewById(Resource.Id.view);
        vh.name = convertView.FindViewById<TextView>(Resource.Id.name);
				vh.safety = convertView.FindViewById<TextView>(Resource.Id.text);
        vh.perferred = convertView.FindViewById<ImageView>(Resource.Id.preferred);

				vh.safetyColor.Background = new ShapeDrawable(new Android.Graphics.Drawables.Shapes.OvalShape());
      }

      string fluidName = fluids[position];

      vh.color.SetBackgroundColor(new Color(fm.GetFluidColor(fluidName))); 
      vh.name.Text = fluidName;

      if (fluidName.Equals(selectedFluid)) {
        convertView.SetBackgroundColor(Resource.Color.light_gray.AsResourceColor(c));
      } else {
        convertView.SetBackgroundColor(Resource.Color.white.AsResourceColor(c));
      }

      if (fm.IsFluidPreferred(fluidName)) {
        vh.perferred.SetColorFilter(Resource.Color.gold.AsResourceColor(c));
      } else {
        vh.perferred.SetColorFilter(Resource.Color.black.AsResourceColor(c));
      }


			var safety = fm.GetFluidSafety(fluidName);
			var b = vh.safetyColor.Background as ShapeDrawable;
			b.Paint.Color = new Color(safety.Color());
			vh.safetyColor.InvalidateDrawable(b);

			if (safety == Fluid.ESafety.Unknown) {
				vh.safety.Text = c.GetString(Resource.String.na);
				vh.safetyColor.Visibility = ViewStates.Gone;
			} else {
				vh.safety.Text = safety.ToString();
				vh.safetyColor.Visibility = ViewStates.Visible;
			}

      vh.perferred.SetOnClickListener(new ViewClickAction((view) => {
        fm.MarkFluidAsPreferred(fluidName, !fm.IsFluidPreferred(fluidName));
      }));

      return convertView;
    }

    private class ViewHolder : Java.Lang.Object {
      public View color, safetyColor;
      public TextView name, safety;
      public ImageView perferred;
    }
  }
}

