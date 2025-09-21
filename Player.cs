using OpenTK.Graphics.OpenGL;

namespace Pong
{
    internal class Player
    {
        private int x;
        private int y;
        private int height;
        private int width;
        private int speed;

        public int Direction { get; set; }

        public Player(int x, int y, int height, int width, int speed)
        {
            this.x = x;
            this.y = y;
            this.height = height;
            this.width = width;
            this.speed = speed;
        }

        public void Update()
        {
            y += Direction * speed;
        }

        public void Render()
        {
            Renderer.DrawRectangle(x, y, height, width);
        }
    }
}
