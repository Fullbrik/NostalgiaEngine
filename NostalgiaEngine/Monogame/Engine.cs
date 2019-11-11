#region Using Statements
using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

#endregion

namespace NostalgiaEngine.Monogame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Engine : Game
    {
        public static Engine Instance { get; private set; }

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D wall;

        public Room room;

        public Engine()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = false;

            room = new Room();

            room.Points = new Vector2[] { new Vector2(0, 0), new Vector2(300, 100), new Vector2(200, 300) };
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();

            Instance = this;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //TODO: use this.Content to load your game content here 
            wall = Content.Load<Texture2D>("Wall");
        }

        Vector2 location = new Vector2(100, 100);
        float rotation;

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // For Mobile devices, this logic will close the Game when the Back button is pressed
            // Exit() is obsolete on iOS
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }


            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                rotation += ((float)gameTime.ElapsedGameTime.TotalMilliseconds / 100f) * 10f;

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                rotation -= ((float)gameTime.ElapsedGameTime.TotalMilliseconds / 100f) * 10f;


            if (Keyboard.GetState().IsKeyDown(Keys.W))
                location.Y -= ((float)gameTime.ElapsedGameTime.TotalMilliseconds / 100f) * 10f;

            if (Keyboard.GetState().IsKeyDown(Keys.S))
                location.Y += ((float)gameTime.ElapsedGameTime.TotalMilliseconds / 100f) * 10f;

            if (Keyboard.GetState().IsKeyDown(Keys.D))
                location.X += ((float)gameTime.ElapsedGameTime.TotalMilliseconds / 100f) * 10f;

            if (Keyboard.GetState().IsKeyDown(Keys.A))
                location.X -= ((float)gameTime.ElapsedGameTime.TotalMilliseconds / 100f) * 10f;




            // TODO: Add your update logic here			
            base.Update(gameTime);
        }

        

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //TODO: Add your drawing code here
            spriteBatch.Begin();

            for (int i = 0; i < room.Points.Length; i++)
            {
                int nextI = i + 1;

                if (nextI >= room.Points.Length)
                    nextI = 0;

                spriteBatch.DrawLine(wall, Color.White, room.Points[i], room.Points[nextI]);
            }

            float FOV = MathHelper.ToRadians(90f);

            int columCount = GraphicsDevice.Viewport.Width;



            float halfFOV = FOV / 2;


            float angle = MathHelper.ToRadians(rotation);

            

            float angleStep = FOV / columCount;

            float startAngle = angle - halfFOV;

            

            for (int x = 0; x < columCount; x++)
            {
                if (room.Raycast(location, startAngle + (x * angleStep), true, out Vector2 end))
                    spriteBatch.DrawLine(wall, Color.Orange, location, end);
            }

            /*if (room.Raycast(location, angle, true, out Vector2 end))
                spriteBatch.DrawLine(GraphicsDevice, Color.Orange, location, end);

            */

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
