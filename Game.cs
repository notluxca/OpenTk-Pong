using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Pong
{
    internal class Game : GameWindow
    {
        private Ball ball;
        private Player player1;
        private Player player2;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);

            player1 = new Player(-300, 0, 90, 20, 8);
            player2 = new Player(300, 0, 90, 20, 8);
            ball = new Ball(0, 0, 10, Width, Height, player1, player2);

            KeyDown += Game_KeyDown;
            KeyUp += Game_KeyUp;
        }

        private void Game_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W: player1.Direction = 1; break;
                case Key.S: player1.Direction = -1; break;
                case Key.Up: player2.Direction = 1; break;
                case Key.Down: player2.Direction = -1; break;
            }
        }

        private void Game_KeyUp(object sender, KeyboardKeyEventArgs e)
        {    
            switch (e.Key)
            {
                case Key.W: player1.Direction = 0; break;
                case Key.S: player1.Direction = 0; break;
                case Key.Up: player2.Direction = 0; break;
                case Key.Down: player2.Direction = 0; break;
            }
        }
        

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            ball.Update();
            player1.Update();
            player2.Update();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);

            Matrix4 projection = Matrix4.CreateOrthographic(Width, Height, 0.0f, 1.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            ball.Render();
            player1.Render();
            player2.Render();

            SwapBuffers();
        }
    }
}
