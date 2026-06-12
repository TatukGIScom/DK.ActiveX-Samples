' Buffers1 sample — demonstrates spatial buffer operations for proximity analysis (ActiveX/COM edition).
'
' What the sample shows:
'   - Loading vector shapefiles into the ActiveX GIS viewer
'   - Creating in-memory vector layer to hold buffer results
'   - Letting user click on shapes to select them as buffer source
'   - Using TGIS_Topology.MakeBuffer to compute buffer polygons around shapes
'   - Interactive buffer distance control via trackbar (range -50 to +50 km)
'   - Negative buffer values produce inward/erosion buffers instead of expansion
'   - Adding result shapes to buffer layer with automatic view refresh
'   - Hit-testing with ActiveX event system for click detection
'   - Converting pixel coordinates to map coordinates with ScreenToMap
'   - Clearing previous buffer results with RevertAll before adding new ones
'
' ActiveX/COM-specific details:
'   - AxTGIS_ViewerWnd is the ActiveX wrapper for the TatukGIS viewer control
'   - API functionality similar to NDK .NET but some method signatures differ
'   - RevertAll instead of RevertShapes in COM edition
'   - MouseDownEvent signature differs from NDK .NET MouseDown
'   - TGIS_Utils accessed as instance through COM, not static class
'
' Key TatukGIS API concepts shown here:
'   TGIS_ViewerWnd (via AxTGIS_ViewerWnd) - main visual map control
'   TGIS_LayerVector        - in-memory or file-backed vector layer
'   TGIS_Topology           - spatial operations (MakeBuffer, Intersection, Union, etc.)
'   TGIS_Shape              - individual geographic feature (point, line, polygon)
'   TGIS_Topology.MakeBuffer() - compute proximity buffer around shape
'   GIS.Locate()            - hit-test at point to find topmost shape
'   GIS.ScreenToMap()       - convert screen pixels to geographic coordinates
' =============================================================================

Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS_XDK11

Namespace Buffers1
    ''' <summary>
    ''' Main form for the Buffers1 (ActiveX) sample.
    ''' Loads a topology shapefile, lets the user click a shape to select it,
    ''' then renders a buffer polygon at a distance set by the slider.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer
        Private imageList1 As System.Windows.Forms.ImageList
        Private statusBar1 As System.Windows.Forms.StatusBar
        Private statusBarPanel1 As System.Windows.Forms.StatusBarPanel
        Private WithEvents GIS As AxTatukGIS_XDK11.AxTGIS_ViewerWnd  ' ActiveX map viewer
        ''' <summary>shp_id holds the Uid of the currently selected shape.</summary>
        Private shp_id As Integer
        Private panel1 As System.Windows.Forms.Panel
        Private WithEvents trackBar1 As System.Windows.Forms.TrackBar  ' -50..+50 km
        Private btnPlus As System.Windows.Forms.ToolBarButton          ' increment slider
        Private panel2 As System.Windows.Forms.Panel
        Private WithEvents toolBar1 As System.Windows.Forms.ToolBar
        Private btnMinus As System.Windows.Forms.ToolBarButton         ' decrement slider
        Private panel3 As System.Windows.Forms.Panel
        Private toolBar2 As System.Windows.Forms.ToolBar
        Private panel4 As System.Windows.Forms.Panel
        Private WithEvents toolBar3 As System.Windows.Forms.ToolBar

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
            Me.imageList1 = New System.Windows.Forms.ImageList(Me.components)
            Me.statusBar1 = New System.Windows.Forms.StatusBar()
            Me.statusBarPanel1 = New System.Windows.Forms.StatusBarPanel()
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.panel4 = New System.Windows.Forms.Panel()
            Me.toolBar3 = New System.Windows.Forms.ToolBar()
            Me.btnPlus = New System.Windows.Forms.ToolBarButton()
            Me.panel3 = New System.Windows.Forms.Panel()
            Me.trackBar1 = New System.Windows.Forms.TrackBar()
            Me.toolBar2 = New System.Windows.Forms.ToolBar()
            Me.panel2 = New System.Windows.Forms.Panel()
            Me.toolBar1 = New System.Windows.Forms.ToolBar()
            Me.btnMinus = New System.Windows.Forms.ToolBarButton()
            Me.GIS = New AxTatukGIS_XDK11.AxTGIS_ViewerWnd()
            CType(Me.statusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.panel1.SuspendLayout()
            Me.panel4.SuspendLayout()
            Me.panel3.SuspendLayout()
            CType(Me.trackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.panel2.SuspendLayout()
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
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
            Me.statusBar1.Location = New System.Drawing.Point(0, 453)
            Me.statusBar1.Name = "statusBar1"
            Me.statusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.statusBarPanel1})
            Me.statusBar1.ShowPanels = True
            Me.statusBar1.Size = New System.Drawing.Size(596, 19)
            Me.statusBar1.TabIndex = 1
            '
            'statusBarPanel1
            '
            Me.statusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
            Me.statusBarPanel1.Name = "statusBarPanel1"
            Me.statusBarPanel1.Text = "Click on shapes to choose one for buffer creation"
            Me.statusBarPanel1.Width = 579
            '
            'panel1
            '
            Me.panel1.Controls.Add(Me.panel4)
            Me.panel1.Controls.Add(Me.panel3)
            Me.panel1.Controls.Add(Me.panel2)
            Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.panel1.Location = New System.Drawing.Point(0, 0)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(596, 25)
            Me.panel1.TabIndex = 0
            '
            'panel4
            '
            Me.panel4.Controls.Add(Me.toolBar3)
            Me.panel4.Dock = System.Windows.Forms.DockStyle.Fill
            Me.panel4.Location = New System.Drawing.Point(264, 0)
            Me.panel4.Name = "panel4"
            Me.panel4.Size = New System.Drawing.Size(332, 25)
            Me.panel4.TabIndex = 2
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
            Me.toolBar3.Size = New System.Drawing.Size(332, 25)
            Me.toolBar3.TabIndex = 0
            '
            'btnPlus
            '
            Me.btnPlus.ImageIndex = 1
            Me.btnPlus.Name = "btnPlus"
            '
            'panel3
            '
            Me.panel3.Controls.Add(Me.trackBar1)
            Me.panel3.Controls.Add(Me.toolBar2)
            Me.panel3.Dock = System.Windows.Forms.DockStyle.Left
            Me.panel3.Location = New System.Drawing.Point(23, 0)
            Me.panel3.Name = "panel3"
            Me.panel3.Size = New System.Drawing.Size(241, 25)
            Me.panel3.TabIndex = 0
            Me.panel3.TabStop = True
            '
            'trackBar1
            '
            Me.trackBar1.AutoSize = False
            Me.trackBar1.Location = New System.Drawing.Point(0, 2)
            Me.trackBar1.Maximum = 50
            Me.trackBar1.Minimum = -50
            Me.trackBar1.Name = "trackBar1"
            Me.trackBar1.Size = New System.Drawing.Size(241, 23)
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
            'toolBar1
            '
            Me.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
            Me.toolBar1.AutoSize = False
            Me.toolBar1.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.btnMinus})
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
            'GIS  (ActiveX OCX control)
            '
            Me.GIS.BackColor = System.Drawing.SystemColors.ControlLightLight
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.Enabled = True
            Me.GIS.Location = New System.Drawing.Point(0, 25)
            Me.GIS.Name = "GIS"
            Me.GIS.OcxState = CType(resources.GetObject("GIS.OcxState"), System.Windows.Forms.AxHost.State)
            Me.GIS.Size = New System.Drawing.Size(596, 428)
            Me.GIS.TabIndex = 1
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(596, 472)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.panel1)
            Me.Controls.Add(Me.statusBar1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Buffers1"
            CType(Me.statusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.panel1.ResumeLayout(False)
            Me.panel4.ResumeLayout(False)
            Me.panel3.ResumeLayout(False)
            Me.panel3.PerformLayout()
            CType(Me.trackBar1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.panel2.ResumeLayout(False)
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
#End Region


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
        ''' ITGIS_Utils instance used to call utility methods on the ActiveX interface.
        ''' In the ActiveX edition, utility helpers are accessed through an instance
        ''' of TGIS_Utils rather than through static/Shared methods.
        ''' </summary>
        Private GisUtils As ITGIS_Utils = New TGIS_Utils()

        ''' <summary>
        ''' Initialises the map when the form loads.
        '''
        ''' Opens the topology sample shapefile, pre-selects shape Uid 2, creates
        ''' an empty in-memory "buffer" overlay layer (50 % transparent, red fill),
        ''' adds it to the viewer, and zooms to the full data extent.
        ''' Note: the ActiveX edition does not expose Lock/Unlock on the viewer,
        ''' so data is added directly.
        ''' </summary>
        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim lb As TGIS_LayerVector
            Dim c As New TGIS_Color()
            ' open a project
            GIS.Open(GisUtils.GisSamplesDataDirDownload() & "\Samples\Topology\topology.shp")
            shp_id = 2
            ' create a layer for buffer
            lb = New TGIS_LayerVector()
            lb.Name = "buffer"
            lb.Transparency = 50            ' 50 % transparent so source shapes remain visible
            lb.Params.Area.Color = c.Red
            GIS.Add(lb)
            GIS.FullExtent()
        End Sub

        ''' <summary>
        ''' Handles the ActiveX MouseDownEvent to select the buffer source shape.
        '''
        ''' The ActiveX event delivers coordinates as separate X/Y Integer arguments
        ''' (wrapped in ITGIS_ViewerWndEvents_MouseDownEvent) rather than a
        ''' MouseEventArgs object.  GisUtils.Point converts them to a TGIS_Point
        ''' value compatible with GIS.ScreenToMap.
        ''' </summary>
        Private Sub GIS_MouseDown(ByVal sender As Object, ByVal e As AxTatukGIS_XDK11.ITGIS_ViewerWndEvents_MouseDownEvent) Handles GIS.MouseDownEvent
            Dim ptg As TGIS_Point
            Dim shp As TGIS_Shape

            If GIS.IsEmpty Then
                Return
            End If
            If GIS.InPaint Then
                Return
            End If

            ' locate a shape after click
            ' GisUtils.Point wraps integer pixel coords into the TGIS_Point type
            ptg = GIS.ScreenToMap(GisUtils.Point(e.X, e.Y))
            shp = CType(GIS.Locate(ptg, 5 / GIS.Zoom), TGIS_Shape) ' 5 pixels precision
            ' remember id to use buffer on selected shape
            If Not shp Is Nothing Then
                shp_id = shp.Uid
                shp.Flash()  ' visual confirmation
            End If

        End Sub

        ''' <summary>
        ''' Recomputes the buffer polygon whenever the slider is moved.
        '''
        ''' trackBar1.Value is in kilometres (-50..+50); multiplied by 1000 to get
        ''' metres for MakeBuffer.  RevertAll clears the buffer layer before each
        ''' new result so the overlay always shows only the current buffer polygon.
        ''' </summary>
        Private Sub trackBar1_Scroll(ByVal sender As Object, ByVal e As System.EventArgs) Handles trackBar1.Scroll
            Dim ll As TGIS_LayerVector   ' source layer (index 0)
            Dim lb As TGIS_LayerVector   ' "buffer" overlay layer
            Dim shp As TGIS_Shape        ' shape being buffered
            Dim tmp As TGIS_Shape        ' temporary MakeBuffer result
            Dim tpl As TGIS_Topology     ' topology engine

            ' In the ActiveX edition, GIS.Items is accessed via Items.Item(index)
            ll = CType(GIS.Items.Item(0), TGIS_LayerVector)
            If ll Is Nothing Then
                Return
            End If

            lb = CType(GIS.Get("buffer"), TGIS_LayerVector)
            If lb Is Nothing Then
                Return
            End If

            shp = ll.GetShape(shp_id)
            If shp Is Nothing Then
                Return
            End If

            ' create a buffer using topology
            tpl = New TGIS_Topology()
            Try
                lb.RevertAll()  ' discard any previously computed buffer shape
                ' trackBar1.Value * 1000 converts km → metres
                tmp = tpl.MakeBuffer(shp, trackBar1.Value * 1000)
                If Not tmp Is Nothing Then
                    lb.AddShape(tmp)
                    tmp = Nothing
                End If
                ' check extents
                ll.RecalcExtent()
                lb.RecalcExtent()
                GIS.RecalcExtent()
                GIS.FullExtent()
            Finally
                tpl = Nothing
            End Try
        End Sub

        ''' <summary>
        ''' Handles the minus toolbar button: decrements the slider by 1 and triggers
        ''' a buffer recompute by calling trackBar1_Scroll directly.
        ''' </summary>
        Private Sub toolBar1_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles toolBar1.ButtonClick
            Select Case toolBar1.Buttons.IndexOf(e.Button)
                Case 0
                    ' change bar position and recalculate buffer
                    If trackBar1.Value > trackBar1.Minimum Then
                        trackBar1.Value -= 1
                        trackBar1_Scroll(Me, e)
                    End If
            End Select
        End Sub

        ''' <summary>
        ''' Handles the plus toolbar button: increments the slider by 1 and triggers
        ''' a buffer recompute by calling trackBar1_Scroll directly.
        ''' </summary>
        Private Sub toolBar3_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles toolBar3.ButtonClick
            Select Case toolBar3.Buttons.IndexOf(e.Button)
                Case 0
                    ' change bar position and recalculate buffer
                    If trackBar1.Value < trackBar1.Maximum Then
                        trackBar1.Value += 1
                        trackBar1_Scroll(Me, e)
                    End If
            End Select
        End Sub
    End Class
End Namespace
