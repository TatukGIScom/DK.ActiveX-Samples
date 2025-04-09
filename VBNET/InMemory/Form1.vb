Option Strict Off
Option Explicit On

Imports TatukGIS_XDK11


Friend Class Form1
    Inherits System.Windows.Forms.Form
#Region "Windows Form Designer generated code "
    Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub
    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
        If Disposing Then
            If Not components Is Nothing Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(Disposing)
    End Sub
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Public ToolTip1 As System.Windows.Forms.ToolTip
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Friend WithEvents ToolBar1 As System.Windows.Forms.ToolBar
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents GIS As AxTatukGIS_XDK11.AxTGIS_ViewerWnd
    Friend WithEvents Button3 As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ToolBar1 = New System.Windows.Forms.ToolBar()
        Me.StatusBar1 = New System.Windows.Forms.StatusBar()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.GIS = New AxTatukGIS_XDK11.AxTGIS_ViewerWnd()
        CType(Me.GIS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolBar1
        '
        Me.ToolBar1.AutoSize = False
        Me.ToolBar1.DropDownArrows = True
        Me.ToolBar1.Location = New System.Drawing.Point(0, 0)
        Me.ToolBar1.Name = "ToolBar1"
        Me.ToolBar1.ShowToolTips = True
        Me.ToolBar1.Size = New System.Drawing.Size(479, 28)
        Me.ToolBar1.TabIndex = 0
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 315)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Size = New System.Drawing.Size(479, 22)
        Me.StatusBar1.TabIndex = 1
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(2, 2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(78, 23)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "Create Layer"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(79, 2)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 4
        Me.Button2.Text = "Add Points"
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(228, 2)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(75, 23)
        Me.Button4.TabIndex = 5
        Me.Button4.Text = "Animate"
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(153, 2)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(75, 23)
        Me.Button3.TabIndex = 6
        Me.Button3.Text = "Add Lines"
        '
        'GIS
        '
        Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GIS.Enabled = True
        Me.GIS.Location = New System.Drawing.Point(0, 28)
        Me.GIS.Name = "GIS"
        Me.GIS.OcxState = CType(resources.GetObject("GIS.OcxState"), System.Windows.Forms.AxHost.State)
        Me.GIS.Size = New System.Drawing.Size(479, 287)
        Me.GIS.TabIndex = 7
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(479, 337)
        Me.Controls.Add(Me.GIS)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.StatusBar1)
        Me.Controls.Add(Me.ToolBar1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "Form1"
        Me.Text = "TatukGIS Samples: Locate"
        CType(Me.GIS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region


    Dim GisUtils As New TGIS_Utils
    Dim SymbolList As New TGIS_SymbolList()
    Private Declare Function GetTickCount Lib "kernel32.dll" () As Integer

    Sub DoWaitEvents(ByRef msWait As Integer)
        Dim msEnd As Integer
        msEnd = GetTickCount + msWait
        Do
            System.Windows.Forms.Application.DoEvents()
        Loop While GetTickCount < msEnd
    End Sub

    Private Sub Button1_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Button1.Click
        Dim ll As TGIS_LayerVector
        ll = New TGIS_LayerVector()
        ll.Name = "symbols"
        ll.Params.Marker.Symbol = SymbolList.Prepare(GisUtils.GisSamplesDataDirDownload & "\Symbols\2267.cgm")
        ll.Params.Marker.Size = -20
        ll.Params.Marker.SymbolRotate = Math.PI / 3
        ll.Params.Line.Symbol = SymbolList.Prepare(GisUtils.GisSamplesDataDirDownload & "\Symbols\1301.cgm")
        ll.Params.Line.Width = -5
        GIS.Add(ll)
        ll.Extent = GisUtils.GisExtent(-180, -90, 180, 90)
        GIS.FullExtent()

    End Sub

    Private Sub Button2_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Button2.Click
        Dim i As Short
        Dim shp As TGIS_Shape
        Dim ll As TGIS_Layer
        Dim c As New TGIS_Color
        If GIS.IsEmpty Then
            MsgBox("First create Layer")
            Exit Sub
        End If

        ll = GIS.Items.Item(0)

        Randomize()
        For i = 0 To 100
            shp = ll.CreateShape(TGIS_ShapeType.Point)
            shp.AddPart()
            shp.AddPoint(GisUtils.GisPoint(180 - Rnd() * 360, 90 - Rnd() * 180))
            shp.Params.Marker.Color = c.FromRGB(Rnd() * 255, Rnd() * 255, Rnd() * 255)
        Next
        GIS.FullExtent()


    End Sub

    Private Sub Button3_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Button3.Click
        Dim i As Short
        Dim shp As TGIS_Shape
        Dim ll As TGIS_LayerVector

        If GIS.IsEmpty Then
            MsgBox("First create Layer")
            Exit Sub
        End If

        ll = GIS.Items.Item(0)

        Randomize()
        shp = ll.CreateShape(TGIS_ShapeType.Arc)
        shp.AddPart()
        For i = 0 To 20
            shp.AddPoint(GisUtils.GisPoint(180 - Rnd() * 360, 90 - Rnd() * 180))
        Next
        GIS.InvalidateWholeMap()


    End Sub

    Private Sub Button4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim i As Short
        Dim shp As ITGIS_Shape
        Dim ll As ITGIS_LayerVector
        Dim c As New TGIS_Color

        If GIS.IsEmpty Then
            MsgBox("First create Layer")
            Exit Sub
        End If

        ll = GIS.Items.Item(0)

        shp = ll.CreateShape(TGIS_ShapeType.Point)
        shp.AddPart()
        shp.AddPoint(GisUtils.GisPoint(0, 0))

        shp.Params.Marker.Color = c.Blue
        shp.Params.Marker.Size = -20

        shp.Invalidate()

        For i = 0 To 90
            If GIS.IsEmpty Then End ' window was closed
            shp.SetPosition(GisUtils.GisPoint(i * 2, i), Nothing, 0)
            shp.Params.Marker.SymbolRotate = Math.PI / 180 * i * 3
            shp.Invalidate()
            DoWaitEvents(100)
        Next
    End Sub
End Class
