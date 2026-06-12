Imports System
Imports System.Windows.Forms
Imports System.Drawing
Imports TatukGIS_XDK11

' Enumerators sample — demonstrates spatial iteration using TGIS_LayerVector.Loop() enumerators (ActiveX/COM).
'
' What the sample shows:
'   - Loading a polygon shapefile (California Counties) into the GIS viewer
'   - Using nested Loop() enumerators to iterate through all shapes
'   - Outer Loop() visits every county polygon in the layer
'   - Inner Loop_5(extent, "", shape, "****T", True) counts topologically adjacent neighbors
'   - DE-9IM spatial relationship filter "****T" identifies touching/intersecting boundaries
'   - Creating a new COUNT field for storing neighbor count results
'   - Using MakeEditable to modify feature attributes during enumeration
'   - Rendering layer as 5-zone white-to-red color ramp keyed on COUNT field value
'   - Displaying COUNT values as labels on each county polygon
'   - Spatial indexing enabling efficient neighbor searches
'
' ActiveX/COM-specific details:
'   - GIS is AxTGIS_ViewerWnd (ActiveX wrapper, not managed control)
'   - GIS.Items.Item(i) replaces array indexer for layer access
'   - GisUtils instance used for utility calls in COM context
'   - Loop_5 is COM-disambiguated name for five-parameter Loop overload
'   - Color constants via (New TGIS_Color).White etc. instead of predefined constants
'
' Key TatukGIS API concepts shown here:
'   TGIS_ViewerWnd (via AxTGIS_ViewerWnd) - main visual map control
'   TGIS_LayerVector            - vector layer with spatial indexing and enumeration
'   TGIS_LayerVector.Loop()     - returns enumerator for spatial iteration
'   TGIS_LayerVectorEnumerator  - iterator supporting MoveNext and Current access
'   TGIS_Shape                  - individual geographic feature (county polygon)
'   TGIS_Shape.ProjectedExtent  - bounding box for spatial queries
'   TGIS_LayerVector.AddField() - create new attribute field in layer schema
'   TGIS_Shape.MakeEditable()   - enter edit mode to modify shape attributes
'   Params.Render.Expression    - value expression for color ramp rendering
'   DE-9IM relationships        - spatial topology predicates (T = touching/intersection)

Namespace Enumerators

    Public Class frmMain
        Inherits System.Windows.Forms.Form

        Friend WithEvents pnl1 As System.Windows.Forms.Panel
        Friend WithEvents tlbr1 As System.Windows.Forms.ToolBar
        Friend WithEvents btnFullExtent As System.Windows.Forms.ToolBarButton
        Friend WithEvents btnZoomIn As System.Windows.Forms.ToolBarButton
        Friend WithEvents btnZoomOut As System.Windows.Forms.ToolBarButton
        Friend WithEvents btnDrag As System.Windows.Forms.ToolBarButton
        Friend WithEvents btnNeighbors As System.Windows.Forms.ToolBarButton
        Friend WithEvents imglst1 As System.Windows.Forms.ImageList
        Friend WithEvents GIS As AxTatukGIS_XDK11.AxTGIS_ViewerWnd
        Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar

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
            Me.pnl1 = New System.Windows.Forms.Panel()
            Me.tlbr1 = New System.Windows.Forms.ToolBar()
            Me.btnFullExtent = New System.Windows.Forms.ToolBarButton()
            Me.btnZoomIn = New System.Windows.Forms.ToolBarButton()
            Me.btnZoomOut = New System.Windows.Forms.ToolBarButton()
            Me.btnDrag = New System.Windows.Forms.ToolBarButton()
            Me.btnNeighbors = New System.Windows.Forms.ToolBarButton()
            Me.imglst1 = New System.Windows.Forms.ImageList(Me.components)
            Me.GIS = New AxTatukGIS_XDK11.AxTGIS_ViewerWnd()
            Me.StatusBar1 = New System.Windows.Forms.StatusBar()
            Me.pnl1.SuspendLayout()
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'pnl1
            '
            Me.pnl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.pnl1.Controls.Add(Me.tlbr1)
            Me.pnl1.Location = New System.Drawing.Point(0, 0)
            Me.pnl1.Name = "pnl1"
            Me.pnl1.Size = New System.Drawing.Size(500, 23)
            Me.pnl1.TabIndex = 1
            '
            'tlbr1
            '
            Me.tlbr1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.tlbr1.AutoSize = False
            Me.tlbr1.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.btnFullExtent, Me.btnZoomIn, Me.btnZoomOut, Me.btnDrag, Me.btnNeighbors})
            Me.tlbr1.Dock = System.Windows.Forms.DockStyle.None
            Me.tlbr1.DropDownArrows = True
            Me.tlbr1.ImageList = Me.imglst1
            Me.tlbr1.Location = New System.Drawing.Point(0, 0)
            Me.tlbr1.Name = "tlbr1"
            Me.tlbr1.ShowToolTips = True
            Me.tlbr1.Size = New System.Drawing.Size(500, 23)
            Me.tlbr1.TabIndex = 0
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
            'btnDrag
            '
            Me.btnDrag.ImageIndex = 3
            Me.btnDrag.Name = "btnDrag"
            Me.btnDrag.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton
            Me.btnDrag.ToolTipText = "Drag mode on/off"
            '
            'btnNeighbors
            '
            Me.btnNeighbors.ImageIndex = 4
            Me.btnNeighbors.Name = "btnNeighbors"
            Me.btnNeighbors.ToolTipText = "Count Neighbors"
            '
            'imglst1
            '
            Me.imglst1.ImageStream = CType(resources.GetObject("imglst1.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.imglst1.TransparentColor = System.Drawing.Color.Fuchsia
            Me.imglst1.Images.SetKeyName(0, "FullExtent.bmp")
            Me.imglst1.Images.SetKeyName(1, "ZoomIn.bmp")
            Me.imglst1.Images.SetKeyName(2, "ZoomOut.bmp")
            Me.imglst1.Images.SetKeyName(3, "Drag.bmp")
            Me.imglst1.Images.SetKeyName(4, "LayerProperties.bmp")
            '
            'GIS
            '
            Me.GIS.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GIS.Cursor = System.Windows.Forms.Cursors.Default
            Me.GIS.Enabled = True
            Me.GIS.Location = New System.Drawing.Point(0, 29)
            Me.GIS.Name = "GIS"
            Me.GIS.OcxState = CType(resources.GetObject("GIS.OcxState"), System.Windows.Forms.AxHost.State)
            Me.GIS.Size = New System.Drawing.Size(500, 400)
            Me.GIS.TabIndex = 2
            '
            'StatusBar1
            '
            Me.StatusBar1.Location = New System.Drawing.Point(0, 432)
            Me.StatusBar1.Name = "StatusBar1"
            Me.StatusBar1.Size = New System.Drawing.Size(500, 22)
            Me.StatusBar1.TabIndex = 3
            '
            'frmMain
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(500, 454)
            Me.Controls.Add(Me.StatusBar1)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.pnl1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "frmMain"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Use enumerators to find neighbors"
            Me.pnl1.ResumeLayout(False)
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).EndInit()
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

        ''' <summary>
        ''' Counts topological neighbors for every county and visualizes the result as a
        ''' 5-zone white-to-red color ramp.  The COUNT field is added if it does not yet exist.
        ''' </summary>
        ''' <summary>
        ''' Counts topologically adjacent neighbors for each county polygon and renders
        ''' the results using a white-to-red color gradient (more neighbors = redder).
        ''' Uses nested Loop() enumerators: outer loop visits every county, inner loop
        ''' uses Loop_5() with DE-9IM "****T" filter to find adjacent neighbors (touching boundary).
        ''' </summary>
        Private Sub actNeighbors()
            Dim shp As TGIS_Shape
            Dim shpNbr As TGIS_Shape
            Dim tmpshp As TGIS_Shape
            Dim itm_cnt_max As Int32
            Dim itm_cnt As Int32
            Dim cnt As Int32
            Dim max_cnt As Int32
            Dim lv As TGIS_LayerVector

            max_cnt = 0

            '' get the vector layer (counties)
            lv = CType(GIS.Items.item(0), TGIS_LayerVector)

            '' create a COUNT field to store the neighbor count for each shape
            '' (only create if it doesn't already exist)
            If lv.FindField("COUNT") < 0 Then
                lv.AddField("COUNT", TGIS_FieldType.Number, 10, 0)
            End If

            itm_cnt = 0
            '' show a progress indicator for the time-consuming neighbor counting
            GIS.HourglassPrepare()

            Try
                '' iterate through each county polygon
                itm_cnt_max = 0

                For Each shp In lv.Loop()
                    '' initialize neighbor count to -1 (will be incremented for each neighbor found)
                    cnt = -1
                    '' use Loop_5 with the DE-9IM "****T" predicate to find topologically adjacent shapes
                    '' the "T" in the predicate means the boundaries must touch (T = true for interior OR boundary)
                    For Each shpNbr In lv.Loop_5(shp.ProjectedExtent, "", shp, "****T", True)
                        cnt = cnt + 1
                        '' animate the hourglass cursor to show progress during processing
                        GIS.HourglassShake()
                    Next
                    '' make the shape editable so we can update its COUNT field
                    tmpshp = shp.MakeEditable()
                    '' store the neighbor count in the COUNT field
                    tmpshp.SetField("COUNT", cnt)
                    '' track the maximum neighbor count for the color ramp scaling
                    If cnt > max_cnt Then
                        max_cnt = cnt
                    End If
                Next
            Finally
                '' hide the progress indicator
                GIS.HourglassRelease()
                '' configure labels to display the COUNT field value for each county
                lv.Params.Labels.Field = "COUNT"
                '' use the COUNT field for color rendering
                lv.Params.Render.Expression = "COUNT"
                lv.Params.Render.MinVal = 1
                '' set the maximum value based on the highest neighbor count found
                lv.Params.Render.MaxVal = max_cnt
                '' create a white-to-red color gradient (low neighbor count = white, high = red)
                lv.Params.Render.StartColor = (New TGIS_Color).White
                lv.Params.Render.EndColor = (New TGIS_Color).Red
                '' divide the color range into 5 discrete zones for clearer visualization
                lv.Params.Render.Zones = 5
                '' apply the gradient color to polygon fill
                lv.Params.Area.Color = (New TGIS_Color).RenderColor
                '' repaint the map to display the neighbor count colors
                GIS.InvalidateWholeMap()
            End Try


        End Sub

        ''' <summary>Loads the California Counties shapefile and fits the viewer to its full extent.</summary>
        Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            '' load the California counties polygon layer; this will be the data analyzed for neighbor counts
            GIS.Add(GisUtils.GisCreateLayer("world", GisUtils.GisSamplesDataDirDownload() & "\World\Countries\USA\States\California\tl_2008_06_county.shp"))
            '' zoom the viewer to show all counties in California
            GIS.FullExtent()
        End Sub

        ''' <summary>Dispatches toolbar button clicks: Full Extent, Zoom In/Out, Drag toggle, Count Neighbors.</summary>
        Private Sub tlbr1_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles tlbr1.ButtonClick
            Select Case tlbr1.Buttons.IndexOf(e.Button)
                Case 0
                    '' button 0: show full extent of all counties
                    GIS.FullExtent()
                Case 1
                    '' button 1: zoom in (increase zoom 2x)
                    GIS.Zoom = GIS.Zoom * 2
                Case 2
                    '' button 2: zoom out (decrease zoom 2x)
                    GIS.Zoom = GIS.Zoom / 2
                Case 3
                    '' button 3: toggle between Select mode and Drag (pan) mode
                    Select Case tlbr1.Buttons(3).Pushed
                        Case True
                            '' button pushed: enable drag/pan mode
                            GIS.Mode = TGIS_ViewerMode.Drag
                        Case False
                            '' button not pushed: enable selection mode
                            GIS.Mode = TGIS_ViewerMode.Select
                    End Select
                Case 4
                    '' button 4: count neighbors and render results
                    actNeighbors()
            End Select
        End Sub
    End Class
End Namespace
