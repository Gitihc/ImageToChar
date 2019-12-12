Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Drawing.Drawing2D
Imports Gif.Components
Imports System.IO

Public Class Form1

    Dim picPath As String = "1.jpg"
    Dim basePath As String = AppDomain.CurrentDomain.BaseDirectory
    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        numUD.Value = 100
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim fileDialog As OpenFileDialog = New OpenFileDialog()
        fileDialog.InitialDirectory = basePath
        fileDialog.Filter = "图片文件|*.jpg;*.png;*.jpeg;*.bmp|所有文件|*.*"
        fileDialog.RestoreDirectory = False
        If fileDialog.ShowDialog() = DialogResult.OK Then
            TextBox1.Text = System.IO.Path.GetFullPath(fileDialog.FileName)
        End If
    End Sub

    Sub Image2Char()
        Try
            Dim picName As String = IO.Path.GetFileNameWithoutExtension(picPath)
            Dim txtpath As String = String.Format("{0}.txt", picName)
            Dim charimgpath As String = String.Format("char_{0}.png", picName)

            Dim sourceImg As Bitmap = New Bitmap(picPath, True)


            'sourceImg.Save("sourceImg.jpg")
            'Dim sourceTxt = ToChar(sourceImg)
            'IO.File.WriteAllText("sourceImg.txt", sourceTxt)

            'Dim smallPic As String = "smallPic.jpg"
            'SmallPicWidth(picPath, smallPic, 600)

            'Dim smallImg As Bitmap = New Bitmap(smallPic, True)

            'Dim grayImg As Bitmap = ToGray(smallImg)
            'grayImg.Save("grayImg.jpg")
            'Dim grayTxt = ToChar(grayImg)
            'IO.File.WriteAllText("grayImg.txt", grayTxt)

            'Dim binaryImg As Bitmap = ConvertTo1Bpp1(grayImg)
            'binaryImg.Save("binaryImg.jpg")
            'Dim imgTxt = ToChar(binaryImg)
            'IO.File.WriteAllText("binaryImg.txt", imgTxt)

            '放大缩小
            Dim smallImg As Bitmap = imgHelper.ToSmallImg(sourceImg, numUD.Value)
            Dim imgWidth As Integer = smallImg.Width
            Dim imgHeight As Integer = smallImg.Height

            'Dim count As Integer = charArray.Count - 1
            'Dim t(count + 2) As String

            'For i = 0 To count
            '    t(i) = charArray(i)
            'Next
            't(count + 1) = "."
            't(count + 2) = " "

            'imgHelper.char_string = t

            Call imgHelper.Img2CharTxt(smallImg, txtpath)
            'Dim smallTxt = ToChar(smallImg)
            smallImg.Dispose()
            'IO.File.WriteAllText(txtpath, smallTxt)

            Call imgHelper.ImgCharTxt2Img(txtpath, charimgpath, imgWidth, imgHeight)

            Dim path As String = basePath
            Process.Start(path)

            smallImg.Dispose()
            'grayImg.Dispose()
            'binaryImg.Dispose()

        Catch ex As Exception

        End Try

    End Sub

    Public Sub SmallPicWidth(ByVal strOldPic As String, ByVal strNewPic As String, ByVal intHeight As Integer)
        Dim objPic, objNewPic As System.Drawing.Bitmap

        Try
            objPic = New System.Drawing.Bitmap(strOldPic)

            If objPic.Height <= intHeight Then
                intHeight = objPic.Height
            End If

            Dim intWidth As Integer = (intHeight / objPic.Height) * objPic.Width
            objNewPic = New System.Drawing.Bitmap(objPic, intWidth, intHeight)
            If IO.File.Exists(strNewPic) Then
                IO.File.Delete(strNewPic)
            End If
            objNewPic.Save(strNewPic)
            objNewPic.Dispose()
            objPic.Dispose()
        Catch exp As Exception
            Throw exp
        Finally
            objPic = Nothing
            objNewPic = Nothing
        End Try
    End Sub

    Public Shared Function ToGray(ByVal bmp As Bitmap) As Bitmap
        For i As Integer = 0 To bmp.Width - 1

            For j As Integer = 0 To bmp.Height - 1
                Dim color As Color = bmp.GetPixel(i, j)
                Dim gray As Integer = CInt((color.R * 0.3 + color.G * 0.59 + color.B * 0.11))
                Dim newColor As Color = color.FromArgb(gray, gray, gray)
                bmp.SetPixel(i, j, newColor)
            Next
        Next

        Return bmp
    End Function

    Public Shared Function ConvertTo1Bpp1(ByVal bmp As Bitmap) As Bitmap
        Dim average As Integer = 0

        For i As Integer = 0 To bmp.Width - 1

            For j As Integer = 0 To bmp.Height - 1
                Dim color As Color = bmp.GetPixel(i, j)
                average += color.B
            Next
        Next

        average = CInt(average) / (bmp.Width * bmp.Height)

        For i As Integer = 0 To bmp.Width - 1

            For j As Integer = 0 To bmp.Height - 1
                Dim color As Color = bmp.GetPixel(i, j)
                Dim value As Integer = 255 - color.B
                Dim newColor As Color = If(value > average, color.FromArgb(0, 0, 0), color.FromArgb(255, 255, 255))
                bmp.SetPixel(i, j, newColor)
            Next
        Next

        Return bmp
    End Function

    Public Shared Function ConvertTo1Bpp2(ByVal img As Bitmap) As Bitmap
        Dim w As Integer = img.Width
        Dim h As Integer = img.Height
        Dim bmp As Bitmap = New Bitmap(w, h, PixelFormat.Format1bppIndexed)
        Dim data As BitmapData = bmp.LockBits(New Rectangle(0, 0, w, h), ImageLockMode.ReadWrite, PixelFormat.Format1bppIndexed)

        For y As Integer = 0 To h - 1
            Dim scan As Byte() = New Byte((w + 7) / 8 - 1) {}

            For x As Integer = 0 To w - 1
                Dim c As Color = img.GetPixel(x, y)

                If c.GetBrightness() >= 0.5 Then
                    'scan(x / 8) |= (byte)(0x80 >> (x % 8))
                End If
            Next

            Marshal.Copy(scan, 0, CType((CInt(data.Scan0) + data.Stride * y), IntPtr), scan.Length)
        Next

        Return bmp
    End Function

    Dim r As Regex = New Regex("[\u4E00-\u9fa5]")
    Dim strTxt As String = "ABCDEFG"
    Dim charArray() As Char = strTxt.ToCharArray()
    Dim maxCharCount As Integer = charArray.Count
    Dim charIndex As Integer = 0

    Public rRate As Double = 0.299
    Public gRate As Double = 0.578
    Public bRate As Double = 0.114

    Public Function ToChar(ByVal img As Bitmap) As String

        Dim a = img.Size.Width
        Dim w As Integer = img.Width - 1
        Dim h As Integer = img.Height - 1
        charIndex = 0
        Dim sb As StringBuilder = New StringBuilder
        With sb
            For i = 0 To h
                If i > 0 Then
                    .AppendLine()
                End If
                For j = 0 To w Step 2
                    Dim curChar = charArray(charIndex)
                    Dim curStr As String = curChar.ToString
                    If Not r.IsMatch(curChar) Then
                        curStr = (curChar + " ").ToString
                    End If

                    Dim color As Color = img.GetPixel(j, i)

                    Dim gray As Integer = CInt((color.R * rRate + color.G * gRate + color.B * bRate))
                    If gray < 180 Then
                        charIndex += 1
                        .Append(curStr)
                    Else
                        .Append("  ")
                    End If
                    If charIndex >= maxCharCount Then
                        charIndex = 0
                    End If
                Next
            Next
        End With
        Return sb.ToString
    End Function

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Button2.Enabled = False
        picPath = TextBox1.Text.Trim
        If IO.File.Exists(picPath) Then
            Image2Char()
        Else
            MsgBox("Please choose the picture first!")
        End If
        Button2.Enabled = True
    End Sub

    Private Sub TextBox2_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox2.TextChanged
        Dim tmpStr = TextBox2.Text.Trim
        If Not String.IsNullOrEmpty(tmpStr) Then
            strTxt = tmpStr
            charArray = strTxt.ToCharArray()
            maxCharCount = charArray.Count
        End If
    End Sub

    Private Sub Form1_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub btnToGray_Click(sender As System.Object, e As System.EventArgs) Handles btnToGray.Click
        Dim path As String = TextBox1.Text.Trim
        Dim picName As String = IO.Path.GetFileNameWithoutExtension(path)
        Dim grayPath As String = String.Format("gray_{0}.png", picName)
        Dim sourceImg As Bitmap = New Bitmap(path, True)
        Dim grayImg = imgHelper.ToGray(sourceImg)
        grayImg.Save(grayPath)
        grayImg.Dispose()
        sourceImg.Dispose()
        Process.Start(basePath)
    End Sub
End Class
