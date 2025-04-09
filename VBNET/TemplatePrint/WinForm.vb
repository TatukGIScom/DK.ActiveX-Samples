Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS_XDK11

Namespace TemplatePrint
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
        Private statusBarPanel3 As System.Windows.Forms.StatusBarPanel
        Private GIS_ControlLegend As AxTatukGIS_XDK11.AxTGIS_ControlLegend
        Private splitter1 As System.Windows.Forms.Splitter
        Private WithEvents GIS As AxTatukGIS_XDK11.AxTGIS_ViewerWnd
        Private GIS_ControlScale As AxTatukGIS_XDK11.AxTGIS_ControlScale
        Friend WithEvents GIS_ControlNorthArrow As AxTatukGIS_XDK11.AxTGIS_ControlNorthArrow
        Private GIS_ControlPrintPreviewSimple As TGIS_ControlPrintPreviewSimple
        Private template As TGIS_TemplatePrint
        Private manager As TGIS_PrintTemplate

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
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.button1 = New System.Windows.Forms.Button()
            Me.button2 = New System.Windows.Forms.Button()
            Me.button3 = New System.Windows.Forms.Button()
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
            Me.statusBarPanel3 = New System.Windows.Forms.StatusBarPanel()
            Me.splitter1 = New System.Windows.Forms.Splitter()
            Me.GIS = New AxTatukGIS_XDK11.AxTGIS_ViewerWnd()
            Me.GIS_ControlNorthArrow = New AxTatukGIS_XDK11.AxTGIS_ControlNorthArrow()
            Me.GIS_ControlScale = New AxTatukGIS_XDK11.AxTGIS_ControlScale()
            Me.GIS_ControlLegend = New AxTatukGIS_XDK11.AxTGIS_ControlLegend()
            Me.panel1.SuspendLayout()
            CType(Me.statusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.statusBarPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.statusBarPanel3, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.GIS.SuspendLayout()
            CType(Me.GIS_ControlNorthArrow, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.GIS_ControlScale, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.GIS_ControlLegend, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'panel1
            '
            Me.panel1.Controls.Add(Me.button3)
            Me.panel1.Controls.Add(Me.button2)
            Me.panel1.Controls.Add(Me.button1)
            Me.panel1.Controls.Add(Me.toolBar1)
            Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.panel1.Location = New System.Drawing.Point(0, 0)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(618, 28)
            Me.panel1.TabIndex = 0
            '
            'button1
            '
            Me.button1.Location = New System.Drawing.Point(85, 1)
            Me.button1.Name = "button1"
            Me.button1.Size = New System.Drawing.Size(75, 25)
            Me.button1.TabIndex = 1
            Me.button1.Text = "Print"
            '
            'button2
            '
            Me.button2.Location = New System.Drawing.Point(166, 1)
            Me.button2.Name = "button2"
            Me.button2.Size = New System.Drawing.Size(75, 25)
            Me.button2.TabIndex = 2
            Me.button2.Text = "Design"
            '
            'button3
            '
            Me.button3.Location = New System.Drawing.Point(247, 1)
            Me.button3.Name = "button3"
            Me.button3.Size = New System.Drawing.Size(75, 25)
            Me.button3.TabIndex = 3
            Me.button3.Text = "Change"
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
            Me.toolBar1.Size = New System.Drawing.Size(618, 28)
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
            Me.statusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.statusBarPanel1, Me.statusBarPanel2, Me.statusBarPanel3})
            Me.statusBar1.ShowPanels = True
            Me.statusBar1.Size = New System.Drawing.Size(618, 19)
            Me.statusBar1.TabIndex = 1
            Me.statusBar1.Text = "statusBar1"
            '
            'statusBarPanel1
            '
            Me.statusBarPanel1.Alignment = System.Windows.Forms.HorizontalAlignment.Center
            Me.statusBarPanel1.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.Raised
            Me.statusBarPanel1.Name = "statusBarPanel1"
            Me.statusBarPanel1.Text = "Scale :"
            Me.statusBarPanel1.Width = 50
            '
            'statusBarPanel2
            '
            Me.statusBarPanel2.Name = "statusBarPanel2"
            Me.statusBarPanel2.Text = "Value"
            Me.statusBarPanel2.Width = 98
            '
            'statusBarPanel3
            '
            Me.statusBarPanel3.Name = "statusBarPanel3"
            Me.statusBarPanel3.Text = "Template"
            Me.statusBarPanel3.Width = 551
            '
            'splitter1
            '
            Me.splitter1.Location = New System.Drawing.Point(180, 28)
            Me.splitter1.Name = "splitter1"
            Me.splitter1.Size = New System.Drawing.Size(3, 419)
            Me.splitter1.TabIndex = 3
            Me.splitter1.TabStop = False
            '
            'GIS
            '
            Me.GIS.Controls.Add(Me.GIS_ControlNorthArrow)
            Me.GIS.Controls.Add(Me.GIS_ControlScale)
            Me.GIS.Cursor = System.Windows.Forms.Cursors.Default
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.Enabled = True
            Me.GIS.Location = New System.Drawing.Point(183, 28)
            Me.GIS.Name = "GIS"
            Me.GIS.OcxState = CType(resources.GetObject("GIS.OcxState"), System.Windows.Forms.AxHost.State)
            Me.GIS.Size = New System.Drawing.Size(435, 419)
            Me.GIS.TabIndex = 4
            '
            'GIS_ControlNorthArrow
            '
            Me.GIS_ControlNorthArrow.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GIS_ControlNorthArrow.Enabled = True
            Me.GIS_ControlNorthArrow.Location = New System.Drawing.Point(340, 4)
            Me.GIS_ControlNorthArrow.Name = "GIS_ControlNorthArrow"
            Me.GIS_ControlNorthArrow.OcxState = CType(resources.GetObject("GIS_ControlNorthArrow.OcxState"), System.Windows.Forms.AxHost.State)
            Me.GIS_ControlNorthArrow.Size = New System.Drawing.Size(91, 80)
            Me.GIS_ControlNorthArrow.TabIndex = 1
            '
            'GIS_ControlScale
            '
            Me.GIS_ControlScale.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GIS_ControlScale.BackColor = System.Drawing.SystemColors.Window
            Me.GIS_ControlScale.Enabled = True
            Me.GIS_ControlScale.Location = New System.Drawing.Point(270, 380)
            Me.GIS_ControlScale.Name = "GIS_ControlScale"
            Me.GIS_ControlScale.OcxState = CType(resources.GetObject("GIS_ControlScale.OcxState"), System.Windows.Forms.AxHost.State)
            Me.GIS_ControlScale.Size = New System.Drawing.Size(161, 31)
            Me.GIS_ControlScale.TabIndex = 0
            '
            'GIS_ControlLegend
            '
            Me.GIS_ControlLegend.Dock = System.Windows.Forms.DockStyle.Left
            Me.GIS_ControlLegend.Enabled = True
            Me.GIS_ControlLegend.Location = New System.Drawing.Point(0, 28)
            Me.GIS_ControlLegend.Name = "GIS_ControlLegend"
            Me.GIS_ControlLegend.OcxState = CType(resources.GetObject("GIS_ControlLegend.OcxState"), System.Windows.Forms.AxHost.State)
            Me.GIS_ControlLegend.Size = New System.Drawing.Size(180, 419)
            Me.GIS_ControlLegend.TabIndex = 2
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(618, 466)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.splitter1)
            Me.Controls.Add(Me.GIS_ControlLegend)
            Me.Controls.Add(Me.statusBar1)
            Me.Controls.Add(Me.panel1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - TemplatePrint"
            Me.panel1.ResumeLayout(False)
            CType(Me.statusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.statusBarPanel2, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.statusBarPanel3, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).EndInit()
            Me.GIS.ResumeLayout(False)
            CType(Me.GIS_ControlNorthArrow, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.GIS_ControlScale, System.ComponentModel.ISupportInitialize).EndInit()
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

        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim src_path As String
            Dim tpl_path As String

            ' open a file
            GIS_ControlNorthArrow.GIS_Viewer = GIS.GetOcx()
            GIS_ControlScale.GIS_Viewer = GIS.GetOcx()
            GIS_ControlLegend.GIS_Viewer = GIS.GetOcx()
            GIS.Open(GisUtils.GisSamplesDataDirDownload() & "\World\Countries\Poland\DCW\poland.ttkproject")

            ' copy template file to the current directory as .ttktemplate
            src_path = TGIS_Utils.GisSamplesDataDirDownload() + "Samples\PrintTemplate\printtemplate.tpl"
            tpl_path = System.IO.Directory.GetCurrentDirectory() + "\\printtemplate.ttktemplate"
            TGIS_TemplatePrintBuilder.CopyTemplateFile(src_path, tpl_path,False)

            ' prepare template for printing
            template = New TGIS_TemplatePrint()
            template.TemplatePath = tpl_path
            template.GIS_Viewer(1) = GIS.GetOcx()
            template.GIS_Legend(1) = GIS_ControlLegend.GetOcx()
            template.GIS_Scale(1) = GIS_ControlScale.GetOcx()
            template.GIS_NorthArrow(1) = GIS_ControlNorthArrow.GetOcx()
            template.GIS_ViewerExtent(1) = GIS.Extent
            template.Text(1) = "Title"
            template.Text(2) = "Copyright"

            ' prepare print manager
            manager = New TGIS_PrintManager()
            manager.Template = template

            statusBar1.Panels(2).Text = System.IO.Path.GetFileName(tpl_path)
        End Sub

        Private Sub button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles button1.Click
            Dim cr As System.Windows.Forms.Cursor

            cr = GIS.Cursor
            Try
                GIS.Cursor = Cursors.WaitCursor
                GIS_ControlPrintPreviewSimple = New TGIS_ControlPrintPreviewSimple()
                GIS_ControlPrintPreviewSimple.GIS_Viewer = GIS.GetOcx()
                GIS_ControlPrintPreviewSimple.Preview_2(manager)
            Finally
                GIS.Cursor = cr
            End Try
        End Sub

        Private Sub toolBar1_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles toolBar1.ButtonClick
            Select Case toolBar1.Buttons.IndexOf(e.Button)
                ' btnFullExt
                Case 0
                    GIS.FullExtent()
                ' btnZoom
                Case 2
                    GIS.Mode = TGIS_ViewerMode.Zoom
                    btnZoom.Pushed = True
                    btnDrag.Pushed = Not btnZoom.Pushed
                ' btnDrag
                Case 3
                    GIS.Mode = TGIS_ViewerMode.Drag
                    btnDrag.Pushed = True
                    btnZoom.Pushed = Not btnDrag.Pushed
            End Select
        End Sub

        Private Sub toolBar1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
            Dim p As Point = New Point(e.X, e.Y)

            If toolBar1.Buttons(0).Rectangle.Contains(p) OrElse toolBar1.Buttons(2).Rectangle.Contains(p) OrElse toolBar1.Buttons(3).Rectangle.Contains(p) Then
                toolBar1.Cursor = Cursors.Hand
            Else
                toolBar1.Cursor = Cursors.Default
            End If
        End Sub

        Private Sub GIS_AfterPaint(ByVal sender As Object, ByVal e As AxTatukGIS_XDK11.ITGIS_ViewerWndEvents_AfterPaintEventEvent) Handles GIS.AfterPaintEvent
            statusBar1.Panels(1).Text = GIS.ScaleAsText
        End Sub

        Private Sub button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles button2.Click
            TGIS_ControlPrintTemplateDesignerForm.ShowPrintTemplateDesigner(template, "", "")
            statusBar1.Panels(2).Text = System.IO.Path.GetFileName(template.TemplatePath)
        End Sub

        Private Sub button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles button3.Click
            Dim dlg As OpenFileDialog
            Dim old As String

            dlg = New OpenFileDialog()
            dlg.Filter = "Print template|*.tpl;*.ttktemplate"
            dlg.FileName = ""
            dlg.InitialDirectory = System.IO.Directory.GetCurrentDirectory()
            If dlg.ShowDialog() = DialogResult.OK Then
                old = template.TemplatePath
                Try
                    template.TemplatePath = dlg.FileName
                    statusBar1.Panels(2).Text = System.IO.Path.GetFileName(template.TemplatePath)
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    template.TemplatePath = old
                End Try
            End If
        End Sub
    End Class
End Namespace
