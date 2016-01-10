using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GoliathGame.Models.UI;

namespace GoliathGame.Models
{
    class Enemy : Unit
    {
        //Този клас има конструктор и не е абстрактен заради тестване на Engine-а 
        private const int START_POSITION_X = 500;
        private const int START_POSITION_Y = 500;
        UIHPbar enemyHpBar;

        public Enemy() { }

        public Enemy(string asset)
        {
            this.AssetName = asset;
            this.enemyHpBar = new UIHPbar(this, new Vector2(this.Position.X + 50, this.Position.Y), 7, 1, 1);                  
        }

        public void LoadEnemyContent(ContentManager theContentManager)
        {
            this.Position = new Vector2(START_POSITION_X, START_POSITION_Y);
            this.LoadContent(theContentManager, this.AssetName);
            this.enemyHpBar.LoadContent(theContentManager, "UIAttributes/HPBar/100");
        }

        public override void Update(GameTime theGameTime)
        {
            UpdatePosition(theGameTime, this.Speed, this.Direction);
            this.enemyHpBar.Update(theGameTime);
        }

        public override void UpdatePosition(GameTime theGameTime, Vector2 theSpeed, Vector2 theDirection)
        {
            this.Position += theDirection * theSpeed * (float)theGameTime.ElapsedGameTime.TotalSeconds;
            this.enemyHpBar.Position = new Vector2(this.Position.X + 50, this.Position.Y);
        }

        public override void Draw(SpriteBatch theSpriteBatch)
        {
            this.enemyHpBar.Draw(theSpriteBatch);
            base.Draw(theSpriteBatch);         
        }


        public override void IdleForwardAnimationDraw(SpriteBatch theSpriteBatch)
        {
            throw new NotImplementedException();
        }

        public override void IdleForwardAnimationUpdate(GameTime theGameTime)
        {
            throw new NotImplementedException();
        }

        public override void AttackAnimationDraw(SpriteBatch theSpriteBatch)
        {
            throw new NotImplementedException();
        }

        public override void AttackAnimationUpdate(GameTime theGameTime)
        {
            throw new NotImplementedException();
        }

        public override void JumpAnimationDraw(SpriteBatch theSpriteBatch)
        {
            throw new NotImplementedException();
        }

        public override void JumpAnimationUpdate(GameTime theGameTime)
        {
            throw new NotImplementedException();
        }

        public override void DeadAnimationDraw(SpriteBatch theSpriteBatch)
        {
            throw new NotImplementedException();
        }

        public override void DeadAnimationUpdate(GameTime theGameTime)
        {
            throw new NotImplementedException();
        }

        public override void RunningForwardAnimationUpdate(GameTime theGameTime)
        {
            throw new NotImplementedException();
        }

        public override void RunningForwardAnimationDraw(SpriteBatch theSpriteBatch)
        {
            throw new NotImplementedException();
        }

        public override void RunningBackwardAnimationUpdate(GameTime theGameTime)
        {
            throw new NotImplementedException();
        }

        public override void RunningBackwardAnimationDraw(SpriteBatch theSpriteBatch)
        {
            throw new NotImplementedException();
        }

        public override void IdleBackwardAnimationDraw(SpriteBatch theSpriteBatch)
        {
            throw new NotImplementedException();
        }

        public override void IdleBackwardAnimationUpdate(GameTime theGameTime)
        {
            throw new NotImplementedException();
        }
    }
}