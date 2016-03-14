Imports System.IO
Imports Xfinium.Pdf
Imports Xfinium.Pdf.Graphics
Imports Xfinium.Pdf.Content

Namespace Xfinium.Pdf.Samples
	''' <summary>
	''' Search text sample.
	''' </summary>
	Public Class SearchText
		''' <summary>
		''' Main method for running the sample.
		''' </summary>
		Public Shared Function Run(input As Stream) As SampleOutputInfo()
			Dim document As New PdfFixedDocument(input)
			Dim ce As New PdfContentExtractor(document.Pages(0))

			' Simple search.
			Dim searchResults As PdfTextSearchResultCollection = ce.SearchText("at")
			HighlightSearchResults(document.Pages(0), searchResults, PdfRgbColor.Red)

			' Whole words search.
			searchResults = ce.SearchText("at", PdfTextSearchOptions.WholeWordSearch)
			HighlightSearchResults(document.Pages(0), searchResults, PdfRgbColor.Green)

			' Regular expression search, find all words that start with uppercase.
			searchResults = ce.SearchText("[A-Z][a-z]*", PdfTextSearchOptions.RegExSearch)
			HighlightSearchResults(document.Pages(0), searchResults, PdfRgbColor.Blue)

			Dim output As SampleOutputInfo() = New SampleOutputInfo() {New SampleOutputInfo(document, "xfinium.pdf.sample.searchtext.pdf")}
			Return output
		End Function

		Private Shared Sub HighlightSearchResults(page As PdfPage, searchResults As PdfTextSearchResultCollection, color As PdfColor)
			Dim pen As New PdfPen(color, 0.5)

			For i As Integer = 0 To searchResults.Count - 1
				Dim tfc As PdfTextFragmentCollection = searchResults(i).TextFragments
				For j As Integer = 0 To tfc.Count - 1
					Dim path As New PdfPath()

					path.StartSubpath(tfc(j).FragmentCorners(0).X, tfc(j).FragmentCorners(0).Y)
					path.AddPolygon(tfc(j).FragmentCorners)

					page.Graphics.DrawPath(pen, path)
				Next
			Next
		End Sub
	End Class
End Namespace