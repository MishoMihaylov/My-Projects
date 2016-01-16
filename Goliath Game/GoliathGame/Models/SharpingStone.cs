using GoliathGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GoliathGame.Models
{
    class SharpingStone : Item, ISharpingStone
    {
        private const int bonusDamageFromSharping = 3;
        private const string sharpingStoneAssetPath = "Items/SharpingStone";

        public SharpingStone() { }

        public void SharpWeapon(IUnit unit)
        {
            unit.AttackDamage += bonusDamageFromSharping;
        }

        public void LoadPotionContent(ContentManager theContentManager)
        {
            this.LoadContent(theContentManager, sharpingStoneAssetPath);
        }

    }
}
