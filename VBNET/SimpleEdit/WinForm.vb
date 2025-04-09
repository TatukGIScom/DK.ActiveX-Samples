Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS_XDK11

Namespace SimpleEdit
    ''' <summary>
    ''' Summary description for WinForm.
    ''' </summary>
    Public Class MainForm
        Inherits System.Windows.Forms.Form
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer
        Private WithEvents toolBar1 As System.Windows.Forms.ToolBar
        Private btnSave As System.Windows.Forms.ToolBarButton
        Private btnPrint As System.Windows.Forms.ToolBarButton
        Private toolBarButton1 As System.Windows.Forms.ToolBarButton
        Private btnFullExtent As System.Windows.Forms.ToolBarButton
        Private toolBarButton2 As System.Windows.Forms.ToolBarButton
        Private btnZoom As System.Windows.Forms.ToolBarButton
        Private btnDrag As System.Windows.Forms.ToolBarButton
        Private btnSelect As System.Windows.Forms.ToolBarButton
        Private btnEdit As System.Windows.Forms.ToolBarButton
        Private toolBarButton3 As System.Windows.Forms.ToolBarButton
        Private imageList1 As System.Windows.Forms.ImageList
        Private statusBar1 As System.Windows.Forms.StatusBar
        Private statusBarPanel1 As System.Windows.Forms.StatusBarPanel
        Private statusBarPanel2 As System.Windows.Forms.StatusBarPanel
        Private statusBarPanel3 As System.Windows.Forms.StatusBarPanel
        Private statusBarPanel4 As System.Windows.Forms.StatusBarPanel
        Private WithEvents GIS As AxTatukGIS_XDK11.AxTGIS_ViewerWnd
        Private panel1 As System.Windows.Forms.Panel
        Private panel2 As System.Windows.Forms.Panel
        Private panel3 As System.Windows.Forms.Panel
        Private toolBar2 As System.Windows.Forms.ToolBar
        Private WithEvents cmbLayer As System.Windows.Forms.ComboBox
        Private toolTip1 As System.Windows.Forms.ToolTip
        Private panel4 As System.Windows.Forms.Panel
        Private WithEvents toolBar3 As System.Windows.Forms.ToolBar
        Private btnAddShape As System.Windows.Forms.ToolBarButton
        Private toolBarButton4 As System.Windows.Forms.ToolBarButton
        Private btnUndo As System.Windows.Forms.ToolBarButton
        Private btnRedo As System.Windows.Forms.ToolBarButton
        Private btnRevert As System.Windows.Forms.ToolBarButton
        Private btnDelete As System.Windows.Forms.ToolBarButton
        Private btnWinding As System.Windows.Forms.ToolBarButton
        Private toolBarButton5 As System.Windows.Forms.ToolBarButton
        Private btnShowInfo As System.Windows.Forms.ToolBarButton
        Private btnAutoCenter As System.Windows.Forms.ToolBarButton
        Private toolBarButton6 As System.Windows.Forms.ToolBarButton
        Private WithEvents toolBar4 As System.Windows.Forms.ToolBar
        Private WithEvents cmbSnap As System.Windows.Forms.ComboBox
        Private toolTip2 As System.Windows.Forms.ToolTip
        Private printDialog1 As System.Windows.Forms.PrintDialog
        Private editLayer As TGIS_LayerAbstract
        Private printDocument1 As System.Drawing.Printing.PrintDocument
        Private contextMenu1 As System.Windows.Forms.ContextMenu
        Private WithEvents mnuAddPart As System.Windows.Forms.MenuItem
        Private WithEvents mnuDeletePart As System.Windows.Forms.MenuItem
        Private menuPos As TGIS_Point
        Friend WithEvents btnNewStyle As System.Windows.Forms.ToolBarButton
        Private info As SimpleEdit.InfoForm

        Public Sub New()
            '
            ' Required for Windows Form Designer support
            '
            InitializeComponent()

            '
            ' TODO: Add any constructor code after InitializeComponent call
            '
            Me.ActiveControl = GIS
            Me.toolTip1.SetToolTip(Me.cmbLayer, "Select shape for editing")
            Me.toolTip2.SetToolTip(Me.cmbSnap, "Snap to layer")
            ''Me.GIS.Editor.ShowFast = False
            menuPos = GisUtils.GisPoint(0, 0)
            info = New InfoForm()
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
            Me.toolBar1 = New System.Windows.Forms.ToolBar()
            Me.btnSave = New System.Windows.Forms.ToolBarButton()
            Me.btnPrint = New System.Windows.Forms.ToolBarButton()
            Me.toolBarButton1 = New System.Windows.Forms.ToolBarButton()
            Me.btnFullExtent = New System.Windows.Forms.ToolBarButton()
            Me.toolBarButton2 = New System.Windows.Forms.ToolBarButton()
            Me.btnZoom = New System.Windows.Forms.ToolBarButton()
            Me.btnDrag = New System.Windows.Forms.ToolBarButton()
            Me.btnSelect = New System.Windows.Forms.ToolBarButton()
            Me.btnEdit = New System.Windows.Forms.ToolBarButton()
            Me.toolBarButton3 = New System.Windows.Forms.ToolBarButton()
            Me.imageList1 = New System.Windows.Forms.ImageList(Me.components)
            Me.statusBar1 = New System.Windows.Forms.StatusBar()
            Me.statusBarPanel1 = New System.Windows.Forms.StatusBarPanel()
            Me.statusBarPanel2 = New System.Windows.Forms.StatusBarPanel()
            Me.statusBarPanel3 = New System.Windows.Forms.StatusBarPanel()
            Me.statusBarPanel4 = New System.Windows.Forms.StatusBarPanel()
            Me.GIS = New AxTatukGIS_XDK11.AxTGIS_ViewerWnd()
            Me.contextMenu1 = New System.Windows.Forms.ContextMenu()
            Me.mnuAddPart = New System.Windows.Forms.MenuItem()
            Me.mnuDeletePart = New System.Windows.Forms.MenuItem()
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.cmbSnap = New System.Windows.Forms.ComboBox()
            Me.toolBar4 = New System.Windows.Forms.ToolBar()
            Me.btnNewStyle = New System.Windows.Forms.ToolBarButton()
            Me.panel4 = New System.Windows.Forms.Panel()
            Me.toolBar3 = New System.Windows.Forms.ToolBar()
            Me.btnAddShape = New System.Windows.Forms.ToolBarButton()
            Me.toolBarButton4 = New System.Windows.Forms.ToolBarButton()
            Me.btnUndo = New System.Windows.Forms.ToolBarButton()
            Me.btnRedo = New System.Windows.Forms.ToolBarButton()
            Me.btnRevert = New System.Windows.Forms.ToolBarButton()
            Me.btnDelete = New System.Windows.Forms.ToolBarButton()
            Me.btnWinding = New System.Windows.Forms.ToolBarButton()
            Me.toolBarButton5 = New System.Windows.Forms.ToolBarButton()
            Me.btnShowInfo = New System.Windows.Forms.ToolBarButton()
            Me.btnAutoCenter = New System.Windows.Forms.ToolBarButton()
            Me.toolBarButton6 = New System.Windows.Forms.ToolBarButton()
            Me.panel3 = New System.Windows.Forms.Panel()
            Me.cmbLayer = New System.Windows.Forms.ComboBox()
            Me.toolBar2 = New System.Windows.Forms.ToolBar()
            Me.panel2 = New System.Windows.Forms.Panel()
            Me.toolTip1 = New System.Windows.Forms.ToolTip(Me.components)
            Me.toolTip2 = New System.Windows.Forms.ToolTip(Me.components)
            Me.printDialog1 = New System.Windows.Forms.PrintDialog()
            Me.printDocument1 = New System.Drawing.Printing.PrintDocument()
            CType(Me.statusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.statusBarPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.statusBarPanel3, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.statusBarPanel4, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.panel1.SuspendLayout()
            Me.panel4.SuspendLayout()
            Me.panel3.SuspendLayout()
            Me.panel2.SuspendLayout()
            Me.SuspendLayout()
            '
            'toolBar1
            '
            Me.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
            Me.toolBar1.AutoSize = False
            Me.toolBar1.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.btnSave, Me.btnPrint, Me.toolBarButton1, Me.btnFullExtent, Me.toolBarButton2, Me.btnZoom, Me.btnDrag, Me.btnSelect, Me.btnEdit, Me.toolBarButton3})
            Me.toolBar1.ButtonSize = New System.Drawing.Size(23, 22)
            Me.toolBar1.DropDownArrows = True
            Me.toolBar1.ImageList = Me.imageList1
            Me.toolBar1.Location = New System.Drawing.Point(0, 0)
            Me.toolBar1.Name = "toolBar1"
            Me.toolBar1.ShowToolTips = True
            Me.toolBar1.Size = New System.Drawing.Size(185, 24)
            Me.toolBar1.TabIndex = 0
            '
            'btnSave
            '
            Me.btnSave.ImageIndex = 0
            Me.btnSave.Name = "btnSave"
            Me.btnSave.ToolTipText = "Save"
            '
            'btnPrint
            '
            Me.btnPrint.ImageIndex = 1
            Me.btnPrint.Name = "btnPrint"
            Me.btnPrint.ToolTipText = "Print"
            '
            'toolBarButton1
            '
            Me.toolBarButton1.Name = "toolBarButton1"
            Me.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
            '
            'btnFullExtent
            '
            Me.btnFullExtent.ImageIndex = 2
            Me.btnFullExtent.Name = "btnFullExtent"
            Me.btnFullExtent.ToolTipText = "Full Extent"
            '
            'toolBarButton2
            '
            Me.toolBarButton2.Name = "toolBarButton2"
            Me.toolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
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
            'btnSelect
            '
            Me.btnSelect.ImageIndex = 5
            Me.btnSelect.Name = "btnSelect"
            Me.btnSelect.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton
            Me.btnSelect.ToolTipText = "Select"
            '
            'btnEdit
            '
            Me.btnEdit.Enabled = False
            Me.btnEdit.ImageIndex = 6
            Me.btnEdit.Name = "btnEdit"
            Me.btnEdit.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton
            Me.btnEdit.ToolTipText = "Edit"
            '
            'toolBarButton3
            '
            Me.toolBarButton3.Name = "toolBarButton3"
            Me.toolBarButton3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
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
            Me.imageList1.Images.SetKeyName(12, "")
            Me.imageList1.Images.SetKeyName(13, "")
            Me.imageList1.Images.SetKeyName(14, "")
            Me.imageList1.Images.SetKeyName(15, "FullScreen.bmp")
            '
            'statusBar1
            '
            Me.statusBar1.Location = New System.Drawing.Point(0, 445)
            Me.statusBar1.Name = "statusBar1"
            Me.statusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.statusBarPanel1, Me.statusBarPanel2, Me.statusBarPanel3, Me.statusBarPanel4})
            Me.statusBar1.ShowPanels = True
            Me.statusBar1.Size = New System.Drawing.Size(631, 19)
            Me.statusBar1.TabIndex = 1
            '
            'statusBarPanel1
            '
            Me.statusBarPanel1.Name = "statusBarPanel1"
            '
            'statusBarPanel2
            '
            Me.statusBarPanel2.Name = "statusBarPanel2"
            '
            'statusBarPanel3
            '
            Me.statusBarPanel3.Name = "statusBarPanel3"
            '
            'statusBarPanel4
            '
            Me.statusBarPanel4.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
            Me.statusBarPanel4.Name = "statusBarPanel4"
            Me.statusBarPanel4.Width = 314
            '
            'GIS
            '
            Me.GIS.Cursor = System.Windows.Forms.Cursors.Default
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.Enabled = True
            Me.GIS.Location = New System.Drawing.Point(0, 24)
            Me.GIS.Name = "GIS"
            Me.GIS.OcxState = CType(resources.GetObject("GIS.OcxState"), System.Windows.Forms.AxHost.State)
            Me.GIS.Size = New System.Drawing.Size(631, 421)
            Me.GIS.TabIndex = 1
            '
            'contextMenu1
            '
            Me.contextMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuAddPart, Me.mnuDeletePart})
            '
            'mnuAddPart
            '
            Me.mnuAddPart.Index = 0
            Me.mnuAddPart.Text = "&Add part"
            '
            'mnuDeletePart
            '
            Me.mnuDeletePart.Index = 1
            Me.mnuDeletePart.Text = "&Delete part"
            '
            'panel1
            '
            Me.panel1.Controls.Add(Me.cmbSnap)
            Me.panel1.Controls.Add(Me.toolBar4)
            Me.panel1.Controls.Add(Me.panel4)
            Me.panel1.Controls.Add(Me.panel3)
            Me.panel1.Controls.Add(Me.panel2)
            Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.panel1.Location = New System.Drawing.Point(0, 0)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(631, 24)
            Me.panel1.TabIndex = 2
            '
            'cmbSnap
            '
            Me.cmbSnap.Cursor = System.Windows.Forms.Cursors.Hand
            Me.cmbSnap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbSnap.Location = New System.Drawing.Point(508, 2)
            Me.cmbSnap.Name = "cmbSnap"
            Me.cmbSnap.Size = New System.Drawing.Size(85, 21)
            Me.cmbSnap.TabIndex = 4
            '
            'toolBar4
            '
            Me.toolBar4.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
            Me.toolBar4.AutoSize = False
            Me.toolBar4.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.btnNewStyle})
            Me.toolBar4.DropDownArrows = True
            Me.toolBar4.ImageList = Me.imageList1
            Me.toolBar4.Location = New System.Drawing.Point(478, 0)
            Me.toolBar4.Name = "toolBar4"
            Me.toolBar4.ShowToolTips = True
            Me.toolBar4.Size = New System.Drawing.Size(153, 24)
            Me.toolBar4.TabIndex = 3
            '
            'btnNewStyle
            '
            Me.btnNewStyle.ImageIndex = 15
            Me.btnNewStyle.Name = "btnNewStyle"
            Me.btnNewStyle.ToolTipText = "New style"
            '
            'panel4
            '
            Me.panel4.Controls.Add(Me.toolBar3)
            Me.panel4.Dock = System.Windows.Forms.DockStyle.Left
            Me.panel4.Location = New System.Drawing.Point(270, 0)
            Me.panel4.Name = "panel4"
            Me.panel4.Size = New System.Drawing.Size(208, 24)
            Me.panel4.TabIndex = 2
            '
            'toolBar3
            '
            Me.toolBar3.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
            Me.toolBar3.AutoSize = False
            Me.toolBar3.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.btnAddShape, Me.toolBarButton4, Me.btnUndo, Me.btnRedo, Me.btnRevert, Me.btnDelete, Me.btnWinding, Me.toolBarButton5, Me.btnShowInfo, Me.btnAutoCenter, Me.toolBarButton6})
            Me.toolBar3.ButtonSize = New System.Drawing.Size(23, 22)
            Me.toolBar3.DropDownArrows = True
            Me.toolBar3.ImageList = Me.imageList1
            Me.toolBar3.Location = New System.Drawing.Point(0, 0)
            Me.toolBar3.Name = "toolBar3"
            Me.toolBar3.ShowToolTips = True
            Me.toolBar3.Size = New System.Drawing.Size(208, 24)
            Me.toolBar3.TabIndex = 0
            '
            'btnAddShape
            '
            Me.btnAddShape.Enabled = False
            Me.btnAddShape.ImageIndex = 7
            Me.btnAddShape.Name = "btnAddShape"
            Me.btnAddShape.ToolTipText = "Add shape"
            '
            'toolBarButton4
            '
            Me.toolBarButton4.Name = "toolBarButton4"
            Me.toolBarButton4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
            '
            'btnUndo
            '
            Me.btnUndo.Enabled = False
            Me.btnUndo.ImageIndex = 8
            Me.btnUndo.Name = "btnUndo"
            Me.btnUndo.ToolTipText = "Undo"
            '
            'btnRedo
            '
            Me.btnRedo.Enabled = False
            Me.btnRedo.ImageIndex = 9
            Me.btnRedo.Name = "btnRedo"
            Me.btnRedo.ToolTipText = "Redo"
            '
            'btnRevert
            '
            Me.btnRevert.Enabled = False
            Me.btnRevert.ImageIndex = 10
            Me.btnRevert.Name = "btnRevert"
            Me.btnRevert.ToolTipText = "Revert shape"
            '
            'btnDelete
            '
            Me.btnDelete.Enabled = False
            Me.btnDelete.ImageIndex = 11
            Me.btnDelete.Name = "btnDelete"
            Me.btnDelete.ToolTipText = "Delete shape"
            '
            'btnWinding
            '
            Me.btnWinding.Enabled = False
            Me.btnWinding.ImageIndex = 12
            Me.btnWinding.Name = "btnWinding"
            Me.btnWinding.ToolTipText = "Change winding"
            '
            'toolBarButton5
            '
            Me.toolBarButton5.Name = "toolBarButton5"
            Me.toolBarButton5.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
            '
            'btnShowInfo
            '
            Me.btnShowInfo.ImageIndex = 13
            Me.btnShowInfo.Name = "btnShowInfo"
            Me.btnShowInfo.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton
            Me.btnShowInfo.ToolTipText = "Show info window"
            '
            'btnAutoCenter
            '
            Me.btnAutoCenter.ImageIndex = 14
            Me.btnAutoCenter.Name = "btnAutoCenter"
            Me.btnAutoCenter.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton
            Me.btnAutoCenter.ToolTipText = "Autoscroll"
            '
            'toolBarButton6
            '
            Me.toolBarButton6.Name = "toolBarButton6"
            Me.toolBarButton6.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
            '
            'panel3
            '
            Me.panel3.Controls.Add(Me.cmbLayer)
            Me.panel3.Controls.Add(Me.toolBar2)
            Me.panel3.Dock = System.Windows.Forms.DockStyle.Left
            Me.panel3.Location = New System.Drawing.Point(185, 0)
            Me.panel3.Name = "panel3"
            Me.panel3.Size = New System.Drawing.Size(85, 24)
            Me.panel3.TabIndex = 1
            '
            'cmbLayer
            '
            Me.cmbLayer.Cursor = System.Windows.Forms.Cursors.Hand
            Me.cmbLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbLayer.Location = New System.Drawing.Point(0, 2)
            Me.cmbLayer.Name = "cmbLayer"
            Me.cmbLayer.Size = New System.Drawing.Size(85, 21)
            Me.cmbLayer.TabIndex = 1
            '
            'toolBar2
            '
            Me.toolBar2.AutoSize = False
            Me.toolBar2.DropDownArrows = True
            Me.toolBar2.Location = New System.Drawing.Point(0, 0)
            Me.toolBar2.Name = "toolBar2"
            Me.toolBar2.ShowToolTips = True
            Me.toolBar2.Size = New System.Drawing.Size(85, 24)
            Me.toolBar2.TabIndex = 0
            '
            'panel2
            '
            Me.panel2.Controls.Add(Me.toolBar1)
            Me.panel2.Dock = System.Windows.Forms.DockStyle.Left
            Me.panel2.Location = New System.Drawing.Point(0, 0)
            Me.panel2.Name = "panel2"
            Me.panel2.Size = New System.Drawing.Size(185, 24)
            Me.panel2.TabIndex = 0
            '
            'printDialog1
            '
            Me.printDialog1.Document = Me.printDocument1
            '
            'MainForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(631, 464)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.panel1)
            Me.Controls.Add(Me.statusBar1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "MainForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - SimpleEdit"
            CType(Me.statusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.statusBarPanel2, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.statusBarPanel3, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.statusBarPanel4, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).EndInit()
            Me.panel1.ResumeLayout(False)
            Me.panel4.ResumeLayout(False)
            Me.panel3.ResumeLayout(False)
            Me.panel2.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
#End Region

        Dim GisUtils As New TGIS_Utils()

        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        <STAThread()>
        Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New MainForm())
        End Sub

        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim ll As TGIS_Layer
            Dim i As Integer

            statusBar1.Panels(3).Text = "See: www.tatukgis.com"
            GIS.Open(GisUtils.GisSamplesDataDirDownload() & "\samples\SimpleEdit\simpleedit.ttkproject")

            cmbLayer.Items.Add("Show all")
            i = 0
            Do While i < GIS.Items.Count
                ll = CType(GIS.Items.Item(i), TGIS_LayerAbstract)
                If TypeOf ll Is TGIS_LayerVector Then
                    cmbLayer.Items.Add(ll.Name)
                End If
                i += 1
            Loop

            If GIS.Items.Count > 0 Then
                cmbLayer.SelectedIndex = 0
            End If

            cmbSnap.Items.Add("No snapping")
            i = 0
            Do While i < GIS.Items.Count
                ll = CType(GIS.Items.Item(i), TGIS_LayerAbstract)
                If TypeOf ll Is TGIS_LayerVector Then
                    cmbSnap.Items.Add(ll.Name)
                End If
                i += 1
            Loop

            If GIS.Items.Count > 0 Then
                cmbSnap.SelectedIndex = 0
            End If

            btnZoom.Pushed = True
            btnZoomClick()
        End Sub


        Private Sub WinForm_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
            endEdit()
            btnSelectClick()

            If (Not GIS.MustSave()) Then
                Return
            End If

            If MessageBox.Show("Save all unsaved work ?", "TatukGIS", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                GIS.SaveAll()
            End If
        End Sub

        Private Sub endEdit()
            btnEdit.Enabled = False
            btnRevert.Enabled = False
            btnDelete.Enabled = False
            btnWinding.Enabled = False

            editLayer = Nothing
            GIS.Editor.EndEdit()

            If btnShowInfo.Pushed Then
                info.ShowInfo(Nothing)
            End If
        End Sub

        Private Sub toolBar1_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles toolBar1.ButtonClick
            Select Case toolBar1.Buttons.IndexOf(e.Button)
                Case 0
                    ' Save
                    btnSaveClick()
                Case 1
                    ' Print
                    btnPrintClick()
                Case 3
                    ' Full Extent
                    btnFullExtentClick()
                Case 5
                    ' Zoom
                    btnZoomClick()
                Case 6
                    ' Drag
                    btnDragClick()
                Case 7
                    ' Select
                    btnSelectClick()
                Case 8
                    ' Edit
                    btnEditClick()
            End Select
        End Sub

        Private Sub btnSaveClick()
            endEdit()
            btnSelectClick()
            GIS.SaveAll()
        End Sub

        Private Sub btnPrintClick()
            Dim manager As TGIS_PrintManager = New TGIS_PrintManager()
            printDialog1.Document = printDocument1
            If (printDialog1.ShowDialog() = DialogResult.OK) Then
                manager.Footer = "Printed by TatukGIS. See our web page: www.tatukgis.com"
                manager.Subtitle = DateTime.Now.ToString()
                Dim p As TGIS_Printer = New TGIS_Printer()
                manager.Print_2(GIS.GetOcx(), p)
            End If
        End Sub

        Private Sub btnFullExtentClick()
            GIS.FullExtent()
        End Sub

        Private Sub btnZoomClick()
            btnDrag.Pushed = False
            btnSelect.Pushed = False
            btnEdit.Pushed = False

            endEdit()
            GIS.Mode = TGIS_ViewerMode.Zoom
        End Sub

        Private Sub btnDragClick()
            btnZoom.Pushed = False
            btnSelect.Pushed = False
            btnEdit.Pushed = False

            endEdit()
            GIS.Mode = TGIS_ViewerMode.Drag
        End Sub

        Private Sub btnSelectClick()
            btnZoom.Pushed = False
            btnDrag.Pushed = False
            btnEdit.Pushed = False

            endEdit()
            GIS.Mode = TGIS_ViewerMode.Select
        End Sub

        Private Sub btnEditClick()
            btnZoom.Pushed = False
            btnDrag.Pushed = False
            btnSelect.Pushed = False

            btnEdit.Enabled = True
            btnRevert.Enabled = True
            btnDelete.Enabled = True
            btnWinding.Enabled = True

            btnEdit.Pushed = True
            If GIS.Mode = TGIS_ViewerMode.Edit Then
                Return
            End If
            endEdit()
            GIS.Mode = TGIS_ViewerMode.Select
        End Sub

        Private Sub cmbLayer_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbLayer.SelectedIndexChanged
            Dim i As Integer
            Dim ll As TGIS_LayerAbstract

            endEdit()
            GIS.Focus()
            btnSelectClick()

            i = 1
            Do While i < cmbLayer.Items.Count
                ll = GIS.Get(CStr(cmbLayer.Items(i)))
                If cmbLayer.SelectedIndex = 0 Then
                    ll.Active = True
                Else
                    ll.Active = (i = cmbLayer.SelectedIndex)
                End If
                i += 1
            Loop

            btnAddShape.Enabled = (cmbLayer.SelectedIndex <> 0)

            GIS.Update()
        End Sub

        Private Sub toolBar3_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles toolBar3.ButtonClick
            Select Case toolBar3.Buttons.IndexOf(e.Button)
                Case 0
                    ' Add shape
                    btnAddShapeClick()
                Case 2
                    ' Undo
                    btnUndoClick()
                Case 3
                    ' Redo
                    btnRedoClick()
                Case 4
                    ' Revert
                    btnRevertClick()
                Case 5
                    ' Delete
                    btnDeleteClick()
                Case 6
                    ' Winding
                    btnWindingClick()
                Case 8
                    ' Show info
                    btnShowInfoClick()
                Case 9
                    ' AutoCenter
                    btnAutoCenterClick()
            End Select
        End Sub

        Private Sub btnAddShapeClick()
            endEdit()
            GIS.Mode = TGIS_ViewerMode.Edit
            editLayer = GIS.Get(CStr(cmbLayer.Items(cmbLayer.SelectedIndex)))
        End Sub

        Private Sub btnUndoClick()
            GIS.Editor.Undo()
            If GIS.AutoCenter Then
                GIS.Zoom = GIS.Zoom
            End If
        End Sub

        Private Sub btnRedoClick()
            GIS.Editor.Redo()
            If GIS.AutoCenter Then
                GIS.Zoom = GIS.Zoom
            End If
        End Sub

        Private Sub btnRevertClick()
            GIS.Editor.RevertShape()
            If btnShowInfo.Pushed Then
                info.ShowInfo(CType(GIS.Editor.CurrentShape, TGIS_Shape))
            End If
        End Sub

        Private Sub btnDeleteClick()
            GIS.Editor.DeleteShape()
            btnSelectClick()
        End Sub

        Private Sub btnWindingClick()
            GIS.Editor.ChangeWinding()
        End Sub

        Private Sub btnShowInfoClick()
            If btnShowInfo.Pushed Then
                info.ShowInfo(CType(GIS.Editor.CurrentShape, TGIS_Shape))
                info.Show()
            Else
                info.Hide()
            End If
        End Sub

        Private Sub btnAutoCenterClick()
            GIS.AutoCenter = btnAutoCenter.Pushed
        End Sub

        Private Sub cmbSnap_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSnap.SelectedIndexChanged
            If cmbSnap.SelectedIndex > 0 Then
                GIS.Editor.SnapLayer = GIS.Get(CStr(cmbSnap.Items(cmbSnap.SelectedIndex)))
            Else
                GIS.Editor.SnapLayer = Nothing
            End If
        End Sub

        Private Sub GIS_MouseMove(ByVal sender As Object, ByVal e As AxTatukGIS_XDK11.ITGIS_ViewerWndEvents_MouseMoveEvent) Handles GIS.MouseMoveEvent
            Dim ptg As TGIS_Point

            If GIS.IsEmpty Then
                Return
            End If

            ptg = GIS.ScreenToMap(GisUtils.Point(e.X, e.Y))
            statusBar1.Panels(1).Text = String.Format("x: {0:F4}", ptg.X)
            statusBar1.Panels(2).Text = String.Format("y: {0:F4}", ptg.Y)
        End Sub


        Private Sub GIS_Paint(ByVal sender As Object, ByVal e As AxTatukGIS_XDK11.ITGIS_ViewerWndEvents_AfterPaintEventEvent) Handles GIS.AfterPaintEvent
            statusBar1.Panels(0).Text = String.Format("zoom: {0:F4}", GIS.Zoom)
        End Sub

        Private Sub GIS_EditorChanged(ByVal sender As Object, ByVal e As AxTatukGIS_XDK11.ITGIS_ViewerWndEvents_EditorChangeEventEvent) Handles GIS.EditorChangeEvent
            btnUndo.Enabled = GIS.Editor.CanUndo
            btnRedo.Enabled = GIS.Editor.CanRedo
        End Sub

        Private Sub mnuAddPart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuAddPart.Click
            GIS.Editor.CreatePart(GisUtils.GisPoint3DFrom2D(menuPos))
        End Sub

        Private Sub mnuDeletePart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuDeletePart.Click
            GIS.Editor.DeletePart()
        End Sub

        Private Sub toolBar1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles toolBar1.MouseMove
            Dim p As Point = New Point(e.X, e.Y)

            If toolBar1.Buttons(0).Rectangle.Contains(p) OrElse toolBar1.Buttons(1).Rectangle.Contains(p) OrElse toolBar1.Buttons(3).Rectangle.Contains(p) OrElse toolBar1.Buttons(5).Rectangle.Contains(p) OrElse toolBar1.Buttons(6).Rectangle.Contains(p) OrElse toolBar1.Buttons(7).Rectangle.Contains(p) OrElse toolBar1.Buttons(8).Rectangle.Contains(p) Then
                toolBar1.Cursor = Cursors.Hand
            Else
                toolBar1.Cursor = Cursors.Default
            End If
        End Sub

        Private Sub toolBar3_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles toolBar3.MouseMove
            Dim p As Point = New Point(e.X, e.Y)

            If toolBar3.Buttons(0).Rectangle.Contains(p) OrElse toolBar3.Buttons(2).Rectangle.Contains(p) OrElse toolBar3.Buttons(3).Rectangle.Contains(p) OrElse toolBar3.Buttons(4).Rectangle.Contains(p) OrElse toolBar3.Buttons(5).Rectangle.Contains(p) OrElse toolBar3.Buttons(6).Rectangle.Contains(p) OrElse toolBar3.Buttons(8).Rectangle.Contains(p) OrElse toolBar3.Buttons(9).Rectangle.Contains(p) Then
                toolBar3.Cursor = Cursors.Hand
            Else
                toolBar3.Cursor = Cursors.Default
            End If
        End Sub

        Private Sub GIS_MouseUp(ByVal sender As Object, ByVal e As AxTatukGIS_XDK11.ITGIS_ViewerWndEvents_MouseUpEvent) Handles GIS.MouseUpEvent
            Dim ptg As TGIS_Point
            Dim shp As TGIS_Shape
            Dim dist As Double
            Dim part As Integer
            Dim proj As TGIS_Point

            If GIS.IsEmpty Then
                Return
            End If
            If GIS.InPaint Then
                Return
            End If

            ptg = GIS.ScreenToMap(GisUtils.Point(e.X, e.Y))

            Select Case e.Button
                Case TMouseButton.mbRight
                    If GIS.Mode = TGIS_ViewerMode.Edit Then
                        menuPos = ptg
                        contextMenu1.Show(GIS, New Point(e.X, e.Y))
                    End If
                Case TMouseButton.mbLeft
                    If GIS.Mode = TGIS_ViewerMode.Edit Then
                        If editLayer Is Nothing Then
                            Return
                        Else
                            GIS.Editor.CreateShape(editLayer, ptg, TGIS_ShapeType.Unknown)
                            If cmbSnap.SelectedIndex > 0 Then
                                GIS.Editor.SnapLayer = GIS.Get(CStr(cmbSnap.Items(cmbSnap.SelectedIndex)))
                            Else
                                GIS.Editor.SnapLayer = Nothing
                            End If

                            If btnShowInfo.Pushed Then
                                info.ShowInfo(CType(GIS.Editor.CurrentShape, TGIS_Shape))
                            End If
                            editLayer = Nothing
                            btnEditClick()
                        End If
                    ElseIf GIS.Mode = TGIS_ViewerMode.Select Then
                        endEdit()
                        If btnShowInfo.Pushed Then
                            info.ShowInfo(Nothing)
                        End If

                        shp = CType(GIS.Locate(ptg, 5 / GIS.Zoom), TGIS_Shape)
                        If shp Is Nothing Then
                            Return
                        End If

                        If cmbLayer.SelectedIndex <> 0 Then
                            If Not shp.Layer Is GIS.Get(CStr(cmbLayer.Items(cmbLayer.SelectedIndex))) Then
                                Return
                            End If
                        End If

                        dist = 0
                        part = 0
                        proj = GisUtils.GisPoint(0, 0)
                        shp = shp.Layer.LocateEx(ptg, 5 / GIS.Zoom, -1, dist, part, proj)
                        If shp Is Nothing Then
                            Return
                        End If

                        GIS.Editor.EditShape(shp, part)
                        If cmbSnap.SelectedIndex > 0 Then
                            GIS.Editor.SnapLayer = CObj(GIS.Get(CStr(cmbSnap.Items(cmbSnap.SelectedIndex))))
                        Else
                            GIS.Editor.SnapLayer = Nothing
                        End If

                        GIS.Mode = TGIS_ViewerMode.Edit

                        If btnShowInfo.Pushed Then
                            info.ShowInfo(shp)
                        End If
                        btnEditClick()
                    End If
            End Select
        End Sub

        Private Sub toolBar4_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles toolBar4.ButtonClick
            Select Case toolBar4.Buttons.IndexOf(e.Button)
                Case 0
                    ' Change editing visual style
                    btnNewStyleClick()
            End Select
        End Sub

        Private Sub btnNewStyleClick()
            GIS.Editor.EditingLinesStyle.PenStyle = TGIS_PenStyle.Dash
            GIS.Editor.EditingLinesStyle.PenColor = (New TGIS_Color).Lime

            GIS.Editor.EditingPointsStyle.PointsFont.Name = "Verdana"
            GIS.Editor.EditingPointsStyle.PointsFont.Size = 8
            GIS.Editor.EditingPointsStyle.PointsFont.Color = (New TGIS_Color).White
            GIS.Editor.EditingPointsStyle.PointsBackground = (New TGIS_Color).Green

            GIS.Editor.EditingPointsStyle.ActivePoints.BrushStyle = TGIS_BrushStyle.Solid
            GIS.Editor.EditingPointsStyle.ActivePoints.BrushColor = (New TGIS_Color).Green
            GIS.Editor.EditingPointsStyle.ActivePoints.PenStyle = TGIS_PenStyle.Solid
            GIS.Editor.EditingPointsStyle.ActivePoints.PenColor = (New TGIS_Color).Black

            GIS.Editor.EditingPointsStyle.InactivePoints.BrushStyle = TGIS_BrushStyle.Solid
            GIS.Editor.EditingPointsStyle.InactivePoints.BrushColor = (New TGIS_Color).Blue
            GIS.Editor.EditingPointsStyle.InactivePoints.PenStyle = TGIS_PenStyle.Solid
            GIS.Editor.EditingPointsStyle.InactivePoints.PenColor = (New TGIS_Color).Black

            GIS.Editor.EditingPointsStyle.SelectedPoints.BrushStyle = TGIS_BrushStyle.Solid
            GIS.Editor.EditingPointsStyle.SelectedPoints.BrushColor = (New TGIS_Color).Red
            GIS.Editor.EditingPointsStyle.SelectedPoints.PenStyle = TGIS_PenStyle.Solid
            GIS.Editor.EditingPointsStyle.SelectedPoints.PenColor = (New TGIS_Color).Black

            GIS.Editor.EditingPointsStyle.Points3D.BrushStyle = TGIS_BrushStyle.Solid
            GIS.Editor.EditingPointsStyle.Points3D.BrushColor = (New TGIS_Color).Purple
            GIS.Editor.EditingPointsStyle.Points3D.PenStyle = TGIS_PenStyle.Solid
            GIS.Editor.EditingPointsStyle.Points3D.PenColor = (New TGIS_Color).Olive

            GIS.Editor.EditingPointsStyle.SnappingPoints.BrushStyle = TGIS_BrushStyle.Solid
            GIS.Editor.EditingPointsStyle.SnappingPoints.BrushColor = (New TGIS_Color).Red
            GIS.Editor.EditingPointsStyle.SnappingPoints.PenStyle = TGIS_PenStyle.Solid
            GIS.Editor.EditingPointsStyle.SnappingPoints.PenColor = (New TGIS_Color).Yellow

            If GIS.Editor.InEdit Then
                GIS.Editor.RefreshShape()
            End If
        End Sub
    End Class
End Namespace
