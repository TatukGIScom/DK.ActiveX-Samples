' DragLabel sample — demonstrates interactive draggable feature labels on maps (ActiveX/COM edition).
'
' What the sample shows:
'   - Two parallel in-memory vector layers: realpoints (features) and sidekicks (labels)
'   - Realpoints layer with visible CGM ship symbols rendered as point markers
'   - Storing ship name attributes for use as label text
'   - Sidekicks layer with invisible ghost points carrying floating labels
'   - UID matching between realpoints and sidekicks for efficient lookups
'   - Custom label rendering via PaintShapeLabelEvent callback
'   - Drawing connecting leader line from real position to label position
'   - Manual editor control for drag-to-reposition interaction
'   - Converting screen click positions to map coordinates with ScreenToMap
'   - Setting sidekick shape position during drag operation
'   - Programmatic movement loop demonstrating simultaneous realpoint/sidekick translation
'
' ActiveX/COM-specific details:
'   - AxTGIS_ViewerWnd is the ActiveX wrapper for the TatukGIS viewer control
'   - Mouse events arrive via COM event interfaces, not standard .NET MouseEventArgs
'   - GisUtils.Point() used for coordinate helpers instead of System.Drawing.Point
'   - ActiveX/COM layer expects TatukGIS-native point/rect types
'   - Same design pattern as NDK variant but with COM interop layer
'
' Key TatukGIS API concepts shown here:
'   TGIS_ViewerWnd (via AxTGIS_ViewerWnd) - main visual map control
'   TGIS_LayerVector                       - in-memory vector layer for features
'   TGIS_Shape                             - individual geographic feature
'   PaintShapeLabelEvent (callback)        - per-shape label rendering hook
'   TGIS_RendererAbstract                  - platform-agnostic drawing interface
'   GIS.Locate()                           - hit-test to find shape at point
'   GIS.MapToScreen()                      - convert geographic coords to screen pixels
'   GIS.ScreenToMap()                      - convert screen pixels to geographic coordinates
'   GIS.Editor.MouseBegin()                - start manual shape editing/dragging
'   TGIS_Shape.SetPosition()               - reposition shape in geographic space
' =============================================================================

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
    ''' Main form for the DragLabel ActiveX sample.
    ''' Uses the AxTGIS_ViewerWnd COM control and TatukGIS_XDK11 type library.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form

        ''' <summary>Required designer variable.</summary>
        Private components As System.ComponentModel.Container = Nothing
        Private statusBar1 As System.Windows.Forms.StatusBar          ' status bar
        Private WithEvents toolBar1 As System.Windows.Forms.ToolBar   ' toolbar
        Private toolBarButton1 As System.Windows.Forms.ToolBarButton  ' "Animate" button
        Private WithEvents GIS As AxTatukGIS_XDK11.AxTGIS_ViewerWnd  ' ActiveX GIS viewer
        Private Const LABEL_TEXT As String = "Ship "

        ''' <summary>
        ''' The sidekick shape currently being dragged. Nothing when idle.
        ''' </summary>
        Private currShape As TGIS_Shape


        Public Sub New()
            InitializeComponent()
        End Sub

        ''' <summary>Clean up any resources being used.</summary>
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
            'GIS - ActiveX control hosted in the form
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

        ' GisUtils provides helper factory methods (GisPoint, GisExtent, Point, Rect)
        ' for the ActiveX COM layer which does not expose .NET static helpers directly.
        Dim GisUtils As New TGIS_Utils()

        ''' <summary>The main entry point for the application.</summary>
        <STAThread>
        Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New WinForm())
        End Sub

        ' EventLayer holds a WithEvents reference to the sidekicks layer so
        ' VB.NET can wire the PaintShapeLabelEvent through the Handles keyword.
        ' The ActiveX COM event model requires a named variable for event binding.
        Private WithEvents EventLayer As TGIS_LayerVector

        ''' <summary>
        ''' WinForm_Load - builds the two-layer data model and populates 20
        ''' randomly positioned ship points.
        '''
        ''' Note: GisUtils instance methods (GisPoint, GisExtent, SymbolList, etc.)
        ''' are used in this ActiveX version because the COM type library does not
        ''' expose static TGIS_Utils members directly as in the .NET NDK.
        ''' </summary>
        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim ll As TGIS_LayerVector
            Dim shp As TGIS_Shape
            Dim ptg As TGIS_Point
            Dim rnd As Random
            Dim i As Integer

            ' --- "realpoints" layer ---
            ll = New TGIS_LayerVector()
            ' GisUtils.SymbolList.Prepare caches and compiles the CGM symbol.
            ll.Params.Marker.Symbol = GisUtils.SymbolList.Prepare(GisUtils.GisSamplesDataDirDownload() & "\Symbols\2267.cgm")
            ll.CachedPaint = False  ' disable tile cache for moving shapes
            ll.Name = "realpoints"

            GIS.Add(ll)
            ll.AddField("name", TGIS_FieldType.String, 100, 0)
            ll.Extent = GisUtils.GisExtent(-180, -180, 180, 180)  ' world-spanning

            ' --- "sidekicks" layer ---
            ll = New TGIS_LayerVector()
            ll.Name = "sidekicks"
            ll.Params.Marker.Size = 0              ' invisible marker
            ll.Params.Labels.Position = TGIS_LabelPosition.MiddleCenter
            ll.CachedPaint = False
            ' EventLayer is the WithEvents holder used to bind PaintShapeLabelEvent
            EventLayer = ll

            GIS.Add(ll)
            ' Wire the custom label painter via AddHandler on the EventLayer reference
            AddHandler EventLayer.PaintShapeLabelEvent, AddressOf doLabelPaint
            ' Disable auto label-placement so labels stay at exact sidekick positions
            ll.Params.Labels.Allocator = False

            GIS.FullExtent()

            ' --- Populate with 20 random ship points ---
            rnd = New Random()
            For i = 0 To 19
                ' GisUtils.GisPoint creates a TGIS_Point COM object
                ptg = GisUtils.GisPoint(rnd.Next(360) - 180, rnd.Next(180) - 90)

                ' Create and configure the visible real point
                shp = (CType(GIS.Get("realpoints"), TGIS_LayerVector)).CreateShape(TGIS_ShapeType.Point)
                shp.Lock(TGIS_Lock.Extent)
                shp.AddPart()
                shp.AddPoint(ptg)
                shp.Params.Marker.SymbolRotate = shp.Uid  ' unique rotation per ship
                shp.Params.Marker.Size = 250
                shp.Params.Marker.Color = (New TGIS_Color).FromRGB(rnd.Next(256), rnd.Next(256), rnd.Next(256))

                shp.SetField("name", String.Format(LABEL_TEXT & ": {0}", shp.Uid))
                shp.Unlock()

                ' Create the matching sidekick offset by 15 pixels in map units
                shp = (CType(GIS.Get("sidekicks"), TGIS_LayerVector)).CreateShape(TGIS_ShapeType.Point)
                shp.Lock(TGIS_Lock.Extent)
                shp.AddPart()

                ' 15 / GIS.Zoom converts 15 screen pixels to the current map-unit scale
                ptg.X = ptg.X + 15 / GIS.Zoom
                ptg.Y = ptg.Y + 15 / GIS.Zoom
                shp.AddPoint(ptg)
                shp.Unlock()
            Next i

            GIS.FullExtent()
        End Sub

        ''' <summary>
        ''' Animate toolbar button handler.
        ''' Moves shape UID=5 and its sidekick 90 steps of (2,1) map units.
        ''' </summary>
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

        ''' <summary>
        ''' Moves the given real-point shape and its matching sidekick by
        ''' (_x, _y) map units simultaneously.
        ''' The TGIS_Extent ex is the union of both positions for optional
        ''' selective invalidation calls.
        ''' </summary>
        Private Sub synchroMove(ByVal _shp As TGIS_Shape, ByVal _x As Integer, ByVal _y As Integer)
            Dim ll As TGIS_LayerVector
            Dim shp As TGIS_Shape
            Dim ptgA As TGIS_Point
            Dim ptgB As TGIS_Point
            Dim ex As New TGIS_Extent

            ' Shift the real point
            ptgA = _shp.GetPoint(0, 0)
            ptgA.X = ptgA.X + _x
            ptgA.Y = ptgA.Y + _y
            _shp.SetPosition(ptgA, Nothing, 0)

            ' Locate and shift the matching sidekick
            ll = CType(GIS.Get("sidekicks"), TGIS_LayerVector)
            shp = ll.GetShape(_shp.Uid)
            ptgB = shp.GetPoint(0, 0)
            ptgB.X = ptgB.X + _x
            ptgB.Y = ptgB.Y + _y
            shp.SetPosition(ptgB, Nothing, 0)

            ' Bounding extent of both positions
            ex.XMin = Math.Min(ptgA.X, ptgB.X)
            ex.YMin = Math.Min(ptgA.Y, ptgB.Y)
            ex.XMax = Math.Max(ptgA.X, ptgB.X)
            ex.YMax = Math.Max(ptgA.Y, ptgB.Y)
        End Sub

        ''' <summary>
        ''' PaintShapeLabelEvent callback - fired for each sidekick during repaint.
        '''
        ''' In the ActiveX variant the signature is different from NDK:
        '''   Translated (ByRef Boolean) must be set to True to signal that the
        '''   event has been handled and the default label rendering should be
        '''   suppressed (we call DrawLabel ourselves).
        '''   Shape is the sidekick being painted.
        '''
        ''' Steps:
        '''   1. Look up the matching realpoint by UID.
        '''   2. Convert both positions to screen pixels via GIS.MapToScreen
        '''      (in the ActiveX version this is called directly on the viewer).
        '''   3. Draw the blue leader line on the abstract renderer canvas.
        '''   4. Set the label value and call DrawLabel.
        ''' </summary>
        Private Sub doLabelPaint(ByRef Translated As Boolean, Shape As TGIS_Shape)
            Dim ptA, ptB As TPoint
            Dim ll As TGIS_LayerVector
            Dim shp As TGIS_Shape
            Dim rnd As TGIS_RendererAbstract

            ' Signal that we are handling label rendering ourselves
            Translated = True

            ' Get the abstract renderer from the viewer
            rnd = CType(GIS.Renderer, TGIS_RendererAbstract)

            ' Find the matching realpoint by UID
            ll = CType(GIS.Get("realpoints"), TGIS_LayerVector)
            shp = ll.GetShape(Shape.Uid)

            ' MapToScreen converts geographic TGIS_Point coordinates to screen pixels.
            ' In the ActiveX variant this is called directly on the GIS viewer object.
            ptA = GIS.MapToScreen(shp.GetPoint(0, 0))    ' real symbol position
            ptB = GIS.MapToScreen(Shape.GetPoint(0, 0))   ' floating label anchor

            ' Draw the blue leader line
            rnd.CanvasPen.Color = (New TGIS_Color).Blue
            rnd.CanvasPen.Style = TGIS_PenStyle.Solid
            rnd.CanvasPen.Width = 1
            rnd.CanvasDrawLine(ptA.X, ptA.Y, ptB.X, ptB.Y)

            ' Set label text from the real feature and render it
            Shape.Params.Labels.Value = shp.GetField("name").ToString()
            Shape.DrawLabel()
        End Sub

        ''' <summary>
        ''' Begins a label drag when the user clicks near a sidekick shape.
        ''' Uses the COM mouse-down event interface specific to the ActiveX viewer.
        ''' GisUtils.Point wraps (e.X, e.Y) into a TGIS-compatible TPoint for
        ''' ScreenToMap and Editor.MouseBegin.
        ''' </summary>
        Private Sub GIS_MouseDown(sender As Object, e As ITGIS_ViewerWndEvents_MouseDownEvent) Handles GIS.MouseDownEvent
            Dim ll As TGIS_LayerVector
            Dim shp As TGIS_Shape

            If GIS.IsEmpty Then Return
            If GIS.InPaint Then Return

            ' Find the sidekick nearest the click, within 100-pixel tolerance
            ll = CType(GIS.Get("sidekicks"), TGIS_LayerVector)
            shp = ll.Locate(GIS.ScreenToMap(GisUtils.Point(e.X, e.Y)), 100 / GIS.Zoom)
            currShape = shp
            If currShape Is Nothing Then Return

            ' Initiate editor drag tracking without switching the global viewer mode
            GIS.Editor.MouseBegin(GisUtils.Point(e.X, e.Y), True)
        End Sub

        ''' <summary>
        ''' Repositions the tracked sidekick to follow the mouse cursor.
        ''' GisUtils.GisIsPointInsideExtent prevents dragging outside the map extent.
        ''' </summary>
        Private Sub GIS_MouseMove(ByVal sender As Object, ByVal e As ITGIS_ViewerWndEvents_MouseMoveEvent) Handles GIS.MouseMoveEvent
            Dim ll As TGIS_LayerVector
            Dim shp As TGIS_Shape
            Dim ptgA As TGIS_Point
            Dim ptgB As TGIS_Point
            Dim ex As New TGIS_Extent

            If GIS.IsEmpty Then Return
            If currShape Is Nothing Then Return

            ' Pre-move extent
            ptgA = currShape.GetPoint(0, 0)
            ll = CType(GIS.Get("realpoints"), TGIS_LayerVector)
            shp = ll.GetShape(currShape.Uid)
            ptgB = shp.GetPoint(0, 0)
            ex.XMin = Math.Min(ptgA.X, ptgB.X)
            ex.YMin = Math.Min(ptgA.Y, ptgB.Y)
            ex.XMax = Math.Max(ptgA.X, ptgB.X)
            ex.YMax = Math.Max(ptgA.Y, ptgB.Y)

            ' Reproject mouse pixel to map coords and move the sidekick
            ptgA = GIS.ScreenToMap(GisUtils.Point(e.X, e.Y))
            If GisUtils.GisIsPointInsideExtent(ptgA, GIS.Extent) Then
                currShape.SetPosition(ptgA, Nothing, 0)
            End If

            ' Post-move extent
            ptgA = currShape.GetPoint(0, 0)
            ll = CType(GIS.Get("realpoints"), TGIS_LayerVector)
            shp = ll.GetShape(currShape.Uid)
            ptgB = shp.GetPoint(0, 0)
            ex.XMin = Math.Min(ptgA.X, ptgB.X)
            ex.YMin = Math.Min(ptgA.Y, ptgB.Y)
            ex.XMax = Math.Max(ptgA.X, ptgB.X)
            ex.YMax = Math.Max(ptgA.Y, ptgB.Y)
        End Sub

        ''' <summary>
        ''' Ends the current label drag by clearing the tracked shape reference.
        ''' </summary>
        Private Sub GIS_MouseUp(ByVal sender As Object, ByVal e As ITGIS_ViewerWndEvents_MouseUpEvent) Handles GIS.MouseUpEvent
            If GIS.IsEmpty Then Return
            currShape = Nothing
        End Sub
    End Class
End Namespace
