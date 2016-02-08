namespace ION.Droid.Widgets.Adapters.FileManager {

  using System;
  using System.Collections.Generic;

  using Android.Support.V7.Widget;
  using Android.Views;
  using Android.Widget;

  using ION.Core.App;
  using ION.Core.IO;

  using ION.Droid.IO;
  using ION.Droid.Util;
  using ION.Droid.Views;

  public class FileManagerAdapter : RecyclerView.Adapter {
    /// <summary>
    /// The delegate that is used when a file is clicked.
    /// </summary>
    public delegate void OnFileClicked(IFile file);
    /// <summary>
    /// The delegate that is used when a folder is clicked.
    /// </summary>
    public delegate void OnFolderClicked(IFolder folder);

    /// <summary>
    /// The event handler that is used to notify listeners of when a file row is clicked.
    /// </summary>
    public event OnFileClicked onFileClicked;
    /// <summary>
    /// The event handler that is used to notify listeners of when a folder row is clicked.
    /// </summary>
    public event OnFolderClicked onFolderClicked;

    /// <summary>
    /// Gets the item count.
    /// </summary>
    /// <value>The item count.</value>
    public override int ItemCount {
      get {
        return records.Count;
      }
    }

    /// <summary>
    /// The cache that will hold the file icons.
    /// </summary>
    private BitmapCache cache; 
    /// <summary>
    /// The ion instance that will provide the adapter with access to the file
    /// </summary>
    private IFileManager fileManager;

    /// <summary>
    /// The folder that the adapter is presenting the contents of.
    /// </summary>
    private IFolder folder;
    /// <summary>
    /// The files that are present in the folder.
    /// </summary>
    private List<IRecord> records = new List<IRecord>();

    public FileManagerAdapter(BitmapCache cache, IFileManager fileManager, IFolder folder) {
      this.cache = cache;
      this.fileManager = fileManager;
      this.folder = folder;

      foreach (var f in folder.GetFolderList()) {
        records.Add(new FolderRecord(f));
      }

      foreach (var f in folder.GetFileList()) {
        records.Add(new FileRecord(f));
      }
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
    public override RecyclerView.ViewHolder OnCreateViewHolder(Android.Views.ViewGroup parent, int viewType) {
      var li = LayoutInflater.From(parent.Context);

      switch ((EViewType)viewType) {
        case EViewType.File:
          return new FileHolder(li.Inflate(Resource.Layout.list_item_file, parent, false), this, cache);
        case EViewType.Folder:
          return new FolderHolder(li.Inflate(Resource.Layout.list_item_file, parent, false), this, cache);
        default:
          throw new Exception("Cannot create view holder for: " + ((EViewType)viewType));
      }
    }

    /// <summary>
    /// Raises the bind view holder event.
    /// </summary>
    /// <param name="holder">Holder.</param>
    /// <param name="position">Position.</param>
    public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position) {
      var record = records[position];

      switch (record.type) {
        case EViewType.File:
          ((FileHolder)holder).BindTo(record as FileRecord);
          return;
        case EViewType.Folder:
          ((FolderHolder)holder).BindTo(record as FolderRecord);
          return;
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

  internal interface IRecord {
    FileManagerAdapter.EViewType type { get; }
  }

  internal class FileRecord : IRecord {
    public FileManagerAdapter.EViewType type {
      get {
        return FileManagerAdapter.EViewType.File;
      }
    }

    public IFile file { get; set; }

    public FileRecord(IFile file) {
      this.file = file;
    }
  }

  internal class FolderRecord : IRecord {
    public FileManagerAdapter.EViewType type {
      get {
        return FileManagerAdapter.EViewType.Folder;
      }
    }

    public IFolder folder { get; set; }

    public FolderRecord(IFolder folder) {
      this.folder = folder;
    }
  }

  internal class FileHolder : RecyclerView.ViewHolder {
    private BitmapCache cache;
    private ImageView icon;
    private TextView name;

    private FileRecord record;

    public FileHolder(View view, FileManagerAdapter adapter, BitmapCache cache) : base(view) {
      this.cache = cache;
      view.SetOnClickListener(new ViewClickAction((v) => {
        adapter.NotifyFileClicked(record.file);
      }));

      icon = view.FindViewById<ImageView>(Resource.Id.icon);
      name = view.FindViewById<TextView>(Resource.Id.name);
    }

    public void BindTo(FileRecord file) {
      this.record = file;

      icon.SetImageBitmap(cache.GetBitmap(record.file.GetIcon()));
      name.Text = record.file.name;
    }
  }

  internal class FolderHolder : RecyclerView.ViewHolder {
    private BitmapCache cache;
    private ImageView icon;
    private TextView name;

    private FolderRecord record;

    public FolderHolder(View view, FileManagerAdapter adapter, BitmapCache cache) : base(view) {
      this.cache = cache;
      view.SetOnClickListener(new ViewClickAction((v) => {
        adapter.NotifyFolderClicked(record.folder);
      }));

      icon = view.FindViewById<ImageView>(Resource.Id.icon);
      name = view.FindViewById<TextView>(Resource.Id.name);
    }

    public void BindTo(FolderRecord folder) {
      this.record = folder;

      icon.SetImageBitmap(cache.GetBitmap(record.folder.GetIcon()));
      name.Text = record.folder.name;
    }
  }
}

