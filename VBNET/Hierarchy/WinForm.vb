Imports System
Imports System.Windows.Forms
Imports System.Drawing
Imports TatukGIS_XDK11

Namespace Hierarchy

    Public Class frmMain
        Inherits System.Windows.Forms.Form

        Friend WithEvents btnHierarchy As System.Windows.Forms.Button
        Friend WithEvents GIS_Legend As AxTatukGIS_XDK11.AxTGIS_ControlLegend
        Friend WithEvents GIS As AxTatukGIS_XDK11.AxTGIS_ViewerWnd

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

        'Form overrides dispose to clean up the component list.
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

#Region "Windows Form Designer generated code"
        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
            Me.btnHierarchy = New System.Windows.Forms.Button()
            Me.GIS_Legend = New AxTatukGIS_XDK11.AxTGIS_ControlLegend()
            Me.GIS = New AxTatukGIS_XDK11.AxTGIS_ViewerWnd()
            CType(Me.GIS_Legend, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'btnHierarchy
            '
            Me.btnHierarchy.Location = New System.Drawing.Point(0, 0)
            Me.btnHierarchy.Name = "btnHierarchy"
            Me.btnHierarchy.Size = New System.Drawing.Size(86, 23)
            Me.btnHierarchy.TabIndex = 0
            Me.btnHierarchy.Text = "Build Hierarchy"
            Me.btnHierarchy.UseVisualStyleBackColor = True
            '
            'GIS_Legend
            '
            Me.GIS_Legend.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.GIS_Legend.Enabled = True
            Me.GIS_Legend.Location = New System.Drawing.Point(0, 23)
            Me.GIS_Legend.Name = "GIS_Legend"
            Me.GIS_Legend.OcxState = CType(resources.GetObject("GIS_Legend.OcxState"), System.Windows.Forms.AxHost.State)
            Me.GIS_Legend.Size = New System.Drawing.Size(180, 387)
            Me.GIS_Legend.TabIndex = 1
            '
            'GIS
            '
            Me.GIS.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GIS.Cursor = System.Windows.Forms.Cursors.Default
            Me.GIS.Enabled = True
            Me.GIS.Location = New System.Drawing.Point(180, 23)
            Me.GIS.Name = "GIS"
            Me.GIS.OcxState = CType(resources.GetObject("GIS.OcxState"), System.Windows.Forms.AxHost.State)
            Me.GIS.Size = New System.Drawing.Size(327, 387)
            Me.GIS.TabIndex = 2
            '
            'frmMain
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(507, 410)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.GIS_Legend)
            Me.Controls.Add(Me.btnHierarchy)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "frmMain"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Hierarchy"
            CType(Me.GIS_Legend, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
#End Region

        Dim GisUtils As New TGIS_Utils()

        <STAThread>
        Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New frmMain())
        End Sub

        Private Sub btnHierarchy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHierarchy.Click
            Dim group As TGIS_HierarchyGroup
            Dim i As Int32
            Dim list As TGIS_StringList

            GIS_Legend.GIS_Viewer = GIS.GetOcx()

            GIS.Close()
            GIS_Legend.Mode = TGIS_ControlLegendMode.Groups

            GIS.Open_2(GisUtils.GisSamplesDataDirDownload() & "\World\Countries\Poland\DCW\poland.ttkproject", False)

            GIS.Hierarchy.ClearGroups()

            group = GIS.Hierarchy.CreateGroup("My group")

            For i = 0 To GIS.Items.Count / 2 - 1
                group.AddLayer(GIS.Items.Item(i))
            Next

            For i = 0 To GIS.Items.Count / 2 - 1
                group.DeleteLayer(GIS.Items.Item(i))
            Next

            group = GIS.Hierarchy.CreateGroup("Root")
            group.CreateGroup("Leaf")

            GIS.Hierarchy.Groups("Leaf").CreateGroup("node").AddLayer(GIS.Get("city1"))

            GIS.Hierarchy.MoveGroup("Root", "My group")

            group = GIS.Hierarchy.CreateGroup("Poland")
            group = group.CreateGroup("Waters")
            group.AddLayer(GIS.Get("Lakes"))
            group.AddLayer(GIS.Get("Rivers"))

            group = GIS.Hierarchy.Groups("Poland").CreateGroup("Areas")
            group.AddLayer(GIS.Get("city"))
            group.AddLayer(GIS.Get("Country area"))

            GIS.Hierarchy.AddOtherLayers()

            list = New TGIS_StringList()

            list.Add("Poland\Waters=Lakes;Rivers")
            list.Add("Poland\Areas=city;Country area")

            GIS.Hierarchy.ClearGroups()
            GIS.Hierarchy.ParseHierarchy_2(list, TGIS_ConfigFormat.Ini)

            GIS_Legend.Update()
            GIS.FullExtent()
        End Sub
    End Class
End Namespace
