Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS_XDK11

Namespace MiniMap
    ''' <summary>
    ''' Summary description for WinForm.
    ''' </summary>
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
        Private toolBarButton1 As System.Windows.Forms.ToolBarButton
        Private btnZoom As System.Windows.Forms.ToolBarButton
        Private btnDrag As System.Windows.Forms.ToolBarButton
        Private imageList1 As System.Windows.Forms.ImageList
        Private statusBar1 As System.Windows.Forms.StatusBar
        Private statusBarPanel1 As System.Windows.Forms.StatusBarPanel
        Private paLeft As System.Windows.Forms.Panel
        Private WithEvents GISm As AxTatukGIS_XDK11.AxTGIS_ViewerWnd
        Private contextMenu1 As System.Windows.Forms.ContextMenu
        Private Rectcolor1 As System.Windows.Forms.MenuItem
        Private WithEvents colorR As System.Windows.Forms.MenuItem
        Private Outlinecolor1 As System.Windows.Forms.MenuItem
        Private WithEvents color0 As System.Windows.Forms.MenuItem
        Private WithEvents GIS As AxTatukGIS_XDK11.AxTGIS_ViewerWnd
        Private dlgColor As System.Windows.Forms.ColorDialog
        Private Const MINIMAP_R_NAME As String = "minimap_rect"
        Private Const MINIMAP_O_NAME As String = "minimap_rect_outline"
        Private minishp As TGIS_Shape 'minimap shape
        Private minishpo As TGIS_Shape 'minimap shape outline
        Private fminiMove As Boolean 'flag for move mini rectangle
        Private lP1, lP2, lP3, lP4 As TGIS_Point
        Private WithEvents gbCanvasInfo As GroupBox
        Private WithEvents lbP4 As Label
        Private WithEvents lbP3 As Label
        Private WithEvents lbP2 As Label
        Private WithEvents lbP1 As Label
        Private ctrlPressed As Boolean

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
            Me.btnFullExtent = New System.Windows.Forms.ToolBarButton()
            Me.btnZoomIn = New System.Windows.Forms.ToolBarButton()
            Me.btnZoomOut = New System.Windows.Forms.ToolBarButton()
            Me.toolBarButton1 = New System.Windows.Forms.ToolBarButton()
            Me.btnZoom = New System.Windows.Forms.ToolBarButton()
            Me.btnDrag = New System.Windows.Forms.ToolBarButton()
            Me.imageList1 = New System.Windows.Forms.ImageList(Me.components)
            Me.statusBar1 = New System.Windows.Forms.StatusBar()
            Me.statusBarPanel1 = New System.Windows.Forms.StatusBarPanel()
            Me.paLeft = New System.Windows.Forms.Panel()
            Me.GISm = New AxTatukGIS_XDK11.AxTGIS_ViewerWnd()
            Me.contextMenu1 = New System.Windows.Forms.ContextMenu()
            Me.Rectcolor1 = New System.Windows.Forms.MenuItem()
            Me.colorR = New System.Windows.Forms.MenuItem()
            Me.Outlinecolor1 = New System.Windows.Forms.MenuItem()
            Me.color0 = New System.Windows.Forms.MenuItem()
            Me.gbCanvasInfo = New System.Windows.Forms.GroupBox()
            Me.lbP4 = New System.Windows.Forms.Label()
            Me.lbP3 = New System.Windows.Forms.Label()
            Me.lbP2 = New System.Windows.Forms.Label()
            Me.lbP1 = New System.Windows.Forms.Label()
            Me.GIS = New AxTatukGIS_XDK11.AxTGIS_ViewerWnd()
            Me.dlgColor = New System.Windows.Forms.ColorDialog()
            CType(Me.statusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.paLeft.SuspendLayout()
            CType(Me.GISm, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.gbCanvasInfo.SuspendLayout()
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'toolBar1
            '
            Me.toolBar1.AutoSize = False
            Me.toolBar1.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.btnFullExtent, Me.btnZoomIn, Me.btnZoomOut, Me.toolBarButton1, Me.btnZoom, Me.btnDrag})
            Me.toolBar1.DropDownArrows = True
            Me.toolBar1.ImageList = Me.imageList1
            Me.toolBar1.Location = New System.Drawing.Point(0, 0)
            Me.toolBar1.Name = "toolBar1"
            Me.toolBar1.ShowToolTips = True
            Me.toolBar1.Size = New System.Drawing.Size(597, 29)
            Me.toolBar1.TabIndex = 0
            '
            'btnFullExtent
            '
            Me.btnFullExtent.ImageIndex = 0
            Me.btnFullExtent.Name = "btnFullExtent"
            Me.btnFullExtent.ToolTipText = "Full Extent"
            '
            'btnZoomIn
            '
            Me.btnZoomIn.ImageIndex = 1
            Me.btnZoomIn.Name = "btnZoomIn"
            Me.btnZoomIn.ToolTipText = "Zoom In"
            '
            'btnZoomOut
            '
            Me.btnZoomOut.ImageIndex = 2
            Me.btnZoomOut.Name = "btnZoomOut"
            Me.btnZoomOut.ToolTipText = "Zoom Out"
            '
            'toolBarButton1
            '
            Me.toolBarButton1.Name = "toolBarButton1"
            Me.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
            '
            'btnZoom
            '
            Me.btnZoom.ImageIndex = 3
            Me.btnZoom.Name = "btnZoom"
            Me.btnZoom.Pushed = True
            Me.btnZoom.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton
            Me.btnZoom.ToolTipText = "Zoom"
            '
            'btnDrag
            '
            Me.btnDrag.ImageIndex = 4
            Me.btnDrag.Name = "btnDrag"
            Me.btnDrag.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton
            Me.btnDrag.ToolTipText = "Drag"
            '
            'imageList1
            '
            Me.imageList1.ImageStream = CType(resources.GetObject("imageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.imageList1.TransparentColor = System.Drawing.Color.Fuchsia
            Me.imageList1.Images.SetKeyName(0, "")
            Me.imageList1.Images.SetKeyName(1, "")
            Me.imageList1.Images.SetKeyName(2, "")
            Me.imageList1.Images.SetKeyName(3, "")
            Me.imageList1.Images.SetKeyName(4, "")
            '
            'statusBar1
            '
            Me.statusBar1.Location = New System.Drawing.Point(0, 447)
            Me.statusBar1.Name = "statusBar1"
            Me.statusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.statusBarPanel1})
            Me.statusBar1.ShowPanels = True
            Me.statusBar1.Size = New System.Drawing.Size(597, 19)
            Me.statusBar1.TabIndex = 1
            '
            'statusBarPanel1
            '
            Me.statusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
            Me.statusBarPanel1.Name = "statusBarPanel1"
            Me.statusBarPanel1.Width = 580
            '
            'paLeft
            '
            Me.paLeft.Controls.Add(Me.GISm)
            Me.paLeft.Controls.Add(Me.gbCanvasInfo)
            Me.paLeft.Dock = System.Windows.Forms.DockStyle.Left
            Me.paLeft.Location = New System.Drawing.Point(0, 29)
            Me.paLeft.Name = "paLeft"
            Me.paLeft.Size = New System.Drawing.Size(201, 418)
            Me.paLeft.TabIndex = 2
            '
            'GISm
            '
            Me.GISm.ContextMenu = Me.contextMenu1
            Me.GISm.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.GISm.Enabled = True
            Me.GISm.Location = New System.Drawing.Point(0, 244)
            Me.GISm.Name = "GISm"
            Me.GISm.OcxState = CType(resources.GetObject("GISm.OcxState"), System.Windows.Forms.AxHost.State)
            Me.GISm.Size = New System.Drawing.Size(201, 174)
            Me.GISm.TabIndex = 1
            '
            'contextMenu1
            '
            Me.contextMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.Rectcolor1, Me.Outlinecolor1})
            '
            'Rectcolor1
            '
            Me.Rectcolor1.Index = 0
            Me.Rectcolor1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.colorR})
            Me.Rectcolor1.Text = "Rectangle"
            '
            'colorR
            '
            Me.colorR.Index = 0
            Me.colorR.Text = "color..."
            '
            'Outlinecolor1
            '
            Me.Outlinecolor1.Index = 1
            Me.Outlinecolor1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.color0})
            Me.Outlinecolor1.Text = "Outline"
            '
            'color0
            '
            Me.color0.Index = 0
            Me.color0.Text = "color..."
            '
            'gbCanvasInfo
            '
            Me.gbCanvasInfo.Controls.Add(Me.lbP4)
            Me.gbCanvasInfo.Controls.Add(Me.lbP3)
            Me.gbCanvasInfo.Controls.Add(Me.lbP2)
            Me.gbCanvasInfo.Controls.Add(Me.lbP1)
            Me.gbCanvasInfo.Dock = System.Windows.Forms.DockStyle.Top
            Me.gbCanvasInfo.Location = New System.Drawing.Point(0, 0)
            Me.gbCanvasInfo.Name = "gbCanvasInfo"
            Me.gbCanvasInfo.Size = New System.Drawing.Size(201, 238)
            Me.gbCanvasInfo.TabIndex = 0
            Me.gbCanvasInfo.TabStop = False
            Me.gbCanvasInfo.Text = " Map extent: "
            '
            'lbP4
            '
            Me.lbP4.Location = New System.Drawing.Point(8, 72)
            Me.lbP4.Name = "lbP4"
            Me.lbP4.Size = New System.Drawing.Size(176, 13)
            Me.lbP4.TabIndex = 3
            Me.lbP4.Text = "p4: x,y "
            '
            'lbP3
            '
            Me.lbP3.Location = New System.Drawing.Point(8, 56)
            Me.lbP3.Name = "lbP3"
            Me.lbP3.Size = New System.Drawing.Size(176, 13)
            Me.lbP3.TabIndex = 2
            Me.lbP3.Text = "p3: x,y "
            '
            'lbP2
            '
            Me.lbP2.Location = New System.Drawing.Point(8, 40)
            Me.lbP2.Name = "lbP2"
            Me.lbP2.Size = New System.Drawing.Size(176, 13)
            Me.lbP2.TabIndex = 1
            Me.lbP2.Text = "p2: x,y "
            '
            'lbP1
            '
            Me.lbP1.Location = New System.Drawing.Point(8, 24)
            Me.lbP1.Name = "lbP1"
            Me.lbP1.Size = New System.Drawing.Size(176, 13)
            Me.lbP1.TabIndex = 0
            Me.lbP1.Text = "p1: x,y "
            '
            'GIS
            '
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.Enabled = True
            Me.GIS.Location = New System.Drawing.Point(201, 29)
            Me.GIS.Name = "GIS"
            Me.GIS.OcxState = CType(resources.GetObject("GIS.OcxState"), System.Windows.Forms.AxHost.State)
            Me.GIS.Size = New System.Drawing.Size(396, 418)
            Me.GIS.TabIndex = 3
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(597, 466)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.paLeft)
            Me.Controls.Add(Me.statusBar1)
            Me.Controls.Add(Me.toolBar1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.KeyPreview = True
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Minimap"
            CType(Me.statusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.paLeft.ResumeLayout(False)
            CType(Me.GISm, System.ComponentModel.ISupportInitialize).EndInit()
            Me.gbCanvasInfo.ResumeLayout(False)
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
            Dim llm As TGIS_LayerVector
            Dim lv As TGIS_LayerVector
            Dim lw As TGIS_LayerVector

            GIS.Open(GisUtils.GisSamplesDataDirDownload() & "\World\Countries\Poland\DCW\poland.ttkproject")

            GIS.SetCSByEPSG(2180)

            llm = CType(GisUtils.GisCreateLayer("country", GisUtils.GisSamplesDataDirDownload() & "\World\Countries\Poland\DCW\country.shp"), TGIS_LayerVector)
            llm.UseConfig = False
            llm.Params.Area.Color = (New TGIS_Color).White
            llm.Params.Area.OutlineColor = (New TGIS_Color).Silver
            GISm.Add(llm) 'add layer to minimap

            lv = New TGIS_LayerVector() 'create minimap transparent rectangle
            lv.Transparency = 30
            lv.Params.Area.Color = (New TGIS_Color).Red
            lv.Params.Area.OutlineWidth = -2
            lv.Name = MINIMAP_R_NAME
            lv.CS = llm.CS
            GISm.Add(lv)
            minishp = (CType(GISm.Get(MINIMAP_R_NAME), TGIS_LayerVector)).CreateShape(TGIS_ShapeType.Polygon)
            lw = New TGIS_LayerVector()
            lw.Params.Line.Color = (New TGIS_Color).Maroon
            lw.Params.Line.Width = -2
            lw.Name = MINIMAP_O_NAME
            lw.CS = llm.CS
            GISm.Add(lw)
            minishpo = (CType(GISm.Get(MINIMAP_O_NAME), TGIS_LayerVector)).CreateShape(TGIS_ShapeType.Arc)

            GISm.FullExtent()
            GIS.FullExtent()

            GISm.RestrictedExtent = GISm.Extent
            minishp.Layer.Extent = GISm.Extent
            GISm.Cursor = Cursors.Hand
            fminiMove = False
        End Sub

        Private Sub toolBar1_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles toolBar1.ButtonClick
            '?
            Select Case toolBar1.Buttons.IndexOf(e.Button)
                Case 0
                    ' btnFullExt
                    GIS.FullExtent()
                Case 1
                    ' btnZoomIn
                    GIS.Zoom = GIS.Zoom * 2
                Case 2
                    ' btnZoomOut
                    GIS.Zoom = GIS.Zoom / 2
                Case 4
                    ' btnZoom
                    btnDrag.Pushed = False
                    GIS.Mode = TGIS_ViewerMode.Zoom
                Case 5
                    ' btnDrag
                    btnZoom.Pushed = False
                    GIS.Mode = TGIS_ViewerMode.Drag
            End Select
        End Sub

        Private Sub GISm_MouseDown(ByVal sender As Object, ByVal e As AxTatukGIS_XDK11.ITGIS_ViewerWndEvents_MouseDownEvent) Handles GISm.MouseDownEvent
            If GIS.IsEmpty Then
                Exit Sub
            End If
            If e.Button = System.Windows.Forms.MouseButtons.Right Then
                Exit Sub
            End If
            fminiMove = True
        End Sub

        Private Sub GISm_MouseMove(ByVal sender As Object, ByVal e As AxTatukGIS_XDK11.ITGIS_ViewerWndEvents_MouseMoveEvent) Handles GISm.MouseMoveEvent
            Dim ptg As TGIS_Point

            If GIS.IsEmpty Then
                Exit Sub
            End If

            If ((Not fminiMove)) AndAlso ((Not ctrlPressed)) Then
                Exit Sub
            End If
            ptg = GISm.ScreenToMap(GisUtils.Point(e.X, e.Y))
            minishp.SetPosition(ptg, Nothing, 1)
            GISm.InvalidateWholeMap()
            If ctrlPressed Then
                GISm_MouseUp(sender, New AxTatukGIS_XDK11.ITGIS_ViewerWndEvents_MouseUpEvent(e.Translated, TMouseButton.mbLeft, e.Shift, e.X, e.Y))
            End If

        End Sub

        Private Sub GISm_MouseUp(ByVal sender As Object, ByVal e As AxTatukGIS_XDK11.ITGIS_ViewerWndEvents_MouseUpEvent) Handles GISm.MouseUpEvent
            'mouse click to change rectangle position on minimap
            Dim ptg As TGIS_Point
            Dim p1, p2, p3, p4 As New TGIS_Point

            If GIS.IsEmpty Then
                Exit Sub
            End If

            If Not fminiMove Then
                Exit Sub
            End If

            ptg = GISm.ScreenToMap(GisUtils.Point(e.X, e.Y))

            minishp.SetPosition(ptg, Nothing, 1)

            GISm.InvalidateWholeMap()
            fminiMove = False

            p1 = minishp.GetPoint(0, 0)
            p2 = minishp.GetPoint(0, 1)
            p3 = minishp.GetPoint(0, 2)
            p4.X = p1.X + (p2.X - p1.X) / 2
            p4.Y = p1.Y + (p3.Y - p2.Y) / 2
            GIS.Center = GISm.CS.ToCS(GIS.CS, p4)
        End Sub

        Private Sub GIS_VisibleExtentChange(ByVal sender As Object, ByVal e As AxTatukGIS_XDK11.ITGIS_ViewerWndEvents_VisibleExtentChangeEventEvent) Handles GIS.VisibleExtentChangeEvent
            Dim ex As TGIS_Extent

            ex = GIS.VisibleExtent
            lP1 = GisUtils.GisPoint(ex.XMin, ex.YMin)
            lP2 = GisUtils.GisPoint(ex.XMax, ex.YMin)
            lP3 = GisUtils.GisPoint(ex.XMax, ex.YMax)
            lP4 = GisUtils.GisPoint(ex.XMin, ex.YMax)
            lbP1.Text = String.Format("P1 : x: {0:F2}   y: {1:F2}", lP1.X, lP1.Y)
            lbP2.Text = String.Format("P2 : x: {0:F2}   y: {1:F2}", lP2.X, lP2.Y)
            lbP3.Text = String.Format("P3 : x: {0:F2}   y: {1:F2}", lP3.X, lP3.Y)
            lbP4.Text = String.Format("P4 : x: {0:F2}   y: {1:F2}", lP4.X, lP4.Y)
            miniMapRefresh()
        End Sub

        Private Sub GIS_MouseMove(ByVal sender As Object, ByVal e As AxTatukGIS_XDK11.ITGIS_ViewerWndEvents_MouseMoveEvent) Handles GIS.MouseMoveEvent
            Dim ptg As TGIS_Point

            If GIS.IsEmpty Then
                Exit Sub
            End If

            ptg = GIS.ScreenToMap(GisUtils.Point(e.X, e.Y))
            statusBar1.Panels(0).Text = String.Format("x: {0:F2}   y: {1:F2}", ptg.X, ptg.Y)
        End Sub

        Private Sub colorR_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles colorR.Click
            'change color of the rectangle
            Dim lv As TGIS_LayerVector

            If dlgColor.ShowDialog() <> System.Windows.Forms.DialogResult.OK Then
                Exit Sub
            End If
            lv = CType(GISm.Get(MINIMAP_R_NAME), TGIS_LayerVector)
            lv.Params.Area.Color = (New TGIS_Color).FromARGB(dlgColor.Color.A, dlgColor.Color.R, dlgColor.Color.G, dlgColor.Color.B)
            GISm.InvalidateWholeMap()
        End Sub

        Private Sub color0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles color0.Click
            'change color of the outline
            Dim lv As TGIS_LayerVector

            If dlgColor.ShowDialog() <> System.Windows.Forms.DialogResult.OK Then
                Exit Sub
            End If
            lv = CType(GISm.Get(MINIMAP_O_NAME), TGIS_LayerVector)
            lv.Params.Line.Color = (New TGIS_Color).FromARGB(dlgColor.Color.A, dlgColor.Color.R, dlgColor.Color.G, dlgColor.Color.B)
            GISm.InvalidateWholeMap()
        End Sub

        Private Sub miniMapRefresh()
            Dim ptg1 As TGIS_Point
            Dim ptg2 As TGIS_Point
            Dim ptg3 As TGIS_Point
            Dim ptg4 As TGIS_Point
            Dim ex As TGIS_Extent

            If GIS.IsEmpty Then
                Exit Sub
            End If

            ex = GISm.CS.ExtentFromCS(GIS.CS, GIS.VisibleExtent)
            ex = GIS.UnrotatedExtent(ex)

            If (ex.XMin < -360) AndAlso (ex.XMax > 360) AndAlso (ex.YMin < -180) AndAlso (ex.YMax > 180) Then
                Exit Sub
            End If

            ptg1 = GisUtils.GisPoint(ex.XMin, ex.YMin)
            ptg2 = GisUtils.GisPoint(ex.XMax, ex.YMin)
            ptg3 = GisUtils.GisPoint(ex.XMax, ex.YMax)
            ptg4 = GisUtils.GisPoint(ex.XMin, ex.YMax)

            If minishp IsNot Nothing Then
                minishp.Reset()
                minishp.Lock(TGIS_Lock.Extent)
                minishp.AddPart()
                minishp.AddPoint(ptg1)
                minishp.AddPoint(ptg2)
                minishp.AddPoint(ptg3)
                minishp.AddPoint(ptg4)
                minishp.Unlock()
            End If

            If minishpo IsNot Nothing Then
                minishpo.Reset()
                minishpo.Lock(TGIS_Lock.Extent)
                minishpo.AddPart()
                minishpo.AddPoint(ptg1)
                minishpo.AddPoint(ptg2)
                minishpo.AddPoint(ptg3)
                minishpo.AddPoint(ptg4)
                minishpo.AddPoint(ptg1)
                minishpo.Unlock()
            End If


            GISm.InvalidateWholeMap()
        End Sub

        Private Function miniRecalc(ByVal _ptg As TGIS_Point) As TGIS_Point
            Dim ptgr As TGIS_Point 'result
            Dim deltax, deltay As Double
            Dim p1, p2, p3, p4 As TGIS_Point
            Dim ex As TGIS_Extent

            p1 = minishp.GetPoint(0, 0)
            p2 = minishp.GetPoint(0, 1)
            p3 = minishp.GetPoint(0, 2)
            p4 = minishp.GetPoint(0, 3)
            ex = GIS.Extent
            deltax = (p2.X - p1.X) / 2 'delta 1/2 of mini rect length
            deltay = (p3.Y - p2.Y) / 2
            ptgr = _ptg
            If _ptg.X < (ex.XMin + deltax) Then
                ptgr.X = (ex.XMin + deltax)
            End If
            If _ptg.Y < (ex.YMin + deltay) Then
                ptgr.Y = (ex.YMin + deltay)
            End If
            If _ptg.X > (ex.XMax - deltax) Then
                ptgr.X = (ex.XMax - deltax)
            End If
            If _ptg.Y > (ex.YMax - deltay) Then
                ptgr.Y = (ex.YMax - deltay)
            End If

            Return ptgr
        End Function

        Private Sub WinForm_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
            'on minimap: Mouse move + ctrl to preview rectangle position 
            If e.Control Then
                ctrlPressed = True
            End If
        End Sub

        Private Sub WinForm_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
            If ctrlPressed Then
                ctrlPressed = False
            End If
        End Sub
    End Class
End Namespace
