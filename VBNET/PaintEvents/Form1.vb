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
  Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
  Friend WithEvents ToolBar1 As System.Windows.Forms.ToolBar
  Friend WithEvents ToolBarButton1 As System.Windows.Forms.ToolBarButton
  Friend WithEvents ToolBarButton2 As System.Windows.Forms.ToolBarButton
  Friend WithEvents ToolBarButton3 As System.Windows.Forms.ToolBarButton
  Friend WithEvents CheckDrag As System.Windows.Forms.CheckBox
  Friend WithEvents ToolBarButton4 As System.Windows.Forms.ToolBarButton
  '  Friend WithEvents GIS As AxTatukGIS_XDK11.AxTGIS_ViewerWnd
  Private WithEvents GIS As AxTatukGIS_XDK11.AxTGIS_ViewerWnd
  Friend WithEvents Panel1 As Panel
  Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
  Friend WithEvents chkAfterPaintRendererEvent As CheckBox
  Friend WithEvents chkPaintExtraEvent As CheckBox
  Friend WithEvents chkBeforePaintRendererEvent As CheckBox
  Friend WithEvents btnTestPrintBmp As Button
  Friend WithEvents chkPrintBmpWithEvents As CheckBox
  Friend WithEvents SaveFileDialog1 As SaveFileDialog
  Friend WithEvents grbRenderers As GroupBox
  Friend WithEvents rbGdi32 As RadioButton
  Friend WithEvents rbGdiPlus As RadioButton
  Friend WithEvents rbDirect2D As RadioButton
  Private center_ptg As TGIS_Point


  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
    Me.ToolBar1 = New System.Windows.Forms.ToolBar()
    Me.ToolBarButton1 = New System.Windows.Forms.ToolBarButton()
    Me.ToolBarButton2 = New System.Windows.Forms.ToolBarButton()
    Me.ToolBarButton3 = New System.Windows.Forms.ToolBarButton()
    Me.ToolBarButton4 = New System.Windows.Forms.ToolBarButton()
    Me.CheckDrag = New System.Windows.Forms.CheckBox()
    Me.StatusBar1 = New System.Windows.Forms.StatusBar()
    Me.GIS = New AxTatukGIS_XDK11.AxTGIS_ViewerWnd()
    Me.Panel1 = New System.Windows.Forms.Panel()
    Me.grbRenderers = New System.Windows.Forms.GroupBox()
    Me.rbGdi32 = New System.Windows.Forms.RadioButton()
    Me.rbGdiPlus = New System.Windows.Forms.RadioButton()
    Me.rbDirect2D = New System.Windows.Forms.RadioButton()
    Me.chkPrintBmpWithEvents = New System.Windows.Forms.CheckBox()
    Me.btnTestPrintBmp = New System.Windows.Forms.Button()
    Me.chkAfterPaintRendererEvent = New System.Windows.Forms.CheckBox()
    Me.chkPaintExtraEvent = New System.Windows.Forms.CheckBox()
    Me.chkBeforePaintRendererEvent = New System.Windows.Forms.CheckBox()
    Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
    CType(Me.GIS, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.Panel1.SuspendLayout()
    Me.grbRenderers.SuspendLayout()
    Me.SuspendLayout()
    '
    'ImageList1
    '
    Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
    Me.ImageList1.TransparentColor = System.Drawing.Color.Magenta
    Me.ImageList1.Images.SetKeyName(0, "")
    Me.ImageList1.Images.SetKeyName(1, "")
    Me.ImageList1.Images.SetKeyName(2, "")
    '
    'ToolBar1
    '
    Me.ToolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
    Me.ToolBar1.AutoSize = False
    Me.ToolBar1.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.ToolBarButton1, Me.ToolBarButton2, Me.ToolBarButton3, Me.ToolBarButton4})
    Me.ToolBar1.DropDownArrows = True
    Me.ToolBar1.ImageList = Me.ImageList1
    Me.ToolBar1.Location = New System.Drawing.Point(0, 0)
    Me.ToolBar1.Name = "ToolBar1"
    Me.ToolBar1.ShowToolTips = True
    Me.ToolBar1.Size = New System.Drawing.Size(776, 28)
    Me.ToolBar1.TabIndex = 0
    '
    'ToolBarButton1
    '
    Me.ToolBarButton1.ImageIndex = 0
    Me.ToolBarButton1.Name = "ToolBarButton1"
    '
    'ToolBarButton2
    '
    Me.ToolBarButton2.ImageIndex = 1
    Me.ToolBarButton2.Name = "ToolBarButton2"
    '
    'ToolBarButton3
    '
    Me.ToolBarButton3.ImageIndex = 2
    Me.ToolBarButton3.Name = "ToolBarButton3"
    '
    'ToolBarButton4
    '
    Me.ToolBarButton4.Name = "ToolBarButton4"
    Me.ToolBarButton4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
    '
    'CheckDrag
    '
    Me.CheckDrag.Location = New System.Drawing.Point(96, 5)
    Me.CheckDrag.Name = "CheckDrag"
    Me.CheckDrag.Size = New System.Drawing.Size(125, 18)
    Me.CheckDrag.TabIndex = 1
    Me.CheckDrag.Text = "Draging"
    '
    'StatusBar1
    '
    Me.StatusBar1.Location = New System.Drawing.Point(0, 580)
    Me.StatusBar1.Name = "StatusBar1"
    Me.StatusBar1.Size = New System.Drawing.Size(776, 18)
    Me.StatusBar1.TabIndex = 2
    '
    'GIS
    '
    Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
    Me.GIS.Enabled = True
    Me.GIS.Location = New System.Drawing.Point(0, 28)
    Me.GIS.Name = "GIS"
    Me.GIS.OcxState = CType(resources.GetObject("GIS.OcxState"), System.Windows.Forms.AxHost.State)
    Me.GIS.Size = New System.Drawing.Size(552, 552)
    Me.GIS.TabIndex = 3
    '
    'Panel1
    '
    Me.Panel1.Controls.Add(Me.grbRenderers)
    Me.Panel1.Controls.Add(Me.chkPrintBmpWithEvents)
    Me.Panel1.Controls.Add(Me.btnTestPrintBmp)
    Me.Panel1.Controls.Add(Me.chkAfterPaintRendererEvent)
    Me.Panel1.Controls.Add(Me.chkPaintExtraEvent)
    Me.Panel1.Controls.Add(Me.chkBeforePaintRendererEvent)
    Me.Panel1.Dock = System.Windows.Forms.DockStyle.Right
    Me.Panel1.Location = New System.Drawing.Point(552, 28)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(224, 552)
    Me.Panel1.TabIndex = 4
    '
    'grbRenderers
    '
    Me.grbRenderers.Controls.Add(Me.rbGdi32)
    Me.grbRenderers.Controls.Add(Me.rbGdiPlus)
    Me.grbRenderers.Controls.Add(Me.rbDirect2D)
    Me.grbRenderers.Location = New System.Drawing.Point(16, 370)
    Me.grbRenderers.Name = "grbRenderers"
    Me.grbRenderers.Size = New System.Drawing.Size(188, 116)
    Me.grbRenderers.TabIndex = 7
    Me.grbRenderers.TabStop = False
    Me.grbRenderers.Text = "Renderers"
    '
    'rbGdi32
    '
    Me.rbGdi32.AutoSize = True
    Me.rbGdi32.Location = New System.Drawing.Point(8, 75)
    Me.rbGdi32.Name = "rbGdi32"
    Me.rbGdi32.Size = New System.Drawing.Size(69, 21)
    Me.rbGdi32.TabIndex = 2
    Me.rbGdi32.Text = "GDI32"
    Me.rbGdi32.UseVisualStyleBackColor = True
    '
    'rbGdiPlus
    '
    Me.rbGdiPlus.AutoSize = True
    Me.rbGdiPlus.Location = New System.Drawing.Point(8, 50)
    Me.rbGdiPlus.Name = "rbGdiPlus"
    Me.rbGdiPlus.Size = New System.Drawing.Size(61, 21)
    Me.rbGdiPlus.TabIndex = 1
    Me.rbGdiPlus.Text = "GDI+"
    Me.rbGdiPlus.UseVisualStyleBackColor = True
    '
    'rbDirect2D
    '
    Me.rbDirect2D.AutoSize = True
    Me.rbDirect2D.Location = New System.Drawing.Point(8, 25)
    Me.rbDirect2D.Name = "rbDirect2D"
    Me.rbDirect2D.Size = New System.Drawing.Size(84, 21)
    Me.rbDirect2D.TabIndex = 0
    Me.rbDirect2D.Text = "Direct2D"
    Me.rbDirect2D.UseVisualStyleBackColor = True
    '
    'chkPrintBmpWithEvents
    '
    Me.chkPrintBmpWithEvents.AutoSize = True
    Me.chkPrintBmpWithEvents.Checked = True
    Me.chkPrintBmpWithEvents.CheckState = System.Windows.Forms.CheckState.Checked
    Me.chkPrintBmpWithEvents.Location = New System.Drawing.Point(16, 250)
    Me.chkPrintBmpWithEvents.Name = "chkPrintBmpWithEvents"
    Me.chkPrintBmpWithEvents.Size = New System.Drawing.Size(161, 21)
    Me.chkPrintBmpWithEvents.TabIndex = 6
    Me.chkPrintBmpWithEvents.Text = "PrintBmp with events"
    Me.chkPrintBmpWithEvents.UseVisualStyleBackColor = True
    '
    'btnTestPrintBmp
    '
    Me.btnTestPrintBmp.Location = New System.Drawing.Point(16, 213)
    Me.btnTestPrintBmp.Name = "btnTestPrintBmp"
    Me.btnTestPrintBmp.Size = New System.Drawing.Size(188, 28)
    Me.btnTestPrintBmp.TabIndex = 5
    Me.btnTestPrintBmp.Text = "Test PrintBmp"
    Me.btnTestPrintBmp.UseVisualStyleBackColor = True
    '
    'chkAfterPaintRendererEvent
    '
    Me.chkAfterPaintRendererEvent.AutoSize = True
    Me.chkAfterPaintRendererEvent.Checked = True
    Me.chkAfterPaintRendererEvent.CheckState = System.Windows.Forms.CheckState.Checked
    Me.chkAfterPaintRendererEvent.Location = New System.Drawing.Point(16, 113)
    Me.chkAfterPaintRendererEvent.Name = "chkAfterPaintRendererEvent"
    Me.chkAfterPaintRendererEvent.Size = New System.Drawing.Size(188, 21)
    Me.chkAfterPaintRendererEvent.TabIndex = 4
    Me.chkAfterPaintRendererEvent.Text = "AfterPaintRendererEvent"
    Me.chkAfterPaintRendererEvent.UseVisualStyleBackColor = True
    '
    'chkPaintExtraEvent
    '
    Me.chkPaintExtraEvent.AutoSize = True
    Me.chkPaintExtraEvent.Checked = True
    Me.chkPaintExtraEvent.CheckState = System.Windows.Forms.CheckState.Checked
    Me.chkPaintExtraEvent.Location = New System.Drawing.Point(16, 77)
    Me.chkPaintExtraEvent.Name = "chkPaintExtraEvent"
    Me.chkPaintExtraEvent.Size = New System.Drawing.Size(130, 21)
    Me.chkPaintExtraEvent.TabIndex = 2
    Me.chkPaintExtraEvent.Text = "PaintExtraEvent"
    Me.chkPaintExtraEvent.UseVisualStyleBackColor = True
    '
    'chkBeforePaintRendererEvent
    '
    Me.chkBeforePaintRendererEvent.AutoSize = True
    Me.chkBeforePaintRendererEvent.Checked = True
    Me.chkBeforePaintRendererEvent.CheckState = System.Windows.Forms.CheckState.Checked
    Me.chkBeforePaintRendererEvent.Location = New System.Drawing.Point(16, 41)
    Me.chkBeforePaintRendererEvent.Name = "chkBeforePaintRendererEvent"
    Me.chkBeforePaintRendererEvent.Size = New System.Drawing.Size(200, 21)
    Me.chkBeforePaintRendererEvent.TabIndex = 0
    Me.chkBeforePaintRendererEvent.Text = "BeforePaintRendererEvent"
    Me.chkBeforePaintRendererEvent.UseVisualStyleBackColor = True
    '
    'Form1
    '
    Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
    Me.ClientSize = New System.Drawing.Size(776, 598)
    Me.Controls.Add(Me.GIS)
    Me.Controls.Add(Me.Panel1)
    Me.Controls.Add(Me.StatusBar1)
    Me.Controls.Add(Me.CheckDrag)
    Me.Controls.Add(Me.ToolBar1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Location = New System.Drawing.Point(4, 23)
    Me.Name = "Form1"
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
    Me.Text = "TatukGIS Samples: PaintEvents"
    CType(Me.GIS, System.ComponentModel.ISupportInitialize).EndInit()
    Me.Panel1.ResumeLayout(False)
    Me.Panel1.PerformLayout()
    Me.grbRenderers.ResumeLayout(False)
    Me.grbRenderers.PerformLayout()
    Me.ResumeLayout(False)

  End Sub
#End Region


  Dim GisUtils As New TGIS_Utils()

  Private Sub ButtonFullExtent_Click()
    GIS.FullExtent()
  End Sub

  Private Sub ButtonZoomIn_Click()
    GIS.Zoom = GIS.Zoom * 2
  End Sub

  Private Sub ButtonZoomOut_Click()
    GIS.Zoom = GIS.Zoom / 2
  End Sub

  Private Sub CheckDrag_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckDrag.CheckStateChanged
    If CheckDrag.CheckState Then
      GIS.Mode = TGIS_ViewerMode.Drag
    Else
      GIS.Mode = TGIS_ViewerMode.Select
    End If

  End Sub

  Private Sub Form1_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
    Dim ll As TGIS_LayerSHP
    Dim c As New TGIS_Color()

    ll = New TGIS_LayerSHP()
    ll.Path = GisUtils.GisSamplesDataDirDownload + "\World\Countries\USA\States\California\Counties.SHP"
    GIS.Add(ll)
    GIS.FullExtent()
    center_ptg = GIS.CenterPtg
    rbDirect2D.Checked = True
  End Sub

  Private Sub ToolBar1_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles ToolBar1.ButtonClick
    Select Case ToolBar1.Buttons.IndexOf(e.Button)
      Case 0
        GIS.FullExtent()
      Case 1
        GIS.Zoom = GIS.Zoom * 2
      Case 2
        GIS.Zoom = GIS.Zoom / 2
    End Select
  End Sub

  Private Sub chkBeforePaintRendererEvent_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkBeforePaintRendererEvent.CheckedChanged
    If chkBeforePaintRendererEvent.Checked Then
      AddHandler GIS.BeforePaintRendererEvent, AddressOf GIS_BeforePaintRendererEvent
    Else
      RemoveHandler GIS.BeforePaintRendererEvent, AddressOf GIS_BeforePaintRendererEvent
    End If
    GIS.Invalidate()
  End Sub

  Private Sub chkPaintExtraEvent_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkPaintExtraEvent.CheckedChanged
    If chkPaintExtraEvent.Checked Then
      AddHandler GIS.PaintExtraEvent, AddressOf GIS_PaintExtraEvent
    Else
      RemoveHandler GIS.PaintExtraEvent, AddressOf GIS_PaintExtraEvent
    End If
    GIS.Invalidate()
  End Sub

  Private Sub chkAfterPaintRendererEvent_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkAfterPaintRendererEvent.CheckedChanged
    If chkAfterPaintRendererEvent.Checked Then
      AddHandler GIS.AfterPaintRendererEvent, AddressOf GIS_AfterPaintRendererEvent
    Else
      RemoveHandler GIS.AfterPaintRendererEvent, AddressOf GIS_AfterPaintRendererEvent
    End If
    GIS.Invalidate()
  End Sub

  Private Sub GIS_BeforePaintRendererEvent(sender As Object, e As AxTatukGIS_XDK11.ITGIS_ViewerWndEvents_BeforePaintRendererEventEvent)
    Dim rdr As TGIS_RendererAbstract
    Dim cl As New TGIS_Color

    rdr = CType(e.renderer, TGIS_RendererAbstract)

    cl = cl.FromRGB(&HEE, &HE8, &HAA)
    rdr.CanvasPen.Color = cl
    rdr.CanvasPen.Width = 1
    rdr.CanvasPen.Style = TGIS_PenStyle.Solid
    rdr.CanvasBrush.Color = cl
    rdr.CanvasBrush.Style = TGIS_BrushStyle.Solid
    rdr.CanvasDrawRectangle(GisUtils.Rect(0, 0, GIS.Width, GIS.Height))

    rdr.CanvasPen.Color = (New TGIS_Color).Blue
    rdr.CanvasPen.Width = 1
    rdr.CanvasBrush.Style = TGIS_BrushStyle.Clear
    rdr.CanvasDrawRectangle(GisUtils.Rect(10, 10, GIS.Width - 10, GIS.Height - 10))
  End Sub

  Private Sub GIS_PaintExtraEvent(sender As Object, e As AxTatukGIS_XDK11.ITGIS_ViewerWndEvents_PaintExtraEventEvent)
    Dim txt As String
    Dim pt As TPoint
    Dim ptc As TPoint
    Dim cl As New TGIS_Color

    txt = "PaintExtra"
    e.renderer.CanvasFont.Name = "Courier New"
    e.renderer.CanvasFont.Size = 24
    e.renderer.CanvasFont.Color = cl.Blue
    pt = e.renderer.CanvasTextExtent(txt)
    ptc = GIS.MapToScreen(center_ptg)
    e.renderer.CanvasDrawText(GisUtils.Rect(ptc.X - pt.X / 2,
                                            ptc.Y - pt.Y / 2,
                                            ptc.X + pt.X / 2,
                                            ptc.Y + pt.Y / 2),
                              txt)
  End Sub

  Private Sub GIS_AfterPaintRendererEvent(sender As Object, e As AxTatukGIS_XDK11.ITGIS_ViewerWndEvents_AfterPaintRendererEventEvent)
    Dim rdr As TGIS_RendererAbstract

    rdr = CType(e.renderer, TGIS_RendererAbstract)
    rdr.CanvasPen.Color = (New TGIS_Color).Blue
    rdr.CanvasPen.Width = 1
    rdr.CanvasBrush.Style = TGIS_BrushStyle.Clear
    rdr.CanvasDrawRectangle(GisUtils.Rect(100, 100, GIS.Width - 100, GIS.Height - 100))
  End Sub

  Private Sub btnTestPrintBmp_Click(sender As Object, e As System.EventArgs) Handles btnTestPrintBmp.Click
    Dim bitmap As System.Drawing.Bitmap

    SaveFileDialog1.DefaultExt = "BMP"
    SaveFileDialog1.Filter = "Window Bitmap (*.bmp)|*.BMP"
    If SaveFileDialog1.ShowDialog() <> DialogResult.OK Then Return
    bitmap = Nothing
    Try
      GIS.PrintBmp_2(bitmap, chkPrintBmpWithEvents.Checked)
      bitmap.Save(SaveFileDialog1.FileName)
    Finally
      bitmap.Dispose()
    End Try
  End Sub

  Private Sub rbDirect2D_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbDirect2D.CheckedChanged
    If rbDirect2D.Checked Then
      GIS.Renderer = New TGIS_RendererVclDirect2D()
      GIS.InvalidateWholeMap()
    End If
  End Sub

  Private Sub rbGdiPlus_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbGdiPlus.CheckedChanged
    If rbGdiPlus.Checked Then
      GIS.Renderer = New TGIS_RendererVclGdiPlus()
      GIS.InvalidateWholeMap()
    End If
  End Sub

  Private Sub rbGdi32_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbGdi32.CheckedChanged
    If rbGdi32.Checked Then
      GIS.Renderer = New TGIS_RendererVclGdi32()
      GIS.InvalidateWholeMap()
    End If
  End Sub
End Class
