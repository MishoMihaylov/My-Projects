using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GoliathGame.Interfaces.EngineInterfaces;
using GoliathGame.Interfaces;

namespace GoliathGame.Models.UI
{
    class UIButton : ISprite
    {

        private Vector2 buttonPosition = new Vector2(0, 0);
        private Texture2D buttonTexture;
        private string buttonAssetName;

        public UIButton()
        {
        }

        public Vector2 Position
        {
            get { return this.buttonPosition; }
            set { this.buttonPosition = value; }
        }

        public void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            buttonTexture = theContentManager.Load<Texture2D>(theAssetName);
            this.buttonAssetName = theAssetName;
        }

        public void Update(GameTime theGameTime)
        {
            
        }

        public void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(this.buttonTexture, this.buttonPosition, Color.White);
            theSpriteBatch.Draw(this.buttonTexture, this.Position,
                new Rectangle(0, 0, this.buttonTexture.Width, this.buttonTexture.Height),
                Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
        }

        public void Click()
        {
        }
    }
}
