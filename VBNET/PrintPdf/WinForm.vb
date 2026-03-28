Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS_XDK11

Namespace PrintPdf
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
        Private toolBarButton1 As System.Windows.Forms.ToolBarButton
        Private btnZoom As System.Windows.Forms.ToolBarButton
        Private btnDrag As System.Windows.Forms.ToolBarButton
        Private imageList1 As System.Windows.Forms.ImageList
        Private statusBar1 As System.Windows.Forms.StatusBar
        Private statusBarPanel1 As System.Windows.Forms.StatusBarPanel
        Private GIS_ControlLegend1 As AxTatukGIS_XDK11.AxTGIS_ControlLegend
        Private WithEvents GIS As AxTatukGIS_XDK11.AxTGIS_ViewerWnd
        Private PdfFileName As String
        Friend WithEvents Panel1 As Panel
        Friend WithEvents Button1 As Button
        Friend WithEvents GroupBox1 As GroupBox
        Friend WithEvents RadioButton5 As RadioButton
        Friend WithEvents RadioButton4 As RadioButton
        Friend WithEvents RadioButton3 As RadioButton
        Friend WithEvents RadioButton2 As RadioButton
        Friend WithEvents RadioButton1 As RadioButton
        Friend WithEvents dlgSave As SaveFileDialog

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
            Me.toolBarButton1 = New System.Windows.Forms.ToolBarButton()
            Me.btnZoom = New System.Windows.Forms.ToolBarButton()
            Me.btnDrag = New System.Windows.Forms.ToolBarButton()
            Me.imageList1 = New System.Windows.Forms.ImageList(Me.components)
            Me.statusBar1 = New System.Windows.Forms.StatusBar()
            Me.statusBarPanel1 = New System.Windows.Forms.StatusBarPanel()
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.Button1 = New System.Windows.Forms.Button()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.RadioButton5 = New System.Windows.Forms.RadioButton()
            Me.RadioButton4 = New System.Windows.Forms.RadioButton()
            Me.RadioButton3 = New System.Windows.Forms.RadioButton()
            Me.RadioButton2 = New System.Windows.Forms.RadioButton()
            Me.RadioButton1 = New System.Windows.Forms.RadioButton()
            Me.GIS = New AxTatukGIS_XDK11.AxTGIS_ViewerWnd()
            Me.GIS_ControlLegend1 = New AxTatukGIS_XDK11.AxTGIS_ControlLegend()
            Me.dlgSave = New System.Windows.Forms.SaveFileDialog()
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.GIS_ControlLegend1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'toolBar1
            '
            Me.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
            Me.toolBar1.AutoSize = False
            Me.toolBar1.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.btnFullExtent, Me.toolBarButton1, Me.btnZoom, Me.btnDrag})
            Me.toolBar1.ButtonSize = New System.Drawing.Size(35, 25)
            Me.toolBar1.Dock = System.Windows.Forms.DockStyle.Top
            Me.toolBar1.DropDownArrows = True
            Me.toolBar1.ImageList = Me.imageList1
            Me.toolBar1.Location = New System.Drawing.Point(0, 0)
            Me.toolBar1.Name = "toolBar1"
            Me.toolBar1.ShowToolTips = True
            Me.toolBar1.Size = New System.Drawing.Size(85, 28)
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
            'imageList1
            '
            Me.imageList1.ImageStream = CType(resources.GetObject("imageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.imageList1.TransparentColor = System.Drawing.Color.Fuchsia
            Me.imageList1.Images.SetKeyName(0, "")
            Me.imageList1.Images.SetKeyName(1, "")
            Me.imageList1.Images.SetKeyName(2, "")
            Me.imageList1.Images.SetKeyName(3, "")
            '
            'statusBar1
            '
            Me.statusBar1.Location = New System.Drawing.Point(0, 447)
            Me.statusBar1.Name = "statusBar1"
            Me.statusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.statusBarPanel1})
            Me.statusBar1.ShowPanels = True
            Me.statusBar1.Size = New System.Drawing.Size(589, 19)
            Me.statusBar1.TabIndex = 2
            '
            'statusBarPanel1
            '
            Me.statusBarPanel1.Alignment = System.Windows.Forms.HorizontalAlignment.Center
            Me.statusBarPanel1.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.Raised
            Me.statusBarPanel1.Name = "statusBarPanel1"
            Me.statusBarPanel1.Text = ""
            Me.statusBarPanel1.Width = 50
            '
            'Panel1
            '
            Me.Panel1.Controls.Add(Me.Button1)
            Me.Panel1.Controls.Add(Me.GroupBox1)
            Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
            Me.Panel1.Location = New System.Drawing.Point(0, 70)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(160, 450)
            Me.Panel1.TabIndex = 9
            '
            'Button1
            '
            Me.Button1.Location = New System.Drawing.Point(30, 260)
            Me.Button1.Name = "Button1"
            Me.Button1.Size = New System.Drawing.Size(75, 35)
            Me.Button1.TabIndex = 1
            Me.Button1.Text = "Print"
            Me.Button1.UseVisualStyleBackColor = True
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.RadioButton5)
            Me.GroupBox1.Controls.Add(Me.RadioButton4)
            Me.GroupBox1.Controls.Add(Me.RadioButton3)
            Me.GroupBox1.Controls.Add(Me.RadioButton2)
            Me.GroupBox1.Controls.Add(Me.RadioButton1)
            Me.GroupBox1.Location = New System.Drawing.Point(10, 50)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(140, 190)
            Me.GroupBox1.TabIndex = 0
            Me.GroupBox1.TabStop = False
            '
            'RadioButton5
            '
            Me.RadioButton5.AutoSize = True
            Me.RadioButton5.Enabled = False
            Me.RadioButton5.Location = New System.Drawing.Point(12, 150)
            Me.RadioButton5.Name = "RadioButton5"
            Me.RadioButton5.Size = New System.Drawing.Size(130, 32)
            Me.RadioButton5.TabIndex = 4
            Me.RadioButton5.TabStop = True
            Me.RadioButton5.Text = "Multi-page print"
            Me.RadioButton5.UseVisualStyleBackColor = True
            '
            'RadioButton4
            '
            Me.RadioButton4.AutoSize = True
            Me.RadioButton4.Enabled = False
            Me.RadioButton4.Location = New System.Drawing.Point(12, 120)
            Me.RadioButton4.Name = "RadioButton4"
            Me.RadioButton4.Size = New System.Drawing.Size(155, 32)
            Me.RadioButton4.TabIndex = 3
            Me.RadioButton4.TabStop = True
            Me.RadioButton4.Text = "Use PrintPage event"
            Me.RadioButton4.UseVisualStyleBackColor = True
            '
            'RadioButton3
            '
            Me.RadioButton3.AutoSize = True
            Me.RadioButton3.Location = New System.Drawing.Point(12, 90)
            Me.RadioButton3.Name = "RadioButton3"
            Me.RadioButton3.Size = New System.Drawing.Size(130, 32)
            Me.RadioButton3.TabIndex = 2
            Me.RadioButton3.TabStop = True
            Me.RadioButton3.Text = "Print a template"
            Me.RadioButton3.UseVisualStyleBackColor = True
            '
            'RadioButton2
            '
            Me.RadioButton2.AutoSize = True
            Me.RadioButton2.Location = New System.Drawing.Point(12, 60)
            Me.RadioButton2.Name = "RadioButton2"
            Me.RadioButton2.Size = New System.Drawing.Size(121, 32)
            Me.RadioButton2.TabIndex = 1
            Me.RadioButton2.TabStop = True
            Me.RadioButton2.Text = "Standard print"
            Me.RadioButton2.UseVisualStyleBackColor = True
            '
            'RadioButton1
            '
            Me.RadioButton1.AutoSize = True
            Me.RadioButton1.Checked = True
            Me.RadioButton1.Location = New System.Drawing.Point(12, 30)
            Me.RadioButton1.Name = "RadioButton1"
            Me.RadioButton1.Size = New System.Drawing.Size(118, 32)
            Me.RadioButton1.TabIndex = 0
            Me.RadioButton1.TabStop = True
            Me.RadioButton1.Text = "GIS.PrintPdf()"
            Me.RadioButton1.UseVisualStyleBackColor = True
            '
            'dlgSave
            '
            Me.dlgSave.DefaultExt = "pdf"
            Me.dlgSave.Filter = "Pdf File (*.pdf)|*.PDF"
            Me.dlgSave.Title = "Select a file"
            '
            'GIS
            '
            Me.GIS.BackColor = System.Drawing.SystemColors.Control
            Me.GIS.Cursor = System.Windows.Forms.Cursors.Default
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.Enabled = True
            Me.GIS.Location = New System.Drawing.Point(148, 28)
            Me.GIS.Name = "GIS"
            Me.GIS.OcxState = CType(resources.GetObject("GIS.OcxState"), System.Windows.Forms.AxHost.State)
            Me.GIS.Size = New System.Drawing.Size(441, 419)
            Me.GIS.TabIndex = 8
            '
            'GIS_ControlLegend1
            '
            Me.GIS_ControlLegend1.Dock = System.Windows.Forms.DockStyle.Right
            Me.GIS_ControlLegend1.Enabled = True
            Me.GIS_ControlLegend1.Location = New System.Drawing.Point(0, 28)
            Me.GIS_ControlLegend1.Name = "GIS_ControlLegend1"
            Me.GIS_ControlLegend1.OcxState = CType(resources.GetObject("GIS_ControlLegend1.OcxState"), System.Windows.Forms.AxHost.State)
            Me.GIS_ControlLegend1.Size = New System.Drawing.Size(120, 419)
            Me.GIS_ControlLegend1.TabIndex = 6
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(589, 466)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.GIS_ControlLegend1)
            Me.Controls.Add(Me.Panel1)
            Me.Controls.Add(Me.statusBar1)
            Me.Controls.Add(Me.toolBar1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - PrintPdf"
            CType(Me.statusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.GIS_ControlLegend1, System.ComponentModel.ISupportInitialize).EndInit()
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
            Application.Run(New WinForm())
        End Sub

        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            ' open a file
            GIS_ControlLegend1.GIS_Viewer = GIS.GetOcx()
            GIS.Open(GisUtils.GisSamplesDataDirDownload() & "\World\Countries\Poland\DCW\poland.ttkproject")
            PdfFileName = ""
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

        Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
            If PdfFileName = "" Then
                dlgSave.InitialDirectory = System.IO.Directory.GetCurrentDirectory()
                dlgSave.FileName = ""
            Else
                dlgSave.InitialDirectory = System.IO.Path.GetDirectoryName(PdfFileName)
                dlgSave.FileName = System.IO.Path.GetFileName(PdfFileName)
            End If
            If dlgSave.ShowDialog() <> DialogResult.OK Then Exit Sub
            PdfFileName = dlgSave.FileName
            statusBarPanel1.Text = PdfFileName

            ' all PrintPdf() methods below
            ' have its versions with a stream instead of file name
            If RadioButton1.Checked Then
                ' GIS.PrintPdf
                GIS.PrintPdf(PdfFileName,
                             CSng(210 * 72 / 25.4),
                             CSng(297 * 72 / 25.4)
                            )
            ElseIf RadioButton2.Checked Then
                ' standard print
                Dim pm As New TGIS_PrintManager
                pm.PrintPdf(GIS.GetOcx, PdfFileName,
                                 CSng(210 * 72 / 25.4),
                                 CSng(297 * 72 / 25.4)
                           )
            ElseIf RadioButton3.Checked Then
                ' template
                Dim tp As New TGIS_TemplatePrint
                tp.TemplatePath = GisUtils.GisSamplesDataDirDownload() & "Samples\PrintTemplate\printtemplate.tpl"
                tp.GIS_Viewer(1) = GIS.GetOcx
                tp.GIS_ViewerExtent(1) = GIS.VisibleExtent
                tp.GIS_ViewerScale(1) = 0
                tp.GIS_Legend(1) = GIS_ControlLegend1.GetOcx
                tp.Text(1) = "Title Title"
                tp.Text(2) = "Copyright"

                Dim pm As New TGIS_PrintManager
                pm.Template = tp
                pm.PrintPdf(GIS.GetOcx, PdfFileName,
                                 CSng(210 * 72 / 25.4),
                                 CSng(297 * 72 / 25.4)
                           )
            ElseIf RadioButton4.Checked Then
                ' PrintPage event
                Dim pm As New TGIS_PrintManager
                ' PrintPage for custom paint
                'pm.PrintPageEvent = New TGIS_PrintPageEvent(AddressOf PrintPage)
                pm.PrintPdf(GIS.GetOcx, PdfFileName,
                                 CSng(210 * 72 / 25.4),
                                 CSng(297 * 72 / 25.4)
                           )
            ElseIf RadioButton5.Checked Then
                ' multi-page: mix of different scenarios
                Dim pm As New TGIS_PrintManager
                ' BeforePrintPage defines the way a page will be printed
                'pm.BeforePrintPageEvent = New TGIS_PrintPageEvent(AddressOf BeforePrintPage)
                pm.PrintPdf(GIS.GetOcx, PdfFileName,
                                 CSng(210 * 72 / 25.4),
                                 CSng(297 * 72 / 25.4)
                           )
            End If
        End Sub
    End Class
End Namespace
