using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NostalgiaEngine
{
    public class NostalgiaEntity : IDisposable
    {
        private Vector2 location;
        public Vector2 Location { get => location;
            set
            {
                location = value;
            }
        }

        private float rotation;
        public float Rotation { get => rotation;
            set
            {
                rotation = value;

                forwardVector = new Vector2((float)Math.Sin(RadRotation), (float)Math.Cos(RadRotation));
                rightVector = new Vector2((float)Math.Cos(RadRotation), -(float)Math.Sin(RadRotation));
            }
        }
        public float RadRotation
        {
            get => MathHelper.ToRadians(rotation);
            set => Rotation = MathHelper.ToDegrees(value);
        }

        private Vector2 forwardVector;
        public Vector2 ForwardVector { get => forwardVector; }

        private Vector2 rightVector;
        public Vector2 RightVector { get => rightVector; }


        public NostalgiaEntity()
        {
        }

        public virtual void Start()
        {

        }

        public virtual void Update(float deltaTime)
        {

        }

        public virtual void Render(SpriteBatch spriteBatch)
        {

        }

        public virtual void End()
        {

        }

        public void Dispose()
        {
            End();
        }
    }
}
