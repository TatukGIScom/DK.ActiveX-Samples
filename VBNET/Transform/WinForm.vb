Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports TatukGIS_XDK11


Namespace Transform

    ''' <summary>
    ''' Summary description for WinForm.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form

        Private WithEvents btnTransform As Button

        Private WithEvents btnCutting As Button

        Private WithEvents btnSave As Button

        Private WithEvents btnRead As Button

        Private WithEvents GIS As AxTatukGIS_XDK11.AxTGIS_ViewerWnd

        Private GIS_TRN_EXT As String = ".trn"

        Private lbCoords As Label

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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WinForm))
            Me.btnTransform = New System.Windows.Forms.Button()
            Me.btnCutting = New System.Windows.Forms.Button()
            Me.btnSave = New System.Windows.Forms.Button()
            Me.btnRead = New System.Windows.Forms.Button()
            Me.lbCoords = New System.Windows.Forms.Label()
            Me.GIS = New AxTatukGIS_XDK11.AxTGIS_ViewerWnd()
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'btnTransform
            '
            Me.btnTransform.Location = New System.Drawing.Point(12, 12)
            Me.btnTransform.Name = "btnTransform"
            Me.btnTransform.Size = New System.Drawing.Size(102, 23)
            Me.btnTransform.TabIndex = 0
            Me.btnTransform.Text = "Transform"
            Me.btnTransform.UseVisualStyleBackColor = True
            '
            'btnCutting
            '
            Me.btnCutting.Location = New System.Drawing.Point(13, 42)
            Me.btnCutting.Name = "btnCutting"
            Me.btnCutting.Size = New System.Drawing.Size(101, 23)
            Me.btnCutting.TabIndex = 1
            Me.btnCutting.Text = "Cutting polygon"
            Me.btnCutting.UseVisualStyleBackColor = True
            '
            'btnSave
            '
            Me.btnSave.Location = New System.Drawing.Point(13, 72)
            Me.btnSave.Name = "btnSave"
            Me.btnSave.Size = New System.Drawing.Size(101, 23)
            Me.btnSave.TabIndex = 2
            Me.btnSave.Text = "Save to file"
            Me.btnSave.UseVisualStyleBackColor = True
            '
            'btnRead
            '
            Me.btnRead.Location = New System.Drawing.Point(13, 102)
            Me.btnRead.Name = "btnRead"
            Me.btnRead.Size = New System.Drawing.Size(101, 23)
            Me.btnRead.TabIndex = 3
            Me.btnRead.Text = "Read from file"
            Me.btnRead.UseVisualStyleBackColor = True
            '
            'lbCoords
            '
            Me.lbCoords.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.lbCoords.AutoSize = True
            Me.lbCoords.Location = New System.Drawing.Point(123, 542)
            Me.lbCoords.Name = "lbCoords"
            Me.lbCoords.Size = New System.Drawing.Size(0, 13)
            Me.lbCoords.TabIndex = 5
            '
            'GIS
            '
            Me.GIS.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GIS.Enabled = True
            Me.GIS.Location = New System.Drawing.Point(129, 12)
            Me.GIS.Name = "GIS"
            Me.GIS.OcxState = CType(resources.GetObject("GIS.OcxState"), System.Windows.Forms.AxHost.State)
            Me.GIS.Size = New System.Drawing.Size(440, 537)
            Me.GIS.TabIndex = 4
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(581, 561)
            Me.Controls.Add(Me.lbCoords)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.btnRead)
            Me.Controls.Add(Me.btnSave)
            Me.Controls.Add(Me.btnCutting)
            Me.Controls.Add(Me.btnTransform)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Transform"
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
#End Region

        Dim GisUtils As New TGIS_Utils()

        <STAThread()>
        Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New WinForm())
        End Sub

        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            GIS.Open((GisUtils.GisSamplesDataDirDownload + "\Samples\Rectify\satellite.jpg"))
        End Sub

        Private Sub btnTransform_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTransform.Click
            Dim trn As TGIS_TransformPolynomial
            Dim lp As TGIS_LayerPixel
            lp = CType(GIS.Items.item(0), TGIS_LayerPixel)
            trn = New TGIS_TransformPolynomial
            trn.AddPoint_2(GisUtils.GisPoint(-0.5, -944.5), GisUtils.GisPoint(1273285.84090909, 239703.615056818), 0, True)
            trn.AddPoint_2(GisUtils.GisPoint(-0.5, 0.5), GisUtils.GisPoint(1273285.84090909, 244759.524147727), 1, True)
            trn.AddPoint_2(GisUtils.GisPoint(1246.5, 0.5), GisUtils.GisPoint(1279722.65909091, 245859.524147727), 2, True)
            trn.AddPoint_2(GisUtils.GisPoint(1246.5, -944.5), GisUtils.GisPoint(1279744.93181818, 239725.887784091), 3, True)
            trn.Prepare(TGIS_PolynomialOrder.First)
            lp.Transform = trn
            lp.Transform.Active = True
            lp.SetCSByEPSG(102748)
            GIS.RecalcExtent()
            GIS.FullExtent()
        End Sub

        Private Sub btnCutting_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCutting.Click
            Dim trn As TGIS_TransformPolynomial
            Dim lp As TGIS_LayerPixel
            lp = CType(GIS.Items.item(0), TGIS_LayerPixel)
            trn = New TGIS_TransformPolynomial
            trn.AddPoint_2(GisUtils.GisPoint(-0.5, -944.5), GisUtils.GisPoint(1273285.84090909, 239703.615056818), 0, True)
            trn.AddPoint_2(GisUtils.GisPoint(-0.5, 0.5), GisUtils.GisPoint(1273285.84090909, 244759.524147727), 1, True)
            trn.AddPoint_2(GisUtils.GisPoint(1246.5, 0.5), GisUtils.GisPoint(1279722.65909091, 244759.524147727), 2, True)
            trn.AddPoint_2(GisUtils.GisPoint(1246.5, -944.5), GisUtils.GisPoint(1279744.93181818, 239725.887784091), 3, True)
            trn.CuttingPolygon = "POLYGON((421.508902077151 -320.017804154303," +
                                 "518.161721068249 -223.364985163205," +
                                 "688.725519287834 -210.572700296736," +
                                 "864.974777448071 -254.635014836795," +
                                 "896.244807121662 -335.652818991098," +
                                 "894.823442136499 -453.626112759644," +
                                 "823.755192878338 -615.661721068249," +
                                 "516.740356083086 -607.13353115727," +
                                 "371.761127596439 -533.222551928783," +
                                 "340.491097922849 -456.46884272997," +
                                 "421.508902077151 -320.017804154303))"

            trn.Prepare(TGIS_PolynomialOrder.First)
            lp.Transform = trn
            lp.Transform.Active = True
            GIS.RecalcExtent()
            GIS.FullExtent()
        End Sub

        Private Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
            Dim lp As TGIS_LayerPixel
            lp = CType(GIS.Items.item(0), TGIS_LayerPixel)
            If (Not (lp.Transform) Is Nothing) Then
                lp.Transform.SaveToFile(("satellite.jpg" + GIS_TRN_EXT))
            End If

        End Sub

        Private Sub btnRead_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRead.Click
            Dim lp As TGIS_LayerPixel
            Dim trn As TGIS_TransformPolynomial
            lp = CType(GIS.Items.item(0), TGIS_LayerPixel)

            trn = New TGIS_TransformPolynomial
            trn.LoadFromFile(("satellite.jpg" + GIS_TRN_EXT))
            lp.Transform = trn
            lp.Transform.Active = True

            GIS.RecalcExtent()
            GIS.FullExtent()
        End Sub

        Private Sub GIS_MouseMove(ByVal sender As Object, ByVal e As AxTatukGIS_XDK11.ITGIS_ViewerWndEvents_MouseMoveEvent) Handles GIS.MouseMoveEvent
            Dim ptg As TGIS_Point
            If GIS.IsEmpty Then
                Return
            End If

            ptg = GIS.ScreenToMap(GisUtils.Point(e.X, e.Y))
            ' lbCoords.Text = "X: " + ptg.X + " | Y:" + ptg.Y;
            lbCoords.Text = String.Format("X: {0:0.0000} | Y: {1:0.0000}", ptg.X, ptg.Y)
        End Sub
    End Class
End Namespace
