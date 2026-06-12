Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS_XDK11

Namespace CuttingPolygon
    ' CuttingPolygon sample — demonstrates how to clip a raster (pixel) layer's rendering to
    ' within an arbitrary polygon boundary using TGIS_LayerPixel.CuttingPolygon.
    '
    ' What the sample shows:
    '   - Loading a raster image (world map) into the GIS viewer
    '   - Creating an in-memory vector layer with a custom polygon
    '   - Implementing raster clipping/masking via CuttingPolygon property
    '   - Assigning a triangular polygon as the cutting mask
    '   - Toggling clipping on/off via button click (Do Cutting)
    '   - Raster rendering constrained to polygon boundary
    '   - Remaining regions outside polygon are not rendered
    '   - Interactive zoom and pan with clipped raster display
    '
    ' ActiveX-specific implementation details:
    '   - GIS.Items.Item(i) is used instead of the standard indexer
    '   - GIS.GetOcx() links the ActiveX control to the legend
    '   - COM interop handling for layer access and properties
    '
    ' Key TatukGIS API concepts shown here:
    '   TGIS_ViewerWnd              - main visual map control (ActiveX COM version)
    '   TGIS_LayerPixel             - raster/image layer
    '   TGIS_LayerVector            - in-memory vector layer
    '   TGIS_LayerPixel.CuttingPolygon - masking/clipping property
    '   TGIS_Shape                  - polygon geometry for clipping
    '   TGIS_ControlLegend          - layer list/legend panel (ActiveX)
    '   OnLoad event                - initialization workflow
    Public Class WinForm
        Inherits System.Windows.Forms.Form
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer
        Private WithEvents btnCutting As Button
        Private WithEvents btnZoom As Button
        Private GIS As AxTatukGIS_XDK11.AxTGIS_ViewerWnd
        Private ll As TGIS_LayerVector
        Private lp As TGIS_LayerPixel
        Private tgiS_ControlLegend1 As AxTatukGIS_XDK11.AxTGIS_ControlLegend

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
            Me.btnCutting = New System.Windows.Forms.Button()
            Me.btnZoom = New System.Windows.Forms.Button()
            Me.GIS = New AxTatukGIS_XDK11.AxTGIS_ViewerWnd()
            Me.tgiS_ControlLegend1 = New AxTatukGIS_XDK11.AxTGIS_ControlLegend()
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.tgiS_ControlLegend1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'btnCutting
            '
            Me.btnCutting.Location = New System.Drawing.Point(1, 2)
            Me.btnCutting.Name = "btnCutting"
            Me.btnCutting.Size = New System.Drawing.Size(94, 23)
            Me.btnCutting.TabIndex = 0
            Me.btnCutting.Text = "Do cutting"
            Me.btnCutting.UseVisualStyleBackColor = True
            '
            'btnZoom
            '
            Me.btnZoom.Location = New System.Drawing.Point(101, 2)
            Me.btnZoom.Name = "btnZoom"
            Me.btnZoom.Size = New System.Drawing.Size(75, 23)
            Me.btnZoom.TabIndex = 1
            Me.btnZoom.Text = "Zoom"
            Me.btnZoom.UseVisualStyleBackColor = True
            '
            'GIS
            '
            Me.GIS.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GIS.Enabled = True
            Me.GIS.Location = New System.Drawing.Point(1, 31)
            Me.GIS.Name = "GIS"
            Me.GIS.OcxState = CType(resources.GetObject("GIS.OcxState"), System.Windows.Forms.AxHost.State)
            Me.GIS.Size = New System.Drawing.Size(529, 380)
            Me.GIS.TabIndex = 2
            '
            'tgiS_ControlLegend1
            '
            Me.tgiS_ControlLegend1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.tgiS_ControlLegend1.Enabled = True
            Me.tgiS_ControlLegend1.Location = New System.Drawing.Point(537, 31)
            Me.tgiS_ControlLegend1.Name = "tgiS_ControlLegend1"
            Me.tgiS_ControlLegend1.OcxState = CType(resources.GetObject("tgiS_ControlLegend1.OcxState"), System.Windows.Forms.AxHost.State)
            Me.tgiS_ControlLegend1.Size = New System.Drawing.Size(192, 380)
            Me.tgiS_ControlLegend1.TabIndex = 3
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(730, 411)
            Me.Controls.Add(Me.tgiS_ControlLegend1)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.btnZoom)
            Me.Controls.Add(Me.btnCutting)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - CuttingPolygon"
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.tgiS_ControlLegend1, System.ComponentModel.ISupportInitialize).EndInit()
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

        ''' <summary>Opens the world raster image, creates an in-memory vector layer named "shape",
        ''' and adds a triangular polygon shape to it.</summary>
        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim shp As TGIS_Shape
            '' link the legend panel to the map viewer control via its COM interface
            tgiS_ControlLegend1.GIS_Viewer = GIS.GetOcx()

            '' load the base world raster image (satellite imagery)
            GIS.Open(GisUtils.GisSamplesDataDirDownload() + "\World\VisibleEarth\world_8km.jpg")
            '' create an in-memory vector layer to hold the clipping polygon
            ll = New TGIS_LayerVector()
            ll.Name = "shape"
            GIS.Add(ll)

            '' create a new polygon shape in the vector layer
            shp = ll.CreateShape(TGIS_ShapeType.Polygon)
            '' lock the shape so the spatial extent updates as we add points
            shp.Lock(TGIS_Lock.Extent)
            '' start a new polygon part (polygon can have multiple parts for complex shapes)
            shp.AddPart()
            '' add three vertices to form a triangle (in geographic coordinates)
            shp.AddPoint(GisUtils.GisPoint(-5, 8))
            shp.AddPoint(GisUtils.GisPoint(40, 2))
            shp.AddPoint(GisUtils.GisPoint(20, -20))
            '' unlock the shape to finalize it
            shp.Unlock()
        End Sub

        ''' <summary>Assigns the "shape" layer's first polygon as the raster layer's CuttingPolygon
        ''' (converting to the raster's coordinate system), then hides the vector layer.</summary>
        Private Sub btnCutting_Click(sender As Object, e As EventArgs) Handles btnCutting.Click
            '' get the raster layer (the first/only pixel layer in the map)
            lp = CType((GIS.Items.Item(0)), TGIS_LayerPixel)
            '' fetch the polygon from the vector layer and convert it to the raster's coordinate system
            '' CreateCopyCS creates a copy of the shape geometry transformed to the target CS
            '' (CuttingPolygon must be in the same coordinate system as the raster layer)
            lp.CuttingPolygon = CType((ll.GetShape(1).CreateCopyCS(lp.CS)), TGIS_ShapePolygon)
            '' hide the clipping polygon layer so only the raster with the clipping effect is visible
            ll.Active = False
            '' repaint the map to apply the clipping effect
            GIS.InvalidateWholeMap()
        End Sub

        ''' <summary>Switches the viewer to Zoom mode.</summary>
        Private Sub btnZoom_Click(sender As Object, e As EventArgs) Handles btnZoom.Click
            GIS.Mode = TGIS_ViewerMode.Zoom
        End Sub
    End Class
End Namespace
