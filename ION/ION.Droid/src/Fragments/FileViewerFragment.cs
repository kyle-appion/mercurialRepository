namespace ION.Droid.Fragments {
  
  using System;

  using Android.Content;
  using Android.Support.V7.Widget;
  using Android.OS;
  using Android.Views;
	using Android.Widget;

	using Appion.Commons.Util;

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
        if (__adapter != null) {
          __adapter.onFileClicked -= NotifyFileClicked;
					__adapter.onFolderClicked -= NotifyFolderClicked;
        }

        __adapter = value;

        if (__adapter != null) {
					__adapter.onFileClicked += NotifyFileClicked;
					__adapter.onFolderClicked += NotifyFolderClicked;

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

		private class FileAdapter : SwipableRecyclerViewAdapter {
			/// <summary>
			/// The event handler that is used to notify listeners of when a file row is clicked.
			/// </summary>
			public OnFileClicked onFileClicked;
			/// <summary>
			/// The event handler that is used to notify listeners of when a folder row is clicked.
			/// </summary>
			public OnFolderClicked onFolderClicked;


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
			/// Gets the type of the item view.
			/// </summary>
			/// <returns>The item view type.</returns>
			/// <param name="position">Position.</param>
			public override int GetItemViewType(int position) {
				var record = records[position];

				if (record is FileRecord) {
					return (int)EViewType.File;
				} else if (record is FolderRecord) {
					return (int)EViewType.Folder;
				} else {
					throw new Exception("Unknown record type: " + record);
				}
			}

			/// <summary>
			/// Raises the create view holder event.
			/// </summary>
			/// <param name="parent">Parent.</param>
			/// <param name="viewType">View type.</param>
			public override SwipableViewHolder OnCreateSwipableViewHolder(ViewGroup parent, int viewType) {
				var li = LayoutInflater.From(parent.Context);

				switch ((EViewType)viewType) {
					case EViewType.File:
						var ret = new FileHolder(parent, this, cache);
						ret.button.SetText(Resource.String.delete);
						return ret;
					case EViewType.Folder:
						return new FolderHolder(parent, this, cache);
					default:
						throw new Exception("Cannot create view holder for: " + ((EViewType)viewType));
				}
			}

			public override bool IsSwipable(int position) {
				var record = records[position];
				return record.viewType == (int)EViewType.File;
			}

/*
			public override Action GetViewHolderSwipeAction(int index) {
				return () => {
					switch ((EViewType)records[index].viewType) {
						case EViewType.File:
							var fileRecord = (FileRecord)records[index];
							fileRecord.file.Delete();
							records.RemoveAt(index);
							NotifyItemRemoved(index);
							break;
					}
				};
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

			/// <summary>
			/// Notifies that a file was clicked.
			/// </summary>
			/// <param name="file">File.</param>
			internal void NotifyFileClicked(IFile file) {
				if (onFileClicked != null) {
					onFileClicked(file);
				}
			}

			/// <summary>
			/// Notifies that a folder was clicked.
			/// </summary>
			/// <param name="folder">Folder.</param>
			internal void NotifyFolderClicked(IFolder folder) {
				if (onFolderClicked != null) {
					onFolderClicked(folder);
				}
			}

			public enum EViewType {
				File,
				Folder,
			}
		}

		private class FileRecord : SwipableRecyclerViewAdapter.IRecord {
			public int viewType { get { return (int)FileAdapter.EViewType.File; }
			}

			public IFile file { get; set; }

			public FileRecord(IFile file) {
				this.file = file;
			}
		}

		private class FolderRecord : SwipableRecyclerViewAdapter.IRecord {
			public int viewType { get { return (int)FileAdapter.EViewType.Folder; } }

			public IFolder folder { get; set; }

			public FolderRecord(IFolder folder) {
				this.folder = folder;
			}
		}

		private class FileHolder : SwipableViewHolder<FileRecord> {
			private BitmapCache cache;
			private ImageView icon;
			private TextView name;

			public FileHolder(ViewGroup parent, FileAdapter adapter, BitmapCache cache) : base(parent, Resource.Layout.list_item_file) {
				this.cache = cache;
				view.SetOnClickListener(new ViewClickAction((v) => {
					adapter.NotifyFileClicked(t.file);
				}));

				icon = view.FindViewById<ImageView>(Resource.Id.icon);
				name = view.FindViewById<TextView>(Resource.Id.name);
			}

			public override void OnBindTo() {
				icon.SetImageBitmap(cache.GetBitmap(t.file.GetIcon()));
				name.Text = t.file.name;
			}
		}

		private class FolderHolder : SwipableViewHolder<FolderRecord> {
			private BitmapCache cache;
			private ImageView icon;
			private TextView name;

			public FolderHolder(ViewGroup parent, FileAdapter adapter, BitmapCache cache) : base(parent, Resource.Layout.list_item_file) {
				this.cache = cache;
				view.SetOnClickListener(new ViewClickAction((v) => {
					adapter.NotifyFolderClicked(t.folder);
				}));

				icon = view.FindViewById<ImageView>(Resource.Id.icon);
				name = view.FindViewById<TextView>(Resource.Id.name);
			}

			public void BindTo(FolderRecord folder) {
				this.record = folder;

				icon.SetImageBitmap(cache.GetBitmap(t.folder.GetIcon()));
				name.Text = t.folder.name;
			}
		}
  }
}

