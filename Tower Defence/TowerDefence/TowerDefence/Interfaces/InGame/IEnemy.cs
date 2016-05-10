using TowerDefence.Interfaces.Core;

namespace TowerDefence.Interfaces.InGame
{
    public interface IEnemy
    {
        int Health { get; }
        int Armor { get; }
    }
}
