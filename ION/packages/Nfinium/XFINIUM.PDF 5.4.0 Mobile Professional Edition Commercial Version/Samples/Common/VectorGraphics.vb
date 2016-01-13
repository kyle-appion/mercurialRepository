Imports System.IO
Imports Xfinium.Pdf
Imports Xfinium.Pdf.Graphics
Imports Xfinium.Pdf.PdfFunctions

Namespace Xfinium.Pdf.Samples
	''' <summary>
	''' VectorGraphics sample.
	''' </summary>
	Public Class VectorGraphics
		''' <summary>
		''' Main method for running the sample.
		''' </summary>
		''' <param name="iccStream"></param>
		Public Shared Function Run(iccStream As Stream) As SampleOutputInfo()
			Dim document As New PdfFixedDocument()
			Dim helveticaBoldTitle As New PdfStandardFont(PdfStandardFontFace.HelveticaBold, 16)
			Dim helveticaSection As New PdfStandardFont(PdfStandardFontFace.Helvetica, 10)

			Dim page As PdfPage = document.Pages.Add()
			DrawLines(page, helveticaBoldTitle, helveticaSection)

			page = document.Pages.Add()
			DrawRectangles(page, helveticaBoldTitle, helveticaSection)

			page = document.Pages.Add()
			DrawRoundRectangles(page, helveticaBoldTitle, helveticaSection)

			page = document.Pages.Add()
			DrawEllipses(page, helveticaBoldTitle, helveticaSection)

			page = document.Pages.Add()
			DrawArcsAndPies(page, helveticaBoldTitle, helveticaSection)

			page = document.Pages.Add()
			DrawBezierCurves(page, helveticaBoldTitle, helveticaSection)

			page = document.Pages.Add()
			DrawAffineTransformations(page, helveticaBoldTitle, helveticaSection)

			page = document.Pages.Add()
			DrawColorsAndColorSpaces(page, helveticaBoldTitle, helveticaSection, iccStream)

			page = document.Pages.Add()
			DrawShadings(page, helveticaBoldTitle, helveticaSection)

			page = document.Pages.Add()
			DrawPatterns(page, helveticaBoldTitle, helveticaSection)

			page = document.Pages.Add()
			DrawFormXObjects(page, helveticaBoldTitle, helveticaSection)

			Dim output As SampleOutputInfo() = New SampleOutputInfo() {New SampleOutputInfo(document, "xfinium.pdf.sample.vectorgraphics.pdf")}
			Return output
		End Function

		Private Shared Sub DrawLines(page As PdfPage, titleFont As PdfFont, sectionFont As PdfFont)
			Dim brush As New PdfBrush()
			Dim blackPen As New PdfPen(PdfRgbColor.Black, 1)
			Dim bluePen As New PdfPen(PdfRgbColor.LightBlue, 16)

			page.Graphics.DrawString("Lines", titleFont, brush, 20, 50)

			page.Graphics.DrawString("Line styles:", sectionFont, brush, 20, 70)
			page.Graphics.DrawString("Solid", sectionFont, brush, 20, 90)
			page.Graphics.DrawLine(blackPen, 100, 95, 400, 95)
			page.Graphics.DrawString("Dashed", sectionFont, brush, 20, 110)
			blackPen.DashPattern = New Double() {3, 3}
			page.Graphics.DrawLine(blackPen, 100, 115, 400, 115)

			page.Graphics.DrawString("Line cap styles:", sectionFont, brush, 20, 150)
			page.Graphics.DrawString("Flat", sectionFont, brush, 20, 175)
			page.Graphics.DrawLine(bluePen, 100, 180, 400, 180)
			blackPen.DashPattern = Nothing
			page.Graphics.DrawLine(blackPen, 100, 180, 400, 180)
			page.Graphics.DrawString("Square", sectionFont, brush, 20, 195)
			bluePen.LineCap = PdfLineCap.Square
			page.Graphics.DrawLine(bluePen, 100, 200, 400, 200)
			blackPen.DashPattern = Nothing
			page.Graphics.DrawLine(blackPen, 100, 200, 400, 200)
			page.Graphics.DrawString("Round", sectionFont, brush, 20, 215)
			bluePen.LineCap = PdfLineCap.Round
			page.Graphics.DrawLine(bluePen, 100, 220, 400, 220)
			blackPen.DashPattern = Nothing
			page.Graphics.DrawLine(blackPen, 100, 220, 400, 220)

			page.Graphics.DrawString("Line join styles:", sectionFont, brush, 20, 250)
			page.Graphics.DrawString("Miter", sectionFont, brush, 20, 280)
			Dim miterPath As New PdfPath()
			miterPath.StartSubpath(150, 320)
			miterPath.AddLineTo(250, 260)
			miterPath.AddLineTo(350, 320)
			bluePen.LineCap = PdfLineCap.Flat
			bluePen.LineJoin = PdfLineJoin.Miter
			page.Graphics.DrawPath(bluePen, miterPath)

			page.Graphics.DrawString("Bevel", sectionFont, brush, 20, 360)
			Dim bevelPath As New PdfPath()
			bevelPath.StartSubpath(150, 400)
			bevelPath.AddLineTo(250, 340)
			bevelPath.AddLineTo(350, 400)
			bluePen.LineCap = PdfLineCap.Flat
			bluePen.LineJoin = PdfLineJoin.Bevel
			page.Graphics.DrawPath(bluePen, bevelPath)

			page.Graphics.DrawString("Round", sectionFont, brush, 20, 440)
			Dim roundPath As New PdfPath()
			roundPath.StartSubpath(150, 480)
			roundPath.AddLineTo(250, 420)
			roundPath.AddLineTo(350, 480)
			bluePen.LineCap = PdfLineCap.Flat
			bluePen.LineJoin = PdfLineJoin.Round
			page.Graphics.DrawPath(bluePen, roundPath)

			page.Graphics.DrawString("Random lines clipped to rectangle", sectionFont, brush, 20, 520)
			Dim clipPath As New PdfPath()
			clipPath.AddRectangle(20, 550, 570, 230)

			page.Graphics.SaveGraphicsState()
			page.Graphics.SetClip(clipPath)

			Dim randomColor As New PdfRgbColor()
			Dim randomPen As New PdfPen(randomColor, 1)
			Dim rnd As New Random()
			For i As Integer = 0 To 99
				randomColor.R = CByte(rnd.[Next](256))
				randomColor.G = CByte(rnd.[Next](256))
				randomColor.B = CByte(rnd.[Next](256))

				page.Graphics.DrawLine(randomPen, rnd.NextDouble() * page.Width, 550 + rnd.NextDouble() * 250, rnd.NextDouble() * page.Width, 550 + rnd.NextDouble() * 250)
			Next

			page.Graphics.RestoreGraphicsState()

			page.Graphics.DrawPath(blackPen, clipPath)

			page.Graphics.CompressAndClose()
		End Sub

		Private Shared Sub DrawRectangles(page As PdfPage, titleFont As PdfFont, sectionFont As PdfFont)
			Dim brush As New PdfBrush()
			Dim blackPen As New PdfPen(PdfRgbColor.Black, 1)
			Dim redPen As New PdfPen(PdfRgbColor.Red, 1)

			Dim randomPenColor As New PdfRgbColor()
			Dim randomPen As New PdfPen(randomPenColor, 1)
			Dim randomBrushColor As New PdfRgbColor()
			Dim randomBrush As New PdfBrush(randomBrushColor)

			page.Graphics.DrawString("Rectangles", titleFont, brush, 20, 50)

			page.Graphics.DrawLine(blackPen, 20, 150, 300, 150)
			page.Graphics.DrawLine(blackPen, 80, 70, 80, 350)
			page.Graphics.DrawRectangle(redPen, 80, 150, 180, 100)

			page.Graphics.DrawLine(blackPen, 320, 150, 600, 150)
			page.Graphics.DrawLine(blackPen, 380, 70, 380, 350)
			page.Graphics.DrawRectangle(redPen, 380, 150, 180, 100, 30)

			page.Graphics.DrawString("Random rectangles clipped to view", sectionFont, brush, 20, 385)
			Dim rectPath As New PdfPath()
			rectPath.AddRectangle(20, 400, 570, 300)

			page.Graphics.SaveGraphicsState()
			page.Graphics.SetClip(rectPath)

			Dim rnd As New Random()
			For i As Integer = 0 To 99
				randomPenColor.R = CByte(rnd.[Next](256))
				randomPenColor.G = CByte(rnd.[Next](256))
				randomPenColor.B = CByte(rnd.[Next](256))

				randomBrushColor.R = CByte(rnd.[Next](256))
				randomBrushColor.G = CByte(rnd.[Next](256))
				randomBrushColor.B = CByte(rnd.[Next](256))

				Dim mode As Integer = rnd.[Next](3)
				Dim left As Double = rnd.NextDouble() * page.Width
				Dim top As Double = 380 + rnd.NextDouble() * 350
				Dim width As Double = rnd.NextDouble() * page.Width
				Dim height As Double = rnd.NextDouble() * 250
				Dim orientation As Double = rnd.[Next](360)
				Select Case mode
					Case 0
						' Stroke rectangle outline
						page.Graphics.DrawRectangle(randomPen, left, top, width, height, orientation)
						Exit Select
					Case 1
						' Fill rectangle interior
						page.Graphics.DrawRectangle(randomBrush, left, top, width, height, orientation)
						Exit Select
					Case 2
						' Stroke and fill rectangle
						page.Graphics.DrawRectangle(randomPen, randomBrush, left, top, width, height, _
							orientation)
						Exit Select
				End Select
			Next

			page.Graphics.RestoreGraphicsState()

			page.Graphics.DrawPath(blackPen, rectPath)

			page.Graphics.CompressAndClose()
		End Sub

		Private Shared Sub DrawRoundRectangles(page As PdfPage, titleFont As PdfFont, sectionFont As PdfFont)
			Dim brush As New PdfBrush()
			Dim blackPen As New PdfPen(PdfRgbColor.Black, 1)
			Dim redPen As New PdfPen(PdfRgbColor.Red, 1)

			Dim randomPenColor As New PdfRgbColor()
			Dim randomPen As New PdfPen(randomPenColor, 1)
			Dim randomBrushColor As New PdfRgbColor()
			Dim randomBrush As New PdfBrush(randomBrushColor)

			page.Graphics.DrawString("Round rectangles", titleFont, brush, 20, 50)

			page.Graphics.DrawLine(blackPen, 20, 150, 300, 150)
			page.Graphics.DrawLine(blackPen, 80, 70, 80, 350)
			page.Graphics.DrawRoundRectangle(redPen, 80, 150, 180, 100, 20, _
				20)

			page.Graphics.DrawLine(blackPen, 320, 150, 600, 150)
			page.Graphics.DrawLine(blackPen, 380, 70, 380, 350)
			page.Graphics.DrawRoundRectangle(redPen, 380, 150, 180, 100, 20, _
				20, 30)

			page.Graphics.DrawString("Random round rectangles clipped to view", sectionFont, brush, 20, 385)
			Dim roundRectPath As New PdfPath()
			roundRectPath.AddRoundRectangle(20, 400, 570, 300, 20, 20)

			page.Graphics.SaveGraphicsState()
			page.Graphics.SetClip(roundRectPath)

			Dim rnd As New Random()
			For i As Integer = 0 To 99
				randomPenColor.R = CByte(rnd.[Next](256))
				randomPenColor.G = CByte(rnd.[Next](256))
				randomPenColor.B = CByte(rnd.[Next](256))

				randomBrushColor.R = CByte(rnd.[Next](256))
				randomBrushColor.G = CByte(rnd.[Next](256))
				randomBrushColor.B = CByte(rnd.[Next](256))

				Dim mode As Integer = rnd.[Next](3)
				Dim left As Double = rnd.NextDouble() * page.Width
				Dim top As Double = 380 + rnd.NextDouble() * 350
				Dim width As Double = rnd.NextDouble() * page.Width
				Dim height As Double = rnd.NextDouble() * 250
				Dim orientation As Double = rnd.[Next](360)
				Select Case mode
					Case 0
						' Stroke rectangle outline
						page.Graphics.DrawRoundRectangle(randomPen, left, top, width, height, width * 0.1, _
							height * 0.1, orientation)
						Exit Select
					Case 1
						' Fill rectangle interior
						page.Graphics.DrawRoundRectangle(randomBrush, left, top, width, height, width * 0.1, _
							height * 0.1, orientation)
						Exit Select
					Case 2
						' Stroke and fill rectangle
						page.Graphics.DrawRoundRectangle(randomPen, randomBrush, left, top, width, height, _
							width * 0.1, height * 0.1, orientation)
						Exit Select
				End Select
			Next

			page.Graphics.RestoreGraphicsState()

			page.Graphics.DrawPath(blackPen, roundRectPath)

			page.Graphics.CompressAndClose()
		End Sub

		Private Shared Sub DrawEllipses(page As PdfPage, titleFont As PdfFont, sectionFont As PdfFont)
			Dim brush As New PdfBrush()
			Dim blackPen As New PdfPen(PdfRgbColor.Black, 1)
			Dim redPen As New PdfPen(PdfRgbColor.Red, 1)

			Dim randomPenColor As New PdfRgbColor()
			Dim randomPen As New PdfPen(randomPenColor, 1)
			Dim randomBrushColor As New PdfRgbColor()
			Dim randomBrush As New PdfBrush(randomBrushColor)

			page.Graphics.DrawString("Ellipses", titleFont, brush, 20, 50)

			page.Graphics.DrawLine(blackPen, 20, 150, 300, 150)
			page.Graphics.DrawLine(blackPen, 80, 70, 80, 350)
			page.Graphics.DrawEllipse(redPen, 80, 150, 180, 100)

			page.Graphics.DrawLine(blackPen, 320, 150, 600, 150)
			page.Graphics.DrawLine(blackPen, 380, 70, 380, 350)
			page.Graphics.DrawEllipse(redPen, 380, 150, 180, 100, 30)

			page.Graphics.DrawString("Random ellipses clipped to view", sectionFont, brush, 20, 385)
			Dim ellipsePath As New PdfPath()
			ellipsePath.AddEllipse(20, 400, 570, 300)

			page.Graphics.SaveGraphicsState()
			page.Graphics.SetClip(ellipsePath)

			Dim rnd As New Random()
			For i As Integer = 0 To 99
				randomPenColor.R = CByte(rnd.[Next](256))
				randomPenColor.G = CByte(rnd.[Next](256))
				randomPenColor.B = CByte(rnd.[Next](256))

				randomBrushColor.R = CByte(rnd.[Next](256))
				randomBrushColor.G = CByte(rnd.[Next](256))
				randomBrushColor.B = CByte(rnd.[Next](256))

				Dim mode As Integer = rnd.[Next](3)
				Dim left As Double = rnd.NextDouble() * page.Width
				Dim top As Double = 380 + rnd.NextDouble() * 350
				Dim width As Double = rnd.NextDouble() * page.Width
				Dim height As Double = rnd.NextDouble() * 250
				Dim orientation As Double = rnd.[Next](360)
				Select Case mode
					Case 0
						' Stroke ellipse outline
						page.Graphics.DrawEllipse(randomPen, left, top, width, height, orientation)
						Exit Select
					Case 1
						' Fill ellipse interior
						page.Graphics.DrawEllipse(randomBrush, left, top, width, height, orientation)
						Exit Select
					Case 2
						' Stroke and fill ellipse
						page.Graphics.DrawEllipse(randomPen, randomBrush, left, top, width, height, _
							orientation)
						Exit Select
				End Select
			Next

			page.Graphics.RestoreGraphicsState()

			page.Graphics.DrawPath(blackPen, ellipsePath)

			page.Graphics.CompressAndClose()
		End Sub

		Private Shared Sub DrawArcsAndPies(page As PdfPage, titleFont As PdfFont, sectionFont As PdfFont)
			Dim brush As New PdfBrush()
			Dim blackPen As New PdfPen(PdfRgbColor.Black, 1)
			Dim redPen As New PdfPen(PdfRgbColor.Red, 1)

			Dim randomPenColor As New PdfRgbColor()
			Dim randomPen As New PdfPen(randomPenColor, 1)
			Dim randomBrushColor As New PdfRgbColor()
			Dim randomBrush As New PdfBrush(randomBrushColor)

			page.Graphics.DrawString("Arcs", titleFont, brush, 20, 50)
			page.Graphics.DrawString("Pies", titleFont, brush, 310, 50)

			page.Graphics.DrawLine(blackPen, 20, 210, 300, 210)
			page.Graphics.DrawLine(blackPen, 160, 70, 160, 350)
			page.Graphics.DrawLine(blackPen, 310, 210, 590, 210)
			page.Graphics.DrawLine(blackPen, 450, 70, 450, 350)

			blackPen.DashPattern = New Double() {2, 2}
			page.Graphics.DrawLine(blackPen, 20, 70, 300, 350)
			page.Graphics.DrawLine(blackPen, 20, 350, 300, 70)
			page.Graphics.DrawLine(blackPen, 310, 70, 590, 350)
			page.Graphics.DrawLine(blackPen, 310, 350, 590, 70)

			page.Graphics.DrawArc(redPen, 30, 80, 260, 260, 0, _
				135)
			page.Graphics.DrawPie(redPen, 320, 80, 260, 260, 45, _
				270)

			page.Graphics.DrawString("Random arcs and pies clipped to view", sectionFont, brush, 20, 385)
			Dim rectPath As New PdfPath()
			rectPath.AddRectangle(20, 400, 570, 300)

			page.Graphics.SaveGraphicsState()
			page.Graphics.SetClip(rectPath)

			Dim rnd As New Random()
			For i As Integer = 0 To 99
				randomPenColor.R = CByte(rnd.[Next](256))
				randomPenColor.G = CByte(rnd.[Next](256))
				randomPenColor.B = CByte(rnd.[Next](256))

				randomBrushColor.R = CByte(rnd.[Next](256))
				randomBrushColor.G = CByte(rnd.[Next](256))
				randomBrushColor.B = CByte(rnd.[Next](256))

				Dim mode As Integer = rnd.[Next](4)
				Dim left As Double = rnd.NextDouble() * page.Width
				Dim top As Double = 380 + rnd.NextDouble() * 350
				Dim width As Double = rnd.NextDouble() * page.Width
				Dim height As Double = rnd.NextDouble() * 250
				Dim startAngle As Double = rnd.[Next](360)
				Dim sweepAngle As Double = rnd.[Next](360)
				Select Case mode
					Case 0
						' Stroke arc outline
						page.Graphics.DrawArc(randomPen, left, top, width, height, startAngle, _
							sweepAngle)
						Exit Select
					Case 1
						' Stroke pie outline
						page.Graphics.DrawPie(randomPen, left, top, width, height, startAngle, _
							sweepAngle)
						Exit Select
					Case 2
						' Fill pie interior
						page.Graphics.DrawPie(randomBrush, left, top, width, height, startAngle, _
							sweepAngle)
						Exit Select
					Case 3
						' Stroke and fill pie
						page.Graphics.DrawPie(randomPen, randomBrush, left, top, width, height, _
							startAngle, sweepAngle)
						Exit Select
				End Select
			Next

			page.Graphics.RestoreGraphicsState()

			blackPen.DashPattern = Nothing
			page.Graphics.DrawPath(blackPen, rectPath)

			page.Graphics.CompressAndClose()
		End Sub

		Private Shared Sub DrawBezierCurves(page As PdfPage, titleFont As PdfFont, sectionFont As PdfFont)
			Dim brush As New PdfBrush()
			Dim blackPen As New PdfPen(PdfRgbColor.Black, 1)
			Dim redPen As New PdfPen(PdfRgbColor.Red, 1)
			Dim blueBrush As New PdfBrush(PdfRgbColor.DarkBlue)

			Dim randomPenColor As New PdfRgbColor()
			Dim randomPen As New PdfPen(randomPenColor, 1)

			page.Graphics.DrawString("Bezier curves", titleFont, brush, 20, 50)

			page.Graphics.DrawLine(blackPen, 20, 210, 600, 210)
			page.Graphics.DrawLine(blackPen, 306, 70, 306, 350)
			page.Graphics.DrawRectangle(blueBrush, 39, 339, 2, 2)
			page.Graphics.DrawRectangle(blueBrush, 279, 79, 2, 2)
			page.Graphics.DrawRectangle(blueBrush, 499, 299, 2, 2)
			page.Graphics.DrawRectangle(blueBrush, 589, 69, 2, 2)
			page.Graphics.DrawBezier(redPen, 40, 340, 280, 80, 500, _
				300, 590, 70)

			page.Graphics.DrawString("Random bezier curves clipped to view", sectionFont, brush, 20, 385)
			Dim rectPath As New PdfPath()
			rectPath.AddRectangle(20, 400, 570, 300)

			page.Graphics.SaveGraphicsState()
			page.Graphics.SetClip(rectPath)

			Dim rnd As New Random()
			For i As Integer = 0 To 99
				randomPenColor.R = CByte(rnd.[Next](256))
				randomPenColor.G = CByte(rnd.[Next](256))
				randomPenColor.B = CByte(rnd.[Next](256))

				Dim x1 As Double = rnd.NextDouble() * page.Width
				Dim y1 As Double = 380 + rnd.NextDouble() * 350
				Dim x2 As Double = rnd.NextDouble() * page.Width
				Dim y2 As Double = 380 + rnd.NextDouble() * 350
				Dim x3 As Double = rnd.NextDouble() * page.Width
				Dim y3 As Double = 380 + rnd.NextDouble() * 350
				Dim x4 As Double = rnd.NextDouble() * page.Width
				Dim y4 As Double = 380 + rnd.NextDouble() * 350

				page.Graphics.DrawBezier(randomPen, x1, y1, x2, y2, x3, _
					y3, x4, y4)
			Next

			page.Graphics.RestoreGraphicsState()

			blackPen.DashPattern = Nothing
			page.Graphics.DrawPath(blackPen, rectPath)

			page.Graphics.CompressAndClose()
		End Sub

		Private Shared Sub DrawAffineTransformations(page As PdfPage, titleFont As PdfFont, sectionFont As PdfFont)
			Dim brush As New PdfBrush()
			Dim blackPen As New PdfPen(PdfRgbColor.Black, 1)
			Dim redPen As New PdfPen(PdfRgbColor.Red, 1)
			Dim bluePen As New PdfPen(PdfRgbColor.Blue, 1)
			Dim greenPen As New PdfPen(PdfRgbColor.Green, 1)

			page.Graphics.DrawString("Affine transformations", titleFont, brush, 20, 50)

			page.Graphics.DrawLine(blackPen, 0, page.Height / 2, page.Width, page.Height / 2)
			page.Graphics.DrawLine(blackPen, page.Width / 2, 0, page.Width / 2, page.Height)

			page.Graphics.SaveGraphicsState()

			' Move the coordinate system in the center of the page.
			page.Graphics.TranslateTransform(page.Width / 2, page.Height / 2)

			' Draw a rectangle with the center at (0, 0)
			page.Graphics.DrawRectangle(redPen, -page.Width / 4, -page.Height / 8, page.Width / 2, page.Height / 4)

			' Rotate the coordinate system with 30 degrees.
			page.Graphics.RotateTransform(30)

			' Draw the same rectangle with the center at (0, 0)
			page.Graphics.DrawRectangle(greenPen, -page.Width / 4, -page.Height / 8, page.Width / 2, page.Height / 4)

			' Scale the coordinate system with 1.5
			page.Graphics.ScaleTransform(1.5, 1.5)

			' Draw the same rectangle with the center at (0, 0)
			page.Graphics.DrawRectangle(bluePen, -page.Width / 4, -page.Height / 8, page.Width / 2, page.Height / 4)

			page.Graphics.RestoreGraphicsState()

			page.Graphics.CompressAndClose()
		End Sub

		Private Shared Sub DrawColorsAndColorSpaces(page As PdfPage, titleFont As PdfFont, sectionFont As PdfFont, iccStream As Stream)
			Dim brush As New PdfBrush()

			page.Graphics.DrawString("Colors and colorspaces", titleFont, brush, 20, 50)

			page.Graphics.DrawString("DeviceRGB", sectionFont, brush, 20, 70)
			Dim rgbPen As New PdfPen(PdfRgbColor.DarkRed, 4)
			Dim rgbBrush As New PdfBrush(PdfRgbColor.LightGoldenrodYellow)
			page.Graphics.DrawRectangle(rgbPen, rgbBrush, 20, 85, 250, 100)

			page.Graphics.DrawString("DeviceCMYK", sectionFont, brush, 340, 70)
			Dim cmykPen As New PdfPen(New PdfCmykColor(1, 0.5, 0, 0.1), 4)
			Dim cmykBrush As New PdfBrush(New PdfCmykColor(0, 0.5, 0.43, 0))
			page.Graphics.DrawRectangle(cmykPen, cmykBrush, 340, 85, 250, 100)

			page.Graphics.DrawString("DeviceGray", sectionFont, brush, 20, 200)
			Dim grayPen As New PdfPen(New PdfGrayColor(0.1), 4)
			Dim grayBrush As New PdfBrush(New PdfGrayColor(0.75))
			page.Graphics.DrawRectangle(grayPen, grayBrush, 20, 215, 250, 100)

			page.Graphics.DrawString("Indexed", sectionFont, brush, 340, 200)
			Dim indexedColorSpace As New PdfIndexedColorSpace()
			indexedColorSpace.ColorCount = 2
			indexedColorSpace.BaseColorSpace = New PdfRgbColorSpace()
			indexedColorSpace.ColorTable = New Byte() {192, 0, 0, 0, 0, 128}
			Dim indexedColor0 As New PdfIndexedColor(indexedColorSpace)
			indexedColor0.ColorIndex = 0
			Dim indexedColor1 As New PdfIndexedColor(indexedColorSpace)
			indexedColor1.ColorIndex = 1
			Dim indexedPen As New PdfPen(indexedColor0, 4)
			Dim indexedBrush As New PdfBrush(indexedColor1)
			page.Graphics.DrawRectangle(indexedPen, indexedBrush, 340, 215, 250, 100)

			page.Graphics.DrawString("CalGray", sectionFont, brush, 20, 330)
			Dim calGrayColorSpace As New PdfCalGrayColorSpace()
			Dim calGrayColor1 As New PdfCalGrayColor(calGrayColorSpace)
			calGrayColor1.Gray = 0.6
			Dim calGrayColor2 As New PdfCalGrayColor(calGrayColorSpace)
			calGrayColor2.Gray = 0.2
			Dim calGrayPen As New PdfPen(calGrayColor1, 4)
			Dim calGrayBrush As New PdfBrush(calGrayColor2)
			page.Graphics.DrawRectangle(calGrayPen, calGrayBrush, 20, 345, 250, 100)

			page.Graphics.DrawString("CalRGB", sectionFont, brush, 340, 330)
			Dim calRgbColorSpace As New PdfCalRgbColorSpace()
			Dim calRgbColor1 As New PdfCalRgbColor(calRgbColorSpace)
			calRgbColor1.Red = 0.1
			calRgbColor1.Green = 0.5
			calRgbColor1.Blue = 0.25
			Dim calRgbColor2 As New PdfCalRgbColor(calRgbColorSpace)
			calRgbColor2.Red = 0.6
			calRgbColor2.Green = 0.1
			calRgbColor2.Blue = 0.9
			Dim calRgbPen As New PdfPen(calRgbColor1, 4)
			Dim calRgbBrush As New PdfBrush(calRgbColor2)
			page.Graphics.DrawRectangle(calRgbPen, calRgbBrush, 340, 345, 250, 100)

			page.Graphics.DrawString("L*a*b", sectionFont, brush, 20, 460)
			Dim labColorSpace As New PdfLabColorSpace()
			Dim labColor1 As New PdfLabColor(labColorSpace)
			labColor1.L = 90
			labColor1.A = -40
			labColor1.B = 120
			Dim labColor2 As New PdfLabColor(labColorSpace)
			labColor2.L = 45
			labColor2.A = 90
			labColor2.B = -34
			Dim labPen As New PdfPen(labColor1, 4)
			Dim labBrush As New PdfBrush(labColor2)
			page.Graphics.DrawRectangle(labPen, labBrush, 20, 475, 250, 100)

			page.Graphics.DrawString("Icc", sectionFont, brush, 340, 460)
			Dim iccColorSpace As New PdfIccColorSpace()
			Dim iccData As Byte() = New Byte(iccStream.Length - 1) {}
			iccStream.Read(iccData, 0, iccData.Length)
			iccColorSpace.IccProfile = iccData
			iccColorSpace.AlternateColorSpace = New PdfRgbColorSpace()
			iccColorSpace.ColorComponents = 3
			Dim iccColor1 As New PdfIccColor(iccColorSpace)
			iccColor1.ColorComponents = New Double() {0.45, 0.1, 0.22}
			Dim iccColor2 As New PdfIccColor(iccColorSpace)
			iccColor2.ColorComponents = New Double() {0.21, 0.76, 0.31}
			Dim iccPen As New PdfPen(iccColor1, 4)
			Dim iccBrush As New PdfBrush(iccColor2)
			page.Graphics.DrawRectangle(iccPen, iccBrush, 340, 475, 250, 100)

			page.Graphics.DrawString("Separation", sectionFont, brush, 20, 590)
			Dim tintTransform As New PdfExponentialFunction()
			tintTransform.Domain = New Double() {0, 1}
			tintTransform.Range = New Double() {0, 1, 0, 1, 0, 1, _
				0, 1}
			tintTransform.Exponent = 1
			tintTransform.C0 = New Double() {0, 0, 0, 0}
			tintTransform.C1 = New Double() {1, 0.5, 0, 0.1}

			Dim separationColorSpace As New PdfSeparationColorSpace()
			separationColorSpace.AlternateColorSpace = New PdfCmykColorSpace()
			separationColorSpace.Colorant = "Custom Blue"
			separationColorSpace.TintTransform = tintTransform

			Dim separationColor1 As New PdfSeparationColor(separationColorSpace)
			separationColor1.Tint = 0.23
			Dim separationColor2 As New PdfSeparationColor(separationColorSpace)
			separationColor2.Tint = 0.74

			Dim separationPen As New PdfPen(separationColor1, 4)
			Dim separationBrush As New PdfBrush(separationColor2)
			page.Graphics.DrawRectangle(separationPen, separationBrush, 20, 605, 250, 100)

			page.Graphics.DrawString("Pantone", sectionFont, brush, 340, 590)
			Dim pantonePen As New PdfPen(PdfPantoneColor.ReflexBlue, 4)
			Dim pantoneBrush As New PdfBrush(PdfPantoneColor.RhodamineRed)
			page.Graphics.DrawRectangle(pantonePen, pantoneBrush, 340, 605, 250, 100)

			page.Graphics.CompressAndClose()
		End Sub

		Private Shared Sub DrawShadings(page As PdfPage, titleFont As PdfFont, sectionFont As PdfFont)
			Dim brush As New PdfBrush()
			Dim blackPen As New PdfPen(PdfRgbColor.Black, 1)

			Dim randomPenColor As New PdfRgbColor()
			Dim randomPen As New PdfPen(randomPenColor, 1)
			Dim randomBrushColor As New PdfRgbColor()
			Dim randomBrush As New PdfBrush(randomBrushColor)

			page.Graphics.DrawString("Shadings", titleFont, brush, 20, 50)

			page.Graphics.DrawString("Horizontal", sectionFont, brush, 25, 70)

			Dim horizontalShading As New PdfAxialShading()
			horizontalShading.StartColor = New PdfRgbColor(255, 0, 0)
			horizontalShading.EndColor = New PdfRgbColor(0, 0, 255)
			horizontalShading.StartPoint = New PdfPoint(25, 90)
			horizontalShading.EndPoint = New PdfPoint(175, 90)

			' Clip the shading to desired area.
			Dim hsArea As New PdfPath()
			hsArea.AddRectangle(25, 90, 150, 150)
			page.Graphics.SaveGraphicsState()
			page.Graphics.SetClip(hsArea)
			page.Graphics.DrawShading(horizontalShading)
			page.Graphics.RestoreGraphicsState()

			page.Graphics.DrawString("Vertical", sectionFont, brush, 225, 70)

			Dim verticalShading As New PdfAxialShading()
			verticalShading.StartColor = New PdfRgbColor(255, 0, 0)
			verticalShading.EndColor = New PdfRgbColor(0, 0, 255)
			verticalShading.StartPoint = New PdfPoint(225, 90)
			verticalShading.EndPoint = New PdfPoint(225, 240)

			' Clip the shading to desired area.
			Dim vsArea As New PdfPath()
			vsArea.AddRectangle(225, 90, 150, 150)
			page.Graphics.SaveGraphicsState()
			page.Graphics.SetClip(vsArea)
			page.Graphics.DrawShading(verticalShading)
			page.Graphics.RestoreGraphicsState()

			page.Graphics.DrawString("Diagonal", sectionFont, brush, 425, 70)

			Dim diagonalShading As New PdfAxialShading()
			diagonalShading.StartColor = New PdfRgbColor(255, 0, 0)
			diagonalShading.EndColor = New PdfRgbColor(0, 0, 255)
			diagonalShading.StartPoint = New PdfPoint(425, 90)
			diagonalShading.EndPoint = New PdfPoint(575, 240)

			' Clip the shading to desired area.
			Dim dsArea As New PdfPath()
			dsArea.AddRectangle(425, 90, 150, 150)
			page.Graphics.SaveGraphicsState()
			page.Graphics.SetClip(dsArea)
			page.Graphics.DrawShading(diagonalShading)
			page.Graphics.RestoreGraphicsState()

			page.Graphics.DrawString("Extended shading", sectionFont, brush, 25, 260)

			Dim extendedShading As New PdfAxialShading()
			extendedShading.StartColor = New PdfRgbColor(255, 0, 0)
			extendedShading.EndColor = New PdfRgbColor(0, 0, 255)
			extendedShading.StartPoint = New PdfPoint(225, 280)
			extendedShading.EndPoint = New PdfPoint(375, 280)
			extendedShading.ExtendStart = True
			extendedShading.ExtendEnd = True

			' Clip the shading to desired area.
			Dim esArea As New PdfPath()
			esArea.AddRectangle(25, 280, 550, 30)
			page.Graphics.SaveGraphicsState()
			page.Graphics.SetClip(esArea)
			page.Graphics.DrawShading(extendedShading)
			page.Graphics.RestoreGraphicsState()
			page.Graphics.DrawPath(blackPen, esArea)

			page.Graphics.DrawString("Limited shading", sectionFont, brush, 25, 330)

			Dim limitedShading As New PdfAxialShading()
			limitedShading.StartColor = New PdfRgbColor(255, 0, 0)
			limitedShading.EndColor = New PdfRgbColor(0, 0, 255)
			limitedShading.StartPoint = New PdfPoint(225, 350)
			limitedShading.EndPoint = New PdfPoint(375, 350)
			limitedShading.ExtendStart = False
			limitedShading.ExtendEnd = False

			' Clip the shading to desired area.
			Dim lsArea As New PdfPath()
			lsArea.AddRectangle(25, 350, 550, 30)
			page.Graphics.SaveGraphicsState()
			page.Graphics.SetClip(lsArea)
			page.Graphics.DrawShading(limitedShading)
			page.Graphics.RestoreGraphicsState()
			page.Graphics.DrawPath(blackPen, lsArea)

			page.Graphics.DrawString("Multi-stop shading", sectionFont, brush, 25, 400)
			' Multi-stop shadings use a stitching function to combine the functions that define each gradient part.
			' Function for red to blue shading.
			Dim redToBlueFunc As New PdfExponentialFunction()
			' Linear function
			redToBlueFunc.Exponent = 1
			redToBlueFunc.Domain = New Double() {0, 1}
			' Red color for start
			redToBlueFunc.C0 = New Double() {1, 0, 0}
			' Blue color for start
			redToBlueFunc.C1 = New Double() {0, 0, 1}
			' Function for blue to green shading.
			Dim blueToGreenFunc As New PdfExponentialFunction()
			' Linear function
			blueToGreenFunc.Exponent = 1
			blueToGreenFunc.Domain = New Double() {0, 1}
			' Blue color for start
			blueToGreenFunc.C0 = New Double() {0, 0, 1}
			' Green color for start
			blueToGreenFunc.C1 = New Double() {0, 1, 0}

			'Stitching function for the shading.
			Dim shadingFunction As New PdfStitchingFunction()
			shadingFunction.Functions.Add(redToBlueFunc)
			shadingFunction.Functions.Add(blueToGreenFunc)
			shadingFunction.Domain = New Double() {0, 1}
			shadingFunction.Encode = New Double() {0, 1, 0, 1}

			' Entire shading goes from 0 to 1 (100%).
			' We set the first shading (red->blue) to cover 30% (0 - 0.3) and
			' the second shading to cover 70% (0.3 - 1).
			shadingFunction.Bounds = New Double() {0.3}
			' The multistop shading
			Dim multiStopShading As New PdfAxialShading()
			multiStopShading.StartPoint = New PdfPoint(25, 420)
			multiStopShading.EndPoint = New PdfPoint(575, 420)
			' The colorspace must match the colors specified in C0 & C1
			multiStopShading.ColorSpace = New PdfRgbColorSpace()
			multiStopShading.[Function] = shadingFunction

			' Clip the shading to desired area.
			Dim mssArea As New PdfPath()
			mssArea.AddRectangle(25, 420, 550, 30)
			page.Graphics.SaveGraphicsState()
			page.Graphics.SetClip(mssArea)
			page.Graphics.DrawShading(multiStopShading)
			page.Graphics.RestoreGraphicsState()
			page.Graphics.DrawPath(blackPen, lsArea)

			page.Graphics.DrawString("Radial shading", sectionFont, brush, 25, 470)

			Dim rs1 As New PdfRadialShading()
			rs1.StartColor = New PdfRgbColor(0, 255, 0)
			rs1.EndColor = New PdfRgbColor(255, 0, 255)
			rs1.StartCircleCenter = New PdfPoint(50, 500)
			rs1.StartCircleRadius = 10
			rs1.EndCircleCenter = New PdfPoint(500, 570)
			rs1.EndCircleRadius = 100

			page.Graphics.DrawShading(rs1)

			Dim rs2 As New PdfRadialShading()
			rs2.StartColor = New PdfRgbColor(0, 255, 0)
			rs2.EndColor = New PdfRgbColor(255, 0, 255)
			rs2.StartCircleCenter = New PdfPoint(80, 600)
			rs2.StartCircleRadius = 10
			rs2.EndCircleCenter = New PdfPoint(110, 690)
			rs2.EndCircleRadius = 100

			page.Graphics.DrawShading(rs2)

			page.Graphics.CompressAndClose()
		End Sub

		Private Shared Sub DrawPatterns(page As PdfPage, titleFont As PdfFont, sectionFont As PdfFont)
			Dim brush As New PdfBrush()
			Dim blackPen As New PdfPen(PdfRgbColor.Black, 1)

			Dim darkRedPen As New PdfPen(New PdfRgbColor(&Hff, &H40, &H40), 0.8)
			Dim darkOrangePen As New PdfPen(New PdfRgbColor(&Ha6, &H4b, &H0), 0.8)
			Dim darkCyanPen As New PdfPen(New PdfRgbColor(&H0, &H63, &H63), 0.8)
			Dim darkGreenPen As New PdfPen(New PdfRgbColor(&H0, &H85, &H0), 0.8)
			Dim lightRedBrush As New PdfBrush(New PdfRgbColor(&Hff, &H73, &H73))
			Dim lightOrangeBrush As New PdfBrush(New PdfRgbColor(&Hff, &H96, &H40))
			Dim lightCyanBrush As New PdfBrush(New PdfRgbColor(&H33, &Hcc, &Hcc))
			Dim lightGreenBrush As New PdfBrush(New PdfRgbColor(&H67, &He6, &H67))

			page.Graphics.DrawString("Patterns", titleFont, brush, 20, 50)

			page.Graphics.DrawString("Colored patterns", sectionFont, brush, 25, 70)

			' Create the pattern visual appearance.
			Dim ctp As New PdfColoredTilingPattern(20, 20)
			' Red circle
			ctp.Graphics.DrawEllipse(darkRedPen, lightRedBrush, 1, 1, 8, 8)
			' Cyan square
			ctp.Graphics.DrawRectangle(darkCyanPen, lightCyanBrush, 11, 1, 8, 8)
			' Green diamond
			Dim diamondPath As New PdfPath()
			diamondPath.StartSubpath(1, 15)
			diamondPath.AddPolyLineTo(New PdfPoint() {New PdfPoint(5, 11), New PdfPoint(9, 15), New PdfPoint(5, 19)})
			diamondPath.CloseSubpath()
			ctp.Graphics.DrawPath(darkGreenPen, lightGreenBrush, diamondPath)
			' Orange triangle
			Dim trianglePath As New PdfPath()
			trianglePath.StartSubpath(11, 19)
			trianglePath.AddPolyLineTo(New PdfPoint() {New PdfPoint(15, 11), New PdfPoint(19, 19)})
			trianglePath.CloseSubpath()
			ctp.Graphics.DrawPath(darkOrangePen, lightOrangeBrush, trianglePath)

			' Create a pattern colorspace from the pattern object.
			Dim coloredPatternColorSpace As New PdfPatternColorSpace(ctp)
			' Create a color based on the pattern colorspace.
			Dim coloredPatternColor As New PdfPatternColor(coloredPatternColorSpace)
			' The pen and brush use the pattern color like any other color.
			Dim patternBrush As New PdfPatternBrush(coloredPatternColor)
			Dim patternPen As New PdfPatternPen(coloredPatternColor, 40)

			page.Graphics.DrawEllipse(patternBrush, 25, 90, 250, 200)
			page.Graphics.DrawRoundRectangle(patternPen, 310, 110, 250, 160, 100, _
				100)

			page.Graphics.DrawString("Uncolored patterns", sectionFont, brush, 25, 300)

			' Create the pattern visual appearance.
			Dim uctp As New PdfUncoloredTilingPattern(20, 20)
			' A pen without color is used to create the pattern content.
			Dim noColorPen As New PdfPen(Nothing, 0.8)
			' Circle
			uctp.Graphics.DrawEllipse(noColorPen, 1, 1, 8, 8)
			' Square
			uctp.Graphics.DrawRectangle(noColorPen, 11, 1, 8, 8)
			' Diamond
			diamondPath = New PdfPath()
			diamondPath.StartSubpath(1, 15)
			diamondPath.AddPolyLineTo(New PdfPoint() {New PdfPoint(5, 11), New PdfPoint(9, 15), New PdfPoint(5, 19)})
			diamondPath.CloseSubpath()
			uctp.Graphics.DrawPath(noColorPen, diamondPath)
			' Triangle
			trianglePath = New PdfPath()
			trianglePath.StartSubpath(11, 19)
			trianglePath.AddPolyLineTo(New PdfPoint() {New PdfPoint(15, 11), New PdfPoint(19, 19)})
			trianglePath.CloseSubpath()
			uctp.Graphics.DrawPath(noColorPen, trianglePath)

			' Create a pattern colorspace from the pattern object.
			Dim uncoloredPatternColorSpace As New PdfPatternColorSpace(uctp)
			' Create a color based on the pattern colorspace.
			Dim uncoloredPatternColor As New PdfPatternColor(uncoloredPatternColorSpace)
			' The pen and brush use the pattern color like any other color.
			patternBrush = New PdfPatternBrush(uncoloredPatternColor)

			' Before using the uncolored pattern set the color that will be used to paint the pattern.
			patternBrush.UncoloredPatternPaintColor = New PdfRgbColor(&Hff, &H40, &H40)
			page.Graphics.DrawEllipse(patternBrush, 25, 320, 125, 200)
			patternBrush.UncoloredPatternPaintColor = New PdfRgbColor(&Ha6, &H4b, &H0)
			page.Graphics.DrawEllipse(patternBrush, 175, 320, 125, 200)
			patternBrush.UncoloredPatternPaintColor = New PdfRgbColor(&H0, &H63, &H63)
			page.Graphics.DrawEllipse(patternBrush, 325, 320, 125, 200)
			patternBrush.UncoloredPatternPaintColor = New PdfRgbColor(&H0, &H85, &H0)
			page.Graphics.DrawEllipse(patternBrush, 475, 320, 125, 200)

			page.Graphics.DrawString("Shading patterns", sectionFont, brush, 25, 550)

			' Create the pattern visual appearance.
			Dim horizontalShading As New PdfAxialShading()
			horizontalShading.StartColor = New PdfRgbColor(255, 0, 0)
			horizontalShading.EndColor = New PdfRgbColor(0, 0, 255)
			horizontalShading.StartPoint = New PdfPoint(25, 600)
			horizontalShading.EndPoint = New PdfPoint(575, 600)
			Dim sp As New PdfShadingPattern(horizontalShading)

			' Create a pattern colorspace from the pattern object.
			Dim shadingPatternColorSpace As New PdfPatternColorSpace(sp)
			' Create a color based on the pattern colorspace.
			Dim shadingPatternColor As New PdfPatternColor(shadingPatternColorSpace)
			' The pen and brush use the pattern color like any other color.
			patternPen = New PdfPatternPen(shadingPatternColor, 40)

			page.Graphics.DrawEllipse(patternPen, 50, 600, 500, 150)

			page.Graphics.CompressAndClose()
		End Sub

		Private Shared Sub DrawFormXObjects(page As PdfPage, titleFont As PdfFont, sectionFont As PdfFont)
			Dim brush As New PdfBrush()
			Dim blackPen As New PdfPen(PdfRgbColor.Black, 1)

			Dim randomPenColor As New PdfRgbColor()
			Dim randomPen As New PdfPen(randomPenColor, 1)
			Dim randomBrushColor As New PdfRgbColor()
			Dim randomBrush As New PdfBrush(randomBrushColor)

			page.Graphics.DrawString("Form XObjects", titleFont, brush, 20, 50)
			page.Graphics.DrawString("Scaling", sectionFont, brush, 20, 70)

			' Create the XObject content - random rectangles
			Dim xobject As New PdfFormXObject(300, 300)
			Dim rnd As New Random()
			For i As Integer = 0 To 99
				randomPenColor.R = CByte(rnd.[Next](256))
				randomPenColor.G = CByte(rnd.[Next](256))
				randomPenColor.B = CByte(rnd.[Next](256))

				randomBrushColor.R = CByte(rnd.[Next](256))
				randomBrushColor.G = CByte(rnd.[Next](256))
				randomBrushColor.B = CByte(rnd.[Next](256))

				Dim left As Double = rnd.NextDouble() * xobject.Width
				Dim top As Double = rnd.NextDouble() * xobject.Height
				Dim width As Double = rnd.NextDouble() * xobject.Width
				Dim height As Double = rnd.NextDouble() * xobject.Height
				Dim orientation As Double = rnd.[Next](360)
				xobject.Graphics.DrawRectangle(randomPen, randomBrush, left, top, width, height, _
					orientation)
			Next

			xobject.Graphics.DrawRectangle(blackPen, 0, 0, xobject.Width, xobject.Height)
			xobject.Graphics.CompressAndClose()

			' Draw the form XObject 3 times on the page at different sizes.
			page.Graphics.DrawFormXObject(xobject, 3, 90, 100, 100)
			page.Graphics.DrawFormXObject(xobject, 106, 90, 200, 200)
			page.Graphics.DrawFormXObject(xobject, 309, 90, 300, 300)

			page.Graphics.DrawString("Flipping", sectionFont, brush, 20, 420)
			page.Graphics.DrawFormXObject(xobject, 20, 440, 150, 150)
			page.Graphics.DrawFormXObject(xobject, 200, 440, 150, 150, 0, _
				PdfFlipDirection.VerticalFlip)
			page.Graphics.DrawFormXObject(xobject, 20, 620, 150, 150, 0, _
				PdfFlipDirection.HorizontalFlip)
			page.Graphics.DrawFormXObject(xobject, 200, 620, 150, 150, 0, _
				PdfFlipDirection.VerticalFlip Or PdfFlipDirection.HorizontalFlip)

			page.Graphics.CompressAndClose()
		End Sub
	End Class
End Namespace