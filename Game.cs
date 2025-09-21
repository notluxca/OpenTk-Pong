using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using Pong.Canva.Text;
using QuickFont;
using System;
using System.Drawing;
namespace Pong
{
    internal class Game : GameWindow
    {
        private Ball ball;
        private Player player1;
        private Player player2;

        public static int player1Score = 0;
        public static int player2Score = 0;
        private TextRenderer textRenderer;
        

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            Width = 800;

            player1 = new Player(-380, 0, 90, 20, 8, Height);
            player2 = new Player(380, 0, 90, 20, 8, Height);
            ball = new Ball(0, 0, 10, Width, Height, player1, player2);
            textRenderer = new TextRenderer("C:\\Users\\Notluxca\\source\\repos\\Pong\\Assets\\Fonts\\PixelFont.ttf", 32);
            textRenderer.Dispose();

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

            ball.Update(e.Time);
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

            // textRenderer.UpdateText($"{player1Score} - {player2Score}");
            ball.Render();
            player1.Render();
            player2.Render();

            SwapBuffers();
        }

        public static void AddScore(int player)
        {
            if (player == 1)
                player1Score++;
            else if (player == 2)
                player2Score++;

            Console.Clear();
            // Console.WriteLine($"Score: Player 1 - {player1Score}, Player 2 - {player2Score}");            
        }
    }
}
