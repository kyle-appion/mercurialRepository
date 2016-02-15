namespace ION.Core.Util {

  using System;


  /// <summary>
  /// A simple contract defining a fil
  /// </summary>
  public interface IFilter<T> {
    /// <summary>
    /// Queries whether or not the given item matches the filter.
    /// </summary>
    /// <param name="t">T.</param>
    bool Matches(T t);
  }

  /// <summary>
  /// A collection of filters that are matched against by all the test items.
  /// </summary>
  public class AndFilterCollection<T> : IFilter<T> { 
    /// <summary>
    /// The filters the item must match.
    /// </summary>
    /// <value>The base filter.</value>
    private IFilter<T>[] filters { get; set; }

    public AndFilterCollection(params IFilter<T>[] filters) {
      this.filters = filters;
    }

    /// <summary>
    /// Queries whether or not the given item matches the filter.
    /// </summary>
    /// <param name="t">T.</param>
    public bool Matches(T t) {
      foreach (var f in filters) {
        if (!f.Matches(t)) {
          return false;
        }
      }
      return true;
    }
  }

  /// <summary>
  /// A filter that is true if any of the filters within the collection are true.
  /// </summary>
  public class OrFilterCollection<T> : IFilter<T> { 
    private IFilter<T>[] filters { get; set; }

    public OrFilterCollection(params IFilter<T>[] filters) {
      this.filters = filters;
    }

    /// <summary>
    /// Queries whether or not the given item matches the filter.
    /// </summary>
    /// <param name="t">T.</param>
    public bool Matches(T t) {
      foreach (var f in filters) {
        if (f.Matches(t)) { 
          return true;
        }
      }
      return false;
    }
  }

  /// <summary>
  /// A Filter that always matches an item.
  /// </summary>
  public class YesFilter<T> : IFilter<T> {
    /// <summary>
    /// Queries whether or not the given item matches the filter.
    /// </summary>
    /// <param name="t">T.</param>
    public bool Matches(T t) {
      return true;
    }
  }

  public class NoFilter<T> : IFilter<T> {
    /// <summary>
    /// Queries whether or not the given item matches the filter.
    /// </summary>
    /// <param name="t">T.</param>
    public bool Matches(T t) {
      return false;
    }
  }
}

