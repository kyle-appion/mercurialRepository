namespace ION.Core {
  /// <summary>
  /// A marker interface that marks an object as a table. This name of this table is a misnomer. The data within this
  /// interface is actually a table item, however, the contract of the item is what makes the table.
  /// </summary>
  public interface ITableRow {
    /// <summary>
    /// Queries the primary id of the table item.
    /// </summary>
    /// <value>The identifier.</value>
    int _id { get; set; }
  }
}

