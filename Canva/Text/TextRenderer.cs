// TextRenderer.cs (usando System.Drawing)
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using OpenTK.Graphics.OpenGL;

// Troque o namespace se o seu for diferente
namespace Pong.Canva.Text
{
    internal class TextRenderer : IDisposable
    {
        private readonly Font _font;
        private int _textureId;
        private int _textureWidth;
        private int _textureHeight;
        private string _lastText;

        public TextRenderer(string fontFamily, int size)
        {
            _font = new Font(fontFamily, size);
            _textureId = -1; // -1 indica que a textura ainda não foi criada
            _lastText = string.Empty;
        }

        // Este método cria/atualiza a textura somente quando o texto muda
        public void UpdateText(string text)
        {
            if (text == _lastText)
                return; // Nenhuma mudança, não faz nada

            _lastText = text;

            // Se o texto for vazio, limpa a textura
            if (string.IsNullOrEmpty(text))
            {
                DeleteTexture();
                return;
            }

            // 1. Medir o tamanho do texto para criar um Bitmap do tamanho certo
            using (var bmpTemp = new Bitmap(1, 1))
            using (var gfxTemp = Graphics.FromImage(bmpTemp))
            {
                var textSize = gfxTemp.MeasureString(text, _font);
                _textureWidth = (int)Math.Ceiling(textSize.Width);
                _textureHeight = (int)Math.Ceiling(textSize.Height);
            }

            // 2. Criar o Bitmap e desenhar o texto nele
            using (var bmp = new Bitmap(_textureWidth, _textureHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
            using (var gfx = Graphics.FromImage(bmp))
            {
                gfx.TextRenderingHint = TextRenderingHint.AntiAlias; // Deixa o texto mais suave
                gfx.Clear(Color.Transparent);
                gfx.DrawString(text, _font, Brushes.White, new PointF(0, 0));

                // 3. Copiar os dados do Bitmap para a textura OpenGL
                if (_textureId == -1)
                    _textureId = GL.GenTexture();

                GL.BindTexture(TextureTarget.Texture2D, _textureId);

                BitmapData data = bmp.LockBits(
                    new Rectangle(0, 0, bmp.Width, bmp.Height),
                    ImageLockMode.ReadOnly,
                    System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                GL.TexImage2D(TextureTarget.Texture2D, 0,
                    PixelInternalFormat.Rgba,
                    data.Width, data.Height, 0,
                    OpenTK.Graphics.OpenGL.PixelFormat.Bgra,
                    PixelType.UnsignedByte,
                    data.Scan0);

                bmp.UnlockBits(data);

                // Configurar parâmetros da textura
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            }
        }

        // Este método desenha a textura na tela
        public void Render(float x, float y)
        {
            if (_textureId == -1) return;

            // Salva o estado do OpenGL para não afetar outros desenhos
            GL.PushAttrib(AttribMask.EnableBit | AttribMask.TextureBit | AttribMask.CurrentBit);

            // Ativa blending para a transparência do texto funcionar
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            // Ativa o uso de texturas
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, _textureId);

            // Desenha um retângulo (quad) com a textura aplicada
            // O 'x' e 'y' aqui representam o centro do texto
            float halfW = _textureWidth / 2f;
            float halfH = _textureHeight / 2f;

            GL.Begin(PrimitiveType.Quads);
            {
                GL.TexCoord2(0, 0); GL.Vertex2(x - halfW, y - halfH);
                GL.TexCoord2(1, 0); GL.Vertex2(x + halfW, y - halfH);
                GL.TexCoord2(1, 1); GL.Vertex2(x + halfW, y + halfH);
                GL.TexCoord2(0, 1); GL.Vertex2(x - halfW, y + halfH);
            }
            GL.End();

            // Restaura o estado anterior do OpenGL
            GL.PopAttrib();
        }

        private void DeleteTexture()
        {
            if (_textureId != -1)
            {
                GL.DeleteTexture(_textureId);
                _textureId = -1;
            }
        }

        public void Dispose()
        {
            _font.Dispose();
            DeleteTexture();
        }
    }
}