namespace ION.Droid.Fragments {
  
  using System;

  using Android.Support.V7.Widget;
  using Android.OS;
  using Android.Views;
	using Android.Widget;

  using ION.Core.IO;

	using ION.Droid.IO;
	using ION.Droid.Util;
	using ION.Droid.Views;
	using ION.Droid.Widgets.RecyclerViews;

  /// <summary>
  /// The file manager fragment is a simple fragment that will act as a file manager/explorer for the app.
  /// </summary>
  public class FileViewerFragment : IONFragment {
		/// <summary>
		/// The delegate that is called when a file is clicked in the fragment.
		/// </summary>
		public delegate void OnFileClicked(IFile file);
		/// <summary>
		/// The delegate that is called when a folder is clicked in the fragment.
		/// </summary>
		public delegate void OnFolderClicked(IFolder folder);

    /// <summary>
    /// The event handler that is used to notify listeners of when a file row is clicked.
    /// </summary>
    public OnFileClicked onFileClicked;
    /// <summary>
    /// The event handler that is used to notify listeners of when a folder row is clicked.
    /// </summary>
    public OnFolderClicked onFolderClicked;

    /// <summary>
    /// The current folder that the fragment will display.
    /// </summary>
    /// <value>The folder.</value>
    public IFolder folder {
      get {
        return __folder;
      }
      set {
        __folder = value;
				if (adapter != null) {
					adapter.SetFolder(__folder);
				}
      }
    } IFolder __folder;

		/// <summary>
		/// The filter that is applied to the files.
		/// </summary>
		/// <value>The filter.</value>
		public FileExtensionFilter filter {
			get {
				return __filter;
			}

			set {
				__filter = value;

				if (__adapter != null) {
					adapter.SetFilter(__filter);
				}
			}
		} FileExtensionFilter __filter;

    /// <summary>
    /// The recycler view that will list the files.
    /// </summary>
    private RecyclerView list;
    /// <summary>
    /// The adapter that will provide views the file manager.
    /// </summary>
    private FileAdapter adapter {
      get {
        return __adapter;
      }
      set {
        __adapter = value;

        if (__adapter != null) {
          if (list != null) {
            list.SetAdapter(adapter);
          }
        }
      }
    } FileAdapter __adapter;

    /// <Docs>The LayoutInflater object that can be used to inflate
    ///  any views in the fragment,</Docs>
    /// <param name="savedInstanceState">If non-null, this fragment is being re-constructed
    ///  from a previous saved state as given here.</param>
    /// <returns>To be added.</returns>
    /// <summary>
    /// Raises the create view event.
    /// </summary>
    /// <param name="inflater">Inflater.</param>
    /// <param name="container">Container.</param>
   public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
      var ret = inflater.Inflate(Resource.Layout.fragment_file_manager, container, false);

      list = ret.FindViewById<RecyclerView>(Resource.Id.list);
      list.SetLayoutManager(new LinearLayoutManager(Activity));

      return ret;
    }

    /// <Docs>If the fragment is being re-created from
    ///  a previous saved state, this is the state.</Docs>
    /// <summary>
    /// Raises the activity created event.
    /// </summary>
    /// <param name="savedInstanceState">Saved instance state.</param>
    public override void OnActivityCreated(Bundle savedInstanceState) {
      base.OnActivityCreated(savedInstanceState);

			adapter = new FileAdapter(cache);
			adapter.SetFilter(filter);

			list.SetAdapter(adapter);

      if (folder != null) {
				adapter.SetFolder(folder);
			}
    }

		public override void OnResume() {
			base.OnResume();

			adapter.onItemClicked += OnItemClicked;
		}

		public override void OnPause() {
			base.OnPause();

			adapter.onItemClicked -= OnItemClicked;
		}

		private void OnItemClicked(int position) {
			var r = adapter[position];
			if (r is FileRecord) {
				var fr = r as FileRecord;
				NotifyFileClicked(fr.data);
			} else if (r is FolderRecord) {
				var fr = r as FolderRecord;
				NotifyFolderClicked(fr.data);
			}
		}

    /// <summary>
    /// Called when an adapter's file row is clicked.
    /// </summary>
    /// <param name="file">File.</param>
    private void NotifyFileClicked(IFile file) {
      if (onFileClicked != null) {
        onFileClicked(file);
      }
    }

    /// <summary>
    /// Called when an adapter's folder row is clicked.
    /// </summary>
    /// <param name="file">File.</param>
    public void NotifyFolderClicked(IFolder folder) {
      if (onFolderClicked != null) {
        onFolderClicked(folder);
      }
    }

		private class FileAdapter : RecordAdapter {

			/// <summary>
			/// The cache that will hold the file icons.
			/// </summary>
			private BitmapCache cache; 

			/// <summary>
			/// The folder that the adapter is presenting the contents of.
			/// </summary>
			private IFolder folder;
			/// <summary>
			/// The filter that that will match to the files in the folder.
			/// </summary>
			/// <param name="cache">Cache.</param>
			private FileExtensionFilter filter;

			public FileAdapter(BitmapCache cache) {
				this.cache = cache;
			}

			/// <summary>
			/// Raises the create view holder event.
			/// </summary>
			/// <param name="parent">Parent.</param>
			/// <param name="viewType">View type.</param>
			public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
				var li = LayoutInflater.From(parent.Context);

				switch ((EViewType)viewType) {
					case EViewType.File:
						return new FileHolder(recyclerView as SwipeRecyclerView, cache);
					case EViewType.Folder:
						return new FolderHolder(recyclerView as SwipeRecyclerView, cache);
					default:
						throw new Exception("Cannot create view holder for: " + ((EViewType)viewType));
				}
			}
/*
			public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position) {
				switch ((EViewType)GetItemViewType(position)) {
					case EViewType.File: {
						var file = holder as FileHolder;
						file.foreground.SetOnClickListener(new ViewClickAction((view) => {
							NotifyFileClicked(file.record.data);
						}));
					} break; // EViewType.File

					case EViewType.Folder: {
						var file = holder as FileHolder;
						file.foreground.SetOnClickListener(new ViewClickAction((view) => {
							NotifyFileClicked(file.record.data);
						}));
					} break; // EViewType.Folder
				}
			}
*/

			/// <summary>
			/// Sets the folder that the adaper will display.
			/// </summary>
			/// <param name="folder">Folder.</param>
			public void SetFolder(IFolder folder) {
				this.folder = folder;

				records.Clear();

				foreach (var f in folder.GetFolderList()) {
					records.Add(new FolderRecord(f));
				}

				foreach (var f in folder.GetFileList()) {
					if (filter == null || filter.Matches(f)) {
						records.Add(new FileRecord(f));
					}
				}

				NotifyDataSetChanged();
			}

			public void SetFilter(FileExtensionFilter filter) {
				this.filter = filter;

				if (folder != null) {
					SetFolder(folder);
				}
			}

			public enum EViewType {
				File,
				Folder,
			}
		}

		private class FileRecord : RecordAdapter.Record<IFile> {
			public override int viewType { get { return (int)FileAdapter.EViewType.File; } }
			public FileRecord(IFile file) : base(file) { }
		}

		private class FolderRecord : RecordAdapter.Record<IFolder> {
			public override int viewType { get { return (int)FileAdapter.EViewType.Folder; } }
			public FolderRecord(IFolder folder) : base(folder) { }
		}

		private class FileHolder : RecordAdapter.SwipeRecordViewHolder<FileRecord> {
			private BitmapCache cache;
			private ImageView icon;
			private TextView name;

			public FileHolder(SwipeRecyclerView rv, BitmapCache cache) : base(rv, Resource.Layout.list_item_file, Resource.Layout.list_item_button) {
				this.cache = cache;

				icon = foreground.FindViewById<ImageView>(Resource.Id.icon);
				name = foreground.FindViewById<TextView>(Resource.Id.name);

				var button = background as TextView;
				button.SetText(Resource.String.remove);
				button.SetOnClickListener(new ViewClickAction((view) => {
					record.data.Delete();
				}));
			}

			public override void Invalidate() {
				icon.SetImageBitmap(cache.GetBitmap(record.data.GetIcon()));
				name.Text = record.data.name;
			}
		}

		private class FolderHolder : RecordAdapter.SwipeRecordViewHolder<FolderRecord> {
			private BitmapCache cache;
			private ImageView icon;
			private TextView name;

			public FolderHolder(SwipeRecyclerView rv, BitmapCache cache) : base(rv, Resource.Layout.list_item_file, Resource.Layout.list_item_button) {
				this.cache = cache;

				icon = foreground.FindViewById<ImageView>(Resource.Id.icon);
				name = foreground.FindViewById<TextView>(Resource.Id.name);

				var button = background as TextView;
				button.SetText(Resource.String.remove);
				button.SetOnClickListener(new ViewClickAction((view) => {
					record.data.Delete();
				}));
			}

			public void BindTo(FolderRecord folder) {
				this.record = folder;

				icon.SetImageBitmap(cache.GetBitmap(record.data.GetIcon()));
				name.Text = record.data.name;
			}
		}
  }
}

