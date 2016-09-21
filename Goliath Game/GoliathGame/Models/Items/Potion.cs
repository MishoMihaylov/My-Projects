using GoliathGame.Interfaces;
using Microsoft.Xna.Framework.Content;

namespace GoliathGame.Models.Items
{
    class Potion : Item, IHealable
    {
        private const int potionAmountOfHealing = 25;
        private const string potionAssetPath = "Items/HealthPotion";

        public Potion() { }

        public void Heal(IUnit unitToBeHealed)
        {
            if (unitToBeHealed.Health + potionAmountOfHealing >= 100)
            {
                unitToBeHealed.Health = 100;
            }
            else
            {
                unitToBeHealed.Health += potionAmountOfHealing;
            }
        }
        public void LoadPotionContent(ContentManager theContentManager)
        {
            this.LoadContent(theContentManager, potionAssetPath);
        }
    }
}
