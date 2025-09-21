using OpenTK.Graphics.OpenGL;
using System;

namespace Pong
{
    internal class Ball
    {
        private int x;
        private int y;
        private int size;

        private int ballSpeed = 5;
        private int current_direction = 1;

        private int windowsWidth;
        private int windowsHeight;
        private Player player1;
        private Player player2;

        public Ball(int x, int y, int size, int windowsWidth, int windowsHeight, Player player1, Player player2 )
        {
            this.x = x;
            this.y = y;
            this.size = size;
            this.windowsWidth = windowsWidth;
            this.windowsHeight = windowsHeight;
            this.player1 = player1;
            this.player2 = player2;
        }

        public void Update()
        {
            // right border
            if (x + size / 2 >= windowsWidth / 2 )
            {
                InvertDirection();
            }
            // left border
            else if (x - size / 2 <= -windowsWidth / 2)
            {
                InvertDirection();
            }

            if(IsCollidingWithPlayer(player1) || IsCollidingWithPlayer(player2))
            {
                InvertDirection();
            }

            x += current_direction * ballSpeed;

            Console.WriteLine(player1.Y);
        }

        private bool IsCollidingWithPlayer(Player player)
        {
            return (x - size / 2 <= player.X + player.Width / 2 && x + size / 2 >= player.X - player.Width / 2) &&
                   (y + size / 2 >= player.Y - player.Height / 2 && y - size / 2 <= player.Y + player.Height / 2);
        }
        public void Render()
        {
            Renderer.DrawRectangle(x, y, size, size);
        }

        private void InvertDirection()
        {
            current_direction *= -1;
        }

    }
}
