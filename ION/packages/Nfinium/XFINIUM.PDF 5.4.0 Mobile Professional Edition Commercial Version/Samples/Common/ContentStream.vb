Imports System.IO
Imports Xfinium.Pdf
Imports Xfinium.Pdf.Core.Cos
Imports Xfinium.Pdf.Graphics
Imports Xfinium.Pdf.Graphics.Text

Namespace Xfinium.Pdf.Samples
	''' <summary>
	''' ContentStream sample.
	''' </summary>
	Public Class ContentStream
		''' <summary>
		''' Main method for running the sample.
		''' </summary>
		''' <param name="stream"></param>
		Public Shared Function Run() As SampleOutputInfo()
			' Create the pdf document
			Dim document As New PdfFixedDocument()
			' Create a new page in the document
			Dim page As PdfPage = document.Pages.Add()

			Dim brush As New PdfBrush(PdfRgbColor.DarkRed)
			Dim helvetica As New PdfStandardFont(PdfStandardFontFace.Helvetica, 36)
			page.Graphics.DrawString("Hello World", helvetica, brush, 100, 100)

			Dim cs As New PdfContentStream(page.Graphics)
			' Sets the stroke and fill colorspaces to DeviceRGB
			cs.SetStrokeColorSpace(New PdfRgbColorSpace())
			cs.SetFillColorSpace(New PdfRgbColorSpace())
			' Set stroke color to blue
			cs.SetStrokeColor(New Double() {0, 0, 1})
			' Set fill color to green
			cs.SetFillColor(New Double() {0, 1, 0})

			' Draw a line from (0, 0) to (page.Width/2, page.Height/2)
			' It will be drawn from top left corner to center of the page.
			cs.MoveTo(0, 0)
			cs.LineTo(page.Width / 2, page.Height / 2)
			cs.StrokePath()

			' Begin a text section
			cs.BeginText()
			cs.SetTextRendering(PdfTextRenderingMode.FillText)
			cs.SetTextMatrix(1, 0, 0, 1, 100, 150)
			cs.SetTextFontAndSize(helvetica, helvetica.Size)

			' This text will appear inverted because the coordinate system is in visual mode.
			Dim binaryText As Byte() = helvetica.EncodeString("Hello World")
			cs.ShowText(New PdfCosBinaryString(binaryText))
			cs.EndText()

			' Reset coordinate system and the current graphics state to default PDF
			cs.ResetPdfCoordinateSystem()
			' Sets the stroke and fill colorspaces to DeviceRGB
			cs.SetStrokeColorSpace(New PdfRgbColorSpace())
			cs.SetFillColorSpace(New PdfRgbColorSpace())
			' Set stroke color to blue
			cs.SetStrokeColor(New Double() {0, 0, 1})
			' Set fill color to green
			cs.SetFillColor(New Double() {0, 1, 0})

			' Draw a line from (0, 0) to (page.Width/2, page.Height/2)
			' It will be drawn from bottom left corner to center of the page because the coordinate system has been reset to default PDF.
			cs.MoveTo(0, 0)
			cs.LineTo(page.Width / 2, page.Height / 2)
			cs.StrokePath()

			' Draw the text again
			cs.BeginText()
			cs.SetTextRendering(PdfTextRenderingMode.FillText)
			cs.SetTextMatrix(1, 0, 0, 1, 100, 150)
			cs.SetTextFontAndSize(helvetica, helvetica.Size)

			' This text will appear ok.
			binaryText = helvetica.EncodeString("Hello World")
			cs.ShowText(New PdfCosBinaryString(binaryText))
			cs.EndText()

			' Restore the visual coordinate system
			cs.RestoreVisualCoordinateSystem()

			Dim output As SampleOutputInfo() = New SampleOutputInfo() {New SampleOutputInfo(document, "xfinium.pdf.sample.contentstream.pdf")}
			Return output
		End Function
	End Class
End Namespace