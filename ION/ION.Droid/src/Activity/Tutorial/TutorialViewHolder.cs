namespace ION.Droid.Activity.Tutorial {

  using System;

  using Android.Views;
  using Android.Widget;

  using ION.Droid.Widgets.RecyclerViews;

  public class TutorialRecord : RecordAdapter.Record<TutorialPage> {
    public override int viewType { get { return 0; } }

    public TutorialRecord(TutorialPage page) : base(page) {
    }
  }

  public class TutorialViewHolder : RecordAdapter.RecordViewHolder<TutorialRecord> {
    private TextView title;
    private AbsoluteLayout layout;
    private ImageView image;
    private ProgressBar progress;
    private Button button;
    private TextView message;

    public TutorialViewHolder(ViewGroup parent) : base(parent, Resource.Layout.list_item_tutorial) {
      title = ItemView.FindViewById<TextView>(Resource.Id.title);
      layout = ItemView.FindViewById<AbsoluteLayout>(Resource.Id.view);
			image = ItemView.FindViewById<ImageView>(Resource.Id.image);
      progress = ItemView.FindViewById<ProgressBar>(Resource.Id.progress);
			button = ItemView.FindViewById<Button>(Resource.Id.button);
      message = ItemView.FindViewById<TextView>(Resource.Id.text);
		}

    public override void Bind() {
      base.Bind();

      title.SetText(record.data.titleResource);
      message.SetText(record.data.contentResource);
      image.SetImageResource(record.data.imageResource);
		}

    public override void Invalidate() {
      base.Invalidate();
    }
  }
}
