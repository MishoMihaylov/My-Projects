using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GoliathGame.Models
{
    public class EnemyNinjaGirl : Enemy
    {
        private const string NinjaGirlMainAssetPath = "Enemy/NinjaGirl/NinjaGirlIdleForward";
        private const string NinjaGirlIdleBackwardAssetPath = "Enemy/NinjaGirl/NinjaGirlIdleBackward";
        private const string NinjaGirlAttackForwardAssetPath = "Enemy/NinjaGirl/NinjaGirlAttackForward";
        private const string NinjaGirlAttackBackwardAssetPath = "Enemy/NinjaGirl/NinjaGirlAttackBackward";
        private const string NinjaGirlRunningForwardAssetPath = "Enemy/NinjaGirl/NinjaNinjaGirlRunForward";
        private const string NinjaGirlRunningBackwardAssetPath = "Enemy/NinjaGirl/NinjaGirlRunBackward";
        private const string NinjaGirlDeadAssetPath = "Enemy/NinjaGirl/NinjaGirlDeadForward";

        public EnemyNinjaGirl()
            : base()
        {
            this.AttackSpeedDelay = 0.3;
            this.IntervalBetweenAttack = TimeSpan.FromSeconds(this.AttackSpeedDelay + 0.1);
            this.enemyHpBar.Position = new Vector2(this.Position.X + 100, this.Position.Y);
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
            this.Position = new Vector2(1300, 530);
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
