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

        public NostalgiaLevel CurrentLevel { get; private set; }

        #region Monogame Variables
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        #endregion

        Texture2D wall;

        public Engine()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = false;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            Instance = this;
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            wall = Content.Load<Texture2D>("Wall");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            if (CurrentLevel != null)
                CurrentLevel.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //TODO: Add your drawing code here
            spriteBatch.Begin();

            if (CurrentLevel != null)
                CurrentLevel.Render(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }


        public T LoadLevel<T>()
            where T : NostalgiaLevel, new()
        {
            if (CurrentLevel != null)
                CurrentLevel.Dispose();

            CurrentLevel = new T();
            CurrentLevel.Start();

            return (T)CurrentLevel;
        }
    }
}
