Imports System
Imports System.Windows.Forms
Imports TatukGIS_XDK11
Imports System.IO

Namespace DirectWrite
    ''' <summary>
    ''' DirectWrite sample — demonstrates five sequential low-level write techniques on
    ''' TGIS_LayerSHP: Build (AddShape loop + SaveData), ImportLayerEx (spatial CONTAINS filter),
    ''' MergeLayerEx (DISJOINT filter), TGIS_LayerVectorDirectWriteHelper (sequential
    ''' high-performance write), and TGIS_LayerVectorMergeHelper (batch-commit write).
    ''' Buttons unlock in sequence; output files go into a numbered Shapes{n} directory.
    '''
    ''' ActiveX/COM differences from the WinForms variant:
    '''   GisUtils and GeometryFactory are instance objects (not static classes).
    '''   Build_2 / AddShape_2 are COM overload aliases for the Build / AddShape signatures.
    '''   dwh.Create_(lv) and mh.Create_(lv, 500) replace constructor arguments (COM limitation).
    '''   (New TGIS_Color).Green is used for the Green color constant.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer
        Private WithEvents btnBuild As Button
        Private WithEvents btnImport As Button
        Private WithEvents btnMergeLayer As Button
        Private WithEvents btnWrite As Button
        Private WithEvents btnMergeHelper As Button
        Private GIS As AxTatukGIS_XDK11.AxTGIS_ViewerWnd
        Private numb As Integer
        Private exist As Boolean

        Public Sub New()
            '
            ' Required for Windows Form Designer support
            '
            InitializeComponent()

            '
            ' TODO: Add any constructor code after InitializeComponent call
            '
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
        ''' the contents of me method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WinForm))
            Me.btnBuild = New System.Windows.Forms.Button()
            Me.btnImport = New System.Windows.Forms.Button()
            Me.btnMergeLayer = New System.Windows.Forms.Button()
            Me.btnWrite = New System.Windows.Forms.Button()
            Me.btnMergeHelper = New System.Windows.Forms.Button()
            Me.GIS = New AxTatukGIS_XDK11.AxTGIS_ViewerWnd()
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'btnBuild
            '
            Me.btnBuild.Location = New System.Drawing.Point(1, 2)
            Me.btnBuild.Name = "btnBuild"
            Me.btnBuild.Size = New System.Drawing.Size(75, 23)
            Me.btnBuild.TabIndex = 0
            Me.btnBuild.Text = "Build layer"
            Me.btnBuild.UseVisualStyleBackColor = True
            '
            'btnImport
            '
            Me.btnImport.Enabled = False
            Me.btnImport.Location = New System.Drawing.Point(82, 2)
            Me.btnImport.Name = "btnImport"
            Me.btnImport.Size = New System.Drawing.Size(75, 23)
            Me.btnImport.TabIndex = 1
            Me.btnImport.Text = "Import layer"
            Me.btnImport.UseVisualStyleBackColor = True
            '
            'btnMergeLayer
            '
            Me.btnMergeLayer.Enabled = False
            Me.btnMergeLayer.Location = New System.Drawing.Point(163, 2)
            Me.btnMergeLayer.Name = "btnMergeLayer"
            Me.btnMergeLayer.Size = New System.Drawing.Size(75, 23)
            Me.btnMergeLayer.TabIndex = 2
            Me.btnMergeLayer.Text = "Merge layer"
            Me.btnMergeLayer.UseVisualStyleBackColor = True
            '
            'btnWrite
            '
            Me.btnWrite.Enabled = False
            Me.btnWrite.Location = New System.Drawing.Point(244, 2)
            Me.btnWrite.Name = "btnWrite"
            Me.btnWrite.Size = New System.Drawing.Size(75, 23)
            Me.btnWrite.TabIndex = 3
            Me.btnWrite.Text = "Direct write"
            Me.btnWrite.UseVisualStyleBackColor = True
            '
            'btnMergeHelper
            '
            Me.btnMergeHelper.Enabled = False
            Me.btnMergeHelper.Location = New System.Drawing.Point(325, 2)
            Me.btnMergeHelper.Name = "btnMergeHelper"
            Me.btnMergeHelper.Size = New System.Drawing.Size(84, 23)
            Me.btnMergeHelper.TabIndex = 4
            Me.btnMergeHelper.Text = "Merge helper"
            Me.btnMergeHelper.UseVisualStyleBackColor = True
            '
            'GIS
            '
            Me.GIS.Enabled = True
            Me.GIS.Location = New System.Drawing.Point(1, 31)
            Me.GIS.Name = "GIS"
            Me.GIS.OcxState = CType(resources.GetObject("GIS.OcxState"), System.Windows.Forms.AxHost.State)
            Me.GIS.Size = New System.Drawing.Size(583, 429)
            Me.GIS.TabIndex = 5
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(591, 461)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.btnMergeHelper)
            Me.Controls.Add(Me.btnWrite)
            Me.Controls.Add(Me.btnMergeLayer)
            Me.Controls.Add(Me.btnImport)
            Me.Controls.Add(Me.btnBuild)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - DirectWrite"
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
#End Region

        Dim GisUtils As New TGIS_Utils()
        Dim GeometryFactory As New TGIS_GeometryFactory()

        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        <STAThread>
        Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New WinForm())
        End Sub

        ''' <summary>Finds the next unused Shapes{n} directory number and creates it as the output destination.</summary>
        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            GIS.Mode = TGIS_ViewerMode.Zoom
            numb = 0
            exist = True

            While exist
                If Directory.Exists("Shapes" + numb.ToString) Then
                    numb = numb + 1
                Else
                    exist = False
                End If
            End While

            Directory.CreateDirectory("Shapes" + numb.ToString)

        End Sub

        ''' <summary>Creates a new SHP layer via Build_2(), opens the cities source, copies structure and
        ''' coordinate system, loops all shapes with AddShape_2, then saves data.</summary>
        Private Sub btnBuild_Click(sender As Object, e As EventArgs) Handles btnBuild.Click
            Dim lv As TGIS_LayerSHP
            Dim ll As TGIS_LayerSHP

            '' close any previously opened layers
            GIS.Close()

            '' unlock the import button so the next technique can be used
            btnImport.Enabled = True

            '' create a new shapefile layer (Technique 1: Build + AddShape loop)
            lv = New TGIS_LayerSHP()
            '' ensure we have a unique output directory
            If Directory.Exists("Shapes" + numb.ToString) Then
                numb = numb + 1
                Directory.CreateDirectory("Shapes" + numb.ToString)
            End If
            '' Build_2 is the COM-friendly variant; it creates a new empty shapefile with:
            ''   - file path for output
            ''   - initial extent (can be updated as shapes are added)
            ''   - shape type (Point, Polyline, Polygon, etc.)
            ''   - dimension type (XY, XYZ, XYM, XYZM)
            lv.Build_2(("Shapes" + numb.ToString + "\build.shp"), GisUtils.GisExtent(-180, -90, 180, 90), TGIS_ShapeType.Point, TGIS_DimensionType.XY)

            lv.Open()
            '' open the source cities layer to copy from
            ll = New TGIS_LayerSHP()

            ll.Path = GisUtils.GisSamplesDataDirDownload() + "\World\WorldDCW\cities.shp"
            ll.Open()

            '' copy the field structure (name, type, width) from source to destination
            lv.ImportStructure(ll)
            '' copy the coordinate system from source so geometry is compatible
            lv.CS = ll.CS

            '' iterate all shapes from the source layer and add them to the new layer
            '' AddShape_2 is the COM-friendly variant of AddShape
            For Each shp As TGIS_Shape In ll.Loop
                lv.AddShape_2(shp, True)
            Next

            '' flush all pending writes to disk and finalize the shapefile
            lv.SaveData()

            '' display the newly created layer in the viewer
            GIS.Add(lv)
            GIS.FullExtent()
            GIS.InvalidateWholeMap()
        End Sub

        ''' <summary>Imports a spatially filtered subset of cities using ImportLayerEx with a CONTAINS
        ''' WKT polygon (European bounding box); the imported layer is displayed in green.</summary>
        Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
            Dim ll As TGIS_LayerSHP
            Dim lv As TGIS_LayerSHP
            Dim shp As TGIS_Shape

            '' close any previously opened layers
            GIS.Close()

            '' unlock the next technique button
            btnMergeLayer.Enabled = True

            '' open the original cities layer as a reference
            ll = New TGIS_LayerSHP()
            ll.Path = GisUtils.GisSamplesDataDirDownload() + "\World\WorldDCW\cities.shp"
            GIS.Add(ll)

            '' Technique 2: ImportLayerEx with spatial filtering
            '' create a polygon from WKT that defines the import area (Europe bounding box)
            '' CONTAINS filter means only cities whose geometry is CONTAINED WITHIN the polygon will be imported
            shp = GeometryFactory.GisCreateShapeFromWKT("POLYGON((7.86 56.39,31.37 56.39,31.37 39.48,7.86 39.48,7.868 56.39))")

            '' create the destination layer that will receive the filtered cities
            lv = New TGIS_LayerSHP()
            lv.Path = "Shapes" + numb.ToString + "\imported.shp"
            '' copy coordinate system from source layer
            lv.CS = ll.CS
            '' ImportLayerEx parameters:
            ''   ll = source layer to import from
            ''   ll.Extent = spatial search area
            ''   TGIS_ShapeType.Unknown = use source shape type
            ''   "" = no SQL WHERE filter (import all matching spatial criteria)
            ''   shp = the filter geometry (Europe bounding polygon)
            ''   GIS_RELATE_CONTAINS() = spatial predicate (import shapes CONTAINED in filter polygon)
            ''   False = do not rebuild spatial index
            lv.ImportLayerEx(ll, ll.Extent, TGIS_ShapeType.Unknown, "", shp, GisUtils.GIS_RELATE_CONTAINS(), False)

            '' add the filtered layer to the viewer and display it in green
            GIS.Add(lv)
            lv.Params.Marker.Color = (New TGIS_Color).Green
            GIS.FullExtent()
            '' zoom to show only the imported features (European region)
            GIS.VisibleExtent = lv.Extent
            GIS.InvalidateWholeMap()
        End Sub

        ''' <summary>Merges cities outside the European polygon using MergeLayerEx with a DISJOINT
        ''' relation; the merged layer is displayed in green.</summary>
        Private Sub btnMergeLayer_Click(sender As Object, e As EventArgs) Handles btnMergeLayer.Click
            Dim ll As TGIS_LayerSHP
            Dim lv As TGIS_LayerSHP
            Dim shp As TGIS_Shape

            GIS.Close()

            btnWrite.Enabled = True

            ll = New TGIS_LayerSHP()
            ll.Path = GisUtils.GisSamplesDataDirDownload() + "\World\WorldDCW\cities.shp"
            GIS.Add(ll)

            shp = GeometryFactory.GisCreateShapeFromWKT("POLYGON((7.86 56.39,31.37 56.39,31.37 39.48,7.86 39.48,7.868 56.39))")

            lv = New TGIS_LayerSHP()
            lv.Path = "Shapes" + numb.ToString + "\imported.shp"
            lv.CS = ll.CS
            lv.MergeLayerEx(ll, ll.Extent, TGIS_ShapeType.Unknown, "", shp, GisUtils.GIS_RELATE_DISJOINT(), False, False)

            GIS.Add(lv)
            lv.Params.Marker.Color = (New TGIS_Color).Green
            GIS.FullExtent()
            GIS.InvalidateWholeMap()
        End Sub

        ''' <summary>Writes all cities to a new SHP using TGIS_LayerVectorDirectWriteHelper
        ''' (Build → AddShape loop → Close) for high-performance sequential writing.</summary>
        Private Sub btnWrite_Click(sender As Object, e As EventArgs) Handles btnWrite.Click
            Dim ll As TGIS_LayerSHP
            Dim lv As TGIS_LayerSHP
            Dim shp As TGIS_Shape
            Dim dwh As TGIS_LayerVectorDirectWriteHelper

            '' close any previously opened layers
            GIS.Close()

            '' unlock the final technique button
            btnMergeHelper.Enabled = True

            '' open the source cities layer
            ll = New TGIS_LayerSHP()

            ll.Path = GisUtils.GisSamplesDataDirDownload() + "\World\WorldDCW\cities.shp"
            ll.Open()

            '' Technique 4: DirectWriteHelper for sequential high-performance writes
            '' create the destination layer
            lv = New TGIS_LayerSHP()
            '' copy structure and coordinate system from source
            lv.ImportStructure(ll)
            lv.CS = ll.CS

            '' create a DirectWriteHelper instance for optimized sequential writing
            '' DirectWriteHelper provides:
            ''   - Direct sequential writes (no buffering, immediate disk writes)
            ''   - Optimized for large datasets
            ''   - Lower memory overhead than standard AddShape loop
            dwh = New TGIS_LayerVectorDirectWriteHelper
            '' Create_ is the COM-compatible constructor (wraps the new operator)
            dwh.Create_(lv)
            '' Build creates a new empty shapefile with specified parameters
            dwh.Build(("Shapes" + numb.ToString + "\direct_write.shp"), ll.Extent, TGIS_ShapeType.Point, TGIS_DimensionType.XY)

            '' sequentially add all shapes from source to destination
            '' each AddShape call immediately writes to disk (no accumulation)
            For Each shp In ll.Loop()
                dwh.AddShape(shp)
            Next

            '' close the helper, finalizing the shapefile
            dwh.Close()

            '' display the newly created layer in the viewer
            GIS.Add(lv)
            GIS.FullExtent()
        End Sub

        ''' <summary>Writes all cities to a new SHP using TGIS_LayerVectorMergeHelper with Commit()
        ''' per shape for batch-commit writing; resets all buttons on completion.</summary>
        Private Sub btnMergeHelper_Click(sender As Object, e As EventArgs) Handles btnMergeHelper.Click
            Dim ll As TGIS_LayerSHP
            Dim lv As TGIS_LayerSHP
            Dim shp As TGIS_Shape
            Dim mh As TGIS_LayerVectorMergeHelper

            '' close any previously opened layers
            GIS.Close()

            '' disable this button since it's the final technique
            btnMergeHelper.Enabled = False

            '' open the source cities layer
            ll = New TGIS_LayerSHP()
            ll.Path = GisUtils.GisSamplesDataDirDownload() + "\World\WorldDCW\cities.shp"
            ll.Open()

            '' Technique 5: MergeHelper for batch-commit writing
            '' create the destination layer
            lv = New TGIS_LayerSHP()
            '' copy structure and coordinate system from source
            lv.ImportStructure(ll)
            lv.CS = ll.CS
            '' build the output shapefile
            lv.Build_2(("Shapes" + numb.ToString + "\merge_helper.shp"), ll.Extent, TGIS_ShapeType.Point, TGIS_DimensionType.XY)

            '' create a MergeHelper instance for batch-commit writing
            '' MergeHelper provides:
            ''   - Buffered writes with periodic commits
            ''   - Better cache management than direct writes
            ''   - Configurable batch size (500 shapes per commit)
            ''   - Good balance between performance and memory usage
            mh = New TGIS_LayerVectorMergeHelper
            '' Create_ initializes the helper with:
            ''   lv = destination layer
            ''   500 = batch size (commit after every N shapes)
            mh.Create_(lv, 500)
            '' add each shape and commit in batches
            For Each shp In ll.Loop()
                '' add shape to the current batch
                mh.AddShape(shp)
                '' commit the batch (after 500 shapes accumulated)
                mh.Commit()
            Next

            '' disable all technique buttons since this is the final one
            btnImport.Enabled = False
            btnMergeLayer.Enabled = False
            btnWrite.Enabled = False

            '' display the newly created layer in the viewer
            GIS.Add(lv)
            GIS.FullExtent()
        End Sub
    End Class
End Namespace

