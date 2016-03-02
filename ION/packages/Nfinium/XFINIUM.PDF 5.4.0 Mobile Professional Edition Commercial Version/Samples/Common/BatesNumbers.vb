Imports System.IO
Imports Xfinium.Pdf
Imports Xfinium.Pdf.Graphics
Imports Xfinium.Pdf.Content

Namespace Xfinium.Pdf.Samples
	''' <summary>
	''' BatesNumbers sample.
	''' </summary>
	Public Class BatesNumbers
		''' <summary>
		''' Main method for running the sample.
		''' </summary>
		Public Shared Function Run(input As Stream) As SampleOutputInfo()
			' Load the input file.
			Dim document As New PdfFixedDocument(input)

			Dim bna As New PdfBatesNumberAppearance()
			bna.Location = New PdfPoint(25, 5)
			bna.Brush = New PdfBrush(PdfRgbColor.DarkRed)

			Dim bnp As New PdfBatesNumberProvider()
			bnp.Prefix = "XFINIUM"
			bnp.Suffix = "PDF"
			bnp.StartNumber = 1

			PdfBatesNumber.WriteBatesNumber(document, bnp, bna)

			Dim output As SampleOutputInfo() = New SampleOutputInfo() {New SampleOutputInfo(document, "xfinium.pdf.sample.batesnumbers.pdf")}
			Return output
		End Function
	End Class
End Namespace