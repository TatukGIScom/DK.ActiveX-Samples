'=============================================================================
' This source code is a part of TatukGIS Developer Kernel.
'=============================================================================
' AddLayer Sample - Demonstrates how to programmatically add vector layers
' to a TatukGIS map viewer.
'
' Key concepts illustrated:
'   - Creating a TGIS_LayerSHP instance directly (manual construction) and
'     adding it to the viewer via GIS.Add.
'   - Using TGIS_Utils.GisCreateLayer as a convenience factory that resolves
'     the correct layer class from the file extension automatically.
'   - Setting visual rendering parameters on a layer (area fill colour, line
'     width, line outline width, line colour) through the Params property tree.
'   - Suppressing automatic .ttkgp config-file loading with UseConfig = False
'     so that the layer always starts with the explicitly assigned params.
'   - Fitting the viewport to all loaded layers with GIS.FullExtent().
'   - Switching the viewer interaction mode between Drag (pan) and Select.
'   - Zooming programmatically by multiplying or dividing the current Zoom value.
'
' Note: This variant uses TatukGIS_XDK11 (ActiveX/COM wrapper), while .NET
' variants use TatukGIS.NDK. The API and layer model are equivalent; only the
' host technology and namespace differ.
'
' Data: DCW (Digital Chart of the World) Shapefiles for Poland, supplied
' via the TatukGIS sample data directory.
'=============================================================================

Option Strict Off
Option Explicit On

Imports TatukGIS_XDK11   ' ActiveX/COM wrapper: TGIS_LayerSHP, TGIS_Color, TGIS_Utils, TGIS_ViewerMode

Friend Class Form1
    Inherits System.Windows.Forms.Form

#Region "Windows Form Designer generated code "
    ''' <summary>
    ''' Initialises the form using the Designer-generated component layout.
    ''' </summary>
    Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub

    ''' <summary>Clean up any resources being used.</summary>
    Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
        If Disposing Then
            If Not components Is Nothing Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(Disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    Public ToolTip1 As System.Windows.Forms.ToolTip
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.

    ' -------------------------------------------------------------------------
    ' Designer-managed fields – layout is configured in InitializeComponent().
    ' -------------------------------------------------------------------------
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents ToolBar1 As System.Windows.Forms.ToolBar
    Friend WithEvents ToolBarButton1 As System.Windows.Forms.ToolBarButton   ' Full Extent button
    Friend WithEvents ToolBarButton2 As System.Windows.Forms.ToolBarButton   ' Zoom In button
    Friend WithEvents ToolBarButton3 As System.Windows.Forms.ToolBarButton   ' Zoom Out button
    ''' <summary>Checkbox that toggles between Drag (pan) and Select interaction modes.</summary>
    Friend WithEvents CheckDrag As System.Windows.Forms.CheckBox
    Friend WithEvents ToolBarButton4 As System.Windows.Forms.ToolBarButton   ' Separator
    ''' <summary>
    ''' The TatukGIS ActiveX viewer control.  All layers are added to this component.
    ''' AxTGIS_ViewerWnd is the COM-interop wrapper generated for the XDK11 OCX.
    ''' </summary>
    Friend WithEvents GIS As AxTatukGIS_XDK11.AxTGIS_ViewerWnd
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.ToolBar1 = New System.Windows.Forms.ToolBar()
        Me.ToolBarButton1 = New System.Windows.Forms.ToolBarButton()
        Me.ToolBarButton2 = New System.Windows.Forms.ToolBarButton()
        Me.ToolBarButton3 = New System.Windows.Forms.ToolBarButton()
        Me.ToolBarButton4 = New System.Windows.Forms.ToolBarButton()
        Me.CheckDrag = New System.Windows.Forms.CheckBox()
        Me.StatusBar1 = New System.Windows.Forms.StatusBar()
        Me.GIS = New AxTatukGIS_XDK11.AxTGIS_ViewerWnd()
        CType(Me.GIS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Magenta
        Me.ImageList1.Images.SetKeyName(0, "")
        Me.ImageList1.Images.SetKeyName(1, "")
        Me.ImageList1.Images.SetKeyName(2, "")
        '
        'ToolBar1
        '
        Me.ToolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
        Me.ToolBar1.AutoSize = False
        Me.ToolBar1.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.ToolBarButton1, Me.ToolBarButton2, Me.ToolBarButton3, Me.ToolBarButton4})
        Me.ToolBar1.DropDownArrows = True
        Me.ToolBar1.ImageList = Me.ImageList1
        Me.ToolBar1.Location = New System.Drawing.Point(0, 0)
        Me.ToolBar1.Name = "ToolBar1"
        Me.ToolBar1.ShowToolTips = True
        Me.ToolBar1.Size = New System.Drawing.Size(476, 24)
        Me.ToolBar1.TabIndex = 0
        '
        'ToolBarButton1
        '
        Me.ToolBarButton1.ImageIndex = 0
        Me.ToolBarButton1.Name = "ToolBarButton1"
        '
        'ToolBarButton2
        '
        Me.ToolBarButton2.ImageIndex = 1
        Me.ToolBarButton2.Name = "ToolBarButton2"
        '
        'ToolBarButton3
        '
        Me.ToolBarButton3.ImageIndex = 2
        Me.ToolBarButton3.Name = "ToolBarButton3"
        '
        'ToolBarButton4
        '
        Me.ToolBarButton4.Name = "ToolBarButton4"
        Me.ToolBarButton4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'CheckDrag
        '
        Me.CheckDrag.Location = New System.Drawing.Point(80, 6)
        Me.CheckDrag.Name = "CheckDrag"
        Me.CheckDrag.Size = New System.Drawing.Size(104, 16)
        Me.CheckDrag.TabIndex = 1
        Me.CheckDrag.Text = "Draging"
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 316)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Size = New System.Drawing.Size(476, 16)
        Me.StatusBar1.TabIndex = 2
        '
        'GIS
        '
        Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GIS.Enabled = True
        Me.GIS.Location = New System.Drawing.Point(0, 24)
        Me.GIS.Name = "GIS"
        Me.GIS.OcxState = CType(resources.GetObject("GIS.OcxState"), System.Windows.Forms.AxHost.State)
        Me.GIS.Size = New System.Drawing.Size(476, 292)
        Me.GIS.TabIndex = 3
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(476, 332)
        Me.Controls.Add(Me.GIS)
        Me.Controls.Add(Me.StatusBar1)
        Me.Controls.Add(Me.CheckDrag)
        Me.Controls.Add(Me.ToolBar1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "Form1"
        Me.Text = "TatukGIS Samples: AddLayer"
        CType(Me.GIS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region

    ' GisUtils provides helper methods such as GisSamplesDataDirDownload,
    ' which resolves the root path where TatukGIS sample datasets are stored.
    Dim GisUtils As New TGIS_Utils()

    ' -------------------------------------------------------------------------
    ' Standalone helper stubs – these are called from ToolBar1_ButtonClick
    ' (not wired as individual event handlers).
    ' -------------------------------------------------------------------------

    ''' <summary>
    ''' Resets the viewport so that all loaded layers fit inside the viewer window.
    ''' </summary>
    Private Sub ButtonFullExtent_Click()
        GIS.FullExtent()
    End Sub

    ''' <summary>
    ''' Doubles the current zoom level, shrinking the visible area by half.
    ''' </summary>
    Private Sub ButtonZoomIn_Click()
        GIS.Zoom = GIS.Zoom * 2
    End Sub

    ''' <summary>
    ''' Halves the current zoom level, doubling the visible area.
    ''' </summary>
    Private Sub ButtonZoomOut_Click()
        GIS.Zoom = GIS.Zoom / 2
    End Sub

    ''' <summary>
    ''' Toggles the viewer's active interaction mode.
    ''' <para>
    ''' TGIS_ViewerMode.Drag   – left-click and drag pans the map canvas.
    ''' </para>
    ''' <para>
    ''' TGIS_ViewerMode.Select – left-click picks the topmost feature under
    ''' the cursor.
    ''' </para>
    ''' </summary>
    Private Sub CheckDrag_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckDrag.CheckStateChanged
        If CheckDrag.CheckState Then
            GIS.Mode = TGIS_ViewerMode.Drag
        Else
            GIS.Mode = TGIS_ViewerMode.Select
        End If

    End Sub

    ''' <summary>
    ''' Handles the Form.Load event.  Creates and configures two Shapefile
    ''' layers – a country polygon layer and a rivers polyline layer – then
    ''' adds them to the GIS viewer and fits the viewport to their combined
    ''' extent.
    ''' </summary>
    Private Sub Form1_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Dim ll As TGIS_LayerSHP
        Dim c As New TGIS_Color()   ' Used to resolve named colour constants from the COM type

        ' --- Layer 1: Country outline (polygon / area layer) ---
        ' Construct the Shapefile layer directly.  The geometry type (polygon)
        ' is determined from the .shp file header when the layer is first accessed.
        ll = New TGIS_LayerSHP()

        ' GisSamplesDataDirDownload returns the path where TatukGIS sample
        ' datasets were installed or downloaded (configured by the DK installer).
        Dim p As String = GisUtils.GisSamplesDataDirDownload & "\World\Countries\Poland\DCW\country.shp"
        ll.Path = GisUtils.GisSamplesDataDirDownload & "\World\Countries\Poland\DCW\country.shp"

        ' A human-readable label used in legends and layer lists.
        ll.Name = "states"

        ' GIS.Add appends the layer to the internal stack.  Layers added earlier
        ' are rendered first (drawn at the bottom of the visual stack).
        GIS.Add(ll)

        ' --- Layer 2: Rivers (polyline layer) ---
        ' A second TGIS_LayerSHP instance is created directly here rather than
        ' through the GisCreateLayer factory, which is equally valid.
        ll = New TGIS_LayerSHP()
        ll.Path = GisUtils.GisSamplesDataDirDownload & "\World\Countries\Poland\DCW\lwaters.shp"
        ll.Name = "rivers"

        ' UseConfig = False prevents the DK from loading a previously saved
        ' .ttkgp configuration file, so the rendering parameters below
        ' always take effect regardless of any saved session state.
        ll.UseConfig = False

        ' OutlineWidth = 0 removes the contrasting halo drawn around lines,
        ' yielding a clean single-colour stroke.
        ll.Params.Line.OutlineWidth = 0

        ' Width is in screen pixels at the reference zoom level.
        ll.Params.Line.Width = 3

        ' c.Blue resolves the blue colour constant through the COM TGIS_Color object.
        ' (In the NDK variant this is the static TGIS_Color.Blue property.)
        ll.Params.Line.Color = c.Blue

        GIS.Add(ll)

        ' Zoom the viewport to the combined bounding box of all layers so the
        ' full map is visible immediately after load.
        GIS.FullExtent()

    End Sub

    ''' <summary>
    ''' Dispatches toolbar button clicks by position index within the toolbar.
    ''' Index 0 = Full Extent, 1 = Zoom In, 2 = Zoom Out.
    ''' </summary>
    Private Sub ToolBar1_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles ToolBar1.ButtonClick
        Select Case ToolBar1.Buttons.IndexOf(e.Button)
            Case 0
                ' Reset the viewport to show all loaded layers at once.
                GIS.FullExtent()
            Case 1
                ' Double the zoom level – the visible area shrinks by half.
                GIS.Zoom = GIS.Zoom * 2
            Case 2
                ' Halve the zoom level – the visible area doubles.
                GIS.Zoom = GIS.Zoom / 2
        End Select
    End Sub

End Class
