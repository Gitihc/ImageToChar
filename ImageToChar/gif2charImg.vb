Imports System.Text
Imports Gif.Components
Imports System.Drawing.Imaging
Imports System.Threading

Public Class gif2charImg
    Dim basePath As String = System.Windows.Forms.Application.StartupPath

    Dim imgBasePath As String = String.Empty
    Dim pngsPath As String = String.Empty
    Dim chartxtPath As String = String.Empty
    Dim charimgPath As String = String.Empty
    Dim charGifPath As String = String.Empty

    Dim gifName As String = String.Empty
    Dim gifPath As String = String.Empty

    Private Sub OpenBasePath()
        System.Diagnostics.Process.Start(imgBasePath)
    End Sub

    Private Sub btnSelectPic_Click(sender As System.Object, e As System.EventArgs) Handles btnSelectPic.Click
        Dim fileDialog As OpenFileDialog = New OpenFileDialog()
        fileDialog.InitialDirectory = basePath
        fileDialog.Filter = "图片文件|*.gif;|所有文件|*.*"
        fileDialog.RestoreDirectory = False
        If fileDialog.ShowDialog() = DialogResult.OK Then
            txtImgPath.Text = System.IO.Path.GetFullPath(fileDialog.FileName)
            gifPath = txtImgPath.Text.Trim
            If Not IO.File.Exists(gifPath) Then
                Throw New ApplicationException("file not exist！")
            End If
            log(String.Format("select file: {0}", gifPath))
            gifName = IO.Path.GetFileNameWithoutExtension(gifPath)
            Call InitBasePath()
        End If
    End Sub

    Private Sub InitBasePath()
        imgBasePath = IO.Path.Combine(basePath, gifName)
        pngsPath = IO.Path.Combine(imgBasePath, "gif2pngs")
        chartxtPath = IO.Path.Combine(imgBasePath, "chartxt")
        charimgPath = IO.Path.Combine(imgBasePath, "charimg")
        charGifPath = IO.Path.Combine(imgBasePath, String.Format("{0}.gif", gifName))

        If Not IO.Directory.Exists(imgBasePath) Then
            IO.Directory.CreateDirectory(imgBasePath)
        End If
        If Not IO.Directory.Exists(pngsPath) Then
            IO.Directory.CreateDirectory(pngsPath)
        End If
        If Not IO.Directory.Exists(chartxtPath) Then
            IO.Directory.CreateDirectory(chartxtPath)
        End If
        If Not IO.Directory.Exists(charimgPath) Then
            IO.Directory.CreateDirectory(charimgPath)
        End If

        BtnEnable(Me.btnGif2Pngs, True)
        BtnEnable(Me.btnPngs2CharImg, True)
        BtnEnable(Me.btnCharImgs2Gif, True)
        BtnEnable(Me.btnGif2CharImg, True)
    End Sub

    Private Sub btnGif2CharImg_Click(sender As System.Object, e As System.EventArgs) Handles btnGif2CharImg.Click
        BtnEnable(Me.btnGif2CharImg, False)
        Call ThreadPool.QueueUserWorkItem(
           Sub()
               Call Gif2Pngs()
               Call Pngs2CharImg()
               Call Pngs2Gif()
               BtnEnable(Me.btnGif2CharImg, True)
               OpenBasePath()
           End Sub)
    End Sub

    Private Sub btnGif2Pngs_Click(sender As System.Object, e As System.EventArgs) Handles btnGif2Pngs.Click
        BtnEnable(Me.btnGif2Pngs, False)
        Call ThreadPool.QueueUserWorkItem(
            Sub()
                Call Gif2Pngs()
                BtnEnable(Me.btnGif2Pngs, True)
                OpenBasePath()
            End Sub)
    End Sub

    Private Sub btnCharImgs2Gif_Click(sender As System.Object, e As System.EventArgs) Handles btnCharImgs2Gif.Click
        BtnEnable(Me.btnCharImgs2Gif, False)
        Threading.ThreadPool.QueueUserWorkItem(
            Sub()
                Call Pngs2Gif()
                BtnEnable(Me.btnCharImgs2Gif, True)
                OpenBasePath()
            End Sub)
    End Sub

    Private Sub btnPngs2CharImg_Click(sender As System.Object, e As System.EventArgs) Handles btnPngs2CharImg.Click
        BtnEnable(Me.btnPngs2CharImg, False)
        Threading.ThreadPool.QueueUserWorkItem(
            Sub()
                Call Pngs2CharImg()
                BtnEnable(Me.btnPngs2CharImg, True)
                OpenBasePath()
            End Sub)

    End Sub


    Private Sub BtnEnable(btn As Button, enable As Boolean)
        If Me.InvokeRequired Then
            Me.Invoke(Sub()
                          btn.Enabled = enable
                      End Sub)
        Else
            btn.Enabled = enable
        End If
    End Sub

    Private Sub gif2charImg_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        numUD.Value = 100
        nud_gif_delay.Value = 100
        AddHandler imgHelper.Log, AddressOf RecordInfo

        BtnEnable(Me.btnGif2Pngs, False)
        BtnEnable(Me.btnPngs2CharImg, False)
        BtnEnable(Me.btnCharImgs2Gif, False)
        BtnEnable(Me.btnGif2CharImg, False)
    End Sub

    Private Sub RecordInfo(msg As String)
        log(msg)
    End Sub

    Private Sub log(msg As String)
        If Me.InvokeRequired Then
            Me.Invoke(Sub()
                          showlog(msg)
                      End Sub)
        Else
            showlog(msg)
        End If
    End Sub

    Private Sub showlog(msg As String)
        If Me.rtb_log.Text.Length > 0 Then
            Me.rtb_log.AppendText(vbNewLine)
        End If
        Me.rtb_log.AppendText(msg)

        '//让文本框获取焦点 
        Me.rtb_log.Focus()
        '//设置光标的位置到文本尾 
        Me.rtb_log.Select(Me.rtb_log.TextLength, 0)
        '//滚动到控件光标处 
        Me.rtb_log.ScrollToCaret()
    End Sub

#Region "实现方法"
    Private Sub Gif2Pngs()
        Call imgHelper.GifToPngs(gifPath, pngsPath)
    End Sub

    Private Sub Pngs2CharImg()
        log("pngs2charimg start")
        Dim pngfiles As String() = IO.Directory.GetFileSystemEntries(pngsPath, "*.png")
        Dim count As Integer = pngfiles.Count
        Dim idx As Integer = 0
        For Each pf In pngfiles
            Dim tmpPf As String = pf
            Dim pfName As String = IO.Path.GetFileNameWithoutExtension(pf)
            Dim txtPath As String = IO.Path.Combine(chartxtPath, String.Format("{0}.txt", pfName))
            Dim sourceImg = New Bitmap(tmpPf)

            '放大缩小
            Dim smallImg As Bitmap = imgHelper.ToSmallImg(sourceImg, numUD.Value)

            Dim imgWidth As Integer = smallImg.Width
            Dim imgHeight As Integer = smallImg.Height

            Call imgHelper.Img2CharTxt(smallImg, txtPath)
            smallImg.Dispose()

            Dim pngPath As String = IO.Path.Combine(charimgPath, String.Format("{0}.png", pfName))
            Call imgHelper.ImgCharTxt2Img(txtPath, pngPath, imgWidth, imgHeight)
            idx += 1
            log(String.Format("pngs2charimg done:{0}/{1}", idx, count))
        Next
        log("pngs2charimg end")
    End Sub

    Private Sub Pngs2Gif()
        Call imgHelper.PngsToGif(charimgPath, charGifPath, nud_gif_delay.Value, True)
    End Sub

#End Region

   
    Private Sub btn_switch_Click(sender As System.Object, e As System.EventArgs) Handles btn_switch.Click
        Dim f1 = New Form1()
        Me.Hide()
        If f1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Me.Show()
        End If

    End Sub
End Class