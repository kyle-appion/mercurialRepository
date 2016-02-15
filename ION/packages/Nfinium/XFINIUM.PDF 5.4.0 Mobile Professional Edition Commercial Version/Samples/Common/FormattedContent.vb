
Imports System.IO
Imports Xfinium.Pdf
Imports Xfinium.Pdf.Graphics
Imports Xfinium.Pdf.Actions
Imports Xfinium.Pdf.Graphics.FormattedContent

Namespace Xfinium.Pdf.Samples
	''' <summary>
	''' FormattedContent sample.
	''' </summary>
	Public Class FormattedContent
		''' <summary>
		''' Main method for running the sample.
		''' </summary>
		Public Shared Function Run() As SampleOutputInfo()
			Dim xfiniumPdfText As String = "XFINIUM.PDF"
			Dim paragraph1Block2Text As String = " library is a .NET/Xamarin library for cross-platform PDF development. Code written for "
			Dim paragraph1Block4Text As String = " can be compiled on all supported platforms without changes. The library features a " + "wide range of capabilities, for both beginers and advanced PDF developers."
			Dim paragraph2Block1Text As String = "The development style is based on fixed document model, where each page is created as needed " + "and content is placed at fixed position using a grid based layout." & vbCr & vbLf + "This gives you access to all PDF features, whether you want to create a simple file " + "or you want to create a transparency knockout group at COS level, and lets you build more complex models on top of the library."
			Dim paragraph3Block2Text As String = " has been developed entirely in C# and it is 100% managed code."
			Dim paragraph4Block1Text As String = "With "
			Dim paragraph4Block3Text As String = " you can port your PDF application logic to other platforms with zero effort which means faster time to market."
			Dim paragraph5Block1Text As String = "Simple licensing per developer with royalty free distribution helps you keep your costs under control."
			Dim paragraph6Block1Text As String = "SUPPORTED PLATFORMS"
			Dim paragraph7Block1Text As String = ".NET 2.0 to .NET 4.5"
			Dim paragraph8Block1Text As String = "Windows Forms"
			Dim paragraph9Block1Text As String = "ASP.NET Webforms and MVC"
			Dim paragraph10Block1Text As String = "Console applications"
			Dim paragraph11Block1Text As String = "Windows services"
			Dim paragraph12Block1Text As String = "Mono"
			Dim paragraph13Block1Text As String = "WPF 4.0 & 4.5"
			Dim paragraph14Block1Text As String = "Silverlight 4 & 5"
			Dim paragraph15Block1Text As String = "WinRT (Windows Store applications)"
			Dim paragraph16Block1Text As String = "Windows Phone 7 & 8"
			Dim paragraph17Block1Text As String = "Xamarin.iOS"
			Dim paragraph18Block1Text As String = "Xamarin.Android"

			Dim textFont As New PdfStandardFont(PdfStandardFontFace.Helvetica, 10)
			Dim xfiniumPdfLinkBlock As New PdfFormattedTextBlock(xfiniumPdfText)
			xfiniumPdfLinkBlock.Font = New PdfStandardFont(PdfStandardFontFace.HelveticaBold, 10)
			xfiniumPdfLinkBlock.Font.Underline = True
			xfiniumPdfLinkBlock.TextColor = New PdfBrush(PdfRgbColor.Blue)
			xfiniumPdfLinkBlock.Action = New PdfUriAction("http://xfiniumpdf.com/")

			Dim paragraph1 As New PdfFormattedParagraph()
			paragraph1.LineSpacingMode = PdfFormattedParagraphLineSpacing.Multiple
			paragraph1.LineSpacing = 1.2
			paragraph1.SpacingAfter = 3
			paragraph1.FirstLineIndent = 10
			paragraph1.HorizontalAlign = PdfStringHorizontalAlign.Justified
			paragraph1.Blocks.Add(xfiniumPdfLinkBlock)
			paragraph1.Blocks.Add(New PdfFormattedTextBlock(paragraph1Block2Text, textFont))
			paragraph1.Blocks.Add(xfiniumPdfLinkBlock)
			paragraph1.Blocks.Add(New PdfFormattedTextBlock(paragraph1Block4Text, textFont))

			Dim paragraph2 As New PdfFormattedParagraph()
			paragraph2.SpacingAfter = 3
			paragraph2.FirstLineIndent = 10
			paragraph2.HorizontalAlign = PdfStringHorizontalAlign.Justified
			paragraph2.LineSpacingMode = PdfFormattedParagraphLineSpacing.Multiple
			paragraph2.LineSpacing = 1.2
			paragraph2.Blocks.Add(New PdfFormattedTextBlock(paragraph2Block1Text, textFont))

			Dim paragraph3 As New PdfFormattedParagraph()
			paragraph3.LineSpacingMode = PdfFormattedParagraphLineSpacing.Multiple
			paragraph3.LineSpacing = 1.2
			paragraph3.SpacingAfter = 3
			paragraph3.FirstLineIndent = 10
			paragraph3.HorizontalAlign = PdfStringHorizontalAlign.Justified
			paragraph3.Blocks.Add(xfiniumPdfLinkBlock)
			paragraph3.Blocks.Add(New PdfFormattedTextBlock(paragraph3Block2Text, textFont))

			Dim paragraph4 As New PdfFormattedParagraph()
			paragraph4.LineSpacingMode = PdfFormattedParagraphLineSpacing.Multiple
			paragraph4.LineSpacing = 1.2
			paragraph4.SpacingAfter = 3
			paragraph4.FirstLineIndent = 10
			paragraph4.HorizontalAlign = PdfStringHorizontalAlign.Justified
			paragraph4.Blocks.Add(New PdfFormattedTextBlock(paragraph4Block1Text, textFont))
			paragraph4.Blocks.Add(xfiniumPdfLinkBlock)
			paragraph4.Blocks.Add(New PdfFormattedTextBlock(paragraph4Block3Text, textFont))

			Dim paragraph5 As New PdfFormattedParagraph()
			paragraph5.FirstLineIndent = 10
			paragraph5.HorizontalAlign = PdfStringHorizontalAlign.Justified
			paragraph5.Blocks.Add(New PdfFormattedTextBlock(paragraph5Block1Text, textFont))

			Dim paragraph6 As New PdfFormattedParagraph()
			paragraph6.SpacingBefore = 10
			paragraph6.Blocks.Add(New PdfFormattedTextBlock(paragraph6Block1Text, textFont))

			Dim bulletBlock As New PdfFormattedTextBlock("v ", New PdfStandardFont(PdfStandardFontFace.ZapfDingbats, 10))

			Dim paragraph7 As New PdfFormattedParagraph()
			paragraph7.SpacingBefore = 3
			paragraph7.Bullet = bulletBlock
			paragraph7.LeftIndentation = 10
			paragraph7.Blocks.Add(New PdfFormattedTextBlock(paragraph7Block1Text, textFont))

			Dim paragraph8 As New PdfFormattedParagraph()
			paragraph8.SpacingBefore = 3
			paragraph8.Bullet = bulletBlock
			paragraph8.LeftIndentation = 10
			paragraph8.Blocks.Add(New PdfFormattedTextBlock(paragraph8Block1Text, textFont))

			Dim paragraph9 As New PdfFormattedParagraph()
			paragraph9.SpacingBefore = 3
			paragraph9.Bullet = bulletBlock
			paragraph9.LeftIndentation = 10
			paragraph9.Blocks.Add(New PdfFormattedTextBlock(paragraph9Block1Text, textFont))

			Dim paragraph10 As New PdfFormattedParagraph()
			paragraph10.SpacingBefore = 3
			paragraph10.Bullet = bulletBlock
			paragraph10.LeftIndentation = 10
			paragraph10.Blocks.Add(New PdfFormattedTextBlock(paragraph10Block1Text, textFont))

			Dim paragraph11 As New PdfFormattedParagraph()
			paragraph11.SpacingBefore = 3
			paragraph11.Bullet = bulletBlock
			paragraph11.LeftIndentation = 10
			paragraph11.Blocks.Add(New PdfFormattedTextBlock(paragraph11Block1Text, textFont))

			Dim paragraph12 As New PdfFormattedParagraph()
			paragraph12.SpacingBefore = 3
			paragraph12.Bullet = bulletBlock
			paragraph12.LeftIndentation = 10
			paragraph12.Blocks.Add(New PdfFormattedTextBlock(paragraph12Block1Text, textFont))

			Dim paragraph13 As New PdfFormattedParagraph()
			paragraph13.SpacingBefore = 3
			paragraph13.Bullet = bulletBlock
			paragraph13.LeftIndentation = 10
			paragraph13.Blocks.Add(New PdfFormattedTextBlock(paragraph13Block1Text, textFont))

			Dim paragraph14 As New PdfFormattedParagraph()
			paragraph14.SpacingBefore = 3
			paragraph14.Bullet = bulletBlock
			paragraph14.LeftIndentation = 10
			paragraph14.Blocks.Add(New PdfFormattedTextBlock(paragraph14Block1Text, textFont))

			Dim paragraph15 As New PdfFormattedParagraph()
			paragraph15.SpacingBefore = 3
			paragraph15.Bullet = bulletBlock
			paragraph15.LeftIndentation = 10
			paragraph15.Blocks.Add(New PdfFormattedTextBlock(paragraph15Block1Text, textFont))

			Dim paragraph16 As New PdfFormattedParagraph()
			paragraph16.SpacingBefore = 3
			paragraph16.Bullet = bulletBlock
			paragraph16.LeftIndentation = 10
			paragraph16.Blocks.Add(New PdfFormattedTextBlock(paragraph16Block1Text, textFont))

			Dim paragraph17 As New PdfFormattedParagraph()
			paragraph17.SpacingBefore = 3
			paragraph17.Bullet = bulletBlock
			paragraph17.LeftIndentation = 10
			paragraph17.Blocks.Add(New PdfFormattedTextBlock(paragraph17Block1Text, textFont))

			Dim paragraph18 As New PdfFormattedParagraph()
			paragraph18.SpacingBefore = 3
			paragraph18.Bullet = bulletBlock
			paragraph18.LeftIndentation = 10
			paragraph18.Blocks.Add(New PdfFormattedTextBlock(paragraph18Block1Text, textFont))

			Dim formattedContent As New PdfFormattedContent()
			formattedContent.Paragraphs.Add(paragraph1)
			formattedContent.Paragraphs.Add(paragraph2)
			formattedContent.Paragraphs.Add(paragraph3)
			formattedContent.Paragraphs.Add(paragraph4)
			formattedContent.Paragraphs.Add(paragraph5)
			formattedContent.Paragraphs.Add(paragraph6)
			formattedContent.Paragraphs.Add(paragraph7)
			formattedContent.Paragraphs.Add(paragraph8)
			formattedContent.Paragraphs.Add(paragraph9)
			formattedContent.Paragraphs.Add(paragraph10)
			formattedContent.Paragraphs.Add(paragraph11)
			formattedContent.Paragraphs.Add(paragraph12)
			formattedContent.Paragraphs.Add(paragraph13)
			formattedContent.Paragraphs.Add(paragraph14)
			formattedContent.Paragraphs.Add(paragraph15)
			formattedContent.Paragraphs.Add(paragraph16)
			formattedContent.Paragraphs.Add(paragraph17)
			formattedContent.Paragraphs.Add(paragraph18)

			Dim document As New PdfFixedDocument()
			Dim page As PdfPage = document.Pages.Add()

			page.Graphics.DrawFormattedContent(formattedContent, 50, 50, 500, 700)
			page.Graphics.CompressAndClose()

			Dim output As SampleOutputInfo() = New SampleOutputInfo() {New SampleOutputInfo(document, "xfinium.pdf.sample.formattedcontent.pdf")}
			Return output
		End Function
	End Class
End Namespace
