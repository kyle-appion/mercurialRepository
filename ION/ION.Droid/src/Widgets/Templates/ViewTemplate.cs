namespace ION.Droid.Widgets.Templates {
  
  using System;

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
    public View parentView;

    public ViewTemplate(View view) {
      parentView = view;
    }

    /// <summary>
    /// Binds the view template to the given data.
    /// </summary>
    /// <param name="t">T.</param>
    public abstract void Bind(T t);

    /// <summary>
    /// Invalidates the view within the template.
    /// </summary>
    public virtual void Invalidate() {
    }

    /// <summary>
    /// Informs the view template that it should unbind itself from its data source.
    /// </summary>
    public abstract void Unbind();
  }
}

