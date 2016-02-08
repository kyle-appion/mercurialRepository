namespace ION.Droid.Widgets.Adapters.Workbench {

  using System;
  using System.Collections.Generic;

  using Android.App;
  using Android.Content;
  using Android.OS;
  using Android.Runtime;
  using Android.Support.V7.Widget;
  using Android.Views;
  using Android.Widget;

  using ION.Core.Content;
  using ION.Core.Devices;
  using ION.Core.Sensors;

  using ION.Droid.Devices;
  using ION.Droid.Util;
  using ION.Droid.Widgets.Templates;

  class ViewerRecord : IRecord {
    // Overridden from IRecord
    public EViewType viewType {
      get {
        return EViewType.Viewer;
      }
    }

    public Manifold manifold;

    public ViewerRecord(Manifold manifold) {
      this.manifold = manifold;
    }
  }

  class ViewerViewHolder : WorkbenchViewHolder<ViewerRecord> {
    private WorkbenchAdapter adapter;
    private BitmapCache cache;

    private ViewerRecord record;

    private ManifoldViewTemplate template;

    public ViewerViewHolder(WorkbenchAdapter adapter, BitmapCache cache, View view) : base(view) {
      this.adapter = adapter;
      this.cache = cache;
      template = new ManifoldViewTemplate(view, cache);
    }

    /// <summary>
    /// Binds to.
    /// </summary>
    /// <param name="record">Record.</param>
    public override void BindTo(ViewerRecord record) {
      this.record = record;

      template.Bind(record.manifold);
      template.Invalidate();
    }

    /// <summary>
    /// Unbind this instance.
    /// </summary>
    public override void Unbind() {
      template.Unbind();
    }
  }
}

