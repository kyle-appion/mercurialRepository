Imports System.IO
Imports Xfinium.Pdf
Imports Xfinium.Pdf.Graphics

Namespace Xfinium.Pdf.Samples
	''' <summary>
	''' Fonts sample.
	''' </summary>
	Public Class Fonts
		''' <summary>
		''' Main method for running the sample.
		''' </summary>
		Public Shared Function Run(ttfStream As Stream) As SampleOutputInfo()
			Dim document As New PdfFixedDocument()

			Dim page As PdfPage = document.Pages.Add()
			DrawStandardFonts(page)

			page = document.Pages.Add()
			DrawStandardCjkFonts(page)

			page = document.Pages.Add()
			DrawTrueTypeFonts(page, ttfStream)

			Dim output As SampleOutputInfo() = New SampleOutputInfo() {New SampleOutputInfo(document, "xfinium.pdf.sample.fonts.pdf")}
			Return output
		End Function

		Private Shared Sub DrawStandardFonts(page As PdfPage)
			Dim titleFont As New PdfStandardFont(PdfStandardFontFace.HelveticaBold, 22)
			Dim blackBrush As New PdfBrush(New PdfRgbColor())

			page.Graphics.DrawString("Standard fonts", titleFont, blackBrush, 20, 50)
			page.Graphics.DrawString("(Base 14 PDF fonts supported by any PDF viewer)", titleFont, blackBrush, 20, 75)

			Dim helvetica As New PdfStandardFont(PdfStandardFontFace.Helvetica, 16)
			page.Graphics.DrawString("Helvetica - Lorem ipsum dolor sit amet, consectetur adipiscing elit.", helvetica, blackBrush, 20, 125)

			Dim helveticaBold As New PdfStandardFont(PdfStandardFontFace.HelveticaBold, 16)
			page.Graphics.DrawString("Helvetica Bold - Lorem ipsum dolor sit amet, consectetur adipiscing elit.", helveticaBold, blackBrush, 20, 150)

			Dim helveticaItalic As New PdfStandardFont(PdfStandardFontFace.HelveticaItalic, 16)
			page.Graphics.DrawString("Helvetica Italic - Lorem ipsum dolor sit amet, consectetur adipiscing elit.", helveticaItalic, blackBrush, 20, 175)

			Dim helveticaBoldItalic As New PdfStandardFont(PdfStandardFontFace.HelveticaBoldItalic, 16)
			page.Graphics.DrawString("Helvetica Bold Italic - Lorem ipsum dolor sit amet, consectetur adipiscing elit.", helveticaBoldItalic, blackBrush, 20, 200)

			Dim timesRoman As New PdfStandardFont(PdfStandardFontFace.TimesRoman, 16)
			page.Graphics.DrawString("Times Roman - Lorem ipsum dolor sit amet, consectetur adipiscing elit.", timesRoman, blackBrush, 20, 225)

			Dim timesRomanBold As New PdfStandardFont(PdfStandardFontFace.TimesRomanBold, 16)
			page.Graphics.DrawString("Times Roman Bold - Lorem ipsum dolor sit amet, consectetur adipiscing elit.", timesRomanBold, blackBrush, 20, 250)

			Dim timesRomanItalic As New PdfStandardFont(PdfStandardFontFace.TimesRomanItalic, 16)
			page.Graphics.DrawString("Times Roman Italic - Lorem ipsum dolor sit amet, consectetur adipiscing elit.", timesRomanItalic, blackBrush, 20, 275)

			Dim timesRomanBoldItalic As New PdfStandardFont(PdfStandardFontFace.TimesRomanBoldItalic, 16)
			page.Graphics.DrawString("Times Roman Bold Italic - Lorem ipsum dolor sit amet, consectetur adipiscing elit.", timesRomanBoldItalic, blackBrush, 20, 300)

			Dim courier As New PdfStandardFont(PdfStandardFontFace.Courier, 16)
			page.Graphics.DrawString("Courier - Lorem ipsum dolor sit amet, consectetur adipiscing elit.", courier, blackBrush, 20, 325)

			Dim courierBold As New PdfStandardFont(PdfStandardFontFace.CourierBold, 16)
			page.Graphics.DrawString("Courier Bold - Lorem ipsum dolor sit amet, consectetur adipiscing elit.", courierBold, blackBrush, 20, 350)

			Dim courierItalic As New PdfStandardFont(PdfStandardFontFace.CourierItalic, 16)
			page.Graphics.DrawString("Courier Italic - Lorem ipsum dolor sit amet, consectetur adipiscing elit.", courierItalic, blackBrush, 20, 375)

			Dim courierBoldItalic As New PdfStandardFont(PdfStandardFontFace.CourierBoldItalic, 16)
			page.Graphics.DrawString("Courier Bold Italic - Lorem ipsum dolor sit amet, consectetur adipiscing elit.", courierBoldItalic, blackBrush, 20, 400)
		End Sub

		Private Shared Sub DrawStandardCjkFonts(page As PdfPage)
			Dim titleFont As New PdfStandardFont(PdfStandardFontFace.HelveticaBold, 22)
			Dim blackBrush As New PdfBrush(New PdfRgbColor())

			page.Graphics.DrawString("Standard CJK fonts", titleFont, blackBrush, 20, 50)
			page.Graphics.DrawString("(CJK fonts supported by Adobe Reader", titleFont, blackBrush, 20, 75)
			page.Graphics.DrawString(" using CJK language packs)", titleFont, blackBrush, 20, 100)

			Dim heiseiKakuGothic As New PdfStandardFont(PdfStandardFontFace.HeiseiKakuGothicW5, 16)
			page.Graphics.DrawString("Heisei Kaku Gothic - サンプル日本語フォントデモテキスト.", heiseiKakuGothic, blackBrush, 20, 150)

			Dim heiseiKakuGothicBold As New PdfStandardFont(PdfStandardFontFace.HeiseiKakuGothicW5Bold, 16)
			page.Graphics.DrawString("Heisei Kaku Gothic Bold - サンプル日本語フォントデモテキスト.", heiseiKakuGothicBold, blackBrush, 20, 175)

			Dim heiseiKakuGothicItalic As New PdfStandardFont(PdfStandardFontFace.HeiseiKakuGothicW5Italic, 16)
			page.Graphics.DrawString("Heisei Kaku Gothic Italic - サンプル日本語フォントデモテキスト.", heiseiKakuGothicItalic, blackBrush, 20, 200)

			Dim heiseiKakuGothicBoldItalic As New PdfStandardFont(PdfStandardFontFace.HeiseiKakuGothicW5BoldItalic, 16)
			page.Graphics.DrawString("Heisei Kaku Gothic Bold Italic - サンプル日本語フォントデモテキスト.", heiseiKakuGothicBoldItalic, blackBrush, 20, 225)

			Dim heiseiMincho As New PdfStandardFont(PdfStandardFontFace.HeiseiMinchoW3, 16)
			page.Graphics.DrawString("Heisei Mincho - サンプル日本語フォントデモテキスト.", heiseiMincho, blackBrush, 20, 250)

			Dim heiseiMinchoBold As New PdfStandardFont(PdfStandardFontFace.HeiseiMinchoW3Bold, 16)
			page.Graphics.DrawString("Heisei Mincho Bold - サンプル日本語フォントデモテキスト.", heiseiMinchoBold, blackBrush, 20, 275)

			Dim heiseiMinchoItalic As New PdfStandardFont(PdfStandardFontFace.HeiseiMinchoW3Italic, 16)
			page.Graphics.DrawString("Heisei Mincho Italic - サンプル日本語フォントデモテキスト.", heiseiMinchoItalic, blackBrush, 20, 300)

			Dim heiseiMinchoBoldItalic As New PdfStandardFont(PdfStandardFontFace.HeiseiMinchoW3BoldItalic, 16)
			page.Graphics.DrawString("Heisei Mincho Bold Italic - サンプル日本語フォントデモテキスト.", heiseiMinchoBoldItalic, blackBrush, 20, 325)

			Dim hanyangSystemsGothicMedium As New PdfStandardFont(PdfStandardFontFace.HanyangSystemsGothicMedium, 16)
			page.Graphics.DrawString("Hanyang Systems Gothic Medium - 샘플 한국어 글꼴 데모 텍스트.", hanyangSystemsGothicMedium, blackBrush, 20, 350)

			Dim hanyangSystemsGothicMediumBold As New PdfStandardFont(PdfStandardFontFace.HanyangSystemsGothicMediumBold, 16)
			page.Graphics.DrawString("Hanyang Systems Gothic Medium Bold - 샘플 한국어 글꼴 데모 텍스트.", hanyangSystemsGothicMediumBold, blackBrush, 20, 375)

			Dim hanyangSystemsGothicMediumItalic As New PdfStandardFont(PdfStandardFontFace.HanyangSystemsGothicMediumItalic, 16)
			page.Graphics.DrawString("Hanyang Systems Gothic Medium Italic - 샘플 한국어 글꼴 데모 텍스트.", hanyangSystemsGothicMediumItalic, blackBrush, 20, 400)

			Dim hanyangSystemsGothicMediumBoldItalic As New PdfStandardFont(PdfStandardFontFace.HanyangSystemsGothicMediumBoldItalic, 16)
			page.Graphics.DrawString("Hanyang Systems Gothic Medium Bold Italic - 샘플 한국어 글꼴 데모 텍스트.", hanyangSystemsGothicMediumBoldItalic, blackBrush, 20, 425)

			Dim hanyangSystemsShinMyeongJoMedium As New PdfStandardFont(PdfStandardFontFace.HanyangSystemsShinMyeongJoMedium, 16)
			page.Graphics.DrawString("Hanyang Systems Shin Myeong Jo Medium - 샘플 한국어 글꼴 데모 텍스트.", hanyangSystemsShinMyeongJoMedium, blackBrush, 20, 450)

			Dim hanyangSystemsShinMyeongJoMediumBold As New PdfStandardFont(PdfStandardFontFace.HanyangSystemsShinMyeongJoMediumBold, 16)
			page.Graphics.DrawString("Hanyang Systems Shin Myeong Jo Medium Bold - 샘플 한국어 글꼴 데모 텍스트.", hanyangSystemsShinMyeongJoMediumBold, blackBrush, 20, 475)

			Dim hanyangSystemsShinMyeongJoMediumItalic As New PdfStandardFont(PdfStandardFontFace.HanyangSystemsShinMyeongJoMediumItalic, 16)
			page.Graphics.DrawString("Hanyang Systems Shin Myeong Jo Medium Italic - 샘플 한국어 글꼴 데모 텍스트.", hanyangSystemsShinMyeongJoMediumItalic, blackBrush, 20, 500)

			Dim hanyangSystemsShinMyeongJoMediumBoldItalic As New PdfStandardFont(PdfStandardFontFace.HanyangSystemsShinMyeongJoMediumBoldItalic, 16)
			page.Graphics.DrawString("Hanyang Systems Shin Myeong Jo Medium Bold Italic - 샘플 한국어 글꼴 데모 텍스트.", hanyangSystemsShinMyeongJoMediumBoldItalic, blackBrush, 20, 525)

			Dim monotypeSungLight As New PdfStandardFont(PdfStandardFontFace.MonotypeSungLight, 16)
			page.Graphics.DrawString("Monotype Sung Light - 中國字體樣本示範文本.", monotypeSungLight, blackBrush, 20, 550)

			Dim monotypeSungLightBold As New PdfStandardFont(PdfStandardFontFace.MonotypeSungLightBold, 16)
			page.Graphics.DrawString("Monotype Sung Light Bold - 中國字體樣本示範文本.", monotypeSungLightBold, blackBrush, 20, 575)

			Dim monotypeSungLightItalic As New PdfStandardFont(PdfStandardFontFace.MonotypeSungLightItalic, 16)
			page.Graphics.DrawString("Monotype Sung Light Italic - 中國字體樣本示範文本.", monotypeSungLightItalic, blackBrush, 20, 600)

			Dim monotypeSungLightBoldItalic As New PdfStandardFont(PdfStandardFontFace.MonotypeSungLightBoldItalic, 16)
			page.Graphics.DrawString("Monotype Sung Light Bold Italic - 中國字體樣本示範文本.", monotypeSungLightBoldItalic, blackBrush, 20, 625)

			Dim sinoTypeSongLight As New PdfStandardFont(PdfStandardFontFace.SinoTypeSongLight, 16)
			page.Graphics.DrawString("Sino Type Song Light - 中国字体样本示范文本.", sinoTypeSongLight, blackBrush, 20, 650)

			Dim sinoTypeSongLightBold As New PdfStandardFont(PdfStandardFontFace.SinoTypeSongLightBold, 16)
			page.Graphics.DrawString("Sino Type Song Light Bold - 中国字体样本示范文本.", sinoTypeSongLightBold, blackBrush, 20, 675)

			Dim sinoTypeSongLightItalic As New PdfStandardFont(PdfStandardFontFace.SinoTypeSongLightItalic, 16)
			page.Graphics.DrawString("Sino Type Song Light Italic - 中国字体样本示范文本.", sinoTypeSongLightItalic, blackBrush, 20, 700)

			Dim sinoTypeSongLightBoldItalic As New PdfStandardFont(PdfStandardFontFace.SinoTypeSongLightBoldItalic, 16)
			page.Graphics.DrawString("Sino Type Song Light Bold Italic - 中国字体样本示范文本.", sinoTypeSongLightBoldItalic, blackBrush, 20, 725)
		End Sub

		Private Shared Sub DrawTrueTypeFonts(page As PdfPage, ttfStream As Stream)
			Dim titleFont As New PdfStandardFont(PdfStandardFontFace.HelveticaBold, 22)
			Dim blackBrush As New PdfBrush(New PdfRgbColor())

			page.Graphics.DrawString("TrueType fonts", titleFont, blackBrush, 20, 50)
			page.Graphics.DrawString("(when embedded they should be supported", titleFont, blackBrush, 20, 75)
			page.Graphics.DrawString(" by any PDF viewer)", titleFont, blackBrush, 20, 100)

			Dim ansiVerdana As New PdfAnsiTrueTypeFont(ttfStream, 16, True)
			page.Graphics.DrawString("Verdana - Ansi TrueType font", ansiVerdana, blackBrush, 20, 150)
			page.Graphics.DrawString("Lorem ipsum dolor sit amet, consectetur adipiscing elit.", ansiVerdana, blackBrush, 20, 175)

			ttfStream.Position = 0
			Dim unicodeVerdana As New PdfUnicodeTrueTypeFont(ttfStream, 16, True)
			page.Graphics.DrawString("Verdana - Unicode TrueType font", unicodeVerdana, blackBrush, 20, 225)

			page.Graphics.DrawString("Russian - Пример русский текст демо шрифт.", unicodeVerdana, blackBrush, 20, 250)
			page.Graphics.DrawString("Greek - Δείγμα ελληνικό κείμενο demo γραμματοσειράς.", unicodeVerdana, blackBrush, 20, 275)
		End Sub
	End Class
End Namespace
