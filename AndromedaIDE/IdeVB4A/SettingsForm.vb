Public Class SettingsForm
    Private Sub SettingsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Location = Form1.Location + New Point(Form1.Size.Width / 2, Form1.Size.Height / 2) - New Point(Me.Size.Width / 2, Me.Size.Height / 2)
    End Sub
End Class