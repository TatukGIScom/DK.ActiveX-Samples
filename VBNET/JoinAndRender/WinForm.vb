Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports System.Data.OleDb
Imports TatukGIS_XDK11

Namespace JoinAndRender
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
        Private panel2 As System.Windows.Forms.Panel
        Private WithEvents toolBar1 As System.Windows.Forms.ToolBar
        Private imageList1 As System.Windows.Forms.ImageList
        Private btnFullExtent As System.Windows.Forms.ToolBarButton
        Private btnZoomIn As System.Windows.Forms.ToolBarButton
        Private btnZoomOut As System.Windows.Forms.ToolBarButton
        Private toolBarButton1 As System.Windows.Forms.ToolBarButton
        Private WithEvents cmbSize As System.Windows.Forms.ComboBox
        Private toolTip1 As System.Windows.Forms.ToolTip
        Private panel3 As System.Windows.Forms.Panel
        Private toolBar2 As System.Windows.Forms.ToolBar
        Private toolBarButton2 As System.Windows.Forms.ToolBarButton
        Private WithEvents panColorStart As System.Windows.Forms.Panel
        Private WithEvents panColorEnd As System.Windows.Forms.Panel
        Private panel4 As System.Windows.Forms.Panel
        Private toolBar3 As System.Windows.Forms.ToolBar
        Private toolBarButton3 As System.Windows.Forms.ToolBarButton
        Private WithEvents scrTransparency As System.Windows.Forms.TrackBar
        Private statusBar1 As System.Windows.Forms.StatusBar
        Private colorDialog1 As System.Windows.Forms.ColorDialog
        Private tgiS_ControlLegend1 As AxTatukGIS_XDK11.AxTGIS_ControlLegend
        Private GIS As AxTatukGIS_XDK11.AxTGIS_ViewerWnd
        Private sqlConnection As System.Data.OleDb.OleDbConnection
        Private sqlCommand As System.Data.OleDb.OleDbCommand
        Private sqlDA As System.Data.OleDb.OleDbDataAdapter
        Private sqlDS As System.Data.DataSet

        Public Sub New()
            '
            ' Required for Windows Form Designer support
            '
            InitializeComponent()

            '
            ' TODO: Add any constructor code after InitializeComponent call
            '
            '?this.ActiveControl = GIS ;
            Me.toolTip1.SetToolTip(Me.cmbSize, "Chart Size")
            Me.toolTip1.SetToolTip(Me.panColorStart, "Click to change start color")
            Me.toolTip1.SetToolTip(Me.panColorEnd, "Click to change end color")
            Me.toolTip1.SetToolTip(Me.panColorEnd, "Transparency")
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
            Me.panel4 = New System.Windows.Forms.Panel()
            Me.scrTransparency = New System.Windows.Forms.TrackBar()
            Me.toolBar3 = New System.Windows.Forms.ToolBar()
            Me.toolBarButton3 = New System.Windows.Forms.ToolBarButton()
            Me.panel3 = New System.Windows.Forms.Panel()
            Me.panColorEnd = New System.Windows.Forms.Panel()
            Me.panColorStart = New System.Windows.Forms.Panel()
            Me.toolBar2 = New System.Windows.Forms.ToolBar()
            Me.toolBarButton2 = New System.Windows.Forms.ToolBarButton()
            Me.panel2 = New System.Windows.Forms.Panel()
            Me.cmbSize = New System.Windows.Forms.ComboBox()
            Me.toolBar1 = New System.Windows.Forms.ToolBar()
            Me.btnFullExtent = New System.Windows.Forms.ToolBarButton()
            Me.btnZoomIn = New System.Windows.Forms.ToolBarButton()
            Me.btnZoomOut = New System.Windows.Forms.ToolBarButton()
            Me.toolBarButton1 = New System.Windows.Forms.ToolBarButton()
            Me.imageList1 = New System.Windows.Forms.ImageList(Me.components)
            Me.toolTip1 = New System.Windows.Forms.ToolTip(Me.components)
            Me.statusBar1 = New System.Windows.Forms.StatusBar()
            Me.colorDialog1 = New System.Windows.Forms.ColorDialog()
            Me.tgiS_ControlLegend1 = New AxTatukGIS_XDK11.AxTGIS_ControlLegend()
            Me.GIS = New AxTatukGIS_XDK11.AxTGIS_ViewerWnd()
            Me.panel1.SuspendLayout()
            Me.panel4.SuspendLayout()
            CType(Me.scrTransparency, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.panel3.SuspendLayout()
            Me.panel2.SuspendLayout()
            CType(Me.tgiS_ControlLegend1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'panel1
            '
            Me.panel1.Controls.Add(Me.panel4)
            Me.panel1.Controls.Add(Me.panel3)
            Me.panel1.Controls.Add(Me.panel2)
            Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.panel1.Location = New System.Drawing.Point(0, 0)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(597, 24)
            Me.panel1.TabIndex = 0
            '
            'panel4
            '
            Me.panel4.Controls.Add(Me.scrTransparency)
            Me.panel4.Controls.Add(Me.toolBar3)
            Me.panel4.Dock = System.Windows.Forms.DockStyle.Fill
            Me.panel4.Location = New System.Drawing.Point(318, 0)
            Me.panel4.Name = "panel4"
            Me.panel4.Size = New System.Drawing.Size(279, 24)
            Me.panel4.TabIndex = 2
            '
            'scrTransparency
            '
            Me.scrTransparency.Location = New System.Drawing.Point(6, 0)
            Me.scrTransparency.Maximum = 100
            Me.scrTransparency.Name = "scrTransparency"
            Me.scrTransparency.Size = New System.Drawing.Size(137, 45)
            Me.scrTransparency.TabIndex = 1
            Me.scrTransparency.TickFrequency = 25
            Me.scrTransparency.Value = 100
            '
            'toolBar3
            '
            Me.toolBar3.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
            Me.toolBar3.AutoSize = False
            Me.toolBar3.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.toolBarButton3})
            Me.toolBar3.DropDownArrows = True
            Me.toolBar3.Location = New System.Drawing.Point(0, 0)
            Me.toolBar3.Name = "toolBar3"
            Me.toolBar3.ShowToolTips = True
            Me.toolBar3.Size = New System.Drawing.Size(279, 24)
            Me.toolBar3.TabIndex = 0
            '
            'toolBarButton3
            '
            Me.toolBarButton3.Name = "toolBarButton3"
            Me.toolBarButton3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
            '
            'panel3
            '
            Me.panel3.Controls.Add(Me.panColorEnd)
            Me.panel3.Controls.Add(Me.panColorStart)
            Me.panel3.Controls.Add(Me.toolBar2)
            Me.panel3.Dock = System.Windows.Forms.DockStyle.Left
            Me.panel3.Location = New System.Drawing.Point(224, 0)
            Me.panel3.Name = "panel3"
            Me.panel3.Size = New System.Drawing.Size(94, 24)
            Me.panel3.TabIndex = 1
            '
            'panColorEnd
            '
            Me.panColorEnd.BackColor = System.Drawing.Color.Navy
            Me.panColorEnd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.panColorEnd.Location = New System.Drawing.Point(50, 2)
            Me.panColorEnd.Name = "panColorEnd"
            Me.panColorEnd.Size = New System.Drawing.Size(42, 22)
            Me.panColorEnd.TabIndex = 2
            '
            'panColorStart
            '
            Me.panColorStart.BackColor = System.Drawing.Color.Aqua
            Me.panColorStart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.panColorStart.Location = New System.Drawing.Point(6, 2)
            Me.panColorStart.Name = "panColorStart"
            Me.panColorStart.Size = New System.Drawing.Size(42, 22)
            Me.panColorStart.TabIndex = 1
            '
            'toolBar2
            '
            Me.toolBar2.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
            Me.toolBar2.AutoSize = False
            Me.toolBar2.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.toolBarButton2})
            Me.toolBar2.DropDownArrows = True
            Me.toolBar2.Location = New System.Drawing.Point(0, 0)
            Me.toolBar2.Name = "toolBar2"
            Me.toolBar2.ShowToolTips = True
            Me.toolBar2.Size = New System.Drawing.Size(94, 24)
            Me.toolBar2.TabIndex = 0
            '
            'toolBarButton2
            '
            Me.toolBarButton2.Name = "toolBarButton2"
            Me.toolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
            '
            'panel2
            '
            Me.panel2.Controls.Add(Me.cmbSize)
            Me.panel2.Controls.Add(Me.toolBar1)
            Me.panel2.Dock = System.Windows.Forms.DockStyle.Left
            Me.panel2.Location = New System.Drawing.Point(0, 0)
            Me.panel2.Name = "panel2"
            Me.panel2.Size = New System.Drawing.Size(224, 24)
            Me.panel2.TabIndex = 0
            '
            'cmbSize
            '
            Me.cmbSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbSize.Items.AddRange(New Object() {"pop2000", "under18", "asia", "black", "white", "hisp_lat", "male2000", "female2000"})
            Me.cmbSize.Location = New System.Drawing.Point(77, 2)
            Me.cmbSize.Name = "cmbSize"
            Me.cmbSize.Size = New System.Drawing.Size(145, 21)
            Me.cmbSize.TabIndex = 1
            Me.cmbSize.TabStop = False
            '
            'toolBar1
            '
            Me.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
            Me.toolBar1.AutoSize = False
            Me.toolBar1.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.btnFullExtent, Me.btnZoomIn, Me.btnZoomOut, Me.toolBarButton1})
            Me.toolBar1.DropDownArrows = True
            Me.toolBar1.ImageList = Me.imageList1
            Me.toolBar1.Location = New System.Drawing.Point(0, 0)
            Me.toolBar1.Name = "toolBar1"
            Me.toolBar1.ShowToolTips = True
            Me.toolBar1.Size = New System.Drawing.Size(224, 24)
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
            Me.statusBar1.Size = New System.Drawing.Size(597, 19)
            Me.statusBar1.TabIndex = 1
            Me.statusBar1.Text = "Use the trackbar to change transparency"
            '
            'tgiS_ControlLegend1
            '
            Me.tgiS_ControlLegend1.Dock = System.Windows.Forms.DockStyle.Left
            Me.tgiS_ControlLegend1.Enabled = True
            Me.tgiS_ControlLegend1.Location = New System.Drawing.Point(0, 24)
            Me.tgiS_ControlLegend1.Name = "tgiS_ControlLegend1"
            Me.tgiS_ControlLegend1.OcxState = CType(resources.GetObject("tgiS_ControlLegend1.OcxState"), System.Windows.Forms.AxHost.State)
            Me.tgiS_ControlLegend1.Size = New System.Drawing.Size(144, 423)
            Me.tgiS_ControlLegend1.TabIndex = 2
            '
            'GIS
            '
            Me.GIS.BackColor = System.Drawing.SystemColors.Control
            Me.GIS.Cursor = System.Windows.Forms.Cursors.Default
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.Enabled = True
            Me.GIS.Location = New System.Drawing.Point(144, 24)
            Me.GIS.Name = "GIS"
            Me.GIS.OcxState = CType(resources.GetObject("GIS.OcxState"), System.Windows.Forms.AxHost.State)
            Me.GIS.Size = New System.Drawing.Size(453, 423)
            Me.GIS.TabIndex = 3
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(597, 466)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.tgiS_ControlLegend1)
            Me.Controls.Add(Me.statusBar1)
            Me.Controls.Add(Me.panel1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Join and Render"
            Me.panel1.ResumeLayout(False)
            Me.panel4.ResumeLayout(False)
            Me.panel4.PerformLayout()
            CType(Me.scrTransparency, System.ComponentModel.ISupportInitialize).EndInit()
            Me.panel3.ResumeLayout(False)
            Me.panel2.ResumeLayout(False)
            CType(Me.tgiS_ControlLegend1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
#End Region

        Dim GisUtils As New TGIS_Utils()
        Dim sqlDC As Object
        Dim sqlRS As Object


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
            Dim ll As TGIS_LayerSHP
            tgiS_ControlLegend1.GIS_Viewer = GIS.GetOcx()


            cmbSize.SelectedIndex = 0

            ' create ADO .NET connection object
            sqlDC = CreateObject("ADODB.Connection")
            sqlRS = CreateObject("ADODB.Recordset")
            sqlDC.Open("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & GisUtils.GisSamplesDataDirDownload & "\World\Countries\USA\Statistical\Stats.mdb")

            ' create a layer, set render and label params
            ll = New TGIS_LayerSHP()
            ll.Path = GisUtils.GisSamplesDataDirDownload() & "\World\Countries\USA\States\California\tl_2008_06_county.shp"
            ll.Name = "tl_2008_06_county"

            ll.UseConfig = False
            ll.Params.Labels.Field = "name"
            ll.Params.Labels.Pattern = TGIS_BrushStyle.Clear
            ll.Params.Labels.OutlineWidth = 0
            ll.Params.Labels.FontColor = (New TGIS_Color).White
            ll.Params.Labels.Color = (New TGIS_Color).Black
            ll.Params.Labels.Position = TGIS_LabelPosition.MiddleCenter Or TGIS_LabelPosition.Flow
            ll.Params.Render.StartSize = 350
            ll.Params.Render.EndSize = 1000

            GIS.Add(ll)
            GIS.FullExtent()

            cmbSize_SelectedIndexChanged(Me, e)
        End Sub

        Private Sub WinForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Leave
            sqlConnection.Close()
        End Sub

        Private Sub cmbSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSize.SelectedIndexChanged
            Dim ll As TGIS_LayerVector
            Dim vsize As String

            Dim vmin As Double
            Dim vmax As Double

            If GIS.Items.Count = 0 Then
                Return
            End If
            ll = CType(GIS.Items.item(0), TGIS_LayerVector)
            If ll Is Nothing Then
                Return
            End If

            ' get params
            vsize = cmbSize.Items(cmbSize.SelectedIndex).ToString()

            ' find min, max values for shapes
            If sqlRS.State <> 0 Then
                sqlRS.Close()
            End If

            sqlRS.Open("SELECT min(" & vsize & ") AS mini, max(" & vsize & ") AS maxi FROM ce2000t", sqlDC, ADODB.CursorTypeEnum.adOpenUnspecified, ADODB.LockTypeEnum.adLockReadOnly, -1)

            vmin = Convert.ToDouble(sqlRS.Fields("mini").Value)
            vmax = Convert.ToDouble(sqlRS.Fields("maxi").Value)
            sqlRS.Close()

            sqlRS.Open("select * FROM ce2000t  ORDER BY fips", sqlDC, ADODB.CursorTypeEnum.adOpenUnspecified, ADODB.LockTypeEnum.adLockReadOnly, -1)

            ll.JoinADO = sqlRS

            ' set primary and foreign keys
            ll.JoinPrimary = "cntyidfp"
            ll.JoinForeign = "fips"

            ' render results
            ll.Params.Render.Expression = vsize
            ll.Params.Render.Zones = 10
            ll.Params.Render.MinVal = vmin
            ll.Params.Render.MaxVal = vmax
            ll.Params.Render.StartColor = (New TGIS_Color).FromARGB(panColorStart.BackColor.A, panColorStart.BackColor.R, panColorStart.BackColor.G, panColorStart.BackColor.B)
            ll.Params.Render.EndColor = (New TGIS_Color).FromARGB(panColorEnd.BackColor.A, panColorEnd.BackColor.R, panColorEnd.BackColor.G, panColorEnd.BackColor.B)
            ll.Params.Area.Color = (New TGIS_Color).RenderColor
            ll.Params.Area.ShowLegend = True

            GIS.InvalidateWholeMap()
        End Sub

        Private Sub toolBar1_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles toolBar1.ButtonClick
            Select Case toolBar1.Buttons.IndexOf(e.Button)
                ' btnFullExt
                Case 0
                    GIS.FullExtent()
                ' btnZoomIn
                Case 1
                    ' change viewer zoom
                    GIS.Zoom = GIS.Zoom * 2
                ' btnZoomOut
                Case 2
                    ' change viewer zoom
                    GIS.Zoom = GIS.Zoom / 2
            End Select
        End Sub

        Private Sub toolBar1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles toolBar1.MouseMove
            Dim p As Point = New Point(e.X, e.Y)

            If toolBar1.Buttons(0).Rectangle.Contains(p) OrElse toolBar1.Buttons(1).Rectangle.Contains(p) OrElse toolBar1.Buttons(2).Rectangle.Contains(p) Then
                toolBar1.Cursor = Cursors.Hand
            Else
                toolBar1.Cursor = Cursors.Default
            End If
        End Sub

        Private Sub panColorStart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles panColorStart.Click
            If colorDialog1.ShowDialog() <> System.Windows.Forms.DialogResult.OK Then
                Return
            End If

            panColorStart.BackColor = colorDialog1.Color
            cmbSize_SelectedIndexChanged(sender, e)
        End Sub

        Private Sub panColorEnd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles panColorEnd.Click
            If colorDialog1.ShowDialog() <> System.Windows.Forms.DialogResult.OK Then
                Return
            End If

            panColorEnd.BackColor = colorDialog1.Color
            cmbSize_SelectedIndexChanged(sender, e)
        End Sub

        Private Sub scrTransparency_Scroll(ByVal sender As Object, ByVal e As System.EventArgs) Handles scrTransparency.Scroll
            Dim ll As TGIS_LayerVector

            ll = CType(GIS.Items.item(0), TGIS_LayerVector)
            If ll Is Nothing Then
                Return
            End If

            ' change transparency
            ll.Transparency = scrTransparency.Value

            GIS.InvalidateWholeMap()
        End Sub
    End Class
End Namespace
