Imports System.IO
Imports Xfinium.Pdf
Imports Xfinium.Pdf.Graphics
Imports Xfinium.Pdf.Redaction

Namespace Xfinium.Pdf.Samples
	''' <summary>
	''' DocumentIncrementalUpdate sample.
	''' </summary>
	Public Class DocumentIncrementalUpdate
		''' <summary>
		''' Main method for running the sample.
		''' </summary>
		Public Shared Function Run(input As Stream) As SampleOutputInfo()
			Dim df As New PdfDocumentFeatures()
			' Do not load file attachments, new file attachments cannot be added.
			df.EnableDocumentFileAttachments = False
			' Do not load form fields, form fields cannot be filled and new form fields cannot be added.
			df.EnableDocumentFormFields = False
			' Do not load embedded JavaScript code, new JavaScript code at document level cannot be added.
			df.EnableDocumentJavaScriptFragments = False
			' Do not load the named destinations, new named destinations cannot be created.
			df.EnableDocumentNamedDestinations = False
			' Do not load the document outlines, new outlines cannot be created.
			df.EnableDocumentOutline = False
			' Do not load annotations, new annotations cannot be added to existing pages.
			df.EnablePageAnnotations = False
			' Do not load the page graphics, new graphics cannot be added to existing pages.
			df.EnablePageGraphics = False
			Dim document As New PdfFixedDocument(input, df)

			Dim helvetica As New PdfStandardFont(PdfStandardFontFace.HelveticaBold, 24)
			Dim brush As New PdfBrush()

			' Add a new page with some content on it.
			Dim page As PdfPage = document.Pages.Add()
			page.Graphics.DrawString("New page added to an existing document.", helvetica, brush, 20, 50)

			' When document features have been specified at load time the document is automatically saved in incremental update mode.
			document.Save(input)

			Return Nothing
		End Function
	End Class
End Namespace
