using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GoliathGame.Interfaces.EngineInterfaces;

namespace GoliathGame.Models.UI
{
    class HeroUI : ISprite
    {
        const int abilitiesSlots = 3;
        private string[] UIAssets = { "UIAttributes/AttackBar/Punch", "UIAttributes/AttackBar/Rampage", "UIAttributes/AttackBar/Ultimate" };

        List<ISprite> UIElements;

        //Rectangle barBackground = new Rectangle(0, 0, 250, 768);
        //Texture2D a = new Texture2D(GraphicsDevice, 5, 5);
        public static UIButton firstAbility = new UIButton();
        public static UIButton secondAbility = new UIButton();
        public static UIButton thirdAbility = new UIButton();

        public HeroUI()
        {
            this.UIElements = new List<ISprite>();
            this.UIElements.Capacity = abilitiesSlots;
            this.UIElements.Add(firstAbility);
            this.UIElements.Add(secondAbility);
            this.UIElements.Add(thirdAbility);
        }

        public Vector2 Position
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            int space = 25;
            for (int i = 0; i < UIElements.Count; i++)
            {
                UIElements[i].Position = new Vector2(20, 40 + space);
                UIElements[i].LoadContent(theContentManager, UIAssets[i]);
                space += 85;
            }        
        }
        public void Update(GameTime theGameTime)
        {

        }
        public void Draw(SpriteBatch theSpriteBatch)
        {
            //theSpriteBatch.Draw(barBackground, barBackground, Color.Black);
            for (int i = 0; i < UIElements.Count; i++)
            {
                UIElements[i].Draw(theSpriteBatch);
            }
        }
    }
}
