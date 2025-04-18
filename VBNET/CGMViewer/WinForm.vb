Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports System.IO
Imports TatukGIS_XDK11

Namespace CGMViewer
    ''' <summary>
    ''' Summary description for WinForm.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.Container = Nothing
        Private statusBar1 As System.Windows.Forms.StatusBar
        Private GIS As AxTatukGIS_XDK11.AxTGIS_ViewerWnd
        Private shp As TGIS_Shape
        Private statusBarPanel1 As System.Windows.Forms.StatusBarPanel
        Private WithEvents button1 As System.Windows.Forms.Button
        Private panel1 As System.Windows.Forms.Panel
        Private WithEvents listBox1 As System.Windows.Forms.ListBox

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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WinForm))
            Me.statusBar1 = New System.Windows.Forms.StatusBar()
            Me.statusBarPanel1 = New System.Windows.Forms.StatusBarPanel()
            Me.GIS = New AxTatukGIS_XDK11.AxTGIS_ViewerWnd()
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.button1 = New System.Windows.Forms.Button()
            Me.listBox1 = New System.Windows.Forms.ListBox()
            CType(Me.statusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.panel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'statusBar1
            '
            Me.statusBar1.Location = New System.Drawing.Point(0, 447)
            Me.statusBar1.Name = "statusBar1"
            Me.statusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.statusBarPanel1})
            Me.statusBar1.ShowPanels = True
            Me.statusBar1.Size = New System.Drawing.Size(602, 19)
            Me.statusBar1.TabIndex = 3
            '
            'statusBarPanel1
            '
            Me.statusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
            Me.statusBarPanel1.Name = "statusBarPanel1"
            Me.statusBarPanel1.Width = 585
            '
            'GIS
            '
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.Enabled = True
            Me.GIS.Location = New System.Drawing.Point(161, 29)
            Me.GIS.Name = "GIS"
            Me.GIS.OcxState = CType(resources.GetObject("GIS.OcxState"), System.Windows.Forms.AxHost.State)
            Me.GIS.Size = New System.Drawing.Size(441, 418)
            Me.GIS.TabIndex = 1
            '
            'panel1
            '
            Me.panel1.Controls.Add(Me.button1)
            Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.panel1.Location = New System.Drawing.Point(0, 0)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(602, 29)
            Me.panel1.TabIndex = 0
            '
            'button1
            '
            Me.button1.BackColor = System.Drawing.SystemColors.Control
            Me.button1.Cursor = System.Windows.Forms.Cursors.Hand
            Me.button1.Location = New System.Drawing.Point(3, 3)
            Me.button1.Name = "button1"
            Me.button1.Size = New System.Drawing.Size(120, 22)
            Me.button1.TabIndex = 0
            Me.button1.TabStop = False
            Me.button1.Text = "Rotate symbol"
            Me.button1.UseVisualStyleBackColor = False
            '
            'listBox1
            '
            Me.listBox1.Dock = System.Windows.Forms.DockStyle.Left
            Me.listBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
            Me.listBox1.IntegralHeight = False
            Me.listBox1.Location = New System.Drawing.Point(0, 29)
            Me.listBox1.Name = "listBox1"
            Me.listBox1.Size = New System.Drawing.Size(161, 418)
            Me.listBox1.TabIndex = 4
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.BackColor = System.Drawing.SystemColors.Window
            Me.ClientSize = New System.Drawing.Size(602, 466)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.listBox1)
            Me.Controls.Add(Me.panel1)
            Me.Controls.Add(Me.statusBar1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - CGM Viewer"
            CType(Me.statusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).EndInit()
            Me.panel1.ResumeLayout(False)
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
            Dim dir As DirectoryInfo
            Dim ll As TGIS_LayerVector

            ' load list box
            dir = New DirectoryInfo(GisUtils.GisSamplesDataDirDownload() + "\Symbols\")
            For Each fileItem As FileInfo In dir.GetFiles("*.cgm")
                listBox1.Items.Add(fileItem.Name)
            Next fileItem

            ' new layer as a grid
            ll = New TGIS_LayerVector()
            GIS.Add(ll)
            ll.Extent = GisUtils.GisExtent(-90, -90, 90, 90)
            GIS.FullExtent()

            ' add coordinate layout
            shp = ll.CreateShape(TGIS_ShapeType.Arc)
            shp.Params.Line.Width = 1
            shp.AddPart()
            shp.AddPoint(GisUtils.GisPoint(-90, 0))
            shp.AddPoint(GisUtils.GisPoint(90, 0))

            shp = ll.CreateShape(TGIS_ShapeType.Arc)
            shp.Params.Line.Width = 1
            shp.AddPart()
            shp.AddPoint(GisUtils.GisPoint(0, -90))
            shp.AddPoint(GisUtils.GisPoint(0, 90))

            shp = ll.CreateShape(TGIS_ShapeType.Point)
            shp.AddPart()
            shp.AddPoint(GisUtils.GisPoint(0, 0))
        End Sub

        Private Sub WinForm_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
            drawSymbol()
        End Sub

        Private Sub drawSymbol()
            Dim w, h As Integer

            If shp Is Nothing Then Exit Sub


            ' create a symbol list
            If listBox1.SelectedIndex > -1 Then
                shp.Params.Marker.Symbol = GisUtils.SymbolList.Prepare(GisUtils.GisSamplesDataDirDownload() & "\Symbols\" & listBox1.Items(listBox1.SelectedIndex))
                ' calculate symbol size
                If Not shp.Params.Marker.Symbol Is Nothing Then
                    shp.Params.Marker.Size = -Math.Min(GIS.Width, GIS.Height) * 2 / 3

                    ' prepare to obtain computed width/height
                    shp.Params.Marker.Symbol.Prepare(GIS.GetOcx(), -5, (New TGIS_Color).Black, (New TGIS_Color).Black, 0, 0, TGIS_SymbolPosition.MiddleCenter, True)
                    w = shp.Params.Marker.Symbol.Width
                    h = shp.Params.Marker.Symbol.Height
                    shp.Params.Marker.Symbol.Unprepare()

                    If h < w Then
                        shp.Params.Marker.Size = shp.Params.Marker.Size * h / w
                    End If
                Else
                    shp.Params.Marker.Size = 0
                End If

                ' set attributes
                shp.Params.Marker.Color = (New TGIS_Color).RenderColor
                shp.Params.Marker.OutlineColor = (New TGIS_Color).RenderColor
                GIS.InvalidateWholeMap()
            End If
        End Sub

        Private Sub button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles button1.Click
            ' rotate symbol
            shp.Params.Marker.SymbolRotate = shp.Params.Marker.SymbolRotate + Math.PI / 2
            shp.Invalidate()
        End Sub

        Private Sub listBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles listBox1.SelectedIndexChanged
            statusBar1.Panels(0).Text = GisUtils.GisSamplesDataDirDownload() + listBox1.Items(listBox1.SelectedIndex)
            drawSymbol()
        End Sub
    End Class
End Namespace
