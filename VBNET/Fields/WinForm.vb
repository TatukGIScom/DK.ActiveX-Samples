Imports System
Imports System.Windows.Forms
Imports System.Drawing
Imports TatukGIS_XDK11

Namespace Fields

    Public Class frmMain
        Inherits System.Windows.Forms.Form

        Friend WithEvents btnCreateLayer As System.Windows.Forms.Button
        Friend WithEvents chckbxUseSymbols As System.Windows.Forms.CheckBox
        Friend WithEvents stsbr1 As System.Windows.Forms.StatusBar
        Friend WithEvents GIS_Legend As AxTatukGIS_XDK11.AxTGIS_ControlLegend
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
            Me.btnCreateLayer = New System.Windows.Forms.Button()
            Me.chckbxUseSymbols = New System.Windows.Forms.CheckBox()
            Me.stsbr1 = New System.Windows.Forms.StatusBar()
            Me.GIS_Legend = New AxTatukGIS_XDK11.AxTGIS_ControlLegend()
            Me.GIS = New AxTatukGIS_XDK11.AxTGIS_ViewerWnd()
            CType(Me.GIS_Legend, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'btnCreateLayer
            '
            Me.btnCreateLayer.Location = New System.Drawing.Point(0, 0)
            Me.btnCreateLayer.Name = "btnCreateLayer"
            Me.btnCreateLayer.Size = New System.Drawing.Size(90, 23)
            Me.btnCreateLayer.TabIndex = 0
            Me.btnCreateLayer.Text = "Create Layer"
            Me.btnCreateLayer.UseVisualStyleBackColor = True
            '
            'chckbxUseSymbols
            '
            Me.chckbxUseSymbols.AutoSize = True
            Me.chckbxUseSymbols.Location = New System.Drawing.Point(96, 4)
            Me.chckbxUseSymbols.Name = "chckbxUseSymbols"
            Me.chckbxUseSymbols.Size = New System.Drawing.Size(87, 17)
            Me.chckbxUseSymbols.TabIndex = 1
            Me.chckbxUseSymbols.Text = "Use Symbols"
            Me.chckbxUseSymbols.UseVisualStyleBackColor = True
            '
            'stsbr1
            '
            Me.stsbr1.Location = New System.Drawing.Point(0, 495)
            Me.stsbr1.Name = "stsbr1"
            Me.stsbr1.Size = New System.Drawing.Size(629, 22)
            Me.stsbr1.TabIndex = 2
            Me.stsbr1.Text = "Open a layer properties form to change parameters"
            '
            'GIS_Legend
            '
            Me.GIS_Legend.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.GIS_Legend.Enabled = True
            Me.GIS_Legend.Location = New System.Drawing.Point(0, 27)
            Me.GIS_Legend.Name = "GIS_Legend"
            Me.GIS_Legend.OcxState = CType(resources.GetObject("GIS_Legend.OcxState"), System.Windows.Forms.AxHost.State)
            Me.GIS_Legend.Size = New System.Drawing.Size(141, 467)
            Me.GIS_Legend.TabIndex = 3
            '
            'GIS
            '
            Me.GIS.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GIS.Cursor = System.Windows.Forms.Cursors.Default
            Me.GIS.Enabled = True
            Me.GIS.Location = New System.Drawing.Point(141, 27)
            Me.GIS.Name = "GIS"
            Me.GIS.OcxState = CType(resources.GetObject("GIS.OcxState"), System.Windows.Forms.AxHost.State)
            Me.GIS.Size = New System.Drawing.Size(488, 466)
            Me.GIS.TabIndex = 4
            '
            'frmMain
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(629, 517)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.GIS_Legend)
            Me.Controls.Add(Me.stsbr1)
            Me.Controls.Add(Me.chckbxUseSymbols)
            Me.Controls.Add(Me.btnCreateLayer)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "frmMain"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Fields"
            CType(Me.GIS_Legend, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
#End Region

        Dim GisUtils As New TGIS_Utils()

        <STAThread>
        Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New frmMain())
        End Sub

        Private Sub btnCreateLayer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreateLayer.Click
            Dim lv As ITGIS_LayerVector
            Dim shp As TGIS_Shape
            Dim i As Int32
            Dim an As Double
            Dim rand As Random

            GIS_Legend.GIS_Viewer = GIS.GetOcx()

            GIS.Close()

            rand = New Random()

            lv = New TGIS_LayerVector()
            lv.Name = "Fields"
            lv.Open()

            lv.AddField("rotateLabel", TGIS_FieldType.Float, 10, 4)
            lv.AddField("rotateSymbol", TGIS_FieldType.Float, 10, 4)
            lv.AddField("color", TGIS_FieldType.Number, 10, 0)
            lv.AddField("outlinecolor", TGIS_FieldType.Number, 10, 0)
            lv.AddField("size", TGIS_FieldType.Number, 10, 0)
            lv.AddField("label", TGIS_FieldType.String, 1, 0)
            lv.AddField("position", TGIS_FieldType.String, 1, 0)
            lv.AddField("scale", TGIS_FieldType.Float, 10, 4)

            For i = 0 To 10
                shp = lv.CreateShape(TGIS_ShapeType.Point)
                shp.Lock(TGIS_Lock.Projection)
                shp.AddPart()
                shp.AddPoint(GisUtils.GisPoint(rand.Next(20), rand.Next(20)))
                an = rand.Next(360)
                shp.SetField("rotateLabel", an)
                shp.SetField("rotateSymbol", an)
                shp.SetField("color", (rand.Next(256) << 16) + (rand.Next(256) << 8) + rand.Next(256))
                shp.SetField("outlinecolor", (rand.Next(256) << 16) + (rand.Next(256) << 8) + rand.Next(256))
                shp.SetField("label", "Point" & Convert.ToString(i))
                shp.SetField("size", rand.Next(400))
                shp.SetField("position", GisUtils.ConstructParamPosition(CType(rand.Next(9), TGIS_LabelPosition)))
                shp.SetField("scale", Math.PI / 180)
                shp.Unlock()
            Next

            shp = lv.CreateShape(TGIS_ShapeType.Arc)
            shp.Lock(TGIS_Lock.Extent)
            shp.AddPart()
            For i = 0 To 10
                shp.AddPoint(GisUtils.GisPoint(rand.Next(20) - 10, rand.Next(20) - 10))
            Next
            an = rand.Next(360)
            shp.SetField("rotateLabel", an)
            shp.SetField("rotateSymbol", an)
            shp.SetField("color", (rand.Next(256) << 16) + (rand.Next(256) << 8) + rand.Next(256))
            shp.SetField("label", "Point" & Convert.ToString(1))
            shp.SetField("outlinecolor", (rand.Next(256) << 16) + (rand.Next(256) << 8) + rand.Next(256))
            shp.SetField("scale", Math.PI / 180)
            shp.Unlock()

            For i = 1 To 11
                shp = lv.CreateShape(TGIS_ShapeType.Arc)
                shp.Lock(TGIS_Lock.Extent)
                shp.AddPart()
                shp.AddPoint(GisUtils.GisPoint(20 + 2 * i, 0))
                shp.AddPoint(GisUtils.GisPoint(30 + 2 * i, 10))
                an = rand.Next(360)
                shp.SetField("rotateLabel", an)
                shp.SetField("rotateSymbol", an)
                shp.SetField("size", i)
                shp.SetField("color", (rand.Next(256) << 16) + (rand.Next(256) << 8) + rand.Next(256))
                shp.SetField("outlinecolor", (rand.Next(256) << 16) + (rand.Next(256) << 8) + rand.Next(256))
                shp.SetField("scale", Math.PI / 180)
                shp.Unlock()
            Next

            shp = lv.CreateShape(TGIS_ShapeType.Polygon)
            shp.Lock(TGIS_Lock.Extent)
            shp.AddPart()
            For i = 0 To 3
                shp.AddPoint(GisUtils.GisPoint(rand.Next(20) - 10, rand.Next(20) - 10))
            Next
            an = rand.Next(360)
            shp.SetField("rotateLabel", an)
            shp.SetField("rotateSymbol", an)
            shp.SetField("color", (rand.Next(256) << 16) + (rand.Next(256) << 8) + rand.Next(256))
            shp.SetField("outlinecolor", (rand.Next(256) << 16) + (rand.Next(256) << 8) + rand.Next(256))
            shp.SetField("label", "Point" & Convert.ToString(2))
            shp.Unlock()

            lv.Params.Marker.ColorAsText = "FIELD:color"
            lv.Params.Marker.OutlineColorAsText = "FIELD:outlinecolor"
            lv.Params.Marker.OutlineWidth = 1
            lv.Params.Marker.Size = 20 * 20 'converting points to twips -> 1pt = 20 twips

            If (chckbxUseSymbols.Checked) Then
                lv.Params.Marker.Symbol = GisUtils.SymbolList.Prepare(GisUtils.GisSamplesDataDirDownload() & "\Symbols\2267.cgm")
                lv.Params.Marker.SizeAsText = "FIELD:size:1 dip"
                lv.Params.Marker.SymbolRotateAsText = "FIELD:rotateSymbol"

            End If

            lv.Params.Labels.Field = "label"
            lv.Params.Labels.Allocator = False
            lv.Params.Labels.ColorAsText = "FIELD:color"
            lv.Params.Labels.OutlineColorAsText = "FIELD:outlinecolor"
            lv.Params.Labels.PositionAsText = "FIELD:position"
            lv.Params.Labels.FontColorAsText = "FIELD:color"
            lv.Params.Labels.RotateAsText = "FIELD:rotateLabel:1 deg"


            If (chckbxUseSymbols.Checked) Then
                lv.Params.Line.Symbol = GisUtils.SymbolList.Prepare(GisUtils.GisSamplesDataDirDownload() & "\Symbols\1301.cgm")
                lv.Params.Line.SymbolRotateAsText = "FIELD:rotateSymbol:1 deg"
            End If
            lv.Params.Line.ColorAsText = "FIELD:color"
            lv.Params.Line.OutlineColorAsText = "FIELD:outlinecolor"
            lv.Params.Line.WidthAsText = "FIELD:size:1 dip"


            lv.Params.Area.SymbolRotateAsText = "rotateSymbol"
            If (chckbxUseSymbols.Checked) Then
                lv.Params.Area.Symbol = GisUtils.SymbolList.Prepare(GisUtils.GisSamplesDataDirDownload() & "\Symbols\1301.cgm")
            End If
            lv.Params.Area.ColorAsText = "FIELD:color"
            lv.Params.Area.OutlineColorAsText = "FIELD:outlinecolor"

            GIS.Add(lv)
            GIS.FullExtent()

        End Sub
    End Class
End Namespace
