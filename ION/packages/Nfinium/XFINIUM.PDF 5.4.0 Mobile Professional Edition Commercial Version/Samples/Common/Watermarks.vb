Imports System.IO
Imports Xfinium.Pdf
Imports Xfinium.Pdf.Graphics
Imports Xfinium.Pdf.Content

Namespace Xfinium.Pdf.Samples
	''' <summary>
	''' Watermarks sample.
	''' </summary>
	Public Class Watermarks
		''' <summary>
		''' Main method for running the sample.
		''' </summary>
		Public Shared Function Run(input As Stream) As SampleOutputInfo()
			' Load the input file.
			Dim document As New PdfFixedDocument(input)

			DrawWatermarkUnderPageContent(document.Pages(0))

			DrawWatermarkOverPageContent(document.Pages(1))

			DrawWatermarkWithTransparency(document.Pages(2))

			' Compress the page graphic content.
			For i As Integer = 0 To document.Pages.Count - 1
				document.Pages(i).Graphics.CompressAndClose()
			Next

			Dim output As SampleOutputInfo() = New SampleOutputInfo() {New SampleOutputInfo(document, "xfinium.pdf.sample.watermarks.pdf")}
			Return output
		End Function

		''' <summary>
		''' 
		''' </summary>
		''' <param name="page"></param>
		Private Shared Sub DrawWatermarkUnderPageContent(page As PdfPage)
			Dim redBrush As New PdfBrush(New PdfRgbColor(192, 0, 0))
			Dim helvetica As New PdfStandardFont(PdfStandardFontFace.HelveticaBold, 36)

			' Set the page graphics to be located under existing page content.
			page.SetGraphicsPosition(PdfPageGraphicsPosition.UnderExistingPageContent)

			Dim sao As New PdfStringAppearanceOptions()
			sao.Brush = redBrush
			sao.Font = helvetica
			Dim slo As New PdfStringLayoutOptions()
			slo.X = 130
			slo.Y = 670
			slo.Rotation = 60
			page.Graphics.DrawString("Sample watermark under page content", sao, slo)
		End Sub

		''' <summary>
		''' 
		''' </summary>
		''' <param name="page"></param>
		Private Shared Sub DrawWatermarkOverPageContent(page As PdfPage)
			Dim redBrush As New PdfBrush(New PdfRgbColor(192, 0, 0))
			Dim helvetica As New PdfStandardFont(PdfStandardFontFace.HelveticaBold, 32)

			' The page graphics is located by default on top of existing page content.
			'page.SetGraphicsPosition(PdfPageGraphicsPosition.OverExistingPageContent);

			' Draw the watermark over page content.
			' Page content under the watermark will be masked.
			page.Graphics.DrawString("Sample watermark over page content", helvetica, redBrush, 20, 335)

			page.Graphics.SaveGraphicsState()

			' Draw the watermark over page content but using the Multiply blend mode.
			' The watermak will appear as if drawn under the page content, useful when watermarking scanned documents.
			' If the watermark is drawn under page content for scanned documents, it will not be visible because the scanned image will block it.
			Dim gs1 As New PdfExtendedGraphicState()
			gs1.BlendMode = PdfBlendMode.Multiply
			page.Graphics.SetExtendedGraphicState(gs1)
			page.Graphics.DrawString("Sample watermark over page content", helvetica, redBrush, 20, 385)

			' Draw the watermark over page content but using the Luminosity blend mode.
			' Both the page content and the watermark will be visible.
			Dim gs2 As New PdfExtendedGraphicState()
			gs2.BlendMode = PdfBlendMode.Luminosity
			page.Graphics.SetExtendedGraphicState(gs2)
			page.Graphics.DrawString("Sample watermark over page content", helvetica, redBrush, 20, 435)

			page.Graphics.RestoreGraphicsState()
		End Sub

		''' <summary>
		''' 
		''' </summary>
		''' <param name="page"></param>
		Private Shared Sub DrawWatermarkWithTransparency(page As PdfPage)
			Dim redBrush As New PdfBrush(New PdfRgbColor(192, 0, 0))
			Dim helvetica As New PdfStandardFont(PdfStandardFontFace.HelveticaBold, 36)

			' The page graphics is located by default on top of existing page content.
			'page.SetGraphicsPosition(PdfPageGraphicsPosition.OverExistingPageContent);

			page.Graphics.SaveGraphicsState()

			Dim sao As New PdfStringAppearanceOptions()
			sao.Brush = redBrush
			sao.Font = helvetica
			Dim slo As New PdfStringLayoutOptions()
			slo.X = 130
			slo.Y = 670
			slo.Rotation = 60

			' Draw the watermark over page content but setting the transparency to a value lower than 1.
			' The page content will be partially visible through the watermark.
			Dim gs1 As New PdfExtendedGraphicState()
			gs1.FillAlpha = 0.3
			page.Graphics.SetExtendedGraphicState(gs1)
			page.Graphics.DrawString("Sample watermark over page content", sao, slo)

			page.Graphics.RestoreGraphicsState()
		End Sub
	End Class
End Namespace
