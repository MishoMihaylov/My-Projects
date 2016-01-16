using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GoliathGame.Models
{
    class EnemyNinjaGirl : Enemy
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
    }
}
