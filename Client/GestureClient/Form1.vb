Imports System.Net.Sockets

Imports OpenTK
Imports OpenTK.Graphics
Imports OpenTK.Graphics.OpenGL



Public Class frmMain

    Dim clientSocket As New System.Net.Sockets.TcpClient()
    Dim serverStream As NetworkStream

    Public viewport As GLControl

    Dim angleX As Double = 0.0
    Dim angleY As Double = 0.0

    Dim direction As String = "None"

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Form.CheckForIllegalCrossThreadCalls = False

        msg("Client Started")
        clientSocket.Connect("127.0.0.1", 8888)
        msg("Client Socket Program - Server Connected ...")

        viewport = New GLControl
        viewport.Dock = DockStyle.Fill
        Me.SplitContainer1.Panel1.Controls.Add(viewport)

        AddHandler viewport.Paint, AddressOf viewport_paint

        GL.Enable(EnableCap.Texture2D)

        Floor.InitTexture()

        GL.Enable(EnableCap.DepthTest)

        ' Enable Light 0 and set its parameters.
        GL.Light(LightName.Light0, LightParameter.Position, New Single() {1.0F, 1.0F, -0.5F})
        GL.Light(LightName.Light0, LightParameter.Ambient, New Single() {0.3F, 0.3F, 0.3F, 1.0F})
        GL.Light(LightName.Light0, LightParameter.Diffuse, New Single() {1.0F, 1.0F, 1.0F, 1.0F})
        GL.Light(LightName.Light0, LightParameter.Specular, New Single() {1.0F, 1.0F, 1.0F, 1.0F})
        GL.Light(LightName.Light0, LightParameter.SpotExponent, New Single() {1.0F, 1.0F, 1.0F, 1.0F})
        GL.LightModel(LightModelParameter.LightModelAmbient, New Single() {0.2F, 0.2F, 0.2F, 1.0F})
        GL.LightModel(LightModelParameter.LightModelTwoSide, 1)
        GL.LightModel(LightModelParameter.LightModelLocalViewer, 1)
        GL.Enable(EnableCap.Lighting)
        GL.Enable(EnableCap.Light0)

        ' Use GL.Material to set your object's material parameters.
        GL.Material(MaterialFace.Front, MaterialParameter.Ambient, New Single() {0.3F, 0.3F, 0.3F, 1.0F})
        GL.Material(MaterialFace.Front, MaterialParameter.Diffuse, New Single() {1.0F, 1.0F, 1.0F, 1.0F})
        GL.Material(MaterialFace.Front, MaterialParameter.Specular, New Single() {1.0F, 1.0F, 1.0F, 1.0F})
        GL.Material(MaterialFace.Front, MaterialParameter.Emission, New Single() {0F, 0F, 0F, 1.0F})

        timerAnim.Start()

    End Sub

    Private Sub cmdStart_Click(sender As Object, e As EventArgs)

        pythonServerListener.RunWorkerAsync()

    End Sub

    Sub msg(ByVal mesg As String)
        RichTextBox1.Text = RichTextBox1.Text + Environment.NewLine + " >> " + mesg
    End Sub

    Private Sub pythonServerListener_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles pythonServerListener.DoWork

        While True

            Dim serverStream As NetworkStream = clientSocket.GetStream()
            Dim outStream As Byte() =
            System.Text.Encoding.ASCII.GetBytes("Message from Client$")
            serverStream.Write(outStream, 0, outStream.Length)
            serverStream.Flush()

            Dim inStream(10024) As Byte
            serverStream.Read(inStream, 0, 1024)
            Dim returndata As String =
            System.Text.Encoding.ASCII.GetString(inStream)
            msg("Data from Server : " + returndata)


            'Dim curAngleX As Double = txtAngleX.Text
            'If returndata.Contains("North") Then
            'curAngleX += 0.1
            'txtAngleX.Text = curAngleX
            'End If

            If returndata.Contains("North") Then
                angleY += 0.5
            ElseIf returndata.Contains("South") Then
                angleY -= 0.5
            ElseIf returndata.Contains("East") Then
                angleX += 0.5
            ElseIf returndata.Contains("West") Then
                angleX -= 0.5
            End If


        End While

    End Sub

    Private Sub viewport_paint(sender As Object, e As PaintEventArgs)

        GL.ClearColor(Color.PaleVioletRed)

        'SetupViewport()
        DrawViewport()

        viewport.SwapBuffers()

    End Sub


    Public Sub SetupViewport()


        GL.Viewport(0, 0, viewport.Width, viewport.Height)

        Dim aspect_ratio As Double = Width / CDbl(Height)

        Dim perspective As OpenTK.Matrix4 = OpenTK.Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, CSng(aspect_ratio), 1, 64)
        GL.MatrixMode(MatrixMode.Projection)
        GL.LoadMatrix(perspective)

    End Sub


    Public Sub DrawViewport()


        GL.Clear(ClearBufferMask.ColorBufferBit Or ClearBufferMask.DepthBufferBit)

        Dim lookat As Matrix4 = Matrix4.LookAt(0, 0, 5, 0, 0, 0, 0, 1, 0)
        GL.MatrixMode(MatrixMode.Modelview)
        GL.LoadMatrix(lookat)

        'drawing functions

        GL.Rotate(angleX, 0, 1, 0)
        GL.Rotate(angleY, 1, 0, 0)

        Floor.draw()


    End Sub




    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox1.TextChanged

        RichTextBox1.SelectionStart = RichTextBox1.Text.Length
        RichTextBox1.ScrollToCaret()

    End Sub

    Private Sub SplitContainer1_Resize(sender As Object, e As EventArgs) Handles SplitContainer1.Resize

        Try
            SetupViewport()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub timerAnim_Tick(sender As Object, e As EventArgs) Handles timerAnim.Tick

        Me.Text = angleX.ToString

        viewport.Refresh()

    End Sub

    Private Sub cmdStart_Click_1(sender As Object, e As EventArgs) Handles cmdStart.Click

        pythonServerListener.RunWorkerAsync()

    End Sub

End Class
