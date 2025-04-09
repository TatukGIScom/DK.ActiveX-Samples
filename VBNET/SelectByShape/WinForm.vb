Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS_XDK11

Namespace SelectByShape
    ''' <summary>
    ''' Summary description for WinForm.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.Container = Nothing
        Private statusBar1 As System.Windows.Forms.StatusBar
        Private statusBarPanel1 As System.Windows.Forms.StatusBarPanel
        Private textBox1 As System.Windows.Forms.TextBox
        Private WithEvents GIS As AxTatukGIS_XDK11.AxTGIS_ViewerWnd
        Private oldPos As New TPoint
        Private oldPos2 As New TPoint
        Private oldRadius As Double
        Private oldZoom As Integer
        Private oldColor As TGIS_Color
        Friend WithEvents btnCircle As CheckBox
        Friend WithEvents btnRectangle As CheckBox
        Private ll As TGIS_LayerVector

        Public Sub New()
            '
            ' Required for Windows Form Designer support
            '
            InitializeComponent()

            '
            ' TODO: Add any constructor code after InitializeComponent call
            '
        End Sub

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Not components Is Nothing Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub

#Region "Windows Form Designer generated code"
        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WinForm))
            Me.statusBar1 = New System.Windows.Forms.StatusBar()
            Me.statusBarPanel1 = New System.Windows.Forms.StatusBarPanel()
            Me.textBox1 = New System.Windows.Forms.TextBox()
            Me.GIS = New AxTatukGIS_XDK11.AxTGIS_ViewerWnd()
            Me.btnCircle = New System.Windows.Forms.CheckBox()
            Me.btnRectangle = New System.Windows.Forms.CheckBox()
            CType(Me.statusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'statusBar1
            '
            Me.statusBar1.Location = New System.Drawing.Point(0, 447)
            Me.statusBar1.Name = "statusBar1"
            Me.statusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.statusBarPanel1})
            Me.statusBar1.ShowPanels = True
            Me.statusBar1.Size = New System.Drawing.Size(592, 19)
            Me.statusBar1.TabIndex = 0
            '
            'statusBarPanel1
            '
            Me.statusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
            Me.statusBarPanel1.Name = "statusBarPanel1"
            Me.statusBarPanel1.Text = "Use left mouse button to select by chosen shape"
            Me.statusBarPanel1.Width = 575
            '
            'textBox1
            '
            Me.textBox1.BackColor = System.Drawing.SystemColors.Window
            Me.textBox1.Dock = System.Windows.Forms.DockStyle.Right
            Me.textBox1.Location = New System.Drawing.Point(408, 0)
            Me.textBox1.Multiline = True
            Me.textBox1.Name = "textBox1"
            Me.textBox1.ReadOnly = True
            Me.textBox1.Size = New System.Drawing.Size(184, 447)
            Me.textBox1.TabIndex = 1
            '
            'GIS
            '
            Me.GIS.Location = New System.Drawing.Point(0, 29)
            Me.GIS.Name = "GIS"
            Me.GIS.OcxState = CType(resources.GetObject("GIS.OcxState"), System.Windows.Forms.AxHost.State)
            Me.GIS.Size = New System.Drawing.Size(408, 418)
            Me.GIS.TabIndex = 2
            '
            'btnCircle
            '
            Me.btnCircle.Appearance = System.Windows.Forms.Appearance.Button
            Me.btnCircle.AutoSize = True
            Me.btnCircle.Location = New System.Drawing.Point(3, 3)
            Me.btnCircle.Name = "btnCircle"
            Me.btnCircle.Size = New System.Drawing.Size(57, 23)
            Me.btnCircle.TabIndex = 3
            Me.btnCircle.Text = "By cricle"
            Me.btnCircle.UseVisualStyleBackColor = True
            '
            'btnRectangle
            '
            Me.btnRectangle.Appearance = System.Windows.Forms.Appearance.Button
            Me.btnRectangle.AutoSize = True
            Me.btnRectangle.Location = New System.Drawing.Point(62, 3)
            Me.btnRectangle.Name = "btnRectangle"
            Me.btnRectangle.Size = New System.Drawing.Size(76, 23)
            Me.btnRectangle.TabIndex = 4
            Me.btnRectangle.Text = "By rectangle"
            Me.btnRectangle.UseVisualStyleBackColor = True
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(592, 466)
            Me.Controls.Add(Me.btnRectangle)
            Me.Controls.Add(Me.btnCircle)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.textBox1)
            Me.Controls.Add(Me.statusBar1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Select by shape"
            CType(Me.statusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
#End Region

        Dim GisUtils As New TGIS_Utils()

        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        <STAThread>
        Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New WinForm())
        End Sub

        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            GIS.Open(GisUtils.GisSamplesDataDirDownload() & "\World\Countries\USA\States\California\Counties.SHP")
            ll = New TGIS_LayerVector()
            ll.Params.Area.Color = (New TGIS_Color).Blue
            ll.Transparency = 50
            ll.Name = "Points"
            ll.CS = GIS.CS
            GIS.Add(ll)
            ll = New TGIS_LayerVector()
            ll.Params.Area.Color = (New TGIS_Color).Blue
            ll.Params.Area.OutlineColor = (New TGIS_Color).Blue
            ll.Transparency = 60
            ll.Name = "Buffers"
            ll.CS = GIS.CS
            GIS.Add(ll)

            btnRectangle.Checked = True
        End Sub

        Private Sub GIS_MouseDown(ByVal sender As Object, ByVal e As AxTatukGIS_XDK11.ITGIS_ViewerWndEvents_MouseDownEvent) Handles GIS.MouseDownEvent
            If GIS.IsEmpty Then
                Return
            End If

            If e.Button = MouseButtons.Right Then
                GIS.Mode = TGIS_ViewerMode.Zoom
                Return
            End If
            oldPos = GisUtils.Point(e.X, e.Y)
            oldPos2 = GisUtils.Point(e.X, e.Y)
            oldRadius = 0
        End Sub

        Private Sub GIS_MouseMove(ByVal sender As Object, ByVal e As AxTatukGIS_XDK11.ITGIS_ViewerWndEvents_MouseMoveEvent) Handles GIS.MouseMoveEvent
            If GIS.IsEmpty Then
                Return
            End If

            If GIS.Mode <> TGIS_ViewerMode.Select Then
                Return
            End If

            If e.Shift <> TShiftState.ssLeft Then
                Exit Sub
            End If


            If btnRectangle.Checked = True Then
                oldPos2 = GisUtils.Point(e.X, e.Y)
            End If

            If btnCircle.Checked = True Then
                oldRadius = CType(Math.Round(Math.Sqrt(Math.Pow(oldPos.X - e.X, 2) + Math.Pow(oldPos.Y - e.Y, 2))), Integer)
            End If

            GIS.Invalidate()
        End Sub

        Private Sub GIS_MouseUp(ByVal sender As Object, ByVal e As AxTatukGIS_XDK11.ITGIS_ViewerWndEvents_MouseUpEvent) Handles GIS.MouseUpEvent
            Dim tpl As TGIS_Topology
            Dim ll As TGIS_LayerVector
            Dim tmp As TGIS_Shape
            Dim buf As TGIS_Shape
            Dim ptg, ptg1, ptg2 As New TGIS_Point
            Dim distance As Double

            buf = Nothing

            If GIS.IsEmpty Then
                Return
            End If

            If e.Button = TMouseButton.mbRight Then
                GIS.Mode = TGIS_ViewerMode.Select
                Return
            End If

            If btnRectangle.Checked Then
                If ((oldPos2.X = oldPos.X) And (oldPos2.Y = oldPos.Y)) Then
                    Return
                End If
            End If

            If btnCircle.Checked Then
                If oldRadius = 0 Then
                    Return
                End If
            End If

            ll = CType(GIS.Get("Points"), TGIS_LayerVector)
            ll.Lock()
            tmp = ll.CreateShape(TGIS_ShapeType.Point)
            tmp.Params.Marker.Size = 0
            tmp.AddPart()

            If btnCircle.Checked Then
                ptg = GIS.ScreenToMap(oldPos)
                tmp = ll.CreateShape(TGIS_ShapeType.Point)
                tmp.Params.Marker.Size = 0
                tmp.Lock(TGIS_Lock.Extent)
                tmp.AddPart()
                tmp.AddPoint(ptg)
                tmp.Unlock()
                ll.Unlock()
                ptg1 = GIS.ScreenToMap(GisUtils.Point(oldPos.X + CType(oldRadius, Integer), e.Y))
            End If

            If btnRectangle.Checked Then
                ptg = GIS.ScreenToMap(oldPos)
                tmp.AddPoint(ptg)
                tmp.Unlock()
                tmp = ll.CreateShape(TGIS_ShapeType.Point)
                tmp.Params.Marker.Size = 0
                tmp.Lock(TGIS_Lock.Extent)
                tmp.AddPart()
                ptg = GIS.ScreenToMap(oldPos2)
                tmp.AddPoint(ptg)
                tmp.Unlock()
                ll.Unlock()
                ptg1 = GIS.ScreenToMap(oldPos)
            End If

            ll = CType(GIS.Get("Buffers"), TGIS_LayerVector)
            ll.RevertShapes()

            If btnCircle.Checked Then
                distance = ptg1.X - ptg.X
                tpl = New TGIS_Topology()
                buf = tpl.MakeBuffer_2(tmp, distance, 32, True)
                buf = ll.AddShape(buf)
            End If

            If btnRectangle.Checked Then
                ptg2 = GIS.ScreenToMap(oldPos2)
                buf = ll.CreateShape(TGIS_ShapeType.Polygon)
                buf.AddPart()
                buf.AddPoint(ptg1)
                buf.AddPoint(GisUtils.GisPoint(ptg1.X, ptg2.Y))
                buf.AddPoint(ptg2)
                buf.AddPoint(GisUtils.GisPoint(ptg2.X, ptg1.Y))
            End If

            ll = CType(GIS.Items.Item(0), TGIS_LayerVector)
            If ll Is Nothing Then
                GIS.InvalidateWholeMap()
                Return
            End If

            ll.DeselectAll()

            textBox1.Clear()

            GIS.InvalidateWholeMap()
            GIS.Lock()
            tmp = ll.FindFirst_4(buf.Extent, "", buf, GisUtils.GIS_RELATE_INTERSECT)
            Do While Not tmp Is Nothing
                textBox1.AppendText(tmp.GetField("name").ToString + vbCrLf)
                tmp.IsSelected = True
                tmp = ll.FindNext
            Loop

            GIS.Unlock()
        End Sub

        Private Sub GIS_PaintExtraEvent(sender As Object, e As AxTatukGIS_XDK11.ITGIS_ViewerWndEvents_PaintExtraEventEvent) Handles GIS.PaintExtraEvent
            Dim rdr As TGIS_RendererAbstract
            Dim rnd As Random

            e.Translated = True
            rnd = New Random()
            rdr = e.Renderer
            rdr.CanvasPen.Width = 1
            rdr.CanvasPen.Color = (New TGIS_Color).FromBGR_2(CType(Rnd.Next(&HFFFFFF), UInteger))
            rdr.CanvasPen.Style = TGIS_PenStyle.Solid
            rdr.CanvasBrush.Style = TGIS_BrushStyle.Clear

            If btnRectangle.Checked Then
                If ((oldPos.X = oldPos2.X) And (oldPos2.Y = oldPos.Y)) Then
                    Return
                End If
                rdr.CanvasDrawRectangle(GisUtils.Rect(oldPos.X, oldPos.Y, oldPos2.X - oldPos.X, oldPos2.Y - oldPos.Y))
            End If

            If btnCircle.Checked Then
                rdr.CanvasDrawEllipse(oldPos.X - CType(Math.Round(oldRadius), Integer), oldPos.Y - CType(Math.Round(oldRadius), Integer), CType(oldRadius * 2, Integer), CType(oldRadius * 2, Integer))
            End If

        End Sub

        Private Sub btnCircle_CheckedChanged(sender As Object, e As EventArgs) Handles btnCircle.CheckedChanged
            btnRectangle.Checked = Not btnCircle.Checked
        End Sub

        Private Sub btnRectangle_CheckedChanged(sender As Object, e As EventArgs) Handles btnRectangle.CheckedChanged
            btnCircle.Checked = Not btnRectangle.Checked
        End Sub
    End Class
End Namespace
