Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS_XDK11
Imports AxTatukGIS_XDK11

Namespace DynamicAggregation
    ''' <summary>
    ''' DynamicAggregation sample — demonstrates TGIS_DynamicAggregatorFactory for real-time
    ''' point clustering on a GIS layer.
    '''
    ''' Loads a TatukGIS project (Aggregation.ttkproject) containing a "cities" point layer.
    ''' A left panel offers three controls: Aggregation method (Off or a factory-registered name),
    ''' Radius (5–80 pt), and Threshold (0–10).  Changing any control calls changeAggregation
    ''' which installs or removes the selected TGIS_DynamicAggregator on the layer.
    '''
    ''' ActiveX-specific differences: GIS is AxTGIS_ViewerWnd; GisUtils and AggregatorFactory
    ''' are instance objects (not static); aggregator names enumerated via
    ''' AggregatorFactory.Names.Strings(i).
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form

        Private pMenu As Panel
        Private GIS As AxTatukGIS_XDK11.AxTGIS_ViewerWnd
        Private WithEvents cbxThreshhold As ComboBox
        Private lblThreshhold As Label
        Private WithEvents cbxRadius As ComboBox
        Private WithEvents lblRadius As Label
        Private WithEvents cbxMethod As ComboBox
        Private lblMethod As Label
        Private components As System.ComponentModel.IContainer

        Private GisUtils As New TGIS_Utils()
        Private AggregatorFactory As New TGIS_DynamicAggregatorFactory()

        Public Sub New()
            InitializeComponent()
        End Sub

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then

                If components IsNot Nothing Then
                    components.Dispose()
                End If
            End If

            MyBase.Dispose(disposing)
        End Sub

        Private Sub InitializeComponent()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WinForm))
            Me.pMenu = New System.Windows.Forms.Panel()
            Me.cbxThreshhold = New System.Windows.Forms.ComboBox()
            Me.lblThreshhold = New System.Windows.Forms.Label()
            Me.cbxRadius = New System.Windows.Forms.ComboBox()
            Me.lblRadius = New System.Windows.Forms.Label()
            Me.cbxMethod = New System.Windows.Forms.ComboBox()
            Me.lblMethod = New System.Windows.Forms.Label()
            Me.GIS = New AxTatukGIS_XDK11.AxTGIS_ViewerWnd()
            Me.pMenu.SuspendLayout()
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'pMenu
            '
            Me.pMenu.Controls.Add(Me.cbxThreshhold)
            Me.pMenu.Controls.Add(Me.lblThreshhold)
            Me.pMenu.Controls.Add(Me.cbxRadius)
            Me.pMenu.Controls.Add(Me.lblRadius)
            Me.pMenu.Controls.Add(Me.cbxMethod)
            Me.pMenu.Controls.Add(Me.lblMethod)
            Me.pMenu.Location = New System.Drawing.Point(12, 12)
            Me.pMenu.Name = "pMenu"
            Me.pMenu.Size = New System.Drawing.Size(200, 442)
            Me.pMenu.TabIndex = 0
            '
            'cbxThreshhold
            '
            Me.cbxThreshhold.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cbxThreshhold.FormattingEnabled = True
            Me.cbxThreshhold.Items.AddRange(New Object() {"0", "1", "2", "5", "10"})
            Me.cbxThreshhold.Location = New System.Drawing.Point(3, 96)
            Me.cbxThreshhold.Name = "cbxThreshhold"
            Me.cbxThreshhold.Size = New System.Drawing.Size(194, 21)
            Me.cbxThreshhold.TabIndex = 5
            '
            'lblThreshhold
            '
            Me.lblThreshhold.AutoSize = True
            Me.lblThreshhold.Location = New System.Drawing.Point(0, 80)
            Me.lblThreshhold.Name = "lblThreshhold"
            Me.lblThreshhold.Size = New System.Drawing.Size(63, 13)
            Me.lblThreshhold.TabIndex = 4
            Me.lblThreshhold.Text = "Threshhold:"
            '
            'cbxRadius
            '
            Me.cbxRadius.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cbxRadius.FormattingEnabled = True
            Me.cbxRadius.Items.AddRange(New Object() {"5 pt", "10 pt", "20 pt", "40 pt", "80 pt"})
            Me.cbxRadius.Location = New System.Drawing.Point(3, 56)
            Me.cbxRadius.Name = "cbxRadius"
            Me.cbxRadius.Size = New System.Drawing.Size(194, 21)
            Me.cbxRadius.TabIndex = 3
            '
            'lblRadius
            '
            Me.lblRadius.AutoSize = True
            Me.lblRadius.Location = New System.Drawing.Point(0, 40)
            Me.lblRadius.Name = "lblRadius"
            Me.lblRadius.Size = New System.Drawing.Size(43, 13)
            Me.lblRadius.TabIndex = 2
            Me.lblRadius.Text = "Radius:"
            '
            'cbxMethod
            '
            Me.cbxMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cbxMethod.FormattingEnabled = True
            Me.cbxMethod.Location = New System.Drawing.Point(3, 16)
            Me.cbxMethod.Name = "cbxMethod"
            Me.cbxMethod.Size = New System.Drawing.Size(194, 21)
            Me.cbxMethod.TabIndex = 1
            '
            'lblMethod
            '
            Me.lblMethod.AutoSize = True
            Me.lblMethod.Location = New System.Drawing.Point(0, 0)
            Me.lblMethod.Name = "lblMethod"
            Me.lblMethod.Size = New System.Drawing.Size(105, 13)
            Me.lblMethod.TabIndex = 0
            Me.lblMethod.Text = "Aggregation method:"
            '
            'GIS
            '
            Me.GIS.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GIS.Enabled = True
            Me.GIS.Location = New System.Drawing.Point(218, 12)
            Me.GIS.Name = "GIS"
            Me.GIS.OcxState = CType(resources.GetObject("GIS.OcxState"), System.Windows.Forms.AxHost.State)
            Me.GIS.Size = New System.Drawing.Size(751, 442)
            Me.GIS.TabIndex = 1
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(981, 466)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.pMenu)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - DynamicAggregation"
            Me.pMenu.ResumeLayout(False)
            Me.pMenu.PerformLayout()
            CType(Me.GIS, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub

        <STAThread>
        Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New WinForm())
        End Sub

        ''' <summary>Opens the project, populates the method combo with all registered aggregator names, and
        ''' sets initial selections with Radius and Threshold disabled.</summary>
        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            '' load the GIS project file containing the cities point layer
            GIS.Open(GisUtils.GisSamplesDataDirDownload() & "\Samples\Aggregation\Aggregation.ttkproject")
            GIS.Open("c:\project\sampledata\Samples\Aggregation\Aggregation.ttkproject")

            '' populate the aggregation method combo box
            '' start with "Off" to disable aggregation as the first option
            cbxMethod.Items.Add("Off")

            '' enumerate all registered aggregator types from the factory
            '' the factory maintains a list of available aggregation algorithms
            For i As Integer = 0 To AggregatorFactory.Names.Count - 1
                '' add each registered aggregator name (e.g., "ShapeReduction", "QuadTree")
                cbxMethod.Items.Add(AggregatorFactory.Names.Strings(i))
            Next

            '' set initial selections
            '' aggregation is off by default
            cbxMethod.SelectedIndex = 0
            '' set threshold to minimum (0) initially
            cbxThreshhold.SelectedIndex = 0
            '' disable radius and threshold controls when aggregation is off
            cbxRadius.Enabled = False
            cbxThreshhold.Enabled = False
        End Sub

        ''' <summary>Sets a sensible default radius index when the method changes: index 0 (5 pt) for
        ''' ShapeReduction, index 3 (40 pt) for all other methods.</summary>
        Private Sub readDeafaultValues()
            If cbxMethod.SelectedItem.ToString().Equals("ShapeReduction") Then
                cbxRadius.SelectedIndex = 0
            Else
                cbxRadius.SelectedIndex = 3
            End If
        End Sub

        ''' <summary>Installs or removes the selected TGIS_DynamicAggregator on the "cities" layer,
        ''' applies the current Radius and Threshold, and redraws the map.</summary>
        Private Sub changeAggregation()
            '' get the aggregation method name from the combo box
            Dim dyn_agg_name As String = cbxMethod.SelectedItem.ToString()
            '' fetch the cities layer from the GIS viewer
            Dim lv As TGIS_LayerVector = CType(GIS.[Get]("cities"), TGIS_LayerVector)
            '' set transparency so clustered points are semi-transparent (visible when stacked)
            lv.Transparency = 70

            If dyn_agg_name.Equals("Off") Then
                '' disable aggregation: turn off radius and threshold controls
                cbxRadius.Enabled = False
                cbxThreshhold.Enabled = False
                '' remove any existing aggregator from the layer
                lv.DynamicAggregator = Nothing
            Else
                '' enable aggregation: activate radius and threshold controls
                cbxRadius.Enabled = True
                cbxThreshhold.Enabled = True

                '' create an aggregator instance using the factory pattern
                '' the factory creates the appropriate aggregator type based on the name
                '' (e.g., "ShapeReduction", "QuadTree", etc.)
                lv.DynamicAggregator = AggregatorFactory.CreateInstance(dyn_agg_name, lv)
                '' set the radius in screen pixels (e.g., "SIZE: 5 pt", "SIZE: 20 pt")
                '' radius determines the cluster distance threshold in the viewer
                lv.DynamicAggregator.RadiusAsText = "SIZE: " & cbxRadius.SelectedItem.ToString()
                '' set the aggregation threshold: minimum cluster size to trigger aggregation
                '' (0 = always aggregate, higher values = fewer clusters)
                lv.DynamicAggregator.Threshold = Int32.Parse(cbxThreshhold.SelectedItem.ToString())
            End If

            '' repaint the map to apply the new aggregation settings
            GIS.InvalidateWholeMap()
        End Sub

        ''' <summary>Resets the default radius for the chosen method, then applies the new aggregator.</summary>
        Private Sub cbxMethod_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbxMethod.SelectedIndexChanged
            readDeafaultValues()
            changeAggregation()
        End Sub

        ''' <summary>Re-applies the aggregator with the newly selected radius.</summary>
        Private Sub cbxRadius_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbxRadius.SelectedIndexChanged
            changeAggregation()
        End Sub

        ''' <summary>Re-applies the aggregator with the newly selected threshold.</summary>
        Private Sub cbxThreshhold_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbxThreshhold.SelectedIndexChanged
            changeAggregation()
        End Sub
    End Class
End Namespace
