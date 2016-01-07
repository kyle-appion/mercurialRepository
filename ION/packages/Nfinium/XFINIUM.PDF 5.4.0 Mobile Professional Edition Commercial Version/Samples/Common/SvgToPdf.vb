Imports System.IO
Imports Xfinium.Pdf
Imports Xfinium.Pdf.Graphics
Imports Xfinium.Pdf.Graphics.Svg

Namespace Xfinium.Pdf.Samples
	''' <summary>
	''' Svg to Pdf conversion sample.
	''' </summary>
	Public Class SvgToPdf
		''' <summary>
		''' Main method for running the sample.
		''' </summary>
		Public Shared Function Run(input As Stream) As SampleOutputInfo()
			Dim document As New PdfFixedDocument()
			Dim page As PdfPage = document.Pages.Add()

			Dim svg As New PdfSvgDrawing(input)
			page.Graphics.DrawFormXObject(svg, 20, 20, page.Width - 40, page.Height - 40)

			Dim output As SampleOutputInfo() = New SampleOutputInfo() {New SampleOutputInfo(document, "xfinium.pdf.sample.svgtopdf.pdf")}
			Return output
		End Function
	End Class
End Namespace
