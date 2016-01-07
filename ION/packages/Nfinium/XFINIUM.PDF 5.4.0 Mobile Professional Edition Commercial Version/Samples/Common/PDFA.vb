Imports System.IO
Imports Xfinium.Pdf
Imports Xfinium.Pdf.Graphics
Imports Xfinium.Pdf.Standards

Namespace Xfinium.Pdf.Samples
	''' <summary>
	''' PDF/A sample.
	''' </summary>
	Public Class PDFA
		''' <summary>
		''' Main method for running the sample.
		''' </summary>
		Public Shared Function Run(iccInput As Stream, ttfInput As Stream) As SampleOutputInfo()
			Dim document As New PdfFixedDocument()

			' Setup the document by creating a PDF/A output intent, based on a RGB ICC profile, 
			' and document information and metadata
			Dim icc As New PdfIccColorSpace()
			Dim profilePayload As Byte() = New Byte(iccInput.Length - 1) {}
			iccInput.Read(profilePayload, 0, profilePayload.Length)
			icc.IccProfile = profilePayload
			Dim oi As New PdfOutputIntent()
			oi.Type = PdfOutputIntentType.PdfA1
			oi.Info = "sRGB IEC61966-2.1"
			oi.OutputConditionIdentifier = "Custom"
			oi.DestinationOutputProfile = icc
			document.OutputIntents = New PdfOutputIntentCollection()
			document.OutputIntents.Add(oi)

			document.DocumentInformation = New PdfDocumentInformation()
			document.DocumentInformation.Author = "XFINIUM Software"
			document.DocumentInformation.Title = "XFINIUM.PDF PDF/A-1B Demo"
			document.DocumentInformation.Creator = "XFINIUM.PDF PDF/A-1B Demo"
			document.DocumentInformation.Producer = "XFINIUM.PDF"
			document.DocumentInformation.Keywords = "pdf/a"
			document.DocumentInformation.Subject = "PDF/A-1B Sample produced by XFINIUM.PDF"
			document.XmpMetadata = New PdfXmpMetadata()

			Dim page As PdfPage = document.Pages.Add()
			page.Rotation = 90

			' All fonts must be embedded in a PDF/A document.
			Dim sao As New PdfStringAppearanceOptions()
			sao.Font = New PdfAnsiTrueTypeFont(ttfInput, 24, True)
			sao.Brush = New PdfBrush(New PdfRgbColor(0, 0, 128))

			Dim slo As New PdfStringLayoutOptions()
			slo.HorizontalAlign = PdfStringHorizontalAlign.Center
			slo.VerticalAlign = PdfStringVerticalAlign.Bottom
			slo.X = page.Width / 2
			slo.Y = page.Height / 2 - 10
			page.Graphics.DrawString("XFINIUM.PDF", sao, slo)
			slo.Y = page.Height / 2 + 10
			slo.VerticalAlign = PdfStringVerticalAlign.Top
			sao.Font.Size = 16
			page.Graphics.DrawString("This is a sample PDF/A document that shows the PDF/A capabilities in XFINIUM.PDF library", sao, slo)

			' The document is formatted as PDF/A using the PdfAFormatter class:
			' PdfAFormatter.Save(document, outputStream, PdfAFormat.PdfA1b)
			Dim output As SampleOutputInfo() = New SampleOutputInfo() {New SampleOutputInfo(document, "xfinium.pdf.sample.pdfa.pdf")}
			Return output
		End Function
	End Class
End Namespace