namespace Appion.Commons.Util {

  using System;

  public interface IFilter {
    bool Matches(object o);
  }

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

  public abstract class AbstractFilter<T> : IFilter<T> {
    /// <summary>
    /// Queries whether or not the given item matches the filter.
    /// </summary>
    /// <param name="t">T.</param>
    /// <param name="o">O.</param>
    public bool Matches(object o) {
      if (o is T) {
        return Matches((T)o);
      } else {
        return false;
      }
    }
    /// <summary>
    /// Queries whether or not the given item matches the filter.
    /// </summary>
    /// <param name="t">T.</param>
    public abstract bool Matches(T t);
  }

  /// <summary>
  /// A collection of filters that are matched against by all the test items.
  /// </summary>
  public class AndFilterCollection<T> : AbstractFilter<T> { 
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
    public override bool Matches(T t) {
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
  public class OrFilterCollection<T> : AbstractFilter<T> { 
    private IFilter<T>[] filters { get; set; }

    public OrFilterCollection(params IFilter<T>[] filters) {
      this.filters = filters;
    }

    /// <summary>
    /// Queries whether or not the given item matches the filter.
    /// </summary>
    /// <param name="t">T.</param>
    public override bool Matches(T t) {
      foreach (var f in filters) {
        if (f.Matches(t)) { 
          return true;
        }
      }
      return false;
    }
  }

  /// <summary>
  /// A filter that will always return true.
  /// </summary>
  public class YesFilter : IFilter {
    /// <summary>
    /// Always returns true;
    /// </summary>
    /// <param name="o">O.</param>
    public bool Matches(object o) {
      return true;
    }
  }

  /// <summary>
  /// A Filter that always matches an item.
  /// </summary>
  public class YesFilter<T> : AbstractFilter<T> {
    /// <summary>
    /// Queries whether or not the given item matches the filter.
    /// </summary>
    /// <param name="t">T.</param>
    public override bool Matches(T t) {
      return true;
    }
  }

  /// <summary>
  /// A filter that always returns false.
  /// </summary>
  public class NoFilter : IFilter {
    /// <summary>
    /// Queries whether or not the given item matches the filter.
    /// </summary>
    /// <param name="t">T.</param>
    public bool Matches(object o) {
      return false;
    }
  }

  /// <summary>
  /// A filter that always returns false.
  /// </summary>
  public class NoFilter<T> : AbstractFilter<T> {
    /// <summary>
    /// Queries whether or not the given item matches the filter.
    /// </summary>
    /// <param name="t">T.</param>
    public override bool Matches(T t) {
      return false;
    }
  }
}

