namespace ION.Droid.Widgets.Analyzer {

	using System;

	using Android.Support.V7.Widget;
	using Android.Views;
	using Android.Widget;

	using ION.Core.Content;

	using ION.Droid.Util;
	using ION.Droid.Widgets.Templates;

	public class AnalyzerManifoldViewTemplate : ManifoldViewTemplate {
		public RecyclerView list { get; private set; }
		public TextView empty { get; private set; }
		public View content { get; private set; }

		public RecyclerView subviews { get; private set; }

		private int background;
		public SubviewAdapter adapter;

		public AnalyzerManifoldViewTemplate(View view, BitmapCache cache, int emptyText, int background, SubviewAdapter.OnSensorPropertyClicked clicked) : base(view, cache) {
			this.list = list;
			this.background = background;

			list = view.FindViewById<RecyclerView>(Resource.Id.list);
			empty = view.FindViewById<TextView>(Resource.Id.empty);
			empty.SetText(emptyText);
			content = view.FindViewById(Resource.Id.content);

			subviews = view.FindViewById<RecyclerView>(Resource.Id.list);

			adapter = new SubviewAdapter(cache);
			adapter.onSensorPropertyClicked += (m, sp) => {
				clicked(m, sp);
			};
			list.SetLayoutManager(new LinearLayoutManager(view.Context));
			list.SetAdapter(adapter);

			Bind(null);
		}

		/// <summary>
		/// Binds the view template to the given data.
		/// </summary>
		/// <param name="manifold">Manifold.</param>
		protected override void OnBind(Manifold manifold) {
			if (manifold == null) {
				this.Unbind();
				empty.Visibility = ViewStates.Visible;
				content.Visibility = ViewStates.Invisible;
				serialNumber.SetBackgroundResource(Resource.Drawable.xml_white_bordered_background);
				adapter.manifold = null;
			} else {
				base.OnBind(manifold);

				adapter.manifold = manifold;

				empty.Visibility = ViewStates.Invisible;
				content.Visibility = ViewStates.Visible;
				serialNumber.SetBackgroundResource(background);
			}
		}

		protected override void OnUnbind() {
			base.OnUnbind();
			adapter.manifold = null;
			adapter.RefreshRecords();
		}
	}
}

