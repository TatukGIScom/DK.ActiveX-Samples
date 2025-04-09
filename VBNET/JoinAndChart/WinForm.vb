Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports System.Data.OleDb
Imports TatukGIS_XDK11

Namespace JoinAndChart
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
        Private panel3 As System.Windows.Forms.Panel
        Private WithEvents toolBar1 As System.Windows.Forms.ToolBar
        Private btnFullExtent As System.Windows.Forms.ToolBarButton
        Private btnZooIn As System.Windows.Forms.ToolBarButton
        Private btnZoomOut As System.Windows.Forms.ToolBarButton
        Private toolBarButton1 As System.Windows.Forms.ToolBarButton
        Private imageList1 As System.Windows.Forms.ImageList
        Private toolBar2 As System.Windows.Forms.ToolBar
        Private WithEvents cmbSize As System.Windows.Forms.ComboBox
        Private toolBarButton2 As System.Windows.Forms.ToolBarButton
        Private WithEvents cmbValues As System.Windows.Forms.ComboBox
        Private panel4 As System.Windows.Forms.Panel
        Private toolBar3 As System.Windows.Forms.ToolBar
        Private toolBarButton3 As System.Windows.Forms.ToolBarButton
        Private WithEvents cmbStyle As System.Windows.Forms.ComboBox
        Private toolTip1 As System.Windows.Forms.ToolTip
        Private toolTip2 As System.Windows.Forms.ToolTip
        Private toolTip3 As System.Windows.Forms.ToolTip
        Private statusBar1 As System.Windows.Forms.StatusBar

        Private GIS As AxTatukGIS_XDK11.AxTGIS_ViewerWnd

        Public Sub New()
            '
            ' Required for Windows Form Designer support
            '
            InitializeComponent()

            '
            ' TODO: Add any constructor code after InitializeComponent call
            '
            Me.ActiveControl = GIS
            Me.toolTip1.SetToolTip(Me.cmbSize, "Chart Size")
            Me.toolTip2.SetToolTip(Me.cmbValues, "Chart Values")
            Me.toolTip3.SetToolTip(Me.cmbStyle, "Chart Style")
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
            Me.cmbStyle = New System.Windows.Forms.ComboBox()
            Me.toolBar3 = New System.Windows.Forms.ToolBar()
            Me.toolBarButton3 = New System.Windows.Forms.ToolBarButton()
            Me.panel3 = New System.Windows.Forms.Panel()
            Me.cmbValues = New System.Windows.Forms.ComboBox()
            Me.toolBar2 = New System.Windows.Forms.ToolBar()
            Me.toolBarButton2 = New System.Windows.Forms.ToolBarButton()
            Me.panel2 = New System.Windows.Forms.Panel()
            Me.cmbSize = New System.Windows.Forms.ComboBox()
            Me.toolBar1 = New System.Windows.Forms.ToolBar()
            Me.btnFullExtent = New System.Windows.Forms.ToolBarButton()
            Me.btnZooIn = New System.Windows.Forms.ToolBarButton()
            Me.btnZoomOut = New System.Windows.Forms.ToolBarButton()
            Me.toolBarButton1 = New System.Windows.Forms.ToolBarButton()
            Me.imageList1 = New System.Windows.Forms.ImageList(Me.components)
            Me.toolTip1 = New System.Windows.Forms.ToolTip(Me.components)
            Me.toolTip2 = New System.Windows.Forms.ToolTip(Me.components)
            Me.toolTip3 = New System.Windows.Forms.ToolTip(Me.components)
            Me.statusBar1 = New System.Windows.Forms.StatusBar()
            Me.GIS = New AxTatukGIS_XDK11.AxTGIS_ViewerWnd()
            Me.panel1.SuspendLayout()
            Me.panel4.SuspendLayout()
            Me.panel3.SuspendLayout()
            Me.panel2.SuspendLayout()
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
            Me.panel1.Size = New System.Drawing.Size(588, 24)
            Me.panel1.TabIndex = 0
            '
            'panel4
            '
            Me.panel4.Controls.Add(Me.cmbStyle)
            Me.panel4.Controls.Add(Me.toolBar3)
            Me.panel4.Dock = System.Windows.Forms.DockStyle.Fill
            Me.panel4.Location = New System.Drawing.Point(393, 0)
            Me.panel4.Name = "panel4"
            Me.panel4.Size = New System.Drawing.Size(195, 24)
            Me.panel4.TabIndex = 2
            '
            'cmbStyle
            '
            Me.cmbStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbStyle.Items.AddRange(New Object() {"Pie", "Bar"})
            Me.cmbStyle.Location = New System.Drawing.Point(8, 2)
            Me.cmbStyle.Name = "cmbStyle"
            Me.cmbStyle.Size = New System.Drawing.Size(88, 21)
            Me.cmbStyle.TabIndex = 1
            '
            'toolBar3
            '
            Me.toolBar3.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
            Me.toolBar3.AutoSize = False
            Me.toolBar3.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.toolBarButton3})
            Me.toolBar3.ButtonSize = New System.Drawing.Size(23, 22)
            Me.toolBar3.DropDownArrows = True
            Me.toolBar3.Location = New System.Drawing.Point(0, 0)
            Me.toolBar3.Name = "toolBar3"
            Me.toolBar3.ShowToolTips = True
            Me.toolBar3.Size = New System.Drawing.Size(195, 24)
            Me.toolBar3.TabIndex = 0
            '
            'toolBarButton3
            '
            Me.toolBarButton3.Name = "toolBarButton3"
            Me.toolBarButton3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
            '
            'panel3
            '
            Me.panel3.Controls.Add(Me.cmbValues)
            Me.panel3.Controls.Add(Me.toolBar2)
            Me.panel3.Dock = System.Windows.Forms.DockStyle.Left
            Me.panel3.Location = New System.Drawing.Point(222, 0)
            Me.panel3.Name = "panel3"
            Me.panel3.Size = New System.Drawing.Size(171, 24)
            Me.panel3.TabIndex = 1
            '
            'cmbValues
            '
            Me.cmbValues.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbValues.Items.AddRange(New Object() {"black:white", "pop2000:square_mil", "male2000:female2000"})
            Me.cmbValues.Location = New System.Drawing.Point(8, 2)
            Me.cmbValues.Name = "cmbValues"
            Me.cmbValues.Size = New System.Drawing.Size(163, 21)
            Me.cmbValues.TabIndex = 1
            '
            'toolBar2
            '
            Me.toolBar2.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
            Me.toolBar2.AutoSize = False
            Me.toolBar2.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.toolBarButton2})
            Me.toolBar2.ButtonSize = New System.Drawing.Size(23, 22)
            Me.toolBar2.DropDownArrows = True
            Me.toolBar2.Location = New System.Drawing.Point(0, 0)
            Me.toolBar2.Name = "toolBar2"
            Me.toolBar2.ShowToolTips = True
            Me.toolBar2.Size = New System.Drawing.Size(171, 24)
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
            Me.panel2.Size = New System.Drawing.Size(222, 24)
            Me.panel2.TabIndex = 0
            '
            'cmbSize
            '
            Me.cmbSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbSize.Items.AddRange(New Object() {"pop2000", "male2000", "female2000", "under18", "asia", "black", "white", "hisp_lat"})
            Me.cmbSize.Location = New System.Drawing.Point(77, 2)
            Me.cmbSize.Name = "cmbSize"
            Me.cmbSize.Size = New System.Drawing.Size(145, 21)
            Me.cmbSize.TabIndex = 1
            '
            'toolBar1
            '
            Me.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
            Me.toolBar1.AutoSize = False
            Me.toolBar1.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.btnFullExtent, Me.btnZooIn, Me.btnZoomOut, Me.toolBarButton1})
            Me.toolBar1.ButtonSize = New System.Drawing.Size(23, 22)
            Me.toolBar1.DropDownArrows = True
            Me.toolBar1.ImageList = Me.imageList1
            Me.toolBar1.Location = New System.Drawing.Point(0, 0)
            Me.toolBar1.Name = "toolBar1"
            Me.toolBar1.ShowToolTips = True
            Me.toolBar1.Size = New System.Drawing.Size(222, 24)
            Me.toolBar1.TabIndex = 0
            '
            'btnFullExtent
            '
            Me.btnFullExtent.ImageIndex = 0
            Me.btnFullExtent.Name = "btnFullExtent"
            Me.btnFullExtent.ToolTipText = "Full Extent"
            '
            'btnZooIn
            '
            Me.btnZooIn.ImageIndex = 1
            Me.btnZooIn.Name = "btnZooIn"
            Me.btnZooIn.ToolTipText = "Zoom In"
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
            Me.statusBar1.ShowPanels = True
            Me.statusBar1.Size = New System.Drawing.Size(588, 19)
            Me.statusBar1.TabIndex = 1
            '
            'GIS
            '
            Me.GIS.BackColor = System.Drawing.SystemColors.ControlLight
            Me.GIS.Cursor = System.Windows.Forms.Cursors.Default
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.Enabled = True
            Me.GIS.Location = New System.Drawing.Point(0, 24)
            Me.GIS.Name = "GIS"
            Me.GIS.OcxState = CType(resources.GetObject("GIS.OcxState"), System.Windows.Forms.AxHost.State)
            Me.GIS.Size = New System.Drawing.Size(588, 423)
            Me.GIS.TabIndex = 0
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(588, 466)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.statusBar1)
            Me.Controls.Add(Me.panel1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Join and Chart"
            Me.panel1.ResumeLayout(False)
            Me.panel4.ResumeLayout(False)
            Me.panel3.ResumeLayout(False)
            Me.panel2.ResumeLayout(False)
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

            cmbSize.SelectedIndex = 0
            cmbValues.SelectedIndex = 0
            cmbStyle.SelectedIndex = 0

            sqlDC = CreateObject("ADODB.Connection")
            sqlRS = CreateObject("ADODB.Recordset")
            sqlDC.Open("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & GisUtils.GisSamplesDataDirDownload & "\World\Countries\USA\Statistical\Stats.mdb")


            ' use layer to display charts
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
            
            GIS.Add(ll)
            GIS.FullExtent()

            cmb_SelectedIndexChanged(Me, e)
        End Sub

        Private Sub WinForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Leave

        End Sub

        Private Sub cmb_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbStyle.SelectedIndexChanged, cmbValues.SelectedIndexChanged, cmbSize.SelectedIndexChanged
            Dim ll As TGIS_LayerVector
            Dim vsize As String
            Dim vvalues As String
            Dim vstyle As String
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
            vvalues = cmbValues.Items(cmbValues.SelectedIndex).ToString()
            vstyle = cmbStyle.Items(cmbStyle.SelectedIndex).ToString()

            If sqlRS.State <> 0 Then
                sqlRS.Close()
            End If
            ' find min, max values for shapes
            sqlRS.Open("SELECT min(" & vsize & ") AS mini, max(" & vsize & ") AS maxi FROM ce2000t", sqlDC)

            ' let's load data to recordset
            vmin = sqlRS.Fields("mini").Value
            vmax = sqlRS.Fields("maxi").Value
            sqlRS.Close()

            sqlRS.Open("select * FROM ce2000t  ORDER BY fips", sqlDC)
            ll.JoinADO = sqlRS
            ' set primary and foreign keys
            ll.JoinPrimary = "cntyidfp"
            ll.JoinForeign = "fips"

            ' Set the chart style: "Pie" or "Bar"
            ll.Params.Chart.Style = GisUtils.ParamChart(vstyle, TGIS_ChartStyle.Pie)
            
            ' The chart size will be set by Render in the range of 350 to 1000
            ' depending on the value of the "vsize" field
            ll.Params.Chart.Size = GisUtils.GIS_RENDER_SIZE()
            ll.Params.Render.StartSize = 350
            ll.Params.Render.EndSize = 1000
            ll.Params.Render.Expression = vsize
            
            ' The Renderer will create 10 zones to group field values,
            ' starting from "vmin" and edning with "vmax"
            ll.Params.Render.Zones = 10
            ll.Params.Render.MinVal = vmin
            ll.Params.Render.MaxVal = vmax

            ' For 'Bar' chart you can replace '0:0' by 'min:max' to set custom Y-axis limits.
            ' 'vvalues' contains list of values displayed on the chart.
            ' In this sample field names are used, e.g. 'male2000:female2000'.
            ' Values need to be divided by a colon ':'.
            ll.Params.Render.Chart = "0:0:" & vvalues

            ' If necessary, the chart can also be included in the legend
            ll.Params.Chart.Legend = ll.Params.Render.Chart
            ll.Params.Chart.ShowLegend = True
            
            GIS.InvalidateWholeMap()
        End Sub

        Private Sub toolBar1_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles toolBar1.ButtonClick
            Select Case toolBar1.Buttons.IndexOf(e.Button)
                Case 0
                    ' btnFullExt
                    GIS.FullExtent()
                Case 1
                    ' btnZoomIn
                    ' change viewer zoom
                    GIS.Zoom = GIS.Zoom * 2
                Case 2
                    ' btnZoomOut
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
    End Class
End Namespace
