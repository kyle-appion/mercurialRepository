Imports System.IO
Imports Xfinium.Pdf
Imports Xfinium.Pdf.Graphics
Imports Xfinium.Pdf.Portfolios
Imports Xfinium.Pdf.Core.Cos

Namespace Xfinium.Pdf.Samples
	''' <summary>
	''' Portfolios sample.
	''' </summary>
	Public Class Portfolios
		''' <summary>
		''' Main method for running the sample.
		''' </summary>
		''' <param name="imageStream"></param>
		''' <param name="pdfStream"></param>
		''' <param name="csStream"></param>
		''' <param name="vbStream"></param>
		Public Shared Function Run(imageStream As Stream, pdfStream As Stream, csStream As Stream, vbStream As Stream) As SampleOutputInfo()
			Dim portfolio As New PdfPortfolio()

			' Build the structure that describes how to files and folders in the portfolio are presented to the user.
			Dim portfolioAttributeDefinitions As New PdfPortfolioAttributeDefinitions()
			Dim nameAttribute As New PdfPortfolioAttributeDefinition()
			nameAttribute.Name = "Name"
			nameAttribute.Type = PdfPortfolioAttributeDefinitionType.[String]
			portfolioAttributeDefinitions("name") = nameAttribute
			Dim typeAttribute As New PdfPortfolioAttributeDefinition()
			typeAttribute.Name = "Type"
			typeAttribute.Type = PdfPortfolioAttributeDefinitionType.[String]
			portfolioAttributeDefinitions("type") = typeAttribute

			portfolio.AttributeDefinitions = portfolioAttributeDefinitions

			' Setup the folders structure
			Dim root As New PdfPortfolioFolder()
			root.Name = "All files"
			root.PortfolioAttributes = New PdfPortfolioItemAttributes()
			root.PortfolioAttributes("name") = New PdfCosString("All files")

			Dim imagesFolder As New PdfPortfolioFolder()
			imagesFolder.Name = "Images"
			imagesFolder.PortfolioAttributes = New PdfPortfolioItemAttributes()
			imagesFolder.PortfolioAttributes("name") = New PdfCosString("Images (1)")
			root.Folders.Add(imagesFolder)

			Dim pdfFolder As New PdfPortfolioFolder()
			pdfFolder.Name = "PDFs"
			pdfFolder.PortfolioAttributes = New PdfPortfolioItemAttributes()
			pdfFolder.PortfolioAttributes("name") = New PdfCosString("PDFs (1)")
			root.Folders.Add(pdfFolder)

			Dim htmlFolder As New PdfPortfolioFolder()
			htmlFolder.Name = "HTML"
			htmlFolder.PortfolioAttributes = New PdfPortfolioItemAttributes()
			htmlFolder.PortfolioAttributes("name") = New PdfCosString("HTML (2)")
			root.Folders.Add(htmlFolder)

			portfolio.Folders.Add(root)

			' Setup the portfolio items
			Dim imageFile As New PdfPortfolioItem()
			imageFile.Folder = imagesFolder
			Dim data As Byte() = New Byte(imageStream.Length - 1) {}
			imageStream.Read(data, 0, data.Length)
			imageFile.Payload = data
			imageFile.FileName = "image.jpg"
			imageFile.Attributes = New PdfPortfolioItemAttributes()
			imageFile.Attributes("name") = New PdfCosString("image.jpg")
			imageFile.Attributes("type") = New PdfCosString("JPEG image")
			portfolio.Items.Add(imageFile)

			Dim pdfFile As New PdfPortfolioItem()
			pdfFile.Folder = pdfFolder
			data = New Byte(pdfStream.Length - 1) {}
			pdfStream.Read(data, 0, data.Length)
			pdfFile.Payload = data
			pdfFile.FileName = "content.pdf"
			pdfFile.Attributes = New PdfPortfolioItemAttributes()
			pdfFile.Attributes("name") = New PdfCosString("content.pdf")
			pdfFile.Attributes("type") = New PdfCosString("PDF file")
			portfolio.Items.Add(pdfFile)

			Dim csFile As New PdfPortfolioItem()
			csFile.Folder = htmlFolder
			data = New Byte(csStream.Length - 1) {}
			csStream.Read(data, 0, data.Length)
			csFile.Payload = data
			csFile.FileName = "portfolios.cs.html"
			csFile.Attributes = New PdfPortfolioItemAttributes()
			csFile.Attributes("name") = New PdfCosString("portfolios.cs.html")
			csFile.Attributes("type") = New PdfCosString("HTML file")
			portfolio.Items.Add(csFile)

			Dim vbFile As New PdfPortfolioItem()
			vbFile.Folder = htmlFolder
			data = New Byte(vbStream.Length - 1) {}
			vbStream.Read(data, 0, data.Length)
			vbFile.Payload = data
			vbFile.FileName = "portfolios.vb.html"
			vbFile.Attributes = New PdfPortfolioItemAttributes()
			vbFile.Attributes("name") = New PdfCosString("portfolios.vb.html")
			vbFile.Attributes("type") = New PdfCosString("HTML file")
			portfolio.Items.Add(vbFile)

			portfolio.StartupDocument = pdfFile

			Dim output As SampleOutputInfo() = New SampleOutputInfo() {New SampleOutputInfo(portfolio, "xfinium.pdf.sample.portfolios.pdf")}
			Return output
		End Function
	End Class
End Namespace
