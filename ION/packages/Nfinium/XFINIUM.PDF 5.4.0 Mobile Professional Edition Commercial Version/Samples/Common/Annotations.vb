Imports Xfinium.Pdf
Imports Xfinium.Pdf.Graphics
Imports Xfinium.Pdf.Annotations
Imports Xfinium.Pdf.Actions
Imports Xfinium.Pdf.Destinations
Imports System.IO

Namespace Xfinium.Pdf.Samples
	''' <summary>
	''' Annotations sample.
	''' </summary>
	Public Class Annotations
		''' <summary>
		''' Main method for running the sample.
		''' </summary>
		Public Shared Function Run(flashStream As Stream, u3dStream As Stream) As SampleOutputInfo()
			' Create a PDF document with 10 pages.
			Dim document As New PdfFixedDocument()
			Dim helvetica As New PdfStandardFont(PdfStandardFontFace.Helvetica, 12)

			CreateTextAnnotations(document, helvetica)

			CreateSquareCircleAnnotations(document, helvetica)

			CreateFileAttachmentAnnotations(document, helvetica)

			CreateInkAnnotations(document, helvetica)

			CreateLineAnnotations(document, helvetica)

			CreatePolygonAnnotations(document, helvetica)

			CreatePolylineAnnotations(document, helvetica)

			CreateRubberStampAnnotations(document, helvetica)

			CreateTextMarkupAnnotations(document, helvetica)

			CreateRichMediaAnnotations(document, helvetica, flashStream)

			Create3DAnnotations(document, helvetica, u3dStream)

			CreateRedactionAnnotations(document, helvetica, u3dStream)

			' Compress the page graphic content.
			For i As Integer = 0 To document.Pages.Count - 1
				document.Pages(i).Graphics.CompressAndClose()
			Next

			Dim output As SampleOutputInfo() = New SampleOutputInfo() {New SampleOutputInfo(document, "xfinium.pdf.sample.annotations.pdf")}
			Return output
		End Function

		Private Shared Sub CreateTextAnnotations(document As PdfFixedDocument, font As PdfFont)
			Dim blackBrush As New PdfBrush()

			Dim page As PdfPage = document.Pages.Add()

			Dim textAnnotationNames As String() = New String() {"Comment", "Check", "Circle", "Cross", "Help", "Insert", _
				"Key", "NewParagraph", "Note", "Paragraph", "RightArrow", "RightPointer", _
				"Star", "UpArrow", "UpLeftArrow"}

			page.Graphics.DrawString("B/W text annotations", font, blackBrush, 50, 50)
			For i As Integer = 0 To textAnnotationNames.Length - 1
				Dim ta As New PdfTextAnnotation()
				ta.Author = "Xfinium.Pdf"
				ta.Contents = "I am a " & textAnnotationNames(i) & " annotation."
				ta.IconName = textAnnotationNames(i)
				page.Annotations.Add(ta)
				ta.Location = New PdfPoint(50, 100 + 40 * i)
				page.Graphics.DrawString(textAnnotationNames(i), font, blackBrush, 90, 100 + 40 * i)
			Next

			Dim rnd As New Random()
			Dim rgb As Byte() = New Byte(2) {}
			page.Graphics.DrawString("Color text annotations", font, blackBrush, 300, 50)
			For i As Integer = 0 To textAnnotationNames.Length - 1
				Dim ta As New PdfTextAnnotation()
				ta.Author = "Xfinium.Pdf"
				ta.Contents = "I am a " & textAnnotationNames(i) & " annotation."
				ta.IconName = textAnnotationNames(i)
				rnd.NextBytes(rgb)
				ta.OutlineColor = New PdfRgbColor(rgb(0), rgb(1), rgb(2))
				rnd.NextBytes(rgb)
				ta.InteriorColor = New PdfRgbColor(rgb(0), rgb(1), rgb(2))
				page.Annotations.Add(ta)
				ta.Location = New PdfPoint(300, 100 + 40 * i)
				page.Graphics.DrawString(textAnnotationNames(i), font, blackBrush, 340, 100 + 40 * i)
			Next

			page.Graphics.DrawString("Text annotations with custom icons", font, blackBrush, 50, 700)
			Dim customTextAnnotation As New PdfTextAnnotation()
			customTextAnnotation.Author = "Xfinium.Pdf"
			customTextAnnotation.Contents = "Text annotation with custom icon."
			page.Annotations.Add(customTextAnnotation)
			customTextAnnotation.IconName = "Custom icon appearance"
			customTextAnnotation.Location = New PdfPoint(50, 720)

			Dim customAppearance As New PdfAnnotationAppearance(50, 50)
			Dim redPen As New PdfPen(New PdfRgbColor(255, 0, 0), 10)
			Dim bluePen As New PdfPen(New PdfRgbColor(0, 0, 192), 10)
			customAppearance.Graphics.DrawRectangle(redPen, 5, 5, 40, 40)
			customAppearance.Graphics.DrawLine(bluePen, 0, 0, customAppearance.Width, customAppearance.Height)
			customAppearance.Graphics.DrawLine(bluePen, 0, customAppearance.Height, customAppearance.Width, 0)
			customAppearance.Graphics.CompressAndClose()
			customTextAnnotation.NormalAppearance = customAppearance
		End Sub

		Private Shared Sub CreateSquareCircleAnnotations(document As PdfFixedDocument, font As PdfFont)
			Dim blackBrush As New PdfBrush()

			Dim page As PdfPage = document.Pages.Add()

			page.Graphics.DrawString("Square annotations", font, blackBrush, 50, 50)

			Dim square1 As New PdfSquareAnnotation()
			page.Annotations.Add(square1)
			square1.Author = "Xfinium.pdf"
			square1.Contents = "Square annotation with red border"
			square1.BorderColor = New PdfRgbColor(255, 0, 0)
			square1.BorderWidth = 3
			square1.VisualRectangle = New PdfVisualRectangle(50, 70, 250, 150)

			Dim square2 As New PdfSquareAnnotation()
			page.Annotations.Add(square2)
			square2.Author = "Xfinium.pdf"
			square2.Contents = "Square annotation with blue interior"
			square2.BorderColor = Nothing
			square2.BorderWidth = 0
			square2.InteriorColor = New PdfRgbColor(0, 0, 192)
			square2.VisualRectangle = New PdfVisualRectangle(50, 270, 250, 150)

			Dim square3 As New PdfSquareAnnotation()
			page.Annotations.Add(square3)
			square3.Author = "Xfinium.pdf"
			square3.Contents = "Square annotation with yellow border and green interior"
			square3.BorderColor = New PdfRgbColor(255, 255, 0)
			square3.BorderWidth = 3
			square3.InteriorColor = New PdfRgbColor(0, 192, 0)
			square3.VisualRectangle = New PdfVisualRectangle(50, 470, 250, 150)

			page.Graphics.DrawString("Circle annotations", font, blackBrush, 50, 350)

			Dim circle1 As New PdfCircleAnnotation()
			page.Annotations.Add(circle1)
			circle1.Author = "Xfinium.pdf"
			circle1.Contents = "Circle annotation with red border"
			circle1.BorderColor = New PdfRgbColor(255, 0, 0)
			circle1.BorderWidth = 3
			circle1.VisualRectangle = New PdfVisualRectangle(350, 70, 250, 150)

			Dim circle2 As New PdfCircleAnnotation()
			page.Annotations.Add(circle2)
			circle2.Author = "Xfinium.pdf"
			circle2.Contents = "Circle annotation with blue interior"
			circle2.BorderColor = Nothing
			circle2.BorderWidth = 0
			circle2.InteriorColor = New PdfRgbColor(0, 0, 192)
			circle2.VisualRectangle = New PdfVisualRectangle(350, 270, 250, 150)

			Dim circle3 As New PdfCircleAnnotation()
			page.Annotations.Add(circle3)
			circle3.Author = "Xfinium.pdf"
			circle3.Contents = "Circle annotation with yellow border and green interior"
			circle3.BorderColor = New PdfRgbColor(255, 255, 0)
			circle3.BorderWidth = 3
			circle3.InteriorColor = New PdfRgbColor(0, 192, 0)
			circle3.VisualRectangle = New PdfVisualRectangle(350, 470, 250, 150)
		End Sub

		Private Shared Sub CreateFileAttachmentAnnotations(document As PdfFixedDocument, font As PdfFont)
			Dim blackBrush As New PdfBrush()
			Dim rnd As New Random()
			' Random binary data to be used a file content for file attachment annotations.
			Dim fileData As Byte() = New Byte(255) {}

			Dim page As PdfPage = document.Pages.Add()

			Dim fileAttachmentAnnotationNames As String() = New String() {"Graph", "Paperclip", "PushPin", "Tag"}

			page.Graphics.DrawString("B/W file attachment annotations", font, blackBrush, 50, 50)
			For i As Integer = 0 To fileAttachmentAnnotationNames.Length - 1
				rnd.NextBytes(fileData)

				Dim faa As New PdfFileAttachmentAnnotation()
				faa.Author = "Xfinium.Pdf"
				faa.Contents = "I am a " & fileAttachmentAnnotationNames(i) & " annotation."
				faa.IconName = fileAttachmentAnnotationNames(i)
				faa.Payload = fileData
				faa.FileName = "BlackAndWhite" & fileAttachmentAnnotationNames(i) & ".dat"
				page.Annotations.Add(faa)
				faa.Location = New PdfPoint(50, 100 + 40 * i)
				page.Graphics.DrawString(fileAttachmentAnnotationNames(i), font, blackBrush, 90, 100 + 40 * i)
			Next

			Dim rgb As Byte() = New Byte(2) {}
			page.Graphics.DrawString("Color file attachment annotations", font, blackBrush, 300, 50)
			For i As Integer = 0 To fileAttachmentAnnotationNames.Length - 1
				rnd.NextBytes(fileData)

				Dim faa As New PdfFileAttachmentAnnotation()
				faa.Author = "Xfinium.Pdf"
				faa.Contents = "I am a " & fileAttachmentAnnotationNames(i) & " annotation."
				faa.IconName = fileAttachmentAnnotationNames(i)
				faa.Payload = fileData
				faa.FileName = "Color" & fileAttachmentAnnotationNames(i) & ".dat"
				rnd.NextBytes(rgb)
				faa.OutlineColor = New PdfRgbColor(rgb(0), rgb(1), rgb(2))
				rnd.NextBytes(rgb)
				faa.InteriorColor = New PdfRgbColor(rgb(0), rgb(1), rgb(2))
				page.Annotations.Add(faa)
				faa.Location = New PdfPoint(300, 100 + 40 * i)
				page.Graphics.DrawString(fileAttachmentAnnotationNames(i), font, blackBrush, 340, 100 + 40 * i)
			Next

			page.Graphics.DrawString("File attachment annotations with custom icons", font, blackBrush, 50, 700)
			Dim customFileAttachmentAnnotation As New PdfFileAttachmentAnnotation()
			customFileAttachmentAnnotation.Author = "Xfinium.Pdf"
			customFileAttachmentAnnotation.Contents = "File attachment annotation with custom icon."
			page.Annotations.Add(customFileAttachmentAnnotation)
			customFileAttachmentAnnotation.IconName = "Custom icon appearance"
			customFileAttachmentAnnotation.Location = New PdfPoint(50, 720)

			Dim customAppearance As New PdfAnnotationAppearance(50, 50)
			Dim redPen As New PdfPen(New PdfRgbColor(255, 0, 0), 10)
			Dim bluePen As New PdfPen(New PdfRgbColor(0, 0, 192), 10)
			customAppearance.Graphics.DrawRectangle(redPen, 5, 5, 40, 40)
			customAppearance.Graphics.DrawLine(bluePen, 0, 0, customAppearance.Width, customAppearance.Height)
			customAppearance.Graphics.DrawLine(bluePen, 0, customAppearance.Height, customAppearance.Width, 0)
			customAppearance.Graphics.CompressAndClose()
			customFileAttachmentAnnotation.NormalAppearance = customAppearance
		End Sub

		Private Shared Sub CreateInkAnnotations(document As PdfFixedDocument, font As PdfFont)
			Dim blackBrush As New PdfBrush()
			Dim rnd As New Random()

			Dim page As PdfPage = document.Pages.Add()

			page.Graphics.DrawString("Ink annotations", font, blackBrush, 50, 50)

			' The ink annotation will contain 3 lines, each one with 10 points.
			Dim points As PdfPoint()() = New PdfPoint(2)() {}
			For i As Integer = 0 To points.Length - 1
				points(i) = New PdfPoint(9) {}
				For j As Integer = 0 To points(i).Length - 1
					points(i)(j) = New PdfPoint(rnd.NextDouble() * page.Width, rnd.NextDouble() * page.Height)
				Next
			Next

			Dim ia As New PdfInkAnnotation()
			page.Annotations.Add(ia)
			ia.Contents = "I am an ink annotation."
			ia.InkColor = New PdfRgbColor(255, 0, 255)
			ia.InkWidth = 5
			ia.Points = points
		End Sub

		Private Shared Sub CreateLineAnnotations(document As PdfFixedDocument, font As PdfFont)
			Dim blackBrush As New PdfBrush()

			Dim page As PdfPage = document.Pages.Add()

			page.Graphics.DrawString("Line annotations", font, blackBrush, 50, 50)

			Dim les As PdfLineEndSymbol() = New PdfLineEndSymbol() {PdfLineEndSymbol.Circle, PdfLineEndSymbol.ClosedArrow, PdfLineEndSymbol.None, PdfLineEndSymbol.OpenArrow}

			For i As Integer = 0 To les.Length - 1
				Dim la As New PdfLineAnnotation()
				page.Annotations.Add(la)
				la.Author = "Xfinium.Pdf"
				la.Contents = "I am a line annotation with " & les(i).ToString() & " ending."
				la.StartPoint = New PdfPoint(50, 100 + 40 * i)
				la.EndPoint = New PdfPoint(250, 100 + 40 * i)
				la.EndLineSymbol = les(i)
				page.Graphics.DrawString("Line end symbol: " & les(i).ToString(), font, blackBrush, 270, 100 + 40 * i)
			Next
		End Sub

		Private Shared Sub CreatePolygonAnnotations(document As PdfFixedDocument, font As PdfFont)
			Dim blackBrush As New PdfBrush()

			Dim page As PdfPage = document.Pages.Add()

			page.Graphics.DrawString("Polygon annotations", font, blackBrush, 50, 50)

			Dim vertices As Integer() = New Integer() {3, 4, 5, 6}
			Dim centerY As Double = 125, centerX As Double = 150
			Dim radius As Double = 50

			For i As Integer = 0 To vertices.Length - 1
				Dim points As PdfPoint() = New PdfPoint(vertices(i) - 1) {}
				Dim angle As Double = 90
				Dim rotation As Double = 360 \ vertices(i)

				For j As Integer = 0 To vertices(i) - 1
					points(j) = New PdfPoint()
					points(j).X = centerX + radius * Math.Cos(Math.PI * angle / 180)
					points(j).Y = centerY - radius * Math.Sin(Math.PI * angle / 180)
					angle = angle + rotation
				Next

				Dim polygon As New PdfPolygonAnnotation()
				page.Annotations.Add(polygon)
				polygon.Author = "Xfinium.Pdf"
				polygon.Contents = "Polygon annotation with " & vertices(i) & " vertices."
				polygon.Points = points
				polygon.LineColor = New PdfRgbColor(192, 0, 0)
				polygon.LineWidth = 3
				polygon.InteriorColor = New PdfRgbColor(0, 0, 192)

				centerY = centerY + 150
			Next
		End Sub

		Private Shared Sub CreatePolylineAnnotations(document As PdfFixedDocument, font As PdfFont)
			Dim blackBrush As New PdfBrush()

			Dim page As PdfPage = document.Pages.Add()

			page.Graphics.DrawString("Polyline annotations", font, blackBrush, 50, 50)

			Dim vertices As Integer() = New Integer() {3, 4, 5, 6}
			Dim centerY As Double = 125, centerX As Double = 150
			Dim radius As Double = 50

			For i As Integer = 0 To vertices.Length - 1
				Dim points As PdfPoint() = New PdfPoint(vertices(i) - 1) {}
				Dim angle As Double = 90
				Dim rotation As Double = 360 \ vertices(i)

				For j As Integer = 0 To vertices(i) - 1
					points(j) = New PdfPoint()
					points(j).X = centerX + radius * Math.Cos(Math.PI * angle / 180)
					points(j).Y = centerY - radius * Math.Sin(Math.PI * angle / 180)
					angle = angle + rotation
				Next

				Dim polyline As New PdfPolylineAnnotation()
				page.Annotations.Add(polyline)
				polyline.Author = "Xfinium.Pdf"
				polyline.Contents = "Polyline annotation with " & vertices(i) & " vertices."
				polyline.Points = points
				polyline.LineColor = New PdfRgbColor(192, 0, 0)
				polyline.LineWidth = 3

				centerY = centerY + 150
			Next
		End Sub

		Private Shared Sub CreateRubberStampAnnotations(document As PdfFixedDocument, font As PdfFont)
			Dim blackBrush As New PdfBrush()

			Dim page As PdfPage = document.Pages.Add()

			Dim rubberStampAnnotationNames As String() = New String() {"Approved", "AsIs", "Confidential", "Departmental", "Draft", "Experimental", _
				"Expired", "Final", "ForComment", "ForPublicRelease", "NotApproved", "NotForPublicRelease", _
				"Sold", "TopSecret"}

			page.Graphics.DrawString("Rubber stamp annotations", font, blackBrush, 50, 50)
			For i As Integer = 0 To rubberStampAnnotationNames.Length - 1
				Dim rsa As New PdfRubberStampAnnotation()
				rsa.Author = "Xfinium.Pdf"
				rsa.Contents = "I am a " & rubberStampAnnotationNames(i) & " rubber stamp annotation."
				rsa.StampName = rubberStampAnnotationNames(i)
				page.Annotations.Add(rsa)
				rsa.VisualRectangle = New PdfVisualRectangle(50, 70 + 50 * i, 200, 40)
				page.Graphics.DrawString(rubberStampAnnotationNames(i), font, blackBrush, 270, 85 + 50 * i)
			Next

			page.Graphics.DrawString("Stamp annotations with custom appearance", font, blackBrush, 350, 50)
			Dim customRubberStampAnnotation As New PdfRubberStampAnnotation()
			customRubberStampAnnotation.Contents = "Rubber stamp annotation with custom appearance."
			customRubberStampAnnotation.StampName = "Custom"
			page.Annotations.Add(customRubberStampAnnotation)
			customRubberStampAnnotation.VisualRectangle = New PdfVisualRectangle(350, 70, 200, 40)

			Dim customAppearance As New PdfAnnotationAppearance(50, 50)
			Dim redPen As New PdfPen(New PdfRgbColor(255, 0, 0), 10)
			Dim bluePen As New PdfPen(New PdfRgbColor(0, 0, 192), 10)
			customAppearance.Graphics.DrawRectangle(redPen, 5, 5, 40, 40)
			customAppearance.Graphics.DrawLine(bluePen, 0, 0, customAppearance.Width, customAppearance.Height)
			customAppearance.Graphics.DrawLine(bluePen, 0, customAppearance.Height, customAppearance.Width, 0)
			customAppearance.Graphics.CompressAndClose()
			customRubberStampAnnotation.NormalAppearance = customAppearance
		End Sub

		Private Shared Sub CreateTextMarkupAnnotations(document As PdfFixedDocument, font As PdfFont)
			Dim blackBrush As New PdfBrush()

			Dim page As PdfPage = document.Pages.Add()

			page.Graphics.DrawString("Text markup annotations", font, blackBrush, 50, 50)

			Dim tmat As PdfTextMarkupAnnotationType() = New PdfTextMarkupAnnotationType() {PdfTextMarkupAnnotationType.Highlight, PdfTextMarkupAnnotationType.Squiggly, PdfTextMarkupAnnotationType.StrikeOut, PdfTextMarkupAnnotationType.Underline}

			Dim sao As New PdfStringAppearanceOptions()
			sao.Brush = blackBrush
			sao.Font = font

			Dim slo As New PdfStringLayoutOptions()
			slo.HorizontalAlign = PdfStringHorizontalAlign.Center
			slo.VerticalAlign = PdfStringVerticalAlign.Bottom
			For i As Integer = 0 To tmat.Length - 1
				Dim tma As New PdfTextMarkupAnnotation()
				page.Annotations.Add(tma)
				tma.VisualRectangle = New PdfVisualRectangle(50, 100 + 50 * i, 200, font.Size + 2)
				tma.TextMarkupType = tmat(i)

				slo.X = 150
				slo.Y = 100 + 50 * i + font.Size

				page.Graphics.DrawString(tmat(i).ToString() & " annotation.", sao, slo)
			Next
		End Sub

		Private Shared Sub CreateRichMediaAnnotations(document As PdfFixedDocument, font As PdfFont, flashStream As Stream)
			Dim blackBrush As New PdfBrush()

			Dim page As PdfPage = document.Pages.Add()

			page.Graphics.DrawString("Rich media annotations", font, blackBrush, 50, 50)

			Dim flashContent As Byte() = New Byte(flashStream.Length - 1) {}
			flashStream.Read(flashContent, 0, flashContent.Length)

			Dim rma As New PdfRichMediaAnnotation()
			page.Annotations.Add(rma)
			rma.VisualRectangle = New PdfVisualRectangle(100, 100, 400, 400)
			rma.FlashPayload = flashContent
			rma.FlashFile = "clock.swf"
			rma.ActivationCondition = PdfRichMediaActivationCondition.PageVisible
		End Sub

		Private Shared Sub Create3DAnnotations(document As PdfFixedDocument, font As PdfFont, u3dStream As Stream)
			Dim blackBrush As New PdfBrush()

			Dim page As PdfPage = document.Pages.Add()
			page.Rotation = 90

			page.Graphics.DrawString("3D annotations", font, blackBrush, 50, 50)

			Dim u3dContent As Byte() = New Byte(u3dStream.Length - 1) {}
			u3dStream.Read(u3dContent, 0, u3dContent.Length)

			Dim view0 As New Pdf3DView()
			view0.CameraToWorldMatrix = New Double() {1, 0, 0, 0, 0, -1, _
				0, 1, 0, -0.417542, -0.881257, -0.125705}
			view0.CenterOfOrbit = 0.123106
			view0.ExternalName = "Default"
			view0.InternalName = "Default"
			view0.Projection = New Pdf3DProjection()
			view0.Projection.FieldOfView = 30

			Dim view1 As New Pdf3DView()
			view1.CameraToWorldMatrix = New Double() {-0.999888, 0.014949, 0, 0.014949, 0.999887, 0.00157084, _
				2.34825E-05, 0.00157066, -0.999999, -0.416654, -0.761122, -0.00184508}
			view1.CenterOfOrbit = 0.123106
			view1.ExternalName = "Top"
			view1.InternalName = "Top"
			view1.Projection = New Pdf3DProjection()
			view1.Projection.FieldOfView = 14.8096

			Dim view2 As New Pdf3DView()
			view2.CameraToWorldMatrix = New Double() {-1.0, -4.11671E-05, 5.09201E-11, -1.01387E-06, 0.0246288, 0.999697, _
				-4.11546E-05, 0.999697, -0.0246288, -0.417072, -0.881787, -0.121915}
			view2.CenterOfOrbit = 0.123106
			view2.ExternalName = "Side"
			view2.InternalName = "Side"
			view2.Projection = New Pdf3DProjection()
			view2.Projection.FieldOfView = 12.3794

			Dim view3 As New Pdf3DView()
			view3.CameraToWorldMatrix = New Double() {-0.797002, -0.603977, -4.38577E-08, -0.294384, 0.388467, 0.873173, _
				-0.527376, 0.695921, -0.48741, -0.3518, -0.844506, -0.0675629}
			view3.CenterOfOrbit = 0.123106
			view3.ExternalName = "Isometric"
			view3.InternalName = "Isometric"
			view3.Projection = New Pdf3DProjection()
			view3.Projection.FieldOfView = 15.1226

			Dim view4 As New Pdf3DView()
			view4.CameraToWorldMatrix = New Double() {0.00737633, -0.999973, -1.47744E-11, -0.0656414, -0.000484206, 0.997843, _
				-0.997816, -0.00736042, -0.0656432, -0.293887, -0.757928, -0.119485}
			view4.CenterOfOrbit = 0.123106
			view4.ExternalName = "Front"
			view4.InternalName = "Front"
			view4.Projection = New Pdf3DProjection()
			view4.Projection.FieldOfView = 15.1226

			Dim view5 As New Pdf3DView()
			view5.CameraToWorldMatrix = New Double() {0.0151008, 0.999886, 2.61366E-11, 0.0492408, -0.000743662, 0.998787, _
				0.998673, -0.0150825, -0.0492464, -0.540019, -0.756862, -0.118884}
			view5.CenterOfOrbit = 0.123106
			view5.ExternalName = "Back"
			view5.InternalName = "Back"
			view5.Projection = New Pdf3DProjection()
			view5.Projection.FieldOfView = 12.3794

			Dim _3dStream As New Pdf3DStream()
			_3dStream.Views.Add(view0)
			_3dStream.Views.Add(view1)
			_3dStream.Views.Add(view2)
			_3dStream.Views.Add(view3)
			_3dStream.Views.Add(view4)
			_3dStream.Views.Add(view5)
			_3dStream.Content = u3dContent
			_3dStream.DefaultViewIndex = 0
			Dim _3da As New Pdf3DAnnotation()
			_3da.Stream = _3dStream

			Dim appearance As New PdfAnnotationAppearance(200, 200)
			appearance.Graphics.DrawString("Click to activate", font, blackBrush, 50, 50)
			_3da.NormalAppearance = appearance

			page.Annotations.Add(_3da)
			_3da.VisualRectangle = New PdfVisualRectangle(36, 36, 720, 540)

			Dim sao As New PdfStringAppearanceOptions()
			sao.Font = font
			sao.Brush = blackBrush
			Dim slo As New PdfStringLayoutOptions()
			slo.Y = 585 + 18 \ 2
			slo.HorizontalAlign = PdfStringHorizontalAlign.Center
			slo.VerticalAlign = PdfStringVerticalAlign.Middle

			Dim blackPen As New PdfPen(New PdfRgbColor(0, 0, 0), 1)

			page.Graphics.DrawRectangle(blackPen, 50, 585, 120, 18)
			slo.X = 50 + 120 \ 2
			page.Graphics.DrawString("Top", sao, slo)

			Dim gotoTopView As New PdfGoTo3DViewAction()
			gotoTopView.ViewIndex = 1
			gotoTopView.TargetAnnotation = _3da
			Dim linkGotoTopView As New PdfLinkAnnotation()
			page.Annotations.Add(linkGotoTopView)
			linkGotoTopView.VisualRectangle = New PdfVisualRectangle(50, 585, 120, 18)
			linkGotoTopView.Action = gotoTopView

			page.Graphics.DrawRectangle(blackPen, 190, 585, 120, 18)
			slo.X = 190 + 120 \ 2
			page.Graphics.DrawString("Side", sao, slo)

			Dim gotoSideView As New PdfGoTo3DViewAction()
			gotoSideView.ViewIndex = 2
			gotoSideView.TargetAnnotation = _3da
			Dim linkGotoSideView As New PdfLinkAnnotation()
			page.Annotations.Add(linkGotoSideView)
			linkGotoSideView.VisualRectangle = New PdfVisualRectangle(190, 585, 120, 18)
			linkGotoSideView.Action = gotoSideView

			page.Graphics.DrawRectangle(blackPen, 330, 585, 120, 18)
			slo.X = 330 + 120 \ 2
			page.Graphics.DrawString("Isometric", sao, slo)

			Dim gotoIsometricView As New PdfGoTo3DViewAction()
			gotoIsometricView.ViewIndex = 3
			gotoIsometricView.TargetAnnotation = _3da
			Dim linkGotoIsometricView As New PdfLinkAnnotation()
			page.Annotations.Add(linkGotoIsometricView)
			linkGotoIsometricView.VisualRectangle = New PdfVisualRectangle(330, 585, 120, 18)
			linkGotoIsometricView.Action = gotoIsometricView

			page.Graphics.DrawRectangle(blackPen, 470, 585, 120, 18)
			slo.X = 470 + 120 \ 2
			page.Graphics.DrawString("Front", sao, slo)

			Dim gotoFrontView As New PdfGoTo3DViewAction()
			gotoFrontView.ViewIndex = 4
			gotoFrontView.TargetAnnotation = _3da
			Dim linkGotoFrontView As New PdfLinkAnnotation()
			page.Annotations.Add(linkGotoFrontView)
			linkGotoFrontView.VisualRectangle = New PdfVisualRectangle(470, 585, 120, 18)
			linkGotoFrontView.Action = gotoFrontView

			page.Graphics.DrawRectangle(blackPen, 610, 585, 120, 18)
			slo.X = 610 + 120 \ 2
			page.Graphics.DrawString("Back", sao, slo)

			Dim gotoBackView As New PdfGoTo3DViewAction()
			gotoBackView.ViewIndex = 5
			gotoBackView.TargetAnnotation = _3da
			Dim linkGotoBackView As New PdfLinkAnnotation()
			page.Annotations.Add(linkGotoBackView)
			linkGotoBackView.VisualRectangle = New PdfVisualRectangle(610, 585, 120, 18)
			linkGotoBackView.Action = gotoBackView
		End Sub

		Private Shared Sub CreateRedactionAnnotations(document As PdfFixedDocument, font As PdfFont, flashStream As Stream)
			Dim blackBrush As New PdfBrush()

			Dim page As PdfPage = document.Pages.Add()

			page.Graphics.DrawString("Redaction annotations", font, blackBrush, 50, 50)

			Dim helvetica As New PdfStandardFont(PdfStandardFontFace.Helvetica, 10)
			page.Graphics.DrawString("Open the file with Adobe Acrobat, right click on the annotation and select ""Apply redactions"". The text under the annotation will be redacted.", helvetica, blackBrush, 50, 110)

			Dim redactionAppearance As New PdfFormXObject(250, 150)
			redactionAppearance.Graphics.DrawRectangle(New PdfBrush(PdfRgbColor.LightGreen), 0, 0, redactionAppearance.Width, redactionAppearance.Height)
			Dim sao As New PdfStringAppearanceOptions()
			sao.Brush = New PdfBrush(PdfRgbColor.DarkRed)
			sao.Font = New PdfStandardFont(PdfStandardFontFace.HelveticaBold, 32)
			Dim slo As New PdfStringLayoutOptions()
			slo.Width = redactionAppearance.Width
			slo.Height = redactionAppearance.Height
			slo.X = 0
			slo.Y = 0
			slo.HorizontalAlign = PdfStringHorizontalAlign.Center
			slo.VerticalAlign = PdfStringVerticalAlign.Middle
			redactionAppearance.Graphics.DrawString("This content has been redacted", sao, slo)

			Dim redactionAnnotation As New PdfRedactionAnnotation()
			page.Annotations.Add(redactionAnnotation)
			redactionAnnotation.Author = "XFINIUM.PDF"
			redactionAnnotation.BorderColor = New PdfRgbColor(192, 0, 0)
			redactionAnnotation.BorderWidth = 1
			redactionAnnotation.OverlayAppearance = redactionAppearance
			redactionAnnotation.VisualRectangle = New PdfVisualRectangle(50, 100, 250, 150)
		End Sub
	End Class
End Namespace