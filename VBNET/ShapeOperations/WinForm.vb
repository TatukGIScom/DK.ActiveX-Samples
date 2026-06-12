' =============================================================================
' This source code is a part of TatukGIS Developer Kernel.
' =============================================================================
'
' ShapeOperations sample - Interactive affine transformation of vector shapes.
' ActiveX / XDK11 edition.
'
' This sample demonstrates how to interactively apply affine geometric
' transformations to individual vector shapes using the TatukGIS XDK11 ActiveX
' API.  The key difference from the NDK edition is that the viewer is hosted as
' an ActiveX control (AxTGIS_ViewerWnd) and mouse events arrive via the
' ITGIS_ViewerWndEvents_Mouse* COM event interfaces rather than standard
' WinForms events.  Static utilities live on a TGIS_Utils instance (GisUtils)
' rather than being called as shared methods.
'
'   Rotate:    Spins the selected shape around its centroid.
'              Horizontal mouse movement maps to rotation angle.
'   Scale:     Grows or shrinks the shape using the ratio of current to
'              previous screen coordinates per axis.
'   Move:      Translates the shape by the map-coordinate delta between
'              consecutive mouse-move events.
'
' Two-layer approach:
'   1. Original shapefile layer holds the real data.
'   2. An in-memory TGIS_LayerVector (edtLayer, CachedPaint=False) renders a
'      live preview without modifying the source layer until commit.
'
' Data: Samples\3D\buildings.shp
' =============================================================================

Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports TatukGIS_XDK11

Imports System.Diagnostics

Namespace ShapeOperations

    ''' <summary>
    ''' Main form for the ShapeOperations (ActiveX) sample.
    ''' Demonstrates interactive rotate, scale, and translate operations on
    ''' individual vector shapes using TGIS_Shape.Transform_2 with an affine matrix.
    ''' Transform_2 is the COM-dispatch-safe variant of TGIS_Shape.Transform.
    ''' </summary>
    Public Class WinForm

        Inherits System.Windows.Forms.Form

        Private lbHint As Label
        ' ActiveX-hosted map viewer control; events have COM-event signatures
        Private WithEvents GIS As AxTatukGIS_XDK11.AxTGIS_ViewerWnd

        ' In-memory overlay layer used as a live edit preview.
        ' CachedPaint = False makes it a tracking layer so InvalidateTopmost
        ' causes an immediate repaint without tile caching.
        Private edtLayer As TGIS_LayerVector

        ' Working copy of currShape placed in edtLayer; receives all transforms.
        Private edtShape As TGIS_Shape

        ' The original shape obtained via MakeEditable; CopyGeometry writes back
        ' the final geometry to this object when the user commits.
        Private currShape As TGIS_Shape

        ' Screen-pixel coordinates from the previous mouse event.
        ' Used to derive the rotation angle and scale ratios.
        Private prevX As Integer
        Private prevY As Integer

        ' Map-coordinate position from the previous mouse event.
        ' Used to compute the translation delta in map units.
        Private prevPtg As New TGIS_Point

        ' Flag that enables transform dragging; toggled by the first mouse-up
        ' (select) and cleared by the second mouse-up (commit).
        Private handleMouseMove As Boolean

        Private WithEvents rbRotate As RadioButton  ' Rotate mode selector
        Private WithEvents rbScale As RadioButton   ' Scale mode selector
        Private WithEvents rbMove As RadioButton    ' Move (translate) mode selector

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
            Me.lbHint = New System.Windows.Forms.Label()
            Me.GIS = New AxTatukGIS_XDK11.AxTGIS_ViewerWnd()
            Me.rbRotate = New System.Windows.Forms.RadioButton()
            Me.rbScale = New System.Windows.Forms.RadioButton()
            Me.rbMove = New System.Windows.Forms.RadioButton()
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'lbHint
            '
            Me.lbHint.AutoSize = True
            Me.lbHint.Location = New System.Drawing.Point(197, 9)
            Me.lbHint.Name = "lbHint"
            Me.lbHint.Size = New System.Drawing.Size(0, 13)
            Me.lbHint.TabIndex = 3
            '
            'GIS
            '
            Me.GIS.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GIS.Enabled = True
            Me.GIS.Location = New System.Drawing.Point(-2, 33)
            Me.GIS.Name = "GIS"
            Me.GIS.OcxState = CType(resources.GetObject("GIS.OcxState"), System.Windows.Forms.AxHost.State)
            Me.GIS.Size = New System.Drawing.Size(779, 527)
            Me.GIS.TabIndex = 4
            '
            'rbRotate
            '
            Me.rbRotate.AutoSize = True
            Me.rbRotate.Location = New System.Drawing.Point(12, 7)
            Me.rbRotate.Name = "rbRotate"
            Me.rbRotate.Size = New System.Drawing.Size(57, 17)
            Me.rbRotate.TabIndex = 5
            Me.rbRotate.Text = "Rotate"
            Me.rbRotate.UseVisualStyleBackColor = True
            '
            'rbScale
            '
            Me.rbScale.AutoSize = True
            Me.rbScale.Location = New System.Drawing.Point(75, 7)
            Me.rbScale.Name = "rbScale"
            Me.rbScale.Size = New System.Drawing.Size(52, 17)
            Me.rbScale.TabIndex = 6
            Me.rbScale.Text = "Scale"
            Me.rbScale.UseVisualStyleBackColor = True
            '
            'rbMove
            '
            Me.rbMove.AutoSize = True
            Me.rbMove.Location = New System.Drawing.Point(133, 7)
            Me.rbMove.Name = "rbMove"
            Me.rbMove.Size = New System.Drawing.Size(52, 17)
            Me.rbMove.TabIndex = 7
            Me.rbMove.Text = "Move"
            Me.rbMove.UseVisualStyleBackColor = True
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(777, 561)
            Me.Controls.Add(Me.rbMove)
            Me.Controls.Add(Me.rbScale)
            Me.Controls.Add(Me.rbRotate)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.lbHint)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - ShapeOperations"
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
#End Region

        ' GisUtils provides static-style helper methods for the ActiveX API.
        ' In the NDK edition these are shared methods on TGIS_Utils; here they
        ' require an instance because the XDK11 COM type library exposes them
        ' as instance members.
        Dim GisUtils As New TGIS_Utils()

        <STAThread()>
        Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New WinForm)
        End Sub

        ''' <summary>
        ''' Initialises the sample on form load:
        ''' opens the buildings shapefile, creates the in-memory preview layer,
        ''' and zooms in to a useful starting extent.
        ''' Note: Lock/Unlock is not called here because the ActiveX viewer
        ''' manages its own painting synchronisation internally.
        ''' </summary>
        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            currShape = Nothing
            edtShape = Nothing

            handleMouseMove = False
            ' Pre-select Rotate mode so the UI is in a known state
            rbRotate.PerformClick()

            ' GisUtils.GisSamplesDataDirDownload resolves the shared sample data folder
            GIS.Open((GisUtils.GisSamplesDataDirDownload + "Samples\3D\buildings.shp"))

            ' Create the in-memory edit/preview overlay layer.
            ' CachedPaint = False marks it as a "tracking" layer: bypasses the tile
            ' cache so InvalidateTopmost redraws it immediately on each transform.
            edtLayer = New TGIS_LayerVector()
            edtLayer.CS = GIS.CS                          ' Match viewer coordinate system
            edtLayer.CachedPaint = False                  ' Tracking layer
            edtLayer.Params.Area.Pattern = TGIS_BrushStyle.Clear
            edtLayer.Params.Area.OutlineColor = (New TGIS_Color).Red  ' Red outline
            GIS.Add(edtLayer)

            ' Zoom in 4x from the default extent so buildings are visible
            GIS.Zoom = (GIS.Zoom * 4)
        End Sub

        ''' <summary>
        ''' Core affine transform applied to a shape's geometry.
        '''
        ''' Applies a 3x3 affine matrix centred on the shape's centroid.
        ''' Transform_2 is the COM-dispatch-safe overload used by the ActiveX API.
        '''
        ''' Matrix convention:
        '''   x' = x*_xx + y*_xy + _dx
        '''   y' = x*_yx + y*_yy + _dy
        '''   z' = z  (unchanged)
        '''
        ''' Passing the centroid as the pivot point means the DK translates the
        ''' shape to the origin, applies the matrix, then translates back — so
        ''' rotate and scale operations stay centred on the shape.
        ''' </summary>
        Private Sub TransformSelectedShape(ByVal _shp As TGIS_Shape, ByVal _xx As Double, ByVal _yx As Double, ByVal _xy As Double, ByVal _yy As Double, ByVal _dx As Double, ByVal _dy As Double)
            Dim centroid As TGIS_Point
            If (_shp Is Nothing) Then
                Return
            End If

            ' Compute the geometric centroid so the transform pivots at the shape centre
            centroid = _shp.Centroid

            ' Apply the affine matrix with the centroid as pivot:
            ' x' = x*xx + y*xy + dx
            ' y' = x*yx + y*yy + dy
            ' z' = z
            ' Transform_2 is the disambiguated COM-safe name for TGIS_Shape.Transform
            _shp.Transform_2(GisUtils.GisPoint3DFrom2D(centroid), _xx, _yx, 0, _xy, _yy, 0, 0, 0, 1, _dx, _dy, 0, False)

            ' Refresh only the preview layer for performance
            GIS.InvalidateTopmost()
        End Sub

        ''' <summary>
        ''' Rotates the shape by the given angle (in radians) around its centroid.
        ''' Standard 2-D rotation matrix: | cos(a)  sin(a) | / |-sin(a)  cos(a) |
        ''' </summary>
        Private Sub RotateSelectedShape(ByVal _shp As TGIS_Shape, ByVal _angle As Double)
            TransformSelectedShape(_shp, Math.Cos(_angle), Math.Sin(_angle), (Math.Sin(_angle) * -1), Math.Cos(_angle), 0, 0)
        End Sub

        ''' <summary>
        ''' Scales the shape by independent factors along each axis.
        ''' Values greater than 1 enlarge; values between 0 and 1 shrink.
        ''' </summary>
        Private Sub ScaleSelectedShape(ByVal _shp As TGIS_Shape, ByVal _x As Double, ByVal _y As Double)
            TransformSelectedShape(_shp, _x, 0, 0, _y, 0, 0)
        End Sub

        ''' <summary>
        ''' Translates (moves) the shape by the given map-coordinate offset.
        ''' The identity submatrix leaves all coordinates unchanged; only dx/dy move.
        ''' </summary>
        Private Sub TranslateSelectedShape(ByVal _shp As TGIS_Shape, ByVal _x As Double, ByVal _y As Double)
            TransformSelectedShape(_shp, 1, 0, 0, 1, _x, _y)
        End Sub

        ''' <summary>
        ''' Responds to mouse movement over the ActiveX viewer.
        ''' The event signature uses the COM interface
        ''' ITGIS_ViewerWndEvents_MouseMoveEvent rather than standard MouseEventArgs.
        ''' While handleMouseMove = True, dispatches incremental affine transforms
        ''' to the preview shape in edtLayer.
        ''' </summary>
        Private Sub GIS_MouseMove(ByVal sender As Object, ByVal e As AxTatukGIS_XDK11.ITGIS_ViewerWndEvents_MouseMoveEvent) Handles GIS.MouseMoveEvent
            Dim ptg As TGIS_Point
            If (edtShape Is Nothing) Then
                Return
            End If

            If handleMouseMove Then
                ' GisUtils.Point constructs a TPoint for the ActiveX ScreenToMap call
                ptg = GIS.ScreenToMap(GisUtils.Point(e.X, e.Y))

                If rbRotate.Checked Then
                    ' 1 pixel of horizontal movement = 1 degree of rotation
                    RotateSelectedShape(edtShape, ((Math.PI / 180) * ((e.X - prevX))))
                    ' Rotate by moving the mouse horizontally
                ElseIf rbScale.Checked Then
                    ' Guard against division-by-zero at the very first event
                    If ((prevX <> 0) _
                                AndAlso (prevY <> 0)) Then
                        ScaleSelectedShape(edtShape, (e.X / prevX), (e.Y / prevY))
                    End If

                ElseIf rbMove.Checked Then
                    ' Delta in map units between this event and the last
                    TranslateSelectedShape(edtShape, (ptg.X - prevPtg.X), (ptg.Y - prevPtg.Y))
                End If

                ' Update previous-position tracking for the next incremental step
                prevPtg.X = ptg.X
                prevPtg.Y = ptg.Y
                prevX = e.X
                prevY = e.Y
            End If

        End Sub

        ''' <summary>
        ''' Handles both the shape-selection click (first mouse-up) and the
        ''' geometry-commit click (second mouse-up).
        ''' The event uses the COM interface ITGIS_ViewerWndEvents_MouseUpEvent.
        '''
        ''' First click (currShape Is Nothing):
        '''   - GIS.Locate finds the nearest shape within a 5-pixel tolerance.
        '''   - MakeEditable returns a writable proxy of the source shape.
        '''   - A copy is added to the preview layer for live editing.
        '''   - handleMouseMove is toggled to start processing transform events.
        '''
        ''' Second click (currShape IsNot Nothing):
        '''   - CopyGeometry writes the final preview geometry back to the source.
        '''   - RevertAll clears the preview layer.
        '''   - InvalidateWholeMap forces a full repaint.
        ''' </summary>
        Private Sub GIS_MouseUp(ByVal sender As Object, ByVal e As AxTatukGIS_XDK11.ITGIS_ViewerWndEvents_MouseUpEvent) Handles GIS.MouseUpEvent
            Dim shp As TGIS_Shape
            Dim ptg As TGIS_Point
            lbHint.Text = "No selected shape. Select shape"

            ' --- COMMIT path: a shape was already selected and being edited ---
            If currShape IsNot Nothing Then
                ' Write the transformed preview geometry back to the real layer
                currShape.CopyGeometry(edtShape)

                ' Discard all shapes in the preview layer
                edtLayer.RevertAll()

                currShape = Nothing
                edtShape = Nothing

                ' Full repaint to show the committed shape in its real layer style
                GIS.InvalidateWholeMap()
                handleMouseMove = False
                Return
            End If

            ' --- SELECT path: pick a shape from the map ---
            If GIS.IsEmpty Then
                Return
            End If
            If GIS.InPaint Then
                Return
            End If

            ' Only proceed if the viewer is in Select interaction mode
            If (GIS.Mode <> TGIS_ViewerMode.Select) Then
                Return
            End If

            ' Convert screen click to map coordinates
            ptg = GIS.ScreenToMap(GisUtils.Point(e.X, e.Y))

            ' Locate the nearest shape; tolerance = 5 screen pixels in map units
            shp = CType(GIS.Locate(ptg, (5 / GIS.Zoom)), TGIS_Shape)
            If (shp Is Nothing) Then
                Return
            End If

            ' MakeEditable returns a proxy that allows geometry modification
            currShape = shp.MakeEditable()

            ' Place a copy of the shape into the overlay layer for live editing
            edtShape = edtLayer.AddShape(currShape.CreateCopy())

            lbHint.Text = ("Selected shape : " _
                        + (currShape.Uid.ToString + ". Click to commit changes"))

            ' Seed previous-position state for the first incremental delta
            prevPtg.X = ptg.X
            prevPtg.Y = ptg.Y
            prevX = e.X
            prevY = e.Y

            ' Toggle: start accepting mouse-move transforms
            handleMouseMove = Not handleMouseMove

        End Sub

        ''' <summary>
        ''' Switches to Rotate mode. If a shape is currently being edited,
        ''' cancels the edit session so the user starts fresh in the new mode.
        ''' </summary>
        Private Sub btnRotate_CheckedChanged(sender As Object, e As EventArgs) Handles rbRotate.CheckedChanged
            lbHint.Text = "Select shape to start rotating"
            If currShape IsNot Nothing Then
                GIS.InvalidateTopmost()
                currShape = Nothing
                handleMouseMove = False
                Return
            End If
        End Sub

        ''' <summary>
        ''' Switches to Scale mode. Cancels any in-progress edit session.
        ''' </summary>
        Private Sub btnScale_CheckedChanged(sender As Object, e As EventArgs) Handles rbScale.CheckedChanged
            lbHint.Text = "Select shape to start scaling"
            If currShape IsNot Nothing Then
                GIS.InvalidateTopmost()
                currShape = Nothing
                handleMouseMove = False
                Return
            End If
        End Sub

        ''' <summary>
        ''' Switches to Move (translate) mode. Cancels any in-progress edit session.
        ''' </summary>
        Private Sub btnMove_CheckedChanged(sender As Object, e As EventArgs) Handles rbMove.CheckedChanged
            lbHint.Text = "Select shape to start moving"
            If currShape IsNot Nothing Then
                GIS.InvalidateTopmost()
                currShape = Nothing
                handleMouseMove = False
                Return
            End If
        End Sub
    End Class
End Namespace
