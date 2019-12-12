Imports Gif.Components
Imports System.Drawing.Imaging
Imports System.Text

Public Class imgHelper
    Public Shared rRate As Double = 0.299
    Public Shared gRate As Double = 0.578
    Public Shared bRate As Double = 0.114
    Public Shared char_string() As String = New String() {"&", "#", "w", "s", "k", "d", "t", "j", "i", ".", " "}

    Public Shared Event Log As Action(Of String)

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

        average = 0.6 * average

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

    Public Shared Sub GifToPngs(ByVal giffile As String, ByVal directory As String)
        RaiseEvent Log("gif2pngs start")
        Dim gifDecoder As Gif.Components.GifDecoder = New Gif.Components.GifDecoder()
        directory += "\"

        If Not IO.Directory.Exists(directory) Then
            IO.Directory.CreateDirectory(directory)
        End If

        gifDecoder.Read(giffile)
        Dim i As Integer = 0, count As Integer = gifDecoder.GetFrameCount()

        While i < count

            Dim frame As Image = gifDecoder.GetFrame(i)
            frame.Save(directory & "\" & i.ToString("d2") & ".png", ImageFormat.Png)
            i += 1
            RaiseEvent Log(String.Format("gif2pngs done:{0}/{1}", i, count))
        End While
        RaiseEvent Log("gif2pngs end")
    End Sub

    Public Shared Sub PngsToGif(ByVal directory As String, ByVal giffile As String, ByVal time As Integer, ByVal repeat As Boolean)
        RaiseEvent Log("pngs2gif start")
        Dim pngfiles As String() = IO.Directory.GetFileSystemEntries(directory, "*.png")
        Dim e As AnimatedGifEncoder = New AnimatedGifEncoder()
        e.Start(giffile)
        e.SetDelay(time)
        e.SetRepeat(If(repeat, 0, -1))
        Dim i As Integer = 0, count As Integer = pngfiles.Length

        While i < count
            Using img = Image.FromFile(pngfiles(i))
                e.AddFrame(img)
                img.Dispose()
                i += 1
            End Using
            RaiseEvent Log(String.Format("pngs2gif done:{0}/{1}", i, count))
        End While

        e.Finish()
        RaiseEvent Log("pngs2gif end")
    End Sub

#Region "图片转文字"

    Public Shared Sub Img2CharTxt(ByVal bitmap As Bitmap, target_txt_path As String)

        Dim w As Integer = bitmap.Width - 1
        Dim h As Integer = bitmap.Height - 1
        Dim wStep As Integer = 1
        Dim hStep As Integer = 1

        Dim charLen As Integer = char_string.Length
        Dim unit = (256 + 1) / charLen

        Dim sb As New StringBuilder
        With sb
            For i = 0 To h Step hStep
                If i > 0 Then
                    .AppendLine()
                End If
                For j = 0 To w Step wStep
                    Dim color = bitmap.GetPixel(j, i)
                    Dim c As Char = RGB2CHAE(color.R, color.G, color.B)
                    .Append(c)
                Next
            Next
        End With
        Call IO.File.WriteAllText(target_txt_path, sb.ToString)
    End Sub

    Public Shared Function RGB2CHAE(r, g, b)
        Dim charLen As Integer = char_string.Length - 1
        Dim unit = (256 + 1) / charLen
        Dim gray As Integer = CInt((r * rRate + g * gRate + b * bRate))
        Dim idx = gray \ unit
        Return char_string(idx)
    End Function

#End Region

#Region "文字转图片"

    Public Shared Sub ImgCharTxt2Img(txt_path As String, img_path As String, img_width As Integer, img_height As Integer)
        Using charImg = New Bitmap(img_width * 18, img_height * 18)
            Dim g = Graphics.FromImage(charImg)
            g.Clear(Color.White)
            Dim f As Font = New System.Drawing.Font("Arial", 18, (System.Drawing.FontStyle.Bold))
            ' Dim b As Drawing2D.LinearGradientBrush = New System.Drawing.Drawing2D.LinearGradientBrush(New Rectangle(0, 0, charImg.Width, charImg.Height), Color.Blue, Color.DarkRed, 1.2F, True)
            Dim b = New SolidBrush(Color.Gray)
            Dim lines As List(Of String) = IO.File.ReadLines(txt_path).ToList

            Dim hStep As Integer = charImg.Height / lines.Count

            For h = 0 To lines.Count - 1
                Dim line = lines(h)
                Dim chars As List(Of Char) = line.ToList
                Dim wStep As Integer = charImg.Width / chars.Count
                For w = 0 To chars.Count - 1
                    Dim c = chars(w)
                    g.DrawString(c.ToString, f, b, w * wStep, h * hStep)
                Next
            Next
            charImg.Save(img_path)
            charImg.Dispose()
        End Using
        GC.Collect()
    End Sub

#End Region

    Public Shared Function ToSmallImg(ByVal bitmap As Bitmap, Optional delta As Double = 100)
        Dim width As Integer = bitmap.Width
        Dim height As Integer = bitmap.Height

        Dim max As Integer = IIf(width > height, width, height)

        Dim scale = max / delta
        width = width / scale
        height = height / scale
        Return New System.Drawing.Bitmap(bitmap, width, height)
    End Function

End Class
