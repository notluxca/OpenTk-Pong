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
        private int current_Ydirection = 1;

        private int windowsWidth;
        private int windowsHeight;
        private Player player1;
        private Player player2;
        private Random random = new Random();

        // timer
        private bool isPaused = false;
        private double timer = 0;

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

        public void Update(double deltaTime)
        {
            // Timer 
            if (isPaused)
            {
                timer -= deltaTime;
                if (timer <= 0)
                {
                    isPaused = false;
                }
                return; // skip movement during pause
            }

            CheckAndProcessMapCollisions();
            CheckAndProcessPlayerCollisions();
            Move();
        }

        private void Move() {
            x += current_direction * ballSpeed;
            y += current_Ydirection * ballSpeed;
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
        private void CheckAndProcessPlayerCollisions()
        {
            if (IsCollidingWithPlayer(player1) || IsCollidingWithPlayer(player2))
            {
                InvertDirection();
            }
        }
        private void CheckAndProcessMapCollisions()
        {
            // -- Walls
            // right border
            if (x + size / 2 >= windowsWidth / 2)
            {
                ResetBall();
                Game.AddScore(1);
            }
            // left border
            else if (x - size / 2 <= -windowsWidth / 2)
            {
                ResetBall();
                Game.AddScore(2);
            }
            // -- Top and Bottom borders
            // top border
            if (y + size / 2 >= windowsHeight / 2)
            {
                current_Ydirection *= -1;
            } // bottom border
            else if (y - size / 2 <= -windowsHeight / 2)
            {
                current_Ydirection *= -1;
            }
        }
        private void ResetBall()
        {
            x = 0;
            y = 0;
            current_direction = random.Next(0, 2) == 0 ? -1 : 1;
            current_Ydirection = random.Next(0, 2) == 0 ? -1 : 1;

            // pause for 2 seconds
            isPaused = true;
            timer = 2.0;
        }

    }
}
