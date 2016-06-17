Imports System.IO
Imports Xfinium.Pdf
Imports Xfinium.Pdf.Graphics

Namespace Xfinium.Pdf.Samples
	''' <summary>
	''' Tiff to Pdf conversion sample.
	''' </summary>
	Public Class TiffToPdf
		''' <summary>
		''' Main method for running the sample.
		''' </summary>
		Public Shared Function Run(input As Stream) As SampleOutputInfo()
			Dim document As New PdfFixedDocument()
			Dim tiff As New PdfTiffImage(input)

			For i As Integer = 0 To tiff.FrameCount - 1
				tiff.ActiveFrame = i
				Dim page As PdfPage = document.Pages.Add()
				page.Graphics.DrawImage(tiff, 0, 0, page.Width, page.Height)
			Next

			Dim output As SampleOutputInfo() = New SampleOutputInfo() {New SampleOutputInfo(document, "xfinium.pdf.sample.tifftopdf.pdf")}
			Return output
		End Function
	End Class
End Namespace