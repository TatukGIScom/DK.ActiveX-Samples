Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS_XDK11

Namespace Grid
    ''' <summary>
    ''' Summary description for WinForm.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer
        Private panel1 As System.Windows.Forms.Panel
        Private WithEvents toolBar1 As System.Windows.Forms.ToolBar
        Private imageList1 As System.Windows.Forms.ImageList
        Private btnFullExtent As System.Windows.Forms.ToolBarButton
        Private toolBarButton1 As System.Windows.Forms.ToolBarButton
        Private btnZoom As System.Windows.Forms.ToolBarButton
        Private btnDrag As System.Windows.Forms.ToolBarButton
        Private toolBarButton2 As System.Windows.Forms.ToolBarButton
        Private WithEvents button1 As System.Windows.Forms.Button
        Private WithEvents button2 As System.Windows.Forms.Button
        Private WithEvents button3 As System.Windows.Forms.Button
        Private statusBar1 As System.Windows.Forms.StatusBar
        Private statusBarPanel1 As System.Windows.Forms.StatusBarPanel
        Private statusBarPanel2 As System.Windows.Forms.StatusBarPanel
        Private GIS_ControlLegend As AxTatukGIS_XDK11.AxTGIS_ControlLegend
        Friend WithEvents btnUserINI As System.Windows.Forms.Button
        Friend WithEvents btnShadow As System.Windows.Forms.Button
        Private WithEvents GIS As AxTatukGIS_XDK11.AxTGIS_ViewerWnd

        Public Sub New()
            '
            ' Required for Windows Form Designer support
            '
            InitializeComponent()

            '
            ' TODO: Add any constructor code after InitializeComponent call
            ''
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
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.btnUserINI = New System.Windows.Forms.Button()
            Me.btnShadow = New System.Windows.Forms.Button()
            Me.button3 = New System.Windows.Forms.Button()
            Me.button2 = New System.Windows.Forms.Button()
            Me.button1 = New System.Windows.Forms.Button()
            Me.toolBar1 = New System.Windows.Forms.ToolBar()
            Me.btnFullExtent = New System.Windows.Forms.ToolBarButton()
            Me.toolBarButton1 = New System.Windows.Forms.ToolBarButton()
            Me.btnZoom = New System.Windows.Forms.ToolBarButton()
            Me.btnDrag = New System.Windows.Forms.ToolBarButton()
            Me.toolBarButton2 = New System.Windows.Forms.ToolBarButton()
            Me.imageList1 = New System.Windows.Forms.ImageList(Me.components)
            Me.statusBar1 = New System.Windows.Forms.StatusBar()
            Me.statusBarPanel1 = New System.Windows.Forms.StatusBarPanel()
            Me.statusBarPanel2 = New System.Windows.Forms.StatusBarPanel()
            Me.GIS_ControlLegend = New AxTatukGIS_XDK11.AxTGIS_ControlLegend()
            Me.GIS = New AxTatukGIS_XDK11.AxTGIS_ViewerWnd()
            Me.panel1.SuspendLayout()
            CType(Me.statusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.statusBarPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.GIS_ControlLegend, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'panel1
            '
            Me.panel1.Controls.Add(Me.btnUserINI)
            Me.panel1.Controls.Add(Me.btnShadow)
            Me.panel1.Controls.Add(Me.button3)
            Me.panel1.Controls.Add(Me.button2)
            Me.panel1.Controls.Add(Me.button1)
            Me.panel1.Controls.Add(Me.toolBar1)
            Me.panel1.Location = New System.Drawing.Point(0, 0)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(592, 28)
            Me.panel1.TabIndex = 0
            '
            'btnUserINI
            '
            Me.btnUserINI.Location = New System.Drawing.Point(345, 2)
            Me.btnUserINI.Name = "btnUserINI"
            Me.btnUserINI.Size = New System.Drawing.Size(75, 23)
            Me.btnUserINI.TabIndex = 4
            Me.btnUserINI.Text = "User INI"
            Me.btnUserINI.UseVisualStyleBackColor = True
            '
            'btnShadow
            '
            Me.btnShadow.Location = New System.Drawing.Point(169, 2)
            Me.btnShadow.Name = "btnShadow"
            Me.btnShadow.Size = New System.Drawing.Size(86, 23)
            Me.btnShadow.TabIndex = 3
            Me.btnShadow.Text = "Shadow on/off"
            Me.btnShadow.UseVisualStyleBackColor = True
            '
            'button3
            '
            Me.button3.Location = New System.Drawing.Point(426, 2)
            Me.button3.Name = "button3"
            Me.button3.Size = New System.Drawing.Size(78, 23)
            Me.button3.TabIndex = 2
            Me.button3.Text = "Reload INI"
            '
            'button2
            '
            Me.button2.Location = New System.Drawing.Point(261, 2)
            Me.button2.Name = "button2"
            Me.button2.Size = New System.Drawing.Size(78, 23)
            Me.button2.TabIndex = 1
            Me.button2.Text = "User Defined"
            '
            'button1
            '
            Me.button1.Location = New System.Drawing.Point(85, 2)
            Me.button1.Name = "button1"
            Me.button1.Size = New System.Drawing.Size(78, 23)
            Me.button1.TabIndex = 1
            Me.button1.Text = "Clear"
            '
            'toolBar1
            '
            Me.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
            Me.toolBar1.AutoSize = False
            Me.toolBar1.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.btnFullExtent, Me.toolBarButton1, Me.btnZoom, Me.btnDrag, Me.toolBarButton2})
            Me.toolBar1.ButtonSize = New System.Drawing.Size(35, 25)
            Me.toolBar1.DropDownArrows = True
            Me.toolBar1.ImageList = Me.imageList1
            Me.toolBar1.Location = New System.Drawing.Point(0, 0)
            Me.toolBar1.Name = "toolBar1"
            Me.toolBar1.ShowToolTips = True
            Me.toolBar1.Size = New System.Drawing.Size(592, 28)
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
            'toolBarButton2
            '
            Me.toolBarButton2.Name = "toolBarButton2"
            Me.toolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
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
            Me.statusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.statusBarPanel1, Me.statusBarPanel2})
            Me.statusBar1.ShowPanels = True
            Me.statusBar1.Size = New System.Drawing.Size(598, 19)
            Me.statusBar1.TabIndex = 1
            Me.statusBar1.Text = "statusBar1"
            '
            'statusBarPanel1
            '
            Me.statusBarPanel1.Alignment = System.Windows.Forms.HorizontalAlignment.Right
            Me.statusBarPanel1.Name = "statusBarPanel1"
            Me.statusBarPanel1.Text = "Altitude:"
            '
            'statusBarPanel2
            '
            Me.statusBarPanel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
            Me.statusBarPanel2.Name = "statusBarPanel2"
            Me.statusBarPanel2.Text = "Value"
            Me.statusBarPanel2.Width = 481
            '
            'GIS_ControlLegend
            '
            Me.GIS_ControlLegend.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.GIS_ControlLegend.Enabled = True
            Me.GIS_ControlLegend.Location = New System.Drawing.Point(0, 28)
            Me.GIS_ControlLegend.Name = "GIS_ControlLegend"
            Me.GIS_ControlLegend.OcxState = CType(resources.GetObject("GIS_ControlLegend.OcxState"), System.Windows.Forms.AxHost.State)
            Me.GIS_ControlLegend.Size = New System.Drawing.Size(145, 419)
            Me.GIS_ControlLegend.TabIndex = 3
            '
            'GIS
            '
            Me.GIS.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GIS.Cursor = System.Windows.Forms.Cursors.Default
            Me.GIS.Enabled = True
            Me.GIS.Location = New System.Drawing.Point(148, 28)
            Me.GIS.Name = "GIS"
            Me.GIS.OcxState = CType(resources.GetObject("GIS.OcxState"), System.Windows.Forms.AxHost.State)
            Me.GIS.Size = New System.Drawing.Size(450, 419)
            Me.GIS.TabIndex = 1
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(598, 466)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.GIS_ControlLegend)
            Me.Controls.Add(Me.statusBar1)
            Me.Controls.Add(Me.panel1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Grid"
            Me.panel1.ResumeLayout(False)
            CType(Me.statusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.statusBarPanel2, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.GIS_ControlLegend, System.ComponentModel.ISupportInitialize).EndInit()
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

        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            ' open a file
            GIS_ControlLegend.GIS_Viewer = GIS.GetOcx()
            GIS.Open(GisUtils.GisSamplesDataDirDownload() & "\World\Countries\USA\States\California\San Bernardino\NED\w001001.adf")
        End Sub

        Private Sub toolBar1_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles toolBar1.ButtonClick
            Select Case toolBar1.Buttons.IndexOf(e.Button)
                Case 0
                    ' show full map
                    GIS.FullExtent()
                Case 2
                    ' set zoom mode
                    GIS.Mode = TGIS_ViewerMode.Zoom
                    toolBar1.Buttons(3).Pushed = False
                Case 3
                    ' set drag mode
                    GIS.Mode = TGIS_ViewerMode.Drag
                    toolBar1.Buttons(2).Pushed = False
            End Select
        End Sub

        Private Sub button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles button1.Click
            Dim ll As TGIS_LayerPixel

            ll = CType(GIS.Items.item(0), TGIS_LayerPixel)
            ll.Params.Pixel.AltitudeMapZones.Clear()
            GIS.InvalidateWholeMap()
        End Sub

        Private Sub button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles button2.Click
            Dim ll As TGIS_LayerPixel

            ll = CType(GIS.Items.item(0), TGIS_LayerPixel)
            ll.Params.Pixel.AltitudeMapZones.Clear()
            ll.Params.Pixel.AltitudeMapZones.Add("200,  400 , OLIVE , VERY LOW")
            ll.Params.Pixel.AltitudeMapZones.Add("400,  700 , OLIVE , LOW")
            ll.Params.Pixel.AltitudeMapZones.Add("700,  900 , GREEN , MID")
            ll.Params.Pixel.AltitudeMapZones.Add("900,  1200, BLUE  , HIGH")
            ll.Params.Pixel.AltitudeMapZones.Add("1200, 1700, RED   , SKY")
            ll.Params.Pixel.AltitudeMapZones.Add("1700, 2200, YELLOW, HEAVEN")
            GIS.InvalidateWholeMap()
        End Sub

        Private Sub button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles button3.Click
            Dim ll As TGIS_LayerPixel

            ll = CType(GIS.Items.item(0), TGIS_LayerPixel)
            ll.ConfigName = GisUtils.GisSamplesDataDirDownload() & "\World\Countries\USA\States\California\San Bernardino\NED\w001001.adf"
            ll.RereadConfig()
            GIS.InvalidateWholeMap()
        End Sub

        Private Sub toolBar1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles toolBar1.MouseMove
            Dim p As Point = New Point(e.X, e.Y)

            If toolBar1.Buttons(0).Rectangle.Contains(p) OrElse toolBar1.Buttons(2).Rectangle.Contains(p) OrElse toolBar1.Buttons(3).Rectangle.Contains(p) Then
                toolBar1.Cursor = Cursors.Hand
            Else
                toolBar1.Cursor = Cursors.Default
            End If
        End Sub

        Private Sub GIS_MouseMove(ByVal sender As Object, ByVal e As AxTatukGIS_XDK11.ITGIS_ViewerWndEvents_MouseMoveEvent) Handles GIS.MouseMoveEvent
            Dim ptg As TGIS_Point
            Dim ll As TGIS_LayerPixel
            Dim rgb As TGIS_Color = New TGIS_Color()
            Dim natives As New TGIS_DoubleArray
            Dim transp As Boolean = False

            If GIS.InPaint Then
                Return
            End If

            ptg = GIS.ScreenToMap(GisUtils.Point(e.X, e.Y))
            ll = CType(GIS.Items.Item(0), TGIS_LayerPixel)

            If ll.Locate(ptg, rgb, natives, transp) Then
                statusBar1.Panels(1).Text = natives.Value(0).ToString()
            Else
                statusBar1.Panels(1).Text = "Unknown"
            End If
        End Sub

        Private Sub btnShadow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShadow.Click
            Dim ll As TGIS_LayerPixel

            ll = CType(GIS.Items.item(0), TGIS_LayerPixel)
            ll.Params.Pixel.GridShadow = Not ll.Params.Pixel.GridShadow
            GIS.InvalidateWholeMap()
        End Sub

        Private Sub btnUserINI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUserINI.Click
            Dim ll As TGIS_LayerPixel

            ll = CType(GIS.Items.item(0), TGIS_LayerPixel)
            ll.ConfigName = GisUtils.GisSamplesDataDirDownload() & "\Samples\Projects\dem_ned.ini"
            ll.RereadConfig()
            GIS.InvalidateWholeMap()
        End Sub
    End Class
End Namespace
