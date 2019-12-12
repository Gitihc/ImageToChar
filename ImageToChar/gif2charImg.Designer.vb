<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class gif2charImg
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
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

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.txtImgPath = New System.Windows.Forms.TextBox()
        Me.btnSelectPic = New System.Windows.Forms.Button()
        Me.btnGif2Pngs = New System.Windows.Forms.Button()
        Me.btnCharImgs2Gif = New System.Windows.Forms.Button()
        Me.btnGif2CharImg = New System.Windows.Forms.Button()
        Me.btnPngs2CharImg = New System.Windows.Forms.Button()
        Me.numUD = New System.Windows.Forms.NumericUpDown()
        Me.rtb_log = New System.Windows.Forms.RichTextBox()
        Me.nud_gif_delay = New System.Windows.Forms.NumericUpDown()
        Me.label_Delay = New System.Windows.Forms.Label()
        Me.btn_switch = New System.Windows.Forms.Button()
        CType(Me.numUD, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nud_gif_delay, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtImgPath
        '
        Me.txtImgPath.Location = New System.Drawing.Point(14, 35)
        Me.txtImgPath.Name = "txtImgPath"
        Me.txtImgPath.Size = New System.Drawing.Size(319, 21)
        Me.txtImgPath.TabIndex = 4
        '
        'btnSelectPic
        '
        Me.btnSelectPic.Location = New System.Drawing.Point(339, 35)
        Me.btnSelectPic.Name = "btnSelectPic"
        Me.btnSelectPic.Size = New System.Drawing.Size(75, 23)
        Me.btnSelectPic.TabIndex = 3
        Me.btnSelectPic.Text = "选择图片"
        Me.btnSelectPic.UseVisualStyleBackColor = True
        '
        'btnGif2Pngs
        '
        Me.btnGif2Pngs.Location = New System.Drawing.Point(14, 110)
        Me.btnGif2Pngs.Name = "btnGif2Pngs"
        Me.btnGif2Pngs.Size = New System.Drawing.Size(94, 34)
        Me.btnGif2Pngs.TabIndex = 5
        Me.btnGif2Pngs.Text = "gif2pngs"
        Me.btnGif2Pngs.UseVisualStyleBackColor = True
        '
        'btnCharImgs2Gif
        '
        Me.btnCharImgs2Gif.Location = New System.Drawing.Point(243, 110)
        Me.btnCharImgs2Gif.Name = "btnCharImgs2Gif"
        Me.btnCharImgs2Gif.Size = New System.Drawing.Size(106, 34)
        Me.btnCharImgs2Gif.TabIndex = 7
        Me.btnCharImgs2Gif.Text = "charImg2gif"
        Me.btnCharImgs2Gif.UseVisualStyleBackColor = True
        '
        'btnGif2CharImg
        '
        Me.btnGif2CharImg.Location = New System.Drawing.Point(12, 64)
        Me.btnGif2CharImg.Name = "btnGif2CharImg"
        Me.btnGif2CharImg.Size = New System.Drawing.Size(402, 34)
        Me.btnGif2CharImg.TabIndex = 8
        Me.btnGif2CharImg.Text = "gif2charImg"
        Me.btnGif2CharImg.UseVisualStyleBackColor = True
        '
        'btnPngs2CharImg
        '
        Me.btnPngs2CharImg.Location = New System.Drawing.Point(133, 110)
        Me.btnPngs2CharImg.Name = "btnPngs2CharImg"
        Me.btnPngs2CharImg.Size = New System.Drawing.Size(88, 34)
        Me.btnPngs2CharImg.TabIndex = 9
        Me.btnPngs2CharImg.Text = "pngs2charImg"
        Me.btnPngs2CharImg.UseVisualStyleBackColor = True
        '
        'numUD
        '
        Me.numUD.DecimalPlaces = 1
        Me.numUD.Increment = New Decimal(New Integer() {5, 0, 0, 65536})
        Me.numUD.Location = New System.Drawing.Point(14, 8)
        Me.numUD.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.numUD.Name = "numUD"
        Me.numUD.Size = New System.Drawing.Size(83, 21)
        Me.numUD.TabIndex = 10
        '
        'rtb_log
        '
        Me.rtb_log.Location = New System.Drawing.Point(12, 150)
        Me.rtb_log.Name = "rtb_log"
        Me.rtb_log.Size = New System.Drawing.Size(400, 132)
        Me.rtb_log.TabIndex = 11
        Me.rtb_log.Text = ""
        '
        'nud_gif_delay
        '
        Me.nud_gif_delay.DecimalPlaces = 1
        Me.nud_gif_delay.Increment = New Decimal(New Integer() {5, 0, 0, 65536})
        Me.nud_gif_delay.Location = New System.Drawing.Point(228, 8)
        Me.nud_gif_delay.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.nud_gif_delay.Name = "nud_gif_delay"
        Me.nud_gif_delay.Size = New System.Drawing.Size(83, 21)
        Me.nud_gif_delay.TabIndex = 12
        '
        'label_Delay
        '
        Me.label_Delay.AutoSize = True
        Me.label_Delay.Location = New System.Drawing.Point(156, 13)
        Me.label_Delay.Name = "label_Delay"
        Me.label_Delay.Size = New System.Drawing.Size(65, 12)
        Me.label_Delay.TabIndex = 13
        Me.label_Delay.Text = "Gif Delay:"
        '
        'btn_switch
        '
        Me.btn_switch.Location = New System.Drawing.Point(348, 6)
        Me.btn_switch.Name = "btn_switch"
        Me.btn_switch.Size = New System.Drawing.Size(64, 23)
        Me.btn_switch.TabIndex = 14
        Me.btn_switch.Text = "toForm1"
        Me.btn_switch.UseVisualStyleBackColor = True
        '
        'gif2charImg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(430, 294)
        Me.Controls.Add(Me.btn_switch)
        Me.Controls.Add(Me.label_Delay)
        Me.Controls.Add(Me.nud_gif_delay)
        Me.Controls.Add(Me.rtb_log)
        Me.Controls.Add(Me.numUD)
        Me.Controls.Add(Me.btnPngs2CharImg)
        Me.Controls.Add(Me.btnGif2CharImg)
        Me.Controls.Add(Me.btnCharImgs2Gif)
        Me.Controls.Add(Me.btnGif2Pngs)
        Me.Controls.Add(Me.txtImgPath)
        Me.Controls.Add(Me.btnSelectPic)
        Me.MaximizeBox = False
        Me.Name = "gif2charImg"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "gif2charImg"
        CType(Me.numUD, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nud_gif_delay, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtImgPath As System.Windows.Forms.TextBox
    Friend WithEvents btnSelectPic As System.Windows.Forms.Button
    Friend WithEvents btnGif2Pngs As System.Windows.Forms.Button
    Friend WithEvents btnCharImgs2Gif As System.Windows.Forms.Button
    Friend WithEvents btnGif2CharImg As System.Windows.Forms.Button
    Friend WithEvents btnPngs2CharImg As System.Windows.Forms.Button
    Friend WithEvents numUD As System.Windows.Forms.NumericUpDown
    Friend WithEvents rtb_log As System.Windows.Forms.RichTextBox
    Friend WithEvents nud_gif_delay As System.Windows.Forms.NumericUpDown
    Friend WithEvents label_Delay As System.Windows.Forms.Label
    Friend WithEvents btn_switch As System.Windows.Forms.Button
End Class
