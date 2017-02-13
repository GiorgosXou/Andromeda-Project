Imports System.IO

Public Class Form3CreateNewForm
    Protected Overrides ReadOnly Property CreateParams() As System.Windows.Forms.CreateParams
        Get
            Const DROPSHADOW = &H20000
            Dim cParam As CreateParams = MyBase.CreateParams
            cParam.ClassStyle = cParam.ClassStyle Or DROPSHADOW
            Return cParam
        End Get
    End Property
    Dim Loc As Point
    Private Sub Panel2_MouseDown(sender As Object, e As MouseEventArgs) Handles Panel2.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Loc = e.Location
        End If
    End Sub

    Private Sub Panel2_MouseMove(sender As Object, e As MouseEventArgs) Handles Panel2.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Me.Location += e.Location - Loc
        End If
    End Sub

    Private Sub Form3CreateNewForm_Load(sender As Object, e As EventArgs) Handles Me.Load
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
        Me.ShowInTaskbar = False
    End Sub
    Private Sub TextBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        'lowercase not symbols not special chars not numbers
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
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
    Public fs As FileStream
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Not TextBox1.Text = "" Then
            If Not File.Exists(Form1.CurrentHardDriver & "VB4Android\Projects\" & Form1.FileCreatedName & "\" & TextBox1.Text & ".vb4a") Then
                fs = File.Create(Form1.CurrentHardDriver & "VB4Android\Projects\" & Form1.FileCreatedName & "\" & TextBox1.Text & ".vb4acode")

                Dim ssWriter As New StreamWriter(fs)

                ssWriter.BaseStream.Seek(0, SeekOrigin.Begin)

                ssWriter.Write(" ")

                ssWriter.Close()

                fs.Close()

                fs = File.Create(Form1.CurrentHardDriver & "VB4Android\Projects\" & Form1.FileCreatedName & "\" & TextBox1.Text & ".DelConts")
                fs.Close()

                fs = File.Create(Form1.CurrentHardDriver & "VB4Android\Projects\" & Form1.FileCreatedName & "\" & TextBox1.Text & ".vb4a")
                Form1.Combobox3.Items.Add(New ComboBoxIconItem(TextBox1.Text & ".vb4a", 0))






                Dim sWriter As New StreamWriter(fs)

                sWriter.BaseStream.Seek(0, SeekOrigin.Begin)

                sWriter.Write("++Form++" & vbNewLine & "Name=" & TextBox1.Text & vbNewLine & "BackgroundImage=" & vbNewLine & "Layout=VERTICAL" & vbNewLine & "Scrollable=False" & vbNewLine & "Title=" & TextBox1.Text & vbNewLine & "BackgroundColor={" & Color.WhiteSmoke.A & ";" & Color.WhiteSmoke.R & ";" & Color.WhiteSmoke.G & ";" & Color.WhiteSmoke.B & "}" & vbNewLine & "--Form--" & vbNewLine)

                sWriter.Close()

                TextBox1.Text = ""

                fs.Close()



                Me.Close()
            Else
                Using New Centered_MessageBox(Me)
                    ' Beep()
                    MsgBox("Form Already Exist!", vbInformation, "File Already Exist!")
                End Using
            End If
        Else
            Using New Centered_MessageBox(Me)
                MsgBox("Empty Project Name", MsgBoxStyle.Critical)
            End Using
        End If
    End Sub
    Private Sub TextBox1_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyCode = Keys.Enter Then
            Me.Button1.PerformClick()
        End If
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub
End Class