<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnCreateGrid = New System.Windows.Forms.Button()
        Me.btnCreateBitmap = New System.Windows.Forms.Button()
        Me.btnAverageColor = New System.Windows.Forms.Button()
        Me.btnMinMax = New System.Windows.Forms.Button()
        Me.btnProfile = New System.Windows.Forms.Button()
        Me.Memo = New System.Windows.Forms.TextBox()
        Me.GIS = New AxTatukGIS_XDK11.AxTGIS_ViewerWnd()
        Me.GIS_Legend = New AxTatukGIS_XDK11.AxTGIS_ControlLegend()
        Me.Panel1.SuspendLayout()
        CType(Me.GIS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GIS_Legend, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnCreateGrid)
        Me.Panel1.Controls.Add(Me.btnCreateBitmap)
        Me.Panel1.Controls.Add(Me.btnAverageColor)
        Me.Panel1.Controls.Add(Me.btnMinMax)
        Me.Panel1.Controls.Add(Me.btnProfile)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(630, 24)
        Me.Panel1.TabIndex = 0
        '
        'btnCreateGrid
        '
        Me.btnCreateGrid.Location = New System.Drawing.Point(480, 0)
        Me.btnCreateGrid.Name = "btnCreateGrid"
        Me.btnCreateGrid.Size = New System.Drawing.Size(120, 22)
        Me.btnCreateGrid.TabIndex = 4
        Me.btnCreateGrid.Text = "Create new GRD"
        Me.btnCreateGrid.UseVisualStyleBackColor = True
        '
        'btnCreateBitmap
        '
        Me.btnCreateBitmap.Location = New System.Drawing.Point(360, 0)
        Me.btnCreateBitmap.Name = "btnCreateBitmap"
        Me.btnCreateBitmap.Size = New System.Drawing.Size(120, 22)
        Me.btnCreateBitmap.TabIndex = 3
        Me.btnCreateBitmap.Text = "Create new JPG"
        Me.btnCreateBitmap.UseVisualStyleBackColor = True
        '
        'btnAverageColor
        '
        Me.btnAverageColor.Location = New System.Drawing.Point(240, 0)
        Me.btnAverageColor.Name = "btnAverageColor"
        Me.btnAverageColor.Size = New System.Drawing.Size(120, 22)
        Me.btnAverageColor.TabIndex = 2
        Me.btnAverageColor.Text = "Bitmap average color"
        Me.btnAverageColor.UseVisualStyleBackColor = True
        '
        'btnMinMax
        '
        Me.btnMinMax.Location = New System.Drawing.Point(120, 0)
        Me.btnMinMax.Name = "btnMinMax"
        Me.btnMinMax.Size = New System.Drawing.Size(120, 22)
        Me.btnMinMax.TabIndex = 1
        Me.btnMinMax.Text = "Grid Min/Max"
        Me.btnMinMax.UseVisualStyleBackColor = True
        '
        'btnProfile
        '
        Me.btnProfile.Location = New System.Drawing.Point(0, 0)
        Me.btnProfile.Name = "btnProfile"
        Me.btnProfile.Size = New System.Drawing.Size(120, 22)
        Me.btnProfile.TabIndex = 0
        Me.btnProfile.Text = "Terrain profile"
        Me.btnProfile.UseVisualStyleBackColor = True
        '
        'Memo
        '
        Me.Memo.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Memo.Location = New System.Drawing.Point(0, 382)
        Me.Memo.Multiline = True
        Me.Memo.Name = "Memo"
        Me.Memo.ReadOnly = True
        Me.Memo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Memo.Size = New System.Drawing.Size(630, 83)
        Me.Memo.TabIndex = 1
        '
        'GIS
        '
        Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GIS.Enabled = True
        Me.GIS.Location = New System.Drawing.Point(163, 24)
        Me.GIS.Name = "GIS"
        Me.GIS.OcxState = CType(resources.GetObject("GIS.OcxState"), System.Windows.Forms.AxHost.State)
        Me.GIS.Size = New System.Drawing.Size(467, 358)
        Me.GIS.TabIndex = 3
        '
        'GIS_Legend
        '
        Me.GIS_Legend.Dock = System.Windows.Forms.DockStyle.Left
        Me.GIS_Legend.Enabled = True
        Me.GIS_Legend.Location = New System.Drawing.Point(0, 24)
        Me.GIS_Legend.Name = "GIS_Legend"
        Me.GIS_Legend.OcxState = CType(resources.GetObject("GIS_Legend.OcxState"), System.Windows.Forms.AxHost.State)
        Me.GIS_Legend.Size = New System.Drawing.Size(163, 358)
        Me.GIS_Legend.TabIndex = 2
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(630, 465)
        Me.Controls.Add(Me.GIS)
        Me.Controls.Add(Me.GIS_Legend)
        Me.Controls.Add(Me.Memo)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "TatukGIS Samples - PixelEdit"
        Me.Panel1.ResumeLayout(False)
        CType(Me.GIS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GIS_Legend, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents btnProfile As Button
    Friend WithEvents btnMinMax As Button
    Friend WithEvents btnAverageColor As Button
    Friend WithEvents btnCreateBitmap As Button
    Friend WithEvents btnCreateGrid As Button
    Friend WithEvents Memo As TextBox
    Friend WithEvents GIS_Legend As AxTatukGIS_XDK11.AxTGIS_ControlLegend
    Friend WithEvents GIS As AxTatukGIS_XDK11.AxTGIS_ViewerWnd
End Class
