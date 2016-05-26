namespace ION.IOS.ViewController.GaugeTesting {
  
  using System;

  using CoreGraphics;
  using Foundation;
  using UIKit;

  using ION.Core.Util;

  public class ScrollableTableCollectionView : UICollectionView {
    internal const string CELL_HEADER = "header";
    internal const string CELL_CONTENT = "content";

    /// <summary>
    /// The table that the collection view will work with.
    /// </summary>
    /// <value>The table.</value>
    public ITable table { 
      get {
        return __table;
      }
      set {
        __table = value;
        layout.table = value;
        this.DataSource = new ScrollableTableSource(table);
        ReloadData();
      }
    } ITable __table;

    /// <summary>
    /// The layout that will be used for the collection view.
    /// </summary>
    private ScrollableTableCollectionLayout layout;
    
    public ScrollableTableCollectionView(CGRect frame) : base(frame, new ScrollableTableCollectionLayout()) {
      layout = CollectionViewLayout as ScrollableTableCollectionLayout;
      Initialize();
    }

    /// <summary>
    /// Initializes the collection view.
    /// </summary>
    private void Initialize() {
      RegisterClassForCell(typeof(HeaderCell), CELL_HEADER);
      RegisterClassForCell(typeof(ContentCell), CELL_CONTENT);
    }
  }

  /// <summary>
  /// The view source that will provide table cells to the scrollable table collection view.
  /// </summary>
  internal class ScrollableTableSource : UICollectionViewDataSource {

    private ITable table; 

    public ScrollableTableSource(ITable table) {
      this.table = table;
    }

    /// <summary>
    /// Numbers the of sections.
    /// </summary>
    /// <returns>The of sections.</returns>
    /// <param name="collectionView">Collection view.</param>
    public override nint NumberOfSections(UICollectionView collectionView) {
      return table.columnCount + (table.hasColumnHeaders ? 1 : 0);
    }

    /// <summary>
    /// Gets the items count.
    /// </summary>
    /// <returns>The items count.</returns>
    /// <param name="collectionView">Collection view.</param>
    /// <param name="section">Section.</param>
    public override nint GetItemsCount(UICollectionView collectionView, nint section) {
      return table.rowCount + (table.hasRowHeaders ? 1 : 0);
    }

    /// <summary>
    /// Queries the cell that will be used for the table.
    /// </summary>
    /// <returns>The cell.</returns>
    /// <param name="collectionView">Collection view.</param>
    /// <param name="indexPath">Index path.</param>
    public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath) {
      if (indexPath.Section == 0) {
        var cell = collectionView.DequeueReusableCell(ScrollableTableCollectionView.CELL_HEADER, indexPath) as HeaderCell;
        cell.label.Text = table.columnHeaders[indexPath.Row];
        return cell;
      } else if (indexPath.Row == 0) {
        var cell = collectionView.DequeueReusableCell(ScrollableTableCollectionView.CELL_HEADER, indexPath) as HeaderCell;
        cell.label.Text = table.rowHeaders[indexPath.Section];
        return cell;
      } else {
        var cell = collectionView.DequeueReusableCell(ScrollableTableCollectionView.CELL_CONTENT, indexPath) as ContentCell;
        cell.label.Text = table[indexPath.Section, indexPath.Row];
        return cell;
      }
    }
  }

  /// <summary>
  /// The layout that will layout a table in a collection view.
  /// </summary>
  internal class ScrollableTableCollectionLayout : UICollectionViewLayout {
    /// <summary>
    /// The table that is to be lain out.
    /// </summary>
    public ITable table { get; set; }
    private UICollectionViewLayoutAttributes[,] attributes;

    private float cellWidth;
    private float cellHeight;
    private float cellPadding;

    public ScrollableTableCollectionLayout() {
    }

    public ScrollableTableCollectionLayout(ITable table, ScrollableTableCollectionLayout layout) {
      table = layout.table;
      cellWidth = layout.cellWidth;
      cellHeight = layout.cellHeight;
      cellPadding = layout.cellHeight;
    }

    /// <summary>
    /// Prepares the layout.
    /// </summary>
    public override void PrepareLayout() {
      if (CollectionView?.NumberOfSections() == 0) {
        return;
      }
    }

    /// <summary>
    /// Layouts the attributes for item.
    /// </summary>
    /// <returns>The attributes for item.</returns>
    /// <param name="indexPath">Index path.</param>
    public override UICollectionViewLayoutAttributes LayoutAttributesForItem(NSIndexPath indexPath) {
      var ret = UICollectionViewLayoutAttributes.CreateForCell(indexPath);

      ret.Size = new CGSize(cellWidth, cellHeight);

      var center = new CGPoint();
      center.X = cellPadding * indexPath.Section * cellWidth * indexPath.Section;
      center.Y = cellPadding * indexPath.Row * cellHeight * indexPath.Row;
      ret.Center = center;

      return ret;
    }

    /// <summary>
    /// Layouts the attributes for elements in rect.
    /// </summary>
    /// <returns>The attributes for elements in rect.</returns>
    /// <param name="rect">Rect.</param>
    public override UICollectionViewLayoutAttributes[] LayoutAttributesForElementsInRect(CGRect rect) {
      int coff = table.hasColumnHeaders ? 1 : 0;
      int roff = table.hasRowHeaders ? 1 : 0;
      int cols = table.columnCount + coff;
      int rows = table.rowCount + roff;
      
      var attrs = new UICollectionViewLayoutAttributes[cols * rows];

      try {

      for (int c = coff; c < cols; c++) {
        for (int r = roff; r < rows; r++) {
          attrs[r] = LayoutAttributesForItem(NSIndexPath.FromItemSection(r, c));
        }
      }
      } catch (Exception e) {
        Log.E(this, "Crash!", e);
      }

      return attrs;
    }
  }

  /// <summary>
  /// The cell that is used to display the headers within the table.
  /// </summary>
  internal class HeaderCell : UICollectionViewCell {
    public UILabel label { get; internal set; }

    public HeaderCell(CGRect frame) : base(frame) {
      label = new UILabel(frame);
      label.Font = UIFont.FromName("TrebuchetMS-Bold", 18);
    }
  }

  /// <summary>
  /// The cell that is used to display the content within the table.
  /// </summary>
  internal class ContentCell : UICollectionViewCell {
    public UILabel label { get; internal set; }

    public ContentCell(CGRect frame) : base(frame) {
      label = new UILabel(frame);
      label.Font = UIFont.FromName("TrebuchetMS", 12);
    }
  }
}

