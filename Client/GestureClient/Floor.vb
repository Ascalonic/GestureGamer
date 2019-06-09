
Imports System.Drawing.Imaging
Imports OpenTK
Imports OpenTK.Graphics
Imports OpenTK.Graphics.OpenGL

Public Module Floor

    Public texture_path As String = "checker.png"

    Dim bmp As Bitmap = New Bitmap(texture_path)
    Dim texture As Integer

    Dim length As Single = 3.0

    Public Sub InitTexture()

        GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest)

        GL.GenTextures(1, texture)
        GL.BindTexture(TextureTarget.Texture2D, texture)
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, CInt(TextureMinFilter.Linear))
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, CInt(TextureMagFilter.Linear))

        Dim data As BitmapData = bmp.LockBits(New System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.[ReadOnly], System.Drawing.Imaging.PixelFormat.Format32bppArgb)

        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
            OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0)

        bmp.UnlockBits(data)

    End Sub



    Public Sub draw()

        GL.BindTexture(TextureTarget.Texture2D, texture)

        GL.Begin(PrimitiveType.Quads)

        GL.TexCoord2(0.0F, 1.0F) : GL.Vertex3(-1.0F * length, 0.0F, -1.0F * length) : GL.Normal3(0.0F, 1.0F, 0.0F)
        GL.TexCoord2(1.0F, 1.0F) : GL.Vertex3(-1.0F * length, 0.0F, 1.0F * length) : GL.Normal3(0.0F, 1.0F, 0.0F)
        GL.TexCoord2(1.0F, 0.0F) : GL.Vertex3(1.0F * length, 0.0F, 1.0F * length) : GL.Normal3(0.0F, 1.0F, 0.0F)
        GL.TexCoord2(0.0F, 0.0F) : GL.Vertex3(1.0F * length, 0.0F, -1.0F * length) : GL.Normal3(0.0F, 1.0F, 0.0F)

        GL.End()

    End Sub




End Module
