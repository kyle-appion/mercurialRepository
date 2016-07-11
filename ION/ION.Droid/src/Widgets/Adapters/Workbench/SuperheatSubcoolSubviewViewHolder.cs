namespace ION.Droid.Widgets.Adapters.Workbench {

  using System;

  using Android.Views;

  using ION.Core.Sensors.Properties;

  using ION.Droid.Widgets.RecyclerViews;
  using ION.Droid.Widgets.Templates;

  class SuperheatSubcoolSubviewRecord : SensorPropertyRecord<SuperheatSubcoolSensorProperty> {
    public SuperheatSubcoolSubviewRecord(SuperheatSubcoolSensorProperty sp) : base(EViewType.SuperheatSubcoolSubview, sp) {
    }
  }

  class SuperheatSubcoolSubviewViewHolder : SwipableViewHolder<SuperheatSubcoolSubviewRecord> {
    private SuperheatSubcoolSubviewTemplate template;

    public SuperheatSubcoolSubviewViewHolder(ViewGroup parent, int viewResource) : base(parent, viewResource) {
      template = new SuperheatSubcoolSubviewTemplate(view);
    }

    public override void OnBindTo() {
      template.Bind(t.item);
			template.UpdateAssociation(t.manifold);
    }

    public override void Unbind() {
      template.Unbind();
    }
  }
}

