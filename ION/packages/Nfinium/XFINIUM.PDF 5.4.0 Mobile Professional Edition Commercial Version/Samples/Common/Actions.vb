Imports Xfinium.Pdf
Imports Xfinium.Pdf.Graphics
Imports Xfinium.Pdf.Annotations
Imports Xfinium.Pdf.Actions
Imports Xfinium.Pdf.Destinations

Namespace Xfinium.Pdf.Samples
	''' <summary>
	''' Actions sample.
	''' </summary>
	Public Class Actions
		''' <summary>
		''' Main method for running the sample.
		''' </summary>
		Public Shared Function Run() As SampleOutputInfo()
			' Create a PDF document with 10 pages.
			Dim document As New PdfFixedDocument()
			Dim helvetica As New PdfStandardFont(PdfStandardFontFace.Helvetica, 216)
			Dim blackBrush As New PdfBrush()
			For i As Integer = 0 To 9
				Dim page As PdfPage = document.Pages.Add()
				page.Graphics.DrawString((i + 1).ToString(), helvetica, blackBrush, 5, 5)
			Next

			CreateNamedActions(document, helvetica)

			CreateGoToActions(document, helvetica)

			CreateRemoteGoToActions(document, helvetica)

			CreateLaunchActions(document, helvetica)

			CreateUriActions(document, helvetica)

			CreateJavaScriptActions(document, helvetica)

			CreateDocumentActions(document)

			' Compress the page graphic content.
			For i As Integer = 0 To document.Pages.Count - 1
				document.Pages(i).Graphics.CompressAndClose()
			Next

			Dim output As SampleOutputInfo() = New SampleOutputInfo() {New SampleOutputInfo(document, "xfinium.pdf.sample.actions.pdf")}
			Return output
		End Function

		Private Shared Sub CreateNamedActions(document As PdfFixedDocument, font As PdfFont)
			Dim blackPen As New PdfPen(New PdfRgbColor(0, 0, 0), 1)
			Dim blackBrush As New PdfBrush()

			font.Size = 12
			Dim sao As New PdfStringAppearanceOptions()
			sao.Brush = blackBrush
			sao.Font = font
			Dim slo As New PdfStringLayoutOptions()
			slo.HorizontalAlign = PdfStringHorizontalAlign.Center
			slo.VerticalAlign = PdfStringVerticalAlign.Middle

			For i As Integer = 0 To document.Pages.Count - 1
				document.Pages(i).Graphics.DrawString("Named actions:", font, blackBrush, 400, 20)

				'''//////////
				' First page
				'''//////////
				document.Pages(i).Graphics.DrawRectangle(blackPen, 400, 40, 200, 20)
				slo.X = 500
				slo.Y = 50
				document.Pages(i).Graphics.DrawString("Go To First Page", sao, slo)

				' Create a link annotation on top of the widget.
				Dim link As New PdfLinkAnnotation()
				document.Pages(i).Annotations.Add(link)
				link.VisualRectangle = New PdfVisualRectangle(400, 40, 200, 20)

				' Create a named action and attach it to the link.
				Dim namedAction As New PdfNamedAction()
				namedAction.NamedAction = PdfActionName.FirstPage
				link.Action = namedAction

				'''//////////
				' Prev page
				'''//////////
				document.Pages(i).Graphics.DrawRectangle(blackPen, 400, 80, 200, 20)
				slo.Y = 90
				document.Pages(i).Graphics.DrawString("Go To Previous Page", sao, slo)

				' Create a link annotation on top of the widget.
				link = New PdfLinkAnnotation()
				document.Pages(i).Annotations.Add(link)
				link.VisualRectangle = New PdfVisualRectangle(400, 80, 200, 20)

				' Create a named action and attach it to the link.
				namedAction = New PdfNamedAction()
				namedAction.NamedAction = PdfActionName.PrevPage
				link.Action = namedAction

				'''//////////
				' Next page
				'''//////////
				document.Pages(i).Graphics.DrawRectangle(blackPen, 400, 120, 200, 20)
				slo.Y = 130
				document.Pages(i).Graphics.DrawString("Go To Next Page", sao, slo)

				' Create a link annotation on top of the widget.
				link = New PdfLinkAnnotation()
				document.Pages(i).Annotations.Add(link)
				link.VisualRectangle = New PdfVisualRectangle(400, 120, 200, 20)

				' Create a named action and attach it to the link.
				namedAction = New PdfNamedAction()
				namedAction.NamedAction = PdfActionName.NextPage
				link.Action = namedAction

				'''//////////
				' Last page
				'''//////////
				document.Pages(i).Graphics.DrawRectangle(blackPen, 400, 160, 200, 20)
				slo.Y = 170
				document.Pages(i).Graphics.DrawString("Go To Last Page", sao, slo)

				' Create a link annotation on top of the widget.
				link = New PdfLinkAnnotation()
				document.Pages(i).Annotations.Add(link)
				link.VisualRectangle = New PdfVisualRectangle(400, 160, 200, 20)

				' Create a named action and attach it to the link.
				namedAction = New PdfNamedAction()
				namedAction.NamedAction = PdfActionName.LastPage
				link.Action = namedAction

				'''//////////
				' Print document
				'''//////////
				document.Pages(i).Graphics.DrawRectangle(blackPen, 400, 200, 200, 20)
				slo.Y = 210
				document.Pages(i).Graphics.DrawString("Print Document", sao, slo)

				' Create a link annotation on top of the widget.
				link = New PdfLinkAnnotation()
				document.Pages(i).Annotations.Add(link)
				link.VisualRectangle = New PdfVisualRectangle(400, 200, 200, 20)

				' Create a named action and attach it to the link.
				namedAction = New PdfNamedAction()
				namedAction.NamedAction = PdfActionName.Print
				link.Action = namedAction
			Next
		End Sub

		Private Shared Sub CreateGoToActions(document As PdfFixedDocument, font As PdfFont)
			Dim blackPen As New PdfPen(New PdfRgbColor(0, 0, 0), 1)
			Dim blackBrush As New PdfBrush()

			font.Size = 12
			Dim sao As New PdfStringAppearanceOptions()
			sao.Brush = blackBrush
			sao.Font = font
			Dim slo As New PdfStringLayoutOptions()
			slo.HorizontalAlign = PdfStringHorizontalAlign.Center
			slo.VerticalAlign = PdfStringVerticalAlign.Middle

			Dim rnd As New Random()
			For i As Integer = 0 To document.Pages.Count - 1
				Dim destinationPage As Integer = rnd.[Next](document.Pages.Count)

				document.Pages(i).Graphics.DrawString("Go To actions:", font, blackBrush, 400, 240)

				document.Pages(i).Graphics.DrawRectangle(blackPen, 400, 260, 200, 20)
				slo.X = 500
				slo.Y = 270
				document.Pages(i).Graphics.DrawString("Go To page: " & (destinationPage + 1).ToString(), sao, slo)

				' Create a link annotation on top of the widget.
				Dim link As New PdfLinkAnnotation()
				document.Pages(i).Annotations.Add(link)
				link.VisualRectangle = New PdfVisualRectangle(400, 260, 200, 20)

				' Create a GoTo action and attach it to the link.
				Dim pageDestination As New PdfPageDirectDestination()
				pageDestination.Page = document.Pages(destinationPage)
				pageDestination.Left = 0
				pageDestination.Top = 0
				pageDestination.Zoom = 0
				' Keep current zoom
				Dim gotoPageAction As New PdfGoToAction()
				gotoPageAction.Destination = pageDestination
				link.Action = gotoPageAction
			Next
		End Sub

		Private Shared Sub CreateRemoteGoToActions(document As PdfFixedDocument, font As PdfFont)
			Dim blackPen As New PdfPen(New PdfRgbColor(0, 0, 0), 1)
			Dim blackBrush As New PdfBrush()

			font.Size = 12
			Dim sao As New PdfStringAppearanceOptions()
			sao.Brush = blackBrush
			sao.Font = font
			Dim slo As New PdfStringLayoutOptions()
			slo.HorizontalAlign = PdfStringHorizontalAlign.Center
			slo.VerticalAlign = PdfStringVerticalAlign.Middle

			Dim rnd As New Random()
			For i As Integer = 0 To document.Pages.Count - 1
				Dim destinationPage As Integer = rnd.[Next](document.Pages.Count)

				document.Pages(i).Graphics.DrawString("Go To Remote actions:", font, blackBrush, 400, 300)

				document.Pages(i).Graphics.DrawRectangle(blackPen, 400, 320, 200, 20)
				slo.X = 500
				slo.Y = 330
				document.Pages(i).Graphics.DrawString("Go To page " & (destinationPage + 1).ToString() & " in sample.pdf", sao, slo)

				' Create a link annotation on top of the widget.
				Dim link As New PdfLinkAnnotation()
				document.Pages(i).Annotations.Add(link)
				link.VisualRectangle = New PdfVisualRectangle(400, 320, 200, 20)

				' Create a GoToR action and attach it to the link.
				Dim pageDestination As New PdfPageNumberDestination()
				pageDestination.PageNumber = destinationPage
				pageDestination.Left = 0
				pageDestination.Top = 792
				pageDestination.Zoom = 0
				' Keep current zoom
				Dim remoteGoToAction As New PdfRemoteGoToAction()
				remoteGoToAction.FileName = "sample.pdf"
				remoteGoToAction.Destination = pageDestination
				link.Action = remoteGoToAction
			Next
		End Sub

		Private Shared Sub CreateLaunchActions(document As PdfFixedDocument, font As PdfFont)
			Dim blackPen As New PdfPen(New PdfRgbColor(0, 0, 0), 1)
			Dim blackBrush As New PdfBrush()

			font.Size = 12
			Dim sao As New PdfStringAppearanceOptions()
			sao.Brush = blackBrush
			sao.Font = font
			Dim slo As New PdfStringLayoutOptions()
			slo.HorizontalAlign = PdfStringHorizontalAlign.Center
			slo.VerticalAlign = PdfStringVerticalAlign.Middle

			For i As Integer = 0 To document.Pages.Count - 1
				document.Pages(i).Graphics.DrawString("Launch actions:", font, blackBrush, 400, 360)

				document.Pages(i).Graphics.DrawRectangle(blackPen, 400, 380, 200, 20)
				slo.X = 500
				slo.Y = 390
				document.Pages(i).Graphics.DrawString("Launch samples explorer", sao, slo)

				' Create a link annotation on top of the widget.
				Dim link As New PdfLinkAnnotation()
				document.Pages(i).Annotations.Add(link)
				link.VisualRectangle = New PdfVisualRectangle(400, 380, 200, 20)

				' Create a launch action and attach it to the link.
				Dim launchAction As New PdfLaunchAction()
				launchAction.FileName = "Xfinium.Pdf.SamplesExplorer.Win.exe"
				link.Action = launchAction
			Next
		End Sub

		Private Shared Sub CreateUriActions(document As PdfFixedDocument, font As PdfFont)
			Dim blackPen As New PdfPen(New PdfRgbColor(0, 0, 0), 1)
			Dim blackBrush As New PdfBrush()

			font.Size = 12
			Dim sao As New PdfStringAppearanceOptions()
			sao.Brush = blackBrush
			sao.Font = font
			Dim slo As New PdfStringLayoutOptions()
			slo.HorizontalAlign = PdfStringHorizontalAlign.Center
			slo.VerticalAlign = PdfStringVerticalAlign.Middle

			For i As Integer = 0 To document.Pages.Count - 1
				document.Pages(i).Graphics.DrawString("Uri actions:", font, blackBrush, 400, 420)

				document.Pages(i).Graphics.DrawRectangle(blackPen, 400, 440, 200, 20)
				slo.X = 500
				slo.Y = 450
				document.Pages(i).Graphics.DrawString("Go to xfiniumpdf.com", sao, slo)

				' Create a link annotation on top of the widget.
				Dim link As New PdfLinkAnnotation()
				document.Pages(i).Annotations.Add(link)
				link.VisualRectangle = New PdfVisualRectangle(400, 440, 200, 20)

				' Create an uri action and attach it to the link.
				Dim uriAction As New PdfUriAction()
				uriAction.URI = "http://www.xfiniumpdf.com"
				link.Action = uriAction
			Next
		End Sub

		Private Shared Sub CreateJavaScriptActions(document As PdfFixedDocument, font As PdfFont)
			Dim blackPen As New PdfPen(New PdfRgbColor(0, 0, 0), 1)
			Dim blackBrush As New PdfBrush()

			font.Size = 12
			Dim sao As New PdfStringAppearanceOptions()
			sao.Brush = blackBrush
			sao.Font = font
			Dim slo As New PdfStringLayoutOptions()
			slo.HorizontalAlign = PdfStringHorizontalAlign.Center
			slo.VerticalAlign = PdfStringVerticalAlign.Middle

			For i As Integer = 0 To document.Pages.Count - 1
				document.Pages(i).Graphics.DrawString("JavaScript actions:", font, blackBrush, 400, 480)

				document.Pages(i).Graphics.DrawRectangle(blackPen, 400, 500, 200, 20)
				slo.X = 500
				slo.Y = 510
				document.Pages(i).Graphics.DrawString("Click me", sao, slo)

				' Create a link annotation on top of the widget.
				Dim link As New PdfLinkAnnotation()
				document.Pages(i).Annotations.Add(link)
				link.VisualRectangle = New PdfVisualRectangle(400, 500, 200, 20)

				' Create a Javascript action and attach it to the link.
				Dim jsAction As New PdfJavaScriptAction()
				jsAction.Script = "app.alert({cMsg: ""JavaScript action: you are now on page " & (i + 1) & """, cTitle: ""Xfinium.Pdf Actions Sample""});"
				link.Action = jsAction
			Next
		End Sub

		Private Shared Sub CreateDocumentActions(document As PdfFixedDocument)
			' Create an action that will open the document at the last page with 200% zoom.
			Dim pageDestination As New PdfPageDirectDestination()
			pageDestination.Page = document.Pages(document.Pages.Count - 1)
			pageDestination.Zoom = 200
			pageDestination.Top = 0
			pageDestination.Left = 0
			Dim openAction As New PdfGoToAction()
			openAction.Destination = pageDestination
			document.OpenAction = openAction

			' Create an action that is executed when the document is closed.
			Dim jsCloseAction As New PdfJavaScriptAction()
			jsCloseAction.Script = "app.alert({cMsg: ""The document will close now."", cTitle: ""Xfinium.Pdf Actions Sample""});"
			document.BeforeCloseAction = jsCloseAction

			' Create an action that is executed before the document is printed.
			Dim jsBeforePrintAction As New PdfJavaScriptAction()
			jsBeforePrintAction.Script = "app.alert({cMsg: ""The document will be printed."", cTitle: ""Xfinium.Pdf Actions Sample""});"
			document.BeforePrintAction = jsBeforePrintAction

			' Create an action that is executed after the document is printed.
			Dim jsAfterPrintAction As New PdfJavaScriptAction()
			jsAfterPrintAction.Script = "app.alert({cMsg: ""The document has been printed."", cTitle: ""Xfinium.Pdf Actions Sample""});"
			document.AfterPrintAction = jsAfterPrintAction
		End Sub
	End Class
End Namespace
