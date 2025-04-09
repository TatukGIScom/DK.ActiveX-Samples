Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS_XDK11

Namespace AddLayer
    ''' <summary>
    ''' Summary description for WinForm.
    ''' </summary>
    Public Class WinForm
        Inherits Form
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As IContainer
        Private GIS As AxTatukGIS_XDK11.AxTGIS_ViewerWnd
        Private label1 As Label
        Private panel2 As Panel
        Private WithEvents btn3D As Button
        Private WithEvents btnVectorize As Button
        Private WithEvents btnStreamOrderStrahler As Button
        Private WithEvents btnBasin As Button
        Private WithEvents btnWatershed As Button
        Private WithEvents btnAddOutlets As Button
        Private WithEvents btnFlowAccumulation As Button
        Private WithEvents btnFlowDirection As Button
        Private WithEvents btnFillSinks As Button
        Private WithEvents btnSink As Button
        Private tgiS_ControlLegend1 As AxTatukGIS_XDK11.AxTGIS_ControlLegend
        Private panel1 As Panel
        Private dem As TGIS_LayerPixel
        Private ext As TGIS_Extent
        Private hydrologyToolset As TGIS_Hydrology
        Const HYDRO_LAYER_SINK As String = "Sinks and flats"
        Const HYDRO_LAYER_DEM As String = "Hydrologically conditioned DEM"
        Const HYDRO_LAYER_DIRECTION As String = "Flow direction"
        Const HYDRO_LAYER_ACCUMULATION As String = "Flow accumulation"
        Const HYDRO_LAYER_STREAM_ORDER As String = "Stream order (Strahler)"
        Const HYDRO_LAYER_OUTLETS As String = "Outlets (pour points)"
        Const HYDRO_LAYER_WATERSHED As String = "Watersheds"
        Const HYDRO_LAYER_BASIN As String = "Basins"
        Const HYDRO_LAYER_STREAM_VEC As String = "Streams (vectorized)"
        Const HYDRO_LAYER_BASIN_VEC As String = "Basins (vectorized)"
        Const HYDRO_FIELD_ORDER As String = "ORDER"
        Private panel3 As Panel
        Private progressBar1 As ProgressBar
        Const HYDRO_FIELD_BASIN As String = "BASIN_ID"

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
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.label1 = New System.Windows.Forms.Label()
            Me.GIS = New AxTatukGIS_XDK11.AxTGIS_ViewerWnd()
            Me.panel2 = New System.Windows.Forms.Panel()
            Me.btn3D = New System.Windows.Forms.Button()
            Me.btnVectorize = New System.Windows.Forms.Button()
            Me.btnStreamOrderStrahler = New System.Windows.Forms.Button()
            Me.btnBasin = New System.Windows.Forms.Button()
            Me.btnWatershed = New System.Windows.Forms.Button()
            Me.btnAddOutlets = New System.Windows.Forms.Button()
            Me.btnFlowAccumulation = New System.Windows.Forms.Button()
            Me.btnFlowDirection = New System.Windows.Forms.Button()
            Me.btnFillSinks = New System.Windows.Forms.Button()
            Me.btnSink = New System.Windows.Forms.Button()
            Me.tgiS_ControlLegend1 = New AxTatukGIS_XDK11.AxTGIS_ControlLegend()
            Me.panel3 = New System.Windows.Forms.Panel()
            Me.progressBar1 = New System.Windows.Forms.ProgressBar()
            Me.panel1.SuspendLayout()
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.panel2.SuspendLayout()
            CType(Me.tgiS_ControlLegend1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.panel3.SuspendLayout()
            Me.SuspendLayout()
            '
            'panel1
            '
            Me.panel1.Controls.Add(Me.label1)
            Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.panel1.Location = New System.Drawing.Point(0, 0)
            Me.panel1.Margin = New System.Windows.Forms.Padding(7)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(2135, 54)
            Me.panel1.TabIndex = 2
            '
            'label1
            '
            Me.label1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
            Me.label1.ForeColor = System.Drawing.SystemColors.HotTrack
            Me.label1.Location = New System.Drawing.Point(0, 0)
            Me.label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
            Me.label1.Name = "label1"
            Me.label1.Size = New System.Drawing.Size(2135, 54)
            Me.label1.TabIndex = 0
            Me.label1.Text = "This sample application is a step-by-step tutorial on how to perform common hydro" &
    "logical analyzes."
            Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'GIS
            '
            Me.GIS.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GIS.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.GIS.Enabled = True
            Me.GIS.Location = New System.Drawing.Point(374, 54)
            Me.GIS.Margin = New System.Windows.Forms.Padding(7)
            Me.GIS.Name = "GIS"
            Me.GIS.OcxState = CType(resources.GetObject("GIS.OcxState"), System.Windows.Forms.AxHost.State)
            Me.GIS.Size = New System.Drawing.Size(1484, 1061)
            Me.GIS.TabIndex = 3
            '
            'panel2
            '
            Me.panel2.Controls.Add(Me.btn3D)
            Me.panel2.Controls.Add(Me.btnVectorize)
            Me.panel2.Controls.Add(Me.btnStreamOrderStrahler)
            Me.panel2.Controls.Add(Me.btnBasin)
            Me.panel2.Controls.Add(Me.btnWatershed)
            Me.panel2.Controls.Add(Me.btnAddOutlets)
            Me.panel2.Controls.Add(Me.btnFlowAccumulation)
            Me.panel2.Controls.Add(Me.btnFlowDirection)
            Me.panel2.Controls.Add(Me.btnFillSinks)
            Me.panel2.Controls.Add(Me.btnSink)
            Me.panel2.Dock = System.Windows.Forms.DockStyle.Left
            Me.panel2.Location = New System.Drawing.Point(0, 54)
            Me.panel2.Margin = New System.Windows.Forms.Padding(2)
            Me.panel2.Name = "panel2"
            Me.panel2.Size = New System.Drawing.Size(374, 1105)
            Me.panel2.TabIndex = 4
            '
            'btn3D
            '
            Me.btn3D.Dock = System.Windows.Forms.DockStyle.Top
            Me.btn3D.Enabled = False
            Me.btn3D.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
            Me.btn3D.Location = New System.Drawing.Point(0, 486)
            Me.btn3D.Margin = New System.Windows.Forms.Padding(2)
            Me.btn3D.Name = "btn3D"
            Me.btn3D.Size = New System.Drawing.Size(374, 54)
            Me.btn3D.TabIndex = 9
            Me.btn3D.Text = "View in 3D"
            Me.btn3D.UseVisualStyleBackColor = True
            '
            'btnVectorize
            '
            Me.btnVectorize.Dock = System.Windows.Forms.DockStyle.Top
            Me.btnVectorize.Enabled = False
            Me.btnVectorize.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
            Me.btnVectorize.Location = New System.Drawing.Point(0, 432)
            Me.btnVectorize.Margin = New System.Windows.Forms.Padding(2)
            Me.btnVectorize.Name = "btnVectorize"
            Me.btnVectorize.Size = New System.Drawing.Size(374, 54)
            Me.btnVectorize.TabIndex = 8
            Me.btnVectorize.Text = "Convert to vector"
            Me.btnVectorize.UseVisualStyleBackColor = True
            '
            'btnStreamOrderStrahler
            '
            Me.btnStreamOrderStrahler.Dock = System.Windows.Forms.DockStyle.Top
            Me.btnStreamOrderStrahler.Enabled = False
            Me.btnStreamOrderStrahler.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
            Me.btnStreamOrderStrahler.Location = New System.Drawing.Point(0, 378)
            Me.btnStreamOrderStrahler.Margin = New System.Windows.Forms.Padding(2)
            Me.btnStreamOrderStrahler.Name = "btnStreamOrderStrahler"
            Me.btnStreamOrderStrahler.Size = New System.Drawing.Size(374, 54)
            Me.btnStreamOrderStrahler.TabIndex = 7
            Me.btnStreamOrderStrahler.Text = "Stream Order (Strahler)"
            Me.btnStreamOrderStrahler.UseVisualStyleBackColor = True
            '
            'btnBasin
            '
            Me.btnBasin.Dock = System.Windows.Forms.DockStyle.Top
            Me.btnBasin.Enabled = False
            Me.btnBasin.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
            Me.btnBasin.Location = New System.Drawing.Point(0, 324)
            Me.btnBasin.Margin = New System.Windows.Forms.Padding(2)
            Me.btnBasin.Name = "btnBasin"
            Me.btnBasin.Size = New System.Drawing.Size(374, 54)
            Me.btnBasin.TabIndex = 6
            Me.btnBasin.Text = "Basin"
            Me.btnBasin.UseVisualStyleBackColor = True
            '
            'btnWatershed
            '
            Me.btnWatershed.Dock = System.Windows.Forms.DockStyle.Top
            Me.btnWatershed.Enabled = False
            Me.btnWatershed.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
            Me.btnWatershed.Location = New System.Drawing.Point(0, 270)
            Me.btnWatershed.Margin = New System.Windows.Forms.Padding(2)
            Me.btnWatershed.Name = "btnWatershed"
            Me.btnWatershed.Size = New System.Drawing.Size(374, 54)
            Me.btnWatershed.TabIndex = 5
            Me.btnWatershed.Text = "Watershed"
            Me.btnWatershed.UseVisualStyleBackColor = True
            '
            'btnAddOutlets
            '
            Me.btnAddOutlets.Dock = System.Windows.Forms.DockStyle.Top
            Me.btnAddOutlets.Enabled = False
            Me.btnAddOutlets.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
            Me.btnAddOutlets.Location = New System.Drawing.Point(0, 216)
            Me.btnAddOutlets.Margin = New System.Windows.Forms.Padding(2)
            Me.btnAddOutlets.Name = "btnAddOutlets"
            Me.btnAddOutlets.Size = New System.Drawing.Size(374, 54)
            Me.btnAddOutlets.TabIndex = 4
            Me.btnAddOutlets.Text = "Add outlets for Watershed"
            Me.btnAddOutlets.UseVisualStyleBackColor = True
            '
            'btnFlowAccumulation
            '
            Me.btnFlowAccumulation.Dock = System.Windows.Forms.DockStyle.Top
            Me.btnFlowAccumulation.Enabled = False
            Me.btnFlowAccumulation.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
            Me.btnFlowAccumulation.Location = New System.Drawing.Point(0, 162)
            Me.btnFlowAccumulation.Margin = New System.Windows.Forms.Padding(2)
            Me.btnFlowAccumulation.Name = "btnFlowAccumulation"
            Me.btnFlowAccumulation.Size = New System.Drawing.Size(374, 54)
            Me.btnFlowAccumulation.TabIndex = 3
            Me.btnFlowAccumulation.Text = "Flow Accumulation"
            Me.btnFlowAccumulation.UseVisualStyleBackColor = True
            '
            'btnFlowDirection
            '
            Me.btnFlowDirection.Dock = System.Windows.Forms.DockStyle.Top
            Me.btnFlowDirection.Enabled = False
            Me.btnFlowDirection.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
            Me.btnFlowDirection.Location = New System.Drawing.Point(0, 108)
            Me.btnFlowDirection.Margin = New System.Windows.Forms.Padding(2)
            Me.btnFlowDirection.Name = "btnFlowDirection"
            Me.btnFlowDirection.Size = New System.Drawing.Size(374, 54)
            Me.btnFlowDirection.TabIndex = 2
            Me.btnFlowDirection.Text = "Flow Direction"
            Me.btnFlowDirection.UseVisualStyleBackColor = True
            '
            'btnFillSinks
            '
            Me.btnFillSinks.Dock = System.Windows.Forms.DockStyle.Top
            Me.btnFillSinks.Enabled = False
            Me.btnFillSinks.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
            Me.btnFillSinks.Location = New System.Drawing.Point(0, 54)
            Me.btnFillSinks.Margin = New System.Windows.Forms.Padding(2)
            Me.btnFillSinks.Name = "btnFillSinks"
            Me.btnFillSinks.Size = New System.Drawing.Size(374, 54)
            Me.btnFillSinks.TabIndex = 1
            Me.btnFillSinks.Text = "Fill sinks"
            Me.btnFillSinks.UseVisualStyleBackColor = True
            '
            'btnSink
            '
            Me.btnSink.Dock = System.Windows.Forms.DockStyle.Top
            Me.btnSink.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
            Me.btnSink.Location = New System.Drawing.Point(0, 0)
            Me.btnSink.Margin = New System.Windows.Forms.Padding(2)
            Me.btnSink.Name = "btnSink"
            Me.btnSink.Size = New System.Drawing.Size(374, 54)
            Me.btnSink.TabIndex = 0
            Me.btnSink.Text = "Identify DEM problems"
            Me.btnSink.UseVisualStyleBackColor = True
            '
            'tgiS_ControlLegend1
            '
            Me.tgiS_ControlLegend1.Dock = System.Windows.Forms.DockStyle.Right
            Me.tgiS_ControlLegend1.Enabled = True
            Me.tgiS_ControlLegend1.Font = New System.Drawing.Font("Verdana", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
            Me.tgiS_ControlLegend1.ForeColor = System.Drawing.SystemColors.WindowText
            Me.tgiS_ControlLegend1.Location = New System.Drawing.Point(1857, 54)
            Me.tgiS_ControlLegend1.Margin = New System.Windows.Forms.Padding(2)
            Me.tgiS_ControlLegend1.Name = "tgiS_ControlLegend1"
            Me.tgiS_ControlLegend1.OcxState = CType(resources.GetObject("tgiS_ControlLegend1.OcxState"), System.Windows.Forms.AxHost.State)
            Me.tgiS_ControlLegend1.Size = New System.Drawing.Size(278, 1105)
            Me.tgiS_ControlLegend1.TabIndex = 7
            '
            'panel3
            '
            Me.panel3.Controls.Add(Me.progressBar1)
            Me.panel3.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.panel3.Location = New System.Drawing.Point(374, 1114)
            Me.panel3.Margin = New System.Windows.Forms.Padding(2)
            Me.panel3.Name = "panel3"
            Me.panel3.Size = New System.Drawing.Size(1483, 45)
            Me.panel3.TabIndex = 8
            '
            'progressBar1
            '
            Me.progressBar1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.progressBar1.Location = New System.Drawing.Point(0, 0)
            Me.progressBar1.Margin = New System.Windows.Forms.Padding(2)
            Me.progressBar1.Name = "progressBar1"
            Me.progressBar1.Size = New System.Drawing.Size(1483, 45)
            Me.progressBar1.TabIndex = 11
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(216.0!, 216.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(2135, 1159)
            Me.Controls.Add(Me.panel3)
            Me.Controls.Add(Me.tgiS_ControlLegend1)
            Me.Controls.Add(Me.panel2)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.panel1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Margin = New System.Windows.Forms.Padding(7)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Hydrology"
            Me.panel1.ResumeLayout(False)
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).EndInit()
            Me.panel2.ResumeLayout(False)
            CType(Me.tgiS_ControlLegend1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.panel3.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
#End Region
        Dim GisUtils As New TGIS_Utils()

        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        <STAThread>
        Shared Sub Main()
            Call Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Call Application.Run(New WinForm())
        End Sub

        Private Sub doBusyEvent(Pos As Integer, [End] As Integer, ByRef Abort As Boolean)
            If Pos < 0 Then
                progressBar1.Value = 0
            ElseIf Pos = 0 Then
                progressBar1.Minimum = 0
                progressBar1.Maximum = 100
                progressBar1.Value = 0
            Else
                progressBar1.Value = CInt(Pos)
            End If
        End Sub

        ' Creates a new grid layer with the same parameters as input DEM and a given name
        Public Function CreateLayerPix(ByVal _dem As TGIS_LayerPixel, ByVal _name As String) As TGIS_LayerPixel
            Dim res As TGIS_LayerPixel = New TGIS_LayerPixel()
            res.Build(True, _dem.CS, _dem.Extent, _dem.BitWidth, _dem.BitHeight)
            res.Name = _name
            res.Params.Pixel.Antialias = False
            res.Params.Pixel.GridShadow = False
            Return res
        End Function

        ' Creates a new vector layer wita a given name, cs and type
        Public Function CreateLayerVec(ByVal _name As String, ByVal _cs As TGIS_CSCoordinateSystem, ByVal _type As TGIS_ShapeType) As TGIS_LayerVector
            Dim res As TGIS_LayerVector = New TGIS_LayerVector()
            res.Name = _name
            res.Open()
            res.CS = _cs
            res.DefaultShapeType = _type
            Return res
        End Function

        ' Gets a pixel layer with a given name from GIS
        Public Function GetLayerGrd(ByVal _name As String) As TGIS_LayerPixel
            Return TryCast(GIS.[Get](_name), TGIS_LayerPixel)
        End Function

        ' Gets a vector layer with a given name from GIS
        Public Function GetLayerVec(ByVal _name As String) As TGIS_LayerVector
            Return TryCast(GIS.[Get](_name), TGIS_LayerVector)
        End Function

        Public Function GetLayer(ByVal _name As String) As TGIS_Layer
            Dim i As Integer
            For i = 1 To GIS.Items.Count - 1
                If GIS.Items.Item(i).Name = _name Then
                    Return GIS.Items.Item(i)
                End If
            Next
        End Function

        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            GIS.Mode = TGIS_ViewerMode.Zoom
            GIS.RestrictedDrag = False
            tgiS_ControlLegend1.GIS_Viewer = GIS.GetOcx()
            GIS.Open(GisUtils.GisSamplesDataDirDownload() & "World\Countries\Poland\DEM\Bytowski_County.tif")
            dem = TryCast(GIS.Items.Item(0), TGIS_LayerPixel)
            ext = dem.Extent
            dem.Params.Pixel.Antialias = False
            dem.Params.Pixel.GridShadow = False
            GIS.InvalidateWholeMap()
            hydrologyToolset = New TGIS_Hydrology()
            AddHandler Me.hydrologyToolset.BusyEvent, AddressOf doBusyEvent
        End Sub

        Private Sub btnSink_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSink.Click
            btnSink.Enabled = False

            ' creating a grid layer for sinks
            Dim sinks As TGIS_LayerPixel = Me.CreateLayerPix(dem, HYDRO_LAYER_SINK)

            ' the Sink algorithm requires only a grid layer with DEM
            hydrologyToolset.Sink(dem, ext, sinks)
            GIS.Add(sinks)

            ' coloring pixels with sinks (pits) and flats
            Dim mn As String = sinks.MinHeight.ToString()
            Dim mx As String = sinks.MaxHeight.ToString()
            sinks.Params.Pixel.AltitudeMapZones.Add(String.Format("{0},{1},165:15:21:255,{2}-{3}", mn, mx, mn, mx))
            GIS.InvalidateWholeMap()
            btnFillSinks.Enabled = True
        End Sub

        Private Sub btnFillSinks_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFillSinks.Click
            btnFillSinks.Enabled = False

            ' turning off layers
            dem.Active = False
            GetLayerGrd(HYDRO_LAYER_SINK).Active = False

            ' creating a grid layer for a hydrologically conditioned DEM
            Dim hydro_dem As TGIS_LayerPixel = Me.CreateLayerPix(dem, HYDRO_LAYER_DEM)

            ' the Fill algorithm requires a grid layer with DEM
            hydrologyToolset.Fill(dem, ext, hydro_dem, True)
            GIS.Add(hydro_dem)

            ' applying the layer symbology
            Dim color_ramp As TGIS_GradientMap = GisUtils.GisColorRampList.ByName("YellowGreen")
            Dim color_map As TGIS_ColorMapArray = color_ramp.RealizeColorMap(TGIS_ColorMapMode.Continuous, 0, True)
            hydro_dem.GenerateRampEx(hydro_dem.MinHeight, hydro_dem.MaxHeight, color_map, Nothing)
            hydro_dem.Params.Pixel.GridShadow = True
            hydro_dem.Params.Pixel.Antialias = True
            hydro_dem.Params.Pixel.ShowLegend = False
            GIS.InvalidateWholeMap()
            btnFlowDirection.Enabled = True
        End Sub

        Private Sub btnFlowDirection_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFlowDirection.Click
            btnFlowDirection.Enabled = False
            Dim hydro_dem As TGIS_LayerPixel = GetLayerGrd(HYDRO_LAYER_DEM)
            hydro_dem.Active = False

            ' creating a grid layer for flow directions
            Dim flowdir As TGIS_LayerPixel = Me.CreateLayerPix(dem, HYDRO_LAYER_DIRECTION)

            ' the FlowDirection algorithm requires a hydrologically conditioned DEM
            hydrologyToolset.FlowDirection(hydro_dem, ext, flowdir, False)

            ' applying a turbo color ramp for direction codes
            flowdir.Params.Pixel.AltitudeMapZones.Add("1,1,48:18:59:255,1")
            flowdir.Params.Pixel.AltitudeMapZones.Add("2,2,71:117:237:255,2")
            flowdir.Params.Pixel.AltitudeMapZones.Add("4,4,29:206:214:255,4")
            flowdir.Params.Pixel.AltitudeMapZones.Add("8,8,98:252:108:255,8")
            flowdir.Params.Pixel.AltitudeMapZones.Add("16,16,210:232:53:255,16")
            flowdir.Params.Pixel.AltitudeMapZones.Add("32,32,254:154:45:255,32")
            flowdir.Params.Pixel.AltitudeMapZones.Add("64,64,217:56:6:255,64")
            flowdir.Params.Pixel.AltitudeMapZones.Add("128,128,122:4:3:255,128")
            flowdir.Params.Pixel.ShowLegend = True
            GIS.Add(flowdir)
            GIS.InvalidateWholeMap()
            btnFlowAccumulation.Enabled = True
        End Sub

        Private Sub btnFlowAccumulation_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFlowAccumulation.Click
            btnFlowAccumulation.Enabled = False
            Dim flowdir As TGIS_LayerPixel = GetLayerGrd(HYDRO_LAYER_DIRECTION)
            flowdir.Active = False

            ' creating a grid layer for flow accumulation
            Dim flowacc As TGIS_LayerPixel = Me.CreateLayerPix(dem, HYDRO_LAYER_ACCUMULATION)

            ' the FlowAccumulation algorithm requires a flow accumulation grid
            hydrologyToolset.FlowAccumulation(flowdir, ext, flowacc)
            GIS.Add(flowacc)
            Dim aflowacc As TGIS_Layer
            aflowacc = GetLayer(HYDRO_LAYER_ACCUMULATION)

            ' performing a geometric classification for a better result visualization
            Dim classifier As TGIS_ClassificationPixel = (New TGIS_ClassificationFactory).CreateClassifier(aflowacc)

            Try
                classifier.Method = TGIS_ClassificationMethod.GeometricalInterval
                classifier.Band = "1"
                classifier.NumClasses = 5
                classifier.ColorRamp = GisUtils.GisColorRampList.ByName("Bathymetry2").RealizeColorMap(TGIS_ColorMapMode.Continuous, 0, True)
                If classifier.MustCalculateStatistics() Then flowacc.Statistics.Calculate()
                classifier.Classify()
                flowacc.Params.Pixel.ShowLegend = True
            Finally
                classifier = Nothing
            End Try

            GIS.InvalidateWholeMap()
            btnAddOutlets.Enabled = True
        End Sub

        Private Sub btnAddOutlets_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddOutlets.Click
            btnAddOutlets.Enabled = False

            ' creating a grid layer for outlets (pour points)
            Dim outlets As TGIS_LayerVector = CreateLayerVec(HYDRO_LAYER_OUTLETS, dem.CS, TGIS_ShapeType.Point)

            ' adding point symbology
            outlets.Params.Marker.Style = TGIS_MarkerStyle.TriangleUp
            outlets.Params.Marker.SizeAsText = "SIZE:8pt"

            ' adding two sample pour points
            ' outlets should be located to cells of high accumulated flow
            Dim shp As TGIS_Shape = outlets.CreateShape(TGIS_ShapeType.Point)
            shp.Lock(TGIS_Lock.Projection)
            shp.AddPart()
            shp.AddPoint(GisUtils.GisPoint(375007.548333318, 696503.13358447))
            shp.Unlock()
            shp = outlets.CreateShape(TGIS_ShapeType.Point)
            shp.Lock(TGIS_Lock.Projection)
            shp.AddPart()
            shp.AddPoint(GisUtils.GisPoint(399612.055851588, 706196.55502031))
            shp.Unlock()
            GIS.Add(outlets)
            GIS.InvalidateWholeMap()
            btnWatershed.Enabled = True
        End Sub

        Private Sub btnWatershed_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnWatershed.Click
            btnWatershed.Enabled = False
            Dim flowdir As TGIS_LayerPixel = GetLayerGrd(HYDRO_LAYER_DIRECTION)
            Dim outlets As TGIS_LayerVector = GetLayerVec(HYDRO_LAYER_OUTLETS)

            ' creating a grid layer for watershed
            Dim watershed As TGIS_LayerPixel = Me.CreateLayerPix(dem, HYDRO_LAYER_WATERSHED)

            ' applying a symbology
            watershed.Params.Pixel.AltitudeMapZones.Add("1,1,62:138:86:255,1")
            watershed.Params.Pixel.AltitudeMapZones.Add("2,2,108:3:174:255,2")
            watershed.Transparency = 50
            watershed.Params.Pixel.ShowLegend = True

            ' the Watershed algorithm requires Flow Direction grid and outlets
            ' (may be vector, or grid)
            hydrologyToolset.Watershed_2(flowdir, outlets, "GIS_UID", ext, watershed)
            GIS.Add(watershed)
            GIS.InvalidateWholeMap()
            btnBasin.Enabled = True
        End Sub

        Private Sub btnBasin_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBasin.Click
            btnBasin.Enabled = False
            Dim flowdir As TGIS_LayerPixel = GetLayerGrd(HYDRO_LAYER_DIRECTION)
            Dim flowacc As TGIS_LayerPixel = GetLayerGrd(HYDRO_LAYER_ACCUMULATION)
            flowacc.Active = False
            GetLayerGrd(HYDRO_LAYER_DEM).Active = False
            GetLayerGrd(HYDRO_LAYER_WATERSHED).Active = False
            GetLayerVec(HYDRO_LAYER_OUTLETS).Active = False

            ' creating a grid layer for Basin
            Dim basins As TGIS_LayerPixel = Me.CreateLayerPix(dem, HYDRO_LAYER_BASIN)

            ' the Basin algorithm only requires a Flow Direction grid
            hydrologyToolset.Basin(flowdir, ext, basins, CInt(Math.Round(flowacc.MaxHeight / 100)))
            GIS.Add(basins)
            Dim abasins As TGIS_Layer = GetLayer(HYDRO_LAYER_BASIN)
            ' classifying basin grid by unique values
            Dim classifier As TGIS_ClassificationPixel = (New TGIS_ClassificationFactory).CreateClassifier(abasins)

            Try
                classifier.Method = TGIS_ClassificationMethod.Unique
                classifier.Band = "Value"
                classifier.ShowLegend = False
                If classifier.MustCalculateStatistics() Then basins.Statistics.Calculate()
                classifier.EstimateNumClasses()
                classifier.ColorRamp = GisUtils.GisColorRampList.ByName("UniquePastel").RealizeColorMap(TGIS_ColorMapMode.Discrete, classifier.NumClasses, False)
                classifier.Classify()
            Finally
                classifier = Nothing
            End Try

            GIS.InvalidateWholeMap()
            btnStreamOrderStrahler.Enabled = True
        End Sub

        Private Sub btnStreamOrderStrahler_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnStreamOrderStrahler.Click
            btnStreamOrderStrahler.Enabled = False
            Dim flowdir As TGIS_LayerPixel = GetLayerGrd(HYDRO_LAYER_DIRECTION)
            Dim flowacc As TGIS_LayerPixel = GetLayerGrd(HYDRO_LAYER_ACCUMULATION)

            ' creating a grid layer for stream order
            Dim stream_order As TGIS_LayerPixel = Me.CreateLayerPix(dem, HYDRO_LAYER_STREAM_ORDER)

            ' applying a symbology from the "Blues" color ramp
            stream_order.Params.Pixel.AltitudeMapZones.Add("1,1,78:179:211:255,1")
            stream_order.Params.Pixel.AltitudeMapZones.Add("2,2,43:140:190:255,2")
            stream_order.Params.Pixel.AltitudeMapZones.Add("3,3,8:104:172:255,3")
            stream_order.Params.Pixel.AltitudeMapZones.Add("4,4,8:64:129:255,4")
            stream_order.Params.Pixel.ShowLegend = True

            ' the StreamOrder algorithm requires Flow Direction and Accumulation grids
            hydrologyToolset.StreamOrder(flowdir, flowacc, ext, stream_order, TGIS_HydrologyStreamOrderMethod.Strahler, -1)
            GIS.Add(stream_order)
            GIS.InvalidateWholeMap()
            btnVectorize.Enabled = True
        End Sub

        Private Sub btnVectorize_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnVectorize.Click
            btnVectorize.Enabled = False
            Dim flowdir As TGIS_LayerPixel = GetLayerGrd(HYDRO_LAYER_DIRECTION)
            Dim streams As TGIS_LayerPixel = GetLayerGrd(HYDRO_LAYER_STREAM_ORDER)
            Dim basins As TGIS_LayerPixel = GetLayerGrd(HYDRO_LAYER_BASIN)
            streams.Active = False
            basins.Active = False

            ' 1. Converting basins to polygon

            ' creating a vector polygon layer for basins
            Dim basins_vec As TGIS_LayerVector = CreateLayerVec(HYDRO_LAYER_BASIN_VEC, dem.CS, TGIS_ShapeType.Polygon)
            basins_vec.AddField(HYDRO_FIELD_BASIN, TGIS_FieldType.Number, 10, 0)

            ' using the GirdToPolygon vectorization tool
            Dim vectorizator As TGIS_GridToPolygon = New TGIS_GridToPolygon()

            Try
                AddHandler vectorizator.BusyEvent, AddressOf doBusyEvent
                vectorizator.Generate(basins, basins_vec, HYDRO_FIELD_BASIN)
            Finally
                vectorizator = Nothing
            End Try

            GIS.Add(basins_vec)
            Dim abasins_vec As TGIS_Layer = GetLayer(HYDRO_LAYER_BASIN_VEC)
            ' classifying a basins vector layer by unique value
            Dim classifier As TGIS_ClassificationVector = (New TGIS_ClassificationFactory).CreateClassifier(abasins_vec)

            Try
                classifier.Method = TGIS_ClassificationMethod.Unique
                classifier.Field = HYDRO_FIELD_BASIN
                classifier.ShowLegend = False
                If classifier.MustCalculateStatistics() Then basins_vec.Statistics.Calculate()
                classifier.EstimateNumClasses()
                classifier.ColorRamp = GisUtils.GisColorRampList.ByName("Unique").RealizeColorMap(TGIS_ColorMapMode.Discrete, classifier.NumClasses, False)
                classifier.Classify()
            Finally
                classifier = Nothing
            End Try

            ' 2. Converting streams to polylines

            ' creating a vector layer for streams from Stream Order grid
            Dim streams_vec As TGIS_LayerVector = CreateLayerVec(HYDRO_LAYER_STREAM_VEC, dem.CS, TGIS_ShapeType.Arc)
            streams_vec.AddField(HYDRO_FIELD_ORDER, TGIS_FieldType.Number, 10, 0)

            ' applying a symbology and width based on a stream order value, and labeling
            streams_vec.Params.Line.WidthAsText = "RENDERER"
            streams_vec.Params.Line.ColorAsText = "ARGB:FF045A8D"
            streams_vec.Params.Render.Expression = HYDRO_FIELD_ORDER
            streams_vec.Params.Render.Zones = 4
            streams_vec.Params.Render.MinVal = 1
            streams_vec.Params.Render.MaxVal = 5
            streams_vec.Params.Render.StartSizeAsText = "SIZE:1pt"
            streams_vec.Params.Render.EndSizeAsText = "SIZE:4pt"
            streams_vec.Params.Labels.Value = "{HYDRO_FIELD_ORDER}"
            streams_vec.Params.Labels.FontSizeAsText = "SIZE:7pt"
            streams_vec.Params.Labels.FontColorAsText = "ARGB:FF045A8D"
            streams_vec.Params.Labels.ColorAsText = "ARGB:FFBDC9E1"
            streams_vec.Params.Labels.Alignment = TGIS_LabelAlignment.Follow
            hydrologyToolset.StreamToPolyline(flowdir, streams, ext, streams_vec, HYDRO_FIELD_ORDER, 0)
            GIS.Add(streams_vec)
            GIS.InvalidateWholeMap()
            btn3D.Enabled = True
        End Sub

        Private Sub btn3D_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn3D.Click
            If GIS.View3D Then
                btn3D.Text = "View in 3D"
                GIS.View3D = False
            Else
                btn3D.Text = "View in 2D"
                Dim basins As TGIS_LayerVector = GetLayerVec(HYDRO_LAYER_BASIN_VEC)
                basins.Active = False
                Dim hdem As TGIS_LayerPixel = GetLayerGrd(HYDRO_LAYER_DEM)
                hdem.Active = True
                hdem.Params.ScaleZ = 1
                hdem.Params.NormalizedZ = TGIS_3DNormalizationType.Range
                Dim streams As TGIS_LayerVector = GetLayerVec(HYDRO_LAYER_STREAM_VEC)
                streams.Params.Labels.Visible = False
                streams.Layer3D = TGIS_3DLayerType.Off
                GIS.InvalidateWholeMap()
                GIS.View3D = True
                GIS.Viewer3D.ShowLights = True
                GIS.Viewer3D.ShadowsLevel = 40
            End If
        End Sub
    End Class
End Namespace
