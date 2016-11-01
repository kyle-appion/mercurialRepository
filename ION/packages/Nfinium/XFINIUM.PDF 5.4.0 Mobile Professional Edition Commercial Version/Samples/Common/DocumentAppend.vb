Imports System.IO
Imports Xfinium.Pdf
Imports Xfinium.Pdf.Graphics
Imports Xfinium.Pdf.Destinations
Imports Xfinium.Pdf.Actions

Namespace Xfinium.Pdf.Samples
	''' <summary>
	''' DocumentAppend sample.
	''' </summary>
	Public Class DocumentAppend
		''' <summary>
		''' Main method for running the sample.
		''' </summary>
		Public Shared Function Run(file1Input As Stream, file2Input As Stream) As SampleOutputInfo()
			Dim document As New PdfFixedDocument()

			' The documents are merged by creating an empty PDF document and appending the file to it.
			' The outlines from the source file are also included in the merged file.
			document.AppendFile(file1Input)
			Dim count As Integer = document.Pages.Count
			document.AppendFile(file2Input)

			' Create outlines that point to each merged file.
			Dim file1Outline As PdfOutlineItem = CreateOutline("First file", document.Pages(0))
			document.Outline.Add(file1Outline)
			Dim file2Outline As PdfOutlineItem = CreateOutline("Second file", document.Pages(count))
			document.Outline.Add(file2Outline)

			' Optionally we can add a new page at the beginning of the merged document.
			Dim page As New PdfPage()
			document.Pages.Insert(0, page)

			Dim blankPageOutline As PdfOutlineItem = CreateOutline("Blank page", page)
			document.Outline.Insert(0, blankPageOutline)

			Dim output As SampleOutputInfo() = New SampleOutputInfo() {New SampleOutputInfo(document, "xfinium.pdf.sample.documentappend.pdf")}
			Return output
		End Function

		Private Shared Function CreateOutline(title As String, page As PdfPage) As PdfOutlineItem
			Dim pageDestination As New PdfPageDirectDestination()
			pageDestination.Page = page
			pageDestination.Top = 0
			pageDestination.Left = 0
			' Inherit current zoom
			pageDestination.Zoom = 0

			' Create a go to action to be executed when the outline is clicked, 
			' the go to action goes to specified destination.
			Dim gotoPage As New PdfGoToAction()
			gotoPage.Destination = pageDestination

			' Create the outline in the table of contents
			Dim outline As New PdfOutlineItem()
			outline.Title = title
			outline.VisualStyle = PdfOutlineItemVisualStyle.Italic
			outline.Action = gotoPage

			Return outline
		End Function
	End Class
End Namespace
