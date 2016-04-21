Imports System.IO
Imports Xfinium.Pdf
Imports Xfinium.Pdf.Graphics
Imports Xfinium.Pdf.Forms

Namespace Xfinium.Pdf.Samples
	''' <summary>
	''' FormFill sample.
	''' </summary>
	Public Class FormFill
		''' <summary>
		''' Main method for running the sample.
		''' </summary>
		''' <param name="stream"></param>
		Public Shared Function Run(stream As Stream) As SampleOutputInfo()
			Dim document As New PdfFixedDocument(stream)
			TryCast(document.Form.Fields("firstname"), PdfTextBoxField).Text = "John"
			TryCast(document.Form.Fields("lastname"), PdfTextBoxField).Value = "Doe"

			TryCast(document.Form.Fields("sex").Widgets(0), PdfRadioButtonWidget).Checked = True

			TryCast(document.Form.Fields("firstcar"), PdfComboBoxField).SelectedIndex = 0

			TryCast(document.Form.Fields("secondcar"), PdfListBoxField).SelectedIndex = 1

			TryCast(document.Form.Fields("agree"), PdfCheckBoxField).Checked = True

			Dim output As SampleOutputInfo() = New SampleOutputInfo() {New SampleOutputInfo(document, "xfinium.pdf.sample.formfill.pdf")}
			Return output
		End Function
	End Class
End Namespace
