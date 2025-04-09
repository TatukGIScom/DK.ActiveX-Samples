Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports System.IO
Imports TatukGIS_XDK11

Namespace Viewer
  ''' <summary>
  ''' Summary description for WinForm.
  ''' </summary>
  Public Class WinForm
    Inherits System.Windows.Forms.Form
    ''' <summary>
    ''' Required designer variable.
    ''' </summary>
    Private components As System.ComponentModel.IContainer
    Private mainMenu1 As System.Windows.Forms.MainMenu
    Private WithEvents toolBar1 As System.Windows.Forms.ToolBar
    Private imageList1 As System.Windows.Forms.ImageList
    Private btnFileOpen As System.Windows.Forms.ToolBarButton
    Private btnAppend As System.Windows.Forms.ToolBarButton
    Private btnClose As System.Windows.Forms.ToolBarButton
    Private btnFilePrint As System.Windows.Forms.ToolBarButton
    Private btnEditFile As System.Windows.Forms.ToolBarButton
    Private btnSaveToImage As System.Windows.Forms.ToolBarButton
    Private toolBarButton1 As System.Windows.Forms.ToolBarButton
    Private btnViewFullExtent As System.Windows.Forms.ToolBarButton
    Private btnViewZoomMode As System.Windows.Forms.ToolBarButton
    Private btnViewDragMode As System.Windows.Forms.ToolBarButton
    Private toolBarButton2 As System.Windows.Forms.ToolBarButton
    Private btnViewSelectMode As System.Windows.Forms.ToolBarButton
    Private btnSearch As System.Windows.Forms.ToolBarButton
    Private btnSaveAll As System.Windows.Forms.ToolBarButton
    Private stsBar As System.Windows.Forms.StatusBar
    Private stsBarPanel1 As System.Windows.Forms.StatusBarPanel
    Private stsBarPanel2 As System.Windows.Forms.StatusBarPanel
    Private stsBarPanel3 As System.Windows.Forms.StatusBarPanel
    Private stsBarPanel4 As System.Windows.Forms.StatusBarPanel
    Private GIS_ControlLegend As AxTatukGIS_XDK11.AxTGIS_ControlLegend
    Private splitter1 As System.Windows.Forms.Splitter
    Public WithEvents GIS As AxTatukGIS_XDK11.AxTGIS_ViewerWnd
    Private mnuFile As System.Windows.Forms.MenuItem
    Private WithEvents mnuFileOpen As System.Windows.Forms.MenuItem
    Private WithEvents mnuAppend As System.Windows.Forms.MenuItem
    Private WithEvents mnuEditFile As System.Windows.Forms.MenuItem
    Private WithEvents mnuExportLayer As System.Windows.Forms.MenuItem
    Private WithEvents mnuFilePrint As System.Windows.Forms.MenuItem
    Private WithEvents mnuClose As System.Windows.Forms.MenuItem
    Private WithEvents mnuFileExit As System.Windows.Forms.MenuItem
    Private menuItem9 As System.Windows.Forms.MenuItem
    Private mnuView As System.Windows.Forms.MenuItem
    Private WithEvents mnuViewFullExtent As System.Windows.Forms.MenuItem
    Private WithEvents mnuViewZoomMode As System.Windows.Forms.MenuItem
    Private menuItem13 As System.Windows.Forms.MenuItem
    Private WithEvents mnuViewDragMode As System.Windows.Forms.MenuItem
    Private WithEvents mnuViewSelectMode As System.Windows.Forms.MenuItem
    Private mnuOptions As System.Windows.Forms.MenuItem
    Private WithEvents mnuUseRTree As System.Windows.Forms.MenuItem
    Private WithEvents mnuColor As System.Windows.Forms.MenuItem
    Private mnuSearch As System.Windows.Forms.MenuItem
    Private WithEvents mnuFindShape As System.Windows.Forms.MenuItem
    Private dlgFileOpen As System.Windows.Forms.OpenFileDialog
    Private dlgFileSave As System.Windows.Forms.SaveFileDialog
    Private dlgColor As System.Windows.Forms.ColorDialog
    Private dlgFileAppend As System.Windows.Forms.OpenFileDialog
    Private dlgSaveImage As System.Windows.Forms.SaveFileDialog
    Private srchForm As SearchForm
    Public infForm As InfoForm
    Private expForm As ExportForm
    Private edtForm As EditForm
    Friend WithEvents ToolBarButton3 As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnLegendMode As System.Windows.Forms.ToolBarButton
    Friend WithEvents ToolBarButton4 As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnCS As System.Windows.Forms.ToolBarButton
    Friend WithEvents ToolBarButton5 As System.Windows.Forms.ToolBarButton
    Friend WithEvents GIS_ControlPrintPreviewSimple As TGIS_ControlPrintPreviewSimple


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
      Me.mainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
      Me.mnuFile = New System.Windows.Forms.MenuItem()
      Me.mnuFileOpen = New System.Windows.Forms.MenuItem()
      Me.mnuAppend = New System.Windows.Forms.MenuItem()
      Me.mnuEditFile = New System.Windows.Forms.MenuItem()
      Me.mnuExportLayer = New System.Windows.Forms.MenuItem()
      Me.mnuFilePrint = New System.Windows.Forms.MenuItem()
      Me.mnuClose = New System.Windows.Forms.MenuItem()
      Me.menuItem9 = New System.Windows.Forms.MenuItem()
      Me.mnuFileExit = New System.Windows.Forms.MenuItem()
      Me.mnuView = New System.Windows.Forms.MenuItem()
      Me.mnuViewFullExtent = New System.Windows.Forms.MenuItem()
      Me.menuItem13 = New System.Windows.Forms.MenuItem()
      Me.mnuViewZoomMode = New System.Windows.Forms.MenuItem()
      Me.mnuViewDragMode = New System.Windows.Forms.MenuItem()
      Me.mnuViewSelectMode = New System.Windows.Forms.MenuItem()
      Me.mnuOptions = New System.Windows.Forms.MenuItem()
      Me.mnuUseRTree = New System.Windows.Forms.MenuItem()
      Me.mnuColor = New System.Windows.Forms.MenuItem()
      Me.mnuSearch = New System.Windows.Forms.MenuItem()
      Me.mnuFindShape = New System.Windows.Forms.MenuItem()
      Me.toolBar1 = New System.Windows.Forms.ToolBar()
      Me.btnFileOpen = New System.Windows.Forms.ToolBarButton()
      Me.btnAppend = New System.Windows.Forms.ToolBarButton()
      Me.btnClose = New System.Windows.Forms.ToolBarButton()
      Me.btnFilePrint = New System.Windows.Forms.ToolBarButton()
      Me.btnEditFile = New System.Windows.Forms.ToolBarButton()
      Me.btnSaveToImage = New System.Windows.Forms.ToolBarButton()
      Me.toolBarButton1 = New System.Windows.Forms.ToolBarButton()
      Me.btnViewFullExtent = New System.Windows.Forms.ToolBarButton()
      Me.btnViewZoomMode = New System.Windows.Forms.ToolBarButton()
      Me.btnViewDragMode = New System.Windows.Forms.ToolBarButton()
      Me.toolBarButton2 = New System.Windows.Forms.ToolBarButton()
      Me.btnViewSelectMode = New System.Windows.Forms.ToolBarButton()
      Me.btnSearch = New System.Windows.Forms.ToolBarButton()
      Me.btnSaveAll = New System.Windows.Forms.ToolBarButton()
      Me.ToolBarButton3 = New System.Windows.Forms.ToolBarButton()
      Me.btnLegendMode = New System.Windows.Forms.ToolBarButton()
      Me.ToolBarButton4 = New System.Windows.Forms.ToolBarButton()
      Me.btnCS = New System.Windows.Forms.ToolBarButton()
      Me.ToolBarButton5 = New System.Windows.Forms.ToolBarButton()
      Me.imageList1 = New System.Windows.Forms.ImageList(Me.components)
      Me.stsBar = New System.Windows.Forms.StatusBar()
      Me.stsBarPanel1 = New System.Windows.Forms.StatusBarPanel()
      Me.stsBarPanel2 = New System.Windows.Forms.StatusBarPanel()
      Me.stsBarPanel3 = New System.Windows.Forms.StatusBarPanel()
      Me.stsBarPanel4 = New System.Windows.Forms.StatusBarPanel()
      Me.splitter1 = New System.Windows.Forms.Splitter()
      Me.dlgFileOpen = New System.Windows.Forms.OpenFileDialog()
      Me.dlgFileSave = New System.Windows.Forms.SaveFileDialog()
      Me.dlgColor = New System.Windows.Forms.ColorDialog()
      Me.dlgFileAppend = New System.Windows.Forms.OpenFileDialog()
      Me.dlgSaveImage = New System.Windows.Forms.SaveFileDialog()
      Me.GIS = New AxTatukGIS_XDK11.AxTGIS_ViewerWnd()
      Me.GIS_ControlLegend = New AxTatukGIS_XDK11.AxTGIS_ControlLegend()
      CType(Me.stsBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
      CType(Me.stsBarPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
      CType(Me.stsBarPanel3, System.ComponentModel.ISupportInitialize).BeginInit()
      CType(Me.stsBarPanel4, System.ComponentModel.ISupportInitialize).BeginInit()
      CType(Me.GIS, System.ComponentModel.ISupportInitialize).BeginInit()
      CType(Me.GIS_ControlLegend, System.ComponentModel.ISupportInitialize).BeginInit()
      Me.SuspendLayout()
      '
      'mainMenu1
      '
      Me.mainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuFile, Me.mnuView, Me.mnuOptions, Me.mnuSearch})
      '
      'mnuFile
      '
      Me.mnuFile.Index = 0
      Me.mnuFile.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuFileOpen, Me.mnuAppend, Me.mnuEditFile, Me.mnuExportLayer, Me.mnuFilePrint, Me.mnuClose, Me.menuItem9, Me.mnuFileExit})
      Me.mnuFile.Text = "File"
      '
      'mnuFileOpen
      '
      Me.mnuFileOpen.Index = 0
      Me.mnuFileOpen.Text = "Open ..."
      '
      'mnuAppend
      '
      Me.mnuAppend.Index = 1
      Me.mnuAppend.Shortcut = System.Windows.Forms.Shortcut.CtrlA
      Me.mnuAppend.Text = "Add ..."
      '
      'mnuEditFile
      '
      Me.mnuEditFile.Index = 2
      Me.mnuEditFile.Shortcut = System.Windows.Forms.Shortcut.CtrlE
      Me.mnuEditFile.Text = "Edit project/config file"
      '
      'mnuExportLayer
      '
      Me.mnuExportLayer.Index = 3
      Me.mnuExportLayer.Text = "Export ..."
      '
      'mnuFilePrint
      '
      Me.mnuFilePrint.Index = 4
      Me.mnuFilePrint.Text = "Print ..."
      '
      'mnuClose
      '
      Me.mnuClose.Index = 5
      Me.mnuClose.Shortcut = System.Windows.Forms.Shortcut.CtrlQ
      Me.mnuClose.Text = "Close"
      '
      'menuItem9
      '
      Me.menuItem9.Index = 6
      Me.menuItem9.Text = "-"
      '
      'mnuFileExit
      '
      Me.mnuFileExit.Index = 7
      Me.mnuFileExit.Text = "Exit"
      '
      'mnuView
      '
      Me.mnuView.Index = 1
      Me.mnuView.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuViewFullExtent, Me.menuItem13, Me.mnuViewZoomMode, Me.mnuViewDragMode, Me.mnuViewSelectMode})
      Me.mnuView.Text = "View"
      '
      'mnuViewFullExtent
      '
      Me.mnuViewFullExtent.Index = 0
      Me.mnuViewFullExtent.Text = "Full Extent"
      '
      'menuItem13
      '
      Me.menuItem13.Index = 1
      Me.menuItem13.Text = "-"
      '
      'mnuViewZoomMode
      '
      Me.mnuViewZoomMode.Index = 2
      Me.mnuViewZoomMode.Text = "Zoom"
      '
      'mnuViewDragMode
      '
      Me.mnuViewDragMode.Index = 3
      Me.mnuViewDragMode.Text = "Drag"
      '
      'mnuViewSelectMode
      '
      Me.mnuViewSelectMode.Index = 4
      Me.mnuViewSelectMode.Text = "Info"
      '
      'mnuOptions
      '
      Me.mnuOptions.Index = 2
      Me.mnuOptions.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuUseRTree, Me.mnuColor})
      Me.mnuOptions.Text = "Options"
      '
      'mnuUseRTree
      '
      Me.mnuUseRTree.Index = 0
      Me.mnuUseRTree.Text = "Use R-tree"
      '
      'mnuColor
      '
      Me.mnuColor.Index = 1
      Me.mnuColor.Text = "Color"
      '
      'mnuSearch
      '
      Me.mnuSearch.Index = 3
      Me.mnuSearch.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuFindShape})
      Me.mnuSearch.Text = "Search"
      '
      'mnuFindShape
      '
      Me.mnuFindShape.Index = 0
      Me.mnuFindShape.Shortcut = System.Windows.Forms.Shortcut.CtrlF
      Me.mnuFindShape.Text = "Find shape"
      '
      'toolBar1
      '
      Me.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
      Me.toolBar1.AutoSize = False
      Me.toolBar1.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.btnFileOpen, Me.btnAppend, Me.btnClose, Me.btnFilePrint, Me.btnEditFile, Me.btnSaveToImage, Me.toolBarButton1, Me.btnViewFullExtent, Me.btnViewZoomMode, Me.btnViewDragMode, Me.toolBarButton2, Me.btnViewSelectMode, Me.btnSearch, Me.btnSaveAll, Me.ToolBarButton3, Me.btnLegendMode, Me.ToolBarButton4, Me.btnCS, Me.ToolBarButton5})
      Me.toolBar1.ButtonSize = New System.Drawing.Size(23, 22)
      Me.toolBar1.DropDownArrows = True
      Me.toolBar1.ImageList = Me.imageList1
      Me.toolBar1.Location = New System.Drawing.Point(0, 0)
      Me.toolBar1.Name = "toolBar1"
      Me.toolBar1.ShowToolTips = True
      Me.toolBar1.Size = New System.Drawing.Size(596, 24)
      Me.toolBar1.TabIndex = 0
      '
      'btnFileOpen
      '
      Me.btnFileOpen.ImageIndex = 4
      Me.btnFileOpen.Name = "btnFileOpen"
      Me.btnFileOpen.ToolTipText = "Open GIS coverage"
      '
      'btnAppend
      '
      Me.btnAppend.ImageIndex = 6
      Me.btnAppend.Name = "btnAppend"
      Me.btnAppend.ToolTipText = "Add layers"
      '
      'btnClose
      '
      Me.btnClose.ImageIndex = 8
      Me.btnClose.Name = "btnClose"
      Me.btnClose.ToolTipText = "Close all layers"
      '
      'btnFilePrint
      '
      Me.btnFilePrint.ImageIndex = 5
      Me.btnFilePrint.Name = "btnFilePrint"
      Me.btnFilePrint.ToolTipText = "Print preview"
      '
      'btnEditFile
      '
      Me.btnEditFile.ImageIndex = 9
      Me.btnEditFile.Name = "btnEditFile"
      Me.btnEditFile.ToolTipText = "Edit project/config file"
      '
      'btnSaveToImage
      '
      Me.btnSaveToImage.ImageIndex = 10
      Me.btnSaveToImage.Name = "btnSaveToImage"
      Me.btnSaveToImage.ToolTipText = "Save to Image"
      '
      'toolBarButton1
      '
      Me.toolBarButton1.Name = "toolBarButton1"
      Me.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
      '
      'btnViewFullExtent
      '
      Me.btnViewFullExtent.ImageIndex = 0
      Me.btnViewFullExtent.Name = "btnViewFullExtent"
      Me.btnViewFullExtent.ToolTipText = "Fit map into the window"
      '
      'btnViewZoomMode
      '
      Me.btnViewZoomMode.ImageIndex = 1
      Me.btnViewZoomMode.Name = "btnViewZoomMode"
      Me.btnViewZoomMode.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton
      Me.btnViewZoomMode.ToolTipText = "Zoom mode"
      '
      'btnViewDragMode
      '
      Me.btnViewDragMode.ImageIndex = 2
      Me.btnViewDragMode.Name = "btnViewDragMode"
      Me.btnViewDragMode.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton
      Me.btnViewDragMode.ToolTipText = "Drag mode"
      '
      'toolBarButton2
      '
      Me.toolBarButton2.Name = "toolBarButton2"
      Me.toolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
      '
      'btnViewSelectMode
      '
      Me.btnViewSelectMode.ImageIndex = 3
      Me.btnViewSelectMode.Name = "btnViewSelectMode"
      Me.btnViewSelectMode.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton
      Me.btnViewSelectMode.ToolTipText = "Select mode"
      '
      'btnSearch
      '
      Me.btnSearch.ImageIndex = 7
      Me.btnSearch.Name = "btnSearch"
      Me.btnSearch.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton
      Me.btnSearch.ToolTipText = "Search"
      '
      'btnSaveAll
      '
      Me.btnSaveAll.ImageIndex = 11
      Me.btnSaveAll.Name = "btnSaveAll"
      Me.btnSaveAll.ToolTipText = "Save changes"
      '
      'ToolBarButton3
      '
      Me.ToolBarButton3.Name = "ToolBarButton3"
      Me.ToolBarButton3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
      '
      'btnLegendMode
      '
      Me.btnLegendMode.ImageIndex = 12
      Me.btnLegendMode.Name = "btnLegendMode"
      Me.btnLegendMode.ToolTipText = "Legend mode"
      '
      'ToolBarButton4
      '
      Me.ToolBarButton4.Name = "ToolBarButton4"
      Me.ToolBarButton4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
      '
      'btnCS
      '
      Me.btnCS.ImageIndex = 13
      Me.btnCS.Name = "btnCS"
      Me.btnCS.ToolTipText = "Coordinate system"
      '
      'ToolBarButton5
      '
      Me.ToolBarButton5.Name = "ToolBarButton5"
      Me.ToolBarButton5.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
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
      Me.imageList1.Images.SetKeyName(5, "")
      Me.imageList1.Images.SetKeyName(6, "")
      Me.imageList1.Images.SetKeyName(7, "")
      Me.imageList1.Images.SetKeyName(8, "")
      Me.imageList1.Images.SetKeyName(9, "")
      Me.imageList1.Images.SetKeyName(10, "")
      Me.imageList1.Images.SetKeyName(11, "")
      Me.imageList1.Images.SetKeyName(12, "DesktopSave1_b.bmp")
      Me.imageList1.Images.SetKeyName(13, "ExtentLayer.bmp")
      Me.imageList1.Images.SetKeyName(14, "FullScreen.bmp")
      '
      'stsBar
      '
      Me.stsBar.Location = New System.Drawing.Point(0, 363)
      Me.stsBar.Name = "stsBar"
      Me.stsBar.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.stsBarPanel1, Me.stsBarPanel2, Me.stsBarPanel3, Me.stsBarPanel4})
      Me.stsBar.ShowPanels = True
      Me.stsBar.Size = New System.Drawing.Size(596, 20)
      Me.stsBar.TabIndex = 1
      '
      'stsBarPanel1
      '
      Me.stsBarPanel1.Name = "stsBarPanel1"
      Me.stsBarPanel1.Width = 60
      '
      'stsBarPanel2
      '
      Me.stsBarPanel2.Name = "stsBarPanel2"
      Me.stsBarPanel2.Width = 200
      '
      'stsBarPanel3
      '
      Me.stsBarPanel3.Name = "stsBarPanel3"
      Me.stsBarPanel3.Width = 200
      '
      'stsBarPanel4
      '
      Me.stsBarPanel4.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
      Me.stsBarPanel4.Name = "stsBarPanel4"
      Me.stsBarPanel4.Width = 119
      '
      'splitter1
      '
      Me.splitter1.Location = New System.Drawing.Point(166, 24)
      Me.splitter1.Name = "splitter1"
      Me.splitter1.Size = New System.Drawing.Size(3, 339)
      Me.splitter1.TabIndex = 3
      Me.splitter1.TabStop = False
      '
      'dlgFileSave
      '
      Me.dlgFileSave.CreatePrompt = True
      Me.dlgFileSave.Filter = "Arcview Shape File (*.shp, *.shx, *.dbf)|*.SHP|Autocad (*.dxf)|*.DXF|Mapinfo Inte" &
    "rchange (*.mif, *.mid)|*.MIF|TatukGIS SQL Layer (.ttkls)|*.TTKLS"
      Me.dlgFileSave.Title = "Export layer"
      '
      'dlgFileAppend
      '
      Me.dlgFileAppend.Multiselect = True
      '
      'dlgSaveImage
      '
      Me.dlgSaveImage.CreatePrompt = True
      Me.dlgSaveImage.Filter = "BMP|*.BMP|JPG|*.JPG|PNG|*.PNG|TIF|*.TIF"
      Me.dlgSaveImage.Title = "Save to Image"
      '
      'GIS
      '
      Me.GIS.Cursor = System.Windows.Forms.Cursors.Default
      Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
      Me.GIS.Enabled = True
      Me.GIS.Location = New System.Drawing.Point(169, 24)
      Me.GIS.Name = "GIS"
      Me.GIS.OcxState = CType(resources.GetObject("GIS.OcxState"), System.Windows.Forms.AxHost.State)
      Me.GIS.Size = New System.Drawing.Size(427, 339)
      Me.GIS.TabIndex = 4
      '
      'GIS_ControlLegend
      '
      Me.GIS_ControlLegend.Dock = System.Windows.Forms.DockStyle.Left
      Me.GIS_ControlLegend.Enabled = True
      Me.GIS_ControlLegend.Location = New System.Drawing.Point(0, 24)
      Me.GIS_ControlLegend.Name = "GIS_ControlLegend"
      Me.GIS_ControlLegend.OcxState = CType(resources.GetObject("GIS_ControlLegend.OcxState"), System.Windows.Forms.AxHost.State)
      Me.GIS_ControlLegend.Size = New System.Drawing.Size(166, 339)
      Me.GIS_ControlLegend.TabIndex = 2
      '
      'WinForm
      '
      Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
      Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
      Me.ClientSize = New System.Drawing.Size(596, 383)
      Me.Controls.Add(Me.GIS)
      Me.Controls.Add(Me.splitter1)
      Me.Controls.Add(Me.GIS_ControlLegend)
      Me.Controls.Add(Me.stsBar)
      Me.Controls.Add(Me.toolBar1)
      Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
      Me.Location = New System.Drawing.Point(200, 120)
      Me.Menu = Me.mainMenu1
      Me.Name = "WinForm"
      Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
      Me.Text = "TatukGIS Coverage Viewer"
      CType(Me.stsBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
      CType(Me.stsBarPanel2, System.ComponentModel.ISupportInitialize).EndInit()
      CType(Me.stsBarPanel3, System.ComponentModel.ISupportInitialize).EndInit()
      CType(Me.stsBarPanel4, System.ComponentModel.ISupportInitialize).EndInit()
      CType(Me.GIS, System.ComponentModel.ISupportInitialize).EndInit()
      CType(Me.GIS_ControlLegend, System.ComponentModel.ISupportInitialize).EndInit()
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

    Private Sub frmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
      ' set File dialogs filter
      Dim filter As String
      filter = GisUtils.GisSupportedFiles(TGIS_FileTypes.All, False)
      filter = filter.Remove(filter.Length - 1, 1)
      dlgFileOpen.Filter = filter

      filter = GisUtils.GisSupportedFiles(TGIS_FileTypes.All, False)
      filter = filter.Remove(filter.Length - 1, 1)
      dlgFileAppend.Filter = filter
      GIS_ControlLegend.GIS_Viewer = DirectCast(GIS.GetOcx(), ITGIS_ViewerWnd_OCX)
      edtForm = New EditForm()
      updateToolBar()
    End Sub

    Private Sub updateToolBar()
      Dim notbusy As Boolean

      ' update toolbar controls
      btnViewZoomMode.Pushed = GIS.Mode = TGIS_ViewerMode.Zoom
      btnViewDragMode.Pushed = GIS.Mode = TGIS_ViewerMode.Drag
      btnViewSelectMode.Pushed = GIS.Mode = TGIS_ViewerMode.Select
      btnSearch.Pushed = False

      notbusy = True
      btnFileOpen.Enabled = notbusy
      mnuFileExit.Enabled = notbusy
      mnuExportLayer.Enabled = notbusy AndAlso ((Not GIS.IsEmpty))
      btnFilePrint.Enabled = notbusy AndAlso ((Not GIS.IsEmpty))
      mnuFilePrint.Enabled = notbusy AndAlso ((Not GIS.IsEmpty))
      btnViewFullExtent.Enabled = notbusy AndAlso ((Not GIS.IsEmpty))
      mnuViewFullExtent.Enabled = notbusy AndAlso ((Not GIS.IsEmpty))
      btnViewZoomMode.Enabled = notbusy AndAlso ((Not GIS.IsEmpty))
      mnuViewZoomMode.Enabled = notbusy AndAlso ((Not GIS.IsEmpty))
      btnViewDragMode.Enabled = notbusy AndAlso ((Not GIS.IsEmpty))
      mnuViewDragMode.Enabled = notbusy AndAlso ((Not GIS.IsEmpty))
      btnViewSelectMode.Enabled = notbusy AndAlso ((Not GIS.IsEmpty))
      mnuViewSelectMode.Enabled = notbusy AndAlso ((Not GIS.IsEmpty))
      btnAppend.Enabled = notbusy AndAlso ((Not GIS.IsEmpty))
      mnuAppend.Enabled = notbusy AndAlso ((Not GIS.IsEmpty))
      btnSearch.Enabled = notbusy AndAlso ((Not GIS.IsEmpty))
      mnuFindShape.Enabled = notbusy AndAlso ((Not GIS.IsEmpty))
      btnClose.Enabled = notbusy AndAlso ((Not GIS.IsEmpty))
      mnuClose.Enabled = notbusy AndAlso ((Not GIS.IsEmpty))
      btnEditFile.Enabled = notbusy AndAlso ((Not GIS.IsEmpty)) AndAlso (edtForm.statusBar1.Panels(1).Text <> "")
      mnuEditFile.Enabled = notbusy AndAlso ((Not GIS.IsEmpty)) AndAlso (edtForm.statusBar1.Panels(1).Text <> "")
      btnSaveToImage.Enabled = notbusy AndAlso ((Not GIS.IsEmpty))
      btnSaveAll.Enabled = notbusy AndAlso (GIS.MustSave())
      notbusy = stsBar.Panels(0).Text = ""
    End Sub

    Private Sub OpenCoverage(ByVal _path As String)
      Dim i As Integer

      ' clear the viewer
      actCloseExecute()

      Try
        ' open selected file
        GIS.Open(_path)

        mnuExportLayer.Enabled = False
        i = 0
        Do While i < GIS.Items.Count
          ' for layers of TGIS_LayerVector type enable export
          If TypeOf (CType(GIS.Items.Item(i), TGIS_LayerAbstract)) Is TGIS_LayerVector Then
            mnuExportLayer.Enabled = True
          End If
          i += 1
        Loop

        stsBar.Panels(3).Text = Path.GetFileName(_path)
      Catch e As Exception
        ' if anything went wrong, show a warning
        MessageBox.Show("File can't be open" & Constants.vbLf & e.Message)
        GIS.Close()
        GIS_ControlLegend.Update()
      End Try
      GIS.UseRTree = mnuUseRTree.Checked
      ' //? GIS.PrintTitle = _path
      ' //? GIS.PrintFooter = "Printed by TatukGIS. See our web page: www.tatukgis.com"
    End Sub

    Private Sub AppendCoverage(ByVal _path As String)
      Dim ll As TGIS_Layer

      stsBar.Panels(3).Text = ""
      Try
        mnuExportLayer.Enabled = False
        ' create a new layer
        ll = GisUtils.GisCreateLayer(Path.GetFileName(_path), _path)
        ' and add it to the viewer
        If Not ll Is Nothing Then
          ll.ReadConfig()
          GIS.Add(CType(ll, TGIS_LayerAbstract))
          If TypeOf ll Is TGIS_LayerVector Then
            mnuExportLayer.Enabled = True
          End If
        End If

        stsBar.Panels(3).Text = stsBar.Panels(3).Text + Path.GetFileName(_path) & " "
      Catch e As Exception
        MessageBox.Show("File can't be open" & Constants.vbLf + e.Message)
      End Try
      GIS.UseRTree = mnuUseRTree.Checked
    End Sub

    Private Sub toolBar1_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles toolBar1.ButtonClick
      Select Case toolBar1.Buttons.IndexOf(e.Button)
        Case 0
          ' btnFileOpen
          actFileOpenExecute()
        Case 1
          ' btnAppend
          actAppendExecute()
        Case 2
          ' btnClose
          actCloseExecute()
        Case 3
          ' btnFilePrint
          actFilePrintExecute()
        Case 4
          ' btnEditFile
          actEditFileExecute()
        Case 5
          ' btnSaveToImage
          actSaveToImageExecute()
        Case 7
          ' btnViewFullExtent
          actViewFullExtentExecute()
        Case 8
          ' btnViewZoomMode
          actViewZoomModeExecute()
        Case 9
          ' btnViewDragMode
          actViewDragModeExecute()
        Case 11
          ' btnViewSelectMode
          actViewSelectModeExecute()
        Case 12
          ' btnSearch
          actSearchExecute()
        Case 13
          ' btnSaveAll
          actSaveAllExecute()
        Case 15
          ' btnLegendMode
          actLegendMode()
        Case 17
          ' btnCS
          actCS()
      End Select
      updateToolBar()
    End Sub

    Private Sub toolBar1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles toolBar1.MouseMove
      Dim p As Point = New Point(e.X, e.Y)

      If toolBar1.Buttons(0).Rectangle.Contains(p) OrElse toolBar1.Buttons(1).Rectangle.Contains(p) OrElse toolBar1.Buttons(2).Rectangle.Contains(p) OrElse toolBar1.Buttons(3).Rectangle.Contains(p) OrElse toolBar1.Buttons(4).Rectangle.Contains(p) OrElse toolBar1.Buttons(5).Rectangle.Contains(p) OrElse toolBar1.Buttons(7).Rectangle.Contains(p) OrElse toolBar1.Buttons(8).Rectangle.Contains(p) OrElse toolBar1.Buttons(9).Rectangle.Contains(p) OrElse toolBar1.Buttons(11).Rectangle.Contains(p) OrElse toolBar1.Buttons(12).Rectangle.Contains(p) OrElse toolBar1.Buttons(13).Rectangle.Contains(p) Then
        toolBar1.Cursor = Cursors.Hand
      Else
        toolBar1.Cursor = Cursors.Default
      End If
    End Sub

    Private Sub mnuFileOpen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuFileOpen.Click
      actFileOpenExecute()
      updateToolBar()
    End Sub

    Private Sub mnuAppend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuAppend.Click
      actAppendExecute()
      updateToolBar()
    End Sub

    Private Sub mnuEditFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuEditFile.Click
      actEditFileExecute()
      updateToolBar()
    End Sub

    Private Sub mnuExportLayer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuExportLayer.Click
      actFileExportExecute()
      updateToolBar()
    End Sub

    Private Sub mnuFilePrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuFilePrint.Click
      actFilePrintExecute()
      updateToolBar()
    End Sub

    Private Sub mnuClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuClose.Click
      actCloseExecute()
      updateToolBar()
    End Sub

    Private Sub mnuFileExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuFileExit.Click
      actFileExitExecute()
    End Sub

    Private Sub mnuViewFullExtent_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuViewFullExtent.Click
      actViewFullExtentExecute()
      updateToolBar()
    End Sub

    Private Sub mnuViewZoomMode_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuViewZoomMode.Click
      actViewZoomModeExecute()
      updateToolBar()
    End Sub

    Private Sub mnuViewDragMode_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuViewDragMode.Click
      actViewDragModeExecute()
      updateToolBar()
    End Sub

    Private Sub mnuViewSelectMode_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuViewSelectMode.Click
      actViewSelectModeExecute()
      updateToolBar()
    End Sub

    Private Sub mnuUseRTree_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuUseRTree.Click
      mnuUseRTree.Checked = Not mnuUseRTree.Checked
      actUseRTreeExecute()
      updateToolBar()
    End Sub

    Private Sub mnuColor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuColor.Click
      actColorExecute()
      updateToolBar()
    End Sub

    Private Sub mnuFindShape_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuFindShape.Click
      actSearchExecute()
      updateToolBar()
    End Sub


    Private Sub GIS_Busy(ByVal _sender As Object, ByVal _e As AxTatukGIS_XDK11.ITGIS_ViewerWndEvents_BusyEventEvent) Handles GIS.BusyEvent
      ' show busy state
      _e.Abort = False
      If _e.End <= 0 Then
        stsBar.Panels(0).Text = ""
      Else
        stsBar.Panels(0).Text = String.Format("Busy {0}%", Math.Round(_e.Pos * 100 / _e.End, 0))
      End If
      Application.DoEvents()
    End Sub

    Private Sub GIS_MouseDown(ByVal sender As Object, ByVal e As AxTatukGIS_XDK11.ITGIS_ViewerWndEvents_MouseDownEvent) Handles GIS.MouseDownEvent
      Dim shp As TGIS_Shape

      ' if there is no layer or we are not in select mode, exit
      If GIS.IsEmpty Then
        Return
      End If
      If GIS.InPaint Then
        Return
      End If
      If GIS.Mode <> TGIS_ViewerMode.Select Then
        Return
      End If

      ' let's try to locate a selected shape on the map
      shp = CType(GIS.Locate(GIS.ScreenToMap(GisUtils.Point(e.X, e.Y)), 5 / GIS.Zoom), TGIS_Shape)
      If shp Is Nothing Then
        Return
      End If

      ' if any found flash it and show shape info
      shp.Flash()
      If infForm Is Nothing Then
        infForm = New InfoForm()
        infForm.mainForm = Me
        infForm.Show()
      End If
      infForm.ShowInfo(shp)
    End Sub

    Private Sub GIS_MouseMove(ByVal sender As Object, ByVal e As AxTatukGIS_XDK11.ITGIS_ViewerWndEvents_MouseMoveEvent) Handles GIS.MouseMoveEvent
      Dim ptg As TGIS_Point

      If GIS.IsEmpty Then
        Return
      End If

      ' let's locate our position on the map and display coordinates, zoom
      ptg = GIS.ScreenToMap(GisUtils.Point(e.X, e.Y))
      stsBar.Panels(1).Text = String.Format("X : {0:F4} | Y : {1:F4}", ptg.X, ptg.Y)
      stsBar.Panels(2).Text = String.Format("Zoom : {0:F4} | ZoomEx : {1:F4}", GIS.Zoom, GIS.ZoomEx)
    End Sub

    Private Sub actFileOpenExecute()
      Dim str, ext As String

      If dlgFileOpen.ShowDialog() <> System.Windows.Forms.DialogResult.OK Then
        Return
      End If

      str = dlgFileOpen.FileName
      ext = Path.GetExtension(str)
      ' if project selected, load it to editor
      If (String.Compare(ext, ".ttkproject") = 0) OrElse (String.Compare(ext, ".ttkls") = 0) Then
        edtForm.statusBar1.Panels(1).Text = str
        edtForm.Editor.Enabled = True
        edtForm.Editor.Clear()
        edtForm.LoadFromFile(str)
        edtForm.btnSave.Enabled = False
      Else
        ' if config found, load it to editor
        If File.Exists(str & ".ini") Then
          edtForm.statusBar1.Panels(1).Text = str & ".ini"
          edtForm.Editor.Enabled = True
          edtForm.Editor.Clear()
          edtForm.LoadFromFile(str & ".ini")
          edtForm.btnSave.Enabled = False
        Else
          edtForm.Editor.Enabled = False
          edtForm.statusBar1.Panels(1).Text = ""
          edtForm.Editor.Clear()
          edtForm.btnSave.Enabled = False
        End If
      End If

      ' check file extension
      Select Case dlgFileOpen.FilterIndex
        Case 8
          str = str & "?ARC"
        Case 9
          str = str & "?PAL"
        Case 10
          str = str & "?LAB"
      End Select
      ' open selected file
      OpenCoverage(str)
    End Sub

    Private Sub actFileExportExecute()
      Dim ll As TGIS_LayerVector
      Dim extent As TGIS_Extent
      Dim shape_type As TGIS_ShapeType
      Dim clipping As Boolean
      Dim ext As String

      expForm = New ExportForm()
      Try
        expForm.mainForm = Me
        If expForm.ShowDialog() = DialogResult.Cancel Then
          Return
        End If

        dlgFileSave.FileName = ""
        If dlgFileSave.ShowDialog() <> System.Windows.Forms.DialogResult.OK Then
          Return
        End If

        ' check the extension to choose a proper layer
        ext = Path.GetExtension(dlgFileSave.FileName)
        If String.Compare(ext.ToLower(), ".shp") = 0 Then
          ll = CType(New TGIS_LayerSHP(), TGIS_LayerVector)
        ElseIf String.Compare(ext.ToLower(), ".mif") = 0 Then
          ll = CType(New TGIS_LayerMIF(), TGIS_LayerVector)
        ElseIf String.Compare(ext.ToLower(), ".dxf") = 0 Then
          ll = CType(New TGIS_LayerDXF(), TGIS_LayerVector)
        ElseIf String.Compare(ext.ToLower(), ".ttkls") = 0 Then
          ll = CType(GisUtils.GisCreateLayer("", dlgFileSave.FileName), TGIS_LayerVector)
        Else
          MessageBox.Show("Unrecognized file extension")
          Return
        End If

        shape_type = TGIS_ShapeType.Unknown
        extent = GisUtils.GisWholeWorld()
        clipping = False

        ' set the extent
        Select Case expForm.SelectedExtent
          Case 0
            extent = GisUtils.GisWholeWorld()
            clipping = False
          Case 1
            extent = GIS.VisibleExtent
            clipping = False
          Case 2
            extent = GIS.VisibleExtent
            clipping = True
          Case Else
            MessageBox.Show("Untested extent case.")
            Return
        End Select

        ' set layer type
        Select Case expForm.cmbShapeType.SelectedIndex
          Case 0
            shape_type = TGIS_ShapeType.Unknown
          Case 1
            shape_type = TGIS_ShapeType.Arc
          Case 2
            shape_type = TGIS_ShapeType.Polygon
          Case 3
            shape_type = TGIS_ShapeType.Point
          Case 4
            shape_type = TGIS_ShapeType.MultiPoint
          Case Else
            MessageBox.Show("Untested shape type case.")
            Return
        End Select

        Try
          ' try to import existing layer to a new one and save it to the file
          ll.Path = dlgFileSave.FileName
          ll.CS = expForm.CS
          ll.ImportLayer(CType(GIS.Get(expForm.cmbLayersList.Text), TGIS_LayerVector), extent, shape_type, expForm.edtQuery.Text, clipping)
        Finally
          ll = Nothing
        End Try
      Finally
        expForm = Nothing
      End Try
    End Sub

    Private Sub actAppendExecute()
      Dim i As Integer
      Dim fileName As String

      If dlgFileAppend.ShowDialog() <> System.Windows.Forms.DialogResult.OK Then
        Return
      End If

      i = 0
      Do While i < dlgFileAppend.FileNames.Length
        fileName = dlgFileAppend.FileNames(i)
        Select Case dlgFileAppend.FilterIndex
          Case 8
            fileName = fileName & "?ARC"
          Case 9
            fileName = fileName & "?PAL"
          Case 10
            fileName = fileName & "?LAB"
        End Select
        ' add all selected files to the viewer
        AppendCoverage(fileName)
        i += 1
      Loop
    End Sub

    Private Sub actCloseExecute()
      GIS.Close()
      stsBar.Panels(0).Text = ""
      stsBar.Panels(1).Text = ""
      stsBar.Panels(2).Text = ""
      stsBar.Panels(3).Text = ""
    End Sub

    Private Sub actFilePrintExecute()
      ' let's see a print preview form
      GIS_ControlPrintPreviewSimple = New TGIS_ControlPrintPreviewSimple()
      GIS_ControlPrintPreviewSimple.GIS_Viewer = CType(GIS.GetOcx(), IGIS_Viewer)
      GIS_ControlPrintPreviewSimple.Preview()
    End Sub

    Private Sub actEditFileExecute()
      edtForm.ShowDialog()
    End Sub

    Private Sub actSaveToImageExecute()
      ' save image from the viewer to file
      Dim pem As TGIS_PixelExportManager
      Dim lp As TGIS_LayerPixel
      Dim w, h, dpi As Integer
      Dim sf As TGIS_LayerPixelSubFormat
      Dim vbmp As TGIS_ViewerBmp
      Dim path, ext As String
      Dim subf As TGIS_PixelSubFormat
      Dim comp As TGIS_CompressionType


      w = 1024
      h = CInt(Math.Round((GIS.Extent.YMax - GIS.Extent.YMin) / (GIS.Extent.XMax - GIS.Extent.XMin) * w))

      If dlgSaveImage.ShowDialog() = System.Windows.Forms.DialogResult.OK Then


        Select Case dlgSaveImage.FilterIndex
          Case 1
            ext = ".bmp"
            subf = TGIS_PixelSubFormat.BMP
            comp = TGIS_CompressionType.None
          Case 2
            ext = ".jpg"
            subf = TGIS_PixelSubFormat.JPEG
            comp = TGIS_CompressionType.JPEG
          Case 3
            ext = ".png"
            subf = TGIS_PixelSubFormat.PNG
            comp = TGIS_CompressionType.PNG
          Case 4
            ext = ".tif"
            subf = TGIS_PixelSubFormat.None
            comp = TGIS_CompressionType.None
          Case Else
            ext = ""
            subf = TGIS_PixelSubFormat.None
            comp = TGIS_CompressionType.None
        End Select

        path = dlgSaveImage.FileName
        If (String.Compare(System.IO.Path.GetExtension(path), "") = 0) Then
          path = path + ext
        End If
        lp = CType(GisUtils.GisCreateLayer(path, path), TGIS_LayerPixel)
        sf = New TGIS_LayerPixelSubFormat()
        sf.Create_(TGIS_PixelFormat.RGB, False, subf, comp, 90)
        lp.Build_2(lp.Path, False, GIS.CS, GIS.Extent, w, h, sf)

        pem = New TGIS_PixelExportManager
        pem.Create_(lp)
        vbmp = New TGIS_ViewerBmp()
        vbmp.Create_(512, 512)

        pem.ExportFrom_2(CType(GIS.GetOcx(), IGIS_Viewer), CType(vbmp, IGIS_ViewerBmp), GIS.Extent, dpi)
        lp.SaveData()
      End If
    End Sub

    Private Sub actViewFullExtentExecute()
      ' show the whole world
      GIS.FullExtent()
    End Sub

    Private Sub actViewZoomModeExecute()
      ' set zoom mode
      GIS.Mode = TGIS_ViewerMode.Zoom
    End Sub

    Private Sub actViewDragModeExecute()
      ' set drag mode
      GIS.Mode = TGIS_ViewerMode.Drag
    End Sub

    Private Sub actViewSelectModeExecute()
      ' set select mode
      GIS.Mode = TGIS_ViewerMode.Select
    End Sub

    Private Sub actUseRTreeExecute()
      GIS.UseRTree = mnuUseRTree.Checked
    End Sub

    Private Sub actSearchExecute()
      ' show search form
      srchForm = New SearchForm()
      Try
        btnSearch.Pushed = True
        srchForm.mainForm = Me
        srchForm.ShowDialog()
      Finally
        btnSearch.Pushed = False
        srchForm = Nothing
      End Try
    End Sub

    Private Sub actColorExecute()
      ' let's change the viewer color
      If dlgColor.ShowDialog() <> System.Windows.Forms.DialogResult.OK Then
        Return
      End If

      GIS.BackColor = dlgColor.Color
      GIS.Update()
    End Sub

    Private Sub actSaveAllExecute()
      GIS.SaveAll()
    End Sub

    Private Sub actFileExitExecute()
      ' close the application
      Application.Exit()
    End Sub

    Private Sub actLegendMode()
      If GIS_ControlLegend.Mode = TGIS_ControlLegendMode.Layers Then
        GIS_ControlLegend.Mode = TGIS_ControlLegendMode.Groups
      Else
        GIS_ControlLegend.Mode = TGIS_ControlLegendMode.Layers
      End If
    End Sub

    Private Sub actCS()
      Dim dlg As TGIS_ControlCSSystem

      dlg = New TGIS_ControlCSSystem()

      If dlg.Execute(GIS.CS) = DialogResult.OK Then
        GIS.CS = dlg.CS
      End If
    End Sub

  End Class
End Namespace

