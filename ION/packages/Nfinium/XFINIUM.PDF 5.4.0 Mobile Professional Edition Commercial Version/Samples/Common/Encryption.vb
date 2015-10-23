Imports System.IO
Imports Xfinium.Pdf
Imports Xfinium.Pdf.Graphics
Imports Xfinium.Pdf.Content
Imports Xfinium.Pdf.Core.Security

Namespace Xfinium.Pdf.Samples
	''' <summary>
	''' Encryption sample.
	''' </summary>
	Public Class Encryption
		''' <summary>
		''' Main method for running the sample.
		''' </summary>
		Public Shared Function Run(input As Stream) As SampleOutputInfo()
			Dim document1 As PdfFixedDocument = EncryptRC4(40)
			Dim document2 As PdfFixedDocument = EncryptRC4(128)
			Dim document3 As PdfFixedDocument = EncryptAES(128)
			Dim document4 As PdfFixedDocument = EncryptAES(256)
			Dim document5 As PdfFixedDocument = Decrypt(input)

			Dim output As SampleOutputInfo() = New SampleOutputInfo() {New SampleOutputInfo(document1, "xfinium.pdf.sample.encryption.rc4.40bit.pdf"), New SampleOutputInfo(document2, "xfinium.pdf.sample.encryption.rc4.128bit.pdf"), New SampleOutputInfo(document3, "xfinium.pdf.sample.encryption.aes.128bit.pdf"), New SampleOutputInfo(document4, "xfinium.pdf.sample.encryption.aes.256bit.pdf"), New SampleOutputInfo(document5, "xfinium.pdf.sample.encryption.decrypted.pdf")}
			Return output
		End Function

		''' <summary>
		''' Generates a PDF document that is encrypted using RC4 method.
		''' </summary>
		''' <param name="keySize">The encryption key size.</param>
		''' <returns></returns>
		Private Shared Function EncryptRC4(keySize As Integer) As PdfFixedDocument
			Dim doc As New PdfFixedDocument()
			Dim rc4 As New PdfRc4SecurityHandler()
			rc4.EnableContentExtractionForAccessibility = False
			rc4.EnableDocumentAssembly = False
			rc4.EnableDocumentChange = False
			rc4.EnableContentExtraction = False
			rc4.EnableFormsFill = False
			rc4.EnableAnnotationsAndFieldsEdit = False
			rc4.EnablePrint = False
			rc4.EncryptMetadata = True
			rc4.KeySize = keySize
			rc4.UserPassword = "userpass"
			rc4.OwnerPassword = "ownerpass"
			doc.SecurityHandler = rc4

			Dim page As PdfPage = doc.Pages.Add()
			Dim helvetica As New PdfStandardFont(PdfStandardFontFace.HelveticaBoldItalic, 16)
			Dim blackBrush As New PdfBrush()
			page.Graphics.DrawString(String.Format("Encryption: RC4 {0} bit", keySize), helvetica, blackBrush, 50, 100)

			Return doc
		End Function

		''' <summary>
		''' Generates a PDF document that is encrypted using AES method.
		''' </summary>
		''' <param name="keySize">The encryption key size.</param>
		''' <returns></returns>
		Private Shared Function EncryptAES(keySize As Integer) As PdfFixedDocument
			Dim doc As New PdfFixedDocument()
			Dim aes As New PdfAesSecurityHandler()
			aes.EnableContentExtractionForAccessibility = False
			aes.EnableDocumentAssembly = False
			aes.EnableDocumentChange = False
			aes.EnableContentExtraction = False
			aes.EnableFormsFill = False
			aes.EnableAnnotationsAndFieldsEdit = False
			aes.EnablePrint = False
			aes.EncryptMetadata = True
			aes.KeySize = keySize
			aes.UserPassword = "userpass"
			aes.OwnerPassword = "ownerpass"
			doc.SecurityHandler = aes

			Dim page As PdfPage = doc.Pages.Add()
			Dim helvetica As New PdfStandardFont(PdfStandardFontFace.HelveticaBoldItalic, 16)
			Dim blackBrush As New PdfBrush()
			page.Graphics.DrawString(String.Format("Encryption: AES {0} bit", keySize), helvetica, blackBrush, 50, 100)

			Return doc
		End Function

		''' <summary>
		''' Decrypts a PDF file
		''' </summary>
		''' <param name="input">Input stream.</param>
		''' <returns></returns>
		Private Shared Function Decrypt(input As Stream) As PdfFixedDocument
			Dim doc As New PdfFixedDocument(input, "xfiniumopen")

			Dim page As PdfPage = doc.Pages(0)
			Dim helvetica As New PdfStandardFont(PdfStandardFontFace.HelveticaBoldItalic, 16)
			Dim blackBrush As New PdfBrush()
			page.Graphics.DrawString("Decrypted document", helvetica, blackBrush, 5, 5)

			Return doc
		End Function
	End Class
End Namespace
