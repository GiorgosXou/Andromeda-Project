<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class Form1
#Region "Windows Form Designer generated code "
	<System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
		MyBase.New()
		'This call is required by the Windows Form Designer.
		InitializeComponent()
	End Sub
	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
		If Disposing Then
			If Not components Is Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(Disposing)
	End Sub
	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer
	Public ToolTip1 As System.Windows.Forms.ToolTip
	Public WithEvents txtText1 As System.Windows.Forms.TextBox
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Form1))
		Me.components = New System.ComponentModel.Container()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
		Me.txtText1 = New System.Windows.Forms.TextBox
		Me.SuspendLayout()
		Me.ToolTip1.Active = True
		Me.Text = "Form1"
		Me.ClientSize = New System.Drawing.Size(582, 329)
		Me.Location = New System.Drawing.Point(4, 30)
		Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultLocation
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.SystemColors.Control
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable
		Me.ControlBox = True
		Me.Enabled = True
		Me.KeyPreview = False
		Me.MaximizeBox = True
		Me.MinimizeBox = True
		Me.Cursor = System.Windows.Forms.Cursors.Default
		Me.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.HelpButton = False
		Me.WindowState = System.Windows.Forms.FormWindowState.Normal
		Me.Name = "Form1"
		Me.txtText1.AutoSize = False
		Me.txtText1.Size = New System.Drawing.Size(569, 313)
		Me.txtText1.Location = New System.Drawing.Point(8, 8)
		Me.txtText1.MultiLine = True
		Me.txtText1.TabIndex = 0
		Me.txtText1.Text = "Text1"
		Me.txtText1.AcceptsReturn = True
		Me.txtText1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtText1.BackColor = System.Drawing.SystemColors.Window
		Me.txtText1.CausesValidation = True
		Me.txtText1.Enabled = True
		Me.txtText1.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtText1.HideSelection = True
		Me.txtText1.ReadOnly = False
		Me.txtText1.Maxlength = 0
		Me.txtText1.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtText1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtText1.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtText1.TabStop = True
		Me.txtText1.Visible = True
		Me.txtText1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.txtText1.Name = "txtText1"
		Me.Controls.Add(txtText1)
		Me.ResumeLayout(False)
		Me.PerformLayout()
	End Sub
#End Region 
End Class