Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports System.IO
Imports System.Globalization
Imports TatukGIS_XDK11

Namespace GPSTracker
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
        Private toolBarButton1 As System.Windows.Forms.ToolBarButton
        Private btnSave As System.Windows.Forms.ToolBarButton
        Private toolBarButton2 As System.Windows.Forms.ToolBarButton
        Private btnRecord As System.Windows.Forms.ToolBarButton
        Private toolBarButton3 As System.Windows.Forms.ToolBarButton
        Private WithEvents cbxCom As System.Windows.Forms.ComboBox
        Private WithEvents cbxBaud As System.Windows.Forms.ComboBox
        Private toolTip1 As System.Windows.Forms.ToolTip
        Private panel2 As System.Windows.Forms.Panel
        Private WithEvents GPS As TGIS_GpsNmea
        Private edtPoint As System.Windows.Forms.TextBox
        Private toolTip2 As System.Windows.Forms.ToolTip
        Private WithEvents btnAdd As System.Windows.Forms.Button
        Private panel3 As System.Windows.Forms.Panel
        Private imageList1 As System.Windows.Forms.ImageList
        Private WithEvents GIS As AxTatukGIS_XDK11.AxTGIS_ViewerWnd
        Private currShape As TGIS_Shape
        Private lastPointGps As TGIS_Point
        Private lastPointMap As TGIS_Point
        Private label1 As System.Windows.Forms.Label

        Public Sub New()
            '
            ' Required for Windows Form Designer support
            '
            InitializeComponent()

            '
            ' TODO: Add any constructor code after InitializeComponent call
            '
            toolTip1.SetToolTip(cbxCom, "Select com port")
            toolTip2.SetToolTip(edtPoint, "Type point name here and click Add")
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
            Me.cbxBaud = New System.Windows.Forms.ComboBox()
            Me.cbxCom = New System.Windows.Forms.ComboBox()
            Me.toolBar1 = New System.Windows.Forms.ToolBar()
            Me.toolBarButton1 = New System.Windows.Forms.ToolBarButton()
            Me.btnSave = New System.Windows.Forms.ToolBarButton()
            Me.toolBarButton2 = New System.Windows.Forms.ToolBarButton()
            Me.btnRecord = New System.Windows.Forms.ToolBarButton()
            Me.toolBarButton3 = New System.Windows.Forms.ToolBarButton()
            Me.imageList1 = New System.Windows.Forms.ImageList(Me.components)
            Me.toolTip1 = New System.Windows.Forms.ToolTip(Me.components)
            Me.panel2 = New System.Windows.Forms.Panel()
            Me.btnAdd = New System.Windows.Forms.Button()
            Me.edtPoint = New System.Windows.Forms.TextBox()
            Me.panel3 = New System.Windows.Forms.Panel()
            Me.label1 = New System.Windows.Forms.Label()
            Me.toolTip2 = New System.Windows.Forms.ToolTip(Me.components)
            Me.GIS = New AxTatukGIS_XDK11.AxTGIS_ViewerWnd()
            Me.panel1.SuspendLayout()
            Me.panel2.SuspendLayout()
            Me.panel3.SuspendLayout()
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'panel1
            '
            Me.panel1.Controls.Add(Me.cbxBaud)
            Me.panel1.Controls.Add(Me.cbxCom)
            Me.panel1.Controls.Add(Me.toolBar1)
            Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.panel1.Location = New System.Drawing.Point(0, 0)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(613, 33)
            Me.panel1.TabIndex = 0
            '
            'cbxBaud
            '
            Me.cbxBaud.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cbxBaud.Items.AddRange(New Object() {"1200", "2400", "4800", "9600", "19200"})
            Me.cbxBaud.Location = New System.Drawing.Point(127, 2)
            Me.cbxBaud.Name = "cbxBaud"
            Me.cbxBaud.Size = New System.Drawing.Size(72, 21)
            Me.cbxBaud.TabIndex = 2
            Me.cbxBaud.TabStop = False
            '
            'cbxCom
            '
            Me.cbxCom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cbxCom.Items.AddRange(New Object() {"Com 1", "Com 2", "Com 3", "Com 4", "Com 5", "Com 6", "Com 7", "Com 8", "Com 9", "Com 10"})
            Me.cbxCom.Location = New System.Drawing.Point(60, 2)
            Me.cbxCom.Name = "cbxCom"
            Me.cbxCom.Size = New System.Drawing.Size(67, 21)
            Me.cbxCom.TabIndex = 1
            Me.cbxCom.TabStop = False
            '
            'toolBar1
            '
            Me.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
            Me.toolBar1.AutoSize = False
            Me.toolBar1.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.toolBarButton1, Me.btnSave, Me.toolBarButton2, Me.btnRecord, Me.toolBarButton3})
            Me.toolBar1.DropDownArrows = True
            Me.toolBar1.ImageList = Me.imageList1
            Me.toolBar1.Location = New System.Drawing.Point(0, 0)
            Me.toolBar1.Name = "toolBar1"
            Me.toolBar1.ShowToolTips = True
            Me.toolBar1.Size = New System.Drawing.Size(613, 33)
            Me.toolBar1.TabIndex = 0
            '
            'toolBarButton1
            '
            Me.toolBarButton1.Name = "toolBarButton1"
            Me.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
            '
            'btnSave
            '
            Me.btnSave.ImageIndex = 0
            Me.btnSave.Name = "btnSave"
            Me.btnSave.ToolTipText = "Save the data"
            '
            'toolBarButton2
            '
            Me.toolBarButton2.Name = "toolBarButton2"
            Me.toolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
            '
            'btnRecord
            '
            Me.btnRecord.ImageIndex = 1
            Me.btnRecord.Name = "btnRecord"
            Me.btnRecord.ToolTipText = "Turn on/off route recording"
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
            '
            'panel2
            '
            Me.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.panel2.Controls.Add(Me.btnAdd)
            Me.panel2.Controls.Add(Me.edtPoint)
            Me.panel2.Controls.Add(Me.panel3)
            Me.panel2.Dock = System.Windows.Forms.DockStyle.Left
            Me.panel2.Location = New System.Drawing.Point(0, 33)
            Me.panel2.Name = "panel2"
            Me.panel2.Size = New System.Drawing.Size(225, 433)
            Me.panel2.TabIndex = 1
            '
            'btnAdd
            '
            Me.btnAdd.Enabled = False
            Me.btnAdd.Location = New System.Drawing.Point(72, 256)
            Me.btnAdd.Name = "btnAdd"
            Me.btnAdd.Size = New System.Drawing.Size(75, 25)
            Me.btnAdd.TabIndex = 2
            Me.btnAdd.Text = "&Add"
            '
            'edtPoint
            '
            Me.edtPoint.Enabled = False
            Me.edtPoint.Location = New System.Drawing.Point(16, 224)
            Me.edtPoint.Name = "edtPoint"
            Me.edtPoint.Size = New System.Drawing.Size(185, 20)
            Me.edtPoint.TabIndex = 1
            Me.edtPoint.Text = "Type name here"
            '
            'panel3
            '
            Me.panel3.Controls.Add(Me.label1)
            Me.panel3.Dock = System.Windows.Forms.DockStyle.Top
            Me.panel3.Location = New System.Drawing.Point(0, 0)
            Me.panel3.Name = "panel3"
            Me.panel3.Size = New System.Drawing.Size(221, 41)
            Me.panel3.TabIndex = 3
            '
            'label1
            '
            Me.label1.Location = New System.Drawing.Point(8, 16)
            Me.label1.Name = "label1"
            Me.label1.Size = New System.Drawing.Size(208, 15)
            Me.label1.TabIndex = 0
            Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'GIS
            '
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.Enabled = True
            Me.GIS.Location = New System.Drawing.Point(225, 33)
            Me.GIS.Name = "GIS"
            Me.GIS.OcxState = CType(resources.GetObject("GIS.OcxState"), System.Windows.Forms.AxHost.State)
            Me.GIS.Size = New System.Drawing.Size(388, 433)
            Me.GIS.TabIndex = 2
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(613, 466)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.panel2)
            Me.Controls.Add(Me.panel1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS GPS Tracker (NMEA)"
            Me.panel1.ResumeLayout(False)
            Me.panel2.ResumeLayout(False)
            Me.panel2.PerformLayout()
            Me.panel3.ResumeLayout(False)
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
            Dim i As Int32
            Dim lv As TGIS_LayerVector

            GPS = New TGIS_GpsNmea()

            cbxCom.SelectedIndex = GPS.Com - 1

            For i = 0 To cbxBaud.Items.Count - 1
                If Convert.ToInt32(cbxBaud.Items(i)) = GPS.BaudRate Then
                    cbxBaud.SelectedIndex = i
                End If
            Next

            GPS.Active = True

            GIS.Open(GisUtils.GisSamplesDataDirDownload() & "\World\WorldDCW\world.shp")

            GIS.Add(GisUtils.GisCreateLayer("routes", "routes.kml"))
            GIS.Add(GisUtils.GisCreateLayer("points", "points.kml"))

            lv = CType(GIS.Get("routes"), TGIS_LayerVector)
            lv.Params.Line.Color = (New TGIS_Color).Red
            Try
                lv.AddField("Date", TGIS_FieldType.String, 10, 0)
            Catch
            End Try

            lv = CType(GIS.Get("points"), TGIS_LayerVector)
            Try
                lv.AddField("Name", TGIS_FieldType.String, 10, 0)
            Catch
            End Try

            lv = CType(GIS.Get("points"), TGIS_LayerVector)
            Try
                lv.AddField("Date", TGIS_FieldType.String, 10, 0)
            Catch
            End Try

            lv.Params.Labels.Value = "{Name}<br><i>{date}</i>"

            GPS.Active = True
        End Sub

        Private Sub cbxCom_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbxCom.SelectedIndexChanged
            GPS.Com = cbxCom.SelectedIndex + 1
            GPS.Active = True
        End Sub

        Private Sub cbxBaud_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbxBaud.SelectedIndexChanged
            GPS.BaudRate = Int32.Parse(CStr(cbxBaud.Items(cbxBaud.SelectedIndex)))
            GPS.Active = True
        End Sub

        Private Sub GPS_Position() Handles GPS.PositionEvent
            Dim ptg As TGIS_Point
            Dim dist As Double
            Dim prec As Double
            Dim cs As TGIS_CSGeographicCoordinateSystem

            If CType((DateTime.Now - GPS.PositionTime), TimeSpan).Hours < 1 Then
                edtPoint.Enabled = True
                btnAdd.Enabled = True
            Else
                edtPoint.Enabled = False
                btnAdd.Enabled = False
            End If

            ' calculate delta of two read-out (in meters)

            cs = GisUtils.CSGeographicCoordinateSystemList.ByEPSG(4326)

            ptg = GisUtils.GisPoint(GPS.Longitude, GPS.Latitude)
            dist = cs.Datum.Ellipsoid.Distance(ptg, lastPointGps)
            lastPointGps = ptg
            lastPointMap = GIS.CS.FromWGS(ptg)

            If Not btnRecord.Pushed Then
                Exit Sub
            End If

            prec = GPS.PositionPrec
            If prec = 0 Then
                prec = 5
            End If

            ' check if point in tolerance

            If dist < prec Then
                Exit Sub
            End If

            If Not currShape Is Nothing Then
                currShape.AddPoint(lastPointMap)
                currShape.SetField("Date", DateTime.Now.ToString("G", DateTimeFormatInfo.InvariantInfo))
            End If
            currShape = (CType(GIS.Get("routes"), TGIS_LayerVector)).CreateShape(TGIS_ShapeType.Arc)
            currShape.AddPart()
            currShape.AddPoint(lastPointMap)

            GIS.Center = lastPointMap
        End Sub

        Private Sub GIS_MouseMove(ByVal sender As Object, ByVal e As AxTatukGIS_XDK11.ITGIS_ViewerWndEvents_MouseMoveEvent) Handles GIS.MouseMoveEvent
            Dim ptg As TGIS_Point
            Dim lon, lat As String

            If GIS.IsEmpty Then
                Return
            End If

            ptg = GIS.CS.ToWGS(GIS.ScreenToMap(GisUtils.Point(e.X, e.Y)))

            Try
                lon = GisUtils.GisLongitudeToStr(ptg.X * Math.PI / 180)
            Catch
                lon = "???"
            End Try

            Try
                lat = GisUtils.GisLatitudeToStr(ptg.Y * Math.PI / 180)
            Catch
                lat = "???"
            End Try

            label1.Text = String.Format("{0} : {1}", lon, lat)
        End Sub

        Private Sub toolBar1_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles toolBar1.ButtonClick
            Select Case toolBar1.Buttons.IndexOf(e.Button)
                Case 1
                    ' btnSave
                    actSaveExecute(sender)
                Case 3
                    ' btnRecord
                    actRecordExecute(sender)
            End Select
        End Sub

        Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
            Dim shp As TGIS_Shape

            shp = (CType(GIS.Get("points"), TGIS_LayerVector)).CreateShape(TGIS_ShapeType.Point)
            shp.AddPart()
            shp.AddPoint(lastPointMap)
            shp.SetField("Name", edtPoint.Text)
            shp.SetField("Date", DateTime.Now.ToString("G", DateTimeFormatInfo.InvariantInfo))

            GIS.Center = lastPointMap
        End Sub

        Private Sub actSaveExecute(ByVal sender As Object)
            If Not sender Is btnSave Then
                btnSave.Pushed = True
                Application.DoEvents()
            End If

            If Not currShape Is Nothing Then
                currShape.AddPoint(lastPointMap)
                currShape = Nothing
            End If

            GIS.SaveAll()

            If Not sender Is btnSave Then
                btnSave.Pushed = False
                Application.DoEvents()
            End If
        End Sub

        Private Sub actRecordExecute(ByVal sender As Object)
            btnRecord.Pushed = Not btnRecord.Pushed

            If (Not btnRecord.Pushed) Then
                ' make recording inactive
                currShape = Nothing
            End If
        End Sub

        Private Sub WinForm_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
            GIS.Editor.EndEdit()
            If (Not GIS.MustSave()) Then
                Return
            End If

            If MessageBox.Show("Save all unsaved work?", "TatukGIS", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                GIS.SaveAll()
            End If
        End Sub
    End Class
End Namespace
