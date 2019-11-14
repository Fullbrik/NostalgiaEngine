using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NostalgiaEngine;
using NostalgiaEngine.Monogame;

namespace NostalgiaEngine.Renderers
{
    public class MinimapRenderer : Renderer
    {
        Texture2D lineSpr;

        public MinimapRenderer()
        {
            FOV = 90;
        }

        public override void Start()
        {
            base.Start();

            lineSpr = new Texture2D(Engine.Instance.GraphicsDevice, 1, 1);

            lineSpr.SetData(new Color[] { new Color(255, 255, 255) });
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            base.Render(spriteBatch);

            if (!IsActive)
                return;

            if(Player.CurrentRoomIndex < Level.Rooms.Length)
            {
                Room room = Level.Rooms[Player.CurrentRoomIndex];

                for (int i = 0; i < room.Points.Length; i++)
                {
                    int nextI = i + 1;

                    if (nextI >= room.Points.Length)
                        nextI = 0;

                    spriteBatch.DrawLine(lineSpr, Color.White, room.Points[i], room.Points[nextI]);
                }
            }
            

            foreach (var ent in Level.GetEntities<NostalgiaEntity>())
            {
                if(ent.IconSprite != null)
                {
                    spriteBatch.Draw(ent.IconSprite, ent.Location, null, Color.White, -ent.RadRotation, ent.IconSprite.Bounds.Center.ToVector2(), 1, SpriteEffects.None, 0);
                }
            }

            foreach (RaycastResult end in Player.Raycast(FOV, Engine.Instance.GraphicsDevice.Viewport.Width))
            {
                spriteBatch.DrawLine(lineSpr, new Color(end.WallPercent, end.WallPercent, end.WallPercent), Player.Location, end.End);
            }
        }
    }
}
