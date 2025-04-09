Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports System.Threading
Imports TatukGIS_XDK11
Imports AxTatukGIS_XDK11

Namespace DragLabel
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
        Private WithEvents toolBar1 As System.Windows.Forms.ToolBar
        Private toolBarButton1 As System.Windows.Forms.ToolBarButton
        Private WithEvents GIS As AxTatukGIS_XDK11.AxTGIS_ViewerWnd
        Private Const LABEL_TEXT As String = "Ship "
        Private currShape As TGIS_Shape


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
            Me.toolBar1 = New System.Windows.Forms.ToolBar()
            Me.toolBarButton1 = New System.Windows.Forms.ToolBarButton()
            Me.GIS = New AxTatukGIS_XDK11.AxTGIS_ViewerWnd()
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'statusBar1
            '
            Me.statusBar1.Location = New System.Drawing.Point(0, 447)
            Me.statusBar1.Name = "statusBar1"
            Me.statusBar1.Size = New System.Drawing.Size(586, 19)
            Me.statusBar1.TabIndex = 0
            '
            'toolBar1
            '
            Me.toolBar1.AutoSize = False
            Me.toolBar1.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.toolBarButton1})
            Me.toolBar1.ButtonSize = New System.Drawing.Size(83, 23)
            Me.toolBar1.DropDownArrows = True
            Me.toolBar1.Location = New System.Drawing.Point(0, 0)
            Me.toolBar1.Name = "toolBar1"
            Me.toolBar1.ShowToolTips = True
            Me.toolBar1.Size = New System.Drawing.Size(586, 29)
            Me.toolBar1.TabIndex = 1
            Me.toolBar1.TextAlign = System.Windows.Forms.ToolBarTextAlign.Right
            '
            'toolBarButton1
            '
            Me.toolBarButton1.Name = "toolBarButton1"
            Me.toolBarButton1.Text = "Animate"
            '
            'GIS
            '
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.Enabled = True
            Me.GIS.Location = New System.Drawing.Point(0, 29)
            Me.GIS.Name = "GIS"
            Me.GIS.OcxState = CType(resources.GetObject("GIS.OcxState"), System.Windows.Forms.AxHost.State)
            Me.GIS.Size = New System.Drawing.Size(586, 418)
            Me.GIS.TabIndex = 2
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(586, 466)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.toolBar1)
            Me.Controls.Add(Me.statusBar1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Draggable Labels"
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

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

        Private WithEvents EventLayer As TGIS_LayerVector

        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim ll As TGIS_LayerVector
            Dim shp As TGIS_Shape
            Dim ptg As TGIS_Point
            Dim rnd As Random
            Dim i As Integer

            ' create real point layer
            ll = New TGIS_LayerVector()
            ll.Params.Marker.Symbol = GisUtils.SymbolList.Prepare(GisUtils.GisSamplesDataDirDownload() & "\Symbols\2267.cgm")
            ll.CachedPaint = False
            ll.Name = "realpoints"

            GIS.Add(ll)
            ll.AddField("name", TGIS_FieldType.String, 100, 0)
            ll.Extent = GisUtils.GisExtent(-180, -180, 180, 180)

            ' create display sidekick
            ll = New TGIS_LayerVector()
            ll.Name = "sidekicks"
            ll.Params.Marker.Size = 0
            ll.Params.Labels.Position = TGIS_LabelPosition.MiddleCenter
            ll.CachedPaint = False
            EventLayer = ll

            GIS.Add(ll)
            AddHandler EventLayer.PaintShapeLabelEvent, AddressOf doLabelPaint
            ll.Params.Labels.Allocator = False

            GIS.FullExtent()

            ' add points
            rnd = New Random()
            For i = 0 To 19
                ptg = GisUtils.GisPoint(rnd.Next(360) - 180, rnd.Next(180) - 90)

                ' add a real point
                shp = (CType(GIS.Get("realpoints"), TGIS_LayerVector)).CreateShape(TGIS_ShapeType.Point)
                shp.Lock(TGIS_Lock.Extent)
                shp.AddPart()
                shp.AddPoint(ptg)
                shp.Params.Marker.SymbolRotate = shp.Uid
                shp.Params.Marker.Size = 250
                shp.Params.Marker.Color = (New TGIS_Color).FromRGB(rnd.Next(256), rnd.Next(256), rnd.Next(256))

                shp.SetField("name", String.Format(LABEL_TEXT & ": {0}", shp.Uid))
                shp.Unlock()

                ' add sideckicks
                shp = (CType(GIS.Get("sidekicks"), TGIS_LayerVector)).CreateShape(TGIS_ShapeType.Point)
                shp.Lock(TGIS_Lock.Extent)
                shp.AddPart()

                ' with a small offset
                ptg.X = ptg.X + 15 / GIS.Zoom
                ptg.Y = ptg.Y + 15 / GIS.Zoom
                shp.AddPoint(ptg)
                shp.Unlock()
            Next i

            GIS.FullExtent()
        End Sub

        Private Sub toolBar1_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles toolBar1.ButtonClick
            Dim i As Integer
            Dim shp As TGIS_Shape

            Select Case toolBar1.Buttons.IndexOf(e.Button)
                Case 0
                    shp = (CType(GIS.Get("realpoints"), TGIS_LayerVector)).GetShape(5)
                    For i = 0 To 89
                        If Me.IsDisposed Then
                            Exit For
                        End If
                        synchroMove(shp, 2, 1)
                        Thread.Sleep(50)
                        Application.DoEvents()
                    Next i
            End Select
        End Sub

        Private Sub synchroMove(ByVal _shp As TGIS_Shape, ByVal _x As Integer, ByVal _y As Integer)
            Dim ll As TGIS_LayerVector
            Dim shp As TGIS_Shape
            Dim ptgA As TGIS_Point
            Dim ptgB As TGIS_Point
            Dim ex As New TGIS_Extent

            ' move main shape
            ptgA = _shp.GetPoint(0, 0)
            ptgA.X = ptgA.X + _x
            ptgA.Y = ptgA.Y + _y
            _shp.SetPosition(ptgA, Nothing, 0)

            ' move "sidekick"
            ll = CType(GIS.Get("sidekicks"), TGIS_LayerVector)
            shp = ll.GetShape(_shp.Uid)
            ptgB = shp.GetPoint(0, 0)
            ptgB.X = ptgB.X + _x
            ptgB.Y = ptgB.Y + _y
            shp.SetPosition(ptgB, Nothing, 0)

            ' aditional invalidation - we have now a starnge big
            ' combo shape
            ex.XMin = Math.Min(ptgA.X, ptgB.X)
            ex.YMin = Math.Min(ptgA.Y, ptgB.Y)
            ex.XMax = Math.Max(ptgA.X, ptgB.X)
            ex.YMax = Math.Max(ptgA.Y, ptgB.Y)
        End Sub

        Private Sub doLabelPaint(ByRef Translated As Boolean, Shape As TGIS_Shape)
            Dim ptA, ptB As TPoint
            Dim ll As TGIS_LayerVector

            Dim shp As TGIS_Shape
            Dim rnd As TGIS_RendererAbstract

            Translated = True

            rnd = CType(GIS.Renderer, TGIS_RendererAbstract)

            ' draw line to real point
            ll = CType(GIS.Get("realpoints"), TGIS_LayerVector)
            shp = ll.GetShape(Shape.Uid)
            ptA = GIS.MapToScreen(shp.GetPoint(0, 0))
            ptB = GIS.MapToScreen(Shape.GetPoint(0, 0))
            rnd.CanvasPen.Color = (New TGIS_Color).Blue
            rnd.CanvasPen.Style = TGIS_PenStyle.Solid
            rnd.CanvasPen.Width = 1
            rnd.CanvasDrawLine(ptA.X, ptA.Y, ptB.X, ptB.Y)

            ' draw label itself
            Shape.Params.Labels.Value = shp.GetField("name").ToString()
            Shape.DrawLabel()
        End Sub

        Private Sub GIS_MouseDown(sender As Object, e As ITGIS_ViewerWndEvents_MouseDownEvent) Handles GIS.MouseDownEvent
            Dim ll As TGIS_LayerVector
            Dim shp As TGIS_Shape

            If GIS.IsEmpty Then
                Return
            End If
            If GIS.InPaint Then
                Return
            End If

            ' start editing of some shape from sidekicks
            ll = CType(GIS.Get("sidekicks"), TGIS_LayerVector)
            shp = ll.Locate(GIS.ScreenToMap(GisUtils.Point(e.X, e.Y)), 100 / GIS.Zoom)
            currShape = shp
            If currShape Is Nothing Then
                Return
            End If
            ' we are not chnging the GIS.Mode to gisEdit because we want to control
            ' editing on our own, so instead we will call MouseBegin, MouseMove and MouseEnd
            ' "manually"
            GIS.Editor.MouseBegin(GisUtils.Point(e.X, e.Y), True)
        End Sub

        Private Sub GIS_MouseMove(ByVal sender As Object, ByVal e As ITGIS_ViewerWndEvents_MouseMoveEvent) Handles GIS.MouseMoveEvent
            Dim ll As TGIS_LayerVector
            Dim shp As TGIS_Shape
            Dim ptgA As TGIS_Point
            Dim ptgB As TGIS_Point
            Dim ex As New TGIS_Extent

            If GIS.IsEmpty Then
                Return
            End If
            If currShape Is Nothing Then
                Return
            End If
            ' aditional invalidation - we have now a strange big
            ' combo shape
            ptgA = currShape.GetPoint(0, 0)
            ll = CType(GIS.Get("realpoints"), TGIS_LayerVector)
            shp = ll.GetShape(currShape.Uid)
            ptgB = shp.GetPoint(0, 0)
            ex.XMin = Math.Min(ptgA.X, ptgB.X)
            ex.YMin = Math.Min(ptgA.Y, ptgB.Y)
            ex.XMax = Math.Max(ptgA.X, ptgB.X)
            ex.YMax = Math.Max(ptgA.Y, ptgB.Y)

            ptgA = GIS.ScreenToMap(GisUtils.Point(e.X, e.Y))
            If GisUtils.GisIsPointInsideExtent(ptgA, GIS.Extent) Then
                currShape.SetPosition(ptgA, Nothing, 0)
            End If

            ' aditional invalidation - we have now a starnge big
            ' combo shape
            ptgA = currShape.GetPoint(0, 0)
            ll = CType(GIS.Get("realpoints"), TGIS_LayerVector)
            shp = ll.GetShape(currShape.Uid)
            ptgB = shp.GetPoint(0, 0)
            ex.XMin = Math.Min(ptgA.X, ptgB.X)
            ex.YMin = Math.Min(ptgA.Y, ptgB.Y)
            ex.XMax = Math.Max(ptgA.X, ptgB.X)
            ex.YMax = Math.Max(ptgA.Y, ptgB.Y)
        End Sub

        Private Sub GIS_MouseUp(ByVal sender As Object, ByVal e As ITGIS_ViewerWndEvents_MouseUpEvent) Handles GIS.MouseUpEvent
            If GIS.IsEmpty Then
                Return
            End If
            currShape = Nothing
        End Sub
    End Class
End Namespace
