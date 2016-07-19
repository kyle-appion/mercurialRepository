Imports System.IO
Imports Xfinium.Pdf
Imports Xfinium.Pdf.Graphics
Imports Xfinium.Pdf.Content
Imports Xfinium.Pdf.Actions
Imports Xfinium.Pdf.Destinations

Namespace Xfinium.Pdf.Samples
	''' <summary>
	''' Outlines sample.
	''' </summary>
	Public Class Outlines
		''' <summary>
		''' Main method for running the sample.
		''' </summary>
		Public Shared Function Run() As SampleOutputInfo()
			Dim document As New PdfFixedDocument()
			document.DisplayMode = PdfDisplayMode.UseOutlines

			Dim helvetica As New PdfStandardFont(PdfStandardFontFace.Helvetica, 216)
			Dim blackBrush As New PdfBrush()
			For i As Integer = 0 To 9
				Dim page As PdfPage = document.Pages.Add()
				page.Graphics.DrawString((i + 1).ToString(), helvetica, blackBrush, 50, 50)
			Next

			Dim root As New PdfOutlineItem()
			root.Title = "Contents"
			root.VisualStyle = PdfOutlineItemVisualStyle.Bold
			root.Color = New PdfRgbColor(255, 0, 0)
			document.Outline.Add(root)

			For i As Integer = 0 To document.Pages.Count - 1
				' Create a destination to target page.
				Dim pageDestination As New PdfPageDirectDestination()
				pageDestination.Page = document.Pages(i)
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
				outline.Title = String.Format("Go to page {0}", i + 1)
				outline.VisualStyle = PdfOutlineItemVisualStyle.Italic
				outline.Action = gotoPage
				root.Items.Add(outline)
			Next
			root.Expanded = True

			' Create an outline that will launch a link in the browser.
			Dim uriAction As New PdfUriAction()
			uriAction.URI = "http://www.xfiniumsoft.com/"

			Dim webOutline As New PdfOutlineItem()
			webOutline.Title = "http://www.xfiniumsoft.com/"
			webOutline.Color = New PdfRgbColor(0, 0, 255)
			webOutline.Action = uriAction
			document.Outline.Add(webOutline)

			Dim output As SampleOutputInfo() = New SampleOutputInfo() {New SampleOutputInfo(document, "xfinium.pdf.sample.outlines.pdf")}
			Return output
		End Function
	End Class
End Namespace
