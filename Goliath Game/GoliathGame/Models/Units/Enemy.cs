using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GoliathGame.Models.UI;
using GoliathGame.Models.Utils;

namespace GoliathGame.Models.Units
{
    public abstract class Enemy : Unit
    {
        public bool readyToBeRemoved = false;
        protected UIHPbar enemyHpBar;
        
        protected Enemy() 
        {
            this.enemyHpBar = new UIHPbar(this, new Vector2(this.Position.X + 50, this.Position.Y), 7, 1, 1);
        }

        public virtual void LoadEnemyContent(ContentManager theContentManager)
        {
            this.enemyHpBar.LoadContent(theContentManager, Constants.HealthBarPath);
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