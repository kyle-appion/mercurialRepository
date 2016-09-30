Imports System.IO
Imports Xfinium.Pdf
Imports Xfinium.Pdf.Graphics
Imports Xfinium.Pdf.Graphics.OptionalContent

Namespace Xfinium.Pdf.Samples
	''' <summary>
	''' OptionalContent sample.
	''' </summary>
	Public Class OptionalContent
		''' <summary>
		''' Main method for running the sample.
		''' </summary>
		Public Shared Function Run() As SampleOutputInfo()
			Dim document As New PdfFixedDocument()
			Dim helveticaBold As New PdfStandardFont(PdfStandardFontFace.HelveticaBold, 18)
			Dim blackBrush As New PdfBrush()
			Dim greenBrush As New PdfBrush(PdfRgbColor.DarkGreen)
			Dim yellowBrush As New PdfBrush(PdfRgbColor.Yellow)
			Dim bluePen As New PdfPen(PdfRgbColor.DarkBlue, 5)
			Dim redPen As New PdfPen(PdfRgbColor.DarkRed, 5)

			Dim page As PdfPage = document.Pages.Add()
			page.Graphics.DrawString("Simple optional content: the green rectangle", helveticaBold, blackBrush, 20, 50)

			Dim ocgPage1 As New PdfOptionalContentGroup()
			ocgPage1.Name = "Page 1 - Green Rectangle"
			page.Graphics.BeginOptionalContentGroup(ocgPage1)
			page.Graphics.DrawRectangle(bluePen, greenBrush, 20, 100, 570, 400)
			page.Graphics.EndOptionalContentGroup()

			page = document.Pages.Add()
			page.Graphics.DrawString("Multipart optional content: the green rectangles", helveticaBold, blackBrush, 20, 50)

			Dim ocgPage2 As New PdfOptionalContentGroup()
			ocgPage2.Name = "Page 2 - Green Rectangles"
			page.Graphics.BeginOptionalContentGroup(ocgPage2)
			page.Graphics.DrawRectangle(bluePen, greenBrush, 20, 200, 570, 200)
			page.Graphics.EndOptionalContentGroup()

			page.Graphics.DrawRectangle(redPen, yellowBrush, 250, 90, 110, 680)

			page.Graphics.BeginOptionalContentGroup(ocgPage2)
			page.Graphics.DrawRectangle(bluePen, greenBrush, 20, 500, 570, 200)
			page.Graphics.EndOptionalContentGroup()

			page = document.Pages.Add()
			page.Graphics.DrawString("Imbricated optional content: the green and yellow rectangles", helveticaBold, blackBrush, 20, 50)

			Dim ocgPage31 As New PdfOptionalContentGroup()
			ocgPage31.Name = "Page 3 - Green Rectangle"
			page.Graphics.BeginOptionalContentGroup(ocgPage31)
			page.Graphics.DrawRectangle(bluePen, greenBrush, 20, 100, 570, 600)

			Dim ocgPage32 As New PdfOptionalContentGroup()
			ocgPage32.Name = "Page 3 - Yellow Rectangle"
			page.Graphics.BeginOptionalContentGroup(ocgPage32)
			page.Graphics.DrawRectangle(redPen, yellowBrush, 100, 200, 400, 300)
			page.Graphics.EndOptionalContentGroup()
			' ocgPage32
			page.Graphics.EndOptionalContentGroup()
			' ocgPage31
			page = document.Pages.Add()
			page.Graphics.DrawString("Multipage optional content: the green rectangles on page 4 & 5", helveticaBold, blackBrush, 20, 50)

			Dim ocgPage45 As New PdfOptionalContentGroup()
			ocgPage45.Name = "Page 4 & 5 - Green Rectangles"
			page.Graphics.BeginOptionalContentGroup(ocgPage45)
			page.Graphics.DrawRectangle(bluePen, greenBrush, 20, 200, 570, 200)
			page.Graphics.EndOptionalContentGroup()

			page.Graphics.DrawRectangle(redPen, yellowBrush, 250, 90, 110, 680)

			page.Graphics.BeginOptionalContentGroup(ocgPage45)
			page.Graphics.DrawRectangle(bluePen, greenBrush, 20, 500, 570, 200)
			page.Graphics.EndOptionalContentGroup()

			page = document.Pages.Add()
			page.Graphics.DrawString("Multipage optional content: continued", helveticaBold, blackBrush, 20, 50)

			page.Graphics.BeginOptionalContentGroup(ocgPage45)
			page.Graphics.DrawRectangle(bluePen, greenBrush, 20, 200, 570, 200)
			page.Graphics.EndOptionalContentGroup()

			page.Graphics.DrawRectangle(redPen, yellowBrush, 250, 90, 110, 680)

			page.Graphics.BeginOptionalContentGroup(ocgPage45)
			page.Graphics.DrawRectangle(bluePen, greenBrush, 20, 500, 570, 200)
			page.Graphics.EndOptionalContentGroup()

			' Build the display tree for the optional content, 
			' how its structure and relationships between optional content groups are presented to the user.
			Dim ocgPage1Node As New PdfOptionalContentDisplayTreeNode(ocgPage1)
			document.OptionalContentProperties.DisplayTree.Nodes.Add(ocgPage1Node)
			Dim ocgPage2Node As New PdfOptionalContentDisplayTreeNode(ocgPage2)
			document.OptionalContentProperties.DisplayTree.Nodes.Add(ocgPage2Node)
			Dim ocgPage31Node As New PdfOptionalContentDisplayTreeNode(ocgPage31)
			document.OptionalContentProperties.DisplayTree.Nodes.Add(ocgPage31Node)
			Dim ocgPage32Node As New PdfOptionalContentDisplayTreeNode(ocgPage32)
			ocgPage31Node.Nodes.Add(ocgPage32Node)
			Dim ocgPage45Node As New PdfOptionalContentDisplayTreeNode(ocgPage45)
			document.OptionalContentProperties.DisplayTree.Nodes.Add(ocgPage45Node)

			Dim output As SampleOutputInfo() = New SampleOutputInfo() {New SampleOutputInfo(document, "xfinium.pdf.sample.optionalcontent.pdf")}
			Return output
		End Function
	End Class
End Namespace
