Imports Xfinium.Pdf

Namespace Xfinium.Pdf.Samples
	''' <summary>
	''' Represents the output of a sample.
	''' </summary>
	Public Class SampleOutputInfo
		''' <summary>
		''' Initializes a new <see cref="SampleOutputInfo"/> object.
		''' </summary>
		''' <param name="document">The document that represents the sample output</param>
		''' <param name="fileName">The file name of the sample output</param>
		Public Sub New(document As PdfDocument, fileName As String)
			Me.m_document = document
			Me.m_fileName = fileName
		End Sub

		Private m_document As PdfDocument
		''' <summary>
		''' Gets or sets the document that represents the sample output.
		''' </summary>
		Public Property Document() As PdfDocument
			Get
				Return m_document
			End Get
			Set
				m_document = value
			End Set
		End Property

		Private m_fileName As String
		''' <summary>
		''' Gets or sets the file name the sample is saved to disk.
		''' </summary>
		Public Property FileName() As String
			Get
				Return m_fileName
			End Get
			Set
				m_fileName = value
			End Set
		End Property
	End Class
End Namespace
