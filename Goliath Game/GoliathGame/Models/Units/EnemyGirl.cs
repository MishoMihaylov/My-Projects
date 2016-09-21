using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using GoliathGame.Models.Utils;

namespace GoliathGame.Models.Units
{
    public class EnemyGirl : Enemy
    {
        private const int GirlAttackAnimationFrames = 7;
        private const int GirlRungingAnimationFrames = 8;
        private const int GirlDeadAnimationFrames = 9;
        private const int GirlDeadAnimationAssetRows = 3;
        private const int GirlAttackAndRunningAnimationAssetColumns = 2;        

        public EnemyGirl()
            : base()
        {
            this.AttackSpeedDelay = 0.5;
            this.IntervalBetweenAttack = TimeSpan.FromSeconds(this.AttackSpeedDelay + 0.1);
            this.Position = new Vector2(500, 535);

        }

        public override void LoadEnemyContent(ContentManager theContentManager)
        {       
            this.LoadContent(theContentManager, Constants.GirlMainAssetPath);
            this.UnitIdleForwardTexture = theContentManager.Load<Texture2D>(Constants.GirlMainAssetPath);
            this.UnitIdleBackwardTexture = theContentManager.Load<Texture2D>(Constants.GirlIdleBackwardAssetPath);
            this.UnitAttackForwardTexture = theContentManager.Load<Texture2D>(Constants.GirlAttackForwardAssetPath);
            this.UnitAttackBackwardTexture = theContentManager.Load<Texture2D>(Constants.GirlAttackBackwardAssetPath);
            this.UnitRunningForwardTexture = theContentManager.Load<Texture2D>(Constants.GirlRunningForwardAssetPath);
            this.UnitRunningBackwardTexture = theContentManager.Load<Texture2D>(Constants.GirlRunningBackwardAssetPath);
            this.UnitDeadTexture = theContentManager.Load<Texture2D>(Constants.GirlDeadAssetPath);
            base.LoadEnemyContent(theContentManager);
        }

        public override void AttackForwardAnimationUpdate(GameTime theGameTime)
        {
            int milisecondsPerFrame = (int)(AttackSpeedDelay * 1000) / (GirlAttackAnimationFrames - 1);

            this.timeSinceLastFrame += theGameTime.ElapsedGameTime.Milliseconds;

            if (timeSinceLastFrame > milisecondsPerFrame)
            {
                this.AnimationAssetCurrentFrame++;
                if (this.AnimationAssetCurrentFrame == GirlAttackAnimationFrames - 1)
                {
                    this.AnimationAssetCurrentFrame = 0;
                }

                this.timeSinceLastFrame = 0;
            }
        }

        public override void AttackBackwardAnimationUpdate(GameTime theGameTime)
        {
            int milisecondsPerFrame = (int)(AttackSpeedDelay * 1000) / (GirlAttackAnimationFrames - 1);

            this.timeSinceLastFrame += theGameTime.ElapsedGameTime.Milliseconds;

            if (timeSinceLastFrame > milisecondsPerFrame)
            {
                this.AnimationAssetCurrentFrame++;
                if (this.AnimationAssetCurrentFrame == GirlAttackAnimationFrames - 1)
                {
                    this.AnimationAssetCurrentFrame = 0;
                }

                this.timeSinceLastFrame = 0;
            }
        }

        public override void RunningForwardAnimationUpdate(GameTime theGameTime)
        {
            int milisecondsPerFrame = 50;
            this.timeSinceLastFrame += theGameTime.ElapsedGameTime.Milliseconds;

            if (timeSinceLastFrame > milisecondsPerFrame)
            {
                this.AnimationAssetCurrentFrame++;
                if (this.AnimationAssetCurrentFrame == GirlRungingAnimationFrames - 1)
                {
                    this.AnimationAssetCurrentFrame = 0;
                }

                this.timeSinceLastFrame = 0;
            }
        }

        public override void RunningBackwardAnimationUpdate(GameTime theGameTime)
        {
            int milisecondsPerFrame = 50;
            this.timeSinceLastFrame += theGameTime.ElapsedGameTime.Milliseconds;

            if (timeSinceLastFrame > milisecondsPerFrame)
            {
                this.AnimationAssetCurrentFrame++;
                if (this.AnimationAssetCurrentFrame == GirlRungingAnimationFrames - 1)
                {
                    this.AnimationAssetCurrentFrame = 0;
                }

                this.timeSinceLastFrame = 0;
            }
        }

        public override void DeadAnimationUpdate(GameTime theGameTime)
        {
            int milisecondsPerFrame = 150;

            this.timeSinceLastFrame += theGameTime.ElapsedGameTime.Milliseconds;

            if (timeSinceLastFrame > milisecondsPerFrame)
            {
                if (this.AnimationAssetCurrentFrame != GirlDeadAnimationFrames - 1)
                {
                    this.AnimationAssetCurrentFrame++;
                }
            }
        }

        public override void AttackForwardAnimationDraw(SpriteBatch theSpriteBatch)
        {
            int width = UnitAttackForwardTexture.Width / GirlAttackAndRunningAnimationAssetColumns;
            int height = UnitAttackForwardTexture.Height / MatrixAnimationAssetRows;
            int row = (int)((float)this.AnimationAssetCurrentFrame / GirlAttackAndRunningAnimationAssetColumns);
            int column = this.AnimationAssetCurrentFrame % GirlAttackAndRunningAnimationAssetColumns;

            Rectangle sourceImageRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationImageRectangle = new Rectangle((int)this.Position.X, (int)this.Position.Y, width, height);

            theSpriteBatch.Draw(this.UnitAttackForwardTexture, destinationImageRectangle, sourceImageRectangle, Color.White);
        }

        public override void AttackBackwardAnimationDraw(SpriteBatch theSpriteBatch)
        {
            int width = UnitAttackBackwardTexture.Width / GirlAttackAndRunningAnimationAssetColumns;
            int height = UnitAttackBackwardTexture.Height / MatrixAnimationAssetRows;
            int row = (int)((float)this.AnimationAssetCurrentFrame / GirlAttackAndRunningAnimationAssetColumns);
            int column = this.AnimationAssetCurrentFrame % GirlAttackAndRunningAnimationAssetColumns;

            Rectangle sourceImageRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationImageRectangle = new Rectangle((int)this.Position.X - 35, (int)this.Position.Y, width, height);

            theSpriteBatch.Draw(this.UnitAttackBackwardTexture, destinationImageRectangle, sourceImageRectangle, Color.White);
        }

        public override void RunningForwardAnimationDraw(SpriteBatch theSpriteBatch)
        {
            int width = UnitRunningForwardTexture.Width / GirlAttackAndRunningAnimationAssetColumns;
            int height = UnitRunningForwardTexture.Height / MatrixAnimationAssetRows;
            int row = (int)((double)this.AnimationAssetCurrentFrame / GirlAttackAndRunningAnimationAssetColumns);
            int column = this.AnimationAssetCurrentFrame % GirlAttackAndRunningAnimationAssetColumns;

            Rectangle sourceImageRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationImageRectangle = new Rectangle((int)this.Position.X, (int)this.Position.Y, width, height);

            theSpriteBatch.Draw(this.UnitRunningForwardTexture, destinationImageRectangle, sourceImageRectangle, Color.White);
        }

        public override void RunningBackwardAnimationDraw(SpriteBatch theSpriteBatch)
        {
            int width = UnitRunningBackwardTexture.Width / GirlAttackAndRunningAnimationAssetColumns;
            int height = UnitRunningBackwardTexture.Height / MatrixAnimationAssetRows;
            int row = (int)((double)this.AnimationAssetCurrentFrame / GirlAttackAndRunningAnimationAssetColumns);
            int column = this.AnimationAssetCurrentFrame % GirlAttackAndRunningAnimationAssetColumns;

            Rectangle sourceImageRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationImageRectangle = new Rectangle((int)this.Position.X, (int)this.Position.Y, width, height);

            theSpriteBatch.Draw(this.UnitRunningBackwardTexture, destinationImageRectangle, sourceImageRectangle, Color.White);
        }

        public override void DeadAnimationDraw(SpriteBatch theSpriteBatch)
        {
            int width = UnitDeadTexture.Width / MartixAnimationAssetColumns;
            int height = UnitDeadTexture.Height / GirlDeadAnimationAssetRows;
            int row = (int)((double)this.AnimationAssetCurrentFrame / MartixAnimationAssetColumns);
            int column = this.AnimationAssetCurrentFrame % MartixAnimationAssetColumns;

            Rectangle sourceImageRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationImageRectangle = new Rectangle((int)this.Position.X, (int)this.Position.Y, width, height);

            theSpriteBatch.Draw(this.UnitDeadTexture, destinationImageRectangle, sourceImageRectangle, Color.White);
        }
    }
}
