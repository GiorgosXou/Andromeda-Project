Option Strict Off
Option Explicit On
Imports System.Collections.Generic
Imports VB = Microsoft.VisualBasic
Friend Class Form1
    Inherits Form
    Dim codeblocks As New List(Of String) ' Xou
    Dim typeblocks As New List(Of String) ' Xou
    Dim nameblocks(9999) As String
    Dim hostblocks(9999) As String
    Dim maxblock As Integer
    Dim codeform As String
    Public APPWidth As Integer
    Public APPHeight As Integer
    Public APPNAME As String
    Public APPTHEME As String
    Public PKGNAME As String
    Public APPICON As String
    Public USE_PHONE As Boolean
    Public USE_TSENS As Boolean
    Public USE_GSENS As Boolean
    Public USE_ASENS As Boolean
    Public USE_LSENS As Boolean
    Public USE_GPS As Boolean
    Public ISMAINFORM As Boolean
    Public APPWITHC As Boolean



    Public CPROJT As String
    Public CPROJN As String
    Public CFILES As String


    Private Declare Function OpenProcess Lib "kernel32" (ByVal dwDesiredAccess As Integer, ByVal bInheritHandle As Integer, ByVal dwProcessId As Integer) As Integer
    Private Declare Function GetExitCodeProcess Lib "kernel32" (ByVal hProcess As Integer, ByRef lpExitCode As Integer) As Integer
    Private Declare Function CloseHandle Lib "kernel32" (ByVal hObject As Integer) As Integer
    Const PROCESS_QUERY_INFORMATION As Integer = &H400
    Const STILL_ALIVE As Integer = &H103


    Public Function initVALS() As Object


        For i As Integer = 0 To 9999
            nameblocks(i) = ""
            hostblocks(i) = ""
        Next

        maxblock = 0
        codeform = ""
        APPNAME = "VB4A"
        APPTHEME = "Theme.Light"
        PKGNAME = "Ezapp"
        APPICON = "icon.png"
        APPWITHC = False
        USE_PHONE = False
        USE_TSENS = False
        USE_GSENS = False
        USE_ASENS = False
        USE_LSENS = False
        USE_GPS = False


    End Function

    Private Sub Form1_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        Dim debugtag As Boolean

        debugtag = False


        Dim projDir, projName As String
        initVALS()

        If VB.Command() <> "" Then

            projDir = Split(VB.Command(), " ")(0)
            projName = Split(VB.Command(), " ")(1)


            pharseProj(projDir & "\" & projName & ".VB4AProj")

            If Me.APPWITHC = True Then
                CompileCFiles(projDir)
            End If

            genProjDir(projDir, projName)

            vb4acompiler(projDir, projName)




        Else

            If debugtag = True Then

                projDir = "d:\dds"
                projName = "dds"
                pharseProj(projDir & "\" & projName & ".VB4AProj")

                genProjDir(projDir, projName)
                vb4acompiler(projDir, projName)

            Else

                MsgBox("AndromedaCompiler V1.0" & vbCrLf & "By Aslamic Louis and modified By George Chousos" & vbCrLf & "In memories of VB4A", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "")

            End If

        End If







        End
    End Sub

    Public Sub Holdrun(ByRef Shellscript As String, ByRef Hideornot As Boolean)
        Dim ExitCode As Object
        Dim hProcess As Object
        Dim pid As Object
        Dim Debugmode As Object
        Dim DebugConsole As Object
        On Error GoTo ErrHand




        If Debugmode Then DebugConsole.Debugtext.Text = DebugConsole.Debugtext.Text & "Running: " & Shellscript & vbCrLf

        If Debugmode Then Hideornot = False

        If Hideornot = True Then

            pid = Shell(Shellscript, AppWinStyle.Hide)
        Else

            pid = Shell(Shellscript, AppWinStyle.NormalFocus)
        End If



        hProcess = OpenProcess(PROCESS_QUERY_INFORMATION, 0, pid)

        Do


            Call GetExitCodeProcess(hProcess, ExitCode)

            System.Windows.Forms.Application.DoEvents()

        Loop While ExitCode = STILL_ALIVE


        Call CloseHandle(hProcess)


        Exit Sub
ErrHand:
        MsgBox("Error Number: " & vbTab & Err.Number & vbCrLf & "Error Description: " & vbTab & Err.Description & vbCrLf & "Error Sub: " & vbTab & "Holdrun", MsgBoxStyle.Exclamation)
    End Sub


    Public Sub exeComp(ByRef PJDir As String)

        Holdrun("cmd /c " & My.Application.Info.DirectoryPath & "\lib\jdk\bin\" & "java -classpath " & My.Application.Info.DirectoryPath & "\lib\" & "apkbuilder.jar" & ";" & My.Application.Info.DirectoryPath & "\lib\" & "dx.jar" & ";" & My.Application.Info.DirectoryPath & "\lib\" & "androidprefs.jar" & ";" & My.Application.Info.DirectoryPath & "\lib\" & "jarutils.jar" & ";" & My.Application.Info.DirectoryPath & "\lib\" & "VB4ACompiler.jar com.google.devtools.simple.compiler.Main " & PJDir & "\BUILD\project.ini > " & PJDir & "\Compile.log & pause", False)

        Debug.Print(VB6.TabLayout("java -classpath " & My.Application.Info.DirectoryPath & "\lib\" & "apkbuilder.jar" & ";" & My.Application.Info.DirectoryPath & "\lib\" & "dx.jar" & ";" & My.Application.Info.DirectoryPath & "\lib\" & "androidprefs.jar" & ";" & My.Application.Info.DirectoryPath & "\lib\" & "jarutils.jar" & ";" & My.Application.Info.DirectoryPath & "\lib\" & "VB4ACompiler.jar com.google.devtools.simple.compiler.Main " & PJDir & "\BUILD\project.ini > " & PJDir & "\Compile.log", AppWinStyle.NormalFocus))

    End Sub

    Public Sub vb4acompiler(ByRef ProjectDir As String, ByRef ProjectName As String)


        Dim MyFile, strFile As String
        strFile = ProjectDir & "\*.vb4a"

        MyFile = Dir(strFile, FileAttribute.Normal)

        If MyFile <> "" Then Compfile(ProjectDir, MyFile)

        Do While MyFile <> ""

            MyFile = Dir()
            If MyFile <> "" Then Compfile(ProjectDir, MyFile)
        Loop

        exeComp(ProjectDir)

    End Sub

    Public Function genCodes(ByRef PJDir As String, ByRef CDFName As String) As String
        Dim tmpstr As Object
        Dim cOds As String
        cOds = ""
        FileOpen(1, PJDir & "\" & CDFName, OpenMode.Input)
        Do While Not EOF(1)
            tmpstr = LineInput(1)

            cOds = cOds & tmpstr & vbCrLf
        Loop
        FileClose(1)
        genCodes = cOds
    End Function



    Public Sub Compfile(ByRef PJDir As String, ByRef UIFName As String)
        Dim fname As String

        pharseUI(PJDir & "\" & UIFName)
        Dim UICodes As String
        Dim CMCodes As String

        CMCodes = genCodes(PJDir, Split(UIFName, ".")(0) & ".vb4acode")
        UICodes = generateUI()
        If ISMAINFORM = True Then fname = Me.PKGNAME Else fname = Split(UIFName, ".")(0)
        FileOpen(1, PJDir & "\BUILD\src\com\vb4a\" & fname & ".vac", OpenMode.Output)
        PrintLine(1, CMCodes)
        PrintLine(1, "")
        PrintLine(1, UICodes)

        FileClose(1)
    End Sub

    Public Function isnum(ByRef inchar As String) As Boolean
        Dim i As Object
        Dim ochar As Object

        ochar = "-"
        For i = 0 To 9


            ochar = Replace(inchar, Trim(Str(i)), "")
        Next i

        If ochar = "" Then
            isnum = True
        Else
            isnum = False
        End If
    End Function


    Public Function getType_Renamed(ByRef typstr As String) As String
        'MsgBox(System.Text.RegularExpressions.Regex.Replace(typstr.Replace("<A", "").Replace(">", ""), "[0-9]+", ""))

        If System.Text.RegularExpressions.Regex.Replace(typstr.Replace("<A", "").Replace(">", ""), "[0-9]+", "") = "VBAWeb" Then Return "VB4AWeb"
        If typstr.StartsWith("<Canvas") Then Return "Canvas"

        Return System.Text.RegularExpressions.Regex.Replace(typstr.Replace("<A", "").Replace(">", ""), "[0-9]+", "")
    End Function


    Public Function generateUI() As String
        Dim j As Integer
        Dim i As Integer

        generateUI = "$Properties" & vbCrLf
        generateUI = generateUI & "$Source $Form" & vbCrLf
        generateUI = generateUI & genFormCode(codeform)

        For i = 0 To maxblock

            If typeblocks.Item(i) = "Panel" Then

                generateUI = generateUI & genBlockCode(codeblocks.Item(i), typeblocks.Item(i))
                For j = 0 To maxblock


                    If hostblocks(j) = nameblocks(i) Then

                        generateUI = generateUI & genBlockCode(codeblocks.Item(j), typeblocks.Item(j)) & vbCrLf
                    End If
                Next j
                generateUI = generateUI & "$End $Define" & vbCrLf
            End If
        Next i

        For i = 0 To maxblock

            If typeblocks.Item(i) <> "Panel" Then

                If hostblocks(i) = "Form" Then

                    generateUI = generateUI & genBlockCode(codeblocks.Item(i), typeblocks.Item(i)) & vbCrLf
                End If
            End If
        Next i
        generateUI = generateUI & "$End $Define" & vbCrLf & "$End $Properties" & vbCrLf

    End Function

    Public Function genProjDir(ByRef Pdir As String, ByRef Pname As String) As Object


        If Dir(Pdir & "\BUILD\src", FileAttribute.Directory) = "" Then

            MkDir(Pdir & "\BUILD")
            MkDir(Pdir & "\BUILD\assets")
            MkDir(Pdir & "\BUILD\src")
            MkDir(Pdir & "\BUILD\src\com")
            MkDir(Pdir & "\BUILD\src\com\vb4a")
            MkDir(Pdir & "\BUILD\build")
        End If

        'Dim utf8Encoding As New System.Text.UTF8Encoding(True)

        'MsgBox(utf8Encoding.GetString(utf8Encoding.GetBytes(APPNAME)))


        FileOpen(2, Pdir & "\BUILD\project.ini", OpenMode.Output)
        PrintLine(2, "main=com.vb4a." & Me.PKGNAME)
        PrintLine(2, "name = " & Me.PKGNAME)
        PrintLine(2, "assets=./assets")
        PrintLine(2, "source=./src")
        PrintLine(2, "build=./build")
        PrintLine(2, "appname=" & Me.APPNAME)
        PrintLine(2, "apptheme=" & Me.APPTHEME)
        PrintLine(2, "appminsdk=4")
        PrintLine(2, "appicon=" & Replace(Pdir & "\" & Me.APPICON, "\", "/"))
        FileClose(2)




    End Function

    Public Function pharseUI(ByRef UIFILE As String) As String
        Dim tmpUI As String
        Dim tmpstr As String
        Dim headread As Boolean
        Dim pnHost As String


        If InStr(1, UIFILE, "form1.vb4a") Then
            ISMAINFORM = True
        Else
            ISMAINFORM = False
        End If

        tmpUI = ""
        Dim blockread As Boolean
        blockread = False
        maxblock = -1
        FileOpen(1, UIFILE, OpenMode.Input)
        codeform = ""

        tmpstr = LineInput(1)
        headread = True

        Do While Not EOF(1)
            tmpstr = LineInput(1)


            If tmpstr = "--Form--" Then
                headread = False
            End If

            If headread = True Then
                codeform = codeform & tmpstr & vbCrLf
            End If

            If Mid(tmpstr, 1, 2) = "</" Then
                blockread = False
            End If

            If blockread = True Then
                codeblocks.Item(maxblock) = codeblocks.Item(maxblock) & tmpstr & vbCrLf
            End If

            If Me.startWith(tmpstr, "IsOnPanel") = True Then
                pnHost = Split(tmpstr, "=")(1)
                If Trim(pnHost) <> "" Then
                    hostblocks(maxblock) = pnHost
                Else
                    hostblocks(maxblock) = "Form"
                End If
            End If

            If Mid(tmpstr, 1, 2) = "<A" Or Mid(tmpstr, 1, 5) = "<VB4A" Or Mid(tmpstr, 1, 5) = "<Pass" Or Mid(tmpstr, 1, 7) = "<Canvas" Then
                tmpstr = tmpstr.Replace("<VB4A", "<AVB4A")
                tmpstr = tmpstr.Replace("<Pass", "<APass")
                blockread = True
                codeblocks.Add("")
                typeblocks.Add("")
                maxblock = maxblock + 1
                typeblocks(maxblock) = getType_Renamed(tmpstr)
                nameblocks(maxblock) = Mid(tmpstr, 2, Len(tmpstr) - 2)
            End If

        Loop


        FileClose(1)










    End Function

    Public Function genBlockCode(ByRef incode As String, ByRef intype As String) As String
        Dim i As Object
        Dim codes() As String
        Dim fcode As String
        Dim nameid As Short
        codes = Split(incode, vbCrLf)
        fcode = ""
        nameid = -1

        For i = 0 To UBound(codes)

            If Mid(codes(i), 1, 4) = "Name" Then nameid = i
        Next i


        fcode = pharseVB(codes(nameid), intype) & vbCrLf



        For i = 0 To UBound(codes)

            If Trim(codes(i)) <> "" And i <> nameid Then

                If pharseVB(codes(i), intype) <> "" Then

                    fcode = fcode & pharseVB(codes(i), intype) & vbCrLf
                End If
            End If

        Next i
        genBlockCode = fcode & "$End $Define"
    End Function

    Public Function genFormCode(ByRef incode As String) As String
        Dim i As Object
        Dim codes() As String
        Dim fcode As String

        codes = Split(incode, vbCrLf)
        If Me.ISMAINFORM = True Then
            fcode = "$Define " & Me.PKGNAME & " $As Form" & vbCrLf
        Else
            fcode = pharseVB(codes(0), "Form") & vbCrLf
        End If


        For i = 1 To UBound(codes)

            If Trim(codes(i)) <> "" Then

                If pharseVB(codes(i), "Form") <> "" Then

                    fcode = fcode & pharseVB(codes(i), "Form") & vbCrLf
                End If
            End If

        Next i

        fcode = fcode & "Layout = 4" & vbCrLf

        genFormCode = fcode
    End Function

    Public Function genColor(ByVal rr As Double, ByVal gg As Double, ByVal bb As Double, ByVal aa As Double) As String
        genColor = "&H" & VB6.Format(Hex(rr), "00") & VB6.Format(Hex(gg), "00") & VB6.Format(Hex(bb), "00") & VB6.Format(Hex(aa), "00")
    End Function

    Public Function genAlign(ByVal inal As String) As String
        Select Case inal

            Case ""

        End Select
    End Function

    Public Function startWith(ByRef stcode As String, ByRef stwt As String) As Boolean
        If Mid(Trim(stcode), 1, Len(stwt)) = stwt Then
            startWith = True
        Else
            startWith = False
        End If
    End Function


    Public Function pharseVB(ByRef codein As String, ByRef ctype_Renamed As String) As String
        Dim ht As Object
        Dim wd As Object
        Dim tsiz As Object
        Dim ty As Object
        Dim tx As Object
        Dim tloc As Object
        Dim tcolstr2 As Object
        Dim tcolstr As Object
        Dim parm1 As String
        Dim val1 As String
        Dim code() As String
        Dim tmpcode As String

        tmpcode = ""
        code = Split(codein, "=")
        parm1 = code(0)
        val1 = code(1)


        Select Case ctype_Renamed

            Case "ComboBox"
                ctype_Renamed = "Combobox"

            Case "ProgressBar"
                ctype_Renamed = "Progressbar"

        End Select


        Select Case parm1

            Case "id"

            Case "IsOnForm"

            Case "IsOnPanel"

            Case "Name"
                tmpcode = "$Define " & val1 & " $As " & ctype_Renamed

            Case "Text"
                Select Case ctype_Renamed

                    Case "Combobox", "Listview"

                    Case Else
                        tmpcode = "Text = " & Chr(34) & Replace(val1, vbCrLf, "\n") & Chr(34)
                End Select


            Case "FontBold"

                Select Case ctype_Renamed

                    Case "Combobox", "Listview"

                    Case Else
                        tmpcode = "FontBold = " & val1
                End Select



            Case "FontSize"

                Select Case ctype_Renamed

                    Case "Combobox", "Listview"

                    Case Else
                        tmpcode = "FontSize = " & val1 '& val1 & "* Application.GetScreenWidth() / " & APPWidth  '"FontSize = " & val1 & " * Application.ZoomRatioW() * 0.8"
                End Select



            Case "FontItalic"
                Select Case ctype_Renamed

                    Case "Combobox", "Listview"

                    Case Else
                        tmpcode = "FontItalic = " & val1
                End Select



            Case "FontTypeface"
                Select Case ctype_Renamed

                    Case "Combobox", "Listview"

                    Case Else
                        tmpcode = "FontTypeface = Component.TYPEFACE_" & val1
                End Select



            Case "TextAlign"
                Select Case ctype_Renamed

                    Case "Combobox", "Listview"

                    Case Else
                        tmpcode = "Justification = Component.JUSTIFY_" & val1
                End Select


            Case "BackColor"

                tcolstr = Mid(val1, 2, Len(val1) - 2)


                tcolstr2 = genColor(Val(Split(tcolstr, ";")(0)), Val(Split(tcolstr, ";")(1)), Val(Split(tcolstr, ";")(2)), Val(Split(tcolstr, ";")(3)))
                Select Case ctype_Renamed

                    Case "Combobox", "Listview"

                    Case Else

                        tmpcode = "BackgroundColor = " & tcolstr2
                End Select


            Case "ForeColor"

                tcolstr = Mid(val1, 2, Len(val1) - 2)


                tcolstr2 = genColor(Val(Split(tcolstr, ";")(0)), Val(Split(tcolstr, ";")(1)), Val(Split(tcolstr, ";")(2)), Val(Split(tcolstr, ";")(3)))
                Select Case ctype_Renamed

                    Case "Combobox", "Listview"

                    Case Else

                        tmpcode = "TextColor = " & tcolstr2
                End Select



            Case "Location"

                tloc = Mid(val1, 2, Len(val1) - 2)


                tx = Split(tloc, ";")(0)


                ty = Split(tloc, ";")(1)


                Select Case ctype_Renamed

                    Case "Timer"

                    Case Else

                        tmpcode = "Left = (Application.GetScreenWidth() * " & tx & ") / " & APPWidth & vbCrLf ' "Left = " & tx & " * Application.ZoomRatioW()" & vbCrLf 

                        tmpcode = tmpcode & "Top = (Application.GetScreenHeight() * " & ty & ") / " & APPHeight ' tmpcode & "Top = " & ty & " * Application.ZoomRatioH()" 
                End Select



            Case "Size"

                tsiz = Mid(val1, 2, Len(val1) - 2)


                wd = Split(tsiz, ";")(0)


                ht = Split(tsiz, ";")(1)

                Select Case ctype_Renamed

                    Case "Timer"

                    Case Else

                        tmpcode = "Width = (Application.GetScreenWidth() * " & wd & ") / " & APPWidth & vbCrLf ' "Width = " & wd & " * Application.ZoomRatioW()" & vbCrLf

                        tmpcode = tmpcode & "Height = (Application.GetScreenHeight() * " & ht & ") / " & APPHeight  '  tmpcode & "Height = " & ht & " * Application.ZoomRatioH()"
                End Select

            Case "Image"


            Case "Enabled"
                Select Case ctype_Renamed

                    Case "Progressbar"

                    Case "PictureBox"

                    Case "VB4AWeb"

                    Case "Canvas"

                    Case Else
                        tmpcode = "Enabled = " & val1
                End Select


            Case "Checked"
                tmpcode = "Value = " & val1

            Case "Title"
                tmpcode = "Title = " & Chr(34) & val1 & Chr(34)

            Case "Scrollable"
                tmpcode = "Scrollable = " & val1

            Case "Interval"
                tmpcode = "Interval = " & val1

            Case "BackgroundImage"
                val1 = Chr(34) & val1 & Chr(34)
                tmpcode = "BackgroundImage = " & val1

            Case "Layout"
                tmpcode = "Layout.Orientation = Component.LAYOUT_ORIENTATION_" & val1

        End Select


        pharseVB = tmpcode
    End Function


    Public Function pharseProj(ByRef codefile As String) As Object
        Dim i As Object
        Dim tmpstr As Object
        Dim parm1 As String
        Dim val1 As String
        Dim code() As String
        Dim tmpcode As String
        Dim codein As String
        codein = ""
        FileOpen(1, codefile, OpenMode.Input)
        Do While Not EOF(1)
            tmpstr = LineInput(1)

            codein = codein & tmpstr & vbCrLf
        Loop
        FileClose(1)


        tmpcode = ""
        For i = 0 To UBound(Split(codein, vbCrLf))

            If Trim(Split(codein, vbCrLf)(i)) <> "" Then

                code = Split(Split(codein, vbCrLf)(i), "=")
                parm1 = code(0)
                val1 = code(1)

                Select Case parm1

                    Case "Phone"
                        If val1 = "True" Then Me.USE_PHONE = True
                    Case "TempSensor"
                        If val1 = "True" Then Me.USE_TSENS = True
                    Case "Network"

                    Case "AccSensor"
                        If val1 = "True" Then Me.USE_ASENS = True
                    Case "LightSensor"
                        If val1 = "True" Then Me.USE_LSENS = True
                    Case "GPS"
                        If val1 = "True" Then Me.USE_GPS = True
                    Case "ApplicationPKGName"
                        Me.PKGNAME = val1
                    Case "ApplicationName"
                        Me.APPNAME = val1
                    Case "ApplicationTheme"
                        Me.APPTHEME = val1
                    Case "ApplicationIcon"
                        Me.APPICON = val1
                    Case "APPWITHC"
                        If val1 = "True" Then Me.APPWITHC = True
                    Case "ScreenSize"
                        Me.APPWidth = val1.Split("x")(0)
                        Me.APPHeight = val1.Split("x")(1)
                End Select
            End If
        Next i
    End Function

    Public Sub pharseCProj(ByRef codefile As String)
        Dim i As Object
        Dim tmpstr As Object
        Dim parm1 As String
        Dim val1 As String
        Dim code() As String
        Dim tmpcode As String
        Dim codein As String
        codein = ""
        FileOpen(1, codefile, OpenMode.Input)
        Do While Not EOF(1)
            tmpstr = LineInput(1)

            codein = codein & tmpstr & vbCrLf
        Loop
        FileClose(1)


        tmpcode = ""
        For i = 0 To UBound(Split(codein, vbCrLf))

            If Trim(Split(codein, vbCrLf)(i)) <> "" Then

                code = Split(Split(codein, vbCrLf)(i), "=")
                parm1 = code(0)
                val1 = code(1)

                Select Case parm1

                    Case "cprojtype"
                        Me.CPROJT = val1
                    Case "cprojname"
                        Me.CPROJN = val1
                    Case "cfiles"
                        Me.CFILES = val1


                End Select
            End If
        Next i
    End Sub

    Public Sub CompileCFiles(ByRef ProjectDir As String)
        Dim CPROJDIR As String
        Dim CPROJFIL As String
        Dim CDISTDIR As String
        Dim CCOMPILE As String
        Dim CPPCOMPF As String
        Dim CARCH As String
        CARCH = "arm" ' arm, x86 but now we only support arm
        CPROJDIR = ProjectDir & "\CPROJ"
        CPROJFIL = CPROJDIR & "\cproject.v4a"

        pharseCProj(CPROJFIL)
        CDISTDIR = ProjectDir & "\BUILD\assets"
        CCOMPILE = "cmd /c " & My.Application.Info.DirectoryPath & "\lib\ndk\toolchains\arm-linux-androideabi-4.8\prebuilt\windows\bin\arm-linux-androideabi-gcc.exe --sysroot=" & My.Application.Info.DirectoryPath & "\lib\ndk\platforms\android-9\arch-" & CARCH & " -static "
        CPPCOMPF = "cmd /c " & My.Application.Info.DirectoryPath & "\lib\ndk\toolchains\arm-linux-androideabi-4.8\prebuilt\windows\bin\arm-linux-androideabi-g++.exe --sysroot=" & My.Application.Info.DirectoryPath & "\lib\ndk\platforms\android-9\arch-" & CARCH & " -static "


        If Me.CPROJT = "Single" Then
            Holdrun(CCOMPILE & CPROJDIR & "\" & Me.CFILES & " -o " & CDISTDIR & "\" & Me.CPROJN & "", True)
        Else

        End If

    End Sub
End Class