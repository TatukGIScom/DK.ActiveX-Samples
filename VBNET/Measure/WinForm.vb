' =============================================================================
' This source code is a part of TatukGIS Developer Kernel.
' =============================================================================
' Measure sample — demonstrates interactive distance and area measurement on a map (ActiveX/COM edition).
'
' What the sample shows:
'   - Creating an in-memory TGIS_LayerVector to hold temporary measurement shapes
'   - Using AxTGIS_ViewerWnd.Editor to create and track polyline and polygon shapes
'   - Responding to EditorChangeEvent for live measurement updates
'   - Using TGIS_CSUnits.AsLinear and AsAreal for human-readable output formatting
'   - Polyline distance measurement with geodetic accuracy
'   - Polygon area measurement with coordinate system awareness
'   - Toggling between Line (distance) and Polygon (area) measurement modes
'   - Real-time display of measurements as user places vertices
'   - Clear button to reset and start new measurement
'
' ActiveX/COM-specific details:
'   - GIS is AxTGIS_ViewerWnd (ActiveX wrapper, not managed control)
'   - Event signatures use COM-typed arguments (CreateShape_3 instead of CreateShape)
'   - MouseDownEvent uses COM mouse event args instead of standard WinForms
'   - Same functional approach as NDK .NET but with ActiveX interop layer
'
' Key TatukGIS API concepts shown here:
'   TGIS_ViewerWnd (via AxTGIS_ViewerWnd) - main visual map control
'   TGIS_ViewerWnd.Editor                 - in-place shape creation and editing
'   TGIS_LayerVector                      - in-memory measurement shape layer
'   TGIS_Shape                            - polyline/polygon measurement geometry
'   TGIS_CSUnits                          - unit formatting and conversion
'   EditorChangeEvent                     - live measurement update trigger
'   TGIS_ViewerMode                       - interaction modes (Select, Edit, Drag)
'   EPSG 904201                           - metric unit set for geodetic calculations
'   EPSG 4326                             - WGS-84 geographic coordinate system
' =============================================================================

Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS_XDK11
Imports AxTatukGIS_XDK11

Namespace Measure
    ''' <summary>
    ''' Main application form for the Measure sample (ActiveX edition).
    ''' Hosts the TatukGIS ActiveX viewer and provides controls to measure
    ''' line lengths and polygon areas directly on the map.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form

        ' Required designer variable.
        Private components As System.ComponentModel.Container = Nothing

        ' Classic StatusBar used in the ActiveX edition (not StatusStrip).
        Private statusBar1 As System.Windows.Forms.StatusBar
        Private statusBarPanel1 As System.Windows.Forms.StatusBarPanel

        ''' <summary>
        ''' In-memory vector layer used to hold the temporary measurement shape.
        ''' No file is backed; shapes exist only for the current session.
        ''' </summary>
        Private ll As TGIS_LayerVector

        ' Mode flags — exactly one may be True at a time.
        Private isLine As Boolean    ' Polyline (distance) measurement is active
        Private isPolygon As Boolean ' Polygon (area) measurement is active

        Private panel1 As Panel  ' Toolbar panel: holds the three action buttons
        Private panel2 As Panel  ' Results panel: holds the length and area displays

        ''' <summary>
        ''' The TatukGIS ActiveX viewer control, wrapped by AxHost for WinForms.
        ''' WithEvents enables the Handles keyword for COM event subscriptions.
        ''' </summary>
        Private WithEvents GIS As AxTGIS_ViewerWnd

        Private tbArea As TextBox    ' Displays the measured area (polygon mode only)
        Private tbLength As TextBox  ' Displays the measured length / perimeter
        Private lblArea As Label
        Private lblLength As Label

        ' Buttons declared WithEvents so the Handles clause can be used directly.
        Private WithEvents btnLine As Button    ' Start polyline measurement
        Private WithEvents btnPolygon As Button ' Start polygon measurement
        Private WithEvents btnClear As Button   ' Clear measurement and return to drag mode

        ''' <summary>
        ''' Unit formatter used to convert raw SI values (metres / m²) into
        ''' labelled strings.  Initialised to EPSG 904201 (metric).
        ''' </summary>
        Private unit As TGIS_CSUnits

        Public Sub New()
            InitializeComponent()
        End Sub

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If components IsNot Nothing Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub

        ' Designer-generated initialisation — do not modify manually.
        Private Sub InitializeComponent()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WinForm))
            Me.statusBar1 = New System.Windows.Forms.StatusBar()
            Me.statusBarPanel1 = New System.Windows.Forms.StatusBarPanel()
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.btnPolygon = New System.Windows.Forms.Button()
            Me.btnClear = New System.Windows.Forms.Button()
            Me.btnLine = New System.Windows.Forms.Button()
            Me.panel2 = New System.Windows.Forms.Panel()
            Me.tbArea = New System.Windows.Forms.TextBox()
            Me.tbLength = New System.Windows.Forms.TextBox()
            Me.lblArea = New System.Windows.Forms.Label()
            Me.lblLength = New System.Windows.Forms.Label()
            Me.GIS = New AxTatukGIS_XDK11.AxTGIS_ViewerWnd()
            CType(Me.statusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.panel1.SuspendLayout()
            Me.panel2.SuspendLayout()
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'statusBar1
            '
            Me.statusBar1.Location = New System.Drawing.Point(0, 447)
            Me.statusBar1.Name = "statusBar1"
            Me.statusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.statusBarPanel1})
            Me.statusBar1.ShowPanels = True
            Me.statusBar1.Size = New System.Drawing.Size(686, 19)
            Me.statusBar1.TabIndex = 0
            '
            'statusBarPanel1
            '
            Me.statusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
            Me.statusBarPanel1.Name = "statusBarPanel1"
            Me.statusBarPanel1.Text = "Use left mouse button to measure"
            Me.statusBarPanel1.Width = 669
            '
            'panel1
            '
            Me.panel1.Controls.Add(Me.btnPolygon)
            Me.panel1.Controls.Add(Me.btnClear)
            Me.panel1.Controls.Add(Me.btnLine)
            Me.panel1.Location = New System.Drawing.Point(12, 12)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(662, 27)
            Me.panel1.TabIndex = 0
            '
            'btnPolygon
            '
            Me.btnPolygon.Location = New System.Drawing.Point(84, 0)
            Me.btnPolygon.Name = "btnPolygon"
            Me.btnPolygon.Size = New System.Drawing.Size(75, 23)
            Me.btnPolygon.TabIndex = 7
            Me.btnPolygon.Text = "By polygon"
            Me.btnPolygon.UseVisualStyleBackColor = True
            '
            'btnClear
            '
            Me.btnClear.Location = New System.Drawing.Point(165, 0)
            Me.btnClear.Name = "btnClear"
            Me.btnClear.Size = New System.Drawing.Size(75, 23)
            Me.btnClear.TabIndex = 6
            Me.btnClear.Text = "Clear"
            Me.btnClear.UseVisualStyleBackColor = True
            '
            'btnLine
            '
            Me.btnLine.Location = New System.Drawing.Point(3, 0)
            Me.btnLine.Name = "btnLine"
            Me.btnLine.Size = New System.Drawing.Size(75, 23)
            Me.btnLine.TabIndex = 5
            Me.btnLine.Text = "By line"
            Me.btnLine.UseVisualStyleBackColor = True
            '
            'panel2
            '
            Me.panel2.Controls.Add(Me.tbArea)
            Me.panel2.Controls.Add(Me.tbLength)
            Me.panel2.Controls.Add(Me.lblArea)
            Me.panel2.Controls.Add(Me.lblLength)
            Me.panel2.Location = New System.Drawing.Point(474, 45)
            Me.panel2.Name = "panel2"
            Me.panel2.Size = New System.Drawing.Size(200, 396)
            Me.panel2.TabIndex = 1
            '
            'tbArea
            '
            Me.tbArea.Location = New System.Drawing.Point(6, 99)
            Me.tbArea.Name = "tbArea"
            Me.tbArea.Size = New System.Drawing.Size(191, 20)
            Me.tbArea.TabIndex = 3
            '
            'tbLength
            '
            Me.tbLength.Location = New System.Drawing.Point(6, 32)
            Me.tbLength.Name = "tbLength"
            Me.tbLength.Size = New System.Drawing.Size(191, 20)
            Me.tbLength.TabIndex = 2
            '
            'lblArea
            '
            Me.lblArea.AutoSize = True
            Me.lblArea.Location = New System.Drawing.Point(3, 83)
            Me.lblArea.Name = "lblArea"
            Me.lblArea.Size = New System.Drawing.Size(32, 13)
            Me.lblArea.TabIndex = 1
            Me.lblArea.Text = "Area:"
            '
            'lblLength
            '
            Me.lblLength.AutoSize = True
            Me.lblLength.Location = New System.Drawing.Point(3, 16)
            Me.lblLength.Name = "lblLength"
            Me.lblLength.Size = New System.Drawing.Size(43, 13)
            Me.lblLength.TabIndex = 0
            Me.lblLength.Text = "Length:"
            '
            'GIS
            '
            Me.GIS.Enabled = True
            Me.GIS.Location = New System.Drawing.Point(15, 45)
            Me.GIS.Name = "GIS"
            Me.GIS.OcxState = CType(resources.GetObject("GIS.OcxState"), System.Windows.Forms.AxHost.State)
            Me.GIS.Size = New System.Drawing.Size(453, 396)
            Me.GIS.TabIndex = 2
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.BackColor = System.Drawing.Color.White
            Me.ClientSize = New System.Drawing.Size(686, 466)
            Me.Controls.Add(Me.panel1)
            Me.Controls.Add(Me.panel2)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.statusBar1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Measure"
            CType(Me.statusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.panel1.ResumeLayout(False)
            Me.panel2.ResumeLayout(False)
            Me.panel2.PerformLayout()
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
        End Sub

        <STAThread>
        Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New WinForm())
        End Sub

        ' A shared TGIS_Utils instance used for path helpers and unit-list access.
        ' The ActiveX edition instantiates TGIS_Utils as a COM object rather than
        ' using shared (static) methods as in the .NET edition.
        Dim GisUtils As New TGIS_Utils()

        ''' <summary>
        ''' Initialises the map viewer: loads the world basemap, creates the
        ''' in-memory measurement layer, and configures the shape editor.
        ''' </summary>
        Private Sub WinForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            ' Lock suspends rendering while we make multiple changes.
            GIS.Lock()

            ' Load a world outline shapefile as the background basemap.
            GIS.Open(GisUtils.GisSamplesDataDirDownload() & "\World\WorldDCW\world.shp")

            ' Create an in-memory vector layer for the measurement shape.
            ll = New TGIS_LayerVector()
            ll.Params.Line.Color = (New TGIS_Color).Red  ' Red for high visibility
            ll.Params.Line.Width = 25                    ' Thick line for easy reading

            ' Assign WGS-84 geographic CS (EPSG 4326).
            ' With a geographic CS, LengthCS() and AreaCS() return geodetically
            ' correct values in metres / square metres.
            ll.SetCSByEPSG(4326)

            GIS.Add(ll)

            ' Restrict scrolling to the initial world extent.
            GIS.RestrictedExtent = GIS.Extent

            GIS.Unlock()

            ' EPSG 904201 = TatukGIS standard metric unit formatter (km / km²).
            unit = GisUtils.CSUnitsList.ByEPSG(904201)

            isLine = False
            isPolygon = False

            ' Widen the rubber-band preview line for better visibility.
            GIS.Editor.EditingLinesStyle.PenWidth = 10

            ' AfterActivePoint draws a live preview segment from the last placed
            ' vertex to the current cursor position.
            GIS.Editor.Mode = TGIS_EditorMode.AfterActivePoint
        End Sub

        ''' <summary>
        ''' Prepares the viewer for polygon area measurement.
        ''' Discards any existing shape and sets GIS.Mode to Select so the
        ''' next click starts a new Polygon shape.
        ''' </summary>
        Private Sub btnPolygon_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPolygon.Click
            GIS.Editor.DeleteShape()
            GIS.Editor.EndEdit()
            tbArea.Text = ""
            tbLength.Text = ""
            ' Both perimeter and area will be displayed in this mode.
            isPolygon = True
            isLine = False
            GIS.Mode = TGIS_ViewerMode.[Select]
        End Sub

        ''' <summary>
        ''' Prepares the viewer for polyline distance measurement.
        ''' Discards any existing shape and sets GIS.Mode to Select so the
        ''' next click starts a new Arc (polyline) shape.
        ''' </summary>
        Private Sub btnLine_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLine.Click
            GIS.Editor.DeleteShape()
            GIS.Editor.EndEdit()
            tbArea.Text = ""
            tbLength.Text = ""
            ' Only the length field will be updated; area stays empty.
            isPolygon = False
            isLine = True
            GIS.Mode = TGIS_ViewerMode.[Select]
        End Sub

        ''' <summary>
        ''' Clears the current measurement shape, resets result fields,
        ''' and returns the viewer to drag/pan mode.
        ''' </summary>
        Private Sub btnClear_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClear.Click
            GIS.Editor.DeleteShape()
            GIS.Editor.EndEdit()
            tbArea.Text = ""
            tbLength.Text = ""
            GIS.Mode = TGIS_ViewerMode.Drag
        End Sub

        ''' <summary>
        ''' Fired by the editor every time a vertex is placed or the mouse moves
        ''' while editing.  Recomputes and displays the current length and, for
        ''' polygons, the enclosed area.
        ''' </summary>
        Private Sub GIS_EditorChanged(ByVal sender As Object, ByVal e As AxTatukGIS_XDK11.ITGIS_ViewerWndEvents_EditorChangeEventEvent) Handles GIS.EditorChangeEvent
            If GIS.Editor.CurrentShape IsNot Nothing Then
                If isLine Then
                    ' LengthCS() returns the geodetic cumulative length in metres.
                    ' AsLinear formats it to a string with the unit label.
                    tbLength.Text = unit.AsLinear((CType(GIS.Editor.CurrentShape, TGIS_Shape)).LengthCS(), True)
                ElseIf isPolygon Then
                    ' For a polygon, LengthCS() is the perimeter.
                    tbLength.Text = unit.AsLinear((CType(GIS.Editor.CurrentShape, TGIS_Shape)).LengthCS(), True)
                    ' AreaCS() returns the geodetic enclosed area in square metres.
                    ' "%s²" expands to the unit abbreviation + superscript 2.
                    tbArea.Text = unit.AsAreal((CType(GIS.Editor.CurrentShape, TGIS_Shape)).AreaCS(), True, "%s²")
                End If
            End If
        End Sub

        ''' <summary>
        ''' Handles the mouse-down event from the ActiveX control.
        ''' The COM event signature differs from standard WinForms: coordinates
        ''' are supplied as Integer fields on the event args struct (e.x, e.y).
        ''' GisUtils.Point() wraps them into the TGIS_Point-compatible TPoint
        ''' that ScreenToMap expects.
        '''
        ''' Creates the initial measurement shape on the first click; thereafter
        ''' the editor manages vertex placement in Edit mode.
        ''' </summary>
        Private Sub GIS_MouseDownEvent(sender As Object, e As AxTatukGIS_XDK11.ITGIS_ViewerWndEvents_MouseDownEvent) Handles GIS.MouseDownEvent
            Dim ptg As TGIS_Point

            ' If the editor is already active, do not create a second shape.
            If GIS.Mode = TGIS_ViewerMode.Edit Then Return

            ' Convert the screen pixel position to geographic map coordinates.
            ' GisUtils.Point wraps the integer COM coordinates into a TPoint.
            ptg = GIS.ScreenToMap(GisUtils.Point(e.x, e.y))

            If isLine Then
                ' CreateShape_3 is the ActiveX-COM overload of CreateShape that
                ' accepts (layer, startPoint, shapeType) — Arc = polyline.
                GIS.Editor.CreateShape_3(ll, ptg, TGIS_ShapeType.Arc)
            ElseIf isPolygon Then
                ' Polygon shape; both AreaCS() and LengthCS() are available.
                GIS.Editor.CreateShape_3(ll, ptg, TGIS_ShapeType.Polygon)
            End If

            ' Hand vertex control to the editor for all subsequent mouse events.
            GIS.Mode = TGIS_ViewerMode.Edit
        End Sub
    End Class
End Namespace
