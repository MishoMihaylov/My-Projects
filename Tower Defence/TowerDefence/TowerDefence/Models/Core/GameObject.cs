using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TowerDefence.Interfaces.Core;

namespace TowerDefence.Models.Core
{
    public abstract class GameObject : IGameObject
    {
        protected string MainAssetPath;
        protected Texture2D MainAsset;

        public Vector2 CenterPoint { get; protected set; }

        public Vector2 Position { get; protected set; }

        public GameObject(string mainAssetPath, Vector2 startingPosition)
        {
            this.MainAssetPath = mainAssetPath;
            this.Position = startingPosition;
           
        }

        protected virtual void Draw(SpriteBatch spriteBatch, Texture2D asset, Rectangle destinationRect,
            Rectangle? sourceRect, Color color)
        {
            spriteBatch.Draw(asset, destinationRect, sourceRect, color);
        }

        public abstract void Draw(SpriteBatch spriteBatch);

        public abstract void LoadContent(ContentManager theContentManager);

        public abstract void UnloadContent();

        public abstract void Update(GameTime gameTime);
    }
}
