Imports System
Imports System.Windows.Forms
Imports System.Drawing
Imports TatukGIS_XDK11

Namespace Triangulation

    Public Class frmMain
        Inherits System.Windows.Forms.Form

        Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents imglst As System.Windows.Forms.ImageList
        Friend WithEvents tlbr As System.Windows.Forms.ToolBar
        Friend WithEvents btnFullExtent As System.Windows.Forms.ToolBarButton
        Friend WithEvents btnZoomIn As System.Windows.Forms.ToolBarButton
        Friend WithEvents btnZoomOut As System.Windows.Forms.ToolBarButton
        Friend WithEvents GIS_Attributes As AxTatukGIS_XDK11.AxTGIS_ControlAttributes
        Friend WithEvents grpbxResult As System.Windows.Forms.GroupBox
        Friend WithEvents edtLayer As System.Windows.Forms.TextBox
        Friend WithEvents lblLayer As System.Windows.Forms.Label
        Friend WithEvents rbtnDelaunay As System.Windows.Forms.RadioButton
        Friend WithEvents rbtnVoronoi As System.Windows.Forms.RadioButton
        Friend WithEvents GIS_Legend As AxTatukGIS_XDK11.AxTGIS_ControlLegend
        Friend WithEvents btnGenerate As System.Windows.Forms.Button
        Friend WithEvents GIS As AxTatukGIS_XDK11.AxTGIS_ViewerWnd

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

        'Form overrides dispose to clean up the component list.
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

#Region "Windows Form Designer generated code"
        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
            Me.StatusBar1 = New System.Windows.Forms.StatusBar()
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.tlbr = New System.Windows.Forms.ToolBar()
            Me.btnFullExtent = New System.Windows.Forms.ToolBarButton()
            Me.btnZoomIn = New System.Windows.Forms.ToolBarButton()
            Me.btnZoomOut = New System.Windows.Forms.ToolBarButton()
            Me.imglst = New System.Windows.Forms.ImageList(Me.components)
            Me.grpbxResult = New System.Windows.Forms.GroupBox()
            Me.edtLayer = New System.Windows.Forms.TextBox()
            Me.lblLayer = New System.Windows.Forms.Label()
            Me.rbtnDelaunay = New System.Windows.Forms.RadioButton()
            Me.rbtnVoronoi = New System.Windows.Forms.RadioButton()
            Me.btnGenerate = New System.Windows.Forms.Button()
            Me.GIS = New AxTatukGIS_XDK11.AxTGIS_ViewerWnd()
            Me.GIS_Legend = New AxTatukGIS_XDK11.AxTGIS_ControlLegend()
            Me.GIS_Attributes = New AxTatukGIS_XDK11.AxTGIS_ControlAttributes()
            Me.Panel1.SuspendLayout()
            Me.grpbxResult.SuspendLayout()
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.GIS_Legend, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.GIS_Attributes, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'StatusBar1
            '
            Me.StatusBar1.Location = New System.Drawing.Point(0, 389)
            Me.StatusBar1.Name = "StatusBar1"
            Me.StatusBar1.Size = New System.Drawing.Size(588, 22)
            Me.StatusBar1.TabIndex = 0
            '
            'Panel1
            '
            Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Panel1.Controls.Add(Me.tlbr)
            Me.Panel1.Location = New System.Drawing.Point(0, 0)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(588, 26)
            Me.Panel1.TabIndex = 1
            '
            'tlbr
            '
            Me.tlbr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.tlbr.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
            Me.tlbr.AutoSize = False
            Me.tlbr.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.btnFullExtent, Me.btnZoomIn, Me.btnZoomOut})
            Me.tlbr.Dock = System.Windows.Forms.DockStyle.None
            Me.tlbr.DropDownArrows = True
            Me.tlbr.ImageList = Me.imglst
            Me.tlbr.Location = New System.Drawing.Point(0, 0)
            Me.tlbr.Name = "tlbr"
            Me.tlbr.ShowToolTips = True
            Me.tlbr.Size = New System.Drawing.Size(588, 26)
            Me.tlbr.TabIndex = 0
            '
            'btnFullExtent
            '
            Me.btnFullExtent.ImageIndex = 0
            Me.btnFullExtent.Name = "btnFullExtent"
            Me.btnFullExtent.ToolTipText = "Full Extent"
            '
            'btnZoomIn
            '
            Me.btnZoomIn.ImageIndex = 1
            Me.btnZoomIn.Name = "btnZoomIn"
            Me.btnZoomIn.ToolTipText = "Zoom In"
            '
            'btnZoomOut
            '
            Me.btnZoomOut.ImageIndex = 2
            Me.btnZoomOut.Name = "btnZoomOut"
            Me.btnZoomOut.ToolTipText = "Zoom Out"
            '
            'imglst
            '
            Me.imglst.ImageStream = CType(resources.GetObject("imglst.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.imglst.TransparentColor = System.Drawing.Color.Fuchsia
            Me.imglst.Images.SetKeyName(0, "FullExtent.bmp")
            Me.imglst.Images.SetKeyName(1, "ZoomIn.bmp")
            Me.imglst.Images.SetKeyName(2, "ZoomOut.bmp")
            '
            'grpbxResult
            '
            Me.grpbxResult.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.grpbxResult.Controls.Add(Me.edtLayer)
            Me.grpbxResult.Controls.Add(Me.lblLayer)
            Me.grpbxResult.Controls.Add(Me.rbtnDelaunay)
            Me.grpbxResult.Controls.Add(Me.rbtnVoronoi)
            Me.grpbxResult.Location = New System.Drawing.Point(428, 182)
            Me.grpbxResult.Name = "grpbxResult"
            Me.grpbxResult.Size = New System.Drawing.Size(160, 92)
            Me.grpbxResult.TabIndex = 3
            Me.grpbxResult.TabStop = False
            Me.grpbxResult.Text = "Result"
            '
            'edtLayer
            '
            Me.edtLayer.Location = New System.Drawing.Point(81, 65)
            Me.edtLayer.Name = "edtLayer"
            Me.edtLayer.Size = New System.Drawing.Size(73, 20)
            Me.edtLayer.TabIndex = 3
            Me.edtLayer.Text = "Voronoi"
            '
            'lblLayer
            '
            Me.lblLayer.AutoSize = True
            Me.lblLayer.Location = New System.Drawing.Point(6, 68)
            Me.lblLayer.Name = "lblLayer"
            Me.lblLayer.Size = New System.Drawing.Size(68, 13)
            Me.lblLayer.TabIndex = 2
            Me.lblLayer.Text = "Layer name :"
            '
            'rbtnDelaunay
            '
            Me.rbtnDelaunay.AutoSize = True
            Me.rbtnDelaunay.Location = New System.Drawing.Point(6, 42)
            Me.rbtnDelaunay.Name = "rbtnDelaunay"
            Me.rbtnDelaunay.Size = New System.Drawing.Size(134, 17)
            Me.rbtnDelaunay.TabIndex = 1
            Me.rbtnDelaunay.Text = "Delaunay Triangulation"
            Me.rbtnDelaunay.UseVisualStyleBackColor = True
            '
            'rbtnVoronoi
            '
            Me.rbtnVoronoi.AutoSize = True
            Me.rbtnVoronoi.Checked = True
            Me.rbtnVoronoi.Location = New System.Drawing.Point(6, 19)
            Me.rbtnVoronoi.Name = "rbtnVoronoi"
            Me.rbtnVoronoi.Size = New System.Drawing.Size(103, 17)
            Me.rbtnVoronoi.TabIndex = 0
            Me.rbtnVoronoi.TabStop = True
            Me.rbtnVoronoi.Text = "Voronoi Diagram"
            Me.rbtnVoronoi.UseVisualStyleBackColor = True
            '
            'btnGenerate
            '
            Me.btnGenerate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnGenerate.Location = New System.Drawing.Point(434, 280)
            Me.btnGenerate.Name = "btnGenerate"
            Me.btnGenerate.Size = New System.Drawing.Size(148, 23)
            Me.btnGenerate.TabIndex = 5
            Me.btnGenerate.Text = "Generate"
            Me.btnGenerate.UseVisualStyleBackColor = True
            '
            'GIS
            '
            Me.GIS.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GIS.Cursor = System.Windows.Forms.Cursors.Default
            Me.GIS.Enabled = True
            Me.GIS.Location = New System.Drawing.Point(0, 28)
            Me.GIS.Name = "GIS"
            Me.GIS.OcxState = CType(resources.GetObject("GIS.OcxState"), System.Windows.Forms.AxHost.State)
            Me.GIS.Size = New System.Drawing.Size(428, 361)
            Me.GIS.TabIndex = 6
            '
            'GIS_Legend
            '
            Me.GIS_Legend.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GIS_Legend.Enabled = True
            Me.GIS_Legend.Location = New System.Drawing.Point(428, 308)
            Me.GIS_Legend.Name = "GIS_Legend"
            Me.GIS_Legend.OcxState = CType(resources.GetObject("GIS_Legend.OcxState"), System.Windows.Forms.AxHost.State)
            Me.GIS_Legend.Size = New System.Drawing.Size(160, 80)
            Me.GIS_Legend.TabIndex = 4
            '
            'GIS_Attributes
            '
            Me.GIS_Attributes.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GIS_Attributes.Enabled = True
            Me.GIS_Attributes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
            Me.GIS_Attributes.Location = New System.Drawing.Point(428, 28)
            Me.GIS_Attributes.Name = "GIS_Attributes"
            Me.GIS_Attributes.OcxState = CType(resources.GetObject("GIS_Attributes.OcxState"), System.Windows.Forms.AxHost.State)
            Me.GIS_Attributes.Size = New System.Drawing.Size(160, 149)
            Me.GIS_Attributes.TabIndex = 2
            '
            'frmMain
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(588, 411)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.btnGenerate)
            Me.Controls.Add(Me.GIS_Legend)
            Me.Controls.Add(Me.grpbxResult)
            Me.Controls.Add(Me.GIS_Attributes)
            Me.Controls.Add(Me.Panel1)
            Me.Controls.Add(Me.StatusBar1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "frmMain"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Triangulation"
            Me.Panel1.ResumeLayout(False)
            Me.grpbxResult.ResumeLayout(False)
            Me.grpbxResult.PerformLayout()
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.GIS_Legend, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.GIS_Attributes, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
#End Region

        Dim GisUtils As New TGIS_Utils()

        <STAThread>
        Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New frmMain())
        End Sub

        Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim lv As TGIS_LayerVector
            GIS_Legend.GIS_Viewer = GIS.GetOcx()
            ' open a file
            GIS.Open(GisUtils.GisSamplesDataDirDownload() & "\World\Countries\Poland\DCW\city.shp")

            ' and add a new parametr
            lv = CType(GIS.Items.item(0), TGIS_LayerVector)
            lv.Params.Marker.Color = (New TGIS_Color).FromARGB(ColorTranslator.FromWin32(&H4080FF).A, ColorTranslator.FromWin32(&H4080FF).R, ColorTranslator.FromWin32(&H4080FF).G, ColorTranslator.FromWin32(&H4080FF).B)
            lv.Params.Marker.OutlineWidth = 2
            lv.Params.Marker.Style = TGIS_MarkerStyle.Circle

            lv.ParamsList.Add()
            lv.Params.Style = "selected"
            lv.Params.Area.OutlineWidth = 1
            lv.Params.Area.Color = (New TGIS_Color).Blue

            GIS_Legend.Update()
        End Sub

        Private Sub tlbr_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles tlbr.ButtonClick
            Select Case tlbr.Buttons.IndexOf(e.Button)
                Case 0
                    GIS.FullExtent()
                Case 1
                    GIS.Zoom = GIS.Zoom * 2
                Case 2
                    GIS.Zoom = GIS.Zoom / 2
            End Select
        End Sub

        Private Sub rbtnVoronoi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnVoronoi.Click
            edtLayer.Text = "Voronoi"
        End Sub

        Private Sub rbtnDelaunay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnDelaunay.Click
            edtLayer.Text = "Delaunay"
        End Sub

        Private Sub GIS_MouseDown(ByVal sender As System.Object, ByVal e As AxTatukGIS_XDK11.ITGIS_ViewerWndEvents_MouseDownEvent) Handles GIS.MouseDownEvent
            Dim ptg As TGIS_Point
            Dim shp As TGIS_Shape

            If GIS.IsEmpty Then
                Exit Sub
            End If
            If GIS.InPaint Then
                Return
            End If

            ' let's locate a shape after click
            ptg = GIS.ScreenToMap(GisUtils.Point(e.X, e.Y))
            shp = CType(GIS.Locate(ptg, 5 / GIS.Zoom), TGIS_Shape) ' 5 pixels precision
            If Not shp Is Nothing Then
                GIS_Attributes.ShowShape(shp)
            End If
        End Sub

        Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
            Dim lv As TGIS_LayerVector

            If Not GIS.Get(edtLayer.Text) Is Nothing Then
                MessageBox.Show("Result layer already exists. Use different name.")
                Exit Sub
            End If

            If rbtnVoronoi.Checked Then
                lv = New TGIS_LayerVoronoi()
            Else
                lv = New TGIS_LayerDelaunay()
            End If

            lv.Name = edtLayer.Text
            lv.ImportLayer(CType(GIS.Items.item(0), TGIS_LayerVector), GIS.Extent, TGIS_ShapeType.Unknown, "", False)
            lv.Transparency = 60

            lv.Params.Render.Expression = "GIS_AREA"
            lv.Params.Render.MinVal = 10000000
            lv.Params.Render.MaxVal = 1300000000
            lv.Params.Render.StartColor = (New TGIS_Color).White
            If rbtnVoronoi.Checked Then
                lv.Params.Render.EndColor = (New TGIS_Color).Red
            Else
                lv.Params.Render.EndColor = (New TGIS_Color).Blue
            End If

            lv.Params.Render.Zones = 10
            lv.Params.Area.Color = (New TGIS_Color).RenderColor
            lv.CS = GIS.CS

            GIS.Add(lv)
            GIS.InvalidateWholeMap()
            GIS_Legend.Invalidate()
        End Sub
    End Class
End Namespace
