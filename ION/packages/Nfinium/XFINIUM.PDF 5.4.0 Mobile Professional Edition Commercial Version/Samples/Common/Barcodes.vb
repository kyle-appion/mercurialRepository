Imports System.IO
Imports Xfinium.Pdf
Imports Xfinium.Pdf.Graphics

Namespace Xfinium.Pdf.Samples
	''' <summary>
	''' Barcodes sample.
	''' </summary>
	Public Class Barcodes
		''' <summary>
		''' Main method for running the sample.
		''' </summary>
		Public Shared Function Run() As SampleOutputInfo()
			Dim document As New PdfFixedDocument()
			Dim titleFont As New PdfStandardFont(PdfStandardFontFace.HelveticaBold, 16)
			Dim barcodeFont As New PdfStandardFont(PdfStandardFontFace.Helvetica, 12)

			Dim page As PdfPage = document.Pages.Add()
			DrawGenericBarcodes(page, titleFont, barcodeFont)

			page = document.Pages.Add()
			DrawPharmaBarcodes(page, titleFont, barcodeFont)

			page = document.Pages.Add()
			DrawEanUpcBarcodes(page, titleFont, barcodeFont)

			page = document.Pages.Add()
			DrawPostAndTransportantionBarcodes(page, titleFont, barcodeFont)

			page = document.Pages.Add()
			Draw2DBarcodes(page, titleFont, barcodeFont)

			Dim output As SampleOutputInfo() = New SampleOutputInfo() {New SampleOutputInfo(document, "xfinium.pdf.sample.barcodes.pdf")}
			Return output
		End Function

		Private Shared Sub DrawGenericBarcodes(page As PdfPage, titleFont As PdfFont, barcodeFont As PdfFont)

			Dim brush As New PdfBrush()
			Dim lightGrayPen As New PdfPen(PdfRgbColor.LightGray, 0.5)

			page.Graphics.DrawString("Generic barcodes", titleFont, brush, 40, 20)
			For i As Integer = 0 To 6
				page.Graphics.DrawLine(lightGrayPen, 40, 50 + 100 * i, 570, 50 + 100 * i)
			Next
			page.Graphics.DrawLine(lightGrayPen, 306, 50, 306, 750)

			Dim barcodes As String() = New String() {"Codabar", "Code 11", "Code 25", "Code 25 Interleaved", "Code 39", "Code 39 Extended", _
				"Code 93", "Code 93 Extended", "Code 128 A", "Code 128 B", "Code 128 C", "COOP 25", _
				"Matrix 25", "MSI/Plessey"}
			Dim sao As New PdfStringAppearanceOptions()
			sao.Brush = brush
			sao.Font = barcodeFont
			Dim slo As New PdfStringLayoutOptions()
			slo.HorizontalAlign = PdfStringHorizontalAlign.Center
			slo.VerticalAlign = PdfStringVerticalAlign.Top

			slo.X = 173
			Dim sign As Integer = 1
			For i As Integer = 0 To barcodes.Length - 1
				slo.Y = 55 + 100 * (i \ 2)

				page.Graphics.DrawString(barcodes(i), sao, slo)

				slo.X = slo.X + sign * 266
				sign = -sign
			Next

			' Codabar
			Dim codabarBarcode As New PdfCodabarBarcode()
			codabarBarcode.Data = "523408943724"
			codabarBarcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom
			page.Graphics.DrawBarcode(codabarBarcode, 173 - codabarBarcode.Width / 2, 70)

			' Code 11
			Dim code11Barcode As New PdfCode11Barcode()
			code11Barcode.Data = "42376524534"
			code11Barcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom
			page.Graphics.DrawBarcode(code11Barcode, 173 + 266 - code11Barcode.Width / 2, 70)

			' Code 25
			Dim code25Barcode As New PdfCode25Barcode()
			code25Barcode.Data = "857621354312"
			code25Barcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom
			page.Graphics.DrawBarcode(code25Barcode, 173 - code25Barcode.Width / 2, 170)

			' Code 25 Interleaved
			Dim code25InterleavedBarcode As New PdfCode25InterleavedBarcode()
			code25InterleavedBarcode.Data = "42376524534"
			code25InterleavedBarcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom
			page.Graphics.DrawBarcode(code25InterleavedBarcode, 173 + 266 - code25InterleavedBarcode.Width / 2, 170)

			' Code 39
			Dim code39Barcode As New PdfCode39Barcode()
			code39Barcode.Data = "6430784327"
			code39Barcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom
			page.Graphics.DrawBarcode(code39Barcode, 173 - code39Barcode.Width / 2, 270)

			' Code 39 Extended
			Dim code39ExtendedBarcode As New PdfCode39ExtendedBarcode()
			code39ExtendedBarcode.Data = "8990436322"
			code39ExtendedBarcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom
			page.Graphics.DrawBarcode(code39ExtendedBarcode, 173 + 266 - code39ExtendedBarcode.Width / 2, 270)

			' Code 93
			Dim code93Barcode As New PdfCode93Barcode()
			code93Barcode.Data = "6345212344"
			code93Barcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom
			page.Graphics.DrawBarcode(code93Barcode, 173 - code93Barcode.Width / 2, 370)

			' Code 39 Extended
			Dim code93ExtendedBarcode As New PdfCode93ExtendedBarcode()
			code93ExtendedBarcode.Data = "125436732"
			code93ExtendedBarcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom
			page.Graphics.DrawBarcode(code93ExtendedBarcode, 173 + 266 - code93ExtendedBarcode.Width / 2, 370)

			' Code 128 A
			Dim code128ABarcode As New PdfCode128ABarcode()
			code128ABarcode.Data = "XFINIUM.PDF"
			code128ABarcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom
			page.Graphics.DrawBarcode(code128ABarcode, 173 - code128ABarcode.Width / 2, 470)

			' Code 128 B
			Dim code128BBarcode As New PdfCode128BBarcode()
			code128BBarcode.Data = "xfinium.pdf"
			code128BBarcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom
			page.Graphics.DrawBarcode(code128BBarcode, 173 + 266 - code128BBarcode.Width / 2, 470)

			' Code 128 C
			Dim code128CBarcode As New PdfCode128CBarcode()
			code128CBarcode.Data = "423409865432"
			code128CBarcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom
			page.Graphics.DrawBarcode(code128CBarcode, 173 - code128CBarcode.Width / 2, 570)

			' COOP 25
			Dim coop25Barcode As New PdfCoop25Barcode()
			coop25Barcode.Data = "43256565543"
			coop25Barcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom
			page.Graphics.DrawBarcode(coop25Barcode, 173 + 266 - coop25Barcode.Width / 2, 570)

			' Matrix 25
			Dim matrix25Barcode As New PdfMatrix25Barcode()
			matrix25Barcode.Data = "500540024300"
			matrix25Barcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom
			page.Graphics.DrawBarcode(matrix25Barcode, 173 - matrix25Barcode.Width / 2, 670)

			' MSI/Plessey
			Dim msiPlesseyBarcode As New PdfMsiPlesseyBarcode()
			msiPlesseyBarcode.Data = "1124332556"
			msiPlesseyBarcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom
			page.Graphics.DrawBarcode(msiPlesseyBarcode, 173 + 266 - msiPlesseyBarcode.Width / 2, 670)

			page.Graphics.CompressAndClose()
		End Sub

		Private Shared Sub DrawPharmaBarcodes(page As PdfPage, titleFont As PdfFont, barcodeFont As PdfFont)
			Dim brush As New PdfBrush()
			Dim lightGrayPen As New PdfPen(PdfRgbColor.LightGray, 0.5)

			page.Graphics.DrawString("Pharma barcodes (barcodes used in the pharmaceutical industry)", titleFont, brush, 40, 20)
			For i As Integer = 0 To 1
				page.Graphics.DrawLine(lightGrayPen, 40, 50 + 100 * i, 570, 50 + 100 * i)
			Next
			page.Graphics.DrawLine(lightGrayPen, 306, 50, 306, 250)

			Dim barcodes As String() = New String() {"Code 32", "Pharmacode", "PZN (Pharma-Zentral-Nummer)"}
			Dim sao As New PdfStringAppearanceOptions()
			sao.Brush = brush
			sao.Font = barcodeFont
			Dim slo As New PdfStringLayoutOptions()
			slo.HorizontalAlign = PdfStringHorizontalAlign.Center
			slo.VerticalAlign = PdfStringVerticalAlign.Top

			slo.X = 173
			Dim sign As Integer = 1
			For i As Integer = 0 To barcodes.Length - 1
				slo.Y = 55 + 100 * (i \ 2)

				page.Graphics.DrawString(barcodes(i), sao, slo)

				slo.X = slo.X + sign * 266
				sign = -sign
			Next

			' Code 32
			Dim code32Barcode As New PdfCode32Barcode()
			code32Barcode.Data = "54925174"
			code32Barcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom
			page.Graphics.DrawBarcode(code32Barcode, 173 - code32Barcode.Width / 2, 70)

			' Pharmacode
			Dim pharmacodeBarcode As New PdfPharmacodeBarcode()
			pharmacodeBarcode.Data = "128128"
			pharmacodeBarcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom
			page.Graphics.DrawBarcode(pharmacodeBarcode, 173 + 266 - pharmacodeBarcode.Width / 2, 70)

			' PZN 
			Dim pznBarcode As New PdfPznBarcode()
			pznBarcode.Data = "903271"
			pznBarcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom
			page.Graphics.DrawBarcode(pznBarcode, 173 - pznBarcode.Width / 2, 170)

			page.Graphics.CompressAndClose()
		End Sub

		Private Shared Sub DrawEanUpcBarcodes(page As PdfPage, titleFont As PdfFont, barcodeFont As PdfFont)

			Dim brush As New PdfBrush()
			Dim lightGrayPen As New PdfPen(PdfRgbColor.LightGray, 0.5)

			page.Graphics.DrawString("EAN/UPC barcodes", titleFont, brush, 40, 20)
			For i As Integer = 0 To 6
				page.Graphics.DrawLine(lightGrayPen, 40, 50 + 100 * i, 570, 50 + 100 * i)
			Next
			page.Graphics.DrawLine(lightGrayPen, 306, 50, 306, 750)

			Dim barcodes As String() = New String() {"EAN 128", "EAN-13", "EAN-8", "ISBN", "ISMN", "ISSN", _
				"JAN-13", "UPC-A", "UPC-E"}
			Dim sao As New PdfStringAppearanceOptions()
			sao.Brush = brush
			sao.Font = barcodeFont
			Dim slo As New PdfStringLayoutOptions()
			slo.HorizontalAlign = PdfStringHorizontalAlign.Center
			slo.VerticalAlign = PdfStringVerticalAlign.Top

			slo.X = 173
			Dim sign As Integer = 1
			For i As Integer = 0 To barcodes.Length - 1
				slo.Y = 55 + 100 * (i \ 2)

				page.Graphics.DrawString(barcodes(i), sao, slo)

				slo.X = slo.X + sign * 266
				sign = -sign
			Next

			' EAN 128
			Dim ean128Barcode As New PdfEan128Barcode()
			ean128Barcode.Data = "WWW.XFINIUMSOFT.COM"
			ean128Barcode.QuietZones.Left = 0
			ean128Barcode.QuietZones.Right = 0
			ean128Barcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom
			ean128Barcode.ApplicationIdentifier = "URL"
			page.Graphics.DrawBarcode(ean128Barcode, 173 - ean128Barcode.Width / 2, 70)

			' EAN-13
			Dim ean13Barcode As New PdfEan13Barcode()
			ean13Barcode.Data = "437612735617"
			ean13Barcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom
			page.Graphics.DrawBarcode(ean13Barcode, 173 + 266 - ean13Barcode.Width / 2, 70)

			' EAN-8
			Dim ean8Barcode As New PdfEan8Barcode()
			ean8Barcode.Data = "5423731"
			ean8Barcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom
			page.Graphics.DrawBarcode(ean8Barcode, 173 - ean8Barcode.Width / 2, 170)

			' ISBN
			Dim isbnBarcode As New PdfIsbnBarcode()
			isbnBarcode.Data = "436314378"
			isbnBarcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom
			page.Graphics.DrawBarcode(isbnBarcode, 173 + 266 - isbnBarcode.Width / 2, 170)

			' ISMN
			Dim ismnBarcode As New PdfIsmnBarcode()
			ismnBarcode.Data = "437612489"
			ismnBarcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom
			page.Graphics.DrawBarcode(ismnBarcode, 173 - ismnBarcode.Width / 2, 270)

			' ISSN
			Dim issnBarcode As New PdfIssnBarcode()
			issnBarcode.Data = "546712341"
			issnBarcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom
			page.Graphics.DrawBarcode(issnBarcode, 173 + 266 - issnBarcode.Width / 2, 270)

			' JAN-13
			Dim jan13Barcode As New PdfJan13Barcode()
			jan13Barcode.Data = "1256127634"
			jan13Barcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom
			page.Graphics.DrawBarcode(jan13Barcode, 173 - jan13Barcode.Width / 2, 370)

			' UPC-A
			Dim upcaBarcode As New PdfUpcaBarcode()
			upcaBarcode.Data = "12543267841"
			upcaBarcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom
			page.Graphics.DrawBarcode(upcaBarcode, 173 + 266 - upcaBarcode.Width / 2, 370)

			' UPC-E
			Dim upceBarcode As New PdfUpceBarcode()
			upceBarcode.Data = "1234532"
			upceBarcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom
			page.Graphics.DrawBarcode(upceBarcode, 173 - upceBarcode.Width / 2, 470)

			page.Graphics.CompressAndClose()
		End Sub

		Private Shared Sub DrawPostAndTransportantionBarcodes(page As PdfPage, titleFont As PdfFont, barcodeFont As PdfFont)

			Dim brush As New PdfBrush()
			Dim lightGrayPen As New PdfPen(PdfRgbColor.LightGray, 0.5)

			page.Graphics.DrawString("Post and transportation barcodes", titleFont, brush, 40, 20)
			For i As Integer = 0 To 6
				page.Graphics.DrawLine(lightGrayPen, 40, 50 + 100 * i, 570, 50 + 100 * i)
			Next
			page.Graphics.DrawLine(lightGrayPen, 306, 50, 306, 750)

			Dim barcodes As String() = New String() {"FedEx Ground 96", "IATA 25", "Identcode", "Leitcode", "KIX", "Planet", _
				"PostNet", "RM4SCC", "SCC-14", "SingaporePost", "SSCC-18", "USPS FIM", _
				"USPS Horizontal", "USPS PIC"}
			Dim sao As New PdfStringAppearanceOptions()
			sao.Brush = brush
			sao.Font = barcodeFont
			Dim slo As New PdfStringLayoutOptions()
			slo.HorizontalAlign = PdfStringHorizontalAlign.Center
			slo.VerticalAlign = PdfStringVerticalAlign.Top

			slo.X = 173
			Dim sign As Integer = 1
			For i As Integer = 0 To barcodes.Length - 1
				slo.Y = 55 + 100 * (i \ 2)

				page.Graphics.DrawString(barcodes(i), sao, slo)

				slo.X = slo.X + sign * 266
				sign = -sign
			Next

			' FedEx Ground 96
			Dim fedexGround96Barcode As New PdfFedExGround96Barcode()
			fedexGround96Barcode.Data = "962343237687543423123"
			fedexGround96Barcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom
			page.Graphics.DrawBarcode(fedexGround96Barcode, 173 - fedexGround96Barcode.Width / 2, 70)

			' IATA 25
			Dim iata25Barcode As New PdfIata25Barcode()
			iata25Barcode.Data = "54366436563"
			iata25Barcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom
			page.Graphics.DrawBarcode(iata25Barcode, 173 + 266 - iata25Barcode.Width / 2, 70)

			' Identcode
			Dim identcodeBarcode As New PdfIdentcodeBarcode()
			identcodeBarcode.Data = "12435678214"
			identcodeBarcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom
			page.Graphics.DrawBarcode(identcodeBarcode, 173 - identcodeBarcode.Width / 2, 170)

			' Leitcode
			Dim leitcodeBarcode As New PdfLeitcodeBarcode()
			leitcodeBarcode.Data = "1243657687321"
			leitcodeBarcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom
			page.Graphics.DrawBarcode(leitcodeBarcode, 173 + 266 - leitcodeBarcode.Width / 2, 170)

			' KIX
			Dim kixBarcode As New PdfKixBarcode()
			kixBarcode.Data = "XFINIUMPDF"
			kixBarcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom
			page.Graphics.DrawBarcode(kixBarcode, 173 - kixBarcode.Width / 2, 270)

			' Planet
			Dim planetBarcode As New PdfPlanetBarcode()
			planetBarcode.Data = "645316643300"
			planetBarcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom
			page.Graphics.DrawBarcode(planetBarcode, 173 + 266 - planetBarcode.Width / 2, 270)

			' PostNet
			Dim postNetBarcode As New PdfPostNetBarcode()
			postNetBarcode.Data = "04231454322"
			postNetBarcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom
			page.Graphics.DrawBarcode(postNetBarcode, 173 - postNetBarcode.Width / 2, 370)

			' RM4SCC
			Dim rm4sccBarcode As New PdfRm4sccBarcode()
			rm4sccBarcode.Data = "XFINIUMPDF"
			rm4sccBarcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom
			page.Graphics.DrawBarcode(rm4sccBarcode, 173 + 266 - rm4sccBarcode.Width / 2, 370)

			' SCC-14
			Dim scc14Barcode As New PdfScc14Barcode()
			scc14Barcode.Data = "3255091205412"
			scc14Barcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom
			page.Graphics.DrawBarcode(scc14Barcode, 173 - scc14Barcode.Width / 2, 470)

			' Singapore Post
			Dim singaporePostBarcode As New PdfSingaporePostBarcode()
			singaporePostBarcode.Data = "XFINIUMPDF"
			singaporePostBarcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom
			page.Graphics.DrawBarcode(singaporePostBarcode, 173 + 266 - singaporePostBarcode.Width / 2, 470)

			' SSCC-18
			Dim sscc18Barcode As New PdfSscc18Barcode()
			sscc18Barcode.Data = "09876543219832435"
			sscc18Barcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom
			page.Graphics.DrawBarcode(sscc18Barcode, 173 - sscc18Barcode.Width / 2, 570)

			' USPS FIM
			Dim uspsFimBarcode As New PdfUspsFimBarcode()
			uspsFimBarcode.Data = "A"
			uspsFimBarcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom
			page.Graphics.DrawBarcode(uspsFimBarcode, 173 + 266 - uspsFimBarcode.Width / 2, 570)

			' USPS Horizontal
			Dim uspsHorizontalBarcode As New PdfUspsHorizontalBarcode()
			uspsHorizontalBarcode.Data = "1111"
			uspsHorizontalBarcode.BarcodeTextPosition = PdfBarcodeTextPosition.None
			page.Graphics.DrawBarcode(uspsHorizontalBarcode, 173 - uspsHorizontalBarcode.Width / 2, 670)

			' USPS PIC
			Dim uspsPicBarcode As New PdfUspsPicBarcode()
			uspsPicBarcode.Data = "914354657901234354019"
			uspsPicBarcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom
			page.Graphics.DrawBarcode(uspsPicBarcode, 173 + 266 - uspsPicBarcode.Width / 2, 670)

			page.Graphics.CompressAndClose()
		End Sub

		Private Shared Sub Draw2DBarcodes(page As PdfPage, titleFont As PdfFont, barcodeFont As PdfFont)
			Dim brush As New PdfBrush()
			Dim lightGrayPen As New PdfPen(PdfRgbColor.LightGray, 0.5)

			page.Graphics.DrawString("2D barcodes", titleFont, brush, 40, 20)
			For i As Integer = 0 To 2
				page.Graphics.DrawLine(lightGrayPen, 40, 50 + 150 * i, 570, 50 + 150 * i)
			Next
			page.Graphics.DrawLine(lightGrayPen, 306, 50, 306, 500)

			Dim barcodes As String() = New String() {"Codablock F", "Code 16K", "PDF417", "Micro PDF417", "DataMatrix"}
			Dim sao As New PdfStringAppearanceOptions()
			sao.Brush = brush
			sao.Font = barcodeFont
			Dim slo As New PdfStringLayoutOptions()
			slo.HorizontalAlign = PdfStringHorizontalAlign.Center
			slo.VerticalAlign = PdfStringVerticalAlign.Top

			slo.X = 173
			Dim sign As Integer = 1
			For i As Integer = 0 To barcodes.Length - 1
				slo.Y = 55 + 150 * (i \ 2)

				page.Graphics.DrawString(barcodes(i), sao, slo)

				slo.X = slo.X + sign * 266
				sign = -sign
			Next

			' Codablock F
			Dim codablockFBarcode As New PdfCodablockFBarcode()
			codablockFBarcode.Data = "*** Xfinium.Pdf ***"
			codablockFBarcode.Columns = 10
			codablockFBarcode.Rows = 5
			page.Graphics.DrawBarcode(codablockFBarcode, 173 - codablockFBarcode.Width / 2, 70)

			' Code 16K
			Dim code16KBarcode As New PdfCode16KBarcode()
			code16KBarcode.Data = "*** Xfinium.Pdf ***"
			code16KBarcode.Rows = 6
			page.Graphics.DrawBarcode(code16KBarcode, 173 + 266 - code16KBarcode.Width / 2, 70)

			' PDF 417
			Dim pdf417Barcode As New Pdf417RegularBarcode()
			pdf417Barcode.Data = "*** Xfinium.Pdf ***"
			pdf417Barcode.Columns = 10
			pdf417Barcode.Rows = 0
			page.Graphics.DrawBarcode(pdf417Barcode, 173 - pdf417Barcode.Width / 2, 220)

			' MicroPDF 417
			Dim microPdf417Barcode As New Pdf417MicroBarcode()
			microPdf417Barcode.Data = "* Xfinium.Pdf *"
			microPdf417Barcode.BarcodeSize = Pdf417MicroBarcodeSize.Rows6Columns4
			page.Graphics.DrawBarcode(microPdf417Barcode, 173 + 266 - microPdf417Barcode.Width / 2, 220)

			' DataMatrix
			Dim datamatrixBarcode As New PdfDataMatrixBarcode()
			datamatrixBarcode.Data = "*** Xfinium.Pdf ***"
			datamatrixBarcode.XDimension = 2
			datamatrixBarcode.BarcodeSize = DataMatrixBarcodeSize.Auto
			page.Graphics.DrawBarcode(datamatrixBarcode, 173 - datamatrixBarcode.Width / 2, 370)

			page.Graphics.CompressAndClose()
		End Sub

	End Class
End Namespace
