' =============================================================================
' Locate sample — demonstrates feature identification and location by finding
' shapes at the cursor position using spatial queries and coordinate conversion
' (ActiveX/COM wrapper edition).
'
' What the sample shows:
'   - Loading a polygon shapefile (California Counties) into the GIS viewer
'   - Feature location: finding which shape is at a given position
'   - Screen-to-map coordinate conversion via ScreenToMap
'   - Spatial tolerance: pixel screen tolerance for easier feature selection
'   - GIS.Locate: queries features at a geographic position
'   - Shape attributes: retrieving field values from selected features
'   - Shape flashing: visual feedback highlighting the selected shape
'   - Dynamic status bar: real-time display of feature attributes
'   - Toolbar with Full Extent, Zoom In/Out navigation buttons
'
' ActiveX-specific implementation details:
'   - Using AxTGIS_ViewerWnd (ActiveX wrapper control, not WinForms.TGIS_ViewerWnd)
'   - GIS.Items.Item(i) instead of standard indexer (COM compatibility)
'   - MouseDown event from AxHost with COM-specific event argument types
'   - GisUtils instance object instead of static TGIS_Utils class
'
' Key TatukGIS API concepts shown here:
'   TGIS_ViewerWnd              - main visual map control (ActiveX COM version)
'   TGIS_ViewerWnd.ScreenToMap  - convert screen pixel to geographic coordinate
'   TGIS_ViewerWnd.Locate       - find topmost shape at location with tolerance
'   TGIS_Shape                  - geographic feature with field access
'   TGIS_Shape.Flash()          - briefly highlight shape for visual feedback
'   OnMouseMove / OnMouseDown   - user interaction event handling
'   TGIS_ControlLegend          - layer list/legend panel
' =============================================================================

Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS_XDK11

Namespace Locate
    Public Class WinForm
        Inherits System.Windows.Forms.Form
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer
        Private WithEvents toolBar1 As System.Windows.Forms.ToolBar
        Private btnFullExtent As System.Windows.Forms.ToolBarButton
        Private btnZoomIn As System.Windows.Forms.ToolBarButton
        Private btnZoomOut As System.Windows.Forms.ToolBarButton
        Private WithEvents GIS As AxTatukGIS_XDK11.AxTGIS_ViewerWnd
        Private statusBar1 As System.Windows.Forms.StatusBar
        Private imageList1 As System.Windows.Forms.ImageList

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
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WinForm))
            Me.toolBar1 = New System.Windows.Forms.ToolBar()
            Me.btnFullExtent = New System.Windows.Forms.ToolBarButton()
            Me.btnZoomIn = New System.Windows.Forms.ToolBarButton()
            Me.btnZoomOut = New System.Windows.Forms.ToolBarButton()
            Me.imageList1 = New System.Windows.Forms.ImageList(Me.components)
            Me.GIS = New AxTatukGIS_XDK11.AxTGIS_ViewerWnd()
            Me.statusBar1 = New System.Windows.Forms.StatusBar()
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'toolBar1
            '
            Me.toolBar1.AccessibleDescription = ""
            Me.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
            Me.toolBar1.AutoSize = False
            Me.toolBar1.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.btnFullExtent, Me.btnZoomIn, Me.btnZoomOut})
            Me.toolBar1.ButtonSize = New System.Drawing.Size(23, 22)
            Me.toolBar1.DropDownArrows = True
            Me.toolBar1.ImageList = Me.imageList1
            Me.toolBar1.Location = New System.Drawing.Point(0, 0)
            Me.toolBar1.Name = "toolBar1"
            Me.toolBar1.ShowToolTips = True
            Me.toolBar1.Size = New System.Drawing.Size(597, 24)
            Me.toolBar1.TabIndex = 0
            '
            'btnFullExtent
            '
            Me.btnFullExtent.ImageIndex = 0
            Me.btnFullExtent.Name = "btnFullExtent"
            Me.btnFullExtent.ToolTipText = "FullExtent"
            '
            'btnZoomIn
            '
            Me.btnZoomIn.ImageIndex = 1
            Me.btnZoomIn.Name = "btnZoomIn"
            Me.btnZoomIn.ToolTipText = "ZooIn"
            '
            'btnZoomOut
            '
            Me.btnZoomOut.ImageIndex = 2
            Me.btnZoomOut.Name = "btnZoomOut"
            Me.btnZoomOut.ToolTipText = "ZoomOut"
            '
            'imageList1
            '
            Me.imageList1.ImageStream = CType(resources.GetObject("imageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.imageList1.TransparentColor = System.Drawing.Color.Fuchsia
            Me.imageList1.Images.SetKeyName(0, "")
            Me.imageList1.Images.SetKeyName(1, "")
            Me.imageList1.Images.SetKeyName(2, "")
            '
            'GIS
            '
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.Enabled = True
            Me.GIS.Location = New System.Drawing.Point(0, 24)
            Me.GIS.Name = "GIS"
            Me.GIS.OcxState = CType(resources.GetObject("GIS.OcxState"), System.Windows.Forms.AxHost.State)
            Me.GIS.Size = New System.Drawing.Size(597, 423)
            Me.GIS.TabIndex = 1
            '
            'statusBar1
            '
            Me.statusBar1.Location = New System.Drawing.Point(0, 447)
            Me.statusBar1.Name = "statusBar1"
            Me.statusBar1.Size = New System.Drawing.Size(597, 19)
            Me.statusBar1.TabIndex = 2
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(597, 466)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.statusBar1)
            Me.Controls.Add(Me.toolBar1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Locate"
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).EndInit()
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

        ''' <summary>Loads the California counties shapefile into the viewer for interaction.</summary>
        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            '' open the counties layer; counties are polygons with a "name" field
            GIS.Open(GisUtils.GisSamplesDataDirDownload() & "\World\Countries\USA\States\California\Counties.SHP")
        End Sub

        ''' <summary>Locates and flashes the shape beneath the mouse click.</summary>
        Private Sub GIS_MouseDown(ByVal sender As Object, ByVal e As AxTatukGIS_XDK11.ITGIS_ViewerWndEvents_MouseDownEvent) Handles GIS.MouseDownEvent
            Dim ptg As TGIS_Point
            Dim shp As TGIS_Shape

            '' skip if map is empty or currently painting
            If GIS.IsEmpty Then
                Return
            End If
            If GIS.InPaint Then
                Return
            End If

            '' convert screen coordinates to map coordinates
            ptg = GIS.ScreenToMap(GisUtils.Point(e.x, e.y))
            '' use Locate to find any shape at this position (with 5-pixel tolerance for easier selection)
            shp = CType(GIS.Locate(ptg, 5 / GIS.Zoom), TGIS_Shape)
            '' if a shape was found, flash it to provide visual feedback to the user
            If Not shp Is Nothing Then
                shp.Flash()
            End If
        End Sub

        ''' <summary>Locates shapes under the cursor and displays the county name in the status bar.</summary>
        Private Sub GIS_MouseMove(ByVal sender As Object, ByVal e As AxTatukGIS_XDK11.ITGIS_ViewerWndEvents_MouseMoveEvent) Handles GIS.MouseMoveEvent
            Dim ptg As TGIS_Point
            Dim shp As TGIS_Shape

            '' skip if map is empty or currently painting
            If GIS.IsEmpty Then
                Return
            End If
            If GIS.InPaint Then
                Return
            End If

            '' convert screen coordinates to map space
            ptg = GIS.ScreenToMap(GisUtils.Point(e.x, e.y))
            '' locate the shape at the current cursor position
            shp = CType(GIS.Locate(ptg, 5 / GIS.Zoom), TGIS_Shape)
            '' if a shape exists, display its name field in the status bar; otherwise clear it
            If shp Is Nothing Then
                statusBar1.Text = ""
            Else
                statusBar1.Text = shp.GetField("name").ToString()
            End If
        End Sub

        ''' <summary>Handles toolbar button clicks for zoom and full extent operations.</summary>
        Private Sub toolBar1_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles toolBar1.ButtonClick
            Select Case toolBar1.Buttons.IndexOf(e.Button)
                Case 0
                    '' button 0: reset map to full extent
                    GIS.FullExtent()
                Case 1
                    '' button 1: zoom in (increase zoom 2x)
                    GIS.Zoom = GIS.Zoom * 2
                Case 2
                    '' button 2: zoom out (decrease zoom 2x)
                    GIS.Zoom = GIS.Zoom / 2
            End Select
        End Sub

        ''' <summary>Changes the cursor to a hand pointer when hovering over toolbar buttons for better UX.</summary>
        Private Sub toolBar1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles toolBar1.MouseMove
            Dim p As Point = New Point(e.X, e.Y)

            '' show hand cursor if mouse is over any of the three toolbar buttons
            If toolBar1.Buttons(0).Rectangle.Contains(p) OrElse toolBar1.Buttons(1).Rectangle.Contains(p) OrElse toolBar1.Buttons(2).Rectangle.Contains(p) Then
                toolBar1.Cursor = Cursors.Hand
            Else
                '' restore default cursor when not over buttons
                toolBar1.Cursor = Cursors.Default
            End If
        End Sub
    End Class
End Namespace
