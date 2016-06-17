Imports System.IO
Imports Xfinium.Pdf
Imports Xfinium.Pdf.Graphics

Namespace Xfinium.Pdf.Samples
	''' <summary>
	''' Images sample.
	''' </summary>
	Public Class Images
		''' <summary>
		''' Main method for running the sample.
		''' </summary>
		''' <param name="imageStream"></param>
		''' <param name="cmykImageStream"></param>
		''' <param name="softMaskStream"></param>
		''' <param name="stencilMaskStream"></param>
		Public Shared Function Run(imageStream As Stream, cmykImageStream As Stream, softMaskStream As Stream, stencilMaskStream As Stream) As SampleOutputInfo()
			Dim document As New PdfFixedDocument()
			Dim helveticaBoldTitle As New PdfStandardFont(PdfStandardFontFace.HelveticaBold, 16)
			Dim helveticaSection As New PdfStandardFont(PdfStandardFontFace.Helvetica, 10)

			Dim page As PdfPage = document.Pages.Add()
			DrawImages(page, imageStream, helveticaBoldTitle, helveticaSection)

			page = document.Pages.Add()
			DrawImageMasks(page, imageStream, softMaskStream, stencilMaskStream, helveticaBoldTitle, helveticaSection)

			page = document.Pages.Add()
			DrawCmykTiff(page, cmykImageStream, helveticaBoldTitle)

			Dim output As SampleOutputInfo() = New SampleOutputInfo() {New SampleOutputInfo(document, "xfinium.pdf.sample.images.pdf")}
			Return output
		End Function

		Private Shared Sub DrawImages(page As PdfPage, imageStream As Stream, titleFont As PdfFont, sectionFont As PdfFont)
			Dim brush As New PdfBrush()

			Dim jpeg As New PdfJpegImage(imageStream)

			page.Graphics.DrawString("Images", titleFont, brush, 20, 50)

			page.Graphics.DrawString("Scaling:", sectionFont, brush, 20, 70)

			' Draw the image 3 times on the page at different sizes.
			page.Graphics.DrawImage(jpeg, 3, 90, 100, 0)
			page.Graphics.DrawImage(jpeg, 106, 90, 200, 0)
			page.Graphics.DrawImage(jpeg, 309, 90, 300, 0)

			page.Graphics.DrawString("Flipping:", sectionFont, brush, 20, 320)
			page.Graphics.DrawImage(jpeg, 20, 340, 260, 0)
			page.Graphics.DrawImage(jpeg, 310, 340, 260, 0, 0, _
				PdfFlipDirection.VerticalFlip)
			page.Graphics.DrawImage(jpeg, 20, 550, 260, 0, 0, _
				PdfFlipDirection.HorizontalFlip)
			page.Graphics.DrawImage(jpeg, 310, 550, 260, 0, 0, _
				PdfFlipDirection.VerticalFlip Or PdfFlipDirection.HorizontalFlip)

			page.Graphics.CompressAndClose()
		End Sub

		Private Shared Sub DrawImageMasks(page As PdfPage, imageStream As Stream, softMaskStream As Stream, stencilMaskStream As Stream, titleFont As PdfFont, sectionFont As PdfFont)
			Dim brush As New PdfBrush()

			page.Graphics.DrawString("Images Masks", titleFont, brush, 20, 50)

			page.Graphics.DrawString("Soft mask:", sectionFont, brush, 20, 70)
			Dim softMaskImage As New PdfPngImage(softMaskStream)
			Dim softMask As New PdfSoftMask(softMaskImage)
			imageStream.Position = 0
			Dim softMaskJpeg As New PdfJpegImage(imageStream)
			softMaskJpeg.Mask = softMask
			' Draw the image with a soft mask.
			page.Graphics.DrawImage(softMaskJpeg, 20, 90, 280, 0)

			page.Graphics.DrawString("Stencil mask:", sectionFont, brush, 320, 70)
			Dim stencilMaskImage As New PdfPngImage(stencilMaskStream)
			Dim stencilMask As New PdfStencilMask(stencilMaskImage)
			imageStream.Position = 0
			Dim stencilMaskJpeg As New PdfJpegImage(imageStream)
			stencilMaskJpeg.Mask = stencilMask
			' Draw the image with a stencil mask.
			page.Graphics.DrawImage(stencilMaskJpeg, 320, 90, 280, 0)

			page.Graphics.DrawString("Stencil mask painting:", sectionFont, brush, 20, 320)
			Dim redBrush As New PdfBrush(PdfRgbColor.DarkRed)
			page.Graphics.DrawStencilMask(stencilMask, redBrush, 20, 340, 280, 0)
			Dim blueBrush As New PdfBrush(PdfRgbColor.DarkBlue)
			page.Graphics.DrawStencilMask(stencilMask, blueBrush, 320, 340, 280, 0)
			Dim greenBrush As New PdfBrush(PdfRgbColor.DarkGreen)
			page.Graphics.DrawStencilMask(stencilMask, greenBrush, 20, 550, 280, 0)
			Dim yellowBrush As New PdfBrush(PdfRgbColor.YellowGreen)
			page.Graphics.DrawStencilMask(stencilMask, yellowBrush, 320, 550, 280, 0)

			page.Graphics.CompressAndClose()
		End Sub

		Private Shared Sub DrawCmykTiff(page As PdfPage, cmykImageStream As Stream, titleFont As PdfFont)
			Dim brush As New PdfBrush()

			page.Graphics.DrawString("CMYK TIFF", titleFont, brush, 20, 50)

			Dim cmykTiff As New PdfTiffImage(cmykImageStream)
			page.Graphics.DrawImage(cmykTiff, 20, 90, 570, 0)

			page.Graphics.CompressAndClose()
		End Sub
	End Class
End Namespace
