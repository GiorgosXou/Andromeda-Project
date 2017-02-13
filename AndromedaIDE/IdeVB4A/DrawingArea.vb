
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Drawing.Drawing2D

''' <summary>
''' Use this for drawing custom graphics and text with transparency.
''' Inherit from DrawingArea and override the OnDraw method.
''' </summary>
Public MustInherit Class DrawingArea
	Inherits Panel
	''' <summary>
	''' Drawing surface where graphics should be drawn.
	''' Use this member in the OnDraw method.
	''' </summary>
	Protected graphics As Graphics

	Protected Overrides ReadOnly Property CreateParams() As CreateParams
		Get
			Dim cp As CreateParams = MyBase.CreateParams
			cp.ExStyle = cp.ExStyle Or &H20
			'WS_EX_TRANSPARENT 
			Return cp
		End Get
	End Property

	Public Sub New()
	End Sub

	Protected Overrides Sub OnPaintBackground(pevent As PaintEventArgs)
		' Don't paint background
	End Sub

	Protected Overrides Sub OnPaint(e As PaintEventArgs)
		' Update the private member so we can use it in the OnDraw method
		Me.graphics = e.Graphics

		' Set the best settings possible (quality-wise)
		Me.graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias
		Me.graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear
		Me.graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality
		Me.graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality

		' Calls the OnDraw subclass method
		OnDraw()
	End Sub

	''' <summary>
	''' Override this method in subclasses for drawing purposes.
	''' </summary>
	Protected MustOverride Sub OnDraw()

	#Region "DrawText helper functions"

	Protected Sub DrawText(text As String, position As Point)
		DrawingArea.DrawText(Me.graphics, text, "Microsoft Sans Serif", 8.25F, FontStyle.Regular, Brushes.Black, _
			position)
	End Sub

	Protected Sub DrawText(text As String, color As Brush, position As Point)
		DrawingArea.DrawText(Me.graphics, text, "Microsoft Sans Serif", 8.25F, FontStyle.Regular, color, _
			position)
	End Sub

	Protected Sub DrawText(text As String, style As FontStyle, position As Point)
		DrawingArea.DrawText(Me.graphics, text, "Microsoft Sans Serif", 8.25F, style, Brushes.Black, _
			position)
	End Sub

	Protected Sub DrawText(text As String, style As FontStyle, color As Brush, position As Point)
		DrawingArea.DrawText(Me.graphics, text, "Microsoft Sans Serif", 8.25F, style, color, _
			position)
	End Sub

	Protected Sub DrawText(text As String, fontSize As Single, style As FontStyle, color As Brush, position As Point)
		DrawingArea.DrawText(Me.graphics, text, "Microsoft Sans Serif", fontSize, style, color, _
			position)
	End Sub

	Protected Sub DrawText(text As String, fontFamily As String, fontSize As Single, style As FontStyle, color As Brush, position As Point)
		DrawingArea.DrawText(Me.graphics, text, fontFamily, fontSize, style, color, _
			position)
	End Sub

	Public Shared Sub DrawText(graphics As Graphics, text As String, fontFamily As String, fontSize As Single, style As FontStyle, color As Brush, _
		position As Point)
		Dim font As New Font(fontFamily, fontSize, style)

		Dim textSizeF As SizeF = graphics.MeasureString(text, font)
		Dim width As Integer = CInt(Math.Ceiling(textSizeF.Width))
		Dim height As Integer = CInt(Math.Ceiling(textSizeF.Height))
		Dim textSize As New Size(width, height)
		Dim rectangle As New Rectangle(position, textSize)

		graphics.DrawString(text, font, color, rectangle)
	End Sub

	#End Region
End Class


