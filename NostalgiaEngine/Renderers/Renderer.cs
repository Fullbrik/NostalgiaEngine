using System;
using Microsoft.Xna.Framework.Graphics;

namespace NostalgiaEngine.Renderers
{
    public class Renderer : NostalgiaEntity
    {
        public virtual float FOV { get; set; }

        public bool IsActive { get; set; }

        protected NostalgiaPlayer Player;

        public Renderer()
        {
        }

        public override void Start()
        {
            base.Start();

            Player = Level.GetEntity<NostalgiaPlayer>();
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if(Player == null)
                Player = Level.GetEntity<NostalgiaPlayer>();
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            base.Render(spriteBatch);

            if (!IsActive)
                return;
        }
    }
}
