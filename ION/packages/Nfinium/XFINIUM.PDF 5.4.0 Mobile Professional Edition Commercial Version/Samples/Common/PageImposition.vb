Imports System.IO
Imports Xfinium.Pdf
Imports Xfinium.Pdf.Graphics
Imports Xfinium.Pdf.Core

Namespace Xfinium.Pdf.Samples
	''' <summary>
	''' PageImposition sample.
	''' </summary>
	Public Class PageImposition
		''' <summary>
		''' Main method for running the sample.
		''' </summary>
		Public Shared Function Run(input As Stream) As SampleOutputInfo()
			Dim file As New PdfFile(input)
			Dim content As PdfPageContent() = file.ExtractPageContent(0, file.PageCount - 1)
			file = Nothing

			Dim document As New PdfFixedDocument()
			Dim page1 As PdfPage = document.Pages.Add()
			' Draw the same page content 4 times on the new page, the content is scaled to half and flipped.
			page1.Graphics.DrawFormXObject(content(0), 0, 0, page1.Width / 2, page1.Height / 2)
			page1.Graphics.DrawFormXObject(content(0), page1.Width / 2, 0, page1.Width / 2, page1.Height / 2, 0, _
				PdfFlipDirection.VerticalFlip)
			page1.Graphics.DrawFormXObject(content(0), 0, page1.Height / 2, page1.Width / 2, page1.Height / 2, 0, _
				PdfFlipDirection.HorizontalFlip)
			page1.Graphics.DrawFormXObject(content(0), page1.Width / 2, page1.Height / 2, page1.Width / 2, page1.Height / 2, 0, _
				PdfFlipDirection.VerticalFlip Or PdfFlipDirection.HorizontalFlip)

			Dim page2 As PdfPage = document.Pages.Add()
			' Draw 3 pages on the new page.
			page2.Graphics.DrawFormXObject(content(0), 0, 0, page2.Width / 2, page2.Height / 2)
			page2.Graphics.DrawFormXObject(content(1), page2.Width / 2, 0, page2.Width / 2, page2.Height / 2)
			page2.Graphics.DrawFormXObject(content(2), 0, page2.Height, page2.Height / 2, page2.Width, 90)

			Dim output As SampleOutputInfo() = New SampleOutputInfo() {New SampleOutputInfo(document, "xfinium.pdf.sample.pageimposition.pdf")}
			Return output
		End Function
	End Class
End Namespace
