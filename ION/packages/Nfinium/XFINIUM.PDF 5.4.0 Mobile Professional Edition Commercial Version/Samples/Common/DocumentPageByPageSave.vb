Imports System.IO
Imports Xfinium.Pdf
Imports Xfinium.Pdf.Graphics
Imports Xfinium.Pdf.Redaction

Namespace Xfinium.Pdf.Samples
	''' <summary>
	''' DocumentPageByPageSave sample.
	''' </summary>
	Public Class DocumentPageByPageSave
		''' <summary>
		''' Main method for running the sample.
		''' </summary>
		Public Shared Function Run(output As Stream) As SampleOutputInfo()
			Dim document As New PdfFixedDocument()

			' Init the save operation
			document.BeginSave(output)

			Dim color As New PdfRgbColor()
			Dim brush As New PdfBrush(color)
			Dim rnd As New Random()

			For i As Integer = 0 To 2
				Dim page As PdfPage = document.Pages.Add()
				page.Width = 1000
				page.Height = 1000

				For y As Integer = 1 To page.Height
					For x As Integer = 0 To page.Width - 1
						color.R = CByte(rnd.[Next](256))
						color.G = CByte(rnd.[Next](256))
						color.B = CByte(rnd.[Next](256))

						page.Graphics.DrawRectangle(brush, x, y - 1, 1, 1)
					Next

					If (y Mod 100) = 0 Then
						' Compress the graphics that have been drawn so far and save them.
						page.Graphics.Compress()
						page.SaveGraphics()
					End If
				Next

				' Close the page graphics and save the page.
				page.Graphics.CompressAndClose()
				page.Save()
			Next

			' Finish the document.
			document.EndSave()

			Return Nothing
		End Function
	End Class
End Namespace
