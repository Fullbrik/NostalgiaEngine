using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace NostalgiaEngine.Monogame
{
    public static class SpritebatchExt
    {
        public static void DrawLine(this SpriteBatch sb, GraphicsDevice graphicsDevice, Color color, Vector2 start, Vector2 end, int width = 1)
        {
            Texture2D pixel = new Texture2D(graphicsDevice, 1, 1);
            pixel.SetData(new Color[] { Color.White });

            sb.DrawLine(pixel, new Vector2(0, 0), new Vector2(1, 1), color, start, end, width);
        }

        public static void DrawLine(this SpriteBatch sb, Texture2D lineTexture, Vector2 textureSampleStart, Vector2 textureSampleSize, Color color, Vector2 start, Vector2 end, int width = 1)
        {
            Vector2 edge = end - start;
            // calculate angle to rotate line
            float angle =
                (float)Math.Atan2(edge.Y, edge.X);


            sb.Draw(lineTexture,
                new Rectangle(// rectangle defines shape of line and position of start of line
                    (int)start.X,
                    (int)start.Y,
                    (int)edge.Length(), //sb will strech the texture to fill this rectangle
                    width), //width of line, change this to make thicker line
                new Rectangle(textureSampleStart.ToPoint(), textureSampleSize.ToPoint()),
                color, //colour of line
                angle,     //angle of line (calulated above)
                new Vector2(0, 0), // point in line about which to rotate
                SpriteEffects.None,
                0);

        }
    }
}
