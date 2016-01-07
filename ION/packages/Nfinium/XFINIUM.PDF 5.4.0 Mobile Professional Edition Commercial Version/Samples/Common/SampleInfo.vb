Imports System.Collections.Generic
Imports System.Text

Namespace Xfinium.Pdf.SamplesExplorer
	''' <summary>
	''' Stores information about a sample.
	''' </summary>
	Public Class SampleInfo
		''' <summary>
		''' Gets or sets the sample name.
		''' </summary>
		Public Property Name() As String
			Get
				Return m_Name
			End Get
			Set
				m_Name = Value
			End Set
		End Property
		Private m_Name As String

		''' <summary>
		''' Gets or sets the sample description.
		''' </summary>
		Public Property Description() As String
			Get
				Return m_Description
			End Get
			Set
				m_Description = Value
			End Set
		End Property
		Private m_Description As String

		''' <summary>
		''' Gets or sets the C# source code file.
		''' </summary>
		Public Property CSharpSourceCodeFile() As String
			Get
				Return m_CSharpSourceCodeFile
			End Get
			Set
				m_CSharpSourceCodeFile = Value
			End Set
		End Property
		Private m_CSharpSourceCodeFile As String

		''' <summary>
		''' Gets or sets the VB.NET source code file.
		''' </summary>
		Public Property VbNetSourceCodeFile() As String
			Get
				Return m_VbNetSourceCodeFile
			End Get
			Set
				m_VbNetSourceCodeFile = Value
			End Set
		End Property
		Private m_VbNetSourceCodeFile As String

		''' <summary>
		''' Gets or sets the sample id.
		''' </summary>
		Public Property ID() As String
			Get
				Return m_ID
			End Get
			Set
				m_ID = Value
			End Set
		End Property
		Private m_ID As String

		Public Overrides Function ToString() As String
			Return Name
		End Function
	End Class
End Namespace
