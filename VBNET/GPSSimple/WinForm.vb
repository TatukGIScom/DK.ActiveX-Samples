Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS_XDK11

Namespace GPSSimple
    ''' <summary>
    ''' Summary description for WinForm.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer
        Private toolBar1 As System.Windows.Forms.ToolBar
        Private panel1 As System.Windows.Forms.Panel
        Private WithEvents cbxCom As System.Windows.Forms.ComboBox
        Private toolTip1 As System.Windows.Forms.ToolTip
        Private WithEvents cbxBaud As System.Windows.Forms.ComboBox
        Private WithEvents GPS As TGIS_GpsNmea
        Private textBox1 As System.Windows.Forms.TextBox

        Public Sub New()
            '
            ' Required for Windows Form Designer support
            '
            InitializeComponent()

            '
            ' TODO: Add any constructor code after InitializeComponent call
            '
            toolTip1.SetToolTip(cbxCom, "Select com port")
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
            Me.toolBar1 = New System.Windows.Forms.ToolBar()
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.cbxBaud = New System.Windows.Forms.ComboBox()
            Me.cbxCom = New System.Windows.Forms.ComboBox()
            Me.toolTip1 = New System.Windows.Forms.ToolTip(Me.components)
            Me.textBox1 = New System.Windows.Forms.TextBox()
            Me.panel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'toolBar1
            '
            Me.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
            Me.toolBar1.AutoSize = False
            Me.toolBar1.DropDownArrows = True
            Me.toolBar1.Location = New System.Drawing.Point(0, 0)
            Me.toolBar1.Name = "toolBar1"
            Me.toolBar1.ShowToolTips = True
            Me.toolBar1.Size = New System.Drawing.Size(606, 33)
            Me.toolBar1.TabIndex = 0
            '
            'panel1
            '
            Me.panel1.Controls.Add(Me.cbxBaud)
            Me.panel1.Controls.Add(Me.cbxCom)
            Me.panel1.Controls.Add(Me.toolBar1)
            Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.panel1.Location = New System.Drawing.Point(0, 0)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(606, 33)
            Me.panel1.TabIndex = 1
            '
            'cbxBaud
            '
            Me.cbxBaud.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cbxBaud.Items.AddRange(New Object() {"1200", "2400", "4800", "9600", "19200"})
            Me.cbxBaud.Location = New System.Drawing.Point(67, 6)
            Me.cbxBaud.Name = "cbxBaud"
            Me.cbxBaud.Size = New System.Drawing.Size(72, 21)
            Me.cbxBaud.TabIndex = 2
            Me.cbxBaud.TabStop = False
            '
            'cbxCom
            '
            Me.cbxCom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cbxCom.Items.AddRange(New Object() {"Com 1", "Com 2", "Com 3", "Com 4", "Com 5", "Com 6", "Com 7", "Com 8", "Com 9", "Com 10"})
            Me.cbxCom.Location = New System.Drawing.Point(0, 6)
            Me.cbxCom.Name = "cbxCom"
            Me.cbxCom.Size = New System.Drawing.Size(67, 21)
            Me.cbxCom.TabIndex = 1
            Me.cbxCom.TabStop = False
            '
            'textBox1
            '
            Me.textBox1.BackColor = System.Drawing.SystemColors.Window
            Me.textBox1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.textBox1.Location = New System.Drawing.Point(0, 33)
            Me.textBox1.Multiline = True
            Me.textBox1.Name = "textBox1"
            Me.textBox1.ReadOnly = True
            Me.textBox1.Size = New System.Drawing.Size(606, 435)
            Me.textBox1.TabIndex = 3
            Me.textBox1.TabStop = False
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(606, 468)
            Me.Controls.Add(Me.textBox1)
            Me.Controls.Add(Me.panel1)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - GPS Interface"
            Me.panel1.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

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

        Private Sub cbxCom_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbxCom.SelectedIndexChanged
            GPS.Com = cbxCom.SelectedIndex + 1
            GPS.Active = True
        End Sub

        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim i As Integer

            GPS = New TGIS_GpsNmea()

            cbxCom.SelectedIndex = GPS.Com - 1

            i = 0
            Do While i < cbxBaud.Items.Count
                If Int32.Parse(CStr(cbxBaud.Items(i))) = GPS.BaudRate Then
                    cbxBaud.SelectedIndex = i
                    Exit Do
                End If
                i += 1
            Loop
            GPS.Active = True
        End Sub

        Private Sub cbxBaud_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbxBaud.SelectedIndexChanged
            GPS.BaudRate = Int32.Parse(CStr(cbxBaud.Items(cbxBaud.SelectedIndex)))
            GPS.Active = True
        End Sub

        Private Sub GPS_Position() Handles GPS.PositionEvent
            Dim str As String

            str = String.Format("{0} {1:F4} {2:F4}", DateTime.Now.ToLocalTime().ToString(), GPS.Longitude * (180 / Math.PI), GPS.Latitude * (180 / Math.PI))
            textBox1.AppendText(str & Constants.vbLf)
        End Sub
    End Class
End Namespace
