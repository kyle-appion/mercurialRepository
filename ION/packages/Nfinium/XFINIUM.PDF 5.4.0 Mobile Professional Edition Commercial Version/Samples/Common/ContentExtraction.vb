Imports System.IO
Imports Xfinium.Pdf
Imports Xfinium.Pdf.Graphics
Imports Xfinium.Pdf.Content

Namespace Xfinium.Pdf.Samples
	''' <summary>
	''' ContentExtraction sample.
	''' </summary>
	Public Class ContentExtraction
		''' <summary>
		''' Main method for running the sample.
		''' </summary>
		Public Shared Function Run(input As Stream) As SampleOutputInfo()
			' Load the input file.
			Dim document As New PdfFixedDocument(input)

			ExtractTextAndHighlight(document)

			ExtractTextAndHighlightGlyphs(document)

			ExtractImagesAndHighlight(document)

			' Compress the page graphic content.
			For i As Integer = 0 To document.Pages.Count - 1
				document.Pages(i).Graphics.CompressAndClose()
			Next

			Dim output As SampleOutputInfo() = New SampleOutputInfo() {New SampleOutputInfo(document, "xfinium.pdf.sample.contentextraction.pdf")}
			Return output
		End Function

		''' <summary>
		''' Extracts text fragments from the first page and highlights the fragment boundaries.
		''' </summary>
		''' <param name="document"></param>
		Private Shared Sub ExtractTextAndHighlight(document As PdfFixedDocument)
			Dim penColor As New PdfRgbColor()
			Dim pen As New PdfPen(penColor, 0.5)
			Dim rnd As New Random()
			Dim rgb As Byte() = New Byte(2) {}

			Dim ce As New PdfContentExtractor(document.Pages(0))
			Dim tfc As PdfTextFragmentCollection = ce.ExtractTextFragments()
			For i As Integer = 0 To tfc.Count - 1
				rnd.NextBytes(rgb)
				penColor.R = rgb(0)
				penColor.G = rgb(1)
				penColor.B = rgb(2)

				Dim boundingPath As New PdfPath()
				boundingPath.StartSubpath(tfc(i).FragmentCorners(0).X, tfc(i).FragmentCorners(0).Y)
				boundingPath.AddLineTo(tfc(i).FragmentCorners(1).X, tfc(i).FragmentCorners(1).Y)
				boundingPath.AddLineTo(tfc(i).FragmentCorners(2).X, tfc(i).FragmentCorners(2).Y)
				boundingPath.AddLineTo(tfc(i).FragmentCorners(3).X, tfc(i).FragmentCorners(3).Y)
				boundingPath.CloseSubpath()

				document.Pages(0).Graphics.DrawPath(pen, boundingPath)
			Next
		End Sub

		''' <summary>
		''' Extracts text fragments from the 2nd page and highlights the glyphs in the fragment.
		''' </summary>
		''' <param name="document"></param>
		Private Shared Sub ExtractTextAndHighlightGlyphs(document As PdfFixedDocument)
			Dim penColor As New PdfRgbColor()
			Dim pen As New PdfPen(penColor, 0.5)
			Dim rnd As New Random()
			Dim rgb As Byte() = New Byte(2) {}

			Dim ce As New PdfContentExtractor(document.Pages(1))
			Dim tfc As PdfTextFragmentCollection = ce.ExtractTextFragments()
			Dim tf As PdfTextFragment = tfc(1)
			For i As Integer = 0 To tf.Glyphs.Count - 1
				rnd.NextBytes(rgb)
				penColor.R = rgb(0)
				penColor.G = rgb(1)
				penColor.B = rgb(2)

				Dim boundingPath As New PdfPath()
				boundingPath.StartSubpath(tf.Glyphs(i).GlyphCorners(0).X, tf.Glyphs(i).GlyphCorners(0).Y)
				boundingPath.AddLineTo(tf.Glyphs(i).GlyphCorners(1).X, tf.Glyphs(i).GlyphCorners(1).Y)
				boundingPath.AddLineTo(tf.Glyphs(i).GlyphCorners(2).X, tf.Glyphs(i).GlyphCorners(2).Y)
				boundingPath.AddLineTo(tf.Glyphs(i).GlyphCorners(3).X, tf.Glyphs(i).GlyphCorners(3).Y)
				boundingPath.CloseSubpath()

				document.Pages(1).Graphics.DrawPath(pen, boundingPath)
			Next
		End Sub

		''' <summary>
		''' Extracts text fragments from the 3rd page and highlights the glyphs in the fragment.
		''' </summary>
		''' <param name="document"></param>
		Private Shared Sub ExtractImagesAndHighlight(document As PdfFixedDocument)
			Dim pen As New PdfPen(New PdfRgbColor(255, 0, 192), 0.5)
			Dim brush As New PdfBrush(New PdfRgbColor(0, 0, 0))
			Dim helvetica As New PdfStandardFont(PdfStandardFontFace.Helvetica, 8)
			Dim sao As New PdfStringAppearanceOptions()
			sao.Brush = brush
			sao.Font = helvetica
			Dim slo As New PdfStringLayoutOptions()
			slo.Width = 1000

			Dim ce As New PdfContentExtractor(document.Pages(2))
			Dim eic As PdfEmbeddedImageCollection = ce.ExtractImages(False)
			For i As Integer = 0 To eic.Count - 1
				Dim imageProperties As String = String.Format("Image ID: {0}" & vbLf & "Pixel width: {1} pixels" & vbLf & "Pixel height: {2} pixels" & vbLf & "Display width: {3} points" & vbLf & "Display height: {4} points" & vbLf & "Horizonal Resolution: {5} dpi" & vbLf & "Vertical Resolution: {6} dpi", eic(i).ImageID, eic(i).Width, eic(i).Height, eic(i).DisplayWidth, eic(i).DisplayHeight, _
					eic(i).DpiX, eic(i).DpiY)

				Dim boundingPath As New PdfPath()
				boundingPath.StartSubpath(eic(i).ImageCorners(0).X, eic(i).ImageCorners(0).Y)
				boundingPath.AddLineTo(eic(i).ImageCorners(1).X, eic(i).ImageCorners(1).Y)
				boundingPath.AddLineTo(eic(i).ImageCorners(2).X, eic(i).ImageCorners(2).Y)
				boundingPath.AddLineTo(eic(i).ImageCorners(3).X, eic(i).ImageCorners(3).Y)
				boundingPath.CloseSubpath()

				document.Pages(2).Graphics.DrawPath(pen, boundingPath)
				slo.X = eic(i).ImageCorners(3).X + 1
				slo.Y = eic(i).ImageCorners(3).Y + 1
				document.Pages(2).Graphics.DrawString(imageProperties, sao, slo)
			Next
		End Sub
	End Class
End Namespace
