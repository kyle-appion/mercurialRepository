﻿namespace ION.Droid.Activity {

  using System.Collections.Generic;

  using Android.App;
  using Android.Content;
  using Android.Content.PM;
  using Android.Graphics;
  using Android.OS;
  using Android.Views;
  using Android.Widget;

  using Android.Support.V4.View;
  using Android.Support.V13.App;


	using Appion.Commons.Util;

  using ION.Core.App;
  using ION.Core.Fluids;

	using ION.Droid.Dialog;
  using ION.Droid.Fragments;
	using ION.Droid.Views;

	[Activity(Label = "@string/fluid_manager", Theme = "@style/TerminalActivityTheme", ScreenOrientation=ScreenOrientation.Portrait)]      
	public class FluidManagerActivity : Activity, ViewPager.IOnPageChangeListener {

    /// <summary>
    /// The key that will retrieve the string name for the fluid that is
    /// to be marked as selected.
    /// </summary>
    public const string EXTRA_SELECTED = "ion.droid.activity.extra.fluid_manager.SELECTED";

    public string selectedFluid { 
      get {
        return __selectedFluid;
      }
      set {
        __selectedFluid = value;

        if (__selectedFluid != null) {
          colorView.SetBackgroundColor(new Color(ion.fluidManager.GetFluidColor(__selectedFluid)));
          fluidNameView.Text = __selectedFluid;
        } else {
          colorView.SetBackgroundColor(Resource.Color.white.AsResourceColor(this));
          fluidNameView.Text = GetString(Resource.String.fluid_nothing_selected);
        }

        preferred.selectedFluid = __selectedFluid;
        library.selectedFluid = __selectedFluid;
      }
    } string __selectedFluid;

    private View colorView { get; set; }
    private TextView fluidNameView { get; set; }
    private ViewPager pagerView { get; set; }
		private Button favoritesButton;
		private Button libraryButton;

    private IION ion { get; set; }

    private FluidFragment preferred { get; set; }
    private FluidFragment library { get; set; }
    private FluidFragmentAdapter adapter { get; set; }

    // Overridden from Activity;
    protected override void OnCreate(Bundle bundle) {
      base.OnCreate(bundle);

      ActionBar.SetDisplayHomeAsUpEnabled(true);

      SetContentView(Resource.Layout.activity_fluid_manager);

      colorView = FindViewById(Resource.Id.color);
      fluidNameView = FindViewById<TextView>(Resource.Id.name);
      pagerView = FindViewById<ViewPager>(Resource.Id.content);
			pagerView.AddOnPageChangeListener(this);
			favoritesButton = FindViewById<Button>(Resource.Id.button1);
			libraryButton = FindViewById<Button>(Resource.Id.button2);

			favoritesButton.Click += (sender, e) => {
				pagerView.SetCurrentItem(0, true);
			};

			libraryButton.Click += (sender, e) => {
				pagerView.SetCurrentItem(1, true);
			};

      ion = AppState.context;
      ion.fluidManager.onFluidPreferenceChanged += OnFluidPreferenceChanged;

      preferred = new FluidFragment(ion);
      preferred.emptyText = GetString(Resource.String.fluid_empty_preferred);
      preferred.title = GetString(Resource.String.favorites);
      preferred.onFluidSelected += OnFluidSelected;

      library = new FluidFragment(ion);
      library.emptyText = GetString(Resource.String.fluid_empty_library);
      library.title = GetString(Resource.String.library);
      library.onFluidSelected += OnFluidSelected;

      selectedFluid = Intent.GetStringExtra(EXTRA_SELECTED);
      if (selectedFluid == null) {
        selectedFluid = ion.fluidManager.lastUsedFluid.name;
      }

      adapter = new FluidFragmentAdapter(FragmentManager, new FluidFragment[] {
        preferred,
        library,
      });

      pagerView.Adapter = adapter;

			var help = FindViewById(Resource.Id.help);
			help.SetOnClickListener(new ViewClickAction((view) => {
				var adb = new IONAlertDialog(this);
				adb.SetTitle(Resource.String.fluid_safety_help);
				adb.SetMessage(Resource.String.fluid_safety_help_descriptions);
				adb.SetPositiveButton(Resource.String.ok, (s, e2) => {
				});
				adb.Show();
			}));
    }

    // Overridden from Activity
    protected override void OnResume() {
      base.OnResume();
      Invalidate();
			OnPageSelected(0);
    }

    // Overridden from Activity
    public override bool OnMenuItemSelected(int featureId, IMenuItem item) {
      switch (item.ItemId) {
        case Android.Resource.Id.Home:
          var res = new Intent();
          res.PutExtra(EXTRA_SELECTED, selectedFluid);
          SetResult(Result.Ok, res);
          Finish();
          return true;
        default:
          return base.OnMenuItemSelected(featureId, item);          
      }
    }

		public void OnPageScrollStateChanged(int state) {
		}

		public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels) {
		}

		public void OnPageSelected(int position) {
			if (position == 0) {
				favoritesButton.SetBackgroundResource(Resource.Drawable.tab_selected_ion_action_bar);
				libraryButton.SetBackgroundResource(Resource.Drawable.tab_unselected_ion_action_bar);
			} else if (position == 1) {
				favoritesButton.SetBackgroundResource(Resource.Drawable.tab_unselected_ion_action_bar);
				libraryButton.SetBackgroundResource(Resource.Drawable.tab_selected_ion_action_bar);
			}
		}

    /// <summary>
    /// Called whan a fluid is selected by on of the fragments.
    /// </summary>
    /// <param name="fragment">Fragment.</param>
    /// <param name="fluidName">Fluid name.</param>
    private void OnFluidSelected(FluidFragment fragment, string fluidName) {
      selectedFluid = fluidName;
      Invalidate();
    }

    /// <summary>
    /// Called when a fluid's preferredness state changes.
    /// </summary>
    private void OnFluidPreferenceChanged(IFluidManager fluidManager, string fluidName) {
      Invalidate();
    }

    /// <summary>
    /// Invalidates the 
    /// </summary>
    private void Invalidate() {
      var plist = new List<string>(ion.fluidManager.preferredFluids);
      var llist = new List<string>(ion.fluidManager.GetAvailableFluidNames());

			plist.Sort(new AlphabeticalStringComparer());
			llist.Sort(new AlphabeticalStringComparer());


      preferred.fluidList = plist;
      library.fluidList = llist;
    }
  }

  /// <summary>
  /// The adapter that will allow the swiping between the fluid lists.
  /// </summary>
  internal class FluidFragmentAdapter : FragmentStatePagerAdapter {

    // Overridden from FragmentStatePagerAdapter
    public override int Count {
      get {
        return fragments.Length;
      }
    }
    
    private FluidFragment[] fragments { get; set; }

    public FluidFragmentAdapter(FragmentManager fm, FluidFragment[] fragments) : base(fm) {
      this.fragments = fragments;
    }


    // Overridden from FragmentStatePagerAdapter
    public override Fragment GetItem(int position) {
      return fragments[position];
    }

    // Overridden from GetPageTitle
    public override Java.Lang.ICharSequence GetPageTitleFormatted(int position) {
      return new Java.Lang.String(fragments[position].title);
    }
  }
}

