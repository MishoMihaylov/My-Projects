using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GoliathGame.Interfaces.EngineInterfaces;

namespace GoliathGame.Models
{
    public abstract class SpriteObject : ISprite
    {
        //Да се провери как точно става рисуването на дадената позиция
        private Vector2 position;
        private Texture2D mainSpriteObjectTexture;
        private string assetName;

        //Име на картинката на обекта
        //protected string AssetName
        //{
        //    get { return this.assetName; }
        //    set { this.assetName = value;}
        //}

        //Позиция на обекта
        public Vector2 Position 
        {
            get { return this.position; }
            set
            {
                this.position = value;
            }
        }

        public float getPositionInX()
        {
            return this.position.X;
        }

        protected Texture2D MainSpriteObjectTexture
        {
            get { return this.mainSpriteObjectTexture; }
            set
            {
                this.mainSpriteObjectTexture = value;
            }
        }

        public string AssetName
        {
            get { return this.assetName; }
            set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Inproper input for unit asset!");
                }
                this.assetName = value;
            }
        }

        //Зареждане на обекта
        public virtual void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            this.MainSpriteObjectTexture = theContentManager.Load<Texture2D>(theAssetName);
            this.assetName = theAssetName;
        }

        //Update everything - movement, assetchange, health and others
        public virtual void Update(GameTime theGameTime){}

        //Чартане на обекта върху прозореца на играта
        public virtual void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(this.mainSpriteObjectTexture, this.Position,
                new Rectangle(0, 0, this.mainSpriteObjectTexture.Width, this.mainSpriteObjectTexture.Height),
                Color.White, 0.0f, Vector2.Zero, 1, SpriteEffects.None, 0);
        }
    }
}
