Public Class Kairen_Main

    'Classes
    Dim lb As New CommonLibrary
    Dim NPCFile As TextFileClass
    Dim AreaFile As TextFileClass
    Dim GrabData_NPC As TextFileClass
    Dim GrabData_Area As TextFileClass
    Dim FAPI_Connection As FAPIConnectionManager

    Dim o_NPCFile_Version As String = "Version"
    Dim o_NPCFile_SafeName As String = "Safe Name"
    Dim o_NPCFile_GameName As String = "Game Name"
    Dim o_NPCFile_X As String = "X"
    Dim o_NPCFile_Y As String = "Y"
    Dim o_NPCFile_Z As String = "Z"
    Dim o_NPCFile_F As String = "F"
    Dim o_NPCFile_Race As String = "Race"
    Dim o_NPCFile_Gender As String = "Gender"
    Dim o_NPCFile_Class As String = "Class"
    Dim o_NPCFile_Level As String = "Level"
    Dim o_NPCFile_HP As String = "HP"

    Dim o_AreaFile_Version As String = "Version"
    Dim o_AreaFile_SafeName As String = "Safe Name"
    Dim o_AreaFile_ZoneName As String = "Zone Name"
    Dim o_AreaFile_SubZoneName As String = "SubZone Name"
    Dim o_AreaFile_Xmin As String = "Xmin"
    Dim o_AreaFile_Xmax As String = "Xmax"
    Dim o_AreaFile_Ymin As String = "Ymin"
    Dim o_AreaFile_Ymax As String = "Ymax"
    Dim o_AreaFile_Zmin As String = "Zmin"
    Dim o_AreaFile_Zmax As String = "Zmax"

    Dim FAPIConnectionMode(4) As String

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Try
            Process.Start(lb.IO_Root & "Cheat Engine\MainTable.ct")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Kairen_Main_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Text = "Kairen Prototype Release 11 PROTOTYPE"
        d_btn_BobsMenu.Visible = True
        FAPIConnectionMode(0) = "Disconnected"
        RefreshTab()
    End Sub

#Region "NPC Maker"
    'Program Subs
    Private Sub Mode_NPCMaker_LoadNPC(ByVal _isOn As Boolean)
        If _isOn = True Then
            'Load NPC
            'Check if NPC Text Isn't Blank
            If d_npcmkr_cmbbx_NPCList.Text <> "" Then
                'Check if NPC File Exist
                Dim _filepath As String = lb.Folder_Custom_NPCs & d_npcmkr_cmbbx_NPCList.Text & lb.Extenion_NPC
                If lb.FE(_filepath) Then
                    'File Exists
                    d_npcmkr_btn_Load_NPC.Checked = True
                    LoadNPC(_filepath) 'Load NPC File
                Else
                    'File Doesn't Exist
                    d_npcmkr_btn_Load_NPC.Checked = False 'Don't Check
                    'lb.DisplayMessage("NPC don't exist Lad.", "Sorreh,", NotifyIcon1)
                    'lb.DisplayMessage("NPC " & Chr(34) & d_npcmkr_cmbbx_NPCList.Text & Chr(34) & " does not exist and cannot be loaded.", "Error:", NotifyIcon1)

                    lb.DisplayMessage("NPC " & Chr(34) & d_npcmkr_cmbbx_NPCList.Text & Chr(34) & " does not exist and cannot be loaded.", "Error:", "NPC Maker Load NPC")
                End If
            Else
                lb.DisplayMessage("You have no NPC selected to be loaded.", "Notice: You need to select an NPC to load.", "Kairen - NPC Maker")
            End If
        Else
            'Unload NPC
            UnloadNPC()
        End If
    End Sub
    Private Sub Mode_NPCMaker_NewNPC(ByVal _inOn As Boolean)
        If _inOn = True Then
            'New NPC
            d_npcmkr_btn_New_NPC.Checked = True
            d_npcmkr_btn_Load_NPC.Enabled = False
            'd_npcmkr_cmbbx_NPCList.Enabled = True
            d_npcmkr_btn_Grab_Data.Enabled = True
            'd_npcmkr_btn_Save_File.Enabled = False
            d_npcmkr_btn_Make_File.Enabled = True
            d_npcmkr_cmbbx_NPCList.Enabled = False

            d_npcmkr_tb_Safe_Name.Enabled = True
            d_npcmkr_tb_Game_Name.Enabled = True
            d_npcmkr_tb_X.Enabled = True
            d_npcmkr_tb_Y.Enabled = True
            d_npcmkr_tb_Z.Enabled = True
            d_npcmkr_tb_F.Enabled = True
            'd_npcmkr_tb_Race()
            'd_npcmkr_tb_Gender()
            'd_npcmkr_tb_Class()
            d_npcmkr_tb_Level.Enabled = True
            'd_npcmkr_tb_HP()
        Else
            'Not New NPC
            d_npcmkr_btn_New_NPC.Checked = False
            d_npcmkr_btn_Load_NPC.Enabled = True
            'd_npcmkr_cmbbx_NPCList.Enabled = True
            d_npcmkr_btn_Grab_Data.Enabled = False
            'd_npcmkr_btn_Save_File.Enabled = False
            d_npcmkr_btn_Make_File.Enabled = False
            d_npcmkr_cmbbx_NPCList.Enabled = False

            d_npcmkr_tb_Safe_Name.Enabled = False
            d_npcmkr_tb_Game_Name.Enabled = False
            d_npcmkr_tb_X.Enabled = False
            d_npcmkr_tb_Y.Enabled = False
            d_npcmkr_tb_Z.Enabled = False
            d_npcmkr_tb_F.Enabled = False
            'd_npcmkr_tb_Race()
            'd_npcmkr_tb_Gender()
            'd_npcmkr_tb_Class()
            d_npcmkr_tb_Level.Enabled = False
            'd_npcmkr_tb_HP()
            UnloadNPC()
        End If
        Exit Sub
        NPCFile.AdditonalData(NPCFile.CurrentIndex) = o_NPCFile_Version
        NPCFile.AdditonalData(NPCFile.CurrentIndex) = o_NPCFile_SafeName
        NPCFile.AdditonalData(NPCFile.CurrentIndex) = o_NPCFile_GameName
        NPCFile.AdditonalData(NPCFile.CurrentIndex) = o_NPCFile_X
        NPCFile.AdditonalData(NPCFile.CurrentIndex) = o_NPCFile_Y
        NPCFile.AdditonalData(NPCFile.CurrentIndex) = o_NPCFile_Z
        NPCFile.AdditonalData(NPCFile.CurrentIndex) = o_NPCFile_F
        NPCFile.AdditonalData(NPCFile.CurrentIndex) = o_NPCFile_Race
        NPCFile.AdditonalData(NPCFile.CurrentIndex) = o_NPCFile_Gender
        NPCFile.AdditonalData(NPCFile.CurrentIndex) = o_NPCFile_Class
        NPCFile.AdditonalData(NPCFile.CurrentIndex) = o_NPCFile_Level
        NPCFile.AdditonalData(NPCFile.CurrentIndex) = o_NPCFile_HP
    End Sub
    Private Sub LoadNPC(ByVal _NPCFile As String)
        If lb.FE(_NPCFile) = False Then
            MsgBox("Load NPC Said File Not Exit so I closing bai bais", MsgBoxStyle.Information, "Totally not LoadNPC()")
            Exit Sub
        End If
        NPCFile = New TextFileClass(_NPCFile, "--")
        NPCFile.LoadFile()
        Dim fileversion As String = NPCFile.ReadLine() ' Version

        If fileversion = "0.1.1" Then
            NPCFile.AdditonalData(NPCFile.CurrentIndex) = o_NPCFile_Version
            d_npcmkr_lbl_Version.Text = fileversion

            d_npcmkr_tb_Safe_Name.Enabled = True
            d_npcmkr_tb_Safe_Name.Text = NPCFile.ReadLine
            NPCFile.AdditonalData(NPCFile.CurrentIndex) = o_NPCFile_SafeName

            d_npcmkr_tb_Game_Name.Enabled = True
            d_npcmkr_tb_Game_Name.Text = NPCFile.ReadLine
            NPCFile.AdditonalData(NPCFile.CurrentIndex) = o_NPCFile_GameName

            d_npcmkr_tb_X.Enabled = True
            d_npcmkr_tb_X.Text = NPCFile.ReadLine
            NPCFile.AdditonalData(NPCFile.CurrentIndex) = o_NPCFile_X

            d_npcmkr_tb_Y.Enabled = True
            d_npcmkr_tb_Y.Text = NPCFile.ReadLine
            NPCFile.AdditonalData(NPCFile.CurrentIndex) = o_NPCFile_Y

            d_npcmkr_tb_Z.Enabled = True
            d_npcmkr_tb_Z.Text = NPCFile.ReadLine
            NPCFile.AdditonalData(NPCFile.CurrentIndex) = o_NPCFile_Z

            d_npcmkr_tb_F.Enabled = True
            d_npcmkr_tb_F.Text = NPCFile.ReadLine
            NPCFile.AdditonalData(NPCFile.CurrentIndex) = o_NPCFile_F

            NPCFile.ReadLine() 'd_npcmkr_tb_Race()
            NPCFile.AdditonalData(NPCFile.CurrentIndex) = o_NPCFile_Race

            NPCFile.ReadLine() 'd_npcmkr_tb_Gender()
            NPCFile.AdditonalData(NPCFile.CurrentIndex) = o_NPCFile_Gender

            NPCFile.ReadLine() 'd_npcmkr_tb_Class()
            NPCFile.AdditonalData(NPCFile.CurrentIndex) = o_NPCFile_Class

            d_npcmkr_tb_Level.Enabled = True
            d_npcmkr_tb_Level.Text = NPCFile.ReadLine
            NPCFile.AdditonalData(NPCFile.CurrentIndex) = o_NPCFile_Level


            NPCFile.ReadLine() 'd_npcmkr_tb_HP()
            NPCFile.AdditonalData(NPCFile.CurrentIndex) = o_NPCFile_HP

            d_npcmkr_btn_New_NPC.Enabled = False
            d_npcmkr_cmbbx_NPCList.Enabled = False
            d_npcmkr_btn_Grab_Data.Enabled = True
            d_npcmkr_btn_Save_File.Enabled = True
            'd_npcmkr_btn_Make_File.Enabled = False
        Else
            MsgBox("File Version Not Supported for: " & d_npcmkr_cmbbx_NPCList.SelectedText & "; Version: " & fileversion)
            Exit Sub
        End If
    End Sub
    Private Sub UnloadNPC()
        'Unloads
        NPCFile = Nothing
        'Enables
        d_npcmkr_cmbbx_NPCList.Enabled = True
        d_npcmkr_btn_Grab_Data.Enabled = False
        d_npcmkr_btn_Save_File.Enabled = False
        'd_npcmkr_btn_Make_File.Enabled = True

        'Clears
        d_npcmkr_btn_New_NPC.Enabled = True
        d_npcmkr_lbl_Version.Text = "-"
        d_npcmkr_tb_Safe_Name.Enabled = False
        d_npcmkr_tb_Safe_Name.Text = ""
        d_npcmkr_tb_Game_Name.Enabled = False
        d_npcmkr_tb_Game_Name.Text = ""
        d_npcmkr_tb_X.Enabled = False
        d_npcmkr_tb_X.Text = ""
        d_npcmkr_tb_Y.Enabled = False
        d_npcmkr_tb_Y.Text = ""
        d_npcmkr_tb_Z.Enabled = False
        d_npcmkr_tb_Z.Text = ""
        d_npcmkr_tb_F.Enabled = False
        d_npcmkr_tb_F.Text = ""
        'd_npcmkr_tb_Race()
        'd_npcmkr_tb_Gender()
        'd_npcmkr_tb_Class()
        d_npcmkr_tb_Level.Enabled = False
        d_npcmkr_tb_Level.Text = ""
        'd_npcmkr_tb_HP()
    End Sub
    Private Sub d_npcmkr_btn_Save_File_Click(sender As System.Object, e As System.EventArgs) Handles d_npcmkr_btn_Save_File.Click
        'Check if any boxes are blank, if so, don't continue
        'Check if file name is gonna be different, if so check if Kairen should continue
        'Check if npc name is changing
        'Alert that file was saved
        'If File Name has changed, reload all NPC Lists (ReFreshTab())
        '
        'Check if any boxes are blank, if so, don't continue
        Dim _FileVersion As String = "0.1.1"
        Dim _renameSafeName As MsgBoxResult
        Dim _renameGameName As MsgBoxResult
        Dim _oldFilePath As String
        Select Case _FileVersion
            Case "0.1.1"
                If CheckSaveContinue("NPC Maker", "Save File") = False Then
                    lb.DisplayMessage("All fields need to be filled in before you can save a file.", "Error:", "Kairen NPC Maker")
                    Exit Sub
                End If
                If d_npcmkr_tb_Safe_Name.Text <> NPCFile.GetValueByText(o_NPCFile_SafeName) Then
                    'file name is going to change
                    _renameSafeName = MsgBox("Warning! Changing the Safe Name will change the file's name, do you want to continue?", MsgBoxStyle.YesNo, "Warning: A file's name is changing")
                    If _renameSafeName = MsgBoxResult.Yes Then
                        If lb.FE(lb.Folder_Custom_NPCs & d_npcmkr_tb_Safe_Name.Text & lb.Extenion_NPC) Then
                            MsgBox("Problem: A file already exists with this name. You will need to choose a different one to continue, and you will need to rename the other file before you can use that name.", MsgBoxStyle.Exclamation, "Problem! File Already Exists")
                            Exit Sub
                        End If
                        _oldFilePath = NPCFile.FilePath
                        NPCFile.UpdateValueByText(o_NPCFile_SafeName, d_npcmkr_tb_Safe_Name.Text)
                        NPCFile.FilePath = lb.Folder_Custom_NPCs & d_npcmkr_tb_Safe_Name.Text & lb.Extenion_NPC
                    Else : Exit Sub
                    End If
                End If
                If d_npcmkr_tb_Game_Name.Text <> NPCFile.GetValueByText(o_NPCFile_GameName) Then
                    'npc name is going to change
                    _renameGameName = MsgBox("Warning! You are changing the NPC's name in the game, do you want to continue?", MsgBoxStyle.YesNo, "Warning: You're changing an NPC's name")
                    If _renameGameName = MsgBoxResult.No Then Exit Sub
                End If
                NPCFile.UpdateValueByText(o_NPCFile_SafeName, d_npcmkr_tb_Safe_Name.Text)
                NPCFile.UpdateValueByText(o_NPCFile_GameName, d_npcmkr_tb_Game_Name.Text)
                NPCFile.UpdateValueByText(o_NPCFile_X, d_npcmkr_tb_X.Text)
                NPCFile.UpdateValueByText(o_NPCFile_Y, d_npcmkr_tb_Y.Text)
                NPCFile.UpdateValueByText(o_NPCFile_Z, d_npcmkr_tb_Z.Text)
                NPCFile.UpdateValueByText(o_NPCFile_F, d_npcmkr_tb_F.Text)
                NPCFile.UpdateValueByText(o_NPCFile_Level, d_npcmkr_tb_Level.Text)
                NPCFile.SaveFile()
                lb.DisplayMessage("NPC Saved.", "NPC Saved", "Kairen - NPC Maker")
                If _renameSafeName = MsgBoxResult.Yes Then
                    My.Computer.FileSystem.DeleteFile(_oldFilePath, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
                    d_npcmkr_cmbbx_NPCList.Text = NPCFile.GetValueByText(o_NPCFile_SafeName)
                End If
                RefreshTab()
            Case Else
                'case not programmed
                Exit Sub
        End Select
    End Sub
    Private Sub d_npcmkr_btn_Make_File_Click(sender As System.Object, e As System.EventArgs) Handles d_npcmkr_btn_Make_File.Click
        'Check if any boxes are blank, if so, don't continue
        'Check if file name is already taken, if so check if Kairen should continue
        'If a file is being overwritten, reload all NPC Lists (ReFreshTab())
        'Alert that file was saved
        '
        'Check if any boxes are blank, if so, don't continue
        Dim _filepath As String = lb.Folder_Custom_NPCs & d_npcmkr_tb_Safe_Name.Text & lb.Extenion_NPC
        If CheckSaveContinue("NPC Maker", "Make File") = False Then
            lb.DisplayMessage("All fields need to be filled in before you can save a file.", "Error:", "Kairen NPC Maker")
            Exit Sub
        End If
        'Check if file name is already taken, if so check if Kairen should continue
        If lb.FE(_filepath) Then
            MsgBox("Problem: A file already exists with this name. You will need to choose a different one and also rename the other file before you can use the new file name.", MsgBoxStyle.Exclamation, "Problem! File Already Exists")
            Exit Sub
        End If
        'If a file is being overwritten, reload all NPC Lists (ReFreshTab())
        'Alert that file was saved
        '

        'save file here
        d_npcmkr_lbl_Version.Text = "0.1.1"
        NPCFile = New TextFileClass(_filepath, "--")
        NPCFile.WriteLine(d_npcmkr_lbl_Version.Text)
        NPCFile.WriteLine(d_npcmkr_tb_Safe_Name.Text)
        NPCFile.WriteLine(d_npcmkr_tb_Game_Name.Text)
        NPCFile.WriteLine(d_npcmkr_tb_X.Text)
        NPCFile.WriteLine(d_npcmkr_tb_Y.Text)
        NPCFile.WriteLine(d_npcmkr_tb_Z.Text)
        NPCFile.WriteLine(d_npcmkr_tb_F.Text)
        NPCFile.WriteLine("Human")
        NPCFile.WriteLine("Male")
        NPCFile.WriteLine("Coachman")
        NPCFile.WriteLine(d_npcmkr_tb_Level.Text)
        NPCFile.WriteLine("255")
        NPCFile.SaveFile()
        lb.DisplayMessage("[Test] NPC Created.", "[Test] NPC Created", "Kairen - NPC Maker")
        Dim _tempname As String = d_npcmkr_tb_Safe_Name.Text
        Mode_NPCMaker_NewNPC(False)
        RefreshTab()
        d_npcmkr_cmbbx_NPCList.Text = _tempname
        Mode_NPCMaker_LoadNPC(True)


    End Sub
    Private Sub d_npcmkr_btn_Grab_Data_Click(sender As System.Object, e As System.EventArgs) Handles d_npcmkr_btn_Grab_Data.Click
        d_npcmkr_tb_X.Text = TextBox2.Text
        d_npcmkr_tb_Y.Text = TextBox3.Text
        d_npcmkr_tb_Z.Text = TextBox5.Text
        d_npcmkr_tb_F.Text = TextBox4.Text
        Exit Sub
        If lb.FE(lb.Folder_Net_Streams & "o\Player Data.txt") = False Then
            lb.DisplayMessage("No Player Data file found. Was it created?", "Error:", "NPC Maker Grab Data")
            Exit Sub
        End If
        GrabData_NPC = New TextFileClass(lb.Folder_Net_Streams & "o\Player Data.txt", "--")
        GrabData_NPC.LoadFile()
        d_npcmkr_tb_X.Text = GrabData_NPC.ReadLine
        d_npcmkr_tb_Y.Text = GrabData_NPC.ReadLine
        d_npcmkr_tb_Z.Text = GrabData_NPC.ReadLine
        d_npcmkr_tb_F.Text = GrabData_NPC.ReadLine
        'If lb.FE(lb.Folder_Temp_NPC_Maker & "New_NPC.txt") Then
        '    My.Computer.FileSystem.DeleteFile(lb.Folder_Temp_NPC_Maker & "New_NPC.txt")
        'End If
        GrabData_NPC = Nothing
    End Sub
    'Display Handles
    Private Sub d_npcmkr_btn_Load_NPC_Click(sender As System.Object, e As System.EventArgs) Handles d_npcmkr_btn_Load_NPC.Click
        Mode_NPCMaker_LoadNPC(sender.Checked)
    End Sub 'Load NPC Button Clicked;
    Private Sub d_npcmkr_btn_New_NPC_Click(sender As System.Object, e As System.EventArgs) Handles d_npcmkr_btn_New_NPC.Click
        Mode_NPCMaker_NewNPC(sender.Checked)
    End Sub 'New NPC Button Clicked;
#End Region
#Region "Both"
    Private Sub RefreshTab()
        'Have Class For Writing And Reading To Display By Their Values, For NPC Maker and Area Maker
        'Check if NPC ComboBox Is Disabled

        'Load NPCs to ComboBox
        Dim NPCsList_Custom() As String
        NPCsList_Custom = lb.ReturnFilesFromFolder(lb.Folder_Custom_NPCs, lb.Extenion_NPC)
        lb.AddItemsToControl(NPCsList_Custom, d_npcmkr_cmbbx_NPCList)
        lb.AddItemsToControl(NPCsList_Custom, ListBox1)

        'Load Area Files to ComboBox
        'Dim _ZoneList() As String
        'Dim i As Integer = 0
        d_areamkr_cmbbx_Zone_Name.Items.Clear()
        For Each _folderName In System.IO.Directory.GetDirectories(lb.Folder_Custom_Zones)
            'ReDim Preserve _ZoneList(i)
            '_ZoneList(i) = folderName
            'i = i + 1
            d_areamkr_cmbbx_Zone_Name.Items.Add(_folderName.Replace(lb.Folder_Custom_Zones, ""))
        Next

    End Sub 'Refresh Tab
    Private Function CheckSaveContinue(ByVal _CheckObject As String, ByVal _CheckContinuationType As String)
        If _CheckObject = "NPC Maker" Then
            Select Case _CheckContinuationType
                Case "Save File"
                    If d_npcmkr_lbl_Version.Text = "" Or _
                        d_npcmkr_tb_Safe_Name.Text = "" Or _
                        d_npcmkr_tb_Game_Name.Text = "" Or _
                        d_npcmkr_tb_X.Text = "" Or _
                        d_npcmkr_tb_Y.Text = "" Or _
                        d_npcmkr_tb_Z.Text = "" Or _
                        d_npcmkr_tb_F.Text = "" Or _
                        d_npcmkr_tb_Level.Text = "" Then
                        Return False
                    Else
                        Return True
                    End If
                Case "Make File"
                    If d_npcmkr_lbl_Version.Text = "" Or _
                        d_npcmkr_tb_Safe_Name.Text = "" Or _
                        d_npcmkr_tb_Game_Name.Text = "" Or _
                        d_npcmkr_tb_X.Text = "" Or _
                        d_npcmkr_tb_Y.Text = "" Or _
                        d_npcmkr_tb_Z.Text = "" Or _
                        d_npcmkr_tb_F.Text = "" Or _
                        d_npcmkr_tb_Level.Text = "" Then
                        Return False
                    Else
                        Return True
                    End If
            End Select
        ElseIf _CheckObject = "Area Maker" Then
            Select Case _CheckContinuationType
                Case "Save File"
                    If d_areamkr_cmbbx_Zone_Name.Text = "" Or d_areamkr_cmbbx_SubZone_Name.Text = "" Or d_areamkr_cmbbx_Location_Name.Text = "" Then
                        lb.DisplayMessage("Zone Data and Area Name fields cannot be blank.", "Error: Missing Data", "Kairen - Area Maker")
                        Return False
                    End If
                    If d_areamkr_tb_X1.Text = "" Or _
                        d_areamkr_tb_Y1.Text = "" Or _
                        d_areamkr_tb_Z1.Text = "" Or _
                        d_areamkr_tb_X4.Text = "" Or _
                        d_areamkr_tb_Y4.Text = "" Or _
                        d_areamkr_tb_Z4.Text = "" Then
                        'if any blank, don't continue
                        'MsgBox("1s and 4s not set")
                        Return False
                    Else
                        If d_areamkr_tb_X2.Text <> "" And _
                            d_areamkr_tb_Y2.Text <> "" And _
                            d_areamkr_tb_Z2.Text <> "" And _
                            d_areamkr_tb_X3.Text <> "" And _
                            d_areamkr_tb_Y3.Text <> "" And _
                            d_areamkr_tb_Z3.Text <> "" Then
                            'if none are blank, then you need to convert then continue

                            Dim order() As Integer
                            order = SortLeastToGreatest(d_areamkr_tb_X1.Text, d_areamkr_tb_X2.Text, d_areamkr_tb_X3.Text, d_areamkr_tb_X4.Text)
                            'AreaFile.UpdateValueByText(o_AreaFile_Xmin, order(0))
                            'AreaFile.UpdateValueByText(o_AreaFile_Xmin, order(3))
                            'd_areamkr_tb_X1.Text = AreaFile.GetValueByText(o_AreaFile_Xmin)
                            d_areamkr_tb_X1.Text = order(0)
                            d_areamkr_tb_X4.Text = order(3)

                            order = SortLeastToGreatest(d_areamkr_tb_Y1.Text, d_areamkr_tb_Y2.Text, d_areamkr_tb_Y3.Text, d_areamkr_tb_Y4.Text)
                            'AreaFile.UpdateValueByText(o_AreaFile_Ymin, order(0))
                            'AreaFile.UpdateValueByText(o_AreaFile_Ymin, order(3))
                            d_areamkr_tb_Y1.Text = order(0)
                            d_areamkr_tb_Y4.Text = order(3)

                            order = SortLeastToGreatest(d_areamkr_tb_Z1.Text, d_areamkr_tb_Z2.Text, d_areamkr_tb_Z3.Text, d_areamkr_tb_Z4.Text)
                            'AreaFile.UpdateValueByText(o_AreaFile_Zmin, order(0))
                            'AreaFile.UpdateValueByText(o_AreaFile_Zmin, order(3))
                            d_areamkr_tb_Z1.Text = order(0)
                            d_areamkr_tb_Z4.Text = order(3)

                            d_areamkr_tb_Y2.Text = ""
                            d_areamkr_tb_Z2.Text = ""
                            d_areamkr_tb_X3.Text = ""
                            d_areamkr_tb_Y3.Text = ""
                            d_areamkr_tb_Z3.Text = ""

                            Return True
                        ElseIf d_areamkr_tb_X2.Text = "" Or _
                            d_areamkr_tb_Y2.Text = "" Or _
                            d_areamkr_tb_Z2.Text = "" Or _
                            d_areamkr_tb_X3.Text = "" Or _
                            d_areamkr_tb_Y3.Text = "" Or _
                            d_areamkr_tb_Z3.Text = "" Then
                            'if some are blank, then don't convert, but continue
                            Return True
                        Else
                            'if some are blank, don't convert
                            'MsgBox("what convert error")
                            lb.DisplayMessage("At least the Min and Max values need to be provided in order to update an area.", "Error: Values Missing", "Kairen - Area Maker")
                            Return False
                        End If
                    End If
                Case "Make File"
                    If d_areamkr_cmbbx_Zone_Name.Text = "" Or d_areamkr_cmbbx_SubZone_Name.Text = "" Or d_areamkr_cmbbx_Location_Name.Text = "" Then
                        lb.DisplayMessage("Zone Data and Area Name fields cannot be blank.", "Error: Missing Data", "Kairen - Area Maker")
                        Return False
                    End If
                    If d_areamkr_tb_X1.Text = "" Or _
                        d_areamkr_tb_Y1.Text = "" Or _
                        d_areamkr_tb_Z1.Text = "" Or _
                        d_areamkr_tb_X4.Text = "" Or _
                        d_areamkr_tb_Y4.Text = "" Or _
                        d_areamkr_tb_Z4.Text = "" Then
                        'if any blank, don't continue
                        'MsgBox("1s and 4s not set")
                        Return False
                    Else
                        If d_areamkr_tb_X2.Text <> "" And _
                            d_areamkr_tb_Y2.Text <> "" And _
                            d_areamkr_tb_Z2.Text <> "" And _
                            d_areamkr_tb_X3.Text <> "" And _
                            d_areamkr_tb_Y3.Text <> "" And _
                            d_areamkr_tb_Z3.Text <> "" Then
                            'if none are blank, then you need to convert then continue

                            Dim order() As Integer
                            order = SortLeastToGreatest(d_areamkr_tb_X1.Text, d_areamkr_tb_X2.Text, d_areamkr_tb_X3.Text, d_areamkr_tb_X4.Text)
                            'AreaFile.UpdateValueByText(o_AreaFile_Xmin, order(0))
                            'AreaFile.UpdateValueByText(o_AreaFile_Xmin, order(3))
                            'd_areamkr_tb_X1.Text = AreaFile.GetValueByText(o_AreaFile_Xmin)
                            d_areamkr_tb_X1.Text = order(0)
                            d_areamkr_tb_X4.Text = order(3)

                            order = SortLeastToGreatest(d_areamkr_tb_Y1.Text, d_areamkr_tb_Y2.Text, d_areamkr_tb_Y3.Text, d_areamkr_tb_Y4.Text)
                            'AreaFile.UpdateValueByText(o_AreaFile_Ymin, order(0))
                            'AreaFile.UpdateValueByText(o_AreaFile_Ymin, order(3))
                            d_areamkr_tb_Y1.Text = order(0)
                            d_areamkr_tb_Y4.Text = order(3)

                            order = SortLeastToGreatest(d_areamkr_tb_Z1.Text, d_areamkr_tb_Z2.Text, d_areamkr_tb_Z3.Text, d_areamkr_tb_Z4.Text)
                            'AreaFile.UpdateValueByText(o_AreaFile_Zmin, order(0))
                            'AreaFile.UpdateValueByText(o_AreaFile_Zmin, order(3))
                            d_areamkr_tb_Z1.Text = order(0)
                            d_areamkr_tb_Z4.Text = order(3)

                            d_areamkr_tb_Y2.Text = ""
                            d_areamkr_tb_Z2.Text = ""
                            d_areamkr_tb_X3.Text = ""
                            d_areamkr_tb_Y3.Text = ""
                            d_areamkr_tb_Z3.Text = ""

                            Return True
                        ElseIf d_areamkr_tb_X2.Text = "" And _
                            d_areamkr_tb_Y2.Text = "" And _
                            d_areamkr_tb_Z2.Text = "" And _
                            d_areamkr_tb_X3.Text = "" And _
                            d_areamkr_tb_Y3.Text = "" And _
                            d_areamkr_tb_Z3.Text = "" Then
                            'if all are blank, then don't convert, but continue
                            Return True
                        Else
                            'if some are blank, don't convert
                            'MsgBox("what convert error"
                            lb.DisplayMessage("At least the Min and Max values need to be provided in order to create an area.", "Error: Values Missing", "Kairen - Area Maker")
                            Return False
                        End If
                    End If

            End Select
        End If
        lb.DisplayMessage("No CheckSaveContinue() condition found.", "Error!", "Kairen - NPC Maker")
        Return False
    End Function
#End Region

    'Program Subs
    'Load Area
    Private Sub Mode_AreaMaker_LoadArea(ByVal _isOn As Boolean)
        If _isOn Then
            'Dim _ZoneIndex As String = lb.Folder_Custom_Zones
            'check if zone index exists..
            'If lb.FE(_ZoneIndex) Then
            'zone index exists
            'd_areamkr_cmbbx_SubZone_Name.Enabled = True
            'Else
            'zone index does not exist
            'End If
            Dim _AreaFilePath As String = lb.Folder_Custom_Zones & d_areamkr_cmbbx_Zone_Name.Text & "\" & d_areamkr_cmbbx_SubZone_Name.Text & "\" & d_areamkr_cmbbx_Location_Name.Text & ".txt"
            'check if file exists..
            If lb.FE(_AreaFilePath) Then
                'does exist
                d_areamkr_btn_New_Area.Enabled = False
                d_areamkr_btn_Grab_Data.Enabled = True
                d_areamkr_btn_Grab_Data_V2.Enabled = True
                d_areamkr_tb_GrabDataV2_Offset.Enabled = True
                d_areamkr_btn_Load_Area.Checked = True
                'd_areamkr_cmbbx_Zone_Name.Enabled = False
                'd_areamkr_cmbbx_SubZone_Name.Enabled = False
                'd_areamkr_cmbbx_Location_Name.Enabled = False
                d_areamkr_tb_X1.Enabled = True
                d_areamkr_tb_X2.Enabled = True
                d_areamkr_tb_X3.Enabled = True
                d_areamkr_tb_X4.Enabled = True
                d_areamkr_tb_Y1.Enabled = True
                d_areamkr_tb_Y2.Enabled = True
                d_areamkr_tb_Y3.Enabled = True
                d_areamkr_tb_Y4.Enabled = True
                d_areamkr_tb_Z1.Enabled = True
                d_areamkr_tb_Z2.Enabled = True
                d_areamkr_tb_Z3.Enabled = True
                d_areamkr_tb_Z4.Enabled = True
                d_areamkr_btn_Add_NPC.Enabled = True
                d_areamkr_btn_Remove_NPC.Enabled = True
                d_areamkr_btn_Save_File.Enabled = True
                LoadAreaFile(_AreaFilePath)
            Else
                'does not exist
                lb.DisplayMessage("Area File does not exist and could not be loaded.", "Error: Area File Not Found", "Kairen - Area Maker")
                d_areamkr_btn_Load_Area.Checked = False
            End If
        Else
            AreaFile = Nothing
            d_areamkr_btn_Load_Area.Checked = False
            d_areamkr_btn_Grab_Data.Enabled = False
            d_areamkr_btn_Grab_Data_V2.Enabled = False
            d_areamkr_tb_GrabDataV2_Offset.Enabled = False
            d_areamkr_btn_New_Area.Enabled = True
            'd_areamkr_cmbbx_Zone_Name.Enabled = True
            'd_areamkr_cmbbx_SubZone_Name.Enabled = True
            'd_areamkr_cmbbx_Location_Name.Enabled = True
            d_areamkr_tb_X1.Enabled = False
            d_areamkr_tb_X2.Enabled = False
            d_areamkr_tb_X3.Enabled = False
            d_areamkr_tb_X4.Enabled = False
            d_areamkr_tb_Y1.Enabled = False
            d_areamkr_tb_Y2.Enabled = False
            d_areamkr_tb_Y3.Enabled = False
            d_areamkr_tb_Y4.Enabled = False
            d_areamkr_tb_Z1.Enabled = False
            d_areamkr_tb_Z2.Enabled = False
            d_areamkr_tb_Z3.Enabled = False
            d_areamkr_tb_Z4.Enabled = False
            d_areamkr_btn_Add_NPC.Enabled = False
            d_areamkr_btn_Remove_NPC.Enabled = False
            d_areamkr_btn_Save_File.Enabled = False


            d_areamkr_tb_GrabDataV2_Offset.Text = ""
            d_areamkr_tb_X1.Text = ""
            d_areamkr_tb_X2.Text = ""
            d_areamkr_tb_X3.Text = ""
            d_areamkr_tb_X4.Text = ""
            d_areamkr_tb_Y1.Text = ""
            d_areamkr_tb_Y2.Text = ""
            d_areamkr_tb_Y3.Text = ""
            d_areamkr_tb_Y4.Text = ""
            d_areamkr_tb_Z1.Text = ""
            d_areamkr_tb_Z2.Text = ""
            d_areamkr_tb_Z3.Text = ""
            d_areamkr_tb_Z4.Text = ""
            ListBox2.Items.Clear()
        End If
    End Sub
    Private Sub LoadAreaFile(ByVal _filePath As String)
        AreaFile = New TextFileClass(_filePath, "--")
        AreaFile.LoadFile()
        Dim _fileVersion As String = AreaFile.ReadLine 'file version
        AreaFile.AdditonalData(AreaFile.CurrentIndex) = o_AreaFile_Version
        Select Case _fileVersion
            Case "1.1"
                AreaFile.ReadLine() 'safe name / file name ' check if wrong
                AreaFile.AdditonalData(AreaFile.CurrentIndex) = o_AreaFile_SafeName

                AreaFile.ReadLine() 'zone name ' check if wrong
                AreaFile.AdditonalData(AreaFile.CurrentIndex) = o_AreaFile_ZoneName

                AreaFile.ReadLine() 'subzone name ' check if wrong
                AreaFile.AdditonalData(AreaFile.CurrentIndex) = o_AreaFile_SubZoneName

                d_areamkr_tb_X1.Text = AreaFile.ReadLine
                AreaFile.AdditonalData(AreaFile.CurrentIndex) = o_AreaFile_Xmin

                d_areamkr_tb_X4.Text = AreaFile.ReadLine
                AreaFile.AdditonalData(AreaFile.CurrentIndex) = o_AreaFile_Xmax

                d_areamkr_tb_Y1.Text = AreaFile.ReadLine
                AreaFile.AdditonalData(AreaFile.CurrentIndex) = o_AreaFile_Ymin

                d_areamkr_tb_Y4.Text = AreaFile.ReadLine
                AreaFile.AdditonalData(AreaFile.CurrentIndex) = o_AreaFile_Ymax

                d_areamkr_tb_Z1.Text = AreaFile.ReadLine
                AreaFile.AdditonalData(AreaFile.CurrentIndex) = o_AreaFile_Zmin

                d_areamkr_tb_Z4.Text = AreaFile.ReadLine
                AreaFile.AdditonalData(AreaFile.CurrentIndex) = o_AreaFile_Zmax

                Dim _npc As String = AreaFile.ReadLine
                Do Until AreaFile.CurrentIndex > AreaFile.NumberOfLines
                    If _npc <> Nothing Then
                        ListBox2.Items.Add(_npc)
                    End If
                    _npc = AreaFile.ReadLine
                Loop


            Case Else

        End Select
    End Sub

    'New Area
    Private Sub Mode_AreaMaker_NewArea(ByVal _isOn As Boolean)
        If _isOn = True Then
            'On
            d_areamkr_btn_Grab_Data.Enabled = True
            d_areamkr_btn_Grab_Data_V2.Enabled = True
            d_areamkr_tb_GrabDataV2_Offset.Enabled = True
            d_areamkr_btn_Load_Area.Enabled = False
            RadioButton1.Checked = True
            d_areamkr_tb_X1.Enabled = True
            d_areamkr_tb_X2.Enabled = True
            d_areamkr_tb_X3.Enabled = True
            d_areamkr_tb_X4.Enabled = True
            d_areamkr_tb_Y1.Enabled = True
            d_areamkr_tb_Y2.Enabled = True
            d_areamkr_tb_Y3.Enabled = True
            d_areamkr_tb_Y4.Enabled = True
            d_areamkr_tb_Z1.Enabled = True
            d_areamkr_tb_Z2.Enabled = True
            d_areamkr_tb_Z3.Enabled = True
            d_areamkr_tb_Z4.Enabled = True
            d_areamkr_btn_Add_NPC.Enabled = True
            d_areamkr_btn_Remove_NPC.Enabled = True
            d_areamkr_btn_Make_File.Enabled = True
        Else
            'Off
            d_areamkr_btn_Grab_Data.Enabled = False
            d_areamkr_btn_Grab_Data_V2.Enabled = False
            d_areamkr_tb_GrabDataV2_Offset.Enabled = True
            d_areamkr_tb_GrabDataV2_Offset.Text = ""
            d_areamkr_btn_New_Area.Checked = False
            d_areamkr_btn_Load_Area.Enabled = True
            RadioButton1.Checked = True
            RadioButton1.Checked = False
            d_areamkr_tb_X1.Enabled = False
            d_areamkr_tb_X2.Enabled = False
            d_areamkr_tb_X3.Enabled = False
            d_areamkr_tb_X4.Enabled = False
            d_areamkr_tb_Y1.Enabled = False
            d_areamkr_tb_Y2.Enabled = False
            d_areamkr_tb_Y3.Enabled = False
            d_areamkr_tb_Y4.Enabled = False
            d_areamkr_tb_Z1.Enabled = False
            d_areamkr_tb_Z2.Enabled = False
            d_areamkr_tb_Z3.Enabled = False
            d_areamkr_tb_Z4.Enabled = False
            d_areamkr_btn_Add_NPC.Enabled = False
            d_areamkr_btn_Remove_NPC.Enabled = False
            d_areamkr_btn_Make_File.Enabled = False
            ListBox2.Items.Clear()
        End If
    End Sub

    'Display Handles
    Private Sub d_areamkr_btn_Load_Area_Click(sender As System.Object, e As System.EventArgs) Handles d_areamkr_btn_Load_Area.Click
        Mode_AreaMaker_LoadArea(sender.Checked)
    End Sub
    Private Sub d_areamkr_btn_New_Area_Click(sender As System.Object, e As System.EventArgs) Handles d_areamkr_btn_New_Area.Click
        Mode_AreaMaker_NewArea(sender.Checked)
    End Sub

    'ZoneName Text Changed; SubZone Text Changed; Area Name Text Changed;
    Private Sub d_areamkr_cmbbx_Zone_Name_TextChanged(sender As System.Object, e As System.EventArgs) Handles d_areamkr_cmbbx_Zone_Name.TextChanged
        'check mode
        If d_areamkr_btn_Load_Area.Checked = False Then
            'no mode
            Dim _folderToSearch As String = lb.Folder_Custom_Zones & "\" & d_areamkr_cmbbx_Zone_Name.Text & "\"
            If lb.DE(_folderToSearch) = False Then Exit Sub
            d_areamkr_cmbbx_SubZone_Name.Items.Clear()
            For Each _folderName In System.IO.Directory.GetDirectories(_folderToSearch)
                'ReDim Preserve _ZoneList(i)
                '_ZoneList(i) = folderName
                'i = i + 1
                d_areamkr_cmbbx_SubZone_Name.Items.Add(_folderName.Replace(_folderToSearch, ""))
            Next
        ElseIf d_areamkr_btn_Load_Area.Checked = True Then
            'load area mode
            Dim _folderToSearch As String = lb.Folder_Custom_Zones & "\" & d_areamkr_cmbbx_Zone_Name.Text & "\"
            If lb.DE(_folderToSearch) = False Then Exit Sub
            d_areamkr_cmbbx_SubZone_Name.Items.Clear()
            For Each _folderName In System.IO.Directory.GetDirectories(_folderToSearch)
                'ReDim Preserve _ZoneList(i)
                '_ZoneList(i) = folderName
                'i = i + 1
                d_areamkr_cmbbx_SubZone_Name.Items.Add(_folderName.Replace(_folderToSearch, ""))
            Next
        Else
            'error unprogrammed mode
        End If

    End Sub
    Private Sub d_areamkr_cmbbx_SubZone_Name_TextChanged(sender As System.Object, e As System.EventArgs) Handles d_areamkr_cmbbx_SubZone_Name.TextChanged
        'check mode
        If d_areamkr_btn_Load_Area.Checked = False Then
            'no mode
            Dim _folderToSearch As String = lb.Folder_Custom_Zones & d_areamkr_cmbbx_Zone_Name.Text & "\" & d_areamkr_cmbbx_SubZone_Name.Text & "\"
            If lb.DE(_folderToSearch) = False Then Exit Sub
            d_areamkr_cmbbx_Location_Name.Items.Clear()
            For Each _fileName In lb.ReturnFilesFromFolder(_folderToSearch, ".txt")
                'ReDim Preserve _ZoneList(i)
                '_ZoneList(i) = folderName
                'i = i + 1
                d_areamkr_cmbbx_Location_Name.Items.Add(_fileName.Replace(_folderToSearch, "").Replace(".txt", ""))
            Next
        ElseIf d_areamkr_btn_Load_Area.Checked = True Then
            'load area mode
        Else
            'error unprogrammed mode
        End If


    End Sub
    Private Sub d_areamkr_cmbbx_Location_Name_TextChanged(sender As System.Object, e As System.EventArgs) Handles d_areamkr_cmbbx_Location_Name.TextChanged
        'check mode
        If d_areamkr_btn_Load_Area.Checked = False Then
            'no mode

        ElseIf d_areamkr_btn_Load_Area.Checked = True Then
            'load area mode
        Else
            'error unprogrammed mode
        End If
    End Sub

    Private Sub d_areamkr_btn_Save_File_Click(sender As System.Object, e As System.EventArgs) Handles d_areamkr_btn_Save_File.Click
        If CheckSaveContinue("Area Maker", "Save File") = False Then
            lb.DisplayMessage("All fields need to be filled in before you can save a file.", "Error:", "Kairen - Area Maker")
            Exit Sub
        End If
        Dim _responseSafeName, _responseZone, _responseSubZone As MsgBoxResult
        If AreaFile.GetValueByText(o_AreaFile_SafeName) <> d_areamkr_cmbbx_Location_Name.Text Then
            _responseSafeName = MsgBox("Warning: Changing the Area's name will also change it's File Name. Do you want to continue?", MsgBoxStyle.YesNo, "Warning: Change File Name?")
            If _responseSafeName = MsgBoxResult.No Then
                lb.DisplayMessage("Save Aborted", "Save Aborted`", "Kairen - Area Maker", , 2500)
                Exit Sub
            End If
        End If
        If AreaFile.GetValueByText(o_AreaFile_ZoneName) <> d_areamkr_cmbbx_Zone_Name.Text Then
            _responseZone = MsgBox("Warning: You are changing the Zone that this area is located in. Do you want to continue?", MsgBoxStyle.YesNo, "Warning: Change Containing Zone?")
            If _responseZone = MsgBoxResult.No Then
                lb.DisplayMessage("Save Aborted", "Save Aborted`", "Kairen - Area Maker", , 2500)
                Exit Sub
            End If
        End If
        If AreaFile.GetValueByText(o_AreaFile_SubZoneName) <> d_areamkr_cmbbx_SubZone_Name.Text Then
            _responseSubZone = MsgBox("Warning: You are changing the SubZone this area is located in. Do you want to continue?" & vbNewLine & _
                               "(This does not mean the area is or isn't being moved to a new zone.)", MsgBoxStyle.YesNo, "Warning: ")
            If _responseSubZone = MsgBoxResult.No Then
                lb.DisplayMessage("Save Aborted", "Save Aborted`", "Kairen - Area Maker", , 2500)
                Exit Sub
            End If
        End If

        'if file is changing locations..
        ''Check for file conflicts here, check if new folders need created,
        ''change file path, delete old file, check if old folder needs deleted [is empty]
        ''continue

        'if file is changing locations..
        Dim _deleteOldFile As Boolean = False
        Dim _oldFilePath As String
        Dim _newFilePath As String = lb.Folder_Custom_Zones & d_areamkr_cmbbx_Zone_Name.Text & "/" & d_areamkr_cmbbx_SubZone_Name.Text & "/" & d_areamkr_cmbbx_Location_Name.Text & lb.Extenion_Area
        If (_responseZone Or _responseSubZone Or _responseSafeName) And (lb.FE(_newFilePath)) Then
            MsgBox("Problem: A file already exists with this name. You will need to choose a different one to continue, and you will need to rename the other file before you can use that name.", MsgBoxStyle.Exclamation, "Problem! File Already Exists")
            Exit Sub
        ElseIf (_responseZone Or _responseSubZone Or _responseSafeName) And (lb.FE(_newFilePath)) = False Then
            MsgBox("file name is free!")
            'update file path
            _oldFilePath = AreaFile.FilePath
            _deleteOldFile = True
        Else
            'MsgBox("save not abortable error")
        End If

        AreaFile = New TextFileClass(_newFilePath, "--")
        AreaFile.WriteLine("1.1")
        AreaFile.WriteLine(d_areamkr_cmbbx_Location_Name.Text)
        AreaFile.WriteLine(d_areamkr_cmbbx_Zone_Name.Text)
        AreaFile.WriteLine(d_areamkr_cmbbx_SubZone_Name.Text)

        AreaFile.WriteLine(d_areamkr_tb_X1.Text)
        AreaFile.WriteLine(d_areamkr_tb_X4.Text)

        AreaFile.WriteLine(d_areamkr_tb_Y1.Text)
        AreaFile.WriteLine(d_areamkr_tb_Y4.Text)

        AreaFile.WriteLine(d_areamkr_tb_Z1.Text)
        AreaFile.WriteLine(d_areamkr_tb_Z4.Text)

        For Each item In ListBox2.Items
            AreaFile.WriteLine(item)
        Next

        AreaFile.SaveFile()

        If _deleteoldfile = True Then
            My.Computer.FileSystem.DeleteFile(_oldFilePath, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
            'check if old folder is empty and needs deleted
        End If
        lb.DisplayMessage("Area File Updated", "File Saved", "Kairen - Area Maker")
        Mode_AreaMaker_LoadArea(False)
        RefreshTab()
        Mode_AreaMaker_LoadArea(True)
        'alert saved
    End Sub
    Private Function SortLeastToGreatest(ByVal input1 As Integer, ByVal input2 As Integer, ByVal input3 As Integer, ByVal input4 As Integer)
        Dim input(3) As Integer
        input(0) = input1
        input(1) = input2
        input(2) = input3
        input(3) = input4
        Dim i As Integer = 0
        Dim i2 As Integer = 0
        Dim temp As Integer
        Do While i2 <= 2
            Do While i <= 2
                If input(i) > input(i + 1) Then
                    temp = input(i)
                    input(i) = input(i + 1)
                    input(i + 1) = temp
                End If
                i = i + 1
            Loop
            i = 0
            i2 = i2 + 1
        Loop
        Return input
    End Function

    Private Sub d_areamkr_btn_Grab_Data_Click(sender As System.Object, e As System.EventArgs) Handles d_areamkr_btn_Grab_Data.Click
        'Label32.Text = "1.0"
        Dim x, y, z, f, Zone, ZoneSub As String
        Dim file As String = lb.Folder_Net_Streams & "o\Player Data.txt"
        If lb.FE(file) = False Then
            lb.DisplayMessage("No Player Data file found. Was it created?", "Error:", "Kairen - Area Maker")
            Exit Sub
        End If
        Dim sr As New IO.StreamReader(file)
        x = sr.ReadLine()
        y = sr.ReadLine()
        z = sr.ReadLine()
        Zone = sr.ReadLine()
        ZoneSub = sr.ReadLine()
        sr.Close()
        Dim FilledWallNumber As Integer = 0
        If RadioButton1.Checked = True Then
            d_areamkr_tb_X1.Text = x
            d_areamkr_tb_Y1.Text = y
            d_areamkr_tb_Z1.Text = z
            RadioButton2.Checked = True
            FilledWallNumber = 1
        ElseIf RadioButton2.Checked = True Then
            d_areamkr_tb_X2.Text = x
            d_areamkr_tb_Y2.Text = y
            d_areamkr_tb_Z2.Text = z
            RadioButton3.Checked = True
            FilledWallNumber = 2
        ElseIf RadioButton3.Checked = True Then
            d_areamkr_tb_X3.Text = x
            d_areamkr_tb_Y3.Text = y
            d_areamkr_tb_Z3.Text = z
            RadioButton4.Checked = True
            FilledWallNumber = 3
        ElseIf RadioButton4.Checked = True Then
            d_areamkr_tb_X4.Text = x
            d_areamkr_tb_Y4.Text = y
            d_areamkr_tb_Z4.Text = z
            RadioButton4.Checked = False
            FilledWallNumber = 4
        End If
        'Dim output(4) As String
        'output(0) = FilledWallNumber
        'output(1) = x
        'output(2) = y
        'output(3) = z
        ' output(4) = z
        'ParseOutsideCommand("Spawn Wall Marker", output)
        If d_areamkr_cmbbx_Zone_Name.Items.Contains(Zone) = False Then
            d_areamkr_cmbbx_Zone_Name.Items.Add(Zone)
        End If
        If d_areamkr_cmbbx_SubZone_Name.Items.Contains(ZoneSub) = False Then
            d_areamkr_cmbbx_SubZone_Name.Items.Add(ZoneSub)
        End If
        'If lb.FE(lb.Folder_Temp_NPC_Maker & "New_Wall.txt") Then
        '    My.Computer.FileSystem.DeleteFile(lb.Folder_Temp_NPC_Maker & "New_Wall.txt")
        'End If
    End Sub

    Private Sub d_areamkr_btn_Grab_Data_V2_Click(sender As System.Object, e As System.EventArgs) Handles d_areamkr_btn_Grab_Data_V2.Click
        'Label32.Text = "1.0"
        Dim x, y, z, f, Zone, ZoneSub As String
        Dim file As String = lb.Folder_Net_Streams & "o\Player Data.txt"
        If lb.FE(file) = False Then
            lb.DisplayMessage("No Player Data file found. Was it created?", "Error:", "Kairen - Area Maker")
            Exit Sub
        End If
        If d_areamkr_tb_GrabDataV2_Offset.Text = "" Then
            lb.DisplayMessage("You must provide an offset to use this button.", "Please Provide an Offset", "Kairen - Area Maker")
            Exit Sub
        End If
        Dim offset As Integer = d_areamkr_tb_GrabDataV2_Offset.Text
        Dim sr As New IO.StreamReader(file)
        x = sr.ReadLine()
        y = sr.ReadLine()
        z = sr.ReadLine()
        Zone = sr.ReadLine()
        ZoneSub = sr.ReadLine()
        sr.Close()
        Dim FilledWallNumber As Integer = 0

        d_areamkr_tb_X1.Text = x - offset
        d_areamkr_tb_Y1.Text = y - offset
        d_areamkr_tb_Z1.Text = z - offset

        d_areamkr_tb_X4.Text = x + offset
        d_areamkr_tb_Y4.Text = y + offset
        d_areamkr_tb_Z4.Text = z + offset
        RadioButton1.Checked = True
        RadioButton1.Checked = False

        'ParseOutsideCommand("Spawn Wall Marker", output)
        If d_areamkr_cmbbx_Zone_Name.Items.Contains(Zone) = False Then
            d_areamkr_cmbbx_Zone_Name.Items.Add(Zone)
        End If
        If d_areamkr_cmbbx_SubZone_Name.Items.Contains(ZoneSub) = False Then
            d_areamkr_cmbbx_SubZone_Name.Items.Add(ZoneSub)
        End If
        'If lb.FE(lb.Folder_Temp_NPC_Maker & "New_Wall.txt") Then
        'My.Computer.FileSystem.DeleteFile(lb.Folder_Temp_NPC_Maker & "New_Wall.txt")
        'End If
    End Sub

    Private Sub d_spwnmkr_btn_Make_File_Click(sender As System.Object, e As System.EventArgs) Handles d_areamkr_btn_Make_File.Click
        If CheckSaveContinue("Area Maker", "Make File") = False Then
            lb.DisplayMessage("All fields need to be filled in before you can save a file.", "Error:", "Kairen - Area Maker")
            Exit Sub
        End If

        Dim _newFilePath As String = lb.Folder_Custom_Zones & d_areamkr_cmbbx_Zone_Name.Text & "/" & d_areamkr_cmbbx_SubZone_Name.Text & "/" & d_areamkr_cmbbx_Location_Name.Text & lb.Extenion_Area
        If lb.FE(_newFilePath) Then
            MsgBox("Problem: A file already exists with this name. You will need to choose a different one to continue, and you will need to rename the other file before you can use that name.", MsgBoxStyle.Exclamation, "Problem! File Already Exists")
            Exit Sub
        Else
            'MsgBox("file name is free!")
            'update file path
            AreaFile = New TextFileClass(_newFilePath, "--")
            If lb.DE(lb.Folder_Custom_Zones & d_areamkr_cmbbx_Zone_Name.Text & "/") = False Then
                My.Computer.FileSystem.CreateDirectory(lb.Folder_Custom_Zones & d_areamkr_cmbbx_Zone_Name.Text & "/")
            End If
            If lb.DE(lb.Folder_Custom_Zones & d_areamkr_cmbbx_Zone_Name.Text & "/" & d_areamkr_cmbbx_SubZone_Name.Text & "/") = False Then
                My.Computer.FileSystem.CreateDirectory(lb.Folder_Custom_Zones & d_areamkr_cmbbx_Zone_Name.Text & "/" & d_areamkr_cmbbx_SubZone_Name.Text & "/")
            End If
        End If

        AreaFile.WriteLine("1.1")
        AreaFile.WriteLine(d_areamkr_cmbbx_Location_Name.Text)
        AreaFile.WriteLine(d_areamkr_cmbbx_Zone_Name.Text)
        AreaFile.WriteLine(d_areamkr_cmbbx_SubZone_Name.Text)

        AreaFile.WriteLine(d_areamkr_tb_X1.Text)
        AreaFile.WriteLine(d_areamkr_tb_X4.Text)

        AreaFile.WriteLine(d_areamkr_tb_Y1.Text)
        AreaFile.WriteLine(d_areamkr_tb_Y4.Text)

        AreaFile.WriteLine(d_areamkr_tb_Z1.Text)
        AreaFile.WriteLine(d_areamkr_tb_Z4.Text)

        For Each item In ListBox2.Items
            AreaFile.WriteLine(item)
        Next

        AreaFile.SaveFile()
        Dim _currentDirAreaFiles() As String
        If lb.FE(lb.Folder_Custom_Zones & d_areamkr_cmbbx_Zone_Name.Text & "/" & d_areamkr_cmbbx_SubZone_Name.Text & "/index.txt") Then
            My.Computer.FileSystem.DeleteFile(lb.Folder_Custom_Zones & d_areamkr_cmbbx_Zone_Name.Text & "/" & d_areamkr_cmbbx_SubZone_Name.Text & "/index.txt", FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
        End If
        _currentDirAreaFiles = lb.ReturnFilesFromFolder(lb.Folder_Custom_Zones & d_areamkr_cmbbx_Zone_Name.Text & "/" & d_areamkr_cmbbx_SubZone_Name.Text & "/", ".txt")
        Dim sw As New IO.StreamWriter(lb.Folder_Custom_Zones & d_areamkr_cmbbx_Zone_Name.Text & "/" & d_areamkr_cmbbx_SubZone_Name.Text & "/index.txt")
        For Each area In _currentDirAreaFiles
            sw.WriteLine(area)
        Next
        sw.Close()
        lb.DisplayMessage("Area File Updated", "File Saved", "Kairen - Area Maker")
        Mode_AreaMaker_NewArea(False)
        RefreshTab()
        Mode_AreaMaker_LoadArea(True)
        'alert saved
    End Sub

    Private Sub d_areamkr_btn_Add_NPC_Click(sender As System.Object, e As System.EventArgs) Handles d_areamkr_btn_Add_NPC.Click
        If ListBox1.SelectedItem = Nothing Then Exit Sub
        If ListBox2.Items.Contains(ListBox1.SelectedItem) Then Exit Sub
        ListBox2.Items.Add(ListBox1.SelectedItem)
    End Sub

    Private Sub d_areamkr_btn_Remove_NPC_Click(sender As System.Object, e As System.EventArgs) Handles d_areamkr_btn_Remove_NPC.Click
        If ListBox2.SelectedItem = Nothing Then Exit Sub
        ListBox2.Items.Remove(ListBox2.SelectedItem)
    End Sub



    ' - - - - - - '
    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles d_btn_BobsMenu.Click
        Kaires.Visible = True
    End Sub

    Private Sub Button2_Click_1(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        'Dim consoleLine(0) As String
        'consoleLine(0) = TextBox1.Text
        'lb.ParseOutsideCommand("PrintToConsole", consoleLine)
        If TextBox1.Text = "" Then Exit Sub
        FAPI_Connection.AddCommand("PrintToConsole", TextBox1.Text)
    End Sub

    Private Sub d_spwnmkr_btn_SpawnThisNPC_All_Click(sender As System.Object, e As System.EventArgs) Handles d_spwnmkr_btn_SpawnThisNPC_All.Click
        'lb.ParseOutsideCommand("Spawn_NPC", ListBox1.Text)
        If ListBox1.Text = "" Then Exit Sub
        FAPI_Connection.AddCommand("Spawn_NPC", ListBox1.Text)
    End Sub

    Private Sub d_spwnmkr_btn_SpawnThisNPC_Zone_Click(sender As System.Object, e As System.EventArgs) Handles d_spwnmkr_btn_SpawnThisNPC_Zone.Click
        'lb.ParseOutsideCommand("Spawn_NPC", ListBox2.Text)
        If ListBox2.Text = "" Then Exit Sub
        FAPI_Connection.AddCommand("Spawn_NPC", ListBox2.Text)
    End Sub

    Private Sub d_npcmkr_btn_SpawnThisNPC_Click(sender As System.Object, e As System.EventArgs) Handles d_npcmkr_btn_SpawnThisNPC.Click
        'lb.ParseOutsideCommand("Spawn_NPC", d_npcmkr_cmbbx_NPCList.Text)
        If d_npcmkr_cmbbx_NPCList.Text = "" Then Exit Sub
        FAPI_Connection.AddCommand("Spawn_NPC", d_npcmkr_cmbbx_NPCList.Text)
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        Dim Input As String = "Poppledon"
        If Input = "" Then
            Input = InputBox("Enter the passphrase to identify yourself or who you got this copy from: ", "Should You Be Here? D8<")
        End If
        Select Case Input
            Case "Poppledon"
                'MsgBox("Success!")
                OnlineForm.Visible = True
                OnlineForm.OnlineName = "Bob"
            Case "gibbyshib1"
                'MsgBox("Success!")
                OnlineForm.Visible = True
                OnlineForm.OnlineName = "Daniel"
            Case "houladon1"
                'MsgBox("Success!")
                OnlineForm.Visible = True
                OnlineForm.OnlineName = "Jeremiah"
            Case "dipbiptie1"
                'MsgBox("Success!")
                OnlineForm.Visible = True
                OnlineForm.OnlineName = "Christopher"
            Case Else
                MsgBox("PWORGTFO!")
        End Select
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBox2.CheckedChanged
        lb.ParseOutsideCommand("OutputPlayerData", sender.Checked)
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBox3.CheckedChanged
        If sender.checked = False Then
            MsgBox("turning off is not coded yet")
        End If
        FAPI_Connection = New FAPIConnectionManager(d_npcmkr_btn_Grab_Data)
        FAPI_Connection.Connect()
    End Sub
    Public Sub UpdateYourData()
        If lb.FE(lb.Folder_Net_Streams & "o\Player Data.txt") = False Then
            lb.DisplayMessage("No Player Data file found. Was it created?", "Error:", "NPC Maker Grab Data")
            Exit Sub
        End If
        GrabData_NPC = New TextFileClass(lb.Folder_Net_Streams & "o\Player Data.txt", "--")
        GrabData_NPC.LoadFile()
        TextBox2.Text = GrabData_NPC.ReadLine
        TextBox3.Text = GrabData_NPC.ReadLine
        TextBox5.Text = GrabData_NPC.ReadLine
        TextBox4.Text = GrabData_NPC.ReadLine
        TextBox6.Text = GrabData_NPC.ReadLine
        TextBox7.Text = GrabData_NPC.ReadLine
        'If lb.FE(lb.Folder_Temp_NPC_Maker & "New_NPC.txt") Then
        '    My.Computer.FileSystem.DeleteFile(lb.Folder_Temp_NPC_Maker & "New_NPC.txt")
        'End If
        GrabData_NPC = Nothing
    End Sub
End Class