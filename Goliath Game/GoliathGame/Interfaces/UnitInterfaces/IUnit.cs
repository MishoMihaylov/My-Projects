using Microsoft.Xna.Framework;
using GoliathGame.Interfaces.AnimationInterfaces;

namespace GoliathGame.Interfaces
{
    public interface IUnit : IAttack, IDefence, IPosition, IMovable, IObject
    {
        int Health { get; set; }
        Rectangle Rectangle { get; set; }
        bool IsDead();
    }
}
