﻿namespace ION.Droid.Widgets.Adapters.Workbench {
  
  using System;

  using Android.Views;

  using ION.Core.Content;

  using ION.Droid.Util;
  using ION.Droid.Widgets.RecyclerViews;
  using ION.Droid.Widgets.Templates;

  class ManifoldRecord : WorkbenchRecord<Manifold> {
    public bool expanded;
    
    public ManifoldRecord(Manifold manifold) : base(EViewType.Manifold, manifold) {
    }
  }

  class ManifoldViewHolder : SwipableViewHolder<ManifoldRecord> {
    public ManifoldViewTemplate template;

    public ManifoldViewHolder(ViewGroup parent, int viewResource, BitmapCache cache) : base(parent, viewResource) {
      template = new ManifoldViewTemplate(view, cache);
    }

    public override void OnBindTo() {
      template.Bind(t.item);
    }

    public override void Unbind() {
      template.Unbind();
    }
  }
}

