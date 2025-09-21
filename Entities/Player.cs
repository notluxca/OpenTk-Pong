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
        private int windowsHeight;

        public int X { get { return x; } }
        public int Y { get { return y; } }
        public int Width { get { return width; } }
        public int Height { get { return height; } }

        public int Direction { get; set; }

        public Player(int x, int y, int height, int width, int speed, int windowsHeight) 
        {
            this.x = x;
            this.y = y;
            this.height = height;
            this.width = width;
            this.speed = speed;
            this.windowsHeight = windowsHeight;
        }

        public void Update()
        {
            // check if it will be out of screen limits
            if (y + Direction * speed + height / 2 > windowsHeight / 2 || y + Direction * speed - height / 2 < -windowsHeight / 2) return;
            y += Direction * speed;
        }

        public void Render()
        {
            Renderer.DrawRectangle(x, y, height, width);
        }
    }
}
