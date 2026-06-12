' =============================================================================
' This source code is a part of TatukGIS Developer Kernel.
' =============================================================================
'
' CustomPaint sample — demonstrates custom painting on TatukGIS map (VB.NET ActiveX/COM).
'
' What the sample shows:
'   - Custom drawing on top of TatukGIS ActiveX viewer via paint-hook callbacks
'   - PaintShapeEvent for per-shape custom rendering
'   - PaintExtraEvent for overlay graphics after all layers rendered
'   - Combining GIS rendering with custom drawing primitives
'   - Drawing rectangles, ellipses, and custom shapes
'   - Loading bitmap resources and rendering them on map
'   - Text rendering using GisUtils methods
'   - Geographic-to-screen coordinate conversion
'   - Per-shape styling customization
'   - HUD elements and watermark rendering
'   - ActiveX control integration (AxTGIS_ViewerWnd)
'
' Key TatukGIS API concepts shown here:
'   TGIS_ViewerWnd              - main visual map control (COM wrapper)
'   AxTGIS_ViewerWnd            - ActiveX host wrapper for VB.NET
'   PaintShapeEvent             - per-shape custom rendering hook (COM)
'   PaintExtraEvent             - overlay rendering hook (COM)
'   GisUtils                    - utility methods for COM objects
'   CanvasDrawRectangle         - draw rectangle primitive
'   CanvasDrawEllipse           - draw ellipse primitive
'   CanvasDrawBitmap            - render bitmap image
'   CanvasDrawText              - render text string
'   MapToScreen conversion      - geographic to screen coordinates
'   - EventLayer is a WithEvents variable holding the layer reference so VB.NET
'     can wire events through the Handles keyword.
'   - Renderer switching is done via a button (btnChangeRenderer) that cycles
'     between GDI32 and GDI+ backends rather than a combo box.
'   - PaintExtraEvent provides access to the renderer's native canvas handle
'     for platform-specific drawing (GDI32 or GDI+ branches shown here).
'
' Layer layout:
'   One TGIS_LayerVector ("CustomPaint") with four invisible point shapes.
'   A "type" string field selects the drawing primitive in GIS_PaintShapeEvent:
'     "Rectangle" at (-25, 25)  -> CanvasDrawRectangle (red border, yellow fill)
'     "Ellipse"   at ( 25, 25)  -> CanvasDrawEllipse   (black border, red fill)
'     "Image1"    at (-25,-25)  -> CanvasDrawBitmap(TGIS_Bitmap from file)
'     "Image2"    at ( 25,-25)  -> CanvasDrawBitmap_2(TGIS_Pixels raw pixel array)
' =============================================================================

Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS_XDK11
Imports AxTatukGIS_XDK11

Namespace CustomPaint
    ''' <summary>
    ''' Main form for the CustomPaint ActiveX sample.
    ''' Uses the AxTGIS_ViewerWnd COM control and TatukGIS_XDK11 type library.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form
        ''' <summary>Required designer variable.</summary>
        Private components As System.ComponentModel.Container = Nothing
        ''' <summary>The "CustomPaint" in-memory vector layer.</summary>
        Private ll As TGIS_LayerVector
        ''' <summary>ActiveX GIS viewer (COM-hosted control).</summary>
        Private WithEvents GIS As AxTatukGIS_XDK11.AxTGIS_ViewerWnd
        ''' <summary>Button to cycle the active renderer between GDI32 and GDI+.</summary>
        Friend WithEvents btnChangeRenderer As Button
        ''' <summary>Bitmap loaded from police.bmp for the Image1 drawing case.</summary>
        Private bmp As TGIS_Bitmap
        ''' <summary>
        ''' Raw ARGB pixel array used for the Image2 drawing case.
        ''' TGIS_Pixels is the COM-compatible pixel buffer type for the ActiveX layer.
        ''' </summary>
        Private px As TGIS_Pixels
        ''' <summary>
        ''' WithEvents holder for the layer so VB.NET can wire PaintShapeEvent
        ''' through the AddHandler pattern with the COM event model.
        ''' </summary>
        Private WithEvents EventLayer As TGIS_LayerVector

        Public Sub New()
            '
            ' Required for Windows Form Designer support
            '
            InitializeComponent()

            '
            ' TODO: Add any constructor code after InitializeComponent call
            '
            Me.ActiveControl = GIS
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
            Me.GIS = New AxTatukGIS_XDK11.AxTGIS_ViewerWnd()
            Me.btnChangeRenderer = New System.Windows.Forms.Button()
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'GIS - ActiveX control hosted in the form
            '
            Me.GIS.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GIS.Enabled = True
            Me.GIS.Location = New System.Drawing.Point(0, 40)
            Me.GIS.Margin = New System.Windows.Forms.Padding(4)
            Me.GIS.Name = "GIS"
            Me.GIS.OcxState = CType(resources.GetObject("GIS.OcxState"), System.Windows.Forms.AxHost.State)
            Me.GIS.Size = New System.Drawing.Size(595, 426)
            Me.GIS.TabIndex = 0
            '
            'btnChangeRenderer - cycles between GDI32 and GDI+ backends
            '
            Me.btnChangeRenderer.Location = New System.Drawing.Point(12, 12)
            Me.btnChangeRenderer.Margin = New System.Windows.Forms.Padding(4)
            Me.btnChangeRenderer.Name = "btnChangeRenderer"
            Me.btnChangeRenderer.Size = New System.Drawing.Size(122, 23)
            Me.btnChangeRenderer.TabIndex = 0
            Me.btnChangeRenderer.Text = "Change to GDI+"
            Me.btnChangeRenderer.UseVisualStyleBackColor = True
            '
            'WinForm
            '
            Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
            Me.ClientSize = New System.Drawing.Size(595, 466)
            Me.Controls.Add(Me.btnChangeRenderer)
            Me.Controls.Add(Me.GIS)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - CustomPaint"
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
#End Region

        ' GisUtils provides instance-method wrappers (GisPoint, GisExtent, Rect, Point,
        ' SymbolList, etc.) for the ActiveX COM layer.  In the ActiveX API these are
        ' instance methods rather than the static members found in the NDK API.
        Dim GisUtils As New TGIS_Utils()

        ''' <summary>The main entry point for the application.</summary>
        <STAThread>
        Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New WinForm())
        End Sub

        ''' <summary>
        ''' Initialises the TGIS_Pixels raw ARGB buffer used for the Image2 drawing case.
        '''
        ''' TGIS_Pixels is the COM-compatible pixel buffer type in the ActiveX layer.
        ''' px.SetLength(24) allocates 24 elements; px.Value(n) sets individual pixels.
        ''' The layout matches TGIS_BitmapFormat.ARGB (0xAARRGGBB per element):
        '''   Index 0-4:  opaque red   (0xFFFF0000)
        '''   Indices [7],[12],[17],[22]: opaque blue (0xFF0000FF) along the diagonal
        '''   All other indices: fully transparent (0x0)
        ''' </summary>
        Private Sub initialize_pixels()
            px = New TGIS_Pixels()
            px.SetLength(24)
            ' Row 0: five opaque red pixels
            px.Value(0) = CType(&HFFFF0000, Int32)
            px.Value(1) = CType(&HFFFF0000, Int32)
            px.Value(2) = CType(&HFFFF0000, Int32)
            px.Value(3) = CType(&HFFFF0000, Int32)
            px.Value(4) = CType(&HFFFF0000, Int32)
            ' Row 1: transparent, blue at column 2
            px.Value(5) = CType(&H0, Int32)
            px.Value(6) = CType(&H0, Int32)
            px.Value(7) = CType(&HFF0000FF, Int32)   ' opaque blue
            px.Value(8) = CType(&H0, Int32)
            px.Value(9) = CType(&H0, Int32)
            ' Row 2
            px.Value(10) = CType(&H0, Int32)
            px.Value(11) = CType(&H0, Int32)
            px.Value(12) = CType(&HFF0000FF, Int32)
            px.Value(13) = CType(&H0, Int32)
            px.Value(14) = CType(&H0, Int32)
            ' Row 3
            px.Value(15) = CType(&H0, Int32)
            px.Value(16) = CType(&H0, Int32)
            px.Value(17) = CType(&HFF0000FF, Int32)
            px.Value(18) = CType(&H0, Int32)
            px.Value(19) = CType(&H0, Int32)
            ' Row 4
            px.Value(20) = CType(&H0, Int32)
            px.Value(21) = CType(&H0, Int32)
            px.Value(22) = CType(&HFF0000FF, Int32)
            px.Value(23) = CType(&H0, Int32)
        End Sub

        ''' <summary>
        ''' WinForm_Load - builds the demo layer with four invisible point shapes
        ''' and registers the PaintShapeEvent callback via EventLayer.
        '''
        ''' Key ActiveX differences from the NDK variant:
        '''   - GisUtils.GisPoint() creates a TGIS_Point COM object.
        '''   - GisUtils.GisExtent() creates a TGIS_Extent COM object.
        '''   - EventLayer (WithEvents) is needed so the COM PaintShapeEvent
        '''     fires through the VB.NET event system.
        '''   - No renderer combo box; a button toggles GDI32 <-> GDI+.
        ''' </summary>
        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim shp As TGIS_Shape

            initialize_pixels()

            ll = New TGIS_LayerVector()
            ll.Name = "CustomPaint"
            ' EventLayer must be set before GIS.Add so that PaintShapeEvent
            ' fires through the WithEvents binding established below.
            EventLayer = ll
            GIS.Add(ll)

            ' Register the per-shape paint callback through AddHandler.
            ' GIS_PaintShapeEvent fires once per shape per repaint pass.
            AddHandler EventLayer.PaintShapeEvent, AddressOf GIS_PaintShapeEvent

            ' Add the "type" attribute field that the paint handler reads.
            ll.AddField("type", TGIS_FieldType.String, 100, 0)
            ll.Extent = GIS.Extent

            ' --- Create the four demo shapes ---
            ' GisUtils.GisPoint creates a TGIS_Point COM object from (x, y).

            shp = ll.CreateShape(TGIS_ShapeType.Point)
            shp.Lock(TGIS_Lock.Extent)
            shp.AddPart()
            shp.AddPoint(GisUtils.GisPoint(-25, 25))  ' NW quadrant
            shp.Params.Marker.Size = 0   ' invisible; custom draw handles all visuals
            shp.SetField("type", "Rectangle")
            shp.Unlock()

            shp = ll.CreateShape(TGIS_ShapeType.Point)
            shp.Lock(TGIS_Lock.Extent)
            shp.AddPart()
            shp.AddPoint(GisUtils.GisPoint(25, 25))   ' NE quadrant
            shp.Params.Marker.Size = 0
            shp.SetField("type", "Ellipse")
            shp.Unlock()

            shp = ll.CreateShape(TGIS_ShapeType.Point)
            shp.Lock(TGIS_Lock.Extent)
            shp.AddPart()
            shp.AddPoint(GisUtils.GisPoint(-25, -25)) ' SW quadrant
            shp.Params.Marker.Size = 0
            shp.SetField("type", "Image1")
            shp.Unlock()

            shp = ll.CreateShape(TGIS_ShapeType.Point)
            shp.Lock(TGIS_Lock.Extent)
            shp.AddPart()
            shp.AddPoint(GisUtils.GisPoint(25, -25))  ' SE quadrant
            shp.Params.Marker.Size = 0
            shp.SetField("type", "Image2")
            shp.Unlock()

            ' Set the layer extent to enclose all four shapes
            ll.Extent = GisUtils.GisExtent(-100, -100, 100, 100)

            ' Load the file-based bitmap for the Image1 drawing case
            bmp = New TGIS_Bitmap()
            bmp.LoadFromFile(GisUtils.GisSamplesDataDirDownload() + "Symbols\police.bmp")

            GIS.FullExtent()
        End Sub

        ''' <summary>
        ''' PaintShapeEvent callback - called once per shape per repaint pass.
        '''
        ''' ActiveX-specific signature difference from the NDK variant:
        '''   "ByRef Translated As Boolean" must be set to True to signal that
        '''   the event has been handled and default rendering should be skipped.
        '''   If Translated is left False, TatukGIS would also run its default
        '''   rendering for the shape (which would be invisible here due to Size=0).
        '''
        ''' GIS.MapToScreen converts the shape's geographic position to screen pixels.
        ''' GisUtils.Rect constructs a TGIS_Rect COM object for the renderer APIs.
        ''' CanvasDrawBitmap_2 is the ActiveX-exposed overload that accepts a
        ''' TGIS_Pixels COM buffer (instead of a managed array in the NDK).
        ''' </summary>
        Private Sub GIS_PaintShapeEvent(ByRef Translated As Boolean, Shape As TGIS_Shape)
            Dim pt As TPoint                 ' screen-pixel position of the shape
            Dim rdr As TGIS_RendererAbstract
            ' Signal that we are handling the rendering; suppress default drawing.
            Translated = True

            ' MapToScreen converts geographic coordinates to screen pixels.
            pt = GIS.MapToScreen(Shape.PointOnShape())
            ' Run any remaining default rendering hooks (no-op for Size=0 markers).
            Shape.Draw()
            ' Obtain the abstract renderer from the shape's owning layer.
            rdr = CType(Shape.Layer.Renderer, TGIS_RendererAbstract)

            ' --- Select drawing primitive by "type" field ---

            If CType(Shape.GetField("type"), String) = "Rectangle" Then
                ' Yellow-filled, red-bordered rectangle.
                ' GisUtils.Rect() builds a TGIS_Rect COM object from (left, top, right, bottom).
                rdr.CanvasPen.Color = (New TGIS_Color).Red
                rdr.CanvasBrush.Color = (New TGIS_Color).Yellow
                rdr.CanvasDrawRectangle(GisUtils.Rect(pt.X, pt.Y, pt.X + 20, pt.Y + 20))
                ' Draw label 20 pixels above the rectangle
                pt.Y = pt.Y - 20
                rdr.CanvasDrawText(GisUtils.Rect(pt.X, pt.Y, pt.X + 50, pt.Y + 20), "Rectangle")
            ElseIf CType(Shape.GetField("type"), String) = "Ellipse" Then
                ' Red-filled, black-bordered ellipse
                rdr.CanvasPen.Color = (New TGIS_Color).Black
                rdr.CanvasBrush.Color = (New TGIS_Color).Red
                rdr.CanvasDrawEllipse(pt.X, pt.Y, 20, 20)
                pt.Y = pt.Y - 20
                rdr.CanvasDrawText(GisUtils.Rect(pt.X, pt.Y, pt.X + 50, pt.Y + 20), "Ellipse")
            ElseIf CType(Shape.GetField("type"), String) = "Image1" Then
                ' Stretch TGIS_Bitmap (police.bmp) into a 20x20 destination rect
                rdr.CanvasDrawBitmap(
                bmp,
                GisUtils.Rect(
                  pt.X,
                  pt.Y,
                  pt.X + 20,
                  pt.Y + 20
                ))
                pt.Y = pt.Y - 20
                rdr.CanvasDrawText(GisUtils.Rect(pt.X, pt.Y, pt.X + 50, pt.Y + 20), "Image1")
            ElseIf CType(Shape.GetField("type"), String) = "Image2" Then
                ' CanvasDrawBitmap_2 is the ActiveX overload accepting a TGIS_Pixels buffer.
                ' GisUtils.Point(5,5) = source offset: skip the solid-red first row.
                ' TGIS_BitmapFormat.ARGB = 0xAARRGGBB layout.
                ' TGIS_BitmapLinesOrder.Down = scanlines top-to-bottom.
                rdr.CanvasDrawBitmap_2(
                  px,
                  GisUtils.Point(5, 5),
                  GisUtils.Rect(pt.X, pt.Y, pt.X + 20, pt.Y + 20),
                  TGIS_BitmapFormat.ARGB,
                  TGIS_BitmapLinesOrder.Down
                )
                pt.Y = pt.Y - 20
                rdr.CanvasDrawText(GisUtils.Rect(pt.X, pt.Y, pt.X + 50, pt.Y + 20), "Image2")
            End If

        End Sub

        ''' <summary>
        ''' Cycles the active rendering backend between GDI32 and GDI+.
        '''
        ''' In the ActiveX variant the available renderer types are accessed
        ''' directly by class (TGIS_RendererVclGdi32, TGIS_RendererVclGdiPlus)
        ''' rather than through a RendererManager combo box as in the NDK.
        ''' GIS.InvalidateWholeMap forces an immediate repaint with the new renderer.
        ''' </summary>
        Private Sub btnChangeRenderer_Click(sender As Object, e As EventArgs) Handles btnChangeRenderer.Click
            If TypeOf GIS.Renderer Is ITGIS_RendererVclGdiPlus Then
                ' Currently GDI+: switch to GDI32
                GIS.Renderer = New TGIS_RendererVclGdi32()
                btnChangeRenderer.Text = "Change to GDI+"
            ElseIf TypeOf GIS.Renderer Is ITGIS_RendererVclGdi32 Then
                ' Currently GDI32: switch to GDI+
                GIS.Renderer = New TGIS_RendererVclGdiPlus()
                btnChangeRenderer.Text = "Change to GDI32"
            Else
                ' Fallback for any other backend: switch to GDI32
                GIS.Renderer = New TGIS_RendererVclGdi32()
                btnChangeRenderer.Text = "Change to GDI+"
            End If
            ' Force a full repaint so the renderer change is immediately visible
            GIS.InvalidateWholeMap()
        End Sub

        ''' <summary>
        ''' PaintExtraEvent callback - fired once per frame after all layers have
        ''' finished rendering.  Demonstrates accessing renderer-native canvas
        ''' handles for platform-specific drawing.
        '''
        ''' In the ActiveX variant the event signature includes
        ''' "e.Translated = True" to signal to TatukGIS that the event was handled.
        '''
        ''' Only GDI32 and GDI+ branches are provided here; extending for other
        ''' backends follows the same pattern of checking TypeOf e.Renderer and
        ''' casting e.Renderer.CanvasNative() to the appropriate canvas type.
        ''' </summary>
        Private Sub GIS_PaintExtraEvent(sender As Object, e As ITGIS_ViewerWndEvents_PaintExtraEventEvent) Handles GIS.PaintExtraEvent
            ' Signal that we are handling the extra paint event.
            e.Translated = True

            ' --- Select drawing based on the active renderer backend ---

            If TypeOf e.Renderer Is ITGIS_RendererVclGdi32 Then
                ' GDI32 path: cast CanvasNative() to a VCL TCanvas equivalent
                ' and use Windows GDI drawing calls here if needed.
            ElseIf TypeOf e.Renderer Is ITGIS_RendererVclGdiPlus Then
                ' GDI+ path: cast CanvasNative() to a GDI+ Graphics context
                ' and use GdipXxx flat-API calls here if needed.
            Else
                ' Future or unrecognised renderer backends: no action required.
            End If
        End Sub
    End Class
End Namespace
