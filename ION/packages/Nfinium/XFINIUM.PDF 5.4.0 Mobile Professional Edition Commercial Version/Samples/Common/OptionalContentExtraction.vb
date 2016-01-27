Imports System.IO
Imports Xfinium.Pdf
Imports Xfinium.Pdf.Graphics
Imports Xfinium.Pdf.Core
Imports Xfinium.Pdf.Content

Namespace Xfinium.Pdf.Samples
	''' <summary>
	''' OptionalContentExtraction sample.
	''' </summary>
	Public Class OptionalContentExtraction
		''' <summary>
		''' Main method for running the sample.
		''' </summary>
		Public Shared Function Run(input As Stream) As SampleOutputInfo()
			Dim file As New PdfFile(input)

			Dim document As New PdfFixedDocument()
			Dim page As PdfPage = document.Pages.Add()

			Dim oc1 As PdfPageOptionalContent = file.ExtractPageOptionalContentGroup(4, "1")
			page.Graphics.DrawFormXObject(oc1, 0, 0, page.Width / 2, page.Height / 2)
			Dim oc2 As PdfPageOptionalContent = file.ExtractPageOptionalContentGroup(4, "2")
			page.Graphics.DrawFormXObject(oc2, page.Width / 2, 0, page.Width / 2, page.Height / 2)
			Dim oc3 As PdfPageOptionalContent = file.ExtractPageOptionalContentGroup(4, "3")
			page.Graphics.DrawFormXObject(oc3, 0, page.Height / 2, page.Width / 2, page.Height / 2)
			Dim oc4 As PdfPageOptionalContent = file.ExtractPageOptionalContentGroup(4, "4")
			page.Graphics.DrawFormXObject(oc4, page.Width / 2, page.Height / 2, page.Width / 2, page.Height / 2)

			Dim output As SampleOutputInfo() = New SampleOutputInfo() {New SampleOutputInfo(document, "xfinium.pdf.sample.optionalcontentextraction.pdf")}
			Return output
		End Function
	End Class
End Namespace