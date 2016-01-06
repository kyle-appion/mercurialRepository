Imports System.IO
Imports Xfinium.Pdf
Imports Xfinium.Pdf.Graphics

Namespace Xfinium.Pdf.Samples
	''' <summary>
	''' FileAttachments sample.
	''' </summary>
	Public Class FileAttachments
		''' <summary>
		''' Main method for running the sample.
		''' </summary>
		Public Shared Function Run(s1 As Stream, s2 As Stream) As SampleOutputInfo()
			Dim document As New PdfFixedDocument()
			document.DisplayMode = PdfDisplayMode.UseAttachments

			Dim helvetica As New PdfStandardFont(PdfStandardFontFace.Helvetica, 16)
			Dim blackBrush As New PdfBrush()
			Dim page As PdfPage = document.Pages.Add()
			page.Graphics.DrawString("This document contains 2 file attachments:", helvetica, blackBrush, 50, 50)
			page.Graphics.DrawString("1. fileattachments.cs.html", helvetica, blackBrush, 50, 70)
			page.Graphics.DrawString("2. fileattachments.vb.html", helvetica, blackBrush, 50, 90)

			Dim fileData1 As Byte() = New Byte(s1.Length - 1) {}
			s1.Read(fileData1, 0, fileData1.Length)
			Dim fileAttachment1 As New PdfDocumentFileAttachment()
			fileAttachment1.Payload = fileData1
			fileAttachment1.FileName = "fileattachments.cs.html"
			fileAttachment1.Description = "C# Source Code for FileAttachments sample"
			document.FileAttachments.Add(fileAttachment1)

			Dim fileData2 As Byte() = New Byte(s2.Length - 1) {}
			s2.Read(fileData2, 0, fileData2.Length)
			Dim fileAttachment2 As New PdfDocumentFileAttachment()
			fileAttachment2.Payload = fileData1
			fileAttachment2.FileName = "fileattachments.vb.html"
			fileAttachment2.Description = "VB.NET Source Code for FileAttachments sample"
			document.FileAttachments.Add(fileAttachment2)

			Dim output As SampleOutputInfo() = New SampleOutputInfo() {New SampleOutputInfo(document, "xfinium.pdf.sample.fileattachments.pdf")}
			Return output
		End Function
	End Class
End Namespace
