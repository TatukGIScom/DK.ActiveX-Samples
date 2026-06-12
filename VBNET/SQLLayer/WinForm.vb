'=============================================================================
' This source code is a part of TatukGIS Developer Kernel.
'=============================================================================
'
' SQLLayer Sample - VB.NET / ActiveX (TatukGIS XDK11)
' ====================================================
' Demonstrates how to open and display an SQL layer using the TatukGIS
' ActiveX (XDK11) COM wrapper.
'
' An SQL layer (*.ttkls file) is a virtual layer definition that describes how
' to retrieve spatial data from a relational database (e.g. PostgreSQL/PostGIS,
' SQL Server, SQLite, Oracle Spatial) using a SQL query.  The .ttkls file
' contains the connection string, the SQL statement, and field mappings; it
' can be opened just like any other layer format supported by the DK.
'
' This variant uses the ActiveX/COM interop binding (AxTatukGIS_XDK11) instead
' of the managed NDK assembly; the API surface is equivalent but method calls
' go through the COM dispatch layer.
'
' Key concepts shown:
'   - Opening a .ttkls project with AxTGIS_ViewerWnd.Open (via COM)
'   - Fitting the view to the full data extent with FullExtent
'   - Switching the viewer between Zoom and Drag interaction modes
'
' To adapt this sample to your own database:
'   Edit gistest.ttkls to supply your connection string and SQL query.
'   The file can also be opened in the TatukGIS Editor for visual configuration.
'=============================================================================

Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS_XDK11  ' TatukGIS ActiveX/COM interop assembly

Namespace SQLLayer
    ''' <summary>
    ''' Main application form for the SQLLayer ActiveX sample.
    ''' Hosts an <see cref="AxTatukGIS_XDK11.AxTGIS_ViewerWnd"/> (COM-hosted viewer)
    ''' that renders data loaded from a .ttkls SQL layer definition file.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form

        ' ------------------------------------------------------------------ '
        '  Designer-managed fields (do not modify names/types)                '
        ' ------------------------------------------------------------------ '

        ''' <summary>Required designer variable.</summary>
        Private components As System.ComponentModel.IContainer

        Private WithEvents toolBar1 As System.Windows.Forms.ToolBar    ' Main toolbar (legacy ToolBar control)
        Private btnFullExtent As System.Windows.Forms.ToolBarButton    ' Zoom to full extent
        Private toolBarButton1 As System.Windows.Forms.ToolBarButton   ' Visual separator
        Private btnZoom As System.Windows.Forms.ToolBarButton          ' Activate zoom mode (toggle)
        Private btnDrag As System.Windows.Forms.ToolBarButton          ' Activate drag/pan mode (toggle)
        Private statusBar1 As System.Windows.Forms.StatusBar           ' Status bar
        Private imageList1 As System.Windows.Forms.ImageList           ' Toolbar icon images
        Private GIS As AxTatukGIS_XDK11.AxTGIS_ViewerWnd              ' TatukGIS ActiveX viewer

        ''' <summary>
        ''' Initialises the form and its child controls.
        ''' </summary>
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
            Me.toolBar1.DropDownArrows = True
            Me.toolBar1.ImageList = Me.imageList1
            Me.toolBar1.Location = New System.Drawing.Point(0, 0)
            Me.toolBar1.Name = "toolBar1"
            Me.toolBar1.ShowToolTips = True
            Me.toolBar1.Size = New System.Drawing.Size(587, 24)
            Me.toolBar1.TabIndex = 0
            '
            'btnFullExtent
            '
            Me.btnFullExtent.ImageIndex = 0
            Me.btnFullExtent.Name = "btnFullExtent"
            Me.btnFullExtent.ToolTipText = "Full Extent"
            '
            'toolBarButton1
            '
            Me.toolBarButton1.Name = "toolBarButton1"
            Me.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
            '
            'btnZoom
            '
            Me.btnZoom.ImageIndex = 1
            Me.btnZoom.Name = "btnZoom"
            Me.btnZoom.Pushed = True
            Me.btnZoom.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton
            Me.btnZoom.ToolTipText = "Zoom Mode"
            '
            'btnDrag
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
            Me.statusBar1.Size = New System.Drawing.Size(587, 19)
            Me.statusBar1.TabIndex = 1
            '
            'GIS  — ActiveX viewer hosted inside a WinForms AxHost wrapper
            '
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.Enabled = True
            Me.GIS.Location = New System.Drawing.Point(0, 24)
            Me.GIS.Name = "GIS"
            Me.GIS.OcxState = CType(resources.GetObject("GIS.OcxState"), System.Windows.Forms.AxHost.State)
            Me.GIS.Size = New System.Drawing.Size(587, 423)
            Me.GIS.TabIndex = 2
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(587, 466)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.statusBar1)
            Me.Controls.Add(Me.toolBar1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - SQL Layer"
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
#End Region

        ' TGIS_Utils instance used to resolve the sample data directory path
        ' via the COM API (GisSamplesDataDirDownload is an instance method on the
        ' ActiveX binding, unlike the shared method in the managed NDK).
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
        ''' Opens the SQL layer project file and zooms to the full data extent.
        ''' </summary>
        ''' <remarks>
        ''' <c>GisUtils.GisSamplesDataDirDownload()</c> resolves to the TatukGIS
        ''' sample data directory.  The gistest.ttkls file inside
        ''' \Samples\SQLLayers\ holds the database connection string and SQL query
        ''' that define the virtual layer.  Edit that file to point at your own
        ''' database before running.
        ''' </remarks>
        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            ' Open the SQL layer definition file via the COM/ActiveX viewer.
            ' The AxTGIS_ViewerWnd.Open method accepts file paths to any
            ' supported layer or project format, including .ttkls files.
            GIS.Open(GisUtils.GisSamplesDataDirDownload() & "\Samples\SQLLayers\gistest.ttkls")

            ' Zoom the view so that all loaded features are visible.
            GIS.FullExtent()
        End Sub

        ''' <summary>
        ''' Changes the toolbar cursor to a hand pointer when over an active button.
        ''' </summary>
        Private Sub toolBar1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles toolBar1.MouseMove
            Dim p As Point = New Point(e.X, e.Y)

            If toolBar1.Buttons(0).Rectangle.Contains(p) OrElse toolBar1.Buttons(2).Rectangle.Contains(p) OrElse toolBar1.Buttons(3).Rectangle.Contains(p) Then
                toolBar1.Cursor = Cursors.Hand
            Else
                toolBar1.Cursor = Cursors.Default
            End If
        End Sub

        ''' <summary>
        ''' Handles toolbar button clicks; dispatches to the appropriate viewer action.
        ''' Uses the legacy ToolBarButtonClickEventArgs (ToolBar control).
        ''' </summary>
        Private Sub toolBar1_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles toolBar1.ButtonClick
            Select Case toolBar1.Buttons.IndexOf(e.Button)
                Case 0
                    ' btnFullExtent — zoom to the full data extent
                    GIS.FullExtent()
                Case 2
                    ' btnZoom — activate rubber-band zoom interaction mode
                    btnDrag.Pushed = False
                    GIS.Mode = TGIS_ViewerMode.Zoom
                Case 3
                    ' btnDrag — activate pan/drag interaction mode
                    btnZoom.Pushed = False
                    GIS.Mode = TGIS_ViewerMode.Drag
            End Select
        End Sub
    End Class
End Namespace
