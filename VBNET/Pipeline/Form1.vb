Option Strict Off
Option Explicit On

Imports TatukGIS_XDK11
Imports System
Imports System.IO
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports AxTatukGIS_XDK11
Imports System.Collections.Generic

Friend Class Form1
    Inherits System.Windows.Forms.Form
#Region "Windows Form Designer generated code "

    Private Class helpEvent
        Implements ITGIS_HelpEvent

        Public Sub [Event](Name As String) Implements ITGIS_HelpEvent.Event
            Dim url As String
            url = "https://docs.tatukgis.com/EDT5/guide:help:dkbuiltin:" & Name
            System.Diagnostics.Process.Start(url)
        End Sub
    End Class

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
    Private WithEvents lstCommands As ListBox
    Private label1 As Label
    Private label2 As Label
    Private WithEvents rtbCode As RichTextBox
    Private WithEvents btnHelp As Button
    Private WithEvents btnExit As Button
    Private WithEvents btnExecute As Button
    Private WithEvents btnOpen As Button
    Private WithEvents btnSave As Button
    Private oPipeline As TGIS_Pipeline
    Private oPipelineCommands As TGIS_StringList
    Private oPipelineLine As Integer
    Private dlgSave As SaveFileDialog
    Friend WithEvents GIS As AxTGIS_ViewerWnd
    Friend WithEvents GIS_Legend As AxTGIS_ControlLegend
    Friend WithEvents Label3 As Label
    Friend WithEvents pnlDynamicProgress As FlowLayoutPanel
    'Friend WithEvents GIS As AxTGIS_ViewerWnd
    Private dlgOpen As OpenFileDialog

    Private progressBarList As List(Of ProgressBar)
    Private etlLabelList As List(Of Label)
    Private nameLabelList As List(Of Label)
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.


    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.lstCommands = New System.Windows.Forms.ListBox()
        Me.label1 = New System.Windows.Forms.Label()
        Me.label2 = New System.Windows.Forms.Label()
        Me.rtbCode = New System.Windows.Forms.RichTextBox()
        Me.btnHelp = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnExecute = New System.Windows.Forms.Button()
        Me.btnOpen = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.dlgSave = New System.Windows.Forms.SaveFileDialog()
        Me.dlgOpen = New System.Windows.Forms.OpenFileDialog()
        Me.GIS = New AxTatukGIS_XDK11.AxTGIS_ViewerWnd()
        Me.GIS_Legend = New AxTatukGIS_XDK11.AxTGIS_ControlLegend()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.pnlDynamicProgress = New System.Windows.Forms.FlowLayoutPanel()
        CType(Me.GIS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GIS_Legend, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lstCommands
        '
        Me.lstCommands.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lstCommands.FormattingEnabled = True
        Me.lstCommands.Location = New System.Drawing.Point(12, 41)
        Me.lstCommands.Name = "lstCommands"
        Me.lstCommands.Size = New System.Drawing.Size(180, 290)
        Me.lstCommands.TabIndex = 0
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(12, 12)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(59, 13)
        Me.label1.TabIndex = 4
        Me.label1.Text = "Commands"
        '
        'label2
        '
        Me.label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(12, 343)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(32, 13)
        Me.label2.TabIndex = 5
        Me.label2.Text = "Code"
        '
        'rtbCode
        '
        Me.rtbCode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rtbCode.Location = New System.Drawing.Point(12, 362)
        Me.rtbCode.Name = "rtbCode"
        Me.rtbCode.Size = New System.Drawing.Size(678, 201)
        Me.rtbCode.TabIndex = 6
        Me.rtbCode.Text = "Say Text=""Hello!""" & Global.Microsoft.VisualBasic.ChrW(10) & "Layer.Open Result=$layer Path=C:\Users\Public\Documents\TatukGI" &
    "S\Data\Samples11\World\VisibleEarth\world_8km.jpg" & Global.Microsoft.VisualBasic.ChrW(10) & "Map.FullExtent" & Global.Microsoft.VisualBasic.ChrW(10) & "Say Text=""Done!" &
    """"
        Me.rtbCode.WordWrap = False
        '
        'btnHelp
        '
        Me.btnHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnHelp.Location = New System.Drawing.Point(12, 590)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(75, 23)
        Me.btnHelp.TabIndex = 7
        Me.btnHelp.Text = "Help"
        Me.btnHelp.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Location = New System.Drawing.Point(734, 590)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 23)
        Me.btnExit.TabIndex = 8
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnExecute
        '
        Me.btnExecute.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExecute.Location = New System.Drawing.Point(653, 590)
        Me.btnExecute.Name = "btnExecute"
        Me.btnExecute.Size = New System.Drawing.Size(75, 23)
        Me.btnExecute.TabIndex = 9
        Me.btnExecute.Text = "Execute"
        Me.btnExecute.UseVisualStyleBackColor = True
        '
        'btnOpen
        '
        Me.btnOpen.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnOpen.Location = New System.Drawing.Point(93, 590)
        Me.btnOpen.Name = "btnOpen"
        Me.btnOpen.Size = New System.Drawing.Size(75, 23)
        Me.btnOpen.TabIndex = 10
        Me.btnOpen.Text = "Open..."
        Me.btnOpen.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(174, 590)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 11
        Me.btnSave.Text = "Save..."
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'dlgSave
        '
        Me.dlgSave.Filter = "Pipeline files|*.ttkpipeline"
        '
        'dlgOpen
        '
        Me.dlgOpen.Filter = "Pipeline files|*.ttkpipeline"
        '
        'GIS
        '
        Me.GIS.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GIS.Enabled = True
        Me.GIS.Location = New System.Drawing.Point(198, 41)
        Me.GIS.Name = "GIS"
        Me.GIS.OcxState = CType(resources.GetObject("GIS.OcxState"), System.Windows.Forms.AxHost.State)
        Me.GIS.Size = New System.Drawing.Size(478, 315)
        Me.GIS.TabIndex = 12
        '
        'GIS_Legend
        '
        Me.GIS_Legend.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GIS_Legend.Enabled = True
        Me.GIS_Legend.Location = New System.Drawing.Point(683, 41)
        Me.GIS_Legend.Name = "GIS_Legend"
        Me.GIS_Legend.OcxState = CType(resources.GetObject("GIS_Legend.OcxState"), System.Windows.Forms.AxHost.State)
        Me.GIS_Legend.Size = New System.Drawing.Size(126, 315)
        Me.GIS_Legend.TabIndex = 13
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(195, 12)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(45, 13)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Preview"
        '
        'pnlDynamicProgress
        '
        Me.pnlDynamicProgress.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.pnlDynamicProgress.Location = New System.Drawing.Point(696, 362)
        Me.pnlDynamicProgress.Name = "pnlDynamicProgress"
        Me.pnlDynamicProgress.Size = New System.Drawing.Size(113, 201)
        Me.pnlDynamicProgress.TabIndex = 15
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(821, 625)
        Me.Controls.Add(Me.pnlDynamicProgress)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.GIS_Legend)
        Me.Controls.Add(Me.GIS)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnOpen)
        Me.Controls.Add(Me.btnExecute)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnHelp)
        Me.Controls.Add(Me.rtbCode)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.lstCommands)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(200, 120)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "TatukGIS Samples - Pipeline"
        CType(Me.GIS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GIS_Legend, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region


    Dim GisUtils As New TGIS_Utils()

    Private Sub Form1_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Dim i As Integer

        readFromFile(GisUtils.GisSamplesDataDirDownload + "/Samples/Pipeline/contouring.ttkpipeline")

        progressBarList = New List(Of ProgressBar)
        nameLabelList = New List(Of Label)
        etlLabelList = New List(Of Label)

        addNameLabel()
        addEtlLabel()
        addProgressBar()

        oPipeline = New TGIS_Pipeline()
        oPipeline.GIS = GIS.GetOcx
        GIS_Legend.GIS_Viewer = GIS.GetOcx
        AddHandler oPipeline.ShowMessageEvent, AddressOf doPipelineMessage
        AddHandler oPipeline.ShowFormEvent, AddressOf doPipelineForm
        AddHandler oPipeline.LogErrorEvent, AddressOf doPipelineMessage
        AddHandler oPipeline.LogWarningEvent, AddressOf doPipelineMessage
        AddHandler oPipeline.BusyEvent, AddressOf doBusyEvent

        prepareCommands()

        For i = 0 To oPipelineCommands.Count - 1
            lstCommands.Items.Add(oPipelineCommands.Strings(i))
        Next
    End Sub


    Private Sub addProgressBar()
        Dim progressBar As ProgressBar = New ProgressBar()
        pnlDynamicProgress.Controls.Add(progressBar)
        progressBarList.Add(progressBar)
    End Sub

    Private Sub addEtlLabel()
        Dim label As Label = New Label()
        pnlDynamicProgress.Controls.Add(label)
        etlLabelList.Add(label)

    End Sub

    Private Sub addNameLabel()
        Dim label As Label = New Label()
        pnlDynamicProgress.Controls.Add(label)
        nameLabelList.Add(label)

    End Sub

    Private Sub removeProgressBar()
        Dim progressBar As ProgressBar = progressBarList.Item(progressBarList.Count - 1)
        pnlDynamicProgress.Controls.Remove(progressBar)
        progressBarList.Remove(progressBar)
    End Sub

    Private Sub removeEtlLabel()
        Dim label As Label = etlLabelList.Item(etlLabelList.Count - 1)
        pnlDynamicProgress.Controls.Remove(label)
        etlLabelList.Remove(label)
    End Sub

    Private Sub removeNameLabel()
        Dim label As Label = nameLabelList.Item(nameLabelList.Count - 1)
        pnlDynamicProgress.Controls.Remove(label)
        nameLabelList.Remove(label)
    End Sub

    Private Sub doBusyEvent(Pos As Integer, [End] As Integer, ByRef Abort As Boolean)

        Dim progressBar As ProgressBar

        progressBar = progressBarList.Item(0)
        Select Case pos
                Case -1
                    progressBar.Value = 0
                Case 0
                    progressBar.Minimum = 0
                progressBar.Maximum = [End]
                progressBar.Value = 0
                Case Else
                    progressBar.Value = pos
            End Select

        Application.DoEvents()
    End Sub

    Private Sub doPipelineMessage(ByVal _message As String)
        MessageBox.Show(_message)
    End Sub

    Private Sub doPipelineForm(ByVal _operation As TGIS_PipelineOperationAbstract)
        Dim frm As TGIS_PipelineParamsEditor

        frm = New TGIS_PipelineParamsEditor()

        If frm.Execute_2(_operation, New helpEvent) = DialogResult.OK Then
            Dim lns As String()
            lns = rtbCode.Lines
            lns(oPipelineLine - 1) = _operation.AsText()
            rtbCode.Lines = lns
        End If
    End Sub

    Private Sub prepareCommands()
        oPipelineCommands = GisUtils.PipelineOperations.Names
    End Sub

    Private Sub rtbCode_DoubleClick(sender As Object, e As EventArgs) Handles rtbCode.DoubleClick
        oPipelineLine = rtbCode.GetLineFromCharIndex(rtbCode.SelectionStart) + 1
        rtbCode.[Select](rtbCode.GetFirstCharIndexFromLine(oPipelineLine - 1), rtbCode.Lines(oPipelineLine - 1).Length)
        oPipeline.SourceCode = rtbCode.Text
        oPipeline.ShowForm(oPipelineLine)
    End Sub

    Private Sub lstCommands_DoubleClick(sender As Object, e As EventArgs) Handles lstCommands.DoubleClick
        oPipeline.ShowForm_2(lstCommands.SelectedItem.ToString(), rtbCode.GetLineFromCharIndex(rtbCode.SelectionStart))
    End Sub

    Private Sub btnHelp_Click(sender As Object, e As EventArgs) Handles btnHelp.Click
        Dim url As String
        url = "https://docs.tatukgis.com/DK11/doc:pipeline"
        System.Diagnostics.Process.Start(url)
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Application.Exit()
    End Sub

    Private Sub btnExecute_Click(sender As Object, e As EventArgs) Handles btnExecute.Click
        oPipeline.SourceCode = rtbCode.Text
        oPipeline.Execute()
    End Sub

    Private Sub readFromFile(ByVal _str As String)
        Dim reader As StreamReader
        Dim line As String

        reader = New StreamReader(_str)
        Try
            rtbCode.Clear()
            Do
                line = reader.ReadLine
                rtbCode.AppendText(line + Global.Microsoft.VisualBasic.ChrW(13) + Global.Microsoft.VisualBasic.ChrW(10))
            Loop Until line Is Nothing
        Finally
            reader.Close()
        End Try
    End Sub

    Private Sub btnOpen_Click(sender As Object, e As EventArgs) Handles btnOpen.Click
        If dlgOpen.ShowDialog() = DialogResult.OK Then
            readFromFile(dlgOpen.FileName)
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim lines As String()
        Dim file As String
        Dim line As String
        Dim writer As StreamWriter

        If dlgSave.ShowDialog() = DialogResult.OK Then
            file = dlgSave.FileName
            lines = rtbCode.Lines
            writer = New StreamWriter(file)
            Try
                For Each line In lines
                    writer.WriteLine(line)
                Next
            Finally
                writer.Close()
            End Try
        End If
    End Sub

    Private Sub rtbCode_Click(sender As Object, e As EventArgs) Handles rtbCode.Click
        oPipelineLine = rtbCode.GetLineFromCharIndex(rtbCode.SelectionStart) + 1
    End Sub
End Class
