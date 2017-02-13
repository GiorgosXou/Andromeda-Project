
Imports System.Runtime.InteropServices



Public Class ShadowForm
    Dim m As New MARGINS '(0, 0, 0, 0)
    <DllImport("dwmapi.dll")>
    Private Shared Function DwmExtendFrameIntoClientArea(hWnd As IntPtr, ByRef pMarInset As Margins) As Integer
    End Function
    <DllImport("dwmapi.dll", PreserveSig:=True)>
    Private Shared Function DwmSetWindowAttribute(hwnd As IntPtr, attr As Integer, ByRef attrValue As Integer, attrSize As Integer) As Integer
    End Function
    Public Structure MARGINS
        Public Left, Right, Top, Bottom As Integer
    End Structure

    Protected Overrides ReadOnly Property CreateParams As System.Windows.Forms.CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H80
            Return cp
        End Get
    End Property

    Private Function CreateDropShadow() As Boolean
        Try
            Dim ret1 As Integer = DwmSetWindowAttribute(Me.Handle, 2, 2, 4)

            If ret1 = 0 Then
                Dim m As New MARGINS

                m.Left = 0
                m.Right = 0
                m.Top = 0
                m.Bottom = 1

                Dim ret2 As Integer = DwmExtendFrameIntoClientArea(Me.Handle, m)
                Return ret2 = 0
            Else
                Return False
            End If
        Catch ex As Exception
            ' Probably dwmapi.dll not found (incompatible OS)
            Return False
        End Try
    End Function

    Protected Overrides Sub OnHandleCreated(e As EventArgs)
        CreateDropShadow()
        MyBase.OnHandleCreated(e)
    End Sub
    Private Sub ShadowForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Location = Form1.Location
        Me.Size = Form1.Size + New Size(2, 2)
        Me.Location = Form1.Location - New Point(1, 1)
        Me.ShowInTaskbar = False
        Me.ShowIcon = False
    End Sub

    Private Sub ShadowForm_LocationChanged(sender As Object, e As EventArgs) Handles Me.LocationChanged

        'Me.BringToFront()
        'Form1.BringToFront()
        'Me.Show()
    End Sub

    Private Sub ShadowForm_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        Form1.BringToFront()

    End Sub

    Public PenC As New Pen(Color.FromArgb(24, 131, 215), 1)
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ' Create rectangle. 
        Dim rect As New Rectangle(0, Me.Height - 1, Me.Width, 1)
        ' Draw rectangle to screen.
        Me.CreateGraphics.DrawRectangle(PenC, rect)
    End Sub

    'Protected Overrides ReadOnly Property CreateParams() As System.Windows.Forms.CreateParams
    '    Get
    '        Const DROPSHADOW = &H20000
    '        Dim cParam As CreateParams = MyBase.CreateParams
    '        cParam.ClassStyle = cParam.ClassStyle Or DROPSHADOW
    '        Return cParam
    '    End Get
    'End Property
End Class