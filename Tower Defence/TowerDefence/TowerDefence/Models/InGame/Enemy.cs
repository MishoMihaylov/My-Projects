using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TowerDefence.Interfaces.InGame;
using TowerDefence.Models.Core;
using TowerDefence.Models.Unilities;

namespace TowerDefence.Models.InGame
{
    public class Enemy : GameObject, IEnemy
    {
        private int health;
        private int armor;

        public Enemy(int health = 100, int armor = 1) : base(Constants.testSubject ,new Vector2(0, 0))
        {
            this.Health = health;
            this.Armor = armor;
            
        }

        public int Health
        {
            get
            {
                return this.health;
            }

            private set
            {
                Validator.CheckIfNegativeOrZero(value, "Health");
                this.health = value;
            }
        }

        public int Armor
        {
            get
            {
                return this.armor;
            }

            private set
            {
                Validator.CheckIfNegativeOrZero(value, "Armor");
                this.armor = value;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.MainAsset, this.Position, null);
        }

        public override void LoadContent(ContentManager theContentManager)
        {
            this.MainAsset = theContentManager.Load<Texture2D>(this.MainAssetPath);
        }

        public override void UnloadContent()
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            
        }




        //Add MoveTo method
    }
}
