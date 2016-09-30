Imports System.IO
Imports Xfinium.Pdf
Imports Xfinium.Pdf.Graphics
Imports Xfinium.Pdf.Transforms

Namespace Xfinium.Pdf.Samples
	''' <summary>
	''' RemoveReplaceImages sample.
	''' </summary>
	Public Class RemoveReplaceImages
		''' <summary>
		''' Main method for running the sample.
		''' </summary>
		Public Shared Function Run(input As Stream) As SampleOutputInfo()
			' Load the input file.
			Dim document As New PdfFixedDocument(input)

			Dim replaceImageTransform As New PdfReplaceImageTransform()
			replaceImageTransform.ReplaceImage += New EventHandler(Of PdfReplaceImageEventArgs)(AddressOf HandleReplaceImage)
			Dim pageTransformer As New PdfPageTransformer(document.Pages(2))
			pageTransformer.ApplyTransform(replaceImageTransform)
			replaceImageTransform.ReplaceImage -= New EventHandler(Of PdfReplaceImageEventArgs)(AddressOf HandleReplaceImage)

			Dim output As SampleOutputInfo() = New SampleOutputInfo() {New SampleOutputInfo(document, "xfinium.pdf.sample.removereplaceimages.pdf")}
			Return output
		End Function

		Private Shared Sub HandleReplaceImage(sender As Object, e As PdfReplaceImageEventArgs)
			If e.OldImageID.Value = "/Im1" Then
				' Replace the existing image with a checkers board.
				Dim checkers As New MemoryStream(New Byte() {0, 255, 0, 255, 0, 255, _
					0, 255, 0, 255, 0, 255, _
					0, 255, 0, 255, 0, 255, _
					0, 255, 0, 255, 0, 255, _
					0})
				Dim rawImage As New PdfRawImage(checkers)
				rawImage.Width = 5
				rawImage.Height = 5
				rawImage.BitsPerComponent = 8
				rawImage.ColorSpace = New PdfGrayColorSpace()

				e.NewImage = rawImage
			Else
				If e.OldImageID.Value = "/Im2" Then
					' Remove the image from the page by setting the new image (the replacement image) to null.
					e.NewImage = Nothing
				End If
			End If
		End Sub
	End Class
End Namespace