Public Class AboutFrm

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Process.Start("http://code.google.com/p/vb4android/")
    End Sub

    Private Sub AboutFrm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = Form1.Icon

        ' LinkLabel1.Location = New Point(724, 163)
        ' LinkLabel1.Size = New Size(194, 13)

        Label1.Size = New Size(488, 32)
        RichTextBox3.Size = New Size(178, 89)
        Label3.Size = New Size(104, 17)
        RichTextBox1.Size = New Size(797, 195)
        RichTextBox4.Size = New Size(159, 72)
        Label6.Size = New Size(140, 17)
        RichTextBox2.Size = New Size(797, 143)

        PictureBox1.Size = New Size(159, 161)
        PictureBox2.Size = New Size(159, 161)


        Label1.Location = New Point(335, 13)
        RichTextBox3.Location = New Point(18, 219)
        Label3.Location = New Point(210, 67)
        RichTextBox1.Location = New Point(234, 98)
        RichTextBox4.Location = New Point(18, 478)
        Label6.Location = New Point(210, 331)
        RichTextBox2.Location = New Point(234, 358)

        PictureBox1.Location = New Point(18, 55)
        PictureBox2.Location = New Point(18, 314)

        Me.Size = New Size(1057, 620)
        Me.MaximumSize = New Size(1057, 620)
        Me.MinimumSize = New Size(1057, 620)

        If Globalization.CultureInfo.CurrentUICulture.TextInfo.CultureName.ToString = "zh-CN" Or _
            Globalization.CultureInfo.CurrentUICulture.TextInfo.CultureName.ToString = "zh-HK" Or _
            Globalization.CultureInfo.CurrentUICulture.TextInfo.CultureName.ToString = "zh-MO" Or _
            Globalization.CultureInfo.CurrentUICulture.TextInfo.CultureName.ToString = "zh-SG" Or _
            Globalization.CultureInfo.CurrentUICulture.TextInfo.CultureName.ToString = "zh-TW" Or _
            Globalization.CultureInfo.CurrentUICulture.TextInfo.CultureName.ToString = "zh-CHS" Or _
            Globalization.CultureInfo.CurrentUICulture.TextInfo.CultureName.ToString = "zh-CHT" Then

            'Label1.Font = New Font("Microsoft Sans Serif", "21")
            'Label2.Font = New Font("Microsoft Sans Serif", "9")
            'Label3.Font = New Font("Microsoft Sans Serif", "10")
            'Label4.Font = New Font("Microsoft Sans Serif", "9")
            'Label5.Font = New Font("Microsoft Sans Serif", "9")
            'Label6.Font = New Font("Microsoft Sans Serif", "10")
            'Label7.Font = New Font("Microsoft Sans Serif", "9")

            '  Me.MaximumSize = New Size(1157, 6200)
            ' Me.MinimumSize = New Size(1157, 620)
            ' Me.Size = New Size(1157, 620)
            'Me.Text = "Chinese Detected! :D  ---- " & Globalization.CultureInfo.CurrentUICulture.TextInfo.CultureName.ToString
        End If


    End Sub
End Class