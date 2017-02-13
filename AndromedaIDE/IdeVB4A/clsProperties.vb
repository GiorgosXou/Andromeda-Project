Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports System.Drawing.Design
Imports System.Text




Public Class ClsButton
    Public Shared _Name As String
    Public Shared _Size As Size
    Public Shared _Location As Point
    Public Shared _BackColor As System.Drawing.Color
    Public Shared _Enabled As Boolean

    Public Shared _Text As String 'Caption
    Public Shared _Font_Size As Single
    Public Shared _bold As Boolean
    Public Shared _ForeColor As System.Drawing.Color
    Public Shared _Italic As Boolean
    Public Shared _TextAlign As String
    Public Shared _FontTypeface As String

    Public Shared _Image As String


    '  Sub New()
    '      _Name = "asd"
    '  End Sub

    <CategoryAttribute("Common"), _
    Browsable(True), _
    [ReadOnly](False), _
    BindableAttribute(False), _
    DesignOnly(False), _
    DescriptionAttribute("Enter The Name Of Button.")> _
    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            ' MsgBox(Form1.ProjectSaveString)
            For i = 0 To Form1.strClsRemove.Count - 1
                value = value.Replace(Form1.strClsRemove(i), "")
            Next
            'value = value.Replace(strRemove(1), "")

            'MsgBox(value)

            If Not Form1.ProjectSaveString.Contains("Name=" & value & vbNewLine) Then
                Form1.Combobox1.Items.Remove(Form1.cmb)

                For item = 0 To Form1.ComboboxImage1.Items.Count
                    If Form1.ComboboxImage1.Items.Item(item).ToString = _Name Then Form1.ComboboxImage1.Items.RemoveAt(item) : Form1.ComboboxImage1.Items.Add(New ComboBoxIconItem(value, 0)) : Exit For
                Next

                If Form1.Textbox3.Text.Contains("Sub " & _Name & "_Click(") Then
                    For Each r In Form1.Textbox3.Range.GetRanges("Sub " & _Name & "_Click")
                        Form1.Textbox3.Selection = r
                        Form1.Textbox3.Text = Form1.Textbox3.Text.Replace(Form1.Textbox3.SelectedText, "Sub " & value & "_Click")
                    Next
                End If

                _Name = value

                Form1.Combobox1.Items.Add(New ComboBoxIconItem(value, 0))

                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Name=", "Name=" & value, Form1.GenControl.Name)
                Form1.GenControl.Tag = _Name

                Form1.Combobox1.Text = _Name

                Form1.BuildAutocompleteMenu()
            Else

                Using New Centered_MessageBox(Form1)
                    'MsgBox(Form1.ProjectSaveString)
                    MsgBox("Name Already Exists!", vbInformation, "Exists!")
                End Using
            End If
        End Set
    End Property
    <CategoryAttribute("Common"), _
    Browsable(True), _
    [ReadOnly](False), _
    BindableAttribute(False), _
    DesignOnly(False), _
    DescriptionAttribute("Enter The Size Of Button.")> _
    Public Property Size() As Size
        Get
            Return _Size
        End Get
        Set(ByVal value As Size)


            If value.Width > Form1.XofFormApp Then value.Width = Form1.XofFormApp
            If value.Height > Form1.YofFormApp Then value.Height = Form1.YofFormApp

            If value.Height < 0 Then value.Height = 0
            If value.Width < 0 Then value.Width = 0

            If _Location.X + value.Width > Form1.XofFormApp AndAlso _Location.Y + value.Height > Form1.YofFormApp Then
                Form1.GenControl.Location = New Point(0, 0)
                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={0;0}", Form1.GenControl.Name)
                _Location = New Point(0, 0)
            ElseIf _Location.X + value.Width > Form1.XofFormApp Then
                Form1.GenControl.Location = New Point(Form1.XofFormApp - value.Width, _Location.Y)
                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={" & Form1.XofFormApp - value.Width & ";" & _Location.Y & "}", Form1.GenControl.Name)
                _Location = New Point(Form1.XofFormApp - value.Width, _Location.Y)
            ElseIf _Location.Y + value.Height > Form1.YofFormApp Then
                Form1.GenControl.Location = New Point(_Location.X, Form1.YofFormApp - value.Height)
                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={" & _Location.X & ";" & Form1.YofFormApp - value.Height & "}", Form1.GenControl.Name)
                _Location = New Point(_Location.X, Form1.YofFormApp - value.Height)
            End If


            _Size = value

            Form1.GenControl.Size = New Size(value)




            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Size=", "Size=" & value.ToString.Replace("Width=", Nothing).Replace(",", ";").Replace("Height=", Nothing).Replace(" ", Nothing), Form1.GenControl.Name)

            Form1.DRWTran()

        End Set
    End Property
    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter The Location Of Button.")>
    Public Property Location() As Point
        Get
            Return _Location
        End Get
        Set(ByVal value As Point)


            If value.X + _Size.Width > Form1.XofFormApp AndAlso value.Y + _Size.Height > Form1.YofFormApp Then : value = New Point(0, 0)
            ElseIf value.X + _Size.Width > Form1.XofFormApp Then : value = New Point(Form1.XofFormApp - _Size.Width, value.Y)
            ElseIf value.Y + _Size.Height > Form1.YofFormApp Then : value = New Point(value.X, Form1.YofFormApp - _Size.Height)
            End If

            If value.X < 0 Then value.X = 0
            If value.Y < 0 Then value.Y = 0

            _Location = value


            Form1.GenControl.Location = New Point(value)
            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={" & value.X.ToString & ";" & value.Y.ToString & "}", Form1.GenControl.Name)
            Form1.DRWTran()
        End Set
    End Property
    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select BackColor(BackGround) Of Button.")>
    Public Property BackColor() As System.Drawing.Color
        Get
            Return _BackColor
        End Get
        Set(ByVal value As System.Drawing.Color)
            _BackColor = value

            Form1.GenControl.BackColor = Color.FromArgb(value.ToArgb)
            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "BackColor=", "BackColor={" & value.A & ";" & value.R & ";" & value.G & ";" & value.B & "}", Form1.GenControl.Name)
            ' Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "BackColor=", "BackColor={" & value.A & ";" & value.R & ";" & value.G & ";" & value.B & "}", Form1.GenControl.Name)
            'MsgBox("ARGB: {" & value.A & ";" & value.R & ";" & value.G & ";" & value.B & "}" & vbNewLine & "A:" & value.A & ", R:" & value.R & ", G:" & value.G & ", B:" & value.B & vbNewLine & Color.FromArgb(value.ToArgb.ToString).ToString)
        End Set
    End Property
    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select If The Button Will Be Enabled Or Not")>
    Public Property Enabled() As Boolean
        Get
            Return _Enabled
        End Get
        Set(ByVal Value As Boolean)
            _Enabled = Value

            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Enabled=", "Enabled=" & Value.ToString, Form1.GenControl.Name)

        End Set
    End Property






    <CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter Font Integer Size For The Button.")>
    Public Property FontSize() As Single
        Get
            Return _Font_Size
        End Get
        Set(ByVal value As Single)
            _Font_Size = value





            If _bold = True Then
                Form1.GenControl.Font = New Font(FontStyle.Bold, value)
                Form1.GenControl.Font = New Font(Form1.GenControl.Font, FontStyle.Bold)
            ElseIf _Italic = True Then
                Form1.GenControl.Font = New Font(FontStyle.Italic, value)
                Form1.GenControl.Font = New Font(Form1.GenControl.Font, FontStyle.Italic)
            Else
                Form1.GenControl.Font = New Font(FontStyle.Regular, value)
            End If


            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "FontSize=", "FontSize=" & value, Form1.GenControl.Name)

        End Set
    End Property
    <CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter Text For The Button.")>
    Public Property Text() As String
        Get
            Return _Text
        End Get
        Set(ByVal value As String)
            _Text = value

            Form1.GenControl.Text = value
            '   MsgBox(Form1.PropertiesOfControl)
            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Text=", "Text=" & value, Form1.GenControl.Name)
            '  MsgBox(Form1.PropertiesOfControl)
        End Set
    End Property
    <CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select If The Text Of Button Will Be Italic Or Not.")>
    Public Property Italic() As Boolean
        Get
            Return _Italic
        End Get
        Set(ByVal value As Boolean)
            _Italic = value

            If _bold = True Then _bold = False : Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "FontBold=", "FontBold=" & "False", Form1.GenControl.Name)

            If _Italic = True Then
                Form1.GenControl.Font = New Font(Form1.GenControl.Font, FontStyle.Italic)
            Else
                Form1.GenControl.Font = New Font(Form1.GenControl.Font, FontStyle.Regular)
            End If

            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "FontItalic=", "FontItalic=" & value.ToString, Form1.GenControl.Name)

        End Set
    End Property
    <CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select The Color Of Button Text.")>
    Public Property ForeColor() As System.Drawing.Color
        Get
            Return _ForeColor
        End Get
        Set(ByVal value As System.Drawing.Color)
            _ForeColor = value

            Form1.GenControl.ForeColor = Color.FromArgb(value.ToArgb)
            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "ForeColor=", "ForeColor={" & value.A & ";" & value.R & ";" & value.G & ";" & value.B & "}", Form1.GenControl.Name)

        End Set
    End Property
    <CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select If The Text Of Button Will Be Bold Or Not.")>
    Public Property Bold() As Boolean
        Get
            Return _bold
        End Get
        Set(ByVal value As Boolean)
            _bold = value

            If _Italic = True Then _Italic = False : Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "FontItalic=", "FontItalic=" & "False", Form1.GenControl.Name)

            If _bold = True Then
                Form1.GenControl.Font = New Font(Form1.GenControl.Font, FontStyle.Bold)
            Else
                Form1.GenControl.Font = New Font(Form1.GenControl.Font, FontStyle.Regular)
            End If


            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "FontBold=", "FontBold=" & value.ToString, Form1.GenControl.Name)

        End Set
    End Property
    <TypeConverter(GetType(TextAlignList)),
    CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select TextAlign For Button's Text.")>
    Public Property TextAlign() As String
        Get
            Return _TextAlign
        End Get
        Set(ByVal value As String)
            _TextAlign = value

            Select Case _TextAlign
                Case Is = "LEFT" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), Button).TextAlign = ContentAlignment.MiddleLeft
                Case Is = "CENTER" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), Button).TextAlign = ContentAlignment.MiddleCenter
                Case Is = "RIGHT" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), Button).TextAlign = ContentAlignment.MiddleRight
                Case Is = "LEFT_TOP" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), Button).TextAlign = ContentAlignment.TopLeft
                Case Is = "LEFT_CENTER" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), Button).TextAlign = ContentAlignment.MiddleLeft
                Case Is = "LEFT_BOTTOM" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), Button).TextAlign = ContentAlignment.BottomLeft
                Case Is = "CENTER_TOP" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), Button).TextAlign = ContentAlignment.TopCenter
                Case Is = "CENTER_CENTER" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), Button).TextAlign = ContentAlignment.MiddleCenter
                Case Is = "CENTER_BOTTOM" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), Button).TextAlign = ContentAlignment.BottomCenter
                Case Is = "RIGHT_TOP" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), Button).TextAlign = ContentAlignment.TopRight
                Case Is = "RIGHT_CENTER" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), Button).TextAlign = ContentAlignment.MiddleRight
                Case Is = "RIGHT_BOTTOM" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), Button).TextAlign = ContentAlignment.BottomRight
            End Select
            ' DirectCast(Form1.Panel5.Controls(Form1.GenControl.Name), Button).TextAlign = ContentAlignment.MiddleCenter

            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "TextAlign=", "TextAlign=" & value.ToString, Form1.GenControl.Name)

        End Set
    End Property
    <TypeConverter(GetType(FontTypefaceList)),
    CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select The Font Type Of Button's Text.")>
    Public Property FontTypeface() As String
        Get
            Return _FontTypeface
        End Get
        Set(ByVal value As String)
            _FontTypeface = value

            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "FontTypeface=", "FontTypeface=" & value.ToString, Form1.GenControl.Name)

        End Set
    End Property





    <CategoryAttribute("Others"),
    Editor(GetType(System.Windows.Forms.Design.FileNameEditor), GetType(System.Drawing.Design.UITypeEditor)),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select An Image For The Button.")>
    Public Property Image() As String
        Get
            Return _Image
        End Get
        Set(ByVal value As String)

            If value.EndsWith(".png") Or value.EndsWith(".gif") Or value.EndsWith(".bmp") Or value.EndsWith(".jpg") Then

                If Not _Image = Nothing Then

                    Form1.GenControl.BackgroundImage.Dispose()

                    'Form1.GenControl.BackgroundImage.
                    Form1.GenControl.BackgroundImage = Nothing


                    _Image = Form1.CurrentHardDriver & _Image.Remove(0, 3)

                    '  Form1.SaveCodeProject(_Image & vbNewLine, "imgDEL.ini", True, Encoding.Default)

                End If

                If Not My.Computer.FileSystem.FileExists(Form1.CurrentHardDriver & "VB4Android\Projects\" & Form1.FileCreatedName & "\BUILD\assets\" & value.Split("\").Last) Then My.Computer.FileSystem.CopyFile(value, Form1.CurrentHardDriver & "VB4Android\Projects\" & Form1.FileCreatedName & "\BUILD\assets\" & value.Split("\").Last)
                value = Form1.CurrentHardDriver & "VB4Android\Projects\" & Form1.FileCreatedName & "\BUILD\assets\" & value.Split("\").Last
                'Form1.SaveCodeProject(value & vbNewLine, "img.ini", True, Encoding.Default)

                Form1.changeFromFileASpecPropOfCont(Form1.CurrentHardDriver & "VB4Android\Projects\" & Form1.FileCreatedName & "\" & Form1.Combobox3.SelectedItem.ToString, "Image=", "Image=" & value, Form1.GenControl.Name)
                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Image=", "Image=" & value, Form1.GenControl.Name)
                Form1.GenControl.BackgroundImage = System.Drawing.Image.FromFile(value)
                If Not _Image = Nothing AndAlso Not Form1.ProjectSaveString.Contains(_Image) AndAlso Form1.IsUsedFromAnotherProc(_Image) = False Then My.Computer.FileSystem.DeleteFile(_Image) : Form1.SaveCodeProject(_Image & vbNewLine, "imgDEL.ini", True, Encoding.Default)

                _Image = value
                '   Form1.AButton.Text = value
                ' value




                ' MsgBox(value)

                'http://bytes.com/topic/visual-basic-net/answers/387220-image-propertygrid

            ElseIf value = Nothing AndAlso Not _Image = Nothing Or value.Replace(" ", "") = Nothing AndAlso Not _Image = Nothing Then
                Form1.GenControl.BackgroundImage.Dispose()
                Form1.GenControl.BackgroundImage = Nothing
                _Image = Form1.CurrentHardDriver & _Image.Remove(0, 3)
                Form1.changeFromFileASpecPropOfCont(Form1.CurrentHardDriver & "VB4Android\Projects\" & Form1.FileCreatedName & "\" & Form1.Combobox3.SelectedItem.ToString, "Image=", "Image=", Form1.GenControl.Name)
                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Image=", "Image=", Form1.GenControl.Name)
                If Not Form1.ProjectSaveString.Contains(_Image) AndAlso Not _Image = Nothing AndAlso Form1.IsUsedFromAnotherProc(_Image) = False Then My.Computer.FileSystem.DeleteFile(_Image)
                _Image = ""
            Else
                MsgBox("You can only Insert png, gif, bmp and jpg files", MsgBoxStyle.Critical, "Wrong type of file.")
            End If

        End Set
    End Property
End Class ' ok..








Public Class ClsCanvas
    Public Shared _Name As String
    Public Shared _Size As Size
    Public Shared _Location As Point
    Public Shared _BackgroundColor As System.Drawing.Color
    Public Shared _BackgroundImage As String
    Public Shared _Enabled As Boolean

    Public Shared _PointColor As System.Drawing.Color

    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter The Name Of Cnavas.")>
    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)

            For i = 0 To Form1.strClsRemove.Count - 1
                value = value.Replace(Form1.strClsRemove(i), "")
            Next

            If Not Form1.ProjectSaveString.Contains("Name=" & value & vbNewLine) Then
                Form1.Combobox1.Items.Remove(Form1.cmb)
                For item = 0 To Form1.ComboboxImage1.Items.Count
                    If Form1.ComboboxImage1.Items.Item(item).ToString = _Name Then Form1.ComboboxImage1.Items.RemoveAt(item) : Form1.ComboboxImage1.Items.Add(New ComboBoxIconItem(value, 1)) : Exit For
                Next

                If Form1.Textbox3.Text.Contains("Sub " & _Name & "_VB4ADown(") Then
                    For Each r In Form1.Textbox3.Range.GetRanges("Sub " & _Name & "_VB4ADown")
                        Form1.Textbox3.Selection = r
                        Form1.Textbox3.Text = Form1.Textbox3.Text.Replace(Form1.Textbox3.SelectedText, "Sub " & value & "_VB4ADown")
                    Next
                End If

                _Name = value

                Form1.Combobox1.Items.Add(New ComboBoxIconItem(value, 1))

                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Name=", "Name=" & value, Form1.GenControl.Name)
                Form1.GenControl.Tag = _Name

                Form1.Combobox1.Text = _Name
                Form1.BuildAutocompleteMenu()
            Else

                Using New Centered_MessageBox(Form1)
                    'MsgBox(Form1.ProjectSaveString)
                    MsgBox("Name Already Exists!", vbInformation, "Exists!")
                End Using
            End If
        End Set
    End Property
    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter The Size Of Cnavas.")>
    Public Property Size() As Size
        Get
            Return _Size
        End Get
        Set(ByVal value As Size)

            If value.Width > Form1.XofFormApp Then value.Width = Form1.XofFormApp
            If value.Height > Form1.YofFormApp Then value.Height = Form1.YofFormApp

            If value.Height < 0 Then value.Height = 0
            If value.Width < 0 Then value.Width = 0

            If _Location.X + value.Width > Form1.XofFormApp AndAlso _Location.Y + value.Height > Form1.YofFormApp Then
                Form1.GenControl.Location = New Point(0, 0)
                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={0;0}", Form1.GenControl.Name)
                _Location = New Point(0, 0)
            ElseIf _Location.X + value.Width > Form1.XofFormApp Then
                Form1.GenControl.Location = New Point(Form1.XofFormApp - value.Width, _Location.Y)
                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={" & Form1.XofFormApp - value.Width & ";" & _Location.Y & "}", Form1.GenControl.Name)
                _Location = New Point(Form1.XofFormApp - value.Width, _Location.Y)
            ElseIf _Location.Y + value.Height > Form1.YofFormApp Then
                Form1.GenControl.Location = New Point(_Location.X, Form1.YofFormApp - value.Height)
                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={" & _Location.X & ";" & Form1.YofFormApp - value.Height & "}", Form1.GenControl.Name)
                _Location = New Point(_Location.X, Form1.YofFormApp - value.Height)
            End If


            _Size = value

            Form1.GenControl.Size = New Size(value)




            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Size=", "Size=" & value.ToString.Replace("Width=", Nothing).Replace(",", ";").Replace("Height=", Nothing).Replace(" ", Nothing), Form1.GenControl.Name)

            Form1.DRWTran()

        End Set
    End Property
    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter The Location Of Cnavas.")>
    Public Property Location() As Point
        Get
            Return _Location
        End Get
        Set(ByVal value As Point)
            If value.X + _Size.Width > Form1.XofFormApp AndAlso value.Y + _Size.Height > Form1.YofFormApp Then : value = New Point(0, 0)
            ElseIf value.X + _Size.Width > Form1.XofFormApp Then : value = New Point(Form1.XofFormApp - _Size.Width, value.Y)
            ElseIf value.Y + _Size.Height > Form1.YofFormApp Then : value = New Point(value.X, Form1.YofFormApp - _Size.Height)
            End If

            If value.X < 0 Then value.X = 0
            If value.Y < 0 Then value.Y = 0

            _Location = value


            Form1.GenControl.Location = New Point(value)
            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={" & value.X.ToString & ";" & value.Y.ToString & "}", Form1.GenControl.Name)

            Form1.DRWTran()
        End Set
    End Property
    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select The BackGround Color Of Cnavas.")>
    Public Property BackgroundColor() As System.Drawing.Color
        Get
            Return _BackgroundColor
        End Get
        Set(ByVal value As System.Drawing.Color)
            _BackgroundColor = value

            Form1.GenControl.BackColor = Color.FromArgb(value.ToArgb)
            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "BackgroundColor=", "BackgroundColor={" & value.A & ";" & value.R & ";" & value.G & ";" & value.B & "}", Form1.GenControl.Name)

        End Set
    End Property
    <CategoryAttribute("Common"),
    Editor(GetType(System.Windows.Forms.Design.FileNameEditor), GetType(System.Drawing.Design.UITypeEditor)),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select a BackGround Image For your Cnavas.")>
    Public Property BackgroundImage() As String
        Get
            Return _BackgroundImage
        End Get
        Set(ByVal value As String)
            If value.EndsWith(".png") Or value.EndsWith(".gif") Or value.EndsWith(".bmp") Or value.EndsWith(".jpg") Then

                If Not _BackgroundImage = Nothing Then

                    ' Try
                    Form1.GenControl.BackgroundImage.Dispose()
                    Form1.GenControl.BackgroundImage = Nothing
                    ' Catch ex As Exception
                    ' End Try

                    _BackgroundImage = Form1.CurrentHardDriver & _BackgroundImage.Remove(0, 3)
                    ' My.Computer.FileSystem.DeleteFile(_BackgroundImage)
                    '    Form1.SaveCodeProject(_BackgroundImage & vbNewLine, "imgDEL.ini", True, Encoding.Default)

                End If


                If Not My.Computer.FileSystem.FileExists(Form1.CurrentHardDriver & "VB4Android\Projects\" & Form1.FileCreatedName & "\BUILD\assets\" & value.Split("\").Last) Then My.Computer.FileSystem.CopyFile(value, Form1.CurrentHardDriver & "VB4Android\Projects\" & Form1.FileCreatedName & "\BUILD\assets\" & value.Split("\").Last)
                value = Form1.CurrentHardDriver & "VB4Android\Projects\" & Form1.FileCreatedName & "\BUILD\assets\" & value.Split("\").Last
                'Form1.SaveCodeProject(value & vbNewLine, "img.ini", True, Encoding.Default)

                Form1.changeFromFileASpecPropOfCont(Form1.CurrentHardDriver & "VB4Android\Projects\" & Form1.FileCreatedName & "\" & Form1.Combobox3.SelectedItem.ToString, "BackgroundImage=", "BackgroundImage=" & value, Form1.GenControl.Name)
                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "BackgroundImage=", "BackgroundImage=" & value, Form1.GenControl.Name)
                Form1.GenControl.BackgroundImage = System.Drawing.Image.FromFile(value)
                If Not _BackgroundImage = Nothing AndAlso Not Form1.ProjectSaveString.Contains(_BackgroundImage) AndAlso Form1.IsUsedFromAnotherProc(_BackgroundImage) = False Then My.Computer.FileSystem.DeleteFile(_BackgroundImage)

                _BackgroundImage = value
                '   Form1.AButton.Text = value
                ' value



            ElseIf value = Nothing AndAlso Not _BackgroundImage = Nothing Or value.Replace(" ", "") = Nothing AndAlso Not _BackgroundImage = Nothing Then
                Form1.GenControl.BackgroundImage.Dispose()
                Form1.GenControl.BackgroundImage = Nothing
                _BackgroundImage = Form1.CurrentHardDriver & _BackgroundImage.Remove(0, 3)
                Form1.changeFromFileASpecPropOfCont(Form1.CurrentHardDriver & "VB4Android\Projects\" & Form1.FileCreatedName & "\" & Form1.Combobox3.SelectedItem.ToString, "BackgroundImage=", "BackgroundImage=", Form1.GenControl.Name)
                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "BackgroundImage=", "BackgroundImage=", Form1.GenControl.Name)
                If Not Form1.ProjectSaveString.Contains(_BackgroundImage) AndAlso Not _BackgroundImage = Nothing AndAlso Form1.IsUsedFromAnotherProc(_BackgroundImage) = False Then My.Computer.FileSystem.DeleteFile(_BackgroundImage)
                _BackgroundImage = ""
            Else
                MsgBox("You can only Insert png, gif, bmp and jpg files", MsgBoxStyle.Critical, "Wrong type of file.")
            End If

        End Set
    End Property
    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select If The Canvas Will Be Enabled Or Not")>
    Public Property Enabled() As Boolean
        Get
            Return _Enabled
        End Get
        Set(ByVal Value As Boolean)
            _Enabled = Value

            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Enabled=", "Enabled=" & Value.ToString, Form1.GenControl.Name)

        End Set
    End Property





    <CategoryAttribute("Others"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select The Pen Color Of Cnavas.")>
    Public Property PointColor() As System.Drawing.Color
        Get
            Return _PointColor
        End Get
        Set(ByVal value As System.Drawing.Color)
            _PointColor = value

            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "PointColor=", "PointColor={" & value.A & ";" & value.R & ";" & value.G & ";" & value.B & "}", Form1.GenControl.Name)

        End Set
    End Property
End Class ' ok..








Public Class ClsCheckBox
    Public Shared _Name As String
    Public Shared _Size As Size
    Public Shared _Location As Point
    Public Shared _BackColor As System.Drawing.Color
    Public Shared _Enabled As Boolean

    Public Shared _Text As String 'Caption
    Public Shared _Font_Size As Integer
    Public Shared _bold As Boolean
    Public Shared _ForeColor As System.Drawing.Color
    Public Shared _Italic As Boolean
    Public Shared _TextAlign As String
    Public Shared _FontTypeface As String

    Public Shared _Checked As Boolean  ' Value

    '  Sub New()
    '      _Name = "asd"
    '  End Sub

    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter The Name Of CheckBox.")>
    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            For i = 0 To Form1.strClsRemove.Count - 1
                value = value.Replace(Form1.strClsRemove(i), "")
            Next
            If Not Form1.ProjectSaveString.Contains("Name=" & value & vbNewLine) Then
                Form1.Combobox1.Items.Remove(Form1.cmb)
                For item = 0 To Form1.ComboboxImage1.Items.Count
                    If Form1.ComboboxImage1.Items.Item(item).ToString = _Name Then Form1.ComboboxImage1.Items.RemoveAt(item) : Form1.ComboboxImage1.Items.Add(New ComboBoxIconItem(value, 2)) : Exit For
                Next

                If Form1.Textbox3.Text.Contains("Sub " & _Name & "_Changed(") Then
                    For Each r In Form1.Textbox3.Range.GetRanges("Sub " & _Name & "_Changed")
                        Form1.Textbox3.Selection = r
                        Form1.Textbox3.Text = Form1.Textbox3.Text.Replace(Form1.Textbox3.SelectedText, "Sub " & value & "_Changed")
                    Next
                End If

                _Name = value

                Form1.Combobox1.Items.Add(New ComboBoxIconItem(value, 2))

                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Name=", "Name=" & value, Form1.GenControl.Name)
                Form1.GenControl.Tag = _Name

                Form1.Combobox1.Text = _Name
                Form1.BuildAutocompleteMenu()
            Else

                Using New Centered_MessageBox(Form1)
                    'MsgBox(Form1.ProjectSaveString)
                    MsgBox("Name Already Exists!", vbInformation, "Exists!")
                End Using
            End If
        End Set
    End Property
    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter The Size Of CheckBox.")>
    Public Property Size() As Size
        Get
            Return _Size
        End Get
        Set(ByVal value As Size)

            If value.Width > Form1.XofFormApp Then value.Width = Form1.XofFormApp
            If value.Height > Form1.YofFormApp Then value.Height = Form1.YofFormApp

            If value.Height < 0 Then value.Height = 0
            If value.Width < 0 Then value.Width = 0

            If _Location.X + value.Width > Form1.XofFormApp AndAlso _Location.Y + value.Height > Form1.YofFormApp Then
                Form1.GenControl.Location = New Point(0, 0)
                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={0;0}", Form1.GenControl.Name)
                _Location = New Point(0, 0)
            ElseIf _Location.X + value.Width > Form1.XofFormApp Then
                Form1.GenControl.Location = New Point(Form1.XofFormApp - value.Width, _Location.Y)
                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={" & Form1.XofFormApp - value.Width & ";" & _Location.Y & "}", Form1.GenControl.Name)
                _Location = New Point(Form1.XofFormApp - value.Width, _Location.Y)
            ElseIf _Location.Y + value.Height > Form1.YofFormApp Then
                Form1.GenControl.Location = New Point(_Location.X, Form1.YofFormApp - value.Height)
                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={" & _Location.X & ";" & Form1.YofFormApp - value.Height & "}", Form1.GenControl.Name)
                _Location = New Point(_Location.X, Form1.YofFormApp - value.Height)
            End If


            _Size = value

            Form1.GenControl.Size = New Size(value)




            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Size=", "Size=" & value.ToString.Replace("Width=", Nothing).Replace(",", ";").Replace("Height=", Nothing).Replace(" ", Nothing), Form1.GenControl.Name)

            Form1.DRWTran()

        End Set
    End Property
    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter The Location Of CheckBox.")>
    Public Property Location() As Point
        Get
            Return _Location
        End Get
        Set(ByVal value As Point)
            If value.X + _Size.Width > Form1.XofFormApp AndAlso value.Y + _Size.Height > Form1.YofFormApp Then : value = New Point(0, 0)
            ElseIf value.X + _Size.Width > Form1.XofFormApp Then : value = New Point(Form1.XofFormApp - _Size.Width, value.Y)
            ElseIf value.Y + _Size.Height > Form1.YofFormApp Then : value = New Point(value.X, Form1.YofFormApp - _Size.Height)
            End If

            If value.X < 0 Then value.X = 0
            If value.Y < 0 Then value.Y = 0

            _Location = value


            Form1.GenControl.Location = New Point(value)
            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={" & value.X.ToString & ";" & value.Y.ToString & "}", Form1.GenControl.Name)

            Form1.DRWTran()

        End Set
    End Property
    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select BackColor(BackGround) Of CheckBox.")>
    Public Property BackColor() As System.Drawing.Color
        Get
            Return _BackColor
        End Get
        Set(ByVal value As System.Drawing.Color)
            _BackColor = value

            Form1.GenControl.BackColor = Color.FromArgb(value.ToArgb)
            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "BackColor=", "BackColor={" & value.A & ";" & value.R & ";" & value.G & ";" & value.B & "}", Form1.GenControl.Name)

        End Set
    End Property
    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select If The CheckBox Will Be Enabled Or Not")>
    Public Property Enabled() As Boolean
        Get
            Return _Enabled
        End Get
        Set(ByVal Value As Boolean)
            _Enabled = Value

            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Enabled=", "Enabled=" & Value.ToString, Form1.GenControl.Name)

        End Set
    End Property





    <CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter Font Integer Size For The CheckBox.")>
    Public Property FontSize() As Integer
        Get
            Return _Font_Size
        End Get
        Set(ByVal value As Integer)
            _Font_Size = value





            If _bold = True Then
                Form1.GenControl.Font = New Font(FontStyle.Bold, value)
                Form1.GenControl.Font = New Font(Form1.GenControl.Font, FontStyle.Bold)
            ElseIf _Italic = True Then
                Form1.GenControl.Font = New Font(FontStyle.Italic, value)
                Form1.GenControl.Font = New Font(Form1.GenControl.Font, FontStyle.Italic)
            Else
                Form1.GenControl.Font = New Font(FontStyle.Regular, value)
            End If


            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "FontSize=", "FontSize=" & value, Form1.GenControl.Name)



        End Set
    End Property
    <CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter Text For The CheckBox.")>
    Public Property Text() As String
        Get
            Return _Text
        End Get
        Set(ByVal value As String)
            _Text = value

            Form1.GenControl.Text = value
            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Text=", "Text=" & value, Form1.GenControl.Name)

        End Set
    End Property
    <CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select If The Text Of CheckBox Will Be Italic Or Not.")>
    Public Property Italic() As Boolean
        Get
            Return _Italic
        End Get
        Set(ByVal value As Boolean)
            _Italic = value

            If _Italic = True Then _Italic = False : Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "FontItalic=", "FontItalic=" & "False", Form1.GenControl.Name)

            If _bold = True Then
                Form1.GenControl.Font = New Font(Form1.GenControl.Font, FontStyle.Bold)
            Else
                Form1.GenControl.Font = New Font(Form1.GenControl.Font, FontStyle.Regular)
            End If


            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "FontBold=", "FontBold=" & value.ToString, Form1.GenControl.Name)


        End Set
    End Property
    <CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select The Color Of CheckBox Text.")>
    Public Property ForeColor() As System.Drawing.Color
        Get
            Return _ForeColor
        End Get
        Set(ByVal value As System.Drawing.Color)
            _ForeColor = value

            Form1.GenControl.ForeColor = Color.FromArgb(value.ToArgb)
            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "ForeColor=", "ForeColor={" & value.A & ";" & value.R & ";" & value.G & ";" & value.B & "}", Form1.GenControl.Name)

        End Set
    End Property
    <CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select If The Text Of CheckBox Will Be Bold Or Not.")>
    Public Property Bold() As Boolean
        Get
            Return _bold
        End Get
        Set(ByVal value As Boolean)
            _bold = value


            If _Italic = True Then _Italic = False : Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "FontItalic=", "FontItalic=" & "False", Form1.GenControl.Name)

            If _bold = True Then
                Form1.GenControl.Font = New Font(Form1.GenControl.Font, FontStyle.Bold)
            Else
                Form1.GenControl.Font = New Font(Form1.GenControl.Font, FontStyle.Regular)
            End If


            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "FontBold=", "FontBold=" & value.ToString, Form1.GenControl.Name)


        End Set
    End Property
    <TypeConverter(GetType(TextAlignList)),
    CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select TextAlign For CheckBox's Text.")>
    Public Property TextAlign() As String
        Get
            Return _TextAlign
        End Get
        Set(ByVal value As String)
            _TextAlign = value


            Select Case _TextAlign
                Case Is = "LEFT" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), CheckBox).TextAlign = ContentAlignment.MiddleLeft
                Case Is = "CENTER" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), CheckBox).TextAlign = ContentAlignment.MiddleCenter
                Case Is = "RIGHT" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), CheckBox).TextAlign = ContentAlignment.MiddleRight
                Case Is = "LEFT_TOP" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), CheckBox).TextAlign = ContentAlignment.TopLeft
                Case Is = "LEFT_CENTER" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), CheckBox).TextAlign = ContentAlignment.MiddleLeft
                Case Is = "LEFT_BOTTOM" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), CheckBox).TextAlign = ContentAlignment.BottomLeft
                Case Is = "CENTER_TOP" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), CheckBox).TextAlign = ContentAlignment.TopCenter
                Case Is = "CENTER_CENTER" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), CheckBox).TextAlign = ContentAlignment.MiddleCenter
                Case Is = "CENTER_BOTTOM" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), CheckBox).TextAlign = ContentAlignment.BottomCenter
                Case Is = "RIGHT_TOP" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), CheckBox).TextAlign = ContentAlignment.TopRight
                Case Is = "RIGHT_CENTER" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), CheckBox).TextAlign = ContentAlignment.MiddleRight
                Case Is = "RIGHT_BOTTOM" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), CheckBox).TextAlign = ContentAlignment.BottomRight
            End Select


            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "TextAlign=", "TextAlign=" & value.ToString, Form1.GenControl.Name)

        End Set
    End Property

    <TypeConverter(GetType(FontTypefaceList)),
    CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select The Font Type Of CheckBox's Text.")>
    Public Property FontTypeface() As String
        Get
            Return _FontTypeface
        End Get
        Set(ByVal value As String)
            _FontTypeface = value

            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "FontTypeface=", "FontTypeface=" & value.ToString, Form1.GenControl.Name)

        End Set
    End Property






    <CategoryAttribute("Others"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select If The CheckBox Will Be Enabled Or Not")>
    Public Property Checked() As Boolean
        Get
            Return _Checked
        End Get
        Set(ByVal Value As Boolean)
            _Checked = Value


            DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), CheckBox).Checked = Value
            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Checked=", "Checked=" & Value.ToString, Form1.GenControl.Name)

        End Set
    End Property
End Class ' ok..








Public Class ClsLabel
    Public Shared _Name As String
    Public Shared _Size As Size
    Public Shared _Location As Point
    Public Shared _BackColor As System.Drawing.Color
    Public Shared _Enabled As Boolean

    Public Shared _Text As String 'Caption
    Public Shared _Font_Size As Integer
    Public Shared _bold As Boolean
    Public Shared _ForeColor As System.Drawing.Color
    Public Shared _Italic As Boolean
    Public Shared _TextAlign As String
    Public Shared _FontTypeface As String


    '  Sub New()
    '      _Name = "asd"
    '  End Sub

    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter The Name Of Label.")>
    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            For i = 0 To Form1.strClsRemove.Count - 1
                value = value.Replace(Form1.strClsRemove(i), "")
            Next
            If Not Form1.ProjectSaveString.Contains("Name=" & value & vbNewLine) Then
                Form1.Combobox1.Items.Remove(Form1.cmb)
                For item = 0 To Form1.ComboboxImage1.Items.Count
                    If Form1.ComboboxImage1.Items.Item(item).ToString = _Name Then Form1.ComboboxImage1.Items.RemoveAt(item) : Form1.ComboboxImage1.Items.Add(New ComboBoxIconItem(value, 4)) : Exit For
                Next

                If Form1.Textbox3.Text.Contains("Sub " & _Name & "_Click(") Then
                    For Each r In Form1.Textbox3.Range.GetRanges("Sub " & _Name & "_Click")
                        Form1.Textbox3.Selection = r
                        Form1.Textbox3.Text = Form1.Textbox3.Text.Replace(Form1.Textbox3.SelectedText, "Sub " & value & "_Click")
                    Next
                End If

                _Name = value

                Form1.Combobox1.Items.Add(New ComboBoxIconItem(value, 4))

                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Name=", "Name=" & value, Form1.GenControl.Name)
                Form1.GenControl.Tag = _Name

                Form1.Combobox1.Text = _Name
                Form1.BuildAutocompleteMenu()
            Else

                Using New Centered_MessageBox(Form1)
                    'MsgBox(Form1.ProjectSaveString)
                    MsgBox("Name Already Exists!", vbInformation, "Exists!")
                End Using
            End If

        End Set
    End Property
    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter The Size Of Label.")>
    Public Property Size() As Size
        Get
            Return _Size
        End Get
        Set(ByVal value As Size)

            If value.Width > Form1.XofFormApp Then value.Width = Form1.XofFormApp
            If value.Height > Form1.YofFormApp Then value.Height = Form1.YofFormApp

            If value.Height < 0 Then value.Height = 0
            If value.Width < 0 Then value.Width = 0


            If _Location.X + value.Width > Form1.XofFormApp AndAlso _Location.Y + value.Height > Form1.YofFormApp Then
                Form1.GenControl.Location = New Point(0, 0)
                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={0;0}", Form1.GenControl.Name)
                _Location = New Point(0, 0)
            ElseIf _Location.X + value.Width > Form1.XofFormApp Then
                Form1.GenControl.Location = New Point(Form1.XofFormApp - value.Width, _Location.Y)
                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={" & Form1.XofFormApp - value.Width & ";" & _Location.Y & "}", Form1.GenControl.Name)
                _Location = New Point(Form1.XofFormApp - value.Width, _Location.Y)
            ElseIf _Location.Y + value.Height > Form1.YofFormApp Then
                Form1.GenControl.Location = New Point(_Location.X, Form1.YofFormApp - value.Height)
                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={" & _Location.X & ";" & Form1.YofFormApp - value.Height & "}", Form1.GenControl.Name)
                _Location = New Point(_Location.X, Form1.YofFormApp - value.Height)
            End If


            _Size = value

            Form1.GenControl.Size = New Size(value)




            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Size=", "Size=" & value.ToString.Replace("Width=", Nothing).Replace(",", ";").Replace("Height=", Nothing).Replace(" ", Nothing), Form1.GenControl.Name)

            Form1.DRWTran()

        End Set
    End Property
    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter The Location Of Label.")>
    Public Property Location() As Point
        Get
            Return _Location
        End Get
        Set(ByVal value As Point)
            If value.X + _Size.Width > Form1.XofFormApp AndAlso value.Y + _Size.Height > Form1.YofFormApp Then : value = New Point(0, 0)
            ElseIf value.X + _Size.Width > Form1.XofFormApp Then : value = New Point(Form1.XofFormApp - _Size.Width, value.Y)
            ElseIf value.Y + _Size.Height > Form1.YofFormApp Then : value = New Point(value.X, Form1.YofFormApp - _Size.Height)
            End If

            If value.X < 0 Then value.X = 0
            If value.Y < 0 Then value.Y = 0

            _Location = value


            Form1.GenControl.Location = New Point(value)
            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={" & value.X.ToString & ";" & value.Y.ToString & "}", Form1.GenControl.Name)

            Form1.DRWTran()

        End Set
    End Property
    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select BackColor(BackGround) Of Label.")>
    Public Property BackColor() As System.Drawing.Color
        Get
            Return _BackColor
        End Get
        Set(ByVal value As System.Drawing.Color)
            _BackColor = value

            Form1.GenControl.BackColor = Color.FromArgb(value.ToArgb)
            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "BackColor=", "BackColor={" & value.A & ";" & value.R & ";" & value.G & ";" & value.B & "}", Form1.GenControl.Name)

        End Set
    End Property
    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select If The Label Will Be Enabled Or Not")>
    Public Property Enabled() As Boolean
        Get
            Return _Enabled
        End Get
        Set(ByVal Value As Boolean)
            _Enabled = Value

            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Enabled=", "Enabled=" & Value.ToString, Form1.GenControl.Name)

        End Set
    End Property




    <CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter Font Integer Size For The Label.")>
    Public Property FontSize() As Integer
        Get
            Return _Font_Size
        End Get
        Set(ByVal value As Integer)
            _Font_Size = value

            If _bold = True Then
                Form1.GenControl.Font = New Font(FontStyle.Bold, value)
                Form1.GenControl.Font = New Font(Form1.GenControl.Font, FontStyle.Bold)
            ElseIf _Italic = True Then
                Form1.GenControl.Font = New Font(FontStyle.Italic, value)
                Form1.GenControl.Font = New Font(Form1.GenControl.Font, FontStyle.Italic)
            Else
                Form1.GenControl.Font = New Font(FontStyle.Regular, value)
            End If


            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "FontSize=", "FontSize=" & value, Form1.GenControl.Name)


        End Set
    End Property
    <CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter Text For The Label.")>
    Public Property Text() As String
        Get
            Return _Text
        End Get
        Set(ByVal value As String)
            _Text = value


            Form1.GenControl.Text = value
            '   MsgBox(Form1.PropertiesOfControl)
            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Text=", "Text=" & value, Form1.GenControl.Name)
            '  MsgBox(Form1.PropertiesOfControl)

        End Set
    End Property
    <CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select If The Text Of Label Will Be Italic Or Not.")>
    Public Property Italic() As Boolean
        Get
            Return _Italic
        End Get
        Set(ByVal value As Boolean)
            _Italic = value


            If _bold = True Then _bold = False : Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "FontBold=", "FontBold=" & "False", Form1.GenControl.Name)

            If _Italic = True Then
                Form1.GenControl.Font = New Font(Form1.GenControl.Font, FontStyle.Italic)
            Else
                Form1.GenControl.Font = New Font(Form1.GenControl.Font, FontStyle.Regular)
            End If

            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "FontItalic=", "FontItalic=" & value.ToString, Form1.GenControl.Name)


        End Set
    End Property
    <CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select The Color Of Label Text.")>
    Public Property ForeColor() As System.Drawing.Color
        Get
            Return _ForeColor
        End Get
        Set(ByVal value As System.Drawing.Color)
            _ForeColor = value

            Form1.GenControl.ForeColor = Color.FromArgb(value.ToArgb)
            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "ForeColor=", "ForeColor={" & value.A & ";" & value.R & ";" & value.G & ";" & value.B & "}", Form1.GenControl.Name)

        End Set
    End Property
    <CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select If The Text Of Label Will Be Bold Or Not.")>
    Public Property Bold() As Boolean
        Get
            Return _bold
        End Get
        Set(ByVal value As Boolean)
            _bold = value


            If _Italic = True Then _Italic = False : Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "FontItalic=", "FontItalic=" & "False", Form1.GenControl.Name)

            If _bold = True Then
                Form1.GenControl.Font = New Font(Form1.GenControl.Font, FontStyle.Bold)
            Else
                Form1.GenControl.Font = New Font(Form1.GenControl.Font, FontStyle.Regular)
            End If


            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "FontBold=", "FontBold=" & value.ToString, Form1.GenControl.Name)


        End Set
    End Property
    <TypeConverter(GetType(TextAlignList)),
    CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select TextAlign For Label's Text.")>
    Public Property TextAlign() As String
        Get
            Return _TextAlign
        End Get
        Set(ByVal value As String)
            _TextAlign = value

            Select Case _TextAlign
                Case Is = "LEFT" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), Label).TextAlign = ContentAlignment.MiddleLeft
                Case Is = "CENTER" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), Label).TextAlign = ContentAlignment.MiddleCenter
                Case Is = "RIGHT" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), Label).TextAlign = ContentAlignment.MiddleRight
                Case Is = "LEFT_TOP" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), Label).TextAlign = ContentAlignment.TopLeft
                Case Is = "LEFT_CENTER" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), Label).TextAlign = ContentAlignment.MiddleLeft
                Case Is = "LEFT_BOTTOM" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), Label).TextAlign = ContentAlignment.BottomLeft
                Case Is = "CENTER_TOP" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), Label).TextAlign = ContentAlignment.TopCenter
                Case Is = "CENTER_CENTER" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), Label).TextAlign = ContentAlignment.MiddleCenter
                Case Is = "CENTER_BOTTOM" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), Label).TextAlign = ContentAlignment.BottomCenter
                Case Is = "RIGHT_TOP" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), Label).TextAlign = ContentAlignment.TopRight
                Case Is = "RIGHT_CENTER" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), Label).TextAlign = ContentAlignment.MiddleRight
                Case Is = "RIGHT_BOTTOM" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), Label).TextAlign = ContentAlignment.BottomRight
            End Select

            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "TextAlign=", "TextAlign=" & value.ToString, Form1.GenControl.Name)

        End Set
    End Property
    <TypeConverter(GetType(FontTypefaceList)),
    CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select The Font Type Of Label's Text.")>
    Public Property FontTypeface() As String
        Get
            Return _FontTypeface
        End Get
        Set(ByVal value As String)
            _FontTypeface = value

            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "FontTypeface=", "FontTypeface=" & value.ToString, Form1.GenControl.Name)

        End Set
    End Property
End Class ' ok..








Public Class ClsListView
    Public Shared _Name As String
    Public Shared _Size As Size
    Public Shared _Location As Point
    Public Shared _BackColor As System.Drawing.Color
    Public Shared _Enabled As Boolean

    Public Shared _Font_Size As Integer
    Public Shared _bold As Boolean
    Public Shared _ForeColor As System.Drawing.Color
    Public Shared _Italic As Boolean
    Public Shared _TextAlign As String
    Public Shared _FontTypeface As String


    '  Sub New()
    '      _Name = "asd"
    '  End Sub

    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter The Name Of Listview.")>
    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            For i = 0 To Form1.strClsRemove.Count - 1
                value = value.Replace(Form1.strClsRemove(i), "")
            Next
            If Not Form1.ProjectSaveString.Contains("Name=" & value & vbNewLine) Then
                Form1.Combobox1.Items.Remove(Form1.cmb)
                For item = 0 To Form1.ComboboxImage1.Items.Count
                    If Form1.ComboboxImage1.Items.Item(item).ToString = _Name Then Form1.ComboboxImage1.Items.RemoveAt(item) : Form1.ComboboxImage1.Items.Add(New ComboBoxIconItem(value, 5)) : Exit For
                Next

                If Form1.Textbox3.Text.Contains("Sub " & _Name & "_ItemClicked(") Then
                    For Each r In Form1.Textbox3.Range.GetRanges("Sub " & _Name & "_ItemClicked")
                        Form1.Textbox3.Selection = r
                        Form1.Textbox3.Text = Form1.Textbox3.Text.Replace(Form1.Textbox3.SelectedText, "Sub " & value & "_ItemClicked")
                    Next
                End If

                _Name = value

                Form1.Combobox1.Items.Add(New ComboBoxIconItem(value, 5))

                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Name=", "Name=" & value, Form1.GenControl.Name)
                Form1.GenControl.Tag = _Name

                Form1.Combobox1.Text = _Name
                Form1.BuildAutocompleteMenu()
            Else

                Using New Centered_MessageBox(Form1)
                    'MsgBox(Form1.ProjectSaveString)
                    MsgBox("Name Already Exists!", vbInformation, "Exists!")
                End Using
            End If
        End Set
    End Property
    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter The Size Of Listview.")>
    Public Property Size() As Size
        Get
            Return _Size
        End Get
        Set(ByVal value As Size)


            If value.Width > Form1.XofFormApp Then value.Width = Form1.XofFormApp
            If value.Height > Form1.YofFormApp Then value.Height = Form1.YofFormApp

            If value.Height < 0 Then value.Height = 0
            If value.Width < 0 Then value.Width = 0

            If _Location.X + value.Width > Form1.XofFormApp AndAlso _Location.Y + value.Height > Form1.YofFormApp Then
                Form1.GenControl.Location = New Point(0, 0)
                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={0;0}", Form1.GenControl.Name)
                _Location = New Point(0, 0)
            ElseIf _Location.X + value.Width > Form1.XofFormApp Then
                Form1.GenControl.Location = New Point(Form1.XofFormApp - value.Width, _Location.Y)
                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={" & Form1.XofFormApp - value.Width & ";" & _Location.Y & "}", Form1.GenControl.Name)
                _Location = New Point(Form1.XofFormApp - value.Width, _Location.Y)
            ElseIf _Location.Y + value.Height > Form1.YofFormApp Then
                Form1.GenControl.Location = New Point(_Location.X, Form1.YofFormApp - value.Height)
                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={" & _Location.X & ";" & Form1.YofFormApp - value.Height & "}", Form1.GenControl.Name)
                _Location = New Point(_Location.X, Form1.YofFormApp - value.Height)
            End If


            _Size = value

            Form1.GenControl.Size = New Size(value)




            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Size=", "Size=" & value.ToString.Replace("Width=", Nothing).Replace(",", ";").Replace("Height=", Nothing).Replace(" ", Nothing), Form1.GenControl.Name)

            Form1.DRWTran()

        End Set
    End Property
    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter The Location Of Listview.")>
    Public Property Location() As Point
        Get
            Return _Location
        End Get
        Set(ByVal value As Point)
            If value.X + _Size.Width > Form1.XofFormApp AndAlso value.Y + _Size.Height > Form1.YofFormApp Then : value = New Point(0, 0)
            ElseIf value.X + _Size.Width > Form1.XofFormApp Then : value = New Point(Form1.XofFormApp - _Size.Width, value.Y)
            ElseIf value.Y + _Size.Height > Form1.YofFormApp Then : value = New Point(value.X, Form1.YofFormApp - _Size.Height)
            End If

            If value.X < 0 Then value.X = 0
            If value.Y < 0 Then value.Y = 0

            _Location = value


            Form1.GenControl.Location = New Point(value)
            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={" & value.X.ToString & ";" & value.Y.ToString & "}", Form1.GenControl.Name)

            Form1.DRWTran()

        End Set
    End Property
    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select BackColor(BackGround) Of Listview.")>
    Public Property BackColor() As System.Drawing.Color
        Get
            Return _BackColor
        End Get
        Set(ByVal value As System.Drawing.Color)
            _BackColor = value

            Form1.GenControl.BackColor = Color.FromArgb(value.ToArgb)
            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "BackColor=", "BackColor={" & value.A & ";" & value.R & ";" & value.G & ";" & value.B & "}", Form1.GenControl.Name)

        End Set
    End Property
    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select If The Listview Will Be Enabled Or Not")>
    Public Property Enabled() As Boolean
        Get
            Return _Enabled
        End Get
        Set(ByVal Value As Boolean)
            _Enabled = Value

            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Enabled=", "Enabled=" & Value.ToString, Form1.GenControl.Name)

        End Set
    End Property






    <CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter Font Integer Size For The Listview.")>
    Public Property FontSize() As Single
        Get
            Return _Font_Size
        End Get
        Set(ByVal value As Single)
            _Font_Size = value


            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "FontSize=", "FontSize=" & value, Form1.GenControl.Name)
        End Set
    End Property

    <CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select If The Text Of Listview Will Be Italic Or Not.")>
    Public Property Italic() As Boolean
        Get
            Return _Italic
        End Get
        Set(ByVal value As Boolean)
            _Italic = value
            If _bold = True Then _bold = False : Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "FontBold=", "FontBold=" & "False", Form1.GenControl.Name)
            'If _bold = True Then
            '    Form1.GenControl.Font = New Font(FontStyle.Bold, value)
            '    Form1.GenControl.Font = New Font(Form1.GenControl.Font, FontStyle.Bold)
            'ElseIf _Italic = True Then
            '    Form1.GenControl.Font = New Font(FontStyle.Italic, value)
            '    Form1.GenControl.Font = New Font(Form1.GenControl.Font, FontStyle.Italic)
            'Else
            '    Form1.GenControl.Font = New Font(FontStyle.Regular, value)
            'End If


            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "FontItalic=", "FontItalic=" & value, Form1.GenControl.Name)


        End Set
    End Property
    <CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select The Color Of Listview Text.")>
    Public Property ForeColor() As System.Drawing.Color
        Get
            Return _ForeColor
        End Get
        Set(ByVal value As System.Drawing.Color)
            _ForeColor = value

            'Form1.GenControl.ForeColor = Color.FromArgb(value.ToArgb)
            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "ForeColor=", "ForeColor={" & value.A & ";" & value.R & ";" & value.G & ";" & value.B & "}", Form1.GenControl.Name)

        End Set
    End Property
    <CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select If The Text Of Listview Will Be Bold Or Not.")>
    Public Property Bold() As Boolean
        Get
            Return _bold
        End Get
        Set(ByVal value As Boolean)
            _bold = value

            If _Italic = True Then _Italic = False : Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "FontItalic=", "FontItalic=" & "False", Form1.GenControl.Name)

            'If _bold = True Then
            '    Form1.GenControl.Font = New Font(Form1.GenControl.Font, FontStyle.Bold)
            'Else
            '    Form1.GenControl.Font = New Font(Form1.GenControl.Font, FontStyle.Regular)
            'End If


            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "FontBold=", "FontBold=" & value.ToString, Form1.GenControl.Name)


        End Set
    End Property
    <TypeConverter(GetType(TextAlignList)),
    CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select TextAlign For Listview's Text.")>
    Public Property TextAlign() As String
        Get
            Return _TextAlign
        End Get
        Set(ByVal value As String)
            _TextAlign = value



            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "TextAlign=", "TextAlign=" & value.ToString, Form1.GenControl.Name)

        End Set
    End Property
    <TypeConverter(GetType(FontTypefaceList)),
    CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select The Font Type Of Listview's Text.")>
    Public Property FontTypeface() As String
        Get
            Return _FontTypeface
        End Get
        Set(ByVal value As String)
            _FontTypeface = value

            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "FontTypeface=", "FontTypeface=" & value.ToString, Form1.GenControl.Name)

        End Set
    End Property
End Class ' ok..








Public Class clsPanel
    '  Dim Layout 8elei 4 simia so einai ligo diskolo kai 8a to afisw pros to parwn
    Public Shared _Name As String
    Public Shared _BackgroundColor As System.Drawing.Color
    Public Shared _BackgroundImage As String
    Public Shared _Location As Point
    Public Shared _Size As Size
    Public Shared _Enabled As Boolean


    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter The Name Of Panel.")>
    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            For i = 0 To Form1.strClsRemove.Count - 1
                value = value.Replace(Form1.strClsRemove(i), "")
            Next
            If Not Form1.ProjectSaveString.Contains("Name=" & value & vbNewLine) Then
                Form1.Combobox1.Items.Remove(Form1.cmb)
                For item = 0 To Form1.ComboboxImage1.Items.Count
                    If Form1.ComboboxImage1.Items.Item(item).ToString = _Name Then Form1.ComboboxImage1.Items.RemoveAt(item) : Form1.ComboboxImage1.Items.Add(New ComboBoxIconItem(value, 6)) : Exit For
                Next


                _Name = value

                Form1.Combobox1.Items.Add(New ComboBoxIconItem(value, 6))

                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Name=", "Name=" & value, Form1.GenControl.Name)
                Form1.GenControl.Tag = _Name

                Form1.Combobox1.Text = _Name
                Form1.BuildAutocompleteMenu()
            Else

                Using New Centered_MessageBox(Form1)
                    'MsgBox(Form1.ProjectSaveString)
                    MsgBox("Name Already Exists!", vbInformation, "Exists!")
                End Using
            End If
        End Set
    End Property
    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter The Size Of Panel.")>
    Public Property Size() As Size
        Get
            Return _Size
        End Get
        Set(ByVal value As Size)


            If value.Width > Form1.XofFormApp Then value.Width = Form1.XofFormApp
            If value.Height > Form1.YofFormApp Then value.Height = Form1.YofFormApp

            If value.Height < 0 Then value.Height = 0
            If value.Width < 0 Then value.Width = 0

            If _Location.X + value.Width > Form1.XofFormApp AndAlso _Location.Y + value.Height > Form1.YofFormApp Then
                Form1.GenControl.Location = New Point(0, 0)
                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={0;0}", Form1.GenControl.Name)
                _Location = New Point(0, 0)
            ElseIf _Location.X + value.Width > Form1.XofFormApp Then
                Form1.GenControl.Location = New Point(Form1.XofFormApp - value.Width, _Location.Y)
                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={" & Form1.XofFormApp - value.Width & ";" & _Location.Y & "}", Form1.GenControl.Name)
                _Location = New Point(Form1.XofFormApp - value.Width, _Location.Y)
            ElseIf _Location.Y + value.Height > Form1.YofFormApp Then
                Form1.GenControl.Location = New Point(_Location.X, Form1.YofFormApp - value.Height)
                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={" & _Location.X & ";" & Form1.YofFormApp - value.Height & "}", Form1.GenControl.Name)
                _Location = New Point(_Location.X, Form1.YofFormApp - value.Height)
            End If


            _Size = value

            Form1.GenControl.Size = New Size(value)




            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Size=", "Size=" & value.ToString.Replace("Width=", Nothing).Replace(",", ";").Replace("Height=", Nothing).Replace(" ", Nothing), Form1.GenControl.Name)

            Form1.DRWTran()

        End Set
    End Property
    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter The Location Of Panel.")>
    Public Property Location() As Point
        Get
            Return _Location
        End Get
        Set(ByVal value As Point)
            If value.X + _Size.Width > Form1.XofFormApp AndAlso value.Y + _Size.Height > Form1.YofFormApp Then : value = New Point(0, 0)
            ElseIf value.X + _Size.Width > Form1.XofFormApp Then : value = New Point(Form1.XofFormApp - _Size.Width, value.Y)
            ElseIf value.Y + _Size.Height > Form1.YofFormApp Then : value = New Point(value.X, Form1.YofFormApp - _Size.Height)
            End If

            If value.X < 0 Then value.X = 0
            If value.Y < 0 Then value.Y = 0

            _Location = value


            Form1.GenControl.Location = New Point(value)
            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={" & value.X.ToString & ";" & value.Y.ToString & "}", Form1.GenControl.Name)

            Form1.DRWTran()

        End Set
    End Property
    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select BackColor(BackGround) Of Panel.")>
    Public Property BackColor() As System.Drawing.Color
        Get
            Return _BackgroundColor
        End Get
        Set(ByVal value As System.Drawing.Color)
            _BackgroundColor = value

            Form1.GenControl.BackColor = Color.FromArgb(value.ToArgb)
            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "BackgroundColor=", "BackgroundColor={" & value.A & ";" & value.R & ";" & value.G & ";" & value.B & "}", Form1.GenControl.Name)

        End Set
    End Property
    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select If The Panel Will Be Enabled Or Not")>
    Public Property Enabled() As Boolean
        Get
            Return _Enabled
        End Get
        Set(ByVal Value As Boolean)
            _Enabled = Value

            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Enabled=", "Enabled=" & Value.ToString, Form1.GenControl.Name)

        End Set
    End Property
    <CategoryAttribute("Others"),
    Editor(GetType(System.Windows.Forms.Design.FileNameEditor), GetType(System.Drawing.Design.UITypeEditor)),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select BackGround Image For Panel.")>
    Public Property BackgroundImage() As String
        Get
            Return _BackgroundImage
        End Get
        Set(ByVal Value As String)
            If Value.EndsWith(".png") Or Value.EndsWith(".gif") Or Value.EndsWith(".bmp") Or Value.EndsWith(".jpg") Then

                If Not _BackgroundImage = Nothing Then

                    ' Try
                    Form1.GenControl.BackgroundImage.Dispose()
                    Form1.GenControl.BackgroundImage = Nothing
                    ' Catch ex As Exception
                    ' End Try

                    _BackgroundImage = Form1.CurrentHardDriver & _BackgroundImage.Remove(0, 3) ' this will be added when it loads the project but why not here also xD (i boring to remove it and i am scared of having any error)
                    ' My.Computer.FileSystem.DeleteFile(_BackgroundImage)
                    '    Form1.SaveCodeProject(_BackgroundImage & vbNewLine, "imgDEL.ini", True, Encoding.Default)
                End If

                If Not My.Computer.FileSystem.FileExists(Form1.CurrentHardDriver & "VB4Android\Projects\" & Form1.FileCreatedName & "\BUILD\assets\" & Value.Split("\").Last) Then My.Computer.FileSystem.CopyFile(Value, Form1.CurrentHardDriver & "VB4Android\Projects\" & Form1.FileCreatedName & "\BUILD\assets\" & Value.Split("\").Last)
                Value = Form1.CurrentHardDriver & "VB4Android\Projects\" & Form1.FileCreatedName & "\BUILD\assets\" & Value.Split("\").Last
                'Form1.SaveCodeProject(value & vbNewLine, "img.ini", True, Encoding.Default)

                Form1.changeFromFileASpecPropOfCont(Form1.CurrentHardDriver & "VB4Android\Projects\" & Form1.FileCreatedName & "\" & Form1.Combobox3.SelectedItem.ToString, "BackgroundImage=", "BackgroundImage=" & Value, Form1.GenControl.Name)
                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "BackgroundImage=", "BackgroundImage=" & Value, Form1.GenControl.Name)
                Form1.GenControl.BackgroundImage = System.Drawing.Image.FromFile(Value)
                If Not _BackgroundImage = Nothing AndAlso Not Form1.ProjectSaveString.Contains(_BackgroundImage) AndAlso Form1.IsUsedFromAnotherProc(_BackgroundImage) = False Then My.Computer.FileSystem.DeleteFile(_BackgroundImage)

                _BackgroundImage = Value
                '   Form1.AButton.Text = value
                ' value



            ElseIf Value = Nothing AndAlso Not _BackgroundImage = Nothing Or Value.Replace(" ", "") = Nothing AndAlso Not _BackgroundImage = Nothing Then
                Form1.GenControl.BackgroundImage.Dispose()
                Form1.GenControl.BackgroundImage = Nothing
                _BackgroundImage = Form1.CurrentHardDriver & _BackgroundImage.Remove(0, 3)
                Form1.changeFromFileASpecPropOfCont(Form1.CurrentHardDriver & "VB4Android\Projects\" & Form1.FileCreatedName & "\" & Form1.Combobox3.SelectedItem.ToString, "BackgroundImage=", "BackgroundImage=", Form1.GenControl.Name)
                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "BackgroundImage=", "BackgroundImage=", Form1.GenControl.Name)
                If Not Form1.ProjectSaveString.Contains(_BackgroundImage) AndAlso Not _BackgroundImage = Nothing AndAlso Form1.IsUsedFromAnotherProc(_BackgroundImage) = False Then My.Computer.FileSystem.DeleteFile(_BackgroundImage)
                _BackgroundImage = ""
            Else
                MsgBox("You can only Insert png, gif, bmp and jpg files", MsgBoxStyle.Critical, "Wrong type of file.")
            End If

        End Set
    End Property
End Class ' Semi-Ok!..








Public Class clsPasswordTextBox
    Public Shared _Name As String
    Public Shared _Size As Size
    Public Shared _Location As Point
    Public Shared _BackColor As System.Drawing.Color
    Public Shared _Enabled As Boolean

    Public Shared _Text As String 'Caption
    Public Shared _Font_Size As Integer
    Public Shared _bold As Boolean
    Public Shared _ForeColor As System.Drawing.Color
    Public Shared _Italic As Boolean
    Public Shared _TextAlign As String
    Public Shared _FontTypeface As String

    Public Shared _Hint As String

    '  Sub New()
    '      _Name = "asd"
    '  End Sub

    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter The Name Of PasswordTextBox.")>
    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            For i = 0 To Form1.strClsRemove.Count - 1
                value = value.Replace(Form1.strClsRemove(i), "")
            Next
            If Not Form1.ProjectSaveString.Contains("Name=" & value & vbNewLine) Then
                Form1.Combobox1.Items.Remove(Form1.cmb)
                For item = 0 To Form1.ComboboxImage1.Items.Count
                    If Form1.ComboboxImage1.Items.Item(item).ToString = _Name Then Form1.ComboboxImage1.Items.RemoveAt(item) : Form1.ComboboxImage1.Items.Add(New ComboBoxIconItem(value, 7)) : Exit For
                Next

                If Form1.Textbox3.Text.Contains("Sub " & _Name & "_GotFocus(") Then
                    For Each r In Form1.Textbox3.Range.GetRanges("Sub " & _Name & "_GotFocus")
                        Form1.Textbox3.Selection = r
                        Form1.Textbox3.Text = Form1.Textbox3.Text.Replace(Form1.Textbox3.SelectedText, "Sub " & value & "_GotFocus")
                    Next
                End If

                _Name = value

                Form1.Combobox1.Items.Add(New ComboBoxIconItem(value, 7))

                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Name=", "Name=" & value, Form1.GenControl.Name)
                Form1.GenControl.Tag = _Name

                Form1.Combobox1.Text = _Name
                Form1.BuildAutocompleteMenu()
            Else

                Using New Centered_MessageBox(Form1)
                    'MsgBox(Form1.ProjectSaveString)
                    MsgBox("Name Already Exists!", vbInformation, "Exists!")
                End Using
            End If
        End Set
    End Property
    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter The Size Of PasswordTextBox.")>
    Public Property Size() As Size
        Get
            Return _Size
        End Get
        Set(ByVal value As Size)


            If value.Width > Form1.XofFormApp Then value.Width = Form1.XofFormApp
            If value.Height > Form1.YofFormApp Then value.Height = Form1.YofFormApp

            If value.Height < 0 Then value.Height = 0
            If value.Width < 0 Then value.Width = 0

            If _Location.X + value.Width > Form1.XofFormApp AndAlso _Location.Y + value.Height > Form1.YofFormApp Then
                Form1.GenControl.Location = New Point(0, 0)
                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={0;0}", Form1.GenControl.Name)
                _Location = New Point(0, 0)
            ElseIf _Location.X + value.Width > Form1.XofFormApp Then
                Form1.GenControl.Location = New Point(Form1.XofFormApp - value.Width, _Location.Y)
                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={" & Form1.XofFormApp - value.Width & ";" & _Location.Y & "}", Form1.GenControl.Name)
                _Location = New Point(Form1.XofFormApp - value.Width, _Location.Y)
            ElseIf _Location.Y + value.Height > Form1.YofFormApp Then
                Form1.GenControl.Location = New Point(_Location.X, Form1.YofFormApp - value.Height)
                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={" & _Location.X & ";" & Form1.YofFormApp - value.Height & "}", Form1.GenControl.Name)
                _Location = New Point(_Location.X, Form1.YofFormApp - value.Height)
            End If


            _Size = value

            Form1.GenControl.Size = New Size(value)




            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Size=", "Size=" & value.ToString.Replace("Width=", Nothing).Replace(",", ";").Replace("Height=", Nothing).Replace(" ", Nothing), Form1.GenControl.Name)
            Form1.DRWTran()
        End Set
    End Property
    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter The Location Of PasswordTextBox.")>
    Public Property Location() As Point
        Get
            Return _Location
        End Get
        Set(ByVal value As Point)
            If value.X + _Size.Width > Form1.XofFormApp AndAlso value.Y + _Size.Height > Form1.YofFormApp Then : value = New Point(0, 0)
            ElseIf value.X + _Size.Width > Form1.XofFormApp Then : value = New Point(Form1.XofFormApp - _Size.Width, value.Y)
            ElseIf value.Y + _Size.Height > Form1.YofFormApp Then : value = New Point(value.X, Form1.YofFormApp - _Size.Height)
            End If

            If value.X < 0 Then value.X = 0
            If value.Y < 0 Then value.Y = 0

            _Location = value


            Form1.GenControl.Location = New Point(value)
            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={" & value.X.ToString & ";" & value.Y.ToString & "}", Form1.GenControl.Name)
            Form1.DRWTran()
        End Set
    End Property
    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select BackColor(BackGround) Of PasswordTextBox.")>
    Public Property BackColor() As System.Drawing.Color
        Get
            Return _BackColor
        End Get
        Set(ByVal value As System.Drawing.Color)
            _BackColor = value

            Form1.GenControl.BackColor = Color.FromArgb(value.ToArgb)
            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "BackColor=", "BackColor={" & value.A & ";" & value.R & ";" & value.G & ";" & value.B & "}", Form1.GenControl.Name)

        End Set
    End Property
    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select If The PasswordTextBox Will Be Enabled Or Not")>
    Public Property Enabled() As Boolean
        Get
            Return _Enabled
        End Get
        Set(ByVal Value As Boolean)
            _Enabled = Value

            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Enabled=", "Enabled=" & Value.ToString, Form1.GenControl.Name)

        End Set
    End Property






    <CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter Font Integer Size For The PasswordTextBox.")>
    Public Property FontSize() As Integer
        Get
            Return _Font_Size
        End Get
        Set(ByVal value As Integer)
            _Font_Size = value

            If _bold = True Then
                Form1.GenControl.Font = New Font(FontStyle.Bold, value)
                Form1.GenControl.Font = New Font(Form1.GenControl.Font, FontStyle.Bold)
            ElseIf _Italic = True Then
                Form1.GenControl.Font = New Font(FontStyle.Italic, value)
                Form1.GenControl.Font = New Font(Form1.GenControl.Font, FontStyle.Italic)
            Else
                Form1.GenControl.Font = New Font(FontStyle.Regular, value)
            End If


            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "FontSize=", "FontSize=" & value, Form1.GenControl.Name)


        End Set
    End Property
    <CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter Text For The PasswordTextBox.")>
    Public Property Text() As String
        Get
            Return _Text
        End Get
        Set(ByVal value As String)
            _Text = value

            Form1.GenControl.Text = value
            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Text=", "Text=" & value, Form1.GenControl.Name)

        End Set
    End Property
    <CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select If The Text Of PasswordTextBox Will Be Italic Or Not.")>
    Public Property Italic() As Boolean
        Get
            Return _Italic
        End Get
        Set(ByVal value As Boolean)
            _Italic = value

            If _bold = True Then _bold = False : Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "FontBold=", "FontBold=" & "False", Form1.GenControl.Name)

            If _Italic = True Then
                Form1.GenControl.Font = New Font(Form1.GenControl.Font, FontStyle.Italic)
            Else
                Form1.GenControl.Font = New Font(Form1.GenControl.Font, FontStyle.Regular)
            End If

            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "FontItalic=", "FontItalic=" & value.ToString, Form1.GenControl.Name)

        End Set
    End Property
    <CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select The Color Of PasswordTextBox Text.")>
    Public Property ForeColor() As System.Drawing.Color
        Get
            Return _ForeColor
        End Get
        Set(ByVal value As System.Drawing.Color)
            _ForeColor = value

            Form1.GenControl.ForeColor = Color.FromArgb(value.ToArgb)
            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "ForeColor=", "ForeColor={" & value.A & ";" & value.R & ";" & value.G & ";" & value.B & "}", Form1.GenControl.Name)

        End Set
    End Property
    <CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select If The Text Of PasswordTextBox Will Be Bold Or Not.")>
    Public Property Bold() As Boolean
        Get
            Return _bold
        End Get
        Set(ByVal value As Boolean)
            _bold = value

            If _Italic = True Then _Italic = False : Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "FontItalic=", "FontItalic=" & "False", Form1.GenControl.Name)

            If _bold = True Then
                Form1.GenControl.Font = New Font(Form1.GenControl.Font, FontStyle.Bold)
            Else
                Form1.GenControl.Font = New Font(Form1.GenControl.Font, FontStyle.Regular)
            End If


            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "FontBold=", "FontBold=" & value.ToString, Form1.GenControl.Name)

        End Set
    End Property
    <TypeConverter(GetType(TextAlignList)),
    CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select TextAlign For PasswordTextBox's Text.")>
    Public Property TextAlign() As String
        Get
            Return _TextAlign
        End Get
        Set(ByVal value As String)
            _TextAlign = value


            Select Case _TextAlign
                'Case Is = "LEFT" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), TextBox).TextAlign = ContentAlignment.MiddleLeft
                ' Case Is = "CENTER" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), TextBox).TextAlign = ContentAlignment.Center
                ' Case Is = "RIGHT" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), TextBox).TextAlign = ContentAlignment.MiddleRight
                Case Is = "LEFT_TOP" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), TextBox).TextAlign = HorizontalAlignment.Left ' https://social.msdn.microsoft.com/forums/windows/en-us/712d4df4-64e4-4748-8bff-6b9ed0db46fb/textbox-text-vertical-alignment
                ' Case Is = "LEFT_CENTER" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), TextBox).TextAlign = ContentAlignment.MiddleLeft
                ' Case Is = "LEFT_BOTTOM" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), TextBox).TextAlign = ContentAlignment.BottomLeft
                Case Is = "CENTER_TOP" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), TextBox).TextAlign = HorizontalAlignment.Center
                ' Case Is = "CENTER_CENTER" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), TextBox).TextAlign = ContentAlignment.MiddleCenter
                '  Case Is = "CENTER_BOTTOM" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), TextBox).TextAlign = ContentAlignment.BottomCenter
                Case Is = "RIGHT_TOP" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), TextBox).TextAlign = HorizontalAlignment.Right
                    ' Case Is = "RIGHT_CENTER" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), TextBox).TextAlign = ContentAlignment.MiddleRight
                    ' Case Is = "RIGHT_BOTTOM" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), TextBox).TextAlign = ContentAlignment.BottomRight
            End Select

            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "TextAlign=", "TextAlign=" & value, Form1.GenControl.Name)
        End Set
    End Property
    <TypeConverter(GetType(FontTypefaceList)),
    CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select The Font Type Of PasswordTextBox's Text.")>
    Public Property FontTypeface() As String
        Get
            Return _FontTypeface
        End Get
        Set(ByVal value As String)
            _FontTypeface = value
            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "FontTypeface=", "FontTypeface=" & value, Form1.GenControl.Name)
        End Set
    End Property





    <CategoryAttribute("Others"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute(" Hint is a gray text shown when nothing inputs , Select If Your PasswordTextBox will have Or Not By leaving it blank.")>
    Public Property Hint() As String
        Get
            Return _Hint
        End Get
        Set(ByVal value As String)
            _Hint = value
            '   Form1.AButton.Text = value
            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Hint=", "Hint=" & value, Form1.GenControl.Name)
        End Set
    End Property

End Class ' ok..








Public Class clsPictureBox
    Public Shared _Name As String
    Public Shared _BackColor As System.Drawing.Color
    Public Shared _Image As String
    Public Shared _Location As Point
    Public Shared _Size As Size
    Public Shared _Enabled As Boolean



    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter The Name Of PictureBox.")>
    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            For i = 0 To Form1.strClsRemove.Count - 1
                value = value.Replace(Form1.strClsRemove(i), "")
            Next
            If Not Form1.ProjectSaveString.Contains("Name=" & value & vbNewLine) Then
                Form1.Combobox1.Items.Remove(Form1.cmb)
                For item = 0 To Form1.ComboboxImage1.Items.Count
                    If Form1.ComboboxImage1.Items.Item(item).ToString = _Name Then Form1.ComboboxImage1.Items.RemoveAt(item) : Form1.ComboboxImage1.Items.Add(New ComboBoxIconItem(value, 8)) : Exit For
                Next

                If Form1.Textbox3.Text.Contains("Sub " & _Name & "_Click(") Then
                    For Each r In Form1.Textbox3.Range.GetRanges("Sub " & _Name & "_Click")
                        Form1.Textbox3.Selection = r
                        Form1.Textbox3.Text = Form1.Textbox3.Text.Replace(Form1.Textbox3.SelectedText, "Sub " & value & "_Click")
                    Next
                End If

                _Name = value

                Form1.Combobox1.Items.Add(New ComboBoxIconItem(value, 8))

                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Name=", "Name=" & value, Form1.GenControl.Name)
                Form1.GenControl.Tag = _Name

                Form1.Combobox1.Text = _Name
                Form1.BuildAutocompleteMenu()
            Else

                Using New Centered_MessageBox(Form1)
                    'MsgBox(Form1.ProjectSaveString)
                    MsgBox("Name Already Exists!", vbInformation, "Exists!")
                End Using
            End If
        End Set
    End Property
    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter The Size Of PictureBox.")>
    Public Property Size() As Size
        Get
            Return _Size
        End Get
        Set(ByVal value As Size)


            If value.Width > Form1.XofFormApp Then value.Width = Form1.XofFormApp
            If value.Height > Form1.YofFormApp Then value.Height = Form1.YofFormApp

            If value.Height < 0 Then value.Height = 0
            If value.Width < 0 Then value.Width = 0

            If _Location.X + value.Width > Form1.XofFormApp AndAlso _Location.Y + value.Height > Form1.YofFormApp Then
                Form1.GenControl.Location = New Point(0, 0)
                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={0;0}", Form1.GenControl.Name)
                _Location = New Point(0, 0)
            ElseIf _Location.X + value.Width > Form1.XofFormApp Then
                Form1.GenControl.Location = New Point(Form1.XofFormApp - value.Width, _Location.Y)
                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={" & Form1.XofFormApp - value.Width & ";" & _Location.Y & "}", Form1.GenControl.Name)
                _Location = New Point(Form1.XofFormApp - value.Width, _Location.Y)
            ElseIf _Location.Y + value.Height > Form1.YofFormApp Then
                Form1.GenControl.Location = New Point(_Location.X, Form1.YofFormApp - value.Height)
                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={" & _Location.X & ";" & Form1.YofFormApp - value.Height & "}", Form1.GenControl.Name)
                _Location = New Point(_Location.X, Form1.YofFormApp - value.Height)
            End If


            _Size = value

            Form1.GenControl.Size = New Size(value)




            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Size=", "Size=" & value.ToString.Replace("Width=", Nothing).Replace(",", ";").Replace("Height=", Nothing).Replace(" ", Nothing), Form1.GenControl.Name)
            Form1.DRWTran()
        End Set
    End Property
    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter The Location Of PictureBox.")>
    Public Property Location() As Point
        Get
            Return _Location
        End Get
        Set(ByVal value As Point)
            If value.X + _Size.Width > Form1.XofFormApp AndAlso value.Y + _Size.Height > Form1.YofFormApp Then : value = New Point(0, 0)
            ElseIf value.X + _Size.Width > Form1.XofFormApp Then : value = New Point(Form1.XofFormApp - _Size.Width, value.Y)
            ElseIf value.Y + _Size.Height > Form1.YofFormApp Then : value = New Point(value.X, Form1.YofFormApp - _Size.Height)
            End If

            If value.X < 0 Then value.X = 0
            If value.Y < 0 Then value.Y = 0

            _Location = value


            Form1.GenControl.Location = New Point(value)
            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={" & value.X.ToString & ";" & value.Y.ToString & "}", Form1.GenControl.Name)
            Form1.DRWTran()
        End Set
    End Property
    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select BackColor(BackGround) Of PictureBox.")>
    Public Property BackColor() As System.Drawing.Color
        Get
            Return _BackColor
        End Get
        Set(ByVal value As System.Drawing.Color)
            _BackColor = value

            Form1.GenControl.BackColor = Color.FromArgb(value.ToArgb)
            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "BackColor=", "BackColor={" & value.A & ";" & value.R & ";" & value.G & ";" & value.B & "}", Form1.GenControl.Name)

        End Set
    End Property
    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select If The PictureBox Will Be Enabled Or Not")>
    Public Property Enabled() As Boolean
        Get
            Return _Enabled
        End Get
        Set(ByVal Value As Boolean)
            _Enabled = Value

            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Enabled=", "Enabled=" & Value.ToString, Form1.GenControl.Name)

        End Set
    End Property
    <CategoryAttribute("Others"),
    Editor(GetType(System.Windows.Forms.Design.FileNameEditor), GetType(System.Drawing.Design.UITypeEditor)),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select Image For Your PictureBox.")>
    Public Property BackgroundImage() As String
        Get
            Return _Image
        End Get
        Set(ByVal Value As String)
            If Value.EndsWith(".png") Or Value.EndsWith(".gif") Or Value.EndsWith(".bmp") Or Value.EndsWith(".jpg") Then

                If Not _Image = Nothing Then

                    ' Try
                    Form1.GenControl.BackgroundImage.Dispose()
                    Form1.GenControl.BackgroundImage = Nothing
                    ' Catch ex As Exception
                    ' End Try

                    _Image = Form1.CurrentHardDriver & _Image.Remove(0, 3)
                    'My.Computer.FileSystem.DeleteFile(_Image)
                    '    Form1.SaveCodeProject(_Image & vbNewLine, "imgDEL.ini", True, Encoding.Default)
                End If

                If Not My.Computer.FileSystem.FileExists(Form1.CurrentHardDriver & "VB4Android\Projects\" & Form1.FileCreatedName & "\BUILD\assets\" & Value.Split("\").Last) Then My.Computer.FileSystem.CopyFile(Value, Form1.CurrentHardDriver & "VB4Android\Projects\" & Form1.FileCreatedName & "\BUILD\assets\" & Value.Split("\").Last)
                Value = Form1.CurrentHardDriver & "VB4Android\Projects\" & Form1.FileCreatedName & "\BUILD\assets\" & Value.Split("\").Last
                'Form1.SaveCodeProject(value & vbNewLine, "img.ini", True, Encoding.Default)

                Form1.changeFromFileASpecPropOfCont(Form1.CurrentHardDriver & "VB4Android\Projects\" & Form1.FileCreatedName & "\" & Form1.Combobox3.SelectedItem.ToString, "Image=", "Image=" & Value, Form1.GenControl.Name)
                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Image=", "Image=" & Value, Form1.GenControl.Name)
                Form1.GenControl.BackgroundImage = System.Drawing.Image.FromFile(Value)
                If Not _Image = Nothing AndAlso Not Form1.ProjectSaveString.Contains(_Image) AndAlso Form1.IsUsedFromAnotherProc(_Image) = False Then My.Computer.FileSystem.DeleteFile(_Image)

                _Image = Value
                '   Form1.AButton.Text = value
                ' value
                'Form1.GenControl.BackgroundImage = System.Drawing.Image.FromFile(Value)



                ' MsgBox(value)
                'Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Image=", "Image=" & Value.ToString, Form1.GenControl.Name)


            ElseIf Value = Nothing AndAlso Not _Image = Nothing Or Value.Replace(" ", "") = Nothing AndAlso Not _Image = Nothing Then
                Form1.GenControl.BackgroundImage.Dispose()
                Form1.GenControl.BackgroundImage = Nothing
                _Image = Form1.CurrentHardDriver & _Image.Remove(0, 3)
                Form1.changeFromFileASpecPropOfCont(Form1.CurrentHardDriver & "VB4Android\Projects\" & Form1.FileCreatedName & "\" & Form1.Combobox3.SelectedItem.ToString, "Image=", "Image=", Form1.GenControl.Name)
                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Image=", "Image=", Form1.GenControl.Name)
                If Not Form1.ProjectSaveString.Contains(_Image) AndAlso Not _Image = Nothing AndAlso Form1.IsUsedFromAnotherProc(_Image) = False Then My.Computer.FileSystem.DeleteFile(_Image)
                _Image = ""
            Else
                MsgBox("You can only Insert png, gif, bmp and jpg files", MsgBoxStyle.Critical, "Wrong type of file.")
            End If

        End Set
    End Property
End Class ' Ok..








Public Class clsProgressBar
    Public Shared _Name As String
    Public Shared _Size As Size
    Public Shared _Location As Point
    Public Shared _Enabled As Boolean


    Public Shared _Value As Integer

    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter The Name Of ProgressBar.")>
    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            For i = 0 To Form1.strClsRemove.Count - 1
                value = value.Replace(Form1.strClsRemove(i), "")
            Next
            If Not Form1.ProjectSaveString.Contains("Name=" & value & vbNewLine) Then
                Form1.Combobox1.Items.Remove(Form1.cmb)
                For item = 0 To Form1.ComboboxImage1.Items.Count
                    If Form1.ComboboxImage1.Items.Item(item).ToString = _Name Then Form1.ComboboxImage1.Items.RemoveAt(item) : Form1.ComboboxImage1.Items.Add(New ComboBoxIconItem(value, 9)) : Exit For
                Next
                _Name = value

                Form1.Combobox1.Items.Add(New ComboBoxIconItem(value, 9))

                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Name=", "Name=" & value, Form1.GenControl.Name)
                Form1.GenControl.Tag = _Name

                Form1.Combobox1.Text = _Name
                Form1.BuildAutocompleteMenu()
            Else

                Using New Centered_MessageBox(Form1)
                    'MsgBox(Form1.ProjectSaveString)
                    MsgBox("Name Already Exists!", vbInformation, "Exists!")
                End Using
            End If
        End Set
    End Property
    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter The Size Of ProgressBar.")>
    Public Property Size() As Size
        Get
            Return _Size
        End Get
        Set(ByVal value As Size)


            If value.Width > Form1.XofFormApp Then value.Width = Form1.XofFormApp
            If value.Height > Form1.YofFormApp Then value.Height = Form1.YofFormApp

            If value.Height < 0 Then value.Height = 0
            If value.Width < 0 Then value.Width = 0

            If _Location.X + value.Width > Form1.XofFormApp AndAlso _Location.Y + value.Height > Form1.YofFormApp Then
                Form1.GenControl.Location = New Point(0, 0)
                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={0;0}", Form1.GenControl.Name)
                _Location = New Point(0, 0)
            ElseIf _Location.X + value.Width > Form1.XofFormApp Then
                Form1.GenControl.Location = New Point(Form1.XofFormApp - value.Width, _Location.Y)
                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={" & Form1.XofFormApp - value.Width & ";" & _Location.Y & "}", Form1.GenControl.Name)
                _Location = New Point(Form1.XofFormApp - value.Width, _Location.Y)
            ElseIf _Location.Y + value.Height > Form1.YofFormApp Then
                Form1.GenControl.Location = New Point(_Location.X, Form1.YofFormApp - value.Height)
                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={" & _Location.X & ";" & Form1.YofFormApp - value.Height & "}", Form1.GenControl.Name)
                _Location = New Point(_Location.X, Form1.YofFormApp - value.Height)
            End If


            _Size = value

            Form1.GenControl.Size = New Size(value)




            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Size=", "Size=" & value.ToString.Replace("Width=", Nothing).Replace(",", ";").Replace("Height=", Nothing).Replace(" ", Nothing), Form1.GenControl.Name)
            Form1.DRWTran()
        End Set
    End Property
    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter The Location Of ProgressBar.")>
    Public Property Location() As Point
        Get
            Return _Location
        End Get
        Set(ByVal value As Point)
            If value.X + _Size.Width > Form1.XofFormApp AndAlso value.Y + _Size.Height > Form1.YofFormApp Then : value = New Point(0, 0)
            ElseIf value.X + _Size.Width > Form1.XofFormApp Then : value = New Point(Form1.XofFormApp - _Size.Width, value.Y)
            ElseIf value.Y + _Size.Height > Form1.YofFormApp Then : value = New Point(value.X, Form1.YofFormApp - _Size.Height)
            End If

            If value.X < 0 Then value.X = 0
            If value.Y < 0 Then value.Y = 0

            _Location = value


            Form1.GenControl.Location = New Point(value)
            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={" & value.X.ToString & ";" & value.Y.ToString & "}", Form1.GenControl.Name)
            Form1.DRWTran()
        End Set
    End Property
    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select If The ProgressBar Will Be Enabled Or Not.")>
    Public Property Enabled() As Boolean
        Get
            Return _Enabled
        End Get
        Set(ByVal Value As Boolean)
            _Enabled = Value
            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Enabled=", "Enabled=" & Value.ToString, Form1.GenControl.Name)
        End Set
    End Property







    <CategoryAttribute("Others"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select The Vlaue Of ProgressBar.")>
    Public Property Value() As Integer
        Get
            Return _Value
        End Get
        Set(ByVal Value As Integer)
            If Value > 100 Then _Value = 100 : Value = 100 : Beep() Else _Value = Value

            ' DirectCast(Form1.Panel5.Controls(Form1.GenControl.Name), ProgressBar).Value = _Value ' telika exei prob prepei prota na vriskw to panel pou einai sto panel5
            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Value=", "Value=" & Value, Form1.GenControl.Name)

        End Set
    End Property

End Class ' ok..








Public Class ClsRadioButton
    Public Shared _Name As String
    Public Shared _Size As Size
    Public Shared _Location As Point
    Public Shared _BackColor As System.Drawing.Color
    Public Shared _Enabled As Boolean

    Public Shared _Text As String 'Caption
    Public Shared _Font_Size As Integer
    Public Shared _bold As Boolean
    Public Shared _ForeColor As System.Drawing.Color
    Public Shared _Italic As Boolean
    Public Shared _TextAlign As String
    Public Shared _FontTypeface As String

    Public Shared _Checked As Boolean  ' Value

    '  Sub New()
    '      _Name = "asd"
    '  End Sub

    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter The Name Of RadioButton.")>
    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            For i = 0 To Form1.strClsRemove.Count - 1
                value = value.Replace(Form1.strClsRemove(i), "")
            Next
            If Not Form1.ProjectSaveString.Contains("Name=" & value & vbNewLine) Then
                Form1.Combobox1.Items.Remove(Form1.cmb)
                For item = 0 To Form1.ComboboxImage1.Items.Count
                    If Form1.ComboboxImage1.Items.Item(item).ToString = _Name Then Form1.ComboboxImage1.Items.RemoveAt(item) : Form1.ComboboxImage1.Items.Add(New ComboBoxIconItem(value, 10)) : Exit For
                Next

                If Form1.Textbox3.Text.Contains("Sub " & _Name & "_Change(") Then
                    For Each r In Form1.Textbox3.Range.GetRanges("Sub " & _Name & "_Change")
                        Form1.Textbox3.Selection = r
                        Form1.Textbox3.Text = Form1.Textbox3.Text.Replace(Form1.Textbox3.SelectedText, "Sub " & value & "_Change")
                    Next
                End If

                _Name = value

                Form1.Combobox1.Items.Add(New ComboBoxIconItem(value, 10))

                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Name=", "Name=" & value, Form1.GenControl.Name)
                Form1.GenControl.Tag = _Name

                Form1.Combobox1.Text = _Name
                Form1.BuildAutocompleteMenu()
            Else

                Using New Centered_MessageBox(Form1)
                    'MsgBox(Form1.ProjectSaveString)
                    MsgBox("Name Already Exists!", vbInformation, "Exists!")
                End Using
            End If
        End Set
    End Property
    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter The Size Of RadioButton.")>
    Public Property Size() As Size
        Get
            Return _Size
        End Get
        Set(ByVal value As Size)


            If value.Width > Form1.XofFormApp Then value.Width = Form1.XofFormApp
            If value.Height > Form1.YofFormApp Then value.Height = Form1.YofFormApp

            If value.Height < 0 Then value.Height = 0
            If value.Width < 0 Then value.Width = 0

            If _Location.X + value.Width > Form1.XofFormApp AndAlso _Location.Y + value.Height > Form1.YofFormApp Then
                Form1.GenControl.Location = New Point(0, 0)
                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={0;0}", Form1.GenControl.Name)
                _Location = New Point(0, 0)
            ElseIf _Location.X + value.Width > Form1.XofFormApp Then
                Form1.GenControl.Location = New Point(Form1.XofFormApp - value.Width, _Location.Y)
                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={" & Form1.XofFormApp - value.Width & ";" & _Location.Y & "}", Form1.GenControl.Name)
                _Location = New Point(Form1.XofFormApp - value.Width, _Location.Y)
            ElseIf _Location.Y + value.Height > Form1.YofFormApp Then
                Form1.GenControl.Location = New Point(_Location.X, Form1.YofFormApp - value.Height)
                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={" & _Location.X & ";" & Form1.YofFormApp - value.Height & "}", Form1.GenControl.Name)
                _Location = New Point(_Location.X, Form1.YofFormApp - value.Height)
            End If


            _Size = value

            Form1.GenControl.Size = New Size(value)




            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Size=", "Size=" & value.ToString.Replace("Width=", Nothing).Replace(",", ";").Replace("Height=", Nothing).Replace(" ", Nothing), Form1.GenControl.Name)
            Form1.DRWTran()
        End Set
    End Property
    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter The Location Of RadioButton.")>
    Public Property Location() As Point
        Get
            Return _Location
        End Get
        Set(ByVal value As Point)
            If value.X + _Size.Width > Form1.XofFormApp AndAlso value.Y + _Size.Height > Form1.YofFormApp Then : value = New Point(0, 0)
            ElseIf value.X + _Size.Width > Form1.XofFormApp Then : value = New Point(Form1.XofFormApp - _Size.Width, value.Y)
            ElseIf value.Y + _Size.Height > Form1.YofFormApp Then : value = New Point(value.X, Form1.YofFormApp - _Size.Height)
            End If

            If value.X < 0 Then value.X = 0
            If value.Y < 0 Then value.Y = 0

            _Location = value


            Form1.GenControl.Location = New Point(value)
            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={" & value.X.ToString & ";" & value.Y.ToString & "}", Form1.GenControl.Name)
            Form1.DRWTran()
        End Set
    End Property
    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select BackColor(BackGround) Of RadioButton.")>
    Public Property BackColor() As System.Drawing.Color
        Get
            Return _BackColor
        End Get
        Set(ByVal value As System.Drawing.Color)
            _BackColor = value

            Form1.GenControl.BackColor = Color.FromArgb(value.ToArgb)
            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "BackColor=", "BackColor={" & value.A & ";" & value.R & ";" & value.G & ";" & value.B & "}", Form1.GenControl.Name)

        End Set
    End Property
    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select If The RadioButton Will Be Enabled Or Not")>
    Public Property Enabled() As Boolean
        Get
            Return _Enabled
        End Get
        Set(ByVal Value As Boolean)
            _Enabled = Value

            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Enabled=", "Enabled=" & Value.ToString, Form1.GenControl.Name)

        End Set
    End Property





    <CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter Font Integer Size For The RadioButton.")>
    Public Property FontSize() As Integer
        Get
            Return _Font_Size
        End Get
        Set(ByVal value As Integer)
            _Font_Size = value


            If _bold = True Then
                Form1.GenControl.Font = New Font(FontStyle.Bold, value)
                Form1.GenControl.Font = New Font(Form1.GenControl.Font, FontStyle.Bold)
            ElseIf _Italic = True Then
                Form1.GenControl.Font = New Font(FontStyle.Italic, value)
                Form1.GenControl.Font = New Font(Form1.GenControl.Font, FontStyle.Italic)
            Else
                Form1.GenControl.Font = New Font(FontStyle.Regular, value)
            End If


            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "FontSize=", "FontSize=" & value, Form1.GenControl.Name)
        End Set
    End Property
    <CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter Text For The RadioButton.")>
    Public Property Text() As String
        Get
            Return _Text
        End Get
        Set(ByVal value As String)
            _Text = value
            Form1.GenControl.Text = value
            '   MsgBox(Form1.PropertiesOfControl)
            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Text=", "Text=" & value, Form1.GenControl.Name)
        End Set
    End Property
    <CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select If The RadioButto's Text Will Be Italic Or Not.")>
    Public Property Italic() As Boolean
        Get
            Return _Italic
        End Get
        Set(ByVal value As Boolean)
            _Italic = value

            If _bold = True Then _bold = False : Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "FontBold=", "FontBold=" & "False", Form1.GenControl.Name)

            If _Italic = True Then
                Form1.GenControl.Font = New Font(Form1.GenControl.Font, FontStyle.Italic)
            Else
                Form1.GenControl.Font = New Font(Form1.GenControl.Font, FontStyle.Regular)
            End If

            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "FontItalic=", "FontItalic=" & value.ToString, Form1.GenControl.Name)
        End Set
    End Property
    <CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select The Color Of RadioButton Text.")>
    Public Property ForeColor() As System.Drawing.Color
        Get
            Return _ForeColor
        End Get
        Set(ByVal value As System.Drawing.Color)
            _ForeColor = value

            Form1.GenControl.ForeColor = Color.FromArgb(value.ToArgb)
            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "ForeColor=", "ForeColor={" & value.A & ";" & value.R & ";" & value.G & ";" & value.B & "}", Form1.GenControl.Name)

        End Set
    End Property
    <CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select If The Text Of RadioButton Will Be Bold Or Not.")>
    Public Property Bold() As Boolean
        Get
            Return _bold
        End Get
        Set(ByVal value As Boolean)
            _bold = value

            If _Italic = True Then _Italic = False : Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "FontItalic=", "FontItalic=" & "False", Form1.GenControl.Name)

            If _bold = True Then
                Form1.GenControl.Font = New Font(Form1.GenControl.Font, FontStyle.Bold)
            Else
                Form1.GenControl.Font = New Font(Form1.GenControl.Font, FontStyle.Regular)
            End If


            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "FontBold=", "FontBold=" & value.ToString, Form1.GenControl.Name)
        End Set
    End Property
    <TypeConverter(GetType(TextAlignList)),
    CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select TextAlign For RadioButton's Text.")>
    Public Property TextAlign() As String
        Get
            Return _TextAlign
        End Get
        Set(ByVal value As String)
            _TextAlign = value

            Select Case _TextAlign
                Case Is = "LEFT" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), RadioButton).TextAlign = ContentAlignment.MiddleLeft
                Case Is = "CENTER" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), RadioButton).TextAlign = ContentAlignment.MiddleCenter
                Case Is = "RIGHT" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), RadioButton).TextAlign = ContentAlignment.MiddleRight
                Case Is = "LEFT_TOP" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), RadioButton).TextAlign = ContentAlignment.TopLeft
                Case Is = "LEFT_CENTER" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), RadioButton).TextAlign = ContentAlignment.MiddleLeft
                Case Is = "LEFT_BOTTOM" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), RadioButton).TextAlign = ContentAlignment.BottomLeft
                Case Is = "CENTER_TOP" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), RadioButton).TextAlign = ContentAlignment.TopCenter
                Case Is = "CENTER_CENTER" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), RadioButton).TextAlign = ContentAlignment.MiddleCenter
                Case Is = "CENTER_BOTTOM" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), RadioButton).TextAlign = ContentAlignment.BottomCenter
                Case Is = "RIGHT_TOP" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), RadioButton).TextAlign = ContentAlignment.TopRight
                Case Is = "RIGHT_CENTER" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), RadioButton).TextAlign = ContentAlignment.MiddleRight
                Case Is = "RIGHT_BOTTOM" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), RadioButton).TextAlign = ContentAlignment.BottomRight
            End Select


            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "TextAlign=", "TextAlign=" & value.ToString, Form1.GenControl.Name)

        End Set
    End Property

    <TypeConverter(GetType(FontTypefaceList)),
    CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select The Font Type Of RadioButton's Text.")>
    Public Property FontTypeface() As String
        Get
            Return _FontTypeface
        End Get
        Set(ByVal value As String)
            _FontTypeface = value

            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "FontTypeface=", "FontTypeface=" & value.ToString, Form1.GenControl.Name)

        End Set
    End Property






    <CategoryAttribute("Others"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select If The CheckBox Will Be Enabled Or Not (Only Supported From Code now)")>
    Public Property Checked() As Boolean
        Get
            Return _Checked
        End Get
        Set(ByVal Value As Boolean)
            _Checked = Value

            '  Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Checked=", "Checked=" & Value.ToString, Form1.GenControl.Name)

        End Set
    End Property
End Class ' ok..          xwris to Checked








Public Class clsTextBox
    Public Shared _Name As String
    Public Shared _Size As Size
    Public Shared _Location As Point
    Public Shared _BackColor As System.Drawing.Color
    Public Shared _Enabled As Boolean

    Public Shared _Text As String 'Caption
    Public Shared _Font_Size As Integer
    Public Shared _bold As Boolean
    Public Shared _ForeColor As System.Drawing.Color
    Public Shared _Italic As Boolean
    Public Shared _TextAlign As String
    Public Shared _FontTypeface As String

    Public Shared _Hint As String
    Public Shared _InputType As String

    '  Sub New()
    '      _Name = "asd"
    '  End Sub

    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter The Name Of TextBox.")>
    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            For i = 0 To Form1.strClsRemove.Count - 1
                value = value.Replace(Form1.strClsRemove(i), "")
            Next
            If Not Form1.ProjectSaveString.Contains("Name=" & value & vbNewLine) Then
                Form1.Combobox1.Items.Remove(Form1.cmb)
                For item = 0 To Form1.ComboboxImage1.Items.Count
                    If Form1.ComboboxImage1.Items.Item(item).ToString = _Name Then Form1.ComboboxImage1.Items.RemoveAt(item) : Form1.ComboboxImage1.Items.Add(New ComboBoxIconItem(value, 12)) : Exit For
                Next

                If Form1.Textbox3.Text.Contains("Sub " & _Name & "_Change(") Then
                    For Each r In Form1.Textbox3.Range.GetRanges("Sub " & _Name & "_Change")
                        Form1.Textbox3.Selection = r
                        Form1.Textbox3.Text = Form1.Textbox3.Text.Replace(Form1.Textbox3.SelectedText, "Sub " & value & "_Change")
                    Next
                End If

                _Name = value

                Form1.Combobox1.Items.Add(New ComboBoxIconItem(value, 12))

                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Name=", "Name=" & value, Form1.GenControl.Name)
                Form1.GenControl.Tag = _Name

                Form1.Combobox1.Text = _Name
                Form1.BuildAutocompleteMenu()
            Else

                Using New Centered_MessageBox(Form1)
                    'MsgBox(Form1.ProjectSaveString)
                    MsgBox("Name Already Exists!", vbInformation, "Exists!")
                End Using
            End If
        End Set
    End Property
    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter The Size Of TextBox.")>
    Public Property Size() As Size
        Get
            Return _Size
        End Get
        Set(ByVal value As Size)


            If value.Width > Form1.XofFormApp Then value.Width = Form1.XofFormApp
            If value.Height > Form1.YofFormApp Then value.Height = Form1.YofFormApp

            If value.Height < 0 Then value.Height = 0
            If value.Width < 0 Then value.Width = 0

            If _Location.X + value.Width > Form1.XofFormApp AndAlso _Location.Y + value.Height > Form1.YofFormApp Then
                Form1.GenControl.Location = New Point(0, 0)
                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={0;0}", Form1.GenControl.Name)
                _Location = New Point(0, 0)
            ElseIf _Location.X + value.Width > Form1.XofFormApp Then
                Form1.GenControl.Location = New Point(Form1.XofFormApp - value.Width, _Location.Y)
                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={" & Form1.XofFormApp - value.Width & ";" & _Location.Y & "}", Form1.GenControl.Name)
                _Location = New Point(Form1.XofFormApp - value.Width, _Location.Y)
            ElseIf _Location.Y + value.Height > Form1.YofFormApp Then
                Form1.GenControl.Location = New Point(_Location.X, Form1.YofFormApp - value.Height)
                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={" & _Location.X & ";" & Form1.YofFormApp - value.Height & "}", Form1.GenControl.Name)
                _Location = New Point(_Location.X, Form1.YofFormApp - value.Height)
            End If


            _Size = value

            Form1.GenControl.Size = New Size(value)




            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Size=", "Size=" & value.ToString.Replace("Width=", Nothing).Replace(",", ";").Replace("Height=", Nothing).Replace(" ", Nothing), Form1.GenControl.Name)
            Form1.DRWTran()
        End Set
    End Property
    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter The Location Of TextBox.")>
    Public Property Location() As Point
        Get
            Return _Location
        End Get
        Set(ByVal value As Point)
            If value.X + _Size.Width > Form1.XofFormApp AndAlso value.Y + _Size.Height > Form1.YofFormApp Then : value = New Point(0, 0)
            ElseIf value.X + _Size.Width > Form1.XofFormApp Then : value = New Point(Form1.XofFormApp - _Size.Width, value.Y)
            ElseIf value.Y + _Size.Height > Form1.YofFormApp Then : value = New Point(value.X, Form1.YofFormApp - _Size.Height)
            End If

            If value.X < 0 Then value.X = 0
            If value.Y < 0 Then value.Y = 0

            _Location = value


            Form1.GenControl.Location = New Point(value)
            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={" & value.X.ToString & ";" & value.Y.ToString & "}", Form1.GenControl.Name)
            Form1.DRWTran()
        End Set
    End Property
    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select BackColor(BackGround) Of TextBox.")>
    Public Property BackColor() As System.Drawing.Color
        Get
            Return _BackColor
        End Get
        Set(ByVal value As System.Drawing.Color)
            _BackColor = value

            Form1.GenControl.BackColor = Color.FromArgb(value.ToArgb)
            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "BackColor=", "BackColor={" & value.A & ";" & value.R & ";" & value.G & ";" & value.B & "}", Form1.GenControl.Name)

        End Set
    End Property
    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select If The TextBox Will Be Enabled Or Not")>
    Public Property Enabled() As Boolean
        Get
            Return _Enabled
        End Get
        Set(ByVal Value As Boolean)
            _Enabled = Value

            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Enabled=", "Enabled=" & Value.ToString, Form1.GenControl.Name)

        End Set
    End Property






    <CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter Font Integer Size For The TextBox.")>
    Public Property FontSize() As Integer
        Get
            Return _Font_Size
        End Get
        Set(ByVal value As Integer)
            _Font_Size = value



            If _bold = True Then
                Form1.GenControl.Font = New Font(FontStyle.Bold, value)
                Form1.GenControl.Font = New Font(Form1.GenControl.Font, FontStyle.Bold)
            ElseIf _Italic = True Then
                Form1.GenControl.Font = New Font(FontStyle.Italic, value)
                Form1.GenControl.Font = New Font(Form1.GenControl.Font, FontStyle.Italic)
            Else
                Form1.GenControl.Font = New Font(FontStyle.Regular, value)
            End If


            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "FontSize=", "FontSize=" & value, Form1.GenControl.Name)
        End Set
    End Property
    <CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter Text For The TextBox.")>
    Public Property Text() As String
        Get
            Return _Text
        End Get
        Set(ByVal value As String)
            _Text = value

            Form1.GenControl.Text = value
            '   MsgBox(Form1.PropertiesOfControl)
            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Text=", "Text=" & value, Form1.GenControl.Name)

        End Set
    End Property
    <CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select If The Text Of TextBox Will Be Italic Or Not.")>
    Public Property Italic() As Boolean
        Get
            Return _Italic
        End Get
        Set(ByVal value As Boolean)
            _Italic = value

            If _bold = True Then _bold = False : Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "FontBold=", "FontBold=" & "False", Form1.GenControl.Name)

            If _Italic = True Then
                Form1.GenControl.Font = New Font(Form1.GenControl.Font, FontStyle.Italic)
            Else
                Form1.GenControl.Font = New Font(Form1.GenControl.Font, FontStyle.Regular)
            End If

            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "FontItalic=", "FontItalic=" & value.ToString, Form1.GenControl.Name)

        End Set
    End Property
    <CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select The Color Of TextBox Text.")>
    Public Property ForeColor() As System.Drawing.Color
        Get
            Return _ForeColor
        End Get
        Set(ByVal value As System.Drawing.Color)
            _ForeColor = value


            Form1.GenControl.ForeColor = Color.FromArgb(value.ToArgb)
            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "ForeColor=", "ForeColor={" & value.A & ";" & value.R & ";" & value.G & ";" & value.B & "}", Form1.GenControl.Name)

        End Set
    End Property
    <CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select If The Text Of TextBox Will Be Bold Or Not.")>
    Public Property Bold() As Boolean
        Get
            Return _bold
        End Get
        Set(ByVal value As Boolean)
            _bold = value

            If _Italic = True Then _Italic = False : Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "FontItalic=", "FontItalic=" & "False", Form1.GenControl.Name)

            If _bold = True Then
                Form1.GenControl.Font = New Font(Form1.GenControl.Font, FontStyle.Bold)
            Else
                Form1.GenControl.Font = New Font(Form1.GenControl.Font, FontStyle.Regular)
            End If


            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "FontBold=", "FontBold=" & value.ToString, Form1.GenControl.Name)

        End Set
    End Property
    <TypeConverter(GetType(TextAlignList)),
    CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select TextAlign For TextBox's Text.")>
    Public Property TextAlign() As String
        Get
            Return _TextAlign
        End Get
        Set(ByVal value As String)
            _TextAlign = value

            ' center left right
            Select Case _TextAlign
                'Case Is = "LEFT" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), TextBox).TextAlign = ContentAlignment.MiddleLeft
                ' Case Is = "CENTER" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), TextBox).TextAlign = ContentAlignment.Center
                ' Case Is = "RIGHT" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), TextBox).TextAlign = ContentAlignment.MiddleRight
                Case Is = "LEFT_TOP" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), TextBox).TextAlign = HorizontalAlignment.Left ' https://social.msdn.microsoft.com/forums/windows/en-us/712d4df4-64e4-4748-8bff-6b9ed0db46fb/textbox-text-vertical-alignment
                ' Case Is = "LEFT_CENTER" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), TextBox).TextAlign = ContentAlignment.MiddleLeft
                ' Case Is = "LEFT_BOTTOM" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), TextBox).TextAlign = ContentAlignment.BottomLeft
                Case Is = "CENTER_TOP" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), TextBox).TextAlign = HorizontalAlignment.Center
                ' Case Is = "CENTER_CENTER" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), TextBox).TextAlign = ContentAlignment.MiddleCenter
                '  Case Is = "CENTER_BOTTOM" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), TextBox).TextAlign = ContentAlignment.BottomCenter
                Case Is = "RIGHT_TOP" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), TextBox).TextAlign = HorizontalAlignment.Right
                    ' Case Is = "RIGHT_CENTER" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), TextBox).TextAlign = ContentAlignment.MiddleRight
                    ' Case Is = "RIGHT_BOTTOM" : DirectCast(Form1.FindControl(Form1.GenControl.Name, Form1.Panel5), TextBox).TextAlign = ContentAlignment.BottomRight
            End Select

            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "TextAlign=", "TextAlign=" & value.ToString, Form1.GenControl.Name)

        End Set
    End Property
    <TypeConverter(GetType(FontTypefaceList)),
    CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select The Font Type Of TextBox's Text.")>
    Public Property FontTypeface() As String
        Get
            Return _FontTypeface
        End Get
        Set(ByVal value As String)
            _FontTypeface = value

            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "FontTypeface=", "FontTypeface=" & value.ToString, Form1.GenControl.Name)

        End Set
    End Property





    <CategoryAttribute("Others"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute(" Hint is a gray text shown when nothing inputs , Select If Your TextBox will have Or Not By leaving it blank.")>
    Public Property Hint() As String
        Get
            Return _Hint
        End Get
        Set(ByVal value As String)
            _Hint = value

            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Hint=", "Hint=" & value, Form1.GenControl.Name)

        End Set
    End Property

    <TypeConverter(GetType(InputTypeList)),
    CategoryAttribute("Others"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select If your TextBox Will Support Only NUMBER(Dec numbers) , .DATETIME(Numbers and /) , .PHONENUMBER(Numbers and * # - +) , .TEXTS(ALL) , .NULL(No ime pops up) , Or  .MULTILINE(Supports crlf).  ")>
    Public Property InputType() As String
        Get
            Return _InputType
        End Get
        Set(ByVal value As String)
            _InputType = value

            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "InputType=", "InputType=" & value, Form1.GenControl.Name)

        End Set
    End Property

End Class ' Ok..             sto InputType prepei na mhn epitrepw otan einai sto Numbers na vazei .... kai tetia ...








Public Class clsTimer
    Public Shared _Name As String
    Public Shared _Enabled As Boolean


    Public Shared _Interval As Integer




    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter The Name Of Timer.")>
    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            For i = 0 To Form1.strClsRemove.Count - 1
                value = value.Replace(Form1.strClsRemove(i), "")
            Next
            If Not Form1.ProjectSaveString.Contains("Name=" & value & vbNewLine) Then
                Form1.Combobox1.Items.Remove(Form1.cmb)
                For item = 0 To Form1.ComboboxImage1.Items.Count
                    If Form1.ComboboxImage1.Items.Item(item).ToString = _Name Then Form1.ComboboxImage1.Items.RemoveAt(item) : Form1.ComboboxImage1.Items.Add(New ComboBoxIconItem(value, 13)) : Exit For
                Next

                If Form1.Textbox3.Text.Contains("Sub " & _Name & "_Timer(") Then
                    For Each r In Form1.Textbox3.Range.GetRanges("Sub " & _Name & "_Timer")
                        Form1.Textbox3.Selection = r
                        Form1.Textbox3.Text = Form1.Textbox3.Text.Replace(Form1.Textbox3.SelectedText, "Sub " & value & "_Timer")
                    Next
                End If

                _Name = value

                Form1.Combobox1.Items.Add(New ComboBoxIconItem(value, 13))

                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Name=", "Name=" & value, Form1.GenControl.Name)
                Form1.GenControl.Tag = _Name

                Form1.Combobox1.Text = _Name
                Form1.BuildAutocompleteMenu()
            Else

                Using New Centered_MessageBox(Form1)
                    'MsgBox(Form1.ProjectSaveString)
                    MsgBox("Name Already Exists!", vbInformation, "Exists!")
                End Using
            End If
        End Set
    End Property
    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select If The Timer Will Be Enabled Or Not")>
    Public Property Enabled() As Boolean
        Get
            Return _Enabled
        End Get
        Set(ByVal Value As Boolean)
            _Enabled = Value

            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Enabled=", "Enabled=" & Value.ToString, Form1.GenControl.Name)
        End Set
    End Property








    <CategoryAttribute("Others"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select Timer's Internal.")>
    Public Property Interval() As Integer
        Get
            Return _Interval
        End Get
        Set(ByVal Value As Integer)
            _Interval = Value

            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Interval=", "Interval=" & Value, Form1.GenControl.Name)

        End Set
    End Property
End Class ' Ok..








Public Class clsComboBox
    Public Shared _Name As String
    Public Shared _Size As Size
    Public Shared _Location As Point
    Public Shared _BackColor As System.Drawing.Color
    Public Shared _Enabled As Boolean

    Public Shared _Text As String 'Caption
    Public Shared _Font_Size As Integer
    Public Shared _bold As Boolean
    Public Shared _ForeColor As System.Drawing.Color
    Public Shared _Italic As Boolean
    Public Shared _TextAlign As String
    Public Shared _FontTypeface As String





    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter The Name Of ComboBox.")>
    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            For i = 0 To Form1.strClsRemove.Count - 1
                value = value.Replace(Form1.strClsRemove(i), "")
            Next
            If Not Form1.ProjectSaveString.Contains("Name=" & value & vbNewLine) Then
                Form1.Combobox1.Items.Remove(Form1.cmb)
                For item = 0 To Form1.ComboboxImage1.Items.Count
                    If Form1.ComboboxImage1.Items.Item(item).ToString = _Name Then Form1.ComboboxImage1.Items.RemoveAt(item) : Form1.ComboboxImage1.Items.Add(New ComboBoxIconItem(value, 14)) : Exit For
                Next

                If Form1.Textbox3.Text.Contains("Sub " & _Name & "_ItemSelected(") Then
                    For Each r In Form1.Textbox3.Range.GetRanges("Sub " & _Name & "_ItemSelected")
                        Form1.Textbox3.Selection = r
                        Form1.Textbox3.Text = Form1.Textbox3.Text.Replace(Form1.Textbox3.SelectedText, "Sub " & value & "_ItemSelected")
                    Next
                End If

                _Name = value

                Form1.Combobox1.Items.Add(New ComboBoxIconItem(value, 14))

                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Name=", "Name=" & value, Form1.GenControl.Name)
                Form1.GenControl.Tag = _Name

                Form1.Combobox1.Text = _Name
                Form1.BuildAutocompleteMenu()
            Else

                Using New Centered_MessageBox(Form1)
                    'MsgBox(Form1.ProjectSaveString)
                    MsgBox("Name Already Exists!", vbInformation, "Exists!")
                End Using
            End If
        End Set
    End Property
    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter The Size Of ComboBox.")>
    Public Property Size() As Size
        Get
            Return _Size
        End Get
        Set(ByVal value As Size)


            If value.Width > Form1.XofFormApp Then value.Width = Form1.XofFormApp
            If value.Height > Form1.YofFormApp Then value.Height = Form1.YofFormApp

            If value.Height < 0 Then value.Height = 0
            If value.Width < 0 Then value.Width = 0

            If _Location.X + value.Width > Form1.XofFormApp AndAlso _Location.Y + value.Height > Form1.YofFormApp Then
                Form1.GenControl.Location = New Point(0, 0)
                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={0;0}", Form1.GenControl.Name)
                _Location = New Point(0, 0)
            ElseIf _Location.X + value.Width > Form1.XofFormApp Then
                Form1.GenControl.Location = New Point(Form1.XofFormApp - value.Width, _Location.Y)
                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={" & Form1.XofFormApp - value.Width & ";" & _Location.Y & "}", Form1.GenControl.Name)
                _Location = New Point(Form1.XofFormApp - value.Width, _Location.Y)
            ElseIf _Location.Y + value.Height > Form1.YofFormApp Then
                Form1.GenControl.Location = New Point(_Location.X, Form1.YofFormApp - value.Height)
                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={" & _Location.X & ";" & Form1.YofFormApp - value.Height & "}", Form1.GenControl.Name)
                _Location = New Point(_Location.X, Form1.YofFormApp - value.Height)
            End If


            _Size = value

            Form1.GenControl.Size = New Size(value)




            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Size=", "Size=" & value.ToString.Replace("Width=", Nothing).Replace(",", ";").Replace("Height=", Nothing).Replace(" ", Nothing), Form1.GenControl.Name)
            Form1.DRWTran()
        End Set
    End Property
    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter The Location Of ComboBox.")>
    Public Property Location() As Point
        Get
            Return _Location
        End Get
        Set(ByVal value As Point)
            If value.X + _Size.Width > Form1.XofFormApp AndAlso value.Y + _Size.Height > Form1.YofFormApp Then : value = New Point(0, 0)
            ElseIf value.X + _Size.Width > Form1.XofFormApp Then : value = New Point(Form1.XofFormApp - _Size.Width, value.Y)
            ElseIf value.Y + _Size.Height > Form1.YofFormApp Then : value = New Point(value.X, Form1.YofFormApp - _Size.Height)
            End If

            If value.X < 0 Then value.X = 0
            If value.Y < 0 Then value.Y = 0

            _Location = value


            Form1.GenControl.Location = New Point(value)
            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={" & value.X.ToString & ";" & value.Y.ToString & "}", Form1.GenControl.Name)
            Form1.DRWTran()
        End Set
    End Property
    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select BackColor(BackGround) Of ComboBox.")>
    Public Property BackColor() As System.Drawing.Color
        Get
            Return _BackColor
        End Get
        Set(ByVal value As System.Drawing.Color)
            _BackColor = value

            Form1.GenControl.BackColor = Color.FromArgb(value.ToArgb)
            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "BackColor=", "BackColor={" & value.A & ";" & value.R & ";" & value.G & ";" & value.B & "}", Form1.GenControl.Name)

        End Set
    End Property
    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select If The ComboBox Will Be Enabled Or Not")>
    Public Property Enabled() As Boolean
        Get
            Return _Enabled
        End Get
        Set(ByVal Value As Boolean)
            _Enabled = Value

            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Enabled=", "Enabled=" & Value.ToString, Form1.GenControl.Name)

        End Set
    End Property






    <CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter Font Integer Size For The ComboBox.")>
    Public Property FontSize() As Integer
        Get
            Return _Font_Size
        End Get
        Set(ByVal value As Integer)
            _Font_Size = value


            If _bold = True Then
                Form1.GenControl.Font = New Font(FontStyle.Bold, value)
                Form1.GenControl.Font = New Font(Form1.GenControl.Font, FontStyle.Bold)
            ElseIf _Italic = True Then
                Form1.GenControl.Font = New Font(FontStyle.Italic, value)
                Form1.GenControl.Font = New Font(Form1.GenControl.Font, FontStyle.Italic)
            Else
                Form1.GenControl.Font = New Font(FontStyle.Regular, value)
            End If


            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "FontSize=", "FontSize=" & value, Form1.GenControl.Name)


        End Set
    End Property
    <CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter Text For The ComboBox.")>
    Public Property Text() As String
        Get
            Return _Text
        End Get
        Set(ByVal value As String)
            _Text = value


            Form1.GenControl.Text = value
            '   MsgBox(Form1.PropertiesOfControl)
            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Text=", "Text=" & value, Form1.GenControl.Name)
            '  MsgBox(Form1.PropertiesOfControl)

        End Set
    End Property
    <CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select If The Text Of ComboBox Will Be Italic Or Not.")>
    Public Property Italic() As Boolean
        Get
            Return _Italic
        End Get
        Set(ByVal value As Boolean)
            _Italic = value

            _Italic = value

            If _bold = True Then _bold = False : Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "FontBold=", "FontBold=" & "False", Form1.GenControl.Name)

            If _Italic = True Then
                Form1.GenControl.Font = New Font(Form1.GenControl.Font, FontStyle.Italic)
            Else
                Form1.GenControl.Font = New Font(Form1.GenControl.Font, FontStyle.Regular)
            End If

            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "FontItalic=", "FontItalic=" & value.ToString, Form1.GenControl.Name)


        End Set
    End Property
    <CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select The Color Of ComboBox Text.")>
    Public Property ForeColor() As System.Drawing.Color
        Get
            Return _ForeColor
        End Get
        Set(ByVal value As System.Drawing.Color)
            _ForeColor = value

            Form1.GenControl.ForeColor = Color.FromArgb(value.ToArgb)
            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "ForeColor=", "ForeColor={" & value.A & ";" & value.R & ";" & value.G & ";" & value.B & "}", Form1.GenControl.Name)

        End Set
    End Property
    <CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select If The Text Of ComboBox Will Be Bold Or Not.")>
    Public Property Bold() As Boolean
        Get
            Return _bold
        End Get
        Set(ByVal value As Boolean)
            _bold = value


            If _Italic = True Then _Italic = False : Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "FontItalic=", "FontItalic=" & "False", Form1.GenControl.Name)

            If _bold = True Then
                Form1.GenControl.Font = New Font(Form1.GenControl.Font, FontStyle.Bold)
            Else
                Form1.GenControl.Font = New Font(Form1.GenControl.Font, FontStyle.Regular)
            End If


            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "FontBold=", "FontBold=" & value.ToString, Form1.GenControl.Name)


        End Set
    End Property
    <TypeConverter(GetType(TextAlignList)),
    CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select TextAlign For ComboBox's Text.")>
    Public Property TextAlign() As String
        Get
            Return _TextAlign
        End Get
        Set(ByVal value As String)
            _TextAlign = value


            ' NoOOoooOOOoooo TextAlign :'(  ... xD

            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "TextAlign=", "TextAlign=" & value.ToString, Form1.GenControl.Name)

        End Set
    End Property
    <TypeConverter(GetType(FontTypefaceList)),
    CategoryAttribute("Text"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Select The Font Type Of ComboBox's Text.")>
    Public Property FontTypeface() As String
        Get
            Return _FontTypeface
        End Get
        Set(ByVal value As String)
            _FontTypeface = value

            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "FontTypeface=", "FontTypeface=" & value.ToString, Form1.GenControl.Name)


        End Set
    End Property



End Class ' Ok..








Public Class clsVB4AWeb
    Public Shared _Name As String
    Public Shared _Size As Size
    Public Shared _Location As Point
    Public Shared _Enabled As Boolean


    Public Shared _SavePassword As Boolean
    Public Shared _SaveFormData As Boolean
    Public Shared _JSEnabled As Boolean
    Public Shared _ZoomEnabled As Boolean
    Public Shared _BuildinZoom As Boolean


    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter The Name Of Your WebBrowser.")>
    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            For i = 0 To Form1.strClsRemove.Count - 1
                value = value.Replace(Form1.strClsRemove(i), "")
            Next
            If Not Form1.ProjectSaveString.Contains("Name=" & value & vbNewLine) Then
                Form1.Combobox1.Items.Remove(Form1.cmb)
                For item = 0 To Form1.ComboboxImage1.Items.Count
                    If Form1.ComboboxImage1.Items.Item(item).ToString = _Name Then Form1.ComboboxImage1.Items.RemoveAt(item) : Form1.ComboboxImage1.Items.Add(New ComboBoxIconItem(value, 15)) : Exit For
                Next
                _Name = value

                Form1.Combobox1.Items.Add(New ComboBoxIconItem(value, 15))

                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Name=", "Name=" & value, Form1.GenControl.Name)
                Form1.GenControl.Tag = _Name

                Form1.Combobox1.Text = _Name
                Form1.BuildAutocompleteMenu()
            Else

                Using New Centered_MessageBox(Form1)
                    'MsgBox(Form1.ProjectSaveString)
                    MsgBox("Name Already Exists!", vbInformation, "Exists!")
                End Using
            End If
        End Set
    End Property
    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter The Size Of VB4AWeb.")>
    Public Property Size() As Size
        Get
            Return _Size
        End Get
        Set(ByVal value As Size)


            If value.Width > Form1.XofFormApp Then value.Width = Form1.XofFormApp
            If value.Height > Form1.YofFormApp Then value.Height = Form1.YofFormApp

            If value.Height < 0 Then value.Height = 0
            If value.Width < 0 Then value.Width = 0

            If _Location.X + value.Width > Form1.XofFormApp AndAlso _Location.Y + value.Height > Form1.YofFormApp Then
                Form1.GenControl.Location = New Point(0, 0)
                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={0;0}", Form1.GenControl.Name)
                _Location = New Point(0, 0)
            ElseIf _Location.X + value.Width > Form1.XofFormApp Then
                Form1.GenControl.Location = New Point(Form1.XofFormApp - value.Width, _Location.Y)
                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={" & Form1.XofFormApp - value.Width & ";" & _Location.Y & "}", Form1.GenControl.Name)
                _Location = New Point(Form1.XofFormApp - value.Width, _Location.Y)
            ElseIf _Location.Y + value.Height > Form1.YofFormApp Then
                Form1.GenControl.Location = New Point(_Location.X, Form1.YofFormApp - value.Height)
                Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={" & _Location.X & ";" & Form1.YofFormApp - value.Height & "}", Form1.GenControl.Name)
                _Location = New Point(_Location.X, Form1.YofFormApp - value.Height)
            End If


            _Size = value

            Form1.GenControl.Size = New Size(value)




            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Size=", "Size=" & value.ToString.Replace("Width=", Nothing).Replace(",", ";").Replace("Height=", Nothing).Replace(" ", Nothing), Form1.GenControl.Name)
            Form1.DRWTran()
        End Set
    End Property
    <CategoryAttribute("Common"),
    Browsable(True),
    [ReadOnly](False),
    BindableAttribute(False),
    DesignOnly(False),
    DescriptionAttribute("Enter The Location Of VB4AWeb.")>
    Public Property Location() As Point
        Get
            Return _Location
        End Get
        Set(ByVal value As Point)
            If value.X + _Size.Width > Form1.XofFormApp AndAlso value.Y + _Size.Height > Form1.YofFormApp Then : value = New Point(0, 0)
            ElseIf value.X + _Size.Width > Form1.XofFormApp Then : value = New Point(Form1.XofFormApp - _Size.Width, value.Y)
            ElseIf value.Y + _Size.Height > Form1.YofFormApp Then : value = New Point(value.X, Form1.YofFormApp - _Size.Height)
            End If

            If value.X < 0 Then value.X = 0
            If value.Y < 0 Then value.Y = 0

            _Location = value


            Form1.GenControl.Location = New Point(value)
            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Location=", "Location={" & value.X.ToString & ";" & value.Y.ToString & "}", Form1.GenControl.Name)
            Form1.DRWTran()
        End Set
    End Property
    <CategoryAttribute("Common"), _
    Browsable(True), _
    [ReadOnly](False), _
    BindableAttribute(False), _
    DesignOnly(False), _
    DescriptionAttribute("Select If The WebBrowser Will Be Enabled Or Not")> _
    Public Property Enabled() As Boolean
        Get
            Return _Enabled
        End Get
        Set(ByVal Value As Boolean)
            _Enabled = Value

            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "Enabled=", "Enabled=" & Value.ToString, Form1.GenControl.Name)

        End Set
    End Property








    <CategoryAttribute("Others"), _
    Browsable(True), _
    [ReadOnly](False), _
    BindableAttribute(False), _
    DesignOnly(False), _
    DescriptionAttribute("Select if passwords will be Saved In WebBrowser Or Not.")> _
    Public Property SavePassword() As Boolean
        Get
            Return _SavePassword
        End Get
        Set(ByVal value As Boolean)
            _SavePassword = value

            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "SavePassword=", "SavePassword=" & value.ToString, Form1.GenControl.Name)

        End Set
    End Property
    <CategoryAttribute("Others"), _
    Browsable(True), _
    [ReadOnly](False), _
    BindableAttribute(False), _
    DesignOnly(False), _
    DescriptionAttribute("Determin if the web control saves form data, currently write only(read will only returns True For Now).")> _
    Public Property SaveFormData() As Boolean
        Get
            Return _SaveFormData
        End Get
        Set(ByVal value As Boolean)
            _SaveFormData = value

            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "SaveFromData=", "SaveFromData=" & value.ToString, Form1.GenControl.Name)

        End Set
    End Property
    <CategoryAttribute("Others"), _
    Browsable(True), _
    [ReadOnly](False), _
    BindableAttribute(False), _
    DesignOnly(False), _
    DescriptionAttribute("Select If you Want your browser to execute the scripts in Sites.")> _
    Public Property JSEnabled() As Boolean
        Get
            Return _JSEnabled
        End Get
        Set(ByVal value As Boolean)
            _JSEnabled = value

            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "JSEnabled=", "JSEnabled=" & value.ToString, Form1.GenControl.Name)

        End Set
    End Property
    <CategoryAttribute("Others"), _
    Browsable(True), _
    [ReadOnly](False), _
    BindableAttribute(False), _
    DesignOnly(False), _
    DescriptionAttribute("Select If The User Will Be Able To Zoom In WebBrowser.")> _
    Public Property ZoomEnabled() As Boolean
        Get
            Return _ZoomEnabled
        End Get
        Set(ByVal value As Boolean)
            _ZoomEnabled = value

            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "ZoomEnabled=", "ZoomEnabled=" & value.ToString, Form1.GenControl.Name)

        End Set
    End Property
    <CategoryAttribute("Others"), _
    Browsable(True), _
    [ReadOnly](False), _
    BindableAttribute(False), _
    DesignOnly(False), _
    DescriptionAttribute("Selecte If your WebBrowser Will Have BuldinZoom or Not [+] [-] .")> _
    Public Property BuildinZoom() As Boolean
        Get
            Return _BuildinZoom
        End Get
        Set(ByVal value As Boolean)
            _BuildinZoom = value

            Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "BuildinZoom=", "BuildinZoom=" & value.ToString, Form1.GenControl.Name)

        End Set
    End Property



End Class ' Ok..






Public Class clsForm
    Public Shared _BackgroundImage As String
    Public Shared _BackgroundColor As System.Drawing.Color
    Public Shared _Layout As String
    Public Shared _Scrollable As Boolean
    Public Shared _Title As String



    <CategoryAttribute("Common"), _
    Browsable(True), _
    [ReadOnly](False), _
    BindableAttribute(False), _
    DesignOnly(False), _
    DescriptionAttribute("Enter The Title Of Your Form.")> _
    Public Property Title() As String
        Get
            Return _Title
        End Get
        Set(ByVal value As String)
            _Title = value
            '   Form1.AButton.Text = value
            Form1.ChangeASpecificPropOfContr(Form1.propertiesOfForm, "Title=", "Title=" & value, "++FORM++")
        End Set
    End Property

    <CategoryAttribute("Common"), _
    Editor(GetType(System.Windows.Forms.Design.FileNameEditor), GetType(System.Drawing.Design.UITypeEditor)), _
    Browsable(True), _
    [ReadOnly](False), _
    BindableAttribute(False), _
    DesignOnly(False), _
    DescriptionAttribute("Select A BackGround Image for your Form.")> _
    Public Property BackgroundImage() As String
        Get
            Return _BackgroundImage
        End Get
        Set(ByVal value As String)
            If value.EndsWith(".png") Or value.EndsWith(".gif") Or value.EndsWith(".bmp") Or value.EndsWith(".jpg") Then

                If Not _BackgroundImage = Nothing Then

                    ' Try
                    Form1.Panel5.BackgroundImage.Dispose()
                    Form1.Panel5.BackgroundImage = Nothing
                    ' Catch ex As Exception
                    ' End Try

                    _BackgroundImage = Form1.CurrentHardDriver & _BackgroundImage.Remove(0, 3)
                    ' My.Computer.FileSystem.DeleteFile(_BackgroundImage)
                    '   Form1.SaveCodeProject(_BackgroundImage & vbNewLine, "imgDEL.ini", True, Encoding.Default)
                End If

                If Not My.Computer.FileSystem.FileExists(Form1.CurrentHardDriver & "VB4Android\Projects\" & Form1.FileCreatedName & "\BUILD\assets\" & value.Split("\").Last) Then My.Computer.FileSystem.CopyFile(value, Form1.CurrentHardDriver & "VB4Android\Projects\" & Form1.FileCreatedName & "\BUILD\assets\" & value.Split("\").Last)
                value = Form1.CurrentHardDriver & "VB4Android\Projects\" & Form1.FileCreatedName & "\BUILD\assets\" & value.Split("\").Last
                'Form1.SaveCodeProject(value & vbNewLine, "img.ini", True, Encoding.Default)

                Form1.changeFromFileASpecPropOfCont(Form1.CurrentHardDriver & "VB4Android\Projects\" & Form1.FileCreatedName & "\" & Form1.Combobox3.SelectedItem.ToString, "BackgroundImage=", "BackgroundImage=" & value, "++Form++")
                ' Form1.changeFromFileASpecPropOfCont(Form1.CurrentHardDriver & "VB4Android\Projects\" & Form1.FileCreatedName & "\" & Form1.Combobox3.SelectedItem.ToString, "BackgroundImage=", "BackgroundImage=" & value, "++Form++")
                Form1.ChangeASpecificPropOfContr(Form1.propertiesOfForm, "BackgroundImage=", "BackgroundImage=" & value, "++FORM++")
                'Form1.ChangeASpecificPropOfContr(Form1.propertiesOfForm, "BackgroundColor=", "BackgroundColor={" & Color.Transparent.A & ";" & Color.Transparent.R & ";" & Color.Transparent.G & ";" & Color.Transparent.B & "}", "++FORM++")
                Form1.Panel5.BackgroundImage = System.Drawing.Image.FromFile(value)
                If Not _BackgroundImage = Nothing AndAlso Not Form1.ProjectSaveString.Contains(_BackgroundImage) AndAlso Form1.IsUsedFromAnotherProc(_BackgroundImage) = False Then My.Computer.FileSystem.DeleteFile(_BackgroundImage)


                _BackgroundImage = value
                '   Form1.AButton.Text = value
                ' value
                'Form1.GenControl.BackgroundImage = System.Drawing.Image.FromFile(value)



                ' MsgBox(value)
                ' Form1.ChangeASpecificPropOfContr(Form1.PropertiesOfControl, "BackgroundImage=", "BackgroundImage=" & value, "++FORM++")


            ElseIf value = Nothing AndAlso Not _BackgroundImage = Nothing Or value.Replace(" ", "") = Nothing AndAlso Not _BackgroundImage = Nothing Then
                Form1.Panel5.BackgroundImage.Dispose()
                Form1.Panel5.BackgroundImage = Nothing
                _BackgroundImage = Form1.CurrentHardDriver & _BackgroundImage.Remove(0, 3)
                Form1.changeFromFileASpecPropOfCont(Form1.CurrentHardDriver & "VB4Android\Projects\" & Form1.FileCreatedName & "\" & Form1.Combobox3.SelectedItem.ToString, "BackgroundImage=", "BackgroundImage=", "++FORM++")
                Form1.ChangeASpecificPropOfContr(Form1.propertiesOfForm, "BackgroundImage=", "BackgroundImage=", "++FORM++")
                If Not Form1.ProjectSaveString.Contains(_BackgroundImage) AndAlso Not _BackgroundImage = Nothing AndAlso Form1.IsUsedFromAnotherProc(_BackgroundImage) = False Then My.Computer.FileSystem.DeleteFile(_BackgroundImage)
                _BackgroundImage = ""
            Else
                MsgBox("You can only Insert png, gif, bmp and jpg files", MsgBoxStyle.Critical, "Wrong type of file.")
            End If

            ' Form1.ChangeASpecificPropOfContr(Form1.propertiesOfForm, "BackgroundImage=", "BackgroundImage=" & value, "++FORM++")
        End Set
    End Property

    <CategoryAttribute("Common"), _
    Browsable(True), _
    [ReadOnly](False), _
    BindableAttribute(False), _
    DesignOnly(False), _
    DescriptionAttribute("Select A BackGround Color for your Form.")> _
    Public Property BackgroundColor() As System.Drawing.Color
        Get
            Return _BackgroundColor
        End Get
        Set(ByVal value As System.Drawing.Color)
            _BackgroundColor = value

            Form1.Panel5.BackColor = value
            Form1.ChangeASpecificPropOfContr(Form1.propertiesOfForm, "BackgroundColor=", "BackgroundColor={" & value.A & ";" & value.R & ";" & value.G & ";" & value.B & "}", "++FORM++")
        End Set
    End Property




    <TypeConverter(GetType(TextLayoutList)), _
    CategoryAttribute("Others"), _
    Browsable(True), _
    [ReadOnly](False), _
    BindableAttribute(False), _
    DesignOnly(False), _
    DescriptionAttribute("Select a Horinontal or Vertical Layout For Your Form.")> _
    Public Property Layout() As String
        Get
            Return _Layout
        End Get
        Set(ByVal value As String)
            _Layout = value
            '   Form1.AButton.Text = value
            Form1.ChangeASpecificPropOfContr(Form1.propertiesOfForm, "Layout=", "Layout=" & value.ToString, "++FORM++")
            If value.ToString = "VERTICAL" Then
                Form1.XofFormApp = 240
                Form1.YofFormApp = 425
                Form1.Panel5.Size = New Size(Form1.XofFormApp + 2, Form1.YofFormApp + 2)
            Else
                Form1.XofFormApp = 425
                Form1.YofFormApp = 240
                Form1.Panel5.Size = New Size(Form1.XofFormApp + 2, Form1.YofFormApp + 2)

            End If
        End Set
    End Property

    '<CategoryAttribute("Others"), _
    'Browsable(True), _
    '[ReadOnly](False), _
    'BindableAttribute(False), _
    'DesignOnly(False), _
    'DescriptionAttribute("Select if Form will be Scrollable or Not.")> _
    'Public Property Scrollable() As Boolean
    '    Get
    '        Return _Scrollable
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _Scrollable = value
    '        Form1.ChangeASpecificPropOfContr(Form1.propertiesOfForm, "Scrollable=", "Scrollable=" & value.ToString, "++FORM++")
    '    End Set
    'End Property
End Class ' ---------- ok..




Public Class TextLayoutList : Inherits System.ComponentModel.StringConverter
    '                                           
    Dim _TextLayout As String() = New String() {"VERTICAL", "HORIZONTAL"}
    Public Overloads Overrides Function GetStandardValues(ByVal context As System.ComponentModel.ITypeDescriptorContext) As System.ComponentModel.TypeConverter.StandardValuesCollection
        Return New StandardValuesCollection(_TextLayout)
    End Function
    Public Overloads Overrides Function GetStandardValuesSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        Return True
    End Function
    Public Overloads Overrides Function GetStandardValuesExclusive(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        Return True
    End Function

End Class





Public Class TextAlignList : Inherits System.ComponentModel.StringConverter
    '                                           JUSTIFY_
    Dim _TextAlign As String() = New String() {"LEFT", "CENTER", "RIGHT", "LEFT_TOP", "LEFT_CENTER", "LEFT_BOTTOM", "CENTER_TOP", "CENTER_CENTER", "CENTER_BOTTOM", _
                                               "RIGHT_TOP", "RIGHT_CENTER", "RIGHT_BOTTOM"}
    Public Overloads Overrides Function GetStandardValues(ByVal context As System.ComponentModel.ITypeDescriptorContext) As System.ComponentModel.TypeConverter.StandardValuesCollection
        Return New StandardValuesCollection(_TextAlign)
    End Function
    Public Overloads Overrides Function GetStandardValuesSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        Return True
    End Function
    Public Overloads Overrides Function GetStandardValuesExclusive(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        Return True
    End Function

End Class


Public Class FontTypefaceList : Inherits System.ComponentModel.StringConverter
    '                                           TYPEFACE_
    Dim _FontTypeface As String() = New String() {"DEFAULT", "SANSSERIF", "SERIF", "MONOSPACE"}
    Public Overloads Overrides Function GetStandardValues(ByVal context As System.ComponentModel.ITypeDescriptorContext) As System.ComponentModel.TypeConverter.StandardValuesCollection
        Return New StandardValuesCollection(_FontTypeface)
    End Function
    Public Overloads Overrides Function GetStandardValuesSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        Return True
    End Function
    Public Overloads Overrides Function GetStandardValuesExclusive(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        Return True
    End Function

End Class


Public Class InputTypeList : Inherits System.ComponentModel.StringConverter
    '                                           INPUTTYPE_
    Dim _InputType As String() = New String() {"NUMBER", "DATETIME", "PHONENUMBER", "TEXTS", "NULL", "MULTILINE"}
    Public Overloads Overrides Function GetStandardValues(ByVal context As System.ComponentModel.ITypeDescriptorContext) As System.ComponentModel.TypeConverter.StandardValuesCollection
        Return New StandardValuesCollection(_InputType)
    End Function
    Public Overloads Overrides Function GetStandardValuesSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        Return True
    End Function
    Public Overloads Overrides Function GetStandardValuesExclusive(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        Return True
    End Function

End Class '


'Public Class FolderNameEditor2
'    Inherits UITypeEditor
'    Public Overrides Function GetEditStyle(context As ITypeDescriptorContext) As UITypeEditorEditStyle
'        Return UITypeEditorEditStyle.Modal
'    End Function

'    Public Overrides Function EditValue(context As ITypeDescriptorContext, provider As IServiceProvider, value As Object) As Object
'        Dim browser As New FolderBrowser2()
'        If value IsNot Nothing Then
'            browser.DirectoryPath = String.Format("{0}", value)
'        End If

'        If browser.ShowDialog(Nothing) = DialogResult.OK Then
'            Return browser.DirectoryPath
'        End If

'        Return value
'    End Function
'End Class

'Public Class FolderBrowser2
'    Public Property DirectoryPath() As String
'        Get
'            Return m_DirectoryPath
'        End Get
'        Set(value As String)
'            m_DirectoryPath = value
'        End Set
'    End Property
'    Private m_DirectoryPath As String

'    Public Function ShowDialog(owner As IWin32Window) As DialogResult
'        Dim hwndOwner As IntPtr = If(owner IsNot Nothing, owner.Handle, GetActiveWindow())

'        Dim dialog As IFileOpenDialog = DirectCast(New FileOpenDialog(), IFileOpenDialog)
'        Try
'            Dim item As IShellItem
'            If Not String.IsNullOrEmpty(DirectoryPath) Then
'                Dim idl As IntPtr
'                Dim atts As UInteger = 0
'                If SHILCreateFromPath(DirectoryPath, idl, atts) = 0 Then
'                    If SHCreateShellItem(IntPtr.Zero, IntPtr.Zero, idl, item) = 0 Then
'                        dialog.SetFolder(item)
'                    End If
'                End If
'            End If
'            dialog.SetOptions(FOS.FOS_PICKFOLDERS Or FOS.FOS_FORCEFILESYSTEM)
'            Dim hr As UInteger = dialog.Show(hwndOwner)
'            If hr = ERROR_CANCELLED Then
'                Return DialogResult.Cancel
'            End If

'            If hr <> 0 Then
'                Return DialogResult.Abort
'            End If

'            dialog.GetResult(item)
'            Dim path As String
'            item.GetDisplayName(SIGDN.SIGDN_FILESYSPATH, path)
'            DirectoryPath = path
'            Return DialogResult.OK
'        Finally
'            Marshal.ReleaseComObject(dialog)
'        End Try
'    End Function

'    <DllImport("shell32.dll")> _
'    Private Shared Function SHILCreateFromPath(<MarshalAs(UnmanagedType.LPWStr)> pszPath As String, ByRef ppIdl As IntPtr, ByRef rgflnOut As UInteger) As Integer
'    End Function

'    <DllImport("shell32.dll")> _
'    Private Shared Function SHCreateShellItem(pidlParent As IntPtr, psfParent As IntPtr, pidl As IntPtr, ByRef ppsi As IShellItem) As Integer
'    End Function

'    <DllImport("user32.dll")> _
'    Private Shared Function GetActiveWindow() As IntPtr
'    End Function

'    Private Const ERROR_CANCELLED As UInteger = &H800704C7UI

'    <ComImport> _
'    <Guid("DC1C5A9C-E88A-4dde-A5A1-60F82A20AEF7")> _
'    Private Class FileOpenDialog
'    End Class

'    <ComImport> _
'    <Guid("42f85136-db7e-439c-85f1-e4075d135fc8")> _
'    <InterfaceType(ComInterfaceType.InterfaceIsIUnknown)> _
'    Private Interface IFileOpenDialog
'        <PreserveSig> _
'        Function Show(<[In]> parent As IntPtr) As UInteger
'        ' IModalWindow
'        Sub SetFileTypes()
'        ' not fully defined
'        Sub SetFileTypeIndex(<[In]> iFileType As UInteger)
'        Sub GetFileTypeIndex(ByRef piFileType As UInteger)
'        Sub Advise()
'        ' not fully defined
'        Sub Unadvise()
'        Sub SetOptions(<[In]> fos As FOS)
'        Sub GetOptions(ByRef pfos As FOS)
'        Sub SetDefaultFolder(psi As IShellItem)
'        Sub SetFolder(psi As IShellItem)
'        Sub GetFolder(ByRef ppsi As IShellItem)
'        Sub GetCurrentSelection(ByRef ppsi As IShellItem)
'        Sub SetFileName(<[In], MarshalAs(UnmanagedType.LPWStr)> pszName As String)
'        Sub GetFileName(<MarshalAs(UnmanagedType.LPWStr)> ByRef pszName As String)
'        Sub SetTitle(<[In], MarshalAs(UnmanagedType.LPWStr)> pszTitle As String)
'        Sub SetOkButtonLabel(<[In], MarshalAs(UnmanagedType.LPWStr)> pszText As String)
'        Sub SetFileNameLabel(<[In], MarshalAs(UnmanagedType.LPWStr)> pszLabel As String)
'        Sub GetResult(ByRef ppsi As IShellItem)
'        Sub AddPlace(psi As IShellItem, alignment As Integer)
'        Sub SetDefaultExtension(<[In], MarshalAs(UnmanagedType.LPWStr)> pszDefaultExtension As String)
'        Sub Close(hr As Integer)
'        Sub SetClientGuid()
'        ' not fully defined
'        Sub ClearClientData()
'        Sub SetFilter(<MarshalAs(UnmanagedType.[Interface])> pFilter As IntPtr)
'        Sub GetResults(<MarshalAs(UnmanagedType.[Interface])> ByRef ppenum As IntPtr)
'        ' not fully defined
'        Sub GetSelectedItems(<MarshalAs(UnmanagedType.[Interface])> ByRef ppsai As IntPtr)
'        ' not fully defined
'    End Interface

'    <ComImport> _
'    <Guid("43826D1E-E718-42EE-BC55-A1E261C37BFE")> _
'    <InterfaceType(ComInterfaceType.InterfaceIsIUnknown)> _
'    Private Interface IShellItem
'        Sub BindToHandler()
'        ' not fully defined
'        Sub GetParent()
'        ' not fully defined
'        Sub GetDisplayName(<[In]> sigdnName As SIGDN, <MarshalAs(UnmanagedType.LPWStr)> ByRef ppszName As String)
'        Sub GetAttributes()
'        ' not fully defined
'        Sub Compare()
'        ' not fully defined
'    End Interface

'    Private Enum SIGDN As UInteger
'        SIGDN_DESKTOPABSOLUTEEDITING = &H8004C000UI
'        SIGDN_DESKTOPABSOLUTEPARSING = &H80028000UI
'        SIGDN_FILESYSPATH = &H80058000UI
'        SIGDN_NORMALDISPLAY = 0
'        SIGDN_PARENTRELATIVE = &H80080001UI
'        SIGDN_PARENTRELATIVEEDITING = &H80031001UI
'        SIGDN_PARENTRELATIVEFORADDRESSBAR = &H8007C001UI
'        SIGDN_PARENTRELATIVEPARSING = &H80018001UI
'        SIGDN_URL = &H80068000UI
'    End Enum

'    <Flags> _
'    Private Enum FOS
'        FOS_ALLNONSTORAGEITEMS = &H80
'        FOS_ALLOWMULTISELECT = &H200
'        FOS_CREATEPROMPT = &H2000
'        FOS_DEFAULTNOMINIMODE = &H20000000
'        FOS_DONTADDTORECENT = &H2000000
'        FOS_FILEMUSTEXIST = &H1000
'        FOS_FORCEFILESYSTEM = &H40
'        FOS_FORCESHOWHIDDEN = &H10000000
'        FOS_HIDEMRUPLACES = &H20000
'        FOS_HIDEPINNEDPLACES = &H40000
'        FOS_NOCHANGEDIR = 8
'        FOS_NODEREFERENCELINKS = &H100000
'        FOS_NOREADONLYRETURN = &H8000
'        FOS_NOTESTFILECREATE = &H10000
'        FOS_NOVALIDATE = &H100
'        FOS_OVERWRITEPROMPT = 2
'        FOS_PATHMUSTEXIST = &H800
'        FOS_PICKFOLDERS = &H20
'        FOS_SHAREAWARE = &H4000
'        FOS_STRICTFILETYPES = 4
'    End Enum
'End Class
