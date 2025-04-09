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
    ''' Summary description for WinForm.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.Container = Nothing
        Private ll As TGIS_LayerVector
        Private WithEvents GIS As AxTatukGIS_XDK11.AxTGIS_ViewerWnd
        Friend WithEvents btnChangeRenderer As Button
        Private bmp As TGIS_Bitmap
        Private px As TGIS_Pixels
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
            'GIS
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
            'btnChangeRenderer
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

        Private Sub initialize_pixels()
            px = New TGIS_Pixels()
            px.SetLength(24)
            px.Value(0) = CType(&HFFFF0000, Int32)
            px.Value(1) = CType(&HFFFF0000, Int32)
            px.Value(2) = CType(&HFFFF0000, Int32)
            px.Value(3) = CType(&HFFFF0000, Int32)
            px.Value(4) = CType(&HFFFF0000, Int32)
            px.Value(5) = CType(&H0, Int32)
            px.Value(6) = CType(&H0, Int32)
            px.Value(7) = CType(&HFF0000FF, Int32)
            px.Value(8) = CType(&H0, Int32)
            px.Value(9) = CType(&H0, Int32)
            px.Value(10) = CType(&H0, Int32)
            px.Value(11) = CType(&H0, Int32)
            px.Value(12) = CType(&HFF0000FF, Int32)
            px.Value(13) = CType(&H0, Int32)
            px.Value(14) = CType(&H0, Int32)
            px.Value(15) = CType(&H0, Int32)
            px.Value(16) = CType(&H0, Int32)
            px.Value(17) = CType(&HFF0000FF, Int32)
            px.Value(18) = CType(&H0, Int32)
            px.Value(19) = CType(&H0, Int32)
            px.Value(20) = CType(&H0, Int32)
            px.Value(21) = CType(&H0, Int32)
            px.Value(22) = CType(&HFF0000FF, Int32)
            px.Value(23) = CType(&H0, Int32)
        End Sub

        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim shp As TGIS_Shape

            initialize_pixels()

            ll = New TGIS_LayerVector()
            ll.Name = "CustomPaint"
            EventLayer = ll
            GIS.Add(ll)

            ''  ll.PaintShapeEvent += GIS_PaintShapeEvent

            AddHandler EventLayer.PaintShapeEvent, AddressOf GIS_PaintShapeEvent

            ll.AddField("type", TGIS_FieldType.String, 100, 0)
            ll.Extent = GIS.Extent

            shp = ll.CreateShape(TGIS_ShapeType.Point)
            shp.Lock(TGIS_Lock.Extent)
            shp.AddPart()
            shp.AddPoint(GisUtils.GisPoint(-25, 25))
            shp.Params.Marker.Size = 0
            shp.SetField("type", "Rectangle")
            shp.Unlock()

            shp = ll.CreateShape(TGIS_ShapeType.Point)
            shp.Lock(TGIS_Lock.Extent)
            shp.AddPart()
            shp.AddPoint(GisUtils.GisPoint(25, 25))
            shp.Params.Marker.Size = 0
            shp.SetField("type", "Ellipse")
            shp.Unlock()

            shp = ll.CreateShape(TGIS_ShapeType.Point)
            shp.Lock(TGIS_Lock.Extent)
            shp.AddPart()
            shp.AddPoint(GisUtils.GisPoint(-25, -25))
            shp.Params.Marker.Size = 0
            shp.SetField("type", "Image1")
            shp.Unlock()

            shp = ll.CreateShape(TGIS_ShapeType.Point)
            shp.Lock(TGIS_Lock.Extent)
            shp.AddPart()
            shp.AddPoint(GisUtils.GisPoint(25, -25))
            shp.Params.Marker.Size = 0
            shp.SetField("type", "Image2")
            shp.Unlock()

            ll.Extent = GisUtils.GisExtent(-100, -100, 100, 100)

            bmp = New TGIS_Bitmap()
            bmp.LoadFromFile(GisUtils.GisSamplesDataDirDownload() + "Symbols\police.bmp")

            GIS.FullExtent()
        End Sub

        Private Sub GIS_PaintShapeEvent(ByRef Translated As Boolean, Shape As TGIS_Shape)
            Dim pt As TPoint
            Dim rdr As TGIS_RendererAbstract
            Translated = True

            pt = GIS.MapToScreen(Shape.PointOnShape())
            Shape.Draw()
            rdr = CType(Shape.Layer.Renderer, TGIS_RendererAbstract)

            ''Drawing with our renderer
            If CType(Shape.GetField("type"), String) = "Rectangle" Then
                rdr.CanvasPen.Color = (New TGIS_Color).Red
                rdr.CanvasBrush.Color = (New TGIS_Color).Yellow
                rdr.CanvasDrawRectangle(GisUtils.Rect(pt.X, pt.Y, pt.X + 20, pt.Y + 20))
                pt.Y = pt.Y - 20
                rdr.CanvasDrawText(GisUtils.Rect(pt.X, pt.Y, pt.X + 50, pt.Y + 20), "Rectangle")
            ElseIf CType(Shape.GetField("type"), String) = "Ellipse" Then
                rdr.CanvasPen.Color = (New TGIS_Color).Black
                rdr.CanvasBrush.Color = (New TGIS_Color).Red
                rdr.CanvasDrawEllipse(pt.X, pt.Y, 20, 20)
                pt.Y = pt.Y - 20
                rdr.CanvasDrawText(GisUtils.Rect(pt.X, pt.Y, pt.X + 50, pt.Y + 20), "Ellipse")
            ElseIf CType(Shape.GetField("type"), String) = "Image1" Then
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

        Private Sub btnChangeRenderer_Click(sender As Object, e As EventArgs) Handles btnChangeRenderer.Click
            If TypeOf GIS.Renderer Is ITGIS_RendererVclGdiPlus Then
                GIS.Renderer = New TGIS_RendererVclGdi32()
                btnChangeRenderer.Text = "Change to GDI+"
            ElseIf TypeOf GIS.Renderer Is ITGIS_RendererVclGdi32 Then
                GIS.Renderer = New TGIS_RendererVclGdiPlus()
                btnChangeRenderer.Text = "Change to GDI32"
            Else
                GIS.Renderer = New TGIS_RendererVclGdi32()
                btnChangeRenderer.Text = "Change to GDI+"
            End If
            GIS.InvalidateWholeMap()
        End Sub

        Private Sub GIS_PaintExtraEvent(sender As Object, e As ITGIS_ViewerWndEvents_PaintExtraEventEvent) Handles GIS.PaintExtraEvent
            e.Translated = True


            '' drawing with native objects, Not recommended
            If TypeOf e.Renderer Is ITGIS_RendererVclGdi32 Then

            ElseIf TypeOf e.Renderer Is ITGIS_RendererVclGdiPlus Then

            Else
                'for other renderers
            End If
        End Sub
    End Class
End Namespace
