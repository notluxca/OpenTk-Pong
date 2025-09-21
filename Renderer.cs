using OpenTK.Graphics.OpenGL;

namespace Pong
{
    internal static class Renderer
    {
        public static void DrawRectangle(float x, float y, float height, float width)
        {
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex2(x - width / 2, y - height / 2);
            GL.Vertex2(x + width / 2, y - height / 2);
            GL.Vertex2(x + width / 2, y + height / 2);
            GL.Vertex2(x - width / 2, y + height / 2);
            GL.End();
        }

        public static void DrawText(float x, float y, string text) //todo implement text rendering
        {
        }

    }
}
