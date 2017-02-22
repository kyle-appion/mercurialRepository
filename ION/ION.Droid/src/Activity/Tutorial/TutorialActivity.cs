namespace ION.Droid.Activity.Tutorial {

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.IO;
	using System.Text;

	using Android.App;
	using Android.Content;
	using Android.Content.PM;
	using Android.OS;
	using Android.Runtime;
	using Android.Support.Design.Widget;
	using Android.Support.V4.View;
	using Android.Support.V13.App;
	using Android.Views;
	using Android.Widget;

	using Appion.Commons.Util;

	using ION.Core.App;

	using ION.Droid.Util;
	using ION.Droid.Views;


	[Activity(Label = "@string/preferences_help_walkthrough", Theme = "@style/TerminalActivityTheme", ScreenOrientation=ScreenOrientation.Portrait)]
	public class TutorialActivity : IONActivity, ViewPager.IOnPageChangeListener {

		private ViewPager pager;
		private TextView tabLayout;
		private Adapter adapter;

		private View left;
		private View right;

		protected override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);

			ActionBar.SetDisplayHomeAsUpEnabled(true);

			SetContentView(Resource.Layout.activity_tutorial);

			left = FindViewById(Resource.Id.left);
			left.SetOnClickListener(new ViewClickAction((v) => {
				if (pager.CurrentItem - 1 < 0) {
					pager.SetCurrentItem(adapter.Count - 1, true);
				} else {
					pager.SetCurrentItem(pager.CurrentItem - 1, true);
				}	
			}));

			right = FindViewById(Resource.Id.right);
			right.SetOnClickListener(new ViewClickAction((v) => {
				if (pager.CurrentItem + 1 >= adapter.Count) {
					pager.SetCurrentItem(0, true);
				} else {
					pager.SetCurrentItem(pager.CurrentItem + 1, true);
				}					
			}));

			FindViewById(Resource.Id.button).SetOnClickListener(new ViewClickAction((view) => {
				Finish();
			}));

			FindViewById(Resource.Id.help).SetOnClickListener(new ViewClickAction((v) => {
				ion.SendAppSupportEmail(this);
			}));

			pager = FindViewById<ViewPager>(Resource.Id.content);
			tabLayout = FindViewById<TextView>(Resource.Id.tab_1);

			adapter = new Adapter(FragmentManager, cache);
/*
			// WELCOME
			adapter.AddPage(new TutorialPage() {
				titleResource = Resource.String.tutorial_welcome,
				imageResource = Resource.Drawable.img_logo_appionblack,
				contentResource = Resource.String.tutorial_introduction,
				xPercent = -1f,
				yPercent = -1f,
			});
*/

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

			pager.Adapter = adapter;
			pager.AddOnPageChangeListener(this);

			pager.CurrentItem = 0;
			left.Visibility = ViewStates.Invisible;

			tabLayout.Text = 1 + " / " + adapter.Count;
		}

		// Overridden from Activity
		protected override void OnPause() {
			base.OnPause();
			var cb = FindViewById<CheckBox>(Resource.Id.check);
			ion.preferences.showTutorial = !cb.Checked;
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

		public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels) {
		}

		public void OnPageScrollStateChanged(int state) {
		}

		public void OnPageSelected(int position) {
			if (position == 0) {
				left.FadeOut();
			} else {
				if (left.Visibility != ViewStates.Visible) {
					left.FadeIn();
				}
			}

			if (position == adapter.Count - 1) {
				right.FadeOut();
			} else {
				if (right.Visibility != ViewStates.Visible) {
					right.FadeIn();
				}
			}

			tabLayout.Text = (position + 1) + " / " + adapter.Count;
		}

		private class Adapter : FragmentPagerAdapter {
			// Overridden from FragmentPagerAdapter
			public override int Count { get { return pages.Count; } }

			private List<TutorialPage> pages;
			private BitmapCache cache;

			public Adapter(FragmentManager fm, BitmapCache cache) : base(fm) {
				pages = new List<TutorialPage>();
				this.cache = cache;
			}

			public void AddPage(TutorialPage page) {
				pages.Add(page);
			}

			public override Fragment GetItem(int position) {
				var ret = new TutorialFragment();
				ret.page = pages[position];
				ret.cache = this.cache;
				return ret;
			}
		}
	}
}
