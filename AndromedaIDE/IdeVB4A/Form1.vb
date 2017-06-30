
' !!!!! BEFORE YOU EVEN JUDGE ME :P !!!!!
' 

' 1) When i started building this IDE i was 16-17 years old.

' 2) ALL MY KNOWLEDGE about programming and computer things was learned from INTERNET.

' 3) At the age of 17 when i realised that my code was fucked up (lol) it was 
'    too late because i had to STUDY FOR MY FINAL EXAMS/ENTRANCE EXAMINATIONS.
'
' 4) I had that SMALL amount of free time that i even worked on IDE at 2-3am, when
'    the next day i had to wake up for school at least at 7am.
' 
' 5) there is a story writen from "inside" of this code :') , story
'    made out of passion and love for programming, days without sleep,
'    days without being able to concentrate because i was thinking wtf
'    was going on with some problems and etc.
'
' 6) Arent those enough ? :3
'



Option Infer On

Imports System.Drawing.Drawing2D
Imports System.Text
Imports System.Runtime.InteropServices
Imports FastColoredTextBoxNS
Imports System.IO
Imports System.Text.RegularExpressions

Imports System.Drawing
Imports System.Windows.Forms
Imports System.ComponentModel

'συγνωμη για τα ορ8ογραφικα εχω δυσλεξια γι αυτο αμα πετύχετε κανενα μην με κραξετε !

'Imports System.Text.RegularExpressions

'29; 89

Public Class Form1
    ' Inherits System.Windows.Forms.Form


    '  Private Declare Function GetAsyncKeyState Lib "user32" (ByVal vKey As Integer) As Integer
    'Dim ctrlk As Integer = GetAsyncKeyState(System.Windows.Forms.Keys.ShiftKey)
    Public Shared ConsoleDouble As Integer = 0  'μου αρέσει που το εχω και Double ενω ειναι Int xD

    ' Public bhcomp As Boolean = True

    Public strClsRemove() As String = {"!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "-", "=", "+", """", "'", ";", "\", "|", "/", "?", ",", ".", "`", "~", "΄", "¨"} '10(-) 23(~) 25(¨)     ειναι και το 0 στην αριθμιση

    Public CurrentHardDriver As String = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).ToString


    Public Function IsUsedFromAnotherProc(FilePath As String)
        Try
            'CREATE A FILE STREAM FROM THE FILE, OPENING IT FOR READ ONLY EXCLUSIVE ACCESS
            Dim FS As IO.FileStream = IO.File.Open(FilePath, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.None)
            'CLOSE AND CLEAN UP RIGHT AWAY, IF THE OPEN SUCCEEDED, WE HAVE OUR ANSWER ALREADY
            FS.Close()
            FS.Dispose()
            FS = Nothing
            'MessageBox.Show("Yes, you are the only one using this file")
        Catch ex As IO.IOException
            'IF AN IO EXCEPTION IS THROWN, WE COULD NOT GET EXCLUSIVE ACCESS TO THE FILE
            Return True
        Catch ex As Exception
            MessageBox.Show("Unknown error occured" & Environment.NewLine & ex.Message)
            Return True

        End Try
        Return False
    End Function
    'Protected Overrides ReadOnly Property CreateParams() As System.Windows.Forms.CreateParams
    '    Get
    '        Const DROPSHADOW = &H20000
    '        Dim cParam As CreateParams = MyBase.CreateParams
    '        cParam.ClassStyle = cParam.ClassStyle Or DROPSHADOW
    '        Return cParam
    '    End Get
    'End Property

    Private Sub ToolStripDropDownButton6_Click(sender As Object, e As EventArgs) Handles ToolStripDropDownButton6.Click
        If ListView1.Visible = True Then
            ListView1.Hide()
        Else
            ListView1.Show()
            ListView1.BringToFront()
            Splitter1.BringToFront()
            RichTextBox1.BringToFront()
            Button10.BringToFront()

        End If
    End Sub

    Private tools(15) As String
    Private tooltags(15) As String

    Public FileCreatedName As String = String.Empty
    Public FileDirName As String = String.Empty ' Directory

    Public UsernamePC As String = My.Computer.Name

    '  Public WithEvents TextBox3 As New FastColoredTextBox

    Public WithEvents Combobox3 As New ComboboxImage
    Public WithEvents Combobox1 As New ComboboxImage


    ' ''    Private Declare Auto Function SendMessage Lib "user32.dll" ( _
    ' ''ByVal hwnd As IntPtr, _
    ' ''ByVal wMsg As Int32, _
    ' ''ByVal wParam As Int32, _
    ' ''ByVal lParam As Int32 _
    ' '') As Int32

    ' ''    Private Const CB_SETITEMHEIGHT As Int32 = &H153


    ' ''    Private Sub SetComboEditHeight( _
    ' ''    ByVal Control As ComboBox, _
    ' ''    ByVal NewHeight As Int32 _
    ' ''    )


    ' ''        SendMessage(Control.Handle, CB_SETITEMHEIGHT, -1, NewHeight)
    ' ''        Control.Refresh()
    ' ''        Control.Select()
    ' ''    End Sub

    ' ''Form1.SetComboEditHeight(Form1.GenControl, 1)

    ' Private Sub Textbox3_TextChanged(ByVal sender As Object, ByVal e As TextChangedEventArgs)


    'Dim langKey As String = InputLanguage.CurrentInputLanguage.Culture.KeyboardLayoutId
    ' If langKey = 2052 Or langKey = 4100 Or langKey = 1028 Or langKey = 3076 Or langKey = 5124 Or langKey = 1032 Then
    ' TextBox3.CharWidth = Math.Round(15.0F)
    'Dim MyString As String = "Hello World!"
    '  TextBox3.Text = MyString.PadLeft(20, "-")



    'End If
    '  End Sub

    Dim CommentRegex,
        NumberRegex,
        StringRegex,
        KeywordRegex,
        SpecialKeywordRegex,
        Special2KeywordRegex As Regex

    Dim PinkStyle As Style = New TextStyle(New SolidBrush(Color.FromArgb(183, 6, 122)), Nothing, FontStyle.Regular)
    Dim BlueStyle As Style = New TextStyle(New SolidBrush(Color.FromArgb(0, 43, 116)), Nothing, FontStyle.Regular)
    Dim BrownStyle As Style = New TextStyle(Brushes.Brown, Nothing, FontStyle.Italic)
    Dim GreenStyle As Style = New TextStyle(Brushes.Green, Nothing, FontStyle.Italic)
    Dim SaddleBrownStyle As Style = New TextStyle(Brushes.SaddleBrown, Nothing, FontStyle.Regular)
    Dim MagentaStyle As Style = New TextStyle(Brushes.SlateBlue, Nothing, FontStyle.Regular)
    Dim Green2style As Style = New TextStyle(Brushes.SeaGreen, Nothing, FontStyle.Regular)

    Dim platformType As Platform
    Public Shared ReadOnly Property RegexCompiledOption() As RegexOptions
        Get
            If Form1.platformType = Platform.X86 Then
                Return RegexOptions.Compiled
            Else
                Return RegexOptions.None
            End If
        End Get
    End Property
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        platformType = Textbox3.SyntaxHighlighter.platformType

        StringRegex = New Regex("""""|"".*?[^\\]""", RegexCompiledOption)
        CommentRegex = New Regex("'.*$", RegexOptions.Multiline Or RegexCompiledOption)
        NumberRegex = New Regex("\b\d+[\.]?\d*([eE]\-?\d+)?\b", RegexCompiledOption)
        KeywordRegex = New Regex("\b(AddHandler|AddressOf|Alias|And|AndAlso|As|Boolean|ByRef|Byte|ByVal|Call|Case|Catch|CBool|CByte|CChar|CDate|CDbl|CDec|Char|CInt|Class|CLng|CObj|Const|Continue|CSByte|CShort|CSng|CStr|CType|CUInt|CULng|CUShort|Date|Decimal|Declare|Default|Delegate|Dim|DirectCast|Do|Double|Each|Else|ElseIf|End|EndIf|Enum|Erase|Error|Event|Exit|False|Finally|For|Friend|Function|Get|GetType|GetXMLNamespace|Global|GoSub|GoTo|Handles|If|Implements|Imports|In|Inherits|Integer|Interface|Is|IsNot|Let|Lib|Like|Long|Loop|Me|Mod|Module|MustInherit|MustOverride|MyBase|MyClass|Namespace|Narrowing|New|Next|Not|Nothing|NotInheritable|NotOverridable|Object|Of|On|Operator|Option|Optional|Or|OrElse|Overloads|Overridable|Overrides|ParamArray|Partial|Private|Property|Protected|Public|RaiseEvent|ReadOnly|ReDim|REM|RemoveHandler|Resume|Return|SByte|Select|Set|Shadows|Shared|Short|Single|Static|Step|Stop|String|Structure|Sub|SyncLock|Then|Throw|To|True|Try|TryCast|TypeOf|UInteger|ULong|UShort|Using|Variant|Wend|When|While|Widening|With|WithEvents|WriteOnly|Xor|Region)\b|(#Const|#Else|#ElseIf|#End|#If|#Region)\b", RegexOptions.IgnoreCase Or RegexCompiledOption)
        SpecialKeywordRegex = New Regex("\b(Application|Arrays|Statistics|Files|Math|Matrix|AcceleromaterSenor|LocationSensor|OrientationSensor|Phone|Component|Dates|Strings|Conversions|)\b", RegexOptions.IgnoreCase Or RegexCompiledOption)
        Special2KeywordRegex = New Regex("\b(Base64|)\b", RegexOptions.IgnoreCase Or RegexCompiledOption)

        PictureBox1.AutoScroll = True
        PictureBox1.VerticalScroll.Visible = True Or PictureBox1.HorizontalScroll.Visible = True


        'grayPen.DashStyle = DashStyle.Dash

        CurrentHardDriver = CurrentHardDriver.Remove(0, 6).Substring(0, 3)
        documentMap1.Hide()
        TransparentDrawing1.Location = New Point(0, 0)
        TransparentDrawing1.Refresh()
        TransparentDrawing1.Hide()



        Combobox1.Anchor = AnchorStyles.Top Or AnchorStyles.Right

        PropertyGrid1.SelectedObject = New clsForm
        PropertyGrid1.Enabled = False

        Panel6.Size = New Size(166, 460)
        Panel6.Location = New Point(852, 81)
        'Panel6.Anchor = AnchorStyles.Right

        documentMap1.Location = New Point(852, 82)
        documentMap1.Size = New Size(165, 457)
        'documentMap1.Anchor = AnchorStyles.Right

        border1.Location = New Point(0, 0)
        border1.Size = New Size(1030, 572) '1030; 572
        border1.BorderStyle = BorderStyle.None
        ' border1.Hide()

        border2.Location = New Point(-9, 29)
        border2.Size = New Size(10, 491)

        BorderText.Location = New Point(360, 84)
        BorderText.Size = New Size(260, 27)

        Button1.Location = New Point(287, 83)
        Button1.Size = New Size(33, 29)

        Button2.Location = New Point(322, 83)
        Button2.Size = New Size(33, 29)

        Button10.Size = New Size(77, 12)
        Button10.Location = New Point((Me.Size.Width - Button10.Size.Width) \ 2, Me.Height - (Button10.Height / 2) - 1)



        Button3.Location = New Point(998, 2)
        Button3.Size = New Size(28, 22)

        Button4.Location = New Point(942, 2)
        Button4.Size = New Size(28, 22)

        Button5.Location = New Point(970, 2)
        Button5.Size = New Size(28, 22)

        Button6.Location = New Point(9, 97)
        Button6.Size = New Size(232, 27)



        CheckBox1.Location = New Point(6, 24)
        CheckBox1.Size = New Size(54, 17)

        CheckBox2.Location = New Point(6, 49)
        CheckBox2.Size = New Size(45, 17)

        CheckBox3.Location = New Point(6, 74)
        CheckBox3.Size = New Size(73, 17)

        CheckBox4.Location = New Point(6, 99)
        CheckBox4.Size = New Size(63, 17)

        CheckBox5.Location = New Point(6, 124)
        CheckBox5.Size = New Size(78, 17)

        CheckBox7.Location = New Point(98, 24)
        CheckBox7.Size = New Size(79, 17)

        CheckBox8.Location = New Point(98, 49)
        CheckBox8.Size = New Size(86, 17)

        CheckBox6.Location = New Point(98, 74)
        CheckBox6.Size = New Size(81, 17)

        Me.Controls.Add(Combobox3)

        With Combobox3
            .Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
            .DropDownStyle = ComboBoxStyle.DropDownList
            .IntegralHeight = False
            .ImageList = ImageList2
            .FlatStyle = FlatStyle.Flat
            .MaxDropDownItems = 9
            .BackColor = Color.WhiteSmoke
            .Size = New Size(254, 21)
            .Location = New Point(363, 87)
            .BringToFront()
        End With


        ToolStripComboBox1.Location = New Point(11, 100)
        ToolStripComboBox1.Size = New Size(226, 21)

        Me.Controls.Add(Combobox1)

        With Combobox1
            .DropDownStyle = ComboBoxStyle.DropDownList
            .IntegralHeight = False
            .ImageList = ImageList1
            .FlatStyle = FlatStyle.Flat
            .MaxDropDownItems = 9
            .BackColor = Color.WhiteSmoke
            .Size = New Size(313, 21)
            .Location = New Point(705, 85)
            .BringToFront()
        End With

        ComboboxImage1.Hide()
        ComboboxImage1.Location = New Point(182, 58)
        ComboboxImage1.Size = New Size(322, 21)
        With ComboboxImage1
            .DropDownStyle = ComboBoxStyle.DropDownList
            .IntegralHeight = False
            .ImageList = ImageList1
            .FlatStyle = FlatStyle.Flat
            .MaxDropDownItems = 9
            .BackColor = Color.FromArgb(XofFormApp, XofFormApp, XofFormApp)
            .BringToFront()
        End With


        grayPen.DashPattern = {1, 1}


        ComboboxImage2.Hide()
        ComboboxImage2.Location = New Point(506, 58)
        ComboboxImage2.Size = New Size(322, 21)
        With ComboboxImage2
            .DropDownStyle = ComboBoxStyle.DropDownList
            .IntegralHeight = False
            .ImageList = ImageList2
            .FlatStyle = FlatStyle.Flat
            .MaxDropDownItems = 9
            .BackColor = Color.FromArgb(XofFormApp, XofFormApp, XofFormApp)
            .BringToFront()
        End With


        Me.Size = New Size(1030, 568)
        Me.BringToFront()
        Me.StartPosition = FormStartPosition.CenterScreen
        'border1.BringToFront()

        Label1.Location = New Point(40, 83)
        Label1.Size = New Size(77, 15)

        Label2.Location = New Point(40, 228)
        Label2.Size = New Size(60, 15)

        Label3.Location = New Point(40, 436)
        Label3.Size = New Size(41, 15)

        Label4.Location = New Point(268, 20) '261; 20   , 266, 20
        Label4.Size = New Size(0, 20)

        Label5.Location = New Point(68, 12)
        Label5.Size = New Size(205, 36)

        Label6.Location = New Point(86, 22)
        Label6.Size = New Size(77, 13)

        Label7.Location = New Point(988, 549)
        Label7.Size = New Size(0, 17)

        Label8.Location = New Point(948, 94)
        Label8.Size = New Size(41, 15)

        Label9.Location = New Point(11, 68)
        Label9.Size = New Size(71, 15)

        Label10.Text = String.Empty
        Label10.Hide()

        ListView1.Location = New Point(29, 83)
        ListView1.Size = New Size(251, 458)

        Panel1.Location = New Point(29, 89)
        Panel1.Size = New Size(251, 135)

        Panel2.Location = New Point(29, 234)
        Panel2.Size = New Size(251, 198)

        Panel3.Location = New Point(29, 442)
        Panel3.Size = New Size(251, 99)

        Button7.Location = New Point(5, 17)
        Button7.Size = New Size(239, 29)

        Button8.Location = New Point(5, 52)
        Button8.Size = New Size(120, 29)

        Button9.Location = New Point(124, 52)
        Button9.Size = New Size(120, 29)

        Panel4.Location = New Point(0, 0)
        Panel4.Size = New Size(1030, 53)

        Panel5.Location = New Point(44, -1)
        Panel5.Size = New Size(XofFormApp + 2, YofFormApp + 2) ' 240 xYofFormApp
        Panel5.BackgroundImageLayout = ImageLayout.Stretch

        PictureBox1.Location = New Point(287, 114)
        PictureBox1.Size = New Size(XofFormApp + 93, YofFormApp + 2) ' WhiteSmoke

        PictureBox2.Location = New Point(3, 3)
        PictureBox2.Size = New Size(61, 49)

        PictureBox3.Location = New Point(10, 15)
        PictureBox3.Size = New Size(71, 53)
        PictureBox3.SizeMode = PictureBoxSizeMode.CenterImage

        PropertyGrid1.Location = New Point(627, 83)
        PropertyGrid1.Size = New Size(391, 458)

        'RadioButton1.Location = New Point(55, 8)
        ' RadioButton1.Size = New Size(54, 17)

        '  RadioButton2.Location = New Point(115, 8)
        'RadioButton2.Size = New Size(57, 17)

        RichTextBox1.Location = New Point(5, 433)
        RichTextBox1.Size = New Size(1020, 132)



        Splitter1.Location = New Point(0, 549)
        Splitter1.Size = New Size(1030, 23)

        TextBox1.Location = New Point(86, 38)
        TextBox1.Size = New Size(155, 22)

        TextBox2.Location = New Point(86, 64)
        TextBox2.Size = New Size(155, 24)

        ToolStrip1.Location = New Point(-6, 56)
        ToolStrip1.Size = New Size(529, 25)

        ToolStrip2.Location = New Point(-6, 96)
        ToolStrip2.Size = New Size(67, 78)



        ToolStrip3.Location = New Point(627, 57)
        ToolStrip3.Size = New Size(179, 25)
        'ToolStrip3.Anchor = AnchorStyles.Right
        'ToolStrip4.Location = New Point(633,508)
        'Toolstrip4.Size = New Size(135,25)

        Panel6.Hide()

        ' Panel5.Hide()

        ToolStripButton7.ToolTipText = "Save"
        ToolStripButton8.ToolTipText = "Undo"
        ToolStripButton9.ToolTipText = "Redo"
        ToolStripButton10.ToolTipText = "Map"
        ToolStripButton11.ToolTipText = "Compile"


        TransparentDrawing2.Location = New Point(9, 61)
        TransparentDrawing2.Size = New Size(10, 25)
        TransparentDrawing2.BringToFront()
        TransparentDrawing2.Hide()

        ToolStrip4.Location = New Point(9, 55)
        ToolStrip4.Hide()

        ToolStrip5.Location = New Point(9, 55)
        ToolStrip5.Hide()

        RichTextBox1.Hide()

        Splitter1.Hide()
        ' Splitter1.Width = 800
        Me.KeyPreview = True
        'FastColoredTextBoxNS.FindForm.DefaultBackColor = Color.Aqua

        Textbox3.DoAutoIndent()
        Me.Controls.Add(Textbox3)
        ' TextBox3.ImeMode = Windows.Forms.ImeMode.On
        ' listview location = 29; 77
        ' TextBox3.Hide()
        ListView1.Hide()
        ListView1.Enabled = False
        ListView1.SmallImageList = ImageList1
        tools(0) = "   Button"
        tools(1) = "   Canvas"
        tools(2) = "   CheckBox"
        tools(3) = "   EmailPicker"
        tools(4) = "   Label"
        tools(5) = "   Listview"
        tools(6) = "   Panel"
        tools(7) = "   PasswordTextBox"
        tools(8) = "   PictureBox"
        tools(9) = "   ProgressBar"
        tools(10) = "   RadioButton"
        tools(11) = "   TreeView"
        tools(12) = "   TextBox"
        tools(13) = "   Timer"
        tools(14) = "   ComboBox"
        tools(15) = "   VB4AWeb"

        tooltags(0) = "A Button Component"
        tooltags(1) = "A Component to draw on"
        tooltags(2) = "A Checkbox for multiselection"
        tooltags(3) = "A Textbox with email auto-completing from your contacts"
        tooltags(4) = "A Component to hold texts"
        tooltags(5) = "A Listview Component"
        tooltags(6) = "A Container Component"
        tooltags(7) = "A Textbox with masks"
        tooltags(8) = "A Picturebox Component"
        tooltags(9) = "A Progressbar Component"
        tooltags(10) = "A RadioButton for single selection"
        tooltags(11) = "{Not Suppotrted yet}"
        tooltags(12) = "A TextBox"
        tooltags(13) = "A Timer"
        tooltags(14) = "A ComboBox (spinner)"
        tooltags(15) = "A Webkit based browser"

        ListView1.ShowItemToolTips = True

        For i = 0 To tools.Length - 1

            ListView1.Items.Add(tools(i))

            ListView1.Items(i).ImageIndex = i 'Align ImageList Images With Array Items
            ListView1.Items(i).ToolTipText = tooltags(i)
        Next

        With Textbox3
            'AddHandler .TextChanged, AddressOf Textbox3_TextChanged
            .Font = New Font("Consolas", CSng(11))
            .Language = Language.Custom
            .Location = New Point(12, 81)
            .Size = New Size(1006, 460)
            .BackColor = Color.FromArgb(249, 249, 249)
            .ForeColor = Color.Black
            .BorderStyle = BorderStyle.FixedSingle
            .LineNumberColor = Color.Black
            .IndentBackColor = Color.FromArgb(230, 230, 230)
            '.SyntaxHighlighter.BlueStyle = New TextStyle(Brushes.Blue, Nothing, FontStyle.Regular)
            '.SyntaxHighlighter.Green2style = New TextStyle(Brushes.Black, Nothing, FontStyle.Regular)
            '.SyntaxHighlighter.Green2style = New TextStyle(Brushes.DarkGreen, Nothing, FontStyle.Regular)
            .SelectionColor = Color.LightBlue
            .ServiceLinesColor = Color.Black
            .Paddings = New System.Windows.Forms.Padding(0)
            .BringToFront()
            .CaretColor = Color.Black
            .CurrentLineColor = Color.Gray
            .ImeMode = Windows.Forms.ImeMode.On 'Windows.Forms.ImeMode.On  'System.Globalization.ChineseLunisolarCalendar.ChineseEra
            .Cursor = Cursors.IBeam
            .ShowFoldingLines = True
        End With
        ' TextBox3.ImeMode = Windows.Forms.ImeMode.OnHalf
        'Textbox3.Language = Language.Custom







        Textbox3.Zoom = 105

        'TextBox3.CharWidth = 15




        Textbox3.Hide()
        border2.BringToFront()




        'TextBox3.ImeMode = Windows.Forms.ImeMode.OnHalf

        ToolTip1.SetToolTip(CheckBox2, "        [LocationSensor]" & vbNewLine & "A Component implies GPS")
        ToolTip1.SetToolTip(CheckBox3, "  [OrientationSensor]" & vbNewLine & "An Orientation Sensor")
        ToolTip1.SetToolTip(CheckBox4, "[VB4ASocket]") '& vbNewLine & "")
        ToolTip1.SetToolTip(CheckBox5, "[AccelerometerSensor]" & vbNewLine & "        Accelerometer")
        ToolTip1.SetToolTip(CheckBox6, "[GyroscopeSensore]" & vbNewLine & "    A Gyro. Senrsor")
        ToolTip1.SetToolTip(CheckBox7, " [VB4ALightSensor]" & vbNewLine & "    A Light Sensor")
        ToolTip1.SetToolTip(CheckBox8, "[VB4ATempetureSensor]" & vbNewLine & "  A Tempeture Sensor")

        Dim fSt As FileStream
        If My.Computer.Name.Contains("-PC") Then UsernamePC = UsernamePC.Replace("-PC", "")
        If Not Directory.Exists(CurrentHardDriver & "VB4Android\Projects\") Then Directory.CreateDirectory(CurrentHardDriver & "VB4Android\Projects\")
        If Not Directory.Exists(CurrentHardDriver & "VB4Android\Programing Languages\") Then
            Directory.CreateDirectory(CurrentHardDriver & "VB4Android\Programing Languages\")
            fSt = File.Create(CurrentHardDriver & "VB4Android\Programing Languages\VB6_EN.pLang")
            fSt.Close()
        End If


        If Not Directory.Exists(CurrentHardDriver & "VB4Android\Languages\") Then

            Directory.CreateDirectory(CurrentHardDriver & "VB4Android\Languages\")



            fSt = File.Create(CurrentHardDriver & "VB4Android\Languages\" & "English.Lang")
            fSt.Close()
            fSt = File.Create(CurrentHardDriver & "VB4Android\Languages\" & "中文.Lang") '中国的 '中文
            fSt.Close()
            fSt = File.Create(CurrentHardDriver & "VB4Android\Languages\" & "Ελληνικά.Lang")
            fSt.Close()

            'fSt = File.Create(CurrentHardDriver & "VB4Android\Languages\" & "English.LangMeth")
            'fSt.Close()
            'fSt = File.Create(CurrentHardDriver & "VB4Android\Languages\" & "中文.LangMeth") '中国的 '中文
            ' fSt.Close()
            ' fSt = File.Create(CurrentHardDriver & "VB4Android\Languages\" & "Ελληνικά.LangMeth")
            'fSt.Close()
        End If

        For Each Lang As String In Directory.GetFiles(CurrentHardDriver & "VB4Android\Languages\", "*.Lang", SearchOption.TopDirectoryOnly).[Select](Function(nm) Path.GetFileName(nm))
            ToolStripDropDownButton2.DropDownItems.Add(Lang.ToString.Replace(".Lang", ""))
            If My.Settings.LangSave = Lang.ToString.Replace(".Lang", "") Then
                ToolStripDropDownButton2.DropDownItems.Item(ToolStripDropDownButton2.DropDownItems.Count - 1).PerformClick()
            End If
        Next

        For Each Lang As String In Directory.GetFiles(CurrentHardDriver & "VB4Android\Programing Languages\", "*.pLang", SearchOption.TopDirectoryOnly).[Select](Function(nm) Path.GetFileName(nm))
            ToolStripMenuItem3.DropDownItems.Add(Lang.ToString.Replace(".pLang", ""))
            If My.Settings.PLangSave = Lang.ToString.Replace(".pLang", "") Then
                ToolStripMenuItem3.DropDownItems.Item(ToolStripMenuItem3.DropDownItems.Count - 1).PerformClick()
            End If
        Next

        Timer1.Stop()
        Timer1.Interval = 240


        Combobox1.Sorted = True

        Label8.Hide()




        Textbox3.AutoCompleteBrackets = True
        PictureBox1.AutoScroll = True
        Panel5.Location = New Point(44, -1)
        Panel5.Size = New Size(XofFormApp + 2, YofFormApp + 2) ' XofFormApp xYofFormApp


        PictureBox4.Location = Panel5.Location + New Point(0, 1)
        PictureBox4.Size = New Size(XofFormApp, YofFormApp)

        'Panel5.MaximumSize = New Size(242, YofFormApp)
        'Panel5.MinimumSize = New Size(242, YofFormApp)

        ToolStripTextBox1.Size = New Size(190, 23)

        'ToolStripComboBox1.Size = New Size(190, 150)

        ToolStripComboBox1.Items.Add("Black")
        ToolStripComboBox1.Items.Add("Black.NoTitleBar")
        ToolStripComboBox1.Items.Add("Black.NoTitleBar.Fullscreen")
        ToolStripComboBox1.Items.Add("Dialog")
        ToolStripComboBox1.Items.Add("InputMethod")
        ToolStripComboBox1.Items.Add("Light")
        ToolStripComboBox1.Items.Add("Light.NoTitleBar.Fullscreen")
        ToolStripComboBox1.Items.Add("Light.Panel")
        ToolStripComboBox1.Items.Add("Light.WallpaperSettings")
        ToolStripComboBox1.Items.Add("NoDisplay")
        ToolStripComboBox1.Items.Add("Translucent.NoTitleBar.Fullscreen")
        ToolStripComboBox1.Items.Add("Wallpaper.NoTitleBar.Fullscreen")


        AddToolStripMenuItem.Enabled = False
        RemoveToolStripMenuItem.Enabled = False

        ToolStripTextBox2.Size = New Size(170, 23)
        ToolStripTextBox2.Enabled = False

        ToolStripButton5.PerformClick()

        ShadowForm.Show()

        ToolStripTextBox3.Text = XofFormApp
        ToolStripTextBox4.Text = YofFormApp

        'FirsttimeBool = True

        ' Me.BackColor = Color.White
    End Sub
    <DllImportAttribute("user32.dll")>
    Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    End Function

    <DllImportAttribute("user32.dll")>
    Public Shared Function ReleaseCapture() As Boolean
    End Function

    Public Const WM_NCLBUTTONDOWN As Integer = &HA1
    Public Const HT_CAPTION As Integer = &H2
    Private Sub XXX_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel4.MouseDown, Label5.MouseDown, PictureBox2.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            ReleaseCapture()
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0)
        End If
    End Sub

    'Dim Loc As Point
    'Private Sub Panel4_MouseDown1(sender As Object, e As MouseEventArgs) Handles Panel4.MouseDown
    '    If e.Button = Windows.Forms.MouseButtons.Left Then
    '        cliornot = False
    '        Loc = e.Location
    '    End If
    'End Sub

    'Private Sub Panel4_MouseMove1(sender As Object, e As MouseEventArgs) Handles Panel4.MouseMove
    '    If e.Button = Windows.Forms.MouseButtons.Left Then
    '        Me.Location += e.Location - Loc
    '    End If
    'End Sub

    'Private Sub Label5_MouseDown(sender As Object, e As MouseEventArgs) Handles Label5.MouseDown
    '    If e.Button = Windows.Forms.MouseButtons.Left Then
    '        cliornot = False
    '        Loc = e.Location
    '    End If
    'End Sub

    'Private Sub Label5_MouseMove(sender As Object, e As MouseEventArgs) Handles Label5.MouseMove
    '    If e.Button = Windows.Forms.MouseButtons.Left Then
    '        Me.Location += e.Location - Loc
    '    End If
    'End Sub
    Public Shared Dnt As Boolean = False ' meh :33
    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        Dnt = True


        'DonateFRM.BringToFront()

        Using New Centered_MessageBox(Me)
            DonateFRM.Show()
            Select Case MsgBox("Wanna Buy Us Some Coffee! :D ???" & vbNewLine & vbNewLine & "    --Donation for Compiler at this PayPal Email:" & vbNewLine & "      {yourdds520@163.com}" & vbNewLine & vbNewLine & "    --Donation for IDE at this PayPal Email:" & vbNewLine & "      {gxousos@gmail.com  }", vbYesNo, "Donate?!?!")
                Case MsgBoxResult.Yes
                    'donate URL
                    DonateFRM.Close()
                Case MsgBoxResult.No
                    DonateFRM.Close()
                    Using New Centered_MessageBox(Me)
                        MessageBox.Show("Thanks For your Support xD !")
                    End Using
            End Select

        End Using
    End Sub


    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
    End Sub


    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim MinimizeBugPoint As Point

        MinimizeBugPoint = Me.Location
        'ShadowForm.Size = Me.Size + New Size(2, 2)
        'ShadowForm.Location = Me.Location - New Point(1, 1)
        Me.WindowState = FormWindowState.Minimized ' it has a bug me.Y = -300...
        Me.Location = MinimizeBugPoint
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        '  Me.WindowState = FormWindowState.Maximized
    End Sub
    Public ProjectNewUserSel As Boolean = False

    'New Project
    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click

        If Label4.Text = Nothing Then
            'Form2NewProject.ShowInTaskbar = False
            Form2NewProject.ShowDialog()
        Else
            ' MsgBox("I am working on it LoL i just have to clear all..... :P For now just restart the exe ")

            '  ProjectNewUserSel = True
            ' Form2NewProject.fs.Close()


            ' If ProjectNewUserSel = False Then GoTo endof
            If RichTextBox1.Visible = False Then Button10.PerformClick()
            Panel5.Hide()
            If Combobox3.Items.Count = 1 Then

                SaveToolStripMenuItem1.PerformClick()
            Else
                SaveToolStripMenuItem.PerformClick()
            End If
            Panel5.Show()
            clear_For_New_Project()

            'Form2NewProject.ShowInTaskbar = False
            Form2NewProject.ShowDialog()


            If RichTextBox1.Visible = True Then Button10.PerformClick()
            'Button10.PerformClick()


        End If


    End Sub
    Private Sub clear_For_New_Project()
        Panel1.Enabled = False
        Panel2.Enabled = False
        Panel3.Enabled = False

        Panel5.Enabled = False

        ListView1.Enabled = False

        'PrevlnnNum = -1

        Textbox3.ClearUndo()

        PropertyGrid1.Enabled = False

        ToolStripTextBox1.Enabled = False
        ToolStripTextBox2.Enabled = False
        'ToolStripComboBox1.Enabled = False
        'bhcomp = True
        'sources = {""}
        BuildAutocompleteMenu()

        Timer2.Enabled = False

        Combobox1.Items.Clear()
        Combobox3.Items.Clear()

        Try
            Panel5.BackgroundImage.Dispose()
        Catch ex As Exception
        End Try
        Panel5.BackgroundImage = Nothing

        Panel5.BackColor = Color.WhiteSmoke

        TextBox1.Text = "{My.Resources.Icon}"
        Try
            PictureBox3.Image.Dispose()
            PictureBox3.Image = Nothing
        Catch ex As Exception
        End Try

        Label4.Text = Nothing
        ConsoleDouble = 0
        RichTextBox1.Text = Nothing
        GenControl = Nothing
        PropertiesOfControl = Nothing
        NameOfControlBeforeClick = Nothing

        CursorDenDiaforetik = True
        loca = New Point
        mousenowclick = 0
        mCl = 0
        mousemoves = False
        mouseCanClick = False
        widthContr = Nothing
        _cursorStartPoint = New Point
        _currentControlStartSize = New Point
        panSE = False
        panNE = False
        panNW = False
        panSW = False
        noMoveHoriz = False
        noMoveHorizLEFT = False
        noMoveHorizRIGHT = False
        noMoveVert = False
        noMoveVertPanw = False
        noMoveVertKatw = False
        ' isLVselec = False
        ' textOfTextbox = Nothing

        propertiesOfForm = Nothing


        Panel5.Controls.Clear()
        Panel5.Controls.Add(TransparentDrawing1) ' omg i find when i had the problem LoL xD 12/7/2015
        TransparentDrawing1.Location = New Point(0, 0)
        TransparentDrawing1.Refresh()

        ButtonString = String.Empty
        CanvasString = String.Empty
        CheckBoxString = String.Empty
        LabelString = String.Empty
        ListBoxString = String.Empty
        PanelString = String.Empty
        PasswordTextBoxString = String.Empty
        PictureBoxString = String.Empty
        ProgressBarString = String.Empty
        RadioButtonString = String.Empty
        TextBoxString = String.Empty
        TimerString = String.Empty
        ComboBoxString = String.Empty
        VB4AWebString = String.Empty

        pnl = Nothing

        PropertiesOfControl = Nothing  ' einai kai writeable 
        NameOfControlBeforeClick = Nothing

        click1 = 0
        controlForContext = Nothing


        BeforeCombo3 = "" ' "form1.vb4a"
        firsttimechange = True
        cmb = Combobox1.SelectedItem
        tagsForCombo = Nothing

        ba1 = 0
        ba2 = 0
        ba3 = 0
        ba4 = 0
        ba5 = 0
        ba6 = 0
        ba7 = 0
        ba8 = 0
        ba9 = 0
        ba10 = 0
        ba11 = 0
        ba12 = 0
        ba13 = 0
        ba14 = 0


        cntrafe1 = 0
        cntrafe2 = 0
        cntrafe3 = 0
        cntrafe4 = 0
        cntrafe5 = 0
        cntrafe6 = 0
        cntrafe7 = 0
        cntrafe8 = 0
        cntrafe9 = 0
        cntrafe10 = 0
        cntrafe11 = 0
        cntrafe12 = 0
        cntrafe13 = 0
        cntrafe14 = 0

        ProjectSaveString = Nothing

        idControl = 0

        Is_Any_Cont_Delted = False
        FormDeleted = False

        ' cGCPSpec = True

        CheckBox1.Checked = False
        CheckBox2.Checked = False
        CheckBox3.Checked = False
        CheckBox4.Checked = False
        CheckBox5.Checked = False
        CheckBox6.Checked = False
        CheckBox7.Checked = False
        CheckBox8.Checked = False

        ComboboxImage1.Items.Clear()
        ComboboxImage2.Items.Clear()

        Textbox3.Text = "" ' if you reamove this and you have created a project named asd and form1 with button1 and a form named sd when load_project() it will appeard sd.vb4acode in textbox3 instead of Form1.vb4acode. 17/4/2015 I dont have idea why the !#!$%# i have this and if it is true xD!

        rc = Nothing

        c = Nothing ' 7/11/2015

        ToolStripComboBox2.Items.Clear()
        AddToolStripMenuItem.Enabled = False
        RemoveToolStripMenuItem.Enabled = False
        ChangeToolStripMenuItem.Enabled = False
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ListView1.Enabled = False Then
            Using New Centered_MessageBox(Me)
                MsgBox("First Create New Project.", MsgBoxStyle.Information)

            End Using
        Else
            Form3CreateNewForm.ShowInTaskbar = False
            Form3CreateNewForm.ShowDialog()

        End If

        ' Form2NewProject.fs = File.Create("C:\Users\" & UsernamePC & "\Documents\VB4Android\Projects\" & TextBox1.Text & "\" & TextBox1.Text & ".Frm")
    End Sub
    ' Private Sub TextBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    If (Microsoft.VisualBasic.Asc(e.KeyChar) < 48) Or (Microsoft.VisualBasic.Asc(e.KeyChar) > 57) Then
    '        e.Handled = True
    '    End If
    '   If (Microsoft.VisualBasic.Asc(e.KeyChar) = 8) Then
    '       e.Handled = False
    '    End If
    'End Sub
    ' Private Sub TextBox2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    If (Microsoft.VisualBasic.Asc(e.KeyChar) < 48) Or (Microsoft.VisualBasic.Asc(e.KeyChar) > 57) Then
    '        e.Handled = True
    '    End If
    '    If (Microsoft.VisualBasic.Asc(e.KeyChar) = 8) Then
    '        e.Handled = False
    '    End If
    'End Sub


    Private Sub ToolStripButton6_Click(sender As Object, e As EventArgs) Handles ToolStripButton6.Click
        ' Dim b As New SolidBrush(ColorTranslator.FromHtml("#003CFF")) kanonika sto mauro einai Color.CornflowerBlue

        If Not Label4.Text = "" Then
            If ListView1.Visible = True Then ListView1.Hide()
            TransparentDrawing2.Show()
            Textbox3.Select()
            ToolStrip4.Show()
            Textbox3.Show()
            'MsgBox(Textbox3.Size)
            'Textbox3.Size = New Size(1006, 460)
            ToolStrip2.Hide()
            ToolStrip1.Hide()
            ComboboxImage2.Show() 'events
            ComboboxImage1.Show()
            ToolStrip3.Location = New Point(877 + plinCodBut, 56) '627,56
            ToolStrip3.Location = New Point(Me.Width - 153 + plinCodBut, 56)
            'ToolStrip3.BringToFront()

        Else
            Using New Centered_MessageBox(Me)
                MsgBox("First Create New Project.", vbInformation)
            End Using
        End If
    End Sub
    Dim FormDeleted As Boolean = False


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Not Label4.Text = Nothing Then

            If Combobox3.Text = "form1.vb4a" Then
                Using New Centered_MessageBox(Me)
                    Select Case MsgBox("form1.vb4a form cant be deleted, because is" & vbNewLine & "the main form.If you want to clear" & vbNewLine & "it from controls select yes", vbYesNo, "Clear form1.")
                        Case MsgBoxResult.Yes
                            MsgBox("It is not supported yet xD but it will be (it is not dificult).")
                        Case MsgBoxResult.No
                    End Select
                End Using

                Exit Sub

            End If

            Using New Centered_MessageBox(Me)
                Select Case MsgBox("Are You Sure You Want to Delete This Form?", vbYesNo, "Delete Form.")
                    Case MsgBoxResult.Yes

                        'preipei sto save part na po8ikeuw olla ta cntrafe kai na ta kanv load apo to open kateu8ian 




                        Dim panelsss As String = Nothing
                        Dim IsLineAControl As Boolean = True
                        For Each Line As String In ProjectSaveString.Split(vbNewLine)
                            Line = Line.Replace(Chr(10), "")
                            Dim contrstr As String = Line.Replace("<", "").Replace(">", "")

                            IsLineAControl = True


                            If Line.StartsWith("<") AndAlso Not Line.StartsWith("</") Then SaveDeletedFormInf(Line.Replace("<", "").Replace(">", ""))

                            If Line.StartsWith("<AButton") AndAlso Not Line.StartsWith("</AButton") Then : cntrafe1 += 1
                            ElseIf Line.StartsWith("<Canvas") AndAlso Not Line.StartsWith("</Canvas") Then : cntrafe2 += 1
                            ElseIf Line.StartsWith("<ACheckBox") AndAlso Not Line.StartsWith("</ACheckBox") Then : cntrafe3 += 1
                            ElseIf Line.StartsWith("<ALabel") AndAlso Not Line.StartsWith("</ALabel") Then : cntrafe4 += 1
                            ElseIf Line.StartsWith("<AListview") AndAlso Not Line.StartsWith("</AListview") Then : cntrafe5 += 1
                            ElseIf Line.StartsWith("<APanel") AndAlso Not Line.StartsWith("</APanel") Then : cntrafe6 += 1 : panelsss &= contrstr & vbNewLine
                            ElseIf Line.StartsWith("<PasswordTextBox") AndAlso Not Line.StartsWith("</PasswordTextBox") Then : cntrafe7 += 1
                            ElseIf Line.StartsWith("<APictureBox") AndAlso Not Line.StartsWith("</APictureBox") Then : cntrafe8 += 1
                            ElseIf Line.StartsWith("<AProgressBar") AndAlso Not Line.StartsWith("</AProgressBar") Then : cntrafe9 += 1
                            ElseIf Line.StartsWith("<ARadioButton") AndAlso Not Line.StartsWith("</ARadioButton") Then : cntrafe10 += 1
                            ElseIf Line.StartsWith("<ATextBox") AndAlso Not Line.StartsWith("</ATextBox") Then : cntrafe11 += 1
                            ElseIf Line.StartsWith("<ATimer") AndAlso Not Line.StartsWith("</ATimer") Then : cntrafe12 += 1
                            ElseIf Line.StartsWith("<AComboBox") AndAlso Not Line.StartsWith("</AComboBox") Then : cntrafe13 += 1
                            ElseIf Line.StartsWith("<VB4AWeb") AndAlso Not Line.StartsWith("</VB4AWeb") Then : cntrafe14 += 1
                            Else : IsLineAControl = False
                            End If



                            If IsLineAControl = True Then

                                If Panel5.Controls.Contains(Panel5.Controls(contrstr)) Then

                                    If Line.StartsWith("<") AndAlso Not Line.StartsWith("</") Or Line.StartsWith(Chr(10) & "<") AndAlso Not Line.StartsWith(Chr(10) & "</") Then Panel5.Controls.Remove(DirectCast(FindControl(contrstr, Me), Control))

                                ElseIf Not panelsss = Nothing Then
                                    'MsgBox(ex.ToString)




                                    If Line.StartsWith("<") AndAlso Not Line.StartsWith("</") Or Line.StartsWith(Chr(10) & "<") AndAlso Not Line.StartsWith(Chr(10) & "</") Then : If panelsss.Contains(CheckIfaPropOfControlIs2(contrstr, "IsOnPanel=")) = False Then : DirectCast(FindControl(RetriveInWhatPanelis(DirectCast(FindControl(contrstr, Me), Control)), Me), Panel).Controls.Remove(DirectCast(FindControl(contrstr, Me), Control)) : End If : End If

                                End If

                            End If
                        Next


                        panelsss = Nothing




                        My.Computer.FileSystem.DeleteFile(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Combobox3.Text.Replace(".vb4a", ".vb4acode"))
                        My.Computer.FileSystem.DeleteFile(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Combobox3.Text)

                        If Not My.Computer.FileSystem.ReadAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Combobox3.Text.Replace(".vb4a", ".DelConts")) = Nothing Then
                            For Each Line As String In My.Computer.FileSystem.ReadAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Combobox3.Text.Replace(".vb4a", ".DelConts")).Split(vbNewLine)
                                Line = Line.Replace(Chr(10), "") ' ποτε μην βαζει κανεις "Nothing" σε "Replace" xD γτ δεν το κανει τιποτε χΔ .... αυτη την βλακια ειχα κανει εγω και ..... xD



                                If Line.StartsWith("AButton") Then
                                    cntrafe1 += 1 : SaveDeletedFormInf(Line)
                                ElseIf Line.StartsWith("Canvas") Then
                                    cntrafe2 += 1 : SaveDeletedFormInf(Line)
                                ElseIf Line.StartsWith("ACheckBox") Then
                                    cntrafe3 += 1 : SaveDeletedFormInf(Line)
                                ElseIf Line.StartsWith("ALabel") Then
                                    cntrafe4 += 1 : SaveDeletedFormInf(Line)
                                ElseIf Line.StartsWith("AListview") Then
                                    cntrafe5 += 1 : SaveDeletedFormInf(Line)
                                ElseIf Line.StartsWith("APanel") Then
                                    cntrafe6 += 1 : SaveDeletedFormInf(Line)
                                ElseIf Line.StartsWith("PasswordTextBox") Then
                                    cntrafe7 += 1 : SaveDeletedFormInf(Line)
                                ElseIf Line.StartsWith("APictureBox") Then
                                    cntrafe8 += 1 : SaveDeletedFormInf(Line)
                                ElseIf Line.StartsWith("AProgressBar") Then
                                    cntrafe9 += 1 : SaveDeletedFormInf(Line)
                                ElseIf Line.StartsWith("ARadioButton") Then
                                    cntrafe10 += 1 : SaveDeletedFormInf(Line)
                                ElseIf Line.StartsWith("ATextBox") Then
                                    cntrafe11 += 1 : SaveDeletedFormInf(Line)
                                ElseIf Line.StartsWith("ATimer") Then
                                    cntrafe12 += 1 : SaveDeletedFormInf(Line)
                                ElseIf Line.StartsWith("AComboBox") Then
                                    cntrafe13 += 1 : SaveDeletedFormInf(Line)
                                ElseIf Line.StartsWith("VB4AWeb") Then
                                    cntrafe14 += 1 : SaveDeletedFormInf(Line)
                                End If


                            Next
                        End If

                        My.Computer.FileSystem.DeleteFile(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Combobox3.Text.Replace(".vb4a", ".DelConts")) '.vb4acode

                        ' My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj", "" & vbNewLine, True)

                        Combobox3.Items.Remove(Combobox3.SelectedItem)
                        Combobox1.Items.Clear()

                        FormDeleted = True
                        ProjectSaveString = Nothing


                        Panel5.BackColor = Color.WhiteSmoke
                        Textbox3.Text = ""

                        Combobox3.SelectedItem = Combobox3.Items.Item(Combobox3.Items.Count - 1)

                    Case MsgBoxResult.No

                End Select
            End Using
        Else
            Using New Centered_MessageBox(Me)
                MsgBox("Create Project First Project>New .", MsgBoxStyle.Information)
            End Using

        End If

    End Sub
    Private Sub SaveDeletedFormInf(cont As String)
        My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".CADel", cont & vbNewLine, True)
    End Sub
    Private Sub ToolStripButton5_Click(sender As Object, e As EventArgs) Handles ToolStripButton5.Click
        ToolStrip2.Show()
        TransparentDrawing2.Hide()
        'TransparentDrawing2.BackColor = Color.AliceBlue
        Textbox3.Size = New Size(Me.Size.Width - 24, Textbox3.Height)
        HideMap = False
        ToolStrip4.Hide()
        ToolStrip5.Hide()
        ToolStrip1.Show()
        documentMap1.Hide()
        Panel6.Hide()
        ToolStrip3.Location = New Point(627, 57)
        ToolStrip3.Location = New Point(Me.Width - 403, 57)
        'Textbox3.Size = New Size(Me.Size.Width - 24, Textbox3.Size.Height)
        ComboboxImage2.Hide()
        ComboboxImage1.Hide()
        Label7.Hide()
        Textbox3.Hide()

        If Not c Is Nothing Then


            TransparentDrawing1.Refresh() ' even if i think that it doesnt need

            Dim j As Graphics = TransparentDrawing1.CreateGraphics
            j.FillRectangle(colorBrushDG, 7 - 5, 7 - 4, 1, c.Size.Height + 7) ' Left
            j.FillRectangle(colorBrushDG, 7 + c.Size.Width + 4, 7 - 4, 1, c.Size.Height + 7) ' Right

            j.FillRectangle(colorBrushDG, 7 - 4, 7 - 5, c.Size.Width + 7, 1) 'Up
            j.FillRectangle(colorBrushDG, 7 - 4, 7 + c.Size.Height + 4, c.Size.Width + 7, 1) ' down

            j.FillRectangle(colorBrushB, 7 - 7, 7 - 7, 5, 5) ' LEFT UP CORNER
            j.FillRectangle(colorBrushB, 7 - 7, 7 + c.Size.Height + 2, 5, 5) ' LEFT DOWN CORNER

            j.FillRectangle(colorBrushB, 7 + c.Size.Width + 2, 7 - 7, 5, 5) ' RIGHT UP CORNER
            j.FillRectangle(colorBrushB, 7 + c.Size.Width + 2, 7 + c.Size.Height + 2, 5, 5) ' RIGHT DOWN CORNER 

        End If
    End Sub

    Dim ecode As Boolean = False
    Public frgt As String = String.Empty

    '  Enum ZoomDirection
    '     None
    '     Up
    '     Down
    '  End Enum
    '  Dim CtrlIsDown As Boolean
    ' Dim ZoomValue As Integer = 10

    ' Private Sub Zoom(ByVal direction As ZoomDirection)
    'change the zoom value based on the direction passed

    '   Select Case direction
    '      Case ZoomDirection.Up
    '         If Not ZoomValue = 20 Then
    '             ZoomValue += 1
    '             TextBox3.Font = New Font("Consolas", CSng(ZoomValue))
    '            Label7.Text = "- [" & ZoomValue & "]"
    '    End If
    '    Case ZoomDirection.Down
    '       If Not ZoomValue = 1 Then
    '               ZoomValue -= 1
    '               TextBox3.Font = New Font("Consolas", CSng(ZoomValue))
    '               Label7.Text = "- [" & ZoomValue & "]"
    '           End If
    '        Case Else
    'do nothing
    '    End Select

    ' End Sub

    ' Dim SyntaxWordMatch As String = Nothing
    Dim HelpStringComp As String

    Private Sub Textbox3_KeyPressed(sender As Object, e As KeyPressEventArgs) Handles Textbox3.KeyPressed
        If Textbox3.Text = "" Then Textbox3.Text = " "
    End Sub
    Private Sub TextBox3_KeyUp(sender As Object, e As KeyEventArgs) Handles Textbox3.KeyUp
        bcksps = True
        'If popupMenu.Visible = True Then popupMenu2.MinFragmentLength = popupMenu.Items.FocussedItem.Text.Length + 1 : popupMenu2.Hide() Else popupMenu2.MinFragmentLength = 2 : popupMenu2.Hide()
        'If popupMenu.Visible = True Then popupMenu2.MinFragmentLength = popupMenu.Items.FocussedItem.Text.Length + 1 : popupMenu2.Hide() Else popupMenu2.MinFragmentLength = 2 : popupMenu2.Hide()
        'If popupMenu2.Visible = True Then popupMenu.MinFragmentLength = popupMenu2.Items.FocussedItem.Text.Length + 1 : popupMenu.Hide() Else popupMenu.MinFragmentLength = 1 : popupMenu.Hide()
        '   If e.KeyCode = Keys.ControlKey Then
        'Label7.Text = Textbox3.Zoom
        '   Label7.Hide()

        '  End If

        '  If e.KeyCode = Keys.A Or e.KeyCode = Keys.B Or e.KeyCode = Keys.C Or e.KeyCode = Keys.D Or e.KeyCode = Keys.E Or e.KeyCode = Keys.F Or _
        '     e.KeyCode = Keys.G Or e.KeyCode = Keys.H Or e.KeyCode = Keys.I Or e.KeyCode = Keys.J Or e.KeyCode = Keys.K Or e.KeyCode = Keys.L Or _
        '     e.KeyCode = Keys.M Or e.KeyCode = Keys.N Or e.KeyCode = Keys.O Or e.KeyCode = Keys.P Or e.KeyCode = Keys.Q Or e.KeyCode = Keys.R Or _
        '     e.KeyCode = Keys.S Or e.KeyCode = Keys.T Or e.KeyCode = Keys.U Or e.KeyCode = Keys.V Or e.KeyCode = Keys.W Or e.KeyCode = Keys.X Or _
        '    e.KeyCode = Keys.Y Or e.KeyCode = Keys.Z Or e.KeyCode = Keys.D1 Or e.KeyCode = Keys.D2 Or e.KeyCode = Keys.D3 Or e.KeyCode = Keys.D4 Or _
        '     e.KeyCode = Keys.D5 Or e.KeyCode = Keys.D6 Or e.KeyCode = Keys.D7 Or e.KeyCode = Keys.D8 Or e.KeyCode = Keys.D9 Or e.KeyCode = Keys.D0 Or _
        '    e.KeyCode = Keys.NumPad1 Or e.KeyCode = Keys.NumPad2 Or e.KeyCode = Keys.NumPad3 Or e.KeyCode = Keys.NumPad4 Or e.KeyCode = Keys.NumPad5 Or e.KeyCode = Keys.NumPad6 Or _
        '    e.KeyCode = Keys.NumPad7 Or e.KeyCode = Keys.NumPad8 Or e.KeyCode = Keys.NumPad9 Or e.KeyCode = Keys.NumPad0 Then

        '    If e.KeyCode = Keys.D1 Then : SyntaxWordMatch &= 1
        '   ElseIf e.KeyCode = Keys.D2 Then : SyntaxWordMatch &= 2
        '    ElseIf e.KeyCode = Keys.D3 Then : SyntaxWordMatch &= 3
        '   ElseIf e.KeyCode = Keys.D4 Then : SyntaxWordMatch &= 4
        '   ElseIf e.KeyCode = Keys.D5 Then : SyntaxWordMatch &= 5
        '   ElseIf e.KeyCode = Keys.D6 Then : SyntaxWordMatch &= 6
        '  ElseIf e.KeyCode = Keys.D7 Then : SyntaxWordMatch &= 7
        '  ElseIf e.KeyCode = Keys.D8 Then : SyntaxWordMatch &= 8
        '  ElseIf e.KeyCode = Keys.D9 Then : SyntaxWordMatch &= 9
        '  ElseIf e.KeyCode = Keys.D0 Then : SyntaxWordMatch &= 0
        '  Else : SyntaxWordMatch &= e.KeyCode.ToString
        '  End If





        'ElseIf e.KeyCode = Keys.Space Then
        'SyntaxWordMatch = ""
        'ElseIf e.KeyCode = Keys.Back Then


        'If Not SyntaxWordMatch = "" Then SyntaxWordMatch = SyntaxWordMatch.Remove(SyntaxWordMatch.Length - 1)
        ''  Label4.Text = SyntaxWordMatch                             <---------------------------------------------------- Auto XRIAZETE peripou se String oxi sto label

        'ElseIf e.KeyCode = Keys.RShiftKey Or e.KeyCode = Keys.LShiftKey Then

        'Else
        'SyntaxWordMatch = ""
        'End If










        ' αυτο θα μου χριαστει απλα πρεπει να αλαξω απο το complete να γινετε και με την τελια εκτωσ απο το space και enter -----------------------------------------------++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        'If e.KeyCode = Keys.Space Or e.KeyCode = Keys.Enter Then
        '    HelpStringComp = ""
        'End If

        'If popupMenu.Visible = True Then


        '    If popupMenu.Items.FocussedItem.Text = "Integer " AndAlso Not HelpStringComp = "Integer " Then
        '        HelpStringComp = "Integer "

        '        methods = {"banana()", "asdasd()"}
        '        BuildAutocompleteMenu()


        '    End If
        'End If

    End Sub



    Private Sub TextBox3_MouseWheel(sender As Object, e As MouseEventArgs) Handles Textbox3.MouseWheel
        ' If CtrlIsDown Then
        ''evaluate the delta's sign and call the appropriate zoom command
        ' Select Case Math.Sign(e.Delta)
        '     Case Is < 0
        'Zoom(ZoomDirection.Down)
        '      Case Is > 0
        ' Zoom(ZoomDirection.Up)
        '      Case Else
        ' Zoom(ZoomDirection.None)
        '  End Select
        ' End If
    End Sub




    Private Sub Splitter1_SplitterMoved(sender As Object, e As SplitterEventArgs) Handles Splitter1.SplitterMoved

    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Timer2.Enabled = False
        If Splitter1.Visible = False Then
            With Splitter1
                .BringToFront()
                .Height = 140
                .Show()
            End With
            With RichTextBox1
                .Show()
                '.Location = New Point(5, 436)
                '.Size = New Size(Me.Size.Width - 10, RichTextBox1.Size.Height)
                .BringToFront()
            End With
            With Button10
                .Location = New Point((Me.Size.Width - Button10.Size.Width) \ 2, Me.Height - RichTextBox1.Height - 14)
                .BringToFront()
                .BackColor = SystemColors.ControlLight
            End With
        Else
            Splitter1.Hide()
            Button10.Location = New Point((Me.Size.Width - Button10.Size.Width) \ 2, Me.Height - (Button10.Height / 2) - 1)
            'Label8.BringToFront()
            RichTextBox1.Hide()
        End If


    End Sub



    Private Sub TextBox1_KeyUp(sender As Object, e As KeyEventArgs)  ' Xp

    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click ' About
        AboutFrm.Show()
        AboutFrm.BringToFront()
    End Sub


    Private Sub ToolStripButton8_Click(sender As Object, e As EventArgs) Handles ToolStripButton8.Click
        Textbox3.Undo()
    End Sub

    Private Sub ToolStripButton9_Click(sender As Object, e As EventArgs) Handles ToolStripButton9.Click
        Textbox3.Redo()
    End Sub


    Private Sub border1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        ' '' '' TextBox3.ImeMode = Windows.Forms.ImeMode.On
        ' '' '' Formabout.Show()
        ' '' ''  Formabout.BringToFront()

        ' HelpForm.Show()
        ' HelpForm.BringToFront()
    End Sub
    ' Public RealButtonNames As String = String.Empty







    Public GenControl As Control = Nothing ' 8a to xrisimopiiso gia na alazw apo to class ta names , ...
    Dim boolhelpEvents As Boolean ' i know xD why i am using this..... i am an idiot xD because i create first these    V          and .....
    Public Sub Go_or_Create_Event()
        ComboboxImage1.Text = GenControl.Tag

        If Not GenControl.Name.StartsWith("VB4AWeb") Or GenControl.Name.StartsWith("AProgressBar") Or GenControl.Name.StartsWith("APanel") Then
            If GenControl.Name.StartsWith("AButton") Or GenControl.Name.StartsWith("ALabel") Or GenControl.Name.StartsWith("APictureBox") Then
                If Not Textbox3.Text.Contains("Event " & GenControl.Tag & ".Click()") Then
                    Textbox3.Text &= vbNewLine & "Event " & GenControl.Tag & ".Click()" & vbNewLine & "    " & vbNewLine & "End Event" & vbNewLine
                    For Each r In Textbox3.Range.GetRanges("Event " & GenControl.Tag & ".Click()")
                        Textbox3.Selection = r
                        Textbox3.DoSelectionVisible()
                    Next
                ElseIf Textbox3.Text.Contains("Event " & GenControl.Tag & ".Click()") Then
                    For Each r In Textbox3.Range.GetRanges("Event " & GenControl.Tag & ".Click()")
                        Textbox3.Selection = r
                        Textbox3.DoSelectionVisible()
                    Next
                End If ' AButton ALabel APictureBox
                ComboboxImage2.Text = "Click()"
            ElseIf GenControl.Name.StartsWith("ATextBox") Or GenControl.Name.StartsWith("ARadioButton") Then
                If Not Textbox3.Text.Contains("Event " & GenControl.Tag & ".Change()") Then
                    Textbox3.Text &= vbNewLine & "Event " & GenControl.Tag & ".Change()" & vbNewLine & "    " & vbNewLine & "End Event" & vbNewLine
                    For Each r In Textbox3.Range.GetRanges("Event " & GenControl.Tag & ".Change()")
                        Textbox3.Selection = r
                        Textbox3.DoSelectionVisible()
                    Next
                ElseIf Textbox3.Text.Contains("Event " & GenControl.Tag & ".Change()") Then
                    For Each r In Textbox3.Range.GetRanges("Event " & GenControl.Tag & ".Change()")
                        Textbox3.Selection = r
                        Textbox3.DoSelectionVisible()
                    Next
                End If 'ATextBox ARadioButton
                ComboboxImage2.Text = "Change()"
            ElseIf GenControl.Name.StartsWith("AListview") Then
                If Not Textbox3.Text.Contains("Event " & GenControl.Tag & ".ItemClicked(") Then
                    Textbox3.Text &= vbNewLine & "Event " & GenControl.Tag & ".ItemClicked(item As Integer)" & vbNewLine & "    " & vbNewLine & "End Event" & vbNewLine
                    For Each r In Textbox3.Range.GetRanges("Event " & GenControl.Tag & ".ItemClicked")
                        Textbox3.Selection = r
                        Textbox3.DoSelectionVisible()
                    Next
                ElseIf Textbox3.Text.Contains("Event " & GenControl.Tag & ".ItemClicked(") Then
                    For Each r In Textbox3.Range.GetRanges("Event " & GenControl.Tag & ".ItemClicked")
                        Textbox3.Selection = r
                        Textbox3.DoSelectionVisible()
                    Next
                End If 'AListView
                ComboboxImage2.Text = "ItemClicked(item As Integer)"
            ElseIf GenControl.Name.StartsWith("Canvas") Then
                If Not Textbox3.Text.Contains("Event " & GenControl.Tag & ".VB4ADown(") Then
                    Textbox3.Text &= vbNewLine & "Event " & GenControl.Tag & ".VB4ADown(x As Integer, y As Integer)" & vbNewLine & "    " & vbNewLine & "End Event" & vbNewLine
                    For Each r In Textbox3.Range.GetRanges("Event " & GenControl.Tag & ".VB4ADown")
                        Textbox3.Selection = r
                        Textbox3.DoSelectionVisible()
                    Next
                ElseIf Textbox3.Text.Contains("Event " & GenControl.Tag & ".VB4ADown(") Then
                    For Each r In Textbox3.Range.GetRanges("Event " & GenControl.Tag & ".VB4ADown")
                        Textbox3.Selection = r
                        Textbox3.DoSelectionVisible()
                    Next
                End If 'Canvas
                ComboboxImage2.Text = "VB4ADown(x As Integer, y As Integer)"
            ElseIf GenControl.Name.StartsWith("ATimer") Then
                If Not Textbox3.Text.Contains("Event " & GenControl.Tag & ".Timer()") Then
                    Textbox3.Text &= vbNewLine & "Event " & GenControl.Tag & ".Timer()" & vbNewLine & "    " & vbNewLine & "End Event" & vbNewLine
                    For Each r In Textbox3.Range.GetRanges("Event " & GenControl.Tag & ".Timer()")
                        Textbox3.Selection = r
                        Textbox3.DoSelectionVisible()
                    Next
                ElseIf Textbox3.Text.Contains("Event " & GenControl.Tag & ".Timer()") Then
                    For Each r In Textbox3.Range.GetRanges("Event " & GenControl.Tag & ".Timer()")
                        Textbox3.Selection = r
                        Textbox3.DoSelectionVisible()
                    Next
                End If 'ATimer
                ComboboxImage2.Text = "Timer()"
            ElseIf GenControl.Name.StartsWith("ACheckBox") Then
                If Not Textbox3.Text.Contains("Event " & GenControl.Tag & ".Changed()") Then
                    Textbox3.Text &= vbNewLine & "Event " & GenControl.Tag & ".Changed()" & vbNewLine & "    " & vbNewLine & "End Event" & vbNewLine
                    For Each r In Textbox3.Range.GetRanges("Event " & GenControl.Tag & ".Changed()")
                        Textbox3.Selection = r
                        Textbox3.DoSelectionVisible()
                    Next
                ElseIf Textbox3.Text.Contains("Event " & GenControl.Tag & ".Changed()") Then
                    For Each r In Textbox3.Range.GetRanges("Event " & GenControl.Tag & ".Changed()")
                        Textbox3.Selection = r
                        Textbox3.DoSelectionVisible()
                    Next
                End If 'ACheckBox
                ComboboxImage2.Text = "Changed()"
            ElseIf GenControl.Name.StartsWith("PasswordTextBox") Then
                If Not Textbox3.Text.Contains("Event " & GenControl.Tag & ".GotFocus()") Then
                    Textbox3.Text &= vbNewLine & "Event " & GenControl.Tag & ".GotFocus()" & vbNewLine & "    " & vbNewLine & "End Event" & vbNewLine
                    For Each r In Textbox3.Range.GetRanges("Event " & GenControl.Tag & ".GotFocus()")
                        Textbox3.Selection = r
                        Textbox3.DoSelectionVisible()
                    Next
                ElseIf Textbox3.Text.Contains("Event " & GenControl.Tag & ".GotFocus()") Then
                    For Each r In Textbox3.Range.GetRanges("Event " & GenControl.Tag & ".GotFocus()")
                        Textbox3.Selection = r
                        Textbox3.DoSelectionVisible()
                    Next
                End If 'PasswordTextBox
                ComboboxImage2.Text = "GotFocus()"
            ElseIf GenControl.Name.StartsWith("AComboBox") Then
                If Not Textbox3.Text.Contains("Event " & GenControl.Tag & ".ItemSelected(") Then
                    Textbox3.Text &= vbNewLine & "Event " & GenControl.Tag & ".ItemSelected(item As Integer)" & vbNewLine & "    " & vbNewLine & "End Event" & vbNewLine
                    For Each r In Textbox3.Range.GetRanges("Event " & GenControl.Tag & ".ItemSelected")
                        Textbox3.Selection = r
                        Textbox3.DoSelectionVisible()
                    Next
                ElseIf Textbox3.Text.Contains("Event " & GenControl.Tag & ".ItemSelected(") Then
                    For Each r In Textbox3.Range.GetRanges("Event " & GenControl.Tag & ".ItemSelected")
                        Textbox3.Selection = r
                        Textbox3.DoSelectionVisible()
                    Next
                End If 'AComboBox
                ComboboxImage2.Text = "ItemSelected(item As Integer)"

            End If
        End If





        ToolStripButton6.PerformClick()
    End Sub


    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles Timer1.Tick
        If mouseCanClick = True Then


            If click1 = 2 Then
                Timer1.Stop()

                boolhelpEvents = True
                ' MsgBox("DoubleClick " & GenControl.Text) '& AButton.Text) 


                Go_or_Create_Event()

                boolhelpEvents = False
                click1 = 0

            Else

                Timer1.Stop()


                'MsgBox("Click " & GenControl.Name) '& AButton.Text)
                'ClickGenControlsPropert(GenControl)
                ' RichTextBox2.Text = ProjectSaveString
                '   MsgBox(GenControl.Tag)


                click1 = 0
                '  mCl = 0
            End If
        Else
            click1 = 0
            mouseCanClick = True
            Timer1.Stop()
        End If
    End Sub
    Public PropertiesOfControl As String = Nothing  ' einai kai writeable 
    Public NameOfControlBeforeClick As String = Nothing

    ' Dim IsCGCPorNot = False
    Dim isusedGEn As Boolean = False
    Private Sub ClickGenControlsPropert(GnControl As String)
        '  IsCGCPorNot = True
        'cGCPSpec = False

        PropertiesOfControl = "<" & GnControl & ">" & vbNewLine

        If GnControl.StartsWith("AButton") Then : PropertyGrid1.SelectedObject = New ClsButton
        ElseIf GnControl.StartsWith("Canvas") Then : PropertyGrid1.SelectedObject = New ClsCanvas
        ElseIf GnControl.StartsWith("ACheckBox") Then : PropertyGrid1.SelectedObject = New ClsCheckBox
        ElseIf GnControl.StartsWith("ALabel") Then : PropertyGrid1.SelectedObject = New ClsLabel
        ElseIf GnControl.StartsWith("AListview") Then : PropertyGrid1.SelectedObject = New ClsListView
        ElseIf GnControl.StartsWith("APanel") Then : PropertyGrid1.SelectedObject = New clsPanel
        ElseIf GnControl.StartsWith("PasswordTextBox") Then : PropertyGrid1.SelectedObject = New clsPasswordTextBox
        ElseIf GnControl.StartsWith("APictureBox") Then : PropertyGrid1.SelectedObject = New clsPictureBox
        ElseIf GnControl.StartsWith("AProgressBar") Then : PropertyGrid1.SelectedObject = New clsProgressBar
        ElseIf GnControl.StartsWith("ARadioButton") Then : PropertyGrid1.SelectedObject = New ClsRadioButton
        ElseIf GnControl.StartsWith("ATextBox") Then : PropertyGrid1.SelectedObject = New clsTextBox
        ElseIf GnControl.StartsWith("ATimer") Then : PropertyGrid1.SelectedObject = New clsTimer
        ElseIf GnControl.StartsWith("AComboBox") Then : PropertyGrid1.SelectedObject = New clsComboBox
        ElseIf GnControl.StartsWith("VB4AWeb") Then : PropertyGrid1.SelectedObject = New clsVB4AWeb
        End If




        Dim controlEndProp As Boolean = False
        For Each Line As String In ProjectSaveString.Split(vbNewLine)
            Line = Line.Replace(Chr(10), "")

            If Line = ("<" & GnControl & ">") Then controlEndProp = True : isusedGEn = True
            'if den einai </ kai control tote vale alios sub exit (alla 8a valw kai ena allo function h sub pou 8a pernei to control kai 8a 3erei amma einai button h kati allo ... malon)
            If Line = ("</" & GnControl & ">") Then controlEndProp = False : PropertiesOfControl &= "</" & GnControl & ">" : isusedGEn = False : Exit Sub ': Exit For : Exit Sub
            'MsgBox(controlEndProp.ToString)
            If controlEndProp = True Then
                If Line = vbNewLine Then Line = Nothing
                If Line.StartsWith("<" & GnControl) Then NameOfControlBeforeClick = Line.Replace("<", "") : NameOfControlBeforeClick = NameOfControlBeforeClick.Replace(">", "") : Line = Nothing

                If Not Line = Nothing Then PropertiesOfControl &= Line & vbNewLine

                ' MsgBox(PropertiesOfControl)
                Dim ContPoints As String = Nothing

                If Not Line = Nothing Then


                    If GnControl.StartsWith("AButton") Then

                        If Line.StartsWith("Name=") Then : ClsButton._Name = Line.Replace("Name=", Nothing) : Combobox1.Text = Line.Replace("Name=", "")
                        ElseIf Line.StartsWith("Text=") Then : ClsButton._Text = Line.Replace("Text=", Nothing)
                        ElseIf Line.StartsWith("FontBold=") Then : ClsButton._bold = Line.Replace("FontBold=", Nothing)
                        ElseIf Line.StartsWith("FontSize=") Then : ClsButton._Font_Size = Line.Replace("FontSize=", Nothing)
                        ElseIf Line.StartsWith("FontItalic=") Then : ClsButton._Italic = Line.Replace("FontItalic=", Nothing)
                        ElseIf Line.StartsWith("FontTypeface=") Then : ClsButton._FontTypeface = Line.Replace("FontTypeface=", Nothing)
                        ElseIf Line.StartsWith("TextAlign=") Then : ClsButton._TextAlign = Line.Replace("TextAlign=", Nothing)
                        ElseIf Line.StartsWith("BackColor=") Then : ClsButton._BackColor = ColourFromData(Line.Replace("BackColor=", Nothing))
                        ElseIf Line.StartsWith("ForeColor=") Then : ClsButton._ForeColor = ColourFromData(Line.Replace("ForeColor=", Nothing))
                        ElseIf Line.StartsWith("Location=") Then : ContPoints = Line.Replace("Location={", Nothing).Replace("}", Nothing) : ClsButton._Location = New Point(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
                        ElseIf Line.StartsWith("Size=") Then : ContPoints = Line.Replace("Size={", Nothing).Replace("}", Nothing) : ClsButton._Size = New Size(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
                        ElseIf Line.StartsWith("Image=") Then : If Line.Replace("Image=", Nothing) = Nothing Then : ClsButton._Image = Nothing : Else : ClsButton._Image = Line.Replace("Image=", Nothing) : End If 'Image.FromFile(
                        ElseIf Line.StartsWith("Enabled=") Then : ClsButton._Enabled = Line.Replace("Enabled=", Nothing)
                        End If

                    ElseIf GnControl.StartsWith("Canvas") Then

                        If Line.StartsWith("Name=") Then : ClsCanvas._Name = Line.Replace("Name=", Nothing) : Combobox1.Text = Line.Replace("Name=", "")
                        ElseIf Line.StartsWith("BackgroundColor=") Then : ClsCanvas._BackgroundColor = ColourFromData(Line.Replace("BackgroundColor=", Nothing))
                        ElseIf Line.StartsWith("Location=") Then : ContPoints = Line.Replace("Location={", Nothing).Replace("}", Nothing) : ClsCanvas._Location = New Point(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
                        ElseIf Line.StartsWith("Size=") Then : ContPoints = Line.Replace("Size={", Nothing).Replace("}", Nothing) : ClsCanvas._Size = New Size(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
                        ElseIf Line.StartsWith("BackgroundImage=") Then : If Line.Replace("BackgroundImage=", Nothing) = Nothing Then : ClsCanvas._BackgroundImage = Nothing : Else : ClsCanvas._BackgroundImage = Line.Replace("BackgroundImage=", Nothing) : End If
                        ElseIf Line.StartsWith("Enabled=") Then : ClsCanvas._Enabled = Line.Replace("Enabled=", Nothing)
                        ElseIf Line.StartsWith("PointColor=") Then : ClsCanvas._PointColor = ColourFromData(Line.Replace("PointColor=", Nothing))
                        End If

                    ElseIf GnControl.StartsWith("ACheckBox") Then

                        If Line.StartsWith("Name=") Then : ClsCheckBox._Name = Line.Replace("Name=", Nothing) : Combobox1.Text = Line.Replace("Name=", "")
                        ElseIf Line.StartsWith("Text=") Then : ClsCheckBox._Text = Line.Replace("Text=", Nothing)
                        ElseIf Line.StartsWith("FontBold=") Then : ClsCheckBox._bold = Line.Replace("FontBold=", Nothing)
                        ElseIf Line.StartsWith("FontSize=") Then : ClsCheckBox._Font_Size = Line.Replace("FontSize=", Nothing)
                        ElseIf Line.StartsWith("FontItalic=") Then : ClsCheckBox._Italic = Line.Replace("FontItalic=", Nothing)
                        ElseIf Line.StartsWith("FontTypeface=") Then : ClsCheckBox._FontTypeface = Line.Replace("FontTypeface=", Nothing)
                        ElseIf Line.StartsWith("TextAlign=") Then : ClsCheckBox._TextAlign = Line.Replace("TextAlign=", Nothing)
                        ElseIf Line.StartsWith("BackColor=") Then : ClsCheckBox._BackColor = ColourFromData(Line.Replace("BackColor=", Nothing))
                        ElseIf Line.StartsWith("ForeColor=") Then : ClsCheckBox._ForeColor = ColourFromData(Line.Replace("ForeColor=", Nothing))
                        ElseIf Line.StartsWith("Location=") Then : ContPoints = Line.Replace("Location={", Nothing).Replace("}", Nothing) : ClsCheckBox._Location = New Point(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
                        ElseIf Line.StartsWith("Size=") Then : ContPoints = Line.Replace("Size={", Nothing).Replace("}", Nothing) : ClsCheckBox._Size = New Size(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
                        ElseIf Line.StartsWith("Checked=") Then : ClsCheckBox._Checked = Line.Replace("Checked=", Nothing)
                        ElseIf Line.StartsWith("Enabled=") Then : ClsCheckBox._Enabled = Line.Replace("Enabled=", Nothing)
                        End If

                    ElseIf GnControl.StartsWith("ALabel") Then

                        If Line.StartsWith("Name=") Then : ClsLabel._Name = Line.Replace("Name=", Nothing) : Combobox1.Text = Line.Replace("Name=", "")
                        ElseIf Line.StartsWith("Text=") Then : ClsLabel._Text = Line.Replace("Text=", Nothing)
                        ElseIf Line.StartsWith("FontBold=") Then : ClsLabel._bold = Line.Replace("FontBold=", Nothing)
                        ElseIf Line.StartsWith("FontSize=") Then : ClsLabel._Font_Size = Line.Replace("FontSize=", Nothing)
                        ElseIf Line.StartsWith("FontItalic=") Then : ClsLabel._Italic = Line.Replace("FontItalic=", Nothing)
                        ElseIf Line.StartsWith("FontTypeface=") Then : ClsLabel._FontTypeface = Line.Replace("FontTypeface=", Nothing)
                        ElseIf Line.StartsWith("TextAlign=") Then : ClsLabel._TextAlign = Line.Replace("TextAlign=", Nothing)
                        ElseIf Line.StartsWith("BackColor=") Then : ClsLabel._BackColor = ColourFromData(Line.Replace("BackColor=", Nothing))
                        ElseIf Line.StartsWith("ForeColor=") Then : ClsLabel._ForeColor = ColourFromData(Line.Replace("ForeColor=", Nothing))
                        ElseIf Line.StartsWith("Location=") Then : ContPoints = Line.Replace("Location={", Nothing).Replace("}", Nothing) : ClsLabel._Location = New Point(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
                        ElseIf Line.StartsWith("Size=") Then : ContPoints = Line.Replace("Size={", Nothing).Replace("}", Nothing) : ClsLabel._Size = New Size(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
                        ElseIf Line.StartsWith("Enabled=") Then : ClsLabel._Enabled = Line.Replace("Enabled=", Nothing)
                        End If

                    ElseIf GnControl.StartsWith("AListview") Then

                        If Line.StartsWith("Name=") Then : ClsListView._Name = Line.Replace("Name=", Nothing) : Combobox1.Text = Line.Replace("Name=", "")
                        ElseIf Line.StartsWith("FontBold=") Then : ClsListView._bold = Line.Replace("FontBold=", Nothing)
                        ElseIf Line.StartsWith("FontSize=") Then : ClsListView._Font_Size = Line.Replace("FontSize=", Nothing)
                        ElseIf Line.StartsWith("FontItalic=") Then : ClsListView._Italic = Line.Replace("FontItalic=", Nothing)
                        ElseIf Line.StartsWith("FontTypeface=") Then : ClsListView._FontTypeface = Line.Replace("FontTypeface=", Nothing)
                        ElseIf Line.StartsWith("TextAlign=") Then : ClsListView._TextAlign = Line.Replace("TextAlign=", Nothing)
                        ElseIf Line.StartsWith("BackColor=") Then : ClsListView._BackColor = ColourFromData(Line.Replace("BackColor=", Nothing))
                        ElseIf Line.StartsWith("ForeColor=") Then : ClsListView._ForeColor = ColourFromData(Line.Replace("ForeColor=", Nothing))
                        ElseIf Line.StartsWith("Location=") Then : ContPoints = Line.Replace("Location={", Nothing).Replace("}", Nothing) : ClsListView._Location = New Point(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
                        ElseIf Line.StartsWith("Size=") Then : ContPoints = Line.Replace("Size={", Nothing).Replace("}", Nothing) : ClsListView._Size = New Size(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
                        ElseIf Line.StartsWith("Enabled=") Then : ClsListView._Enabled = Line.Replace("Enabled=", Nothing)
                        End If

                    ElseIf GnControl.StartsWith("APanel") Then

                        If Line.StartsWith("Name=") Then : clsPanel._Name = Line.Replace("Name=", Nothing) : Combobox1.Text = Line.Replace("Name=", "")
                        ElseIf Line.StartsWith("BackgroundColor=") Then : clsPanel._BackgroundColor = ColourFromData(Line.Replace("BackgroundColor=", Nothing))
                        ElseIf Line.StartsWith("Location=") Then : ContPoints = Line.Replace("Location={", Nothing).Replace("}", Nothing) : clsPanel._Location = New Point(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
                        ElseIf Line.StartsWith("Size=") Then : ContPoints = Line.Replace("Size={", Nothing).Replace("}", Nothing) : clsPanel._Size = New Size(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
                        ElseIf Line.StartsWith("BackgroundImage=") Then : If Line.Replace("BackgroundImage=", Nothing) = Nothing Then : clsPanel._BackgroundImage = Nothing : Else : clsPanel._BackgroundImage = (Line.Replace("BackgroundImage=", Nothing)) : End If
                        ElseIf Line.StartsWith("Enabled=") Then : clsPanel._Enabled = Line.Replace("Enabled=", Nothing)
                        End If

                    ElseIf GnControl.StartsWith("PasswordTextBox") Then

                        If Line.StartsWith("Name=") Then : clsPasswordTextBox._Name = Line.Replace("Name=", Nothing) : Combobox1.Text = Line.Replace("Name=", "")
                        ElseIf Line.StartsWith("Text=") Then : clsPasswordTextBox._Text = Line.Replace("Text=", Nothing)
                        ElseIf Line.StartsWith("FontBold=") Then : clsPasswordTextBox._bold = Line.Replace("FontBold=", Nothing)
                        ElseIf Line.StartsWith("FontSize=") Then : clsPasswordTextBox._Font_Size = Line.Replace("FontSize=", Nothing)
                        ElseIf Line.StartsWith("FontItalic=") Then : clsPasswordTextBox._Italic = Line.Replace("FontItalic=", Nothing)
                        ElseIf Line.StartsWith("FontTypeface=") Then : clsPasswordTextBox._FontTypeface = Line.Replace("FontTypeface=", Nothing)
                        ElseIf Line.StartsWith("TextAlign=") Then : clsPasswordTextBox._TextAlign = Line.Replace("TextAlign=", Nothing)
                        ElseIf Line.StartsWith("BackColor=") Then : clsPasswordTextBox._BackColor = ColourFromData(Line.Replace("BackColor=", Nothing))
                        ElseIf Line.StartsWith("ForeColor=") Then : clsPasswordTextBox._ForeColor = ColourFromData(Line.Replace("ForeColor=", Nothing))
                        ElseIf Line.StartsWith("Location=") Then : ContPoints = Line.Replace("Location={", Nothing).Replace("}", Nothing) : clsPasswordTextBox._Location = New Point(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
                        ElseIf Line.StartsWith("Size=") Then : ContPoints = Line.Replace("Size={", Nothing).Replace("}", Nothing) : clsPasswordTextBox._Size = New Size(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
                        ElseIf Line.StartsWith("Hint=") Then : clsPasswordTextBox._Hint = Line.Replace("Hint=", Nothing)
                        ElseIf Line.StartsWith("Enabled=") Then : clsPasswordTextBox._Enabled = Line.Replace("Enabled=", Nothing)
                        End If

                    ElseIf GnControl.StartsWith("APictureBox") Then

                        If Line.StartsWith("Name=") Then : clsPictureBox._Name = Line.Replace("Name=", Nothing) : Combobox1.Text = Line.Replace("Name=", "")
                        ElseIf Line.StartsWith("BackColor=") Then : clsPictureBox._BackColor = ColourFromData(Line.Replace("BackColor=", Nothing))
                        ElseIf Line.StartsWith("Location=") Then : ContPoints = Line.Replace("Location={", Nothing).Replace("}", Nothing) : clsPictureBox._Location = New Point(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
                        ElseIf Line.StartsWith("Size=") Then : ContPoints = Line.Replace("Size={", Nothing).Replace("}", Nothing) : clsPictureBox._Size = New Size(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
                        ElseIf Line.StartsWith("Image=") Then : If Line.Replace("Image=", Nothing) = Nothing Then : clsPictureBox._Image = Nothing : Else : clsPictureBox._Image = Line.Replace("Image=", Nothing) : End If
                        ElseIf Line.StartsWith("Enabled=") Then : clsPictureBox._Enabled = Line.Replace("Enabled=", Nothing)
                        End If

                    ElseIf GnControl.StartsWith("AProgressBar") Then

                        If Line.StartsWith("Name=") Then : clsProgressBar._Name = Line.Replace("Name=", Nothing) : Combobox1.Text = Line.Replace("Name=", "")
                        ElseIf Line.StartsWith("Location=") Then : ContPoints = Line.Replace("Location={", Nothing).Replace("}", Nothing) : clsProgressBar._Location = New Point(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
                        ElseIf Line.StartsWith("Size=") Then : ContPoints = Line.Replace("Size={", Nothing).Replace("}", Nothing) : clsProgressBar._Size = New Size(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
                        ElseIf Line.StartsWith("Value=") Then : clsProgressBar._Value = Line.Replace("Value=", Nothing)
                        ElseIf Line.StartsWith("Enabled=") Then : clsProgressBar._Enabled = Line.Replace("Enabled=", Nothing)
                        End If

                    ElseIf GnControl.StartsWith("ARadioButton") Then

                        If Line.StartsWith("Name=") Then : ClsRadioButton._Name = Line.Replace("Name=", Nothing) : Combobox1.Text = Line.Replace("Name=", "")
                        ElseIf Line.StartsWith("Text=") Then : ClsRadioButton._Text = Line.Replace("Text=", Nothing)
                        ElseIf Line.StartsWith("FontBold=") Then : ClsRadioButton._bold = Line.Replace("FontBold=", Nothing)
                        ElseIf Line.StartsWith("FontSize=") Then : ClsRadioButton._Font_Size = Line.Replace("FontSize=", Nothing)
                        ElseIf Line.StartsWith("FontItalic=") Then : ClsRadioButton._Italic = Line.Replace("FontItalic=", Nothing)
                        ElseIf Line.StartsWith("FontTypeface=") Then : ClsRadioButton._FontTypeface = Line.Replace("FontTypeface=", Nothing)
                        ElseIf Line.StartsWith("TextAlign=") Then : ClsRadioButton._TextAlign = Line.Replace("TextAlign=", Nothing)
                        ElseIf Line.StartsWith("BackColor=") Then : ClsRadioButton._BackColor = ColourFromData(Line.Replace("BackColor=", Nothing))
                        ElseIf Line.StartsWith("ForeColor=") Then : ClsRadioButton._ForeColor = ColourFromData(Line.Replace("ForeColor=", Nothing))
                        ElseIf Line.StartsWith("Location=") Then : ContPoints = Line.Replace("Location={", Nothing).Replace("}", Nothing) : ClsRadioButton._Location = New Point(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
                        ElseIf Line.StartsWith("Size=") Then : ContPoints = Line.Replace("Size={", Nothing).Replace("}", Nothing) : ClsRadioButton._Size = New Size(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
                        ElseIf Line.StartsWith("Checked=") Then : ClsRadioButton._Checked = Line.Replace("Checked=", Nothing)
                        ElseIf Line.StartsWith("Enabled=") Then : ClsRadioButton._Enabled = Line.Replace("Enabled=", Nothing)
                        End If

                    ElseIf GnControl.StartsWith("ATextBox") Then

                        If Line.StartsWith("Name=") Then : clsTextBox._Name = Line.Replace("Name=", Nothing) : Combobox1.Text = Line.Replace("Name=", "")
                        ElseIf Line.StartsWith("Text=") Then : clsTextBox._Text = Line.Replace("Text=", Nothing)
                        ElseIf Line.StartsWith("FontBold=") Then : clsTextBox._bold = Line.Replace("FontBold=", Nothing)
                        ElseIf Line.StartsWith("FontSize=") Then : clsTextBox._Font_Size = Line.Replace("FontSize=", Nothing)
                        ElseIf Line.StartsWith("FontItalic=") Then : clsTextBox._Italic = Line.Replace("FontItalic=", Nothing)
                        ElseIf Line.StartsWith("FontTypeface=") Then : clsTextBox._FontTypeface = Line.Replace("FontTypeface=", Nothing)
                        ElseIf Line.StartsWith("TextAlign=") Then : clsTextBox._TextAlign = Line.Replace("TextAlign=", Nothing)
                        ElseIf Line.StartsWith("BackColor=") Then : clsTextBox._BackColor = ColourFromData(Line.Replace("BackColor=", Nothing))
                        ElseIf Line.StartsWith("ForeColor=") Then : clsTextBox._ForeColor = ColourFromData(Line.Replace("ForeColor=", Nothing))
                        ElseIf Line.StartsWith("Location=") Then : ContPoints = Line.Replace("Location={", Nothing).Replace("}", Nothing) : clsTextBox._Location = New Point(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
                        ElseIf Line.StartsWith("Size=") Then : ContPoints = Line.Replace("Size={", Nothing).Replace("}", Nothing) : clsTextBox._Size = New Size(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
                        ElseIf Line.StartsWith("Hint=") Then : clsTextBox._Hint = Line.Replace("Hint=", Nothing)
                        ElseIf Line.StartsWith("Enabled=") Then : clsTextBox._Enabled = Line.Replace("Enabled=", Nothing)
                        ElseIf Line.StartsWith("InputType=") Then : clsTextBox._InputType = Line.Replace("InputType=", Nothing)
                        End If

                    ElseIf GnControl.StartsWith("ATimer") Then

                        If Line.StartsWith("Name=") Then : clsTimer._Name = Line.Replace("Name=", Nothing) : Combobox1.Text = Line.Replace("Name=", "")
                        ElseIf Line.StartsWith("Interval=") Then : clsTimer._Interval = Line.Replace("Interval=", Nothing)
                        ElseIf Line.StartsWith("Enabled=") Then : clsTimer._Enabled = Line.Replace("Enabled=", Nothing)
                        End If

                    ElseIf GnControl.StartsWith("AComboBox") Then

                        If Line.StartsWith("Name=") Then : clsComboBox._Name = Line.Replace("Name=", Nothing) : Combobox1.Text = Line.Replace("Name=", "")
                        ElseIf Line.StartsWith("Text=") Then : clsComboBox._Text = Line.Replace("Text=", Nothing)
                        ElseIf Line.StartsWith("FontBold=") Then : clsComboBox._bold = Line.Replace("FontBold=", Nothing)
                        ElseIf Line.StartsWith("FontSize=") Then : clsComboBox._Font_Size = Line.Replace("FontSize=", Nothing)
                        ElseIf Line.StartsWith("FontItalic=") Then : clsComboBox._Italic = Line.Replace("FontItalic=", Nothing)
                        ElseIf Line.StartsWith("FontTypeface=") Then : clsComboBox._FontTypeface = Line.Replace("FontTypeface=", Nothing)
                        ElseIf Line.StartsWith("TextAlign=") Then : clsComboBox._TextAlign = Line.Replace("TextAlign=", Nothing)
                        ElseIf Line.StartsWith("BackColor=") Then : clsComboBox._BackColor = ColourFromData(Line.Replace("BackColor=", Nothing))
                        ElseIf Line.StartsWith("ForeColor=") Then : clsComboBox._ForeColor = ColourFromData(Line.Replace("ForeColor=", Nothing))
                        ElseIf Line.StartsWith("Location=") Then : ContPoints = Line.Replace("Location={", Nothing).Replace("}", Nothing) : clsComboBox._Location = New Point(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
                        ElseIf Line.StartsWith("Size=") Then : ContPoints = Line.Replace("Size={", Nothing).Replace("}", Nothing) : clsComboBox._Size = New Size(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
                        ElseIf Line.StartsWith("Enabled=") Then : clsComboBox._Enabled = Line.Replace("Enabled=", Nothing)
                        End If



                    ElseIf GnControl.StartsWith("VB4AWeb") Then

                        If Line.StartsWith("Name=") Then : clsVB4AWeb._Name = Line.Replace("Name=", Nothing) : Combobox1.Text = Line.Replace("Name=", "")
                        ElseIf Line.StartsWith("SavePassword=") Then : clsVB4AWeb._SavePassword = Line.Replace("SavePassword=", Nothing)
                        ElseIf Line.StartsWith("SaveFromData=") Then : clsVB4AWeb._SaveFormData = Line.Replace("SaveFromData=", Nothing)
                        ElseIf Line.StartsWith("JSEnabled=") Then : clsVB4AWeb._JSEnabled = Line.Replace("JSEnabled=", Nothing)
                        ElseIf Line.StartsWith("ZoomEnabled=") Then : clsVB4AWeb._ZoomEnabled = Line.Replace("ZoomEnabled=", Nothing)
                        ElseIf Line.StartsWith("BuildinZoom=") Then : clsVB4AWeb._BuildinZoom = Line.Replace("BuildinZoom=", Nothing)
                        ElseIf Line.StartsWith("Location=") Then : ContPoints = Line.Replace("Location={", Nothing).Replace("}", Nothing) : clsVB4AWeb._Location = New Point(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
                        ElseIf Line.StartsWith("Size=") Then : ContPoints = Line.Replace("Size={", Nothing).Replace("}", Nothing) : clsVB4AWeb._Size = New Size(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
                        ElseIf Line.StartsWith("Enabled=") Then : clsVB4AWeb._Enabled = Line.Replace("Enabled=", Nothing)
                        End If

                    End If
                End If
                ContPoints = Nothing
            End If

        Next



        ' ClsButton._Name = "asd"
        '    MsgBox(PropertiesOfControl)
        '  ChangeASpecificPropOfContr(PropertiesOfControl, "Name=", "Name=masdasd", GnControl)
        ' cGCPSpec = True
    End Sub
    Public Sub changeFromFileASpecPropOfCont(Filepath As String, PropBefore As String, PropAfter As String, NameOfCont As String)
        Dim a As Boolean = False
        Dim b As String = Nothing
        Dim c As String = Nothing
        For Each Line As String In My.Computer.FileSystem.ReadAllText(Filepath).Split(vbNewLine)
            Line = Line.Replace(Chr(10), "")
            ' If Line.StartsWith("++Form++") Or Line.StartsWith(Chr(10) & "++Form++") Then helpotherbool = False
            '  If Line.StartsWith(Chr(10) & "--Form--") Or Line.StartsWith("--Form--") Then helpotherbool = True
            '  If helpotherbool = True Then
            If Line.StartsWith("<" & NameOfCont) Or NameOfCont = Line Then a = True
            If Line.StartsWith("</" & NameOfCont) Or NameOfCont = Line.Replace("++", "--") Then a = False

            If a = True Then
                b &= Line & vbNewLine
                If Not Line.StartsWith(PropBefore) Then c &= Line & vbNewLine Else c &= PropAfter & vbNewLine : Exit For
            End If
            ' End If
        Next

        My.Computer.FileSystem.WriteAllText(Filepath, My.Computer.FileSystem.ReadAllText(Filepath).Replace(b, c), False)

    End Sub

    Public Sub ChangeASpecificPropOfContr(AllPROPSOFCONT As String, PropBefore As String, PropAfter As String, RealName As String)
        '  MsgBox(AllPROPSOFCONT)

        'AllPROPSOFCONT  = AllPROPSOFCONT.Replace()
        '  Dim helpotherbool As Boolean = False
        For Each Line As String In AllPROPSOFCONT.Split(vbNewLine)
            Line = Line.Replace(Chr(10), "")
            ' If Line.StartsWith("++Form++") Or Line.StartsWith(Chr(10) & "++Form++") Then helpotherbool = False
            '  If Line.StartsWith(Chr(10) & "--Form--") Or Line.StartsWith("--Form--") Then helpotherbool = True
            '  If helpotherbool = True Then
            If Line.StartsWith(PropBefore) Then AllPROPSOFCONT = AllPROPSOFCONT.Replace(Line, PropAfter) : Exit For ' GoTo here
            ' End If


        Next



        If Not RealName = "++FORM++" Then ProjectSaveString = ProjectSaveString.Replace(PropertiesOfControl, AllPROPSOFCONT) _
        : PropertiesOfControl = AllPROPSOFCONT Else ProjectSaveString = ProjectSaveString.Replace(propertiesOfForm, AllPROPSOFCONT) : propertiesOfForm = AllPROPSOFCONT


        ' MsgBox(AllPROPSOFCONT)
    End Sub


    Dim click1 As Integer = 0



    ' Dim eOfPanel5 As System.Windows.Forms.MouseEventArgs
    Private Sub Panel5_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel5.MouseMove
        ' ControlPosi = New System.Drawing.Point(e.Location.X - AButton.Size.Width / 2, e.Location.Y - AButton.Size.Height / 2)
        '  Label5.Text = New System.Drawing.Point(e.Location.X - AButton.Size.Width / 2, e.Location.Y - AButton.Size.Height / 2).ToString
        ' eOfPanel5 = e
    End Sub
    Private Function RetriveInWhatPanelis(sControl As Control) As String

        Dim allpanels As String = Nothing
        Dim panelis As String = Nothing

        For Each line In ProjectSaveString.Split(vbNewLine)
            Dim contrstr As String = line.Replace("<", "")
            contrstr = contrstr.Replace(">", "")
            contrstr = contrstr.Replace(Chr(10), "")
            'Console.WriteLine(sControl.Name)
            If line.StartsWith("<APanel") AndAlso Not line.StartsWith("</APanel") Or line.StartsWith(Chr(10) & "<APanel") AndAlso Not line.StartsWith(Chr(10) & "</APanel") Then allpanels &= contrstr & vbNewLine
            If Panel5.Controls.Contains(Panel5.Controls(sControl.Name)) Then
                If line.StartsWith("<") AndAlso Not line.StartsWith("</") Or line.StartsWith(Chr(10) & "<") AndAlso Not line.StartsWith(Chr(10) & "</") Then panelis = Panel5.Name
            ElseIf Not allpanels = Nothing Then
                For Each lines In allpanels.Split(vbNewLine)
                    Dim contrstrPANEL As String = lines.Replace(Chr(10), "")
                    '  Try
                    If Not contrstrPANEL = Nothing Then
                        '   Try


                        If line.StartsWith("<") AndAlso Not line.StartsWith("</") Or line.StartsWith(Chr(10) & "<") AndAlso Not line.StartsWith(Chr(10) & "</") Then
                            ' If sControl.Name = "APanel2" Then MsgBox("ox amaaaa1")
                            'If Not contrstrPANEL = sControl.Name Then


                            If DirectCast(FindControl(contrstrPANEL, Me), Panel).Contains(DirectCast(FindControl(sControl.Name, Me), Control)) Then panelis = contrstrPANEL
                            'If sControl.Name = "APanel2" Then MsgBox(contrstrPANEL & "   " & sControl.Name)
                            'End If

                            'If sControl.Name = "APanel2" Then MsgBox("ox amaaa2")
                            'If sControl.Name = "APanel2" Then MsgBox("ox amaaaaaaaaaaaaaaaaaaaaaaaan " & panelis)

                        End If
                        'panelis = DirectCast(FindControl(contrstrPANEL, Me), Panel).Name
                        '   Catch ex As Exception
                        '     MsgBox(sControl.FindForm.TopLevelControl.ToString)
                        ' panelis = contrstrPANEL 'DirectCast(FindControl(contrstrPANEL, Me), Button).ToString

                        '  End Try
                    End If
                Next
            End If
        Next
        'MsgBox(panelis)
        If panelis = "" Then

            Dim b As Boolean = False

            For Each line In ProjectSaveString.Split(vbNewLine)
                line = line.Replace(Chr(10), "")

                If line = "<" & sControl.Name & ">" Then b = True

                If b = True Then
                    If line.StartsWith("IsOnPanel=") Then Return line.Replace("IsOnPanel=", "")
                End If

            Next
        End If

        Return panelis
    End Function
    Private Sub rightclick(ByVal sender As System.Object, ByVal e As MouseEventArgs)

        ContextMenuStrip1.Items.Clear()
        ContextMenuStrip1.Items.Add("View Code").Image = My.Resources.viewcode
        ContextMenuStrip1.Items.Add(New ToolStripSeparator)
        ContextMenuStrip1.Items.Add("Bring to Front").Image = My.Resources.BringtoFront_10687
        ContextMenuStrip1.Items.Add("Send to Back").Image = My.Resources.SendtoBack_10681
        ContextMenuStrip1.Items.Add(New ToolStripSeparator)
        If Not RetriveInWhatPanelis(sender) = Panel5.Name Then
            ' MsgBox(RetriveInWhatPanelis(sender))
            ContextMenuStrip1.Items.Add("Select '" & Combobox3.Text.Remove(Combobox3.Text.Length - 5, 5) & "'")
            ContextMenuStrip1.Items.Add(New ToolStripSeparator)
        End If
        ContextMenuStrip1.Items.Add("Cut").Image = My.Resources.Cut_6523
        ContextMenuStrip1.Items.Add("Copy").Image = My.Resources.Copy_6524
        ContextMenuStrip1.Items.Add("Paste").Image = My.Resources.Paste_6520
        ContextMenuStrip1.Items.Add("Delete").Image = My.Resources.delete
        ContextMenuStrip1.Items.Add(New ToolStripSeparator)
        ContextMenuStrip1.Items.Add("Properties").Image = My.Resources.Property_501

        controlForContext = sender

    End Sub

    Private Sub ParseGridItems(gi As GridItem)
        If gi.GridItemType = GridItemType.Category Then
            For Each item As GridItem In gi.GridItems
                ParseGridItems(item)
            Next
        End If
        'MsgBox(gi.Label)
        If gi.Label = "BackgroundImage" Or gi.Label = "Image" Then
            If gi.Value IsNot Nothing Then ' if i will remove this (if gi.value IsNot...Then) then it crush and i dont know why even if there is a value (PLEASE SOMEONE EXPLAIN ME WHY ,WHY EVEN if there is a value xD )
                controlForContext.BackgroundImage.Dispose()
                'MsgBox(gi.Value.ToString())
                If IsUsedFromAnotherProc(gi.Value.ToString()) = False Then My.Computer.FileSystem.DeleteFile(gi.Value.ToString())
            End If
        End If


    End Sub

    Dim controlForContext As Control

    'Dim Afercntrafe2 As Integer = 0
    Public Function getPropsOfCont(name As String) As String
        Dim b As Boolean = False
        Dim a As String = String.Empty

        For Each line As String In ProjectSaveString.Split(vbNewLine)
            line = line.Replace(Chr(10), "")

            If line = "<" & name & ">" Then b = True

            If b = True Then
                a &= line & vbNewLine
            End If

            If line = "</" & name & ">" Then b = False : Return a
        Next
        Return String.Empty
    End Function

    Private Sub ContextMenuStrip1_ItemClicked(sender As System.Object, e As ToolStripItemClickedEventArgs) Handles ContextMenuStrip1.ItemClicked
        ' MsgBox("right " & controlForContext.Name)

        Select Case e.ClickedItem.Text
            Case "View Code"

            Case "Bring to Front"
                controlForContext.BringToFront()

            Case "Send to Back"
                controlForContext.SendToBack()

            'Me.Controls("Panel5").Controls(RetriveInWhatPanelis(controlForContext)).Remove(controlForContext)

            Case "Cut"

            Case "Copy"

            Case "Paste"

            Case "Delete"
                '   MsgBox(ButtonString)

                TransparentDrawing1.Hide()
                If Not RetriveInWhatPanelis(controlForContext) = "Panel5" Then FindControl(RetriveInWhatPanelis(controlForContext), Panel5).Controls.Remove(TransparentDrawing1) : Panel5.Controls.Add(TransparentDrawing1)
                Dim listhlp As New List(Of Control)
                Dim hlplist2 As New List(Of Control)

                If Not controlForContext.Name.StartsWith("APanel") Then ' Control Is Not A Panel
                    If RetriveInWhatPanelis(controlForContext) = "Panel5" Then  ' Is On Form

                        My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Combobox3.SelectedItem.ToString.Replace(".vb4a", ".DelConts"), controlForContext.Name & vbNewLine, True)
                        For i = 0 To Combobox1.Items.Count - 1
                            If Combobox1.Items.Item(i).ToString = controlForContext.Tag Then Combobox1.Items.RemoveAt(i) : Exit For
                        Next
                        ' MsgBox(controlForContext.Controls.Count)
                        Panel5.Controls.Remove(controlForContext)
                        ProjectSaveString = ProjectSaveString.Replace(PropertiesOfControl & vbNewLine, "")
                        PropertiesOfControl = Nothing
                        formisProp()
                        Is_Any_Cont_Delted = True

                    Else 'Control Is On Panel

                        My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Combobox3.SelectedItem.ToString.Replace(".vb4a", ".DelConts"), controlForContext.Name & vbNewLine, True)
                        For i = 0 To Combobox1.Items.Count - 1
                            If Combobox1.Items.Item(i).ToString = controlForContext.Tag Then Combobox1.Items.RemoveAt(i) : Exit For
                        Next
                        FindControl(RetriveInWhatPanelis(controlForContext), Panel5).Controls.Remove(controlForContext)
                        ProjectSaveString = ProjectSaveString.Replace(PropertiesOfControl & vbNewLine, "")
                        PropertiesOfControl = Nothing
                        formisProp()
                        Is_Any_Cont_Delted = True

                    End If
                Else ' Control is A Panel

                    If RetriveInWhatPanelis(controlForContext) = "Panel5" Then  'Panel Is On Form

                        If controlForContext.Controls.Count = 0 Then '  Αν ειναι χωρις κανενα  control διέγραψε το 
                            My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Combobox3.SelectedItem.ToString.Replace(".vb4a", ".DelConts"), controlForContext.Name & vbNewLine, True)
                            For i = 0 To Combobox1.Items.Count - 1
                                If Combobox1.Items.Item(i).ToString = controlForContext.Tag Then Combobox1.Items.RemoveAt(i) : Exit For
                            Next
                            Panel5.Controls.Remove(controlForContext)
                            ProjectSaveString = ProjectSaveString.Replace(PropertiesOfControl & vbNewLine, "")
                            PropertiesOfControl = Nothing
                            formisProp()
                            Is_Any_Cont_Delted = True

                        Else ' αλιός  ...  (Πτωτα τα Controls και μετα το Panel)

                            Dim ii As Integer
                            Dim aa As New List(Of Control)
                            Dim bb As Integer = 0
                            listhlp.Add(controlForContext)
                            Dim pnll As New Control
here1:                      ii = 0
                            bb = 0
                            pnll = Nothing
                            For Each cont As Control In controlForContext.Controls
                                ii += 1
                                If (cont.GetType() Is GetType(Panel)) Then
                                    listhlp.Add(cont)
                                    pnll = cont
                                    'MsgBox(cont.Name)
                                    aa.Add(cont)
                                    ' bb += 1
                                End If
                                ' MsgBox(ii & " = " & controlForContext.Controls.Count & "   aa=" & aa.Count)
                                If ii = controlForContext.Controls.Count Then
                                    If Not pnll Is Nothing Then
                                        For i = 0 To aa.Count - 1
                                            If aa.Item(i) Is pnll Then aa.RemoveAt(i)
                                        Next
                                        controlForContext = pnll : GoTo here1
                                    End If
                                End If
                                'MsgBox(1)
                                If aa.Count > 0 And ii = controlForContext.Controls.Count And pnll Is Nothing Then
                                    For i = 0 To aa.Count - 1
                                        'MsgBox(aa.Item(i).Name)
                                        controlForContext = aa.Item(i)
                                        aa.RemoveAt(i)
                                        GoTo here1
                                    Next
                                End If
                            Next

                            For i = listhlp.Count - 1 To 0 Step -1
                                For Each cont As Control In listhlp.Item(i).Controls
                                    ' MsgBox(cont.Name)
                                    hlplist2.Add(cont)
                                Next
                            Next
                            'MsgBox("ad")
                            ' Console.WriteLine(ProjectSaveString & vbNewLine & "------------------" & PropertiesOfControl)
                            For i = 0 To hlplist2.Count - 1
                                'MsgBox(hlplist2.Item(i).Name & " in panel: " & RetriveInWhatPanelis(hlplist2.Item(i)))
                                For a = 0 To Combobox1.Items.Count - 1
                                    If Combobox1.Items.Item(a).ToString = hlplist2.Item(i).Tag Then Combobox1.Items.RemoveAt(a) : Exit For
                                Next

                                'FindControl(RetriveInWhatPanelis(hlplist2.Item(i)), Panel5).Controls.Remove(hlplist2.Item(i))
                                My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Combobox3.SelectedItem.ToString.Replace(".vb4a", ".DelConts"), hlplist2.Item(i).Name & vbNewLine, True)
                                ProjectSaveString = ProjectSaveString.Replace(getPropsOfCont(hlplist2.Item(i).Name), "")
                                'PropertiesOfControl = Nothing
                            Next

                            For a = 0 To Combobox1.Items.Count - 1
                                If Combobox1.Items.Item(a).ToString = listhlp.Item(0).Tag Then Combobox1.Items.RemoveAt(a) : Exit For
                            Next
                            My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Combobox3.SelectedItem.ToString.Replace(".vb4a", ".DelConts"), listhlp.Item(0).Name & vbNewLine, True)
                            Panel5.Controls.Remove(listhlp.Item(0))
                            ProjectSaveString = ProjectSaveString.Replace(PropertiesOfControl & vbNewLine, "")
                            PropertiesOfControl = Nothing
                            formisProp()
                            Is_Any_Cont_Delted = True
                            ' Console.WriteLine(vbNewLine & "------++------++------" & ProjectSaveString)
                        End If

                    Else 'Panel Is On Panel

                        If controlForContext.Controls.Count = 0 Then '  Αν ειναι χωρις κανενα  control διέγραψε το 

                            My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Combobox3.SelectedItem.ToString.Replace(".vb4a", ".DelConts"), controlForContext.Name & vbNewLine, True)
                            Combobox1.Items.Remove(cmb)
                            DirectCast(FindControl(RetriveInWhatPanelis(controlForContext), Panel5), Panel).Controls.Remove(controlForContext)
                            ProjectSaveString = ProjectSaveString.Replace(PropertiesOfControl & vbNewLine, "")
                            PropertiesOfControl = Nothing
                            formisProp()
                            Is_Any_Cont_Delted = True

                        Else ' αλιός  ...  (Πτωτα τα Controls και μετα το Panel)

                            ' Dim hlplist2 As New List(Of Control)
                            'MsgBox(controlForContext.Name)
                            Dim ii As Integer
                            Dim dd As New List(Of Control)
                            Dim bb As Integer = 0
                            listhlp.Add(controlForContext)
                            Dim pnll As New Control
here2:                      ii = 0
                            bb = 0
                            pnll = Nothing
                            For Each cont As Control In controlForContext.Controls
                                ii += 1
                                If (cont.GetType() Is GetType(Panel)) Then
                                    listhlp.Add(cont)
                                    pnll = cont
                                    'MsgBox(cont.Name)
                                    dd.Add(cont)

                                    ' bb += 1
                                End If
                                ' MsgBox(ii & " = " & controlForContext.Controls.Count & "   dd=" & dd.Count)
                                If ii = controlForContext.Controls.Count Then
                                    If Not pnll Is Nothing Then
                                        For i = 0 To dd.Count - 1
                                            If dd.Item(i) Is pnll Then dd.RemoveAt(i)
                                        Next
                                        controlForContext = pnll : GoTo here2
                                    End If
                                End If
                                'MsgBox(1)
                                If dd.Count > 0 And ii = controlForContext.Controls.Count And pnll Is Nothing Then
                                    For i = 0 To dd.Count - 1
                                        'MsgBox(dd.Item(i).Name)
                                        controlForContext = dd.Item(i)
                                        dd.RemoveAt(i)
                                        GoTo here2
                                    Next
                                End If
                            Next

                            For i = listhlp.Count - 1 To 0 Step -1
                                For Each cont As Control In listhlp.Item(i).Controls
                                    'MsgBox(cont.Name)
                                    hlplist2.Add(cont)
                                Next
                            Next
                            ' Console.WriteLine(ProjectSaveString & vbNewLine & "------------------" & PropertiesOfControl)
                            Console.WriteLine("=---------------------=")
                            For i = 0 To hlplist2.Count - 1
                                Console.WriteLine(hlplist2.Item(i).Name)
                                For a = 0 To Combobox1.Items.Count - 1
                                    If Combobox1.Items.Item(a).ToString = hlplist2.Item(i).Tag Then Combobox1.Items.RemoveAt(a) : Exit For
                                Next

                                'FindControl(RetriveInWhatPanelis(hlplist2.Item(i)), Panel5).Controls.Remove(hlplist2.Item(i))
                                My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Combobox3.SelectedItem.ToString.Replace(".vb4a", ".DelConts"), hlplist2.Item(i).Name & vbNewLine, True)
                                ProjectSaveString = ProjectSaveString.Replace(getPropsOfCont(hlplist2.Item(i).Name), "")
                                'PropertiesOfControl = Nothing
                            Next
                            Console.WriteLine("Item0 : " & listhlp.Item(0).Name.ToString)
                            Console.WriteLine("=------++++++++-------=")
                            For a = 0 To Combobox1.Items.Count - 1
                                If Combobox1.Items.Item(a).ToString = listhlp.Item(0).Tag Then Combobox1.Items.RemoveAt(a) : Exit For
                            Next
                            'MsgBox(hlplist2.Item(0).Name & " in panel: " & RetriveInWhatPanelis(hlplist2.Item(0)))
                            My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Combobox3.SelectedItem.ToString.Replace(".vb4a", ".DelConts"), listhlp.Item(0).Name & vbNewLine, True)
                            FindControl(RetriveInWhatPanelis(listhlp.Item(0)), Panel5).Controls.Remove(listhlp.Item(0))
                            ProjectSaveString = ProjectSaveString.Replace(PropertiesOfControl & vbNewLine, "")
                            PropertiesOfControl = Nothing
                            formisProp()
                            Is_Any_Cont_Delted = True

                        End If
                    End If
                End If

                ' If contCotext.Name.StartsWith("APanel") Then MsgBox("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaddffgg") : controlForContext = contCotext : listhelp.Clear() : listhelp.Add(controlForContext) : GoTo here1

                'Here1:          If Not controlForContext.BackgroundImage Is Nothing Then ' fist time i use is nothing on backgroundImage i didnt know it xD 17/4/2015

                '                    ''Dim pdc As System.ComponentModel.PropertyDescriptorCollection = System.ComponentModel.TypeDescriptor.GetProperties(PropertyGrid1.SelectedObject, True)
                '                    ''Dim pd As System.ComponentModel.PropertyDescriptor

                '                    ''For Each pd In pdc

                '                    ''    'MsgBox(pd.Name)
                '                    ''    MsgBox(pd.GetValue())

                '                    ''Next

                '                    'Dim gi As GridItem = PropertyGrid1.SelectedGridItem
                '                    'While gi.Parent IsNot Nothing
                '                    '    gi = gi.Parent
                '                    'End While
                '                    'For Each item As GridItem In gi.GridItems
                '                    '    'recursive
                '                    '    ParseGridItems(item)
                '                    'Next

                '                End If

                '                If Not controlForContext.Name.StartsWith("APanel") Then ' Control Is Not A Panel



                '                    If RetriveInWhatPanelis(controlForContext) = "Panel5" Then  ' Is On Form


                '                        'If controlForContext.Name.StartsWith("AButton") Then

                '                        'ElseIf controlForContext.Name.StartsWith("Canvas") Then
                '                        ' CanvasString = CanvasString.Replace(controlForContext.Name & vbNewLine, Nothing) '------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------VIDEOOOOOOOOOOOOOOOOOOOOO STO THLLLLLL MOUUUUUU
                '                        'cntrafe2 -= 1
                '                        My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Combobox3.SelectedItem.ToString.Replace(".vb4a", ".DelConts"), controlForContext.Name & vbNewLine, True)


                '                        Combobox1.Items.Remove(cmb)
                '                        Panel5.Controls.Remove(controlForContext)
                '                        ' edw einai to provlima!!!!!!!!! ===============================------------------------------------Otan Alazei Form To vrhka!!!!

                '                        'ProjectSaveString = ProjectSaveString.Replace(PropertiesOfControl & vbNewLine, "<" & controlForContext.Name & ">" & vbNewLine & "DELETED" & "<\" & controlForContext.Name & ">")
                '                        ProjectSaveString = ProjectSaveString.Replace(PropertiesOfControl & vbNewLine, "")

                '                        PropertiesOfControl = Nothing
                '                        formisProp()

                '                        Is_Any_Cont_Delted = True
                '                    Else ' Is On Panel

                '                        My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Combobox3.SelectedItem.ToString.Replace(".vb4a", ".DelConts"), controlForContext.Name & vbNewLine, True)
                '                        Combobox1.Items.Remove(cmb)

                '                        DirectCast(FindControl(RetriveInWhatPanelis(controlForContext), Panel5), Panel).Controls.Remove(controlForContext)
                '                        ' DirectCast(FindControl(RetriveInWhatPanelis(DirectCast(FindControl(contrstr, Me), Control)), Me), Panel).Controls(contrstr).Hide()
                '                        ProjectSaveString = ProjectSaveString.Replace(PropertiesOfControl & vbNewLine, "")

                '                        PropertiesOfControl = Nothing
                '                        formisProp()

                '                        Is_Any_Cont_Delted = True
                '                        'MsgBox("yes")
                '                        ' MsgBox(RetriveInWhatPanelis(controlForContext).ToString)
                '                    End If



                '                Else ' Control is A Panel         ++

                '                    If RetriveInWhatPanelis(controlForContext) = "Panel5" Then  ' Is On Form

                '                        If controlForContext.Controls.Count = 0 Then '  Αν ειναι χωρις κανενα  control διέγραψε το 

                '                            My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Combobox3.SelectedItem.ToString.Replace(".vb4a", ".DelConts"), controlForContext.Name & vbNewLine, True)


                '                            Combobox1.Items.Remove(cmb)
                '                            Panel5.Controls.Remove(controlForContext)

                '                            ProjectSaveString = ProjectSaveString.Replace(PropertiesOfControl & vbNewLine, "")

                '                            PropertiesOfControl = Nothing
                '                            formisProp()

                '                            Is_Any_Cont_Delted = True
                '                        Else ' αλιός  ...  (Πτωτα τα Controls και μετα το Panel)

                '                            My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Combobox3.SelectedItem.ToString.Replace(".vb4a", ".DelConts"), controlForContext.Name & vbNewLine, True)
                '                            For Each ContInPan As Control In controlForContext.Controls
                '                                My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Combobox3.SelectedItem.ToString.Replace(".vb4a", ".DelConts"), ContInPan.Name & vbNewLine, True)

                '                                Combobox1.Text = ContInPan.Tag

                '                                Combobox1.Items.Remove(cmb)
                '                                ' controlForContext.Controls.Remove(ContInPan) ' αν και μπορουσα να κανω clear() τελος παντον xD LoL

                '                                ProjectSaveString = ProjectSaveString.Replace(PropertiesOfControl & vbNewLine, "")

                '                                PropertiesOfControl = Nothing

                '                            Next


                '                            Combobox1.Text = controlForContext.Tag

                '                                Combobox1.Items.Remove(cmb)
                '                                Panel5.Controls.Remove(controlForContext)

                '                                ProjectSaveString = ProjectSaveString.Replace(PropertiesOfControl & vbNewLine, "")

                '                                PropertiesOfControl = Nothing

                '                                formisProp()

                '                                Is_Any_Cont_Delted = True
                '                            End If

                '                            Else ' Is On Panel
                '                        If controlForContext.Controls.Count = 0 Then '  Αν ειναι χωρις κανενα  control διέγραψε το 
                '                            My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Combobox3.SelectedItem.ToString.Replace(".vb4a", ".DelConts"), controlForContext.Name & vbNewLine, True)
                '                            Combobox1.Items.Remove(cmb)

                '                            DirectCast(FindControl(RetriveInWhatPanelis(controlForContext), Panel5), Panel).Controls.Remove(controlForContext)
                '                            ProjectSaveString = ProjectSaveString.Replace(PropertiesOfControl & vbNewLine, "")

                '                            PropertiesOfControl = Nothing
                '                            formisProp()

                '                            Is_Any_Cont_Delted = True
                '                        Else ' αλιός  ...  (Πτωτα τα Controls και μετα το Panel)

                '                            Dim lstHelp As New List(Of Control)
                '                            lstHelp.Add(controlForContext)




                '                            Console.WriteLine(ProjectSaveString)
                '                            MsgBox(controlForContext.Tag & "=" & controlForContext.Name & vbNewLine & PropertiesOfControl)

                '                            My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Combobox3.SelectedItem.ToString.Replace(".vb4a", ".DelConts"), controlForContext.Name & vbNewLine, True)
                '                            For Each ContInPan As Control In controlForContext.Controls
                '                                If Not ContInPan.Name.StartsWith("APanel") Then

                '                                    My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Combobox3.SelectedItem.ToString.Replace(".vb4a", ".DelConts"), ContInPan.Name & vbNewLine, True)

                '                                    Combobox1.Text = ContInPan.Tag

                '                                    Combobox1.Items.Remove(cmb)
                '                                    ' controlForContext.Controls.Remove(ContInPan) ' αν και μπορουσα να κανω clear() τελος παντον xD LoL

                '                                    ProjectSaveString = ProjectSaveString.Replace(PropertiesOfControl & vbNewLine, "")

                '                                    PropertiesOfControl = Nothing

                '                                Else
                '                                    lstHelp.Add(ContInPan)
                '                                    PropertiesOfControl = Nothing
                '                                End If
                '                            Next
                '                            If Not lstHelp.Count = 1 Then controlForContext = lstHelp.Item(1) : PropertiesOfControl = getPropsOfCont(controlForContext.Name) : lstHelp.RemoveAt(1) : GoTo Here1

                '                            Combobox1.Text = controlForContext.Tag

                '                            Combobox1.Items.Remove(cmb)
                '                            DirectCast(FindControl(RetriveInWhatPanelis(controlForContext), Panel5), Panel).Controls.Remove(controlForContext)
                '                            '
                '                            ProjectSaveString = ProjectSaveString.Replace(PropertiesOfControl & vbNewLine, "")

                '                            PropertiesOfControl = Nothing

                '                            formisProp()

                '                            Is_Any_Cont_Delted = True
                '                        End If
                '                    End If


                '                End If


                '                For item = 0 To ComboboxImage1.Items.Count
                '                    If ComboboxImage1.Items.Item(item).ToString = controlForContext.Tag Then ComboboxImage1.Items.RemoveAt(item) : Exit For
                '                Next

                '                ComboboxImage2.Items.Clear()
                '                BuildAutocompleteMenu()
                Dim currentcombtext As String = Combobox3.SelectedItem.ToString
                Combobox3.SelectedItem = Nothing
                For i = 0 To Combobox3.Items.Count - 1
                    If Combobox3.Items.Item(i).ToString = currentcombtext Then Combobox3.SelectedItem = Combobox3.Items.Item(i)
                Next
            Case "Properties"

            Case Else
                If e.ClickedItem.Text.StartsWith("Select") Then

                    ' Controls.RemoveByKey(controlForContext.Name)
                    'Dim locationofcontBef As New Point
                    ' locationofcontBef = controlForContext.Location

                    Panel5.Controls.Add(controlForContext)
                    ' PropertiesOfControl = PropertiesOfControl.Replace("IsOnPanel=" & RetriveInWhatPanelis(controlForContext), "IsOnPanel=")
                    ChangeASpecificPropOfContr(PropertiesOfControl, "IsOnPanel=", "IsOnPanel=", controlForContext.Name)
                    ChangeASpecificPropOfContr(PropertiesOfControl, "IsOnForm=", "IsOnForm=True", controlForContext.Name)
                    ' 8a kanw ena loop gia na vgenei swsto to location οταν το βγαζω απο το Panel που 8α ειναι γτ μπορει να είναι το ιδιο το panel μεσα σε ενα αλλο


                    '  Panel5.Controls(controlForContext).Location =                                                              '                                                                                    ===================================================================                PREPEI NA TO FTIAJW AKRIVOS NA BGENEI STO FORM                            
                    '  controlForContext.Location =
                    '     Me.Controls("Panel5").Controls(RetriveInWhatPanelis(controlForContext).Name).Remove(controlForContext)
                End If
        End Select
        ' MsgBox(ButtonString)
    End Sub

    Public Function FindControl(
ByVal ControlName As String,
ByVal CurrentControl As Control
) As Control
        Dim ctr As Control
        For Each ctr In CurrentControl.Controls
            If ctr.Name = ControlName Then
                Return ctr
            Else
                ctr = FindControl(ControlName, ctr)
                If Not ctr Is Nothing Then
                    Return ctr
                End If
            End If
        Next ctr
    End Function

    Public Shared XofFormApp As Integer = 240
    Public Shared YofFormApp As Integer = 425 ' --------------------------- YofFormApp

    Dim CursorDenDiaforetik As Boolean = True
    Dim loca As New Point
    '  Dim mousepot As Integer = 0
    Dim mousenowclick As Integer = 0
    Dim mCl As Integer = 0
    Dim mousemoves As Boolean = False
    Dim mouseCanClick As Boolean = False
    ' Dim allaclick As Boolean = False
    'Dim _mouseDown As Boolean = False

    Dim widthContr As Integer = Nothing

    Dim _cursorStartPoint As New Point
    Dim _currentControlStartSize As New Point

    Dim panSE As Boolean = False
    Dim panNE As Boolean = False
    Dim panNW As Boolean = False
    Dim panSW As Boolean = False
    Dim noMoveHoriz As Boolean = False
    Dim noMoveHorizLEFT As Boolean = False
    Dim noMoveHorizRIGHT As Boolean = False
    Dim noMoveVert As Boolean = False
    Dim noMoveVertPanw As Boolean = False
    Dim noMoveVertKatw As Boolean = False


    Public Shared LFRTUPDN As Boolean

    Public Shared pnl As Panel = Nothing
    Public Shared grayPen As New Pen(Color.Gray, 1)

    Private Sub TransparentDrawing1_LocationChanged(sender As Object, e As EventArgs) Handles TransparentDrawing1.LocationChanged

    End Sub


    Private Sub TransparentDrawing1_MouseDown(sender As Object, e As MouseEventArgs) Handles TransparentDrawing1.MouseDown

        If e.Button = Windows.Forms.MouseButtons.Left Then
            mMouseDown = True
            Label10.Show()
        End If
    End Sub
    Private Sub TransparentDrawing1_MouseMove(sender As Object, e As MouseEventArgs) Handles TransparentDrawing1.MouseMove
        ' Console.WriteLine(locGenControl.X - 4 & "   ,   " & ResizeableControl.locGenControl.X - 15 & "  |  " & e.Location.X)
        If Not c Is Nothing AndAlso mMouseDown = False Then
            If e.Location.X <= 7 - 3 AndAlso e.Location.X >= 7 - 8 AndAlso e.Location.Y <= 8 + GenControl.Size.Height AndAlso e.Location.Y >= 5 Then ' left
                TransparentDrawing1.Cursor = Cursors.VSplit
                LFRTUPDN = False
            ElseIf e.Location.X <= 8 + GenControl.Size.Width + 5 AndAlso e.Location.X >= 7 + GenControl.Size.Width AndAlso e.Location.Y <= 8 + GenControl.Size.Height AndAlso e.Location.Y >= 5 Then ' right 
                TransparentDrawing1.Cursor = Cursors.VSplit
                LFRTUPDN = True
            ElseIf e.Location.X >= 5 AndAlso e.Location.X <= 8 + GenControl.Width AndAlso e.Location.Y <= 7 AndAlso e.Location.Y >= 7 - 7 Then ' up 
                TransparentDrawing1.Cursor = Cursors.HSplit
                LFRTUPDN = False
            ElseIf e.Location.X >= 5 AndAlso e.Location.X <= 8 + GenControl.Width AndAlso e.Location.Y >= 7 + GenControl.Height AndAlso e.Location.Y <= 7 + GenControl.Height + 7 Then ' down
                TransparentDrawing1.Cursor = Cursors.HSplit
                LFRTUPDN = True
            ElseIf e.Location.X <= 7 - 3 AndAlso e.Location.X >= 7 - 8 AndAlso e.Location.Y < 5 AndAlso e.Location.Y >= 0 Then ' LEFT UP
                TransparentDrawing1.Cursor = Cursors.SizeNWSE
                LFRTUPDN = False
            ElseIf e.Location.X <= 8 + GenControl.Size.Width + 5 AndAlso e.Location.X >= 7 + GenControl.Size.Width AndAlso e.Location.Y > 7 + GenControl.Height AndAlso e.Location.Y <= 7 + GenControl.Height + 7 Then ' RIGHT DOWN
                TransparentDrawing1.Cursor = Cursors.SizeNWSE
                LFRTUPDN = True
            ElseIf e.Location.X <= 7 - 3 AndAlso e.Location.X >= 7 - 8 AndAlso e.Location.Y > 7 + GenControl.Height AndAlso e.Location.Y <= 7 + GenControl.Height + 7 Then ' LEFT DOWN
                TransparentDrawing1.Cursor = Cursors.SizeNESW
                LFRTUPDN = False
            ElseIf e.Location.X <= 8 + GenControl.Size.Width + 5 AndAlso e.Location.X >= 7 + GenControl.Size.Width AndAlso e.Location.Y <= 7 AndAlso e.Location.Y >= 7 - 7 Then ' RIGHT UP
                TransparentDrawing1.Cursor = Cursors.SizeNESW
                LFRTUPDN = True
            Else
                TransparentDrawing1.Cursor = Cursors.Arrow
            End If
        End If



        If Not c Is Nothing AndAlso mMouseDown = True Then

            GenControl.SuspendLayout()

            Select Case TransparentDrawing1.Cursor
                Case Is = Cursors.VSplit
                    If LFRTUPDN = False Then ' left 
                        GenControl.SetBounds(GenControl.Location.X + (e.Location.X + TransparentDrawing1.Location.X - GenControl.Location.X) + 7, GenControl.Location.Y, GenControl.Width - (e.Location.X + TransparentDrawing1.Location.X - GenControl.Location.X) - 7, GenControl.Height)
                        TransparentDrawing1.SetBounds(TransparentDrawing1.Location.X + (e.Location.X - 7) + 7, TransparentDrawing1.Location.Y, GenControl.Width + 14, GenControl.Height + 14)
                    Else ' Right 
                        GenControl.SetBounds(GenControl.Location.X, GenControl.Location.Y, GenControl.Width + (e.Location.X + TransparentDrawing1.Location.X - GenControl.Location.X - GenControl.Width) - 5, GenControl.Height)
                        TransparentDrawing1.SetBounds(TransparentDrawing1.Location.X, TransparentDrawing1.Location.Y, GenControl.Width + 14, GenControl.Height + 14)
                    End If
                Case Is = Cursors.HSplit
                    If LFRTUPDN = False Then ' up
                        GenControl.SetBounds(GenControl.Location.X, GenControl.Location.Y + (e.Location.Y + TransparentDrawing1.Location.Y - GenControl.Location.Y) + 7, GenControl.Width, GenControl.Height - (e.Location.Y + TransparentDrawing1.Location.Y - GenControl.Location.Y) - 7)
                        TransparentDrawing1.SetBounds(TransparentDrawing1.Location.X, GenControl.Location.Y - 7, GenControl.Width + 14, GenControl.Height + 14)
                    Else ' Down
                        GenControl.SetBounds(GenControl.Location.X, GenControl.Location.Y, GenControl.Width, GenControl.Height - (GenControl.Height - e.Location.Y) - 12)
                        TransparentDrawing1.SetBounds(TransparentDrawing1.Location.X, GenControl.Location.Y - 7, GenControl.Width + 14, GenControl.Height + 14)
                    End If
                Case Is = Cursors.SizeNWSE
                    If LFRTUPDN = False Then ' Left Up
                        GenControl.SetBounds(GenControl.Location.X + e.Location.X + (TransparentDrawing1.Location.X - GenControl.Location.X) + 7, GenControl.Location.Y + (e.Location.Y + TransparentDrawing1.Location.Y - GenControl.Location.Y) + 7, GenControl.Width - (e.Location.X + TransparentDrawing1.Location.X - GenControl.Location.X) - 7, GenControl.Height - (e.Location.Y + TransparentDrawing1.Location.Y - GenControl.Location.Y) - 7)
                        TransparentDrawing1.SetBounds(TransparentDrawing1.Location.X + (e.Location.X - 7) + 7, GenControl.Location.Y - 7, GenControl.Width + 14, GenControl.Height + 14)
                    Else ' Right down
                        GenControl.SetBounds(GenControl.Location.X, GenControl.Location.Y, GenControl.Width + (e.Location.X + TransparentDrawing1.Location.X - GenControl.Location.X - GenControl.Width) - 5, GenControl.Height - (GenControl.Height - e.Location.Y) - 12)
                        TransparentDrawing1.SetBounds(TransparentDrawing1.Location.X, GenControl.Location.Y - 7, GenControl.Width + 14, GenControl.Height + 14)
                    End If
                Case Is = Cursors.SizeNESW
                    If LFRTUPDN = False Then ' left Down
                        GenControl.SetBounds(GenControl.Location.X + (e.Location.X + TransparentDrawing1.Location.X - GenControl.Location.X) + 7, GenControl.Location.Y, GenControl.Width - (e.Location.X + TransparentDrawing1.Location.X - GenControl.Location.X) - 7, GenControl.Height - (GenControl.Height - e.Location.Y) - 12)
                        TransparentDrawing1.SetBounds(TransparentDrawing1.Location.X + (e.Location.X - 7) + 7, GenControl.Location.Y - 7, GenControl.Width + 14, GenControl.Height + 14)
                    Else ' Right Up
                        GenControl.SetBounds(GenControl.Location.X, GenControl.Location.Y + (e.Location.Y + TransparentDrawing1.Location.Y - GenControl.Location.Y) + 7, GenControl.Width + (e.Location.X + TransparentDrawing1.Location.X - GenControl.Location.X - GenControl.Width) - 5, GenControl.Height - (e.Location.Y + TransparentDrawing1.Location.Y - GenControl.Location.Y) - 7)
                        TransparentDrawing1.SetBounds(TransparentDrawing1.Location.X, GenControl.Location.Y - 7, GenControl.Width + 14, GenControl.Height + 14)


                    End If

            End Select

            GenControl.ResumeLayout()
            TransparentDrawing1.Refresh()
            pnl.Refresh()

            Label10.BackColor = Panel5.BackColor
            Label10.Text = "w:" & GenControl.Size.Width & ", h:" & GenControl.Size.Height

            If Not Panel5.Location.X + GenControl.Location.X - Label10.Width - 6 <= 0 Then
                Label10.SetBounds(Panel5.Location.X + GenControl.Location.X - Label10.Width - 6, Label10.Location.Y, Label10.Width, Label10.Height)
            ElseIf Not Panel5.Location.X + GenControl.Location.X + 1 <= 0 Then
                Label10.SetBounds(Panel5.Location.X + GenControl.Location.X + 1, Label10.Location.Y, Label10.Width, Label10.Height)
            ElseIf Not Panel5.Location.X + GenControl.Location.X + GenControl.Width + Label10.Width + 6 >= PictureBox1.Width - 1 Then
                Label10.SetBounds(Panel5.Location.X + GenControl.Location.X + GenControl.Width + 6, Label10.Location.Y, Label10.Width, Label10.Height)
            ElseIf Not Panel5.Location.X + GenControl.Location.X + GenControl.Width - Label10.Width >= PictureBox1.Width - 1 Then
                Label10.SetBounds(Panel5.Location.X + GenControl.Location.X + GenControl.Width - Label10.Width, Label10.Location.Y, Label10.Width, Label10.Height)
                'Else
                '    Label10.Hide()
            End If

            If Not Panel5.Location.Y + GenControl.Location.Y - Label10.Height - 6 <= 0 Then
                Label10.SetBounds(Label10.Location.X, Panel5.Location.Y + GenControl.Location.Y - Label10.Height - 6, Label10.Width, Label10.Height)
            ElseIf Not Panel5.Location.y + GenControl.Location.y + 1 <= 0 Then
                Label10.SetBounds(Label10.Location.X, Panel5.Location.Y + GenControl.Location.Y + 1, Label10.Width, Label10.Height)

            ElseIf Not Panel5.Location.Y + GenControl.Location.Y + GenControl.Height + Label10.Height + 6 >= PictureBox1.Height - 1 Then
                Label10.SetBounds(Label10.Location.X, Panel5.Location.Y + GenControl.Location.Y + GenControl.Height + 6, Label10.Width, Label10.Height)
            ElseIf Not Panel5.Location.Y + GenControl.Location.Y + GenControl.Height - Label10.Height >= PictureBox1.Height - 1 Then
                Label10.SetBounds(Label10.Location.X, Panel5.Location.Y + GenControl.Location.Y + GenControl.Height - Label10.Height, Label10.Height, Label10.Height)

            End If

            Label10.Refresh()

            'Form1.TransparentDrawing1.Refresh()
            'pnl.Refresh()
            Dim g As Graphics = pnl.CreateGraphics


                g.DrawLine(grayPen, 0, c.Location.Y + c.Height, pnl.Width, c.Location.Y + c.Height)
                g.DrawLine(grayPen, 0, c.Location.Y - 1, pnl.Width, c.Location.Y - 1)

                g.DrawLine(grayPen, c.Location.X - 1, 0, c.Location.X - 1, pnl.Height)
                g.DrawLine(grayPen, c.Location.X + c.Width, 0, c.Location.X + c.Width, pnl.Height)




                Dim j As Graphics = TransparentDrawing1.CreateGraphics
                j.FillRectangle(colorBrushDG, 7 - 5, 7 - 4, 1, GenControl.Size.Height + 7) ' Left

                j.FillRectangle(colorBrushDG, 7 + GenControl.Size.Width + 4, 7 - 4, 1, GenControl.Size.Height + 7) ' Right

                j.FillRectangle(colorBrushDG, 7 - 4, 7 - 5, GenControl.Size.Width + 7, 1) 'Up
                j.FillRectangle(colorBrushDG, 7 - 4, 7 + GenControl.Size.Height + 4, GenControl.Size.Width + 7, 1) ' down

                j.FillRectangle(colorBrushB, 7 - 7, 7 - 7, 5, 5) ' LEFT UP CORNER
                j.FillRectangle(colorBrushB, 7 - 7, 7 + GenControl.Size.Height + 2, 5, 5) ' LEFT DOWN CORNER

                j.FillRectangle(colorBrushB, 7 + GenControl.Size.Width + 2, 7 - 7, 5, 5) ' RIGHT UP CORNER
                j.FillRectangle(colorBrushB, 7 + GenControl.Size.Width + 2, 7 + GenControl.Size.Height + 2, 5, 5) ' RIGHT DOWN CORNER 

            End If
    End Sub
    Private Sub TransparentDrawing1_MouseUp(sender As Object, e As MouseEventArgs) Handles TransparentDrawing1.MouseUp
        If Not c Is Nothing Then


            GenControl.ResumeLayout()

            If GenControl.Width > XofFormApp Then GenControl.Width = XofFormApp
            If GenControl.Height > YofFormApp Then GenControl.Height = YofFormApp

            If GenControl.Location.X < 0 Then GenControl.Location = New Point(0, GenControl.Location.Y)
            If GenControl.Location.Y < 0 Then GenControl.Location = New Point(GenControl.Location.X, 0)
            If GenControl.Location.X + GenControl.Size.Width > XofFormApp Then GenControl.Location = New Point(XofFormApp - GenControl.Size.Width, GenControl.Location.Y)
            If GenControl.Location.Y + GenControl.Size.Height > YofFormApp Then GenControl.Location = New Point(GenControl.Location.X, YofFormApp - GenControl.Size.Height)

            ChangeASpecificPropOfContr(PropertiesOfControl, "Size=", "Size=" & GenControl.Size.ToString.Replace("Width=", Nothing).Replace(",", ";").Replace("Height=", Nothing).Replace(" ", Nothing), GenControl.Name)
            ChangeASpecificPropOfContr(PropertiesOfControl, "Location=", "Location={" & GenControl.Location.X & ";" & GenControl.Location.Y & "}", GenControl.Name)
            ClickGenControlsPropert(GenControl.Name)


            With TransparentDrawing1
                .Hide()
                .Location = New Point(GenControl.Location.X - 7, GenControl.Location.Y - 7)
                .Size = New Size(GenControl.Width + 14, GenControl.Height + 14)
                .BringToFront()
                .Show()
                .Refresh()
            End With
            GenControl.BringToFront()

            pnl.Refresh() ' here prob

            mMouseDown = False

            Dim j As Graphics = TransparentDrawing1.CreateGraphics
            TransparentDrawing1.Refresh()
            'Form1.PictureBox20.BackColor = Color.Transparent

            j.FillRectangle(colorBrushDG, 7 - 5, 7 - 4, 1, GenControl.Size.Height + 7) ' Left
            j.FillRectangle(colorBrushDG, 7 + GenControl.Size.Width + 4, 7 - 4, 1, GenControl.Size.Height + 7) ' Right

            j.FillRectangle(colorBrushDG, 7 - 4, 7 - 5, GenControl.Size.Width + 7, 1) 'Up
            j.FillRectangle(colorBrushDG, 7 - 4, 7 + GenControl.Size.Height + 4, GenControl.Size.Width + 7, 1) ' down

            j.FillRectangle(colorBrushB, 7 - 7, 7 - 7, 5, 5) ' LEFT UP CORNER
            j.FillRectangle(colorBrushB, 7 - 7, 7 + GenControl.Size.Height + 2, 5, 5) ' LEFT DOWN CORNER

            j.FillRectangle(colorBrushB, 7 + GenControl.Size.Width + 2, 7 - 7, 5, 5) ' RIGHT UP CORNER
            j.FillRectangle(colorBrushB, 7 + GenControl.Size.Width + 2, 7 + GenControl.Size.Height + 2, 5, 5) ' RIGHT DOWN CORNER 

            Label10.Hide()
            ' GenControl.Location = New Point(GenControl.Location.X - (GenControl.Location.X - e.X) + (e.Location.X - GenControl.Location.X), GenControl.Location.Y)
            ' GenControl.Size = New Size(GenControl.Size.Width + (GenControl.Location.X - e.X) + (e.Location.X - GenControl.Location.X), GenControl.Size.Height)

        End If
    End Sub
    Private Sub TransparentDrawing1_MouseLeave(sender As Object, e As EventArgs) Handles TransparentDrawing1.MouseLeave
        If mouseonform = False AndAlso Not GenControl Is Nothing Then
            Dim j As Graphics = TransparentDrawing1.CreateGraphics

            j.FillRectangle(colorBrushDG, 7 - 5, 7 - 4, 1, GenControl.Size.Height + 7) ' Left
            j.FillRectangle(colorBrushDG, 7 + GenControl.Size.Width + 4, 7 - 4, 1, GenControl.Size.Height + 7) ' Right

            j.FillRectangle(colorBrushDG, 7 - 4, 7 - 5, GenControl.Size.Width + 7, 1) 'Up
            j.FillRectangle(colorBrushDG, 7 - 4, 7 + GenControl.Size.Height + 4, GenControl.Size.Width + 7, 1) ' down

            j.FillRectangle(colorBrushB, 7 - 7, 7 - 7, 5, 5) ' LEFT UP CORNER
            j.FillRectangle(colorBrushB, 7 - 7, 7 + GenControl.Size.Height + 2, 5, 5) ' LEFT DOWN CORNER

            j.FillRectangle(colorBrushB, 7 + GenControl.Size.Width + 2, 7 - 7, 5, 5) ' RIGHT UP CORNER
            j.FillRectangle(colorBrushB, 7 + GenControl.Size.Width + 2, 7 + GenControl.Size.Height + 2, 5, 5) ' RIGHT DOWN CORNER 

        End If
    End Sub

    Dim rc As ResizeableControl

    Public Shared colorBrushDG As Brush = Brushes.DarkGray
    Public Shared colorBrushB As Brush = Brushes.Black

    Shared c As Control
    Shared mouseonform As Boolean = False

    Public Shared mMouseDown As Boolean = False
    Public Class ResizeableControl
        Shared WithEvents PanelIn As TransparentDrawing = Form1.TransparentDrawing1
        Private WithEvents mControl As Control



        Public Sub New(ByVal Control As Control)
            mControl = Control
        End Sub
        Dim CTRL As Boolean = False
        Dim ALT As Boolean = False
        Private Sub mControl_KeyDown(sender As Object, e As KeyEventArgs) Handles mControl.KeyDown
            c = CType(sender, Control)


            If CTRL = False AndAlso e.KeyCode = Keys.ControlKey Then
                CTRL = True
                ' pnl.Refresh()
                'Form1.TransparentDrawing1.Refresh()
                'pnl.Refresh()
                GoTo Here

            ElseIf ALT = False AndAlso e.KeyCode = Keys.Menu Then
                ALT = True
Here:           pnl.Refresh()
                'Form1.TransparentDrawing1.Refresh()
                'pnl.Refresh()
                Dim g As Graphics = pnl.CreateGraphics


                g.DrawLine(grayPen, 0, c.Location.Y + c.Height, pnl.Width, c.Location.Y + c.Height)
                g.DrawLine(grayPen, 0, c.Location.Y - 1, pnl.Width, c.Location.Y - 1)

                g.DrawLine(grayPen, c.Location.X - 1, 0, c.Location.X - 1, pnl.Height)
                g.DrawLine(grayPen, c.Location.X + c.Width, 0, c.Location.X + c.Width, pnl.Height)

                With Form1.TransparentDrawing1
                    .Hide()
                    .Location = New Point(c.Location.X - 7, c.Location.Y - 7)
                    .Size = New Size(c.Width + 14, c.Height + 14)
                    .BringToFront()
                    .Show()
                    .Refresh()
                End With
                c.BringToFront()


                ' mMouseDown = False
                Dim j As Graphics = Form1.TransparentDrawing1.CreateGraphics
                ' Form1.TransparentDrawing1.Refresh()
                'Form1.PictureBox20.BackColor = Color.Transparent

                j.FillRectangle(colorBrushDG, 7 - 5, 7 - 4, 1, c.Size.Height + 7) ' Left
                j.FillRectangle(colorBrushDG, 7 + c.Size.Width + 4, 7 - 4, 1, c.Size.Height + 7) ' Right

                j.FillRectangle(colorBrushDG, 7 - 4, 7 - 5, c.Size.Width + 7, 1) 'Up
                j.FillRectangle(colorBrushDG, 7 - 4, 7 + c.Size.Height + 4, c.Size.Width + 7, 1) ' down

                j.FillRectangle(colorBrushB, 7 - 7, 7 - 7, 5, 5) ' LEFT UP CORNER
                j.FillRectangle(colorBrushB, 7 - 7, 7 + c.Size.Height + 2, 5, 5) ' LEFT DOWN CORNER

                j.FillRectangle(colorBrushB, 7 + c.Size.Width + 2, 7 - 7, 5, 5) ' RIGHT UP CORNER
                j.FillRectangle(colorBrushB, 7 + c.Size.Width + 2, 7 + c.Size.Height + 2, 5, 5) ' RIGHT DOWN CORNER 
            End If

            If CTRL = True Then
                If e.KeyCode = Keys.Up Then
                    c.Size = New Size(c.Size.Width, c.Size.Height - 1)
                ElseIf e.KeyCode = Keys.Down Then
                    c.Size = New Size(c.Size.Width, c.Size.Height + 1)
                ElseIf e.KeyCode = Keys.Left Then
                    c.Size = New Size(c.Size.Width - 1, c.Size.Height)
                ElseIf e.KeyCode = Keys.Right Then
                    c.Size = New Size(c.Size.Width + 1, c.Size.Height)
                End If

                If e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Or e.KeyCode = Keys.Left Or e.KeyCode = Keys.Right Then

                    c.ResumeLayout()

                    If c.Width > XofFormApp Then c.Width = XofFormApp
                    If c.Height > YofFormApp Then c.Height = YofFormApp

                    If c.Location.X < 0 Then c.Location = New Point(0, c.Location.Y)
                    If c.Location.Y < 0 Then c.Location = New Point(c.Location.X, 0)
                    If c.Location.X + c.Size.Width > XofFormApp Then c.Location = New Point(XofFormApp - c.Size.Width, c.Location.Y)
                    If c.Location.Y + c.Size.Height > YofFormApp Then c.Location = New Point(c.Location.X, YofFormApp - c.Size.Height)

                    Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Size=", "Size=" & c.Size.ToString.Replace("Width=", Nothing).Replace(",", ";").Replace("Height=", Nothing).Replace(" ", Nothing), c.Name)
                    Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={" & c.Location.X & ";" & c.Location.Y & "}", c.Name)
                    Form1.ClickGenControlsPropert(c.Name)


                    With Form1.TransparentDrawing1
                        .Hide()
                        .Location = New Point(c.Location.X - 7, c.Location.Y - 7)
                        .Size = New Size(c.Width + 14, c.Height + 14)
                        .BringToFront()
                        .Show()
                        .Refresh()
                    End With
                    c.BringToFront()


                    mMouseDown = False

                    pnl.Refresh()
                    'Form1.TransparentDrawing1.Refresh()
                    'pnl.Refresh()
                    Dim g As Graphics = pnl.CreateGraphics


                    g.DrawLine(grayPen, 0, c.Location.Y + c.Height, pnl.Width, c.Location.Y + c.Height)
                    g.DrawLine(grayPen, 0, c.Location.Y - 1, pnl.Width, c.Location.Y - 1)

                    g.DrawLine(grayPen, c.Location.X - 1, 0, c.Location.X - 1, pnl.Height)
                    g.DrawLine(grayPen, c.Location.X + c.Width, 0, c.Location.X + c.Width, pnl.Height)

                    Dim j As Graphics = Form1.TransparentDrawing1.CreateGraphics
                    ' Form1.TransparentDrawing1.Refresh()
                    'Form1.PictureBox20.BackColor = Color.Transparent

                    j.FillRectangle(colorBrushDG, 7 - 5, 7 - 4, 1, c.Size.Height + 7) ' Left
                    j.FillRectangle(colorBrushDG, 7 + c.Size.Width + 4, 7 - 4, 1, c.Size.Height + 7) ' Right

                    j.FillRectangle(colorBrushDG, 7 - 4, 7 - 5, c.Size.Width + 7, 1) 'Up
                    j.FillRectangle(colorBrushDG, 7 - 4, 7 + c.Size.Height + 4, c.Size.Width + 7, 1) ' down

                    j.FillRectangle(colorBrushB, 7 - 7, 7 - 7, 5, 5) ' LEFT UP CORNER
                    j.FillRectangle(colorBrushB, 7 - 7, 7 + c.Size.Height + 2, 5, 5) ' LEFT DOWN CORNER

                    j.FillRectangle(colorBrushB, 7 + c.Size.Width + 2, 7 - 7, 5, 5) ' RIGHT UP CORNER
                    j.FillRectangle(colorBrushB, 7 + c.Size.Width + 2, 7 + c.Size.Height + 2, 5, 5) ' RIGHT DOWN CORNER 




                End If

            ElseIf ALT = True Then
                If e.KeyCode = Keys.Up Then
                    c.Location = New Point(c.Location.X, c.Location.Y - 1)
                ElseIf e.KeyCode = Keys.Down Then
                    c.Location = New Point(c.Location.X, c.Location.Y + 1)
                ElseIf e.KeyCode = Keys.Left Then
                    c.Location = New Point(c.Location.X - 1, c.Location.Y)
                ElseIf e.KeyCode = Keys.Right Then
                    c.Location = New Point(c.Location.X + 1, c.Location.Y)
                End If

                If e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Or e.KeyCode = Keys.Left Or e.KeyCode = Keys.Right Then
                    c.Refresh()


                    If Form1.RetriveInWhatPanelis(c) = Form1.Panel5.Name Then Form1.Panel5.Controls.Add(Form1.TransparentDrawing1) Else Form1.FindControl(Form1.RetriveInWhatPanelis(c), Form1.Panel5).Controls.Add(Form1.TransparentDrawing1)


                    If c.Location.X < 0 Then c.Location = New Point(0, c.Location.Y)
                    If c.Location.Y < 0 Then c.Location = New Point(c.Location.X, 0)
                    If c.Location.X + c.Size.Width > XofFormApp Then c.Location = New Point(XofFormApp - c.Size.Width, c.Location.Y)
                    If c.Location.Y + c.Size.Height > YofFormApp Then c.Location = New Point(c.Location.X, YofFormApp - c.Size.Height)
                    'c.SendToBack()

                    If c.Width < 12 Then c.Width = 12
                    If c.Height < 12 Then c.Height = 12

                    '     MsgBox(PropertiesOfControl)
                    Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={" & c.Location.X & ";" & c.Location.Y & "}", c.Name)
                    ' Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Size=", "Size=" & c.Size.ToString.Replace("Width=", Nothing).Replace(",", ";").Replace("Height=", Nothing).Replace(" ", Nothing), c.Name)

                    With Form1.TransparentDrawing1
                        .Hide()
                        .Location = New Point(c.Location.X - 7, c.Location.Y - 7)
                        .Size = New Size(c.Width + 14, c.Height + 14)
                        .BringToFront()
                        .Show()
                        .Refresh()
                    End With


                    c.BringToFront()

                    pnl.Refresh()
                    'Form1.TransparentDrawing1.Refresh()
                    'pnl.Refresh()
                    Dim g As Graphics = pnl.CreateGraphics


                    g.DrawLine(grayPen, 0, c.Location.Y + c.Height, pnl.Width, c.Location.Y + c.Height)
                    g.DrawLine(grayPen, 0, c.Location.Y - 1, pnl.Width, c.Location.Y - 1)

                    g.DrawLine(grayPen, c.Location.X - 1, 0, c.Location.X - 1, pnl.Height)
                    g.DrawLine(grayPen, c.Location.X + c.Width, 0, c.Location.X + c.Width, pnl.Height)

                    Dim j As Graphics = Form1.TransparentDrawing1.CreateGraphics

                    j.FillRectangle(colorBrushDG, 7 - 5, 7 - 4, 1, c.Size.Height + 7) ' Left
                    j.FillRectangle(colorBrushDG, 7 + c.Size.Width + 4, 7 - 4, 1, c.Size.Height + 7) ' Right

                    j.FillRectangle(colorBrushDG, 7 - 4, 7 - 5, c.Size.Width + 7, 1) 'Up
                    j.FillRectangle(colorBrushDG, 7 - 4, 7 + c.Size.Height + 4, c.Size.Width + 7, 1) ' down

                    j.FillRectangle(colorBrushB, 7 - 7, 7 - 7, 5, 5) ' LEFT UP CORNER
                    j.FillRectangle(colorBrushB, 7 - 7, 7 + c.Size.Height + 2, 5, 5) ' LEFT DOWN CORNER

                    j.FillRectangle(colorBrushB, 7 + c.Size.Width + 2, 7 - 7, 5, 5) ' RIGHT UP CORNER
                    j.FillRectangle(colorBrushB, 7 + c.Size.Width + 2, 7 + c.Size.Height + 2, 5, 5) ' RIGHT DOWN CORNER 


                    'MsgBox(ProjectSaveString)
                    Form1.ClickGenControlsPropert(c.Name) ' meh much cpu

                End If
            End If
            Form1.GenControl.Select()
        End Sub




        Private Sub mControl_KeyUp(sender As Object, e As KeyEventArgs) Handles mControl.KeyUp
            c = CType(sender, Control)
            Form1.GenControl.Select()
            If e.KeyCode = Keys.ControlKey Then
                CTRL = False
                pnl.Refresh()
            ElseIf e.KeyCode = Keys.Menu Then
                ALT = False
                pnl.Refresh()
            ElseIf e.KeyCode = Keys.Enter Then
                Form1.Go_or_Create_Event()
            End If



        End Sub

        Shared Lochelp As Point
        Public Shared Namehelp As String
        ' Public Shared l As Point
        Public Shared Sub mControl_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles mControl.MouseDown
            c = CType(sender, Control)
            '  If e.Button = Windows.Forms.MouseButtons.Left AndAlso Not c.Name.StartsWith("APanel") Then
            ' MsgBox(Form1.PropertiesOfControl)
            If c.Name.StartsWith("APanel") AndAlso IsListViewSelectes() = True Then
                Exit Sub
            Else

                If Form1.RetriveInWhatPanelis(c) = Form1.Panel5.Name Then Form1.Panel5.Controls.Add(Form1.TransparentDrawing1) Else Form1.FindControl(Form1.RetriveInWhatPanelis(c), Form1.Panel5).Controls.Add(Form1.TransparentDrawing1)
                Form1.ClickGenControlsPropert(c.Name)

                DownClickLoc = e.Location

                ' MsgBox(Form1.PropertiesOfControl)
                'Form1.CursorDenDiaforetik = True
                ' Form1.click1 += 1
                ' Console.WriteLine("DOWN")
                'If Form1.mCl = 1 Then Form1.mousenowclick = e.Location.X
                With Form1.TransparentDrawing1
                    .Hide()
                    .Location = New Point(c.Location.X - 7, c.Location.Y - 7)
                    .Size = New Size(c.Width + 14, c.Height + 14)
                    .BringToFront()
                    .Show()
                    .Refresh()
                End With



                If Form1.RetriveInWhatPanelis(c) = Form1.Panel5.Name Then pnl = Form1.Panel5 Else pnl = Form1.FindControl(Form1.RetriveInWhatPanelis(c), Form1.Panel5)

                grayPen.DashPattern = {1, 1}

                c.BringToFront()

                Lochelp = c.Location
                Namehelp = c.Name
                'MsgBox(111)
                Form1.mousemoves = True
                ' Form1.loca = e.Location
                ' End If
                ' Me.Location += e.Location - Loc
                Form1.loca = e.Location
                mMouseDown = True
            End If
        End Sub
        ' Public Shared pnl As Panel = Nothing
        Shared DownClickLoc As New Point
        Shared EdgeBoolLR As Boolean = False
        Shared EdgeBoolUD As Boolean = False
        Public Shared Sub mControl_MouseMove(sender As Object, e As MouseEventArgs) Handles mControl.MouseMove   ' OXI EDW .... OTAN 8A KANEI CREATE TO CONTROL 8A KALEI KAI ENA FUNCTION GIA DRAW TA BORDERS :)

            If Form1.mousemoves = True Then

                c = CType(sender, Control)
                ' mouseCanClick = False

                If EdgeBoolLR = False And EdgeBoolUD = False Then
                    c.Location += e.Location - Form1.loca
                ElseIf EdgeBoolLR = False Then
                    c.Location += New Point(e.Location.X - Form1.loca.X, 0)
                ElseIf EdgeBoolUD = False Then
                    c.Location += New Point(0, e.Location.Y - Form1.loca.Y)
                End If

                If DownClickLoc.X < e.X - 10 Or DownClickLoc.X > e.X + 10 Then
                    EdgeBoolLR = False
                    Exit Sub
                End If
                If DownClickLoc.Y < e.Y - 10 Or DownClickLoc.Y > e.Y + 10 Then
                    EdgeBoolUD = False
                    Exit Sub
                End If

                If Not Lochelp = c.Location AndAlso Namehelp = c.Name Then

                    Dim LeftBorderColor As Color = Color.Gray
                    Dim RightBorderColor As Color = Color.Gray
                    Dim DownBorderColor As Color = Color.Gray
                    Dim UpBorderColor As Color = Color.Gray

                    EdgeBoolLR = False
                    EdgeBoolUD = False

                    For Each cont As Control In pnl.Controls
                        If Not cont Is c And cont.Visible = True And Not cont Is Form1.TransparentDrawing1 And Not cont Is Form1.TransparentDrawing2 Then
                            If cont.Location.X = c.Location.X Then
                                LeftBorderColor = Color.FromArgb(24, 131, 215)
                                EdgeBoolLR = True
                            End If

                            If cont.Location.Y = c.Location.Y Then
                                UpBorderColor = Color.FromArgb(24, 131, 215)
                                EdgeBoolUD = True
                            End If

                            If cont.Location.X + cont.Width + 1 = c.Location.X Then
                                LeftBorderColor = Color.FromArgb(24, 131, 215)
                                EdgeBoolLR = True
                            End If

                            If cont.Location.Y + cont.Height + 1 = c.Location.Y Then
                                UpBorderColor = Color.FromArgb(24, 131, 215)
                                EdgeBoolUD = True
                            End If

                            If cont.Location.X = c.Location.X + c.Width + 1 Then
                                RightBorderColor = Color.FromArgb(24, 131, 215)
                                EdgeBoolLR = True
                            End If

                            If cont.Location.Y = c.Location.Y + c.Height + 1 Then
                                DownBorderColor = Color.FromArgb(24, 131, 215)
                                EdgeBoolUD = True
                            End If

                            If cont.Location.X + cont.Width + 1 = c.Location.X + c.Width + 1 Then
                                RightBorderColor = Color.FromArgb(24, 131, 215)
                                EdgeBoolLR = True
                            End If

                            If cont.Location.Y + cont.Height + 1 = c.Location.Y + c.Height + 1 Then
                                DownBorderColor = Color.FromArgb(24, 131, 215)
                                EdgeBoolUD = True
                            End If
                        End If
                    Next

                    Form1.Label10.BackColor = Form1.Panel5.BackColor
                    Form1.Label10.Text = "X:" & Form1.GenControl.Location.X & ", Y:" & Form1.GenControl.Location.Y

                    If Not Form1.Panel5.Location.X + Form1.GenControl.Location.X - Form1.Label10.Width <= 0 Then
                        Form1.Label10.SetBounds(Form1.Panel5.Location.X + Form1.GenControl.Location.X - Form1.Label10.Width, Form1.Label10.Location.Y, Form1.Label10.Width, Form1.Label10.Height)
                    ElseIf Not Form1.Panel5.Location.X + Form1.GenControl.Location.X + 1 <= 0 Then
                        Form1.Label10.SetBounds(Form1.Panel5.Location.X + Form1.GenControl.Location.X + 1, Form1.Label10.Location.Y, Form1.Label10.Width, Form1.Label10.Height)
                    ElseIf Not Form1.Panel5.Location.X + Form1.GenControl.Location.X + Form1.GenControl.Width + Form1.Label10.Width >= Form1.PictureBox1.Width - 1 Then
                        Form1.Label10.SetBounds(Form1.Panel5.Location.X + Form1.GenControl.Location.X + Form1.GenControl.Width + 2, Form1.Label10.Location.Y, Form1.Label10.Width, Form1.Label10.Height)
                    ElseIf Not Form1.Panel5.Location.X + Form1.GenControl.Location.X + Form1.GenControl.Width - Form1.Label10.Width >= Form1.PictureBox1.Width - 1 Then
                        Form1.Label10.SetBounds(Form1.Panel5.Location.X + Form1.GenControl.Location.X + Form1.GenControl.Width - Form1.Label10.Width, Form1.Label10.Location.Y, Form1.Label10.Width, Form1.Label10.Height)
                        'Else
                        '    Form1.Label10.Hide()
                    End If

                    If Not Form1.Panel5.Location.Y + Form1.GenControl.Location.Y - Form1.Label10.Height <= 0 Then
                        Form1.Label10.SetBounds(Form1.Label10.Location.X, Form1.Panel5.Location.Y + Form1.GenControl.Location.Y - Form1.Label10.Height, Form1.Label10.Width, Form1.Label10.Height)
                    ElseIf Not Form1.Panel5.Location.Y + Form1.GenControl.Location.Y + 1 <= 0 Then
                        Form1.Label10.SetBounds(Form1.Label10.Location.X, Form1.Panel5.Location.Y + Form1.GenControl.Location.Y + 1, Form1.Label10.Width, Form1.Label10.Height)

                    ElseIf Not Form1.Panel5.Location.Y + Form1.GenControl.Location.Y + Form1.GenControl.Height + Form1.Label10.Height >= Form1.PictureBox1.Height - 1 Then
                        Form1.Label10.SetBounds(Form1.Label10.Location.X, Form1.Panel5.Location.Y + Form1.GenControl.Location.Y + Form1.GenControl.Height + 2, Form1.Label10.Width, Form1.Label10.Height)
                    ElseIf Not Form1.Panel5.Location.Y + Form1.GenControl.Location.Y + Form1.GenControl.Height - Form1.Label10.Height >= Form1.PictureBox1.Height - 1 Then
                        Form1.Label10.SetBounds(Form1.Label10.Location.X, Form1.Panel5.Location.Y + Form1.GenControl.Location.Y + Form1.GenControl.Height - Form1.Label10.Height, Form1.Label10.Height, Form1.Label10.Height)

                    End If

                    Form1.Label10.Refresh()
                    Form1.Label10.Show()

                    'Form1.TransparentDrawing1.Refresh()
                    'pnl.Refresh()

                    Dim g As Graphics = pnl.CreateGraphics

                    grayPen.Color = DownBorderColor

                    pnl.Refresh()

                    g.DrawLine(grayPen, 0, c.Location.Y + c.Height, pnl.Width, c.Location.Y + c.Height)
                    grayPen.Color = UpBorderColor
                    g.DrawLine(grayPen, 0, c.Location.Y - 1, pnl.Width, c.Location.Y - 1)

                    grayPen.Color = LeftBorderColor
                    g.DrawLine(grayPen, c.Location.X - 1, 0, c.Location.X - 1, pnl.Height)
                    grayPen.Color = RightBorderColor
                    g.DrawLine(grayPen, c.Location.X + c.Width, 0, c.Location.X + c.Width, pnl.Height)
                    'g.Dispose()

                End If

                'Form1.TransparentDrawing1.Refresh()



                Form1.mouseCanClick = False
                ' c.BringToFront()
                ' MsgBox(AButton.Text)
                ' mouseCanClick = False



            End If
        End Sub

        Public Shared Sub mControl_MouseUp(sender As Object, e As MouseEventArgs) Handles mControl.MouseUp ' meh :3 -->  14/11/2015 

            If c Is Nothing Then Exit Sub ' there is a reason for this it is because when you double click on one project to open it and after , when the window with folders close if a control is under cursor on panel5(even if panel5 is not enabled) the mouseup event is being called and i have NullReff...Exception  7/11/2015 ... :'D

            c = CType(sender, Control)

            grayPen.Color = Color.Gray

            If c.Name.StartsWith("APanel") AndAlso IsListViewSelectes() = True Then

                Form1.AddControl(e, c)
                Exit Sub
            End If
            ' Console.WriteLine("UP")
            ' Console.WriteLine(" ")
            mouseonform = False
            c.Refresh()

            If Form1.RetriveInWhatPanelis(c) = Form1.Panel5.Name Then Form1.Panel5.Controls.Add(Form1.TransparentDrawing1) Else Form1.FindControl(Form1.RetriveInWhatPanelis(c), Form1.Panel5).Controls.Add(Form1.TransparentDrawing1)



            'grayPen.DashStyle = DashStyle.Dot


            'Form1.PictureBox20.SuspendLayout()


            mMouseDown = False

            Form1.Timer20.Start()

            '    If c.Width > XofFormApp Then c.Width = XofFormApp
            '     If c.Height > YofFormApp Then c.Height = YofFormApp

            If c.Location.X < 0 Then c.Location = New Point(0, c.Location.Y)
            If c.Location.Y < 0 Then c.Location = New Point(c.Location.X, 0)
            If c.Location.X + c.Size.Width > XofFormApp Then c.Location = New Point(XofFormApp - c.Size.Width, c.Location.Y)
            If c.Location.Y + c.Size.Height > YofFormApp Then c.Location = New Point(c.Location.X, YofFormApp - c.Size.Height)
            'c.SendToBack()

            If c.Width < 12 Then c.Width = 12
            If c.Height < 12 Then c.Height = 12

            '     MsgBox(PropertiesOfControl)
            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={" & c.Location.X & ";" & c.Location.Y & "}", c.Name)
            ' Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Size=", "Size=" & c.Size.ToString.Replace("Width=", Nothing).Replace(",", ";").Replace("Height=", Nothing).Replace(" ", Nothing), c.Name)
            pnl.Refresh()

            With Form1.TransparentDrawing1
                .Hide()
                .Location = New Point(c.Location.X - 7, c.Location.Y - 7)
                .Size = New Size(c.Width + 14, c.Height + 14)
                .BringToFront()
                .Show()
                .Refresh()
            End With


            c.BringToFront()

            Dim j As Graphics = Form1.TransparentDrawing1.CreateGraphics

            j.FillRectangle(colorBrushDG, 7 - 5, 7 - 4, 1, c.Size.Height + 7) ' Left
            j.FillRectangle(colorBrushDG, 7 + c.Size.Width + 4, 7 - 4, 1, c.Size.Height + 7) ' Right

            j.FillRectangle(colorBrushDG, 7 - 4, 7 - 5, c.Size.Width + 7, 1) 'Up
            j.FillRectangle(colorBrushDG, 7 - 4, 7 + c.Size.Height + 4, c.Size.Width + 7, 1) ' down

            j.FillRectangle(colorBrushB, 7 - 7, 7 - 7, 5, 5) ' LEFT UP CORNER
            j.FillRectangle(colorBrushB, 7 - 7, 7 + c.Size.Height + 2, 5, 5) ' LEFT DOWN CORNER

            j.FillRectangle(colorBrushB, 7 + c.Size.Width + 2, 7 - 7, 5, 5) ' RIGHT UP CORNER
            j.FillRectangle(colorBrushB, 7 + c.Size.Width + 2, 7 + c.Size.Height + 2, 5, 5) ' RIGHT DOWN CORNER 


            'MsgBox(ProjectSaveString)
            Form1.ClickGenControlsPropert(c.Name)
            ' MsgBox(c.Name & vbNewLine & "UsrName: " & c.Tag)

            Form1.Label8.Hide()
            Form1.Label10.Hide()
            'GenControl.Location = New Point(c.Location)

            '_Location = c.Location 

            Form1.mousemoves = False

            ' If Not mousenowclick = mousepot Then
            '  mousemoves = False
            '    mCl = 0

            ' Else

            If e.Button = Windows.Forms.MouseButtons.Right Then
                c.ContextMenuStrip = Form1.ContextMenuStrip1
                Form1.rightclick(c, e)
                'Form1.ContextMenuStrip1.Show()
            End If

            ' If CursorDenDiaforetik = True Then
            Form1.mouseCanClick = True
            If Not e.Button = Windows.Forms.MouseButtons.Right Then Form1.click1 += 1
            If e.Button = Windows.Forms.MouseButtons.Left AndAlso Form1.click1 = 1 Then
                '    mousemoves = False
                ' MsgBox(c.Text)
                ' Dim c As Button


                ' c = CType(sender, Button)
                Form1.GenControl = c
                ' Console.WriteLine(Form1.click1)

                Form1.Timer1.Start()
                ' End If

                'Form1.click1 = 0
                'Console.WriteLine(Form1.click1)
                ' Console.WriteLine(vbNewLine)
            End If
            ' End If



        End Sub

    End Class
    Private Sub Timer20_Tick(sender As Object, e As EventArgs) Handles Timer20.Tick
        If Not c Is Nothing AndAlso mMouseDown = False Then

            Dim j As Graphics = TransparentDrawing1.CreateGraphics

            j.FillRectangle(colorBrushDG, 7 - 5, 7 - 4, 1, c.Size.Height + 7) ' Left
            j.FillRectangle(colorBrushDG, 7 + c.Size.Width + 4, 7 - 4, 1, c.Size.Height + 7) ' Right

            j.FillRectangle(colorBrushDG, 7 - 4, 7 - 5, c.Size.Width + 7, 1) 'Up
            j.FillRectangle(colorBrushDG, 7 - 4, 7 + c.Size.Height + 4, c.Size.Width + 7, 1) ' down

            j.FillRectangle(colorBrushB, 7 - 7, 7 - 7, 5, 5) ' LEFT UP CORNER
            j.FillRectangle(colorBrushB, 7 - 7, 7 + c.Size.Height + 2, 5, 5) ' LEFT DOWN CORNER

            j.FillRectangle(colorBrushB, 7 + c.Size.Width + 2, 7 - 7, 5, 5) ' RIGHT UP CORNER
            j.FillRectangle(colorBrushB, 7 + c.Size.Width + 2, 7 + c.Size.Height + 2, 5, 5) ' RIGHT DOWN CORNER 
            '  Console.WriteLine(i)
            ' i += 1
            Timer20.Stop()
        End If
    End Sub
    'Private Sub Timer4_Tick(sender As Object, e As EventArgs)


    '    'Dim pnl As Panel
    '    'If RetriveInWhatPanelis(c) = Panel5.Name Then pnl = Panel5 Else pnl = FindControl(RetriveInWhatPanelis(c), Panel5)
    '    'Dim grayPen As New Pen(Color.Gray, 1)
    '    'grayPen.DashPattern = {1, 1}

    '    ''pnl.Refresh()

    '    'pnl.Refresh()
    '    'Dim g As Graphics = pnl.CreateGraphics

    '    'g.DrawLine(grayPen, 0, c.Location.Y - 1, Panel5.Width, c.Location.Y - 1)
    '    'TransparentDrawing1.Refresh()
    '    'Dim j As Graphics = TransparentDrawing1.CreateGraphics

    '    'j.FillRectangle(colorBrushDG, 7 - 5, 7 - 4, 1, c.Size.Height + 7) ' Left
    '    'j.FillRectangle(colorBrushDG, 7 + c.Size.Width + 4, 7 - 4, 1, c.Size.Height + 7) ' Right

    '    'j.FillRectangle(colorBrushDG, 7 - 4, 7 - 5, c.Size.Width + 7, 1) 'Up
    '    'j.FillRectangle(colorBrushDG, 7 - 4, 7 + c.Size.Height + 4, c.Size.Width + 7, 1) ' down

    '    'j.FillRectangle(colorBrushB, 7 - 7, 7 - 7, 5, 5) ' LEFT UP CORNER
    '    'j.FillRectangle(colorBrushB, 7 - 7, 7 + c.Size.Height + 2, 5, 5) ' LEFT DOWN CORNER

    '    'j.FillRectangle(colorBrushB, 7 + c.Size.Width + 2, 7 - 7, 5, 5) ' RIGHT UP CORNER
    '    'j.FillRectangle(colorBrushB, 7 + c.Size.Width + 2, 7 + c.Size.Height + 2, 5, 5) ' RIGHT DOWN CORNER 

    'End Sub

    ' Dim ControlPosi As New Point
    '================================================================================================================================ BUTTON
    'Private Sub AButton_MouseMove(sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)

    '    Dim Abutton As Button
    '    Abutton = CType(sender, Button)



    '    If panSE = True Or panNE = True Or panNW = True Or panSW = True Or noMoveHoriz = True Or noMoveVert = True Then

    '        If Not Abutton.Width > XofFormApp AndAlso Not Abutton.Height > YofFormApp Then : Label8.Text = Abutton.Width & "x" & Abutton.Height

    '        ElseIf Abutton.Width > XofFormApp AndAlso Abutton.Height > YofFormApp Then : Label8.Text = "XofFormAppxYofFormApp"
    '        ElseIf Abutton.Width > XofFormApp Then : Label8.Text = "XofFormAppx" & Abutton.Height
    '        ElseIf Abutton.Height > YofFormApp Then : Label8.Text = Abutton.Width & "xYofFormApp"
    '        End If



    '        Label8.Show()
    '        Label8.BringToFront()
    '    End If


    '    If panSE = True Then 'katw de3ia 
    '        Panel5.Cursor = Cursors.PanSE
    '        Abutton.Width = (e.X - _cursorStartPoint.X) + _currentControlStartSize.X
    '        Abutton.Height = (e.Y - _cursorStartPoint.Y) + _currentControlStartSize.Y

    '    ElseIf panNE = True Then ' panw de3ia
    '        Abutton.Width = (e.X - _cursorStartPoint.X) + _currentControlStartSize.X
    '        Abutton.Height -= (e.Y - _cursorStartPoint.Y)
    '        Abutton.Top += (e.Y - _cursorStartPoint.Y)


    '    ElseIf panNW = True Then 'panw aristera
    '        Abutton.Width -= (e.X - _cursorStartPoint.X)
    '        Abutton.Left += (e.X - _cursorStartPoint.X)
    '        Abutton.Height -= (e.Y - _cursorStartPoint.Y)
    '        Abutton.Top += (e.Y - _cursorStartPoint.Y)
    '    ElseIf panSW = True Then 'katw aristera
    '        Panel5.Cursor = Cursors.PanSW
    '        Abutton.Width -= (e.X - _cursorStartPoint.X)
    '        Abutton.Left += (e.X - _cursorStartPoint.X)
    '        Abutton.Height = (e.Y - _cursorStartPoint.Y) + _currentControlStartSize.Y


    '    ElseIf noMoveHoriz = True Then ' aristero-dexi
    '        If noMoveHorizRIGHT = True Then ' dexia
    '            Panel5.Cursor = Cursors.NoMoveHoriz

    '            Abutton.Width = e.X

    '        ElseIf noMoveHorizLEFT = True Then ' aristera 
    '            Panel5.Cursor = Cursors.NoMoveHoriz
    '            Abutton.Width -= (e.X - _cursorStartPoint.X)
    '            Abutton.Left += (e.X - _cursorStartPoint.X)
    '        End If

    '    ElseIf noMoveVert = True Then 'panw-katw
    '        If noMoveVertPanw = True Then
    '            Abutton.Height -= (e.Y - _cursorStartPoint.Y)
    '            Abutton.Top += (e.Y - _cursorStartPoint.Y)
    '        ElseIf noMoveVertKatw Then
    '            Abutton.Height = (e.Y - _currentControlStartSize.Y) + _currentControlStartSize.Y
    '        End If



    '    Else






    '        If Not e.Location.Y > Abutton.ClientSize.Height - 6 And Not e.Location.Y < 5 AndAlso e.Location.X > Abutton.ClientSize.Width - 6 And e.Location.X < Abutton.ClientSize.Width - 1 Or _
    '    Not e.Location.Y > Abutton.ClientSize.Height - 6 And Not e.Location.Y < 5 AndAlso e.Location.X < 5 And e.Location.X > 0 Then Abutton.Cursor = Cursors.NoMoveHoriz Else Abutton.Cursor = Cursors.NoMove2D

    '        If e.Location.Y < 5 And e.Location.X > Abutton.ClientSize.Width - 6 And e.Location.X < Abutton.ClientSize.Width - 1 Then Abutton.Cursor = Cursors.PanNE
    '        If e.Location.Y > Abutton.ClientSize.Height - 6 AndAlso e.Location.Y < Abutton.ClientSize.Height - 1 And e.Location.X > Abutton.ClientSize.Width - 6 And e.Location.X < Abutton.ClientSize.Width - 1 Then Abutton.Cursor = Cursors.PanSE

    '        If e.Location.Y < 5 And e.Location.X < 5 And e.Location.X > 1 Then Abutton.Cursor = Cursors.PanNW
    '        If e.Location.Y > Abutton.ClientSize.Height - 6 And e.Location.X < 5 And e.Location.X > 1 Then Abutton.Cursor = Cursors.PanSW

    '        If e.Location.Y < 5 And e.Location.X < Abutton.ClientSize.Width - 5 AndAlso e.Location.X > 4 Then Abutton.Cursor = Cursors.NoMoveVert
    '        If e.Location.Y > Abutton.ClientSize.Height - 5 And e.Location.X < Abutton.ClientSize.Width - 5 AndAlso e.Location.X > 4 Then Abutton.Cursor = Cursors.NoMoveVert



    '    End If







    '    If mousemoves = True Then
    '        mouseCanClick = False
    '        Abutton.BringToFront()
    '        Abutton.Location += e.Location - loca

    '        ' MsgBox(AButton.Text)
    '        ' mouseCanClick = False



    '    End If
    'End Sub
    'Private Sub AButton_MouseDown(Sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)


    '    Dim Abutton As Button
    '    Abutton = CType(Sender, Button)
    '    ' AButton.BringToFront()

    '    _cursorStartPoint = New Point(e.X, e.Y)
    '    _currentControlStartSize = Abutton.Size

    '    ' ChangeASpecificPropOfContr(PropertiesOfControl, "Location=", "Location={X=" & Abutton.Location.X & ",Y=" & Abutton.Location.Y & "}", Abutton.Name)

    '    ClickGenControlsPropert(Abutton.Name)

    '    ' Dim mousepot As Integer = e.Location.X
    '    '  If Not mousenowclick = mousepot Then
    '    Abutton.ContextMenuStrip = ContextMenuStrip1


    '    If e.Button = Windows.Forms.MouseButtons.Left Then
    '        If Abutton.Cursor = Cursors.PanSE Then 'katw de3ia 
    '            Abutton.Cursor = Cursors.PanSE
    '            panSE = True
    '            CursorDenDiaforetik = False
    '            '  _mouseDown = True

    '            ' Abutton.Size = e.Location
    '        ElseIf Abutton.Cursor = Cursors.PanNE Then ' panw de3ia
    '            Abutton.Cursor = Cursors.PanNE
    '            panNE = True
    '            CursorDenDiaforetik = False
    '            ' _mouseDown = True

    '        ElseIf Abutton.Cursor = Cursors.PanNW Then 'panw aristera
    '            Abutton.Cursor = Cursors.PanNW
    '            panNW = True
    '            CursorDenDiaforetik = False
    '            '  _mouseDown = True

    '        ElseIf Abutton.Cursor = Cursors.PanSW Then 'katw aristera
    '            Abutton.Cursor = Cursors.PanSW
    '            panSW = True
    '            CursorDenDiaforetik = False
    '            '  _mouseDown = True

    '        ElseIf Abutton.Cursor = Cursors.NoMoveHoriz Then ' aristero-dexi
    '            Abutton.Cursor = Cursors.NoMoveHoriz
    '            noMoveHoriz = True
    '            widthContr = Abutton.Width
    '            If Not e.Location.Y > Abutton.ClientSize.Height - 6 And Not e.Location.Y < 5 AndAlso e.Location.X > Abutton.ClientSize.Width - 6 And e.Location.X < Abutton.ClientSize.Width - 1 Then noMoveHorizRIGHT = True Else noMoveHorizLEFT = True
    '            CursorDenDiaforetik = False
    '            ' _mouseDown = True

    '        ElseIf Abutton.Cursor = Cursors.NoMoveVert Then 'panw-katw
    '            Abutton.Cursor = Cursors.NoMoveVert
    '            noMoveVert = True
    '            CursorDenDiaforetik = False
    '            ' _mouseDown = True
    '            If e.Location.Y < 5 And e.Location.X < Abutton.ClientSize.Width - 5 AndAlso e.Location.X > 4 Then noMoveVertPanw = True Else noMoveVertKatw = True
    '        Else
    '            ' _mouseDown = False

    '            'If ListView1.SelectedItems.Count = 0 Then
    '            CursorDenDiaforetik = True
    '            mCl += 1
    '            If mCl = 1 Then mousenowclick = e.Location.X
    '            mousemoves = True
    '            loca = e.Location

    '            ' Else
    '            ' AddControl(e)
    '            'End If
    '        End If


    '    End If
    '    ' End If
    '    'MyCombo.Items.Add(New ComboBoxIconItem("Bart", 0))

    'End Sub
    'Private Sub AButton_MouseUp(Sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    '    AButton = CType(Sender, Button)
    '    panSE = False
    '    panNE = False
    '    panNW = False
    '    panSW = False
    '    noMoveHoriz = False
    '    noMoveHorizLEFT = False
    '    noMoveHorizRIGHT = False
    '    noMoveVert = False
    '    noMoveVertPanw = False
    '    noMoveVertKatw = False

    '    mousemoves = False
    '    mouseCanClick = True

    '    'AButton.Location = New Point(AButton.Location.X + e.X, AButton.Location.Y)
    '    Panel5.Cursor = Cursors.NoMove2D

    '    ' _mouseDown = False
    '    ' AButton.Cursor = Panel5.Cursor
    '    If AButton.Width > XofFormApp Then AButton.Width = XofFormApp
    '    If AButton.Height > YofFormApp Then AButton.Height = YofFormApp

    '    If AButton.Location.X < 0 Then AButton.Location = New Point(0, AButton.Location.Y)
    '    If AButton.Location.Y < 0 Then AButton.Location = New Point(AButton.Location.X, 0)
    '    If AButton.Location.X + AButton.Size.Width > XofFormApp Then AButton.Location = New Point(XofFormApp - AButton.Size.Width, AButton.Location.Y)
    '    If AButton.Location.Y + AButton.Size.Height > YofFormApp Then AButton.Location = New Point(AButton.Location.X, YofFormApp - AButton.Size.Height)
    '    'AButton.SendToBack()

    '    If AButton.Width < 12 Then AButton.Width = 12
    '    If AButton.Height < 12 Then AButton.Height = 12

    '    '     MsgBox(PropertiesOfControl)
    '    ChangeASpecificPropOfContr(PropertiesOfControl, "Location=", "Location={" & AButton.Location.X & ";" & AButton.Location.Y & "}", AButton.Name)
    '    ChangeASpecificPropOfContr(PropertiesOfControl, "Size=", "Size=" & AButton.Size.ToString.Replace("Width=", Nothing).Replace(",", ";").Replace("Height=", Nothing).Replace(" ", Nothing), AButton.Name)
    '    'MsgBox(ProjectSaveString)
    '    ClickGenControlsPropert(AButton.Name)
    '    ' MsgBox(AButton.Name & vbNewLine & "UsrName: " & AButton.Tag)

    '    Label8.Hide()

    '    'GenControl.Location = New Point(AButton.Location)

    '    '_Location = AButton.Location 

    '    mousemoves = False
    '    ' If Not mousenowclick = mousepot Then
    '    '  mousemoves = False
    '    '    mCl = 0

    '    ' Else

    '    If e.Button = Windows.Forms.MouseButtons.Right Then
    '        rightclick(AButton, e)
    '    End If

    '    If CursorDenDiaforetik = True Then

    '        If Not e.Button = Windows.Forms.MouseButtons.Right Then click1 += 1
    '        If e.Button = Windows.Forms.MouseButtons.Left AndAlso click1 = 1 Then
    '            '    mousemoves = False
    '            ' MsgBox(Abutton.Text)
    '            ' Dim Abutton As Button


    '            AButton = CType(Sender, Button)
    '            GenControl = AButton

    '            Timer1.Start()
    '            mCl = 0

    '        End If
    '    End If
    'End Sub





    '================================================================================================================================== TIMER

    Private Sub ATimer_MouseMove(sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)

        Dim ATimer As PictureBox
        ATimer = CType(sender, PictureBox)

        If mousemoves = True Then
            mouseCanClick = False
            ATimer.BringToFront()
            ATimer.Location += e.Location - loca

            ' MsgBox(ATimer.Text)
            ' mouseCanClick = False

            ' TransparentDrawing1.Hide()

        End If
    End Sub
    Private Sub ATimer_MouseDown(Sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If Not pnl Is Nothing Then pnl.Refresh()
        'TransparentDrawing1.Hide()
        Dim ATimer As PictureBox
        ATimer = CType(Sender, PictureBox)
        ' ATimer.BringToFront()
        _cursorStartPoint = New Point(e.X, e.Y)
        _currentControlStartSize = ATimer.Size

        ATimer.ContextMenuStrip = ContextMenuStrip1
        ' Dim mousepot As Integer = e.Location.X
        '  If Not mousenowclick = mousepot Then

        ClickGenControlsPropert(ATimer.Name)

        If e.Button = Windows.Forms.MouseButtons.Left Then

            CursorDenDiaforetik = True
            mCl += 1
            If mCl = 1 Then mousenowclick = e.Location.X
            mousemoves = True
            loca = e.Location

            ' Else
            ' AddControl(e)
            'End If
        End If

        ' End If
        'MyCombo.Items.Add(New ComboBoxIconItem("Bart", 0))

    End Sub
    Private Sub ATimer_MouseUp(Sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        ATimer = CType(Sender, PictureBox)
        panSE = False
        panNE = False
        panNW = False
        panSW = False
        noMoveHoriz = False
        noMoveHorizLEFT = False
        noMoveHorizRIGHT = False
        noMoveVert = False
        noMoveVertPanw = False
        noMoveVertKatw = False

        mousemoves = False
        mouseCanClick = True
        'ATimer.Location = New Point(ATimer.Location.X + e.X, ATimer.Location.Y)
        Panel5.Cursor = Cursors.NoMove2D

        ' _mouseDown = False
        ' ATimer.Cursor = Panel5.Cursor
        If ATimer.Width > XofFormApp Then ATimer.Width = XofFormApp
        If ATimer.Height > YofFormApp Then ATimer.Height = YofFormApp

        If ATimer.Location.X < 0 Then ATimer.Location = New Point(0, ATimer.Location.Y)
        If ATimer.Location.Y < 0 Then ATimer.Location = New Point(ATimer.Location.X, 0)
        If ATimer.Location.X + ATimer.Size.Width > XofFormApp Then ATimer.Location = New Point(XofFormApp - ATimer.Size.Width, ATimer.Location.Y)
        If ATimer.Location.Y + ATimer.Size.Height > YofFormApp Then ATimer.Location = New Point(ATimer.Location.X, YofFormApp - ATimer.Size.Height)
        'ATimer.SendToBack()

        If ATimer.Width < 12 Then ATimer.Width = 12
        If ATimer.Height < 12 Then ATimer.Height = 12

        ChangeASpecificPropOfContr(PropertiesOfControl, "Location=", "Location={" & ATimer.Location.X & ";" & ATimer.Location.Y & "}", ATimer.Name)
        'ChangeASpecificPropOfContr(PropertiesOfControl, "Size=", "Size=" & ATimer.Size.ToString.Replace("Width=", Nothing).Replace(",", ";").Replace("Height=", Nothing).Replace(" ",Nothing), ATimer.Name)

        ClickGenControlsPropert(ATimer.Name)

        Label8.Hide()

        mousemoves = False
        ' If Not mousenowclick = mousepot Then
        '  mousemoves = False
        '    mCl = 0

        ' Else
        '  TransparentDrawing1.Hide()
        If e.Button = Windows.Forms.MouseButtons.Right Then
            rightclick(ATimer, e)
        End If

        If CursorDenDiaforetik = True Then

            If Not e.Button = Windows.Forms.MouseButtons.Right Then click1 += 1
            If e.Button = Windows.Forms.MouseButtons.Left AndAlso click1 = 1 Then
                '    mousemoves = False
                ' MsgBox(ATimer.Text)
                ' Dim ATimer As Button


                ATimer = CType(Sender, PictureBox)
                GenControl = ATimer
                Timer1.Start()
                mCl = 0

            End If
        End If
    End Sub






    Public propertiesOfForm As String = Nothing
    Public Sub formisProp()
        If Not Label4.Text = "" Then

            PropertyGrid1.SelectedObject = New clsForm

            ' Combobox1.Items.Add(New ComboBoxIconItem(Combobox3.Text.Replace(".vb4a", Nothing), 16))
            propertiesOfForm = Nothing
            Dim formFinded As Boolean = False
            For Each line In ProjectSaveString.Split(vbNewLine)
                line = line.Replace(Chr(10), "")
                If line = "++Form++" Then formFinded = True Else If line = "--Form--" Then formFinded = Nothing : Exit Sub

                If formFinded = True Then
                    If line.StartsWith("BackgroundImage=") Then : If line.Replace("BackgroundImage=", Nothing) = Nothing Then : clsForm._BackgroundImage = Nothing : Panel5.BackgroundImage = Nothing : Else : clsForm._BackgroundImage = CurrentHardDriver & line.Replace("BackgroundImage=", Nothing).Remove(0, 3) : Panel5.BackgroundImage = Image.FromFile(CurrentHardDriver & line.Replace("BackgroundImage=", Nothing).Remove(0, 3)) : End If 'Form1.CurrentHardDriver & _BackgroundImage.Remove(0, 3)
                    ElseIf line.StartsWith("Layout=") Then : clsForm._Layout = line.Replace("Layout=", Nothing)
                    ElseIf line.StartsWith("Scrollable=") Then : clsForm._Scrollable = line.Replace("Scrollable=", Nothing)
                    ElseIf line.StartsWith("Title=") Then : clsForm._Title = line.Replace("Title=", Nothing)
                    ElseIf line.StartsWith("BackgroundColor=") Then : clsForm._BackgroundColor = ColourFromData(line.Replace("BackgroundColor=", Nothing)) : Panel5.BackColor = ColourFromData(line.Replace("BackgroundColor=", Nothing))

                    End If
                    propertiesOfForm &= line & vbNewLine
                End If
            Next

        End If
    End Sub



    Public Shared Function IsListViewSelectes()
        Dim a As Boolean = False

        For i As Integer = 0 To Form1.ListView1.Items.Count - 1 Step 1
            If Form1.ListView1.Items(i).Selected = True Then a = True
        Next

        Return a
    End Function



    Private Function listbool(NameOfControl As String, ForSearch As String)
        Dim a As Boolean = True


        For Each Line As String In ForSearch.Split(vbNewLine)
            Line = Line.Replace(vbLf, "")

            If Line = NameOfControl Then Return a = False

        Next
        Return a
    End Function
    Private Function CheckIfaPropOfControlIs(prop As String)
        For Each Line As String In PropertiesOfControl.Split(vbNewLine)
            Line = Line.Replace(vbLf, "")
            If Line = prop Or Line.StartsWith(prop) Then Return Line.Replace(prop, "")
        Next
        Return Nothing
    End Function
    Private Function CheckIfaPropOfControlIs2(name As String, prop As String)
        Dim hl As Boolean = False
        For Each Line As String In ProjectSaveString.Split(vbNewLine)
            Line = Line.Replace(vbLf, "")
            If Line = "<" & name & ">" Then hl = True
            If hl = True Then If Line = prop Or Line.StartsWith(prop) Then Return Line.Replace(prop, "")
        Next
        Return Nothing
    End Function

    Private AButton As New Button
    Private Canvas As New PictureBox
    Private ACheckBox As New CheckBox
    Private ALabel As New Label
    Private AListBox As New PictureBox ' i had a strange problem when i this was Listbox
    Private APanel As New Panel
    Private PasswordTextBox As New TextBox  ' i think it would be better if all where picturebox and just i had set them images backround and text 
    Private APictureBox As New PictureBox
    Private AProgressBar As New ProgressBar
    Private ARadioButton As New RadioButton
    Private ATextBox As New TextBox
    Private WithEvents ATimer As New PictureBox
    Private AComboBox As New ComboBox
    Private VB4AWeb As New PictureBox

    Dim ButtonString As String = String.Empty
    Dim CanvasString As String = String.Empty
    Dim CheckBoxString As String = String.Empty
    Dim LabelString As String = String.Empty
    Dim ListBoxString As String = String.Empty
    Dim PanelString As String = String.Empty                    ' mporousa na ta kanv se ena alla fovomoun mhn argei meta kai kolei sto search gia 
    Dim PasswordTextBoxString As String = String.Empty
    Dim PictureBoxString As String = String.Empty
    Dim ProgressBarString As String = String.Empty
    Dim RadioButtonString As String = String.Empty
    Dim TextBoxString As String = String.Empty
    Dim TimerString As String = String.Empty
    Dim ComboBoxString As String = String.Empty
    Dim VB4AWebString As String = String.Empty

    Public ba1 As Integer = 0
    Public ba2 As Integer = 0
    Public ba3 As Integer = 0
    Public ba4 As Integer = 0
    Public ba5 As Integer = 0
    Public ba6 As Integer = 0
    Public ba7 As Integer = 0
    Public ba8 As Integer = 0
    Public ba9 As Integer = 0
    Public ba10 As Integer = 0
    Public ba11 As Integer = 0
    Public ba12 As Integer = 0
    Public ba13 As Integer = 0
    Public ba14 As Integer = 0


    Public cntrafe1 As Integer = 0
    Public cntrafe2 As Integer = 0
    Public cntrafe3 As Integer = 0
    Public cntrafe4 As Integer = 0
    Public cntrafe5 As Integer = 0
    Public cntrafe6 As Integer = 0
    Public cntrafe7 As Integer = 0
    Public cntrafe8 As Integer = 0
    Public cntrafe9 As Integer = 0
    Public cntrafe10 As Integer = 0
    Public cntrafe11 As Integer = 0
    Public cntrafe12 As Integer = 0
    Public cntrafe13 As Integer = 0
    Public cntrafe14 As Integer = 0

    Public ProjectSaveString As String = Nothing

    Public idControl As Integer

    Public Sub SaveProInString(projs As String, append As Boolean)
        If append = True Then

            ProjectSaveString &= projs

        Else


        End If
    End Sub
    Public Sub SaveProj(projtext As String, append As Boolean, nfile As String)
        If File.Exists(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & nfile) Then
tryagain1:  Try
                My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & nfile, projtext, append, Encoding.GetEncoding("gb2312")) ' Encoding.GetEncoding("gb2312")
            Catch ex As Exception
                '  MsgBox(ex.ToString)
                GoTo tryagain1
            End Try

            If append = True Then ProjectSaveString &= projtext
        Else
            Using New Centered_MessageBox(Me)
                MsgBox("File Doesn't Exist." & vbNewLine & CurrentHardDriver & "VB4Android\Projects\" &
                      FileCreatedName & "\" & nfile, MsgBoxStyle.Exclamation)
            End Using
        End If
    End Sub

    Public Sub SaveCodeProject(projtxt As String, nfile As String, append As Boolean, enc As Encoding)
        If Not projtxt = Nothing Then


tryagain1:  Try


                My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & nfile, projtxt, append, enc)
            Catch ex As Exception

                MsgBox("Click ""OK"" There Was A Problem :PP Our Program Will Retry" & vbNewLine & "If This Message Will Apear Again Then Copy" & vbNewLine & " The Above Error And Send It to us :" & vbNewLine & vbNewLine & ex.ToString)
                GoTo tryagain1
            End Try
        End If
    End Sub



    Private Sub TextcoderWordsReplace() ' it needs a fix because it makes also comments ....
        Dim lnNum As Integer = 0
        Dim lines() As String = Textbox3.Lines.ToArray
        For Each Line As String In Textbox3.Text.Split(vbNewLine)
            Line = Line.Replace(vbLf, "")


            'Dim b As String() = 
            Dim c As String = String.Empty
            Dim h As String = String.Empty
            Dim i As Integer = 0

            For Each sbs As String In Line.Split(""""c)
                h = sbs
                For Each item As String In cheackstrings ' Dim cheackstrings() As String = {"As", "If", "In", "Is" , ...}
                    h = If(i Mod 2 = 0, h.Replace(" " & item.ToLower & ",", " " & item & ","), (Convert.ToString("""") & sbs) + """")
                    h = If(i Mod 2 = 0, h.Replace(" " & item.ToLower & " ", " " & item & " "), (Convert.ToString("""") & sbs) + """")

                    ' If Not Regex.Match(h, "([A-Za-z0-9]+)" & item.ToLower & " ", RegexOptions.IgnoreCase).Value = Nothing Then Console.WriteLine(Regex.Match(h, "([A-Za-z0-9]+)" & item.ToLower & " ", RegexOptions.IgnoreCase).Value)
                    If Not Regex.Match(h, "[A-Za-z0-9]" & item.ToLower & " ", RegexOptions.IgnoreCase).Success Then

                        h = If(i Mod 2 = 0, h.Replace(item.ToLower & " ", item & " "), (Convert.ToString("""") & sbs) + """")
                        h = If(i Mod 2 = 0, h.Replace(" " & item.ToLower & ",", " " & item & ","), (Convert.ToString("""") & sbs) + """")
                    End If
                    If Not Regex.Match(h, " " & item.ToLower & "([A-Za-z0-9])", RegexOptions.IgnoreCase).Success Then ' i have to remove ignore case i think
                        h = If(i Mod 2 = 0, h.Replace(" " & item.ToLower, " " & item), (Convert.ToString("""") & sbs) + """")
                    End If


                Next
                c += h
                'for the every odd value of i "word" is within single quotes
                i += 1
            Next

            lines(lnNum) = c


            lnNum += 1
        Next
        Textbox3.Text = Join(lines, vbCrLf)
        'Textbox3.GoHome()
    End Sub
    Private Sub SaveToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem1.Click ' SAVE
        If Not Combobox3.Items.Count = 0 Then
            TextBox2.Select()
            SendKeys.Send("{ENTER}")
            Dim i As Integer = Textbox3.Selection.Start.iLine
            TextcoderWordsReplace()
            Textbox3.Navigate(i)
            Dim selitcom As String = Combobox3.SelectedItem.ToString
            Combobox3.Text = Nothing
            Combobox3.Text = selitcom
            selitcom = Nothing
            Timer2.Enabled = True

        Else
            Using New Centered_MessageBox(Me)
                MsgBox("You Must Create A Project First, In Order " & vbNewLine & " To Be Able To Save It. xD", MsgBoxStyle.Information)
            End Using
        End If

    End Sub
    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click ' SAVE ALL
        If Not Combobox3.Items.Count = 0 Then
            If Not Combobox3.Items.Count = 1 Then
                TextBox2.Select()
                SendKeys.Send("{ENTER}")
                Dim i As Integer = Textbox3.Selection.Start.iLine

                Textbox3.ClearUndo()

                If Not RichTextBox1.Visible = True Then Button10.PerformClick()
                Dim nowCom3Item As String = Combobox3.SelectedItem.ToString
                For item = 0 To Combobox3.Items.Count - 1
                    TextcoderWordsReplace()
                    Combobox3.Text = Combobox3.Items.Item(item).ToString

                Next

                Textbox3.Navigate(i)

                Combobox3.Text = nowCom3Item
                nowCom3Item = Nothing
                'Timer2.Enabled = True
                Textbox3.ClearUndo()
            Else
                SaveToolStripMenuItem1.PerformClick()
            End If
        Else
            Using New Centered_MessageBox(Me)
                MsgBox("You Must Create A Project First, In Order To Be Able To Save It. xD", MsgBoxStyle.Information)
            End Using
        End If
    End Sub

    Public BeforeCombo3 As String = "" '"form1.vb4a"
    Public firsttimechange As Boolean = True
    Dim Is_Any_Cont_Delted As Boolean = False






    Private Sub Combobox3_SelectedValueChanged(sender As Object, e As EventArgs) Handles Combobox3.SelectedValueChanged
        If firsttimechange = True AndAlso Not Combobox3.Text = Nothing Then
            firsttimechange = False
            Exit Sub


        ElseIf Panel5.Controls.Count = 1 AndAlso Not Combobox3.Text = Nothing AndAlso Is_Any_Cont_Delted = False And Not Combobox3.Text = Nothing Then ' LoL idk why the fuck i have this here but i am not removing it because it works fine and i am scared xD
            ' TransparentDrawing1.Hide()
            If Not FormDeleted = True Then SaveProj(ProjectSaveString, False, BeforeCombo3)

            If Not FormDeleted = True Then SaveCodeProject(Textbox3.Text, BeforeCombo3.Replace(".vb4a", ".vb4acode"), False, Encoding.GetEncoding("gb2312")) 'Combobox3.Text


            ProjectSaveString = My.Computer.FileSystem.ReadAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Combobox3.Text, Encoding.GetEncoding("gb2312"))

            BeforeCombo3 = Combobox3.Text

            Combobox1.Items.Clear()

            PropertyGrid1.SelectedObject = Nothing
            Combobox1.Items.Add(New ComboBoxIconItem(Combobox3.Text.Replace(".vb4a", Nothing), 16))
            ComboboxImage1.Items.Add(New ComboBoxIconItem(Combobox3.Text.Replace(".vb4a", Nothing), 16))
            Combobox1.Text = Combobox3.Text.Replace(".vb4a", Nothing)
            formisProp()

            Textbox3.Text = My.Computer.FileSystem.ReadAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Combobox3.Text.Replace(".vb4a", ".vb4acode"), Encoding.GetEncoding("gb2312"))


        ElseIf Not Panel5.Controls.Count = 1 AndAlso Not Combobox3.Text = Nothing Or Is_Any_Cont_Delted = True AndAlso Not Combobox3.Text = Nothing Then
            TransparentDrawing1.Hide()

            If Not FormDeleted = True Then SaveProj(ProjectSaveString, False, BeforeCombo3)

            If Not FormDeleted = True Then SaveCodeProject(Textbox3.Text, BeforeCombo3.Replace(".vb4a", ".vb4acode"), False, Encoding.GetEncoding("gb2312")) 'Combobox3.Text

            ConsoleDouble += 1
            If Not FormDeleted = True Then RichTextBox1.AppendText("[" & ConsoleDouble & "] " & "File '" & BeforeCombo3.Replace(".vb4a", Nothing) & "' Saved At " & DateTime.Now.ToString & "." & vbNewLine)
            RichTextBox1.ScrollToCaret()

            '  cntrafe1 = 0
            ' cntrafe2 = 0
            '   cntrafe3 = 0
            '   cntrafe4 = 0
            '  cntrafe5 = 0
            '  cntrafe6 = 0
            '  cntrafe7 = 0
            ' cntrafe8 = 0
            ' cntrafe9 = 0
            ' cntrafe10 = 0
            ' cntrafe11 = 0
            ' cntrafe12 = 0
            ' cntrafe13 = 0
            ' cntrafe14 = 0

            'AndAlso File.Exists(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & BeforeCombo3.Replace(".vb4a", ".DelConts"))
            If Not FormDeleted = True Then : For Each Line As String In My.Computer.FileSystem.ReadAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & BeforeCombo3.Replace(".vb4a", ".DelConts")).Split(vbNewLine)
                    Line = Line.Replace(Chr(10), "") ' ποτε μην βαζει κανεις "Nothing" σε "Replace" xD γτ δεν το κανει τιποτε χΔ .... αυτη την βλακια ειχα κανει εγω και ..... xD



                    If Line.StartsWith("AButton") Then
                        cntrafe1 += 1
                    ElseIf Line.StartsWith("Canvas") Then
                        cntrafe2 += 1
                    ElseIf Line.StartsWith("ACheckBox") Then
                        cntrafe3 += 1
                    ElseIf Line.StartsWith("ALabel") Then
                        cntrafe4 += 1
                    ElseIf Line.StartsWith("AListview") Then
                        cntrafe5 += 1
                    ElseIf Line.StartsWith("APanel") Then
                        cntrafe6 += 1
                    ElseIf Line.StartsWith("PasswordTextBox") Then
                        cntrafe7 += 1
                    ElseIf Line.StartsWith("APictureBox") Then
                        cntrafe8 += 1
                    ElseIf Line.StartsWith("AProgressBar") Then
                        cntrafe9 += 1
                    ElseIf Line.StartsWith("ARadioButton") Then
                        cntrafe10 += 1
                    ElseIf Line.StartsWith("ATextBox") Then
                        cntrafe11 += 1
                    ElseIf Line.StartsWith("ATimer") Then
                        cntrafe12 += 1
                    ElseIf Line.StartsWith("AComboBox") Then
                        cntrafe13 += 1
                    ElseIf Line.StartsWith("VB4AWeb") Then
                        cntrafe14 += 1
                    End If


                Next : Else : FormDeleted = False : End If

            Dim panelsss As String = Nothing

            If Not ProjectSaveString = Nothing Then


                For Each line In ProjectSaveString.Split(vbNewLine)
                    Dim contrstr As String = line.Replace("<", "")
                    contrstr = contrstr.Replace(">", "")
                    contrstr = contrstr.Replace(Chr(10), "")







                    If line.StartsWith("<AButton") AndAlso Not line.StartsWith("</AButton") Or line.StartsWith(Chr(10) & "<AButton") AndAlso Not line.StartsWith(Chr(10) & "</AButton") Then : cntrafe1 += 1
                    ElseIf line.StartsWith("<Canvas") AndAlso Not line.StartsWith("</Canvas") Or line.StartsWith(Chr(10) & "<Canvas") AndAlso Not line.StartsWith(Chr(10) & "</Canvas") Then : cntrafe2 += 1
                    ElseIf line.StartsWith("<ACheckBox") AndAlso Not line.StartsWith("</ACheckBox") Or line.StartsWith(Chr(10) & "<ACheckBox") AndAlso Not line.StartsWith(Chr(10) & "</ACheckBox") Then : cntrafe3 += 1
                    ElseIf line.StartsWith("<ALabel") AndAlso Not line.StartsWith("</ALabel") Or line.StartsWith(Chr(10) & "<ALabel") AndAlso Not line.StartsWith(Chr(10) & "</ALabel") Then : cntrafe4 += 1
                    ElseIf line.StartsWith("<AListview") AndAlso Not line.StartsWith("</AListview") Or line.StartsWith(Chr(10) & "<AListview") AndAlso Not line.StartsWith(Chr(10) & "</AListview") Then : cntrafe5 += 1
                    ElseIf line.StartsWith("<APanel") AndAlso Not line.StartsWith("</APanel") Or line.StartsWith(Chr(10) & "<APanel") AndAlso Not line.StartsWith(Chr(10) & "</APanel") Then : cntrafe6 += 1 : panelsss &= contrstr & vbNewLine
                    ElseIf line.StartsWith("<PasswordTextBox") AndAlso Not line.StartsWith("</PasswordTextBox") Or line.StartsWith(Chr(10) & "<PasswordTextBox") AndAlso Not line.StartsWith(Chr(10) & "</PasswordTextBox") Then : cntrafe7 += 1
                    ElseIf line.StartsWith("<APictureBox") AndAlso Not line.StartsWith("</APictureBox") Or line.StartsWith(Chr(10) & "<APictureBox") AndAlso Not line.StartsWith(Chr(10) & "</APictureBox") Then : cntrafe8 += 1
                    ElseIf line.StartsWith("<AProgressBar") AndAlso Not line.StartsWith("</AProgressBar") Or line.StartsWith(Chr(10) & "<AProgressBar") AndAlso Not line.StartsWith(Chr(10) & "</AProgressBar") Then : cntrafe9 += 1
                    ElseIf line.StartsWith("<ARadioButton") AndAlso Not line.StartsWith("</ARadioButton") Or line.StartsWith(Chr(10) & "<ARadioButton") AndAlso Not line.StartsWith(Chr(10) & "</ARadioButton") Then : cntrafe10 += 1
                    ElseIf line.StartsWith("<ATextBox") AndAlso Not line.StartsWith("</ATextBox") Or line.StartsWith(Chr(10) & "<ATextBox") AndAlso Not line.StartsWith(Chr(10) & "</ATextBox") Then : cntrafe11 += 1
                    ElseIf line.StartsWith("<ATimer") AndAlso Not line.StartsWith("</ATimer") Or line.StartsWith(Chr(10) & "<ATimer") AndAlso Not line.StartsWith(Chr(10) & "</ATimer") Then : cntrafe12 += 1
                    ElseIf line.StartsWith("<AComboBox") AndAlso Not line.StartsWith("</AComboBox") Or line.StartsWith(Chr(10) & "<AComboBox") AndAlso Not line.StartsWith(Chr(10) & "</AComboBox") Then : cntrafe13 += 1
                    ElseIf line.StartsWith("<VB4AWeb") AndAlso Not line.StartsWith("</VB4AWeb") Or line.StartsWith(Chr(10) & "<VB4AWeb") AndAlso Not line.StartsWith(Chr(10) & "</VB4AWeb") Then : cntrafe14 += 1
                    Else
                    End If


                    If Panel5.Controls.Contains(Panel5.Controls(contrstr)) Then
                        If line.StartsWith("<") AndAlso Not line.StartsWith("</") Or line.StartsWith(Chr(10) & "<") AndAlso Not line.StartsWith(Chr(10) & "</") Then Panel5.Controls(contrstr).Hide()
                    ElseIf Not panelsss = Nothing Then
                        For Each lines In panelsss.Split(vbNewLine)
                            Dim contrstrPANEL As String = lines.Replace(Chr(10), "")
                            '  Try
                            If Not contrstrPANEL = Nothing Then
                                Try
                                    If Not line = Nothing Then


                                        If line.StartsWith("<") AndAlso Not line.StartsWith("</") Or line.StartsWith(Chr(10) & "<") AndAlso Not line.StartsWith(Chr(10) & "</") Then DirectCast(FindControl(RetriveInWhatPanelis(DirectCast(FindControl(contrstr, Me), Control)), Me), Panel).Controls(contrstr).Hide()

                                    End If

                                Catch ex As Exception
                                    MsgBox(ex.ToString)
                                End Try
                            End If
                        Next
                    End If


                Next
            End If
            ' ta afer prepei na einai oso ta controls sto arxio ara twra prepei na to kanw na ta diaazei
            Combobox1.Items.Clear()

            ComboboxImage1.Items.Clear()
            ComboboxImage2.Items.Clear()

            ProjectSaveString = My.Computer.FileSystem.ReadAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Combobox3.Text, Encoding.GetEncoding("gb2312"))

            BeforeCombo3 = Combobox3.Text

            panelsss = Nothing

            Try




                For Each line In ProjectSaveString.Split(vbNewLine)
                    Dim contrstr As String = line.Replace("<", "")
                    contrstr = contrstr.Replace(">", "")
                    contrstr = contrstr.Replace(Chr(10), "")
                    ' If line.StartsWith("<") AndAlso Not line.StartsWith("</") Or line.StartsWith(Chr(10) & "<") AndAlso Not line.StartsWith(Chr(10) & "</") Then Panel5.Controls(contrstr).Show()
                    If line.StartsWith("<AButton") AndAlso Not line.StartsWith("</AButton") Or line.StartsWith(Chr(10) & "<AButton") AndAlso Not line.StartsWith(Chr(10) & "</AButton") Then : cntrafe1 -= 1
                    ElseIf line.StartsWith("<Canvas") AndAlso Not line.StartsWith("</Canvas") Or line.StartsWith(Chr(10) & "<Canvas") AndAlso Not line.StartsWith(Chr(10) & "</Canvas") Then : cntrafe2 -= 1
                    ElseIf line.StartsWith("<ACheckBox") AndAlso Not line.StartsWith("</ACheckBox") Or line.StartsWith(Chr(10) & "<ACheckBox") AndAlso Not line.StartsWith(Chr(10) & "</ACheckBox") Then : cntrafe3 -= 1
                    ElseIf line.StartsWith("<ALabel") AndAlso Not line.StartsWith("</ALabel") Or line.StartsWith(Chr(10) & "<ALabel") AndAlso Not line.StartsWith(Chr(10) & "</ALabel") Then : cntrafe4 -= 1
                    ElseIf line.StartsWith("<AListview") AndAlso Not line.StartsWith("</AListview") Or line.StartsWith(Chr(10) & "<AListview") AndAlso Not line.StartsWith(Chr(10) & "</AListview") Then : cntrafe5 -= 1
                    ElseIf line.StartsWith("<APanel") AndAlso Not line.StartsWith("</APanel") Or line.StartsWith(Chr(10) & "<APanel") AndAlso Not line.StartsWith(Chr(10) & "</APanel") Then : cntrafe6 -= 1 : panelsss &= contrstr & vbNewLine
                    ElseIf line.StartsWith("<PasswordTextBox") AndAlso Not line.StartsWith("</PasswordTextBox") Or line.StartsWith(Chr(10) & "<PasswordTextBox") AndAlso Not line.StartsWith(Chr(10) & "</PasswordTextBox") Then : cntrafe7 -= 1
                    ElseIf line.StartsWith("<APictureBox") AndAlso Not line.StartsWith("</APictureBox") Or line.StartsWith(Chr(10) & "<APictureBox") AndAlso Not line.StartsWith(Chr(10) & "</APictureBox") Then : cntrafe8 -= 1
                    ElseIf line.StartsWith("<AProgressBar") AndAlso Not line.StartsWith("</AProgressBar") Or line.StartsWith(Chr(10) & "<AProgressBar") AndAlso Not line.StartsWith(Chr(10) & "</AProgressBar") Then : cntrafe9 -= 1
                    ElseIf line.StartsWith("<ARadioButton") AndAlso Not line.StartsWith("</ARadioButton") Or line.StartsWith(Chr(10) & "<ARadioButton") AndAlso Not line.StartsWith(Chr(10) & "</ARadioButton") Then : cntrafe10 -= 1
                    ElseIf line.StartsWith("<ATextBox") AndAlso Not line.StartsWith("</ATextBox") Or line.StartsWith(Chr(10) & "<ATextBox") AndAlso Not line.StartsWith(Chr(10) & "</ATextBox") Then : cntrafe11 -= 1
                    ElseIf line.StartsWith("<ATimer") AndAlso Not line.StartsWith("</ATimer") Or line.StartsWith(Chr(10) & "<ATimer") AndAlso Not line.StartsWith(Chr(10) & "</ATimer") Then : cntrafe12 -= 1
                    ElseIf line.StartsWith("<AComboBox") AndAlso Not line.StartsWith("</AComboBox") Or line.StartsWith(Chr(10) & "<AComboBox") AndAlso Not line.StartsWith(Chr(10) & "</AComboBox") Then : cntrafe13 -= 1
                    ElseIf line.StartsWith("<VB4AWeb") AndAlso Not line.StartsWith("</VB4AWeb") Or line.StartsWith(Chr(10) & "<VB4AWeb") AndAlso Not line.StartsWith(Chr(10) & "</VB4AWeb") Then : cntrafe14 -= 1
                    Else
                    End If



                    If Panel5.Controls.Contains(Panel5.Controls(contrstr)) Then

                        If line.StartsWith("<") AndAlso Not line.StartsWith("</") Or line.StartsWith(Chr(10) & "<") AndAlso Not line.StartsWith(Chr(10) & "</") Then Panel5.Controls(contrstr).Show() : Panel5.Controls(contrstr).BringToFront()
                        If contrstr.StartsWith("AButton") Then : Combobox1.Items.Add(New ComboBoxIconItem(Panel5.Controls(contrstr).Tag, 0)) : ComboboxImage1.Items.Add(New ComboBoxIconItem(Panel5.Controls(contrstr).Tag, 0))
                        ElseIf contrstr.StartsWith("Canvas") Then : Combobox1.Items.Add(New ComboBoxIconItem(Panel5.Controls(contrstr).Tag, 1)) : ComboboxImage1.Items.Add(New ComboBoxIconItem(Panel5.Controls(contrstr).Tag, 1))
                        ElseIf contrstr.StartsWith("ACheckBox") Then : Combobox1.Items.Add(New ComboBoxIconItem(Panel5.Controls(contrstr).Tag, 2)) : ComboboxImage1.Items.Add(New ComboBoxIconItem(Panel5.Controls(contrstr).Tag, 2))
                        ElseIf contrstr.StartsWith("ALabel") Then : Combobox1.Items.Add(New ComboBoxIconItem(Panel5.Controls(contrstr).Tag, 4)) : ComboboxImage1.Items.Add(New ComboBoxIconItem(Panel5.Controls(contrstr).Tag, 4))
                        ElseIf contrstr.StartsWith("AListview") Then : Combobox1.Items.Add(New ComboBoxIconItem(Panel5.Controls(contrstr).Tag, 5)) : ComboboxImage1.Items.Add(New ComboBoxIconItem(Panel5.Controls(contrstr).Tag, 5))
                        ElseIf contrstr.StartsWith("APanel") Then : Combobox1.Items.Add(New ComboBoxIconItem(Panel5.Controls(contrstr).Tag, 6)) : ComboboxImage1.Items.Add(New ComboBoxIconItem(Panel5.Controls(contrstr).Tag, 6))
                        ElseIf contrstr.StartsWith("PasswordTextBox") Then : Combobox1.Items.Add(New ComboBoxIconItem(Panel5.Controls(contrstr).Tag, 7)) : ComboboxImage1.Items.Add(New ComboBoxIconItem(Panel5.Controls(contrstr).Tag, 7))
                        ElseIf contrstr.StartsWith("APictureBox") Then : Combobox1.Items.Add(New ComboBoxIconItem(Panel5.Controls(contrstr).Tag, 8)) : ComboboxImage1.Items.Add(New ComboBoxIconItem(Panel5.Controls(contrstr).Tag, 8))
                        ElseIf contrstr.StartsWith("AProgressBar") Then : Combobox1.Items.Add(New ComboBoxIconItem(Panel5.Controls(contrstr).Tag, 9)) : ComboboxImage1.Items.Add(New ComboBoxIconItem(Panel5.Controls(contrstr).Tag, 9))
                        ElseIf contrstr.StartsWith("ARadioButton") Then : Combobox1.Items.Add(New ComboBoxIconItem(Panel5.Controls(contrstr).Tag, 10)) : ComboboxImage1.Items.Add(New ComboBoxIconItem(Panel5.Controls(contrstr).Tag, 10))
                        ElseIf contrstr.StartsWith("ATextBox") Then : Combobox1.Items.Add(New ComboBoxIconItem(Panel5.Controls(contrstr).Tag, 12)) : ComboboxImage1.Items.Add(New ComboBoxIconItem(Panel5.Controls(contrstr).Tag, 12))
                        ElseIf contrstr.StartsWith("ATimer") Then : Combobox1.Items.Add(New ComboBoxIconItem(Panel5.Controls(contrstr).Tag, 13)) : ComboboxImage1.Items.Add(New ComboBoxIconItem(Panel5.Controls(contrstr).Tag, 13))
                        ElseIf contrstr.StartsWith("AComboBox") Then : Combobox1.Items.Add(New ComboBoxIconItem(Panel5.Controls(contrstr).Tag, 14)) : ComboboxImage1.Items.Add(New ComboBoxIconItem(Panel5.Controls(contrstr).Tag, 14))
                        ElseIf contrstr.StartsWith("VB4AWeb") Then : Combobox1.Items.Add(New ComboBoxIconItem(Panel5.Controls(contrstr).Tag, 15)) : ComboboxImage1.Items.Add(New ComboBoxIconItem(Panel5.Controls(contrstr).Tag, 15))

                        End If


                    ElseIf Not panelsss = Nothing Then
                        'MsgBox(ex.ToString)


                        Dim ControlTag As String = Nothing

                        If line.StartsWith("<") AndAlso Not line.StartsWith("</") Or line.StartsWith(Chr(10) & "<") AndAlso Not line.StartsWith(Chr(10) & "</") Then : DirectCast(FindControl(RetriveInWhatPanelis(DirectCast(FindControl(contrstr, Me), Control)), Me), Panel).Controls(contrstr).Show() :: Panel5.Controls(contrstr).BringToFront() : ControlTag = DirectCast(FindControl(RetriveInWhatPanelis(DirectCast(FindControl(contrstr, Me), Control)), Me), Panel).Controls(contrstr).Tag.ToString : End If

                        If contrstr.StartsWith("AButton") Then : Combobox1.Items.Add(New ComboBoxIconItem(ControlTag, 0)) : ComboboxImage1.Items.Add(New ComboBoxIconItem(ControlTag, 0))
                        ElseIf contrstr.StartsWith("Canvas") Then : Combobox1.Items.Add(New ComboBoxIconItem(ControlTag, 1)) : ComboboxImage1.Items.Add(New ComboBoxIconItem(ControlTag, 1))
                        ElseIf contrstr.StartsWith("ACheckBox") Then : Combobox1.Items.Add(New ComboBoxIconItem(ControlTag, 2)) : ComboboxImage1.Items.Add(New ComboBoxIconItem(ControlTag, 2))
                        ElseIf contrstr.StartsWith("ALabel") Then : Combobox1.Items.Add(New ComboBoxIconItem(ControlTag, 4)) : ComboboxImage1.Items.Add(New ComboBoxIconItem(ControlTag, 4))
                        ElseIf contrstr.StartsWith("AListview") Then : Combobox1.Items.Add(New ComboBoxIconItem(ControlTag, 5)) : ComboboxImage1.Items.Add(New ComboBoxIconItem(ControlTag, 5))
                        ElseIf contrstr.StartsWith("APanel") Then : Combobox1.Items.Add(New ComboBoxIconItem(ControlTag, 6)) : ComboboxImage1.Items.Add(New ComboBoxIconItem(ControlTag, 6))
                        ElseIf contrstr.StartsWith("PasswordTextBox") Then : Combobox1.Items.Add(New ComboBoxIconItem(ControlTag, 7)) : ComboboxImage1.Items.Add(New ComboBoxIconItem(ControlTag, 7))
                        ElseIf contrstr.StartsWith("APictureBox") Then : Combobox1.Items.Add(New ComboBoxIconItem(ControlTag, 8)) : ComboboxImage1.Items.Add(New ComboBoxIconItem(ControlTag, 8))
                        ElseIf contrstr.StartsWith("AProgressBar") Then : Combobox1.Items.Add(New ComboBoxIconItem(ControlTag, 9)) : ComboboxImage1.Items.Add(New ComboBoxIconItem(ControlTag, 9))
                        ElseIf contrstr.StartsWith("ARadioButton") Then : Combobox1.Items.Add(New ComboBoxIconItem(ControlTag, 10)) : ComboboxImage1.Items.Add(New ComboBoxIconItem(ControlTag, 10))
                        ElseIf contrstr.StartsWith("ATextBox") Then : Combobox1.Items.Add(New ComboBoxIconItem(ControlTag, 12)) : ComboboxImage1.Items.Add(New ComboBoxIconItem(ControlTag, 12))
                        ElseIf contrstr.StartsWith("ATimer") Then : Combobox1.Items.Add(New ComboBoxIconItem(ControlTag, 13)) : ComboboxImage1.Items.Add(New ComboBoxIconItem(ControlTag, 13))
                        ElseIf contrstr.StartsWith("AComboBox") Then : Combobox1.Items.Add(New ComboBoxIconItem(ControlTag, 14)) : ComboboxImage1.Items.Add(New ComboBoxIconItem(ControlTag, 14))
                        ElseIf contrstr.StartsWith("VB4AWeb") Then : Combobox1.Items.Add(New ComboBoxIconItem(ControlTag, 15)) : ComboboxImage1.Items.Add(New ComboBoxIconItem(ControlTag, 15))

                        End If

                    End If
                Next




            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try

            PropertyGrid1.SelectedObject = Nothing
            Combobox1.Items.Add(New ComboBoxIconItem(Combobox3.Text.Replace(".vb4a", Nothing), 16))
            ComboboxImage1.Items.Add(New ComboBoxIconItem(Combobox3.Text.Replace(".vb4a", Nothing), 16))
            Combobox1.Text = Combobox3.Text.Replace(".vb4a", Nothing)
            formisProp()
            Textbox3.Text = My.Computer.FileSystem.ReadAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Combobox3.Text.Replace(".vb4a", ".vb4acode"), Encoding.GetEncoding("gb2312"))


            For Each Line As String In My.Computer.FileSystem.ReadAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Combobox3.Text.Replace(".vb4a", ".DelConts")).Split(vbNewLine)
                Line = Line.Replace(Chr(10), "")



                If Line.StartsWith("AButton") Then
                    cntrafe1 -= 1
                ElseIf Line.StartsWith("Canvas") Then
                    cntrafe2 -= 1
                ElseIf Line.StartsWith("ACheckBox") Then
                    cntrafe3 -= 1
                ElseIf Line.StartsWith("ALabel") Then
                    cntrafe4 -= 1
                ElseIf Line.StartsWith("AListview") Then
                    cntrafe5 -= 1
                ElseIf Line.StartsWith("APanel") Then
                    cntrafe6 -= 1
                ElseIf Line.StartsWith("PasswordTextBox") Then
                    cntrafe7 -= 1
                ElseIf Line.StartsWith("APictureBox") Then
                    cntrafe8 -= 1
                ElseIf Line.StartsWith("AProgressBar") Then
                    cntrafe9 -= 1
                ElseIf Line.StartsWith("ARadioButton") Then
                    cntrafe10 -= 1
                ElseIf Line.StartsWith("ATextBox") Then
                    cntrafe11 -= 1
                ElseIf Line.StartsWith("ATimer") Then
                    cntrafe12 -= 1
                ElseIf Line.StartsWith("AComboBox") Then
                    cntrafe13 -= 1
                ElseIf Line.StartsWith("VB4AWeb") Then
                    cntrafe14 -= 1
                End If


            Next


            BuildAutocompleteMenu()

        End If
    End Sub


    Class BorderlessButton
        Inherits Button
        Protected Overrides Sub OnPaint(pevent As PaintEventArgs)
            MyBase.OnPaint(pevent)
            pevent.Graphics.DrawRectangle(New Pen(Me.BackColor, 5), Me.ClientRectangle)
        End Sub
    End Class
    Public Sub AddControl(e As MouseEventArgs, PanelPl As Panel)
        If e.Button = Windows.Forms.MouseButtons.Left Then

            Dim onFormIs As Boolean = False
            Dim onPanelIs As String = Nothing
            Dim it_is_not_in_Combo As Boolean = False



            Try
                idControl += 1
                Select Case ListView1.SelectedItems(0).Text



                    Case Is = ListView1.Items.Item(0).Text '"   Button"
                        AButton = New BorderlessButton
                        ba1 = 1
                        ba1 -= cntrafe1
                        Dim i As Integer = 1 ' mporw na to kanw kai alios apla einai xronovoro kai anyway....(apla auto isos kolaei ligo polu ligo...)
                        Dim DoBool As Boolean = False

                        Do
                            If listbool("AButton" & i, ButtonString) = True Then

                                '------------------------------------------------------=------------------------------------------------------=------------------------------------------------------=------------------------------------------------------=------------------------------------------------------=------------------------------------------------------=------------------------------------------------------=------------------------------------------------------=------------------------------------------------------=------------------------------------------------------=------------------------------------------------------=------------------------------------------------------=------------------------------------------------------=------------------------------------------------------=------------------------------------------------------=------------------------------------------------------=------------------------------------------------------=------------------------------------------------------=TO κοματι εδω απο κατω δεν ειναι τεσταρισμενο 6 lines ισοσ να υπαρξει προμπλεμ
                                Do
                                    For w = 0 To Combobox1.Items.Count - 1 Step 1
                                        If "Button" & ba1 = Combobox1.Items.Item(w).ToString Then it_is_not_in_Combo = False : ba1 += 1 : Exit For Else it_is_not_in_Combo = True
                                    Next
                                Loop Until it_is_not_in_Combo = True


                                With AButton
                                    .Enabled = True
                                    .BackColor = Color.LightGray
                                    .Name = "AButton" & i
                                    .Text = "Button" & ba1 ' ListView1.Items.Item(0).Text & ba1 ----------------------------------------------------------<<<<<<<<<<<<<<<<<<
                                    .Size() = New System.Drawing.Size(XofFormApp / 240 * 101, YofFormApp / 425 * 31)
                                    ' .BringToFront()
                                    .Tag = "Button" & ba1 ' 8a einai to name gt twra to name einai enos kanonikou enov gia ta properties
                                    .Location = New System.Drawing.Point(e.Location.X - AButton.Size.Width / 2, e.Location.Y - AButton.Size.Height / 2)
                                    .Font = New Font(Font.FontFamily, 10)
                                    .BackgroundImageLayout = ImageLayout.Stretch
                                    .FlatStyle = FlatStyle.Popup
                                End With
                                ' Label3.Text = New System.Drawing.Point(e.Location.X - AButton.Size.Width / 2, e.Location.Y - AButton.Size.Height / 2).ToString
                                PanelPl.Controls.Add(AButton)
                                ButtonString &= ("AButton" & i & vbNewLine)

                                Combobox1.Items.Add(New ComboBoxIconItem(AButton.Tag, 0))
                                Combobox1.Text = AButton.Tag


                                '     AButton.BringToFront()


                                If AButton.Location.X < 0 Then AButton.Location = New Point(0, AButton.Location.Y)
                                If AButton.Location.Y < 0 Then AButton.Location = New Point(AButton.Location.X, 0)
                                If AButton.Location.X + AButton.Size.Width > XofFormApp Then AButton.Location = New Point(XofFormApp - AButton.Size.Width, AButton.Location.Y)
                                If AButton.Location.Y + AButton.Size.Height > YofFormApp Then AButton.Location = New Point(AButton.Location.X, YofFormApp - AButton.Size.Height)


                                If PanelPl.Name = Panel5.Name Then
                                    onFormIs = True
                                Else
                                    onFormIs = False
                                    onPanelIs = RetriveInWhatPanelis(AButton)
                                End If

                                SaveProj("<AButton" & i & ">" & vbNewLine & "id=" & idControl & vbNewLine & "IsOnForm=" & onFormIs.ToString & vbNewLine & "IsOnPanel=" & onPanelIs & vbNewLine & "Name=" & AButton.Tag & vbNewLine & "Text=" & AButton.Text &
                                          vbNewLine & "FontBold=False" & vbNewLine & "FontSize=" & AButton.Font.Size & vbNewLine & "FontItalic=False" & vbNewLine &
                                          "FontTypeface=DEFAULT" & vbNewLine & "TextAlign=CENTER_CENTER" & vbNewLine & "BackColor={" & AButton.BackColor.A & ";" & AButton.BackColor.R & ";" & AButton.BackColor.G & ";" & AButton.BackColor.B & "}" & vbNewLine _
                                          & "ForeColor={" & AButton.ForeColor.A & ";" & AButton.ForeColor.R & ";" & AButton.ForeColor.G & ";" & AButton.ForeColor.B & "}" & vbNewLine & "Location=" & AButton.Location.ToString.Replace("X=", Nothing).Replace(",", ";").Replace("Y=", Nothing) & vbNewLine & "Size=" & AButton.Size.ToString.Replace("Width=", Nothing).Replace(",", ";").Replace("Height=", Nothing).Replace(" ", Nothing) _
                                          & vbNewLine & "Image=" & vbNewLine & "Enabled=True" & vbNewLine & "</AButton" & i & ">" & vbNewLine, True, Combobox3.Text)
                                '.tostring("X")
                                'MsgBox(AButton.Tag & vbNewLine & AButton.Location.ToString.Replace("Width=",Nothing).Replace(",",";").Replace("Height=",Nothing))
                                DoBool = True
                            Else
                                i += 1
                                ba1 += 1
                            End If

                        Loop Until DoBool = True
                        GenControl = AButton
                        '  rc = New ResizeableControl(AButton)
                        ComboboxImage1.Items.Add(New ComboBoxIconItem(AButton.Tag, 0))
                    ' ClickGenControlsPropert(AButton.Name)

                    Case Is = ListView1.Items.Item(1).Text '"   Canvas"
                        Canvas = New PictureBox
                        ba2 = 1
                        ba2 -= cntrafe2
                        Dim i As Integer = 1 ' mporw na to kanw kai alios apla einai xronovoro kai anyway....(apla auto isos kolaei ligo polu ligo...)
                        Dim DoBool As Boolean = False

                        Do
                            If listbool("Canvas" & i, CanvasString) = True Then


                                Do
                                    For w = 0 To Combobox1.Items.Count - 1 Step 1
                                        If "Canvas" & ba2 = Combobox1.Items.Item(w).ToString Then it_is_not_in_Combo = False : ba2 += 1 : Exit For Else it_is_not_in_Combo = True
                                    Next
                                Loop Until it_is_not_in_Combo = True


                                With Canvas
                                    .Enabled = True
                                    .BackColor = Color.White
                                    .Name = "Canvas" & i
                                    .Text = "Canvas" & ba2
                                    .Size() = New System.Drawing.Size(XofFormApp / 240 * 165, YofFormApp / 425 * 265)
                                    ' .BringToFront()
                                    .Tag = "Canvas" & ba2 ' 8a einai to name gt twra to name einai enos kanonikou enov gia ta properties
                                    .Location = New System.Drawing.Point(e.Location.X - Canvas.Size.Width / 2, e.Location.Y - Canvas.Size.Height / 2)
                                    '  .Font = New Font(Canvas.Font.FontFamily, 10)
                                    .BorderStyle = BorderStyle.FixedSingle
                                    .BackgroundImageLayout = ImageLayout.Stretch
                                End With
                                ' Label3.Text = New System.Drawing.Point(e.Location.X - Canvas.Size.Width / 2, e.Location.Y - Canvas.Size.Height / 2).ToString
                                PanelPl.Controls.Add(Canvas)
                                CanvasString &= ("Canvas" & i & vbNewLine)


                                Combobox1.Items.Add(New ComboBoxIconItem(Canvas.Tag, 1))
                                Combobox1.Text = Canvas.Tag


                                ' Canvas.BringToFront()

                                If Canvas.Location.X < 0 Then Canvas.Location = New Point(0, Canvas.Location.Y)
                                If Canvas.Location.Y < 0 Then Canvas.Location = New Point(Canvas.Location.X, 0)
                                If Canvas.Location.X + Canvas.Size.Width > XofFormApp Then Canvas.Location = New Point(XofFormApp - Canvas.Size.Width, Canvas.Location.Y)
                                If Canvas.Location.Y + Canvas.Size.Height > YofFormApp Then Canvas.Location = New Point(Canvas.Location.X, YofFormApp - Canvas.Size.Height)

                                If PanelPl.Name = Panel5.Name Then
                                    onFormIs = True
                                Else
                                    onFormIs = False
                                    onPanelIs = RetriveInWhatPanelis(Canvas)
                                End If
                                'MsgBox(Canvas.Size.ToString)
                                SaveProj("<Canvas" & i & ">" & vbNewLine & "id=" & idControl & vbNewLine & "IsOnForm=" & onFormIs.ToString & vbNewLine & "IsOnPanel=" & onPanelIs & vbNewLine & "Name=" & Canvas.Tag & vbNewLine _
                                         & "BackgroundColor={" & Canvas.BackColor.A & ";" & Canvas.BackColor.R & ";" & Canvas.BackColor.G & ";" & Canvas.BackColor.B & "}" & vbNewLine _
                                          & "BackgroundImage=" & vbNewLine & "Location=" & Canvas.Location.ToString.Replace("X=", Nothing).Replace(",", ";").Replace("Y=", Nothing) & vbNewLine & "Size=" & Canvas.Size.ToString.Replace("Width=", Nothing).Replace(",", ";").Replace("Height=", Nothing).Replace(" ", Nothing) _
                                         & vbNewLine & "Enabled=True" & vbNewLine & "PointColor={255;0;0;0}" & vbNewLine & "</Canvas" & i & ">" & vbNewLine, True, Combobox3.Text)

                                DoBool = True
                            Else
                                i += 1
                                ba2 += 1
                            End If
                        Loop Until DoBool = True
                        GenControl = Canvas
                        ' rc = New ResizeableControl(Canvas)
                        ComboboxImage1.Items.Add(New ComboBoxIconItem(Canvas.Tag, 1))
                    ' ClickGenControlsPropert(Canvas.Name)

                    Case Is = ListView1.Items.Item(2).Text '"   CheckBox"
                        ACheckBox = New CheckBox
                        ba3 = 1
                        ba3 -= cntrafe3
                        Dim i As Integer = 1
                        Dim DoBool As Boolean = False

                        Do
                            If listbool("ACheckBox" & i, CheckBoxString) = True Then


                                Do
                                    For w = 0 To Combobox1.Items.Count - 1 Step 1
                                        If "CheckBox" & ba3 = Combobox1.Items.Item(w).ToString Then it_is_not_in_Combo = False : ba3 += 1 : Exit For Else it_is_not_in_Combo = True
                                    Next
                                Loop Until it_is_not_in_Combo = True


                                With ACheckBox
                                    .Enabled = True
                                    .BackColor = Color.Transparent
                                    .Name = "ACheckBox" & i
                                    .Text = "CheckBox" & ba3
                                    .Size() = New System.Drawing.Size(XofFormApp / 240 * 101, YofFormApp / 425 * 20)
                                    ' .BringToFront()
                                    .Tag = "CheckBox" & ba3
                                    .Location = New System.Drawing.Point(e.Location.X - ACheckBox.Size.Width / 2, e.Location.Y - ACheckBox.Size.Height / 2)
                                    .Font = New Font(Font.FontFamily, 10)
                                    .FlatStyle = FlatStyle.Flat
                                    .AutoSize = False
                                End With

                                PanelPl.Controls.Add(ACheckBox)
                                CheckBoxString &= ("ACheckBox" & i & vbNewLine)

                                Combobox1.Items.Add(New ComboBoxIconItem(ACheckBox.Tag, 2))
                                Combobox1.Text = ACheckBox.Tag


                                '       ACheckBox.BringToFront()

                                If ACheckBox.Location.X < 0 Then ACheckBox.Location = New Point(0, ACheckBox.Location.Y)
                                If ACheckBox.Location.Y < 0 Then ACheckBox.Location = New Point(ACheckBox.Location.X, 0)
                                If ACheckBox.Location.X + ACheckBox.Size.Width > XofFormApp Then ACheckBox.Location = New Point(XofFormApp - ACheckBox.Size.Width, ACheckBox.Location.Y)
                                If ACheckBox.Location.Y + ACheckBox.Size.Height > YofFormApp Then ACheckBox.Location = New Point(ACheckBox.Location.X, YofFormApp - ACheckBox.Size.Height)

                                If PanelPl.Name = Panel5.Name Then
                                    onFormIs = True
                                Else
                                    onFormIs = False
                                    onPanelIs = RetriveInWhatPanelis(ACheckBox)
                                End If

                                SaveProj("<ACheckBox" & i & ">" & vbNewLine & "id=" & idControl & vbNewLine & "IsOnForm=" & onFormIs.ToString & vbNewLine & "IsOnPanel=" & onPanelIs & vbNewLine & "Name=" & ACheckBox.Tag & vbNewLine & "Text=" & ACheckBox.Text &
                                          vbNewLine & "FontBold=False" & vbNewLine & "FontSize=" & ACheckBox.Font.Size & vbNewLine & "FontItalic=False" & vbNewLine &
                                          "FontTypeface=DEFAULT" & vbNewLine & "TextAlign=LEFT_CENTER" & vbNewLine & "BackColor={" & ACheckBox.BackColor.A & ";" & ACheckBox.BackColor.R & ";" & ACheckBox.BackColor.G & ";" & ACheckBox.BackColor.B & "}" & vbNewLine _
                                          & "ForeColor={" & ACheckBox.ForeColor.A & ";" & ACheckBox.ForeColor.R & ";" & ACheckBox.ForeColor.G & ";" & ACheckBox.ForeColor.B & "}" & vbNewLine & "Location=" & ACheckBox.Location.ToString.Replace("X=", Nothing).Replace(",", ";").Replace("Y=", Nothing) & vbNewLine & "Size=" & ACheckBox.Size.ToString.Replace("Width=", Nothing).Replace(",", ";").Replace("Height=", Nothing).Replace(" ", Nothing) _
                                          & vbNewLine & "Checked=False" & vbNewLine & "Enabled=True" & vbNewLine & "</ACheckBox" & i & ">" & vbNewLine, True, Combobox3.Text)


                                DoBool = True
                            Else
                                i += 1
                                ba3 += 1
                            End If
                        Loop Until DoBool = True
                        GenControl = ACheckBox
                        ' rc = New ResizeableControl(ACheckBox)
                        ComboboxImage1.Items.Add(New ComboBoxIconItem(ACheckBox.Tag, 2))
                    '  ClickGenControlsPropert(ACheckBox.Name)

                    Case Is = ListView1.Items.Item(3).Text '"   EmailPicker"
                        ' ListView1.Select()
                    Case Is = ListView1.Items.Item(4).Text '"   Label"
                        ALabel = New Label
                        ba4 = 1
                        ba4 -= cntrafe4
                        Dim i As Integer = 1
                        Dim DoBool As Boolean = False

                        Do
                            If listbool("ALabel" & i, LabelString) = True Then


                                Do
                                    For w = 0 To Combobox1.Items.Count - 1 Step 1
                                        If "Label" & ba4 = Combobox1.Items.Item(w).ToString Then it_is_not_in_Combo = False : ba4 += 1 : Exit For Else it_is_not_in_Combo = True
                                    Next
                                Loop Until it_is_not_in_Combo = True


                                With ALabel
                                    .Enabled = True
                                    .BackColor = Color.Transparent 'System.Drawing.Color.FromArgb(235, 235, 235)
                                    .Name = "ALabel" & i
                                    .Text = "Label" & ba4
                                    .Size() = New System.Drawing.Size(XofFormApp / 240 * 55, YofFormApp / 425 * 18)
                                    ' .BringToFront()
                                    .Tag = "Label" & ba4
                                    .Location = New System.Drawing.Point(e.Location.X - ALabel.Size.Width / 2, e.Location.Y - ALabel.Size.Height / 2)
                                    .Font = New Font(Font.FontFamily, 10)
                                    .FlatStyle = FlatStyle.Flat
                                    .AutoSize = False
                                End With

                                PanelPl.Controls.Add(ALabel)
                                LabelString &= ("ALabel" & i & vbNewLine)

                                Combobox1.Items.Add(New ComboBoxIconItem(ALabel.Tag, 4))
                                Combobox1.Text = ALabel.Tag


                                '    ALabel.BringToFront()

                                If ALabel.Location.X < 0 Then ALabel.Location = New Point(0, ALabel.Location.Y)
                                If ALabel.Location.Y < 0 Then ALabel.Location = New Point(ALabel.Location.X, 0)
                                If ALabel.Location.X + ALabel.Size.Width > XofFormApp Then ALabel.Location = New Point(XofFormApp - ALabel.Size.Width, ALabel.Location.Y)
                                If ALabel.Location.Y + ALabel.Size.Height > YofFormApp Then ALabel.Location = New Point(ALabel.Location.X, YofFormApp - ALabel.Size.Height)

                                If PanelPl.Name = Panel5.Name Then
                                    onFormIs = True
                                Else
                                    onFormIs = False
                                    onPanelIs = RetriveInWhatPanelis(ALabel)
                                End If

                                SaveProj("<ALabel" & i & ">" & vbNewLine & "id=" & idControl & vbNewLine & "IsOnForm=" & onFormIs.ToString & vbNewLine & "IsOnPanel=" & onPanelIs & vbNewLine & "Name=" & ALabel.Tag & vbNewLine & "Text=" & ALabel.Text &
                                          vbNewLine & "FontBold=False" & vbNewLine & "FontSize=" & ALabel.Font.Size & vbNewLine & "FontItalic=False" & vbNewLine &
                                          "FontTypeface=DEFAULT" & vbNewLine & "TextAlign=LEFT_TOP" & vbNewLine & "BackColor={" & ALabel.BackColor.A & ";" & ALabel.BackColor.R & ";" & ALabel.BackColor.G & ";" & ALabel.BackColor.B & "}" & vbNewLine _
                                          & "ForeColor={" & ALabel.ForeColor.A & ";" & ALabel.ForeColor.R & ";" & ALabel.ForeColor.G & ";" & ALabel.ForeColor.B & "}" & vbNewLine & "Location=" & ALabel.Location.ToString.Replace("X=", Nothing).Replace(",", ";").Replace("Y=", Nothing) & vbNewLine & "Size=" & ALabel.Size.ToString.Replace("Width=", Nothing).Replace(",", ";").Replace("Height=", Nothing).Replace(" ", Nothing) _
                                          & vbNewLine & "Enabled=True" & vbNewLine & "</ALabel" & i & ">" & vbNewLine, True, Combobox3.Text)


                                DoBool = True
                            Else
                                i += 1
                                ba4 += 1
                            End If
                        Loop Until DoBool = True
                        GenControl = ALabel
                        ' rc = New ResizeableControl(ALabel)
                        ComboboxImage1.Items.Add(New ComboBoxIconItem(ALabel.Tag, 4))
                    '  ClickGenControlsPropert(ALabel.Name)

                    Case Is = ListView1.Items.Item(5).Text '"   ListView"
                        AListBox = New PictureBox
                        ba5 = 1
                        ba5 -= cntrafe5
                        Dim i As Integer = 1
                        Dim DoBool As Boolean = False

                        Do
                            If listbool("AListview" & i, ListBoxString) = True Then


                                Do
                                    For w = 0 To Combobox1.Items.Count - 1 Step 1
                                        If "Listview" & ba5 = Combobox1.Items.Item(w).ToString Then it_is_not_in_Combo = False : ba5 += 1 : Exit For Else it_is_not_in_Combo = True
                                    Next
                                Loop Until it_is_not_in_Combo = True


                                With AListBox
                                    .Enabled = True
                                    .BackColor = System.Drawing.Color.FromArgb(235, 235, 235)
                                    .Name = "AListview" & i
                                    '.Text = "ListView" & ba5
                                    .Size() = New System.Drawing.Size(XofFormApp / 240 * 150, YofFormApp / 425 * 250)
                                    ' .BringToFront()
                                    .Tag = "Listview" & ba5
                                    .Location = New System.Drawing.Point(e.Location.X - AListBox.Size.Width / 2, e.Location.Y - AListBox.Size.Height / 2)
                                    .BorderStyle = BorderStyle.FixedSingle
                                End With

                                PanelPl.Controls.Add(AListBox)
                                ListBoxString &= ("AListview" & i & vbNewLine)

                                Combobox1.Items.Add(New ComboBoxIconItem(AListBox.Tag, 5))
                                Combobox1.Text = AListBox.Tag


                                '     AListBox.BringToFront()

                                If AListBox.Location.X < 0 Then AListBox.Location = New Point(0, AListBox.Location.Y)
                                If AListBox.Location.Y < 0 Then AListBox.Location = New Point(AListBox.Location.X, 0)
                                If AListBox.Location.X + AListBox.Size.Width > XofFormApp Then AListBox.Location = New Point(XofFormApp - AListBox.Size.Width, AListBox.Location.Y)
                                If AListBox.Location.Y + AListBox.Size.Height > YofFormApp Then AListBox.Location = New Point(AListBox.Location.X, YofFormApp - AListBox.Size.Height)


                                If PanelPl.Name = Panel5.Name Then
                                    onFormIs = True
                                Else
                                    onFormIs = False
                                    onPanelIs = RetriveInWhatPanelis(AListBox)
                                End If

                                SaveProj("<AListview" & i & ">" & vbNewLine & "id=" & idControl & vbNewLine & "IsOnForm=" & onFormIs.ToString & vbNewLine & "IsOnPanel=" & onPanelIs & vbNewLine & "Name=" & AListBox.Tag &
                                          vbNewLine & "FontBold=False" & vbNewLine & "FontSize=10" & vbNewLine & "FontItalic=False" & vbNewLine &
                                          "FontTypeface=DEFAULT" & vbNewLine & "TextAlign=CENTER_CENTER" & vbNewLine & "BackColor={" & AListBox.BackColor.A & ";" & AListBox.BackColor.R & ";" & AListBox.BackColor.G & ";" & AListBox.BackColor.B & "}" & vbNewLine _
                                          & "ForeColor={" & AListBox.ForeColor.A & ";" & AListBox.ForeColor.R & ";" & AListBox.ForeColor.G & ";" & AListBox.ForeColor.B & "}" & vbNewLine & "Location=" & AListBox.Location.ToString.Replace("X=", Nothing).Replace(",", ";").Replace("Y=", Nothing) & vbNewLine & "Size=" & AListBox.Size.ToString.Replace("Width=", Nothing).Replace(",", ";").Replace("Height=", Nothing).Replace(" ", Nothing) _
                                          & vbNewLine & "Enabled=True" & vbNewLine & "</AListview" & i & ">" & vbNewLine, True, Combobox3.Text)


                                DoBool = True
                            Else
                                i += 1
                                ba5 += 1
                            End If
                        Loop Until DoBool = True
                        GenControl = AListBox
                        ' rc = New ResizeableControl(AListBox)
                        ComboboxImage1.Items.Add(New ComboBoxIconItem(AListBox.Tag, 5))
                    '  ClickGenControlsPropert(AListBox.Name)


                    Case Is = ListView1.Items.Item(6).Text '"   Panel"

                        '                        APanel = New Panel        ' I NEEEEEEEED THISSS  ------------------------------------------------------------------------------------------------------------------------------------------------------ IT IS NOT SUPPORTED ONLY FOR NOW ================= IT IS NOT SUPPORTED ONLY FOR NOW ================= IT IS NOT SUPPORTED ONLY FOR NOW ================= IT IS NOT SUPPORTED ONLY FOR NOW ================= IT IS NOT SUPPORTED ONLY FOR NOW ================= IT IS NOT SUPPORTED ONLY FOR NOW ================= IT IS NOT SUPPORTED ONLY FOR NOW =================
                        '                        ba6 = 1
                        '                        ba6 -= cntrafe6
                        '                        Dim i As Integer = 1
                        '                        Dim DoBool As Boolean = False

                        '                        Do
                        '                            If listbool("APanel" & i, PanelString) = True Then


                        '                                Do
                        '                                    For w = 0 To Combobox1.Items.Count - 1 Step 1
                        '                                        If "Panel" & ba6 = Combobox1.Items.Item(w).ToString Then it_is_not_in_Combo = False : ba6 += 1 : Exit For Else it_is_not_in_Combo = True
                        '                                    Next
                        '                                Loop Until it_is_not_in_Combo = True


                        '                                With APanel
                        '                                    .Enabled = True
                        '                                    .BackColor = Color.WhiteSmoke
                        '                                    .Name = "APanel" & i
                        '                                    .Text = "Panel" & ba6
                        '                                    .Size() = New System.Drawing.Size(XofFormApp / 240 * 218, YofFormApp / 425 * 151)

                        '                                    .Tag = "Panel" & ba6
                        '                                    .Location = New System.Drawing.Point(e.Location.X - APanel.Size.Width / 2, e.Location.Y - APanel.Size.Height / 2)
                        '                                    ' .Font = New Font(APanel.Font.FontFamily, 10)
                        '                                    .BorderStyle = BorderStyle.Fixed3D
                        '                                    .BackgroundImageLayout = ImageLayout.Stretch
                        '                                End With

                        '                                PanelPl.Controls.Add(APanel)
                        '                                PanelString &= ("APanel" & i & vbNewLine)

                        '                                Combobox1.Items.Add(New ComboBoxIconItem(APanel.Tag, 6))
                        '                                Combobox1.Text = APanel.Tag


                        '                                '   APanel.BringToFront()

                        '                                If APanel.Location.X < 0 Then APanel.Location = New Point(0, APanel.Location.Y)
                        '                                If APanel.Location.Y < 0 Then APanel.Location = New Point(APanel.Location.X, 0)
                        '                                If APanel.Location.X + APanel.Size.Width > XofFormApp Then APanel.Location = New Point(XofFormApp - APanel.Size.Width, APanel.Location.Y)
                        '                                If APanel.Location.Y + APanel.Size.Height > YofFormApp Then APanel.Location = New Point(APanel.Location.X, YofFormApp - APanel.Size.Height)


                        '                                If PanelPl.Name = Panel5.Name Then
                        '                                    onFormIs = True
                        '                                Else
                        '                                    onFormIs = False
                        '                                    onPanelIs = RetriveInWhatPanelis(APanel)
                        '                                End If

                        '                                SaveProj("<APanel" & i & ">" & vbNewLine & "id=" & idControl & vbNewLine & "IsOnForm=" & onFormIs.ToString & vbNewLine & "IsOnPanel=" & onPanelIs & vbNewLine & "Name=" & APanel.Tag & vbNewLine & "BackgroundColor={" & APanel.BackColor.A & ";" & APanel.BackColor.R & ";" & APanel.BackColor.G & ";" & APanel.BackColor.B & "}" &
                        '                                         vbNewLine & "BackgroundImage=" & APanel.BackgroundImage & vbNewLine & "Location=" & APanel.Location.ToString.Replace("X=", Nothing).Replace(",", ";").Replace("Y=", Nothing) & vbNewLine & "Size=" & APanel.Size.ToString.Replace("Width=", Nothing).Replace(",", ";").Replace("Height=", Nothing).Replace(" ", Nothing) _
                        '                                         & vbNewLine & "Enabled=True" & vbNewLine & "</APanel" & i & ">" & vbNewLine, True, Combobox3.Text)


                        '                                DoBool = True
                        '                            Else
                        '                                i += 1
                        '                                ba6 += 1
                        '                            End If
                        '                        Loop Until DoBool = True
                        '                        GenControl = APanel
                        '                        ' rc = New ResizeableControl(APanel)
                        '                        ComboboxImage1.Items.Add(New ComboBoxIconItem(APanel.Tag, 6))
                        '                    ' ClickGenControlsPropert(APanel.Name)

                    Case Is = ListView1.Items.Item(7).Text '"   PasswordTextBox"
                        PasswordTextBox = New TextBox
                        ba7 = 1
                        ba7 -= cntrafe7
                        Dim i As Integer = 1 ' mporw na to kanw kai alios apla einai xronovoro kai anyway....(apla auto isos kolaei ligo polu ligo...)
                        Dim DoBool As Boolean = False

                        Do
                            If listbool("PasswordTextBox" & i, PasswordTextBoxString) = True Then


                                Do
                                    For w = 0 To Combobox1.Items.Count - 1 Step 1
                                        If "PasswordTextBox" & ba7 = Combobox1.Items.Item(w).ToString Then it_is_not_in_Combo = False : ba7 += 1 : Exit For Else it_is_not_in_Combo = True
                                    Next
                                Loop Until it_is_not_in_Combo = True


                                With PasswordTextBox
                                    .Enabled = True
                                    .BorderStyle = BorderStyle.FixedSingle
                                    .BackColor = System.Drawing.Color.FromArgb(235, 235, 235)
                                    .Name = "PasswordTextBox" & i
                                    .Text = "PasswordTextBox" & ba7
                                    .Size() = New System.Drawing.Size(XofFormApp / 240 * 190, YofFormApp / 425 * 23)
                                    ' .BringToFront()
                                    .Tag = "PasswordTextBox" & ba7 ' 8a einai to name gt twra to name einai enos kanonikou enov gia ta properties
                                    .Location = New System.Drawing.Point(e.Location.X - PasswordTextBox.Size.Width / 2, e.Location.Y - PasswordTextBox.Size.Height / 2)
                                    .Font = New Font(Font.FontFamily, 10)
                                    .Multiline = True
                                    .Cursor = Cursors.NoMove2D
                                    .ReadOnly = True
                                End With
                                ' Label3.Text = New System.Drawing.Point(e.Location.X - PasswordTextBox.Size.Width / 2, e.Location.Y - PasswordTextBox.Size.Height / 2).ToString
                                PanelPl.Controls.Add(PasswordTextBox)
                                PasswordTextBoxString &= ("PasswordTextBox" & i & vbNewLine)

                                Combobox1.Items.Add(New ComboBoxIconItem(PasswordTextBox.Tag, 7))
                                Combobox1.Text = PasswordTextBox.Tag


                                '     PasswordTextBox.BringToFront()

                                If PasswordTextBox.Location.X < 0 Then PasswordTextBox.Location = New Point(0, PasswordTextBox.Location.Y)
                                If PasswordTextBox.Location.Y < 0 Then PasswordTextBox.Location = New Point(PasswordTextBox.Location.X, 0)
                                If PasswordTextBox.Location.X + PasswordTextBox.Size.Width > XofFormApp Then PasswordTextBox.Location = New Point(XofFormApp - PasswordTextBox.Size.Width, PasswordTextBox.Location.Y)
                                If PasswordTextBox.Location.Y + PasswordTextBox.Size.Height > YofFormApp Then PasswordTextBox.Location = New Point(PasswordTextBox.Location.X, YofFormApp - PasswordTextBox.Size.Height)


                                If PanelPl.Name = Panel5.Name Then
                                    onFormIs = True
                                Else
                                    onFormIs = False
                                    onPanelIs = RetriveInWhatPanelis(PasswordTextBox)
                                End If

                                SaveProj("<PasswordTextBox" & i & ">" & vbNewLine & "id=" & idControl & vbNewLine & "IsOnForm=" & onFormIs.ToString & vbNewLine & "IsOnPanel=" & onPanelIs & vbNewLine & "Name=" & PasswordTextBox.Tag & vbNewLine & "Text=" & PasswordTextBox.Text &
                                          vbNewLine & "FontBold=False" & vbNewLine & "FontSize=" & PasswordTextBox.Font.Size & vbNewLine & "FontItalic=False" & vbNewLine &
                                          "FontTypeface=DEFAULT" & vbNewLine & "TextAlign=LEFT_TOP" & vbNewLine & "BackColor={" & PasswordTextBox.BackColor.A & ";" & PasswordTextBox.BackColor.R & ";" & PasswordTextBox.BackColor.G & ";" & PasswordTextBox.BackColor.B & "}" & vbNewLine _
                                          & "ForeColor={" & PasswordTextBox.ForeColor.A & ";" & PasswordTextBox.ForeColor.R & ";" & PasswordTextBox.ForeColor.G & ";" & PasswordTextBox.ForeColor.B & "}" & vbNewLine & "Hint=" & vbNewLine & "Location=" & PasswordTextBox.Location.ToString.Replace("X=", Nothing).Replace(",", ";").Replace("Y=", Nothing) & vbNewLine & "Size=" & PasswordTextBox.Size.ToString.Replace("Width=", Nothing).Replace(",", ";").Replace("Height=", Nothing).Replace(" ", Nothing) _
                                          & vbNewLine & "Enabled=True" & vbNewLine & "</PasswordTextBox" & i & ">" & vbNewLine, True, Combobox3.Text)


                                DoBool = True
                            Else
                                i += 1
                                ba7 += 1
                            End If
                        Loop Until DoBool = True
                        GenControl = PasswordTextBox
                        ' rc = New ResizeableControl(PasswordTextBox)
                        ComboboxImage1.Items.Add(New ComboBoxIconItem(PasswordTextBox.Tag, 7))
                    '  ClickGenControlsPropert(PasswordTextBox.Name)

                    Case Is = ListView1.Items.Item(8).Text '"   PictureBox" ' Image in specs
                        APictureBox = New PictureBox
                        ba8 = 1
                        ba8 -= cntrafe8
                        Dim i As Integer = 1
                        Dim DoBool As Boolean = False

                        Do
                            If listbool("APictureBox" & i, PictureBoxString) = True Then


                                Do
                                    For w = 0 To Combobox1.Items.Count - 1 Step 1
                                        If "PictureBox" & ba8 = Combobox1.Items.Item(w).ToString Then it_is_not_in_Combo = False : ba8 += 1 : Exit For Else it_is_not_in_Combo = True
                                    Next
                                Loop Until it_is_not_in_Combo = True


                                With APictureBox
                                    .Enabled = True
                                    .BackColor = Color.White
                                    .Name = "APictureBox" & i
                                    .Text = "PictureBox" & ba8
                                    .Size() = New System.Drawing.Size(XofFormApp / 240 * 218, YofFormApp / 425 * 151)
                                    .Tag = "PictureBox" & ba8
                                    .Location = New System.Drawing.Point(e.Location.X - APictureBox.Size.Width / 2, e.Location.Y - APictureBox.Size.Height / 2)
                                    ' .Font = New Font(APictureBox.Font.FontFamily, 10)
                                    .BorderStyle = BorderStyle.FixedSingle
                                    .BackgroundImageLayout = ImageLayout.Stretch
                                End With

                                PanelPl.Controls.Add(APictureBox)
                                PictureBoxString &= ("APictureBox" & i & vbNewLine)

                                Combobox1.Items.Add(New ComboBoxIconItem(APictureBox.Tag, 8))
                                Combobox1.Text = APictureBox.Tag


                                '    APictureBox.BringToFront()

                                If APictureBox.Location.X < 0 Then APictureBox.Location = New Point(0, APictureBox.Location.Y)
                                If APictureBox.Location.Y < 0 Then APictureBox.Location = New Point(APictureBox.Location.X, 0)
                                If APictureBox.Location.X + APictureBox.Size.Width > XofFormApp Then APictureBox.Location = New Point(XofFormApp - APictureBox.Size.Width, APictureBox.Location.Y)
                                If APictureBox.Location.Y + APictureBox.Size.Height > YofFormApp Then APictureBox.Location = New Point(APictureBox.Location.X, YofFormApp - APictureBox.Size.Height)


                                If PanelPl.Name = Panel5.Name Then
                                    onFormIs = True

                                Else
                                    onFormIs = False
                                    onPanelIs = RetriveInWhatPanelis(APictureBox)
                                End If

                                SaveProj("<APictureBox" & i & ">" & vbNewLine & "id=" & idControl & vbNewLine & "IsOnForm=" & onFormIs.ToString & vbNewLine & "IsOnPanel=" & onPanelIs &
                                         vbNewLine & "Name=" & APictureBox.Tag & vbNewLine & "BackColor={" & APictureBox.BackColor.A & ";" & APictureBox.BackColor.R & ";" & APictureBox.BackColor.G & ";" & APictureBox.BackColor.B & "}" & vbNewLine & "Image=" & vbNewLine &
                                         "Location=" & APictureBox.Location.ToString.Replace("X=", Nothing).Replace(",", ";").Replace("Y=", Nothing) & vbNewLine & "Size=" & APictureBox.Size.ToString.Replace("Width=", Nothing).Replace(",", ";").Replace("Height=", Nothing).Replace(" ", Nothing) _
                                         & vbNewLine & "Enabled=True" & vbNewLine & "</APictureBox" & i & ">" & vbNewLine, True, Combobox3.Text)


                                DoBool = True
                            Else
                                i += 1
                                ba8 += 1
                            End If
                        Loop Until DoBool = True
                        GenControl = APictureBox
                        ' rc = New ResizeableControl(APictureBox)
                        ComboboxImage1.Items.Add(New ComboBoxIconItem(APictureBox.Tag, 8))
                    ' ClickGenControlsPropert(APictureBox.Name)

                    Case Is = ListView1.Items.Item(9).Text '"   ProgressBar"
                        AProgressBar = New ProgressBar
                        ba9 = 1
                        ba9 -= cntrafe9
                        Dim i As Integer = 1
                        Dim DoBool As Boolean = False

                        '  AddHandler AProgressBar.MouseDown, AddressOf Me.AProgressBar_MouseDown

                        Do
                            If listbool("AProgressBar" & i, ProgressBarString) = True Then


                                Do
                                    For w = 0 To Combobox1.Items.Count - 1 Step 1
                                        If "ProgressBar" & ba9 = Combobox1.Items.Item(w).ToString Then it_is_not_in_Combo = False : ba9 += 1 : Exit For Else it_is_not_in_Combo = True
                                    Next
                                Loop Until it_is_not_in_Combo = True


                                With AProgressBar
                                    .Enabled = True
                                    .Name = "AProgressBar" & i
                                    .Text = "ProgressBar" & ba9
                                    .Size() = New System.Drawing.Size(XofFormApp / 240 * 180, YofFormApp / 425 * 15)
                                    .Tag = "ProgressBar" & ba9
                                    .Location = New System.Drawing.Point(e.Location.X - AProgressBar.Size.Width / 2, e.Location.Y - AProgressBar.Size.Height / 2)
                                    .Maximum = 100
                                    ' .Font = New Font(AProgressBar.Font.FontFamily, 10)
                                End With

                                PanelPl.Controls.Add(AProgressBar)
                                ProgressBarString &= ("AProgressBar" & i & vbNewLine)

                                Combobox1.Items.Add(New ComboBoxIconItem(AProgressBar.Tag, 9))
                                Combobox1.Text = AProgressBar.Tag


                                ' AProgressBar.BringToFront()

                                If AProgressBar.Location.X < 0 Then AProgressBar.Location = New Point(0, AProgressBar.Location.Y)
                                If AProgressBar.Location.Y < 0 Then AProgressBar.Location = New Point(AProgressBar.Location.X, 0)
                                If AProgressBar.Location.X + AProgressBar.Size.Width > XofFormApp Then AProgressBar.Location = New Point(XofFormApp - AProgressBar.Size.Width, AProgressBar.Location.Y)
                                If AProgressBar.Location.Y + AProgressBar.Size.Height > YofFormApp Then AProgressBar.Location = New Point(AProgressBar.Location.X, YofFormApp - AProgressBar.Size.Height)


                                If PanelPl.Name = Panel5.Name Then
                                    onFormIs = True

                                Else
                                    onFormIs = False
                                    onPanelIs = RetriveInWhatPanelis(AProgressBar)
                                End If

                                SaveProj("<AProgressBar" & i & ">" & vbNewLine & "id=" & idControl & vbNewLine & "IsOnForm=" & onFormIs.ToString & vbNewLine & "IsOnPanel=" & onPanelIs &
                                         vbNewLine & "Name=" & AProgressBar.Tag & vbNewLine & "Location=" & AProgressBar.Location.ToString.Replace("X=", Nothing).Replace(",", ";").Replace("Y=", Nothing) & vbNewLine & "Size=" & AProgressBar.Size.ToString.Replace("Width=", Nothing).Replace(",", ";").Replace("Height=", Nothing).Replace(" ", Nothing) _
                                         & vbNewLine & "Enabled=True" & vbNewLine & "Value=0" & vbNewLine & "</AProgressBar" & i & ">" & vbNewLine, True, Combobox3.Text)


                                DoBool = True
                            Else
                                i += 1
                                ba9 += 1
                            End If

                        Loop Until DoBool = True
                        GenControl = AProgressBar
                        ' rc = New ResizeableControl(AProgressBar)
                        ComboboxImage1.Items.Add(New ComboBoxIconItem(AProgressBar.Tag, 9))
                    ' ClickGenControlsPropert(AProgressBar.Name)

                    Case Is = ListView1.Items.Item(10).Text '"   RadioButton"
                        ARadioButton = New RadioButton
                        ba10 = 1
                        ba10 -= cntrafe10
                        Dim i As Integer = 1
                        Dim DoBool As Boolean = False

                        Do
                            If listbool("ARadioButton" & i, RadioButtonString) = True Then


                                Do
                                    For w = 0 To Combobox1.Items.Count - 1 Step 1
                                        If "RadioButton" & ba10 = Combobox1.Items.Item(w).ToString Then it_is_not_in_Combo = False : ba10 += 1 : Exit For Else it_is_not_in_Combo = True
                                    Next
                                Loop Until it_is_not_in_Combo = True


                                With ARadioButton
                                    .Enabled = True
                                    .BackColor = Color.Transparent 'System.Drawing.Color.FromArgb(235, 235, 235)
                                    .Name = "ARadioButton" & i
                                    .Text = "RadioButton" & ba10
                                    .Size() = New System.Drawing.Size(XofFormApp / 240 * 116, YofFormApp / 425 * 22)
                                    ' .BringToFront()
                                    .Tag = "RadioButton" & ba10
                                    .Location = New System.Drawing.Point(e.Location.X - ARadioButton.Size.Width / 2, e.Location.Y - ARadioButton.Size.Height / 2)
                                    .Font = New Font(Font.FontFamily, 10)
                                    .FlatStyle = FlatStyle.Flat

                                End With

                                PanelPl.Controls.Add(ARadioButton)
                                RadioButtonString &= ("ARadioButton" & i & vbNewLine)

                                Combobox1.Items.Add(New ComboBoxIconItem(ARadioButton.Tag, 10))
                                Combobox1.Text = ARadioButton.Tag


                                '        ARadioButton.BringToFront()

                                If ARadioButton.Location.X < 0 Then ARadioButton.Location = New Point(0, ARadioButton.Location.Y)
                                If ARadioButton.Location.Y < 0 Then ARadioButton.Location = New Point(ARadioButton.Location.X, 0)
                                If ARadioButton.Location.X + ARadioButton.Size.Width > XofFormApp Then ARadioButton.Location = New Point(XofFormApp - ARadioButton.Size.Width, ARadioButton.Location.Y)
                                If ARadioButton.Location.Y + ARadioButton.Size.Height > YofFormApp Then ARadioButton.Location = New Point(ARadioButton.Location.X, YofFormApp - ARadioButton.Size.Height)


                                If PanelPl.Name = Panel5.Name Then
                                    onFormIs = True
                                Else
                                    onFormIs = False
                                    onPanelIs = RetriveInWhatPanelis(ARadioButton)
                                End If

                                SaveProj("<ARadioButton" & i & ">" & vbNewLine & "id=" & idControl & vbNewLine & "IsOnForm=" & onFormIs.ToString & vbNewLine & "IsOnPanel=" & onPanelIs & vbNewLine & "Name=" & ARadioButton.Tag & vbNewLine & "Text=" & ARadioButton.Text &
                                          vbNewLine & "FontBold=False" & vbNewLine & "FontSize=" & ARadioButton.Font.Size & vbNewLine & "FontItalic=False" & vbNewLine &
                                          "FontTypeface=DEFAULT" & vbNewLine & "TextAlign=LEFT_CENTER" & vbNewLine & "BackColor={" & ARadioButton.BackColor.A & ";" & ARadioButton.BackColor.R & ";" & ARadioButton.BackColor.G & ";" & ARadioButton.BackColor.B & "}" & vbNewLine _
                                          & "ForeColor={" & ARadioButton.ForeColor.A & ";" & ARadioButton.ForeColor.R & ";" & ARadioButton.ForeColor.G & ";" & ARadioButton.ForeColor.B & "}" & vbNewLine & "Location=" & ARadioButton.Location.ToString.Replace("X=", Nothing).Replace(",", ";").Replace("Y=", Nothing) & vbNewLine & "Size=" & ARadioButton.Size.ToString.Replace("Width=", Nothing).Replace(",", ";").Replace("Height=", Nothing).Replace(" ", Nothing) _
                                          & vbNewLine & "Enabled=True" & vbNewLine & "Checked=False" & vbNewLine & "</ARadioButton" & i & ">" & vbNewLine, True, Combobox3.Text)


                                DoBool = True
                            Else
                                i += 1
                                ba10 += 1
                            End If
                        Loop Until DoBool = True
                        GenControl = ARadioButton
                        '  rc = New ResizeableControl(ARadioButton)
                        ComboboxImage1.Items.Add(New ComboBoxIconItem(ARadioButton.Tag, 10))
                    '  ClickGenControlsPropert(ARadioButton.Name)

                    Case Is = ListView1.Items.Item(11).Text ' "   TreeView"
                        Using New Centered_MessageBox(Me)
                            MsgBox("Is Not Supported yet.", MsgBoxStyle.Information)
                        End Using
                        MsgBox("Save your project and restart app because i have a Bug here when someone use this :P")
                    Case Is = ListView1.Items.Item(12).Text '"   TextBox"
                        ATextBox = New TextBox
                        ba11 = 1
                        ba11 -= cntrafe11
                        Dim i As Integer = 1 ' mporw na to kanw kai alios apla einai xronovoro kai anyway....(apla auto isos kolaei ligo polu ligo...)
                        Dim DoBool As Boolean = False

                        Do
                            If listbool("ATextBox" & i, TextBoxString) = True Then


                                Do
                                    For w = 0 To Combobox1.Items.Count - 1 Step 1
                                        If "TextBox" & ba11 = Combobox1.Items.Item(w).ToString Then it_is_not_in_Combo = False : ba11 += 1 : Exit For Else it_is_not_in_Combo = True
                                    Next
                                Loop Until it_is_not_in_Combo = True


                                With ATextBox
                                    .Enabled = True
                                    .BorderStyle = BorderStyle.None
                                    .BackColor = System.Drawing.Color.FromArgb(235, 235, 235)
                                    .Name = "ATextBox" & i
                                    .Text = "TextBox" & ba11
                                    .Size() = New System.Drawing.Size(XofFormApp / 240 * 190, YofFormApp / 425 * 23)
                                    ' .BringToFront()
                                    .Tag = "TextBox" & ba11 ' 8a einai to name gt twra to name einai enos kanonikou enov gia ta properties
                                    .Location = New System.Drawing.Point(e.Location.X - ATextBox.Size.Width / 2, e.Location.Y - ATextBox.Size.Height / 2)
                                    .Font = New Font(Font.FontFamily, 10)
                                    .Cursor = Cursors.NoMove2D
                                    .Multiline = True
                                    .ReadOnly = True
                                End With
                                ' Label3.Text = New System.Drawing.Point(e.Location.X - ATextBox.Size.Width / 2, e.Location.Y - ATextBox.Size.Height / 2).ToString
                                PanelPl.Controls.Add(ATextBox)
                                TextBoxString &= ("ATextBox" & i & vbNewLine)

                                Combobox1.Items.Add(New ComboBoxIconItem(ATextBox.Tag, 12))
                                Combobox1.Text = ATextBox.Tag


                                '    ATextBox.BringToFront()

                                If ATextBox.Location.X < 0 Then ATextBox.Location = New Point(0, ATextBox.Location.Y)
                                If ATextBox.Location.Y < 0 Then ATextBox.Location = New Point(ATextBox.Location.X, 0)
                                If ATextBox.Location.X + ATextBox.Size.Width > XofFormApp Then ATextBox.Location = New Point(XofFormApp - ATextBox.Size.Width, ATextBox.Location.Y)
                                If ATextBox.Location.Y + ATextBox.Size.Height > YofFormApp Then ATextBox.Location = New Point(ATextBox.Location.X, YofFormApp - ATextBox.Size.Height)

                                If PanelPl.Name = Panel5.Name Then
                                    onFormIs = True
                                Else
                                    onFormIs = False
                                    onPanelIs = RetriveInWhatPanelis(ATextBox)
                                End If

                                SaveProj("<ATextBox" & i & ">" & vbNewLine & "id=" & idControl & vbNewLine & "IsOnForm=" & onFormIs.ToString & vbNewLine & "IsOnPanel=" & onPanelIs & vbNewLine & "Name=" & ATextBox.Tag & vbNewLine & "Text=" & ATextBox.Text &
                                          vbNewLine & "FontBold=False" & vbNewLine & "FontSize=" & ATextBox.Font.Size & vbNewLine & "FontItalic=False" & vbNewLine &
                                          "FontTypeface=DEFAULT" & vbNewLine & "TextAlign=LEFT_TOP" & vbNewLine & "BackColor={" & ATextBox.BackColor.A & ";" & ATextBox.BackColor.R & ";" & ATextBox.BackColor.G & ";" & ATextBox.BackColor.B & "}" & vbNewLine _
                                          & "ForeColor={" & ATextBox.ForeColor.A & ";" & ATextBox.ForeColor.R & ";" & ATextBox.ForeColor.G & ";" & ATextBox.ForeColor.B & "}" & vbNewLine & "Hint=" & vbNewLine & "InputType=TEXTS" & vbNewLine & "Location=" & ATextBox.Location.ToString.Replace("X=", Nothing).Replace(",", ";").Replace("Y=", Nothing) & vbNewLine & "Size=" & ATextBox.Size.ToString.Replace("Width=", Nothing).Replace(",", ";").Replace("Height=", Nothing).Replace(" ", Nothing) _
                                          & vbNewLine & "Enabled=True" & vbNewLine & "</ATextBox" & i & ">" & vbNewLine, True, Combobox3.Text)


                                'TEXTS

                                DoBool = True
                            Else
                                i += 1
                                ba11 += 1
                            End If
                        Loop Until DoBool = True
                        GenControl = ATextBox
                        ' rc = New ResizeableControl(ATextBox)
                        ComboboxImage1.Items.Add(New ComboBoxIconItem(ATextBox.Tag, 12))
                    'ClickGenControlsPropert(ATextBox.Name)

                    Case Is = ListView1.Items.Item(13).Text '"   Timer"
                        ATimer = New PictureBox
                        ba12 = 1
                        ba12 -= cntrafe12
                        Dim i As Integer = 1
                        Dim DoBool As Boolean = False

                        AddHandler ATimer.MouseDown, AddressOf Me.ATimer_MouseDown
                        AddHandler ATimer.MouseUp, AddressOf Me.ATimer_MouseUp
                        AddHandler ATimer.MouseMove, AddressOf Me.ATimer_MouseMove

                        Do
                            If listbool("ATimer" & i, TimerString) = True Then


                                Do
                                    For w = 0 To Combobox1.Items.Count - 1 Step 1
                                        If "Timer" & ba12 = Combobox1.Items.Item(w).ToString Then it_is_not_in_Combo = False : ba12 += 1 : Exit For Else it_is_not_in_Combo = True
                                    Next
                                Loop Until it_is_not_in_Combo = True


                                With ATimer
                                    .Enabled = True
                                    .BackColor = Color.White
                                    .Name = "ATimer" & i
                                    .Size = New System.Drawing.Size(20, 20)
                                    .Tag = "Timer" & ba12
                                    .Location = New System.Drawing.Point(e.Location.X - ATimer.Size.Width / 2, e.Location.Y - ATimer.Size.Height / 2)
                                    ' .Font = New Font(ATimer.Font.FontFamily, 10)
                                    .BorderStyle = BorderStyle.FixedSingle
                                    .SizeMode = PictureBoxSizeMode.CenterImage
                                    .Image = My.Resources.Timer
                                End With

                                PanelPl.Controls.Add(ATimer)
                                TimerString &= ("ATimer" & i & vbNewLine)

                                Combobox1.Items.Add(New ComboBoxIconItem(ATimer.Tag, 13))
                                Combobox1.Text = ATimer.Tag


                                ATimer.BringToFront()

                                If ATimer.Location.X < 0 Then ATimer.Location = New Point(0, ATimer.Location.Y)
                                If ATimer.Location.Y < 0 Then ATimer.Location = New Point(ATimer.Location.X, 0)
                                If ATimer.Location.X + ATimer.Size.Width > XofFormApp Then ATimer.Location = New Point(XofFormApp - ATimer.Size.Width, ATimer.Location.Y)
                                If ATimer.Location.Y + ATimer.Size.Height > YofFormApp Then ATimer.Location = New Point(ATimer.Location.X, YofFormApp - ATimer.Size.Height)

                                If PanelPl.Name = Panel5.Name Then
                                    onFormIs = True
                                Else
                                    onFormIs = False
                                    onPanelIs = RetriveInWhatPanelis(ATimer)
                                End If

                                SaveProj("<ATimer" & i & ">" & vbNewLine & "id=" & idControl & vbNewLine & "IsOnForm=" & onFormIs.ToString & vbNewLine & "IsOnPanel=" & onPanelIs & vbNewLine & "Name=" & ATimer.Tag & vbNewLine & "Interval=100" & vbNewLine & "Location=" & ATimer.Location.ToString.Replace("X=", Nothing).Replace(",", ";").Replace("Y=", Nothing) & vbNewLine & "Enabled=False" & vbNewLine & "</ATimer" & i & ">" & vbNewLine, True, Combobox3.Text)


                                DoBool = True
                            Else
                                i += 1
                                ba12 += 1
                            End If
                        Loop Until DoBool = True
                        GenControl = ATimer
                        ComboboxImage1.Items.Add(New ComboBoxIconItem(ATimer.Tag, 13))
                    '  ClickGenControlsPropert(ATimer.Name)


                    Case Is = ListView1.Items.Item(14).Text '"   ComboBox"
                        AComboBox = New ComboBox
                        ba13 = 1
                        ba13 -= cntrafe13
                        Dim i As Integer = 1 ' mporw na to kanw kai alios apla einai xronovoro kai anyway....(apla auto isos kolaei ligo polu ligo...)
                        Dim DoBool As Boolean = False
                        ' AddHandler AComboBox.Click, AddressOf Me.AComboBox_Click


                        Do
                            If listbool("AComboBox" & i, ComboBoxString) = True Then


                                Do
                                    For w = 0 To Combobox1.Items.Count - 1 Step 1
                                        If "ComboBox" & ba13 = Combobox1.Items.Item(w).ToString Then it_is_not_in_Combo = False : ba13 += 1 Else it_is_not_in_Combo = True
                                    Next
                                Loop Until it_is_not_in_Combo = True


                                With AComboBox
                                    .Enabled = True
                                    .BackColor = System.Drawing.Color.FromArgb(235, 235, 235)
                                    .Name = "AComboBox" & i
                                    .Text = "ComboBox" & ba13
                                    .Size() = New System.Drawing.Size(XofFormApp / 240 * 190, YofFormApp / 425 * 21)
                                    ' .BringToFront()
                                    .Tag = "ComboBox" & ba13 ' 8a einai to name gt twra to name einai enos kanonikou enov gia ta properties
                                    .Location = New System.Drawing.Point(e.Location.X - AComboBox.Size.Width / 2, e.Location.Y - AComboBox.Size.Height / 2)
                                    .Font = New Font(Font.FontFamily, 10)
                                    .FlatStyle = FlatStyle.Flat
                                    .DropDownStyle = ComboBoxStyle.DropDown

                                End With
                                ' Label3.Text = New System.Drawing.Point(e.Location.X - AComboBox.Size.Width / 2, e.Location.Y - AComboBox.Size.Height / 2).ToString
                                PanelPl.Controls.Add(AComboBox)
                                ComboBoxString &= ("AComboBox" & i & vbNewLine)


                                Combobox1.Items.Add(New ComboBoxIconItem(AComboBox.Tag, 14))
                                Combobox1.Text = AComboBox.Tag


                                '  AComboBox.BringToFront()

                                If AComboBox.Location.X < 0 Then AComboBox.Location = New Point(0, AComboBox.Location.Y)
                                If AComboBox.Location.Y < 0 Then AComboBox.Location = New Point(AComboBox.Location.X, 0)
                                If AComboBox.Location.X + AComboBox.Size.Width > XofFormApp Then AComboBox.Location = New Point(XofFormApp - AComboBox.Size.Width, AComboBox.Location.Y)
                                If AComboBox.Location.Y + AComboBox.Size.Height > YofFormApp Then AComboBox.Location = New Point(AComboBox.Location.X, YofFormApp - AComboBox.Size.Height)


                                If PanelPl.Name = Panel5.Name Then
                                    onFormIs = True
                                Else
                                    onFormIs = False
                                    onPanelIs = RetriveInWhatPanelis(AComboBox)
                                End If

                                SaveProj("<AComboBox" & i & ">" & vbNewLine & "id=" & idControl & vbNewLine & "IsOnForm=" & onFormIs.ToString & vbNewLine & "IsOnPanel=" & onPanelIs & vbNewLine & "Name=" & AComboBox.Tag & vbNewLine & "Text=" & AComboBox.Text &
                                          vbNewLine & "FontBold=False" & vbNewLine & "FontSize=" & AComboBox.Font.Size & vbNewLine & "FontItalic=False" & vbNewLine &
                                          "FontTypeface=DEFAULT" & vbNewLine & "TextAlign=CENTER_CENTER" & vbNewLine & "BackColor={" & AComboBox.BackColor.A & ";" & AComboBox.BackColor.R & ";" & AComboBox.BackColor.G & ";" & AComboBox.BackColor.B & "}" & vbNewLine _
                                          & "ForeColor={" & AComboBox.ForeColor.A & ";" & AComboBox.ForeColor.R & ";" & AComboBox.ForeColor.G & ";" & AComboBox.ForeColor.B & "}" & vbNewLine & "Location=" & AComboBox.Location.ToString.Replace("X=", Nothing).Replace(",", ";").Replace("Y=", Nothing) & vbNewLine & "Size=" & AComboBox.Size.ToString.Replace("Width=", Nothing).Replace(",", ";").Replace("Height=", Nothing).Replace(" ", Nothing) _
                                          & vbNewLine & "Enabled=True" & vbNewLine & "</AComboBox" & i & ">" & vbNewLine, True, Combobox3.Text)


                                DoBool = True
                            Else
                                i += 1
                                ba13 += 1
                            End If
                        Loop Until DoBool = True
                        GenControl = AComboBox
                        ' rc = New ResizeableControl(AComboBox)
                        ComboboxImage1.Items.Add(New ComboBoxIconItem(AComboBox.Tag, 14))
                    ' ClickGenControlsPropert(AComboBox.Name)

                    Case Is = ListView1.Items.Item(15).Text '"   VB4AWeb"
                        VB4AWeb = New PictureBox
                        ba14 = 1
                        ba14 -= cntrafe14
                        Dim i As Integer = 1 ' mporw na to kanw kai alios apla einai xronovoro kai anyway....(apla auto isos kolaei ligo polu ligo...)
                        Dim DoBool As Boolean = False

                        Do
                            If listbool("VB4AWeb" & i, VB4AWebString) = True Then


                                Do
                                    For w = 0 To Combobox1.Items.Count - 1 Step 1
                                        If "VB4AWeb" & ba14 = Combobox1.Items.Item(w).ToString Then it_is_not_in_Combo = False : ba14 += 1 : Exit For Else it_is_not_in_Combo = True
                                    Next
                                Loop Until it_is_not_in_Combo = True


                                With VB4AWeb
                                    .Enabled = True
                                    .BackColor = Color.White
                                    .Name = "VB4AWeb" & i
                                    .Text = "VB4AWeb" & ba14
                                    .Size() = New System.Drawing.Size(XofFormApp / 240 * 104, YofFormApp / 425 * 161)
                                    ' .BringToFront()
                                    .Tag = "VB4AWeb" & ba14 ' 8a einai to name gt twra to name einai enos kanonikou enov gia ta properties
                                    .Location = New System.Drawing.Point(e.Location.X - VB4AWeb.Size.Width / 2, e.Location.Y - VB4AWeb.Size.Height / 2)
                                    ' .Font = New Font(VB4AWeb.Font.FontFamily, 10)
                                    .BorderStyle = BorderStyle.FixedSingle
                                    .SizeMode = PictureBoxSizeMode.CenterImage
                                    .Image = My.Resources.browser16__1_
                                End With
                                ' Label3.Text = New System.Drawing.Point(e.Location.X - VB4AWeb.Size.Width / 2, e.Location.Y - VB4AWeb.Size.Height / 2).ToString
                                PanelPl.Controls.Add(VB4AWeb)

                                ' APanel.Controls.Remove(VB4AWeb)
                                ' Panel5.Controls.Add(VB4AWeb)

                                VB4AWebString &= ("VB4AWeb" & i & vbNewLine)


                                Combobox1.Items.Add(New ComboBoxIconItem(VB4AWeb.Tag, 15))
                                Combobox1.Text = VB4AWeb.Tag


                                'VB4AWeb.BringToFront()

                                If VB4AWeb.Location.X < 0 Then VB4AWeb.Location = New Point(0, VB4AWeb.Location.Y)
                                If VB4AWeb.Location.Y < 0 Then VB4AWeb.Location = New Point(VB4AWeb.Location.X, 0)
                                If VB4AWeb.Location.X + VB4AWeb.Size.Width > XofFormApp Then VB4AWeb.Location = New Point(XofFormApp - VB4AWeb.Size.Width, VB4AWeb.Location.Y)
                                If VB4AWeb.Location.Y + VB4AWeb.Size.Height > YofFormApp Then VB4AWeb.Location = New Point(VB4AWeb.Location.X, YofFormApp - VB4AWeb.Size.Height)

                                If PanelPl.Name = Panel5.Name Then
                                    onFormIs = True
                                Else
                                    onFormIs = False
                                    onPanelIs = RetriveInWhatPanelis(VB4AWeb)
                                End If

                                SaveProj("<VB4AWeb" & i & ">" & vbNewLine & "id=" & idControl & vbNewLine & "IsOnForm=" & onFormIs.ToString & vbNewLine & "IsOnPanel=" & onPanelIs & vbNewLine _
                                         & "Name=" & VB4AWeb.Tag & vbNewLine & "SavePassword=False" & vbNewLine & "SaveFromData=False" & vbNewLine & "JSEnabled=True" & vbNewLine & "ZoomEnabled=True" _
                                         & vbNewLine & "BuildinZoom=False" & vbNewLine & "Location=" & VB4AWeb.Location.ToString.Replace("X=", Nothing).Replace(",", ";").Replace("Y=", Nothing) & vbNewLine & "Size=" & VB4AWeb.Size.ToString.Replace("Width=", Nothing).Replace(",", ";").Replace("Height=", Nothing).Replace(" ", Nothing) _
                                         & vbNewLine & "Enabled=True" & vbNewLine & "</VB4AWeb" & i & ">" & vbNewLine, True, Combobox3.Text)


                                DoBool = True
                            Else
                                i += 1
                                ba14 += 1
                            End If
                        Loop Until DoBool = True
                        GenControl = VB4AWeb

                        ComboboxImage1.Items.Add(New ComboBoxIconItem(VB4AWeb.Tag, 15))

                End Select
                'If not GenControl is nothing because i have some controls that tey are not supported... :P
                If Not GenControl Is Nothing AndAlso Not GenControl.Name.StartsWith("ATimer") Then


                    rc = New ResizeableControl(GenControl)
                    c = GenControl


                    'If RetriveInWhatPanelis(c) = Form1.Panel5.Name Then pnl = Form1.Panel5 Else pnl = Form1.FindControl(Form1.RetriveInWhatPanelis(c), Form1.Panel5)
                    If RetriveInWhatPanelis(c) = Panel5.Name Then Panel5.Controls.Add(TransparentDrawing1) : pnl = Panel5 Else FindControl(RetriveInWhatPanelis(c), Panel5).Controls.Add(TransparentDrawing1) : pnl = FindControl(RetriveInWhatPanelis(c), Panel5)

                    With TransparentDrawing1
                        .Hide()
                        .Location = New Point(GenControl.Location.X - 7, GenControl.Location.Y - 7)
                        .Size = New Size(GenControl.Width + 14, GenControl.Height + 14)
                        .BringToFront()
                        .Show()
                        .Refresh()
                    End With
                    GenControl.BringToFront()


                    ' mMouseDown = False
                    Dim j As Graphics = TransparentDrawing1.CreateGraphics
                    'TransparentDrawing1.Refresh()
                    'Form1.PictureBox20.BackColor = Color.Transparent

                    j.FillRectangle(colorBrushDG, 7 - 5, 7 - 4, 1, GenControl.Size.Height + 7) ' Left
                    j.FillRectangle(colorBrushDG, 7 + GenControl.Size.Width + 4, 7 - 4, 1, GenControl.Size.Height + 7) ' Right

                    j.FillRectangle(colorBrushDG, 7 - 4, 7 - 5, GenControl.Size.Width + 7, 1) 'Up
                    j.FillRectangle(colorBrushDG, 7 - 4, 7 + GenControl.Size.Height + 4, GenControl.Size.Width + 7, 1) ' down

                    j.FillRectangle(colorBrushB, 7 - 7, 7 - 7, 5, 5) ' LEFT UP CORNER
                    j.FillRectangle(colorBrushB, 7 - 7, 7 + GenControl.Size.Height + 2, 5, 5) ' LEFT DOWN CORNER

                    j.FillRectangle(colorBrushB, 7 + GenControl.Size.Width + 2, 7 - 7, 5, 5) ' RIGHT UP CORNER
                    j.FillRectangle(colorBrushB, 7 + GenControl.Size.Width + 2, 7 + GenControl.Size.Height + 2, 5, 5) ' RIGHT DOWN CORNER 

                    BuildAutocompleteMenu()

                End If


                If Not GenControl Is Nothing Then ClickGenControlsPropert(GenControl.Name)

                ListView1.SelectedItems(0).Selected = False
                ' ListView1.Hide()
            Catch ex As ArgumentOutOfRangeException ' Form
                ' MsgBox(idControl)
                idControl -= 1


                Combobox1.Text = Combobox3.Text.Replace(".vb4a", Nothing)
                formisProp()
            End Try


        ElseIf e.Button = Windows.Forms.MouseButtons.Right Then


        End If
    End Sub


    Public Shared Sub DRWTran()
        With Form1.TransparentDrawing1
            .Hide()
            .Location = New Point(Form1.GenControl.Location.X - 7, Form1.GenControl.Location.Y - 7)
            .Size = New Size(Form1.GenControl.Width + 14, Form1.GenControl.Height + 14)
            .BringToFront()
            .Show()
            .Refresh()
        End With
        Form1.GenControl.BringToFront()

        Dim j As Graphics = Form1.TransparentDrawing1.CreateGraphics
        'Form1.TransparentDrawing1.Refresh()
        'Form1.PictureBox20.BackColor = Color.Transparent

        j.FillRectangle(Form1.colorBrushDG, 7 - 5, 7 - 4, 1, Form1.GenControl.Size.Height + 7) ' Left
        j.FillRectangle(Form1.colorBrushDG, 7 + Form1.GenControl.Size.Width + 4, 7 - 4, 1, Form1.GenControl.Size.Height + 7) ' Right

        j.FillRectangle(Form1.colorBrushDG, 7 - 4, 7 - 5, Form1.GenControl.Size.Width + 7, 1) 'Up
        j.FillRectangle(Form1.colorBrushDG, 7 - 4, 7 + Form1.GenControl.Size.Height + 4, Form1.GenControl.Size.Width + 7, 1) ' down

        j.FillRectangle(Form1.colorBrushB, 7 - 7, 7 - 7, 5, 5) ' LEFT UP CORNER
        j.FillRectangle(Form1.colorBrushB, 7 - 7, 7 + Form1.GenControl.Size.Height + 2, 5, 5) ' LEFT DOWN CORNER

        j.FillRectangle(Form1.colorBrushB, 7 + Form1.GenControl.Size.Width + 2, 7 - 7, 5, 5) ' RIGHT UP CORNER
        j.FillRectangle(Form1.colorBrushB, 7 + Form1.GenControl.Size.Width + 2, 7 + Form1.GenControl.Size.Height + 2, 5, 5) ' RIGHT DOWN CORNER 

        ' j.ResetTransform()
        ' mMouseDown = False

    End Sub



    Private Sub Panel5_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles Panel5.MouseDoubleClick
        If Not Label4.Text = "" Then
            If Not Textbox3.Text.Contains("Event " & Combobox3.Text.Replace(".vb4a", Nothing) & ".Initialize()") Then
                Textbox3.Text &= vbNewLine & "Event " & Combobox3.Text.Replace(".vb4a", Nothing) & ".Initialize()" & vbNewLine & "    " & vbNewLine & "End Event" & vbNewLine
                For Each r In Textbox3.Range.GetRanges("Event " & Combobox3.Text.Replace(".vb4a", Nothing) & ".Initialize()")
                    Textbox3.Selection = r
                    Textbox3.DoSelectionVisible()
                Next
            ElseIf Textbox3.Text.Contains("Event " & Combobox3.Text.Replace(".vb4a", Nothing) & ".Initialize()") Then
                For Each r In Textbox3.Range.GetRanges("Event " & Combobox3.Text.Replace(".vb4a", Nothing) & ".Initialize()")
                    Textbox3.Selection = r
                    Textbox3.DoSelectionVisible()
                Next
            End If
            ToolStripButton6.PerformClick()
        End If

    End Sub
    Private Sub Panel5_MouseClick(sender As Object, e As MouseEventArgs) Handles Panel5.MouseClick ' nai nai 3erw mporousa na to kanw kai alios me function kai subs kai 8a eperne ligotero xoro xD apla vargiomoun! siga more!! gia liga kb xoro!
        'If Not GenControl Is Nothing AndAlso GenControl.Name.StartsWith("AButton") Then bbt11.Select()
        Panel5.Select() ' i am using this because of the Button's borders
        TransparentDrawing1.Hide()
        If Not pnl Is Nothing Then pnl.Refresh()
        AddControl(e, Panel5)
    End Sub '         otan alazei foorm na kanw hideto transparentDrawing1 ~~~~~~~~~~~~~~~~~~~~~~~~!!!! kai na diagrapsw ta events

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub


    Private Sub Panel5_Paint(sender As Object, e As PaintEventArgs) Handles Panel5.Paint

    End Sub




    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click

    End Sub

    Private Sub PropertyGrid1_Click(sender As Object, e As EventArgs)

    End Sub
    Public cmb As Object = Combobox1.SelectedItem
    Public tagsForCombo As String = Nothing

    Private Sub Combobox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Combobox1.SelectedIndexChanged

        If Not Combobox1.Text = Combobox3.Text.Replace(".vb4a", Nothing) AndAlso Combobox1.SelectedItem IsNot Nothing Then
            Dim whatContis As String = String.Empty

            '  If IsCGCPorNot = True Then ' IsCGCPorNot = Is ClickGenControlsPro.....
            'IsCGCPorNot = False
            ' ElseIf IsCGCPorNot = False Then


            Select Case Combobox1.Items.Item(Combobox1.SelectedIndex).ImageIndex
                Case Is = 0 : whatContis = "AButton" 'Button
                Case Is = 1 : whatContis = "Canvas" 'Canvas
                Case Is = 2 : whatContis = "ACheckBox" 'CheckBox
                Case Is = 4 : whatContis = "ALabel" 'Label
                Case Is = 5 : whatContis = "AListview" 'ListView
                Case Is = 6 : whatContis = "APanel" 'Panel
                Case Is = 7 : whatContis = "PasswordTextBox" 'PasswordTextBox
                Case Is = 8 : whatContis = "APictureBox" 'PictureBox Image
                Case Is = 9 : whatContis = "AProgressBar" 'ProgressBar
                Case Is = 10 : whatContis = "ARadioButton" 'RadioButton
                Case Is = 12 : whatContis = "ATextBox" 'TextBox
                Case Is = 13 : whatContis = "ATimer" 'Timer
                Case Is = 14 : whatContis = "AComboBox" 'ComboBox VB4ASpinner
                Case Is = 15 : whatContis = "VB4AWeb" 'VB4AWeb
            End Select

            '''' ' ClickGenControlsPropert__SPECIALFORCOMB1(Combobox1.SelectedItem.ToString)   ' na 3erw malakia mou alla den paei kala h vb LoL otan vazw Boolean gia auto

            Dim StartEnd As Boolean = False
            Dim contnam As String = String.Empty ' i know i could use only whatContis.... i am boring to change now :P 
            cmb = Combobox1.SelectedItem

            For Each Line As String In ProjectSaveString.Split(vbNewLine)
                Line = Line.Replace(Chr(10), "")

                If Line.StartsWith("<" & whatContis) Then StartEnd = True : contnam = Line.Replace("<", "").Replace(">", "")

                If StartEnd = True Then
                    If Line.StartsWith("Name=") Then
                        If Line.Replace("Name=", "") = Combobox1.SelectedItem.ToString Then

                            GenControl = FindControl(contnam, Panel5) : If isusedGEn = False Then ClickGenControlsPropert(GenControl.Name)

                            ' it is faster and bettter than calling DRWTran() funtion
                            If Not GenControl.Name.StartsWith("ATimer") Then
                                If RetriveInWhatPanelis(GenControl) = Panel5.Name Then Panel5.Controls.Add(TransparentDrawing1) Else FindControl(RetriveInWhatPanelis(GenControl), Panel5).Controls.Add(TransparentDrawing1)
                                With TransparentDrawing1
                                    .Hide()
                                    .BringToFront()
                                    .Location = New Point(GenControl.Location.X - 7, GenControl.Location.Y - 7)
                                    .Size = New Size(GenControl.Width + 14, GenControl.Height + 14)
                                    .Show()
                                    .Refresh()
                                End With
                                GenControl.Select()
                                GenControl.Refresh()
                                GenControl.BringToFront()

                                Dim j As Graphics = TransparentDrawing1.CreateGraphics
                                'TransparentDrawing1.Refresh()
                                'PictureBox20.BackColor = Color.Transparent

                                j.FillRectangle(colorBrushDG, 7 - 5, 7 - 4, 1, GenControl.Size.Height + 7) ' Left
                                j.FillRectangle(colorBrushDG, 7 + GenControl.Size.Width + 4, 7 - 4, 1, GenControl.Size.Height + 7) ' Right

                                j.FillRectangle(colorBrushDG, 7 - 4, 7 - 5, GenControl.Size.Width + 7, 1) 'Up
                                j.FillRectangle(colorBrushDG, 7 - 4, 7 + GenControl.Size.Height + 4, GenControl.Size.Width + 7, 1) ' down

                                j.FillRectangle(colorBrushB, 7 - 7, 7 - 7, 5, 5) ' LEFT UP CORNER
                                j.FillRectangle(colorBrushB, 7 - 7, 7 + GenControl.Size.Height + 2, 5, 5) ' LEFT DOWN CORNER

                                j.FillRectangle(colorBrushB, 7 + GenControl.Size.Width + 2, 7 - 7, 5, 5) ' RIGHT UP CORNER
                                j.FillRectangle(colorBrushB, 7 + GenControl.Size.Width + 2, 7 + GenControl.Size.Height + 2, 5, 5) ' RIGHT DOWN CORNER 

                            Else : TransparentDrawing1.Hide()
                            End If

                            Exit Sub
                            ' FindControl(contnam, Panel5).PerformClick() : Exit Sub

                        Else : StartEnd = False
                        End If
                    End If
                End If

            Next

            '   IsCGCPorNot = False
            ' End If



            '  MsgBox(PropertiesOfControl)
            'IsCGCPorNot = False
        Else ' einai to FORM Prop
            TransparentDrawing1.Hide()
            formisProp()             '          Exei Diko tou Properties pou legete   propertiesOfForm 
            '    IsCGCPorNot = False

        End If

    End Sub ' 294 to 65 lines 
    'Dim cGCPSpec As Boolean = True
    '    Private Sub ClickGenControlsPropert__SPECIALFORCOMB1(GnControl As String)
    '        If cGCPSpec = False Then
    '            cGCPSpec = True
    '        Else

    '            Dim ContrlFND As String = Nothing
    '            Dim IntHelp As Integer = 0
    '            Dim helpbool As Boolean = False
    '            '  MsgBox("-----------2" & vbNewLine & PropertiesOfControl)
    '            PropertiesOfControl = ""

    '            Dim controlEndProp As Boolean = False
    '            For Each Line As String In ProjectSaveString.Split(vbNewLine)
    '                Line = Line.Replace(Chr(10), "")

    '                ' If IntHelp = 0 Then


    '                If Line.StartsWith("<AButton") Then : PropertyGrid1.SelectedObject = New ClsButton : ContrlFND = Line.Replace("<", Nothing) : ContrlFND = ContrlFND.Replace(">", Nothing) : IntHelp = 1 : PropertiesOfControl = "<" & ContrlFND & ">" & vbNewLine : GenControl = FindControl(ContrlFND, Panel5)
    '                ElseIf Line.StartsWith("<Canvas") Then : PropertyGrid1.SelectedObject = New ClsCanvas : ContrlFND = Line.Replace("<", Nothing) : ContrlFND = ContrlFND.Replace(">", Nothing) : IntHelp = 2 : PropertiesOfControl = "<" & ContrlFND & ">" & vbNewLine : GenControl = FindControl(ContrlFND, Panel5)
    '                ElseIf Line.StartsWith("<ACheckBox") Then : PropertyGrid1.SelectedObject = New ClsCheckBox : ContrlFND = Line.Replace("<", Nothing) : ContrlFND = ContrlFND.Replace(">", Nothing) : IntHelp = 3 : PropertiesOfControl = "<" & ContrlFND & ">" & vbNewLine : GenControl = FindControl(ContrlFND, Panel5)
    '                ElseIf Line.StartsWith("<ALabel") Then : PropertyGrid1.SelectedObject = New ClsLabel : ContrlFND = Line.Replace("<", Nothing) : ContrlFND = ContrlFND.Replace(">", Nothing) : IntHelp = 4 : PropertiesOfControl = "<" & ContrlFND & ">" & vbNewLine : GenControl = FindControl(ContrlFND, Panel5)
    '                ElseIf Line.StartsWith("<AListView") Then : PropertyGrid1.SelectedObject = New ClsListView : ContrlFND = Line.Replace("<", Nothing) : ContrlFND = ContrlFND.Replace(">", Nothing) : IntHelp = 5 : PropertiesOfControl = "<" & ContrlFND & ">" & vbNewLine : GenControl = FindControl(ContrlFND, Panel5)
    '                ElseIf Line.StartsWith("<APanel") Then : PropertyGrid1.SelectedObject = New clsPanel : ContrlFND = Line.Replace("<", Nothing) : ContrlFND = ContrlFND.Replace(">", Nothing) : IntHelp = 6 : PropertiesOfControl = "<" & ContrlFND & ">" & vbNewLine : GenControl = FindControl(ContrlFND, Panel5)
    '                ElseIf Line.StartsWith("<PasswordTextBox") Then : PropertyGrid1.SelectedObject = New clsPasswordTextBox : ContrlFND = Line.Replace("<", Nothing) : ContrlFND = ContrlFND.Replace(">", Nothing) : IntHelp = 7 : PropertiesOfControl = "<" & ContrlFND & ">" & vbNewLine : GenControl = FindControl(ContrlFND, Panel5)
    '                ElseIf Line.StartsWith("<APictureBox") Then : PropertyGrid1.SelectedObject = New clsPictureBox : ContrlFND = Line.Replace("<", Nothing) : ContrlFND = ContrlFND.Replace(">", Nothing) : IntHelp = 8 : PropertiesOfControl = "<" & ContrlFND & ">" & vbNewLine : GenControl = FindControl(ContrlFND, Panel5)
    '                ElseIf Line.StartsWith("<AProgressBar") Then : PropertyGrid1.SelectedObject = New clsProgressBar : ContrlFND = Line.Replace("<", Nothing) : ContrlFND = ContrlFND.Replace(">", Nothing) : IntHelp = 9 : PropertiesOfControl = "<" & ContrlFND & ">" & vbNewLine : GenControl = FindControl(ContrlFND, Panel5)
    '                ElseIf Line.StartsWith("<ARadioButton") Then : PropertyGrid1.SelectedObject = New ClsRadioButton : ContrlFND = Line.Replace("<", Nothing) : ContrlFND = ContrlFND.Replace(">", Nothing) : IntHelp = 10 : PropertiesOfControl = "<" & ContrlFND & ">" & vbNewLine : GenControl = FindControl(ContrlFND, Panel5)
    '                ElseIf Line.StartsWith("<ATextBox") Then : PropertyGrid1.SelectedObject = New clsTextBox : ContrlFND = Line.Replace("<", Nothing) : ContrlFND = ContrlFND.Replace(">", Nothing) : IntHelp = 11 : PropertiesOfControl = "<" & ContrlFND & ">" & vbNewLine : GenControl = FindControl(ContrlFND, Panel5)
    '                ElseIf Line.StartsWith("<ATimer") Then : PropertyGrid1.SelectedObject = New clsTimer : ContrlFND = Line.Replace("<", Nothing) : ContrlFND = ContrlFND.Replace(">", Nothing) : IntHelp = 12 : PropertiesOfControl = "<" & ContrlFND & ">" & vbNewLine : GenControl = FindControl(ContrlFND, Panel5)
    '                ElseIf Line.StartsWith("<AComboBox") Then : PropertyGrid1.SelectedObject = New clsComboBox : ContrlFND = Line.Replace("<", Nothing) : ContrlFND = ContrlFND.Replace(">", Nothing) : IntHelp = 13 : PropertiesOfControl = "<" & ContrlFND & ">" & vbNewLine : GenControl = FindControl(ContrlFND, Panel5)
    '                ElseIf Line.StartsWith("<VB4AWeb") Then : PropertyGrid1.SelectedObject = New clsVB4AWeb : ContrlFND = Line.Replace("<", Nothing) : ContrlFND = ContrlFND.Replace(">", Nothing) : IntHelp = 14 : PropertiesOfControl = "<" & ContrlFND & ">" & vbNewLine : GenControl = FindControl(ContrlFND, Panel5)
    '                End If

    '                ' End If

    '                If Not ContrlFND = Nothing Then














    '                    If Line = "<" & ContrlFND & ">" Then controlEndProp = True
    '                    'if den einai </ kai control tote vale alios sub exit (alla 8a valw kai ena allo function h sub pou 8a pernei to control kai 8a 3erei amma einai button h kati allo ... malon)
    '                    If Line = "</" & ContrlFND & ">" Then controlEndProp = False
    '                    If Line = "</" & ContrlFND & ">" AndAlso helpbool = True Then PropertiesOfControl &= "</" & ContrlFND & ">" : Exit Sub


    '                    If controlEndProp = True Then
    '                        If Line = vbNewLine Then Line = Nothing
    '                        If Line.StartsWith("<" & ContrlFND) Then NameOfControlBeforeClick = Line.Replace("<", "") : NameOfControlBeforeClick = NameOfControlBeforeClick.Replace(">", "") : Line = Nothing

    '                        If Not Line = Nothing Then PropertiesOfControl &= Line & vbNewLine

    '                        Dim ContPoints As String = Nothing

    '                        If Not Line = Nothing Then


    '                            If ContrlFND.StartsWith("AButton") Or IntHelp = 1 Then

    '                                If Line.StartsWith("Name=" & GnControl) Then : ClsButton._Name = Line.Replace("Name=", Nothing) : Combobox1.Text = Line.Replace("Name=", "") : helpbool = True
    '                                ElseIf Line.StartsWith("Name=") AndAlso Not Line.StartsWith("Name=" & GnControl) Then : PropertiesOfControl = Nothing
    '                                ElseIf Line.StartsWith("Text=") Then : ClsButton._Text = Line.Replace("Text=", Nothing)
    '                                ElseIf Line.StartsWith("FontBold=") Then : ClsButton._bold = Line.Replace("FontBold=", Nothing)
    '                                ElseIf Line.StartsWith("FontSize=") Then : ClsButton._Font_Size = Line.Replace("FontSize=", Nothing)
    '                                ElseIf Line.StartsWith("FontItalic=") Then : ClsButton._Italic = Line.Replace("FontItalic=", Nothing)
    '                                ElseIf Line.StartsWith("FontTypeface=") Then : ClsButton._FontTypeface = Line.Replace("FontTypeface=", Nothing)
    '                                ElseIf Line.StartsWith("TextAlign=") Then : ClsButton._TextAlign = Line.Replace("TextAlign=", Nothing)
    '                                ElseIf Line.StartsWith("BackColor=") Then : ClsButton._BackColor = ColourFromData(Line.Replace("BackColor=", Nothing))
    '                                ElseIf Line.StartsWith("ForeColor=") Then : ClsButton._ForeColor = ColourFromData(Line.Replace("ForeColor=", Nothing))
    '                                ElseIf Line.StartsWith("Location=") Then : ContPoints = Line.Replace("Location={", Nothing).Replace("}", Nothing) : ClsButton._Location = New Point(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
    '                                ElseIf Line.StartsWith("Size=") Then : ContPoints = Line.Replace("Size={", Nothing).Replace("}", Nothing) : ClsButton._Size = New Size(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
    '                                ElseIf Line.StartsWith("Image=") Then : If Line.Replace("Image=", Nothing) = Nothing Then : ClsButton._Image = Nothing : Else : ClsButton._Image = Line.Replace("Image=", Nothing) : End If ' Image.FromFile(
    '                                ElseIf Line.StartsWith("Enabled=") Then : ClsButton._Enabled = Line.Replace("Enabled=", Nothing)
    '                                End If


    '                            ElseIf ContrlFND.StartsWith("Canvas") Or IntHelp = 2 Then

    '                                If Line.StartsWith("Name=" & GnControl) Then : ClsCanvas._Name = Line.Replace("Name=", Nothing) : Combobox1.Text = Line.Replace("Name=", "") : helpbool = True
    '                                ElseIf Line.StartsWith("Name=") AndAlso Not Line.StartsWith("Name=" & GnControl) Then : PropertiesOfControl = Nothing
    '                                ElseIf Line.StartsWith("BackgroundColor=") Then : ClsCanvas._BackgroundColor = ColourFromData(Line.Replace("BackgroundColor=", Nothing))
    '                                ElseIf Line.StartsWith("Location=") Then : ContPoints = Line.Replace("Location={", Nothing).Replace("}", Nothing) : ClsCanvas._Location = New Point(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
    '                                ElseIf Line.StartsWith("Size=") Then : ContPoints = Line.Replace("Size={", Nothing).Replace("}", Nothing) : ClsCanvas._Size = New Size(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
    '                                ElseIf Line.StartsWith("BackgroundImage=") Then : If Line.Replace("BackgroundImage=", Nothing) = Nothing Then : ClsCanvas._BackgroundImage = Nothing : Else : ClsCanvas._BackgroundImage = Line.Replace("BackgroundImage=", Nothing) : End If
    '                                ElseIf Line.StartsWith("Enabled=") Then : ClsCanvas._Enabled = Line.Replace("Enabled=", Nothing)
    '                                ElseIf Line.StartsWith("PointColor=") Then : ClsCanvas._PointColor = ColourFromData(Line.Replace("PointColor=", Nothing))
    '                                End If

    '                            ElseIf ContrlFND.StartsWith("ACheckBox") Or IntHelp = 3 Then

    '                                If Line.StartsWith("Name=" & GnControl) Then : ClsCheckBox._Name = Line.Replace("Name=", Nothing) : Combobox1.Text = Line.Replace("Name=", "") : helpbool = True
    '                                ElseIf Line.StartsWith("Name=") AndAlso Not Line.StartsWith("Name=" & GnControl) Then : PropertiesOfControl = Nothing
    '                                ElseIf Line.StartsWith("Text=") Then : ClsCheckBox._Text = Line.Replace("Text=", Nothing)
    '                                ElseIf Line.StartsWith("FontBold=") Then : ClsCheckBox._bold = Line.Replace("FontBold=", Nothing)
    '                                ElseIf Line.StartsWith("FontSize=") Then : ClsCheckBox._Font_Size = Line.Replace("FontSize=", Nothing)
    '                                ElseIf Line.StartsWith("FontItalic=") Then : ClsCheckBox._Italic = Line.Replace("FontItalic=", Nothing)
    '                                ElseIf Line.StartsWith("FontTypeface=") Then : ClsCheckBox._FontTypeface = Line.Replace("FontTypeface=", Nothing)
    '                                ElseIf Line.StartsWith("TextAlign=") Then : ClsCheckBox._TextAlign = Line.Replace("TextAlign=", Nothing)
    '                                ElseIf Line.StartsWith("BackColor=") Then : ClsCheckBox._BackColor = ColourFromData(Line.Replace("BackColor=", Nothing))
    '                                ElseIf Line.StartsWith("ForeColor=") Then : ClsCheckBox._ForeColor = ColourFromData(Line.Replace("ForeColor=", Nothing))
    '                                ElseIf Line.StartsWith("Location=") Then : ContPoints = Line.Replace("Location={", Nothing).Replace("}", Nothing) : ClsCheckBox._Location = New Point(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
    '                                ElseIf Line.StartsWith("Size=") Then : ContPoints = Line.Replace("Size={", Nothing).Replace("}", Nothing) : ClsCheckBox._Size = New Size(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
    '                                ElseIf Line.StartsWith("Checked=") Then : ClsCheckBox._Checked = Line.Replace("Checked=", Nothing)
    '                                ElseIf Line.StartsWith("Enabled=") Then : ClsCheckBox._Enabled = Line.Replace("Enabled=", Nothing)
    '                                End If

    '                            ElseIf ContrlFND.StartsWith("ALabel") Or IntHelp = 4 Then

    '                                If Line.StartsWith("Name=" & GnControl) Then : ClsLabel._Name = Line.Replace("Name=", Nothing) : Combobox1.Text = Line.Replace("Name=", "") : helpbool = True
    '                                ElseIf Line.StartsWith("Name=") AndAlso Not Line.StartsWith("Name=" & GnControl) Then : PropertiesOfControl = Nothing
    '                                ElseIf Line.StartsWith("Text=") Then : ClsLabel._Text = Line.Replace("Text=", Nothing)
    '                                ElseIf Line.StartsWith("FontBold=") Then : ClsLabel._bold = Line.Replace("FontBold=", Nothing)
    '                                ElseIf Line.StartsWith("FontSize=") Then : ClsLabel._Font_Size = Line.Replace("FontSize=", Nothing)
    '                                ElseIf Line.StartsWith("FontItalic=") Then : ClsLabel._Italic = Line.Replace("FontItalic=", Nothing)
    '                                ElseIf Line.StartsWith("FontTypeface=") Then : ClsLabel._FontTypeface = Line.Replace("FontTypeface=", Nothing)
    '                                ElseIf Line.StartsWith("TextAlign=") Then : ClsLabel._TextAlign = Line.Replace("TextAlign=", Nothing)
    '                                ElseIf Line.StartsWith("BackColor=") Then : ClsLabel._BackColor = ColourFromData(Line.Replace("BackColor=", Nothing))
    '                                ElseIf Line.StartsWith("ForeColor=") Then : ClsLabel._ForeColor = ColourFromData(Line.Replace("ForeColor=", Nothing))
    '                                ElseIf Line.StartsWith("Location=") Then : ContPoints = Line.Replace("Location={", Nothing).Replace("}", Nothing) : ClsLabel._Location = New Point(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
    '                                ElseIf Line.StartsWith("Size=") Then : ContPoints = Line.Replace("Size={", Nothing).Replace("}", Nothing) : ClsLabel._Size = New Size(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
    '                                ElseIf Line.StartsWith("Enabled=") Then : ClsLabel._Enabled = Line.Replace("Enabled=", Nothing)
    '                                End If

    '                            ElseIf ContrlFND.StartsWith("AListView") Or IntHelp = 5 Then

    '                                If Line.StartsWith("Name=" & GnControl) Then : ClsListView._Name = Line.Replace("Name=", Nothing) : Combobox1.Text = Line.Replace("Name=", "") : helpbool = True
    '                                ElseIf Line.StartsWith("Name=") AndAlso Not Line.StartsWith("Name=" & GnControl) Then : PropertiesOfControl = Nothing
    '                                ElseIf Line.StartsWith("FontBold=") Then : ClsListView._bold = Line.Replace("FontBold=", Nothing)
    '                                ElseIf Line.StartsWith("FontSize=") Then : ClsListView._Font_Size = Line.Replace("FontSize=", Nothing)
    '                                ElseIf Line.StartsWith("FontItalic=") Then : ClsListView._Italic = Line.Replace("FontItalic=", Nothing)
    '                                ElseIf Line.StartsWith("FontTypeface=") Then : ClsListView._FontTypeface = Line.Replace("FontTypeface=", Nothing)
    '                                ElseIf Line.StartsWith("TextAlign=") Then : ClsListView._TextAlign = Line.Replace("TextAlign=", Nothing)
    '                                ElseIf Line.StartsWith("BackColor=") Then : ClsListView._BackColor = ColourFromData(Line.Replace("BackColor=", Nothing))
    '                                ElseIf Line.StartsWith("ForeColor=") Then : ClsListView._ForeColor = ColourFromData(Line.Replace("ForeColor=", Nothing))
    '                                ElseIf Line.StartsWith("Location=") Then : ContPoints = Line.Replace("Location={", Nothing).Replace("}", Nothing) : ClsListView._Location = New Point(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
    '                                ElseIf Line.StartsWith("Size=") Then : ContPoints = Line.Replace("Size={", Nothing).Replace("}", Nothing) : ClsListView._Size = New Size(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
    '                                ElseIf Line.StartsWith("Enabled=") Then : ClsListView._Enabled = Line.Replace("Enabled=", Nothing)
    '                                End If

    '                            ElseIf ContrlFND.StartsWith("APanel") Or IntHelp = 6 Then

    '                                If Line.StartsWith("Name=" & GnControl) Then : clsPanel._Name = Line.Replace("Name=", Nothing) : Combobox1.Text = Line.Replace("Name=", "") : helpbool = True
    '                                ElseIf Line.StartsWith("Name=") AndAlso Not Line.StartsWith("Name=" & GnControl) Then : PropertiesOfControl = Nothing
    '                                ElseIf Line.StartsWith("BackgroundColor=") Then : clsPanel._BackgroundColor = ColourFromData(Line.Replace("BackgroundColor=", Nothing))
    '                                ElseIf Line.StartsWith("Location=") Then : ContPoints = Line.Replace("Location={", Nothing).Replace("}", Nothing) : clsPanel._Location = New Point(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
    '                                ElseIf Line.StartsWith("Size=") Then : ContPoints = Line.Replace("Size={", Nothing).Replace("}", Nothing) : clsPanel._Size = New Size(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
    '                                ElseIf Line.StartsWith("BackgroundImage=") Then : If Line.Replace("BackgroundImage=", Nothing) = Nothing Then : clsPanel._BackgroundImage = Nothing : Else : clsPanel._BackgroundImage = Line.Replace("BackgroundImage=", Nothing) : End If
    '                                ElseIf Line.StartsWith("Enabled=") Then : clsPanel._Enabled = Line.Replace("Enabled=", Nothing)
    '                                End If

    '                            ElseIf ContrlFND.StartsWith("PasswordTextBox") Or IntHelp = 7 Then

    '                                If Line.StartsWith("Name=" & GnControl) Then : clsPasswordTextBox._Name = Line.Replace("Name=", Nothing) : Combobox1.Text = Line.Replace("Name=", "") : helpbool = True
    '                                ElseIf Line.StartsWith("Name=") AndAlso Not Line.StartsWith("Name=" & GnControl) Then : PropertiesOfControl = Nothing
    '                                ElseIf Line.StartsWith("Text=") Then : clsPasswordTextBox._Text = Line.Replace("Text=", Nothing)
    '                                ElseIf Line.StartsWith("FontBold=") Then : clsPasswordTextBox._bold = Line.Replace("FontBold=", Nothing)
    '                                ElseIf Line.StartsWith("FontSize=") Then : clsPasswordTextBox._Font_Size = Line.Replace("FontSize=", Nothing)
    '                                ElseIf Line.StartsWith("FontItalic=") Then : clsPasswordTextBox._Italic = Line.Replace("FontItalic=", Nothing)
    '                                ElseIf Line.StartsWith("FontTypeface=") Then : clsPasswordTextBox._FontTypeface = Line.Replace("FontTypeface=", Nothing)
    '                                ElseIf Line.StartsWith("TextAlign=") Then : clsPasswordTextBox._TextAlign = Line.Replace("TextAlign=", Nothing)
    '                                ElseIf Line.StartsWith("BackColor=") Then : clsPasswordTextBox._BackColor = ColourFromData(Line.Replace("BackColor=", Nothing))
    '                                ElseIf Line.StartsWith("ForeColor=") Then : clsPasswordTextBox._ForeColor = ColourFromData(Line.Replace("ForeColor=", Nothing))
    '                                ElseIf Line.StartsWith("Location=") Then : ContPoints = Line.Replace("Location={", Nothing).Replace("}", Nothing) : clsPasswordTextBox._Location = New Point(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
    '                                ElseIf Line.StartsWith("Size=") Then : ContPoints = Line.Replace("Size={", Nothing).Replace("}", Nothing) : clsPasswordTextBox._Size = New Size(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
    '                                ElseIf Line.StartsWith("Hint=") Then : clsPasswordTextBox._Hint = Line.Replace("Hint=", Nothing)
    '                                ElseIf Line.StartsWith("Enabled=") Then : clsPasswordTextBox._Enabled = Line.Replace("Enabled=", Nothing)
    '                                End If

    '                            ElseIf ContrlFND.StartsWith("APictureBox") Or IntHelp = 8 Then

    '                                If Line.StartsWith("Name=" & GnControl) Then : clsPictureBox._Name = Line.Replace("Name=", Nothing) : Combobox1.Text = Line.Replace("Name=", "") : helpbool = True
    '                                ElseIf Line.StartsWith("Name=") AndAlso Not Line.StartsWith("Name=" & GnControl) Then : PropertiesOfControl = Nothing
    '                                ElseIf Line.StartsWith("BackColor=") Then : clsPictureBox._BackColor = ColourFromData(Line.Replace("BackColor=", Nothing))
    '                                ElseIf Line.StartsWith("Location=") Then : ContPoints = Line.Replace("Location={", Nothing).Replace("}", Nothing) : clsPictureBox._Location = New Point(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
    '                                ElseIf Line.StartsWith("Size=") Then : ContPoints = Line.Replace("Size={", Nothing).Replace("}", Nothing) : clsPictureBox._Size = New Size(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
    '                                ElseIf Line.StartsWith("Image=") Then : If Line.Replace("Image=", Nothing) = Nothing Then : clsPictureBox._Image = Nothing : Else : clsPictureBox._Image = Line.Replace("Image=", Nothing) : End If
    '                                ElseIf Line.StartsWith("Enabled=") Then : clsPictureBox._Enabled = Line.Replace("Enabled=", Nothing)
    '                                End If

    '                            ElseIf ContrlFND.StartsWith("AProgressBar") Or IntHelp = 9 Then

    '                                If Line.StartsWith("Name=" & GnControl) Then : clsProgressBar._Name = Line.Replace("Name=", Nothing) : Combobox1.Text = Line.Replace("Name=", "") : helpbool = True
    '                                ElseIf Line.StartsWith("Name=") AndAlso Not Line.StartsWith("Name=" & GnControl) Then : PropertiesOfControl = Nothing
    '                                ElseIf Line.StartsWith("Location=") Then : ContPoints = Line.Replace("Location={", Nothing).Replace("}", Nothing) : clsProgressBar._Location = New Point(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
    '                                ElseIf Line.StartsWith("Size=") Then : ContPoints = Line.Replace("Size={", Nothing).Replace("}", Nothing) : clsProgressBar._Size = New Size(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
    '                                ElseIf Line.StartsWith("Value=") Then : clsProgressBar._Value = Line.Replace("Value=", Nothing)
    '                                ElseIf Line.StartsWith("Enabled=") Then : clsProgressBar._Enabled = Line.Replace("Enabled=", Nothing)
    '                                End If

    '                            ElseIf ContrlFND.StartsWith("ARadioButton") Or IntHelp = 10 Then

    '                                If Line.StartsWith("Name=" & GnControl) Then : ClsRadioButton._Name = Line.Replace("Name=", Nothing) : Combobox1.Text = Line.Replace("Name=", "") : helpbool = True
    '                                ElseIf Line.StartsWith("Name=") AndAlso Not Line.StartsWith("Name=" & GnControl) Then : PropertiesOfControl = Nothing
    '                                ElseIf Line.StartsWith("Text=") Then : ClsRadioButton._Text = Line.Replace("Text=", Nothing)
    '                                ElseIf Line.StartsWith("FontBold=") Then : ClsRadioButton._bold = Line.Replace("FontBold=", Nothing)
    '                                ElseIf Line.StartsWith("FontSize=") Then : ClsRadioButton._Font_Size = Line.Replace("FontSize=", Nothing)
    '                                ElseIf Line.StartsWith("FontItalic=") Then : ClsRadioButton._Italic = Line.Replace("FontItalic=", Nothing)
    '                                ElseIf Line.StartsWith("FontTypeface=") Then : ClsRadioButton._FontTypeface = Line.Replace("FontTypeface=", Nothing)
    '                                ElseIf Line.StartsWith("TextAlign=") Then : ClsRadioButton._TextAlign = Line.Replace("TextAlign=", Nothing)
    '                                ElseIf Line.StartsWith("BackColor=") Then : ClsRadioButton._BackColor = ColourFromData(Line.Replace("BackColor=", Nothing))
    '                                ElseIf Line.StartsWith("ForeColor=") Then : ClsRadioButton._ForeColor = ColourFromData(Line.Replace("ForeColor=", Nothing))
    '                                ElseIf Line.StartsWith("Location=") Then : ContPoints = Line.Replace("Location={", Nothing).Replace("}", Nothing) : ClsRadioButton._Location = New Point(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
    '                                ElseIf Line.StartsWith("Size=") Then : ContPoints = Line.Replace("Size={", Nothing).Replace("}", Nothing) : ClsRadioButton._Size = New Size(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
    '                                ElseIf Line.StartsWith("Checked=") Then : ClsRadioButton._Checked = Line.Replace("Checked=", Nothing)
    '                                ElseIf Line.StartsWith("Enabled=") Then : ClsRadioButton._Enabled = Line.Replace("Enabled=", Nothing)
    '                                End If

    '                            ElseIf ContrlFND.StartsWith("ATextBox") Or IntHelp = 11 Then

    '                                If Line.StartsWith("Name=" & GnControl) Then : clsTextBox._Name = Line.Replace("Name=", Nothing) : Combobox1.Text = Line.Replace("Name=", "") : helpbool = True
    '                                ElseIf Line.StartsWith("Name=") AndAlso Not Line.StartsWith("Name=" & GnControl) Then : PropertiesOfControl = Nothing
    '                                ElseIf Line.StartsWith("Text=") Then : clsTextBox._Text = Line.Replace("Text=", Nothing)
    '                                ElseIf Line.StartsWith("FontBold=") Then : clsTextBox._bold = Line.Replace("FontBold=", Nothing)
    '                                ElseIf Line.StartsWith("FontSize=") Then : clsTextBox._Font_Size = Line.Replace("FontSize=", Nothing)
    '                                ElseIf Line.StartsWith("FontItalic=") Then : clsTextBox._Italic = Line.Replace("FontItalic=", Nothing)
    '                                ElseIf Line.StartsWith("FontTypeface=") Then : clsTextBox._FontTypeface = Line.Replace("FontTypeface=", Nothing)
    '                                ElseIf Line.StartsWith("TextAlign=") Then : clsTextBox._TextAlign = Line.Replace("TextAlign=", Nothing)
    '                                ElseIf Line.StartsWith("BackColor=") Then : clsTextBox._BackColor = ColourFromData(Line.Replace("BackColor=", Nothing))
    '                                ElseIf Line.StartsWith("ForeColor=") Then : clsTextBox._ForeColor = ColourFromData(Line.Replace("ForeColor=", Nothing))
    '                                ElseIf Line.StartsWith("Location=") Then : ContPoints = Line.Replace("Location={", Nothing).Replace("}", Nothing) : clsTextBox._Location = New Point(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
    '                                ElseIf Line.StartsWith("Size=") Then : ContPoints = Line.Replace("Size={", Nothing).Replace("}", Nothing) : clsTextBox._Size = New Size(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
    '                                ElseIf Line.StartsWith("Hint=") Then : clsTextBox._Hint = Line.Replace("Hint=", Nothing)
    '                                ElseIf Line.StartsWith("Enabled=") Then : clsTextBox._Enabled = Line.Replace("Enabled=", Nothing)
    '                                ElseIf Line.StartsWith("InputType=") Then : clsTextBox._InputType = Line.Replace("InputType=", Nothing)
    '                                End If

    '                            ElseIf ContrlFND.StartsWith("ATimer") Or IntHelp = 12 Then

    '                                If Line.StartsWith("Name=" & GnControl) Then : clsTimer._Name = Line.Replace("Name=", Nothing) : Combobox1.Text = Line.Replace("Name=", "") : helpbool = True
    '                                ElseIf Line.StartsWith("Name=") AndAlso Not Line.StartsWith("Name=" & GnControl) Then : PropertiesOfControl = Nothing
    '                                ElseIf Line.StartsWith("Interval=") Then : clsTimer._Interval = Line.Replace("Interval=", Nothing)
    '                                ElseIf Line.StartsWith("Enabled=") Then : clsTimer._Enabled = Line.Replace("Enabled=", Nothing)
    '                                End If

    '                            ElseIf ContrlFND.StartsWith("AComboBox") Or IntHelp = 13 Then

    '                                If Line.StartsWith("Name=" & GnControl) Then : clsComboBox._Name = Line.Replace("Name=", Nothing) : Combobox1.Text = Line.Replace("Name=", "") : helpbool = True
    '                                ElseIf Line.StartsWith("Name=") AndAlso Not Line.StartsWith("Name=" & GnControl) Then : PropertiesOfControl = Nothing
    '                                ElseIf Line.StartsWith("Text=") Then : clsComboBox._Text = Line.Replace("Text=", Nothing)
    '                                ElseIf Line.StartsWith("FontBold=") Then : clsComboBox._bold = Line.Replace("FontBold=", Nothing)
    '                                ElseIf Line.StartsWith("FontSize=") Then : clsComboBox._Font_Size = Line.Replace("FontSize=", Nothing)
    '                                ElseIf Line.StartsWith("FontItalic=") Then : clsComboBox._Italic = Line.Replace("FontItalic=", Nothing)
    '                                ElseIf Line.StartsWith("FontTypeface=") Then : clsComboBox._FontTypeface = Line.Replace("FontTypeface=", Nothing)
    '                                ElseIf Line.StartsWith("TextAlign=") Then : clsComboBox._TextAlign = Line.Replace("TextAlign=", Nothing)
    '                                ElseIf Line.StartsWith("BackColor=") Then : clsComboBox._BackColor = ColourFromData(Line.Replace("BackColor=", Nothing))
    '                                ElseIf Line.StartsWith("ForeColor=") Then : clsComboBox._ForeColor = ColourFromData(Line.Replace("ForeColor=", Nothing))
    '                                ElseIf Line.StartsWith("Location=") Then : ContPoints = Line.Replace("Location={", Nothing).Replace("}", Nothing) : clsComboBox._Location = New Point(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
    '                                ElseIf Line.StartsWith("Size=") Then : ContPoints = Line.Replace("Size={", Nothing).Replace("}", Nothing) : clsComboBox._Size = New Size(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
    '                                ElseIf Line.StartsWith("Enabled=") Then : clsComboBox._Enabled = Line.Replace("Enabled=", Nothing)
    '                                End If



    '                            ElseIf ContrlFND.StartsWith("VB4AWeb") Or IntHelp = 14 Then

    '                                If Line.StartsWith("Name=" & GnControl) Then : clsVB4AWeb._Name = Line.Replace("Name=", Nothing) : Combobox1.Text = Line.Replace("Name=", "") : helpbool = True
    '                                ElseIf Line.StartsWith("Name=") AndAlso Not Line.StartsWith("Name=" & GnControl) Then : PropertiesOfControl = Nothing
    '                                ElseIf Line.StartsWith("SavePassword=") Then : clsVB4AWeb._SavePassword = Line.Replace("SavePassword=", Nothing)
    '                                ElseIf Line.StartsWith("SaveFromData=") Then : clsVB4AWeb._SaveFormData = Line.Replace("SaveFromData=", Nothing)
    '                                ElseIf Line.StartsWith("JSEnabled=") Then : clsVB4AWeb._JSEnabled = Line.Replace("JSEnabled=", Nothing)
    '                                ElseIf Line.StartsWith("ZoomEnabled=") Then : clsVB4AWeb._ZoomEnabled = Line.Replace("ZoomEnabled=", Nothing)
    '                                ElseIf Line.StartsWith("BuildinZoom=") Then : clsVB4AWeb._BuildinZoom = Line.Replace("BuildinZoom=", Nothing)
    '                                ElseIf Line.StartsWith("Location=") Then : ContPoints = Line.Replace("Location={", Nothing).Replace("}", Nothing) : clsVB4AWeb._Location = New Point(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
    '                                ElseIf Line.StartsWith("Size=") Then : ContPoints = Line.Replace("Size={", Nothing).Replace("}", Nothing) : clsVB4AWeb._Size = New Size(CInt(ContPoints.Split(";")(0)), CInt(ContPoints.Split(";")(1)))
    '                                ElseIf Line.StartsWith("Enabled=") Then : clsVB4AWeb._Enabled = Line.Replace("Enabled=", Nothing)
    '                                End If

    '                            End If
    '                        End If
    '                        ContPoints = Nothing
    '                    End If
    '                End If
    'next1:      Next

    '        End If

    '        ' MsgBox(PropertiesOfControl)
    '        ' ClsButton._Name = "asd"

    '        '  ChangeASpecificPropOfContr(PropertiesOfControl, "Name=", "Name=masdasd", GnControl)

    '    End Sub
    Private Sub ToolStrip3_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles ToolStrip3.ItemClicked

    End Sub

    Private Sub Panel4_Paint(sender As Object, e As PaintEventArgs) Handles Panel4.Paint

    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub TextBox2_Enter(sender As Object, e As EventArgs) Handles TextBox2.Enter
        'MsgBox(1)

    End Sub

    Private Sub TextBox2_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyUp
        ' MsgBox(TextBox2.Text)
        If e.KeyCode = Keys.Enter Then : If Not TextBox2.Text = "" AndAlso Not TextBox2.Text.TrimEnd = "" Then
                'MsgBox()
                'MsgBox(My.Computer.FileSystem.ReadAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj").Split("#")(1).Split("=")(1).Replace(vbNewLine, ""))
                For Each ln As String In My.Computer.FileSystem.ReadAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj").Split(vbNewLine)
                    ln = ln.Replace(vbLf, Nothing)
                    If ln.StartsWith("ApplicationName") Then My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj", My.Computer.FileSystem.ReadAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj").Replace("ApplicationName=" & ln.Replace("ApplicationName=", Nothing), "ApplicationName=" & TextBox2.Text), False) : Exit For
                Next

                ConsoleDouble += 1
                RichTextBox1.AppendText("[" & ConsoleDouble & "] " & "Application's Name Changed To '" & TextBox2.Text & "' " & "At " & DateTime.Now.ToString & "." & vbNewLine)
                Timer2.Enabled = True
                RichTextBox1.ScrollToCaret()
            Else : MsgBox("You have to name your App.", MsgBoxStyle.Information, "App Name.")
            End If : End If
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        If Button10.BackColor = SystemColors.ControlLight Then
            Button10.BackColor = SystemColors.ControlLightLight
        Else : Button10.BackColor = SystemColors.ControlLight
        End If
    End Sub





    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub



    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        'Textbox3.NavigateBackward()
        'Textbox3.ClearUndo()
        ' matrix
        'navigate

    End Sub
    Public Class ExtendedPanel
        Inherits System.Windows.Forms.Panel


        Private Overloads Property HScroll() As Boolean
            Get

                Return MyBase.HScroll
            End Get

            Set(ByVal Value As Boolean)

                MyBase.HScroll = Value

            End Set
        End Property
    End Class


    Private Sub Button12_Click(sender As Object, e As EventArgs)
        'PictureBox1.AutoSize = True


        ''PictureBox1.AutoScrollPosition = New Point(100, 100)
        'Panel5.Size = New Size(YofFormApp, 242)
        '    Panel5.Location = New Point(((PictureBox1.Size.Width - Panel5.Size.Width) \ 2) - 1, ((PictureBox1.Size.Height - Panel5.Size.Height) \ 2) - 1)
        '    Panel5.Refresh()
        ''  PictureBox4.Size = Panel5.Size '+ New Size(40, 0)
        '' PictureBox4.Location = Panel5.Location '- New Point(20, 0)

        ''Panel5.Hide()
        'PictureBox1.AutoScroll = True



        'For l As Long = 1 To 100

        '    Dim label As New Label()

        '    label.Name = "label" & CStr(l)

        '    label.Left = 0

        '    label.AutoEllipsis = True

        '    label.Height = 30

        '    label.Top = ((l - 1) * label.Height) + 2

        '    label.Text = "label" & CStr(l) & ": " & CStr(label.Top)

        '    label.Width = 125

        '    label.Visible = True

        '    PictureBox1.Controls.Add(label)




        '    Dim ctl As New ComboBox




        '    ctl.Left = label.Left + label.Width

        '    ctl.Top = label.Top




        '    ctl.Anchor = AnchorStyles.Left Or AnchorStyles.Right




        '    PictureBox1.Controls.Add(ctl)

        'Next l

        'PictureBox1.AutoScroll = True

        ' PictureBox1.Refresh()
        'MsgBox(ATextBox.Name)

        'BuildAutocompleteMenu()
        ' Dim lines() As String = Textbox3.Lines.ToArray

        ' lines(3) = "this is actually line 4"
        ' Textbox3.Text = Join(lines, vbCrLf)




        '' ''IO.File.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & "form1.vb4acode", Textbox3.Text, Encoding.GetEncoding("gb2312"))
        '' ''Dim args() As String = {"", ""}
        '' ''Dim infile As String = args(0)
        '' ''Dim outfile As String = args(1)
        '' ''Dim sr As StreamReader = New StreamReader(infile, Encoding.GetEncoding("gb2312"))
        '' ''Dim sw As StreamWriter = New StreamWriter(outfile, False, Encoding.UTF8)
        '' ''sw.Write(sr.ReadToEnd)
        '' ''sw.Close()
        '' ''sr.Close()



        ''IO.File.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & "form1.vb4acode", Textbox3.Text, Encoding.GetEncoding("gb2312"))

        ''Dim Encw1252 As Encoding = Encoding.GetEncoding("gb2312")
        ''Dim EncUTF8 As Encoding = Encoding.GetEncoding("utf-8")
        ''Dim Str As String
        ''Str = Encw1252.GetString(Encoding.Convert(Encw1252, EncUTF8, Encoding.Default.GetBytes(IO.File.ReadAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & "form1.vb4acode"))))
        ''Textbox3.Text = Str
        ' For Each oFile In IO.Directory.GetFiles(Dir, "*.*", IO.SearchOption.AllDirectories)
        'IO.File.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & "form1.vb4acode", Textbox3.Text, Encoding.UTF8)
        ' Textbox3.Text = IO.File.ReadAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & "form1.vb4acode", Encoding.UTF8)
        ' Next
        'For Each cont As Control In Panel5.Controls
        '    cont.Show()
        'Next
        '  MsgBox("start: " & strClsRemove(0) & vbNewLine & "End: " & strClsRemove(strClsRemove.Count - 1) & vbNewLine & "allnum: " & strClsRemove.Count -1)



        '  Me.Scale(New Size(200, 200))
        ' Check_In_Combobox_For_Contrl_Name(1, "asd")
        ' MsgBox(PropertiesOfControl)

    End Sub



    Private Sub TextBox3_PaintLine(sender As Object, e As PaintLineEventArgs) Handles Textbox3.PaintLine
        'Dim i As Single
        '  Dim sf As StringFormat
        ' Dim String1 As String = "Here is out                               正体字/繁体字 test string"
        'Dim CharSizeF As SizeF

        ' sf = StringFormat.GenericTypographic

        ' CharSizeF = e.Graphics.MeasureString(String1, Me.Font, 10000, sf)
        '   CharSizeF.Width /= String1.Length

        '  e.Graphics.DrawString(String1, New Font("Consolas", 15), Brushes.Black, 0, 0, sf)


    End Sub




    Private Sub ToolStrip4_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles ToolStrip4.ItemClicked

    End Sub
    Dim cheackstrings() As String = {"As", "If", "In", "Is", "Me", "On", "To", "And", "Dim", "End", "For", "Get", "New", "Not", "Sub",
                                     "Each", "Case", "Else", "Exit", "Like", "Long", "Next", "Step", "Then", "True",
                                     "Alias", "ByRef", "ByVal", "Const", "Event", "False", "IsNot", "Short", "Until", "While",
                                     "Double", "ElseIf", "Object", "Single", "Static", "String", "Boolean", "Integer", "Variant", "Function", "Collection"}
    'HERE TEXTBOX3 ,... cheackstrings() i know i could use keyworks.Lowercase also but ..... i know :P !
    Private popupMenu As AutocompleteMenu
    'Private popupMenu2 As AutocompleteMenu ' 2 : , "If"
    Private keywords As String() = {"As", "In", "Is", "Me", "On", "To", "And ", "Dim", "End", "For", "Get", "New", "Not", "Str", "Sub", "Val", "Xor", "Byte", "Case", "Date", "Each", "Else", "Exit", "Like", "Long", "Next", "Step",
                                    "Then", "True", "Alias", "ByRef", "ByVal", "Const", "Error", "Event", "False", "IsNot", "Short", "Split", "Until", "While", "Arrays",
                                    "Double", "ElseIf", "Finish", "Format", "Object", "Single", "Static", "String", "TypeOf", "Boolean", "Integer", "Variant",
                                    "Function", "Select", "Collection", "VB4AMsgboxClicked",
                                    "VB4AInputBoxClicked"}

    'Shared sources As New List(Of String)
    Private methods As String() = {""}
    Private methodsToolTip As String() = {""}
    Private methodsToolTipTitle As String() = {""}
    Private snippets As String() = {"If^ Then" & vbLf & vbLf & "End If", "Do" & vbLf & "^" & vbLf & "Loop", "Do" & vbLf & "Loop While^"
                                    } ' "Try" & vbLf & "^" & vbLf & "Catch ex As Exception" & vbLf & vbLf & "End Try"
    Private declarationSnippets As String() = {"Sub^()" & vbLf & "End Sub", "Event^()" & vbLf & "End Event", "Function^()" & vbLf & "End Function", "Select^" & vbLf & "Case" & vbLf & "End Select"}

    Public Sub New()
        InitializeComponent()

        'create autocomplete popup menu
        popupMenu = New AutocompleteMenu(Textbox3)
        popupMenu.Items.ImageList = ImageList1

        popupMenu.SearchPattern = "[\w\.:=!<>]"
        popupMenu.AppearInterval = 1
        popupMenu.ToolTipDuration = 11000
        popupMenu.MinFragmentLength = 2



        'popupMenu2 = New AutocompleteMenu(Textbox3)
        'popupMenu2.Items.ImageList = ImageList1

        'popupMenu2.SearchPattern = "[\w\.:=!<>]"
        'popupMenu2.AppearInterval = 1
        'popupMenu2.ToolTipDuration = 11000
        'popupMenu2.MinFragmentLength = 2

        ' na valw icons




        BuildAutocompleteMenu()
        'BuildAutocompleteMenuNoStadar()
    End Sub
    'Private Sub BuildAutocompleteMenuNoStadar()
    '    Dim items As New List(Of AutocompleteItem)()




    '    'For Each item As String In ButtonList.ToArray
    '    '    items.Add(New MethodAutocompleteItem2(item) With {.ImageIndex = 2, .ToolTipTitle = "", .ToolTipText = ""})
    '    'Next
    '    items.Add(New InsertSpaceSnippet())
    '    items.Add(New InsertSpaceSnippet("^(\w+)([=<>!:]+)(\w+)$"))
    '    items.Add(New InsertEnterSnippet())


    '    'set as autocomplete source
    '    ' Console.WriteLine("====END1111====")
    '    popupMenu2.Items.SetAutocompleteItems(items)
    'End Sub
    Public Sub BuildAutocompleteMenu()
        ' If Not popupMenu.ToolTip.ToolTipTitle = "" Then MsgBox(popupMenu.ToolTip.ToolTipTitle)

        Dim items As New List(Of AutocompleteItem)()

        'Dim i As Integer = 0

        Dim ListAll As New List(Of String)

        For Each item As String In CotntroltypesList.ToArray  ' mhn 3exasw na patisw to lang :P ----------------------------------------------
            items.Add(New AutocompleteItem(Regex.Split(item, EndBracket)(0)) With {.ImageIndex = CInt(Regex.Split(item, EndBracket)(1)), .ToolTipTitle = Regex.Split(item, EndBracket)(2), .ToolTipText = Regex.Split(item, EndBracket)(3)})
        Next
        For i = 0 To ComboboxImage1.Items.Count - 1

            If Not ComboboxImage1.Items.Item(i).ToString = Combobox3.Text.Replace(".vb4a", Nothing) Then

                Select Case ComboboxImage1.Items.Item(i).ImageIndex
                    Case Is = 0 : ListAll = ButtonList 'Button
                    Case Is = 1 : ListAll = CanvasList 'Canvas
                    Case Is = 2 : ListAll = CheckBoxList 'CheckBox
                    Case Is = 4 : ListAll = LabelList 'Label
                    Case Is = 5 : ListAll = ListviewList 'ListView
                    Case Is = 6 : ListAll = PanelList 'Panel
                    Case Is = 7 : ListAll = PasswordTextBoxList 'PasswordTextBox
                    Case Is = 8 : ListAll = PictureBoxList 'PictureBox Image
                    Case Is = 9 : ListAll = ProgressBarList 'ProgressBar
                    Case Is = 10 : ListAll = RadioButtonList 'RadioButton
                    Case Is = 12 : ListAll = TextBoxList 'TextBox
                    Case Is = 13 : ListAll = TimerList 'Timer
                    Case Is = 14 : ListAll = ComboBoxList 'ComboBox VB4ASpinner
                    Case Is = 15 : ListAll = VB4WebList 'VB4AWeb

                End Select

                For Each item As String In ListAll.ToArray  ' mhn 3exasw na patisw to lang :P ----------------------------------------------
                    items.Add(New MethodAutocompleteItem2(Regex.Split(item, EndBracket)(0).Replace(ControlNameOfPlangFiles, ComboboxImage1.Items.Item(i).ToString)) With {.ImageIndex = CInt(Regex.Split(item, EndBracket)(1)), .ToolTipTitle = Regex.Split(item, EndBracket)(2).Replace(ControlNameOfPlangFiles, ComboboxImage1.Items.Item(i).ToString), .ToolTipText = Regex.Split(item, EndBracket)(3).Replace(ControlNameOfPlangFiles, ComboboxImage1.Items.Item(i).ToString)})
                Next

            End If
        Next

        For Each item As String In ApplicationList.ToArray 
            items.Add(New MethodAutocompleteItem2(Regex.Split(item, EndBracket)(0)) With {.ImageIndex = CInt(Regex.Split(item, EndBracket)(1)), .ToolTipTitle = Regex.Split(item, EndBracket)(2), .ToolTipText = Regex.Split(item, EndBracket)(3)})
        Next
        For Each item As String In ArraysList.ToArray
            items.Add(New MethodAutocompleteItem2(Regex.Split(item, EndBracket)(0)) With {.ImageIndex = CInt(Regex.Split(item, EndBracket)(1)), .ToolTipTitle = Regex.Split(item, EndBracket)(2), .ToolTipText = Regex.Split(item, EndBracket)(3)})
        Next
        For Each item As String In VB4AInputList.ToArray
            items.Add(New MethodAutocompleteItem2(Regex.Split(item, EndBracket)(0)) With {.ImageIndex = CInt(Regex.Split(item, EndBracket)(1)), .ToolTipTitle = Regex.Split(item, EndBracket)(2), .ToolTipText = Regex.Split(item, EndBracket)(3)})
        Next
        For Each item As String In FilesList.ToArray
            items.Add(New MethodAutocompleteItem2(Regex.Split(item, EndBracket)(0)) With {.ImageIndex = CInt(Regex.Split(item, EndBracket)(1)), .ToolTipTitle = Regex.Split(item, EndBracket)(2), .ToolTipText = Regex.Split(item, EndBracket)(3)})
        Next
        For Each item As String In MathList.ToArray
            items.Add(New MethodAutocompleteItem2(Regex.Split(item, EndBracket)(0)) With {.ImageIndex = CInt(Regex.Split(item, EndBracket)(1)), .ToolTipTitle = Regex.Split(item, EndBracket)(2), .ToolTipText = Regex.Split(item, EndBracket)(3)})
        Next
        For Each item As String In AcceleromaterSenorList.ToArray
            items.Add(New MethodAutocompleteItem2(Regex.Split(item, EndBracket)(0)) With {.ImageIndex = CInt(Regex.Split(item, EndBracket)(1)), .ToolTipTitle = Regex.Split(item, EndBracket)(2), .ToolTipText = Regex.Split(item, EndBracket)(3)})
        Next
        For Each item As String In LocationSensorList.ToArray
            items.Add(New MethodAutocompleteItem2(Regex.Split(item, EndBracket)(0)) With {.ImageIndex = CInt(Regex.Split(item, EndBracket)(1)), .ToolTipTitle = Regex.Split(item, EndBracket)(2), .ToolTipText = Regex.Split(item, EndBracket)(3)})
        Next
        For Each item As String In OrientationSensorList.ToArray
            items.Add(New MethodAutocompleteItem2(Regex.Split(item, EndBracket)(0)) With {.ImageIndex = CInt(Regex.Split(item, EndBracket)(1)), .ToolTipTitle = Regex.Split(item, EndBracket)(2), .ToolTipText = Regex.Split(item, EndBracket)(3)})
        Next
        For Each item As String In PhoneList.ToArray
            items.Add(New MethodAutocompleteItem2(Regex.Split(item, EndBracket)(0)) With {.ImageIndex = CInt(Regex.Split(item, EndBracket)(1)), .ToolTipTitle = Regex.Split(item, EndBracket)(2), .ToolTipText = Regex.Split(item, EndBracket)(3)})
        Next
        For Each item As String In ComponentList.ToArray
            items.Add(New MethodAutocompleteItem2(Regex.Split(item, EndBracket)(0)) With {.ImageIndex = CInt(Regex.Split(item, EndBracket)(1)), .ToolTipTitle = Regex.Split(item, EndBracket)(2), .ToolTipText = Regex.Split(item, EndBracket)(3)})
        Next
        For Each item As String In DatesList.ToArray
            items.Add(New MethodAutocompleteItem2(Regex.Split(item, EndBracket)(0)) With {.ImageIndex = CInt(Regex.Split(item, EndBracket)(1)), .ToolTipTitle = Regex.Split(item, EndBracket)(2), .ToolTipText = Regex.Split(item, EndBracket)(3)})
        Next
        For Each item As String In StringsList.ToArray
            items.Add(New MethodAutocompleteItem2(Regex.Split(item, EndBracket)(0)) With {.ImageIndex = CInt(Regex.Split(item, EndBracket)(1)), .ToolTipTitle = Regex.Split(item, EndBracket)(2), .ToolTipText = Regex.Split(item, EndBracket)(3)})
        Next
        For Each item As String In ConversionsList.ToArray
            items.Add(New MethodAutocompleteItem2(Regex.Split(item, EndBracket)(0)) With {.ImageIndex = CInt(Regex.Split(item, EndBracket)(1)), .ToolTipTitle = Regex.Split(item, EndBracket)(2), .ToolTipText = Regex.Split(item, EndBracket)(3)})
        Next
        For Each item As String In Base64List.ToArray
            items.Add(New MethodAutocompleteItem2(Regex.Split(item, EndBracket)(0)) With {.ImageIndex = CInt(Regex.Split(item, EndBracket)(1)), .ToolTipTitle = Regex.Split(item, EndBracket)(2), .ToolTipText = Regex.Split(item, EndBracket)(3)})
        Next
        For Each item As String In StatisticsList.ToArray
            items.Add(New MethodAutocompleteItem2(Regex.Split(item, EndBracket)(0)) With {.ImageIndex = CInt(Regex.Split(item, EndBracket)(1)), .ToolTipTitle = Regex.Split(item, EndBracket)(2), .ToolTipText = Regex.Split(item, EndBracket)(3)})
        Next
        For Each item As String In MatrixList.ToArray 
            items.Add(New MethodAutocompleteItem2(Regex.Split(item, EndBracket)(0)) With {.ImageIndex = CInt(Regex.Split(item, EndBracket)(1)), .ToolTipTitle = Regex.Split(item, EndBracket)(2), .ToolTipText = Regex.Split(item, EndBracket)(3)})
        Next


        For Each item As String In snippets
            items.Add(New SnippetAutocompleteItem(item) With {.ImageIndex = 1})
        Next
        For Each item As String In declarationSnippets
            items.Add(New DeclarationSnippet(item) With {.ImageIndex = 0})
        Next
        ' Console.WriteLine("====Start====")
        'For Each item As String In methods
        '    'MsgBox(methodsToolTip(i))
        '    items.Add(New MethodAutocompleteItem(item) With {.ImageIndex = 2, .ToolTipTitle = methodsToolTipTitle(i), .ToolTipText = methodsToolTip(i)})
        '    'Console.WriteLine(item)
        '    i += 1
        'Next
        'Console.WriteLine("====END====")
        For Each item As String In keywords
            items.Add(New AutocompleteItem(item))
        Next

        items.Add(New InsertSpaceSnippet())
        items.Add(New InsertSpaceSnippet("^(\w+)([=<>!:]+)(\w+)$"))
        items.Add(New InsertEnterSnippet())


        'set as autocomplete source
        ' Console.WriteLine("====END1111====")
        popupMenu.Items.SetAutocompleteItems(items)
        ' Console.WriteLine("====END222222222====")
        'popupMenu.ToolTip.AutoPopDelay = 10000

        'popupMenu.ToolTip.ReshowDelay = 1000
        'popupMenu.ShowItemToolTips = True
    End Sub
    Public Class MethodAutocompleteItem2
        Inherits MethodAutocompleteItem
        Private firstPart As String
        Private lastPart As String

        Public Sub New(text As String)
            MyBase.New(text)
            Dim i = text.LastIndexOf("."c)
            If i < 0 Then
                firstPart = text
            Else
                firstPart = text.Substring(0, i)
                lastPart = text.Substring(i + 1)
            End If
        End Sub

        Public Overrides Function Compare(fragmentText As String) As CompareResult
            'If fragmentText.Length = 1 Then SendKeys.Send("{ESC}")



            'Form1.methods = {}
            'Form1.methodsToolTipTitle = {}
            'Form1.methodsToolTip = {}
            'Form1.BuildAutocompleteMenu()



            Dim i2 As Integer = fragmentText.LastIndexOf("."c)
            If i2 < 0 Then
                If firstPart.ToLower.StartsWith(fragmentText.ToLower) AndAlso String.IsNullOrEmpty(lastPart) Then
                    Return CompareResult.VisibleAndSelected

                End If

            Else

                Dim fragmentFirstPart = fragmentText.Substring(0, i2)
                Dim fragmentLastPart = fragmentText.Substring(i2 + 1)


                'If (firstPart.ToLower().Contains(fragmentText.ToLower())) Then
                '    Return CompareResult.Visible
                'End If

                If firstPart <> fragmentFirstPart Then
                    Return CompareResult.Hidden
                End If

                If lastPart IsNot Nothing AndAlso lastPart.StartsWith(fragmentLastPart) Then
                    Return CompareResult.VisibleAndSelected
                End If




                If lastPart IsNot Nothing AndAlso lastPart.ToLower().Contains(fragmentLastPart.ToLower()) Then
                    Return CompareResult.Visible

                End If
            End If

            Return CompareResult.Hidden
        End Function

        Public Overrides Function GetTextForReplace() As String
            If lastPart Is Nothing Then
                Return firstPart
            End If

            Return Convert.ToString(firstPart & Convert.ToString(".")) & lastPart
        End Function

        Public Overrides Function ToString() As String
            If lastPart Is Nothing Then
                Return firstPart
            End If

            Return lastPart
        End Function
    End Class
    Private Class DeclarationSnippet
        Inherits SnippetAutocompleteItem
        Public Sub New(ByVal snippet As String)
            MyBase.New(snippet)
        End Sub

        Public Overrides Function Compare(ByVal fragmentText As String) As CompareResult
            Dim pattern = Regex.Escape(fragmentText)
            If Regex.IsMatch(Text, "\b" & pattern, RegexOptions.IgnoreCase) Then
                Return CompareResult.Visible
            End If
            Return CompareResult.Hidden
        End Function
    End Class


    Private Class InsertSpaceSnippet
        Inherits AutocompleteItem
        Private pattern As String

        Public Sub New(ByVal pattern As String)
            MyBase.New("")
            Me.pattern = pattern
        End Sub


        Public Sub New()
            Me.New("^(\d+)([a-zA-Z_]+)(\d*)$")
        End Sub


        'Public Overrides Function Compare(ByVal fragmentText As String) As CompareResult
        '    'edw 8a 8a tsekarw apo to fragmentText kai 8a alazw ta methods
        '    'BuildAutocompleteMenu()
        '    'MsgBox(Me.ComboboxImage1.Items.Count())
        '    ' MsgBox(fragmentText)
        '    ' Console.WriteLine(fragmentText)
        '    'Console.WriteLine(fragmentText) '[Dim]+ [a-zA-Z0-9]+ [As]+ [a-zA-Z]+
        '    ' because i cant close it with other way.... (and i have reason i have this  popupMenu.MinFragmentLength = 1  too!!! :P )
        '    'If fragmentText.Length = 1 Then Form1.popupMenu.Close()
        '    'Form1.popupMenu.Refresh()
        '    Form1.frgt = fragmentText

        '    'Console.WriteLine(fragmentText)
        '    'If Not Form1.methods.Count = 1 AndAlso Not Form1.methods(1) = "" Then
        '    '    For i = 1 To Form1.methods.Count - 1
        '    '        If Form1.methods(1).StartsWith(fragmentText.Remove(0,mexri thn telia)) Then

        '    '        End If
        '    '    Next
        '    '    Form1.methods = {""}
        '    '    Form1.methodsToolTip = {""}
        '    '    Form1.BuildAutocompleteMenu()
        '    'End If

        '    Form1.methods = {""}
        '    Form1.methodsToolTip = {""}
        '    Form1.methodsToolTipTitle = {""}


        '    If fragmentText.Length = 1 Then SendKeys.Send("{ESC}") ' easiest way :D

        '    ' Form1.BuildAutocompleteMenu()
        '    'MsgBox(Form1.Textbox3.Text) 
        '    For i = 0 To Form1.ComboboxImage1.Items.Count - 1 Step 1
        '        ' If fragmentText.EndsWith(".") Then
        '        ' MsgBox(i)
        '        ' MsgBox(fragmentText & " = " & Form1.ComboboxImage1.Items.Item(i).ToString & vbNewLine & "i = " & i & vbNewLine & "ComCount = " & Form1.ComboboxImage1.Items.Count - 1)
        '        If fragmentText.StartsWith(Form1.ComboboxImage1.Items.Item(i).ToString & ".") AndAlso Not fragmentText = Form1.ComboboxImage1.Items.Item(i).ToString & "." AndAlso fragmentText.Contains(".") AndAlso Not Replace(fragmentText, ".", "", , 1).Contains(".") Then
        '            If Form1.ecode = True Then Form1.ecode = False : SendKeys.Send(".")
        '        End If
        '        If Form1.ComboboxImage1.Items.Item(i).ToString = fragmentText AndAlso Not fragmentText.Contains(".") AndAlso Not Replace(fragmentText, ".", "", , 1).Contains(".") Then '& "."
        '            ' Console.WriteLine((fragmentText))
        '            Dim contname As String = Form1.ComboboxImage1.Items.Item(i).ToString

        '            ' i was asking myself what is better.... having a small exe or an exe getting a little bit more RAM kbs

        '            If Form1.ComboboxImage1.Items.Item(i).ImageIndex = 0 Then 'Button = 0
        '                Form1.methods = {"BackgroundColor", "FontBold", "FontItalic", "FontSize", "Height", "Text", "Image", "TextColor", "Enabled", "Width"}
        '                Form1.methodsToolTipTitle = {"BackgroundColor As Color", _
        '                                             "FontBold As Boolean", _
        '                                             "FontItalic As Boolean", _
        '                                             "FontSize As Integer", _
        '                                             "Height As Integer", _
        '                                             "Text As String", _
        '                                             "Image As String", _
        '                                             "TextColor As Color", _
        '                                             "Enabled As Boolean", _
        '                                             "Width As Integer"}

        '                Form1.methodsToolTip = Form1.btnMethTLst.ToArray()  ' Form1.methodsToolTipAll.GetRange(0, 8).ToArray()
        '            ElseIf Form1.ComboboxImage1.Items.Item(i).ImageIndex = 1 Then 'Canvas
        '                Form1.methods = {"BackgroundColor", "BackgroundImage", "Clear()", "DrawPoint()", "DrawCircle()", "DrawLine()", "Height", "PaintColor", "SetVB4APaintSize()", "VB4ADrawRect()", "VB4ADrawText()", "VB4ARotate()", "VB4ADrawArc()", "VB4ADrawRoundRect()", "VB4AInvalidate()", "Width"}
        '                Form1.methodsToolTipTitle = {"BackgroundColor As Color", _
        '                                             "BackgroundImage As String", _
        '                                              "-", _
        '                                              "-", _
        '                                              "-", _
        '                                              "-", _
        '                                              "Height As Integer", _
        '                                              "-", _
        '                                              "-", _
        '                                              "-", _
        '                                              "-", _
        '                                              "-", _
        '                                              "-", _
        '                                              "-", _
        '                                              "-", _
        '                                              "Width As Integer"}
        '                Form1.methodsToolTip = Form1.CnvsMethTlst.ToArray()
        '            ElseIf Form1.ComboboxImage1.Items.Item(i).ImageIndex = 2 Then 'CheckBox
        '                Form1.methods = {"BackgroundColor", "FontBold", "FontItalic", "FontSize", "Height", "Text", "TextColor", "Enabled", "Value", "Width"}
        '                Form1.methodsToolTipTitle = {"BackgroundColor As Color", _
        '                                             "FontBold As Boolean", _
        '                                             "FontItalic As Boolean", _
        '                                             "FontSize As Integer", _
        '                                             "Height As Integer", _
        '                                             "Text As String", _
        '                                             "TextColor As Color", _
        '                                             "Enabled As Boolean", _
        '                                             "Value As Boolean", _
        '                                             "Width As Integer"}
        '                Form1.methodsToolTip = Form1.ChckBxMethTlst.ToArray()
        '            ElseIf Form1.ComboboxImage1.Items.Item(i).ImageIndex = 4 Then 'Label
        '                Form1.methods = {"BackgroundColor", "FontBold", "FontItalic", "FontSize", "Height", "Text", "TextColor", "Width"}
        '                Form1.methodsToolTipTitle = {"BackgroundColor As Color", _
        '                                             "FontBold As Boolean", _
        '                                             "FontItalic As Boolean", _
        '                                             "FontSize As Integer", _
        '                                             "Height As Integer", _
        '                                             "Text As String", _
        '                                             "TextColor As Color", _
        '                                             "Width As Integer"}
        '                Form1.methodsToolTip = Form1.LblMethTlst.ToArray()
        '            ElseIf Form1.ComboboxImage1.Items.Item(i).ImageIndex = 5 Then 'Listview 
        '                Form1.methods = {"Height", _
        '                                 "SetItem", _
        '                                 "Width"}
        '                Form1.methodsToolTipTitle = {"Height As Integer", _
        '                                             "SetItem(Item As String())", _
        '                                             "Width As Integer"}
        '                Form1.methodsToolTip = Form1.LstVwMethTlst.ToArray()
        '            ElseIf Form1.ComboboxImage1.Items.Item(i).ImageIndex = 6 Then 'Panel
        '                Form1.methods = {"BackgroundColor", "Column", "Height", "Layout", "Row", "Width"}
        '                Form1.methodsToolTipTitle = {"BackgroundColor As Color", _
        '                                             "-", _
        '                                             "Height As Integer", _
        '                                             "-", _
        '                                             "-", _
        '                                             "Width As Integer"}
        '                Form1.methodsToolTip = Form1.pnlMethTlst.ToArray()
        '            ElseIf Form1.ComboboxImage1.Items.Item(i).ImageIndex = 7 Then 'PasswordTextBox
        '                Form1.methods = {"BackgroundColor", "FontBold", "FontItalic", "FontSize", "Height", "Text", "TextColor", "Hint", "Enabled", "Width"}
        '                Form1.methodsToolTipTitle = {"BackgroundColor As Color", _
        '                                             "FontBold As Boolean", _
        '                                             "FontItalic As Boolean", _
        '                                             "FontSize As Integer", _
        '                                             "Height As Integer", _
        '                                             "Text As String", _
        '                                             "TextColor As Color", _
        '                                             "Hint As String", _
        '                                             "Enabled As Boolean", _
        '                                             "Width As Integer"}
        '                Form1.methodsToolTip = Form1.pswtxtbxMethTlst.ToArray()
        '            ElseIf Form1.ComboboxImage1.Items.Item(i).ImageIndex = 8 Then 'PictureBox Image
        '                Form1.methods = {"BackgroundColor", "Height", "Picture", "Width"}
        '                Form1.methodsToolTipTitle = {"BackgroundColor As Color", _
        '                                             "Height As Integer", _
        '                                             "-", _
        '                                             "Width As Integer"}
        '                Form1.methodsToolTip = Form1.pctrbxMethTlst.ToArray()
        '            ElseIf Form1.ComboboxImage1.Items.Item(i).ImageIndex = 9 Then 'ProgressBar
        '                Form1.methods = {"Height", "Value", "Max", "Width"}
        '                Form1.methodsToolTipTitle = {"Height As Integer", _
        '                                             "Value As Integer", _
        '                                             "-", _
        '                                             "Width As Integer"}
        '                Form1.methodsToolTip = Form1.progMethTlst.ToArray()
        '            ElseIf Form1.ComboboxImage1.Items.Item(i).ImageIndex = 10 Then 'RadioButton
        '                Form1.methods = {"BackgroundColor", "FontBold", "FontItalic", "FontSize", "Height", "Text", "TextColor", "Enabled", "Value", "Width"}
        '                Form1.methodsToolTipTitle = {"BackgroundColor As Color", _
        '                                             "FontBold As Boolean", _
        '                                             "FontItalic As Boolean", _
        '                                             "FontSize As Integer", _
        '                                             "Height As Integer", _
        '                                             "Text As String", _
        '                                             "TextColor As Color", _
        '                                             "Enabled As Boolean", _
        '                                             "Value As Boolean", _
        '                                             "Width As Integer"}
        '                Form1.methodsToolTip = Form1.rdiobtnMethTlst.ToArray()
        '            ElseIf Form1.ComboboxImage1.Items.Item(i).ImageIndex = 12 Then 'TextBox
        '                Form1.methods = {"BackgroundColor", "FontBold", "FontItalic", "FontSize", "Height", "InputType", "SingleLine", "Text", "TextColor", "Hint", "Enabled", "Width"} ' SingleLine InputType Not Sure because Aslamic 's Autocomplete.doc .... :P
        '                Form1.methodsToolTipTitle = {"BackgroundColor As Color", _
        '                                             "FontBold As Boolean", _
        '                                             "FontItalic As Boolean", _
        '                                             "FontSize As Integer", _
        '                                             "Height As Integer", _
        '                                             "-", _
        '                                             "-", _
        '                                             "Text As String", _
        '                                             "TextColor As Color", _
        '                                             "Hint As String", _
        '                                             "Enabled As Boolean", _
        '                                             "Width As Integer"}
        '                Form1.methodsToolTip = Form1.txtbxMethTlst.ToArray()
        '            ElseIf Form1.ComboboxImage1.Items.Item(i).ImageIndex = 13 Then 'Timer
        '                Form1.methods = {"Enabled", "Interval"}
        '                Form1.methodsToolTipTitle = {"Enabled As Boolean", _
        '                                             "Interval As Integer"}
        '                Form1.methodsToolTip = Form1.tmrMethTlst.ToArray()
        '            ElseIf Form1.ComboboxImage1.Items.Item(i).ImageIndex = 14 Then 'ComboBox VB4ASpinner
        '                Form1.methods = {"Height", "SetItem", "Enabled", "Value", " Index", "Width"}
        '                Form1.methodsToolTipTitle = {"Height As Integer", _
        '                                             "-", _
        '                                             "Enabled As Boolean", _
        '                                             "-", _
        '                                             "-", _
        '                                             "Width As Integer"}
        '                Form1.methodsToolTip = Form1.cmbbxMethTlst.ToArray()
        '            ElseIf Form1.ComboboxImage1.Items.Item(i).ImageIndex = 15 Then 'VB4AWeb
        '                Form1.methods = {"Height", "LoadURL("""")", "LoadURL2("""")", "GoBack()", "GoForward()", "Reload()", "Stop()", "SavePassword", "SaveFormData", "JSEnabled", "Width", "ZoomEnabled", "BuildinZoom"}
        '                Form1.methodsToolTipTitle = {"Height As Integer", _
        '                                             "LoadURL(Url As String)", _
        '                                             "LoadURL2(Url As String)", _
        '                                             "GoBack() [Function]", _
        '                                             "GoForward() [Function]", _
        '                                             "Reload() [Function]", _
        '                                             "Stop() [Function]", _
        '                                             "SavePassword As Boolean", _
        '                                             "SaveFormData As Boolean", _
        '                                             "JSEnabled As Boolean", _
        '                                             "Width As Integer", _
        '                                             "ZoomEnabled As Boolean", _
        '                                             "BuildinZoom As Boolean"}
        '                Form1.methodsToolTip = Form1.vb4wbMethTlst.ToArray()
        '            End If


        '            Form1.BuildAutocompleteMenu()

        '            ' Form1.popupMenu.Items.Refresh()
        '            ' Form1.popupMenu.Refresh()

        '            '  Form1.bhcomp = False



        '            Exit For


        '        ElseIf Form1.ComboboxImage1.Items.Item(i).ToString = fragmentText AndAlso Not fragmentText.Contains(".") AndAlso Replace(fragmentText, ".", "", , 1).Contains(".") Then ' DOUBBLLEEE DOOOT!! xD

        '        ElseIf i = Form1.ComboboxImage1.Items.Count - 1 AndAlso Not fragmentText.Contains(".") AndAlso Replace(fragmentText, ".", "", , 1).Contains(".") Then ' DOUBBLLEEE DOOOT!! xD

        '        ElseIf i = Form1.ComboboxImage1.Items.Count - 1 AndAlso Not fragmentText.Contains(".") AndAlso Not Replace(fragmentText, ".", "", , 1).Contains(".") Then

        '            'Console.WriteLine("IN: " & fragmentText)
        '            'If Not Form1.popupMenu.Items.FocussedItem.Text = "" Then 
        '            'If Form1.popupMenu.Visible = False Then
        '            '    ' Try ' i Dont Know How to Fix it :P HEEELP!
        '            '    If Form1.popupMenu.Items.FocussedItem.Text.StartsWith(fragmentText) Then fragmentText = Form1.popupMenu.Items.FocussedItem.Text

        '            '    '  Catch ex As NullReferenceException
        '            '    '  End Try
        '            'End If

        '            Form1.methods = {""}
        '            Form1.methodsToolTip = {""}
        '            Form1.methodsToolTipTitle = {""}

        '            Select Case fragmentText
        '                Case Is = "Application"
        '                    Form1.methods = {"AddMenu()", "AddMenuItem()", "Beep()", "CreatPlay()", "GetClip()", "GetDate()", "GetFreeMem()", "GetPreference()", "GetScreenDensity()", "GetScreenHeight()", "GetScreenWidth()", "GetStatusbarHeight()", "GetTime()", "GetTitlebarHeight()", "GetTotMem()", "Hide()", "Msgbox()", "PausePlay()", "Play()", "PlayMedia()", "PlayMedia2()", "Quit()", "ReleasePlay()", "SeekPlay()", "SetClip()", "SQLEXEC()", "SQLPREPARE()", "StartRec()", "StopPlay()", "StopRec()", "StorePreference()", "SwitchForm()", "ToastMessage()", "UserHide()", "UserQuit()", "VB4AInputBoxShow()", "VB4AMsgboxShow()", "VB4ANotify()", "VB4ASetExe()", "VB4AShell()"}

        '                    Form1.methodsToolTipTitle = {"AddMenu()", _
        '                                                 "AddMenuItem(Name As String)", _
        '                                                 "Beep()", _
        '                                                 "CreatPlay(FilenameAsset As String)", _
        '                                                 "GetClip()", _
        '                                                 "GetDate()", _
        '                                                 "GetFreeMem()", _
        '                                                 "GetPreference(Name As String)", _
        '                                                 "GetScreenDensity()", _
        '                                                 "GetScreenHeight()", _
        '                                                 "GetScreenWidth()", _
        '                                                 "GetStatusbarHeight()", _
        '                                                 "GetTime()", _
        '                                                 "GetTitlebarHeight()", _
        '                                                 "GetTotMem()", _
        '                                                 "Hide()", _
        '                                                 "Msgbox(title As String, msg As String, btn As String)", _
        '                                                 "PausePlay()", _
        '                                                 "Play()", _
        '                                                 "PlayMedia(FilenameAsset As String)", _
        '                                                 "PlayMedia2(FilenameSD As String)", _
        '                                                 "Quit()", _
        '                                                 "ReleasePlay()", _
        '                                                 "SeekPlay(pos As Integer)", _
        '                                                 "SetClip(String cont)", _
        '                                                 "SQLEXEC(DBName As String, SQL_CMD As String)", _
        '                                                 "SQLPREPARE(DBName As String, SQL_CMD As String, ItemSeperator As String, LineSeperator As String)", _
        '                                                 "StartRec(vSource As Integer, vFile As String, ErrorInfo As String)", _
        '                                                 "StopPlay()", _
        '                                                 "StopRec()", _
        '                                                 "StorePreference(Name As String, Value as Variant)", _
        '                                                 "SwitchForm(FormName As Form)", _
        '                                                 "ToastMessage(msg As String)", _
        '                                                 "UserHide(Title As String, Message As String, ButtonYes As String, ButtonNo As String)", _
        '                                                 "UserQuit(Title As String, Message As String, ButtonYes As String, ButtonNo As String)", _
        '                                                 "VB4AInputBoxShow(title As String, YesButton As String, NoButton As String)", _
        '                                                 "VB4AMsgboxShow(title As String, Msg As String, YesButton As String, NoButton As String)", _
        '                                                 "VB4ANotify(Icon As Integer, ID As Integer, Title As String, Title2 As String, Context As String)", _
        '                                                 "VB4ASetExe(exefile As String)", _
        '                                                 "VB4AShell(cmd As String)"}

        '                    Form1.methodsToolTip = Form1.ApplicationMethTlst.ToArray()
        '                Case Is = "Arrays"
        '                    Form1.methods = {"Filter()", "Join()", "Split()", "Split2()", "UBound()"}
        '                    Form1.methodsToolTipTitle = {"Filter(array As String(), str As String, include As Boolean)", _
        '                                                     "Join(array As String(), separator As String)", _
        '                                                     "Split(str As String, separator As String)", _
        '                                                     "Split2(str As String, separator As String, count As Integer)", _
        '                                                     "UBound(array As Variant,dim As Integer)"}
        '                    Form1.methodsToolTip = Form1.ArraysMethTlst.ToArray()
        '                Case Is = "VB4AInput"
        '                    Form1.methods = {"DATETIME", "MULTILINE", "NULL", "NUMBER", "PHONENUMBER", "TEXTS"}
        '                    Form1.methodsToolTipTitle = {"-", _
        '                                                 "-", _
        '                                                 "-", _
        '                                                 "-", _
        '                                                 "-", _
        '                                                 "-"}
        '                    Form1.methodsToolTip = Form1.VB4AInputMethTlst.ToArray()
        '                Case Is = "Files"
        '                    Form1.methods = {"Close()", "Delete()", "Delete2()", "Eof()", "Exists()", "Exists2()", "GetSDPath()", "IsDirectory()", "IsDirectory2()", "Mkdir()", "Mkdir2()", "Open()", "Open2()", "ReadBoolean()", "ReadByte()", "ReadDouble()", "ReadInteger()", "ReadLong()", "ReadShort()", "ReadSingle()", "ReadString()", "ReadTxt()", "Rename()", "Rename2()", "Rmdir()", "Rmdir2()", "Seek()", "Size()", "Unpack()", "WriteBoolean()", "WriteByte()", "WriteDouble()", "WriteInteger()", "WriteLong()", "WriteShort()", "WriteSingle()", "WriteString()", "WriteTxt()"}
        '                    Form1.methodsToolTipTitle = {"Close(Handle As Integer)", _
        '                                                 "Delete(name As String)", _
        '                                                 "Delete2(namesd As String)", _
        '                                                 "Eof(Handle As Integer)", _
        '                                                 "Exists(name As String)", _
        '                                                 "Exists2(namesd As String)", _
        '                                                 "GetSDPath()", _
        '                                                 "IsDirectory(name As String)", _
        '                                                 "IsDirectory2(namesd As String)", _
        '                                                 "Mkdir(name As String)", _
        '                                                 "Mkdir2(namesd As String)", _
        '                                                 "Open(Name As String)", _
        '                                                 "Open2(NameSD As String)", _
        '                                                 "ReadBoolean(Handle As Integer)", _
        '                                                 "ReadByte(Handle As Integer)", _
        '                                                 "ReadDouble(Handle As Integer)", _
        '                                                 "ReadInteger(Handle As Integer)", _
        '                                                 "ReadLong(Handle As Integer, value As Long)", _
        '                                                 "ReadShort(Handle As Integer)", _
        '                                                 "ReadSingle(Handle As Integer)", _
        '                                                 "ReadString(Handle As Integer, value As String)", _
        '                                                 "ReadTxt(FilePath As String)", _
        '                                                 "Rename(oldname As String, newname As String)", _
        '                                                 "Rename2(oldnamesd As String, newnamesd As String)", _
        '                                                 "Rmdir(name As String)", _
        '                                                 "Rmdir2(namesd As String)", _
        '                                                 "Seek(handle As Integer, offset As Long)", _
        '                                                 "Size(handle As Integer)", _
        '                                                 "Unpack(assetFname As String, outPathAndFname As String)", _
        '                                                 "WriteBoolean(Handle As Integer, value As Boolean)", _
        '                                                 "WriteByte(Handle As Integer, value As Byte)", _
        '                                                 "WriteDouble(Handle As Integer, value As Double)", _
        '                                                 "WriteInteger(Handle As Integer, value As Integer)", _
        '                                                 "WriteLong(Handle As Integer, value As Long)", _
        '                                                 "WriteShort(Handle As Integer, value As Short)", _
        '                                                 "WriteSingle(Handle As Integer, value As Single)", _
        '                                                 "WriteString(Handle As Integer, value As String)", _
        '                                                 "WriteTxt(FilePath As String,TxtToWrite As String)"}

        '                    Form1.methodsToolTip = Form1.FilesMethTlst.ToArray()
        '                Case Is = "Math"
        '                    Form1.methods = {"Abs()", _
        '                                     "Acos()", _
        '                                     "Asin()", _
        '                                     "Atn()", _
        '                                     "Atn2()", _
        '                                     "BigDecimicalAdd()", _
        '                                     "BigDecimicalCompare()", _
        '                                     "BigDecimicalDivide()", _
        '                                     "BigDecimicalSubstract()", _
        '                                     "BigDecimicalMultiply()", _
        '                                     "BigDecimicalRemainder()", _
        '                                     "BigDecimicalToPlainString()", _
        '                                     "Ceil()", _
        '                                     "Cos()", _
        '                                     "DegreesToRadians()", _
        '                                     "E", _
        '                                     "Exp()", _
        '                                     "Floor()", _
        '                                     "Int()", _
        '                                     "Log()", _
        '                                     "Max()", _
        '                                     "Min()", _
        '                                     "PI", _
        '                                     "RadiansToDegrees()", _
        '                                     "Rnd()", _
        '                                     "Round()", _
        '                                     "Sgn()", _
        '                                     "Sin()", _
        '                                     "Sqr()", _
        '                                     "Tan()"}

        '                    Form1.methodsToolTipTitle = {"Abs(v)", _
        '                                                 "Acos(v)", _
        '                                                 "Asin(v)", _
        '                                                 "Atn(v)", _
        '                                                 "Atn2(y,x)", _
        '                                                 "BigDecimicalAdd(a As String, b As String)", _
        '                                                 "BigDecimicalCompare(a As String, b As String)", _
        '                                                 "BigDecimicalDivide(a As String, b As String)", _
        '                                                 "BigDecimicalSubstract(a As String, b As String)", _
        '                                                 "BigDecimicalMultiply(a As String, b As String)", _
        '                                                 "BigDecimicalRemainder(a As String, b As String)", _
        '                                                 "BigDecimicalToPlainString(a As String, b As String)", _
        '                                                 "Ceil(v)", _
        '                                                 "Cos(v)", _
        '                                                 "DegreesToRadians(d)", _
        '                                                 "E", _
        '                                                 "Exp(v)", _
        '                                                 "Floor(v)", _
        '                                                 "Int(v)", _
        '                                                 "Log(v)", _
        '                                                 "Max(v1,v2)", _
        '                                                 "Min(v1,v2)", _
        '                                                 "PI", _
        '                                                 "RadiansToDegrees(d)", _
        '                                                 "Rnd()", _
        '                                                 "Round(v)", _
        '                                                 "Sgn(v)", _
        '                                                 "Sin(v)", _
        '                                                 "Sqr(v)", _
        '                                                 "Tan(v)"}
        '                    Form1.methodsToolTip = Form1.MathMethTlst.ToArray()
        '                Case Is = "AcceleromaterSenor"
        '                    Form1.methods = {"Available", "Enabled", "XAccel", "YAccel", "ZAccel"}
        '                    Form1.methodsToolTipTitle = {"Available As Boolean", "Enabled As Boolean", "XAccel As Integer", "YAccel As Integer", "ZAccel As Integer"}
        '                    Form1.methodsToolTip = Form1.AcceleromaterSenorMethTlst.ToArray()
        '                Case Is = "LocationSensor"
        '                    Form1.methods = {"Accuracy", "Altitude", "Available", "CurrentAddress", "Enabled", "Latitude", "Longitude"}
        '                    Form1.methodsToolTipTitle = {"Accuracy", "Altitude", "Available As Boolean", "CurrentAddress", "Enabled As Boolean", "Latitude", "Longitude"}
        '                    Form1.methodsToolTip = Form1.LocationSensorMethTlst.ToArray()
        '                Case Is = "OrientationSensor"
        '                    Form1.methods = {"Angle", "Available", "Enabled", "Magnitude", "Pitch", "Roll", "Yaw"}
        '                    Form1.methodsToolTipTitle = {"Angle", "Available As Boolean", "Enabled As Boolean", "Magnitude", "Pitch", "Roll", "Yaw"}
        '                    Form1.methodsToolTip = Form1.OrientationSensorMethTlst.ToArray()
        '                Case Is = "Phone"
        '                    Form1.methods = {"Available", "Call()", "GetURL()", "JumpURL()", "SendMail()", "SendSMS()", "SendMail()", "SocketSend()", "Vibrate()"}
        '                    Form1.methodsToolTipTitle = {"Available As Boolean", _
        '                                                 "Call(PhoneNumber As String)", _
        '                                                 "GetURL(url As String, codec As String)", _
        '                                                 "JumpURL(url As String)", _
        '                                                 "SendMail(Address As String, Text As String)", _
        '                                                 "SendSMS(PhoneNumber As String, Text As String, Warnings As String)", _
        '                                                 "SendMail(Address As String, Text As String)", _
        '                                                 "SocketSend(Ip As String, Port As Integer, Data As String)", _
        '                                                 "Vibrate(Duration As Integer)"}
        '                    Form1.methodsToolTip = Form1.PhoneMethTlst.ToArray()
        '                Case Is = "Component"
        '                    Form1.methods = {"COLOR_BLACK"}
        '                    Form1.methodsToolTipTitle = {"COLOR_BLACK As Color"}
        '                    Form1.methodsToolTip = Form1.ComponentMethTlst.ToArray()
        '                Case Is = "Dates"
        '                    Form1.methods = {"DateAdd()", _
        '                                     "DateValue()", _
        '                                     "Day()", _
        '                                     "FormatDate()", _
        '                                     "Hour()", _
        '                                     "Minute()", _
        '                                     "Month()", _
        '                                     "MonthName()", _
        '                                     "Now()", _
        '                                     "Second()", _
        '                                     "Timer()", _
        '                                     "Weekday()", _
        '                                     "WeekdayName()", _
        '                                     "Year()", _
        '                                     "DATE_YEAR", _
        '                                     "DATE_MONTH", _
        '                                     "DATE_DAY", _
        '                                     "DATE_WEEK", _
        '                                     "DATE_HOUR", _
        '                                     "DATE_MINUTE", _
        '                                     "DATE_SECOND", _
        '                                     "DATE_JANUARY", _
        '                                     "DATE_FEBRUARY", _
        '                                     "DATE_MARCH", _
        '                                     "DATE_APRIL", _
        '                                     "DATE_MAY", _
        '                                     "DATE_JUNE", _
        '                                     "DATE_JULY", _
        '                                     "DATE_AUGUST", _
        '                                     "DATE_SEPTEMBER", _
        '                                     "DATE_OCTOBER", _
        '                                     "DATE_NOVEMBER", _
        '                                     "DATE_DECEMBER", _
        '                                     "DATE_MONDAY", _
        '                                     "DATE_TUESDAY", _
        '                                     "DATE_WEDNESDAY", _
        '                                     "DATE_THURSDAY", _
        '                                     "DATE_FRIDAY", _
        '                                     "DATE_SATURDAY", _
        '                                     "DATE_SUNDAY"}
        '                    Form1.methodsToolTipTitle = {"DateAdd(date As Date, intervalKind As Integer, Interval As Integer)", _
        '                                                 "DateValue(value As String)", _
        '                                                 "Day(date As Date)", _
        '                                                 "FormatDate(date As Date)", _
        '                                                 "Hour(date As Date)", _
        '                                                 "Minute(date As Date)", _
        '                                                 "Month(date As Date)", _
        '                                                 "MonthName(date As Date)", _
        '                                                 "Now()", _
        '                                                 "Second(date As Date)", _
        '                                                 "Timer()", _
        '                                                 "Weekday(date As Date)", _
        '                                                 "WeekdayName(date As Date)", _
        '                                                 "Year(date As Date)", _
        '                                                 "DATE_YEAR", _
        '                                                 "DATE_MONTH", _
        '                                                 "DATE_DAY", _
        '                                                 "DATE_WEEK", _
        '                                                 "DATE_HOUR", _
        '                                                 "DATE_MINUTE", _
        '                                                 "DATE_SECOND", _
        '                                                 "DATE_JANUARY", _
        '                                                 "DATE_FEBRUARY", _
        '                                                 "DATE_MARCH", _
        '                                                 "DATE_APRIL", _
        '                                                 "DATE_MAY", _
        '                                                 "DATE_JUNE", _
        '                                                 "DATE_JULY", _
        '                                                 "DATE_AUGUST", _
        '                                                 "DATE_SEPTEMBER", _
        '                                                 "DATE_OCTOBER", _
        '                                                 "DATE_NOVEMBER", _
        '                                                 "DATE_DECEMBER", _
        '                                                 "DATE_MONDAY", _
        '                                                 "DATE_TUESDAY", _
        '                                                 "DATE_WEDNESDAY", _
        '                                                 "DATE_THURSDAY", _
        '                                                 "DATE_FRIDAY", _
        '                                                 "DATE_SATURDAY", _
        '                                                 "DATE_SUNDAY"}
        '                    Form1.methodsToolTip = Form1.DatesMethTlst.ToArray()
        '                Case Is = "Strings"
        '                    Form1.methods = {"AscW()", _
        '                                     "ChrW()", _
        '                                     "Format()", _
        '                                     "InStr()", _
        '                                     "InStrRev()", _
        '                                     "LCase()", _
        '                                     "Left()", _
        '                                     "Len()", _
        '                                     "LTrim()", _
        '                                     "Mid()", _
        '                                     "RC4()", _
        '                                     "Replace()", _
        '                                     "Right()", _
        '                                     "RTrim()", _
        '                                     "StrComp()", _
        '                                     "StrReverse()", _
        '                                     "Trim()", _
        '                                     "UCase()"}
        '                    Form1.methodsToolTipTitle = {"AscW(inChar As String)", _
        '                                                 "ChrW(unicodeVal As Long)", _
        '                                                 "Format(val As String, format As String)", _
        '                                                 "InStr(str1 As String, str2 As String, start As Integer)", _
        '                                                 "InStrRev(str1 As String, str2 As String, start As Integer)", _
        '                                                 "LCase(ByRef str As String)", _
        '                                                 "Left(str As String, len As Integer)", _
        '                                                 "Len(str As String)", _
        '                                                 "LTrim(ByRef str As String)", _
        '                                                 "Mid(str As String, start As Integer, len As Integer)", _
        '                                                 "RC4(str As String, key As String)", _
        '                                                 "Replace(ByRef str As String, find As String, replace As String, start As Integer, count As Integer)", _
        '                                                 "Right(str As String, len As Integer)", _
        '                                                 "RTrim(ByRef str As String)", _
        '                                                 "StrComp(str1 As String, str2 As String)", _
        '                                                 "StrReverse(ByRef str As String)", _
        '                                                 "Trim(ByRef str As String)", _
        '                                                 "UCase(ByRef str As String)"}

        '                    Form1.methodsToolTip = Form1.StringsMethTlst.ToArray()
        '                Case Is = "Conversions"
        '                    Form1.methods = {"Asc()", "Chr()", "Hex()", "Str()", "Val()"}
        '                    Form1.methodsToolTipTitle = {"Asc(str As String)", "Chr(value As Integer)", "Hex(v As Variant)", "Str(v As Variant)", "Val(v As String)"}
        '                    Form1.methodsToolTip = Form1.ConversionsMethTlst.ToArray()

        '                Case Is = "Base64"
        '                    Form1.methods = {"B64Encode()", "B64Decode()"}
        '                    Form1.methodsToolTipTitle = {"B64Encode( - )", "B64Decode(str As String)"}
        '                    Form1.methodsToolTip = Form1.Base64MethTlst.ToArray()

        '                Case Is = "Statistics"
        '                    Form1.methods = {"AsArray()", _
        '                                     "GenNorm()", _
        '                                     "GetAverage()", _
        '                                     "GetCount()", _
        '                                     "GetMax()", _
        '                                     "GetMedian()", _
        '                                     "GetMin()", _
        '                                     "GetSquareSum()", _
        '                                     "GetStandardDeviation()", _
        '                                     "GetSum()", _
        '                                     "GetVariance()", _
        '                                     "Sort()"}
        '                    Form1.methodsToolTipTitle = {"AsArray(SrtWithComma As String)", _
        '                                                 "GenNorm(count As Integer, mean As Double, sd As Double)", _
        '                                                 "GetAverage(inputarr As Double())", _
        '                                                 "GetCount(inputarr As Double())", _
        '                                                 "GetMax(inputarr As Double())", _
        '                                                 "GetMedian(inputarr As Double())", _
        '                                                 "GetMin(inputarr As Double())", _
        '                                                 "GetSquareSum(inputarr As Double())", _
        '                                                 "GetStandardDeviation(inputarr As Double())", _
        '                                                 "GetSum(inputarr As Double())", _
        '                                                 "GetVariance(inputarr As Double())", _
        '                                                 "Sort(inputarr As Double(), left As integer, right As Integer)"}
        '                    Form1.methodsToolTip = Form1.StatisticsMethTlst.ToArray()
        '                Case Is = "Matrix"
        '                    Form1.methods = {"Add()", _
        '                                     "AsMatrix()", _
        '                                     "GetComp()", _
        '                                     "GetDet()", _
        '                                     "Inverse()", _
        '                                     "Muliply()", _
        '                                     "ScalarMultiply()", _
        '                                     "Subtract()", _
        '                                     "Transpose()"}
        '                    Form1.methodsToolTipTitle = {"Add(inmatrix1 As double(,), inmatrix2 As Double(,))", _
        '                                                 "AsMatrix(inma As String)", _
        '                                                 "GetComp(inmatrix As Double(,), h As Integer, v As Integer)", _
        '                                                 "GetDet(inmatrix As Double(,))", _
        '                                                 "Inverse(inmatrix As Double(,))", _
        '                                                 "Muliply(inmatrix1 As Double(,), inmatrix2 as Double(,))", _
        '                                                 "ScalarMultiply(inmatrix As Double(,))", _
        '                                                 "Subtract(inmatrix1 As double(,), inmatrix2 As Double(,))", _
        '                                                 "Transpose(inmatrix As Double(,))"}

        '                    Form1.methodsToolTip = Form1.MatrixMethTlst.ToArray()
        '            End Select

        '            ' If Form1.bhcomp = True Then

        '            Form1.BuildAutocompleteMenu()
        '            If Form1.ecode = True Then Form1.ecode = False : SendKeys.Send(".")
        '            ' If Not Form1.methods.Count = 1 AndAlso Not Form1.methods(0) = "" Then SendKeys.Send(Form1.methods(0).Substring(0, 1)) : SendKeys.Send("{BACKSPACE}")
        '            'Form1.popupMenu.Visible = True
        '            ' Form1.popupMenu.Items.Refresh()
        '            ' Form1.popupMenu.Refresh()
        '            '  Form1.bhcomp = False
        '            Exit For
        '            'End If
        '        End If

        '        ' End If
        '    Next


        '    ' If Form1.ecode = True Then Form1.ecode = False : SendKeys.Send(".")


        '    If Regex.IsMatch(fragmentText, pattern) Then
        '        Text = InsertSpaces(fragmentText)
        '        If Text <> fragmentText Then
        '            '   Form1.b = False
        '            Return CompareResult.Visible
        '        End If
        '    End If
        '    ' Form1.b = True
        '    Return CompareResult.Hidden
        'End Function

        Public Function InsertSpaces(ByVal fragment As String) As String
            Dim m = Regex.Match(fragment, pattern)
            If m Is Nothing Then
                Return fragment
            End If
            If m.Groups(1).Value = "" AndAlso m.Groups(3).Value = "" Then
                Return fragment
            End If
            Return (m.Groups(1).Value & " " & m.Groups(2).Value & " " & m.Groups(3).Value).Trim()
        End Function

        Public Overrides Property ToolTipTitle() As String
            Get
                Return Text
            End Get
            Set(ByVal value As String)
            End Set
        End Property
    End Class


    Private Class InsertEnterSnippet
        Inherits AutocompleteItem
        Private enterPlace As Place = Place.Empty

        Public Sub New()
            MyBase.New("[Line break]")
        End Sub

        Public Overrides Function Compare(ByVal fragmentText As String) As CompareResult
            Dim r = Parent.Fragment.Clone()
            While r.Start.iChar > 0
                If r.CharBeforeStart = "}"c Then
                    enterPlace = r.Start
                    Return CompareResult.Visible
                End If

                r.GoLeftThroughFolded()
            End While

            Return CompareResult.Hidden
        End Function

        Public Overrides Function GetTextForReplace() As String
            'extend range
            Dim r As Range = Parent.Fragment
            Dim [end] As Place = r.[End]
            r.Start = enterPlace
            r.[End] = r.[End]
            'insert line break
            Return Environment.NewLine + r.Text
        End Function

        Public Overrides Sub OnSelected(ByVal popupMenu As AutocompleteMenu, ByVal e As SelectedEventArgs)
            MyBase.OnSelected(popupMenu, e)
            If Parent.Fragment.tb.AutoIndent Then
                Parent.Fragment.tb.DoAutoIndent()
            End If
        End Sub

        Public Overrides Property ToolTipTitle() As String
            Get
                Return "Insert line break after '}'"
            End Get
            Set(ByVal value As String)
            End Set
        End Property
    End Class

    Public Sub load_project()
        Dim OpenFileDialog1 As New OpenFileDialog ' 30/10/2016 Bug fix
        OpenFileDialog1.InitialDirectory = CurrentHardDriver & "VB4Android\Projects\"
        OpenFileDialog1.Filter = "VB4A Project Files |*.VB4AProj"
        OpenFileDialog1.RestoreDirectory = True
        OpenFileDialog1.Title = "Select Project File."

        If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            '   Try
            If Not OpenFileDialog1.FileName.StartsWith(CurrentHardDriver & "VB4Android\Projects\") Then
                Using New Centered_MessageBox(Me)
                    MsgBox("Copy or move your project at this location:" & vbNewLine & vbNewLine & CurrentHardDriver & "VB4Android\Projects\" & vbNewLine & "In order to be opened.", MsgBoxStyle.Information)
                End Using
                Exit Sub
            Else
                'Panel5.Enabled = True
                ' MsgBox(OpenFileDialog1.SafeFileName.Replace(".VB4AProj", ""))
                PropertyGrid1.Enabled = True
                AddToolStripMenuItem.Enabled = True
                ToolStripTextBox2.Enabled = True
                RemoveToolStripMenuItem.Enabled = True

                Combobox3.Items.Clear()
                idControl = 0
                Label4.Text = "- ( " & OpenFileDialog1.SafeFileName.Replace(".VB4AProj", "") & " )" : FileCreatedName = OpenFileDialog1.SafeFileName.Replace(".VB4AProj", "")
                Dim has_form_file As Boolean = False
                'Dim teleuteoformName As String = String.Empty
                PictureBox3.Image = Image.FromFile(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\icon.png")
                TextBox1.Text = CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\IconImage.ico"
                PictureBox3.SizeMode = PictureBoxSizeMode.CenterImage
                PictureBox3.Refresh()
                PictureBox3.SizeMode = PictureBoxSizeMode.Zoom
                PictureBox3.Refresh()

                Dim f As String = String.Empty
                For Each fl As String In My.Computer.FileSystem.GetFiles(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\CPROJ\")
                    If fl.EndsWith(".cpp") Or fl.EndsWith(".c") Or fl.EndsWith(".vb4acode") Then ToolStripComboBox2.Items.Add(fl.Split("\").Last) : f &= fl.Split("\").Last & "|"
                Next
                For Each ln As String In My.Computer.FileSystem.ReadAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\CPROJ\cproject.v4a").Split(vbNewLine)
                    If ln.Replace(vbLf, "").StartsWith("cprojname=") Then ToolStripTextBox2.Text = ln.Replace(vbLf, "").Substring(10, ln.Replace(vbLf, "").Length - 10) ' you will ask me why not ln.Length - 1 and ln.Replace(vbLf,"").Length (because i dont know if it will be there any chr10 and ... or why not then replace because it might be a cprojname=cprojname=   thats why :) )
                Next
                If My.Computer.FileSystem.GetFiles(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\CPROJ\").Count - 1 = 1 Then ' i am just checking for any file that has been set in folder after closing the project :P xD
                    My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\CPROJ\cproject.v4a", "cprojtype=Single" & vbNewLine &
                                                                                                                                              "cprojname=" & ToolStripTextBox2.Text & vbNewLine &
                                                                                                                                              "cfiles=" & f.Substring(0, f.Length - 1), False)

                ElseIf My.Computer.FileSystem.GetFiles(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\CPROJ\").Count - 1 > 1 Then
                    My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\CPROJ\cproject.v4a", "cprojtype=Multiple" & vbNewLine &
                                                                                                                                              "cprojname=" & ToolStripTextBox2.Text & vbNewLine &
                                                                                                                                              "cfiles=" & f.Substring(0, f.Length - 1), False)
                ElseIf My.Computer.FileSystem.GetFiles(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\CPROJ\").Count - 1 = 0 Then
                    My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\CPROJ\cproject.v4a", "cprojtype=" & vbNewLine &
                                                                                                                                              "cprojname=" & ToolStripTextBox2.Text & vbNewLine &
                                                                                                                                              "cfiles=", False)
                    My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj", My.Computer.FileSystem.ReadAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj").Replace("APPWITHC=True", "APPWITHC=False"), False)
                End If

                For Each Line As String In My.Computer.FileSystem.ReadAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj").Split(vbNewLine)
                    Line = Line.Replace(Chr(10), "")

                    If Line.StartsWith("Phone=") Then : CheckBox1.Checked = Convert.ToBoolean(Line.Replace("Phone=", ""))
                    ElseIf Line.StartsWith("TempSensor=") Then : CheckBox8.Checked = Convert.ToBoolean(Line.Replace("TempSensor=", ""))
                    ElseIf Line.StartsWith("GSensore=") Then : CheckBox3.Checked = Convert.ToBoolean(Line.Replace("GSensore=", ""))
                    ElseIf Line.StartsWith("Network=") Then : CheckBox4.Checked = Convert.ToBoolean(Line.Replace("Network=", ""))
                    ElseIf Line.StartsWith("AccSensor=") Then : CheckBox5.Checked = Convert.ToBoolean(Line.Replace("AccSensor=", ""))
                    ElseIf Line.StartsWith("GyroSensor=") Then : CheckBox6.Checked = Convert.ToBoolean(Line.Replace("GyroSensor=", ""))
                    ElseIf Line.StartsWith("LightSensor=") Then : CheckBox7.Checked = Convert.ToBoolean(Line.Replace("LightSensor=", ""))
                    ElseIf Line.StartsWith("GPS=") Then : CheckBox2.Checked = Convert.ToBoolean(Line.Replace("GPS=", ""))
                    ElseIf Line.StartsWith("ApplicationPKGName=") Then : ToolStripTextBox1.Text = Line.Replace("ApplicationPKGName=", "com.vb4a.") : BeforeCombo3 = Line.Replace("ApplicationPKGName=", "") & ".vb4a"
                    ElseIf Line.StartsWith("ApplicationName=") Then : TextBox2.Text = Line.Replace("ApplicationName=", "")
                    ElseIf Line.StartsWith("ApplicationTheme=") Then : ToolStripComboBox1.Text = Line.Replace("ApplicationTheme=Theme.", "")
                    ElseIf Line.StartsWith("ScreenSize=") Then : XofFormApp = Line.Split("=")(1).Split("x")(0) : YofFormApp = Line.Split("=")(1).Split("x")(1) : Panel5.Location = New Point(0, 0) : Panel5.Size = New Size(0, 0) : PictureBox4.Location = New Point(0, 0) : PictureBox4.Size = New Size(0, 0) : PictureBox4.Size = New Size(XofFormApp, YofFormApp) : Panel5.Size = New Size(XofFormApp + 2, YofFormApp + 2) : Panel5.Location = New Point(((PictureBox1.Size.Width - Panel5.Size.Width) \ 2) - 1, ((PictureBox1.Size.Height - Panel5.Size.Height) \ 2) - 1) : PictureBox4.Location = Panel5.Location + New Point(0, 1) : ToolStripTextBox3.Text = XofFormApp : ToolStripTextBox4.Text = YofFormApp
                    End If
                Next

                Dim FormsNumber As Integer = 0
                For Each File As String In My.Computer.FileSystem.GetFiles(OpenFileDialog1.FileName.Replace(OpenFileDialog1.SafeFileName, ""))
                    If File.EndsWith(".vb4a") Then
                        FormsNumber += 1
                    End If
                Next
                'Console.WriteLine("===== " & FormsNumber & " =====")

                For Each Line As String In My.Computer.FileSystem.ReadAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".CADel").Split(vbNewLine) ' because main form cant be deleted thats why i dont have "if not ...."
                    Line = Line.Replace(Chr(10), "")

                    ': SaveDeletedFormInf(Line)
                    If Line.StartsWith("AButton") Then
                        cntrafe1 += 1 : idControl += 1 : ButtonString &= Line & vbNewLine
                    ElseIf Line.StartsWith("Canvas") Then
                        cntrafe2 += 1 : idControl += 1 : CanvasString &= Line & vbNewLine
                    ElseIf Line.StartsWith("ACheckBox") Then
                        cntrafe3 += 1 : idControl += 1 : CheckBoxString &= Line & vbNewLine
                    ElseIf Line.StartsWith("ALabel") Then
                        cntrafe4 += 1 : idControl += 1 : LabelString &= Line & vbNewLine
                    ElseIf Line.StartsWith("AListview") Then
                        cntrafe5 += 1 : idControl += 1 : ListBoxString &= Line & vbNewLine
                    ElseIf Line.StartsWith("APanel") Then
                        cntrafe6 += 1 : idControl += 1 : PanelString &= Line & vbNewLine
                    ElseIf Line.StartsWith("PasswordTextBox") Then
                        cntrafe7 += 1 : idControl += 1 : PasswordTextBoxString &= Line & vbNewLine
                    ElseIf Line.StartsWith("APictureBox") Then
                        cntrafe8 += 1 : idControl += 1 : PictureBoxString &= Line & vbNewLine
                    ElseIf Line.StartsWith("AProgressBar") Then
                        cntrafe9 += 1 : idControl += 1 : ProgressBarString &= Line & vbNewLine
                    ElseIf Line.StartsWith("ARadioButton") Then
                        cntrafe10 += 1 : idControl += 1 : RadioButtonString &= Line & vbNewLine
                    ElseIf Line.StartsWith("ATextBox") Then
                        cntrafe11 += 1 : idControl += 1 : TextBoxString &= Line & vbNewLine
                    ElseIf Line.StartsWith("ATimer") Then
                        cntrafe12 += 1 : idControl += 1 : TimerString &= Line & vbNewLine
                    ElseIf Line.StartsWith("AComboBox") Then
                        cntrafe13 += 1 : idControl += 1 : ComboBoxString &= Line & vbNewLine
                    ElseIf Line.StartsWith("VB4AWeb") Then
                        cntrafe14 += 1 : idControl += 1 : VB4AWebString &= Line & vbNewLine
                    End If

                Next

                '  i knowww !!!!!!!! GoTo.... pffff i was borring that day
                For Each File As String In My.Computer.FileSystem.GetFiles(OpenFileDialog1.FileName.Replace(OpenFileDialog1.SafeFileName, ""))
                    'Console.WriteLine(File)
                    'file ειναι με το directory
hrfor2:             If File.Replace(OpenFileDialog1.FileName.Replace(OpenFileDialog1.SafeFileName, ""), "") = ToolStripTextBox1.Text.Replace("com.vb4a.", "") & ".vb4a" Then has_form_file = True
                    '   MsgBox(File)



                    If File.EndsWith(".vb4a") Then
                        If Not FormsNumber = 1 AndAlso File.Replace(OpenFileDialog1.FileName.Replace(OpenFileDialog1.SafeFileName, ""), "") = ToolStripTextBox1.Text.Replace("com.vb4a.", "") & ".vb4a" Then GoTo hrfor


                        'βαζω το name  απο το form στο combobox3 
                        'οριζω το FileCreatedName απο τωρα για καλό και για κακό



                        ListView1.Enabled = True
                        Panel1.Enabled = True
                        Panel2.Enabled = True
                        Panel3.Enabled = True
                        'Button12.Enabled = True
                        'Panel5.Enabled = True
                        ListView1.Hide()

                        Combobox3.Items.Add(New ComboBoxIconItem(File.Replace(OpenFileDialog1.FileName.Replace(OpenFileDialog1.SafeFileName, ""), ""), 0))
                        ' MsgBox(Line)





                        Dim controlEndProp As Boolean = False
                        Dim GnControl As String = String.Empty

                        ProjectSaveString = My.Computer.FileSystem.ReadAllText(File, Encoding.GetEncoding("gb2312"))
                        '  MsgBox(ProjectSaveString)
                        ' ''For Each Ln As String In My.Computer.FileSystem.ReadAllText(OpenFileDialog1.FileName.Replace(OpenFileDialog1.SafeFileName, "imgDEL.ini"))
                        ' ''    Ln = Ln.Replace(Chr(10), "")
                        ' ''    If ProjectSaveString.Contains(Ln) AndAlso Not Ln = "" Then
                        ' ''        ProjectSaveString = ProjectSaveString.Replace(Ln, "")
                        ' ''    End If

                        ' ''Next

                        For Each Line As String In ProjectSaveString.Split(vbNewLine)
                            Line = Line.Replace(Chr(10), "")
                            ' Dim ContPoints As String = Nothing
                            Dim PanelIS As New Panel

                            '                                                              -------------------------------------
                            If Line.StartsWith("<") AndAlso Not Line.StartsWith("</") Then idControl += 1 : controlEndProp = False : PropertiesOfControl = "" : GnControl = Line.Replace("<", "").Replace(">", "") Else If Line.StartsWith("</") Then controlEndProp = True






                            If Line.StartsWith("<AButton") Then : PropertyGrid1.SelectedObject = New ClsButton
                            ElseIf Line.StartsWith("<Canvas") Then : PropertyGrid1.SelectedObject = New ClsCanvas
                            ElseIf Line.StartsWith("<ACheckBox") Then : PropertyGrid1.SelectedObject = New ClsCheckBox
                            ElseIf Line.StartsWith("<ALabel") Then : PropertyGrid1.SelectedObject = New ClsLabel
                            ElseIf Line.StartsWith("<AListview") Then : PropertyGrid1.SelectedObject = New ClsListView
                            ElseIf Line.StartsWith("<APanel") Then : PropertyGrid1.SelectedObject = New clsPanel
                            ElseIf Line.StartsWith("<PasswordTextBox") Then : PropertyGrid1.SelectedObject = New clsPasswordTextBox
                            ElseIf Line.StartsWith("<APictureBox") Then : PropertyGrid1.SelectedObject = New clsPictureBox
                            ElseIf Line.StartsWith("<AProgressBar") Then : PropertyGrid1.SelectedObject = New clsProgressBar
                            ElseIf Line.StartsWith("<ARadioButton") Then : PropertyGrid1.SelectedObject = New ClsRadioButton
                            ElseIf Line.StartsWith("<ATextBox") Then : PropertyGrid1.SelectedObject = New clsTextBox
                            ElseIf Line.StartsWith("<ATimer") Then : PropertyGrid1.SelectedObject = New clsTimer
                            ElseIf Line.StartsWith("<AComboBox") Then : PropertyGrid1.SelectedObject = New clsComboBox
                            ElseIf Line.StartsWith("<VB4AWeb") Then : PropertyGrid1.SelectedObject = New clsVB4AWeb
                            End If

                            If controlEndProp = False Then
                                PropertiesOfControl &= Line & vbNewLine 'If Not Line = "--Form--" Then ... Else PropertiesOfControl = ""
                            ElseIf Not Line = Nothing Then
                                If GnControl.StartsWith("AButton") Then
                                    PropertiesOfControl &= "</" & GnControl & ">"
                                    ' MsgBox(PropertiesOfControl)
                                    'ckekarw ama einai se panel -------------------------------apo to projectsavestring
                                    For Each lines As String In PropertiesOfControl.Split(vbNewLine)
                                        lines = lines.Replace(Chr(10), "")
                                        If lines = "IsOnPanel=" Then PanelIS = Panel5 : Exit For Else If lines.StartsWith("IsOnPanel=") Then PanelIS = DirectCast(FindControl(lines.Replace("IsOnPanel=", ""), Panel5), Panel) : Exit For ' MsgBox(lines.Replace("IsOnPanel=", ""))
                                    Next
                                    'MsgBox(PropertiesOfControl)
                                    AButton = New BorderlessButton

                                    With AButton
                                        .Enabled = True
                                        .Name = GnControl
                                        .Text = ""
                                        .Location = New Point(0, 0)
                                        .Font = New Font(Font.FontFamily, 10)
                                        .BackgroundImageLayout = ImageLayout.Stretch
                                        .FlatStyle = FlatStyle.Popup
                                        .Hide()
                                    End With
                                    ' MsgBox(AButton.Name & " in panel " & PanelIS.Name & vbNewLine & PropertiesOfControl)
                                    PanelIS.Controls.Add(AButton)
                                    ButtonString &= (GnControl & vbNewLine)


                                    AButton.BringToFront()

                                    'to torrentz einai pou 8elei 10 gia na ginei swsta
                                    If Not File.EndsWith(ToolStripTextBox1.Text.Replace("com.vb4a.", "") & ".vb4a") Then
                                        cntrafe1 += 1
                                        ' Console.WriteLine(cntrafe1)
                                    End If

                                    '  Console.WriteLine(cntrafe1)
                                    GenControl = AButton
                                    rc = New ResizeableControl(AButton)
                                    '  ClickGenControlsPropert(AButton.Name)

                                    For Each Lines As String In PropertiesOfControl.Split(vbNewLine)
                                        Lines = Lines.Replace(Chr(10), "")
                                        'DirectCast(FindControl(RetriveInWhatPanelis(DirectCast(FindControl(GnControl, Me), Control)), Me), Panel).Controls(GnControl).Tag
                                        If Lines.StartsWith("Name=") Then : AButton.Tag = Lines.Replace("Name=", "") ': Combobox1.Text = Lines.Replace("Name=", "")
                                        ElseIf Lines.StartsWith("Text=") Then : AButton.Text = Lines.Replace("Text=", Nothing)
                                        ElseIf Lines.StartsWith("FontBold=") Then
                                            If Lines.Replace("FontBold=", "") = "True" Then
                                                AButton.Font = New Font(Font.FontFamily, AButton.Font.Size, FontStyle.Bold)
                                            End If
                                        ElseIf Lines.StartsWith("FontSize=") Then
                                            '  MsgBox(AButton.Font.Size)
                                            If AButton.Font.Bold = True Then
                                                AButton.Font = New Font(Font.FontFamily, CInt(Lines.Replace("FontSize=", "")), FontStyle.Bold)
                                            Else
                                                AButton.Font = New Font(Font.FontFamily, CInt(Lines.Replace("FontSize=", "")), FontStyle.Regular)
                                            End If
                                        ElseIf Lines.StartsWith("FontItalic=") Then
                                            If Lines.Replace("FontItalic=", "") = "True" Then
                                                AButton.Font = New Font(Font.FontFamily, AButton.Font.Size, FontStyle.Italic)
                                            End If
                                        ElseIf Lines.StartsWith("FontTypeface=") Then
                                        ElseIf Lines.StartsWith("TextAlign=") Then
                                            Select Case Lines.Replace("TextAlign=", Nothing)
                                                Case Is = "LEFT" : AButton.TextAlign = ContentAlignment.MiddleLeft
                                                Case Is = "CENTER" : AButton.TextAlign = ContentAlignment.MiddleCenter
                                                Case Is = "RIGHT" : AButton.TextAlign = ContentAlignment.MiddleRight
                                                Case Is = "LEFT_TOP" : AButton.TextAlign = ContentAlignment.TopLeft
                                                Case Is = "LEFT_CENTER" : AButton.TextAlign = ContentAlignment.MiddleLeft
                                                Case Is = "LEFT_BOTTOM" : AButton.TextAlign = ContentAlignment.BottomLeft
                                                Case Is = "CENTER_TOP" : AButton.TextAlign = ContentAlignment.TopCenter
                                                Case Is = "CENTER_CENTER" : AButton.TextAlign = ContentAlignment.MiddleCenter
                                                Case Is = "CENTER_BOTTOM" : AButton.TextAlign = ContentAlignment.BottomCenter
                                                Case Is = "RIGHT_TOP" : AButton.TextAlign = ContentAlignment.TopRight
                                                Case Is = "RIGHT_CENTER" : AButton.TextAlign = ContentAlignment.MiddleRight
                                                Case Is = "RIGHT_BOTTOM" : AButton.TextAlign = ContentAlignment.BottomRight
                                            End Select
                                        ElseIf Lines.StartsWith("BackColor=") Then : AButton.BackColor = ColourFromData(Lines.Replace("BackColor=", Nothing))
                                        ElseIf Lines.StartsWith("ForeColor=") Then : AButton.ForeColor = ColourFromData(Lines.Replace("ForeColor=", Nothing)) 'Replace("Location={X=", Nothing).Replace("}", Nothing).Replace("Y=", Nothing)
                                        ElseIf Lines.StartsWith("Location=") Then : AButton.Location = New Point(CInt(Lines.Split(";")(0).Replace("Location={", Nothing)), CInt(Lines.Split(";")(1).Replace("}", Nothing)))
                                        ElseIf Lines.StartsWith("Size=") Then : AButton.Size = New Size(New Point(CInt(Lines.Split(";")(0).Replace("Size={", Nothing)), CInt(Lines.Split(";")(1).Replace("}", Nothing))))
                                        ElseIf Lines.StartsWith("Image=") Then : If Lines.Replace("Image=", Nothing) = Nothing Then : Else : AButton.BackgroundImage = Image.FromFile(Lines.Replace("Image=", Nothing)) : End If
                                        ElseIf Lines.StartsWith("Enabled=") Then
                                        End If
                                    Next

                                    Combobox1.Items.Add(New ComboBoxIconItem(AButton.Tag, 0))
                                    ' ClickGenControlsPropert(AButton.Name)
                                    'AButton.Hide()
                                    ' MsgBox(PropertiesOfControl)

                                ElseIf GnControl.StartsWith("Canvas") Then
                                    PropertiesOfControl &= "</" & GnControl & ">"

                                    For Each lines As String In PropertiesOfControl.Split(vbNewLine)
                                        lines = lines.Replace(Chr(10), "")
                                        If lines = "IsOnPanel=" Then PanelIS = Panel5 : Exit For Else If lines.StartsWith("IsOnPanel=") Then PanelIS = DirectCast(FindControl(lines.Replace("IsOnPanel=", ""), Panel5), Panel) : Exit For ' MsgBox(lines.Replace("IsOnPanel=", ""))
                                    Next

                                    Canvas = New PictureBox

                                    With Canvas
                                        .Enabled = True
                                        .Name = GnControl
                                        .Location = New Point(0, 0)
                                        .BorderStyle = BorderStyle.FixedSingle
                                        .BackgroundImageLayout = ImageLayout.Stretch
                                        .Hide()
                                    End With

                                    PanelIS.Controls.Add(Canvas)
                                    CanvasString &= (GnControl & vbNewLine)


                                    If Not File.EndsWith(ToolStripTextBox1.Text.Replace("com.vb4a.", "") & ".vb4a") Then
                                        cntrafe2 += 1
                                    End If

                                    GenControl = Canvas
                                    rc = New ResizeableControl(Canvas)

                                    For Each Lines As String In PropertiesOfControl.Split(vbNewLine)
                                        Lines = Lines.Replace(Chr(10), "")

                                        If Lines.StartsWith("Name=") Then : Canvas.Tag = Lines.Replace("Name=", "")
                                        ElseIf Lines.StartsWith("BackgroundColor=") Then : Canvas.BackColor = ColourFromData(Lines.Replace("BackgroundColor=", Nothing))
                                        ElseIf Lines.StartsWith("Location=") Then : Canvas.Location = New Point(CInt(Lines.Split(";")(0).Replace("Location={", Nothing)), CInt(Lines.Split(";")(1).Replace("}", Nothing)))
                                        ElseIf Lines.StartsWith("Size=") Then : Canvas.Size = New Size(New Point(CInt(Lines.Split(";")(0).Replace("Size={", Nothing)), CInt(Lines.Split(";")(1).Replace("}", Nothing))))
                                        ElseIf Lines.StartsWith("BackgroundImage=") Then : If Lines.Replace("BackgroundImage=", Nothing) = Nothing Then : Else : Canvas.BackgroundImage = Image.FromFile(Lines.Replace("BackgroundImage=", Nothing)) : End If
                                        End If
                                    Next

                                    Combobox1.Items.Add(New ComboBoxIconItem(Canvas.Tag, 1))


                                ElseIf GnControl.StartsWith("ACheckBox") Then
                                    PropertiesOfControl &= "</" & GnControl & ">"

                                    For Each lines As String In PropertiesOfControl.Split(vbNewLine)
                                        lines = lines.Replace(Chr(10), "")
                                        If lines = "IsOnPanel=" Then PanelIS = Panel5 : Exit For Else If lines.StartsWith("IsOnPanel=") Then PanelIS = DirectCast(FindControl(lines.Replace("IsOnPanel=", ""), Panel5), Panel) : Exit For ' MsgBox(lines.Replace("IsOnPanel=", ""))
                                    Next

                                    ACheckBox = New CheckBox

                                    With ACheckBox
                                        .Enabled = True
                                        .Name = GnControl
                                        .Text = ""
                                        .Location = New Point(0, 0)
                                        .FlatStyle = FlatStyle.Flat
                                        .AutoSize = False
                                        .Hide()
                                    End With

                                    PanelIS.Controls.Add(ACheckBox)
                                    CheckBoxString &= (GnControl & vbNewLine)


                                    If Not File.EndsWith(ToolStripTextBox1.Text.Replace("com.vb4a.", "") & ".vb4a") Then
                                        cntrafe3 += 1
                                    End If

                                    GenControl = ACheckBox
                                    rc = New ResizeableControl(ACheckBox)

                                    For Each Lines As String In PropertiesOfControl.Split(vbNewLine)
                                        Lines = Lines.Replace(Chr(10), "")

                                        If Lines.StartsWith("Name=") Then : ACheckBox.Tag = Lines.Replace("Name=", "")
                                        ElseIf Lines.StartsWith("Text=") Then : ACheckBox.Text = Lines.Replace("Text=", Nothing)
                                        ElseIf Lines.StartsWith("FontBold=") Then
                                            If Lines.Replace("FontBold=", "") = "True" Then
                                                ACheckBox.Font = New Font(Font.FontFamily, ACheckBox.Font.Size, FontStyle.Bold)
                                            End If
                                        ElseIf Lines.StartsWith("FontSize=") Then
                                            If ACheckBox.Font.Bold = True Then
                                                ACheckBox.Font = New Font(Font.FontFamily, CInt(Lines.Replace("FontSize=", "")), FontStyle.Bold)
                                            Else
                                                ACheckBox.Font = New Font(Font.FontFamily, CInt(Lines.Replace("FontSize=", "")), FontStyle.Regular)
                                            End If
                                        ElseIf Lines.StartsWith("FontItalic=") Then
                                            If Lines.Replace("FontItalic=", "") = "True" Then
                                                ACheckBox.Font = New Font(Font.FontFamily, ACheckBox.Font.Size, FontStyle.Italic)
                                            End If
                                        ElseIf Lines.StartsWith("FontTypeface=") Then
                                        ElseIf Lines.StartsWith("TextAlign=") Then
                                            Select Case Lines.Replace("TextAlign=", Nothing)
                                                Case Is = "LEFT" : ACheckBox.TextAlign = ContentAlignment.MiddleLeft
                                                Case Is = "CENTER" : ACheckBox.TextAlign = ContentAlignment.MiddleCenter
                                                Case Is = "RIGHT" : ACheckBox.TextAlign = ContentAlignment.MiddleRight
                                                Case Is = "LEFT_TOP" : ACheckBox.TextAlign = ContentAlignment.TopLeft
                                                Case Is = "LEFT_CENTER" : ACheckBox.TextAlign = ContentAlignment.MiddleLeft
                                                Case Is = "LEFT_BOTTOM" : ACheckBox.TextAlign = ContentAlignment.BottomLeft
                                                Case Is = "CENTER_TOP" : ACheckBox.TextAlign = ContentAlignment.TopCenter
                                                Case Is = "CENTER_CENTER" : ACheckBox.TextAlign = ContentAlignment.MiddleCenter
                                                Case Is = "CENTER_BOTTOM" : ACheckBox.TextAlign = ContentAlignment.BottomCenter
                                                Case Is = "RIGHT_TOP" : ACheckBox.TextAlign = ContentAlignment.TopRight
                                                Case Is = "RIGHT_CENTER" : ACheckBox.TextAlign = ContentAlignment.MiddleRight
                                                Case Is = "RIGHT_BOTTOM" : ACheckBox.TextAlign = ContentAlignment.BottomRight
                                            End Select
                                        ElseIf Lines.StartsWith("BackColor=") Then : ACheckBox.BackColor = ColourFromData(Lines.Replace("BackColor=", Nothing))
                                        ElseIf Lines.StartsWith("ForeColor=") Then : ACheckBox.ForeColor = ColourFromData(Lines.Replace("ForeColor=", Nothing))
                                        ElseIf Lines.StartsWith("Location=") Then : ACheckBox.Location = New Point(CInt(Lines.Split(";")(0).Replace("Location={", Nothing)), CInt(Lines.Split(";")(1).Replace("}", Nothing)))
                                        ElseIf Lines.StartsWith("Size=") Then : ACheckBox.Size = New Size(New Point(CInt(Lines.Split(";")(0).Replace("Size={", Nothing)), CInt(Lines.Split(";")(1).Replace("}", Nothing))))
                                        ElseIf Lines.StartsWith("Checked=") Then : ACheckBox.Checked = Convert.ToBoolean(Lines.Replace("Checked=", Nothing))
                                        ElseIf Lines.StartsWith("Enabled=") Then
                                        End If
                                    Next


                                    Combobox1.Items.Add(New ComboBoxIconItem(ACheckBox.Tag, 2))


                                ElseIf GnControl.StartsWith("ALabel") Then
                                    PropertiesOfControl &= "</" & GnControl & ">"

                                    For Each lines As String In PropertiesOfControl.Split(vbNewLine)
                                        lines = lines.Replace(Chr(10), "")
                                        If lines = "IsOnPanel=" Then PanelIS = Panel5 : Exit For Else If lines.StartsWith("IsOnPanel=") Then PanelIS = DirectCast(FindControl(lines.Replace("IsOnPanel=", ""), Panel5), Panel) : Exit For ' MsgBox(lines.Replace("IsOnPanel=", ""))
                                    Next

                                    ALabel = New Label

                                    With ALabel
                                        .Enabled = True
                                        .Name = GnControl
                                        .Text = ""
                                        .Location = New Point(0, 0)
                                        .FlatStyle = FlatStyle.Flat
                                        AutoSize = False
                                        .Hide()
                                    End With

                                    PanelIS.Controls.Add(ALabel)
                                    LabelString &= (GnControl & vbNewLine)


                                    If Not File.EndsWith(ToolStripTextBox1.Text.Replace("com.vb4a.", "") & ".vb4a") Then
                                        cntrafe4 += 1
                                    End If

                                    GenControl = ALabel
                                    rc = New ResizeableControl(ALabel)

                                    For Each Lines As String In PropertiesOfControl.Split(vbNewLine)
                                        Lines = Lines.Replace(Chr(10), "")

                                        If Lines.StartsWith("Name=") Then : ALabel.Tag = Lines.Replace("Name=", Nothing)
                                        ElseIf Lines.StartsWith("Text=") Then : ALabel.Text = Lines.Replace("Text=", Nothing)
                                        ElseIf Lines.StartsWith("FontBold=") Then
                                            If Lines.Replace("FontBold=", "") = "True" Then
                                                ALabel.Font = New Font(Font.FontFamily, ALabel.Font.Size, FontStyle.Bold)
                                            End If
                                        ElseIf Lines.StartsWith("FontSize=") Then
                                            If ALabel.Font.Bold = True Then
                                                ALabel.Font = New Font(Font.FontFamily, CInt(Lines.Replace("FontSize=", "")), FontStyle.Bold)
                                            Else
                                                ALabel.Font = New Font(Font.FontFamily, CInt(Lines.Replace("FontSize=", "")), FontStyle.Regular)
                                            End If
                                        ElseIf Lines.StartsWith("FontItalic=") Then
                                            If Lines.Replace("FontItalic=", "") = "True" Then
                                                ALabel.Font = New Font(Font.FontFamily, ALabel.Font.Size, FontStyle.Italic)
                                            End If
                                        ElseIf Lines.StartsWith("FontTypeface=") Then
                                        ElseIf Lines.StartsWith("TextAlign=") Then
                                            Select Case Lines.Replace("TextAlign=", Nothing)
                                                Case Is = "LEFT" : ALabel.TextAlign = ContentAlignment.MiddleLeft
                                                Case Is = "CENTER" : ALabel.TextAlign = ContentAlignment.MiddleCenter
                                                Case Is = "RIGHT" : ALabel.TextAlign = ContentAlignment.MiddleRight
                                                Case Is = "LEFT_TOP" : ALabel.TextAlign = ContentAlignment.TopLeft
                                                Case Is = "LEFT_CENTER" : ALabel.TextAlign = ContentAlignment.MiddleLeft
                                                Case Is = "LEFT_BOTTOM" : ALabel.TextAlign = ContentAlignment.BottomLeft
                                                Case Is = "CENTER_TOP" : ALabel.TextAlign = ContentAlignment.TopCenter
                                                Case Is = "CENTER_CENTER" : ALabel.TextAlign = ContentAlignment.MiddleCenter
                                                Case Is = "CENTER_BOTTOM" : ALabel.TextAlign = ContentAlignment.BottomCenter
                                                Case Is = "RIGHT_TOP" : ALabel.TextAlign = ContentAlignment.TopRight
                                                Case Is = "RIGHT_CENTER" : ALabel.TextAlign = ContentAlignment.MiddleRight
                                                Case Is = "RIGHT_BOTTOM" : ALabel.TextAlign = ContentAlignment.BottomRight
                                            End Select
                                        ElseIf Lines.StartsWith("BackColor=") Then : ALabel.BackColor = ColourFromData(Lines.Replace("BackColor=", Nothing))
                                        ElseIf Lines.StartsWith("ForeColor=") Then : ALabel.ForeColor = ColourFromData(Lines.Replace("ForeColor=", Nothing))
                                        ElseIf Lines.StartsWith("Location=") Then : ALabel.Location = New Point(CInt(Lines.Split(";")(0).Replace("Location={", Nothing)), CInt(Lines.Split(";")(1).Replace("}", Nothing)))
                                        ElseIf Lines.StartsWith("Size=") Then : ALabel.Size = New Size(New Point(CInt(Lines.Split(";")(0).Replace("Size={", Nothing)), CInt(Lines.Split(";")(1).Replace("}", Nothing))))
                                        ElseIf Lines.StartsWith("Enabled=") Then
                                        End If
                                    Next

                                    Combobox1.Items.Add(New ComboBoxIconItem(ALabel.Tag, 4))

                                ElseIf GnControl.StartsWith("AListview") Then
                                    PropertiesOfControl &= "</" & GnControl & ">"

                                    For Each lines As String In PropertiesOfControl.Split(vbNewLine)
                                        lines = lines.Replace(Chr(10), "")
                                        If lines = "IsOnPanel=" Then PanelIS = Panel5 : Exit For Else If lines.StartsWith("IsOnPanel=") Then PanelIS = DirectCast(FindControl(lines.Replace("IsOnPanel=", ""), Panel5), Panel) : Exit For ' MsgBox(lines.Replace("IsOnPanel=", ""))
                                    Next

                                    AListBox = New PictureBox

                                    With AListBox
                                        .Enabled = True
                                        .Name = GnControl
                                        .Location = New Point(0, 0)
                                        .BorderStyle = BorderStyle.FixedSingle
                                        .Hide()
                                    End With

                                    PanelIS.Controls.Add(AListBox)
                                    ListBoxString &= (GnControl & vbNewLine)


                                    If Not File.EndsWith(ToolStripTextBox1.Text.Replace("com.vb4a.", "") & ".vb4a") Then
                                        cntrafe5 += 1
                                    End If

                                    GenControl = AListBox
                                    rc = New ResizeableControl(AListBox)

                                    For Each Lines As String In PropertiesOfControl.Split(vbNewLine)
                                        Lines = Lines.Replace(Chr(10), "")

                                        If Lines.StartsWith("Name=") Then : AListBox.Tag = Lines.Replace("Name=", "")
                                        ElseIf Lines.StartsWith("BackColor=") Then : AListBox.BackColor = ColourFromData(Lines.Replace("BackColor=", Nothing))
                                        ElseIf Lines.StartsWith("Location=") Then : AListBox.Location = New Point(CInt(Lines.Split(";")(0).Replace("Location={", Nothing)), CInt(Lines.Split(";")(1).Replace("}", Nothing)))
                                        ElseIf Lines.StartsWith("Size=") Then : AListBox.Size = New Size(New Point(CInt(Lines.Split(";")(0).Replace("Size={", Nothing)), CInt(Lines.Split(";")(1).Replace("}", Nothing))))
                                        ElseIf Lines.StartsWith("Enabled=") Then
                                        End If

                                    Next
                                    Combobox1.Items.Add(New ComboBoxIconItem(AListBox.Tag, 5))

                                ElseIf GnControl.StartsWith("APanel") Then
                                    PropertiesOfControl &= "</" & GnControl & ">"

                                    'MsgBox(PropertiesOfControl)

                                    For Each lines As String In PropertiesOfControl.Split(vbNewLine)
                                        lines = lines.Replace(Chr(10), "")
                                        If lines = "IsOnPanel=" Then PanelIS = Panel5 : Exit For Else If lines.StartsWith("IsOnPanel=") Then PanelIS = DirectCast(FindControl(lines.Replace("IsOnPanel=", ""), Panel5), Panel) : Exit For ' MsgBox(lines.Replace("IsOnPanel=", ""))
                                    Next

                                    APanel = New Panel

                                    With APanel
                                        .Enabled = True
                                        .Name = GnControl
                                        .Location = New Point(0, 0)
                                        .BorderStyle = BorderStyle.Fixed3D
                                        .BackgroundImageLayout = ImageLayout.Stretch
                                        .Hide()
                                    End With

                                    PanelIS.Controls.Add(APanel)
                                    PanelString &= (GnControl & vbNewLine)


                                    If Not File.EndsWith(ToolStripTextBox1.Text.Replace("com.vb4a.", "") & ".vb4a") Then
                                        cntrafe6 += 1
                                    End If

                                    GenControl = APanel
                                    rc = New ResizeableControl(APanel)

                                    For Each Lines As String In PropertiesOfControl.Split(vbNewLine)
                                        Lines = Lines.Replace(Chr(10), "")

                                        If Lines.StartsWith("Name=") Then : APanel.Tag = Lines.Replace("Name=", "")
                                        ElseIf Lines.StartsWith("BackgroundColor=") Then : APanel.BackColor = ColourFromData(Lines.Replace("BackgroundColor=", Nothing))
                                        ElseIf Lines.StartsWith("Location=") Then : APanel.Location = New Point(CInt(Lines.Split(";")(0).Replace("Location={", Nothing)), CInt(Lines.Split(";")(1).Replace("}", Nothing)))
                                        ElseIf Lines.StartsWith("Size=") Then : APanel.Size = New Size(New Point(CInt(Lines.Split(";")(0).Replace("Size={", Nothing)), CInt(Lines.Split(";")(1).Replace("}", Nothing))))
                                        ElseIf Lines.StartsWith("BackgroundImage=") Then : If Lines.Replace("BackgroundImage=", Nothing) = Nothing Then : Else : APanel.BackgroundImage = Image.FromFile(Lines.Replace("BackgroundImage=", Nothing)) : End If
                                        ElseIf Lines.StartsWith("Enabled=") Then
                                        End If
                                    Next

                                    Combobox1.Items.Add(New ComboBoxIconItem(APanel.Tag, 6))


                                ElseIf GnControl.StartsWith("PasswordTextBox") Then
                                    PropertiesOfControl &= "</" & GnControl & ">"


                                    For Each lines As String In PropertiesOfControl.Split(vbNewLine)
                                        lines = lines.Replace(Chr(10), "")
                                        If lines = "IsOnPanel=" Then PanelIS = Panel5 : Exit For Else If lines.StartsWith("IsOnPanel=") Then PanelIS = DirectCast(FindControl(lines.Replace("IsOnPanel=", ""), Panel5), Panel) : Exit For ' MsgBox(lines.Replace("IsOnPanel=", ""))
                                    Next

                                    PasswordTextBox = New TextBox

                                    With PasswordTextBox
                                        .Enabled = True
                                        .BorderStyle = BorderStyle.FixedSingle
                                        .Name = GnControl
                                        .Location = New Point(0, 0)
                                        .Cursor = Cursors.NoMove2D
                                        .Multiline = True
                                        .ReadOnly = True
                                        .Hide()
                                    End With

                                    PanelIS.Controls.Add(PasswordTextBox)
                                    PasswordTextBoxString &= (GnControl & vbNewLine)


                                    If Not File.EndsWith(ToolStripTextBox1.Text.Replace("com.vb4a.", "") & ".vb4a") Then
                                        cntrafe7 += 1
                                    End If

                                    GenControl = PasswordTextBox
                                    rc = New ResizeableControl(PasswordTextBox)

                                    For Each Lines As String In PropertiesOfControl.Split(vbNewLine)
                                        Lines = Lines.Replace(Chr(10), "")

                                        If Lines.StartsWith("Name=") Then : PasswordTextBox.Tag = Lines.Replace("Name=", "")
                                        ElseIf Lines.StartsWith("Text=") Then : PasswordTextBox.Text = Lines.Replace("Text=", Nothing)
                                        ElseIf Lines.StartsWith("FontBold=") Then
                                            If Lines.Replace("FontBold=", "") = "True" Then
                                                PasswordTextBox.Font = New Font(Font.FontFamily, PasswordTextBox.Font.Size, FontStyle.Bold)
                                            End If
                                        ElseIf Lines.StartsWith("FontSize=") Then
                                            If PasswordTextBox.Font.Bold = True Then
                                                PasswordTextBox.Font = New Font(Font.FontFamily, CInt(Lines.Replace("FontSize=", "")), FontStyle.Bold)
                                            Else
                                                PasswordTextBox.Font = New Font(Font.FontFamily, CInt(Lines.Replace("FontSize=", "")), FontStyle.Regular)
                                            End If
                                        ElseIf Lines.StartsWith("FontItalic=") Then
                                            If Lines.Replace("FontItalic=", "") = "True" Then
                                                PasswordTextBox.Font = New Font(Font.FontFamily, PasswordTextBox.Font.Size, FontStyle.Italic)
                                            End If
                                        ElseIf Lines.StartsWith("FontTypeface=") Then
                                        ElseIf Lines.StartsWith("TextAlign=") Then
                                            Select Case Lines.Replace("TextAlign=", Nothing)
                                                Case Is = "LEFT_TOP" : PasswordTextBox.TextAlign = HorizontalAlignment.Left
                                                Case Is = "CENTER_TOP" : PasswordTextBox.TextAlign = HorizontalAlignment.Center
                                                Case Is = "RIGHT_TOP" : PasswordTextBox.TextAlign = HorizontalAlignment.Right
                                            End Select
                                        ElseIf Lines.StartsWith("BackColor=") Then : PasswordTextBox.BackColor = ColourFromData(Lines.Replace("BackColor=", Nothing))
                                        ElseIf Lines.StartsWith("ForeColor=") Then : PasswordTextBox.ForeColor = ColourFromData(Lines.Replace("ForeColor=", Nothing))
                                        ElseIf Lines.StartsWith("Location=") Then : PasswordTextBox.Location = New Point(CInt(Lines.Split(";")(0).Replace("Location={", Nothing)), CInt(Lines.Split(";")(1).Replace("}", Nothing)))
                                        ElseIf Lines.StartsWith("Size=") Then : PasswordTextBox.Size = New Size(New Point(CInt(Lines.Split(";")(0).Replace("Size={", Nothing)), CInt(Lines.Split(";")(1).Replace("}", Nothing))))
                                        ElseIf Lines.StartsWith("Hint=") Then
                                        ElseIf Lines.StartsWith("Enabled=") Then
                                        End If
                                    Next

                                    Combobox1.Items.Add(New ComboBoxIconItem(PasswordTextBox.Tag, 7))


                                ElseIf GnControl.StartsWith("APictureBox") Then
                                    PropertiesOfControl &= "</" & GnControl & ">"

                                    For Each lines As String In PropertiesOfControl.Split(vbNewLine)
                                        lines = lines.Replace(Chr(10), "")
                                        If lines = "IsOnPanel=" Then PanelIS = Panel5 : Exit For Else If lines.StartsWith("IsOnPanel=") Then PanelIS = DirectCast(FindControl(lines.Replace("IsOnPanel=", ""), Panel5), Panel) : Exit For ' MsgBox(lines.Replace("IsOnPanel=", ""))
                                    Next

                                    APictureBox = New PictureBox

                                    With APictureBox
                                        .Enabled = True
                                        .Name = GnControl
                                        .Location = New Point(0, 0)
                                        .BorderStyle = BorderStyle.FixedSingle
                                        .Hide()
                                        .BackgroundImageLayout = ImageLayout.Stretch
                                    End With

                                    PanelIS.Controls.Add(APictureBox)
                                    PictureBoxString &= (GnControl & vbNewLine)


                                    If Not File.EndsWith(ToolStripTextBox1.Text.Replace("com.vb4a.", "") & ".vb4a") Then
                                        cntrafe8 += 1
                                    End If

                                    GenControl = APictureBox
                                    rc = New ResizeableControl(APictureBox)

                                    For Each Lines As String In PropertiesOfControl.Split(vbNewLine)
                                        Lines = Lines.Replace(Chr(10), "")

                                        If Lines.StartsWith("Name=") Then : APictureBox.Tag = Lines.Replace("Name=", "")
                                        ElseIf Lines.StartsWith("BackColor=") Then : APictureBox.BackColor = ColourFromData(Lines.Replace("BackColor=", Nothing))
                                        ElseIf Lines.StartsWith("Location=") Then : APictureBox.Location = New Point(CInt(Lines.Split(";")(0).Replace("Location={", Nothing)), CInt(Lines.Split(";")(1).Replace("}", Nothing)))
                                        ElseIf Lines.StartsWith("Size=") Then : APictureBox.Size = New Size(New Point(CInt(Lines.Split(";")(0).Replace("Size={", Nothing)), CInt(Lines.Split(";")(1).Replace("}", Nothing))))
                                        ElseIf Lines.StartsWith("Image=") Then : If Lines.Replace("Image=", Nothing) = Nothing Then : Else : APictureBox.BackgroundImage = Image.FromFile(Lines.Replace("Image=", Nothing)) : End If
                                        End If
                                    Next

                                    Combobox1.Items.Add(New ComboBoxIconItem(APictureBox.Tag, 8))




                                ElseIf GnControl.StartsWith("AProgressBar") Then
                                    PropertiesOfControl &= "</" & GnControl & ">"

                                    For Each lines As String In PropertiesOfControl.Split(vbNewLine)
                                        lines = lines.Replace(Chr(10), "")
                                        If lines = "IsOnPanel=" Then PanelIS = Panel5 : Exit For Else If lines.StartsWith("IsOnPanel=") Then PanelIS = DirectCast(FindControl(lines.Replace("IsOnPanel=", ""), Panel5), Panel) : Exit For ' MsgBox(lines.Replace("IsOnPanel=", ""))
                                    Next

                                    AProgressBar = New ProgressBar

                                    With AProgressBar
                                        .Enabled = True
                                        .Name = GnControl
                                        .Location = New Point(0, 0)
                                        .Maximum = 100
                                        .Hide()
                                    End With

                                    PanelIS.Controls.Add(AProgressBar)
                                    ProgressBarString &= (GnControl & vbNewLine)


                                    If Not File.EndsWith(ToolStripTextBox1.Text.Replace("com.vb4a.", "") & ".vb4a") Then
                                        cntrafe9 += 1
                                    End If

                                    GenControl = AProgressBar
                                    rc = New ResizeableControl(AProgressBar)

                                    For Each Lines As String In PropertiesOfControl.Split(vbNewLine)
                                        Lines = Lines.Replace(Chr(10), "")

                                        If Lines.StartsWith("Name=") Then : AProgressBar.Tag = Lines.Replace("Name=", "")
                                        ElseIf Lines.StartsWith("Location=") Then : AProgressBar.Location = New Point(CInt(Lines.Split(";")(0).Replace("Location={", Nothing)), CInt(Lines.Split(";")(1).Replace("}", Nothing)))
                                        ElseIf Lines.StartsWith("Size=") Then : AProgressBar.Size = New Size(New Point(CInt(Lines.Split(";")(0).Replace("Size={", Nothing)), CInt(Lines.Split(";")(1).Replace("}", Nothing))))
                                        ElseIf Lines.StartsWith("Value=") Then : AProgressBar.Value = CInt(Lines.Replace("Value=", ""))
                                        End If
                                    Next

                                    Combobox1.Items.Add(New ComboBoxIconItem(AProgressBar.Tag, 9))


                                ElseIf GnControl.StartsWith("ARadioButton") Then
                                    PropertiesOfControl &= "</" & GnControl & ">"

                                    For Each lines As String In PropertiesOfControl.Split(vbNewLine)
                                        lines = lines.Replace(Chr(10), "")
                                        If lines = "IsOnPanel=" Then PanelIS = Panel5 : Exit For Else If lines.StartsWith("IsOnPanel=") Then PanelIS = DirectCast(FindControl(lines.Replace("IsOnPanel=", ""), Panel5), Panel) : Exit For ' MsgBox(lines.Replace("IsOnPanel=", ""))
                                    Next

                                    ARadioButton = New RadioButton

                                    With ARadioButton
                                        .Enabled = True
                                        .Name = GnControl
                                        .Location = New Point(0, 0)
                                        .FlatStyle = FlatStyle.Flat
                                        .AutoSize = False
                                        .Hide()
                                    End With

                                    PanelIS.Controls.Add(ARadioButton)
                                    RadioButtonString &= (GnControl & vbNewLine)


                                    If Not File.EndsWith(ToolStripTextBox1.Text.Replace("com.vb4a.", "") & ".vb4a") Then
                                        cntrafe10 += 1
                                    End If

                                    GenControl = ARadioButton
                                    rc = New ResizeableControl(ARadioButton)

                                    For Each Lines As String In PropertiesOfControl.Split(vbNewLine)
                                        Lines = Lines.Replace(Chr(10), "")

                                        If Lines.StartsWith("Name=") Then : ARadioButton.Tag = Lines.Replace("Name=", "")
                                        ElseIf Lines.StartsWith("Text=") Then : ARadioButton.Text = Lines.Replace("Text=", Nothing)
                                        ElseIf Lines.StartsWith("FontBold=") Then
                                            If Lines.Replace("FontBold=", "") = "True" Then
                                                ARadioButton.Font = New Font(Font.FontFamily, ARadioButton.Font.Size, FontStyle.Bold)
                                            End If
                                        ElseIf Lines.StartsWith("FontSize=") Then
                                            If ARadioButton.Font.Bold = True Then
                                                ARadioButton.Font = New Font(Font.FontFamily, CInt(Lines.Replace("FontSize=", "")), FontStyle.Bold)
                                            Else
                                                ARadioButton.Font = New Font(Font.FontFamily, CInt(Lines.Replace("FontSize=", "")), FontStyle.Regular)
                                            End If
                                        ElseIf Lines.StartsWith("FontItalic=") Then
                                            If Lines.Replace("FontItalic=", "") = "True" Then
                                                ARadioButton.Font = New Font(Font.FontFamily, ARadioButton.Font.Size, FontStyle.Italic)
                                            End If
                                        ElseIf Lines.StartsWith("FontTypeface=") Then
                                        ElseIf Lines.StartsWith("TextAlign=") Then
                                            Select Case Lines.Replace("TextAlign=", Nothing)
                                                Case Is = "LEFT" : ARadioButton.TextAlign = ContentAlignment.MiddleLeft
                                                Case Is = "CENTER" : ARadioButton.TextAlign = ContentAlignment.MiddleCenter
                                                Case Is = "RIGHT" : ARadioButton.TextAlign = ContentAlignment.MiddleRight
                                                Case Is = "LEFT_TOP" : ARadioButton.TextAlign = ContentAlignment.TopLeft
                                                Case Is = "LEFT_CENTER" : ARadioButton.TextAlign = ContentAlignment.MiddleLeft
                                                Case Is = "LEFT_BOTTOM" : ARadioButton.TextAlign = ContentAlignment.BottomLeft
                                                Case Is = "CENTER_TOP" : ARadioButton.TextAlign = ContentAlignment.TopCenter
                                                Case Is = "CENTER_CENTER" : ARadioButton.TextAlign = ContentAlignment.MiddleCenter
                                                Case Is = "CENTER_BOTTOM" : ARadioButton.TextAlign = ContentAlignment.BottomCenter
                                                Case Is = "RIGHT_TOP" : ARadioButton.TextAlign = ContentAlignment.TopRight
                                                Case Is = "RIGHT_CENTER" : ARadioButton.TextAlign = ContentAlignment.MiddleRight
                                                Case Is = "RIGHT_BOTTOM" : ARadioButton.TextAlign = ContentAlignment.BottomRight
                                            End Select
                                        ElseIf Lines.StartsWith("BackColor=") Then : ARadioButton.BackColor = ColourFromData(Lines.Replace("BackColor=", Nothing))
                                        ElseIf Lines.StartsWith("ForeColor=") Then : ARadioButton.ForeColor = ColourFromData(Lines.Replace("ForeColor=", Nothing))
                                        ElseIf Lines.StartsWith("Location=") Then : ARadioButton.Location = New Point(CInt(Lines.Split(";")(0).Replace("Location={", Nothing)), CInt(Lines.Split(";")(1).Replace("}", Nothing)))
                                        ElseIf Lines.StartsWith("Size=") Then : ARadioButton.Size = New Size(New Point(CInt(Lines.Split(";")(0).Replace("Size={", Nothing)), CInt(Lines.Split(";")(1).Replace("}", Nothing))))
                                        ElseIf Lines.StartsWith("Checked=") Then : ARadioButton.Checked = Convert.ToBoolean(Lines.Replace("Checked=", ""))
                                        ElseIf Lines.StartsWith("Enabled=") Then
                                        End If
                                    Next

                                    Combobox1.Items.Add(New ComboBoxIconItem(ARadioButton.Tag, 10))


                                ElseIf GnControl.StartsWith("ATextBox") Then
                                    PropertiesOfControl &= "</" & GnControl & ">"


                                    For Each lines As String In PropertiesOfControl.Split(vbNewLine)
                                        lines = lines.Replace(Chr(10), "")
                                        If lines = "IsOnPanel=" Then PanelIS = Panel5 : Exit For Else If lines.StartsWith("IsOnPanel=") Then PanelIS = DirectCast(FindControl(lines.Replace("IsOnPanel=", ""), Panel5), Panel) : Exit For ' MsgBox(lines.Replace("IsOnPanel=", ""))
                                    Next

                                    ATextBox = New TextBox

                                    With ATextBox
                                        .Enabled = True
                                        .BorderStyle = BorderStyle.None
                                        .Name = GnControl
                                        .Location = New Point(0, 0)
                                        .Cursor = Cursors.NoMove2D
                                        .Multiline = True
                                        .ReadOnly = True
                                        .Hide()
                                    End With

                                    PanelIS.Controls.Add(ATextBox)
                                    TextBoxString &= (GnControl & vbNewLine)


                                    If Not File.EndsWith(ToolStripTextBox1.Text.Replace("com.vb4a.", "") & ".vb4a") Then
                                        cntrafe11 += 1
                                    End If

                                    GenControl = ATextBox
                                    rc = New ResizeableControl(ATextBox)

                                    For Each Lines As String In PropertiesOfControl.Split(vbNewLine)
                                        Lines = Lines.Replace(Chr(10), "")

                                        If Lines.StartsWith("Name=") Then : ATextBox.Tag = Lines.Replace("Name=", "")
                                        ElseIf Lines.StartsWith("Text=") Then : ATextBox.Text = Lines.Replace("Text=", Nothing)
                                        ElseIf Lines.StartsWith("FontBold=") Then
                                            If Lines.Replace("FontBold=", "") = "True" Then
                                                ATextBox.Font = New Font(Font.FontFamily, ATextBox.Font.Size, FontStyle.Bold)
                                            End If
                                        ElseIf Lines.StartsWith("FontSize=") Then
                                            If ATextBox.Font.Bold = True Then
                                                ATextBox.Font = New Font(Font.FontFamily, CInt(Lines.Replace("FontSize=", "")), FontStyle.Bold)
                                            Else
                                                ATextBox.Font = New Font(Font.FontFamily, CInt(Lines.Replace("FontSize=", "")), FontStyle.Regular)
                                            End If
                                        ElseIf Lines.StartsWith("FontItalic=") Then
                                            If Lines.Replace("FontItalic=", "") = "True" Then
                                                ATextBox.Font = New Font(Font.FontFamily, ATextBox.Font.Size, FontStyle.Italic)
                                            End If
                                        ElseIf Lines.StartsWith("FontTypeface=") Then
                                        ElseIf Lines.StartsWith("TextAlign=") Then
                                            Select Case Lines.Replace("TextAlign=", Nothing)
                                                Case Is = "LEFT_TOP" : PasswordTextBox.TextAlign = HorizontalAlignment.Left
                                                Case Is = "CENTER_TOP" : PasswordTextBox.TextAlign = HorizontalAlignment.Center
                                                Case Is = "RIGHT_TOP" : PasswordTextBox.TextAlign = HorizontalAlignment.Right
                                            End Select
                                        ElseIf Lines.StartsWith("BackColor=") Then : ATextBox.BackColor = ColourFromData(Lines.Replace("BackColor=", Nothing))
                                        ElseIf Lines.StartsWith("ForeColor=") Then : ATextBox.ForeColor = ColourFromData(Lines.Replace("ForeColor=", Nothing))
                                        ElseIf Lines.StartsWith("Location=") Then : ATextBox.Location = New Point(CInt(Lines.Split(";")(0).Replace("Location={", Nothing)), CInt(Lines.Split(";")(1).Replace("}", Nothing)))
                                        ElseIf Lines.StartsWith("Size=") Then : ATextBox.Size = New Size(New Point(CInt(Lines.Split(";")(0).Replace("Size={", Nothing)), CInt(Lines.Split(";")(1).Replace("}", Nothing))))
                                        ElseIf Lines.StartsWith("Hint=") Then
                                        ElseIf Lines.StartsWith("Enabled=") Then
                                        ElseIf Lines.StartsWith("InputType=") Then
                                        End If
                                    Next

                                    Combobox1.Items.Add(New ComboBoxIconItem(ATextBox.Tag, 12))



                                ElseIf GnControl.StartsWith("ATimer") Then
                                    PropertiesOfControl &= "</" & GnControl & ">"

                                    For Each lines As String In PropertiesOfControl.Split(vbNewLine)
                                        lines = lines.Replace(Chr(10), "")
                                        If lines = "IsOnPanel=" Then PanelIS = Panel5 : Exit For Else If lines.StartsWith("IsOnPanel=") Then PanelIS = DirectCast(FindControl(lines.Replace("IsOnPanel=", ""), Panel5), Panel) : Exit For ' MsgBox(lines.Replace("IsOnPanel=", ""))
                                    Next

                                    ATimer = New PictureBox

                                    AddHandler ATimer.MouseDown, AddressOf Me.ATimer_MouseDown
                                    AddHandler ATimer.MouseUp, AddressOf Me.ATimer_MouseUp
                                    AddHandler ATimer.MouseMove, AddressOf Me.ATimer_MouseMove

                                    With ATimer
                                        .Enabled = True
                                        .Name = GnControl
                                        .BackColor = Color.White
                                        .Size = New Size(20, 20)
                                        .Location = New Point(0, 0)
                                        .BorderStyle = BorderStyle.FixedSingle
                                        .SizeMode = PictureBoxSizeMode.CenterImage
                                        .Image = My.Resources.Timer
                                        .Hide()
                                    End With

                                    PanelIS.Controls.Add(ATimer)
                                    TimerString &= (GnControl & vbNewLine)


                                    If Not File.EndsWith(ToolStripTextBox1.Text.Replace("com.vb4a.", "") & ".vb4a") Then
                                        cntrafe12 += 1
                                    End If

                                    GenControl = ATimer

                                    For Each Lines As String In PropertiesOfControl.Split(vbNewLine)
                                        Lines = Lines.Replace(Chr(10), "")

                                        If Lines.StartsWith("Name=") Then : ATimer.Tag = Lines.Replace("Name=", "")
                                        ElseIf Lines.StartsWith("Location=") Then : ATimer.Location = New Point(CInt(Lines.Split(";")(0).Replace("Location={", Nothing)), CInt(Lines.Split(";")(1).Replace("}", Nothing)))
                                        End If
                                    Next

                                    Combobox1.Items.Add(New ComboBoxIconItem(ATimer.Tag, 13))


                                ElseIf GnControl.StartsWith("AComboBox") Then ' check
                                    PropertiesOfControl &= "</" & GnControl & ">"

                                    For Each lines As String In PropertiesOfControl.Split(vbNewLine)
                                        lines = lines.Replace(Chr(10), "")
                                        If lines = "IsOnPanel=" Then PanelIS = Panel5 : Exit For Else If lines.StartsWith("IsOnPanel=") Then PanelIS = DirectCast(FindControl(lines.Replace("IsOnPanel=", ""), Panel5), Panel) : Exit For ' MsgBox(lines.Replace("IsOnPanel=", ""))
                                    Next

                                    AComboBox = New ComboBox

                                    With AComboBox
                                        .Enabled = True
                                        .Name = GnControl
                                        .Location = New Point(0, 0)
                                        .FlatStyle = FlatStyle.Flat
                                        .DropDownStyle = ComboBoxStyle.DropDown
                                        .Hide()
                                    End With

                                    PanelIS.Controls.Add(AComboBox)
                                    ComboBoxString &= (GnControl & vbNewLine)


                                    If Not File.EndsWith(ToolStripTextBox1.Text.Replace("com.vb4a.", "") & ".vb4a") Then
                                        cntrafe13 += 1
                                    End If

                                    GenControl = AComboBox
                                    rc = New ResizeableControl(AComboBox)

                                    For Each Lines As String In PropertiesOfControl.Split(vbNewLine)
                                        Lines = Lines.Replace(Chr(10), "")

                                        If Lines.StartsWith("Name=") Then : AComboBox.Tag = Lines.Replace("Name=", "")
                                        ElseIf Lines.StartsWith("Text=") Then : AComboBox.Text = Lines.Replace("Text=", Nothing)
                                        ElseIf Lines.StartsWith("FontBold=") Then
                                            If Lines.Replace("FontBold=", "") = "True" Then
                                                AComboBox.Font = New Font(Font.FontFamily, AComboBox.Font.Size, FontStyle.Bold)
                                            End If
                                        ElseIf Lines.StartsWith("FontSize=") Then
                                            If AComboBox.Font.Bold = True Then
                                                AComboBox.Font = New Font(Font.FontFamily, CInt(Lines.Replace("FontSize=", "")), FontStyle.Bold)
                                            Else
                                                AComboBox.Font = New Font(Font.FontFamily, CInt(Lines.Replace("FontSize=", "")), FontStyle.Regular)
                                            End If
                                        ElseIf Lines.StartsWith("FontItalic=") Then
                                            If Lines.Replace("FontItalic=", "") = "True" Then
                                                AComboBox.Font = New Font(Font.FontFamily, AComboBox.Font.Size, FontStyle.Italic)
                                            End If
                                        ElseIf Lines.StartsWith("FontTypeface=") Then
                                        ElseIf Lines.StartsWith("TextAlign=") Then
                                        ElseIf Lines.StartsWith("BackColor=") Then : AComboBox.BackColor = ColourFromData(Lines.Replace("BackColor=", Nothing))
                                        ElseIf Lines.StartsWith("ForeColor=") Then : AComboBox.ForeColor = ColourFromData(Lines.Replace("ForeColor=", Nothing))
                                        ElseIf Lines.StartsWith("Location=") Then : AComboBox.Location = New Point(CInt(Lines.Split(";")(0).Replace("Location={", Nothing)), CInt(Lines.Split(";")(1).Replace("}", Nothing)))
                                        ElseIf Lines.StartsWith("Size=") Then : AComboBox.Size = New Size(New Point(CInt(Lines.Split(";")(0).Replace("Size={", Nothing)), CInt(Lines.Split(";")(1).Replace("}", Nothing))))
                                        ElseIf Lines.StartsWith("Enabled=") Then
                                        End If
                                    Next

                                    Combobox1.Items.Add(New ComboBoxIconItem(AComboBox.Tag, 14))




                                ElseIf GnControl.StartsWith("VB4AWeb") Then
                                    PropertiesOfControl &= "</" & GnControl & ">"

                                    For Each lines As String In PropertiesOfControl.Split(vbNewLine)
                                        lines = lines.Replace(Chr(10), "")
                                        If lines = "IsOnPanel=" Then PanelIS = Panel5 : Exit For Else If lines.StartsWith("IsOnPanel=") Then PanelIS = DirectCast(FindControl(lines.Replace("IsOnPanel=", ""), Panel5), Panel) : Exit For ' MsgBox(lines.Replace("IsOnPanel=", ""))
                                    Next

                                    VB4AWeb = New PictureBox


                                    With VB4AWeb
                                        .Enabled = True
                                        .Name = GnControl
                                        .BackColor = Color.White
                                        .Location = New Point(0, 0)
                                        .BorderStyle = BorderStyle.FixedSingle
                                        .SizeMode = PictureBoxSizeMode.CenterImage
                                        .Image = My.Resources.browser16__1_
                                        .Hide()
                                    End With

                                    PanelIS.Controls.Add(VB4AWeb)
                                    VB4AWebString &= (GnControl & vbNewLine)


                                    If Not File.EndsWith(ToolStripTextBox1.Text.Replace("com.vb4a.", "") & ".vb4a") Then
                                        cntrafe14 += 1
                                    End If

                                    GenControl = VB4AWeb
                                    rc = New ResizeableControl(VB4AWeb)

                                    For Each Lines As String In PropertiesOfControl.Split(vbNewLine)
                                        Lines = Lines.Replace(Chr(10), "")

                                        If Lines.StartsWith("Name=") Then : VB4AWeb.Tag = Lines.Replace("Name=", "")
                                        ElseIf Lines.StartsWith("Location=") Then : VB4AWeb.Location = New Point(CInt(Lines.Split(";")(0).Replace("Location={", Nothing)), CInt(Lines.Split(";")(1).Replace("}", Nothing)))
                                        ElseIf Lines.StartsWith("Size=") Then : VB4AWeb.Size = New Size(New Point(CInt(Lines.Split(";")(0).Replace("Size={", Nothing)), CInt(Lines.Split(";")(1).Replace("}", Nothing))))

                                        End If
                                    Next

                                    Combobox1.Items.Add(New ComboBoxIconItem(VB4AWeb.Tag, 15))


                                End If

                                If Not GenControl Is Nothing Then GenControl.BringToFront()

                            End If



                        Next
                        'MsgBox(ProjectSaveString)
                        ' If Not FormsNumber = 1 Then
                        '  MsgBox(My.Computer.FileSystem.ReadAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & File.Replace(OpenFileDialog1.FileName.Replace(OpenFileDialog1.SafeFileName, ""), "").Replace(".vb4a", ".DelConts")))
                        'delopen project για τεσταρισμα
                        For Each Line As String In My.Computer.FileSystem.ReadAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & File.Replace(OpenFileDialog1.FileName.Replace(OpenFileDialog1.SafeFileName, ""), "").Replace(".vb4a", ".DelConts")).Split(vbNewLine)
                            '  If Not File.ToLower.EndsWith(Label4.Text.Replace("- ( ", "").Replace(" )", ".vb4a")) Then ' because label4.text first letter isnt uppercase 

                            Line = Line.Replace(Chr(10), "")

                            ' If Line.StartsWith("A") Then idControl += 1 : ButtonString &= Line & vbNewLine : If Not FormsNumber = 1 Then cntrafe1 += 1 ' : Console.WriteLine(1)
                            If Line.StartsWith("AButton") Then : idControl += 1 : ButtonString &= Line & vbNewLine : If Not File.ToLower.EndsWith(Label4.Text.Replace("- ( ", "").Replace(" )", ".vb4a")) Then cntrafe1 += 1
                            ElseIf Line.StartsWith("Canvas") Then : idControl += 1 : CanvasString &= Line & vbNewLine : If Not File.ToLower.EndsWith(Label4.Text.Replace("- ( ", "").Replace(" )", ".vb4a")) Then cntrafe2 += 1
                            ElseIf Line.StartsWith("ACheckBox") Then : idControl += 1 : CheckBoxString &= Line & vbNewLine : If Not File.ToLower.EndsWith(Label4.Text.Replace("- ( ", "").Replace(" )", ".vb4a")) Then cntrafe3 += 1
                            ElseIf Line.StartsWith("ALabel") Then : idControl += 1 : LabelString &= Line & vbNewLine : If Not File.ToLower.EndsWith(Label4.Text.Replace("- ( ", "").Replace(" )", ".vb4a")) Then cntrafe4 += 1
                            ElseIf Line.StartsWith("AListview") Then : idControl += 1 : ListBoxString &= Line & vbNewLine : If Not File.ToLower.EndsWith(Label4.Text.Replace("- ( ", "").Replace(" )", ".vb4a")) Then cntrafe5 += 1
                            ElseIf Line.StartsWith("APanel") Then : idControl += 1 : PanelString &= Line & vbNewLine : If Not File.ToLower.EndsWith(Label4.Text.Replace("- ( ", "").Replace(" )", ".vb4a")) Then cntrafe6 += 1
                            ElseIf Line.StartsWith("PasswordTextBox") Then : idControl += 1 : PasswordTextBoxString &= Line & vbNewLine : If Not File.ToLower.EndsWith(Label4.Text.Replace("- ( ", "").Replace(" )", ".vb4a")) Then cntrafe7 += 1
                            ElseIf Line.StartsWith("APictureBox") Then : idControl += 1 : PictureBoxString &= Line & vbNewLine : If Not File.ToLower.EndsWith(Label4.Text.Replace("- ( ", "").Replace(" )", ".vb4a")) Then cntrafe8 += 1
                            ElseIf Line.StartsWith("AProgressBar") Then : idControl += 1 : ProgressBarString &= Line & vbNewLine : If Not File.ToLower.EndsWith(Label4.Text.Replace("- ( ", "").Replace(" )", ".vb4a")) Then cntrafe9 += 1
                            ElseIf Line.StartsWith("ARadioButton") Then : idControl += 1 : RadioButtonString &= Line & vbNewLine : If Not File.ToLower.EndsWith(Label4.Text.Replace("- ( ", "").Replace(" )", ".vb4a")) Then cntrafe10 += 1
                            ElseIf Line.StartsWith("ATextBox") Then : idControl += 1 : TextBoxString &= Line & vbNewLine : If Not File.ToLower.EndsWith(Label4.Text.Replace("- ( ", "").Replace(" )", ".vb4a")) Then cntrafe11 += 1
                            ElseIf Line.StartsWith("ATimer") Then : idControl += 1 : TimerString &= Line & vbNewLine : If Not File.ToLower.EndsWith(Label4.Text.Replace("- ( ", "").Replace(" )", ".vb4a")) Then cntrafe12 += 1
                            ElseIf Line.StartsWith("AComboBox") Then : idControl += 1 : ComboBoxString &= Line & vbNewLine : If Not File.ToLower.EndsWith(Label4.Text.Replace("- ( ", "").Replace(" )", ".vb4a")) Then cntrafe13 += 1
                            ElseIf Line.StartsWith("VB4AWeb") Then : idControl += 1 : VB4AWebString &= Line & vbNewLine : If Not File.ToLower.EndsWith(Label4.Text.Replace("- ( ", "").Replace(" )", ".vb4a")) Then cntrafe14 += 1
                            End If

                            ' End If

                        Next
                        '  End If



                        ' MsgBox(ButtonString)
                        FormsNumber -= 1
                        If FormsNumber = 1 AndAlso Not File.Replace(OpenFileDialog1.FileName.Replace(OpenFileDialog1.SafeFileName, ""), "") = ToolStripTextBox1.Text.Replace("com.vb4a.", "") & ".vb4a" Then File = CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & ToolStripTextBox1.Text.Replace("com.vb4a.", "") & ".vb4a" : GoTo hrfor2



                        ' MsgBox(File.Replace(OpenFileDialog1.FileName.Replace(OpenFileDialog1.SafeFileName, ""), ""))

                        '  Combobox3.Text = ""
                        'teleuteoformName = File.Replace(OpenFileDialog1.FileName.Replace(OpenFileDialog1.SafeFileName, ""), "")
                        'Combobox3.SelectedItem = Combobox3.Items.Item(Combobox3.Items.Count - 1)
                        '  Combobox3.SelectedItem = Nothing
                        ' Combobox3.SelectedItem = Combobox3.Items.Item(Combobox3.Items.Count - 1)
                    End If

                    ' MsgBox(ProjectSaveString)
hrfor:          Next

                ' cntrafe1 -= 1
                Combobox3.SelectedItem = Combobox3.Items.Item(Combobox3.Items.Count - 1)
                Combobox3.SelectedItem = Nothing
                Combobox3.SelectedItem = Combobox3.Items.Item(Combobox3.Items.Count - 1)
                Textbox3.ClearUndo() ' i know i could use it when ide reads each file for better but .... anyway !! :P 

                BuildAutocompleteMenu()
                ' For i = 0 To Combobox3.Items.Count - 1
                'MsgBox(Combobox3.Items.Item(i).ToString)
                '  Next

                'ToolStripTextBox1.Enabled = True 
                ' ToolStripComboBox1.Enabled = True
                ' teleuteoformName = Nothing
                If has_form_file = False Then
                    Using New Centered_MessageBox(Me)
                        '    MsgBox("If you want your app to work succesfully " & vbNewLine & "Create A Form named 'form1' [because form1 is the main form]", vbInformation) ' it was when form1 was the main form
                    End Using

                ElseIf Textbox3.Text = "" Then : Textbox3.Text = My.Computer.FileSystem.ReadAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & ToolStripTextBox1.Text.Replace("com.vb4a.", "") & ".vb4acode", Encoding.GetEncoding("gb2312")) ' i dont care if the user really has "" the file because i need to be sure that has read the file (i had a prob before and i dont know if it was just an illusion of my brain or real).... i know i could also check the file if it is really "" from user but i am not gonna use cpu for... :P 
                End If

                ChangeToolStripMenuItem.Enabled = True
                Panel5.Enabled = True
            End If


            'Catch Ex As Exception
            '    TextBox1.Text = "{My.Resources.Icon}"
            '    Using New Centered_MessageBox(Me)
            '        MessageBox.Show("Cannot read file from disk. Original error: " & Ex.Message)
            '    End Using
            'End Try
        End If
    End Sub

    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        If Label4.Text = Nothing Then
            load_project()


        Else
            ' MsgBox("I am working on it LoL i just have to clear all..... :P For now just restart the exe ")

            '  ProjectNewUserSel = True
            ' Form2NewProject.fs.Close()


            ' If ProjectNewUserSel = False Then GoTo endof
            Panel5.Hide()
            If Combobox3.Items.Count = 1 Then
                SaveToolStripMenuItem1.PerformClick()
            Else
                SaveToolStripMenuItem.PerformClick()
            End If
            Panel5.Show()

            clear_For_New_Project()
            Button10.PerformClick()

            load_project()

        End If

    End Sub


    Dim plinCodBut As Integer = 0
    Private Sub ToolStripDropDownButton2_DropDownItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles ToolStripDropDownButton2.DropDownItemClicked
        'MsgBox(e.ClickedItem.Text)
        ' Set_Language(e.ClickedItem.Text & ".Lang")
        If Not e.ClickedItem Is ToolStripDropDownButton2.DropDownItems.Item(0) And Not e.ClickedItem Is ToolStripDropDownButton2.DropDownItems.Item(1) Then

            If File.Exists(CurrentHardDriver & "VB4Android\Languages\" & e.ClickedItem.Text & ".Lang") = True Then ' AndAlso File.Exists(CurrentHardDriver & "VB4Android\Languages\" & e.ClickedItem.Text & ".LangMeth") = True

                Dim a As Integer = 0
                Dim b As Integer = 1

                plinCodBut = 0

                For Each Line As String In My.Computer.FileSystem.ReadAllText(CurrentHardDriver & "VB4Android\Languages\" & e.ClickedItem.Text & ".Lang").Split(vbNewLine)

                    Line = Line.Replace(vbLf, Nothing)

                    Select Case a
                        Case Is = 0 '       --Lablels
                            For Each item As String In Line.Split("|")
                                Select Case b
                                    Case Is = 1 : Label1.Text = item
                                    Case Is = 2 : Label2.Text = item
                                    Case Is = 3 : Label3.Text = item
                                    Case Is = 4 : Label6.Text = item
                                    Case Is = 5 : Label9.Text = item
                                End Select : b += 1
                            Next
                        Case Is = 1 : b = 1 '--CheckBoxes
                            For Each item As String In Line.Split("|")
                                Select Case b
                                    Case Is = 1 : CheckBox1.Text = item
                                    Case Is = 2 : CheckBox2.Text = item
                                    Case Is = 3 : CheckBox3.Text = item
                                    Case Is = 4 : CheckBox4.Text = item
                                    Case Is = 5 : CheckBox5.Text = item
                                    Case Is = 6 : CheckBox6.Text = item
                                    Case Is = 7 : CheckBox7.Text = item
                                    Case Is = 8 : CheckBox8.Text = item
                                End Select : b += 1
                            Next
                        Case Is = 2 : b = 1 '--Buttons
                            For Each item As String In Line.Split("|")
                                Select Case b
                                    Case Is = 1 : Button7.Text = item
                                    Case Is = 2 : Button8.Text = item
                                    Case Is = 3 : Button9.Text = item
                                        'Case Is = 4 : Button12.Text = item
                                End Select : b += 1
                            Next
                        Case Is = 3 : b = 1 '--ToolStripButtons
                            For Each item As String In Line.Split("|")
                                Select Case b
                                    Case Is = 1 : ToolStripButton1.Text = item
                                    Case Is = 2 : ToolStripButton2.Text = item
                                    Case Is = 3 : ToolStripButton3.Text = item
                                    Case Is = 4 : ToolStripButton4.Text = item
                                    Case Is = 5 : ToolStripButton5.Text = item
                                    Case Is = 6 : ToolStripButton6.Text = item
                                    Case Is = 7 : plinCodBut = CInt(item)
                                End Select : b += 1
                            Next
                        Case Is = 4 : b = 1 '--ToolStripDropDownButtons
                            For Each item As String In Line.Split("|")
                                Select Case b
                                    Case Is = 1 : ToolStripDropDownButton1.Text = item
                                    Case Is = 2 : ToolStripDropDownButton2.Text = item
                                    Case Is = 3 : ToolStripDropDownButton3.Text = item
                                    Case Is = 4 : ToolStripDropDownButton4.Text = item
                                    Case Is = 5 : ToolStripDropDownButton5.Text = item
                                    Case Is = 6 : ToolStripDropDownButton6.Text = item
                                End Select : b += 1
                            Next
                        Case Is = 5 : b = 1 '--XXXToolStripMenuItems Project
                            For Each item As String In Line.Split("|")
                                Select Case b
                                    Case Is = 1 : NewToolStripMenuItem.Text = item
                                    Case Is = 2 : SaveToolStripMenuItem1.Text = item ' save 
                                    Case Is = 3 : SaveToolStripMenuItem.Text = item ' save all
                                    Case Is = 4 : OpenToolStripMenuItem.Text = item
                                    Case Is = 5 : CloseToolStripMenuItem.Text = item
                                    Case Is = 6 : RecoveryToolStripMenuItem.Text = item ' View Folder
                                    Case Is = 7 : LoadExampleCntrEToolStripMenuItem.Text = item ' Assets
                                    Case Is = 8 : ExitToolStripMenuItem.Text = item
                                End Select : b += 1
                            Next
                        Case Is = 6 : b = 1 '--XXXToolStripMenuItems Compile
                            For Each item As String In Line.Split("|")
                                Select Case b
                                    Case Is = 1 : CompileToolStripMenuItem.Text = item
                                    Case Is = 2 : RunToolStripMenuItem.Text = item
                                    Case Is = 3 : ShowApkToolStripMenuItem.Text = item
                                End Select : b += 1
                            Next
                        Case Is = 7 : b = 1 '--XXXToolStripMenuItems Add
                            For Each item As String In Line.Split("|")
                                Select Case b
                                    Case Is = 1 : NewFormToolStripMenuItem.Text = item : Form3CreateNewForm.Label1.Text = item
                                    Case Is = 2 : FormFromFileToolStripMenuItem.Text = item
                                    Case Is = 3 : ModuleToolStripMenuItem.Text = item
                                End Select : b += 1
                            Next
                        Case Is = 8 : b = 1 '--XXXToolStripMenuItems Add > Module
                            For Each item As String In Line.Split("|")
                                Select Case b
                                    Case Is = 1 : AddToolStripMenuItem.Text = item
                                    Case Is = 2 : RemoveToolStripMenuItem.Text = item
                                    Case Is = 3 : ToolStripMenuItem2.Text = item
                                End Select : b += 1
                            Next
                        Case Is = 9 : b = 1 '--XXXToolStripMenuItems Language > Create Language
                            For Each item As String In Line.Split("|")
                                Select Case b
                                    Case Is = 1 : ToolStripMenuItem1.Text = item
                                End Select : b += 1
                            Next
                        Case Is = 10 : b = 1 '--XXXToolStripMenuItems Settings
                            For Each item As String In Line.Split("|")
                                Select Case b
                                    Case Is = 1 : IDESettingsToolStripMenuItem.Text = item
                                    Case Is = 2 : AppsPropertiesToolStripMenuItem.Text = item
                                End Select : b += 1
                            Next
                        Case Is = 11 : b = 1 '--XXXToolStripMenuItems Settings > [More] Compile Setup
                            For Each item As String In Line.Split("|")
                                Select Case b
                                    Case Is = 1 : PackagenameToolStripMenuItem.Text = item
                                    Case Is = 2 : ToolStripTextBox1.ToolTipText = item
                                End Select : b += 1
                            Next
                        Case Is = 12 : b = 0 '--ListView1 Lol by accident i start at cases from zero and i was boring to fix this and i change b xD.
                            For Each item As String In Line.Split("|")
                                Select Case b
                                    Case Is = 0 : ListView1.Items.Item(0).Text = Regex.Replace("   " & item, "\d", "")  'Button"
                                    Case Is = 1 : ListView1.Items.Item(1).Text = Regex.Replace("   " & item, "\d", "")  'Canvas"
                                    Case Is = 2 : ListView1.Items.Item(2).Text = Regex.Replace("   " & item, "\d", "")  'CheckBox"
                                    Case Is = 3 : ListView1.Items.Item(3).Text = Regex.Replace("   " & item, "\d", "") 'EmailPicker"
                                    Case Is = 4 : ListView1.Items.Item(4).Text = Regex.Replace("   " & item, "\d", "") 'Label"
                                    Case Is = 5 : ListView1.Items.Item(5).Text = Regex.Replace("   " & item, "\d", "")  'ListView"
                                    Case Is = 6 : ListView1.Items.Item(6).Text = Regex.Replace("   " & item, "\d", "")   'Panel"
                                    Case Is = 7 : ListView1.Items.Item(7).Text = Regex.Replace("   " & item, "\d", "")  'PasswordTextBox"
                                    Case Is = 8 : ListView1.Items.Item(8).Text = Regex.Replace("   " & item, "\d", "")   'PictureBox" ' Image in specs
                                    Case Is = 9 : ListView1.Items.Item(9).Text = Regex.Replace("   " & item, "\d", "")  'ProgressBar"
                                    Case Is = 10 : ListView1.Items.Item(10).Text = Regex.Replace("   " & item, "\d", "")  'RadioButton"
                                    Case Is = 11 : ListView1.Items.Item(11).Text = Regex.Replace("   " & item, "\d", "") 'TreeView"
                                    Case Is = 12 : ListView1.Items.Item(12).Text = Regex.Replace("   " & item, "\d", "")  'TextBox"
                                    Case Is = 13 : ListView1.Items.Item(13).Text = Regex.Replace("   " & item, "\d", "") 'Timer"
                                    Case Is = 14 : ListView1.Items.Item(14).Text = Regex.Replace("   " & item, "\d", "") 'ComboBox"
                                    Case Is = 15 : ListView1.Items.Item(15).Text = Regex.Replace("   " & item, "\d", "") 'VB4AWeb"
                                End Select : b += 1
                            Next
                        Case Is = 13 : b = 1 '--Form2NewProject , Form3CreateNewForm
                            For Each item As String In Line.Split("|")
                                Select Case b
                                    Case Is = 1 : Form2NewProject.Label1.Text = item
                                    Case Is = 2 : Form2NewProject.Label2.Text = item
                                    Case Is = 3 : Form2NewProject.Button1.Text = item : Form3CreateNewForm.Button1.Text = item
                                    Case Is = 4 : Form3CreateNewForm.Label2.Text = item
                                End Select : b += 1
                            Next
                            'Case Is = 14 : b = 1 '--XXXToolStripMenuItems
                            '    For Each item As String In Line.Split("|")
                            '        Select Case b
                            '            Case Is = 1
                            '        End Select : b += 1
                            '    Next

                    End Select : a += 1

                Next

                'btnMethTLst.Clear() ' TOOO MANY LIST :PP !!!!
                'CnvsMethTlst.Clear()
                'ChckBxMethTlst.Clear()
                'LblMethTlst.Clear()
                'LstVwMethTlst.Clear()
                'pnlMethTlst.Clear()
                'pswtxtbxMethTlst.Clear()
                'pctrbxMethTlst.Clear()
                'progMethTlst.Clear()
                'rdiobtnMethTlst.Clear()
                'txtbxMethTlst.Clear()
                'tmrMethTlst.Clear()
                'cmbbxMethTlst.Clear()
                'vb4wbMethTlst.Clear()
                'ApplicationMethTlst.Clear()
                'ArraysMethTlst.Clear()
                'VB4AInputMethTlst.Clear()
                'FilesMethTlst.Clear()
                'MathMethTlst.Clear()
                'AcceleromaterSenorMethTlst.Clear()
                'LocationSensorMethTlst.Clear()
                'OrientationSensorMethTlst.Clear()
                'PhoneMethTlst.Clear()
                'ComponentMethTlst.Clear()
                'DatesMethTlst.Clear()
                'StringsMethTlst.Clear()
                'ConversionsMethTlst.Clear()
                'Base64MethTlst.Clear()
                'StatisticsMethTlst.Clear()
                'MatrixMethTlst.Clear()

                'Dim blhlp As Boolean = False
                'Dim str As String = String.Empty
                'For Each Line As String In My.Computer.FileSystem.ReadAllText(CurrentHardDriver & "VB4Android\Languages\" & e.ClickedItem.Text & ".LangMeth").Split(vbNewLine)
                '    Line = Line.Replace(vbLf, "").Replace("|@&|NewLN|&@|", vbNewLine)

                '    If Line.StartsWith("</") Then blhlp = False

                '    If blhlp = True Then
                '        Select Case str
                '            Case Is = "Button" : btnMethTLst.Add(Line)
                '            Case Is = "Canvas" : CnvsMethTlst.Add(Line)
                '            Case Is = "CheckBox" : ChckBxMethTlst.Add(Line)
                '            Case Is = "Label" : LblMethTlst.Add(Line)
                '            Case Is = "Listview" : LstVwMethTlst.Add(Line)
                '            Case Is = "Panel" : pnlMethTlst.Add(Line)
                '            Case Is = "PasswordTextBox" : pswtxtbxMethTlst.Add(Line)
                '            Case Is = "PictureBox" : pctrbxMethTlst.Add(Line)
                '            Case Is = "ProgressBar" : progMethTlst.Add(Line)
                '            Case Is = "RadioButton" : rdiobtnMethTlst.Add(Line)
                '            Case Is = "TextBox" : txtbxMethTlst.Add(Line)
                '            Case Is = "Timer" : tmrMethTlst.Add(Line)
                '            Case Is = "ComboBox" : cmbbxMethTlst.Add(Line)
                '            Case Is = "VB4AWeb" : vb4wbMethTlst.Add(Line)
                '            Case Is = "Application" : ApplicationMethTlst.Add(Line)
                '            Case Is = "Arrays" : ArraysMethTlst.Add(Line)
                '            Case Is = "VB4AInput" : VB4AInputMethTlst.Add(Line)
                '            Case Is = "Files" : FilesMethTlst.Add(Line)
                '            Case Is = "Math" : MathMethTlst.Add(Line)
                '            Case Is = "AcceleromaterSenor" : AcceleromaterSenorMethTlst.Add(Line)
                '            Case Is = "LocationSensor" : LocationSensorMethTlst.Add(Line)
                '            Case Is = "OrientationSensor" : OrientationSensorMethTlst.Add(Line)
                '            Case Is = "Phone" : PhoneMethTlst.Add(Line)
                '            Case Is = "Component" : ComponentMethTlst.Add(Line)
                '            Case Is = "Dates" : DatesMethTlst.Add(Line)
                '            Case Is = "Strings" : StringsMethTlst.Add(Line)
                '            Case Is = "Conversions" : ConversionsMethTlst.Add(Line)
                '            Case Is = "Base64" : Base64MethTlst.Add(Line)
                '            Case Is = "Statistics" : StatisticsMethTlst.Add(Line)
                '            Case Is = "Matrix" : MatrixMethTlst.Add(Line)
                '        End Select

                '    End If

                '    If Line.StartsWith("<") AndAlso Not Line.StartsWith("</") Then blhlp = True : str = Line.Replace("<", "").Replace(">", "")

                'Next
                ToolStripButton5.PerformClick() ' meh :3 lol

                My.Settings.LangSave = e.ClickedItem.Text
                My.Settings.Save()

            ElseIf File.Exists(CurrentHardDriver & "VB4Android\Languages\" & e.ClickedItem.Text & ".Lang") = False Then '  Or File.Exists(CurrentHardDriver & "VB4Android\Languages\" & e.ClickedItem.Text & ".LangMeth") = False
                Using New Centered_MessageBox(Me)
                    MsgBox("File Doesn't Exist.", MsgBoxStyle.Exclamation, "Doesn't Exist.")
                End Using
            Else

                'MsgBox(1)
            End If

        End If
    End Sub
    ' Public Sub Set_Language(lang As String)

    ' End Sub

    Private Sub TextBox1_DoubleClick(sender As Object, e As EventArgs) Handles TextBox1.DoubleClick
        If Not Label4.Text = "" Then
            OpenFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            OpenFileDialog1.Filter = "Icon Files |*.png"
            OpenFileDialog1.Title = "Select An Icon File."

            If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                Try

                    If OpenFileDialog1.FileName = CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & "icon.png" Then Beep() : Exit Sub

                    TextBox1.Text = CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & "icon.png"





                    PictureBox3.Image.Dispose()
                    PictureBox3.Image = Nothing

                    'Try

                    'Catch ex As Exception
                    'End Try
                    'MsgBox(OpenFileDialog1.FileName)

                    My.Computer.FileSystem.DeleteFile(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & "icon.png")
                    My.Computer.FileSystem.CopyFile(OpenFileDialog1.FileName,
                                                    CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & "icon.png",
                                                    False)

                    PictureBox3.Image = Image.FromFile(OpenFileDialog1.FileName)

                    PictureBox3.SizeMode = PictureBoxSizeMode.Zoom
                    PictureBox3.Refresh()
                    PictureBox3.SizeMode = PictureBoxSizeMode.CenterImage
                    PictureBox3.Refresh()

                Catch Ex As Exception
                    TextBox1.Text = "{My.Resources.Icon}"
                    Using New Centered_MessageBox(Me)
                        MessageBox.Show("TextBox1_DoubleClick Error:" & vbNewLine & Ex.Message)
                    End Using

                End Try
            End If

        End If
    End Sub


    Public Shared Function ColourFromData(s As String) As Color
        Dim fallbackColour = Color.Black



        ' Extract whatever is between the brackets.
        Dim re = New Regex("\{(.+?)}")
        Dim colorNameMatch = re.Match(s)
        If Not colorNameMatch.Success Then
            Return fallbackColour
        End If

        Dim colourName = colorNameMatch.Groups(1).Value

        ' Get the names of the known colours.
        'TODO: If this function is called frequently, consider creating allColours as a variable with a larger scope.
        Dim allColours = [Enum].GetNames(GetType(System.Drawing.KnownColor))

        ' Attempt a case-insensitive match to the known colours.
        Dim nameOfColour = allColours.FirstOrDefault(Function(c) String.Compare(c, colourName, StringComparison.OrdinalIgnoreCase) = 0)

        If Not String.IsNullOrEmpty(nameOfColour) Then
            Return Color.FromName(nameOfColour)
        End If

        ' Was not a named colour. Parse for ARGB values.
        re = New Regex("(\d+).*?;(\d+).*?;(\d+).*?;(\d+)", RegexOptions.IgnoreCase)
        Dim componentMatches = re.Match(colourName)

        If componentMatches.Success Then

            Dim a = Integer.Parse(componentMatches.Groups(1).Value)
            Dim r = Integer.Parse(componentMatches.Groups(2).Value)
            Dim g = Integer.Parse(componentMatches.Groups(3).Value)
            Dim b = Integer.Parse(componentMatches.Groups(4).Value)

            Dim maxValue = 255

            If a > maxValue OrElse r > maxValue OrElse g > maxValue OrElse b > maxValue Then
                Return fallbackColour
            End If

            Return System.Drawing.Color.FromArgb(a, r, g, b)

        End If

        Return fallbackColour

    End Function

    Private Sub ViewFolderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RecoveryToolStripMenuItem.Click
        If Not Label4.Text = "" Then
            System.Diagnostics.Process.Start(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName)
        Else
            System.Diagnostics.Process.Start(CurrentHardDriver & "VB4Android\Projects\")
        End If
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        'Using New Centered_MessageBox(Me)
        '    Select Case MsgBox("", vbYesNo, "Save all.")
        '        Case MsgBoxResult.Yes
        '            MsgBox("It is not supported yet xD but it will be (it is not dificult).")
        '        Case MsgBoxResult.No
        '    End Select
        'End Using
        End
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged ' Phone
        If Not Label4.Text = "" Then
            If CheckBox1.Checked = True Then
                My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj", My.Computer.FileSystem.ReadAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj").Replace("Phone=False", "Phone=True"), False)
            Else
                My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj", My.Computer.FileSystem.ReadAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj").Replace("Phone=True", "Phone=False"), False)
            End If
        End If
    End Sub

    Private Sub CheckBox8_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox8.CheckedChanged ' Temp-Sensor 
        If Not Label4.Text = "" Then
            If CheckBox8.Checked = True Then
                My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj", My.Computer.FileSystem.ReadAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj").Replace("TempSensor=False", "TempSensor=True"), False)
            Else
                My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj", My.Computer.FileSystem.ReadAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj").Replace("TempSensor=True", "TempSensor=False"), False)
            End If
        End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged ' G-Sensore
        If Not Label4.Text = "" Then
            If CheckBox3.Checked = True Then
                My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj", My.Computer.FileSystem.ReadAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj").Replace("GSensore=False", "GSensore=True"), False)
            Else
                My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj", My.Computer.FileSystem.ReadAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj").Replace("GSensore=True", "GSensore=False"), False)
            End If
        End If
    End Sub

    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckedChanged ' Network
        If Not Label4.Text = "" Then
            If CheckBox4.Checked = True Then
                My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj", My.Computer.FileSystem.ReadAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj").Replace("Network=False", "Network=True"), False)
            Else
                My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj", My.Computer.FileSystem.ReadAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj").Replace("Network=True", "Network=False"), False)
            End If
        End If
    End Sub

    Private Sub CheckBox5_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox5.CheckedChanged ' Acc-Sensor
        If Not Label4.Text = "" Then
            If CheckBox5.Checked = True Then
                My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj", My.Computer.FileSystem.ReadAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj").Replace("AccSensor=False", "AccSensor=True"), False)
            Else
                My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj", My.Computer.FileSystem.ReadAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj").Replace("AccSensor=True", "AccSensor=False"), False)
            End If
        End If
    End Sub
    Private Sub CheckBox6_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox6.CheckedChanged 'Gyro-Sensor 
        If Not Label4.Text = "" Then
            If CheckBox6.Checked = True Then
                My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj", My.Computer.FileSystem.ReadAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj").Replace("GyroSensor=False", "GyroSensor=True"), False)
            Else
                My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj", My.Computer.FileSystem.ReadAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj").Replace("GyroSensor=True", "GyroSensor=False"), False)
            End If
        End If
    End Sub
    Private Sub CheckBox7_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox7.CheckedChanged ' LightSensor
        If Not Label4.Text = "" Then
            If CheckBox7.Checked = True Then
                My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj", My.Computer.FileSystem.ReadAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj").Replace("LightSensor=False", "LightSensor=True"), False)
            Else
                My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj", My.Computer.FileSystem.ReadAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj").Replace("LightSensor=True", "LightSensor=False"), False)
            End If
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged ' GPS
        If Not Label4.Text = "" Then
            If CheckBox2.Checked = True Then
                My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj", My.Computer.FileSystem.ReadAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj").Replace("GPS=False", "GPS=True"), False)
            Else
                My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj", My.Computer.FileSystem.ReadAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj").Replace("GPS=True", "GPS=False"), False)
            End If
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click

    End Sub

    Private Sub PictureBox3_DoubleClick(sender As Object, e As EventArgs) Handles PictureBox3.DoubleClick
        If PictureBox3.SizeMode = PictureBoxSizeMode.CenterImage Then
            PictureBox3.SizeMode = PictureBoxSizeMode.Zoom
        Else
            PictureBox3.SizeMode = PictureBoxSizeMode.CenterImage
        End If
    End Sub

    Private Sub ComboboxImage1_TextChanged(sender As Object, e As EventArgs) Handles ComboboxImage1.TextChanged
        ComboboxImage2.Items.Clear()




        For i = 0 To ComboboxImage1.Items.Count - 1
            If ComboboxImage1.Items.Item(i).ToString = ComboboxImage1.Text Then

                Select Case ComboboxImage1.Items.Item(i).ImageIndex
                    Case Is = 0 'AButton
                        With ComboboxImage2.Items
                            .Add(New ComboBoxIconItem("Click()", 1))
                            .Add(New ComboBoxIconItem("GotFocus()", 1))
                            .Add(New ComboBoxIconItem("PressDown()", 1))
                            .Add(New ComboBoxIconItem("PressUp()", 1))
                            .Add(New ComboBoxIconItem("LostFocus()", 1))
                        End With
                    Case Is = 1 ' Canvas
                        With ComboboxImage2.Items
                            .Add(New ComboBoxIconItem("VB4ADown(x As Integer, y As Integer)", 1))
                            .Add(New ComboBoxIconItem("VB4AUp(x As Integer, y As Integer)", 1))
                            .Add(New ComboBoxIconItem("VB4AMove(lastx As Integer, lasty As Integer, currentx As Integer, currenty As Integer)", 1))
                        End With
                    Case Is = 2 'ACheckBox
                        With ComboboxImage2.Items
                            .Add(New ComboBoxIconItem("Changed()", 1))
                            .Add(New ComboBoxIconItem("GotFocus()", 1))
                            .Add(New ComboBoxIconItem("LostFocus()", 1))
                        End With
                    Case Is = 4 'ALabel
                        With ComboboxImage2.Items
                            .Add(New ComboBoxIconItem("Click()", 1))
                            .Add(New ComboBoxIconItem("LongClick()", 1))
                        End With
                    Case Is = 5 'AListView
                        With ComboboxImage2.Items
                            .Add(New ComboBoxIconItem("GotFocus()", 1))
                            .Add(New ComboBoxIconItem("LostFocus()", 1))
                            .Add(New ComboBoxIconItem("ItemClicked(item As Integer)", 1))
                            .Add(New ComboBoxIconItem("ItemLongClicked(item As Integer)", 1))
                        End With
                    Case Is = 6 'APanel

                    Case Is = 7 'PasswordTextBox
                        With ComboboxImage2.Items
                            .Add(New ComboBoxIconItem("GotFocus()", 1))
                            .Add(New ComboBoxIconItem("LostFocus()", 1))
                        End With
                    Case Is = 8 ' APictureBox
                        With ComboboxImage2.Items
                            .Add(New ComboBoxIconItem("Click()", 1))
                            .Add(New ComboBoxIconItem("LongClick()", 1))
                        End With
                    Case Is = 9 'AProgressBar

                    Case Is = 10 'ARadioButton
                        With ComboboxImage2.Items
                            .Add(New ComboBoxIconItem("Change()", 1))
                            .Add(New ComboBoxIconItem("GotFocus()", 1))
                            .Add(New ComboBoxIconItem("LostFocus()", 1))
                        End With
                    Case Is = 12 ' ATextBox
                        With ComboboxImage2.Items
                            .Add(New ComboBoxIconItem("Change()", 1))
                            .Add(New ComboBoxIconItem("GotFocus()", 1))
                            .Add(New ComboBoxIconItem("LostFocus()", 1))
                            '.Add(New ComboBoxIconItem("Validate(String text,               BooleanReferenceParameter() accept)", 1))
                        End With
                    Case Is = 13 ' ATimer
                        With ComboboxImage2.Items
                            .Add(New ComboBoxIconItem("Timer()", 1))
                        End With
                    Case Is = 14 'AComboBox
                        With ComboboxImage2.Items
                            .Add(New ComboBoxIconItem("GotFocus()", 1))
                            .Add(New ComboBoxIconItem("LostFocus()", 1))
                            .Add(New ComboBoxIconItem("ItemSelected(item As Integer)", 1))
                            .Add(New ComboBoxIconItem("NothingSelected()", 1))
                        End With
                    Case Is = 15 'VB4AWeb

                    Case Is = 16 ' Form
                        With ComboboxImage2.Items
                            .Add(New ComboBoxIconItem("Keyboard(Keycode As Integer)", 1))
                            .Add(New ComboBoxIconItem("VB4AInputBoxClicked(button As Integer)", 1))
                            .Add(New ComboBoxIconItem("VB4AListboxClicked(id As Integer)", 1))
                            .Add(New ComboBoxIconItem("VB4ARadioboxClicked(id As Integer)", 1))
                            .Add(New ComboBoxIconItem("VB4ACheckboxClicked(val As Boolean)", 1))
                            .Add(New ComboBoxIconItem("Resumed()", 1))
                            .Add(New ComboBoxIconItem("FormLoad()", 1))
                            .Add(New ComboBoxIconItem("Initialize()", 1))
                            .Add(New ComboBoxIconItem("FormHide()", 1))
                            .Add(New ComboBoxIconItem("Stopped()", 1))
                            .Add(New ComboBoxIconItem("MenuSelected(caption As String)", 1))
                            .Add(New ComboBoxIconItem("TouchGesture(direction As Integer)", 1))
                        End With
                End Select
                Exit For
            End If
        Next


    End Sub

    Private Sub ComboboxImage2_SelectedValueChanged(sender As Object, e As EventArgs) Handles ComboboxImage2.SelectedValueChanged
        If boolhelpEvents = False Then

            'MsgBox("Sub " & ComboboxImage1.Text & "_" & ComboboxImage2.Text.Split("(").First.Split(")").Last)

            If Not Textbox3.Text.Contains("Event " & ComboboxImage1.Text & "." & ComboboxImage2.Text.Split("(").First.Split(")").Last) Then
                Textbox3.Text &= vbNewLine & "Event " & ComboboxImage1.Text & "." & ComboboxImage2.Text & vbNewLine & "    " & vbNewLine & "End Event" & vbNewLine
                For Each r In Textbox3.Range.GetRanges("Event " & ComboboxImage1.Text & "." & ComboboxImage2.Text.Split("(").First.Split(")").Last)
                    Textbox3.Selection = r
                    Textbox3.DoSelectionVisible()
                Next
            ElseIf Textbox3.Text.Contains("Event " & ComboboxImage1.Text & "." & ComboboxImage2.Text.Split("(").First.Split(")").Last) Then
                For Each r In Textbox3.Range.GetRanges("Event " & ComboboxImage1.Text & "." & ComboboxImage2.Text.Split("(").First.Split(")").Last)
                    Textbox3.Selection = r
                    Textbox3.DoSelectionVisible()
                Next
            End If

        End If
    End Sub

    Private Sub ToolTip1_Popup(sender As Object, e As PopupEventArgs) Handles ToolTip1.Popup

    End Sub

    Private Sub ToolStripTextBox1_Click(sender As Object, e As EventArgs) Handles ToolStripTextBox1.Click
        If ToolStripTextBox1.TextLength < 9 Or Not ToolStripTextBox1.Text.StartsWith("com.vb4a.") Then ToolStripTextBox1.Text = "com.vb4a."
    End Sub

    Private Sub ToolStripTextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles ToolStripTextBox1.KeyDown
        If ToolStripTextBox1.TextLength < 9 Or Not ToolStripTextBox1.Text.StartsWith("com.vb4a.") Then ToolStripTextBox1.Text = "com.vb4a."
        If ToolStripTextBox1.TextLength < 9 Or Not ToolStripTextBox1.Text.StartsWith("com.vb4a.") Then ToolStripTextBox1.Text = "com.vb4a."
    End Sub

    Private Sub ToolStripTextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ToolStripTextBox1.KeyPress
        If (Microsoft.VisualBasic.Asc(e.KeyChar) < 97) Or (Microsoft.VisualBasic.Asc(e.KeyChar) > 122) Then
            e.Handled = True
        End If
        If (Microsoft.VisualBasic.Asc(e.KeyChar) = 8) Then
            e.Handled = False

        End If
        If (Microsoft.VisualBasic.Asc(e.KeyChar) > 47) AndAlso (Microsoft.VisualBasic.Asc(e.KeyChar) < 58) Then
            e.Handled = False
        End If
    End Sub

    Private Sub ToolStripTextBox1_KeyUp(sender As Object, e As KeyEventArgs) Handles ToolStripTextBox1.KeyUp
        If ToolStripTextBox1.TextLength < 9 Or Not ToolStripTextBox1.Text.StartsWith("com.vb4a.") Then ToolStripTextBox1.Text = "com.vb4a."
        If e.KeyCode = Keys.Enter Then : If Not ToolStripTextBox1.Text = "com.vb4a." Then
                ToolStripTextBox1.Text = "com.vb4a." & ToolStripTextBox1.Text.Replace("com.vb4a.", "").Replace(ToolStripTextBox1.Text.Replace("com.vb4a.", "").Substring(0, 1) & ToolStripTextBox1.Text.Replace("com.vb4a.", "").Substring(1, ToolStripTextBox1.Text.Replace("com.vb4a.", "").Length - 1), ToolStripTextBox1.Text.Replace("com.vb4a.", "").Substring(0, 1).ToUpper & ToolStripTextBox1.Text.Replace("com.vb4a.", "").Substring(1, ToolStripTextBox1.Text.Replace("com.vb4a.", "").Length - 1).ToLower)
                'MsgBox()
                'MsgBox(My.Computer.FileSystem.ReadAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj").Split("#")(1).Split("=")(1).Replace(vbNewLine, ""))
                'Dim prvnam As String = 
                For Each ln As String In My.Computer.FileSystem.ReadAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj").Split(vbNewLine)
                    ln = ln.Replace(vbLf, Nothing)
                    If ln.StartsWith("ApplicationPKGName") Then

                        ' If BeforeCombo3 = ln.Replace("ApplicationPKGName=", "") & ".vb4a" Then BeforeCombo3 = ToolStripTextBox1.Text.Replace("com.vb4a.", Nothing) & ".vb4a"
                        My.Computer.FileSystem.RenameFile(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & ln.Replace("ApplicationPKGName=", "") & ".vb4a", ToolStripTextBox1.Text.Replace("com.vb4a.", Nothing) & ".vb4a")
                        My.Computer.FileSystem.RenameFile(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & ln.Replace("ApplicationPKGName=", "") & ".vb4acode", ToolStripTextBox1.Text.Replace("com.vb4a.", Nothing) & ".vb4acode")
                        My.Computer.FileSystem.RenameFile(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & ln.Replace("ApplicationPKGName=", "") & ".DelConts", ToolStripTextBox1.Text.Replace("com.vb4a.", Nothing) & ".DelConts")
                        For item = 0 To Combobox3.Items.Count - 1 ' also check i the begining if this new name exist allready in other form
                            If Combobox3.Items.Item(item).ToString = ln.Replace("ApplicationPKGName=", "") & ".vb4a" Then
                                If BeforeCombo3 = ln.Replace("ApplicationPKGName=", "") & ".vb4a" Then BeforeCombo3 = ToolStripTextBox1.Text.Replace("com.vb4a.", Nothing) & ".vb4a"
                                Combobox3.Items.RemoveAt(item)
                                Combobox3.Items.Add(New ComboBoxIconItem(ToolStripTextBox1.Text.Replace("com.vb4a.", Nothing) & ".vb4a", 0))
                                Combobox3.Text = ToolStripTextBox1.Text.Replace("com.vb4a.", Nothing) & ".vb4a"

                            End If
                        Next

                        For item = 0 To Combobox1.Items.Count - 1 ' also check i the begining if this new name exist allready in other form
                            If Combobox1.Items.Item(item).ToString = ln.Replace("ApplicationPKGName=", "") Then
                                'If BeforeCombo3 = ln.Replace("ApplicationPKGName=", "") Then BeforeCombo3 = ToolStripTextBox1.Text.Replace("com.vb4a.", Nothing)
                                Combobox1.Items.RemoveAt(item)
                                Combobox1.Items.Add(New ComboBoxIconItem(ToolStripTextBox1.Text.Replace("com.vb4a.", Nothing), 16)) 'Combobox1.Items.Add(New ComboBoxIconItem(Combobox3.Text.Replace(".vb4a", Nothing), 16))
                                Combobox1.Text = ToolStripTextBox1.Text.Replace("com.vb4a.", Nothing)

                            End If
                        Next

                        ' also i have to add and one more for...next for imagecombobox (for events)

                        My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj", My.Computer.FileSystem.ReadAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj").Replace("ApplicationPKGName=" & ln.Replace("ApplicationPKGName=", Nothing), "ApplicationPKGName=" & ToolStripTextBox1.Text.Replace("com.vb4a.", Nothing)), False)
                        Exit For
                    End If

                Next


                ConsoleDouble += 1
                RichTextBox1.AppendText("[" & ConsoleDouble & "] " & "Application's Packagename Changed To '" & ToolStripTextBox1.Text & "' " & "At " & DateTime.Now.ToString & "." & vbNewLine)
                Timer2.Enabled = True
                RichTextBox1.ScrollToCaret()
            Else : MsgBox("You have to set a Packagename in your App.", MsgBoxStyle.Information, "Packagename.")
            End If : End If
    End Sub

    Private Sub ToolStripComboBox1_Click(sender As Object, e As EventArgs) Handles ToolStripComboBox1.Click

    End Sub

    Private Sub ToolStripComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ToolStripComboBox1.SelectedIndexChanged
        'MsgBox(ToolStripComboBox1.SelectedItem.ToString())
        For Each ln As String In My.Computer.FileSystem.ReadAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj").Split(vbNewLine)
            ln = ln.Replace(vbLf, Nothing)
            If ln.StartsWith("ApplicationTheme=") Then My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj", My.Computer.FileSystem.ReadAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj").Replace("ApplicationTheme=" & ln.Replace("ApplicationTheme=", Nothing), "ApplicationTheme=Theme." & ToolStripComboBox1.SelectedItem.ToString), False) : Exit For
        Next
    End Sub


    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click ' i have to check also if apk is in folder
        If Directory.Exists(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\BUILD\build\deploy") Then
            Process.Start("explorer.exe", CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\BUILD\build\deploy")
        Else
            Using New Centered_MessageBox(Me)
                MsgBox("There is no Apk File.", MsgBoxStyle.Critical, "Apk File not found.")
            End Using
        End If
    End Sub


    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        If File.Exists(CurrentHardDriver & "VB4Android\" & "compiler.exe") AndAlso Directory.Exists(CurrentHardDriver & "VB4Android\lib") Then
            ShadowForm.PenC = New Pen(Color.FromArgb(202, 81, 0), 1)
            ShadowForm.BackColor = Color.FromArgb(202, 81, 0)
            SaveToolStripMenuItem1.PerformClick()
            If RichTextBox1.Visible = False Then Button10.PerformClick()
            Panel3.Enabled = False
            ConsoleDouble += 2
            File.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\Compile.log", "")
            RichTextBox1.AppendText("[" & ConsoleDouble - 1 & "]" & vbNewLine & "[" & ConsoleDouble & "] Starting Console." & vbNewLine)
            Shell("cmd /c" & CurrentHardDriver & "VB4Android\compiler.exe " & CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & " " & Label4.Text.Replace("- ( ", "").Replace(" )", ""), AppWinStyle.Hide)
            ConsoleDouble += 1
            RichTextBox1.AppendText("[" & ConsoleDouble & "] Console Successfully Started." & vbNewLine)
            RichTextBox1.ScrollToCaret()
            ' i was boring xD
            'Loop1:      If Not System.Diagnostics.Process.GetProcessesByName("compiler").Length > 0 Then 'AndAlso File.Exists(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\Compile.log") = True Then ' there is chance running a compiler.exe but not this exe xD but anyway for now ---------------------------
            '                If File.Exists(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\Compile.log") = True Then

            '                    For Each ln As String In File.ReadAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\Compile.log").Split(vbNewLine)
            '                        ConsoleDouble += 1
            '                        RichTextBox1.AppendText("[" & ConsoleDouble & "] " & ln.Replace(vbNewLine, "").Replace(vbLf, "") & vbNewLine)
            '                    Next
            '                Else : GoTo Loop1
            '                End If
            '            Else : GoTo Loop1
            '            End If




            'If RichTextBox1.Visible = False Then Button10.PerformClick()

            Timer3.Enabled = True

        ElseIf Not File.Exists(CurrentHardDriver & "VB4Android\compiler.exe") And Not Directory.Exists(CurrentHardDriver & "VB4Android\lib") Then
            Using New Centered_MessageBox(Me)
                MsgBox("There is no Compiler (compiler.exe File) and lib folder.", MsgBoxStyle.Critical, "Compiler and lib folder not found.")
            End Using
        ElseIf Not File.Exists(CurrentHardDriver & "VB4Android\compiler.exe") Then
            Using New Centered_MessageBox(Me)
                MsgBox("There is no Compiler (compiler.exe File).", MsgBoxStyle.Critical, "Compiler not found.")
            End Using
        ElseIf Not Directory.Exists(CurrentHardDriver & "VB4Android\lib") Then
            Using New Centered_MessageBox(Me)
                MsgBox("There is no lib folder.", MsgBoxStyle.Critical, "lib folder not found.")
            End Using
        End If
    End Sub



    Private Sub border1_MouseMove(sender As Object, e As MouseEventArgs)
        'If e.Location.X > Me.Size.Width - 7 Then : border1.Cursor = Cursors.SizeWE
        '    If Textbox3.Visible = True AndAlso e.Button = Windows.Forms.MouseButtons.Left Then
        '        'border1.Cursor = Cursors.PanNE
        '        If e.X - Button3.Location.X > 0 AndAlso Not e.X - Button3.Location.X < Button3.Size.Width + 5 Then
        '            Button3.Location = New Point(Button3.Location.X + 1, Button3.Location.Y)
        '        ElseIf e.X - Button3.Location.X > 0 AndAlso e.X - Button3.Location.X < Button3.Size.Width + 5 Then
        '            Button3.Location = New Point(Button3.Location.X - 1, Button3.Location.Y)
        '        End If

        '        Me.Size = New Size(Me.PointToClient(MousePosition).X - 1, Me.Size.Height)
        '        border1.Size = New Size(border1.PointToClient(MousePosition).X, border1.Size.Height)
        '        Panel4.Size = New Size(Panel4.PointToClient(MousePosition).X, Panel4.Size.Height)
        '    End If
        'Else : border1.Cursor = Cursors.Arrow
        '    If Not border1.Size.Width = Me.Size.Width Or Not border1.Size.Width = Panel4.Size.Width Then
        '        Me.Size = New Size(border1.Size.Width, Me.Size.Height)
        '        Panel4.Size = New Size(border1.Size.Width, Panel4.Size.Height)
        '    End If
        '    If Not border1.Size.Width - 5 - Button3.Size.Width = Button3.Location.X Then

        '    End If
        'End If

    End Sub

    Private Sub border1_MouseUp(sender As Object, e As MouseEventArgs)

    End Sub

    Private Sub ToolStripButton7_Click(sender As Object, e As EventArgs) Handles ToolStripButton7.Click
        If Not Combobox3.Items.Count = 0 Then
            Dim i As Integer = Textbox3.Selection.Start.iLine
            TextcoderWordsReplace()
            Textbox3.Navigate(i)
            Dim selitcom As String = Combobox3.SelectedItem.ToString
            Combobox3.Text = Nothing
            Combobox3.Text = selitcom
            selitcom = Nothing
            Timer2.Enabled = True
        Else
            Using New Centered_MessageBox(Me)
                MsgBox("You Must Create A Project First, In Order " & vbNewLine & " To Be Able To Save It. xD But wtf ^_^ how you can save from here when...", MsgBoxStyle.Information)
            End Using
        End If
    End Sub

    Private Sub RunToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RunToolStripMenuItem.Click
        If Not Label4.Text = Nothing Then Button8.PerformClick()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        If File.Exists(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\BUILD\Build\deploy\" & ToolStripTextBox1.Text.Remove(0, 9) & ".apk") AndAlso File.Exists(CurrentHardDriver & "VB4Android\lib\bin\adb.exe") Then
            ' If Not RichTextBox1.Visible = True Then Button10.PerformClick()
            'app.path & "\lib\bin\adb.exe install -r " & app.path & "\Projects\" & PRJNAME & "\BUILD\build\deploy\" & PKGNAME & ".apk
            'MsgBox(ToolStripTextBox1.Text.Remove(0, 9))
            ConsoleDouble += 2
            RichTextBox1.AppendText("[" & ConsoleDouble - 1 & "]" & vbNewLine & "[" & ConsoleDouble & "] Installing apk." & vbNewLine)
            RichTextBox1.ScrollToCaret()
            Shell("cmd /c" & CurrentHardDriver & "VB4Android\lib\bin\adb.exe install -r " & CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\BUILD\Build\deploy\" & ToolStripTextBox1.Text.Remove(0, 9) & ".apk") ', AppWinStyle.Hide

        ElseIf Not File.Exists(CurrentHardDriver & "VB4Android\" & FileCreatedName & "\BUILD\Build\deploy\" & ToolStripTextBox1.Text.Remove(0, 9) & ".apk") AndAlso Not File.Exists(CurrentHardDriver & "VB4Android\lib\bin\adb.exe") Then
            Using New Centered_MessageBox(Me)
                MsgBox(ToolStripTextBox1.Text.Remove(0, 9) & ".apk and also '" & CurrentHardDriver & "VB4Android\lib\bin\adb.exe" & "' not found.", MsgBoxStyle.Critical, "Apk And adb.exe Not Found")
            End Using
        ElseIf Not File.Exists(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\BUILD\Build\deploy\" & ToolStripTextBox1.Text.Remove(0, 9) & ".apk") Then
            Using New Centered_MessageBox(Me)
                MsgBox(ToolStripTextBox1.Text.Remove(0, 9) & ".apk not found, try first to compile the project and then run it.", MsgBoxStyle.Exclamation, ToolStripTextBox1.Text.Remove(0, 9) & " Not Found.")
            End Using
        ElseIf Not File.Exists(CurrentHardDriver & "VB4Android\lib\bin\adb.exe") Then
            Using New Centered_MessageBox(Me)
                MsgBox(CurrentHardDriver & "VB4Android\lib\bin\adb.exe" & " not found.", MsgBoxStyle.Critical, "adb.exe Not Found")
            End Using
        End If
    End Sub

    Private Sub CompileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CompileToolStripMenuItem.Click
        If Panel3.Enabled = True Then Button7.PerformClick() Else Beep()
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        If Panel3.Enabled = True Then

            If File.Exists(CurrentHardDriver & "VB4Android\" & "VB4AEMU.exe") AndAlso Directory.Exists(CurrentHardDriver & "VB4Android\lib\VB4AEMU") Then

                ConsoleDouble += 2
                RichTextBox1.AppendText("[" & ConsoleDouble - 1 & "]" & vbNewLine & "[" & ConsoleDouble & "] Starting Emulator." & vbNewLine)
                Shell("cmd /c" & CurrentHardDriver & "VB4Android\VB4AEMU.exe 480x800", AppWinStyle.Hide)


                Do
                    If Process.GetProcessesByName("VB4AEMU").Length > 0 Then
                        ConsoleDouble += 1
                        RichTextBox1.AppendText("[" & ConsoleDouble & "] Emulator Started At 480x800 Resolution." & vbNewLine)
                        Exit Do
                    End If
                Loop
                RichTextBox1.ScrollToCaret()
            ElseIf Not File.Exists(CurrentHardDriver & "VB4Android\VB4AEMU.exe") And Not Directory.Exists(CurrentHardDriver & "VB4Android\lib\VB4AEMU") Then
                Using New Centered_MessageBox(Me)
                    MsgBox("There is no Emulator (VB4AEMU.exe File) and VB4AEMU folder.", MsgBoxStyle.Critical, "Emulator and VB4AEMU folder not found.")
                End Using
            ElseIf Not File.Exists(CurrentHardDriver & "VB4Android\VB4AEMU.exe") Then
                Using New Centered_MessageBox(Me)
                    MsgBox("There is no Emulator (VB4AEMU.exe File).", MsgBoxStyle.Critical, "Compiler not found.")
                End Using
            ElseIf Not Directory.Exists(CurrentHardDriver & "VB4Android\lib\VB4AEMU") Then
                Using New Centered_MessageBox(Me)
                    MsgBox("There is no VB4AEMU folder.", MsgBoxStyle.Critical, "VB4AEMU folder not found.")
                End Using
            End If

        End If
    End Sub

    Private Sub NewFormToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewFormToolStripMenuItem.Click
        Button1.PerformClick()
    End Sub

    Private Sub LoadExampleCntrEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadExampleCntrEToolStripMenuItem.Click

    End Sub


    Private Sub AddToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddToolStripMenuItem.Click
        OpenFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
        OpenFileDialog1.Filter = "C++\C\vb4a Files |*.cpp;*.c;*.vb4acode|C++ Files|*.cpp|C Files|*.c|vb4a Files|*.vb4acode"
        OpenFileDialog1.Title = "Select A cpp or c File."

        If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then ' exw na kanw sto load to read kai epishs opos kai edw add to name sto combobox (tooltip or tag 'remember)
            If IsUsedFromAnotherProc(OpenFileDialog1.FileName) = False Then
                If Not File.Exists(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\CPROJ\" & OpenFileDialog1.SafeFileName) Then
                    My.Computer.FileSystem.CopyFile(OpenFileDialog1.FileName, CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\CPROJ\" & OpenFileDialog1.SafeFileName)
                    ToolStripComboBox2.Items.Add(OpenFileDialog1.SafeFileName)
                    ' My.Computer.FileSystem.WriteAllText("", "", False)
                    Dim f As String = String.Empty
                    For Each fl As String In My.Computer.FileSystem.GetFiles(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\CPROJ\")
                        If fl.EndsWith(".cpp") Or fl.EndsWith(".c") Or fl.EndsWith(".vb4acode") Then f &= fl.Split("\").Last & "|"
                    Next
                    My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj", My.Computer.FileSystem.ReadAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj").Replace("APPWITHC=False", "APPWITHC=True"), False)
                    If My.Computer.FileSystem.GetFiles(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\CPROJ\").Count - 1 = 1 Then ' i know i could do it and with other way but anyway :PPPPP!!!
                        My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\CPROJ\cproject.v4a", "cprojtype=Single" & vbNewLine &
                                                                                                                                                  "cprojname=" & ToolStripTextBox2.Text & vbNewLine &
                                                                                                                                                  "cfiles=" & f.Substring(0, f.Length - 1), False)

                    ElseIf My.Computer.FileSystem.GetFiles(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\CPROJ\").Count - 1 > 1 Then
                        My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\CPROJ\cproject.v4a", "cprojtype=Multiple" & vbNewLine &
                                                                                                                                                  "cprojname=" & ToolStripTextBox2.Text & vbNewLine &
                                                                                                                                                  "cfiles=" & f.Substring(0, f.Length - 1), False)
                        ' na valw edw kai to toolstriptextbox2.text kai na to kanw epishs kai edit to event gia save 
                    End If
                Else : Using New Centered_MessageBox(Me) : MsgBox("File Already Exists.", vbExclamation) : End Using
                End If

            Else : Using New Centered_MessageBox(Me) : MsgBox("File is being Used By another process", vbCritical) : End Using
            End If
        End If
    End Sub
    Private Sub ToolStripComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ToolStripComboBox2.SelectedIndexChanged

    End Sub
    Private Sub ToolStripComboBox2_Click(sender As Object, e As EventArgs) Handles ToolStripComboBox2.Click

    End Sub
    Private Sub ToolStripDropDownButton2_OwnerChanged(sender As Object, e As EventArgs) Handles ToolStripDropDownButton2.OwnerChanged

    End Sub


    Private Sub RemoveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RemoveToolStripMenuItem.Click
        If IsUsedFromAnotherProc(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\CPROJ\" & ToolStripComboBox2.SelectedItem) = False AndAlso Not ToolStripTextBox2.Text.TrimEnd.TrimStart = "" Then
            If Not ToolStripComboBox2.SelectedItem = Nothing AndAlso File.Exists(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\CPROJ\" & ToolStripComboBox2.SelectedItem) Then
                My.Computer.FileSystem.DeleteFile(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\CPROJ\" & ToolStripComboBox2.SelectedItem)
                ToolStripComboBox2.Items.Remove(ToolStripComboBox2.SelectedItem)
                ToolStripTextBox2.Text = ToolStripTextBox2.Text.TrimEnd.TrimStart
                Dim f As String = String.Empty
                For fl = 0 To ToolStripComboBox2.Items.Count - 1
                    If ToolStripComboBox2.Items.Item(fl).EndsWith(".cpp") Or ToolStripComboBox2.Items.Item(fl).EndsWith(".c") Or ToolStripComboBox2.Items.Item(fl).EndsWith(".vb4acode") Then f &= ToolStripComboBox2.Items.Item(fl) & "|"
                Next
                If ToolStripComboBox2.Items.Count = 1 Then
                    My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\CPROJ\cproject.v4a", "cprojtype=Single" & vbNewLine &
                                                                                                                                              "cprojname=" & ToolStripTextBox2.Text & vbNewLine &
                                                                                                                                              "cfiles=" & f.Substring(0, f.Length - 1), False)

                ElseIf ToolStripComboBox2.Items.Count > 1 Then
                    My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\CPROJ\cproject.v4a", "cprojtype=Multiple" & vbNewLine &
                                                                                                                                              "cprojname=" & ToolStripTextBox2.Text & vbNewLine &
                                                                                                                                              "cfiles=" & f.Substring(0, f.Length - 1), False)

                ElseIf ToolStripComboBox2.Items.Count = 0 Then
                    My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\CPROJ\cproject.v4a", "cprojtype=" & vbNewLine &
                                                                                                                                              "cprojname=" & ToolStripTextBox2.Text.TrimEnd.TrimStart & vbNewLine &
                                                                                                                                              "cfiles=", False)
                    My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj", My.Computer.FileSystem.ReadAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj").Replace("APPWITHC=True", "APPWITHC=False"), False)
                End If


            End If
        ElseIf IsUsedFromAnotherProc(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\CPROJ\" & ToolStripComboBox2.SelectedItem) = True Then : Using New Centered_MessageBox(Me) : MsgBox("File is being Used By another process", vbCritical) : End Using
        ElseIf ToolStripTextBox2.Text.TrimEnd.TrimStart = "" Then : Using New Centered_MessageBox(Me) : MsgBox("You must set C-ProjectName.", vbCritical) : End Using
        End If
    End Sub

    Private Sub ToolStripTextBox2_KeyUp(sender As Object, e As KeyEventArgs) Handles ToolStripTextBox2.KeyUp
        If e.KeyCode = Keys.Enter AndAlso Not ToolStripTextBox2.Text.TrimEnd.TrimStart = "" Then
            ToolStripTextBox2.Text = ToolStripTextBox2.Text.TrimEnd.TrimStart
            Dim o As String = String.Empty

            For Each ln As String In My.Computer.FileSystem.ReadAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\CPROJ\cproject.v4a").Split(vbNewLine)
                If Not ln.Replace(vbLf, Nothing).StartsWith("cprojname=") Then o &= ln.Replace(vbLf, Nothing) & vbNewLine Else o &= "cprojname=" & ToolStripTextBox2.Text & vbNewLine
            Next
            My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\CPROJ\cproject.v4a", o.Substring(0, o.Length - 2), False)

        ElseIf ToolStripTextBox2.Text.TrimEnd.TrimStart = "" Then : Using New Centered_MessageBox(Me) : MsgBox("You must set C-ProjectName.", vbCritical) : End Using
        End If
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click

    End Sub

    Private Sub ToolStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles ToolStrip1.ItemClicked

    End Sub
    Dim bcksps As Boolean = True  ' lol trast me i am a programmer xD
    Private Sub TextBox3_KeyDown(sender As Object, e As KeyEventArgs) Handles Textbox3.KeyDown
        'If popupMenu2.Visible = True Then popupMenu.MinFragmentLength = popupMenu2.Items.FocussedItem.Text.Length + 1 : popupMenu.Hide() Else popupMenu.MinFragmentLength = 1 : popupMenu.Hide()
        'If popupMenu.Visible = True Then popupMenu2.MinFragmentLength = popupMenu.Items.FocussedItem.Text.Length + 1 : popupMenu2.Hide() Else popupMenu2.MinFragmentLength = 2 : popupMenu2.Hide()

        '  If e.KeyCode = Keys.S And (e.Control) Then
        '  End If
        'MsgBox(fragmentText)
        ' If e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Or e.KeyCode = Keys.PageUp Or e.KeyCode = Keys.PageDown Or e.KeyCode = Keys.Enter Then
        '   If popupMenu.Enabled = False Then popupMenu.Enabled = True
        'PrevLineNum = Textbox3.Selection.Start.iLine

        'End If
        ' If e.KeyCode = Keys.Left Or e.KeyCode = Keys.Right Then
        '      If Not PrevlnnNum = Textbox3.Selection.Start.iLine Then
        '          popupMenu.Enabled = True
        ' End If
        ' End If

        If e.KeyCode = Keys.Back AndAlso bcksps = True Then

            If Textbox3.Selection.Start.iChar > 0 AndAlso Textbox3(Textbox3.Selection.Start.iLine).Text.Substring(0, Textbox3.Selection.Start.iChar).Trim = "" Then
                bcksps = False
                SendKeys.Send("^({BACKSPACE})")
                SendKeys.Send("{BACKSPACE}")
            End If

        End If

        ecode = False
        If e.KeyCode = Keys.OemPeriod Then ecode = True

        If e.KeyCode = Keys.ControlKey Then
            'CtrlIsDown = Keys.Control
            Label7.Show()
            Label7.Text = Textbox3.Zoom
            If Textbox3.Zoom = 107 Then Textbox3.Zoom = 100
        End If

        If Textbox3.Text = "" Then Textbox3.Text = " " ' i have a reason for this :PP!! 

        ' Console.WriteLine(e.KeyValue)

    End Sub
    '   Dim cheackstrings() As String = {"As", "If", "In", "Is", "Me", "On", "To", "And", "Dim", "End", "For", "Get", "New", "Not", "Sub", _
    '                                "Each", "Case", "Else", "Exit", "Like", "Long", "Next", "Step", "Then", "True", _
    '                                "Alias", "ByRef", "ByVal", "Const", "Event", "False", "IsNot", "Short", "Until", "While", _
    '                               "Double", "ElseIf", "Object", "Single", "Static", "String", "Boolean", "Integer", "Variant", "Function", "Collection"}
    ' Dim currentLineIsSame As Boolean = False
    ' Dim PrevLineNum As Double = -1
    ' Dim PrevlnnNum As Double = -1
    'Dim Defarray() As String = {"Dim", "Const"}
    Dim PublicVariables() As String = {"Button=", "Canvas=", "Dates=", "Files=", "Label=", "Panel=", "Phone=", "Timer=", "Arrays=", "Button=", "Canvas=", "Double=", "Matrix=", "Object=", "Single=", "String=", "Boolean=", "Integer=", "Strings=", "TextBox=", "Variant=", "VB4AWeb=", "CheckBox=", "ComboBox=", "Listview=", "Component=", "VB4AInput=", "Collection=", "PictureBox=", "Statistics=", "Application=", "Conversions=", "ProgressBar=", "RadioButton=", "LocationSensor=", "PasswordTextBox=", "OrientationSensor=", "AcceleromaterSenor="}
    Dim PrivateVariables() As String = {"Button=", "Canvas=", "Dates=", "Files=", "Label=", "Panel=", "Phone=", "Timer=", "Arrays=", "Button=", "Canvas=", "Double=", "Matrix=", "Object=", "Single=", "String=", "Boolean=", "Integer=", "Strings=", "TextBox=", "Variant=", "VB4AWeb=", "CheckBox=", "ComboBox=", "Listview=", "Component=", "VB4AInput=", "Collection=", "PictureBox=", "Statistics=", "Application=", "Conversions=", "ProgressBar=", "RadioButton=", "LocationSensor=", "PasswordTextBox=", "OrientationSensor=", "AcceleromaterSenor="}
    'Dim DefKeywordsArray() As String = {"Button", "Canvas", "Dates", "Files", "Label", "Panel", "Phone", "Timer", "Arrays", "Button", "Canvas", "Double", "Matrix", "Object", "Single", "String", "Boolean", "Integer", "Strings", "TextBox", "Variant", "VB4AWeb", "CheckBox", "ComboBox", "Listview", "Component", "VB4AInput", "Collection", "PictureBox", "Statistics", "Application", "Conversions", "ProgressBar", "RadioButton", "LocationSensor", "PasswordTextBox", "OrientationSensor", "AcceleromaterSenor"}

    '  Dim ime As Boolean = False
    '''''''Dim ConstDimVariables As New List(Of String)
    Private Sub Textbox3_TextChanged(sender As Object, e As TextChangedEventArgs) Handles Textbox3.TextChanged
        If Textbox3.Text = "" Then Exit Sub
        ' If ime = True Then ime = False : Exit Sub
        '"[^\x00-\x7F]+\ *(?:[^\x00-\x7F]| )*"  ' i think i can fix the chinese cahracters ....
        'If Textbox3(Textbox3.Selection.Start.iLine).Text.Contains("<-") Then
        'Textbox3.Selection.ClearStyle(New TextStyle(New SolidBrush(Color.FromArgb(0, 43, 116)), Nothing, FontStyle.Bold))

        ''''''''' [\u4e00-\u9fa5][^ |a-z-A-Z-0-9|u4e00-u9fa5]
        ''''''''If Regex.IsMatch(Textbox3(Textbox3.Selection.Start.iLine).Text, "[\u4e00-\u9fa5]") Then

        ''''''''    RemoveHandler Textbox3.TextChanged, AddressOf Me.Textbox3_TextChanged
        ''''''''    For Each r In Textbox3.Range.GetRanges("[\u4e00-\u9fa5]")
        ''''''''        Textbox3.Selection = r

        ''''''''        '    ime = True
        ''''''''        '  MsgBox(r.Text)
        ''''''''        Textbox3.SelectedText = r.Text & " "

        ''''''''        '    Textbox3.Selection.SetStyle(New TextStyle(New SolidBrush(Color.FromArgb(0, 43, 116)), Nothing, FontStyle.Bold))
        ''''''''        '    Textbox3.Selection.Start = New Place(Textbox3.Selection.Start.iLine, Textbox3.Selection.Start.iChar + 1)

        ''''''''        '    Textbox3.Selection.Start = New Place(Textbox3.Selection.Start.iLine, Textbox3.Selection.Start.iChar)
        ''''''''        '    Textbox3.Selection.End = Textbox3.Selection.Start
        ''''''''    Next
        ''''''''    AddHandler Textbox3.TextChanged, AddressOf Me.Textbox3_TextChanged
        ''''''''End If

        ' End If

        'e.ChangedRange.ClearFoldingMarkers()
        'e.ChangedRange.SetFoldingMarkers("{", "}")

        ' If Not popupMenu Is Nothing Then If popupMenu.Visible = True Then popupMenu2.MinFragmentLength = popupMenu.Items.FocussedItem.Text.Length Else popupMenu2.MinFragmentLength = 3 : popupMenu2.Hide()
        'Textbox3.GetLineText(Textbox3.Selection.Start.iLine).Trim() '[Dim]+ [a-zA-Z0-9]+ [As]+ [a-zA-Z]+
        ' If Regex.IsMatch(Textbox3.GetLineText(Textbox3.Selection.Start.iLine).Trim(), "[Dim]+ [a-zA-Z0-9]+ [As]+ [a-zA-Z]+") Then

        'End If
        If Regex.IsMatch(Textbox3.GetLineText(Textbox3.Selection.Start.iLine).Trim(), "(?m)^[ \t]*(?:(?:REM|')[^\r\n]*)?[\r\n]+|(?mn)^(?<line>[^\r\n""R']*((""[^""]*""|(?!REM)R)[^\r\n""R']*)*)(REM|')[^\r\n]*") AndAlso Regex.Replace(Textbox3.GetLineText(Textbox3.Selection.Start.iLine), "(?m)^[ \t]*(?:(?:REM|')[^\r\n]*)?[\r\n]+|(?mn)^(?<line>[^\r\n""R']*((""[^""]*""|(?!REM)R)[^\r\n""R']*)*)(REM|')[^\r\n]*", "${line}").Length < Textbox3.Selection.Start.iChar Then ' I dont Have Any Idea how it works xD 
            If popupMenu.Enabled = True Then popupMenu.Enabled = False ': PrevlnnNum = Textbox3.Selection.Start.iLine
            ' If popupMenu2.Enabled = True Then popupMenu2.Enabled = False
        Else
            If Not popupMenu Is Nothing Then popupMenu.Enabled = True
            'If Not popupMenu2 Is Nothing Then popupMenu2.Enabled = True
            'If Not PrevLineNum = Textbox3.Selection.Start.iLine Then

            ' Dim PublicOrNot As Boolean ' here i will check if variable is private or public


            '   If Textbox3.GetLineText(Textbox3.Selection.Start.iLine).Trim().StartsWith("Dim") Then

            ' I was trying the best optimisation ever here but anyway lol xD
            '''''''If Regex.IsMatch(Regex.Replace(Textbox3.GetLineText(Textbox3.Selection.Start.iLine).Trim().Split("=")(0), "(?m)^[ \t]*(?:(?:REM|')[^\r\n]*)?[\r\n]+|(?mn)^(?<line>[^\r\n""R']*((""[^""]*""|(?!REM)R)[^\r\n""R']*)*)(REM|')[^\r\n]*", "${line}"), "[Dim||Const]+ [a-zA-Z0-9_]+ [As]+ [a-zA-Z]+") Then ' you never know Lol

            '''''''    Dim regtext As String = Regex.Match(Textbox3.GetLineText(Textbox3.Selection.Start.iLine).Trim().Split("=")(0), "[Dim||Const]+ [a-zA-Z0-9_]+ [As]+ [a-zA-Z]+").Value
            '''''''    For i = 0 To PublicVariables.Count - 1
            '''''''        If regtext.EndsWith(PublicVariables(i).Split("=")(0)) Then
            '''''''            ' i cant use "i" again >:(
            '''''''            For j = 0 To ConstDimVariables.Count
            '''''''                If j = ConstDimVariables.Count Then
            '''''''                    ConstDimVariables.Add(regtext & "|" & Textbox3.Selection.Start.iLine)
            '''''''                    Console.WriteLine(regtext & "|" & Textbox3.Selection.Start.iLine)
            '''''''                ElseIf Textbox3.Selection.Start.iLine = ConstDimVariables.Item(Textbox3.Selection.Start.iLine).Split("|")(1) Then

            '''''''                End If
            '''''''            Next
            '''''''            'Console.WriteLine(Textbox3.Selection.Start.iLine & ": " & regtext)
            '''''''            Exit For
            '''''''        End If
            '''''''    Next
            '''''''End If

            'ElseIf Textbox3.GetLineText(Textbox3.Selection.Start.iLine).Trim().StartsWith("Const") Then
            '    If Regex.IsMatch(Regex.Replace(Textbox3.GetLineText(Textbox3.Selection.Start.iLine).Trim().Split("=")(0), "(?m)^[ \t]*(?:(?:REM|')[^\r\n]*)?[\r\n]+|(?mn)^(?<line>[^\r\n""R']*((""[^""]*""|(?!REM)R)[^\r\n""R']*)*)(REM|')[^\r\n]*", "${line}"), "[Const]+ [a-zA-Z0-9_]+ [As]+ [a-zA-Z]+") Then

            '        Dim regtext As String = Regex.Match(Textbox3.GetLineText(Textbox3.Selection.Start.iLine).Trim().Split("=")(0), "[Const]+ [a-zA-Z0-9_]+ [As]+ [a-zA-Z]+").Value
            '        For i = 0 To PublicVariables.Count - 1
            '            If regtext.EndsWith(PublicVariables(i).Split("=")(0)) Then
            '                ' Console.WriteLine(Textbox3.Selection.Start.iLine & ": " & regtext)
            '            End If
            '        Next

            '    End If
            ' End If
            'Else

            ' PrevLineNum = Textbox3.Selection.Start.iLine
            ' End If

        End If











    End Sub
    Private Sub Textbox3_AutoIndentNeeded(sender As Object, args As AutoIndentEventArgs) Handles Textbox3.AutoIndentNeeded
        'end of block
        If Regex.IsMatch(args.LineText, "^\s*(End|EndIf|Next|Loop)\b", RegexOptions.IgnoreCase) Then
            If Not Regex.IsMatch(args.LineText, "\""(([a-zA-Z0-9α-ωΑ-Ω ]|)+ (End|EndIf|Next|Loop)+ ([a-zA-Z0-9α-ωΑ-Ω ]|)+)""", RegexOptions.IgnoreCase) Then

                args.Shift = -args.TabLength
                args.ShiftNextLines = -args.TabLength
                Return

            End If
        End If
        ' then ...
        If Regex.IsMatch(args.LineText, "\b(Then)\s*\S+", RegexOptions.IgnoreCase) Then
            Return
        End If
        'start of operator block
        If Regex.IsMatch(args.LineText, "\b(Property|Enum|Event|Structure|Sub|Function|Namespace|Interface|Get)\b|(Set\s*\()", RegexOptions.IgnoreCase) Then ' meh :3

            args.ShiftNextLines = args.TabLength
            Return
        End If
        If Regex.IsMatch(args.LineText, "^\s*(If|While|For|Do|Try|With|Using|Select)\b", RegexOptions.IgnoreCase) Then ' meh :3

            args.ShiftNextLines = args.TabLength
            Return
        End If
        'Statements else, elseif, case etc
        If Regex.IsMatch(args.LineText, "^\s*(Else|ElseIf|Case|Catch|Finally)\b", RegexOptions.IgnoreCase) Then
            args.Shift = -args.TabLength
            Return
        End If
        If args.PrevLineText.TrimEnd().EndsWith("_") Then
            args.Shift = args.TabLength
            Return
        End If
    End Sub
    Private Function IsPublicOrNot() As Boolean
        Dim i As Integer = Textbox3.Selection.Start.iLine
        Do
            If Textbox3.GetLineText(i).Trim().StartsWith("End") Then
                Return False
                'ElseIf 

            End If

            i -= 1
        Loop Until i < 0
        Return True
    End Function

    Private Sub Textbox3_MouseClick(sender As Object, e As MouseEventArgs) Handles Textbox3.MouseClick
        ' Dim r As Range = Textbox3.Selection.Clone() : r.Expand()
        'If Not PrevlnnNum = Textbox3.Selection.Start.iLine Then
        '   popupMenu.Enabled = True
        'End If
        'PrevLineNum = Textbox3.Selection.Start.iLine
        ' currentLineIsSame = False

    End Sub
    Private Sub Textbox3_ToolTipNeeded(sender As Object, e As ToolTipNeededEventArgs) Handles Textbox3.ToolTipNeeded
        If Not String.IsNullOrEmpty(e.HoveredWord) Then
            Console.WriteLine(e.HoveredWord)
            Select Case e.HoveredWord
                Case Is = "Dim"
                    e.ToolTipTitle = e.HoveredWord & " Statment"
                    e.ToolTipText = "   Declares and allocates storage space for one or more variables." & vbNewLine & "   Dim Variable_Name As Variable_Type" ' e.HoveredWord  
                Case Is = "String"
                    e.ToolTipTitle = e.HoveredWord
                    e.ToolTipText = "   Represents text as a series of Unicode characters." & vbNewLine & "   Dim\Const Variable_Name As String"
            End Select
            'Dim
        End If
    End Sub
    Dim HideMap As Boolean = False
    Private Sub ToolStripButton10_Click(sender As Object, e As EventArgs) Handles ToolStripButton10.Click
        If HideMap = True Then
            'Textbox3.Size = New Size(1006, 460)
            Panel6.Hide()
            Textbox3.Size = New Size(Me.Size.Width - 24, Textbox3.Height)
            documentMap1.Hide()
            HideMap = False
        Else
            HideMap = True
            'Textbox3.Size = New Size(840, 460)
            Textbox3.Size = New Size(Me.Size.Width - 24, Textbox3.Height)
            Textbox3.Size = New Size(Textbox3.Size.Width - 166, Textbox3.Height)

            'Panel6.Size = New Size(166, 460)
            'Panel6.Location = New Point(852, 87)
            Panel6.BringToFront()

            documentMap1.BringToFront()
            Splitter1.BringToFront()
            RichTextBox1.BringToFront()
            Button10.BringToFront()
            documentMap1.Show()
            Panel6.Show()

            documentMap1.Select()
            ' Splitter2.BringToFront()
        End If

    End Sub

    Private Sub ToolStripButton10_DoubleClick(sender As Object, e As EventArgs) Handles ToolStripButton10.DoubleClick

    End Sub

    Private Sub ToolStripButton11_Click(sender As Object, e As EventArgs) Handles ToolStripButton11.Click
        If Not Label4.Text = Nothing Then Button7.PerformClick() ' γιατι ποτε δεν ξερεις Λολ :Ρ
    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        If System.Diagnostics.Process.GetProcessesByName("compiler").Length = 0 Then 'AndAlso File.Exists(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\Compile.log") = True Then ' there is chance running a compiler.exe but not this exe xD but anyway for now ---------------------------
            If File.Exists(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\Compile.log") = True Then
                If File.ReadAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\Compile.log").TrimStart.StartsWith("VB4A") Then


                    For Each ln As String In File.ReadAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\Compile.log").Split(vbNewLine)
                        ConsoleDouble += 1
                        RichTextBox1.AppendText("[" & ConsoleDouble & "] " & ln.Replace(vbNewLine, "").Replace(vbLf, "") & vbNewLine)
                        RichTextBox1.ScrollToCaret()
                    Next
                    ConsoleDouble += 1
                    RichTextBox1.AppendText("[" & ConsoleDouble & "] Compile Ended." & vbNewLine)

                    Timer3.Enabled = False
                    Panel3.Enabled = True

                    ShadowForm.PenC = New Pen(Color.FromArgb(24, 131, 215), 1)
                    ShadowForm.BackColor = Color.FromArgb(24, 131, 215)

                    ShadowForm.BringToFront()
                    Me.BringToFront()
                    Using New Centered_MessageBox(Me)

                        Select Case MsgBox("Wanna run the Apk on your phone?", vbYesNo, "Run Apk.")
                            Case MsgBoxResult.Yes
                                'If RichTextBox1.Visible = False Then Button10.PerformClick()
                                Button8.PerformClick()

                            Case MsgBoxResult.No
                        End Select
                    End Using
                    Me.BringToFront()
                    RichTextBox1.ScrollToCaret()

                End If
            End If
        End If
    End Sub




    Private Sub ToolStripButton12_Click(sender As Object, e As EventArgs) Handles ToolStripButton12.Click
        Textbox3.NavigateBackward()
    End Sub

    Private Sub ToolStripButton13_Click(sender As Object, e As EventArgs) Handles ToolStripButton13.Click
        Textbox3.NavigateForward()
    End Sub

    Private Sub TransparentDrawing2_Click(sender As Object, e As EventArgs) Handles TransparentDrawing2.Click
        If ToolStrip4.Visible = True Then
            ToolStrip4.Hide()
            ToolStrip5.Show()
            ' TransparentDrawing2.BringToFront()
        Else
            ToolStrip5.Hide()
            ToolStrip4.Show()
        End If
    End Sub

    Private Sub TransparentDrawing2_MouseMove(sender As Object, e As MouseEventArgs) Handles TransparentDrawing2.MouseMove

    End Sub

    Private Sub TransparentDrawing2_Paint(sender As Object, e As PaintEventArgs) Handles TransparentDrawing2.Paint

    End Sub
    Dim cliornot As Boolean = False

    Private Sub Form1_LocationChanged(sender As Object, e As EventArgs) Handles Me.LocationChanged


        If Me.Location.X < 0 Then Me.Location = New Point(0, Me.Location.Y) : Me.Refresh()
        If Me.Location.Y < 0 Then Me.Location = New Point(Me.Location.X, 0) : Me.Refresh()

        ShadowForm.Size = Me.Size + New Size(2, 2)
        ShadowForm.Location = Me.Location - New Point(1, 1)

        If cliornot = True Then ShadowForm.BringToFront() : Me.BringToFront() : cliornot = False
        ShadowForm.Refresh()


    End Sub

    Private Sub Form1_MaximizedBoundsChanged(sender As Object, e As EventArgs) Handles Me.MaximizedBoundsChanged
        'Me.Location = MinimizeBugPoint

    End Sub

    Private Sub Form1_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove

    End Sub



    Private Sub border1_MouseMove1(sender As Object, e As MouseEventArgs) Handles border1.MouseMove

    End Sub

    Private Sub TransparentDrawing3_MouseDown(sender As Object, e As MouseEventArgs) Handles TransparentDrawing3.MouseDown


    End Sub

    Private Sub TransparentDrawing3_MouseMove(sender As Object, e As MouseEventArgs) Handles TransparentDrawing3.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then

            Me.Size = New Size(MousePosition.X - Me.Location.X, Me.Size.Height)
            'Me.Location = New Point(MousePosition.X, Me.Location.Y)
            'PictureBox1.Size = New Size(Me.Size.Width - 697, PictureBox1.Size.Height)
            PictureBox1.Refresh()
            'Panel4.Size = New Size(Me.Size.Width, Panel4.Size.Height)
            Panel4.Refresh()
            ' border1.Size = New Size(Me.Size.Width, border1.Size.Height)
            border1.Refresh()
            'BorderText.Size = New Size(Me.Size.Width - 770, BorderText.Size.Height)
            BorderText.Refresh()

            'Combobox3.Size = New Size(Me.Size.Width - 776, Combobox3.Size.Height)
            Combobox3.Refresh()

            If PictureBox1.Size.Width < Panel5.Size.Width Then
                Panel5.Location = New Point(-1, Panel5.Location.Y)
            Else
                Panel5.Location = New Point(((PictureBox1.Size.Width - Panel5.Size.Width) \ 2) - 1, Panel5.Location.Y)
            End If

            Panel5.Refresh()
            PictureBox4.Location = Panel5.Location + New Point(0, 1)
            PictureBox4.Refresh()
            '  PictureBox4.Size = Panel5.Size + New Size(80, 0)
            'PictureBox4.Location = Panel5.Location - New Point(40, 0)
            'Button10.Location = New Point((Me.Size.Width - Button10.Size.Width) \ 2, Button10.Location.Y)
            Button10.Refresh()
            ' RichTextBox1.Size = New Size(Me.Size.Width - 10, RichTextBox1.Size.Height)
            RichTextBox1.Refresh()
            Splitter1.Refresh()

            Panel6.Refresh()
            Combobox1.Refresh()
            PropertyGrid1.Refresh()
            ToolStrip3.Refresh()

            If documentMap1.Visible = True Then

                ' Textbox3.Size = New Size(Me.Size.Width - 24, Textbox3.Size.Height)
                'Textbox3.Size = New Size(Textbox3.Size.Width - 166, Textbox3.Size.Height)
                documentMap1.Refresh()
                ' Else
                'Textbox3.Size = New Size(Me.Size.Width - 24, Textbox3.Size.Height)
            End If

            Textbox3.Refresh()

        End If

    End Sub


    Private Sub TransparentDrawing4_MouseMove(sender As Object, e As MouseEventArgs) Handles TransparentDrawing4.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then

            ' Dim loc2 As Point = Combobox1.Location
            Me.Size = New Size(Me.Size.Width, MousePosition.Y - Me.Location.Y)

            'ToolStrip3.Refresh()
            'Combobox1.Location = loc2
            'Combobox1.Refresh()

            ' PictureBox1.Size = New Size(PictureBox1.Size.Width, Me.Size.Height - 145)
            PictureBox1.Refresh()
            'Panel4.Size = New Size(Panel4.Size.Width, Me.Size.Height)
            'Panel4.Refresh()
            'border1.Size = New Size(border1.Size.Width, Me.Size.Height)
            border1.Refresh()
            'BorderText.Size = New Size(Me.Size.Width, BorderText.Size.Height)
            'BorderText.Refresh()
            If PictureBox1.Size.Height < Panel5.Size.Height Then
                Panel5.Location = New Point(Panel5.Location.X, -1)
            Else
                Panel5.Location = New Point(Panel5.Location.X, ((PictureBox1.Size.Height - Panel5.Size.Height) \ 2) - 1)
            End If


            PictureBox4.Location = Panel5.Location + New Point(0, 1)
            PictureBox4.Refresh()
            'PictureBox4.Size = Panel5.Size + New Size(40, 0)
            'PictureBox4.Location = Panel5.Location - New Point(60, 0)
            Panel5.Refresh()
            Button10.Refresh()
            Textbox3.Refresh()
            documentMap1.Refresh()
            'Panel3.Location = New Point()
            PropertyGrid1.Refresh()
            'Button12.Refresh()
            Label3.Refresh()
            Panel3.Refresh()
        End If
    End Sub

    Private Sub TransparentDrawing4_Paint(sender As Object, e As PaintEventArgs) Handles TransparentDrawing4.Paint

    End Sub



    'Private Sub TransparentDrawing1_SizeChanged(sender As Object, e As EventArgs) Handles TransparentDrawing1.SizeChanged
    '    If pnl IsNot Nothing AndAlso Not c Is Nothing AndAlso ResizeableControl.Namehelp = c.Name Then

    '        'pnl.Refresh()
    '        ''Form1.TransparentDrawing1.Refresh()
    '        ''pnl.Refresh()
    '        'Dim g As Graphics = pnl.CreateGraphics


    '        'g.DrawLine(grayPen, 0, c.Location.Y + c.Height, pnl.Width, c.Location.Y + c.Height)
    '        'g.DrawLine(grayPen, 0, c.Location.Y - 1, pnl.Width, c.Location.Y - 1)

    '        'g.DrawLine(grayPen, c.Location.X - 1, 0, c.Location.X - 1, pnl.Height)
    '        'g.DrawLine(grayPen, c.Location.X + c.Width, 0, c.Location.X + c.Width, pnl.Height)


    '    End If
    'End Sub

    Private Sub TransparentDrawing3_MouseUp(sender As Object, e As MouseEventArgs)

    End Sub
    Private Sub TransparentDrawing3_Paint(sender As Object, e As PaintEventArgs)

    End Sub
    Private Sub TransparentDrawing4_MouseDown(sender As Object, e As MouseEventArgs)

    End Sub
    'Dim FirsttimeBool As Boolean = False

    Private Sub Form1_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        ' MsgBox(231)
        ShadowForm.Size = Me.Size + New Size(2, 2)
        ShadowForm.Location = Me.Location - New Point(1, 1)
        ShadowForm.Refresh()


    End Sub



    Private Sub Label5_MouseUp(sender As Object, e As MouseEventArgs) Handles Label5.MouseUp
        cliornot = True
    End Sub

    Private Sub Panel4_MouseUp(sender As Object, e As MouseEventArgs) Handles Panel4.MouseUp
        cliornot = True
    End Sub



    Private Declare Function GetAsyncKeyState Lib "user32.dll" (ByVal vKey As System.Int32) As Integer
    Dim clickoutofform As Boolean = False
    Private Sub Timer4_Tick(sender As Object, e As EventArgs) Handles Timer4.Tick
        If clickoutofform = False Then
            If Cursor.Position.X < Me.Location.X - 1 Or Cursor.Position.X > Me.Location.X + ShadowForm.Width Or Cursor.Position.Y < Me.Location.Y - 1 Or Cursor.Position.Y > Me.Location.Y + ShadowForm.Height Then
                If GetAsyncKeyState(1) <> 0 Then
                    clickoutofform = True
                End If

            End If
        End If
    End Sub

    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles Me.Resize

    End Sub

    Private Sub Form1_Activated(sender As Object, e As EventArgs) Handles Me.Activated ' Omg ....!!! it is a BIG story lol
        If clickoutofform = True Then
            clickoutofform = False
            'ShadowForm.Show()
            ShadowForm.BringToFront()
            Me.BringToFront()

        End If
    End Sub

    Private Sub ToolStripMenuItem3_DropDownItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles ToolStripMenuItem3.DropDownItemClicked
        If Not e.ClickedItem Is ToolStripMenuItem3.DropDownItems.Item(0) Then
            ChangeProgramingLang(e.ClickedItem.Text)
            BuildAutocompleteMenu()
            'BuildAutocompleteMenuNoStadar()
        End If
    End Sub

    Public ButtonListToReplace As New List(Of String)
    Public CanvasListToReplace As New List(Of String)
    Public CheckBoxListToReplace As New List(Of String)
    Public LabelListToReplace As New List(Of String)
    Public ListviewListToReplace As New List(Of String)
    Public PanelListToReplace As New List(Of String)
    Public PasswordTextBoxListToReplace As New List(Of String)
    Public PictureBoxListToReplace As New List(Of String)
    Public ProgressBarListToReplace As New List(Of String)
    Public RadioButtonListToReplace As New List(Of String)
    Public TextBoxListToReplace As New List(Of String)
    Public TimerListToReplace As New List(Of String)
    Public ComboBoxListToReplace As New List(Of String)
    Public VB4WebListToReplace As New List(Of String)
    Public ApplicationListToReplace As New List(Of String)
    Public ArraysListToReplace As New List(Of String)
    Public VB4AInputListToReplace As New List(Of String)
    Public FilesListToReplace As New List(Of String)
    Public MathListToReplace As New List(Of String)
    Public AcceleromaterSenorListToReplace As New List(Of String)
    Public LocationSensorListToReplace As New List(Of String)
    Public OrientationSensorListToReplace As New List(Of String)
    Public PhoneListToReplace As New List(Of String)
    Public ComponentListToReplace As New List(Of String)
    Public DatesListToReplace As New List(Of String)
    Public StringsListToReplace As New List(Of String)
    Public ConversionsListToReplace As New List(Of String)
    Public Base64ListToReplace As New List(Of String)
    Public StatisticsListToReplace As New List(Of String)
    Public MatrixListToReplace As New List(Of String)

    Public ButtonList As New List(Of String) ' yes i knowww!!! i am sure that you are now like ...wtf is happening here ?!.... i had to translate everithing so .... first i used one list with this way: Form1.methodsToolTipAll.GetRange(0, 8).ToArray() , but adding and adding new commands i had  to change too many times the numbers inside Getrange....!!!
    Public CanvasList As New List(Of String)
    Public CheckBoxList As New List(Of String)
    Public LabelList As New List(Of String)
    Public ListviewList As New List(Of String)
    Public PanelList As New List(Of String)
    Public PasswordTextBoxList As New List(Of String)
    Public PictureBoxList As New List(Of String)
    Public ProgressBarList As New List(Of String)
    Public RadioButtonList As New List(Of String)
    Public TextBoxList As New List(Of String)
    Public TimerList As New List(Of String)
    Public ComboBoxList As New List(Of String)
    Public VB4WebList As New List(Of String)
    Public ApplicationList As New List(Of String)
    Public ArraysList As New List(Of String)
    Public VB4AInputList As New List(Of String)
    Public FilesList As New List(Of String)
    Public MathList As New List(Of String)
    Public AcceleromaterSenorList As New List(Of String)
    Public LocationSensorList As New List(Of String)
    Public OrientationSensorList As New List(Of String)
    Public PhoneList As New List(Of String)
    Public ComponentList As New List(Of String)
    Public DatesList As New List(Of String)
    Public StringsList As New List(Of String)
    Public ConversionsList As New List(Of String)
    Public Base64List As New List(Of String)
    Public StatisticsList As New List(Of String)
    Public MatrixList As New List(Of String)

    Public CotntroltypesList As New List(Of String)
    Public CotntroltypesToReplaceList As New List(Of String)

    Public ControlNameOfPlangFiles As String = String.Empty
    Public EndBracket As String = String.Empty ' public because i am using it and as seperator in lists

    Dim FoldingMarkerStart As String = String.Empty
    Dim FoldingMarkerEnd As String = String.Empty
    Dim AnyTXTinside As New List(Of String)
    Dim commafunc As String = String.Empty

    Private Sub ChangeProgramingLang(lang As String)
        If File.Exists(CurrentHardDriver & "VB4Android\Programing Languages\" & lang & ".pLang") = True Then

            CotntroltypesList.Clear()
            CotntroltypesToReplaceList.Clear()

            ButtonListToReplace.Clear()
            CanvasListToReplace.Clear()
            CheckBoxListToReplace.Clear()
            LabelListToReplace.Clear()
            ListviewListToReplace.Clear()
            PanelListToReplace.Clear()
            PasswordTextBoxListToReplace.Clear()
            PictureBoxListToReplace.Clear()
            ProgressBarListToReplace.Clear()
            RadioButtonListToReplace.Clear()
            TextBoxListToReplace.Clear()
            TimerListToReplace.Clear()
            ComboBoxListToReplace.Clear()
            VB4WebListToReplace.Clear()
            ApplicationListToReplace.Clear()
            ArraysListToReplace.Clear()
            VB4AInputListToReplace.Clear()
            FilesListToReplace.Clear()
            MathListToReplace.Clear()
            AcceleromaterSenorListToReplace.Clear()
            LocationSensorListToReplace.Clear()
            OrientationSensorListToReplace.Clear()
            PhoneListToReplace.Clear()
            ComponentListToReplace.Clear()
            DatesListToReplace.Clear()
            StringsListToReplace.Clear()
            ConversionsListToReplace.Clear()
            Base64ListToReplace.Clear()
            StatisticsListToReplace.Clear()
            MatrixListToReplace.Clear()

            ButtonList.Clear()
            CanvasList.Clear()
            CheckBoxList.Clear()
            LabelList.Clear()
            ListviewList.Clear()
            PanelList.Clear()
            PasswordTextBoxList.Clear()
            PictureBoxList.Clear()
            ProgressBarList.Clear()
            RadioButtonList.Clear()
            TextBoxList.Clear()
            TimerList.Clear()
            ComboBoxList.Clear()
            VB4WebList.Clear()
            ApplicationList.Clear()
            ArraysList.Clear()
            VB4AInputList.Clear()
            FilesList.Clear()
            MathList.Clear()
            AcceleromaterSenorList.Clear()
            LocationSensorList.Clear()
            OrientationSensorList.Clear()
            PhoneList.Clear()
            ComponentList.Clear()
            DatesList.Clear()
            StringsList.Clear()
            ConversionsList.Clear()
            Base64List.Clear()
            StatisticsList.Clear()
            MatrixList.Clear()


            Dim TextForListItem As String = String.Empty
            Dim StartBracket As String = String.Empty
            EndBracket = String.Empty

            For Each ln As String In File.ReadAllLines(CurrentHardDriver & "VB4Android\Programing Languages\" & lang & ".pLang") ' i didnt know that this existed xD  ... i was reading lines with this way File.ReadAllText("...").Split(vbnewline)    [9/8/2015]
                If ln.Split("=")(0) = "start" Then StartBracket = ln.Split("=")(1)
                If ln.Split("=")(0) = "Control" Then ControlNameOfPlangFiles = ln.Split("=")(1)
                If ln.Split("=")(0) = "SetFolding" Then If ln.Split("=")(1) = "" Then FoldingMarkerStart = String.Empty : FoldingMarkerEnd = String.Empty Else FoldingMarkerStart = ln.Split("=")(1).Split(",")(0) : FoldingMarkerEnd = ln.Split("=")(1).Split(",")(1)
                If ln.Split("=")(0) = "AnyTXTinside" Then
                    For Each lln As String In ln.Split("=")(1).Split(",")
                        AnyTXTinside.Add(lln)
                    Next
                End If
                If ln.Split("=")(0) = "commafunc" Then commafunc = ln.Split("=")(1)
                If ln.Split("=")(0) = "end" Then EndBracket = ln.Split("=")(1) : Exit For
            Next


            Dim StartEndControlTypeEntry As Boolean = False
            Dim StartEndKeywordTypeEntry As Boolean = False
            Dim StartEndControlEntry As Boolean = False
            Dim BracketsEndStart As Boolean = False
            Dim StartEndControl As Boolean = False
            Dim NameOfKeywoType As String = String.Empty

            Dim ListOfall As New List(Of String)
            Dim ListOfallToReplace As New List(Of String)

            Dim i As Integer = 0

            For Each ln As String In File.ReadAllLines(CurrentHardDriver & "VB4Android\Programing Languages\" & lang & ".pLang")

                If ln = "</Controls>" Then StartEndControlEntry = False

                If StartEndControlEntry = True AndAlso Not ln.Trim = "" Then

                    If ln.Trim.StartsWith("<") AndAlso Not ln.Trim.StartsWith("</") Then
                        StartEndControl = True
                        Select Case ln.Trim.Replace("<", "").Replace(">", "")
                            Case Is = "Button" : ListOfall = ButtonList : ListOfallToReplace = ButtonListToReplace
                            Case Is = "Canvas" : ListOfall = CanvasList : ListOfallToReplace = CanvasListToReplace
                            Case Is = "CheckBox" : ListOfall = CheckBoxList : ListOfallToReplace = CheckBoxListToReplace
                            Case Is = "Label" : ListOfall = LabelList : ListOfallToReplace = LabelListToReplace
                            Case Is = "Listview" : ListOfall = ListviewList : ListOfallToReplace = ListviewListToReplace
                            Case Is = "Panel" : ListOfall = PanelList : ListOfallToReplace = PanelListToReplace
                            Case Is = "PasswordTextBox" : ListOfall = PasswordTextBoxList : ListOfallToReplace = PasswordTextBoxListToReplace
                            Case Is = "PictureBox" : ListOfall = PictureBoxList : ListOfallToReplace = PictureBoxListToReplace
                            Case Is = "ProgressBar" : ListOfall = ProgressBarList : ListOfallToReplace = ProgressBarListToReplace
                            Case Is = "RadioButton" : ListOfall = RadioButtonList : ListOfallToReplace = RadioButtonListToReplace
                            Case Is = "TextBox" : ListOfall = TextBoxList : ListOfallToReplace = TextBoxListToReplace
                            Case Is = "Timer" : ListOfall = TimerList : ListOfallToReplace = TimerListToReplace
                            Case Is = "ComboBox" : ListOfall = ComboBoxList : ListOfallToReplace = ComboBoxListToReplace
                            Case Is = "VB4AWeb" : ListOfall = VB4WebList : ListOfallToReplace = VB4WebListToReplace
                        End Select
                    ElseIf ln.Trim.StartsWith("</") Then : StartEndControl = False
                    End If

                    If StartEndControl = True Then

                        If ln.Trim.StartsWith(ControlNameOfPlangFiles) AndAlso BracketsEndStart = False Or ln.Trim = ControlNameOfPlangFiles AndAlso BracketsEndStart = False Then ' [13/8/2015] :') !!! it worked from the first time 
                            Dim lnn As String = ln.Split("=")(0).Trim
                            Dim lnn2 As String = ln.Split("=")(1).Trim

                            lnn = lnn.Replace(commafunc, "") ' this line is better to be set here , before 'for each' because it uses less cpu  but  AnyTXTinside items might be anything ,... like #1,11,... but anyway i dont whink that there are so idiot people to do this :P
                            'Dim lnth As Integer = 0

                            If lnn2.Contains(AnyTXTinside(0).ToString) Then
                                For Each item As String In AnyTXTinside.ToArray
                                    lnn = lnn.Replace(item, "")
                                Next
                                Dim t As String = String.Empty
                                Dim intt As New List(Of Integer)
                                Dim intt2 As New List(Of Integer)

                                For Each chr As String In ln.Split("=")(1).Trim.ToArray
                                    t &= chr
                                    For i = 0 To AnyTXTinside.Count - 1
                                        If t.EndsWith(AnyTXTinside(i).ToString) Then intt.Add(i)
                                    Next
                                Next
                                t = String.Empty

                                For i = 0 To AnyTXTinside.Count - 1
                                    intt2.Add(i)
                                Next

                                'If Not intt Is intt2 Then
                                Dim b As String = String.Empty
                                Dim c As String = String.Empty
                                For i = 0 To intt.Count - 1
                                    b &= AnyTXTinside(intt(i).ToString) & commafunc
                                    c &= AnyTXTinside(intt2(i).ToString) & commafunc
                                Next
                                lnn2 = lnn2.Replace(c.Remove(c.Length - 1, 1), b.Remove(c.Length - 1, 1))
                                'Console.WriteLine(lnn2 & "=" & lnn) ' I Need This Console for some checks ;)
                                ' End If


                            End If
                            'Console.WriteLine(lnn2)
                            ListOfallToReplace.Add(lnn2)
                            TextForListItem = (lnn) : i = 0
                        End If

                        If ln.Trim = EndBracket Then BracketsEndStart = False : ListOfall.Add(TextForListItem)

                        If BracketsEndStart = True Then : i += 1 ' first lines of file uk george :P
                            If i = 1 Then TextForListItem &= EndBracket & ln.Split("=")(1).Trim
                            If i = 2 Then TextForListItem &= EndBracket & ln.Remove(0, 1) & EndBracket
                            If i > 2 Then TextForListItem &= vbNewLine & ln.Remove(0, 1)
                        End If

                        If ln.Trim = StartBracket Then BracketsEndStart = True

                    End If
                End If

                If ln = "<Controls>" Then StartEndControlEntry = True

                If ln = "</ControlTypes>" Then StartEndControlTypeEntry = False

                If StartEndControlTypeEntry = True AndAlso Not ln.Trim = "" Then
                    If ln.StartsWith("Button =") Or ln.StartsWith("Canvas =") Or ln.StartsWith("CheckBox =") Or ln.StartsWith("Label =") Or ln.StartsWith("Listview =") Or ln.StartsWith("Panel =") Or ln.StartsWith("PasswordTextBox =") Or
                       ln.StartsWith("PictureBox =") Or ln.StartsWith("ProgressBar =") Or ln.StartsWith("RadioButton =") Or ln.StartsWith("TextBox =") Or ln.StartsWith("Timer =") Or ln.StartsWith("ComboBox =") Or ln.StartsWith("VB4AWeb =") Then
                        If BracketsEndStart = False Then : i = 0
                            CotntroltypesToReplaceList.Add(ln.Split("=")(1).Trim)
                            TextForListItem = (ln.Split("=")(0).Trim)
                        End If
                    End If

                    If ln.Trim = EndBracket Then BracketsEndStart = False : CotntroltypesList.Add(TextForListItem)

                    If BracketsEndStart = True Then : i += 1
                        If i = 1 Then TextForListItem &= EndBracket & ln.Split("=")(1).Trim
                        If i = 2 Then TextForListItem &= EndBracket & ln.Remove(0, 1) & EndBracket
                        If i > 2 Then TextForListItem &= vbNewLine & ln.Remove(0, 1)
                    End If

                    If ln.Trim = StartBracket Then BracketsEndStart = True


                End If

                If ln = "<ControlTypes>" Then StartEndControlTypeEntry = True : TextForListItem = String.Empty : BracketsEndStart = False '  TextForListItem = String.Empty just in case ....


                If ln = "</TypesOfKeywords>" Then StartEndKeywordTypeEntry = False

                If StartEndKeywordTypeEntry = True AndAlso Not ln.Trim = "" Then

                    If ln.Trim.StartsWith("<") AndAlso Not ln.Trim.StartsWith("</") Then
                        StartEndControl = True : NameOfKeywoType = ln.Trim.Replace("<", "").Replace(">", "")
                        Select Case ln.Trim.Replace("<", "").Replace(">", "")
                            Case Is = "Application" : ListOfall = ApplicationList : ListOfallToReplace = ApplicationListToReplace
                            Case Is = "Arrays" : ListOfall = ArraysList : ListOfallToReplace = ArraysListToReplace
                            Case Is = "VB4AInput" : ListOfall = VB4AInputList : ListOfallToReplace = VB4AInputListToReplace
                            Case Is = "Files" : ListOfall = FilesList : ListOfallToReplace = FilesListToReplace
                            Case Is = "Math" : ListOfall = MathList : ListOfallToReplace = MathListToReplace
                            Case Is = "AcceleromaterSenor" : ListOfall = AcceleromaterSenorList : ListOfallToReplace = AcceleromaterSenorListToReplace
                            Case Is = "LocationSensor" : ListOfall = LocationSensorList : ListOfallToReplace = LocationSensorListToReplace
                            Case Is = "OrientationSensor" : ListOfall = OrientationSensorList : ListOfallToReplace = OrientationSensorListToReplace
                            Case Is = "Phone" : ListOfall = PhoneList : ListOfallToReplace = PhoneListToReplace
                            Case Is = "Component" : ListOfall = ComponentList : ListOfallToReplace = ComponentListToReplace
                            Case Is = "Dates" : ListOfall = DatesList : ListOfallToReplace = DatesListToReplace
                            Case Is = "Strings" : ListOfall = StringsList : ListOfallToReplace = StringsListToReplace
                            Case Is = "Conversions" : ListOfall = ConversionsList : ListOfallToReplace = ConversionsListToReplace
                            Case Is = "Base64" : ListOfall = Base64List : ListOfallToReplace = Base64ListToReplace
                            Case Is = "Statistics" : ListOfall = StatisticsList : ListOfallToReplace = StatisticsListToReplace
                            Case Is = "Matrix" : ListOfall = MatrixList : ListOfallToReplace = MatrixListToReplace
                        End Select
                    ElseIf ln.Trim.StartsWith("</") Then : StartEndControl = False
                    End If

                    If StartEndControl = True Then

                        If ln.Trim.StartsWith(NameOfKeywoType) AndAlso BracketsEndStart = False Or ln.Trim = NameOfKeywoType AndAlso BracketsEndStart = False Then
                            ListOfallToReplace.Add(ln.Split("=")(1).Trim)
                            TextForListItem = (ln.Split("=")(0).Trim) : i = 0
                        End If

                        If ln.Trim = EndBracket Then BracketsEndStart = False : ListOfall.Add(TextForListItem)

                        If BracketsEndStart = True Then : i += 1
                            If i = 1 Then TextForListItem &= EndBracket & ln.Split("=")(1).Trim
                            If i = 2 Then TextForListItem &= EndBracket & ln.Remove(0, 1) & EndBracket
                            If i > 2 Then TextForListItem &= vbNewLine & ln.Remove(0, 1)
                        End If

                        If ln.Trim = StartBracket Then BracketsEndStart = True

                    End If
                End If

                If ln = "<TypesOfKeywords>" Then StartEndKeywordTypeEntry = True

            Next

            ' For i = 0 To CotntroltypesToReplaceList.Count - 1
            '    Console.WriteLine(CotntroltypesToReplaceList.Item(i) & " = " & CotntroltypesList.Item(i))
            'Next

        Else
            Using New Centered_MessageBox(Me)
                MsgBox("Programing Language '" & lang & "' Doesn't Exist.", MsgBoxStyle.Exclamation, "Doesn't Exist.")
            End Using
        End If
    End Sub






    Private Sub ListView1_MouseDown(sender As Object, e As MouseEventArgs) Handles ListView1.MouseDown
        If ListView1.SelectedItems.Count > 0 Then ListView1.SelectedItems.Clear() ' it Worked with the First TRY :DDDDDDDDDD xD  [10/8/2015]         [DD/MM/YY]

    End Sub

    Private Sub ListView1_MouseClick(sender As Object, e As MouseEventArgs) Handles ListView1.MouseClick
        'MsgBox(ListView1.FocusedItem.Text)
        If ListView1.FocusedItem.Text = ListView1.Items.Item(3).Text Or ListView1.FocusedItem.Text = ListView1.Items.Item(11).Text Or ListView1.FocusedItem.Text = ListView1.Items.Item(6).Text Then
            MsgBox("'" & ListView1.Items.Item(ListView1.FocusedItem.Index).Text.Trim & "' Control is not Supported yet.")
            ListView1.SelectedItems.Clear() ' Really !? ... :P MS i can't believe you... CLEAR to deselect .... WHyyy!?!? xD
        End If

    End Sub

    Private Sub ToolStripButton16_DropDownOpening(sender As Object, e As EventArgs) Handles ToolStripButton16.DropDownOpening
        ToolStripButton16.DropDownItems.Clear()
        For Each bookmark In Textbox3.Bookmarks
            Dim item = ToolStripButton16.DropDownItems.Add("BookMark At Line " & bookmark.LineIndex + 1)
            item.Tag = bookmark
        Next
    End Sub

    Private Sub ToolStripButton14_Click(sender As Object, e As EventArgs) Handles ToolStripButton14.Click
        Textbox3.Bookmarks.Add(Textbox3.Selection.Start.iLine)
    End Sub

    Private Sub ToolStripMenuItem4_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem4.Click
        If ToolStripMenuItem4.Text = "[✓] IME mode" Then
            Textbox3.ImeMode = False
            ToolStripMenuItem4.Text = "[X] IME mode"
        Else
            Textbox3.ImeMode = True
            ToolStripMenuItem4.Text = "[✓] IME mode"
        End If
    End Sub

    Private Sub MoreSettingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MoreSettingToolStripMenuItem.Click
        SettingsForm.Show()
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        ' MsgBox(Me.Size.ToString)
    End Sub

    Private Sub ChangeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChangeToolStripMenuItem.Click

        If ToolStripTextBox3.Text.Trim = Nothing Then
            Using New Centered_MessageBox(Me)
                MsgBox("Width Box is Empty", MsgBoxStyle.Information, "No Width.")
            End Using : Exit Sub
        End If

        If ToolStripTextBox4.Text.Trim = Nothing Then
            Using New Centered_MessageBox(Me)
                MsgBox("Height Box is Empty", MsgBoxStyle.Information, "No Height.")
            End Using : Exit Sub
        End If

        XofFormApp = CInt(ToolStripTextBox3.Text)
        YofFormApp = CInt(ToolStripTextBox4.Text)

        Panel5.Size = New Size(XofFormApp + 2, YofFormApp + 2)
        Panel5.Location = New Point(((PictureBox1.Size.Width - Panel5.Size.Width) \ 2) - 1, ((PictureBox1.Size.Height - Panel5.Size.Height) \ 2) - 1)

        PictureBox4.Location = Panel5.Location + New Point(0, 1)
        PictureBox4.Size = New Size(XofFormApp, YofFormApp)


        For Each ln As String In My.Computer.FileSystem.ReadAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj").Split(vbNewLine)
            ln = ln.Replace(vbLf, Nothing)
            If ln.StartsWith("ScreenSize") Then My.Computer.FileSystem.WriteAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj", My.Computer.FileSystem.ReadAllText(CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & "\" & Label4.Text.Replace("- ( ", "").Replace(" )", "") & ".VB4AProj").Replace("ScreenSize=" & ln.Replace("ScreenSize=", Nothing), "ScreenSize=" & XofFormApp & "x" & YofFormApp), False) : Exit Sub
        Next

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub



    Private Sub ToolStripButton15_Click(sender As Object, e As EventArgs) Handles ToolStripButton15.Click
        Textbox3.Bookmarks.Remove(Textbox3.Selection.Start.iLine)
    End Sub

    Private Sub ToolStripButton16_DropDownItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles ToolStripButton16.DropDownItemClicked

        TryCast(e.ClickedItem.Tag, Bookmark).DoVisible()

    End Sub
    ' Dim components As IContainer = Nothing


    'Dim label1 As Label

    'Dim KeywordsStyle As Style = New TextStyle(Brushes.Green, Nothing, FontStyle.Regular)

    'Dim FunctionNameStyle As Style = New TextStyle(Brushes.Blue, Nothing, FontStyle.Regular)
    Private Sub Textbox3_TextChangedDelayed(sender As Object, e As TextChangedEventArgs) Handles Textbox3.TextChangedDelayed



        e.ChangedRange.ClearStyle(New Style() {BrownStyle, GreenStyle, MagentaStyle, BlueStyle, Green2style, SaddleBrownStyle})

        'string highlighting
        e.ChangedRange.SetStyle(BrownStyle, StringRegex)
        'comment highlighting
        e.ChangedRange.SetStyle(GreenStyle, CommentRegex)
        'number highlighting
        e.ChangedRange.SetStyle(MagentaStyle, NumberRegex)
        'keyword highlighting
        e.ChangedRange.SetStyle(BlueStyle, KeywordRegex)

        e.ChangedRange.SetStyle(Green2style, SpecialKeywordRegex)

        e.ChangedRange.SetStyle(SaddleBrownStyle, Special2KeywordRegex)


        'Me.Textbox3.Range.ClearStyle(New Style() {KeywordsStyle, FunctionNameStyle})
        'Me.Textbox3.Range.SetStyle(KeywordsStyle, "\b(and|eval|else|if|lambda|or|set|defun)\b", RegexOptions.IgnoreCase)
        'For Each found As Range In Me.Textbox3.GetRanges("\b(defun|DEFUN)\s+(?<range>\w+)\b")
        '    Me.Textbox3.Range.SetStyle(FunctionNameStyle, "\b" + found.Text + "\b")
        'Next
    End Sub

    Private Sub LiveSizeViewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LiveSizeViewToolStripMenuItem.Click

    End Sub

    Sub Ονομα_Sub() ' Μια "συναρτιση" ειναι χωρις return τεσπα  ' 30/10/2016 ΧΑΧΑΧΑΧΑΧΑΧΑ αυτο το ειχα γραψει γιατι ειχα βαλει ενα στοιχημα με μια κοπελα αν μπορω να κανω μια αλλη κοπελια να καταλαβει εστω κι ενα πραγμα απο αυτα που γραφω λολ 

        Dim i As Integer = 0 ' οριζω μια μετεβλιτη

        If i = 0 Then ' Αν το ι ειναι ισο με 0 τοτε κανε οτι σου λεει απο κατω

            Console.WriteLine("το ι ειναι 0")

        Else ' Αλλιως 

            Console.WriteLine("το ι δεν ειναι 0")

        End If

    End Sub ' ΤΕλος "συναρτισης"

    Private Sub Textbox3_RegionChanged(sender As Object, e As EventArgs) Handles Textbox3.RegionChanged

    End Sub

    Private Sub Textbox3_VisibleRangeChangedDelayed(sender As Object, e As EventArgs) Handles Textbox3.VisibleRangeChangedDelayed
        Textbox3.VisibleRange.ClearFoldingMarkers()
        'Textbox3.VisibleRange.SetFoldingMarkers 
        'e.ChangedRange.ClearFoldingMarkers() ' kai gia ta comments (') na valw (?<=^([^""]|""[^""]*"")*)
        Textbox3.VisibleRange.SetFoldingMarkers("(?<=^([^""]|""[^""]*"")*)(?<=^([^']|'[^\n]*\n)*)^\s*(?<range>While)[ \t]+\S+", "(?<=^([^""]|""[^""]*"")*)(?<=^([^']|'[^\n]*\n)*)^\s*(?<range>End While)\b", RegexOptions.Multiline Or RegexOptions.IgnoreCase)
        Textbox3.VisibleRange.SetFoldingMarkers("(?<=^([^""]|""[^""]*"")*)(?<=^([^']|'[^\n]*\n)*)\b(Property)[ \t]+\S+", "(?<=^([^""]|""[^""]*"")*)(?<=^([^']|'[^\n]*\n)*)\bEnd (Property)\b", RegexOptions.IgnoreCase)
        Textbox3.VisibleRange.SetFoldingMarkers("(?<=^([^""]|""[^""]*"")*)(?<=^([^']|'[^\n]*\n)*)\b(Sub|Function|Event)[ \t]+([^\s']+|[^\s""]+)", "(?<=^([^""]|""[^""]*"")*)(?<=^([^']|'[^\n]*\n)*)\bEnd (Sub|Function|Event)\b", RegexOptions.IgnoreCase)
        Textbox3.VisibleRange.SetFoldingMarkers("(?<=^([^""]|""[^""]*"")*)(?<=^([^']|'[^\n]*\n)*)(\r|\n|^)[ \t]*(?<range>Get|Set)[ \t]*(\r|\n|$)", "(?<=^([^""]|""[^""]*"")*)(?<=^([^']|'[^\n]*\n)*)\bEnd (Get|Set)\b", RegexOptions.IgnoreCase)
        Textbox3.VisibleRange.SetFoldingMarkers("(?<=^([^""]|""[^""]*"")*)(?<=^([^']|'[^\n]*\n)*)^\s*(?<range>For|For\s+Each)\b", "(?<=^([^""]|""[^""]*"")*)(?<=^([^']|'[^\n]*\n)*)^\s*(?<range>Next)\b", RegexOptions.Multiline Or RegexOptions.IgnoreCase)
        Textbox3.VisibleRange.SetFoldingMarkers("(?<=^([^""]|""[^""]*"")*)(?<=^([^']|'[^\n]*\n)*)^\s*(?<range>Do)\b", "(?<=^([^""]|""[^""]*"")*)(?<=^([^']|'[^\n]*\n)*)^\s*(?<range>Loop)\b", RegexOptions.Multiline Or RegexOptions.IgnoreCase)
        Textbox3.VisibleRange.SetFoldingMarkers("(?<=^([^""]|""[^""]*"")*)(?<=^([^']|'[^\n]*\n)*)\b(If) Then[ \t]+([^\s']+|[^\s""]+)", "(?<=^([^""]|""[^""]*"")*)(?<=^([^']|'[^\n]*\n)*)\b(Then)[ \t]+([^\s']+|[^\s""]+)", RegexOptions.IgnoreCase) ' Or RegexOptions.Singleline  ? ' Fix the  end when commenting
        Textbox3.VisibleRange.SetFoldingMarkers("(?<=^([^""]|""[^""]*"")*)(?<=^([^']|'[^\n]*\n)*)\b(If)[ \t]+([^\s']+|[^\s""]+)", "(?<=^([^""]|""[^""]*"")*)(?<=^([^']|'[^\n]*\n)*)\b(End If)\b", RegexOptions.IgnoreCase)





        'e.ChangedRange.ClearFoldingMarkers() ' kai gia ta comments (') na valw (?<=^([^""]|""[^""]*"")*)
        '  e.ChangedRange.SetFoldingMarkers("(?<=^([^""]|""[^""]*"")*)^\s*(?<range>While)[ \t]+\S+", "(?<=^([^""]|""[^""]*"")*)^\s*(?<range>End While)\b", RegexOptions.Multiline Or RegexOptions.IgnoreCase)
        '  e.ChangedRange.SetFoldingMarkers("(?<=^([^""]|""[^""]*"")*)\b(Property)[ \t]+\S+", "(?<=^([^""]|""[^""]*"")*)\bEnd (Property)\b", RegexOptions.IgnoreCase)
        'e.ChangedRange.SetFoldingMarkers("(?<=^([^""]|""[^""]*"")*)\b(Sub|Function|Event)[ \t]+[^\s']+", "(?<=^([^""]|""[^""]*"")*)\bEnd (Sub|Function|Event)\b", RegexOptions.IgnoreCase)
        '   e.ChangedRange.SetFoldingMarkers("(?<=^([^""]|""[^""]*"")*)(\r|\n|^)[ \t]*(?<range>Get|Set)[ \t]*(\r|\n|$)", "(?<=^([^""]|""[^""]*"")*)\bEnd (Get|Set)\b", RegexOptions.IgnoreCase)
        '   e.ChangedRange.SetFoldingMarkers("(?<=^([^""]|""[^""]*"")*)^\s*(?<range>For|For\s+Each)\b", "(?<=^([^""]|""[^""]*"")*)^\s*(?<range>Next)\b", RegexOptions.Multiline Or RegexOptions.IgnoreCase)
        'e.ChangedRange.SetFoldingMarkers("(?<=^([^""]|""[^""]*"")*)^\s*(?<range>Do)\b", "(?<=^([^""]|""[^""]*"")*)^\s*(?<range>Loop)\b", RegexOptions.Multiline Or RegexOptions.IgnoreCase)
        ' e.ChangedRange.SetFoldingMarkers("(?<=^([^""]|""[^""]*"")*)\b(If)[ \t]+([^\s']+|[^\s""]+)", "(?<=^([^""]|""[^""]*"")*)\b(End If)\b", RegexOptions.IgnoreCase)


    End Sub

    Private Sub ToolStripTextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ToolStripTextBox3.KeyPress
        If (Microsoft.VisualBasic.Asc(e.KeyChar) < 47) Or (Microsoft.VisualBasic.Asc(e.KeyChar) > 58) Then
            e.Handled = True
        End If
        If (Microsoft.VisualBasic.Asc(e.KeyChar) = 8) Then
            e.Handled = False
        End If
    End Sub

    Private Sub ToolStripTextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ToolStripTextBox4.KeyPress
        If (Microsoft.VisualBasic.Asc(e.KeyChar) < 47) Or (Microsoft.VisualBasic.Asc(e.KeyChar) > 58) Then
            e.Handled = True
        End If
        If (Microsoft.VisualBasic.Asc(e.KeyChar) = 8) Then
            e.Handled = False
        End If
    End Sub
End Class
'TextBox3.Redo()
'TextBox3.Undo()



' MSGBOX STHN MESH


Public Class Centered_MessageBox
    Implements IDisposable
    Private mTries As Integer = 0
    Private mOwner As Form

    Public Sub New(owner As Form)
        mOwner = owner
        owner.BeginInvoke(New MethodInvoker(AddressOf findDialog))
    End Sub

    Private Sub findDialog()
        If mTries < 0 Then
            Return
        End If
        Dim callback As New EnumThreadWndProc(AddressOf checkWindow)
        If EnumThreadWindows(GetCurrentThreadId(), callback, IntPtr.Zero) Then
            If System.Threading.Interlocked.Increment(mTries) < 10 Then
                mOwner.BeginInvoke(New MethodInvoker(AddressOf findDialog))
            End If
        End If

    End Sub
    Private Function checkWindow(hWnd As IntPtr, lp As IntPtr) As Boolean
        Dim sb As New StringBuilder(260)
        GetClassName(hWnd, sb, sb.Capacity)
        If sb.ToString() <> "#32770" Then
            Return True
        End If
        Dim frmRect As New Rectangle(mOwner.Location, mOwner.Size)
        Dim dlgRect As RECT
        GetWindowRect(hWnd, dlgRect)
        MoveWindow(hWnd, frmRect.Left + (frmRect.Width - dlgRect.Right + dlgRect.Left) \ 2, frmRect.Top + (frmRect.Height - dlgRect.Bottom + dlgRect.Top) \ 2, dlgRect.Right - dlgRect.Left, dlgRect.Bottom - dlgRect.Top, True)
        If Form1.Dnt = True Then
            Form1.Dnt = False
            DonateFRM.Size = New Size(245, 245)
            DonateFRM.Location = New Point((frmRect.Left + (frmRect.Width - dlgRect.Right + dlgRect.Left) \ 2) - DonateFRM.Size.Width, (frmRect.Top + (frmRect.Height - dlgRect.Bottom + dlgRect.Top) \ 2))
        End If
        Return False
    End Function
    Public Sub Dispose() Implements IDisposable.Dispose
        mTries = -1
    End Sub
    Private Delegate Function EnumThreadWndProc(hWnd As IntPtr, lp As IntPtr) As Boolean
    <DllImport("user32.dll")>
    Private Shared Function EnumThreadWindows(tid As Integer, callback As EnumThreadWndProc, lp As IntPtr) As Boolean
    End Function
    <DllImport("kernel32.dll")>
    Private Shared Function GetCurrentThreadId() As Integer
    End Function
    <DllImport("user32.dll")>
    Private Shared Function GetClassName(hWnd As IntPtr, buffer As StringBuilder, buflen As Integer) As Integer
    End Function
    <DllImport("user32.dll")>
    Private Shared Function GetWindowRect(hWnd As IntPtr, ByRef rc As RECT) As Boolean
    End Function
    <DllImport("user32.dll")>
    Private Shared Function MoveWindow(hWnd As IntPtr, x As Integer, y As Integer, w As Integer, h As Integer, repaint As Boolean) As Boolean
    End Function
    Private Structure RECT
        Public Left As Integer
        Public Top As Integer
        Public Right As Integer
        Public Bottom As Integer
    End Structure

End Class


'IMAGE STA COMBOBOX
Public Class ComboboxImage
    Inherits ComboBox

    Private ListImage1 As New ImageList

    Public Property ImageList() As ImageList
        Get
            Return ListImage1
        End Get
        Set(ByVal ListImagem As ImageList)
            ListImage1 = ListImagem
        End Set
    End Property

    Public Sub New()
        DrawMode = DrawMode.OwnerDrawFixed
    End Sub



    Protected Overrides Sub OnDrawItem(ByVal e As System.Windows.Forms.DrawItemEventArgs)
        e.DrawBackground()
        e.DrawFocusRectangle()

        Dim item As New ComboBoxIconItem
        Dim imageSize As New Size
        imageSize = ListImage1.ImageSize

        Dim bounds As New Rectangle
        bounds = e.Bounds

        Try
            item = Me.Items(e.Index)
            If (item.ImageIndex <> -1) Then
                Me.ImageList.Draw(e.Graphics, bounds.Left, bounds.Top, item.ImageIndex)
                e.Graphics.DrawString(item.Text, e.Font, New SolidBrush(e.ForeColor), bounds.Left + imageSize.Width, bounds.Top)
            Else
                e.Graphics.DrawString(item.Text, e.Font, New SolidBrush(e.ForeColor), bounds.Left, bounds.Top)
            End If
        Catch ex As Exception
            If (e.Index <> -1) Then
                e.Graphics.DrawString(Items(e.Index).ToString(), e.Font, New SolidBrush(e.ForeColor), bounds.Left, bounds.Top)
            Else
                e.Graphics.DrawString(Text, e.Font, New SolidBrush(e.ForeColor), bounds.Left, bounds.Top)
            End If

        End Try
        MyBase.OnDrawItem(e)
    End Sub


End Class

Class ComboBoxIconItem
    Private _text As String

    Property Text() As String
        Get
            Return _text
        End Get
        Set(ByVal Value As String)
            _text = Value
        End Set
    End Property

    Private _imageIndex As Integer

    Property ImageIndex() As Integer
        Get
            Return _imageIndex
        End Get

        Set(ByVal Value As Integer)
            _imageIndex = Value
        End Set
    End Property

    Public Sub New()
        _text = ""
    End Sub

    Public Sub New(ByVal text As String)
        _text = text
    End Sub

    Public Sub New(ByVal text As String, ByVal imageIndex As Integer)
        _text = text
        _imageIndex = imageIndex
    End Sub


    Public Overrides Function ToString() As String
        Return _text
    End Function
End Class


Class TransparentDrawing
    Inherits DrawingArea
    Protected Overrides Sub OnDraw()

    End Sub
End Class

''Button7
'Try
'    'MsgBox(Directory.Exists(CurrentHardDriver & "VB4Android\Projects\lib"))
'    If File.Exists(CurrentHardDriver & "VB4Android\" & "compiler.exe") AndAlso Directory.Exists(CurrentHardDriver & "VB4Android\lib") Then

'        psi = New ProcessStartInfo("cmd.exe")

'        ConsoleDouble += 2
'        RichTextBox1.AppendText("[" & ConsoleDouble - 1 & "] & vbNewLine & "[" & ConsoleDouble & "] Starting Console." & vbNewLine)

'        Dim systemencoding As System.Text.Encoding = _
'            System.Text.Encoding.GetEncoding(Globalization.CultureInfo.CurrentUICulture.TextInfo.OEMCodePage)

'        With psi
'            .UseShellExecute = False  ' Required for redirection
'            .RedirectStandardError = True
'            .RedirectStandardOutput = True
'            .RedirectStandardInput = True
'            .CreateNoWindow = True
'            .StandardOutputEncoding = systemencoding  ' Use OEM encoding for console applications
'            .StandardErrorEncoding = systemencoding
'        End With

'        ' EnableraisingEvents is required for Exited event
'        cmd = New Process With {.StartInfo = psi, .EnableRaisingEvents = True}

'        AddHandler cmd.ErrorDataReceived, AddressOf Async_Data_Received
'        AddHandler cmd.OutputDataReceived, AddressOf Async_Data_Received
'        AddHandler cmd.Exited, AddressOf CMD_Exited

'        cmd.Start()
'        ' Start async reading of the redirected streams
'        ' Without these calls the events won't fire
'        cmd.BeginOutputReadLine()
'        cmd.BeginErrorReadLine()

'        ConsoleDouble += 1
'        RichTextBox1.AppendText("[" & ConsoleDouble & "] Console Successfully Started." & vbNewLine)

'        cmd.StandardInput.WriteLine(CurrentHardDriver & "VB4Android\compiler.exe " & CurrentHardDriver & "VB4Android\Projects\" & FileCreatedName & " " & Label4.Text.Replace("- ( ", "").Replace(" )", ""))
'        ' Me.txtConsoleIn.Select()
'    ElseIf Not File.Exists(CurrentHardDriver & "VB4Android\compiler.exe") And Not Directory.Exists(CurrentHardDriver & "VB4Android\lib") Then
'        Using New Centered_MessageBox(Me)
'            MsgBox("There is no Compiler (compiler.exe File) and lib folder.", MsgBoxStyle.Critical, "Compiler and lib folder not found.")
'        End Using
'    ElseIf Not File.Exists(CurrentHardDriver & "VB4Android\compiler.exe") Then
'        Using New Centered_MessageBox(Me)
'            MsgBox("There is no Compiler (compiler.exe File).", MsgBoxStyle.Critical, "Compiler not found.")
'        End Using
'    ElseIf Not Directory.Exists(CurrentHardDriver & "VB4Android\lib") Then
'        Using New Centered_MessageBox(Me)
'            MsgBox("There is no lib folder.", MsgBoxStyle.Critical, "lib folder not found.")
'        End Using
'    End If
'Catch ex As Exception
'    ConsoleDouble += 1
'    RichTextBox1.AppendText(vbNewLine & "[" & ConsoleDouble & "] Console Failed to start. Error:" & vbNewLine & ex.ToString & vbNewLine)
'End Try

'Private psi As ProcessStartInfo
'Private cmd As Process
'Private Delegate Sub InvokeWithString(ByVal text As String)
'Private Sub CMD_Exited(ByVal sender As Object, ByVal e As EventArgs)
'    ConsoleDouble += 1
'    RichTextBox1.AppendText("[" & ConsoleDouble & "] End Console.")
'End Sub

'' This sub gets called in a different thread so invokation is required
'Private Sub Async_Data_Received(ByVal sender As Object, ByVal e As DataReceivedEventArgs)
'    Me.Invoke(New InvokeWithString(AddressOf Sync_Output), e.Data)



'End Sub

'Private Sub Sync_Output(ByVal text As String)
'    ' txtConsoleOut.AppendText(text & Environment.NewLine)
'    'txtConsoleOut.ScrollToCaret()
'    ConsoleDouble += 1
'    RichTextBox1.AppendText("[" & ConsoleDouble & "] " & text & Environment.NewLine)

'    '8a to kanw na stelnei to minima kai meta na kanei loop mexri na svistei to minima

'End Sub
