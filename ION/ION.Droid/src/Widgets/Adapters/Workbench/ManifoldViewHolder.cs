namespace ION.Droid.Widgets.Adapters.Workbench {
  
  using System;

  using Android.Views;

  using ION.Core.Content;

  using ION.Droid.Util;
  using ION.Droid.Widgets.Templates;

  class ManifoldRecord : WorkbenchRecord<Manifold> {
    public ManifoldRecord(Manifold manifold) : base(EViewType.Manifold, manifold) {
    }
  }

  class ManifoldViewHolder : WorkbenchViewHolder<ManifoldRecord> {
    private ManifoldViewTemplate template;

    public ManifoldViewHolder(View view, BitmapCache cache) : base(view) {
      template = new ManifoldViewTemplate(view, cache);
    }

    public override void BindTo(ManifoldRecord t) {
      template.Bind(t.item);
    }

    public override void Unbind() {
      template.Unbind();
    }
  }
}

