Public Class TextFileClass
    Private vLines() As String
    Private vIsComment() As Boolean ' Not a Property
    Private vFilePath As String
    Private vCommentMarker As String
    Private vindex_ReadFile As Integer = -1
    Private vIOMode As String

    Private vAdditionalData(0) As String

    Private AllowFileDeletion As Boolean = False
    Private NewLine = vbNewLine

    Public Sub New(ByVal _filepath As String, ByVal _commentMarker As String, Optional ByVal WithDeletes As Boolean = False)
        vFilePath = _filepath
        vCommentMarker = _commentMarker
        AllowFileDeletion = WithDeletes
    End Sub

#Region "Properties"
    Public Property Line(ByVal i As Integer) As String
        Get
            Return vLines(i)
        End Get
        Set(value As String)
            If vLines.Length < i Then
                ReDim Preserve vLines(i)
            End If
            vLines(i) = value
        End Set
    End Property
    Public Property FilePath() As String
        Get
            Return vFilePath
        End Get
        Set(value As String)
            vFilePath = value
        End Set
    End Property
    Public Property CommentMarker(ByVal line As String) As String
        Get
            Return vCommentMarker
        End Get
        Set(value As String)
            vCommentMarker = value
        End Set
    End Property
    Public Property CurrentIndex As Integer
        Get
            Return vindex_ReadFile
        End Get
        Set(value As Integer)
            vindex_ReadFile = value
        End Set
    End Property
    Public Property AdditonalData(ByVal i As Integer) As String
        Get
            Return vAdditionalData(i)
        End Get
        Set(value As String)
            If vAdditionalData.Length <= i Then
                ReDim Preserve vAdditionalData(i)
            End If
            vAdditionalData(i) = value
        End Set
    End Property
#End Region

#Region "Functions"
    Public Sub SaveFile()
        Dim sw As New IO.StreamWriter(vFilePath)
        For Each _line In vLines
            sw.WriteLine(_line)
        Next
        sw.Close()
    End Sub
    Public Sub LoadFile()
        Dim sr As New IO.StreamReader(vFilePath)
        Dim i As Integer = 0
        Do Until sr.EndOfStream()
            ReDim Preserve vLines(i)
            ReDim Preserve vIsComment(i)
            vLines(i) = sr.ReadLine ' Reads file's line to variable
            If Left(vLines(i), vCommentMarker.Length) = vCommentMarker Then ' Check if Line is a Comment
                ' Line is a Comment
                vIsComment(i) = True
            Else
                ' Line is Not a Comment
                vIsComment(i) = False
            End If
            i = i + 1
        Loop
        sr.Close()
    End Sub
    Public Function ReadLine()
        vindex_ReadFile = vindex_ReadFile + 1 ' increase index to next line..
        If vindex_ReadFile >= vLines.Length Then Return Nothing
        Dim _line As String = vLines(vindex_ReadFile) ' read new index line..
        Do While vLines(vindex_ReadFile) <> Nothing And Left(_line, vCommentMarker.Length) = vCommentMarker ' if it's a comment..
            vindex_ReadFile = vindex_ReadFile + 1 ' increase index..
            _line = vLines(vindex_ReadFile) ' read line at new index..
        Loop ' then skip the comment and attempt to loop again..
        ' when not a comment or when no more lines..
        If vLines(vindex_ReadFile) = Nothing Then Return Nothing ' if no more lines..
        Return vLines(vindex_ReadFile) ' if not a comment..
    End Function
    Public Sub WriteLine(ByVal _newline As String)
        vindex_ReadFile = vindex_ReadFile + 1 ' increase index to next line..
        ReDim Preserve vLines(vindex_ReadFile)
        vLines(vindex_ReadFile) = _newline ' replace old line with the new one..
    End Sub
    Public Sub WriteAllLines(ByVal _newlines() As String)
        vLines = _newlines
    End Sub
    Public Sub UpdateValueByText(ByVal _text As String, ByVal _newvalue As String)
        If _newvalue = "1" Then _newvalue = "true" Else If _newvalue = "0" Then _newvalue = "false"
        Dim i As Integer = 0
        Try
            Do Until vAdditionalData(i) = _text
                i = i + 1
            Loop
        Catch ex As Exception
            MsgBox("An error occured in TextFileClass.UpdateValueByText(). Please give the data from the next window to the developer, as this error is completely unprogrammed for." & NewLine & _
                   "(" & _text & ", " & _newvalue & ")", MsgBoxStyle.Critical, "Error!")
            InputBox("This is the data the developer needs. Thank you, and I am sorry for the inconvenience.", "Error Help", ex.Message)
            Exit Sub
        End Try
        vLines(i) = _newvalue
    End Sub
    Public Function GetValueByText(ByVal _text As String)
        Dim i As Integer = 0
        Try
            Do Until vAdditionalData(i) = _text
                i = i + 1
            Loop
        Catch ex As Exception
            MsgBox("An error occured in TextFileClass.GetValueByText(). Please give the data from the next window to the developer, as this error is completely unprogrammed for." & NewLine & _
                   "(" & _text & ")", MsgBoxStyle.Critical, "Error - GetValueByText()")
            InputBox("This is the data the developer needs. Thank you, and I am sorry for the inconvenience.", "Error Help - GetValueByText()", ex.Message)
            Exit Function
        End Try
        Return vLines(i)
    End Function
    Public Function FileExists() As Boolean
        Return My.Computer.FileSystem.FileExists(vFilePath)
    End Function

    Public Function NumberOfLines()
        Return vLines.Length
    End Function
    Sub DeleteFile()
        If AllowFileDeletion = True Then
            My.Computer.FileSystem.DeleteFile(vFilePath, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
        End If
    End Sub
#End Region
End Class

