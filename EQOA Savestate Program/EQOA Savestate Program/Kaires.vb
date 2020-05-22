Public Class Kaires

    Dim lb As New CommonLibrary
    Dim CEKey() As CEKey

#Region "Shared"
    Private Sub Kaires_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub
#End Region


    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim lines() As String = TextBox1.Lines.Clone ' i is index
        Dim i As Integer ' lines() index
        Dim luaComment As String
        Do While i < lines.Length
            lines(i) = lines(i).Trim
            If lb.Left(lines(i), 5) = "<?xml" Then

            ElseIf lb.Left(lines(i), 12) = "<CheatTable>" Then

            ElseIf lb.Left(lines(i), 14) = "<CheatEntries>" Then
            ElseIf lb.Left(lines(i), 12) = "<CheatEntry>" Then
            ElseIf lb.Left(lines(i), 4) = "<ID>" Then

            ElseIf lb.Left(lines(i), 13) = "<Description>" Then
                luaComment = lines(i).Replace("<Description>" & Chr(34), "").Replace(Chr(34) & "</Description>", "")
            ElseIf lb.Left(lines(i), 10) = "<LastState" Then
            ElseIf lb.Left(lines(i), 11) = "<ShowAsHex>" Then
            ElseIf lb.Left(lines(i), 7) = "<Color>" Then
            ElseIf lb.Left(lines(i), 14) = "<VariableType>" Then
                'luaComment = lines(i).Replace("", "").Replace("", "")
            ElseIf lb.Left(lines(i), 12) = "<ByteLength>" Then
                'luaComment = lines(i).Replace("", "").Replace("", "")
            ElseIf lb.Left(lines(i), 9) = "<Address>" Then
                TextBox2.AppendText(Chr(34) & "[" & lines(i).Replace("<Address>", "").Replace(Chr(34), "").Replace("</Address>", "") & "]+")
            ElseIf lb.Left(lines(i), 9) = "<Offsets>" Then
            ElseIf lb.Left(lines(i), 8) = "<Offset>" Then
                TextBox2.AppendText(lines(i).Replace("<Offset>", "").Replace("</Offset>", "") & Chr(34) & "--" & luaComment & vbNewLine)
                'luaComment = lines(i).Replace("", "").Replace("", "")
            ElseIf lb.Left(lines(i), 10) = "</Offsets>" Then
            ElseIf lb.Left(lines(i), 13) = "</CheatEntry>" Then
            ElseIf lb.Left(lines(i), 8) = "<Length>" Then
                'luaComment = lines(i).Replace("", "").Replace("", "")
            ElseIf lb.Left(lines(i), 9) = "<Unicode>" Then
            ElseIf lb.Left(lines(i), 15) = "<ZeroTerminate>" Then
            ElseIf lb.Left(lines(i), 8) = "<Options" Then
            ElseIf lb.Left(lines(i), 15) = "</CheatEntries>" Then
            ElseIf lb.Left(lines(i), 13) = "</CheatTable>" Then
            Else
                TextBox2.AppendText(lines(i) & vbNewLine)
            End If
            i = i + 1
        Loop


        Exit Sub
        'Dim lines() As String = TextBox23.Lines.Clone ' i is index
        Dim newlineNumber As Integer ' index for below newline_ variables
        Dim newline_description(), newline_variableType(), newline_address(), newline_offeset() As String
        Dim errorline() As String
        'Dim i As Integer ' lines() index
        'Dim i2 As Integer ' newline() index
        Dim _step As Integer = 0
        Do While i < lines.Length
            Select Case _step
                Case 0
                    Select Case lines(i)
                        Case "<?xml version=" & Chr(34) & "1.0" & Chr(34) & " encoding=" & Chr(34) & "utf-8" & Chr(34) & "?>"
                            _step = _step + 1
                        Case Else
                            MsgBox("error 1, exit sub.")
                            Exit Do
                    End Select
                Case 1
                    Select Case lines(i).Trim(" ")
                        Case "<CheatTable>"
                            _step = _step + 1
                        Case Else
                            MsgBox("error 2, exit sub.")
                            Exit Do
                    End Select
                Case 2
                    Select Case lines(i).Trim(" ")
                        Case "<CheatEntries>"
                            _step = _step + 1
                        Case Else
                            MsgBox("error 3, exit sub.")
                            Exit Do
                    End Select
                Case 3
                    If lines(i).Trim(" ") = "<CheatEntry>" Then
                        _step = _step + 1
                    ElseIf lines(i).Trim(" ") = "</CheatEntries>" Then
                        _step = 14 ' jumps up here
                    Else
                        MsgBox("error 3.0, exit sub.")
                    End If
                Case 4
                    Select Case Microsoft.VisualBasic.Left(lines(i).Trim(" "), 4)
                        Case "<ID>"
                            _step = _step + 1
                        Case Else
                            MsgBox("error 3, exit sub.")
                            Exit Do
                    End Select
                Case 5
                    Select Case Microsoft.VisualBasic.Left(lines(i).Trim(" "), 13)
                        Case "<Description>"
                            newlineNumber = newlineNumber + 1
                            ReDim Preserve newline_description(newlineNumber)
                            newline_description(newlineNumber) = lines(i).Replace("<Description>" & Chr(34), "").Replace(Chr(34) & "</Description>", "").Trim(" ")
                            _step = _step + 1
                        Case Else
                            MsgBox("error 3, exit sub.")
                            Exit Do
                    End Select
                Case 6
                    Select Case Microsoft.VisualBasic.Left(lines(i).Trim(" "), 10)
                        Case "<LastState"
                            _step = _step + 1
                        Case Else
                            MsgBox("error 3, exit sub.")
                            Exit Do
                    End Select
                Case 7
                    Select Case Microsoft.VisualBasic.Left(lines(i).Trim(" "), 7)
                        Case "<Color>"
                            _step = _step + 1
                        Case Else
                            MsgBox("error 3, exit sub.")
                            Exit Do
                    End Select
                Case 8
                    Select Case Microsoft.VisualBasic.Left(lines(i).Trim(" "), 14)
                        Case "<VariableType>"
                            ReDim Preserve newline_variableType(newlineNumber)
                            newline_variableType(newlineNumber) = lines(i).Replace("<VariableType>", "").Replace("</VariableType>", "").Trim(" ")
                            _step = _step + 1
                        Case Else
                            MsgBox("error 3, exit sub.")
                            Exit Do
                    End Select
                Case 9
                    Select Case newline_variableType(newlineNumber)
                        Case "4 Bytes"
                            Select Case Microsoft.VisualBasic.Left(lines(i).Trim(" "), 9)
                                Case "<Address>"
                                    ReDim Preserve newline_address(newlineNumber)
                                    newline_address(newlineNumber) = lines(i).Replace("<Address>", "").Replace("</Address>", "").Trim(" ")
                                    _step = _step + 1
                                Case Else
                                    MsgBox("error case 9, exit sub.")
                                    Exit Do
                            End Select
                        Case "String"

                    End Select

                Case 10
                    Select Case Microsoft.VisualBasic.Left(lines(i).Trim(" "), 14)
                        Case "<Offsets>"
                            _step = _step + 1
                        Case Else
                            MsgBox("error 3, exit sub.")
                            Exit Do
                    End Select
                Case 11
                    Select Case Microsoft.VisualBasic.Left(lines(i).Trim(" "), 8)
                        Case "<Offset>"
                            ReDim Preserve newline_offeset(newlineNumber)
                            newline_offeset(newlineNumber) = lines(i).Replace("<Offset>", "").Replace("</Offset>", "").Trim(" ")
                            _step = _step + 1
                        Case "<Offset>"
                            _step = _step + 1
                        Case Else
                            MsgBox("error 3, exit sub.")
                            Exit Do
                    End Select
                Case 12
                    Select Case Microsoft.VisualBasic.Left(lines(i).Trim(" "), 10)
                        Case "</Offsets>"
                            _step = _step + 1
                        Case Else
                            MsgBox("error 3, exit sub.")
                            Exit Do
                    End Select
                Case 13
                    Select Case Microsoft.VisualBasic.Left(lines(i).Trim(" "), 13)
                        Case "</CheatEntry>"
                            _step = 3 ' jumps back here
                        Case Else
                            MsgBox("error 3, exit sub.")
                            Exit Do
                    End Select
                Case 14
                    Select Case Microsoft.VisualBasic.Left(lines(i).Trim(" "), 13)
                        Case "</CheatTable>"
                            _step = _step + 1
                        Case Else
                            MsgBox("error 3, exit sub.")
                            Exit Do
                    End Select

            End Select
            i = i + 1
        Loop
        'TextBox23.Clear()
        i = 1
        'newlineNumber = 0
        Do Until i > newlineNumber
            '    TextBox23.AppendText(newline_address(i) & Chr(34) & "+" & newline_offeset(i) & " -- " & newline_description(i) & " -- type:" & newline_variableType(i))
            '    TextBox23.AppendText(vbNewLine)
            i = i + 1
        Loop
        'TextBox23.AppendText(newline)
    End Sub

End Class