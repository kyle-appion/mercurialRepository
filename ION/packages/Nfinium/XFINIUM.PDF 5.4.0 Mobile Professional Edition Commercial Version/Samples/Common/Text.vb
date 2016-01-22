Imports System.IO
Imports Xfinium.Pdf
Imports Xfinium.Pdf.Graphics
Imports Xfinium.Pdf.Graphics.Text

Namespace Xfinium.Pdf.Samples
	''' <summary>
	''' Text sample.
	''' </summary>
	Public Class Text
		''' <summary>
		''' Main method for running the sample.
		''' </summary>
		Public Shared Function Run() As SampleOutputInfo()
			Dim document As New PdfFixedDocument()
			Dim helveticaBold As New PdfStandardFont(PdfStandardFontFace.HelveticaBold, 16)

			Dim page As PdfPage = document.Pages.Add()
			DrawTextLines(page, helveticaBold)

			page = document.Pages.Add()
			DrawTextWrap(page, helveticaBold)

			page = document.Pages.Add()
			DrawTextRenderingModes(page, helveticaBold)

			Dim output As SampleOutputInfo() = New SampleOutputInfo() {New SampleOutputInfo(document, "xfinium.pdf.sample.text.pdf")}
			Return output
		End Function

		Private Shared Sub DrawTextLines(page As PdfPage, titleFont As PdfStandardFont)
			Dim brush As New PdfBrush()
			Dim redPen As New PdfPen(PdfRgbColor.Red, 0.5)
			Dim helvetica As New PdfStandardFont(PdfStandardFontFace.Helvetica, 12)

			page.Graphics.DrawString("Text lines", titleFont, brush, 20, 50)

			page.Graphics.DrawLine(redPen, 20, 70, 150, 70)
			page.Graphics.DrawLine(redPen, 20, 70, 20, 80)
			page.Graphics.DrawString("Simple text line with default top left text alignment and no rotation", helvetica, brush, 20, 70)

			page.Graphics.DrawString("Text align", helvetica, brush, 20, 110)

			redPen.DashPattern = New Double() {1, 1}
			page.Graphics.DrawLine(redPen, 20, 125, 590, 125)
			page.Graphics.DrawLine(redPen, 20, 165, 590, 165)
			page.Graphics.DrawLine(redPen, 20, 205, 590, 205)
			page.Graphics.DrawLine(redPen, 20, 125, 20, 205)
			page.Graphics.DrawLine(redPen, 305, 125, 305, 205)
			page.Graphics.DrawLine(redPen, 590, 125, 590, 205)

			Dim sao As New PdfStringAppearanceOptions()
			sao.Brush = brush
			sao.Font = helvetica
			Dim slo As New PdfStringLayoutOptions()

			' Top left aligned text
			slo.HorizontalAlign = PdfStringHorizontalAlign.Left
			slo.VerticalAlign = PdfStringVerticalAlign.Top
			slo.X = 20
			slo.Y = 125
			page.Graphics.DrawString("Top Left", sao, slo)

			' Top center aligned text
			slo.HorizontalAlign = PdfStringHorizontalAlign.Center
			slo.VerticalAlign = PdfStringVerticalAlign.Top
			slo.X = 305
			slo.Y = 125
			page.Graphics.DrawString("Top Center", sao, slo)

			' Top right aligned text
			slo.HorizontalAlign = PdfStringHorizontalAlign.Right
			slo.VerticalAlign = PdfStringVerticalAlign.Top
			slo.X = 590
			slo.Y = 125
			page.Graphics.DrawString("Top Right", sao, slo)

			' Middle left aligned text
			slo.HorizontalAlign = PdfStringHorizontalAlign.Left
			slo.VerticalAlign = PdfStringVerticalAlign.Middle
			slo.X = 20
			slo.Y = 165
			page.Graphics.DrawString("Middle Left", sao, slo)

			' Middle center aligned text
			slo.HorizontalAlign = PdfStringHorizontalAlign.Center
			slo.VerticalAlign = PdfStringVerticalAlign.Middle
			slo.X = 305
			slo.Y = 165
			page.Graphics.DrawString("Middle Center", sao, slo)

			' Middle right aligned text
			slo.HorizontalAlign = PdfStringHorizontalAlign.Right
			slo.VerticalAlign = PdfStringVerticalAlign.Middle
			slo.X = 590
			slo.Y = 165
			page.Graphics.DrawString("Middle Right", sao, slo)

			' Bottom left aligned text
			slo.HorizontalAlign = PdfStringHorizontalAlign.Left
			slo.VerticalAlign = PdfStringVerticalAlign.Bottom
			slo.X = 20
			slo.Y = 205
			page.Graphics.DrawString("Bottom Left", sao, slo)

			' Bottom center aligned text
			slo.HorizontalAlign = PdfStringHorizontalAlign.Center
			slo.VerticalAlign = PdfStringVerticalAlign.Bottom
			slo.X = 305
			slo.Y = 205
			page.Graphics.DrawString("Bottom Center", sao, slo)

			' Bottom right aligned text
			slo.HorizontalAlign = PdfStringHorizontalAlign.Right
			slo.VerticalAlign = PdfStringVerticalAlign.Bottom
			slo.X = 590
			slo.Y = 205
			page.Graphics.DrawString("Bottom Right", sao, slo)

			page.Graphics.DrawString("Text rotation", helvetica, brush, 20, 250)

			redPen.DashPattern = New Double() {1, 1}
			page.Graphics.DrawLine(redPen, 20, 265, 590, 265)
			page.Graphics.DrawLine(redPen, 20, 305, 590, 305)
			page.Graphics.DrawLine(redPen, 20, 345, 590, 345)
			page.Graphics.DrawLine(redPen, 20, 265, 20, 345)
			page.Graphics.DrawLine(redPen, 305, 265, 305, 345)
			page.Graphics.DrawLine(redPen, 590, 265, 590, 345)

			slo.Rotation = 30
			' Top left aligned text
			slo.HorizontalAlign = PdfStringHorizontalAlign.Left
			slo.VerticalAlign = PdfStringVerticalAlign.Top
			slo.X = 20
			slo.Y = 265
			page.Graphics.DrawString("Top Left", sao, slo)

			' Top center aligned text
			slo.HorizontalAlign = PdfStringHorizontalAlign.Center
			slo.VerticalAlign = PdfStringVerticalAlign.Top
			slo.X = 305
			slo.Y = 265
			page.Graphics.DrawString("Top Center", sao, slo)

			' Top right aligned text
			slo.HorizontalAlign = PdfStringHorizontalAlign.Right
			slo.VerticalAlign = PdfStringVerticalAlign.Top
			slo.X = 590
			slo.Y = 265
			page.Graphics.DrawString("Top Right", sao, slo)

			' Middle left aligned text
			slo.HorizontalAlign = PdfStringHorizontalAlign.Left
			slo.VerticalAlign = PdfStringVerticalAlign.Middle
			slo.X = 20
			slo.Y = 305
			page.Graphics.DrawString("Middle Left", sao, slo)

			' Middle center aligned text
			slo.HorizontalAlign = PdfStringHorizontalAlign.Center
			slo.VerticalAlign = PdfStringVerticalAlign.Middle
			slo.X = 305
			slo.Y = 305
			page.Graphics.DrawString("Middle Center", sao, slo)

			' Middle right aligned text
			slo.HorizontalAlign = PdfStringHorizontalAlign.Right
			slo.VerticalAlign = PdfStringVerticalAlign.Middle
			slo.X = 590
			slo.Y = 305
			page.Graphics.DrawString("Middle Right", sao, slo)

			' Bottom left aligned text
			slo.HorizontalAlign = PdfStringHorizontalAlign.Left
			slo.VerticalAlign = PdfStringVerticalAlign.Bottom
			slo.X = 20
			slo.Y = 345
			page.Graphics.DrawString("Bottom Left", sao, slo)

			' Bottom center aligned text
			slo.HorizontalAlign = PdfStringHorizontalAlign.Center
			slo.VerticalAlign = PdfStringVerticalAlign.Bottom
			slo.X = 305
			slo.Y = 345
			page.Graphics.DrawString("Bottom Center", sao, slo)

			' Bottom right aligned text
			slo.HorizontalAlign = PdfStringHorizontalAlign.Right
			slo.VerticalAlign = PdfStringVerticalAlign.Bottom
			slo.X = 590
			slo.Y = 345
			page.Graphics.DrawString("Bottom Right", sao, slo)

			page.Graphics.CompressAndClose()
		End Sub

		Private Shared Sub DrawTextWrap(page As PdfPage, titleFont As PdfStandardFont)
			Dim brush As New PdfBrush()
			Dim redPen As New PdfPen(PdfRgbColor.Red, 0.5)
			Dim helvetica As New PdfStandardFont(PdfStandardFontFace.Helvetica, 12)

			page.Graphics.DrawString("Text wrapping", titleFont, brush, 20, 50)

			page.Graphics.DrawLine(redPen, 20, 70, 20, 150)
			page.Graphics.DrawLine(redPen, 300, 70, 300, 150)
			page.Graphics.DrawLine(redPen, 20, 70, 300, 70)

			Dim sao As New PdfStringAppearanceOptions()
			sao.Brush = brush
			sao.Font = helvetica

			' Height is not set, text has no vertical limit.
			Dim slo As New PdfStringLayoutOptions()
			slo.HorizontalAlign = PdfStringHorizontalAlign.Justified
			slo.VerticalAlign = PdfStringVerticalAlign.Top
			slo.X = 20
			slo.Y = 70
			slo.Width = 280
			Dim text As String = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " & "Sed vel euismod risus. Fusce viverra, nisi auctor ullamcorper porttitor, " & "ipsum lacus lobortis metus, sit amet dictum lacus velit nec diam. " & "Morbi arcu diam, euismod a auctor nec, aliquam in lectus." & "Ut ultricies iaculis augue sit amet adipiscing. Aenean blandit tortor a nisi " & "dignissim fermentum id adipiscing mauris. Aenean libero turpis, varius nec ultricies " & "faucibus, pretium quis lectus. Morbi mollis lorem vel erat condimentum mattis mollis " & "nulla sollicitudin. Nunc ut massa id felis laoreet feugiat eget at eros."
			page.Graphics.DrawString(text, sao, slo)

			page.Graphics.DrawLine(redPen, 310, 70, 310, 147)
			page.Graphics.DrawLine(redPen, 590, 70, 590, 147)
			page.Graphics.DrawLine(redPen, 310, 70, 590, 70)
			page.Graphics.DrawLine(redPen, 310, 147, 590, 147)

			' Height is set, text is limited on vertical.
			slo.X = 310
			slo.Y = 70
			slo.Width = 280
			slo.Height = 77
			page.Graphics.DrawString(text, sao, slo)

			Dim clipPath As New PdfPath()
			clipPath.AddRectangle(310, 160, 280, 77)
			page.Graphics.DrawPath(redPen, clipPath)

			page.Graphics.SaveGraphicsState()
			page.Graphics.SetClip(clipPath)

			' Height is not set but text is cliped on vertical.
			slo.X = 310
			slo.Y = 160
			slo.Width = 280
			slo.Height = 0
			page.Graphics.DrawString(text, sao, slo)

			page.Graphics.RestoreGraphicsState()

			page.Graphics.DrawLine(redPen, 10, 400, 300, 400)
			page.Graphics.DrawLine(redPen, 20, 300, 20, 500)
			' Wrapped text is always rotated around top left corner, no matter the text alignment
			page.Graphics.DrawRectangle(redPen, 20, 400, 280, 80, 30)
			slo.X = 20
			slo.Y = 400
			slo.Width = 280
			slo.Height = 80
			slo.Rotation = 30
			page.Graphics.DrawString(text, sao, slo)

			page.Graphics.DrawLine(redPen, 310, 600, 590, 600)
			page.Graphics.DrawLine(redPen, 450, 450, 450, 750)

			' Rotation around the center of the box requires some affine transformations.
			page.Graphics.SaveGraphicsState()
			page.Graphics.TranslateTransform(450, 600)
			page.Graphics.RotateTransform(30)
			page.Graphics.DrawRectangle(redPen, -140, -40, 280, 80)
			slo.X = -140
			slo.Y = -40
			slo.Width = 280
			slo.Height = 80
			slo.Rotation = 0
			page.Graphics.DrawString(text, sao, slo)

			page.Graphics.RestoreGraphicsState()
		End Sub

		Private Shared Sub DrawTextRenderingModes(page As PdfPage, titleFont As PdfStandardFont)
			Dim brush As New PdfBrush()
			Dim redPen As New PdfPen(PdfRgbColor.Red, 0.5)
			Dim helvetica As New PdfStandardFont(PdfStandardFontFace.Helvetica, 12)
			Dim helveticaBold As New PdfStandardFont(PdfStandardFontFace.Helvetica, 80)

			page.Graphics.DrawString("Text rendering modes", titleFont, brush, 20, 50)

			page.Graphics.DrawString("Fill text", helvetica, brush, 20, 90)
			page.Graphics.DrawString("Stroke text", helvetica, brush, 20, 160)
			page.Graphics.DrawString("Fill and stroke text", helvetica, brush, 20, 230)
			page.Graphics.DrawString("Invisible text", helvetica, brush, 20, 300)
			page.Graphics.DrawString("Fill and clip text", helvetica, brush, 20, 370)
			page.Graphics.DrawString("Stroke and clip text", helvetica, brush, 20, 440)
			page.Graphics.DrawString("Fill, stroke and clip text", helvetica, brush, 20, 510)
			page.Graphics.DrawString("Clip text", helvetica, brush, 20, 580)

			' Fill text - text interior is filled because only the brush is available for drawing. 
			page.Graphics.DrawString("A B C", helveticaBold, brush, 300, 90)

			' Stroke text - text outline is stroked becuase only the pen is available for drawing.
			Dim slo As New PdfStringLayoutOptions()
			slo.X = 300
			slo.Y = 160
			Dim sao As New PdfStringAppearanceOptions()
			sao.Font = helveticaBold
			sao.Pen = redPen
			sao.Brush = Nothing
			page.Graphics.DrawString("A B C", sao, slo)

			' Fill and stroke text - text interior is filled and text outline is stroked 
			' because both pen and brush are available.
			slo.Y = 230
			sao.Pen = redPen
			sao.Brush = brush
			page.Graphics.DrawString("A B C", sao, slo)

			' Invisible text - text is not displayed because both pen and brush are not available.
			slo.Y = 300
			sao.Pen = Nothing
			sao.Brush = Nothing
			page.Graphics.DrawString("A B C", sao, slo)

			' Fill and clip text - text interior is filled and then text outline is added to current clipping path.
			page.Graphics.SaveGraphicsState()
			helveticaBold.TextRenderingMode = PdfTextRenderingMode.FillAndClipText
			slo.Y = 370
			sao.Pen = Nothing
			sao.Brush = brush
			page.Graphics.DrawString("A B C", sao, slo)
			DrawHorizontalLines(page.Graphics, redPen, slo.X, slo.Y, 250, 70)
			page.Graphics.RestoreGraphicsState()

			' Stroke and clip text - text outline is stroked and then text outline is added to current clipping path.
			page.Graphics.SaveGraphicsState()
			helveticaBold.TextRenderingMode = PdfTextRenderingMode.StrokeAndClipText
			slo.Y = 440
			sao.Pen = redPen
			sao.Brush = Nothing
			page.Graphics.DrawString("A B C", sao, slo)
			DrawHorizontalLines(page.Graphics, redPen, slo.X, slo.Y, 250, 70)
			page.Graphics.RestoreGraphicsState()

			' Fill, Stroke and clip text - text interior is filled, text outline is stroked and then text outline is added to current clipping path.
			page.Graphics.SaveGraphicsState()
			helveticaBold.TextRenderingMode = PdfTextRenderingMode.FillStrokeAndClipText
			slo.Y = 510
			sao.Pen = redPen
			sao.Brush = brush
			page.Graphics.DrawString("A B C", sao, slo)
			DrawHorizontalLines(page.Graphics, redPen, slo.X, slo.Y, 250, 70)
			page.Graphics.RestoreGraphicsState()

			' Clip text - text outline is added to current clipping path.
			page.Graphics.SaveGraphicsState()
			helveticaBold.TextRenderingMode = PdfTextRenderingMode.ClipText
			slo.Y = 580
			sao.Pen = redPen
			sao.Brush = brush
			page.Graphics.DrawString("A B C", sao, slo)
			DrawHorizontalLines(page.Graphics, redPen, slo.X, slo.Y, 250, 70)
			page.Graphics.RestoreGraphicsState()
		End Sub

		Private Shared Sub DrawHorizontalLines(g As PdfGraphics, pen As PdfPen, x As Double, y As Double, width As Double, height As Double)
			Dim i As Double = 0
			While i < height
				g.DrawLine(pen, x, y + i, x + width, y + i)
				i = i + 5
			End While
		End Sub
	End Class
End Namespace