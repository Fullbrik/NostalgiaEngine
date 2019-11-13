using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NostalgiaEngine
{
    public class NostalgiaPlayer : NostalgiaEntity
    {
        public int CurrentRoomIndex = 0;

        private Texture2D icon;

        public override Texture2D IconSprite => icon;

        public NostalgiaPlayer()
        {
            Rotation = 0;
        }

        public override void Start()
        {
            base.Start();

            icon = new Texture2D(NostalgiaEngine.Monogame.Engine.Instance.GraphicsDevice, 10, 10);

            Color[] spr = new Color[icon.Width * icon.Height];

            for (int i = 0; i < spr.Length; i++)
            {
                spr[i] = new Color(255, 1, 100);
            }

            icon.SetData(spr);

            Level.CreateEntity<Renderers.MinimapRenderer>();
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            KeyboardState inp = Keyboard.GetState();

            Vector2 movement = Location;

            if (inp.IsKeyDown(Keys.W))
                movement -= ForwardVector * deltaTime * 100;

            if (inp.IsKeyDown(Keys.S))
                movement += ForwardVector * deltaTime * 100;

            if (inp.IsKeyDown(Keys.A))
                movement -= RightVector * deltaTime * 100;

            if (inp.IsKeyDown(Keys.D))
                movement += RightVector * deltaTime * 100;



            Point mouse = Mouse.GetState().Position;


            if(mouse.X > 0)
                Rotation += deltaTime * 50;

            if(mouse.X < 0)
                Rotation -= deltaTime * 50;


            Mouse.SetPosition(0, 0);

            Location = movement;
        }

        public IEnumerable Raycast(float fov, int rayCount)
        {
            Room room = Level.Rooms[CurrentRoomIndex];

            float startAngle = MathHelper.ToRadians(Rotation - (fov / 2));

            float endAngle = MathHelper.ToRadians(Rotation + (fov / 2));

            float stepAngle = MathHelper.ToRadians(fov/(float)rayCount);

            for (int ray = 0; ray < rayCount; ray++)
            {
                if(room.Raycast(Location, startAngle + (stepAngle * ray), out RaycastResult end))
                {
                    yield return end;
                }
                else
                {
                    yield return null;
                }
            }
        }
    }
}
