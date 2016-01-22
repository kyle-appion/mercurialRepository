namespace ION.Core.Pdf {

  using System;
  using System.Collections.Generic;
  using System.IO;

  using Xfinium.Pdf;
  using Xfinium.Pdf.Graphics;
  using Xfinium.Pdf.Graphics.Text;

  using ION.Core.Report;

  /// <summary>
  /// Exports a Screenshot report as a PDF.
  /// </summary>
  public class ScreenshotReportPdfExporter {
    /// <summary>
    /// Commits the pdf to a stream.
    /// </summary>
    /// <param name="outstream">Outstream.</param>
    public static void Export(ScreenshotReport report, Stream outstream) {
      // Create the document to export.
      var document = new PdfFixedDocument();
      // Add the first page.
      var page = document.Pages.Add();

      // Init the document fonts
      var title = new PdfStandardFont(PdfStandardFontFace.HelveticaBold, 18);
      var subtitle = new PdfStandardFont(PdfStandardFontFace.HelveticaBold, 14);
      var header = new PdfStandardFont(PdfStandardFontFace.HelveticaBold, 12);
      var content = new PdfStandardFont(PdfStandardFontFace.Helvetica, 12);

      double ml, mt, mr, mb; // Margins
      ml = mt = mr = mb = 18;
      double pw = page.Width - (ml + mr);

      // Bounds
      // 0,0 is top left with positive x going right, and positive y going down. Lower
      // approaches the orgin whilst higher extends away from the orgin approaching infinity.
      var headerBounds = NewRect(ml, mt, pw, 50); // ux, uy, lx, ly
      var cx = page.Width / 2;
      var imageBounds = NewRect(ml, headerBounds.URY, cx - mr - ml, page.Height - headerBounds.Height - mt - mb);
      var contentBounds = NewRect(cx + mr, headerBounds.URY, cx - mr - ml, page.Height - headerBounds.Height - mt - mb);

      // Draw the page
      DrawHeader(report, page, headerBounds, title, subtitle);
      DrawScreenshot(report, page, imageBounds);
      DrawContent(report, page, contentBounds, header, content);

      // Commit
      document.Save(outstream);
    }


    /// <summary>
    /// Draws the header of the screenshot report.
    /// </summary>
    private static void DrawHeader(ScreenshotReport report, PdfPage page, PdfStandardRectangle bounds, PdfStandardFont title, PdfStandardFont subtitle) {
      var layout = NewLayout(PdfStringHorizontalAlign.Center);
      layout.Height = bounds.Height;
      layout.Width = bounds.Width;
      var pen = NewPen();

      var g = page.Graphics;

      var l = bounds.LLX;
      var r = bounds.URX;

      layout.X = l;
      layout.Y = bounds.LLY;
      g.DrawString(report.title, NewAppearance(title), layout);
      layout.Y += PdfTextEngine.GetStringHeight("Dg", title, layout.Width);
      g.DrawString(report.subtitle, NewAppearance(subtitle), layout);

      layout.Y += PdfTextEngine.GetStringHeight("Dg", title, layout.Width);
      g.DrawLine(pen, l, layout.Y - 1, r, layout.Y - 1);
    }

    /// <summary>
    /// Draws the screenshot png.
    /// </summary>
    /// <param name="page">Page.</param>
    /// <param name="bounds">Bounds.</param>
    private static void DrawScreenshot(ScreenshotReport report, PdfPage page, PdfStandardRectangle bounds) {
      var l = bounds.LLX;
      var t = bounds.LLY;

      var ms = new MemoryStream(report.screenshot);

      var png = new PdfPngImage(ms);

      var whP = png.Width / (double)png.Height;

      // TODO ahodder@appioninc.com: If, for some strange reason, the height and width are stupid
      // disproprtional, the bottom of the image will hang of the page.
      // What I'm saying is: this bitch needs clipping.
      var w = bounds.Width;
      var h = ((double)w * (double)png.Height) / (double)png.Width;

      page.Graphics.DrawImage(png, l, t, w, h); 
    }

    /// <summary>
    /// Draws the reports contents to the page.
    /// </summary>
    /// <param name="page">Page.</param>
    /// <param name="bounds">Bounds.</param>
    private static void DrawContent(ScreenshotReport report, PdfPage page, PdfStandardRectangle bounds, PdfStandardFont header, PdfStandardFont content) {
      var g = page.Graphics;

      var table = BuildReportContentTable(report.tableData, header, content);

      var xoffset = 0.0;
      var yoffset = 0.0;

      for (int c = 0; c < table.length; c++) {
        var col = table[c];
        yoffset = 0.0;

        for (int r = 0; r < col.length; r++) {
          var l = NewLayout();
          l.X = bounds.LLX + xoffset + (5 * c);
          l.Y = bounds.LLY + yoffset;
          l.Width = col.columnWidth;
          l.Height = col.rowHeight;

          g.DrawString(table[c][r], col.appearance, l);

          yoffset += col.rowHeight;
        }

        xoffset += col.columnWidth;
      }

      {
        var l = NewLayout();
        l.X = bounds.LLX;
        l.Y = bounds.LLY + yoffset;
        l.Width = bounds.Width;
        l.Height = bounds.URY - l.Y;
        g.DrawString(report.notes, NewAppearance(content), l);
      }

/*
      var yoffset = 0.0;
      var xoffset = 0.0;

      for (int c = 0; c < table.length; c++) {
        var col = table[c];

        if (c > 0) {
          xoffset += col.columnWidth;
        }

        var layout = NewLayout();
        layout.Width = col.columnWidth;
        layout.Y = bounds.LLY;
        layout.X = bounds.LLX + xoffset;

        for (int r = 0; r < col.length; r++) {
          g.DrawString(col[r], col.appearance, layout);
          layout.Y += col.rowHeight;
          if (layout.Y > yoffset) {
            yoffset = layout.Y;
          }
        }
      }

      var looks = NewAppearance(content);
      var l = NewLayout();
      l.X = bounds.LLX;
      l.Y = yoffset;
      l.Width = bounds.Width;
      l.Height = bounds.Height - (bounds.LLY - yoffset);

      g.DrawString(report.notes, looks, l);
*/
    }

    /// <summary>
    /// Builds a string table from the reports content.
    /// </summary>
    /// <returns>The report content table.</returns>
    private static TextTable BuildReportContentTable(string[,] table, PdfStandardFont header, PdfStandardFont content) {
      var cols = new List<TextColumn>();

      for (int c = 0; c < table.Rank; c++) {
        var rows = new List<string>();

        // TODO ahodder@appioninc.com: Possible bug.
        // Setting to c would yield unexpected results, so I hard coded 0. Fix? No. But I didn't cry myself to sleep this night.
        var len = table.GetLength(0);

        for (int r = 0; r < len; r++) {
          rows.Add(table[r,c]);
          ION.Core.Util.Log.D("SR", "Record[" + r + ", " + c + "] = " + table[r, c]);
        }

        var app = NewAppearance(content);
        if (c == 0) {
          app = NewAppearance(header);
        }

        cols.Add(new TextColumn(rows.ToArray(), app));
      }

      return new TextTable(cols.ToArray());
    }

    /// <summary>
    /// Measures the widths of the columns within the given table.
    /// </summary>
    /// <returns>The table column widths.</returns>
    /// <param name="table">Table.</param>
    private static double[] MeasureTableColumns(string[,] table, PdfFont[] fonts) {
      var ret = new double[table.GetLength(0)];

      for (int c = 0; c < ret.Rank; c++) {
        for (int r = 0; r < table.GetLength(c); r++) {
          var p = PdfTextEngine.MeasureString(table[r, c], fonts[c]);
          if (p.Width > ret[c]) {
            ret[c] = p.Width;
          }
        }
      }

      return ret;
    }

    /// <summary>
    /// Utility method that will create a new string appearance options.
    /// </summary>
    /// <returns>The appearance.</returns>
    /// <param name="font">Font.</param>
    private static PdfStringAppearanceOptions NewAppearance(PdfStandardFont font) {
      return NewAppearance(font, PdfRgbColor.Black);
    }

    /// <summary>
    /// Utility method that will create a new string appearance options.
    /// </summary>
    /// <returns>The appearence options.</returns>
    /// <param name="font">Font.</param>
    /// <param name="color">Color.</param>
    private static PdfStringAppearanceOptions NewAppearance(PdfStandardFont font, PdfColor color) {
      var ret = new PdfStringAppearanceOptions();

      ret.Font = font;
      ret.Brush = new PdfBrush(color);

      return ret;
    }

    /// <summary>
    /// Utility method that will create a new string layout.
    /// </summary>
    /// <returns>The layout.</returns>
    /// <param name="align">Align.</param>
    private static PdfStringLayoutOptions NewLayout(PdfStringHorizontalAlign align = PdfStringHorizontalAlign.Left) {
      var ret = new PdfStringLayoutOptions();

      ret.HorizontalAlign = align;

      return ret;
    }

    /// <summary>
    /// Returns a new PdfStandardRectangle using a sane rectangle creation method.
    /// </summary>
    /// <returns>The rect.</returns>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    /// <param name="width">Width.</param>
    /// <param name="height">Height.</param>
    private static PdfStandardRectangle NewRect(double x, double y, double width, double height) {
      return new PdfStandardRectangle(x, y + height, x + width, y);
    }

    /// <summary>
    /// Utility method that will create a new pen.
    /// </summary>
    /// <returns>The pen.</returns>
    /// <param name="color">Color.</param>
    private static PdfPen NewPen(double width = 1) {
      return NewPen(PdfRgbColor.Black);
    }

    /// <summary>
    /// Utility method that will create a new pen.
    /// </summary>
    /// <returns>The pen.</returns>
    /// <param name="color">Color.</param>
    private static PdfPen NewPen(PdfColor color, double width = 1) {
      var ret = new PdfPen();

      ret.Color = color;
      ret.Width = 1;

      return ret;
    }
  }

  public class TextTable {
    public double width {
      get {
        var ret = 0.0; 

        foreach (var tc in columns) {        
          ret += tc.columnWidth;
        }

        return ret;
      }
    }

    public double height {
      get {
        var ret = 0.0;

        foreach (var tc in columns) {
          if (tc.columnHeight > ret) {
            ret = tc.columnHeight;
          }
        }

        return ret;
      }
    }

    public TextColumn[] columns { get; set; }

    public TextColumn this[int i] {
      get {
        return columns[i];
      }
    }

    public int length { get { return columns.Length; } }

    public TextTable(TextColumn[] columns) {
      this.columns = columns;
    }
  }

  public class TextColumn {
    public PdfStringAppearanceOptions appearance { get; private set; }
    public string[] data { get; private set; }

    public double columnWidth { get; private set; }
    public double rowHeight { get; private set; }
    public double columnHeight {
      get {
        return rowHeight * data.Length;
      }
    }

    public string this[int i] {
      get {
        return data[i];
      }
    }

    public int length { get { return data.Length; } }

    public TextColumn(string[] data, PdfStringAppearanceOptions appearance) {
      this.data = data;
      this.appearance = appearance;

      for (int i = 0; i < data.Length; i++) {
        var size = PdfTextEngine.MeasureString(data[i], appearance.Font);
        if (size.Height > rowHeight) {
          rowHeight = size.Height;
        }

        if (size.Width > columnWidth) {
          columnWidth = size.Width;
        }
      }
    }
  }
}

