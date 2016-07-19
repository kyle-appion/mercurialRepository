Imports System.IO
Imports Xfinium.Pdf
Imports Xfinium.Pdf.Graphics
Imports Xfinium.Pdf.Redaction

Namespace Xfinium.Pdf.Samples
	''' <summary>
	''' Redaction sample.
	''' </summary>
	Public Class Redaction
		''' <summary>
		''' Main method for running the sample.
		''' </summary>
		Public Shared Function Run(input As Stream) As SampleOutputInfo()
			Dim document As New PdfFixedDocument(input)
			Dim crText As New PdfContentRedactor(document.Pages(0))
			' Redact a rectangular area of 200*100 points and leave the redacted area uncovered.
			crText.RedactArea(New PdfVisualRectangle(50, 50, 200, 100))
			' Redact a rectangular area of 200*100 points and mark the redacted area with red.
			crText.RedactArea(New PdfVisualRectangle(50, 350, 200, 100), PdfRgbColor.Red)

			Dim crImages As New PdfContentRedactor(document.Pages(2))
			' Initialize the bulk redaction.
			crImages.BeginRedaction()
			' Prepare for redaction a rectangular area of 500*100 points and leave the redacted area uncovered.
			crImages.RedactArea(New PdfVisualRectangle(50, 50, 500, 100))
			' Prepare for redaction a rectangular area of 200*100 points and mark the redacted area with red.
			crImages.RedactArea(New PdfVisualRectangle(50, 350, 500, 100), PdfRgbColor.Red)
			' When images are redacted, the cleared pixels are set to 0. Depending on image colorspace the redacted area can appear black or colored.
			' Finish the redaction
			crImages.ApplyRedaction()

			Dim output As SampleOutputInfo() = New SampleOutputInfo() {New SampleOutputInfo(document, "xfinium.pdf.sample.redaction.pdf")}
			Return output
		End Function
	End Class
End Namespace
