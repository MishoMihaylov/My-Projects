using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GoliathGame.Models.Utils;

namespace GoliathGame.Models.Units
{
    public class EnemyNinjaGirl : Enemy
    {
        public EnemyNinjaGirl()
            : base()
        {
            this.AttackSpeedDelay = 0.3;
            this.IntervalBetweenAttack = TimeSpan.FromSeconds(this.AttackSpeedDelay + 0.1);
            this.enemyHpBar.Position = new Vector2(this.Position.X + 100, this.Position.Y);
            this.Position = new Vector2(300, 535);
        }

        public override void LoadEnemyContent(ContentManager theContentManager)
        {       
            this.LoadContent(theContentManager, Constants.NinjaGirlMainAssetPath);
            this.UnitIdleForwardTexture = theContentManager.Load<Texture2D>(Constants.NinjaGirlMainAssetPath);
            this.UnitIdleBackwardTexture = theContentManager.Load<Texture2D>(Constants.NinjaGirlIdleBackwardAssetPath);
            this.UnitAttackForwardTexture = theContentManager.Load<Texture2D>(Constants.NinjaGirlAttackForwardAssetPath);
            this.UnitAttackBackwardTexture = theContentManager.Load<Texture2D>(Constants.NinjaGirlAttackBackwardAssetPath);
            this.UnitRunningForwardTexture = theContentManager.Load<Texture2D>(Constants.NinjaGirlRunningForwardAssetPath);
            this.UnitRunningBackwardTexture = theContentManager.Load<Texture2D>(Constants.NinjaGirlRunningBackwardAssetPath);
            this.UnitDeadTexture = theContentManager.Load<Texture2D>(Constants.NinjaGirlDeadAssetPath);
            base.LoadEnemyContent(theContentManager);
        }

        public override void AttackForwardAnimationUpdate(GameTime theGameTime)
        {
            int milisecondsPerFrame = (int)(AttackSpeedDelay * 1000) / (AnimationWithTenTotalFrames - 1);

            this.timeSinceLastFrame += theGameTime.ElapsedGameTime.Milliseconds;

            if (timeSinceLastFrame > milisecondsPerFrame)
            {
                this.AnimationAssetCurrentFrame++;
                if (this.AnimationAssetCurrentFrame == AnimationWithTenTotalFrames - 1)
                {
                    this.AnimationAssetCurrentFrame = 0;
                }

                this.timeSinceLastFrame = 0;
            }
        }

        public override void AttackBackwardAnimationUpdate(GameTime theGameTime)
        {
            int milisecondsPerFrame = (int)(AttackSpeedDelay * 1000) / (AnimationWithTenTotalFrames - 1);

            this.timeSinceLastFrame += theGameTime.ElapsedGameTime.Milliseconds;

            if (timeSinceLastFrame > milisecondsPerFrame)
            {
                this.AnimationAssetCurrentFrame++;
                if (this.AnimationAssetCurrentFrame == AnimationWithTenTotalFrames - 1)
                {
                    this.AnimationAssetCurrentFrame = 0;
                }

                this.timeSinceLastFrame = 0;
            }
        }     
    }
}
