Imports System.IO
Imports Xfinium.Pdf
Imports Xfinium.Pdf.Graphics
Imports Xfinium.Pdf.Forms
Imports Xfinium.Pdf.Actions

Namespace Xfinium.Pdf.Samples
	''' <summary>
	''' FormGenerator sample.
	''' </summary>
	Public Class FormGenerator
		''' <summary>
		''' Main method for running the sample.
		''' </summary>
		Public Shared Function Run() As SampleOutputInfo()
			Dim document As New PdfFixedDocument()
			Dim helvetica As New PdfStandardFont(PdfStandardFontFace.Helvetica, 12)
			Dim brush As New PdfBrush()

			Dim page As PdfPage = document.Pages.Add()

			' First name
			page.Graphics.DrawString("First name:", helvetica, brush, 50, 50)
			Dim firstNameTextBox As New PdfTextBoxField("firstname")
			page.Fields.Add(firstNameTextBox)
			firstNameTextBox.Widgets(0).Font = helvetica
			firstNameTextBox.Widgets(0).VisualRectangle = New PdfVisualRectangle(150, 45, 200, 20)
			firstNameTextBox.Widgets(0).BorderColor = PdfRgbColor.Black
			firstNameTextBox.Widgets(0).BorderWidth = 1

			' Last name
			page.Graphics.DrawString("Last name:", helvetica, brush, 50, 80)
			Dim lastNameTextBox As New PdfTextBoxField("lastname")
			page.Fields.Add(lastNameTextBox)
			lastNameTextBox.Widgets(0).Font = helvetica
			lastNameTextBox.Widgets(0).VisualRectangle = New PdfVisualRectangle(150, 75, 200, 20)
			lastNameTextBox.Widgets(0).BorderColor = PdfRgbColor.Black
			lastNameTextBox.Widgets(0).BorderWidth = 1

			' Sex
			page.Graphics.DrawString("Sex:", helvetica, brush, 50, 110)
			Dim sexRadioButton As New PdfRadioButtonField("sex")
			Dim maleRadioItem As New PdfRadioButtonWidget()
			sexRadioButton.Widgets.Add(maleRadioItem)
			Dim femaleRadioItem As New PdfRadioButtonWidget()
			sexRadioButton.Widgets.Add(femaleRadioItem)
			page.Fields.Add(sexRadioButton)

			page.Graphics.DrawString("Male", helvetica, brush, 180, 110)
			maleRadioItem.ExportValue = "M"
			maleRadioItem.CheckStyle = PdfCheckStyle.Circle
			maleRadioItem.VisualRectangle = New PdfVisualRectangle(150, 105, 20, 20)
			maleRadioItem.BorderColor = PdfRgbColor.Black
			maleRadioItem.BorderWidth = 1

			page.Graphics.DrawString("Female", helvetica, brush, 280, 110)
			femaleRadioItem.ExportValue = "F"
			femaleRadioItem.CheckStyle = PdfCheckStyle.Circle
			femaleRadioItem.VisualRectangle = New PdfVisualRectangle(250, 105, 20, 20)
			femaleRadioItem.BorderColor = PdfRgbColor.Black
			femaleRadioItem.BorderWidth = 1

			' First car
			page.Graphics.DrawString("First car:", helvetica, brush, 50, 140)
			Dim firstCarList As New PdfComboBoxField("firstcar")
			firstCarList.Items.Add(New PdfListItem("Mercedes", "Mercedes"))
			firstCarList.Items.Add(New PdfListItem("BMW", "BMW"))
			firstCarList.Items.Add(New PdfListItem("Audi", "Audi"))
			firstCarList.Items.Add(New PdfListItem("Volkswagen", "Volkswagen"))
			firstCarList.Items.Add(New PdfListItem("Porsche", "Porsche"))
			firstCarList.Items.Add(New PdfListItem("Honda", "Honda"))
			firstCarList.Items.Add(New PdfListItem("Toyota", "Toyota"))
			firstCarList.Items.Add(New PdfListItem("Lexus", "Lexus"))
			firstCarList.Items.Add(New PdfListItem("Infiniti", "Infiniti"))
			firstCarList.Items.Add(New PdfListItem("Acura", "Acura"))
			page.Fields.Add(firstCarList)
			firstCarList.Widgets(0).Font = helvetica
			firstCarList.Widgets(0).VisualRectangle = New PdfVisualRectangle(150, 135, 200, 20)
			firstCarList.Widgets(0).BorderColor = PdfRgbColor.Black
			firstCarList.Widgets(0).BorderWidth = 1

			' Second car
			page.Graphics.DrawString("Second car:", helvetica, brush, 50, 170)
			Dim secondCarList As New PdfListBoxField("secondcar")
			secondCarList.Items.Add(New PdfListItem("Mercedes", "Mercedes"))
			secondCarList.Items.Add(New PdfListItem("BMW", "BMW"))
			secondCarList.Items.Add(New PdfListItem("Audi", "Audi"))
			secondCarList.Items.Add(New PdfListItem("Volkswagen", "Volkswagen"))
			secondCarList.Items.Add(New PdfListItem("Porsche", "Porsche"))
			secondCarList.Items.Add(New PdfListItem("Honda", "Honda"))
			secondCarList.Items.Add(New PdfListItem("Toyota", "Toyota"))
			secondCarList.Items.Add(New PdfListItem("Lexus", "Lexus"))
			secondCarList.Items.Add(New PdfListItem("Infiniti", "Infiniti"))
			secondCarList.Items.Add(New PdfListItem("Acura", "Acura"))
			page.Fields.Add(secondCarList)
			secondCarList.Widgets(0).Font = helvetica
			secondCarList.Widgets(0).VisualRectangle = New PdfVisualRectangle(150, 165, 200, 60)
			secondCarList.Widgets(0).BorderColor = PdfRgbColor.Black
			secondCarList.Widgets(0).BorderWidth = 1

			' I agree
			page.Graphics.DrawString("I agree:", helvetica, brush, 50, 240)
			Dim agreeCheckBox As New PdfCheckBoxField("agree")
			page.Fields.Add(agreeCheckBox)
			agreeCheckBox.Widgets(0).Font = helvetica
			TryCast(agreeCheckBox.Widgets(0), PdfCheckWidget).ExportValue = "YES"
			TryCast(agreeCheckBox.Widgets(0), PdfCheckWidget).CheckStyle = PdfCheckStyle.Check
			agreeCheckBox.Widgets(0).VisualRectangle = New PdfVisualRectangle(150, 235, 20, 20)
			agreeCheckBox.Widgets(0).BorderColor = PdfRgbColor.Black
			agreeCheckBox.Widgets(0).BorderWidth = 1

			' Sign here
			page.Graphics.DrawString("Sign here:", helvetica, brush, 50, 270)
			Dim signHereField As New PdfSignatureField("signhere")
			page.Fields.Add(signHereField)
			signHereField.Widgets(0).Font = helvetica
			signHereField.Widgets(0).VisualRectangle = New PdfVisualRectangle(150, 265, 200, 60)

			' Submit form
			Dim submitBtn As New PdfPushButtonField("submit")
			page.Fields.Add(submitBtn)
			submitBtn.Widgets(0).VisualRectangle = New PdfVisualRectangle(450, 45, 150, 30)
			TryCast(submitBtn.Widgets(0), PdfPushButtonWidget).Caption = "Submit form"
			submitBtn.Widgets(0).BackgroundColor = PdfRgbColor.LightGray
			Dim submitFormAction As New PdfSubmitFormAction()
			submitFormAction.DataFormat = PdfSubmitDataFormat.FDF
			submitFormAction.Fields.Add("firstname")
			submitFormAction.Fields.Add("lastname")
			submitFormAction.Fields.Add("sex")
			submitFormAction.Fields.Add("firstcar")
			submitFormAction.Fields.Add("secondcar")
			submitFormAction.Fields.Add("agree")
			submitFormAction.Fields.Add("signhere")
			submitFormAction.SubmitFields = True
			submitFormAction.Url = "http://www.xfiniumsoft.com/"
			submitBtn.Widgets(0).MouseUp = submitFormAction

			' Reset form
			Dim resetBtn As New PdfPushButtonField("reset")
			page.Fields.Add(resetBtn)
			resetBtn.Widgets(0).VisualRectangle = New PdfVisualRectangle(450, 85, 150, 30)
			TryCast(resetBtn.Widgets(0), PdfPushButtonWidget).Caption = "Reset form"
			resetBtn.Widgets(0).BackgroundColor = PdfRgbColor.LightGray
			Dim resetFormAction As New PdfResetFormAction()
			resetBtn.Widgets(0).MouseUp = resetFormAction

			' Print form
			Dim printBtn As New PdfPushButtonField("print")
			page.Fields.Add(printBtn)
			printBtn.Widgets(0).VisualRectangle = New PdfVisualRectangle(450, 125, 150, 30)
			TryCast(printBtn.Widgets(0), PdfPushButtonWidget).Caption = "Print form"
			printBtn.Widgets(0).BackgroundColor = PdfRgbColor.LightGray
			Dim printAction As New PdfJavaScriptAction()
			printAction.Script = "this.print(true);" & vbLf
			printBtn.Widgets(0).MouseUp = printAction

			Dim output As SampleOutputInfo() = New SampleOutputInfo() {New SampleOutputInfo(document, "xfinium.pdf.sample.formgenerator.pdf")}
			Return output
		End Function
	End Class
End Namespace
