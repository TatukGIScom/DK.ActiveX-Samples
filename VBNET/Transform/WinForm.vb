' =============================================================================
' This source code is a part of TatukGIS Developer Kernel.
' =============================================================================
'
' Transform sample - Polynomial georeferencing of a raster image.
' ActiveX / XDK11 edition.
'
' This sample demonstrates how to georeference (rectify) an unregistered
' raster image using the TatukGIS XDK11 ActiveX polynomial transform API.
'
' Key difference from the NDK edition:
'   - The map viewer is hosted as an ActiveX control (AxTGIS_ViewerWnd).
'   - Mouse events use the COM interface ITGIS_ViewerWndEvents_MouseMoveEvent.
'   - Static helper methods (GisPoint, GisSamplesDataDirDownload, etc.) are
'     called on a TGIS_Utils instance (GisUtils) rather than as shared methods.
'   - Layer access uses GIS.Items.item(index) instead of GIS.Items[index].
'   - Transform.AddPoint is named AddPoint_2 (COM disambiguation).
'
' Workflow:
'   btnTransform  - 4-GCP first-order polynomial + CRS assignment (EPSG 102748).
'   btnCutting    - Same GCPs + CuttingPolygon masking the image.
'   btnSave       - Save current transform to a ".trn" sidecar file.
'   btnRead       - Reload a previously saved transform sidecar.
'
' Data: Samples\Rectify\satellite.jpg  (an unrectified aerial/satellite image)
' =============================================================================

Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports TatukGIS_XDK11


Namespace Transform

    ''' <summary>
    ''' Main form for the Transform (ActiveX) sample.
    ''' Demonstrates polynomial georeferencing of a raster image using
    ''' TGIS_TransformPolynomial and TGIS_LayerPixel.Transform via the XDK11 COM API.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form

        Private WithEvents btnTransform As Button  ' Apply 4-GCP first-order polynomial transform
        Private WithEvents btnCutting As Button    ' Apply transform with CuttingPolygon mask
        Private WithEvents btnSave As Button       ' Save transform to .trn sidecar file
        Private WithEvents btnRead As Button       ' Load transform from .trn sidecar file

        ' ActiveX-hosted map viewer control
        Private WithEvents GIS As AxTatukGIS_XDK11.AxTGIS_ViewerWnd

        ' Extension for transform sidecar files ("<image_path>.trn").
        Private GIS_TRN_EXT As String = ".trn"

        Private lbCoords As Label  ' Status label showing cursor map coordinates

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

        ' GisUtils provides helper methods for the ActiveX API.
        ' In the NDK edition these are shared methods; here they require an
        ' instance because the XDK11 COM type library exposes them as instance members.
        Dim GisUtils As New TGIS_Utils()

        <STAThread()>
        Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New WinForm())
        End Sub

        ''' <summary>
        ''' Opens the unrectified satellite image on form load.
        ''' No transform is applied at startup; the user clicks buttons to georeference.
        ''' </summary>
        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            ' GisUtils.GisSamplesDataDirDownload resolves the shared sample data folder
            GIS.Open((GisUtils.GisSamplesDataDirDownload + "\Samples\Rectify\satellite.jpg"))
        End Sub

        ''' <summary>
        ''' Applies a first-order polynomial georeference to the satellite image.
        '''
        ''' 1. Creates TGIS_TransformPolynomial and adds four corner GCPs using
        '''    AddPoint_2 (the COM-disambiguation name for AddPoint).
        ''' 2. Fits a first-order (affine) polynomial via Prepare().
        ''' 3. Assigns the transform to the raster layer and activates warping.
        ''' 4. Declares the CRS (EPSG 102748 = NAD83 / Washington South State Plane).
        ''' 5. Recomputes the layer extent and zooms to full extent.
        ''' </summary>
        Private Sub btnTransform_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTransform.Click
            Dim trn As TGIS_TransformPolynomial
            Dim lp As TGIS_LayerPixel

            ' GIS.Items.item(0) is the COM way to index layers in the ActiveX API
            lp = CType(GIS.Items.item(0), TGIS_LayerPixel)

            trn = New TGIS_TransformPolynomial

            ' Add four corner GCPs: source pixel -> target real-world coordinates.
            ' AddPoint_2 is the COM-safe disambiguation of the overloaded AddPoint.
            trn.AddPoint_2(GisUtils.GisPoint(-0.5, -944.5), GisUtils.GisPoint(1273285.84090909, 239703.615056818), 0, True)
            trn.AddPoint_2(GisUtils.GisPoint(-0.5, 0.5), GisUtils.GisPoint(1273285.84090909, 244759.524147727), 1, True)
            trn.AddPoint_2(GisUtils.GisPoint(1246.5, 0.5), GisUtils.GisPoint(1279722.65909091, 245859.524147727), 2, True)
            trn.AddPoint_2(GisUtils.GisPoint(1246.5, -944.5), GisUtils.GisPoint(1279744.93181818, 239725.887784091), 3, True)

            ' Fit the polynomial (First = affine)
            trn.Prepare(TGIS_PolynomialOrder.First)

            ' Assign the transform to the layer and activate on-the-fly warping
            lp.Transform = trn
            lp.Transform.Active = True

            ' Declare the CRS so the viewer knows the real-world coordinate space
            lp.SetCSByEPSG(102748)

            GIS.RecalcExtent()
            GIS.FullExtent()
        End Sub

        ''' <summary>
        ''' Applies a first-order polynomial transform with a CuttingPolygon mask.
        '''
        ''' Identical GCPs to btnTransform_Click but adds a CuttingPolygon in
        ''' pixel (source) coordinates.  Only pixels inside the polygon are rendered
        ''' after warping; the rest of the image is clipped out at render time.
        ''' </summary>
        Private Sub btnCutting_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCutting.Click
            Dim trn As TGIS_TransformPolynomial
            Dim lp As TGIS_LayerPixel

            lp = CType(GIS.Items.item(0), TGIS_LayerPixel)
            trn = New TGIS_TransformPolynomial

            ' Four corner GCPs (same pixel-to-world mapping as btnTransform_Click)
            trn.AddPoint_2(GisUtils.GisPoint(-0.5, -944.5), GisUtils.GisPoint(1273285.84090909, 239703.615056818), 0, True)
            trn.AddPoint_2(GisUtils.GisPoint(-0.5, 0.5), GisUtils.GisPoint(1273285.84090909, 244759.524147727), 1, True)
            trn.AddPoint_2(GisUtils.GisPoint(1246.5, 0.5), GisUtils.GisPoint(1279722.65909091, 244759.524147727), 2, True)
            trn.AddPoint_2(GisUtils.GisPoint(1246.5, -944.5), GisUtils.GisPoint(1279744.93181818, 239725.887784091), 3, True)

            ' WKT polygon in SOURCE (pixel) coordinates that masks the visible region.
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

        ''' <summary>
        ''' Saves the current polynomial transform to a ".trn" sidecar file.
        ''' This is a no-op if no transform has been assigned to the layer yet.
        ''' </summary>
        Private Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
            Dim lp As TGIS_LayerPixel
            lp = CType(GIS.Items.item(0), TGIS_LayerPixel)

            ' Guard: only save if a transform has been assigned to the layer
            If (Not (lp.Transform) Is Nothing) Then
                lp.Transform.SaveToFile(("satellite.jpg" + GIS_TRN_EXT))
            End If

        End Sub

        ''' <summary>
        ''' Loads a polynomial transform from a ".trn" sidecar file and applies it.
        ''' Creates a new TGIS_TransformPolynomial, loads from file, assigns to
        ''' the raster layer, activates warping, and zooms to fit.
        ''' </summary>
        Private Sub btnRead_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRead.Click
            Dim lp As TGIS_LayerPixel
            Dim trn As TGIS_TransformPolynomial

            lp = CType(GIS.Items.item(0), TGIS_LayerPixel)

            ' Create a transform and load all GCPs and coefficients from the sidecar
            trn = New TGIS_TransformPolynomial
            trn.LoadFromFile(("satellite.jpg" + GIS_TRN_EXT))
            lp.Transform = trn
            lp.Transform.Active = True

            GIS.RecalcExtent()
            GIS.FullExtent()
        End Sub

        ''' <summary>
        ''' Converts the cursor screen position to map coordinates and displays them.
        ''' The event uses ITGIS_ViewerWndEvents_MouseMoveEvent (COM interface).
        ''' GisUtils.Point constructs a TPoint for the ScreenToMap call.
        ''' </summary>
        Private Sub GIS_MouseMove(ByVal sender As Object, ByVal e As AxTatukGIS_XDK11.ITGIS_ViewerWndEvents_MouseMoveEvent) Handles GIS.MouseMoveEvent
            Dim ptg As TGIS_Point
            If GIS.IsEmpty Then
                Return
            End If

            ' Convert screen pixel to map coordinate using the current view transform
            ptg = GIS.ScreenToMap(GisUtils.Point(e.X, e.Y))

            ' Display coordinates formatted to 4 decimal places
            lbCoords.Text = String.Format("X: {0:0.0000} | Y: {1:0.0000}", ptg.X, ptg.Y)
        End Sub
    End Class
End Namespace
