'=============================================================================
' This source code is a part of TatukGIS Developer Kernel.
'=============================================================================
'
' GeoCoding Sample - VB.NET / ActiveX (TatukGIS XDK11)
' =====================================================
' Demonstrates address geocoding and shortest-path routing using the TatukGIS
' ActiveX (XDK11) COM wrapper.
'
' Geocoding translates a human-readable address string (e.g. "Chrysler 1345")
' into geographic coordinates by matching it against street-range attribute
' fields stored in a vector layer.  The TGIS_Geocoding class performs this
' matching entirely on the client side using the loaded shapefile — no
' external service is needed.
'
' Routing finds the shortest path between two geocoded points using
' TGIS_ShortestPath, which builds an in-memory Dijkstra graph from the road
' network.  The resulting path segments are visualised by copying them into a
' separate in-memory "RouteDisplay" layer rendered in red, with green/red
' point markers for the start and end positions.
'
' This variant uses the ActiveX/COM interop binding (AxTatukGIS_XDK11) instead
' of the managed NDK assembly.  The API surface is equivalent but:
'   - Objects such as TGIS_Geocoding and TGIS_ShortestPath require an explicit
'     Create_() call after construction (COM two-phase initialisation).
'   - TGIS_Color and other value types are accessed via New instances.
'   - GIS_ControlScale.GIS_Viewer is set by passing GIS.GetOcx() (the underlying
'     IDispatch pointer) rather than the managed TGIS_ViewerWnd object.
'
' Key concepts shown:
'   - Opening a TatukGIS project (.ttkproject) and retrieving a named layer
'   - Constructing TGIS_Geocoding (COM) with TIGER/Line address-range field mappings
'   - Using TGIS_Geocoding.Parse to resolve typed addresses to coordinates
'   - Building a TGIS_ShortestPath graph with per-link-type cost modifiers
'   - Handling the LinkTypeEvent callback to classify road segments by MTFCC code
'   - Drawing route results and address markers into a TGIS_LayerVector overlay
'
' Data used:
'   California.ttkproject — a TatukGIS project referencing US Census TIGER/Line
'   street data.  Relevant attribute fields:
'     FULLNAME  — full street name
'     LFROMADD  — address range start, left side of street
'     LTOADD    — address range end, left side of street
'     RFROMADD  — address range start, right side of street
'     RTOADD    — address range end, right side of street
'     MTFCC     — MAF/TIGER Feature Class Code; "S1400"+ = local/residential road
'=============================================================================

Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS_XDK11  ' TatukGIS ActiveX/COM interop assembly

Namespace Geocoding
    ''' <summary>
    ''' Main application form for the GeoCoding &amp; Routing ActiveX sample.
    ''' Hosts an <see cref="AxTatukGIS_XDK11.AxTGIS_ViewerWnd"/> (COM-hosted viewer)
    ''' displaying California street data and provides address resolution and
    ''' shortest-path route finding.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form

        ' ------------------------------------------------------------------ '
        '  Designer-managed fields (do not modify names/types)                '
        ' ------------------------------------------------------------------ '

        ''' <summary>Required designer variable.</summary>
        Private components As System.ComponentModel.IContainer

        Private panel1 As System.Windows.Forms.Panel           ' Right-hand control panel
        Private memRoute As System.Windows.Forms.TextBox       ' Read-only directions text area
        Private groupBox1 As System.Windows.Forms.GroupBox     ' Groups routing parameter controls
        Private lblSmallRoads As System.Windows.Forms.Label    ' Label for trkSmallRoads
        Private trkSmallRoads As System.Windows.Forms.TrackBar ' Local-road preference slider (1–10)
        Private lblHighways As System.Windows.Forms.Label      ' Label for trkHighways
        Private trkHighways As System.Windows.Forms.TrackBar   ' Highway preference slider (1–10)
        Private lblAddrFrom As System.Windows.Forms.Label      ' Label for edtAddrFrom
        Private edtAddrFrom As System.Windows.Forms.TextBox    ' Start address input
        Private lblAddrTo As System.Windows.Forms.Label        ' Label for edtAddrTo
        Private edtAddrTo As System.Windows.Forms.TextBox      ' End address input
        Private WithEvents btnResolve As System.Windows.Forms.Button  ' Resolve start address
        Private WithEvents btnRoute As System.Windows.Forms.Button    ' Find shortest route
        Private GIS As AxTatukGIS_XDK11.AxTGIS_ViewerWnd              ' TatukGIS ActiveX viewer
        Private layerSrc As TGIS_LayerVector                           ' Source streets layer
        Private layerRoute As TGIS_LayerVector                         ' In-memory route display layer
        Private geoObj As TGIS_Geocoding                               ' Geocoding engine (COM)
        Private rtrObj As TGIS_ShortestPath                            ' Routing engine (COM, Dijkstra)
        Private toolTip1 As System.Windows.Forms.ToolTip               ' Tooltip provider for address boxes
        Private GIS_ControlScale As AxTatukGIS_XDK11.AxTGIS_ControlScale ' Scale-bar overlay (COM)

        ''' <summary>
        ''' Initialises the form and wires up tooltip hints on the address text boxes.
        ''' The hints show example address strings that the California data set supports.
        ''' </summary>
        Public Sub New()
            '
            ' Required for Windows Form Designer support
            '
            InitializeComponent()

            ' Provide example address strings as tooltips on the From/To inputs
            ' so users know what format the geocoder expects.
            toolTip1.SetToolTip(Me.edtAddrFrom, """Pen"", ""Pennsylvania"", ""Penn 12""")
            toolTip1.SetToolTip(Me.edtAddrTo, """Pen"", ""Pennsylvania"", ""Penn 12""")
        End Sub

        ''' <summary>
        ''' Releases managed and unmanaged resources held by the form.
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
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WinForm))
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.memRoute = New System.Windows.Forms.TextBox()
            Me.groupBox1 = New System.Windows.Forms.GroupBox()
            Me.btnRoute = New System.Windows.Forms.Button()
            Me.btnResolve = New System.Windows.Forms.Button()
            Me.edtAddrTo = New System.Windows.Forms.TextBox()
            Me.lblAddrTo = New System.Windows.Forms.Label()
            Me.edtAddrFrom = New System.Windows.Forms.TextBox()
            Me.lblAddrFrom = New System.Windows.Forms.Label()
            Me.trkHighways = New System.Windows.Forms.TrackBar()
            Me.lblHighways = New System.Windows.Forms.Label()
            Me.trkSmallRoads = New System.Windows.Forms.TrackBar()
            Me.lblSmallRoads = New System.Windows.Forms.Label()
            Me.toolTip1 = New System.Windows.Forms.ToolTip(Me.components)
            Me.GIS_ControlScale = New AxTatukGIS_XDK11.AxTGIS_ControlScale()
            Me.GIS = New AxTatukGIS_XDK11.AxTGIS_ViewerWnd()
            Me.panel1.SuspendLayout()
            Me.groupBox1.SuspendLayout()
            CType(Me.trkHighways, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.trkSmallRoads, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.GIS_ControlScale, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'panel1
            '
            Me.panel1.AccessibleDescription = ""
            Me.panel1.Controls.Add(Me.memRoute)
            Me.panel1.Controls.Add(Me.groupBox1)
            Me.panel1.Dock = System.Windows.Forms.DockStyle.Right
            Me.panel1.Location = New System.Drawing.Point(396, 0)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(188, 466)
            Me.panel1.TabIndex = 0
            '
            'memRoute
            '
            Me.memRoute.Dock = System.Windows.Forms.DockStyle.Fill
            Me.memRoute.Location = New System.Drawing.Point(0, 249)
            Me.memRoute.Multiline = True
            Me.memRoute.Name = "memRoute"
            Me.memRoute.ReadOnly = True
            Me.memRoute.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
            Me.memRoute.Size = New System.Drawing.Size(188, 217)
            Me.memRoute.TabIndex = 1
            Me.memRoute.WordWrap = False
            '
            'groupBox1
            '
            Me.groupBox1.Controls.Add(Me.btnRoute)
            Me.groupBox1.Controls.Add(Me.btnResolve)
            Me.groupBox1.Controls.Add(Me.edtAddrTo)
            Me.groupBox1.Controls.Add(Me.lblAddrTo)
            Me.groupBox1.Controls.Add(Me.edtAddrFrom)
            Me.groupBox1.Controls.Add(Me.lblAddrFrom)
            Me.groupBox1.Controls.Add(Me.trkHighways)
            Me.groupBox1.Controls.Add(Me.lblHighways)
            Me.groupBox1.Controls.Add(Me.trkSmallRoads)
            Me.groupBox1.Controls.Add(Me.lblSmallRoads)
            Me.groupBox1.Dock = System.Windows.Forms.DockStyle.Top
            Me.groupBox1.Location = New System.Drawing.Point(0, 0)
            Me.groupBox1.Name = "groupBox1"
            Me.groupBox1.Size = New System.Drawing.Size(188, 249)
            Me.groupBox1.TabIndex = 0
            Me.groupBox1.TabStop = False
            Me.groupBox1.Text = "Routing parameters"
            '
            'btnRoute
            '
            Me.btnRoute.Location = New System.Drawing.Point(94, 214)
            Me.btnRoute.Name = "btnRoute"
            Me.btnRoute.Size = New System.Drawing.Size(75, 23)
            Me.btnRoute.TabIndex = 9
            Me.btnRoute.Text = "Find &Route"
            '
            'btnResolve
            '
            Me.btnResolve.Location = New System.Drawing.Point(94, 160)
            Me.btnResolve.Name = "btnResolve"
            Me.btnResolve.Size = New System.Drawing.Size(75, 23)
            Me.btnResolve.TabIndex = 8
            Me.btnResolve.Text = "Find &Address"
            '
            'edtAddrTo
            '
            Me.edtAddrTo.Location = New System.Drawing.Point(24, 192)
            Me.edtAddrTo.Name = "edtAddrTo"
            Me.edtAddrTo.Size = New System.Drawing.Size(145, 20)
            Me.edtAddrTo.TabIndex = 7
            Me.edtAddrTo.Text = "wash"
            '
            'lblAddrTo
            '
            Me.lblAddrTo.Location = New System.Drawing.Point(24, 176)
            Me.lblAddrTo.Name = "lblAddrTo"
            Me.lblAddrTo.Size = New System.Drawing.Size(40, 13)
            Me.lblAddrTo.TabIndex = 6
            Me.lblAddrTo.Text = "&To"
            '
            'edtAddrFrom
            '
            Me.edtAddrFrom.Location = New System.Drawing.Point(24, 136)
            Me.edtAddrFrom.Name = "edtAddrFrom"
            Me.edtAddrFrom.Size = New System.Drawing.Size(145, 20)
            Me.edtAddrFrom.TabIndex = 5
            Me.edtAddrFrom.Text = "Chrys 1345"
            '
            'lblAddrFrom
            '
            Me.lblAddrFrom.Location = New System.Drawing.Point(24, 120)
            Me.lblAddrFrom.Name = "lblAddrFrom"
            Me.lblAddrFrom.Size = New System.Drawing.Size(40, 13)
            Me.lblAddrFrom.TabIndex = 4
            Me.lblAddrFrom.Text = "&From"
            '
            'trkHighways
            '
            Me.trkHighways.AutoSize = False
            Me.trkHighways.LargeChange = 1
            Me.trkHighways.Location = New System.Drawing.Point(16, 88)
            Me.trkHighways.Minimum = 1
            Me.trkHighways.Name = "trkHighways"
            Me.trkHighways.Size = New System.Drawing.Size(161, 25)
            Me.trkHighways.TabIndex = 3
            Me.trkHighways.Value = 5
            '
            'lblHighways
            '
            Me.lblHighways.Location = New System.Drawing.Point(24, 72)
            Me.lblHighways.Name = "lblHighways"
            Me.lblHighways.Size = New System.Drawing.Size(90, 13)
            Me.lblHighways.TabIndex = 2
            Me.lblHighways.Text = "Prefer &highway"
            '
            'trkSmallRoads
            '
            Me.trkSmallRoads.AutoSize = False
            Me.trkSmallRoads.LargeChange = 1
            Me.trkSmallRoads.Location = New System.Drawing.Point(16, 40)
            Me.trkSmallRoads.Minimum = 1
            Me.trkSmallRoads.Name = "trkSmallRoads"
            Me.trkSmallRoads.Size = New System.Drawing.Size(161, 25)
            Me.trkSmallRoads.TabIndex = 1
            Me.trkSmallRoads.Value = 1
            '
            'lblSmallRoads
            '
            Me.lblSmallRoads.Location = New System.Drawing.Point(24, 24)
            Me.lblSmallRoads.Name = "lblSmallRoads"
            Me.lblSmallRoads.Size = New System.Drawing.Size(100, 13)
            Me.lblSmallRoads.TabIndex = 0
            Me.lblSmallRoads.Text = "Prefer &local roads"
            '
            'GIS_ControlScale  — ActiveX scale-bar control hosted in an AxHost wrapper
            '
            Me.GIS_ControlScale.BackColor = System.Drawing.SystemColors.Control
            Me.GIS_ControlScale.Enabled = True
            Me.GIS_ControlScale.Location = New System.Drawing.Point(8, 8)
            Me.GIS_ControlScale.Name = "GIS_ControlScale"
            Me.GIS_ControlScale.OcxState = CType(resources.GetObject("GIS_ControlScale.OcxState"), System.Windows.Forms.AxHost.State)
            Me.GIS_ControlScale.Size = New System.Drawing.Size(145, 25)
            Me.GIS_ControlScale.TabIndex = 1
            '
            'GIS  — ActiveX viewer hosted inside a WinForms AxHost wrapper
            '
            Me.GIS.Cursor = System.Windows.Forms.Cursors.Default
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.Enabled = True
            Me.GIS.Location = New System.Drawing.Point(0, 0)
            Me.GIS.Name = "GIS"
            Me.GIS.OcxState = CType(resources.GetObject("GIS.OcxState"), System.Windows.Forms.AxHost.State)
            Me.GIS.Size = New System.Drawing.Size(396, 466)
            Me.GIS.TabIndex = 1
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(584, 466)
            Me.Controls.Add(Me.GIS_ControlScale)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.panel1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Geocoding & Routing"
            Me.panel1.ResumeLayout(False)
            Me.panel1.PerformLayout()
            Me.groupBox1.ResumeLayout(False)
            Me.groupBox1.PerformLayout()
            CType(Me.trkHighways, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.trkSmallRoads, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.GIS_ControlScale, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
#End Region

        ' TGIS_Utils instance used to resolve the sample data directory and unit lists
        ' via the COM API (methods are instance-level on the ActiveX binding,
        ' unlike the shared methods in the managed NDK).
        Dim GisUtils As New TGIS_Utils()

        ''' <summary>
        ''' Application entry point.  Configures visual styles and launches the form.
        ''' </summary>
        <STAThread>
        Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New WinForm())
        End Sub

        ''' <summary>
        ''' Handles the form Load event.
        ''' Opens the California street project, creates the route-display overlay
        ''' layer, and initialises the geocoding and routing engines.
        ''' </summary>
        ''' <remarks>
        ''' In the ActiveX binding, <c>TGIS_Geocoding</c> and <c>TGIS_ShortestPath</c>
        ''' require an explicit <c>Create_()</c> call after <c>New</c> to complete
        ''' COM object initialisation.
        ''' <c>GIS.GetOcx()</c> returns the underlying IDispatch pointer needed by
        ''' controls such as <c>GIS_ControlScale</c> that hold a COM viewer reference.
        ''' </remarks>
        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            ' Link the scale-bar control to the COM viewer object via IDispatch.
            GIS_ControlScale.GIS_Viewer = GIS.GetOcx()

            ' Open the TatukGIS project which contains the California street data.
            GIS.Open(GisUtils.GisSamplesDataDirDownload() & "Samples\Projects\California.ttkproject")

            ' Retrieve the streets vector layer by name so the geocoding and
            ' routing engines can operate on its shape and attribute data.
            layerSrc = CType(GIS.Get("streets"), TGIS_LayerVector)

            If layerSrc Is Nothing Then
                Exit Sub
            End If

            ' Zoom to the full extent of the street network on startup.
            GIS.VisibleExtent = layerSrc.Extent

            ' ------------------------------------------------------------------
            ' Create an in-memory overlay layer for route visualisation.
            ' UseConfig = False prevents the DK from persisting a style config.
            ' Red line (Width = -2, negative = device pixels) and 1-px outline.
            ' ------------------------------------------------------------------
            layerRoute = New TGIS_LayerVector()
            layerRoute.UseConfig = False
            layerRoute.Params.Line.Color = (New TGIS_Color).Red
            layerRoute.Params.Line.Width = -2
            layerRoute.Params.Marker.OutlineWidth = 1
            layerRoute.Name = "RouteDisplay"
            layerRoute.CS = GIS.CS  ' Must share the viewer's coordinate system

            GIS.Add(layerRoute)

            ' ------------------------------------------------------------------
            ' Configure the geocoding engine with TIGER/Line field mappings.
            ' COM two-phase initialisation: New allocates, Create_(layer) connects
            ' to the source layer and prepares the geocoding index.
            '   Offset    — distance along the segment for the geocoded point.
            '   RoadName  — attribute field with the full street name.
            '   LFrom/LTo — address number range, left side of street.
            '   RFrom/RTo — address number range, right side of street.
            ' ------------------------------------------------------------------
            geoObj = New TGIS_Geocoding
            geoObj.Create_(layerSrc)
            geoObj.Offset = 0.0001
            geoObj.RoadName = "FULLNAME"
            geoObj.LFrom = "LFROMADD"
            geoObj.LTo = "LTOADD"
            geoObj.RFrom = "RFROMADD"
            geoObj.RTo = "RTOADD"

            ' ------------------------------------------------------------------
            ' Configure the routing engine.
            ' COM two-phase initialisation: Create_(viewer) connects the router to
            ' the map viewer so it can resolve coordinate systems.
            ' LoadTheData builds the Dijkstra graph from all arc shapes in layerSrc.
            ' ------------------------------------------------------------------
            rtrObj = New TGIS_ShortestPath
            rtrObj.Create_(GIS.GetOcx())
            AddHandler rtrObj.LinkTypeEvent, AddressOf doLinkType
            rtrObj.LoadTheData(layerSrc)
            rtrObj.RoadName = "FULLNAME"

            ' Display the scale bar in miles (EPSG 9035 = statute mile).
            GIS_ControlScale.Units = GisUtils.CSUnitsList.ByEPSG(9035)
        End Sub

        ''' <summary>
        ''' Releases geocoding and routing objects when the form loses focus.
        ''' layerRoute is owned by the viewer and freed automatically.
        ''' </summary>
        Private Sub WinForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Leave
            layerRoute = Nothing
            geoObj = Nothing
            rtrObj = Nothing
        End Sub

        ''' <summary>
        ''' Classifies a road segment as highway (link type 0) or local road (link type 1).
        ''' Called by <see cref="TGIS_ShortestPath"/> for every arc during graph construction.
        ''' </summary>
        ''' <remarks>
        ''' In the ActiveX binding, the event signature differs slightly from the NDK:
        ''' the shape is passed as <c>TGIS_ShapeArc</c> and the type as a ByRef Integer
        ''' rather than through an event-args object.
        '''
        ''' MTFCC codes "S1400" and above identify local/residential streets.
        ''' Codes below "S1400" are interstates, US routes, state routes, etc.
        ''' </remarks>
        Private Sub doLinkType(Shape As TGIS_ShapeArc, ByRef Type As Integer)
            If Not (Shape.GetField("MTFCC")?.ToString().CompareTo("S1400") < 0) Then
                ' MTFCC >= "S1400" → local/residential road
                Type = 1
            Else
                ' MTFCC < "S1400" → highway or major road
                Type = 0
            End If
        End Sub

        ''' <summary>
        ''' Handles the Find Route button click.
        ''' Geocodes both addresses, computes the shortest path between them,
        ''' and displays the route as a red line with turn-by-turn directions.
        ''' </summary>
        ''' <remarks>
        ''' Cost modifiers derived from the highway/local-road sliders are applied
        ''' before routing.  The formula maps slider values 1–10 to multipliers
        ''' 1.0–~0.09 (lower = cheaper = preferred by the router).
        '''
        ''' The <c>Compass</c> property encodes turn direction: 0 = straight,
        ''' 1..3 = right (slight/right/sharp), 4 = U-turn, -1..-3 = left, -4 = left U-turn.
        ''' Consecutive steps on the same named street are suppressed.
        ''' </remarks>
        Private Sub btnRoute_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRoute.Click
            Dim i As Integer
            Dim shp As TGIS_Shape
            Dim res As Integer
            Dim pt_a As TGIS_Point
            Dim pt_b As TGIS_Point
            Dim ang As String
            Dim oldnam As String

            ' Apply slider-derived cost modifiers.
            ' CostModifiers(0) = highway cost; CostModifiers(1) = local-road cost.
            rtrObj.CostModifiers(0) = 1 - 1 / 11.0 * trkHighways.Value
            rtrObj.CostModifiers(1) = 1 - 1 / 11.0 * trkSmallRoads.Value

            ' Geocode the start address.
            ' Parse() returns the number of matching candidates; refine if found.
            res = geoObj.Parse(edtAddrFrom.Text, True, True)
            If res > 0 Then
                edtAddrFrom.Text = geoObj.Query(0)
            Else
                edtAddrFrom.AppendText(" ???")
            End If

            ' Abort if the start address could not be resolved.
            If res <= 0 Then
                Return
            End If
            pt_a = geoObj.Point(0)   ' Geocoded start coordinate

            ' Geocode the end address.
            res = geoObj.Parse(edtAddrTo.Text, True, True)
            If res > 0 Then
                edtAddrTo.Text = geoObj.Query(0)
            Else
                edtAddrTo.AppendText(" ???")
            End If

            ' Abort if the end address could not be resolved.
            If res <= 0 Then
                Return
            End If
            pt_b = geoObj.Point(0)   ' Geocoded end coordinate

            ' UpdateTheData refreshes the graph; Find() computes the shortest path.
            ' Unproject converts viewer display coordinates to the layer's CS.
            rtrObj.UpdateTheData()
            rtrObj.Find(layerRoute.Unproject(pt_a), layerRoute.Unproject(pt_b))

            memRoute.Clear()
            oldnam = "#$@3eqewe"  ' Sentinel to detect the first direction step

            ' Build turn-by-turn directions text.
            ang = ""
            i = 0
            Do While i < rtrObj.ItemsCount
                ' Map the numeric Compass value to a readable direction abbreviation.
                Select Case rtrObj.Items(i).Compass
                    Case 0  : ang = "FWD  "
                    Case 1  : ang = "RIGHT"   ' Slight right
                    Case 2  : ang = "RIGHT"   ' Right
                    Case 3  : ang = "RIGHT"   ' Sharp right
                    Case 4  : ang = "BACK "   ' U-turn
                    Case -1 : ang = "LEFT "   ' Slight left
                    Case -2 : ang = "LEFT "   ' Left
                    Case -3 : ang = "LEFT "   ' Sharp left
                    Case -4 : ang = "BACK "   ' Left U-turn
                End Select

                ' Suppress consecutive items on the same named street.
                If oldnam = rtrObj.Items(i).Name Then
                    GoTo Continue1
                End If
                oldnam = rtrObj.Items(i).Name

                memRoute.AppendText(ang & " " & rtrObj.Items(i).Name & Constants.vbCrLf)
Continue1:
                i += 1
            Loop

            ' Clear the route display layer and repopulate with the new path.
            layerRoute.RevertAll()

            ' Copy each route segment from the source layer into the display layer.
            i = 0
            Do While i < rtrObj.ItemsCount
                shp = rtrObj.Items(i).Layer.GetShape(rtrObj.Items(i).Uid)
                If shp Is Nothing Then
                    GoTo Continue2
                End If
                layerRoute.AddShape(shp)
                If i = 0 Then
                    layerRoute.Extent = shp.Extent  ' Seed the bounding extent
                End If
Continue2:
                i += 1
            Loop

            ' Place a green point marker at the start position (pt_a).
            shp = layerRoute.CreateShape(TGIS_ShapeType.Point)
            shp.Lock(TGIS_Lock.Extent)
            shp.AddPart()
            shp.AddPoint(pt_a)
            shp.Params.Marker.Color = (New TGIS_Color).Green
            shp.Unlock()

            ' Place a red (default) point marker at the end position (pt_b).
            shp = layerRoute.CreateShape(TGIS_ShapeType.Point)
            shp.Lock(TGIS_Lock.Extent)
            shp.AddPart()
            shp.AddPoint(pt_b)
            shp.Unlock()

            ' Zoom to the route bounding box, pulling back slightly (0.7×) for context.
            GIS.Lock()
            GIS.VisibleExtent = layerRoute.Extent
            GIS.Zoom = 0.7 * GIS.Zoom
            GIS.Unlock()
        End Sub

        ''' <summary>
        ''' Handles the Find Address button click.
        ''' Geocodes the address typed in <c>edtAddrFrom</c> and highlights all
        ''' matching street segments on the map with a green point marker at each
        ''' geocoded coordinate.
        ''' </summary>
        ''' <remarks>
        ''' <c>Parse()</c> returns the number of matching candidates.  Each candidate
        ''' provides a <c>Query</c> (canonical name), a <c>Uid</c> (primary segment ID),
        ''' and a <c>UidEx</c> (complementary opposite-side segment ID).
        ''' A green marker is placed at <c>Point(i)</c>, the interpolated coordinate
        ''' for the house number along the matched segment.
        ''' </remarks>
        Private Sub btnResolve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnResolve.Click
            Dim i As Integer
            Dim r As Integer
            Dim shp As TGIS_Shape

            If geoObj Is Nothing Then
                Return
            End If

            ' Clear any previously displayed route or address highlights.
            layerRoute.RevertAll()

            ' Parse returns the number of matches; r is the last valid index.
            ' Append "???" if no match was found.
            r = geoObj.Parse(edtAddrFrom.Text, True, True) - 1
            If r <= 0 Then
                edtAddrFrom.AppendText(" ???")
            End If

            i = 0
            Do While i <= r
                ' Refine the text box to the canonical name of each matched street.
                edtAddrFrom.Text = geoObj.Query(i)
                Application.DoEvents()  ' Allow the UI to refresh between results

                ' Add the primary matching segment to the route display layer.
                shp = layerSrc.GetShape(geoObj.Uid(i))
                layerRoute.AddShape(shp)

                ' Seed the bounding extent from the first matched segment.
                If i = 0 Then
                    layerRoute.Extent = shp.ProjectedExtent
                End If

                ' Add the complementary (opposite-side) segment if available.
                shp = layerSrc.GetShape(geoObj.UidEx(i))
                If Not shp Is Nothing Then
                    layerRoute.AddShape(shp)
                End If

                ' Place a green marker at the interpolated geocoded coordinate.
                ' FromCS converts from the source layer's CS to the route layer's CS.
                shp = layerRoute.CreateShape(TGIS_ShapeType.Point)
                shp.Lock(TGIS_Lock.Extent)
                shp.AddPart()
                shp.AddPoint(layerRoute.CS.FromCS(layerSrc.CS, geoObj.Point(i)))
                shp.Params.Marker.Color = (New TGIS_Color).Green
                shp.Unlock()
                i += 1
            Loop

            ' Zoom to the matched results with a slight zoom-out factor for context.
            GIS.Lock()
            GIS.VisibleExtent = layerRoute.ProjectedExtent
            GIS.Zoom = 0.7 * GIS.Zoom
            GIS.Unlock()
        End Sub
    End Class
End Namespace
