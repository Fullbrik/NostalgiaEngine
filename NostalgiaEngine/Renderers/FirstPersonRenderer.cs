using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NostalgiaEngine.Monogame;

namespace NostalgiaEngine.Renderers
{
    public class FirstPersonRenderer : Renderer
    {
        Texture2D lineSpr;

        public FirstPersonRenderer()
        {
        }

        public override void Start()
        {
            base.Start();

            lineSpr = new Texture2D(Engine.Instance.GraphicsDevice, 1, 1);

            lineSpr.SetData(new Color[] { new Color(255, 255, 255) });
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            base.Render(spriteBatch);

            Viewport screen = Engine.Instance.GraphicsDevice.Viewport;

            RaycastResult[] hits = Player.Raycast(FOV, Engine.Instance.GraphicsDevice.Viewport.Width).ToArray();


            int i = 0;

            foreach (var hit in Player.Raycast(FOV, Engine.Instance.GraphicsDevice.Viewport.Width))
            {
                float rayDist = Vector2.Distance(Player.Location, hit.End);

                Console.WriteLine("Line dustance: " + rayDist.ToString());

                float top = (screen.Height / 2f) - (screen.Height / rayDist);

                spriteBatch.DrawLine(lineSpr, new Color((byte)(rayDist), (byte)(rayDist), (byte)(rayDist)), new Vector2(i, top), new Vector2(i, screen.Height - top));

                i++;
            }

            /*
            for (int i = 0; i < screen.Width; i++)
            {
                RaycastResult end = hits[i];

                float rayDist = Vector2.Distance(Player.Location, hits[i].End);

                float top = (screen.Height / 2f) - (screen.Height / rayDist);

                spriteBatch.DrawLine(lineSpr, new Color((byte)(rayDist), (byte)(rayDist), (byte)(rayDist)), new Vector2(i, top), new Vector2(i, screen.Height - top));

                //throw new Exception(top.ToString());
            }
            */
        }
    }
}
