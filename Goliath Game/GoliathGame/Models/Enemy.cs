using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GoliathGame.Models.UI;

namespace GoliathGame.Models
{
    class Enemy : Unit
    {
        //Този клас има конструктор и не е абстрактен заради тестване на Engine-а 
        //private const string NinjaGirlMainAssetPath = "Enemy/NinjaGirl/NinjaGirlIdleForward";
        //private const string NinjaGirlIdleBackwardAssetPath = "Enemy/NinjaGirl/NinjaGirlIdleBackward";
        //private const string NinjaGirlAttackForwardAssetPath = "Enemy/NinjaGirl/NinjaGirlAttackForward";
        //private const string NinjaGirlAttackBackwardAssetPath = "Enemy/NinjaGirl/NinjaGirlAttackBackward";
        //private const string NinjaGirlRunningForwardAssetPath = "Enemy/NinjaGirl/NinjaNinjaGirlRunForward";
        //private const string NinjaGirlRunningBackwardAssetPath = "Enemy/NinjaGirl/NinjaGirlRunBackward";
        //private const string NinjaGirlDeadAssetPath = "Enemy/NinjaGirl/NinjaGirlDeadForward";
        private const int START_POSITION_X = 500;
        private const int START_POSITION_Y = 500;
        protected UIHPbar enemyHpBar;
        public bool readyToBeRemoved = false;

        public Enemy() 
        {
            this.enemyHpBar = new UIHPbar(this, new Vector2(this.Position.X + 50, this.Position.Y), 7, 1, 1);
        }

        public Enemy(string asset)
        {
            //this.AssetName = asset;
            this.enemyHpBar = new UIHPbar(this, new Vector2(this.Position.X + 50, this.Position.Y), 7, 1, 1);
        }

        public virtual void LoadEnemyContent(ContentManager theContentManager)
        {
            this.Position = new Vector2(START_POSITION_X, START_POSITION_Y);
            //this.LoadContent(theContentManager, NinjaGirlMainAssetPath);
            //this.UnitIdleForwardTexture = theContentManager.Load<Texture2D>(NinjaGirlMainAssetPath);
            //this.UnitIdleBackwardTexture = theContentManager.Load<Texture2D>(NinjaGirlIdleBackwardAssetPath);
            //this.UnitAttackForwardTexture = theContentManager.Load<Texture2D>(NinjaGirlAttackForwardAssetPath);
            //this.UnitAttackBackwardTexture = theContentManager.Load<Texture2D>(NinjaGirlAttackBackwardAssetPath);
            //this.UnitRunningForwardTexture = theContentManager.Load<Texture2D>(NinjaGirlRunningForwardAssetPath);
            //this.UnitRunningBackwardTexture = theContentManager.Load<Texture2D>(NinjaGirlRunningBackwardAssetPath);
            //this.UnitDeadTexture = theContentManager.Load<Texture2D>(NinjaGirlDeadAssetPath);
            this.enemyHpBar.LoadContent(theContentManager, "UIAttributes/HPBar/100");
        }

        public override void Update(GameTime theGameTime)
        {
            if (!IsDead())
            {
                UpdateState(theGameTime);
                UpdatePosition(theGameTime, this.Speed, this.Direction);
                this.enemyHpBar.Update(theGameTime);
            }
            else
            {
                DeadAnimationUpdate(theGameTime);
            }     
        }

        public override void UpdatePosition(GameTime theGameTime, Vector2 theSpeed, Vector2 theDirection)
        {
            this.Position += theDirection * theSpeed * (float)theGameTime.ElapsedGameTime.TotalSeconds;
            this.enemyHpBar.Position = new Vector2(this.Position.X + 50, this.Position.Y);
        }

        public override void Draw(SpriteBatch theSpriteBatch)
        {
            if (!this.IsDead())
            {
                DrawState(theSpriteBatch);

                this.enemyHpBar.Draw(theSpriteBatch);
                //base.Draw(theSpriteBatch);     
            }
            else
            {
                DeadAnimationDraw(theSpriteBatch);
            }
              
        }

        public override void JumpAnimationUpdate(GameTime theGameTime)
        {
            throw new NotImplementedException();
        }

        public override void JumpAnimationDraw(SpriteBatch theSpriteBatch)
        {
            throw new NotImplementedException();
        }

        public override void DeadAnimationUpdate(GameTime theGameTime)
        {
            int milisecondsPerFrame = 150;

            this.timeSinceLastFrame += theGameTime.ElapsedGameTime.Milliseconds;

            if (timeSinceLastFrame > milisecondsPerFrame)
            {

                if (this.AnimationAssetCurrentFrame == AnimationWithTenTotalFrames - 1)
                {
                        this.readyToBeRemoved = true;
                }
                else
                {
                    this.AnimationAssetCurrentFrame++;
                }
            }
        }

        public override void DeadAnimationDraw(SpriteBatch theSpriteBatch)
        {
            int width = UnitDeadTexture.Width / MartixAnimationAssetColumns;
            int height = UnitDeadTexture.Height / MatrixAnimationAssetRows;
            int row = (int)((double)this.AnimationAssetCurrentFrame / MartixAnimationAssetColumns);
            int column = this.AnimationAssetCurrentFrame % MartixAnimationAssetColumns;

            Rectangle sourceImageRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationImageRectangle = new Rectangle((int)this.Position.X, (int)this.Position.Y, width, height);

            theSpriteBatch.Draw(this.UnitDeadTexture, destinationImageRectangle, sourceImageRectangle, Color.White);
        }    
    }
}