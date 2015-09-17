using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ION.Core.Database {
  /// <summary>
  ///  An interface that is used to wrap database access.
  /// </summary>
  public interface IDao<T> {
    /// <summary>
    /// Queries for an item with the given primary key.
    /// </summary>
    /// <returns>The for.</returns>
    /// <param name="primaryKey">Primary key.</param>
    Task<T> QueryForAsync(long primaryKey);
    /// <summary>
    /// Loads all of the items within the DAO. This should only be use for 
    /// </summary>
    /// <returns>The all.</returns>
    Task<List<T>> QueryForAllAsync();
    /// <summary>
    /// Queries the number of items that are present in the dao.
    /// </summary>
    /// <returns>The async.</returns>
    Task<long> CountAsync();
    /// <summary>
    /// Commits the item to the database.
    /// </summary>
    /// <param name="item">Item.</param>
    Task<bool> SaveAsync(T item);
  }
}

