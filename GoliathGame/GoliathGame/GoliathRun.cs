using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GoliathGame.Models;
using GoliathGame.Models.UI;
using GoliathGame.Models.Engine;
using GoliathGame.Interfaces;
using System.Collections.Generic;

namespace GoliathGame
{
    public class GoliathRun : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Background levelBackground = new Background();
        Engine newEngine;

        public GoliathRun()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 1024;
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
            levelBackground.LoadContent(this.Content, "LevelsBackground/LevelOneBackground");    
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
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();           
            levelBackground.Draw(this.spriteBatch);
            newEngine.Draw(this.spriteBatch);       
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}