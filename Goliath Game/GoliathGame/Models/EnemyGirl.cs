using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GoliathGame.Models
{
    class EnemyGirl : Enemy
    {
        private const int GirlAttackAnimationFrames = 7;
        private const int GirlRungingAnimationFrames = 8;
        private const int GirlDeadAnimationFrames = 9;
        private const int GirlDeadAnimationAssetRows = 3;
        private const int GirlAttackAndRunningAnimationAssetColumns = 2;
        private const string NinjaGirlMainAssetPath = "Enemy/Girl/GirlIdleForward";
        private const string NinjaGirlIdleBackwardAssetPath = "Enemy/Girl/GirlIdleBackward";
        private const string NinjaGirlAttackForwardAssetPath = "Enemy/Girl/GirlAttackForward";
        private const string NinjaGirlAttackBackwardAssetPath = "Enemy/Girl/GirlAttackBackward";
        private const string NinjaGirlRunningForwardAssetPath = "Enemy/Girl/GirlRunningForward";
        private const string NinjaGirlRunningBackwardAssetPath = "Enemy/Girl/GirlRunningBackward";
        private const string NinjaGirlDeadAssetPath = "Enemy/Girl/GirlDeadForward";

        public EnemyGirl()
            : base()
        {
        }
        
        public override void LoadEnemyContent(ContentManager theContentManager)
        {       
            this.LoadContent(theContentManager, NinjaGirlMainAssetPath);
            this.UnitIdleForwardTexture = theContentManager.Load<Texture2D>(NinjaGirlMainAssetPath);
            this.UnitIdleBackwardTexture = theContentManager.Load<Texture2D>(NinjaGirlIdleBackwardAssetPath);
            this.UnitAttackForwardTexture = theContentManager.Load<Texture2D>(NinjaGirlAttackForwardAssetPath);
            this.UnitAttackBackwardTexture = theContentManager.Load<Texture2D>(NinjaGirlAttackBackwardAssetPath);
            this.UnitRunningForwardTexture = theContentManager.Load<Texture2D>(NinjaGirlRunningForwardAssetPath);
            this.UnitRunningBackwardTexture = theContentManager.Load<Texture2D>(NinjaGirlRunningBackwardAssetPath);
            this.UnitDeadTexture = theContentManager.Load<Texture2D>(NinjaGirlDeadAssetPath);
            base.LoadEnemyContent(theContentManager);
        }

        public override void AttackForwardAnimationUpdate(GameTime theGameTime)
        {
            int milisecondsPerFrame = (AttackSpeedDelay * 1000) / (GirlAttackAnimationFrames - 1);

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
            int milisecondsPerFrame = (AttackSpeedDelay * 1000) / (GirlAttackAnimationFrames - 1);

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
            Rectangle destinationImageRectangle = new Rectangle((int)this.Position.X - 85, (int)this.Position.Y, width, height);

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
