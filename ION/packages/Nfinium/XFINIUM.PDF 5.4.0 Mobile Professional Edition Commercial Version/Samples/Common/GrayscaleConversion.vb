Imports System.IO
Imports Xfinium.Pdf
Imports Xfinium.Pdf.Graphics
Imports Xfinium.Pdf.Transforms

Namespace Xfinium.Pdf.Samples
	''' <summary>
	''' GrayscaleConversion sample.
	''' </summary>
	Public Class GrayscaleConversion
		''' <summary>
		''' Main method for running the sample.
		''' </summary>
		Public Shared Function Run(input As Stream) As SampleOutputInfo()
			' Load the input file.
			Dim document As New PdfFixedDocument(input)

			Dim grayTransform As New PdfConvertToGrayTransform()
			Dim pageTransformer As New PdfPageTransformer(document.Pages(3))
			pageTransformer.ApplyTransform(grayTransform)

			Dim output As SampleOutputInfo() = New SampleOutputInfo() {New SampleOutputInfo(document, "xfinium.pdf.sample.grayscaleconversion.pdf")}
			Return output
		End Function
	End Class
End Namespace