using GoliathGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GoliathGame.Models
{
    class Armor : Item, IArmor
    {
        private const int armor = 2;
        private const string armorTunicAssetPath = "Items/RedTunic";

        public Armor() { }

        public void Equip(IUnit unit)
        {
            unit.Defence += armor;
        }

        public void LoadArmorContent(ContentManager theContentManager)
        {
            this.LoadContent(theContentManager, armorTunicAssetPath);
        }

        
    }
}
