' Fields sample — demonstrates attribute field creation and data-driven rendering (ActiveX/COM edition).
'
' What the sample shows:
'   - Defining custom attribute fields on in-memory vector layer via AddField
'   - Field properties: name, type (TGIS_FieldType), width, decimal precision
'   - Writing field values to individual shapes via TGIS_Shape.SetField
'   - Data-driven rendering using "FIELD:<name>" expression syntax
'   - Per-attribute colour assignment via field values
'   - Per-attribute size control via field values
'   - Per-attribute rotation via field values
'   - Label text from field values
'   - Label positioning from field values
'   - Exposing layer attribute table via TGIS_DataSet
'   - Interactive grid editing with automatic map refresh
'   - Lock types: Projection (layer CS) vs Extent (world coordinates)
'
' ActiveX/COM-specific details:
'   - Uses ActiveX wrapper (TatukGIS_XDK11 / AxTGIS_*) instead of managed NDK
'   - GIS viewer and legend hosted as ActiveX controls (AxTGIS_ViewerWnd, AxTGIS_ControlLegend)
'   - TGIS_Utils instance created explicitly because static methods not available in COM
'   - Same functional approach as NDK .NET but with ActiveX interop layer
'
' Key TatukGIS API concepts shown here:
'   TGIS_ViewerWnd (via AxTGIS_ViewerWnd) - main visual map control
'   TGIS_LayerVector            - vector layer with attribute schema
'   TGIS_LayerVector.AddField() - define custom attribute field
'   TGIS_FieldType              - data type enumeration (String, Integer, Date, etc.)
'   TGIS_Shape.SetField()       - update feature attribute value
'   TGIS_DataSet                - attribute table accessor
'   "FIELD:<name>" expression   - dynamic rendering based on field value
'   TGIS_Params.Marker          - point symbol parameters (colour, size, rotation)
'   TGIS_Params.Line            - line rendering parameters
'   TGIS_Params.Area            - polygon fill parameters
'   TGIS_Params.Labels          - label text and positioning
'   TGIS_Lock                   - coordinate system locking (Projection vs Extent)
' =============================================================================

Imports System
Imports System.Windows.Forms
Imports System.Drawing
Imports TatukGIS_XDK11  ' COM-interop assembly for the ActiveX / XDK11 edition

Namespace Fields

    ''' <summary>
    ''' Main form for the Fields (ActiveX) sample.
    ''' Demonstrates defining attribute fields on a vector layer and using
    ''' per-shape field values to drive rendering parameters such as color,
    ''' size, rotation, label text, and label placement position.
    ''' </summary>
    Public Class frmMain
        Inherits System.Windows.Forms.Form

        ' -----------------------------------------------------------------------
        ' Control declarations
        ' -----------------------------------------------------------------------
        Friend WithEvents btnCreateLayer As System.Windows.Forms.Button       ' Triggers layer creation
        Friend WithEvents chckbxUseSymbols As System.Windows.Forms.CheckBox   ' Toggle CGM symbol rendering
        Friend WithEvents stsbr1 As System.Windows.Forms.StatusBar            ' Status bar (classic WinForms StatusBar for ActiveX host)
        Friend WithEvents GIS_Legend As AxTatukGIS_XDK11.AxTGIS_ControlLegend ' ActiveX legend control wrapper
        Friend WithEvents GIS As AxTatukGIS_XDK11.AxTGIS_ViewerWnd            ' ActiveX map viewer wrapper

        ''' <summary>
        ''' Initialises the form, its designer-generated controls, and sets the
        ''' initial keyboard focus to the map viewer.
        ''' </summary>
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

        ' GisUtils provides access to helper methods (GisPoint, ConstructParamPosition,
        ' GisSamplesDataDirDownload, SymbolList) that are static in the NDK managed API
        ' but must be accessed through an instance in the COM/ActiveX binding.
        Dim GisUtils As New TGIS_Utils()

        <STAThread>
        Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New frmMain())
        End Sub

        ''' <summary>
        ''' Handles the "Create Layer" button click.
        ''' Builds an in-memory TGIS_LayerVector, adds typed attribute fields,
        ''' populates it with randomly-positioned Point, Arc, and Polygon shapes,
        ''' assigns per-shape field values, then wires those fields into the
        ''' layer's rendering parameters via "FIELD:&lt;name&gt;" expressions.
        '''
        ''' Note: Unlike the NDK edition the ActiveX version uses ITGIS_LayerVector
        ''' (COM interface) and GIS_Legend.GIS_Viewer must be set to the unwrapped
        ''' COM object via GIS.GetOcx() before any viewer operations.
        ''' </summary>
        Private Sub btnCreateLayer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreateLayer.Click
            Dim lv As ITGIS_LayerVector  ' COM interface for the in-memory vector layer
            Dim shp As TGIS_Shape        ' Individual shape being created (Point / Arc / Polygon)
            Dim i As Int32               ' Loop counter
            Dim an As Double             ' Random rotation angle (degrees) for label and symbol
            Dim rand As Random

            ' The ActiveX legend control requires an explicit assignment of the
            ' underlying COM viewer object; GetOcx() unwraps the ActiveX wrapper.
            GIS_Legend.GIS_Viewer = GIS.GetOcx()

            GIS.Close()  ' Discard any previously loaded layers before rebuilding

            rand = New Random()

            ' Create a new in-memory vector layer.  No file path is given, so
            ' the layer lives entirely in RAM and is not persisted to disk.
            lv = New TGIS_LayerVector()
            lv.Name = "Fields"
            lv.Open()  ' Must be opened before adding fields or shapes

            ' -----------------------------------------------------------------
            ' Field definitions
            ' AddField(name, type, width, decimals)
            '   TGIS_FieldType.Float  : floating-point value with decimal places
            '   TGIS_FieldType.Number : integer-compatible numeric field (decimals=0)
            '   TGIS_FieldType.String : text field; width is the character count
            '
            ' These fields are later referenced in rendering-parameter
            ' "FIELD:<name>" expressions so the DK renderer reads their values
            ' per shape at draw time.
            ' -----------------------------------------------------------------
            lv.AddField("rotateLabel",  TGIS_FieldType.Float,  10, 4) ' Label rotation (degrees)
            lv.AddField("rotateSymbol", TGIS_FieldType.Float,  10, 4) ' Symbol/marker rotation (degrees)
            lv.AddField("color",        TGIS_FieldType.Number, 10, 0) ' Fill/stroke color as packed RGB integer
            lv.AddField("outlinecolor", TGIS_FieldType.Number, 10, 0) ' Outline color as packed RGB integer
            lv.AddField("size",         TGIS_FieldType.Number, 10, 0) ' Symbol or line width size value
            lv.AddField("label",        TGIS_FieldType.String,  1, 0) ' Text to display as the shape label
            lv.AddField("position",     TGIS_FieldType.String,  1, 0) ' Encoded label placement position
            lv.AddField("scale",        TGIS_FieldType.Float,  10, 4) ' Scale factor (Pi/180 for deg->rad)

            ' -----------------------------------------------------------------
            ' Create 11 Point shapes at random locations within [0,20] x [0,20].
            ' Each point receives its own random color, rotation, label, and
            ' label-position value.
            ' TGIS_Lock.Projection: coordinates are interpreted in the layer's CRS.
            ' -----------------------------------------------------------------
            For i = 0 To 10
                shp = lv.CreateShape(TGIS_ShapeType.Point)
                shp.Lock(TGIS_Lock.Projection)  ' Coordinates are in the layer's coordinate system
                shp.AddPart()                    ' A Point shape requires exactly one part
                shp.AddPoint(GisUtils.GisPoint(rand.Next(20), rand.Next(20)))

                an = rand.Next(360)  ' One random angle drives both label and symbol rotation

                ' SetField(name, value) writes the attribute for the current shape.
                shp.SetField("rotateLabel",  an)
                shp.SetField("rotateSymbol", an)

                ' Pack three 8-bit random channel values: 0x00RRGGBB
                shp.SetField("color",        (rand.Next(256) << 16) + (rand.Next(256) << 8) + rand.Next(256))
                shp.SetField("outlinecolor", (rand.Next(256) << 16) + (rand.Next(256) << 8) + rand.Next(256))
                shp.SetField("label",        "Point" & Convert.ToString(i))
                shp.SetField("size",         rand.Next(400))

                ' ConstructParamPosition encodes a TGIS_LabelPosition value as
                ' the string token expected by Params.Labels.PositionAsText.
                shp.SetField("position", GisUtils.ConstructParamPosition(CType(rand.Next(9), TGIS_LabelPosition)))

                ' Pi/180 is stored so the renderer can convert raw degree values
                ' from rotateLabel/rotateSymbol fields to radians when needed.
                shp.SetField("scale", Math.PI / 180)
                shp.Unlock()  ' Commit geometry and attributes; shape joins the layer
            Next

            ' -----------------------------------------------------------------
            ' One multi-vertex Arc (open polyline) with 11 random vertices
            ' spread around the origin [-10,10] x [-10,10].
            ' TGIS_Lock.Extent: coordinates are in world/extent space.
            ' -----------------------------------------------------------------
            shp = lv.CreateShape(TGIS_ShapeType.Arc)
            shp.Lock(TGIS_Lock.Extent)
            shp.AddPart()
            For i = 0 To 10
                shp.AddPoint(GisUtils.GisPoint(rand.Next(20) - 10, rand.Next(20) - 10))
            Next
            an = rand.Next(360)
            shp.SetField("rotateLabel",  an)
            shp.SetField("rotateSymbol", an)
            shp.SetField("color",        (rand.Next(256) << 16) + (rand.Next(256) << 8) + rand.Next(256))
            shp.SetField("label",        "Point" & Convert.ToString(1))
            shp.SetField("outlinecolor", (rand.Next(256) << 16) + (rand.Next(256) << 8) + rand.Next(256))
            shp.SetField("scale",        Math.PI / 180)
            shp.Unlock()

            ' -----------------------------------------------------------------
            ' 11 two-vertex Arc shapes with incrementing "size" field values
            ' (i = 1..11), arranged in a horizontal row.
            ' This group demonstrates that "FIELD:size" drives line width so
            ' each segment is drawn progressively thicker.
            ' -----------------------------------------------------------------
            For i = 1 To 11
                shp = lv.CreateShape(TGIS_ShapeType.Arc)
                shp.Lock(TGIS_Lock.Extent)
                shp.AddPart()
                shp.AddPoint(GisUtils.GisPoint(20 + 2 * i, 0))
                shp.AddPoint(GisUtils.GisPoint(30 + 2 * i, 10))
                an = rand.Next(360)
                shp.SetField("rotateLabel",  an)
                shp.SetField("rotateSymbol", an)
                shp.SetField("size",         i)  ' Increasing size produces visibly thicker lines
                shp.SetField("color",        (rand.Next(256) << 16) + (rand.Next(256) << 8) + rand.Next(256))
                shp.SetField("outlinecolor", (rand.Next(256) << 16) + (rand.Next(256) << 8) + rand.Next(256))
                shp.SetField("scale",        Math.PI / 180)
                shp.Unlock()
            Next

            ' -----------------------------------------------------------------
            ' One Polygon with 4 random vertices near the origin.
            ' -----------------------------------------------------------------
            shp = lv.CreateShape(TGIS_ShapeType.Polygon)
            shp.Lock(TGIS_Lock.Extent)
            shp.AddPart()
            For i = 0 To 3
                shp.AddPoint(GisUtils.GisPoint(rand.Next(20) - 10, rand.Next(20) - 10))
            Next
            an = rand.Next(360)
            shp.SetField("rotateLabel",  an)
            shp.SetField("rotateSymbol", an)
            shp.SetField("color",        (rand.Next(256) << 16) + (rand.Next(256) << 8) + rand.Next(256))
            shp.SetField("outlinecolor", (rand.Next(256) << 16) + (rand.Next(256) << 8) + rand.Next(256))
            shp.SetField("label",        "Point" & Convert.ToString(2))
            shp.Unlock()

            ' =================================================================
            ' Rendering parameter configuration
            '
            ' The "FIELD:<fieldname>" token in any *AsText property tells the
            ' DK renderer to evaluate the named field on each shape at draw time
            ' instead of using a single static value.  An optional unit suffix
            ' (":1 dip", ":1 deg") converts the raw numeric value to the
            ' expected rendering unit.
            ' =================================================================

            ' --- Point / Marker rendering ---
            lv.Params.Marker.ColorAsText        = "FIELD:color"        ' Per-shape fill color
            lv.Params.Marker.OutlineColorAsText = "FIELD:outlinecolor" ' Per-shape outline color
            lv.Params.Marker.OutlineWidth       = 1                    ' Static 1-pixel outline
            lv.Params.Marker.Size               = 20 * 20 'converting points to twips -> 1pt = 20 twips

            If (chckbxUseSymbols.Checked) Then
                ' Prepare() registers the CGM file in the SymbolList cache and
                ' returns a handle used by the renderer to look up the symbol.
                lv.Params.Marker.Symbol             = GisUtils.SymbolList.Prepare(GisUtils.GisSamplesDataDirDownload() & "\Symbols\2267.cgm")
                lv.Params.Marker.SizeAsText         = "FIELD:size:1 dip"   ' Size from field, device-independent pixels
                lv.Params.Marker.SymbolRotateAsText = "FIELD:rotateSymbol" ' Symbol rotation from field

            End If

            ' --- Label rendering ---
            lv.Params.Labels.Field              = "label"              ' Field that supplies label text
            lv.Params.Labels.Allocator          = False                ' Disable automatic conflict avoidance
            lv.Params.Labels.ColorAsText        = "FIELD:color"        ' Label background color from field
            lv.Params.Labels.OutlineColorAsText = "FIELD:outlinecolor" ' Label outline color from field
            lv.Params.Labels.PositionAsText     = "FIELD:position"    ' Label anchor position from field
            lv.Params.Labels.FontColorAsText    = "FIELD:color"        ' Font color from field
            lv.Params.Labels.RotateAsText       = "FIELD:rotateLabel:1 deg" ' Label rotation from field, in degrees


            ' --- Line / Arc rendering ---
            If (chckbxUseSymbols.Checked) Then
                lv.Params.Line.Symbol             = GisUtils.SymbolList.Prepare(GisUtils.GisSamplesDataDirDownload() & "\Symbols\1301.cgm")
                lv.Params.Line.SymbolRotateAsText = "FIELD:rotateSymbol:1 deg"
            End If
            lv.Params.Line.ColorAsText        = "FIELD:color"
            lv.Params.Line.OutlineColorAsText = "FIELD:outlinecolor"
            lv.Params.Line.WidthAsText        = "FIELD:size:1 dip" ' Line width from "size" field


            ' --- Area / Polygon rendering ---
            lv.Params.Area.SymbolRotateAsText = "rotateSymbol" ' Note: no FIELD: prefix here intentionally
            If (chckbxUseSymbols.Checked) Then
                lv.Params.Area.Symbol = GisUtils.SymbolList.Prepare(GisUtils.GisSamplesDataDirDownload() & "\Symbols\1301.cgm")
            End If
            lv.Params.Area.ColorAsText        = "FIELD:color"
            lv.Params.Area.OutlineColorAsText = "FIELD:outlinecolor"

            ' Add the finished layer to the viewer and zoom to fit all shapes.
            ' The ActiveX edition does not include a DataGrid, so there is no
            ' attribute table binding in this version.
            GIS.Add(lv)
            GIS.FullExtent()

        End Sub
    End Class
End Namespace
