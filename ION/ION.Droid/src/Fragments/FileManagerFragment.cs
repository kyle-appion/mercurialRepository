namespace ION.Droid.Fragments {
  
  using System;

  using Android.Content;
  using Android.Support.V7.Widget;
  using Android.OS;
  using Android.Views;

  using ION.Core.IO;
  using ION.Core.Util;

  using ION.Droid.Widgets.Adapters.FileManager;

  /// <summary>
  /// The file manager fragment is a simple fragment that will act as a file manager/explorer for the app.
  /// </summary>
  public class FileManagerFragment : IONFragment {
    /// <summary>
    /// The event handler that is used to notify listeners of when a file row is clicked.
    /// </summary>
    public event FileManagerAdapter.OnFileClicked onFileClicked;
    /// <summary>
    /// The event handler that is used to notify listeners of when a folder row is clicked.
    /// </summary>
    public event FileManagerAdapter.OnFolderClicked onFolderClicked;

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
        Log.D(this, "Folder is: " + __folder.fullPath);
        adapter = new FileManagerAdapter(cache, ion.fileManager, __folder);
      }
    } IFolder __folder;

    /// <summary>
    /// The recycler view that will list the files.
    /// </summary>
    private RecyclerView list;
    /// <summary>
    /// The adapter that will provide views the file manager.
    /// </summary>
    private FileManagerAdapter adapter {
      get {
        return __adapter;
      }
      set {
        if (__adapter != null) {
          __adapter.onFileClicked -= OnFileClicked;
          __adapter.onFolderClicked -= OnFolderClicked;
        }

        __adapter = value;

        if (__adapter != null) {
          __adapter.onFileClicked += OnFileClicked;
          __adapter.onFolderClicked += OnFolderClicked;

          if (list != null) {
            list.SetAdapter(adapter);
          }
        }
      }
    } FileManagerAdapter __adapter;

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

      if (folder != null) {
        adapter = new FileManagerAdapter(cache, ion.fileManager, folder);
        list.SetAdapter(adapter);
      }
    }

    /// <summary>
    /// Called when an adapter's file row is clicked.
    /// </summary>
    /// <param name="file">File.</param>
    private void OnFileClicked(IFile file) {
      if (onFileClicked != null) {
        onFileClicked(file);
      }
    }

    /// <summary>
    /// Called when an adapter's folder row is clicked.
    /// </summary>
    /// <param name="file">File.</param>
    public void OnFolderClicked(IFolder folder) {
      if (onFolderClicked != null) {
        onFolderClicked(folder);
      }
    }
  }
}

