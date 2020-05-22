Public Class CommonLibrary

    Public IO_RootAsDesktop As String = My.Computer.FileSystem.SpecialDirectories.Desktop & "\"
    Public IO_RootAsUserData As String = My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\..\..\..\Rundatshityo\"

    'Public IO_Root As String = IO_RootAsDesktop
    Public IO_Root As String = IO_RootAsUserData

    Public IO_EQOA As String = IO_Root & "EQOA\"
    Public IO_LUAs As String = IO_EQOA & "LUAs\"

    'Paths
    Public Folder_EQOA As String = IO_EQOA
    Public Folder_GameData As String = Folder_EQOA & "Game Data\"
    Public Folder_NPCs As String = Folder_GameData & "NPCs\"
    Public Folder_NPC_Areas As String = Folder_GameData & "NPC Areas\"
    Public Folder_Ghosts As String = Folder_GameData & "Ghosts\"
    'Custom Data
    Public Folder_Custom_Data As String = Folder_EQOA & "Custom Data\"
    Public Folder_Custom_NPCs As String = Folder_Custom_Data & "NPC Maker\"
    Public Folder_Custom_Areas As String = Folder_Custom_NPCs & "NPC Areas\"
    Public Folder_Custom_Zones As String = Folder_Custom_Areas & "Zones\"
    'Temp Data
    Public Folder_Temp As String = Folder_EQOA & "Temp\"
    Public Folder_Temp_NPC_Maker As String = Folder_Temp & "NPC Maker\"
    'Net Streams
    Public Folder_Net_Streams As String = Folder_EQOA & "Net Streams\"

    Public Extenion_NPC As String = ".txt"
    Public Extenion_Area As String = ".txt"

    Public IO_Kairen As String = IO_Root & "Kairen\"

    Private DisplayMessage_UsingOwn As Boolean = False


#Region "MISC"
    ''' <summary>
    ''' Checks if a Directory/Folder Exists.
    ''' </summary>
    ''' <param name="_path">The Folder to Check.</param>
    ''' <returns>returns True if it does exist, or False if it doesn't.</returns>
    ''' <remarks></remarks>
    Public Function DE(_path) As Boolean
        If My.Computer.FileSystem.DirectoryExists(_path) Then
            Return True
        Else : Return False
        End If
    End Function
    ''' <summary>
    ''' Checks if a File Exists.
    ''' </summary>
    ''' <param name="_path">The File to Check.</param>
    ''' <returns>returns True if it does exist, or False if it doesn't.</returns>
    ''' <remarks></remarks>
    Public Function FE(_path) As Boolean
        If My.Computer.FileSystem.FileExists(_path) Then
            Return True
        Else : Return False
        End If
    End Function
    Public Function IO_ReadNextLine(ByRef sr As IO.StreamReader, Optional ByVal commentString As String = Nothing)
        Dim line As String
        If sr.EndOfStream = False Then
            If commentString = Nothing Then
                Return sr.ReadLine()
            Else
                line = sr.ReadLine()
                Do While Left(line, commentString.Length) = commentString
                    line = sr.ReadLine()
                Loop
                Return line
            End If
        Else
            Return Nothing
        End If
    End Function
    ''' <summary>
    ''' Returns characters from the right end of a string.
    ''' </summary>
    ''' <param name="Stringaling">The string to retrieve from.</param>
    ''' <param name="Number">How many charaters to return.</param>
    ''' <returns>The new string as String.</returns>
    ''' <remarks>The string retrieved as String</remarks>
    Public Shadows Function Right(ByVal Stringaling As String, ByVal Number As Integer)
        Return Microsoft.VisualBasic.Right(Stringaling, Number)
    End Function
    ''' <summary>
    ''' Returns characters from the left end of a string.
    ''' </summary>
    ''' <param name="Stringaling">The string to retrieve from.</param>
    ''' <param name="Number">How many charaters to return.</param>
    ''' <returns>The new string as String.</returns>
    ''' <remarks>The string retrieved as String</remarks>
    Public Shadows Function Left(ByVal Stringaling As String, ByVal Number As Integer)
        Return Microsoft.VisualBasic.Left(Stringaling, Number)
    End Function
    Public Function ReadFileToArray1(ByVal _filepath As String, Optional ByVal _commentMarker As String = Nothing)
        If FE(_filepath) Then
            'file does exist
            Dim sr As New IO.StreamReader(_filepath)
            Dim returnvar() As String
            Dim i As Integer = 1
            Dim tempvar As String
            Do Until sr.EndOfStream
                tempvar = sr.ReadLine
                If _commentMarker <> Nothing And Left(tempvar, _commentMarker.Length) <> _commentMarker Then
                    ReDim Preserve returnvar(i)
                    returnvar(i) = tempvar
                    i = i + 1
                ElseIf _commentMarker Is Nothing Then
                    ReDim Preserve returnvar(i)
                    returnvar(i) = tempvar
                    i = i + 1
                End If
            Loop
            If i > 1 Then
                'file is not empty
                returnvar(0) = "Success"
                Return returnvar
            Else
                'file is empty
                ReDim returnvar(0)
                returnvar(0) = "File Is Empty"
                Return returnvar
            End If
        Else
            'file doesn't exist
            Dim returnvar(0) As String
            returnvar(0) = "File Doesn't Exist"
            Return returnvar
        End If
    End Function
    Public Function ReadFileToArray(ByVal _filepath As String)
        If FE(_filepath) Then
            'file does exist
            Dim sr As New IO.StreamReader(_filepath)
            Dim returnvar() As String
            Dim i As Integer = 1
            Dim tempvar As String
            Do Until sr.EndOfStream
                ReDim Preserve returnvar(i)
                returnvar(i) = sr.ReadLine
                i = i + 1
            Loop
            If i > 1 Then
                'file is not empty
                returnvar(0) = "Success"
                Return returnvar
            Else
                'file is empty
                ReDim returnvar(0)
                returnvar(0) = "File Is Empty"
                Return returnvar
            End If
        Else
            'file doesn't exist
            Dim returnvar(0) As String
            returnvar(0) = "File Doesn't Exist"
            Return returnvar
        End If
    End Function
    Public Function ReadFoldersToArray(ByVal _folderPath As String)

        Dim _returnVar() As String
        Dim i As Integer = 0

        Dim di As New IO.DirectoryInfo(_folderPath)
        Dim diar1 As IO.FileInfo() = di.GetFiles()
        Dim dra As IO.FileInfo

        'list the names of all files in the specified directory
        For Each dra In diar1
            ReDim Preserve _returnVar(i)
            _returnVar(i) = dra.ToString(i)
            i = i + 1
        Next

    End Function
    Public Function ReturnNextNonCommentIndex(ByVal _array() As String, ByRef _index As Integer, ByVal _commentMarker As String)
        If _index = -1 Then
            _index = 0
        Else : _index = _index + 1
        End If
        Do While Left(_array(_index), _commentMarker.Length) = _commentMarker
            _index = _index + 1
        Loop
        Return _index
    End Function
    Public Function WriteArrayToFile(ByVal _filepath As String, ByVal _array() As String)
        Dim sw As New IO.StreamWriter(_filepath)
        For Each line In _array
            sw.WriteLine(line)
        Next
        sw.Close()
    End Function

    Public Function ReturnFilesFromFolder(ByVal _folder As String, Optional ByVal _extension As String = "")
        If DE(_folder) = False Then Exit Function

        Dim _returnVar() As String
        Dim i As Integer = 0

        Dim di As New IO.DirectoryInfo(_folder)
        Dim diar1 As IO.FileInfo() = di.GetFiles()
        Dim dra As IO.FileInfo

        'list the names of all files in the specified directory
        If diar1.Length = 0 Then
            ReDim _returnVar(0)
            _returnVar(0) = ""
        ElseIf _extension = "" Then
            For Each dra In diar1
                ReDim Preserve _returnVar(i)
                _returnVar(i) = dra.ToString()
                i = i + 1
            Next
        Else
            For Each dra In diar1
                If Microsoft.VisualBasic.Right(dra.ToString(), _extension.Length) = _extension Then
                    ReDim Preserve _returnVar(i)
                    _returnVar(i) = dra.ToString.Replace(_extension, "")
                    i = i + 1
                End If
            Next
        End If

        Return _returnVar
    End Function
    Public Function AddItemsToControl(ByVal _itemsToAdd() As String, ByVal _objectToAddTo As Object)
        _objectToAddTo.items.clear()
        For Each item In _itemsToAdd
            _objectToAddTo.items.add(item)
        Next
        Return _itemsToAdd
    End Function
    'Display Message Using Own NotifyIcon
    Public Sub DisplayMessage(ByVal _message As String, ByVal _title As String, ByVal _text As String, Optional ByVal _icon As Object = Nothing, Optional ByVal MillisecondsToDisplay As Integer = 5000)
        DisplayMessage_NotifyIcon = New System.Windows.Forms.NotifyIcon
        DisplayMessage_UsingOwn = True
        If IsNothing(_icon) Then
            DisplayMessage_NotifyIcon.Icon = My.Resources._64x64
        Else
            DisplayMessage_NotifyIcon.Icon = _icon
        End If
        DisplayMessage_NotifyIcon.Text = _text
        DisplayMessage_NotifyIcon.Visible = True
        DisplayMessage_NotifyIcon.BalloonTipText = _message
        DisplayMessage_NotifyIcon.BalloonTipTitle = _title
        DisplayMessage_NotifyIcon.ShowBalloonTip(MillisecondsToDisplay) 'Milliseconds is Optional
        DisplayMessage_NotifyIcon.Visible = True
        DisplayMessageTimer.Interval = MillisecondsToDisplay
        DisplayMessageTimer.Start()
    End Sub
    Private WithEvents DisplayMessageTimer As New Windows.Forms.Timer
    Private DisplayMessage_NotifyIcon As New System.Windows.Forms.NotifyIcon
    Private Sub DisplayMessageTimer_Tick() Handles DisplayMessageTimer.Tick
        DisplayMessage_NotifyIcon.BalloonTipText = ""
        DisplayMessage_NotifyIcon.BalloonTipTitle = ""
        If DisplayMessage_UsingOwn Then
            DisplayMessage_NotifyIcon.Visible = False
            DisplayMessage_NotifyIcon = Nothing
        End If
        DisplayMessageTimer.Stop()
        DisplayMessage_NotifyIcon = Nothing
    End Sub
    'Display Message Using Provided NotifyIcon
    Public Sub DisplayMessage(ByVal _message As String, ByVal _title As String, ByRef _NotifyIcon As Object, Optional ByVal MillisecondsToDisplay As Integer = 5000)
        DisplayMessage_NotifyIcon = _NotifyIcon
        DisplayMessage_UsingOwn = False
        DisplayMessage_NotifyIcon.BalloonTipText = _message
        DisplayMessage_NotifyIcon.BalloonTipTitle = _title
        DisplayMessage_NotifyIcon.ShowBalloonTip(MillisecondsToDisplay) 'Milliseconds is Optional
        DisplayMessageTimer.Interval = MillisecondsToDisplay
        DisplayMessageTimer.Start()
    End Sub
    Public Sub ParseOutsideCommand(ByVal command As String, ByVal lines() As String)
        Dim filewrite As String = Folder_EQOA & "Temp\Outside Command" & ".txt"

        Dim sw As New IO.StreamWriter(filewrite)
        sw.WriteLine(command)
        Select Case command
            Case "Spawn_NPC"
                For Each line In lines
                    sw.WriteLine(line)
                Next
            Case "Spawn Wall Marker"
                For Each line In lines
                    sw.WriteLine(line)
                Next
            Case "PrintToConsole"
                For Each line In lines
                    sw.WriteLine(line)
                Next
        End Select
        sw.Close()
    End Sub
    Public Sub ParseOutsideCommand(ByVal command As String, ByVal line As String)
        Dim filewrite As String = Folder_EQOA & "Temp\Outside Command" & ".txt"

        Dim sw As New IO.StreamWriter(filewrite)
        sw.WriteLine(command)
        Select Case command
            Case "Spawn_NPC"
                sw.WriteLine(line)
            Case "Spawn Wall Marker"
                sw.WriteLine(line)
            Case "PrintToConsole"
                sw.WriteLine(line)
            Case "OutputPlayerData"
                sw.WriteLine(line.ToLower)
        End Select
        sw.Close()
    End Sub

    Public Sub ParseOutsideCommand(ByVal command As String)
        Dim filewrite As String = Folder_EQOA & "Temp\Outside Command" & ".txt"

        Dim sw As New IO.StreamWriter(filewrite)
        sw.WriteLine(command)
        Select Case command
            Case "ConnectToKairen"
                sw.WriteLine("KairenIsAsking")
        End Select
        sw.Close()
    End Sub
#End Region

End Class
