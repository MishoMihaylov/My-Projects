using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GoliathGame.Models.Core;
using GoliathGame.Interfaces.AnimationInterfaces;

namespace GoliathGame
{
    public sealed class GoliathRun : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        IEngine newEngine;

        public GoliathRun()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 1333;
            graphics.PreferredBackBufferHeight = 768;

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            newEngine = new Engine();
            base.Initialize();
            base.IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            newEngine.LoadContent(Content,"");
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
            this.Content.Unload();
            this.spriteBatch.Dispose();         
        }

        protected override void Update(GameTime gameTime)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) { Exit(); }        
            newEngine.Running(gameTime);
            base.Update(gameTime);
            
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGreen);

            spriteBatch.Begin();           
            newEngine.Draw(this.spriteBatch);       
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}