Imports System.IO
Public Class Form2NewProject

    Dim Loc As Point
    Protected Overrides ReadOnly Property CreateParams() As System.Windows.Forms.CreateParams
        Get
            Const DROPSHADOW = &H20000
            Dim cParam As CreateParams = MyBase.CreateParams
            cParam.ClassStyle = cParam.ClassStyle Or DROPSHADOW
            Return cParam
        End Get
    End Property
    Private Sub Panel2_MouseDown1(sender As Object, e As MouseEventArgs) Handles Panel2.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Loc = e.Location
        End If
    End Sub

    Private Sub Panel2_MouseMove1(sender As Object, e As MouseEventArgs) Handles Panel2.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Me.Location += e.Location - Loc
        End If
    End Sub
    Private Sub Form2NewProject_FormClosed(sender As Object, e As EventArgs) Handles Me.FormClosed
        '  Form1.SaveProj("++Form++" & vbNewLine & "BackgroundImage=" & vbNewLine & "Layout=VERTICAL" & vbNewLine & "Scrollable=Flase" & vbNewLine & "Title=Form1" & vbNewLine & "BackgroundColor=16777215" & vbNewLine & "--Form--" & vbNewLine, True, "form1" & ".vb4a")
    End Sub
    Private Sub Form2NewProject_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Size = New Size(316, 127)
        Panel1.Size = New Size(316, 127)
        Panel2.Size = New Size(317, 23)
        Label1.Location = New Point(4, 2)
        Button2.Location = New Point(287, -1)
        Button2.Size = New Size(26, 22)
        Label2.Location = New Point(9, 45)
        TextBox1.Location = New Point(11, 64)
        TextBox1.Size = New Size(292, 20)
        Button1.Size = New Size(75, 23)
        Button1.Location = New Point(228, 94)

        Me.Location = Form1.Location + Form1.Panel5.Location + Form1.PictureBox1.Location - New Point(36, -(Form1.Panel5.Height / 2) + Me.Height / 2)
        Me.BringToFront()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Me.Close()
    End Sub
    Private Sub TextBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        'lowercase not symbols not special chars not numbers
        If (Microsoft.VisualBasic.Asc(e.KeyChar) < 97) Or (Microsoft.VisualBasic.Asc(e.KeyChar) > 122) Then
            e.Handled = True
        End If
        If (Microsoft.VisualBasic.Asc(e.KeyChar) = 8) Then
            e.Handled = False
        End If

    End Sub
    Public fs As FileStream ' because we will need to use it for save at form1 .... ΛοΛ Ελληνας Developer 16 χρονων :D
    Function UppercaseFirstLetter(ByVal val As String) As String
        ' Test for nothing or empty.
        If String.IsNullOrEmpty(val) Then
            Return val
        End If

        ' Convert to character array.
        Dim array() As Char = val.ToCharArray

        ' Uppercase first character.
        array(0) = Char.ToUpper(array(0))

        ' Return new string.
        Return New String(array)
    End Function

    Private Sub EndofBt1()
        Try
            fs = File.Create(Form1.CurrentHardDriver & "VB4Android\Projects\" & TextBox1.Text & "\" & TextBox1.Text & ".VB4AProj")
            fs.Close()
            My.Computer.FileSystem.CreateDirectory(Form1.CurrentHardDriver & "VB4Android\Projects\" & TextBox1.Text & "\BUILD\assets")
            My.Computer.FileSystem.CreateDirectory(Form1.CurrentHardDriver & "VB4Android\Projects\" & TextBox1.Text & "\BUILD\build\classes\com\vb4a")
            My.Computer.FileSystem.CreateDirectory(Form1.CurrentHardDriver & "VB4Android\Projects\" & TextBox1.Text & "\BUILD\build\deploy")
            My.Computer.FileSystem.CreateDirectory(Form1.CurrentHardDriver & "VB4Android\Projects\" & TextBox1.Text & "\BUILD\build\res\drawable")
            My.Computer.FileSystem.CreateDirectory(Form1.CurrentHardDriver & "VB4Android\Projects\" & TextBox1.Text & "\BUILD\build\tmp")
            My.Computer.FileSystem.CreateDirectory(Form1.CurrentHardDriver & "VB4Android\Projects\" & TextBox1.Text & "\BUILD\src\com\vb4a")
            My.Computer.FileSystem.CreateDirectory(Form1.CurrentHardDriver & "VB4Android\Projects\" & TextBox1.Text & "\CPROJ")
            fs = File.Create(Form1.CurrentHardDriver & "VB4Android\Projects\" & TextBox1.Text & "\CPROJ\cproject.v4a")
            fs.Close()

            My.Computer.FileSystem.WriteAllText(Form1.CurrentHardDriver & "VB4Android\Projects\" & TextBox1.Text & "\CPROJ\cproject.v4a", "cprojtype=" & vbNewLine & "cprojname=" & "CProjName" & My.Computer.FileSystem.GetDirectories(Form1.CurrentHardDriver & "VB4Android\Projects\").Count & vbNewLine & "cfiles=", False)
            'fs = File.Create(Form1.CurrentHardDriver & "VB4Android\Projects\" & TextBox1.Text & "\imgDEL.ini")
            'fs.Close()
            'fs = File.Create(Form1.CurrentHardDriver & "VB4Android\Projects\" & TextBox1.Text & "\img.ini")
            'fs.Close()
            Form1.TextBox2.Text = "VB4Application" & My.Computer.FileSystem.GetDirectories(Form1.CurrentHardDriver & "VB4Android\Projects\").Count
            Form1.ToolStripTextBox1.Text = "com.vb4a." & UppercaseFirstLetter(TextBox1.Text) 'Vapp" & My.Computer.FileSystem.GetDirectories(Form1.CurrentHardDriver & "VB4Android\Projects\").Count
            My.Computer.FileSystem.WriteAllText(Form1.CurrentHardDriver & "VB4Android\Projects\" & TextBox1.Text & "\" & TextBox1.Text & ".VB4AProj",
                                                "Phone=False" & vbNewLine &
                                                "TempSensor=False" & vbNewLine &
                                                "GSensore=False" & vbNewLine &
                                                "Network=False" & vbNewLine &
                                                "AccSensor=False" & vbNewLine &
                                                "GyroSensor=False" & vbNewLine &
                                                "LightSensor=False" & vbNewLine &
                                                "GPS=False" & vbNewLine &
                                                "ApplicationPKGName=" & UppercaseFirstLetter(TextBox1.Text) & vbNewLine &
                                                "ApplicationName=VB4Application" & My.Computer.FileSystem.GetDirectories(Form1.CurrentHardDriver & "VB4Android\Projects\").Count & vbNewLine &
                                                "ApplicationTheme=Theme.Light.NoTitleBar.Fullscreen" & vbNewLine &
                                                "ApplicationIcon=icon.png" & vbNewLine &
                                                "APPWITHC=False" & vbNewLine &
                                                "ScreenSize=240x425", False)   'Vapp" & My.Computer.FileSystem.GetDirectories(Form1.CurrentHardDriver & "VB4Android\Projects\").Count & vbNewLine & _

            fs = File.Create(Form1.CurrentHardDriver & "VB4Android\Projects\" & TextBox1.Text & "\" & Form1.ToolStripTextBox1.Text.Replace("com.vb4a.", "") & ".DelConts")
            fs.Close()
            fs = File.Create(Form1.CurrentHardDriver & "VB4Android\Projects\" & TextBox1.Text & "\" & Form1.ToolStripTextBox1.Text.Replace("com.vb4a.", "") & ".vb4acode")
            Dim ssWriter As New StreamWriter(fs)

            ssWriter.BaseStream.Seek(0, SeekOrigin.Begin)

            ssWriter.Write(" ")

            ssWriter.Close()

            fs.Close()
            fs = File.Create(Form1.CurrentHardDriver & "VB4Android\Projects\" & TextBox1.Text & "\" & TextBox1.Text & ".CADel")
            fs.Close()
            fs = File.Create(Form1.CurrentHardDriver & "VB4Android\Projects\" & TextBox1.Text & "\" & Form1.ToolStripTextBox1.Text.Replace("com.vb4a.", "") & ".vb4a") ' TextBox1.Text

            'IO.File.WriteAllBytes(Form1.CurrentHardDriver & "VB4Android\Projects\" & TextBox1.Text & "\" & "IconImage.ico", My.Resources.IconImage)
            Dim someImage As Bitmap = My.Resources.icon
            someImage.Save(Form1.CurrentHardDriver & "VB4Android\Projects\" & TextBox1.Text & "\" & "icon.png", System.Drawing.Imaging.ImageFormat.Png)

            'Form1.PictureBox3.SizeMode = PictureBoxSizeMode.CenterImage
            Form1.PictureBox3.Image = Image.FromFile(Form1.CurrentHardDriver & "VB4Android\Projects\" & TextBox1.Text & "\" & "icon.png")
            Form1.TextBox1.Text = Form1.CurrentHardDriver & "VB4Android\Projects\" & TextBox1.Text & "\" & "icon.png"

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        Form1.FileCreatedName = TextBox1.Text
        Form1.FileDirName = Form1.CurrentHardDriver & "VB4Android\Projects\" & TextBox1.Text & "\"

        Form1.Label4.Text = "- ( " & TextBox1.Text & " )"
        With Form1
            .BeforeCombo3 = Form1.ToolStripTextBox1.Text.Replace("com.vb4a.", "") & ".vb4a"
            .ListView1.Enabled = True
            .Panel1.Enabled = True
            .Panel2.Enabled = True
            .Panel3.Enabled = True
            '.Button12.Enabled = True
            .Panel5.Enabled = True
            .Combobox3.Items.Clear()
            .Combobox3.Items.Add(New ComboBoxIconItem(Form1.ToolStripTextBox1.Text.Replace("com.vb4a.", "") & ".vb4a", 0))
            .ComboboxImage1.Items.Add(New ComboBoxIconItem(Form1.ToolStripTextBox1.Text.Replace("com.vb4a.", ""), 16))
            .Combobox3.Text = Form1.ToolStripTextBox1.Text.Replace("com.vb4a.", "") & ".vb4a"
            .Textbox3.Text = " "
            .ListView1.Hide()
            '.ToolStripTextBox1.Enabled = True
            '.ToolStripComboBox1.Enabled = True
            .ToolStripTextBox2.Text = "CProjName" & My.Computer.FileSystem.GetDirectories(Form1.CurrentHardDriver & "VB4Android\Projects\").Count
            .ToolStripComboBox1.Text = "Light.NoTitleBar.Fullscreen"
            .PropertyGrid1.Enabled = True
            .AddToolStripMenuItem.Enabled = True
            .ToolStripTextBox2.Enabled = True
            .RemoveToolStripMenuItem.Enabled = True

            .XofFormApp = 240
            .YofFormApp = 425
            .Panel5.Location = New Point(0, 0)
            .Panel5.Size = New Size(0, 0)
            .PictureBox4.Location = New Point(0, 0)
            .PictureBox4.Size = New Size(0, 0)
            .PictureBox4.Size = New Size(Form1.XofFormApp, Form1.YofFormApp)
            .Panel5.Size = New Size(Form1.XofFormApp + 2, Form1.YofFormApp + 2)
            .Panel5.Location = New Point(((Form1.PictureBox1.Size.Width - Form1.Panel5.Size.Width) \ 2) - 1, ((Form1.PictureBox1.Size.Height - Form1.Panel5.Size.Height) \ 2) - 1)
            .PictureBox4.Location = Form1.Panel5.Location + New Point(0, 1)
            .ToolStripTextBox3.Text = Form1.XofFormApp
            .ToolStripTextBox4.Text = Form1.YofFormApp
            .PictureBox1.Refresh()

            .ChangeToolStripMenuItem.Enabled = True
        End With

        TextBox1.Text = ""

        Dim sWriter As New StreamWriter(fs)

        sWriter.BaseStream.Seek(0, SeekOrigin.Begin)

        Form1.ProjectSaveString = "++Form++" & vbNewLine & "Name=" & Form1.ToolStripTextBox1.Text.Replace("com.vb4a.", "") & vbNewLine & "BackgroundImage=" & vbNewLine & "Layout=VERTICAL" & vbNewLine & "Scrollable=False" & vbNewLine & "Title=" & Form1.ToolStripTextBox1.Text.Replace("com.vb4a.", "") & vbNewLine & "BackgroundColor={" & Color.WhiteSmoke.A & ";" & Color.WhiteSmoke.R & ";" & Color.WhiteSmoke.G & ";" & Color.WhiteSmoke.B & "}" & vbNewLine & "--Form--" & vbNewLine

        sWriter.Write("++Form++" & vbNewLine & "BackgroundImage=" & vbNewLine & "Layout=VERTICAL" & vbNewLine & "Scrollable=False" & vbNewLine & "Title=" & Form1.ToolStripTextBox1.Text.Replace("com.vb4a.", "") & vbNewLine & "BackgroundColor={" & Color.WhiteSmoke.A & ";" & Color.WhiteSmoke.R & ";" & Color.WhiteSmoke.G & ";" & Color.WhiteSmoke.B & "}" & vbNewLine & "--Form--" & vbNewLine)

        Form1.Combobox1.Items.Add(New ComboBoxIconItem(Form1.ToolStripTextBox1.Text.Replace("com.vb4a.", ""), 16))

        sWriter.Close()

        fs.Close()



        Form1.Combobox1.Text = Form1.ToolStripTextBox1.Text.Replace("com.vb4a.", "")
        'Form1.formisProp()


        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If Not TextBox1.Text = "" Then
            If Not Form1.Label4.Text = "" Then fs.Close()
            If Not Directory.Exists(Form1.CurrentHardDriver & "VB4Android\Projects\" & TextBox1.Text) Then
                Directory.CreateDirectory(Form1.CurrentHardDriver & "VB4Android\Projects\" & TextBox1.Text)
                EndofBt1()
            Else
                Using New Centered_MessageBox(Me)
                    Beep()
                    Select Case MsgBox("Do You Want To Overwrite Files?", vbYesNo, "File Already Exist!")
                        Case MsgBoxResult.Yes
                            ' Directory.Delete()
                            If Not Form1.Label4.Text = "" Then fs.Close()
                            My.Computer.FileSystem.DeleteDirectory(Form1.CurrentHardDriver & "VB4Android\Projects\" & TextBox1.Text, FileIO.DeleteDirectoryOption.DeleteAllContents)
                            Directory.CreateDirectory(Form1.CurrentHardDriver & "VB4Android\Projects\" & TextBox1.Text)

                            EndofBt1()

                        Case MsgBoxResult.No
                    End Select
                End Using
            End If
        Else
            Using New Centered_MessageBox(Me)
                MsgBox("Empty Project Name", MsgBoxStyle.Critical)
            End Using
        End If
    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub TextBox1_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyCode = Keys.Enter Then
            Me.Button1.PerformClick()
        End If
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
End Class