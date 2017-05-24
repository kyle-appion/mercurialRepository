namespace ION.Droid.Activity.Tutorial {

  using System;
	using System.Collections.Generic;

	using Android.App;
	using Android.Content.PM;
	using Android.OS;
	using Android.Support.V4.View;
	using Android.Support.V7.Widget;
	using Android.Views;
	using Android.Widget;

  using Appion.Commons.Util;

	using ION.Droid.Net.Portal;
	using ION.Droid.Util;
	using ION.Droid.Views;
  using ION.Droid.Widgets.RecyclerViews;

	[Activity(Label = "@string/preferences_help_walkthrough", Theme = "@style/TerminalActivityTheme", ScreenOrientation=ScreenOrientation.Portrait)]
  public class TutorialActivity : IONActivity {

    private RecyclerView list;
		private TextView tabLayout;
		private Adapter adapter;
    private OnScroll scroller;

		private View left;
		private View right;

		protected override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);

			ActionBar.SetDisplayHomeAsUpEnabled(true);

			SetContentView(Resource.Layout.activity_tutorial);

			left = FindViewById(Resource.Id.left);
			left.SetOnClickListener(new ViewClickAction((v) => {
        scroller.ScrollLeft();
			}));

			right = FindViewById(Resource.Id.right);
			right.SetOnClickListener(new ViewClickAction((v) => {
        scroller.ScrollRight();
			}));

			FindViewById(Resource.Id.button).SetOnClickListener(new ViewClickAction((view) => {
				Finish();
			}));

			FindViewById(Resource.Id.help).SetOnClickListener(new ViewClickAction((v) => {
				ion.portal.SendAppSupportEmail(ion, this);
			}));

      list = FindViewById<RecyclerView>(Resource.Id.list);
			tabLayout = FindViewById<TextView>(Resource.Id.tab_1);

      adapter = new Adapter();

			// WORKBENCH
			adapter.AddPage(new TutorialPage() {
				titleResource = Resource.String.tutorial_workbench,
				imageResource = Resource.Drawable.tutorial_workbench,
				contentResource = Resource.String.tutorial_workbench_description,
				xPercent = -1f,
				yPercent = -1f,
			});

			// WORKBENCH ADD VIEWER
			adapter.AddPage(new TutorialPage() {
				titleResource = Resource.String.tutorial_workbench,
				imageResource = Resource.Drawable.tutorial_workbench,
				contentResource = Resource.String.tutorial_workbench_add_viewer,
				xPercent = 0.5f,
				yPercent = 0.3248945148f,
			});

			// DEVICE MANAGER
			adapter.AddPage(new TutorialPage() {
				titleResource = Resource.String.tutorial_device_manager,
				imageResource = Resource.Drawable.tutorial_device_manager,
				contentResource = Resource.String.tutorial_device_manager_description,
				xPercent = -1f,
				yPercent = -1f,
			});

			adapter.AddPage(new TutorialPage() {
				titleResource = Resource.String.tutorial_device_manager,
				imageResource = Resource.Drawable.tutorial_device_manager_scan,
				contentResource = Resource.String.tutorial_device_manager_scan,
				xPercent = 0.9435f,
				yPercent = 0.05907173f,
			});

			adapter.AddPage(new TutorialPage() {
				titleResource = Resource.String.tutorial_device_manager,
				imageResource = Resource.Drawable.tutorial_device_manager,
				contentResource = Resource.String.tutorial_device_manager_connect,
				xPercent = 0.94375f,
				yPercent = 0.1814345992f,
			});

			adapter.AddPage(new TutorialPage() {
				titleResource = Resource.String.tutorial_device_manager,
				imageResource = Resource.Drawable.tutorial_device_manager,
				contentResource = Resource.String.tutorial_device_manager_add,
				xPercent = 0.85f,
				yPercent = 0.2362869198f,
			});

			// WORKBENCH CONTINUED
			adapter.AddPage(new TutorialPage() {
				titleResource = Resource.String.tutorial_workbench,
				imageResource = Resource.Drawable.tutorial_workbench_actions,
				contentResource = Resource.String.tutorial_workbench_actions,
				xPercent = 0.5f,
				yPercent = 0.168777f,
			});

			// NAVIGATION DRAWER
			adapter.AddPage(new TutorialPage() {
				titleResource = Resource.String.tutorial_navigation,
				imageResource = Resource.Drawable.tutorial_navigation_drawer,
				contentResource = Resource.String.tutorial_navigation_description,
				xPercent = 0.075f,
				yPercent = 0.05907173f,
			});

			adapter.AddPage(new TutorialPage() {
				titleResource = Resource.String.tutorial_navigation,
				imageResource = Resource.Drawable.tutorial_navigation_drawer,
				contentResource = Resource.String.tutorial_navigation_exit,
				xPercent = 0.25f,
				yPercent = 0.683544f,
			});

			// WORKBENCH CONTINUED
			adapter.AddPage(new TutorialPage() {
				titleResource = Resource.String.tutorial_workbench,
				imageResource = Resource.Drawable.tutorial_workbench,
				contentResource = Resource.String.tutorial_workbench_data_log,
				xPercent = 0.9475f,//4375f,
				yPercent = 0.05907173f,
			});

			adapter.AddPage(new TutorialPage() {
				titleResource = Resource.String.tutorial_workbench,
				imageResource = Resource.Drawable.tutorial_workbench,
				contentResource = Resource.String.tutorial_workbench_screenshot,
				xPercent = 0.84f,//0.8375f,
				yPercent = 0.05907173f,
			});

			// ANALYZER
			adapter.AddPage(new TutorialPage() {
				titleResource = Resource.String.tutorial_analyzer,
				imageResource = Resource.Drawable.tutorial_analyzer,
				contentResource = Resource.String.tutorial_analyzer_description,
				xPercent = -1f,
				yPercent = -1f,
			});

			adapter.AddPage(new TutorialPage() {
				titleResource = Resource.String.tutorial_analyzer,
				imageResource = Resource.Drawable.tutorial_analyzer,
				contentResource = Resource.String.tutorial_analyzer_add,
				xPercent = 0.34375f,
				yPercent = 0.1603376f,
			});

			adapter.AddPage(new TutorialPage() {
				titleResource = Resource.String.tutorial_analyzer,
				imageResource = Resource.Drawable.tutorial_analyzer_actions,
				contentResource = Resource.String.tutorial_analyzer_actions,
				xPercent = -1,//0.26f,
				yPercent = -1,//0.616034f,
			});

      scroller = new OnScroll(list, UpdatePosition);
      list.SetLayoutManager(new LinearLayoutManager(this, LinearLayoutManager.Horizontal, false));
      list.AddOnScrollListener(scroller);
      var snap = new LinearSnapHelper();
      snap.AttachToRecyclerView(list);
      list.SetAdapter(adapter);
			left.Visibility = ViewStates.Invisible;

      tabLayout.Text = 1 + " / " + adapter.ItemCount;
		}

    protected override void OnResume() {
      base.OnResume();
      UpdatePosition(scroller.currentIndex);
    }

		// Overridden from Activity
		protected override void OnPause() {
			base.OnPause();
			var cb = FindViewById<CheckBox>(Resource.Id.check);
			ion.appPrefs.showTutorial = !cb.Checked;
		}

		// Overridden from Activity
		public override bool OnMenuItemSelected(int featureId, IMenuItem item) {
			switch (item.ItemId) {
				case Android.Resource.Id.Home:
					SetResult(Result.Canceled);
					Finish();
				return true;
				default:
				return base.OnMenuItemSelected(featureId, item);
			}
		}

    private void UpdatePosition(int index) {
      if (index <= 0) {
        left.Visibility = ViewStates.Invisible;
      } else {
        left.Visibility = ViewStates.Visible;
      }

      if (index >= adapter.ItemCount - 1) {
        right.Visibility = ViewStates.Invisible;
      } else {
        right.Visibility = ViewStates.Visible;
      }

      tabLayout.Text = (index + 1) + " / " + adapter.ItemCount;
    }

    private class Adapter : RecordAdapter {
      public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
        return new TutorialViewHolder(parent);
      }

      public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position) {
        var vh = holder as TutorialViewHolder;
        vh.record = records[position] as TutorialRecord;
      }

      public void AddPage(TutorialPage page) {
        records.Add(new TutorialRecord(page));
      }
    }

    private class OnScroll : RecyclerView.OnScrollListener {
      public int currentIndex {
        get {
          return __currentIndex;
        }
        set {
          __currentIndex = value;
          onItemSelected(value);
        }
      } int __currentIndex;

      private RecyclerView recyclerView;
      private Action<int> onItemSelected;

      public OnScroll(RecyclerView recyclerView, Action<int> onItemSelected) {
        this.recyclerView = recyclerView;
        this.onItemSelected = onItemSelected;
        __currentIndex = 0;
      }

      public override void OnScrollStateChanged(RecyclerView recyclerView, int newState) {
				base.OnScrollStateChanged(recyclerView, newState);
				Log.D(this, "The current scroll state is: " + newState);

//        if (newState == RecyclerView.ScrollStateIdle) {
          var lm = recyclerView.GetLayoutManager() as LinearLayoutManager;
          currentIndex = lm.FindFirstVisibleItemPosition();
          onItemSelected(currentIndex);
//        }
      }

      public void ScrollLeft() {
        if (currentIndex > 0) {
          recyclerView.SmoothScrollToPosition(--currentIndex );
        }
      }

      public void ScrollRight() {
        if (currentIndex < recyclerView.GetAdapter().ItemCount - 1) {
          recyclerView.SmoothScrollToPosition(++currentIndex);
        }
      }
    }
	}
}
