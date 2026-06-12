' =============================================================================
' Buffers2 - TatukGIS DK ActiveX (VB.NET) sample
' =============================================================================
'
' Demonstrates an advanced buffer workflow that combines TGIS_Topology.MakeBuffer
' with a spatial intersection query to highlight all features that overlap the
' computed buffer polygon, using the TatukGIS Developer Kernel exposed as an
' ActiveX (XDK11) control.
'
' The ActiveX edition wraps the GIS viewer in an AxTGIS_ViewerWnd OCX host.
' Most API concepts are identical to the NDK .NET edition, but some method
' names differ:
'   - FindFirst_3(extent, filter) replaces FindFirst(extent, filter)
'   - FindFirst_2(extent)          replaces FindFirst(extent)
'   - GisUtils.GisWholeWorld()     replaces TGIS_Utils.GisWholeWorld()
'   - GisUtils.GisCreateLayer()    replaces TGIS_Utils.GisCreateLayer()
'   - RevertAll                    replaces RevertShapes
'   - TGIS_Color instance methods  (e.g. c.Yellow) replace static properties
'
' What the sample shows:
'   - Loading California counties via GisUtils.GisCreateLayer
'   - Creating a semi-transparent buffer overlay layer (TGIS_LayerVector)
'   - Finding Merced County by attribute filter using FindFirst_3
'   - Computing a planar buffer with TGIS_Topology.MakeBuffer (distance =
'     trackBar1.Value / 100 degrees)
'   - Performing a two-stage spatial intersection query:
'       Stage 1 - FindFirst_2(buf.Extent): bounding-box pre-filter
'       Stage 2 - buf.IsCommonPoint(tmp): precise geometric overlap test
'   - Marking intersecting counties blue and listing their names in textBox1
'   - Using a Timer (250 ms) to debounce rapid slider movements
'
' Key TatukGIS XDK11 types used:
'   AxTGIS_ViewerWnd   - ActiveX-hosted map viewer
'   TGIS_LayerVector   - in-memory or file-backed vector layer
'   TGIS_LayerAbstract - base type returned by GisCreateLayer
'   TGIS_Topology      - spatial operations engine
'   TGIS_Shape         - a single geographic feature
'   TGIS_Utils / ITGIS_Utils - utility helpers (data paths, GisCreateLayer, etc.)
' =============================================================================

Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS_XDK11

Namespace Buffers2
    ''' <summary>
    ''' Main form for the Buffers2 (ActiveX) sample.
    '''
    ''' Loads California county data, computes a buffer around Merced County at a
    ''' distance controlled by a slider, then highlights every county that intersects
    ''' the buffer and lists their names in a text box.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form
        ''' <summary>Required designer variable.</summary>
        Private components As System.ComponentModel.IContainer
        ''' <summary>Minus toolbar button: decreases buffer distance by 25 steps.</summary>
        Private WithEvents toolBar1 As System.Windows.Forms.ToolBar
        Private btnMinus As System.Windows.Forms.ToolBarButton
        Private imageList1 As System.Windows.Forms.ImageList
        Private statusBar1 As System.Windows.Forms.StatusBar           ' shows distance in km
        Private GIS As AxTatukGIS_XDK11.AxTGIS_ViewerWnd              ' ActiveX map viewer
        Private textBox1 As System.Windows.Forms.TextBox               ' lists intersecting counties
        Private panel1 As System.Windows.Forms.Panel
        Private panel2 As System.Windows.Forms.Panel
        Private panel3 As System.Windows.Forms.Panel
        Private panel4 As System.Windows.Forms.Panel
        Private toolBar2 As System.Windows.Forms.ToolBar
        ''' <summary>Buffer distance slider (0..200; divide by 100 = 0..2 degrees).</summary>
        Private WithEvents trackBar1 As System.Windows.Forms.TrackBar
        Private panel5 As System.Windows.Forms.Panel
        ''' <summary>Plus toolbar button: increases buffer distance by 25 steps.</summary>
        Private WithEvents toolBar3 As System.Windows.Forms.ToolBar
        Private btnPlus As System.Windows.Forms.ToolBarButton
        ''' <summary>Debounce timer: 250 ms delay before running the buffer query.</summary>
        Private WithEvents timer1 As System.Windows.Forms.Timer
        Private statusBarPanel1 As System.Windows.Forms.StatusBarPanel

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
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WinForm))
            Me.toolBar1 = New System.Windows.Forms.ToolBar()
            Me.btnMinus = New System.Windows.Forms.ToolBarButton()
            Me.imageList1 = New System.Windows.Forms.ImageList(Me.components)
            Me.statusBar1 = New System.Windows.Forms.StatusBar()
            Me.statusBarPanel1 = New System.Windows.Forms.StatusBarPanel()
            Me.GIS = New AxTatukGIS_XDK11.AxTGIS_ViewerWnd()
            Me.textBox1 = New System.Windows.Forms.TextBox()
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.panel5 = New System.Windows.Forms.Panel()
            Me.toolBar3 = New System.Windows.Forms.ToolBar()
            Me.btnPlus = New System.Windows.Forms.ToolBarButton()
            Me.panel3 = New System.Windows.Forms.Panel()
            Me.panel4 = New System.Windows.Forms.Panel()
            Me.trackBar1 = New System.Windows.Forms.TrackBar()
            Me.toolBar2 = New System.Windows.Forms.ToolBar()
            Me.panel2 = New System.Windows.Forms.Panel()
            Me.timer1 = New System.Windows.Forms.Timer(Me.components)
            CType(Me.statusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.panel1.SuspendLayout()
            Me.panel5.SuspendLayout()
            Me.panel3.SuspendLayout()
            Me.panel4.SuspendLayout()
            CType(Me.trackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.panel2.SuspendLayout()
            Me.SuspendLayout()
            '
            'toolBar1
            '
            Me.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
            Me.toolBar1.AutoSize = False
            Me.toolBar1.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.btnMinus})
            Me.toolBar1.ButtonSize = New System.Drawing.Size(23, 23)
            Me.toolBar1.Dock = System.Windows.Forms.DockStyle.None
            Me.toolBar1.DropDownArrows = True
            Me.toolBar1.ImageList = Me.imageList1
            Me.toolBar1.Location = New System.Drawing.Point(0, 0)
            Me.toolBar1.Name = "toolBar1"
            Me.toolBar1.ShowToolTips = True
            Me.toolBar1.Size = New System.Drawing.Size(23, 25)
            Me.toolBar1.TabIndex = 0
            '
            'btnMinus
            '
            Me.btnMinus.ImageIndex = 0
            Me.btnMinus.Name = "btnMinus"
            '
            'imageList1
            '
            Me.imageList1.ImageStream = CType(resources.GetObject("imageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.imageList1.TransparentColor = System.Drawing.Color.Fuchsia
            Me.imageList1.Images.SetKeyName(0, "")
            Me.imageList1.Images.SetKeyName(1, "")
            '
            'statusBar1
            '
            Me.statusBar1.Location = New System.Drawing.Point(0, 452)
            Me.statusBar1.Name = "statusBar1"
            Me.statusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.statusBarPanel1})
            Me.statusBar1.ShowPanels = True
            Me.statusBar1.Size = New System.Drawing.Size(592, 19)
            Me.statusBar1.TabIndex = 1
            '
            'statusBarPanel1
            '
            Me.statusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
            Me.statusBarPanel1.Name = "statusBarPanel1"
            Me.statusBarPanel1.Width = 575
            '
            'GIS  (ActiveX OCX control)
            '
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.Enabled = True
            Me.GIS.Location = New System.Drawing.Point(0, 25)
            Me.GIS.Name = "GIS"
            Me.GIS.OcxState = CType(resources.GetObject("GIS.OcxState"), System.Windows.Forms.AxHost.State)
            Me.GIS.Size = New System.Drawing.Size(477, 427)
            Me.GIS.TabIndex = 3
            '
            'textBox1  (read-only list of intersecting county names)
            '
            Me.textBox1.BackColor = System.Drawing.SystemColors.ControlLightLight
            Me.textBox1.Dock = System.Windows.Forms.DockStyle.Right
            Me.textBox1.Location = New System.Drawing.Point(477, 25)
            Me.textBox1.Multiline = True
            Me.textBox1.Name = "textBox1"
            Me.textBox1.ReadOnly = True
            Me.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
            Me.textBox1.Size = New System.Drawing.Size(115, 427)
            Me.textBox1.TabIndex = 2
            '
            'panel1
            '
            Me.panel1.Controls.Add(Me.panel5)
            Me.panel1.Controls.Add(Me.panel3)
            Me.panel1.Controls.Add(Me.panel2)
            Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.panel1.Location = New System.Drawing.Point(0, 0)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(592, 25)
            Me.panel1.TabIndex = 4
            '
            'panel5
            '
            Me.panel5.Controls.Add(Me.toolBar3)
            Me.panel5.Dock = System.Windows.Forms.DockStyle.Fill
            Me.panel5.Location = New System.Drawing.Point(264, 0)
            Me.panel5.Name = "panel5"
            Me.panel5.Size = New System.Drawing.Size(328, 25)
            Me.panel5.TabIndex = 2
            '
            'toolBar3
            '
            Me.toolBar3.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
            Me.toolBar3.AutoSize = False
            Me.toolBar3.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.btnPlus})
            Me.toolBar3.DropDownArrows = True
            Me.toolBar3.ImageList = Me.imageList1
            Me.toolBar3.Location = New System.Drawing.Point(0, 0)
            Me.toolBar3.Name = "toolBar3"
            Me.toolBar3.ShowToolTips = True
            Me.toolBar3.Size = New System.Drawing.Size(328, 25)
            Me.toolBar3.TabIndex = 0
            '
            'btnPlus
            '
            Me.btnPlus.ImageIndex = 1
            Me.btnPlus.Name = "btnPlus"
            '
            'panel3
            '
            Me.panel3.Controls.Add(Me.panel4)
            Me.panel3.Dock = System.Windows.Forms.DockStyle.Left
            Me.panel3.Location = New System.Drawing.Point(23, 0)
            Me.panel3.Name = "panel3"
            Me.panel3.Size = New System.Drawing.Size(241, 25)
            Me.panel3.TabIndex = 1
            '
            'panel4
            '
            Me.panel4.Controls.Add(Me.trackBar1)
            Me.panel4.Controls.Add(Me.toolBar2)
            Me.panel4.Location = New System.Drawing.Point(0, 0)
            Me.panel4.Name = "panel4"
            Me.panel4.Size = New System.Drawing.Size(241, 25)
            Me.panel4.TabIndex = 0
            '
            'trackBar1  (0..200 buffer distance; divide by 100 = 0..2 degrees)
            '
            Me.trackBar1.Location = New System.Drawing.Point(0, 2)
            Me.trackBar1.Maximum = 200
            Me.trackBar1.Name = "trackBar1"
            Me.trackBar1.Size = New System.Drawing.Size(241, 45)
            Me.trackBar1.TabIndex = 1
            '
            'toolBar2
            '
            Me.toolBar2.DropDownArrows = True
            Me.toolBar2.Location = New System.Drawing.Point(0, 0)
            Me.toolBar2.Name = "toolBar2"
            Me.toolBar2.ShowToolTips = True
            Me.toolBar2.Size = New System.Drawing.Size(241, 42)
            Me.toolBar2.TabIndex = 0
            '
            'panel2
            '
            Me.panel2.Controls.Add(Me.toolBar1)
            Me.panel2.Dock = System.Windows.Forms.DockStyle.Left
            Me.panel2.Location = New System.Drawing.Point(0, 0)
            Me.panel2.Name = "panel2"
            Me.panel2.Size = New System.Drawing.Size(23, 25)
            Me.panel2.TabIndex = 0
            '
            'timer1  (250 ms debounce)
            '
            Me.timer1.Interval = 250
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(592, 471)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.textBox1)
            Me.Controls.Add(Me.panel1)
            Me.Controls.Add(Me.statusBar1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Buffers2"
            CType(Me.statusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).EndInit()
            Me.panel1.ResumeLayout(False)
            Me.panel5.ResumeLayout(False)
            Me.panel3.ResumeLayout(False)
            Me.panel4.ResumeLayout(False)
            Me.panel4.PerformLayout()
            CType(Me.trackBar1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.panel2.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
#End Region

        ''' <summary>
        ''' ITGIS_Utils instance used to call utility methods on the ActiveX interface.
        ''' In the ActiveX edition, utility helpers are accessed through an instance of
        ''' TGIS_Utils rather than through static/Shared methods as in the NDK edition.
        ''' </summary>
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

        ''' <summary>
        ''' Form load handler - opens the California counties shapefile and creates
        ''' the buffer overlay layer.
        '''
        ''' Steps:
        '''   1. Call GisUtils.GisCreateLayer to open the shapefile with the logical
        '''      name "counties" (used later by GIS.Get).
        '''   2. Add the county layer to the viewer directly (no Lock/Unlock in the
        '''      ActiveX edition for this sample).
        '''   3. Create an empty in-memory "buffer" overlay layer (40 % transparent,
        '''      yellow fill).
        '''   4. Zoom to the full data extent.
        ''' Note: TGIS_Color is instantiated as an object (c.Yellow) rather than via
        ''' static properties as in the NDK edition.
        ''' </summary>
        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim la As TGIS_LayerAbstract  ' file-backed county layer
            Dim lb As TGIS_LayerVector    ' in-memory buffer overlay layer
            Dim c As New TGIS_Color       ' colour helper (ActiveX uses instance properties)

            ' GisUtils.GisCreateLayer selects the correct layer class for the SHP format
            la = GisUtils.GisCreateLayer("counties", GisUtils.GisSamplesDataDirDownload() & "\World\Countries\USA\States\California\Counties.SHP")
            GIS.Add(la)

            ' Buffer overlay: 40 % transparent yellow so county boundaries remain visible
            lb = New TGIS_LayerVector()
            lb.Name = "buffer"
            lb.Transparency = 40
            lb.Params.Area.Color = c.Yellow
            GIS.Add(lb)

            GIS.FullExtent()
        End Sub

        ''' <summary>
        ''' Minus button handler: decrements the slider by 25 steps (clamped to minimum)
        ''' and immediately triggers a buffer recompute via timer1_Tick.
        ''' </summary>
        Private Sub toolBar1_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles toolBar1.ButtonClick
            Select Case toolBar1.Buttons.IndexOf(e.Button)
                Case 0
                    ' Clamp to the minimum to avoid going below 0
                    If trackBar1.Value > trackBar1.Minimum + 25 Then
                        trackBar1.Value -= 25
                        timer1_Tick(Me, e)
                    ElseIf trackBar1.Value > trackBar1.Minimum Then
                        trackBar1.Value = trackBar1.Minimum
                        timer1_Tick(Me, e)
                    End If
            End Select
        End Sub

        ''' <summary>
        ''' Plus button handler: increments the slider by 25 steps (clamped to maximum)
        ''' and immediately triggers a buffer recompute via timer1_Tick.
        ''' </summary>
        Private Sub toolBar3_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles toolBar3.ButtonClick
            Select Case toolBar3.Buttons.IndexOf(e.Button)
                Case 0
                    ' Clamp to the maximum to avoid exceeding 200
                    If trackBar1.Value < trackBar1.Maximum - 25 Then
                        trackBar1.Value += 25
                        timer1_Tick(Me, e)
                    ElseIf trackBar1.Value < trackBar1.Maximum Then
                        trackBar1.Value = trackBar1.Maximum
                        timer1_Tick(Me, e)
                    End If
            End Select
        End Sub

        ''' <summary>
        ''' Core buffer and intersection logic, fired by the debounce timer.
        '''
        ''' The timer is disabled immediately at entry so rapid slider movement
        ''' does not queue multiple overlapping queries.
        '''
        ''' Key ActiveX differences from the NDK edition:
        '''   - FindFirst_3(extent, filter) is used instead of FindFirst(extent, filter)
        '''   - FindFirst_2(extent)          is used instead of FindFirst(extent)
        '''   - GisUtils.GisWholeWorld()     is used instead of TGIS_Utils.GisWholeWorld()
        '''   - RevertAll                    is used instead of RevertShapes
        '''   - TGIS_Color instance property (.Blue) rather than static TGIS_Color.Blue
        '''
        ''' Algorithm:
        '''   1. Retrieve "counties" and "buffer" layers by logical name.
        '''   2. Use FindFirst_3 with GisUtils.GisWholeWorld() and attribute filter
        '''      "NAME='Merced'" to locate the source county.
        '''   3. Call TGIS_Topology.MakeBuffer: distance = trackBar1.Value / 100 degrees.
        '''   4. Clear the buffer overlay (RevertAll) and store the new polygon.
        '''   5. Two-stage spatial query:
        '''        FindFirst_2(buf.Extent) - bounding-box pre-filter
        '''        buf.IsCommonPoint(tmp)  - precise geometric intersection test
        '''   6. Matching counties are made editable, coloured blue, and listed.
        '''   7. GIS.InvalidateWholeMap redraws in the Finally block.
        ''' </summary>
        Private Sub timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles timer1.Tick
            Dim ll As TGIS_LayerVector   ' the county source layer
            Dim lb As TGIS_LayerVector   ' the buffer overlay layer
            Dim shp As TGIS_Shape        ' the Merced county shape (buffer source)
            Dim tmp As TGIS_Shape        ' iterator shape in FindFirst/FindNext loop
            Dim buf As TGIS_Shape        ' the computed buffer polygon stored in lb
            Dim tpl As TGIS_Topology     ' topology engine

            ' Disable the timer so it does not fire again while we process
            timer1.Enabled = False

            Try
                ' Retrieve layers by their logical names
                ll = CType(GIS.Get("counties"), TGIS_LayerVector)
                If ll Is Nothing Then
                    Return
                End If

                lb = CType(GIS.Get("buffer"), TGIS_LayerVector)
                If lb Is Nothing Then
                    Return
                End If

                ' FindFirst_3 (ActiveX overload) accepts extent + attribute filter.
                ' GisUtils.GisWholeWorld() ensures no shape is excluded spatially.
                shp = ll.FindFirst_3(GisUtils.GisWholeWorld(), "NAME='Merced'")
                If shp Is Nothing Then
                    Return
                End If

                tpl = New TGIS_Topology()
                Try
                    lb.RevertAll()  ' discard any previously computed buffer polygon
                    ' Divide by 100 to convert the integer slider value to degrees
                    tmp = tpl.MakeBuffer(shp, trackBar1.Value / 100)
                    If Not tmp Is Nothing Then
                        ' AddShape copies geometry into the overlay and returns the
                        ' stored reference (buf) used for the intersection query below.
                        buf = lb.AddShape(tmp)
                        tmp = Nothing
                    Else
                        buf = Nothing
                    End If
                Finally
                    tpl = Nothing
                End Try

                ' ── Intersection query ────────────────────────────────────────────
                If buf Is Nothing Then
                    Return
                End If

                ' Re-fetch county layer (AddShape may have invalidated the reference)
                ll = CType(GIS.Get("counties"), TGIS_LayerVector)
                ' IgnoreShapeParams = False lets per-shape colour overrides take effect
                ll.IgnoreShapeParams = False
                If ll Is Nothing Then
                    Return
                End If
                ll.RevertAll()   ' reset per-shape colour overrides from the previous run
                textBox1.Clear()

                ' Stage 1: bounding-box pre-filter (FindFirst_2 is the extent-only overload)
                tmp = ll.FindFirst_2(buf.Extent)
                Do While Not tmp Is Nothing
                    ' Stage 2: precise geometric intersection test
                    If buf.IsCommonPoint(tmp) Then
                        ' MakeEditable returns a writable copy so Params.Area.Color can be set
                        tmp = tmp.MakeEditable()
                        textBox1.AppendText(tmp.GetField("name").ToString() & Constants.vbCrLf)
                        ' In the ActiveX edition TGIS_Color colours are instance properties
                        tmp.Params.Area.Color = (New TGIS_Color()).Blue
                    End If
                    tmp = ll.FindNext()  ' advance to the next bounding-box candidate
                Loop

            Finally
                ' Always refresh the map, even if an early Return occurred above
                GIS.InvalidateWholeMap()
            End Try
        End Sub

        ''' <summary>
        ''' Debounces rapid slider movement using the timer.
        '''
        ''' Each scroll event resets the timer so that the buffer computation
        ''' (timer1_Tick) only fires once the user pauses for 250 ms.  The current
        ''' distance value is shown in the status bar immediately for responsiveness.
        ''' Note: the ActiveX edition uses statusBar1.Panels(0) rather than
        ''' statusBar1.Items(0) as in the NDK edition.
        ''' </summary>
        Private Sub trackBar1_Scroll(ByVal sender As Object, ByVal e As System.EventArgs) Handles trackBar1.Scroll
            timer1.Enabled = False
            ' Show the current slider value in the status bar while dragging
            statusBar1.Panels(0).Text = trackBar1.Value.ToString() & " km"
            timer1.Enabled = True
        End Sub
    End Class
End Namespace
