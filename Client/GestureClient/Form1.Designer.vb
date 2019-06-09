<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.pythonServerListener = New System.ComponentModel.BackgroundWorker()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.cmdStart = New System.Windows.Forms.Button()
        Me.timerAnim = New System.Windows.Forms.Timer(Me.components)
        Me.txtAngleX = New System.Windows.Forms.TextBox()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pythonServerListener
        '
        '
        'RichTextBox1
        '
        Me.RichTextBox1.BackColor = System.Drawing.SystemColors.HotTrack
        Me.RichTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.RichTextBox1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.RichTextBox1.ForeColor = System.Drawing.Color.White
        Me.RichTextBox1.Location = New System.Drawing.Point(0, 633)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(1433, 95)
        Me.RichTextBox1.TabIndex = 2
        Me.RichTextBox1.Text = ""
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtAngleX)
        Me.SplitContainer1.Panel2.Controls.Add(Me.cmdStart)
        Me.SplitContainer1.Size = New System.Drawing.Size(1433, 633)
        Me.SplitContainer1.SplitterDistance = 1187
        Me.SplitContainer1.TabIndex = 4
        '
        'cmdStart
        '
        Me.cmdStart.Location = New System.Drawing.Point(15, 12)
        Me.cmdStart.Name = "cmdStart"
        Me.cmdStart.Size = New System.Drawing.Size(215, 26)
        Me.cmdStart.TabIndex = 5
        Me.cmdStart.Text = "Start"
        Me.cmdStart.UseVisualStyleBackColor = True
        '
        'timerAnim
        '
        Me.timerAnim.Interval = 17
        '
        'txtAngleX
        '
        Me.txtAngleX.Location = New System.Drawing.Point(86, 58)
        Me.txtAngleX.Name = "txtAngleX"
        Me.txtAngleX.Size = New System.Drawing.Size(73, 22)
        Me.txtAngleX.TabIndex = 6
        Me.txtAngleX.Text = "0.0"
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1433, 728)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RichTextBox1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmMain"
        Me.Text = "MotionGame Client - Demo"
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pythonServerListener As System.ComponentModel.BackgroundWorker
    Friend WithEvents RichTextBox1 As RichTextBox
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents cmdStart As Button
    Friend WithEvents timerAnim As Timer
    Friend WithEvents txtAngleX As TextBox
End Class
