namespace ION.Core.Report.DataLogs.Exporter {

  using System;
  using System.Threading.Tasks;

  using FlexCel.Core;
  using FlexCel.Render;
  using FlexCel.XlsAdapter;

  using ION.Core.App;

  public abstract class BaseFormattedFlexCelDataLogExporter : IDataLogExporter {
    /// <summary>
    /// The format that is used to render the title.
    /// </summary>
    protected int titleFormat;
    /// <summary>
    /// The format that is used to render the section header.
    /// </summary>
    protected int sectionHeaderFormat;
    /// <summary>
    /// The format that is used to render the section content.
    /// </summary>
    protected int sectionContentFormat;

    // Implemented for IDataLogExporter
    public abstract Task<bool> Export(DataLogReport dlr);

    /// <summary>
    /// Initializes the formats for the given file.
    /// </summary>
    /// <param name="file">File.</param>
    protected void InitializeToFile(XlsFile file) {
      titleFormat = CreateTitleTextFormat(file);
      sectionHeaderFormat = CreateSectionHeaderFormat(file);
      sectionContentFormat = CreateSectionContentFormat(file);
    }

		/// <summary>
		/// Creates and registers the format that is used to draw title text.
		/// </summary>
		/// <returns>The bold text format.</returns>
		/// <param name="file">File.</param>
		private int CreateTitleTextFormat(XlsFile file) {
			var format = file.GetDefaultFormat;
			format.Font.Style = TFlxFontStyles.Bold;
			format.Font.Size20 = 400;
			return file.AddFormat(format);
		}

    /// <summary>
    /// Creates and registers the format that is used to draw the header
    /// </summary>
    /// <returns>The section header format.</returns>
    /// <param name="file">File.</param>
    private int CreateSectionHeaderFormat(XlsFile file) {
      var format = file.GetDefaultFormat;
      format.FillPattern = new TFlxFillPattern { Pattern = TFlxPatternStyle.Solid, FgColor = Colors.Black };
			format.VAlignment = TVFlxAlignment.top;
			format.HAlignment = THFlxAlignment.left;
      format.Font.Color = Colors.White;
      return file.AddFormat(format);
    }

    private int CreateSectionContentFormat(XlsFile file) {
      var format = file.GetDefaultFormat;
      format.Borders.Top.Color = Colors.Black;
			format.Borders.Top.Style = TFlxBorderStyle.Thin;
			format.Borders.Left.Color = Colors.Black;
			format.Borders.Left.Style = TFlxBorderStyle.Thin;
			format.Borders.Right.Color = Colors.Black;
			format.Borders.Right.Style = TFlxBorderStyle.Thin;
			format.Borders.Bottom.Color = Colors.Black;
			format.Borders.Bottom.Style = TFlxBorderStyle.Thin;
      format.WrapText = true;
      format.VAlignment = TVFlxAlignment.top;
      format.HAlignment = THFlxAlignment.left;
      return file.AddFormat(format);
    }
  }
}
