Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS_XDK11

Namespace Snap
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
        Private toolBar1 As System.Windows.Forms.ToolBar
        Private WithEvents btnWithoutSnapping As System.Windows.Forms.Button
        Private WithEvents btnWithSnapping As System.Windows.Forms.Button
        Private WithEvents tmrWithSnapping As System.Windows.Forms.Timer
        Private WithEvents tmrWithoutSnapping As System.Windows.Forms.Timer
        Private GIS As AxTatukGIS_XDK11.AxTGIS_ViewerWnd
        Private shpPolice As TGIS_Shape ' police shape
        Private cntPoint As Integer ' number of evaluated points

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
            Me.btnWithSnapping = New System.Windows.Forms.Button()
            Me.btnWithoutSnapping = New System.Windows.Forms.Button()
            Me.toolBar1 = New System.Windows.Forms.ToolBar()
            Me.tmrWithSnapping = New System.Windows.Forms.Timer(Me.components)
            Me.tmrWithoutSnapping = New System.Windows.Forms.Timer(Me.components)
            Me.GIS = New AxTatukGIS_XDK11.AxTGIS_ViewerWnd()
            Me.panel1.SuspendLayout()
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'panel1
            '
            Me.panel1.Controls.Add(Me.btnWithSnapping)
            Me.panel1.Controls.Add(Me.btnWithoutSnapping)
            Me.panel1.Controls.Add(Me.toolBar1)
            Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.panel1.Location = New System.Drawing.Point(0, 0)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(602, 29)
            Me.panel1.TabIndex = 0
            '
            'btnWithSnapping
            '
            Me.btnWithSnapping.Location = New System.Drawing.Point(129, 2)
            Me.btnWithSnapping.Name = "btnWithSnapping"
            Me.btnWithSnapping.Size = New System.Drawing.Size(144, 25)
            Me.btnWithSnapping.TabIndex = 2
            Me.btnWithSnapping.Text = "Start (with snapping)"
            '
            'btnWithoutSnapping
            '
            Me.btnWithoutSnapping.Location = New System.Drawing.Point(0, 2)
            Me.btnWithoutSnapping.Name = "btnWithoutSnapping"
            Me.btnWithoutSnapping.Size = New System.Drawing.Size(129, 25)
            Me.btnWithoutSnapping.TabIndex = 1
            Me.btnWithoutSnapping.Text = "Start w/o snapping"
            '
            'toolBar1
            '
            Me.toolBar1.AutoSize = False
            Me.toolBar1.DropDownArrows = True
            Me.toolBar1.Location = New System.Drawing.Point(0, 0)
            Me.toolBar1.Name = "toolBar1"
            Me.toolBar1.ShowToolTips = True
            Me.toolBar1.Size = New System.Drawing.Size(602, 29)
            Me.toolBar1.TabIndex = 0
            '
            'tmrWithSnapping
            '
            Me.tmrWithSnapping.Interval = 50
            '
            'tmrWithoutSnapping
            '
            Me.tmrWithoutSnapping.Interval = 50
            '
            'GIS
            '
            Me.GIS.Cursor = System.Windows.Forms.Cursors.Default
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.Enabled = True
            Me.GIS.Location = New System.Drawing.Point(0, 29)
            Me.GIS.Name = "GIS"
            Me.GIS.OcxState = CType(resources.GetObject("GIS.OcxState"), System.Windows.Forms.AxHost.State)
            Me.GIS.Size = New System.Drawing.Size(602, 437)
            Me.GIS.TabIndex = 1
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(602, 466)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.panel1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Snapping"
            Me.panel1.ResumeLayout(False)
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
            Dim ll As TGIS_LayerVector

            ' lets open streets
            GIS.Open(GisUtils.GisSamplesDataDirDownload() & "\World\Countries\USA\States\California\San Bernardino\TIGER\tl_2008_06071_edges_trunc.SHP")
            GIS.Zoom = GIS.Zoom * 5
            GIS.CenterViewport(GisUtils.GisPoint(-117.0208, 34.0629))

            ' now create a points layer
            ll = New TGIS_LayerVector()
            ll.Path = "trackingpoints"
            ll.CS = GIS.CS
            GIS.Add(ll)
            ll.Params.Labels.Allocator = False

            ' and attach to it our test police-car point
            shpPolice = ll.CreateShape(TGIS_ShapeType.Point)
            shpPolice.Params.Marker.Symbol = GisUtils.SymbolList.Prepare(GisUtils.GisSamplesDataDirDownload() & "\Symbols\police.bmp?TRUE")
            shpPolice.Params.Marker.Size = -13
            shpPolice.Params.Labels.OutlineWidth = 0
            shpPolice.Params.Labels.Pattern = TGIS_BrushStyle.Clear
            shpPolice.Params.Labels.Position = TGIS_LabelPosition.DownCenter
            shpPolice.Params.Labels.Value = "112"
        End Sub

        Private Sub WinForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Leave
            ' close viewer - all layers and shapes will be free
            GIS.Close()
        End Sub

        Private Sub btnWithoutSnapping_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnWithoutSnapping.Click
            btnWithoutSnapping.Enabled = False
            btnWithSnapping.Enabled = False

            ' Let's travel from center of the screen
            shpPolice.SetPosition(GIS.Center, Nothing, 0)

            cntPoint = 0
            tmrWithoutSnapping.Enabled = True
        End Sub

        Private Sub btnWithSnapping_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnWithSnapping.Click
            btnWithoutSnapping.Enabled = False
            btnWithSnapping.Enabled = False

            ' Let's travel from Left-dwon to Upper-right
            shpPolice.SetPosition(GIS.Center, Nothing, 0)

            cntPoint = 0
            tmrWithSnapping.Enabled = True
        End Sub

        Private Sub tmrWithSnapping_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrWithSnapping.Tick
            Dim ptg As New TGIS_Point

            ' to protect against circular calling
            tmrWithSnapping.Enabled = False

            ' let's move in some aribtrary direction
            ' normally you can read here a GPS position etc.
            ptg.X = shpPolice.Centroid().X - 0.0002
            ptg.Y = shpPolice.Centroid().Y + 0.0001

            ' move icon over the map
            ' is not elegant to access Items[0] but its only sample :>
            shpPolice.Lock(TGIS_Lock.Projection)
            shpPolice.SetPosition(ptg, CType(GIS.Items.item(0), TGIS_LayerVector), 0.05)
            shpPolice.Unlock()
            cntPoint += 1

            ' not end? - reenable the timer
            If cntPoint < 120 Then
                tmrWithSnapping.Enabled = True
            Else
                btnWithoutSnapping.Enabled = True
                btnWithSnapping.Enabled = True
            End If
        End Sub

        Private Sub tmrWithoutSnapping_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrWithoutSnapping.Tick
            Dim ptg As New TGIS_Point

            ' to protect against circular calling
            tmrWithoutSnapping.Enabled = False

            ' let's move in some aribtrary direction
            ' normally you can read here a GPS position etc.
            ptg.X = shpPolice.Centroid().X - 0.0002
            ptg.Y = shpPolice.Centroid().Y + 0.0001

            ' move icon over the map
            shpPolice.Lock(TGIS_Lock.Projection)
            shpPolice.SetPosition(ptg, Nothing, 0)
            shpPolice.Unlock()
            cntPoint += 1

            ' not end? - reenable the timer
            If cntPoint < 120 Then
                tmrWithoutSnapping.Enabled = True
            Else
                btnWithoutSnapping.Enabled = True
                btnWithSnapping.Enabled = True
            End If
        End Sub
    End Class
End Namespace
