Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports TatukGIS_XDK11


Namespace SQLLayerAdvanced

    ''' <summary>
    ''' Summary description for WinForm.
    ''' </summary>
    Public Class WinForm

        Inherits System.Windows.Forms.Form

        Private GIS_Legend As AxTatukGIS_XDK11.AxTGIS_ControlLegend
        Private panel1 As Panel
        Private GIS As AxTatukGIS_XDK11.AxTGIS_ViewerWnd
        Private memo As RichTextBox
        Private WithEvents btnLog As Button
        Private WithEvents btnAttach As Button
        Private WithEvents btnBuild As Button
        Private groupBox1 As GroupBox
        Private WithEvents btnOpenStyles As Button
        Private cbStyles As ComboBox
        Private WithEvents btnGetStyles As Button
        Private WithEvents btnOpenProjects As Button
        Private cbProjects As ComboBox
        Private WithEvents btnGetProjects As Button
        Private WithEvents btnWriteConfigStyle As Button
        Private WithEvents btnWriteConfigProject As Button
        Private WithEvents btnImport As Button
        Private WithEvents btnOpen As Button
        Private ilIcons As ImageList
        Private WithEvents btnZoom As Button
        Private WithEvents btnDrag As Button
        Private WithEvents btnFullExtent As Button
        Private currDir As String

        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer

        Public Sub New()
            MyBase.New
            '
            ' Required for Windows Form Designer support
            '
            Me.InitializeComponent()
            '
            ' TODO: Add any constructor code after InitializeComponent call
            '
        End Sub

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If (Not (Me.components) Is Nothing) Then
                    Me.components.Dispose()
                End If

            End If

            MyBase.Dispose(disposing)
        End Sub
#Region "Windows Form Designer generated code"

        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WinForm))
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.groupBox1 = New System.Windows.Forms.GroupBox()
            Me.btnOpenStyles = New System.Windows.Forms.Button()
            Me.cbStyles = New System.Windows.Forms.ComboBox()
            Me.btnGetStyles = New System.Windows.Forms.Button()
            Me.btnOpenProjects = New System.Windows.Forms.Button()
            Me.cbProjects = New System.Windows.Forms.ComboBox()
            Me.btnGetProjects = New System.Windows.Forms.Button()
            Me.btnWriteConfigStyle = New System.Windows.Forms.Button()
            Me.btnWriteConfigProject = New System.Windows.Forms.Button()
            Me.btnImport = New System.Windows.Forms.Button()
            Me.btnOpen = New System.Windows.Forms.Button()
            Me.btnLog = New System.Windows.Forms.Button()
            Me.btnAttach = New System.Windows.Forms.Button()
            Me.btnBuild = New System.Windows.Forms.Button()
            Me.memo = New System.Windows.Forms.RichTextBox()
            Me.ilIcons = New System.Windows.Forms.ImageList(Me.components)
            Me.btnZoom = New System.Windows.Forms.Button()
            Me.btnDrag = New System.Windows.Forms.Button()
            Me.btnFullExtent = New System.Windows.Forms.Button()
            Me.GIS = New AxTatukGIS_XDK11.AxTGIS_ViewerWnd()
            Me.GIS_Legend = New AxTatukGIS_XDK11.AxTGIS_ControlLegend()
            Me.panel1.SuspendLayout()
            Me.groupBox1.SuspendLayout()
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.GIS_Legend, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'panel1
            '
            Me.panel1.Controls.Add(Me.groupBox1)
            Me.panel1.Controls.Add(Me.btnLog)
            Me.panel1.Controls.Add(Me.btnAttach)
            Me.panel1.Controls.Add(Me.btnBuild)
            Me.panel1.Location = New System.Drawing.Point(-1, 21)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(219, 456)
            Me.panel1.TabIndex = 4
            '
            'groupBox1
            '
            Me.groupBox1.Controls.Add(Me.btnOpenStyles)
            Me.groupBox1.Controls.Add(Me.cbStyles)
            Me.groupBox1.Controls.Add(Me.btnGetStyles)
            Me.groupBox1.Controls.Add(Me.btnOpenProjects)
            Me.groupBox1.Controls.Add(Me.cbProjects)
            Me.groupBox1.Controls.Add(Me.btnGetProjects)
            Me.groupBox1.Controls.Add(Me.btnWriteConfigStyle)
            Me.groupBox1.Controls.Add(Me.btnWriteConfigProject)
            Me.groupBox1.Controls.Add(Me.btnImport)
            Me.groupBox1.Controls.Add(Me.btnOpen)
            Me.groupBox1.Location = New System.Drawing.Point(4, 103)
            Me.groupBox1.Name = "groupBox1"
            Me.groupBox1.Size = New System.Drawing.Size(209, 346)
            Me.groupBox1.TabIndex = 3
            Me.groupBox1.TabStop = False
            Me.groupBox1.Text = "Embedded projects"
            '
            'btnOpenStyles
            '
            Me.btnOpenStyles.Location = New System.Drawing.Point(133, 255)
            Me.btnOpenStyles.Name = "btnOpenStyles"
            Me.btnOpenStyles.Size = New System.Drawing.Size(70, 23)
            Me.btnOpenStyles.TabIndex = 7
            Me.btnOpenStyles.Text = "Apply"
            Me.btnOpenStyles.UseVisualStyleBackColor = True
            '
            'cbStyles
            '
            Me.cbStyles.FormattingEnabled = True
            Me.cbStyles.Location = New System.Drawing.Point(6, 255)
            Me.cbStyles.Name = "cbStyles"
            Me.cbStyles.Size = New System.Drawing.Size(121, 21)
            Me.cbStyles.TabIndex = 8
            '
            'btnGetStyles
            '
            Me.btnGetStyles.Location = New System.Drawing.Point(6, 226)
            Me.btnGetStyles.Name = "btnGetStyles"
            Me.btnGetStyles.Size = New System.Drawing.Size(197, 23)
            Me.btnGetStyles.TabIndex = 6
            Me.btnGetStyles.Text = "6. Get available styles"
            Me.btnGetStyles.UseVisualStyleBackColor = True
            '
            'btnOpenProjects
            '
            Me.btnOpenProjects.Location = New System.Drawing.Point(133, 180)
            Me.btnOpenProjects.Name = "btnOpenProjects"
            Me.btnOpenProjects.Size = New System.Drawing.Size(70, 23)
            Me.btnOpenProjects.TabIndex = 4
            Me.btnOpenProjects.Text = "Open"
            Me.btnOpenProjects.UseVisualStyleBackColor = True
            '
            'cbProjects
            '
            Me.cbProjects.FormattingEnabled = True
            Me.cbProjects.Location = New System.Drawing.Point(6, 180)
            Me.cbProjects.Name = "cbProjects"
            Me.cbProjects.Size = New System.Drawing.Size(121, 21)
            Me.cbProjects.TabIndex = 5
            '
            'btnGetProjects
            '
            Me.btnGetProjects.Location = New System.Drawing.Point(6, 151)
            Me.btnGetProjects.Name = "btnGetProjects"
            Me.btnGetProjects.Size = New System.Drawing.Size(197, 23)
            Me.btnGetProjects.TabIndex = 4
            Me.btnGetProjects.Text = "5. Get available projects"
            Me.btnGetProjects.UseVisualStyleBackColor = True
            '
            'btnWriteConfigStyle
            '
            Me.btnWriteConfigStyle.Location = New System.Drawing.Point(6, 106)
            Me.btnWriteConfigStyle.Name = "btnWriteConfigStyle"
            Me.btnWriteConfigStyle.Size = New System.Drawing.Size(197, 23)
            Me.btnWriteConfigStyle.TabIndex = 3
            Me.btnWriteConfigStyle.Text = "4. Write styles configuration"
            Me.btnWriteConfigStyle.UseVisualStyleBackColor = True
            '
            'btnWriteConfigProject
            '
            Me.btnWriteConfigProject.Location = New System.Drawing.Point(6, 77)
            Me.btnWriteConfigProject.Name = "btnWriteConfigProject"
            Me.btnWriteConfigProject.Size = New System.Drawing.Size(197, 23)
            Me.btnWriteConfigProject.TabIndex = 2
            Me.btnWriteConfigProject.Text = "3. Write project configuration"
            Me.btnWriteConfigProject.UseVisualStyleBackColor = True
            '
            'btnImport
            '
            Me.btnImport.Location = New System.Drawing.Point(6, 48)
            Me.btnImport.Name = "btnImport"
            Me.btnImport.Size = New System.Drawing.Size(197, 23)
            Me.btnImport.TabIndex = 1
            Me.btnImport.Text = "2. Import layers"
            Me.btnImport.UseVisualStyleBackColor = True
            '
            'btnOpen
            '
            Me.btnOpen.Location = New System.Drawing.Point(6, 19)
            Me.btnOpen.Name = "btnOpen"
            Me.btnOpen.Size = New System.Drawing.Size(197, 23)
            Me.btnOpen.TabIndex = 0
            Me.btnOpen.Text = "1. Open sample project"
            Me.btnOpen.UseVisualStyleBackColor = True
            '
            'btnLog
            '
            Me.btnLog.Location = New System.Drawing.Point(14, 67)
            Me.btnLog.Name = "btnLog"
            Me.btnLog.Size = New System.Drawing.Size(114, 23)
            Me.btnLog.TabIndex = 2
            Me.btnLog.Text = "Log changes"
            Me.btnLog.UseVisualStyleBackColor = True
            '
            'btnAttach
            '
            Me.btnAttach.Location = New System.Drawing.Point(14, 38)
            Me.btnAttach.Name = "btnAttach"
            Me.btnAttach.Size = New System.Drawing.Size(114, 23)
            Me.btnAttach.TabIndex = 1
            Me.btnAttach.Text = "Attach trace event"
            Me.btnAttach.UseVisualStyleBackColor = True
            '
            'btnBuild
            '
            Me.btnBuild.Location = New System.Drawing.Point(14, 9)
            Me.btnBuild.Name = "btnBuild"
            Me.btnBuild.Size = New System.Drawing.Size(114, 23)
            Me.btnBuild.TabIndex = 0
            Me.btnBuild.Text = "Build empty layer"
            Me.btnBuild.UseVisualStyleBackColor = True
            '
            'memo
            '
            Me.memo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.memo.Location = New System.Drawing.Point(-1, 476)
            Me.memo.Name = "memo"
            Me.memo.ReadOnly = True
            Me.memo.Size = New System.Drawing.Size(873, 120)
            Me.memo.TabIndex = 6
            Me.memo.Text = ""
            '
            'ilIcons
            '
            Me.ilIcons.ImageStream = CType(resources.GetObject("ilIcons.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.ilIcons.TransparentColor = System.Drawing.Color.Fuchsia
            Me.ilIcons.Images.SetKeyName(0, "Drag.bmp")
            Me.ilIcons.Images.SetKeyName(1, "FullExtent.bmp")
            Me.ilIcons.Images.SetKeyName(2, "ZoomEx.bmp")
            '
            'btnZoom
            '
            Me.btnZoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnZoom.ImageIndex = 2
            Me.btnZoom.ImageList = Me.ilIcons
            Me.btnZoom.Location = New System.Drawing.Point(28, -1)
            Me.btnZoom.Name = "btnZoom"
            Me.btnZoom.Size = New System.Drawing.Size(30, 23)
            Me.btnZoom.TabIndex = 7
            Me.btnZoom.UseVisualStyleBackColor = True
            '
            'btnDrag
            '
            Me.btnDrag.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnDrag.ImageIndex = 0
            Me.btnDrag.ImageList = Me.ilIcons
            Me.btnDrag.Location = New System.Drawing.Point(57, -1)
            Me.btnDrag.Name = "btnDrag"
            Me.btnDrag.Size = New System.Drawing.Size(30, 23)
            Me.btnDrag.TabIndex = 8
            Me.btnDrag.UseVisualStyleBackColor = True
            '
            'btnFullExtent
            '
            Me.btnFullExtent.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnFullExtent.ImageIndex = 1
            Me.btnFullExtent.ImageList = Me.ilIcons
            Me.btnFullExtent.Location = New System.Drawing.Point(-1, -1)
            Me.btnFullExtent.Name = "btnFullExtent"
            Me.btnFullExtent.Size = New System.Drawing.Size(30, 23)
            Me.btnFullExtent.TabIndex = 9
            Me.btnFullExtent.UseVisualStyleBackColor = True
            '
            'GIS
            '
            Me.GIS.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GIS.Enabled = True
            Me.GIS.Location = New System.Drawing.Point(217, 21)
            Me.GIS.Name = "GIS"
            Me.GIS.OcxState = CType(resources.GetObject("GIS.OcxState"), System.Windows.Forms.AxHost.State)
            Me.GIS.Size = New System.Drawing.Size(501, 456)
            Me.GIS.TabIndex = 5
            '
            'GIS_Legend
            '
            Me.GIS_Legend.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GIS_Legend.Enabled = True
            Me.GIS_Legend.Location = New System.Drawing.Point(718, 21)
            Me.GIS_Legend.Name = "GIS_Legend"
            Me.GIS_Legend.OcxState = CType(resources.GetObject("GIS_Legend.OcxState"), System.Windows.Forms.AxHost.State)
            Me.GIS_Legend.Size = New System.Drawing.Size(154, 456)
            Me.GIS_Legend.TabIndex = 0
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(869, 597)
            Me.Controls.Add(Me.btnFullExtent)
            Me.Controls.Add(Me.btnDrag)
            Me.Controls.Add(Me.btnZoom)
            Me.Controls.Add(Me.memo)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.panel1)
            Me.Controls.Add(Me.GIS_Legend)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - SQLLayerAdvanced"
            Me.panel1.ResumeLayout(False)
            Me.groupBox1.ResumeLayout(False)
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.GIS_Legend, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
#End Region

        Dim GisUtils As New TGIS_Utils()

        <STAThread()>
        Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New WinForm)
        End Sub

        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            currDir = AppDomain.CurrentDomain.BaseDirectory
            GIS_Legend.GIS_Viewer = GIS.GetOcx()
        End Sub

        Private Sub btnBuild_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBuild.Click
            Dim lsql As TGIS_LayerSqlSqlite
            lsql = New TGIS_LayerSqlSqlite
            lsql.SetCSByEPSG(4326)
            lsql.Path = getGISTESTPath(False)
            lsql.Build_2(lsql.Path, GisUtils.GisExtent(14, 49, 24, 55), TGIS_ShapeType.Point, TGIS_DimensionType.XY)
            GIS.Add(lsql)
            GIS.FullExtent()
            GIS.InvalidateWholeMap()
        End Sub

        Private Function getGISTESTPath(ByVal _logging As Boolean) As String
            Dim str As String
            If _logging Then
                str = "\nLogging=True\n"
            Else
                str = ""
            End If

            Return ("[TatukGIS Layer]\n" +
                    "Storage=Native\n" +
                    "LAYER=GISTEST\n" +
                    "DIALECT=SQLITE\n" +
                    "Sqlite=" + currDir + "gistest.sqlite\n" +
                    "ENGINEOPTIONS=16" + str + "\n.ttkls"
                   )
        End Function

        Private Sub traceLog(ByVal _str As String)
            memo.AppendText((_str + Environment.NewLine))
        End Sub

        Private Sub btnAttach_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAttach.Click
            Dim lsql As TGIS_LayerSqlSqlite
            GIS.Close()
            lsql = New TGIS_LayerSqlSqlite
            lsql.Path = getGISTESTPath(False)
            ' lsql.SQLExecuteEvent = AddressOf traceLog
            GIS.Add(lsql)
            GIS.FullExtent()
            GIS.InvalidateWholeMap()
        End Sub

        Private Sub btnLog_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLog.Click
            Dim lsql As TGIS_LayerSqlSqlite
            Dim i As Integer
            Dim shp As TGIS_Shape
            Dim logs As TGIS_StringList
            Dim rnd As Random
            GIS.Close()
            rnd = New Random
            lsql = New TGIS_LayerSqlSqlite
            lsql.Path = getGISTESTPath(True)
            lsql.SetCSByEPSG(4326)
            lsql.Build_2(lsql.Path, GisUtils.GisExtent(14, 49, 24, 55), TGIS_ShapeType.Point, TGIS_DimensionType.XY)
            GIS.Add(lsql)
            i = 1
            Do While (i <= 10)
                shp = lsql.CreateShape_2(TGIS_ShapeType.Point, TGIS_DimensionType.XY)
                shp.AddPart()
                shp.AddPoint(GisUtils.GisPoint((14 + rnd.Next(0, 10)), (49 + rnd.Next(0, 10))))
                i = (i + 1)
            Loop

            GIS.SaveData()
            GIS.FullExtent()
            GIS.InvalidateWholeMap()
            logs = lsql.GetLogs
            memo.AppendText(logs.Text)
        End Sub

        Private Sub btnFullExtent_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFullExtent.Click
            If GIS.IsEmpty Then
                Return
            End If

            GIS.FullExtent()
        End Sub

        Private Sub btnZoom_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnZoom.Click
            If GIS.IsEmpty Then
                Return
            End If

            GIS.Mode = TGIS_ViewerMode.Zoom
        End Sub

        Private Sub btnDrag_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDrag.Click
            If GIS.IsEmpty Then
                Return
            End If

            GIS.Mode = TGIS_ViewerMode.Drag
        End Sub

        Private Sub btnOpen_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOpen.Click
            GIS.Open((GisUtils.GisSamplesDataDirDownload + "\World\Countries\Poland\DCW\poland.ttkproject"))
            GIS.InvalidateWholeMap()
        End Sub

        Private Sub btnImport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnImport.Click
            Dim i As Integer
            Dim lv As TGIS_LayerVector
            Dim lsql As TGIS_LayerSqlSqlite
            If GIS.IsEmpty Then
                Return
            End If

            i = 0
            Do While (i _
                        < (GIS.Items.Count - 1))
                lv = CType(GIS.Items.Item(i), TGIS_LayerVector)
                lsql = New TGIS_LayerSqlSqlite
                lsql.Path = "[TatukGIS Layer]\n" +
                            "Storage=Native\n" +
                            "LAYER=" + GisUtils.GisCanonicalSQLName(lv.Name) + "\n" +
                            "DIALECT=SQLITE\n" +
                            "Sqlite=" + currDir + "gistest.sqlite\n" +
                            "ENGINEOPTIONS=16\n.ttkls"
                lsql.SetCSByEPSG(lv.CS.EPSG)
                lsql.ImportLayer(lv, lv.Extent, TGIS_ShapeType.Unknown, "", False)
                i = (i + 1)
            Loop

        End Sub

        Private Sub btnWriteConfigProject_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnWriteConfigProject.Click
            Dim lv As TGIS_LayerVectorSqlAbstract
            Dim lst As TGIS_StringList
            Dim i As Integer
            Dim ll As TGIS_Layer
            Dim cfg As TGIS_Config
            If GIS.IsEmpty Then
                Return
            End If

            lst = New TGIS_StringList
            i = 0
            Do While (i _
                        < (GIS.Items.Count - 1))
                ll = CType(GIS.Items.Item(i), TGIS_Layer)
                ll.Path = "[TatukGIS Layer]\\n" +
                          "Storage=Native\\n" +
                          "Sqlite=<#PATH#>gistest.sqlite\\n" +
                          "Dialect=SQLITE\\n" +
                          "Layer=" + GisUtils.GisCanonicalSQLName(ll.Name) + "\\n" +
                          "Style=" + GisUtils.GisCanonicalSQLName(ll.Name) + "\\n.ttkls"
                i = (i + 1)
            Loop

            cfg = (New TGIS_ConfigFactory).CreateConfig(Nothing, "test.ttkproject")
            GIS.SaveProjectAsEx(cfg, "")
            cfg.GetStrings(lst)
            lv = New TGIS_LayerSqlSqlite
            lv.Path = getGISTESTPath(False)
            GIS.Add(lv)
            lv.CreateProjectTable()
            lv.WriteProject("POLAND", "Map of Poland", lst.Text)
        End Sub

        Private Sub btnWriteConfigStyle_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnWriteConfigStyle.Click
            Dim lv As TGIS_LayerVectorSqlAbstract
            Dim lst As TGIS_StringList
            Dim ll As TGIS_Layer
            Dim la As TGIS_LayerAbstract
            Dim i As Integer
            If GIS.IsEmpty Then
                Return
            End If

            lv = CType(GIS.Get("GISTEST"), TGIS_LayerVectorSqlAbstract)
            lv.CreateStyleTable()
            lst = New TGIS_StringList
            For i = 0 To GIS.Items.Count - 1
                la = GIS.Items.Item(i)
                ll = CType(la, TGIS_Layer)
                CType(GIS.ProjectFile, TGIS_Config).SetLayer(ll)
                lst.Clear()
                ll.ParamsList.SaveToStrings(lst)
                lv.WriteStyle(GisUtils.GisCanonicalSQLName(ll.Name), ll.Caption, lst.Text)
            Next
        End Sub

        Private Sub btnGetProjects_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGetProjects.Click
            Dim lv As TGIS_LayerVectorSqlAbstract
            Dim lst As TGIS_StringList
            Dim i As Integer
            lv = CType(GIS.Get("GISTEST"), TGIS_LayerVectorSqlAbstract)
            If (lv Is Nothing) Then
                lv = New TGIS_LayerSqlSqlite
                lv.Path = getGISTESTPath(False)
                GIS.Add(lv)
            End If

            lst = lv.GetAvailableProjects
            i = 0
            Do While (i < lst.Count)
                cbProjects.Items.Add(lst.Strings(i))
                i = (i + 1)
            Loop

        End Sub

        Private Sub btnGetStyles_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGetStyles.Click
            Dim lv As TGIS_LayerVectorSqlAbstract
            Dim lst As TGIS_StringList
            Dim i As Integer
            lv = CType(GIS.Get("GISTEST"), TGIS_LayerVectorSqlAbstract)
            If (lv Is Nothing) Then
                lv = New TGIS_LayerSqlSqlite
                lv.Path = getGISTESTPath(False)
                GIS.Add(lv)
            End If

            lst = lv.GetAvailableStyles
            i = 0
            Do While (i < lst.Count)
                cbStyles.Items.Add(lst.Strings(i))
                i = (i + 1)
            Loop

        End Sub

        Private Sub btnOpenProjects_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOpenProjects.Click
            Dim lv As TGIS_LayerVectorSqlAbstract
            Dim tkn As TGIS_StringList
            Dim name As String
            lv = CType(GIS.Get("GISTEST"), TGIS_LayerVectorSqlAbstract)
            If (lv Is Nothing) Then
                Return
            End If

            tkn = New TGIS_StringList
            tkn.Add(("PATH=" + GisUtils.ConstructParamString(currDir)))
            name = cbProjects.SelectedItem.ToString
            If (name = "") Then
                name = "POLAND"
            End If

            GIS.OpenEx(lv.GetProject((name + ".ttkproject"), tkn), ".ttkproject")
            GIS.InvalidateWholeMap()
        End Sub

        Private Sub btnOpenStyles_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOpenStyles.Click
            Dim lv As TGIS_LayerVectorSqlAbstract
            If GIS.IsEmpty Then
                Return
            End If

            lv = CType(GIS.Get("GISTEST"), TGIS_LayerVectorSqlAbstract)
            If (lv Is Nothing) Then
                Return
            End If

            lv.ApplyStyle(lv.ReadStyle(cbStyles.SelectedItem.ToString))
            GIS.InvalidateWholeMap()
        End Sub
    End Class
End Namespace
