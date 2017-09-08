namespace ION.Droid.Fragments._Analyzer {
  
  using Android.Views;

  /// <summary>
  /// A view template is a structure that will manage the content of similar views. This allows the views to change,
  /// but the actual data and content management to remain the same. A View Template operates extremely similar to 
  /// RecyclerView.ViewHolder. ViewTemplates are declarations of a resource/view mapping contract. You should list the
  /// views and their id for each template.
  /// </summary>
  public abstract class ViewTemplate<T> {
    /// <summary>
    /// The parent view that this view template is populating.
    /// </summary>
    public View parentView { get; private set; } 
    /// <summary>
    /// The item that the view template is bound to.
    /// </summary>
    /// <value>The item.</value>
    public T item { get; private set; }

    public ViewTemplate(View view) {
      parentView = view;
    }

    /// <summary>
    /// Binds the template to its item. Calls invalidate after the bind is complete.
    /// </summary>
    /// <param name="t">T.</param>
    public void Bind(T t) {
      if (item != null) {
        Unbind();
      }

      item = t;

      OnBind(t);

      Invalidate();
    }

    /// <summary>
    /// Unbinds the template form its item.
    /// </summary>
    public void Unbind() {
      if (item != null) {
        OnUnbind();
        item = default(T);
      }
    }

    /// <summary>
    /// Invalidates the view within the template.
    /// </summary>
    public virtual void Invalidate() {
    }

    /// <summary>
    /// Binds the view template to the given data.
    /// </summary>
    /// <param name="t">T.</param>
    protected abstract void OnBind(T t);

    /// <summary>
    /// Informs the view template that it should unbind itself from its data source.
    /// </summary>
    protected abstract void OnUnbind();
  }
}

