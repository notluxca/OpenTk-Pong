using System;
using System.Media;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Pong
{
    internal class Program : GameWindow
    {

        int xBallPosition = 0;
        int yBallPosition = 0;

        int playerYDirection = 0;
        float playerSpeed = 5.0f;



        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            KeyDown += Program_KeyDown;
            KeyUp += Program_KeyUp;
        }
        

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            xBallPosition++;
        }

        private void Program_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            Console.WriteLine(e.Key);
            switch (e.Key)
            {
                case Key.Up:
                    playerYDirection = 1;
                    break;
                case Key.Down:
                    playerYDirection = -1;
                    break;
                default:
                    return;
            }
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);

            Matrix4 projection = Matrix4.CreateOrthographic(Width, Height, 0.0f, 1.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            DrawRectangle(xBallPosition, yBallPosition, 20, 20);
            DrawRectangle(-300, 0, 90, 20);
            DrawRectangle(300, 0, 90, 20);

            SwapBuffers();
        }

        private void DrawRectangle(float x, float y, float Height, float Width)
        {
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex2(x - Width / 2, y - Height / 2);
            GL.Vertex2(x + Width / 2, y - Height / 2);
            GL.Vertex2(x + Width / 2, y + Height / 2);
            GL.Vertex2(x - Width / 2, y + Height / 2);
            GL.End();
        }

        static void Main()
        {
            new Program().Run();
        }
    }
}
