'==============================================================================
' This source code is a part of TatukGIS Developer Kernel.
'==============================================================================
'
' Renderer sample (ActiveX / COM edition) — demonstrates how to load and
' display a TatukGIS project file that contains pre-configured custom
' rendering rules using the ActiveX control wrapper.
'
' Key concepts shown:
'   - Opening a .ttkproject file with the AxTGIS_ViewerWnd ActiveX control
'   - Switching the viewer interaction mode between Zoom and Drag using the
'     TGIS_ViewerMode enumeration exposed by the TatukGIS_XDK11 COM type library
'   - Restoring the full map extent with FullExtent()
'   - Using TGIS_Utils (instantiated as a COM object) to resolve the sample
'     data path via GisSamplesDataDirDownload
'
' The rendering definitions (symbol styles, color ramps, scale-dependent
' rules, etc.) are stored inside renderer.ttkproject.  This form simply loads
' that project and wires up toolbar buttons for map navigation.
'==============================================================================

Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS_XDK11   ' TatukGIS ActiveX / COM type library interop assembly

Namespace Renderer
    ''' <summary>
    ''' Main application form for the Renderer sample (ActiveX edition).
    ''' Hosts the AxTGIS_ViewerWnd ActiveX map control together with a
    ''' navigation toolbar and a status bar.  On load it opens the
    ''' pre-configured renderer project file.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form

        ' ---------------------------------------------------------------
        ' Designer-managed fields (do not rename — referenced by .resx)
        ' ---------------------------------------------------------------
        ''' <summary>Required designer variable.</summary>
        Private components As System.ComponentModel.IContainer
        Private WithEvents toolBar1 As System.Windows.Forms.ToolBar         ' Navigation toolbar
        Private btnFullExtent As System.Windows.Forms.ToolBarButton          ' Full-extent button
        Private toolBarButton1 As System.Windows.Forms.ToolBarButton         ' Toolbar separator
        Private btnZoom As System.Windows.Forms.ToolBarButton                ' Zoom-mode toggle button
        Private btnDrag As System.Windows.Forms.ToolBarButton                ' Drag/pan-mode toggle button
        Private GIS As AxTatukGIS_XDK11.AxTGIS_ViewerWnd                    ' ActiveX map viewer control
        Private statusBar1 As System.Windows.Forms.StatusBar                 ' Status bar
        Private imageList1 As System.Windows.Forms.ImageList                 ' Toolbar button icons

        ''' <summary>
        ''' Initialises the form components and gives the map viewer initial
        ''' keyboard focus so navigation shortcuts are available immediately.
        ''' </summary>
        Public Sub New()
            '
            ' Required for Windows Form Designer support
            '
            InitializeComponent()

            '
            ' TODO: Add any constructor code after InitializeComponent call
            '
            ' Give the ActiveX map control focus on start-up.
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
            Me.toolBarButton1 = New System.Windows.Forms.ToolBarButton()
            Me.btnZoom = New System.Windows.Forms.ToolBarButton()
            Me.btnDrag = New System.Windows.Forms.ToolBarButton()
            Me.imageList1 = New System.Windows.Forms.ImageList(Me.components)
            Me.statusBar1 = New System.Windows.Forms.StatusBar()
            Me.GIS = New AxTatukGIS_XDK11.AxTGIS_ViewerWnd()
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'toolBar1
            '
            Me.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
            Me.toolBar1.AutoSize = False
            Me.toolBar1.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.btnFullExtent, Me.toolBarButton1, Me.btnZoom, Me.btnDrag})
            Me.toolBar1.ButtonSize = New System.Drawing.Size(23, 22)
            Me.toolBar1.DropDownArrows = True
            Me.toolBar1.ImageList = Me.imageList1
            Me.toolBar1.Location = New System.Drawing.Point(0, 0)
            Me.toolBar1.Name = "toolBar1"
            Me.toolBar1.ShowToolTips = True
            Me.toolBar1.Size = New System.Drawing.Size(595, 24)
            Me.toolBar1.TabIndex = 0
            '
            'btnFullExtent
            '
            Me.btnFullExtent.ImageIndex = 0
            Me.btnFullExtent.Name = "btnFullExtent"
            Me.btnFullExtent.ToolTipText = "Full Extent"
            '
            'toolBarButton1 — separator between Full Extent and mode buttons
            '
            Me.toolBarButton1.Name = "toolBarButton1"
            Me.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
            '
            'btnZoom — toggle button for zoom interaction mode
            '
            Me.btnZoom.ImageIndex = 1
            Me.btnZoom.Name = "btnZoom"
            Me.btnZoom.Pushed = True
            Me.btnZoom.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton
            Me.btnZoom.ToolTipText = "Zoom Mode"
            '
            'btnDrag — toggle button for drag/pan interaction mode
            '
            Me.btnDrag.ImageIndex = 2
            Me.btnDrag.Name = "btnDrag"
            Me.btnDrag.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton
            Me.btnDrag.ToolTipText = "Drag Mode"
            '
            'imageList1
            '
            Me.imageList1.ImageStream = CType(resources.GetObject("imageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.imageList1.TransparentColor = System.Drawing.Color.Fuchsia
            Me.imageList1.Images.SetKeyName(0, "")
            Me.imageList1.Images.SetKeyName(1, "")
            Me.imageList1.Images.SetKeyName(2, "")
            '
            'statusBar1
            '
            Me.statusBar1.Location = New System.Drawing.Point(0, 447)
            Me.statusBar1.Name = "statusBar1"
            Me.statusBar1.ShowPanels = True
            Me.statusBar1.Size = New System.Drawing.Size(595, 19)
            Me.statusBar1.TabIndex = 2
            '
            'GIS — ActiveX TatukGIS map viewer (AxTGIS_ViewerWnd)
            '
            Me.GIS.BackColor = System.Drawing.SystemColors.Control
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.Enabled = True
            Me.GIS.Location = New System.Drawing.Point(0, 24)
            Me.GIS.Name = "GIS"
            Me.GIS.OcxState = CType(resources.GetObject("GIS.OcxState"), System.Windows.Forms.AxHost.State)
            Me.GIS.Size = New System.Drawing.Size(595, 423)
            Me.GIS.TabIndex = 1
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(595, 466)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.statusBar1)
            Me.Controls.Add(Me.toolBar1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Renderer"
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
#End Region

        ' GisUtils provides helper methods from the TatukGIS COM type library,
        ' such as GisSamplesDataDirDownload which resolves the sample data path.
        Dim GisUtils As New TGIS_Utils()

        ''' <summary>
        ''' Application entry point.
        ''' Enables visual styles and starts the Windows Forms message loop.
        ''' </summary>
        <STAThread>
        Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New WinForm())
        End Sub

        ''' <summary>
        ''' Handles the Form.Load event.
        ''' Opens the renderer project file so that all pre-configured layer
        ''' rendering rules are applied automatically.
        '''
        ''' GisUtils.GisSamplesDataDirDownload() (COM helper) returns the root
        ''' path of the downloaded TatukGIS sample dataset.
        ''' </summary>
        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            ' Open the pre-built renderer project; all layer styles are
            ' embedded in the .ttkproject XML file — no code-level styling needed.
            GIS.Open(GisUtils.GisSamplesDataDirDownload() & "\Samples\Projects\renderer.ttkproject")
        End Sub

        ''' <summary>
        ''' Handles toolbar ButtonClick events for the classic ToolBar control.
        ''' The button index within the Buttons collection identifies the action:
        '''   0 — Full Extent
        '''   2 — Zoom mode (TGIS_ViewerMode.Zoom)
        '''   3 — Drag mode (TGIS_ViewerMode.Drag)
        ''' Toggle-button state is managed manually to keep only one mode active.
        ''' </summary>
        Private Sub toolBar1_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles toolBar1.ButtonClick
            Select Case toolBar1.Buttons.IndexOf(e.Button)
                Case 0  ' btnFullExtent — zoom to fit all loaded layers
                    GIS.FullExtent()
                Case 2  ' btnZoom — enable rubber-band / scroll-wheel zoom
                    btnDrag.Pushed = False
                    GIS.Mode = TGIS_ViewerMode.Zoom
                Case 3  ' btnDrag — enable click-and-drag panning
                    btnZoom.Pushed = False
                    GIS.Mode = TGIS_ViewerMode.Drag
            End Select
        End Sub

        ''' <summary>
        ''' Changes the toolbar cursor to a hand when the pointer is over an
        ''' active button, providing a visual affordance for clickable items.
        ''' Buttons 0, 2, 3 are active; button 1 is the separator.
        ''' </summary>
        Private Sub toolBar1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles toolBar1.MouseMove
            Dim p As Point = New Point(e.X, e.Y)

            If toolBar1.Buttons(0).Rectangle.Contains(p) OrElse toolBar1.Buttons(2).Rectangle.Contains(p) OrElse toolBar1.Buttons(3).Rectangle.Contains(p) Then
                toolBar1.Cursor = Cursors.Hand
            Else
                toolBar1.Cursor = Cursors.Default
            End If
        End Sub
    End Class
End Namespace
