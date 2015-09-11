using System;

namespace ION.Core.Util {
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
  /// A collection of filters that are matched against by test items.
  /// </summary>
  public class FilterCollection<T> : IFilter<T> { 
    /// <summary>
    /// The filters the item must match.
    /// </summary>
    /// <value>The base filter.</value>
    private IFilter<T>[] filters { get; set; }

    public FilterCollection(params IFilter<T>[] filters) {
      this.filters = filters;
    }

    // Overridden from IFilter
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
  /// A Filter that always matches an item.
  /// </summary>
  public class YesFilter<T> : IFilter<T> {
    // Overridden from IFilter
    public bool Matches(T t) {
      return true;
    }
  }
}

