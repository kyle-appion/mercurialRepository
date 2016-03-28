Imports System.IO
Imports Xfinium.Pdf
Imports Xfinium.Pdf.Graphics
Imports Xfinium.Pdf.Core
Imports Xfinium.Pdf.Content

Namespace Xfinium.Pdf.Samples
	''' <summary>
	''' PageObjects sample.
	''' </summary>
	Public Class PageObjects
		''' <summary>
		''' Main method for running the sample.
		''' </summary>
		Public Shared Function Run(input As Stream) As SampleOutputInfo()
			Dim brush As New PdfBrush()
			Dim redPen As New PdfPen(PdfRgbColor.Red, 1)
			Dim helvetica As New PdfStandardFont(PdfStandardFontFace.Helvetica, 10)

			Dim document As New PdfFixedDocument(input)

			Dim ce As New PdfContentExtractor(document.Pages(0))
			Dim voc As PdfVisualObjectCollection = ce.ExtractVisualObjects(False)

			Dim contour As PdfPath = Nothing
			For i As Integer = 0 To voc.Count - 1
				Select Case voc(i).Type
					Case PdfVisualObjectType.Image
						Dim ivo As PdfImageVisualObject = TryCast(voc(i), PdfImageVisualObject)
						contour = New PdfPath()
						contour.StartSubpath(ivo.Image.ImageCorners(0).X - 5, ivo.Image.ImageCorners(0).Y + 5)
						contour.AddLineTo(ivo.Image.ImageCorners(1).X + 5, ivo.Image.ImageCorners(1).Y + 5)
						contour.AddLineTo(ivo.Image.ImageCorners(2).X + 5, ivo.Image.ImageCorners(2).Y - 5)
						contour.AddLineTo(ivo.Image.ImageCorners(3).X - 5, ivo.Image.ImageCorners(3).Y - 5)
						contour.CloseSubpath()
						document.Pages(0).Graphics.DrawPath(redPen, contour)

						document.Pages(0).Graphics.DrawString("Image", helvetica, brush, ivo.Image.ImageCorners(0).X - 5, ivo.Image.ImageCorners(0).Y + 5)
						Exit Select
					Case PdfVisualObjectType.Text
						Dim tvo As PdfTextVisualObject = TryCast(voc(i), PdfTextVisualObject)
						contour = New PdfPath()
						contour.StartSubpath(tvo.TextFragment.FragmentCorners(0).X - 5, tvo.TextFragment.FragmentCorners(0).Y + 5)
						contour.AddLineTo(tvo.TextFragment.FragmentCorners(1).X + 5, tvo.TextFragment.FragmentCorners(1).Y + 5)
						contour.AddLineTo(tvo.TextFragment.FragmentCorners(2).X + 5, tvo.TextFragment.FragmentCorners(2).Y - 5)
						contour.AddLineTo(tvo.TextFragment.FragmentCorners(3).X - 5, tvo.TextFragment.FragmentCorners(3).Y - 5)
						contour.CloseSubpath()
						document.Pages(0).Graphics.DrawPath(redPen, contour)

						document.Pages(0).Graphics.DrawString("Text", helvetica, brush, tvo.TextFragment.FragmentCorners(0).X - 5, tvo.TextFragment.FragmentCorners(0).Y + 5)
						Exit Select
					Case PdfVisualObjectType.Path
						Dim pvo As PdfPathVisualObject = TryCast(voc(i), PdfPathVisualObject)
						' Examine all the path points and determine the minimum rectangle that bounds the path.
						Dim minX As Double = 999999, minY As Double = 999999, maxX As Double = -999999, maxY As Double = -999999
						For j As Integer = 0 To pvo.PathItems.Count - 1
							Dim pi As PdfPathItem = pvo.PathItems(j)
							If pi.Points IsNot Nothing Then
								For k As Integer = 0 To pi.Points.Length - 1
									If minX >= pi.Points(k).X Then
										minX = pi.Points(k).X
									End If
									If minY >= pi.Points(k).Y Then
										minY = pi.Points(k).Y
									End If
									If maxX <= pi.Points(k).X Then
										maxX = pi.Points(k).X
									End If
									If maxY <= pi.Points(k).Y Then
										maxY = pi.Points(k).Y
									End If
								Next
							End If
						Next

						contour = New PdfPath()
						contour.StartSubpath(minX - 5, minY - 5)
						contour.AddLineTo(maxX + 5, minY - 5)
						contour.AddLineTo(maxX + 5, maxY + 5)
						contour.AddLineTo(minX - 5, maxY + 5)
						contour.CloseSubpath()
						document.Pages(0).Graphics.DrawPath(redPen, contour)

						document.Pages(0).Graphics.DrawString("Path", helvetica, brush, minX - 5, maxY + 5)
						' Skip the rest of path objects, they are the evaluation message
						i = voc.Count
						Exit Select
				End Select
			Next

			Dim output As SampleOutputInfo() = New SampleOutputInfo() {New SampleOutputInfo(document, "xfinium.pdf.sample.pageobjects.pdf")}
			Return output
		End Function
	End Class
End Namespace