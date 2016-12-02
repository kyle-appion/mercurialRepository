Imports System.IO
Imports Xfinium.Pdf
Imports Xfinium.Pdf.Graphics
Imports Xfinium.Pdf.Destinations
Imports Xfinium.Pdf.Actions
Imports Xfinium.Pdf.Core

Namespace Xfinium.Pdf.Samples
	''' <summary>
	''' DocumentSplit sample.
	''' </summary>
	Public Class DocumentSplit
		''' <summary>
		''' Main method for running the sample.
		''' </summary>
		Public Shared Function Run(input As Stream) As SampleOutputInfo()
			' The input file is split by extracting pages from source file and inserting them in new empty PDF documents.
			Dim file As New PdfFile(input)
			Dim output As SampleOutputInfo() = New SampleOutputInfo(file.PageCount - 1) {}

			For i As Integer = 0 To file.PageCount - 1
				Dim document As New PdfFixedDocument()
				Dim page As PdfPage = file.ExtractPage(i)
				document.Pages.Add(page)

				output(i) = New SampleOutputInfo(document, String.Format("xfinium.pdf.sample.documentsplit.{0}.pdf", i + 1))
			Next

			Return output
		End Function
	End Class
End Namespace
