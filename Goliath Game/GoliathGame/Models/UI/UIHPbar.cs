using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GoliathGame.Interfaces;
using GoliathGame.Interfaces.EngineInterfaces;
using Microsoft.Xna.Framework.Input;

namespace GoliathGame.Models.UI
{
    public class UIHPbar : ISprite
    {
        private Vector2 position;
        private Texture2D bar;
        IUnit currentUnit;
        private int scaleX = 1; 
        private int scaleY = 1;
        private int width;
        private int height;

        public Rectangle healthRect;

        public Vector2 Position
        {
            get
            {
                return this.position;
            }
            set
            {
                this.position = value;
            }
        }

        public UIHPbar() { }

        public UIHPbar(IUnit unit, Vector2 barPosition, int barHeight, int scaleX, int scaleY)
        {
            currentUnit = unit;
            this.width = currentUnit.Health;
            this.height = barHeight;
            this.Position = barPosition;
            this.scaleX = scaleX;
            this.scaleY = scaleY;
        }

        public void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            bar = theContentManager.Load<Texture2D>(theAssetName);
        }

        public void Update(GameTime theGameTime)
        {
            this.width = currentUnit.Health;
            healthRect = new Rectangle((int)this.Position.X, (int)this.Position.Y, this.width * this.scaleX, this.height * this.scaleY);
        }

        public void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(bar, healthRect, Color.White);
        }

    }
}
