Imports System.IO
Imports Xfinium.Pdf
Imports Xfinium.Pdf.Graphics

Namespace Xfinium.Pdf.Samples
	''' <summary>
	''' DocumentProperties sample.
	''' </summary>
	Public Class DocumentProperties
		''' <summary>
		''' Main method for running the sample.
		''' </summary>
		Public Shared Function Run() As SampleOutputInfo()
			Dim document As New PdfFixedDocument()
			document.Pages.Add()
			' Display the document in full screen mode.
			document.DisplayMode = PdfDisplayMode.FullScreen

			' Fill the document information.
			document.DocumentInformation = New PdfDocumentInformation()
			document.DocumentInformation.Author = "Xfinium Software"
			document.DocumentInformation.CreationDate = DateTime.Now
			document.DocumentInformation.ModifyDate = DateTime.Now
			document.DocumentInformation.Creator = "Xfinium.Pdf DocumentProperties sample"
			document.DocumentInformation.Producer = "Xfinium.Pdf"
			document.DocumentInformation.Title = "Xfinium.Pdf DocumentProperties sample"
			document.DocumentInformation.Subject = "Xfinium.Pdf sample code"
			document.DocumentInformation.Keywords = "xfinium.pdf,pdf,sample"

			' Set custom metadata in the XMP metadata.
			document.XmpMetadata = New PdfXmpMetadata()
			' This custom metadata will appear as a child of 'xmpmeta' root node.
			document.XmpMetadata.Metadata = "<custom>Custom metadata</custom>"

			' Set the viewer preferences.
			document.ViewerPreferences = New PdfViewerPreferences()
			document.ViewerPreferences.CenterWindow = True
			document.ViewerPreferences.DisplayDocumentTitle = True
			document.ViewerPreferences.HideMenubar = True
			document.ViewerPreferences.HideToolbar = True
			document.ViewerPreferences.HideWindowUI = True
			document.ViewerPreferences.PrintScaling = PdfPrintScaling.None

			' Set the PDF version.
			document.PdfVersion = PdfVersion.Version15

			Dim output As SampleOutputInfo() = New SampleOutputInfo() {New SampleOutputInfo(document, "xfinium.pdf.sample.documentproperties.pdf")}
			Return output
		End Function
	End Class
End Namespace
