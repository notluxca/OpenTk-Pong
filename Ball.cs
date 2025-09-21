using OpenTK.Graphics.OpenGL;

namespace Pong
{
    internal class Ball
    {
        private int x;
        private int y;
        private int size;

        public Ball(int x, int y, int size)
        {
            this.x = x;
            this.y = y;
            this.size = size;
        }

        public void Update()
        {
            x++;
        }

        public void Render()
        {
            Renderer.DrawRectangle(x, y, size, size);
        }
    }
}
