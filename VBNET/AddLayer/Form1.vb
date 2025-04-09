Option Strict Off
Option Explicit On

Imports TatukGIS_XDK11

Friend Class Form1
    Inherits System.Windows.Forms.Form
#Region "Windows Form Designer generated code "
    Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub
    'Form overrides dispose to clean up the component list.
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
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents ToolBar1 As System.Windows.Forms.ToolBar
    Friend WithEvents ToolBarButton1 As System.Windows.Forms.ToolBarButton
    Friend WithEvents ToolBarButton2 As System.Windows.Forms.ToolBarButton
    Friend WithEvents ToolBarButton3 As System.Windows.Forms.ToolBarButton
    Friend WithEvents CheckDrag As System.Windows.Forms.CheckBox
    Friend WithEvents ToolBarButton4 As System.Windows.Forms.ToolBarButton
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


    Dim GisUtils As New TGIS_Utils()

    Private Sub ButtonFullExtent_Click()
        GIS.FullExtent()
    End Sub

    Private Sub ButtonZoomIn_Click()
        GIS.Zoom = GIS.Zoom * 2
    End Sub

    Private Sub ButtonZoomOut_Click()
        GIS.Zoom = GIS.Zoom / 2
    End Sub

    Private Sub CheckDrag_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckDrag.CheckStateChanged
        If CheckDrag.CheckState Then
            GIS.Mode = TGIS_ViewerMode.Drag
        Else
            GIS.Mode = TGIS_ViewerMode.Select
        End If

    End Sub

    Private Sub Form1_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Dim ll As TGIS_LayerSHP
        Dim c As New TGIS_Color()

        ll = New TGIS_LayerSHP()
        Dim p As String = GisUtils.GisSamplesDataDirDownload & "\World\Countries\Poland\DCW\country.shp"
        ll.Path = GisUtils.GisSamplesDataDirDownload & "\World\Countries\Poland\DCW\country.shp"
        ll.Name = "states"

        GIS.Add(ll)

        ll = New TGIS_LayerSHP()
        ll.Path = GisUtils.GisSamplesDataDirDownload & "\World\Countries\Poland\DCW\lwaters.shp"
        ll.Name = "rivers"
        ll.UseConfig = False
        ll.Params.Line.OutlineWidth = 0
        ll.Params.Line.Width = 3
        ll.Params.Line.Color = c.Blue
        GIS.Add(ll)

        GIS.FullExtent()

    End Sub

    Private Sub ToolBar1_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles ToolBar1.ButtonClick
        Select Case ToolBar1.Buttons.IndexOf(e.Button)
            Case 0
                GIS.FullExtent()
            Case 1
                GIS.Zoom = GIS.Zoom * 2
            Case 2
                GIS.Zoom = GIS.Zoom / 2
        End Select
    End Sub

End Class
