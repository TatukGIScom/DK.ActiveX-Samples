Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports TatukGIS_XDK11


Namespace SQLWizard

    ''' <summary>
    ''' Summary description for WinForm.
    ''' </summary>
    Public Class WinForm

        Inherits System.Windows.Forms.Form

        Private ilIcons As ImageList
        Private WithEvents btnFullExtent As Button
        Private WithEvents btnZoom As Button
        Private WithEvents btnDrag As Button
        Private WithEvents btnAddLayer As Button
        Private GIS As AxTatukGIS_XDK11.AxTGIS_ViewerWnd

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
            Me.ilIcons = New System.Windows.Forms.ImageList(Me.components)
            Me.btnFullExtent = New System.Windows.Forms.Button()
            Me.btnZoom = New System.Windows.Forms.Button()
            Me.btnDrag = New System.Windows.Forms.Button()
            Me.btnAddLayer = New System.Windows.Forms.Button()
            Me.GIS = New AxTatukGIS_XDK11.AxTGIS_ViewerWnd()
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'ilIcons
            '
            Me.ilIcons.ImageStream = CType(resources.GetObject("ilIcons.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.ilIcons.TransparentColor = System.Drawing.Color.Fuchsia
            Me.ilIcons.Images.SetKeyName(0, "FullExtent.bmp")
            Me.ilIcons.Images.SetKeyName(1, "ZoomEx.bmp")
            Me.ilIcons.Images.SetKeyName(2, "Drag.bmp")
            Me.ilIcons.Images.SetKeyName(3, "LayerAdd.bmp")
            '
            'btnFullExtent
            '
            Me.btnFullExtent.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnFullExtent.ImageIndex = 0
            Me.btnFullExtent.ImageList = Me.ilIcons
            Me.btnFullExtent.Location = New System.Drawing.Point(0, 0)
            Me.btnFullExtent.Name = "btnFullExtent"
            Me.btnFullExtent.Size = New System.Drawing.Size(33, 23)
            Me.btnFullExtent.TabIndex = 0
            Me.btnFullExtent.UseVisualStyleBackColor = True
            '
            'btnZoom
            '
            Me.btnZoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnZoom.ImageIndex = 1
            Me.btnZoom.ImageList = Me.ilIcons
            Me.btnZoom.Location = New System.Drawing.Point(32, 0)
            Me.btnZoom.Name = "btnZoom"
            Me.btnZoom.Size = New System.Drawing.Size(33, 23)
            Me.btnZoom.TabIndex = 1
            Me.btnZoom.UseVisualStyleBackColor = True
            '
            'btnDrag
            '
            Me.btnDrag.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnDrag.ImageIndex = 2
            Me.btnDrag.ImageList = Me.ilIcons
            Me.btnDrag.Location = New System.Drawing.Point(64, 0)
            Me.btnDrag.Name = "btnDrag"
            Me.btnDrag.Size = New System.Drawing.Size(33, 23)
            Me.btnDrag.TabIndex = 2
            Me.btnDrag.UseVisualStyleBackColor = True
            '
            'btnAddLayer
            '
            Me.btnAddLayer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnAddLayer.ImageIndex = 3
            Me.btnAddLayer.ImageList = Me.ilIcons
            Me.btnAddLayer.Location = New System.Drawing.Point(96, 0)
            Me.btnAddLayer.Name = "btnAddLayer"
            Me.btnAddLayer.Size = New System.Drawing.Size(33, 23)
            Me.btnAddLayer.TabIndex = 3
            Me.btnAddLayer.UseVisualStyleBackColor = True
            '
            'GIS
            '
            Me.GIS.Enabled = True
            Me.GIS.Location = New System.Drawing.Point(0, 29)
            Me.GIS.Name = "GIS"
            Me.GIS.OcxState = CType(resources.GetObject("GIS.OcxState"), System.Windows.Forms.AxHost.State)
            Me.GIS.Size = New System.Drawing.Size(705, 492)
            Me.GIS.TabIndex = 4
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(702, 544)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.btnAddLayer)
            Me.Controls.Add(Me.btnDrag)
            Me.Controls.Add(Me.btnZoom)
            Me.Controls.Add(Me.btnFullExtent)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - SQLWizard"
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).EndInit()
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

        End Sub

        Private Sub btnAddLayer_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddLayer.Click
            Dim sqlForm As SQLForm
            sqlForm = New SQLForm
            sqlForm.setGIS(GIS)
            sqlForm.ShowDialog(Me)
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
    End Class
End Namespace

