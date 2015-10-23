
Imports System.IO
Imports Xfinium.Pdf
Imports Xfinium.Pdf.Graphics

Namespace Xfinium.Pdf.Samples
	''' <summary>
	''' Type3 fonts sample.
	''' </summary>
	Public Class Type3Fonts
		''' <summary>
		''' Main method for running the sample.
		''' </summary>
		Public Shared Function Run() As SampleOutputInfo()
			Dim document As New PdfFixedDocument()
			Dim page As PdfPage = document.Pages.Add()

			Dim helvetica As New PdfStandardFont(PdfStandardFontFace.Helvetica, 20)
			Dim blackBrush As New PdfBrush(PdfRgbColor.Black)
			page.Graphics.DrawString("The digits below, from 0 to 9, are drawn using a Type3 font.", helvetica, blackBrush, 50, 100)

			Dim t3 As New PdfType3Font("DemoT3")
			t3.Size = 24
			t3.FirstChar = CByte(" "C)
			t3.LastChar = CByte("9"C)
			t3.FontMatrix = New PdfMatrix(0.01, 0, 0, 0.01, 0, 0)
			Dim widths As Double() = New Double() {100, 0, 0, 0, 0, 0, _
				0, 0, 0, 0, 0, 0, _
				0, 0, 0, 0, 100, 100, _
				100, 100, 100, 100, 100, 100, _
				100, 100}
			t3.Widths = widths

			Dim hollowPen As New PdfPen(Nothing, 8)
			Dim hollowBrush As New PdfBrush(Nothing)
			' space
			Dim t3s As New PdfType3Glyph(&H20, New PdfSize(100, 100))
			t3.Glyphs.Add(t3s)
			' 0
			Dim t30 As New PdfType3Glyph(&H30, New PdfSize(100, 100))
			t30.Graphics.DrawRectangle(hollowPen, 5, 5, 90, 90)
			t30.Graphics.CompressAndClose()
			t3.Glyphs.Add(t30)
			' 1
			Dim t31 As New PdfType3Glyph(&H31, New PdfSize(100, 100))
			t31.Graphics.DrawRectangle(hollowPen, 5, 5, 90, 90)
			t31.Graphics.DrawEllipse(hollowBrush, 40, 40, 20, 20)
			t31.Graphics.CompressAndClose()
			t3.Glyphs.Add(t31)
			' 2
			Dim t32 As New PdfType3Glyph(&H32, New PdfSize(100, 100))
			t32.Graphics.DrawRectangle(hollowPen, 5, 5, 90, 90)
			t32.Graphics.DrawEllipse(hollowBrush, 15, 15, 20, 20)
			t32.Graphics.DrawEllipse(hollowBrush, 65, 65, 20, 20)
			t32.Graphics.CompressAndClose()
			t3.Glyphs.Add(t32)
			' 3
			Dim t33 As New PdfType3Glyph(&H33, New PdfSize(100, 100))
			t33.Graphics.DrawRectangle(hollowPen, 5, 5, 90, 90)
			t33.Graphics.DrawEllipse(hollowBrush, 15, 15, 20, 20)
			t33.Graphics.DrawEllipse(hollowBrush, 40, 40, 20, 20)
			t33.Graphics.DrawEllipse(hollowBrush, 65, 65, 20, 20)
			t33.Graphics.CompressAndClose()
			t3.Glyphs.Add(t33)
			' 4
			Dim t34 As New PdfType3Glyph(&H34, New PdfSize(100, 100))
			t34.Graphics.DrawRectangle(hollowPen, 5, 5, 90, 90)
			t34.Graphics.DrawEllipse(hollowBrush, 15, 15, 20, 20)
			t34.Graphics.DrawEllipse(hollowBrush, 65, 15, 20, 20)
			t34.Graphics.DrawEllipse(hollowBrush, 15, 65, 20, 20)
			t34.Graphics.DrawEllipse(hollowBrush, 65, 65, 20, 20)
			t34.Graphics.CompressAndClose()
			t3.Glyphs.Add(t34)
			' 5
			Dim t35 As New PdfType3Glyph(&H35, New PdfSize(100, 100))
			t35.Graphics.DrawRectangle(hollowPen, 5, 5, 90, 90)
			t35.Graphics.DrawEllipse(hollowBrush, 15, 15, 20, 20)
			t35.Graphics.DrawEllipse(hollowBrush, 65, 15, 20, 20)
			t35.Graphics.DrawEllipse(hollowBrush, 40, 40, 20, 20)
			t35.Graphics.DrawEllipse(hollowBrush, 15, 65, 20, 20)
			t35.Graphics.DrawEllipse(hollowBrush, 65, 65, 20, 20)
			t35.Graphics.CompressAndClose()
			t3.Glyphs.Add(t35)
			' 6
			Dim t36 As New PdfType3Glyph(&H36, New PdfSize(100, 100))
			t36.Graphics.DrawRectangle(hollowPen, 5, 5, 90, 90)
			t36.Graphics.DrawEllipse(hollowBrush, 15, 15, 20, 20)
			t36.Graphics.DrawEllipse(hollowBrush, 65, 15, 20, 20)
			t36.Graphics.DrawEllipse(hollowBrush, 15, 40, 20, 20)
			t36.Graphics.DrawEllipse(hollowBrush, 65, 40, 20, 20)
			t36.Graphics.DrawEllipse(hollowBrush, 15, 65, 20, 20)
			t36.Graphics.DrawEllipse(hollowBrush, 65, 65, 20, 20)
			t36.Graphics.CompressAndClose()
			t3.Glyphs.Add(t36)
			' 7
			Dim t37 As New PdfType3Glyph(&H37, New PdfSize(100, 100))
			t37.Graphics.DrawRectangle(hollowPen, 5, 5, 90, 90)
			t37.Graphics.DrawEllipse(hollowBrush, 15, 15, 20, 20)
			t37.Graphics.DrawEllipse(hollowBrush, 65, 15, 20, 20)
			t37.Graphics.DrawEllipse(hollowBrush, 15, 40, 20, 20)
			t37.Graphics.DrawEllipse(hollowBrush, 40, 40, 20, 20)
			t37.Graphics.DrawEllipse(hollowBrush, 65, 40, 20, 20)
			t37.Graphics.DrawEllipse(hollowBrush, 15, 65, 20, 20)
			t37.Graphics.DrawEllipse(hollowBrush, 65, 65, 20, 20)
			t37.Graphics.CompressAndClose()
			t3.Glyphs.Add(t37)
			' 8
			Dim t38 As New PdfType3Glyph(&H38, New PdfSize(100, 100))
			t38.Graphics.DrawRectangle(hollowPen, 5, 5, 90, 90)
			t38.Graphics.DrawEllipse(hollowBrush, 15, 15, 20, 20)
			t38.Graphics.DrawEllipse(hollowBrush, 40, 15, 20, 20)
			t38.Graphics.DrawEllipse(hollowBrush, 65, 15, 20, 20)
			t38.Graphics.DrawEllipse(hollowBrush, 15, 40, 20, 20)
			t38.Graphics.DrawEllipse(hollowBrush, 65, 40, 20, 20)
			t38.Graphics.DrawEllipse(hollowBrush, 15, 65, 20, 20)
			t38.Graphics.DrawEllipse(hollowBrush, 40, 65, 20, 20)
			t38.Graphics.DrawEllipse(hollowBrush, 65, 65, 20, 20)
			t38.Graphics.CompressAndClose()
			t3.Glyphs.Add(t38)
			' 9
			Dim t39 As New PdfType3Glyph(&H39, New PdfSize(100, 100))
			t39.Graphics.DrawRectangle(hollowPen, 5, 5, 90, 90)
			t39.Graphics.DrawEllipse(hollowBrush, 15, 15, 20, 20)
			t39.Graphics.DrawEllipse(hollowBrush, 40, 15, 20, 20)
			t39.Graphics.DrawEllipse(hollowBrush, 65, 15, 20, 20)
			t39.Graphics.DrawEllipse(hollowBrush, 15, 40, 20, 20)
			t39.Graphics.DrawEllipse(hollowBrush, 40, 40, 20, 20)
			t39.Graphics.DrawEllipse(hollowBrush, 65, 40, 20, 20)
			t39.Graphics.DrawEllipse(hollowBrush, 15, 65, 20, 20)
			t39.Graphics.DrawEllipse(hollowBrush, 40, 65, 20, 20)
			t39.Graphics.DrawEllipse(hollowBrush, 65, 65, 20, 20)
			t39.Graphics.CompressAndClose()
			t3.Glyphs.Add(t39)

			Dim paleVioletRedbrush As New PdfBrush(PdfRgbColor.PaleVioletRed)
			page.Graphics.DrawString("0 1 2 3 4 5 6 7 8 9", t3, paleVioletRedbrush, 50, 150)
			Dim midnightBluebrush As New PdfBrush(PdfRgbColor.MidnightBlue)
			page.Graphics.DrawString("0 1 2 3 4 5 6 7 8 9", t3, midnightBluebrush, 50, 200)

			Dim output As SampleOutputInfo() = New SampleOutputInfo() {New SampleOutputInfo(document, "xfinium.pdf.sample.type3fonts.pdf")}
			Return output
		End Function
	End Class
End Namespace
